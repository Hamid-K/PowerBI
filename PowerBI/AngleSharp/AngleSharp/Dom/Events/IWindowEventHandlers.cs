using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F9 RID: 505
	[DomName("WindowEventHandlers")]
	[DomNoInterfaceObject]
	public interface IWindowEventHandlers
	{
		// Token: 0x140000EB RID: 235
		// (add) Token: 0x0600111A RID: 4378
		// (remove) Token: 0x0600111B RID: 4379
		[DomName("onafterprint")]
		event DomEventHandler Printed;

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x0600111C RID: 4380
		// (remove) Token: 0x0600111D RID: 4381
		[DomName("onbeforeprint")]
		event DomEventHandler Printing;

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x0600111E RID: 4382
		// (remove) Token: 0x0600111F RID: 4383
		[DomName("onbeforeunload")]
		event DomEventHandler Unloading;

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06001120 RID: 4384
		// (remove) Token: 0x06001121 RID: 4385
		[DomName("onhashchange")]
		event DomEventHandler HashChanged;

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06001122 RID: 4386
		// (remove) Token: 0x06001123 RID: 4387
		[DomName("onmessage")]
		event DomEventHandler MessageReceived;

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06001124 RID: 4388
		// (remove) Token: 0x06001125 RID: 4389
		[DomName("onoffline")]
		event DomEventHandler WentOffline;

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06001126 RID: 4390
		// (remove) Token: 0x06001127 RID: 4391
		[DomName("ononline")]
		event DomEventHandler WentOnline;

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06001128 RID: 4392
		// (remove) Token: 0x06001129 RID: 4393
		[DomName("onpagehide")]
		event DomEventHandler PageHidden;

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x0600112A RID: 4394
		// (remove) Token: 0x0600112B RID: 4395
		[DomName("onpageshow")]
		event DomEventHandler PageShown;

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x0600112C RID: 4396
		// (remove) Token: 0x0600112D RID: 4397
		[DomName("onpopstate")]
		event DomEventHandler PopState;

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x0600112E RID: 4398
		// (remove) Token: 0x0600112F RID: 4399
		[DomName("onstorage")]
		event DomEventHandler Storage;

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06001130 RID: 4400
		// (remove) Token: 0x06001131 RID: 4401
		[DomName("onunload")]
		event DomEventHandler Unloaded;
	}
}
