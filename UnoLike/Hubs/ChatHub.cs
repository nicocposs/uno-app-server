using Microsoft.AspNetCore.SignalR;                                       
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnoLike.Hubs
{
    public class ChatHub : Hub                                           
    {
        public static int nbrOfPlayers;
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
            return Clients.All.SendAsync("PlayerName", name);
        }
    }
}