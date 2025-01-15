using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000145 RID: 325
	public class RouteEntry
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00015EBE File Offset: 0x000140BE
		public RouteEntry(string name, IHttpRoute route)
		{
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			this._name = name;
			this._route = route;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00015EE2 File Offset: 0x000140E2
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00015EEA File Offset: 0x000140EA
		public IHttpRoute Route
		{
			get
			{
				return this._route;
			}
		}

		// Token: 0x04000266 RID: 614
		private readonly string _name;

		// Token: 0x04000267 RID: 615
		private readonly IHttpRoute _route;
	}
}
