using System;
using System.Collections.Generic;
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
                //IBoardId boirdId = "dsfsdf";
                //var cards = trello.Cards.for
                /*var cards = trello.Cards.ForBoard(new BoardId(boards.Id));
                for(int y = 0; y != cards.Count(); y++)
                {
                    Card card = cards.ElementAt(y);
                    cardModel = boardModel[i].CardModel;
                    if (cardModel == null)
                        cardModel = new BindingList<CardModel>();
                    cardModel.Add(new CardModel() { Id = card.Id, IdBoard = card.IdBoard, Name = card.Name });
                    var Lists = trello.Lists.ForCard(new CardId(card.Id));
                    //for(int z = 0; z != Lists.)
                    boardModel[i].CardModel = cardModel;
                }*/
                //var Lists = trello.Lists.ForBoard(new BoardId(boards.Id));
                //for (int y = 0; y != Lists.Count(); y++)
                //{
                //    List list = Lists.ElementAt(y);
                //    listModel = boardModel[i].ListModel;
                //    if (listModel == null)
                //        listModel = new BindingList<ListModel>();
                //    listModel.Add(new ListModel() { Id = list.Id, IdBoard = list.IdBoard, Name = list.Name });
                //    var cards = trello.Cards.ForList(new ListId(list.Id));
                //    for(int z = 0; z != cards.Count(); z++)
                //    {
                //        Card card = cards.ElementAt(z);
                //        cardModel = listModel[y].CardModel;
                //        if (cardModel == null)
                //            cardModel = new BindingList<CardModel>();
                //        cardModel.Add(new CardModel() { Id = card.Id, IdBoard = card.IdBoard, IdList = card.IdList, Name = card.Name, Desc = card.Desc });
                //        listModel[y].CardModel = cardModel;
                //    }
                //    if(cards.Count() == 0)
                //    {
                //        cardModel = listModel[y].CardModel;
                //        if (cardModel == null)
                //            cardModel = new BindingList<CardModel>();
                //        cardModel.Add(new CardModel() { IdBoard = boards.Id, IdList = list.Id, Name = "Пусто" });
                //        listModel[y].CardModel = cardModel;
                //    }
                //    boardModel[i].ListModel = listModel;
                //}
                //if(Lists.Count() == 0)
                //{
                //    listModel = boardModel[i].ListModel;
                //    if (listModel == null)
                //        listModel = new BindingList<ListModel>();
                //    listModel.Add(new ListModel());
                //    cardModel = listModel[0].CardModel;
                //    if (cardModel == null)
                //        cardModel = new BindingList<CardModel>();
                //    cardModel.Add(new CardModel());
                //    listModel[0].CardModel = cardModel;
                //    boardModel[i].ListModel = listModel;

                //}
            }
            
            //System.Collections.Generic.IEnumerable<Organization> organization = trello.Organizations.ForMe();

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
        //public static TrelloViewModelOld()
        //{
        //    ITrello trello = new Trello("b9d8a6a9f2aaa03e9b6426c6c135a689");
        //    //var url = trello.GetAuthorizationUrl("TempTrelloApi", Scope.ReadWrite, Expiration.Never);
        //    trello.Authorize("9180c1adedfd5f7a48153af5c7beed8b45458df229f5262887beb6e4b9e16027");
        //    var board = trello.Members.Me();
        //    /*boardModel = new BindingList<BoardModel>();
        //    boardModel.Add(new BoardModel() { Id = "1", Desc = "Описание 1", Name = "Тестовое описание 1" });
        //    cardModel = boardModel[0].CardModel;
        //    if (cardModel == null)
        //        cardModel = new BindingList<CardModel>();
        //    cardModel.Add(new CardModel() { Id = "11", IdBoard = "1", Name = "Карточка 11" });
        //    cardModel.Add(new CardModel() { Id = "12", IdBoard = "1", Name = "Карточка 12" });
        //    cardModel.Add(new CardModel() { Id = "13", IdBoard = "1", Name = "Карточка 13" });
        //    boardModel[0].CardModel = cardModel;
        //    listModel = boardModel[0].CardModel[0].ListModel;
        //    if (listModel == null)
        //        listModel = new BindingList<ListModel>();
        //    listModel.Add(new ListModel() { Id = "111", Name = "Tested ListModel 1", IdCard = "11", IdBoard="1"});
        //    listModel.Add(new ListModel() { Id = "112", Name = "Tested ListModel 2", IdCard = "11", IdBoard = "1" });
        //    listModel.Add(new ListModel() { Id = "113", Name = "Tested ListModel 3", IdCard = "11", IdBoard = "1" });
        //    boardModel[0].CardModel[0].ListModel = listModel;

        //    listModel = boardModel[0].CardModel[2].ListModel;
        //    if (listModel == null)
        //        listModel = new BindingList<ListModel>();
        //    listModel.Add(new ListModel() { Id = "131", Name = "Tested ListModel 1", IdCard = "13", IdBoard = "1" });
        //    listModel.Add(new ListModel() { Id = "132", Name = "Tested ListModel 2", IdCard = "13", IdBoard = "1" });
        //    //listModel.Add(new ListModel() { Id = "123", Name = "Tested ListModel 3" });
        //    boardModel[0].CardModel[2].ListModel = listModel;*/


        //    boardModel.Add(new BoardModel() { Id = "2", Desc = "Описание 2", Name = "Тестовое описание 2" });
        //    boardModel.Add(new BoardModel() { Id = "3", Desc = "Описание 3", Name = "Тестовое описание 3" });
        //    cardModel = boardModel[2].CardModel;
        //    if (cardModel == null)
        //        cardModel = new BindingList<CardModel>();
        //    cardModel.Add(new CardModel() { Id = "31", IdBoard = "3", Name = "Карточка 31" });
        //    cardModel.Add(new CardModel() { Id = "32", IdBoard = "3", Name = "Карточка 32" });
        //    cardModel.Add(new CardModel() { Id = "33", IdBoard = "3", Name = "Карточка 33" });


        //    /*cardModel.Add(new CardModel() { Id = "34", IdList = "3", Name = "Карточка 34" });
        //    cardModel.Add(new CardModel() { Id = "35", IdList = "3", Name = "Карточка 35" });
        //    cardModel.Add(new CardModel() { Id = "36", IdList = "3", Name = "Карточка 36" });
        //    cardModel.Add(new CardModel() { Id = "37", IdList = "3", Name = "Карточка 37" });
        //    cardModel.Add(new CardModel() { Id = "31", IdList = "3", Name = "Карточка 31" });
        //    cardModel.Add(new CardModel() { Id = "32", IdList = "3", Name = "Карточка 32" });
        //    cardModel.Add(new CardModel() { Id = "33", IdList = "3", Name = "Карточка 33" });
        //    cardModel.Add(new CardModel() { Id = "34", IdList = "3", Name = "Карточка 34" });
        //    cardModel.Add(new CardModel() { Id = "35", IdList = "3", Name = "Карточка 35" });
        //    cardModel.Add(new CardModel() { Id = "36", IdList = "3", Name = "Карточка 36" });
        //    cardModel.Add(new CardModel() { Id = "37", IdList = "3", Name = "Карточка 37" });*/

        //    boardModel[2].CardModel = cardModel;

        //    listModel = boardModel[2].CardModel[0].ListModel;
        //    if (listModel == null)
        //        listModel = new BindingList<ListModel>();
        //    listModel.Add(new ListModel() { Id = "111", Name = "Tested ListModel 1", IdCard = "31" });
        //    listModel.Add(new ListModel() { Id = "112", Name = "Tested ListModel 2", IdCard = "31" });
        //    listModel.Add(new ListModel() { Id = "113", Name = "Tested ListModel 3", IdCard = "31" });
        //    boardModel[2].CardModel[0].ListModel = listModel;
        //}
    }
}
