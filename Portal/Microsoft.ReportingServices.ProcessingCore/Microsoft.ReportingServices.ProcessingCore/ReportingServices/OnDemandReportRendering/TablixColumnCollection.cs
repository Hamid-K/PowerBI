using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200034D RID: 845
	public sealed class TablixColumnCollection : ReportElementCollectionBase<TablixColumn>
	{
		// Token: 0x06002093 RID: 8339 RVA: 0x0007E94E File Offset: 0x0007CB4E
		internal TablixColumnCollection(Tablix owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x17001262 RID: 4706
		public override TablixColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_columns == null)
				{
					this.m_columns = new TablixColumn[this.Count];
				}
				if (this.m_columns[index] == null)
				{
					this.m_columns[index] = new TablixColumn(this.m_owner, index);
				}
				return this.m_columns[index];
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0007E9EC File Offset: 0x0007CBEC
		public override int Count
		{
			get
			{
				int num = 0;
				if (this.m_owner.IsOldSnapshot)
				{
					switch (this.m_owner.SnapshotTablixType)
					{
					case DataRegion.Type.List:
						num = 1;
						break;
					case DataRegion.Type.Table:
						num = this.m_owner.RenderTable.Columns.Count;
						break;
					case DataRegion.Type.Matrix:
						num = this.m_owner.MatrixColDefinitionMapping.Length;
						break;
					}
				}
				else
				{
					num = this.m_owner.TablixDef.TablixColumns.Count;
				}
				return num;
			}
		}

		// Token: 0x0400105F RID: 4191
		private Tablix m_owner;

		// Token: 0x04001060 RID: 4192
		private TablixColumn[] m_columns;
	}
}
