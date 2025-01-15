using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200008E RID: 142
	internal sealed class DataShapeDynamicMemberInstance : DataShapeMemberInstance, IDynamicInstance, IReportScopeInstance
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x000264ED File Offset: 0x000246ED
		internal DataShapeDynamicMemberInstance(DataShape ownerDataShape, DataShapeMember dataShapeMember, InternalDynamicMemberLogic memberLogic)
			: base(ownerDataShape, dataShapeMember)
		{
			this.m_memberLogic = memberLogic;
			this.ResetContext();
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00026504 File Offset: 0x00024704
		public string UniqueName
		{
			get
			{
				return this.m_dataShapeMember.UniqueName;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00026511 File Offset: 0x00024711
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0002651E File Offset: 0x0002471E
		public bool IsNewContext
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

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0002652C File Offset: 0x0002472C
		public IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00026534 File Offset: 0x00024734
		public void ResetContext()
		{
			this.m_memberLogic.ResetContext();
			if (!this.m_dataShapeMember.IsColumn && this.m_dataShapeMember.Parent == null)
			{
				this.m_ownerDataShape.ResetLimitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits();
			}
			if (this.m_dataShapeMember.IsColumn && this.m_ownerDataShape.StartedRowHierarchyPass && this.m_dataShapeMember.Parent == null)
			{
				this.m_ownerDataShape.ResetColumnHierarchyLimitsWithDataShapeAsWithinScope();
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000265A4 File Offset: 0x000247A4
		public bool MoveNext()
		{
			Global.Tracer.Assert(this.m_dataShapeMember.IsColumn || this.m_ownerDataShape.StartedRowHierarchyPass, "Cannot iterate through rows before calling PrepareRowHierarchy");
			return this.m_memberLogic.MoveNext() && this.IncrementAndCheckLimit();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000265F0 File Offset: 0x000247F0
		private bool IncrementAndCheckLimit()
		{
			this.m_dataShapeMember.ResetWithinLimits();
			return this.m_dataShapeMember.IncrementTargetLimits() && this.m_dataShapeMember.VerifyChildLimitsNotExceeded();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00026617 File Offset: 0x00024817
		public int GetInstanceIndex()
		{
			return this.m_memberLogic.GetInstanceIndex();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00026624 File Offset: 0x00024824
		public bool SetInstanceIndex(int index)
		{
			return this.m_memberLogic.SetInstanceIndex(index);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00026632 File Offset: 0x00024832
		public ScopeID GetScopeID()
		{
			return this.m_memberLogic.GetScopeID();
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002663F File Offset: 0x0002483F
		public ScopeID GetLastScopeID()
		{
			return this.m_memberLogic.GetLastScopeID();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002664C File Offset: 0x0002484C
		public void SetScopeID(ScopeID scopeID)
		{
			Global.Tracer.Assert(false, "SetScopeID should never be called; query restart should be handled in DSQT.");
		}

		// Token: 0x04000248 RID: 584
		private readonly InternalDynamicMemberLogic m_memberLogic;
	}
}
