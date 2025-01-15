using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000138 RID: 312
	public class ExpandedReferenceSelectItem : SelectItem
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x000293F8 File Offset: 0x000275F8
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource)
			: this(pathToNavigationProperty, navigationSource, null, null, default(long?), default(long?), default(bool?), null)
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002942C File Offset: 0x0002762C
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption)
			: this(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption, null)
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00029450 File Offset: 0x00027650
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, ComputeClause computeOption)
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
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x000294B4 File Offset: 0x000276B4
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x000294BC File Offset: 0x000276BC
		public ODataExpandPath PathToNavigationProperty { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x000294C5 File Offset: 0x000276C5
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x000294CD File Offset: 0x000276CD
		public IEdmNavigationSource NavigationSource { get; private set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000294D6 File Offset: 0x000276D6
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x000294DE File Offset: 0x000276DE
		public FilterClause FilterOption { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x000294E7 File Offset: 0x000276E7
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x000294EF File Offset: 0x000276EF
		public SearchClause SearchOption { get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x000294F8 File Offset: 0x000276F8
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x00029500 File Offset: 0x00027700
		public OrderByClause OrderByOption { get; private set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00029509 File Offset: 0x00027709
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x00029511 File Offset: 0x00027711
		public ComputeClause ComputeOption { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0002951A File Offset: 0x0002771A
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x00029522 File Offset: 0x00027722
		public long? TopOption { get; private set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0002952B File Offset: 0x0002772B
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x00029533 File Offset: 0x00027733
		public long? SkipOption { get; private set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0002953C File Offset: 0x0002773C
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x00029544 File Offset: 0x00027744
		public bool? CountOption { get; private set; }

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002954D File Offset: 0x0002774D
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00029556 File Offset: 0x00027756
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
