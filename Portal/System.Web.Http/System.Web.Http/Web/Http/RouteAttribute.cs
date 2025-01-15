using System;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class RouteAttribute : Attribute, IDirectRouteFactory, IHttpRouteInfoProvider
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003B90 File Offset: 0x00001D90
		public RouteAttribute()
		{
			this.Template = string.Empty;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003BA3 File Offset: 0x00001DA3
		public RouteAttribute(string template)
		{
			if (template == null)
			{
				throw Error.ArgumentNull("template");
			}
			this.Template = template;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003BC0 File Offset: 0x00001DC0
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public string Name { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003BD1 File Offset: 0x00001DD1
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003BD9 File Offset: 0x00001DD9
		public int Order { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003BE2 File Offset: 0x00001DE2
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003BEA File Offset: 0x00001DEA
		public string Template { get; private set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BF3 File Offset: 0x00001DF3
		RouteEntry IDirectRouteFactory.CreateRoute(DirectRouteFactoryContext context)
		{
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this.Template);
			directRouteBuilder.Name = this.Name;
			directRouteBuilder.Order = this.Order;
			return directRouteBuilder.Build();
		}
	}
}
