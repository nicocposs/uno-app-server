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
        public static int nbrOfPlayers;
        public static Deck deck;

        public static List<Player> players = new List<Player>(); 
        public override Task OnConnectedAsync()
        {
            nbrOfPlayers++;
            Console.WriteLine("Joueur : " + nbrOfPlayers);
            SendConnectedState();
            if(nbrOfPlayers == 4)
            {
                
                SendHasEnoughPlayers();
                
                
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            nbrOfPlayers--;
            Console.WriteLine("Joueur : " + nbrOfPlayers);
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendConnectedState()
        {
            Console.WriteLine(Context.ConnectionId);
            return Clients.Client(Context.ConnectionId).SendAsync("ConnectedState", true);
        }

        public Task SendHasEnoughPlayers()
        {
            return Clients.All.SendAsync("HasEnoughPlayers", true);
        }

        public Task SendName(string name)              
        {
            Console.WriteLine(Context.ConnectionId);
            Player newPlayer = new Player(Context.ConnectionId, name);
            players.Add(newPlayer);
            return Clients.All.SendAsync("PlayerName", name);
        }

        public void CreateGame()
        {
            CreateDeck();
            int[] vals = { 1, 2, 3, 4, 5, 6, 7 };
            ChooseCardMax("blue",vals);
        }

        public Task ChooseCardMax(string color, int[] values)
        {
            List<int> newList = new List<int>(values);
            return Clients.Client(players[0].connectionId).SendAsync("ChooseCardMax", color, values);
        }

        public void CreateDeck()
        {
            deck = new Deck();
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

        //Ajouter la fonction du invoke pour réceptionner le max
    }
}