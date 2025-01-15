using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200034C RID: 844
	public sealed class TablixBody
	{
		// Token: 0x0600208D RID: 8333 RVA: 0x0007E766 File Offset: 0x0007C966
		internal TablixBody(Tablix owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0007E775 File Offset: 0x0007C975
		internal bool HasRowCollection
		{
			get
			{
				return this.m_rowCollection != null;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x0007E780 File Offset: 0x0007C980
		public TablixRowCollection RowCollection
		{
			get
			{
				if (this.m_rowCollection == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						switch (this.m_owner.SnapshotTablixType)
						{
						case DataRegion.Type.List:
							this.m_rowCollection = new ShimListRowCollection(this.m_owner);
							break;
						case DataRegion.Type.Table:
							this.m_rowCollection = new ShimTableRowCollection(this.m_owner);
							break;
						case DataRegion.Type.Matrix:
							this.m_rowCollection = new ShimMatrixRowCollection(this.m_owner);
							break;
						}
					}
					else
					{
						this.m_rowCollection = new InternalTablixRowCollection(this.m_owner, this.m_owner.TablixDef.TablixRows);
					}
				}
				return this.m_rowCollection;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x0007E827 File Offset: 0x0007CA27
		public TablixColumnCollection ColumnCollection
		{
			get
			{
				if (this.m_columnCollection == null)
				{
					this.m_columnCollection = new TablixColumnCollection(this.m_owner);
				}
				return this.m_columnCollection;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0007E848 File Offset: 0x0007CA48
		public bool IgnoreCellPageBreaks
		{
			get
			{
				if (this.m_ignoreCellPageBreaks == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						this.m_ignoreCellPageBreaks = new bool?(DataRegion.Type.List != this.m_owner.SnapshotTablixType);
					}
					else
					{
						this.m_ignoreCellPageBreaks = new bool?(true);
						Tablix tablixDef = this.m_owner.TablixDef;
						if (tablixDef.ColumnCount == 1 && tablixDef.ColumnMembers[0].IsStatic && ((TablixMember)tablixDef.ColumnMembers[0]).TablixHeader == null)
						{
							TablixMemberList tablixMemberList = (TablixMemberList)tablixDef.RowMembers;
							this.m_ignoreCellPageBreaks = new bool?(this.HasHeader(tablixMemberList));
						}
					}
				}
				return this.m_ignoreCellPageBreaks.Value;
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0007E908 File Offset: 0x0007CB08
		private bool HasHeader(TablixMemberList members)
		{
			if (members != null)
			{
				for (int i = 0; i < members.Count; i++)
				{
					if (members[i].TablixHeader != null || this.HasHeader(members[i].SubMembers))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400105B RID: 4187
		private Tablix m_owner;

		// Token: 0x0400105C RID: 4188
		private TablixRowCollection m_rowCollection;

		// Token: 0x0400105D RID: 4189
		private TablixColumnCollection m_columnCollection;

		// Token: 0x0400105E RID: 4190
		private bool? m_ignoreCellPageBreaks;
	}
}
