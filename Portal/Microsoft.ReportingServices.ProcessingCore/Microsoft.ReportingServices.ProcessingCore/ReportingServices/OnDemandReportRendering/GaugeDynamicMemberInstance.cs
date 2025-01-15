using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000113 RID: 275
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class GaugeDynamicMemberInstance : GaugeMemberInstance, IDynamicInstance, IReportScopeInstance
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0003502E File Offset: 0x0003322E
		internal GaugeDynamicMemberInstance(GaugePanel owner, GaugeMember memberDef, InternalDynamicMemberLogic memberLogic)
			: base(owner, memberDef)
		{
			this.m_memberLogic = memberLogic;
			this.ResetContext();
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00035045 File Offset: 0x00033245
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00035052 File Offset: 0x00033252
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0003505F File Offset: 0x0003325F
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_memberLogic.IsNewContext;
			}
			set
			{
				this.m_memberLogic.IsNewContext = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0003506D File Offset: 0x0003326D
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00035075 File Offset: 0x00033275
		public bool MoveNext()
		{
			return this.m_memberLogic.MoveNext();
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00035082 File Offset: 0x00033282
		public bool SetInstanceIndex(int index)
		{
			return this.m_memberLogic.SetInstanceIndex(index);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00035090 File Offset: 0x00033290
		public void ResetContext()
		{
			this.m_memberLogic.ResetContext();
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003509D File Offset: 0x0003329D
		void IDynamicInstance.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000350A5 File Offset: 0x000332A5
		bool IDynamicInstance.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000350AD File Offset: 0x000332AD
		int IDynamicInstance.GetInstanceIndex()
		{
			return this.GetInstanceIndex();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000350B5 File Offset: 0x000332B5
		bool IDynamicInstance.SetInstanceIndex(int index)
		{
			return this.SetInstanceIndex(index);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000350BE File Offset: 0x000332BE
		ScopeID IDynamicInstance.GetScopeID()
		{
			return this.GetScopeID();
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000350C6 File Offset: 0x000332C6
		void IDynamicInstance.SetScopeID(ScopeID scopeID)
		{
			this.SetScopeID(scopeID);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000350CF File Offset: 0x000332CF
		public int GetInstanceIndex()
		{
			return this.m_memberLogic.GetInstanceIndex();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000350DC File Offset: 0x000332DC
		internal ScopeID GetScopeID()
		{
			return this.m_memberLogic.GetScopeID();
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000350E9 File Offset: 0x000332E9
		internal void SetScopeID(ScopeID scopeID)
		{
			this.m_memberLogic.SetScopeID(scopeID);
		}

		// Token: 0x04000545 RID: 1349
		private readonly InternalDynamicMemberLogic m_memberLogic;
	}
}
