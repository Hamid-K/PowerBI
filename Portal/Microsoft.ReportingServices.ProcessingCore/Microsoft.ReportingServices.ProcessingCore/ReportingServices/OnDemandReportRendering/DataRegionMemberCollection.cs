using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000292 RID: 658
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataRegionMemberCollection<T> : ReportElementCollectionBase<T>, IDefinitionPath, IDataRegionMemberCollection
	{
		// Token: 0x06001982 RID: 6530 RVA: 0x00067AD8 File Offset: 0x00065CD8
		internal DataRegionMemberCollection(IDefinitionPath parentDefinitionPath, ReportItem owner)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_owner = owner;
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06001983 RID: 6531
		public abstract string DefinitionPath { get; }

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00067AEE File Offset: 0x00065CEE
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_parentDefinitionPath;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00067AF8 File Offset: 0x00065CF8
		void IDataRegionMemberCollection.SetNewContext()
		{
			if (this.m_children != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.m_children[i] != null)
					{
						this.m_children[i].SetNewContext(false);
					}
				}
			}
		}

		// Token: 0x04000CBE RID: 3262
		protected DataRegionMember[] m_children;

		// Token: 0x04000CBF RID: 3263
		protected IDefinitionPath m_parentDefinitionPath;

		// Token: 0x04000CC0 RID: 3264
		protected ReportItem m_owner;
	}
}
