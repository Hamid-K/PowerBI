using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200028C RID: 652
	internal sealed class ShimDataMemberCollection : DataMemberCollection
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x00066EA0 File Offset: 0x000650A0
		internal ShimDataMemberCollection(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, bool isColumnMember, ShimDataMember parent, DataGroupingCollection definitionGroups)
			: base(parentDefinitionPath, owner)
		{
			this.m_isColumnMember = isColumnMember;
			this.m_definitionGroups = definitionGroups;
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			if (definitionGroups[0] != null && definitionGroups[0][0] != null)
			{
				this.m_isStatic = definitionGroups[0][0].IsStatic;
			}
			int count = definitionGroups.Count;
			DataRegionMember[] array = new ShimDataMember[count];
			this.m_children = array;
			for (int i = 0; i < count; i++)
			{
				this.m_children[i] = new ShimDataMember(this, owner, parent, i, this.m_isColumnMember, this.m_isStatic, definitionGroups[i], i);
			}
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x17000E67 RID: 3687
		public override DataMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return (DataMember)this.m_children[index];
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00066FC0 File Offset: 0x000651C0
		public override int Count
		{
			get
			{
				return this.m_children.Length;
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00066FCC File Offset: 0x000651CC
		internal void UpdateContext()
		{
			if (this.m_children == null)
			{
				return;
			}
			if (this.m_isColumnMember)
			{
				this.ResetContext(base.OwnerCri.RenderCri.CustomData.DataColumnGroupings);
				return;
			}
			this.ResetContext(base.OwnerCri.RenderCri.CustomData.DataRowGroupings);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00067024 File Offset: 0x00065224
		internal void ResetContext(DataGroupingCollection definitionGroups)
		{
			if (this.m_children == null)
			{
				return;
			}
			if (definitionGroups != null)
			{
				this.m_definitionGroups = definitionGroups;
			}
			if (this.m_isStatic)
			{
				for (int i = 0; i < this.m_children.Length; i++)
				{
					((ShimDataMember)this.m_children[i]).ResetContext(this.m_definitionGroups[i]);
				}
			}
		}

		// Token: 0x04000CA5 RID: 3237
		private bool m_isStatic;

		// Token: 0x04000CA6 RID: 3238
		private bool m_isColumnMember;

		// Token: 0x04000CA7 RID: 3239
		private int m_definitionStartIndex = -1;

		// Token: 0x04000CA8 RID: 3240
		private int m_definitionEndIndex = -1;

		// Token: 0x04000CA9 RID: 3241
		private DataGroupingCollection m_definitionGroups;
	}
}
