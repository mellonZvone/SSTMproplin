using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SSTM.Driver.CommonLib;
using SSTM.ProplinWeb.DataLayer;
using SSTM.WebServices.Common.Model;

namespace SSTM.ProplinWeb
{
    public class ProplinHub : Hub
    {
        private readonly IDataLayer istance;
      

        public ProplinHub(): this(DataProvider.Instance) { }
        public ProplinHub(IDataLayer istance) => this.istance = istance;



        public override Task OnConnected()
        {
            istance.OnConnected(Context.ConnectionId);
            return base.OnConnected();
        }

        public async Task<ITag> GetTag(string name)
        {     
            istance.RegisterTag(name, this.Context.ConnectionId);
            return (await istance.GetTagAsync(name)).FirstOrDefault();
        }

        public void RegisterTag(string name)
        {
            istance.RegisterTag(name, this.Context.ConnectionId);
            //return (await istance.GetTagAsync(name)).FirstOrDefault();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            istance.OnDisconnected(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
        
    }
}