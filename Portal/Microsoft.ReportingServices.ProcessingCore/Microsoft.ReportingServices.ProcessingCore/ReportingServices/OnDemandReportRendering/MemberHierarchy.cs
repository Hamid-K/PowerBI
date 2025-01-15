using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000291 RID: 657
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MemberHierarchy<T> : IDefinitionPath
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x00067A7E File Offset: 0x00065C7E
		internal MemberHierarchy(ReportItem owner, bool isColumn)
		{
			this.m_owner = owner;
			this.m_isColumn = isColumn;
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00067A94 File Offset: 0x00065C94
		public string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = DefinitionPathConstants.GetTablixHierarchyDefinitionPath(this.m_owner, this.m_isColumn);
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00067ABB File Offset: 0x00065CBB
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00067AC3 File Offset: 0x00065CC3
		internal void SetNewContext()
		{
			if (this.m_members != null)
			{
				((IDataRegionMemberCollection)this.m_members).SetNewContext();
			}
		}

		// Token: 0x06001981 RID: 6529
		internal abstract void ResetContext();

		// Token: 0x04000CBA RID: 3258
		protected DataRegionMemberCollection<T> m_members;

		// Token: 0x04000CBB RID: 3259
		protected bool m_isColumn;

		// Token: 0x04000CBC RID: 3260
		protected ReportItem m_owner;

		// Token: 0x04000CBD RID: 3261
		protected string m_definitionPath;
	}
}
