using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000098 RID: 152
	internal sealed class DataShapeMemberCollection : DataRegionMemberCollection<DataShapeMember>
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00026E44 File Offset: 0x00025044
		internal DataShapeMemberCollection(IDefinitionPath parentDefinitionPath, DataShape owner, DataShapeMember parent, DataShapeMemberList rifMemberCollection)
			: base(parentDefinitionPath, owner)
		{
			this.m_parent = parent;
			this.m_rifMemberCollection = rifMemberCollection;
		}

		// Token: 0x170005CC RID: 1484
		public override DataShapeMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_children == null)
				{
					this.m_children = new DataRegionMember[this.Count];
				}
				DataShapeMember dataShapeMember = (DataShapeMember)this.m_children[index];
				if (dataShapeMember == null)
				{
					DataShapeMember rifMember = this.m_rifMemberCollection[index];
					IReportScope reportScope = ((this.m_parent == null) ? this.m_owner.ReportScope : this.m_parent.ReportScope);
					dataShapeMember = (this.m_children[index] = new DataShapeMember(reportScope, this, this.OwnerDataShape, this.m_parent, rifMember, index, this.GetLimitsForMember((DataShapeLimit limit) => limit.Within == rifMember.Name), this.GetLimitsForMember((DataShapeLimit limit) => limit.Target == rifMember.Name), this.GetApplicableChildLimitsForMember(rifMember)));
				}
				return dataShapeMember;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00026F66 File Offset: 0x00025166
		public override int Count
		{
			get
			{
				if (this.m_rifMemberCollection == null)
				{
					return 0;
				}
				return this.m_rifMemberCollection.OriginalNodeCount;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00026F7D File Offset: 0x0002517D
		public override string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath is DataShapeMember)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return this.m_parentDefinitionPath.DefinitionPath;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00026FAD File Offset: 0x000251AD
		internal DataShapeMemberList MemberDefs
		{
			get
			{
				return this.m_rifMemberCollection;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00026FB5 File Offset: 0x000251B5
		internal DataShape OwnerDataShape
		{
			get
			{
				return (DataShape)this.m_owner;
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00026FC2 File Offset: 0x000251C2
		private DataShapeLimit[] GetLimitsForMember(Func<DataShapeLimit, bool> predicate)
		{
			if (this.OwnerDataShape.RifDataShapeDefinition.Limits == null)
			{
				return null;
			}
			return this.OwnerDataShape.Limits.Where(predicate).ToArray<DataShapeLimit>();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00026FF0 File Offset: 0x000251F0
		private DataShapeLimit[] GetApplicableChildLimitsForMember(DataShapeMember rifMember)
		{
			if (this.OwnerDataShape.RifDataShapeDefinition.Limits == null)
			{
				return null;
			}
			List<DataShapeLimit> list = new List<DataShapeLimit>();
			this.VisitAllSubmembers(rifMember, list);
			return list.ToArray();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00027028 File Offset: 0x00025228
		private void VisitAllSubmembers(DataShapeMember rifMember, List<DataShapeLimit> childLimits)
		{
			if (rifMember.SubMembers == null || rifMember.SubMembers.OriginalNodeCount == 0)
			{
				return;
			}
			for (int i = 0; i < rifMember.SubMembers.OriginalNodeCount; i++)
			{
				DataShapeMember dataShapeMember = rifMember.SubMembers[i];
				this.AddChildLimitsForMember(dataShapeMember, childLimits);
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00027078 File Offset: 0x00025278
		private void AddChildLimitsForMember(DataShapeMember rifMember, List<DataShapeLimit> limits)
		{
			DataShapeLimit[] limitsForMember = this.GetLimitsForMember((DataShapeLimit limit) => limit.Target == rifMember.Name && limit.Within == this.OwnerDataShape.ClientID);
			limits.AddRange(limitsForMember);
			this.VisitAllSubmembers(rifMember, limits);
		}

		// Token: 0x04000266 RID: 614
		private readonly DataShapeMember m_parent;

		// Token: 0x04000267 RID: 615
		private DataShapeMemberList m_rifMemberCollection;
	}
}
