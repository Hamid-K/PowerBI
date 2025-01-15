using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000176 RID: 374
	public sealed class MapMemberCollection : DataRegionMemberCollection<MapMember>
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x0004401C File Offset: 0x0004221C
		internal MapMemberCollection(IDefinitionPath parentDefinitionPath, MapDataRegion owner)
			: base(parentDefinitionPath, owner)
		{
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00044026 File Offset: 0x00042226
		internal MapMemberCollection(IDefinitionPath parentDefinitionPath, MapDataRegion owner, MapMember parent, MapMemberList memberDefs)
			: base(parentDefinitionPath, owner)
		{
			this.m_parent = parent;
			this.m_memberDefs = memberDefs;
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0004403F File Offset: 0x0004223F
		public override string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath is MapMember)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return this.m_parentDefinitionPath.DefinitionPath;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0004406F File Offset: 0x0004226F
		internal MapDataRegion OwnerMapDataRegion
		{
			get
			{
				return this.m_owner as MapDataRegion;
			}
		}

		// Token: 0x17000866 RID: 2150
		public override MapMember this[int index]
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
				MapMember mapMember = (MapMember)this.m_children[index];
				if (mapMember == null)
				{
					IReportScope reportScope = ((this.m_parent != null) ? this.m_parent.ReportScope : this.m_owner.ReportScope);
					mapMember = (this.m_children[index] = new MapMember(reportScope, this, this.OwnerMapDataRegion, this.m_parent, this.m_memberDefs[index]));
				}
				return mapMember;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0004413C File Offset: 0x0004233C
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0400074B RID: 1867
		private MapMember m_parent;

		// Token: 0x0400074C RID: 1868
		private MapMemberList m_memberDefs;
	}
}
