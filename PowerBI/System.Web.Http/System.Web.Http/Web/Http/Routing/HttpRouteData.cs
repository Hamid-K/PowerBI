using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000161 RID: 353
	public class HttpRouteData : IHttpRouteData
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x00018B0B File Offset: 0x00016D0B
		public HttpRouteData(IHttpRoute route)
			: this(route, new HttpRouteValueDictionary())
		{
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00018B19 File Offset: 0x00016D19
		public HttpRouteData(IHttpRoute route, HttpRouteValueDictionary values)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this._route = route;
			this._values = values;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00018B4B File Offset: 0x00016D4B
		public IHttpRoute Route
		{
			get
			{
				return this._route;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00018B53 File Offset: 0x00016D53
		public IDictionary<string, object> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x04000291 RID: 657
		private IHttpRoute _route;

		// Token: 0x04000292 RID: 658
		private IDictionary<string, object> _values;
	}
}
