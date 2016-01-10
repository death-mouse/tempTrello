using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tempTrello.Model;
using TrelloNet;


namespace tempTrello.View
{
    class TrelloViewModel
    {
        /// <summary>
        /// The required design width
        /// </summary>
        private const double RequiredWidth = 1920;

        /// <summary>
        /// The required design height
        /// </summary>
        private const double RequiredHeight = 1080;
        Browser browser;
        /// <summary>
        /// Добавление новых задач
        /// </summary>
        /// <param name="listId"> Id листа в которую нужно добавить задачи</param>
        /// <param name="_tasksId">Список задач через "," которые нужно добавить </param>
        public void addTask(string listId, string _tasksId)
        {
            ITrello trello = new Trello("b9d8a6a9f2aaa03e9b6426c6c135a689");
            trello.Authorize("9180c1adedfd5f7a48153af5c7beed8b45458df229f5262887beb6e4b9e16027");
            List list = trello.Lists.WithId(listId);
            string[] tasksId = _tasksId.Split(new char[] { ',' });
            BoardModel borad = findBoardModel(boardModel, list.IdBoard);
            ListModel listMod = findListModel(borad.ListModel, list.Id);
            BindingList<CardModel> cardModel = listMod.CardModel;
            foreach (string taskId in tasksId)
            {
                //trello.Cards.Add()
                NewCard card = new NewCard(taskId, new ListId(list.Id));
                /*card.Name = taskId;
                card.IdBoard = list.IdBoard;
                card.IdList = list.Id;*/
                Card cardAdd = trello.Cards.Add(card);
                cardModel.Add( new CardModel() {Id = cardAdd.Id, IdBoard = cardAdd.IdBoard, IdList = cardAdd.IdList, Name = cardAdd.Name, Desc = cardAdd.Desc != "" ? cardAdd.Desc : "Пусто" });

            }
            //listMod.CardModel = cardModel;
            //borad.ListModel.Add(listMod);
            //boardModel.Add(borad);
        }
        private BoardModel findBoardModel(BindingList<BoardModel> boardModel, string _idBoard)
        {
            ObservableCollection<BoardModel> filtererd = new ObservableCollection<tempTrello.Model.BoardModel>(boardModel.Where(t => t.Id == _idBoard)); ;
            return filtererd[0];
        }
        private CardModel findCardModel(BindingList<CardModel> cardModel, string _idCard)
        {
            ObservableCollection<CardModel> filtererd = new ObservableCollection<CardModel>(cardModel.Where(t => t.Id == _idCard)); ;
            return filtererd[0];
        }
        private ListModel findListModel(BindingList<ListModel> listModel, string _idList)
        {
            ObservableCollection<ListModel> filtererd = new ObservableCollection<ListModel>(listModel.Where(t => t.Id == _idList)); ;
            return filtererd[0];
        }
        public BindingList<BoardModel> boardModel { get; set; }
        public BindingList<BoardModel> boardModelCheckBox { get; set; }
        public BindingList<CardModel> cardModel { get; set; }
        public BindingList<ListModel> listModel { get; set; }
        public  TrelloViewModel()
        {
            //return;
            ITrello trello = new Trello("b9d8a6a9f2aaa03e9b6426c6c135a689");
            trello.Authorize("9180c1adedfd5f7a48153af5c7beed8b45458df229f5262887beb6e4b9e16027");
            boardModel = new BindingList<BoardModel>();

            /*var test = trello.Members.Me();
            System.Collections.Generic.IEnumerable<Organization> organization = trello.Organizations.ForMe();
            Organization org = organization.ElementAt(0);*/

            var board = trello.Boards.ForMe();
            for (int i = 0; i != board.Count(); i++)
            {
                Board boards = board.ElementAt(i);
                if (boardModelCheckBox == null)
                    boardModelCheckBox = new BindingList<BoardModel>();
                boardModelCheckBox.Add(new BoardModel() { Id = boards.Id, Name = boards.Name, Desc = boards.Desc });
            }

            browser = new Browser();
            string document = browser.GET("http://servicedesk.gradient.ru/", Encoding.UTF8);
            if(document == null)
                document = browser.GET("http://servicedesk.gradient.ru/", Encoding.UTF8);


        }
        public void getBoardListAndCard(string _boardId)
        {
            ITrello trello = new Trello("b9d8a6a9f2aaa03e9b6426c6c135a689");
            trello.Authorize("9180c1adedfd5f7a48153af5c7beed8b45458df229f5262887beb6e4b9e16027");
            Board boards = trello.Boards.WithId(_boardId);
            if (boardModel == null)
                boardModel = new BindingList<BoardModel>();
            else
                boardModel.Clear();

            boardModel.Add(new BoardModel() { Id = boards.Id, Name = boards.Name, Desc = boards.Desc });
            var Lists = trello.Lists.ForBoard(new BoardId(boards.Id));
            for (int y = 0; y != Lists.Count(); y++)
            {
                List list = Lists.ElementAt(y);
                listModel = boardModel[0].ListModel;
                if (listModel == null)
                    listModel = new BindingList<ListModel>();
                listModel.Add(new ListModel() { Id = list.Id, IdBoard = list.IdBoard, Name = list.Name });
                var cards = trello.Cards.ForList(new ListId(list.Id));
                for (int z = 0; z != cards.Count(); z++)
                {
                    Card card = cards.ElementAt(z);
                    cardModel = listModel[y].CardModel;
                    if (cardModel == null)
                        cardModel = new BindingList<CardModel>();
                    cardModel.Add(new CardModel() { Id = card.Id, IdBoard = card.IdBoard, IdList = card.IdList, Name = card.Name, Desc = card.Desc != "" ? card.Desc : "Пусто" });
                    listModel[y].CardModel = cardModel;
                }
                if (cards.Count() == 0)
                {
                    cardModel = listModel[y].CardModel;
                    if (cardModel == null)
                        cardModel = new BindingList<CardModel>();
                    cardModel.Add(new CardModel() { IdBoard = boards.Id, IdList = list.Id, Name = "Пусто", Desc = "Пусто" });
                    listModel[y].CardModel = cardModel;
                }
                boardModel[0].ListModel = listModel;
            }
            if (Lists.Count() == 0)
            {
                listModel = boardModel[0].ListModel;
                if (listModel == null)
                    listModel = new BindingList<ListModel>();
                listModel.Add(new ListModel());
                cardModel = listModel[0].CardModel;
                if (cardModel == null)
                    cardModel = new BindingList<CardModel>();
                cardModel.Add(new CardModel());
                listModel[0].CardModel = cardModel;
                boardModel[0].ListModel = listModel;

            }

        }
        
    }
}
