using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x0200010D RID: 269
	internal sealed class ScopeElementWithDistances
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x00028451 File Offset: 0x00026651
		internal ScopeElementWithDistances(ScopePlanElement scopeElement, int indexInPlan)
		{
			this.m_scopeElement = scopeElement;
			this.m_originalIndexInPlan = indexInPlan;
			this.m_primaryDistance = -1;
			this.m_secondaryDistance = -1;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00028475 File Offset: 0x00026675
		public ScopePlanElement ScopeElement
		{
			get
			{
				return this.m_scopeElement;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002847D File Offset: 0x0002667D
		public int OriginalIndexInPlan
		{
			get
			{
				return this.m_originalIndexInPlan;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00028485 File Offset: 0x00026685
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002848D File Offset: 0x0002668D
		public int PrimaryDistance
		{
			get
			{
				return this.m_primaryDistance;
			}
			set
			{
				this.m_primaryDistance = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00028496 File Offset: 0x00026696
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0002849E File Offset: 0x0002669E
		public int SecondaryDistance
		{
			get
			{
				return this.m_secondaryDistance;
			}
			set
			{
				this.m_secondaryDistance = value;
			}
		}

		// Token: 0x04000517 RID: 1303
		private readonly ScopePlanElement m_scopeElement;

		// Token: 0x04000518 RID: 1304
		private readonly int m_originalIndexInPlan;

		// Token: 0x04000519 RID: 1305
		private int m_primaryDistance;

		// Token: 0x0400051A RID: 1306
		private int m_secondaryDistance;
	}
}
