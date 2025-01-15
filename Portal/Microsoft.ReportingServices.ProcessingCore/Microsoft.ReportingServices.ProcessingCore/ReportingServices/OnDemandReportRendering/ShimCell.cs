using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200035B RID: 859
	internal abstract class ShimCell : TablixCell
	{
		// Token: 0x060020D7 RID: 8407 RVA: 0x0007F6E8 File Offset: 0x0007D8E8
		internal ShimCell(Tablix owner, int rowIndex, int colIndex, bool inSubtotal)
			: base(null, owner, rowIndex, colIndex)
		{
			this.m_inSubtotal = inSubtotal;
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0007F6FC File Offset: 0x0007D8FC
		public override string ID
		{
			get
			{
				return base.DefinitionPath;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0007F704 File Offset: 0x0007D904
		public override string DataElementName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0007F707 File Offset: 0x0007D907
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				return DataElementOutputTypes.ContentsOnly;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0007F70A File Offset: 0x0007D90A
		public override StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				return StructureTypeOverwriteType.None;
			}
		}

		// Token: 0x0400107E RID: 4222
		protected bool m_inSubtotal;

		// Token: 0x0400107F RID: 4223
		protected string m_shimID;
	}
}
