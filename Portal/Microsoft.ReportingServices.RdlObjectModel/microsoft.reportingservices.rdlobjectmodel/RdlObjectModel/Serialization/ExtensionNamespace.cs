using System;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D4 RID: 724
	public sealed class ExtensionNamespace
	{
		// Token: 0x06001648 RID: 5704 RVA: 0x000335AD File Offset: 0x000317AD
		public ExtensionNamespace(string localName, string xmlNamespace, bool mustUnderstand = false)
		{
			this.LocalName = localName;
			this.Namespace = xmlNamespace;
			this.MustUnderstand = mustUnderstand;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x000335CA File Offset: 0x000317CA
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x000335D2 File Offset: 0x000317D2
		public string LocalName { get; private set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000335DB File Offset: 0x000317DB
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x000335E3 File Offset: 0x000317E3
		public string Namespace { get; private set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000335EC File Offset: 0x000317EC
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x000335F4 File Offset: 0x000317F4
		public bool MustUnderstand { get; private set; }
	}
}
