using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x02000141 RID: 321
	public interface IDirectRouteBuilder
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008CA RID: 2250
		// (set) Token: 0x060008CB RID: 2251
		string Name { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008CC RID: 2252
		// (set) Token: 0x060008CD RID: 2253
		string Template { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008CE RID: 2254
		// (set) Token: 0x060008CF RID: 2255
		IDictionary<string, object> Defaults { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008D0 RID: 2256
		// (set) Token: 0x060008D1 RID: 2257
		IDictionary<string, object> Constraints { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008D2 RID: 2258
		// (set) Token: 0x060008D3 RID: 2259
		IDictionary<string, object> DataTokens { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008D4 RID: 2260
		// (set) Token: 0x060008D5 RID: 2261
		int Order { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008D6 RID: 2262
		// (set) Token: 0x060008D7 RID: 2263
		decimal Precedence { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008D8 RID: 2264
		IReadOnlyCollection<HttpActionDescriptor> Actions { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008D9 RID: 2265
		bool TargetIsAction { get; }

		// Token: 0x060008DA RID: 2266
		RouteEntry Build();
	}
}
