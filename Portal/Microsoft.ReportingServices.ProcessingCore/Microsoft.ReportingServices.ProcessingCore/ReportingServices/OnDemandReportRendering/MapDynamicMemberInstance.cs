using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A0 RID: 416
	public sealed class MapDynamicMemberInstance : MapMemberInstance, IDynamicInstance, IReportScopeInstance
	{
		// Token: 0x060010BD RID: 4285 RVA: 0x00047103 File Offset: 0x00045303
		internal MapDynamicMemberInstance(MapDataRegion owner, MapMember memberDef, InternalDynamicMemberLogic memberLogic)
			: base(owner, memberDef)
		{
			this.m_memberLogic = memberLogic;
			this.ResetContext();
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0004711A File Offset: 0x0004531A
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00047127 File Offset: 0x00045327
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00047134 File Offset: 0x00045334
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

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00047142 File Offset: 0x00045342
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004714A File Offset: 0x0004534A
		public bool MoveNext()
		{
			return this.m_memberLogic.MoveNext();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00047157 File Offset: 0x00045357
		public bool SetInstanceIndex(int index)
		{
			return this.m_memberLogic.SetInstanceIndex(index);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00047165 File Offset: 0x00045365
		ScopeID IDynamicInstance.GetScopeID()
		{
			return this.GetScopeID();
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0004716D File Offset: 0x0004536D
		void IDynamicInstance.SetScopeID(ScopeID scopeID)
		{
			this.SetScopeID(scopeID);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00047176 File Offset: 0x00045376
		public void ResetContext()
		{
			this.m_memberLogic.ResetContext();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00047183 File Offset: 0x00045383
		void IDynamicInstance.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004718B File Offset: 0x0004538B
		public int GetInstanceIndex()
		{
			return this.m_memberLogic.GetInstanceIndex();
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00047198 File Offset: 0x00045398
		internal ScopeID GetScopeID()
		{
			return this.m_memberLogic.GetScopeID();
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000471A5 File Offset: 0x000453A5
		internal void SetScopeID(ScopeID scopeID)
		{
			this.m_memberLogic.SetScopeID(scopeID);
		}

		// Token: 0x040007EF RID: 2031
		private readonly InternalDynamicMemberLogic m_memberLogic;
	}
}
