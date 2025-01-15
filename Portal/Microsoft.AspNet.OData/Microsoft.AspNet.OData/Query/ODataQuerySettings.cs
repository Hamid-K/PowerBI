using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D0 RID: 208
	public class ODataQuerySettings
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x000179E8 File Offset: 0x00015BE8
		public ODataQuerySettings()
		{
			this.EnsureStableOrdering = true;
			this.EnableConstantParameterization = true;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000179FE File Offset: 0x00015BFE
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00017A08 File Offset: 0x00015C08
		internal int? ModelBoundPageSize
		{
			get
			{
				return this._modelBoundPageSize;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() <= num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
					}
				}
				this._modelBoundPageSize = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00017A57 File Offset: 0x00015C57
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00017A5F File Offset: 0x00015C5F
		public bool EnsureStableOrdering { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00017A68 File Offset: 0x00015C68
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00017A70 File Offset: 0x00015C70
		public HandleNullPropagationOption HandleNullPropagation
		{
			get
			{
				return this._handleNullPropagationOption;
			}
			set
			{
				HandleNullPropagationOptionHelper.Validate(value, "value");
				this._handleNullPropagationOption = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00017A84 File Offset: 0x00015C84
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00017A8C File Offset: 0x00015C8C
		public bool EnableConstantParameterization { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00017A95 File Offset: 0x00015C95
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00017A9D File Offset: 0x00015C9D
		public bool EnableCorrelatedSubqueryBuffering { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00017AA6 File Offset: 0x00015CA6
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00017AB0 File Offset: 0x00015CB0
		public int? PageSize
		{
			get
			{
				return this._pageSize;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() <= num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
					}
				}
				this._pageSize = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00017AFF File Offset: 0x00015CFF
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00017B07 File Offset: 0x00015D07
		public bool HandleReferenceNavigationPropertyExpandFilter { get; set; }

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017B10 File Offset: 0x00015D10
		internal void CopyFrom(ODataQuerySettings settings)
		{
			this.EnsureStableOrdering = settings.EnsureStableOrdering;
			this.EnableConstantParameterization = settings.EnableConstantParameterization;
			this.HandleNullPropagation = settings.HandleNullPropagation;
			this.PageSize = settings.PageSize;
			this.ModelBoundPageSize = settings.ModelBoundPageSize;
			this.HandleReferenceNavigationPropertyExpandFilter = settings.HandleReferenceNavigationPropertyExpandFilter;
			this.EnableCorrelatedSubqueryBuffering = settings.EnableCorrelatedSubqueryBuffering;
		}

		// Token: 0x04000201 RID: 513
		private HandleNullPropagationOption _handleNullPropagationOption;

		// Token: 0x04000202 RID: 514
		private int? _pageSize;

		// Token: 0x04000203 RID: 515
		private int? _modelBoundPageSize;
	}
}
