using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000368 RID: 872
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class TablixDynamicMemberInstance : TablixMemberInstance, IDynamicInstance, IReportScopeInstance
	{
		// Token: 0x06002144 RID: 8516 RVA: 0x00080CE9 File Offset: 0x0007EEE9
		internal TablixDynamicMemberInstance(Tablix owner, TablixMember memberDef, InternalDynamicMemberLogic memberLogic)
			: base(owner, memberDef)
		{
			this.m_memberLogic = memberLogic;
			this.ResetContext();
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00080D00 File Offset: 0x0007EF00
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x00080D0D File Offset: 0x0007EF0D
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x00080D1A File Offset: 0x0007EF1A
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

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00080D28 File Offset: 0x0007EF28
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00080D30 File Offset: 0x0007EF30
		void IDynamicInstance.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00080D38 File Offset: 0x0007EF38
		bool IDynamicInstance.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00080D40 File Offset: 0x0007EF40
		int IDynamicInstance.GetInstanceIndex()
		{
			return this.GetInstanceIndex();
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00080D48 File Offset: 0x0007EF48
		bool IDynamicInstance.SetInstanceIndex(int index)
		{
			return this.SetInstanceIndex(index);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00080D51 File Offset: 0x0007EF51
		ScopeID IDynamicInstance.GetScopeID()
		{
			return this.GetScopeID();
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00080D59 File Offset: 0x0007EF59
		void IDynamicInstance.SetScopeID(ScopeID scopeID)
		{
			this.SetScopeID(scopeID);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00080D62 File Offset: 0x0007EF62
		public void ResetContext()
		{
			this.m_memberLogic.ResetContext();
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00080D6F File Offset: 0x0007EF6F
		public bool MoveNext()
		{
			return this.m_memberLogic.MoveNext();
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00080D7C File Offset: 0x0007EF7C
		public int GetInstanceIndex()
		{
			return this.m_memberLogic.GetInstanceIndex();
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00080D89 File Offset: 0x0007EF89
		public bool SetInstanceIndex(int index)
		{
			return this.m_memberLogic.SetInstanceIndex(index);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00080D97 File Offset: 0x0007EF97
		internal ScopeID GetScopeID()
		{
			return this.m_memberLogic.GetScopeID();
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00080DA4 File Offset: 0x0007EFA4
		internal void SetScopeID(ScopeID scopeID)
		{
			this.m_memberLogic.SetScopeID(scopeID);
		}

		// Token: 0x040010BD RID: 4285
		private readonly InternalDynamicMemberLogic m_memberLogic;
	}
}
