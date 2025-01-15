using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000287 RID: 647
	public sealed class DataDynamicMemberInstance : DataMemberInstance, IDynamicInstance, IReportScopeInstance
	{
		// Token: 0x06001910 RID: 6416 RVA: 0x00066ABE File Offset: 0x00064CBE
		internal DataDynamicMemberInstance(CustomReportItem owner, DataMember memberDef, InternalDynamicMemberLogic memberLogic)
			: base(owner, memberDef)
		{
			this.m_memberLogic = memberLogic;
			this.ResetContext();
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00066AD5 File Offset: 0x00064CD5
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x00066AE2 File Offset: 0x00064CE2
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x00066AEF File Offset: 0x00064CEF
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

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x00066AFD File Offset: 0x00064CFD
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00066B05 File Offset: 0x00064D05
		void IDynamicInstance.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00066B0D File Offset: 0x00064D0D
		bool IDynamicInstance.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00066B15 File Offset: 0x00064D15
		int IDynamicInstance.GetInstanceIndex()
		{
			return this.GetInstanceIndex();
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00066B1D File Offset: 0x00064D1D
		bool IDynamicInstance.SetInstanceIndex(int index)
		{
			return this.SetInstanceIndex(index);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00066B26 File Offset: 0x00064D26
		ScopeID IDynamicInstance.GetScopeID()
		{
			return this.GetScopeID();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00066B2E File Offset: 0x00064D2E
		void IDynamicInstance.SetScopeID(ScopeID scopeID)
		{
			this.SetScopeID(scopeID);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00066B37 File Offset: 0x00064D37
		public void ResetContext()
		{
			this.m_memberLogic.ResetContext();
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00066B44 File Offset: 0x00064D44
		public bool MoveNext()
		{
			return this.m_memberLogic.MoveNext();
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00066B51 File Offset: 0x00064D51
		public int GetInstanceIndex()
		{
			return this.m_memberLogic.GetInstanceIndex();
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00066B5E File Offset: 0x00064D5E
		public bool SetInstanceIndex(int index)
		{
			return this.m_memberLogic.SetInstanceIndex(index);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00066B6C File Offset: 0x00064D6C
		internal ScopeID GetScopeID()
		{
			return this.m_memberLogic.GetScopeID();
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00066B79 File Offset: 0x00064D79
		internal void SetScopeID(ScopeID scopeID)
		{
			this.m_memberLogic.SetScopeID(scopeID);
		}

		// Token: 0x04000CA0 RID: 3232
		private readonly InternalDynamicMemberLogic m_memberLogic;
	}
}
