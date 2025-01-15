using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Packaging.Project.Artifacts;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000072 RID: 114
	public class PBIProjectReport : ArtifactSourceControl
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00008A3D File Offset: 0x00006C3D
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00008A45 File Offset: 0x00006C45
		public IStreamablePowerBIProjectPartContent Report { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00008A4E File Offset: 0x00006C4E
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00008A56 File Offset: 0x00006C56
		public ReportDefinition ReportDefinition { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00008A5F File Offset: 0x00006C5F
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00008A67 File Offset: 0x00006C67
		public IStreamablePowerBIProjectPartContent MobileState { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00008A70 File Offset: 0x00006C70
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00008A78 File Offset: 0x00006C78
		public IStreamablePowerBIProjectPartContent Thumbnail { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00008A81 File Offset: 0x00006C81
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00008A89 File Offset: 0x00006C89
		public IStreamablePowerBIProjectPartContent DiagramLayout { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00008A92 File Offset: 0x00006C92
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00008A9A File Offset: 0x00006C9A
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> CustomVisuals { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00008AA3 File Offset: 0x00006CA3
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00008AAB File Offset: 0x00006CAB
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> StaticResources { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00008AB4 File Offset: 0x00006CB4
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00008ABC File Offset: 0x00006CBC
		public ReportLocalSettings LocalSettings { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00008AC5 File Offset: 0x00006CC5
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00008ACD File Offset: 0x00006CCD
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> DaxQueryView { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00008AD6 File Offset: 0x00006CD6
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00008ADE File Offset: 0x00006CDE
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> Exploration { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00008AE7 File Offset: 0x00006CE7
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00008AEF File Offset: 0x00006CEF
		public PBIProjectDataset Dataset { get; set; } = new PBIProjectDataset();
	}
}
