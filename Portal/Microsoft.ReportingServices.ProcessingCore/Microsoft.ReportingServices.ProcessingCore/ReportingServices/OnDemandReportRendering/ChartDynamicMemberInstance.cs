using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000257 RID: 599
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class ChartDynamicMemberInstance : ChartMemberInstance, IDynamicInstance, IReportScopeInstance
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x0005E7A5 File Offset: 0x0005C9A5
		internal ChartDynamicMemberInstance(Chart owner, ChartMember memberDef, InternalDynamicMemberLogic memberLogic)
			: base(owner, memberDef)
		{
			this.m_memberLogic = memberLogic;
			this.ResetContext();
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0005E7BC File Offset: 0x0005C9BC
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0005E7C9 File Offset: 0x0005C9C9
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x0005E7D6 File Offset: 0x0005C9D6
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

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0005E7E4 File Offset: 0x0005C9E4
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0005E7EC File Offset: 0x0005C9EC
		void IDynamicInstance.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0005E7F4 File Offset: 0x0005C9F4
		bool IDynamicInstance.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0005E7FC File Offset: 0x0005C9FC
		int IDynamicInstance.GetInstanceIndex()
		{
			return this.GetInstanceIndex();
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0005E804 File Offset: 0x0005CA04
		bool IDynamicInstance.SetInstanceIndex(int index)
		{
			return this.SetInstanceIndex(index);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0005E80D File Offset: 0x0005CA0D
		ScopeID IDynamicInstance.GetScopeID()
		{
			return this.GetScopeID();
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005E815 File Offset: 0x0005CA15
		void IDynamicInstance.SetScopeID(ScopeID scopeID)
		{
			this.SetScopeID(scopeID);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005E81E File Offset: 0x0005CA1E
		public void ResetContext()
		{
			this.m_memberLogic.ResetContext();
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0005E82B File Offset: 0x0005CA2B
		public bool MoveNext()
		{
			return this.m_memberLogic.MoveNext();
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0005E838 File Offset: 0x0005CA38
		public int GetInstanceIndex()
		{
			return this.m_memberLogic.GetInstanceIndex();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0005E845 File Offset: 0x0005CA45
		public bool SetInstanceIndex(int index)
		{
			return this.m_memberLogic.SetInstanceIndex(index);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0005E853 File Offset: 0x0005CA53
		internal ScopeID GetScopeID()
		{
			return this.m_memberLogic.GetScopeID();
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005E860 File Offset: 0x0005CA60
		internal void SetScopeID(ScopeID scopeID)
		{
			this.m_memberLogic.SetScopeID(scopeID);
		}

		// Token: 0x04000B82 RID: 2946
		private readonly InternalDynamicMemberLogic m_memberLogic;
	}
}
