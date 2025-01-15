using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000708 RID: 1800
	internal class ClientEnlistmentInformation
	{
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x000BF25C File Offset: 0x000BD45C
		// (set) Token: 0x0600390A RID: 14602 RVA: 0x000BF264 File Offset: 0x000BD464
		internal int Cookie { get; private set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000BF26D File Offset: 0x000BD46D
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x000BF275 File Offset: 0x000BD475
		internal IXaClientEnlistment IXaClientEnlistment { get; private set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000BF27E File Offset: 0x000BD47E
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x000BF286 File Offset: 0x000BD486
		internal IXaClientStartEnlistment IXaClientStartEnlistment { get; private set; }

		// Token: 0x0600390F RID: 14607 RVA: 0x000BF28F File Offset: 0x000BD48F
		internal ClientEnlistmentInformation(int cookie, IXaClientEnlistment iXaClientEnlistment)
		{
			this.Cookie = cookie;
			this.IXaClientEnlistment = iXaClientEnlistment;
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000BF2A5 File Offset: 0x000BD4A5
		internal ClientEnlistmentInformation(int cookie, IXaClientStartEnlistment iXaClientStartEnlistment)
		{
			this.Cookie = cookie;
			this.IXaClientStartEnlistment = iXaClientStartEnlistment;
		}
	}
}
