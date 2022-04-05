using Microsoft.AspNetCore.SignalR;                                       
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnoLike.Classes;

namespace UnoLike.Hubs
{
    public class ChatHub : Hub                                           
    {
        public static Deck deck;

        public static List<Player> players = new List<Player>(); 

        public static List<int> currentValsToSend = new List<int>(){ 1, 2, 3, 4, 5, 6, 7 };

        public override Task OnConnectedAsync()
        {
            Player partialPlayer = new Player() { connectionId = Context.ConnectionId };
            players.Add(partialPlayer);
            //Console.WriteLine("Joueurs : " + players.Count);
            SendConnectedState();
            if(players.Count == 4)
            {
                SendHasEnoughPlayers();
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Player playerToDelete = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
            players.Remove(playerToDelete);
            //Console.WriteLine("Joueurs : " + players.Count);
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendConnectedState()
        {
            Console.WriteLine(Context.ConnectionId);
            return Clients.Client(Context.ConnectionId).SendAsync("ConnectedState", true);
        }

        public Task SendHasEnoughPlayers()
        {
            currentValsToSend = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            return Clients.All.SendAsync("HasEnoughPlayers", true);
        }

        public Task SendName(string name)              
        {
            Player playerToFill = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
            playerToFill.name = name;
            Console.WriteLine(string.Join("\n", players));
            return Clients.All.SendAsync("PlayerName", name);
        }

        public void CreateGame()
        {
            CreateDeck();
            
            ChooseCardMax("blue", currentValsToSend, players[0].connectionId);
        }

        public Task ChooseCardMax(string color, List<int> values, string connId)
        {
            return Clients.Client(connId).SendAsync("ChooseCardMax", color, values);
        }

        public void CreateDeck()
        {
            deck = new Deck();
            deck.initializeDeck();
        }

        public Task PassTurn()
        {
            foreach(Player p in players)
            {
                Console.WriteLine(p.ToString() + ", Actuellement : " + Context.ConnectionId);
            }
            Player currentPlayer = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
            int index = players.IndexOf(currentPlayer);
            Console.WriteLine("Index avant incrément : " + index);
            string nextName = "";
            if(index == 3)
            {
                nextName = players[0].name;
            }
            else
            {
                index++;
                Console.WriteLine("Index après incrément : " + index);
                nextName = players[index].name;
            }
            return Clients.All.SendAsync("PlayerNameTurn", nextName);
        }

        public Task ManageMaxColors(string color, int choice)
        {

            Player maxColorToAdd = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
            List<Player> currentList = null;

            if (color == "blue")
            {
                maxColorToAdd.maxBlue = choice;
                currentList = players.Where(pl => pl.maxBlue != 0).ToList();
            }
            else if (color == "red")
            {
                maxColorToAdd.maxRed = choice;
                currentList = players.Where(pl => pl.maxRed != 0).ToList();
            }
            else if (color == "green")
            {
                maxColorToAdd.maxGreen = choice;
                currentList = players.Where(pl => pl.maxGreen != 0).ToList();
            }
            else
            {
                maxColorToAdd.maxYellow = choice;
                currentList = players.Where(pl => pl.maxYellow != 0).ToList();
            }

            afficherList(currentList);

            CheckNextPlayer(color, currentList);

            return Clients.All.SendAsync("SendColorsToAll", maxColorToAdd.name, color, choice);
        }


        public void CheckNextPlayer(string color, List<Player> cList)
        {
            if(color == "blue")
            {
                if(cList.Count < 4)
                {
                    Player playerToLocate = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
                    
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = "";
                    if (pos < 3)
                    {
                        pos++;
                        nextConnId = players[pos].connectionId;
                    }
                    else
                    {
                        pos = 0;
                        nextConnId = players[pos].connectionId;
                    }
                    currentValsToSend = customizeVals(color, players[pos]);
                    ChooseCardMax(color, currentValsToSend,nextConnId);
                       
                }
                else
                {
                    Player playerToLocate = cList.OrderBy(pl => pl.maxBlue).FirstOrDefault();
                    currentValsToSend = customizeVals("red", playerToLocate);
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = players[pos].connectionId;
                    ChooseCardMax("red", currentValsToSend, nextConnId);
                }
            }
            if (color == "red")
            {
                if (cList.Count < 4)
                {
                    Player playerToLocate = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
                    
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = "";
                    if (pos < 3)
                    {
                        pos++;
                        nextConnId = players[pos].connectionId;
                    }
                    else
                    {
                        pos = 0;
                        nextConnId = players[pos].connectionId;
                    }
                    currentValsToSend = customizeVals(color, players[pos]);
                    ChooseCardMax(color, currentValsToSend, nextConnId);
                }
                else
                {
                    Player playerToLocate = cList.OrderBy(pl => pl.maxRed).FirstOrDefault();
                    currentValsToSend = customizeVals("green", playerToLocate);
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = players[pos].connectionId;
                    ChooseCardMax("green", currentValsToSend, nextConnId);
                }
            }
            if (color == "green")
            {
                if (cList.Count < 4)
                {
                    Player playerToLocate = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = "";
                    if (pos < 3)
                    {
                        pos++;
                        nextConnId = players[pos].connectionId;
                    }
                    else
                    {
                        pos = 0;
                        nextConnId = players[pos].connectionId;
                    }
                    currentValsToSend = customizeVals(color, players[pos]);
                    ChooseCardMax(color, currentValsToSend, nextConnId);
                }
                else
                {
                    Player playerToLocate = cList.OrderBy(pl => pl.maxGreen).FirstOrDefault();
                    currentValsToSend = customizeVals("yellow", playerToLocate);
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = players[pos].connectionId;
                    ChooseCardMax("yellow", currentValsToSend, nextConnId);
                }
            }
            if (color == "yellow")
            {
                if (cList.Count < 4)
                {
                    Player playerToLocate = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
                    int pos = players.IndexOf(playerToLocate);
                    string nextConnId = "";
                    if (pos < 3)
                    {
                        pos++;
                        nextConnId = players[pos].connectionId;
                    }
                    else
                    {
                        pos = 0;
                        nextConnId = players[pos].connectionId;
                    }
                    currentValsToSend = customizeVals(color, players[pos]);
                    ChooseCardMax(color, currentValsToSend, nextConnId);
                }
                else
                {
                    //Traitement post choix
                    foreach (Player p in players)
                    {
                        p.hand = new Deck();
                        for(int i = 0; i < 7; i++)
                        {
                            p.hand.cardList.Add(deck.cardList[0]);
                            deck.cardList.RemoveAt(0);
                        }
                    }

                    SendAllCards();
                }
            }
        }

        public Task SendAllCards()
        {
            List<string> playerNames = new List<string>();
            List<List<int>> hands = new List<List<int>>();

            foreach(Player p in players)
            {
                playerNames.Add(p.name);
                hands.Add(p.hand.cardList);
            }
            return Clients.All.SendAsync("SendCardsToAll",playerNames,hands);
        }

        public List<int> customizeVals(string color, Player cPl)
        {
            List<int> listToReturn = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            List<int> numsToRemove = new List<int>();

            Console.WriteLine(cPl);

            if (color == "blue")
            {
                foreach(Player p in players)
                {
                    numsToRemove.Add(p.maxBlue);
                }
            }
            else if(color == "red")
            {
                foreach (Player p in players)
                {
                    numsToRemove.Add(p.maxRed);
                }
                foreach(int v in listToReturn)
                {
                    if(cPl.maxBlue + v > 11)
                    {

                        numsToRemove.Add(v);
                    }
                }
            }
            else if (color == "green")
            {
                foreach (Player p in players)
                {
                    numsToRemove.Add(p.maxGreen);
                }
                foreach (int v in listToReturn)
                {
                    if (cPl.maxBlue + cPl.maxRed + v > 15)
                    {

                        numsToRemove.Add(v);
                    }
                }
            }
            else
            {
                foreach (Player p in players)
                {
                    numsToRemove.Add(p.maxYellow);
                }
                foreach (int v in listToReturn)
                {
                    if (cPl.maxBlue + cPl.maxRed + cPl.maxGreen + v > 18)
                    {

                        numsToRemove.Add(v);
                    }
                }
            }



            foreach (int rem in numsToRemove)
            {
                Console.WriteLine();
                listToReturn.Remove(rem);
            }

            return listToReturn;
        }

        public void afficherList(List<Player> list)
        {
            
            foreach (Player p in list)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("Joueur : " + p.name);
                Console.WriteLine("Max bleu : " + p.maxBlue);
                Console.WriteLine("Max rouge : " + p.maxRed);
                Console.WriteLine("Max vert : " + p.maxGreen);
                Console.WriteLine("Max jaune : " + p.maxYellow);
                Console.WriteLine("-----------------");
            }
        }
    }
}