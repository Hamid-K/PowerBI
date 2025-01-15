using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000175 RID: 373
	public sealed class MapMember : DataRegionMember
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x00043D50 File Offset: 0x00041F50
		internal MapMember(IReportScope reportScope, IDefinitionPath parentDefinitionPath, MapDataRegion owner, MapMember parent, MapMember memberDef)
			: base(parentDefinitionPath, owner, parent, 0)
		{
			this.m_memberDef = memberDef;
			if (this.m_memberDef.IsStatic)
			{
				this.m_reportScope = reportScope;
			}
			if (this.m_memberDef.Grouping != null)
			{
				this.m_group = new Group(owner, this.m_memberDef, this);
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00043DA4 File Offset: 0x00041FA4
		internal MapMember(IDefinitionPath parentDefinitionPath, MapDataRegion owner, MapMember parent)
			: base(parentDefinitionPath, owner, parent, 0)
		{
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00043DB0 File Offset: 0x00041FB0
		public MapMember Parent
		{
			get
			{
				return this.m_parent as MapMember;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00043DBD File Offset: 0x00041FBD
		internal override string UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00043DCA File Offset: 0x00041FCA
		public override string ID
		{
			get
			{
				return this.m_memberDef.RenderingModelID;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00043DD7 File Offset: 0x00041FD7
		public override bool IsStatic
		{
			get
			{
				return this.m_memberDef.Grouping == null;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00043DE9 File Offset: 0x00041FE9
		public override int MemberCellIndex
		{
			get
			{
				return this.m_memberDef.MemberCellIndex;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00043DF6 File Offset: 0x00041FF6
		internal MapMember MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00043DFE File Offset: 0x00041FFE
		internal override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.MemberDefinition;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00043E06 File Offset: 0x00042006
		internal override IReportScope ReportScope
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_reportScope;
				}
				return this;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00043E18 File Offset: 0x00042018
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_reportScope.RIFReportScope;
				}
				return this.MemberDefinition;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00043E34 File Offset: 0x00042034
		internal override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_reportScope.ReportScopeInstance;
				}
				return (IReportScopeInstance)this.Instance;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00043E55 File Offset: 0x00042055
		internal MapDataRegion OwnerMapDataRegion
		{
			get
			{
				return this.m_owner as MapDataRegion;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00043E64 File Offset: 0x00042064
		public MapMemberInstance Instance
		{
			get
			{
				if (this.OwnerMapDataRegion.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new MapMemberInstance(this.OwnerMapDataRegion, this);
					}
					else
					{
						MapDynamicMemberInstance mapDynamicMemberInstance = new MapDynamicMemberInstance(this.OwnerMapDataRegion, this, this.BuildOdpMemberLogic(this.OwnerMapDataRegion.RenderingContext.OdpContext));
						this.m_owner.RenderingContext.AddDynamicInstance(mapDynamicMemberInstance);
						this.m_instance = mapDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00043EEA File Offset: 0x000420EA
		public MapMember ChildMapMember
		{
			get
			{
				if (this.m_children != null && this.m_children.Count == 1)
				{
					return this.m_children[0];
				}
				return null;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00043F10 File Offset: 0x00042110
		internal override IDataRegionMemberCollection SubMembers
		{
			get
			{
				if (this.m_children == null && this.m_memberDef.InnerHierarchy != null)
				{
					MapMemberList mapMemberList = (MapMemberList)this.m_memberDef.InnerHierarchy;
					if (mapMemberList == null)
					{
						return null;
					}
					this.m_children = new MapMemberCollection(this, this.OwnerMapDataRegion, this, mapMemberList);
				}
				return this.m_children;
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00043F62 File Offset: 0x00042162
		internal override bool GetIsColumn()
		{
			return this.m_memberDef.IsColumn;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00043F6F File Offset: 0x0004216F
		private List<MapVectorLayer> GetChildLayers()
		{
			return ((MapDataRegion)this.m_owner).GetChildLayers();
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00043F84 File Offset: 0x00042184
		internal override void SetNewContext(bool fromMoveNext)
		{
			if (!fromMoveNext && this.m_instance != null && !this.IsStatic)
			{
				((IDynamicInstance)this.m_instance).ResetContext();
			}
			base.SetNewContext(fromMoveNext);
			if (this.ChildMapMember == null)
			{
				foreach (MapVectorLayer mapVectorLayer in this.GetChildLayers())
				{
					mapVectorLayer.SetNewContext();
				}
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000747 RID: 1863
		private MapMemberCollection m_children;

		// Token: 0x04000748 RID: 1864
		private MapMemberInstance m_instance;

		// Token: 0x04000749 RID: 1865
		private MapMember m_memberDef;

		// Token: 0x0400074A RID: 1866
		private IReportScope m_reportScope;
	}
}
