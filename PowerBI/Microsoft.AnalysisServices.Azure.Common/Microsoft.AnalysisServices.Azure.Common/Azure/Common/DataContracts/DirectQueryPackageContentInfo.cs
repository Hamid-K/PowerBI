using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common.DataContracts
{
	// Token: 0x02000156 RID: 342
	[DataContract]
	public sealed class DirectQueryPackageContentInfo
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00049523 File Offset: 0x00047723
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0004952B File Offset: 0x0004772B
		[DataMember]
		public string XmlaContent { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00049534 File Offset: 0x00047734
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x0004953C File Offset: 0x0004773C
		[DataMember]
		public string TemplateModelName { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00049545 File Offset: 0x00047745
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0004954D File Offset: 0x0004774D
		[DataMember]
		public string PackageName { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00049556 File Offset: 0x00047756
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0004955E File Offset: 0x0004775E
		[DataMember]
		public string ResourceName { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00049567 File Offset: 0x00047767
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x0004956F File Offset: 0x0004776F
		[DataMember]
		public string ContentProviderName { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00049578 File Offset: 0x00047778
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x00049580 File Offset: 0x00047780
		[DataMember]
		public string DefaultDatabaseName { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00049589 File Offset: 0x00047789
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00049591 File Offset: 0x00047791
		[DataMember]
		public Stream PackageStream { get; set; }
	}
}
