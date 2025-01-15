using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200027B RID: 635
	public sealed class CustomData : IReportScopeInstance
	{
		// Token: 0x060018D7 RID: 6359 RVA: 0x0006614F File Offset: 0x0006434F
		internal CustomData(CustomReportItem owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00066165 File Offset: 0x00064365
		public string DataSetName
		{
			get
			{
				if (this.m_owner.IsOldSnapshot)
				{
					return this.m_owner.RenderCri.CriDefinition.DataSetName;
				}
				return ((DataRegion)this.m_owner.ReportItemDef).DataSetName;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000661A0 File Offset: 0x000643A0
		public DataHierarchy DataColumnHierarchy
		{
			get
			{
				if (this.m_columns == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (this.m_owner.RenderCri.CustomData.DataColumnGroupings != null)
						{
							this.m_columns = new DataHierarchy(this.m_owner, true);
						}
					}
					else if (this.m_owner.CriDef.DataColumnMembers != null)
					{
						this.m_columns = new DataHierarchy(this.m_owner, true);
					}
				}
				return this.m_columns;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00066218 File Offset: 0x00064418
		public DataHierarchy DataRowHierarchy
		{
			get
			{
				if (this.m_rows == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (this.m_owner.RenderCri.CustomData.DataRowGroupings != null)
						{
							this.m_rows = new DataHierarchy(this.m_owner, false);
						}
					}
					else if (this.m_owner.CriDef.DataRowMembers != null)
					{
						this.m_rows = new DataHierarchy(this.m_owner, false);
					}
				}
				return this.m_rows;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0006628F File Offset: 0x0006448F
		internal bool HasDataRowCollection
		{
			get
			{
				return this.m_rowCollection != null;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0006629C File Offset: 0x0006449C
		public DataRowCollection RowCollection
		{
			get
			{
				if (this.m_rowCollection == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (this.m_owner.RenderCri.CustomData.DataCells != null)
						{
							this.m_rowCollection = new ShimDataRowCollection(this.m_owner);
						}
					}
					else if (this.m_owner.CriDef.DataRows != null)
					{
						this.m_rowCollection = new InternalDataRowCollection(this.m_owner, this.m_owner.CriDef.DataRows);
					}
				}
				return this.m_rowCollection;
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00066321 File Offset: 0x00064521
		internal void SetNewContext()
		{
			this.m_isNewContext = true;
			if (this.m_rows != null)
			{
				this.m_rows.SetNewContext();
			}
			if (this.m_columns != null)
			{
				this.m_columns.SetNewContext();
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00066350 File Offset: 0x00064550
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_owner.InstanceUniqueName;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0006635D File Offset: 0x0006455D
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00066365 File Offset: 0x00064565
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0006636E File Offset: 0x0006456E
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_owner.ReportScope;
			}
		}

		// Token: 0x04000C87 RID: 3207
		private CustomReportItem m_owner;

		// Token: 0x04000C88 RID: 3208
		private DataHierarchy m_columns;

		// Token: 0x04000C89 RID: 3209
		private DataHierarchy m_rows;

		// Token: 0x04000C8A RID: 3210
		private DataRowCollection m_rowCollection;

		// Token: 0x04000C8B RID: 3211
		private bool m_isNewContext = true;
	}
}
