using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace tempTrello
{
    /// <summary>
    /// Логика взаимодействия для Parameters.xaml
    /// </summary>
    public partial class Parameters : Window
    {
        public Parameters()
        {
            InitializeComponent();
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (currentConfig.AppSettings.Settings["AppKey"] == null)
                currentConfig.AppSettings.Settings.Add(new KeyValueConfigurationElement("AppKey", ""));
            if (currentConfig.AppSettings.Settings["Token"] == null)
                currentConfig.AppSettings.Settings.Add(new KeyValueConfigurationElement("Token", ""));
            if (currentConfig.AppSettings.Settings["User"] == null)
                currentConfig.AppSettings.Settings.Add(new KeyValueConfigurationElement("User", ""));
            if (currentConfig.AppSettings.Settings["Password"] == null)
                currentConfig.AppSettings.Settings.Add(new KeyValueConfigurationElement("Password", ""));
            if (currentConfig.AppSettings.Settings["Domian"] == null)
                currentConfig.AppSettings.Settings.Add(new KeyValueConfigurationElement("Domian", ""));
            AppKey.Text = currentConfig.AppSettings.Settings["AppKey"].Value;
            Token.Text = currentConfig.AppSettings.Settings["Token"].Value;
            User.Text = currentConfig.AppSettings.Settings["User"].Value;
            if (currentConfig.AppSettings.Settings["Password"].Value != "")
            {
                Decryptor dec = new Decryptor(currentConfig.AppSettings.Settings["Password"].Value);
                Password.Text = dec.DescryptStr;
            }
            Domian.Text = currentConfig.AppSettings.Settings["Domian"].Value;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }

        private void Closed_Click(object sender, RoutedEventArgs e)
        {
            Encryptor encryptor = new Encryptor(Password.Text);
            string str = encryptor.EncryptStr;
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings["Token"].Value = Token.Text;
            currentConfig.AppSettings.Settings["User"].Value = User.Text;
            currentConfig.AppSettings.Settings["Password"].Value = str;
            currentConfig.AppSettings.Settings["Domian"].Value = Domian.Text;
            currentConfig.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
            this.Close();
        }
    }
    class Decryptor
    {
        string descryPass;
        string descryString;

        public Decryptor()
        {
            descryPass = descryString;
        }
        public Decryptor(string encrStr)
        {
            descryPass = CreSap();
            descryString = Decrypt(encrStr);
        }
        public string DescryptStr
        {
            get { return descryString; }
            set { descryString = Decrypt(value); }

        }

        double SimAlgEncry(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else
            {
                if (n == 1)
                {
                    return 1;
                }
                else
                {
                    return SimAlgEncry(n - 1) + SimAlgEncry(n - 2);
                }
            }
        }
        string CreSap()
        {
            return Math.Pow(SimAlgEncry(10), 3).ToString();
        }
        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="crypt_str"></param>
        /// <returns></returns>

        string Decrypt(string crypt_str)
        {
            // Получаем массив байт
            byte[] crypt_data = Convert.FromBase64String(crypt_str);

            // Алгоритм 
            SymmetricAlgorithm sa_out = Rijndael.Create();
            // Объект для преобразования данных
            ICryptoTransform ct_out = sa_out.CreateDecryptor(
                (new PasswordDeriveBytes(descryPass, null)).GetBytes(16),
                new byte[16]);
            // Поток
            MemoryStream ms_out = new MemoryStream(crypt_data);
            // Расшифровываем поток
            CryptoStream cs_out = new CryptoStream(ms_out, ct_out, CryptoStreamMode.Read);
            // Создаем строку
            StreamReader sr_out = new StreamReader(cs_out);
            string source_out = sr_out.ReadToEnd();
            return source_out;
        }
    }
    class Encryptor
    {
        string encryPass;
        string encryString;



        public Encryptor()
        {
            encryPass = CreSap();
        }

        public Encryptor(string encrStr)
        {
            encryPass = CreSap();
            encryString = Encrypt(encrStr);
        }
        public string EncryptStr
        {
            get { return encryString; }
            set { encryString = Encrypt(value); }

        }

        double SimAlgEncry(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else
            {
                if (n == 1)
                {
                    return 1;
                }
                else
                {
                    return SimAlgEncry(n - 1) + SimAlgEncry(n - 2);
                }
            }
        }
        string CreSap()
        {
            return Math.Pow(SimAlgEncry(10), 3).ToString();
        }
        /// <summary>
        /// Encrypt string
        /// </summary>
        /// <param name="source_str"></param>
        /// <returns></returns>
        string Encrypt(string source_str)
        {
            // Получаем из строки набор байт, которые будем шифровать
            byte[] source_data = Encoding.UTF8.GetBytes(source_str);
            // Алгоритм 
            SymmetricAlgorithm sa_in = Rijndael.Create();
            // Объект для преобразования данных
            ICryptoTransform ct_in = sa_in.CreateEncryptor(
                (new PasswordDeriveBytes(encryPass, null)).GetBytes(16), new byte[16]);
            // Поток
            MemoryStream ms_in = new MemoryStream();
            // Шифровальщик потока
            CryptoStream cs_in = new CryptoStream(ms_in, ct_in, CryptoStreamMode.Write);
            // Записываем шифрованные данные в поток
            cs_in.Write(source_data, 0, source_data.Length);
            cs_in.FlushFinalBlock();
            // Создаем строку
            return Convert.ToBase64String(ms_in.ToArray());
            // Выводим зашифрованную строку

        }
        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="crypt_str"></param>
        /// <returns></returns>
        string Decrypt(string crypt_str)
        {
            // Получаем массив байт
            byte[] crypt_data = Convert.FromBase64String(crypt_str);

            // Алгоритм 
            SymmetricAlgorithm sa_out = Rijndael.Create();
            // Объект для преобразования данных
            ICryptoTransform ct_out = sa_out.CreateDecryptor(
                (new PasswordDeriveBytes(encryPass, null)).GetBytes(16),
                new byte[16]);
            // Поток
            MemoryStream ms_out = new MemoryStream(crypt_data);
            // Расшифровываем поток
            CryptoStream cs_out = new CryptoStream(ms_out, ct_out, CryptoStreamMode.Read);
            // Создаем строку
            StreamReader sr_out = new StreamReader(cs_out);
            string source_out = sr_out.ReadToEnd();
            return source_out;
        }
    }
}
