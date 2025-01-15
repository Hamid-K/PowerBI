using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000182 RID: 386
	public class ExpandedReferenceSelectItem : SelectItem
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x00039084 File Offset: 0x00037284
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource)
			: this(pathToNavigationProperty, navigationSource, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000390B8 File Offset: 0x000372B8
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption)
			: this(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption, null)
		{
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000390DC File Offset: 0x000372DC
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, ComputeClause computeOption)
			: this(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption, computeOption, null)
		{
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00039100 File Offset: 0x00037300
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, ComputeClause computeOption, ApplyClause applyOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.PathToNavigationProperty = pathToNavigationProperty;
			this.NavigationSource = navigationSource;
			this.FilterOption = filterOption;
			this.OrderByOption = orderByOption;
			this.TopOption = topOption;
			this.SkipOption = skipOption;
			this.CountOption = countOption;
			this.SearchOption = searchOption;
			this.ComputeOption = computeOption;
			this.ApplyOption = applyOption;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0003916C File Offset: 0x0003736C
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x00039174 File Offset: 0x00037374
		public ODataExpandPath PathToNavigationProperty { get; private set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0003917D File Offset: 0x0003737D
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x00039185 File Offset: 0x00037385
		public IEdmNavigationSource NavigationSource { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0003918E File Offset: 0x0003738E
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x00039196 File Offset: 0x00037396
		public FilterClause FilterOption { get; private set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0003919F File Offset: 0x0003739F
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x000391A7 File Offset: 0x000373A7
		public SearchClause SearchOption { get; private set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x000391B0 File Offset: 0x000373B0
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x000391B8 File Offset: 0x000373B8
		public OrderByClause OrderByOption { get; private set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x000391C1 File Offset: 0x000373C1
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x000391C9 File Offset: 0x000373C9
		public ComputeClause ComputeOption { get; private set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x000391D2 File Offset: 0x000373D2
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x000391DA File Offset: 0x000373DA
		public ApplyClause ApplyOption { get; private set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x000391E3 File Offset: 0x000373E3
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x000391EB File Offset: 0x000373EB
		public long? TopOption { get; private set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x000391F4 File Offset: 0x000373F4
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x000391FC File Offset: 0x000373FC
		public long? SkipOption { get; private set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00039205 File Offset: 0x00037405
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x0003920D File Offset: 0x0003740D
		public bool? CountOption { get; private set; }

		// Token: 0x06001321 RID: 4897 RVA: 0x00039216 File Offset: 0x00037416
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003921F File Offset: 0x0003741F
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
