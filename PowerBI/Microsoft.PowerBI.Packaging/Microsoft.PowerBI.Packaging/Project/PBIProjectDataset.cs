using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Packaging.Project.Artifacts;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200006B RID: 107
	public class PBIProjectDataset : ArtifactSourceControl
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00008598 File Offset: 0x00006798
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000085A0 File Offset: 0x000067A0
		public IStreamablePowerBIProjectPartContent DataModel { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000085A9 File Offset: 0x000067A9
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000085B1 File Offset: 0x000067B1
		public IStreamablePowerBIProjectPartContent DataModelSchema { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000085BA File Offset: 0x000067BA
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x000085C2 File Offset: 0x000067C2
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> DataModelSchemaTmdl { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000085CB File Offset: 0x000067CB
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000085D3 File Offset: 0x000067D3
		public DatasetDefinition Definition { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000085DC File Offset: 0x000067DC
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000085E4 File Offset: 0x000067E4
		public DatasetModelReference ModelReference { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000085ED File Offset: 0x000067ED
		// (set) Token: 0x060002ED RID: 749 RVA: 0x000085F5 File Offset: 0x000067F5
		public IStreamablePowerBIProjectPartContent Thumbnail { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000085FE File Offset: 0x000067FE
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00008606 File Offset: 0x00006806
		public IStreamablePowerBIProjectPartContent DiagramLayout { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000860F File Offset: 0x0000680F
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00008617 File Offset: 0x00006817
		public DatasetLocalSettings LocalSettings { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00008620 File Offset: 0x00006820
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00008628 File Offset: 0x00006828
		public DatasetEditorSettings EditorSettings { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00008631 File Offset: 0x00006831
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00008639 File Offset: 0x00006839
		public UnappliedChanges UnappliedChanges { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00008642 File Offset: 0x00006842
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000864A File Offset: 0x0000684A
		public IDictionary<Uri, IStreamablePowerBIProjectPartContent> DaxQueryView { get; set; }
	}
}
