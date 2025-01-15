using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000147 RID: 327
	internal class RouteInfoDirectRouteFactory : IDirectRouteFactory
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x000160C4 File Offset: 0x000142C4
		public RouteInfoDirectRouteFactory(IHttpRouteInfoProvider infoProvider)
		{
			if (infoProvider == null)
			{
				throw new ArgumentNullException("infoProvider");
			}
			this._infoProvider = infoProvider;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000160E1 File Offset: 0x000142E1
		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this._infoProvider.Template);
			directRouteBuilder.Name = this._infoProvider.Name;
			directRouteBuilder.Order = this._infoProvider.Order;
			return directRouteBuilder.Build();
		}

		// Token: 0x0400026B RID: 619
		private readonly IHttpRouteInfoProvider _infoProvider;
	}
}
