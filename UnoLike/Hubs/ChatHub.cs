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

        public Task PassTurn()
        {
            Player currentPlayer = players.Where(pl => pl.connectionId == Context.ConnectionId).FirstOrDefault();
            int index = players.IndexOf(currentPlayer);
            string nextName = "";
            if(index == 3)
            {
                nextName = players[0].name;
            }
            else
            {
                nextName = players[index+1].name;
            }
            return Clients.All.SendAsync("PlayerNameTurn", nextName);
        }
    }
}