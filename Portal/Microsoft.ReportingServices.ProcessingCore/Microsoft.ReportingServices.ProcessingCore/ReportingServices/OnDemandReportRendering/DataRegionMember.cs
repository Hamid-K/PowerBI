using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000293 RID: 659
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataRegionMember : IDefinitionPath, IReportScope
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x00067B36 File Offset: 0x00065D36
		internal DataRegionMember(IDefinitionPath parentDefinitionPath, ReportItem owner, DataRegionMember parent, int parentCollectionIndex)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_owner = owner;
			this.m_parent = parent;
			this.m_parentCollectionIndex = parentCollectionIndex;
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06001987 RID: 6535
		internal abstract string UniqueName { get; }

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06001988 RID: 6536
		public abstract string ID { get; }

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x00067B5B File Offset: 0x00065D5B
		public string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = DefinitionPathConstants.GetCollectionDefinitionPath(this.m_parentDefinitionPath, this.m_parentCollectionIndex);
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x00067B82 File Offset: 0x00065D82
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_parentDefinitionPath;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x00067B8A File Offset: 0x00065D8A
		public Group Group
		{
			get
			{
				return this.m_group;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x0600198C RID: 6540
		public abstract bool IsStatic { get; }

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00067B92 File Offset: 0x00065D92
		public virtual CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customPropertyCollection == null)
				{
					this.m_customPropertyCollection = new CustomPropertyCollection();
				}
				return this.m_customPropertyCollection;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x0600198E RID: 6542
		public abstract int MemberCellIndex { get; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x0600198F RID: 6543
		internal abstract IReportScope ReportScope { get; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x00067BAD File Offset: 0x00065DAD
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.ReportScopeInstance;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06001991 RID: 6545
		internal abstract IReportScopeInstance ReportScopeInstance { get; }

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00067BB5 File Offset: 0x00065DB5
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.RIFReportScope;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06001993 RID: 6547
		internal abstract IRIFReportScope RIFReportScope { get; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00067BBD File Offset: 0x00065DBD
		internal IDataRegion OwnerDataRegion
		{
			get
			{
				return (IDataRegion)this.m_owner;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06001995 RID: 6549
		internal abstract IDataRegionMemberCollection SubMembers { get; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06001996 RID: 6550
		internal abstract ReportHierarchyNode DataRegionMemberDefinition { get; }

		// Token: 0x06001997 RID: 6551
		internal abstract bool GetIsColumn();

		// Token: 0x06001998 RID: 6552 RVA: 0x00067BCA File Offset: 0x00065DCA
		internal virtual void ResetContext()
		{
			if (this.m_group != null)
			{
				this.m_group.SetNewContext();
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00067BE0 File Offset: 0x00065DE0
		internal virtual void SetNewContext(bool fromMoveNext)
		{
			if (this.m_group != null)
			{
				this.m_group.SetNewContext();
			}
			if (this.IsStatic || this.SubMembers == null || fromMoveNext)
			{
				if (this.SubMembers != null)
				{
					this.SubMembers.SetNewContext();
				}
				else
				{
					this.SetCellsNewContext();
				}
			}
			if (!fromMoveNext && this.DataRegionMemberDefinition != null)
			{
				this.DataRegionMemberDefinition.ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00067C4C File Offset: 0x00065E4C
		private void SetCellsNewContext()
		{
			if (this.OwnerDataRegion.HasDataCells)
			{
				IDataRegionRowCollection rowCollection = this.OwnerDataRegion.RowCollection;
				if (this.GetIsColumn())
				{
					for (int i = 0; i < rowCollection.Count; i++)
					{
						IDataRegionRow ifExists = rowCollection.GetIfExists(i);
						if (ifExists != null)
						{
							IDataRegionCell ifExists2 = ifExists.GetIfExists(this.MemberCellIndex);
							if (ifExists2 != null)
							{
								ifExists2.SetNewContext();
							}
						}
					}
					return;
				}
				IDataRegionRow ifExists3 = rowCollection.GetIfExists(this.MemberCellIndex);
				if (ifExists3 != null)
				{
					for (int j = 0; j < ifExists3.Count; j++)
					{
						IDataRegionCell ifExists4 = ifExists3.GetIfExists(j);
						if (ifExists4 != null)
						{
							ifExists4.SetNewContext();
						}
					}
				}
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00067CEE File Offset: 0x00065EEE
		internal virtual InternalDynamicMemberLogic BuildOdpMemberLogic(OnDemandProcessingContext odpContext)
		{
			if (odpContext.StreamingMode)
			{
				return new InternalStreamingOdpDynamicMemberLogic(this, odpContext);
			}
			return new InternalFullOdpDynamicMemberLogic(this, odpContext);
		}

		// Token: 0x04000CC1 RID: 3265
		protected IDefinitionPath m_parentDefinitionPath;

		// Token: 0x04000CC2 RID: 3266
		protected int m_parentCollectionIndex;

		// Token: 0x04000CC3 RID: 3267
		protected string m_definitionPath;

		// Token: 0x04000CC4 RID: 3268
		protected ReportItem m_owner;

		// Token: 0x04000CC5 RID: 3269
		protected Group m_group;

		// Token: 0x04000CC6 RID: 3270
		protected DataRegionMember m_parent;

		// Token: 0x04000CC7 RID: 3271
		protected CustomPropertyCollection m_customPropertyCollection;
	}
}
