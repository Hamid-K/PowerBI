using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000137 RID: 311
	public sealed class ExpandedNavigationSelectItem : ExpandedReferenceSelectItem
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00029310 File Offset: 0x00027510
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectExpandOption)
			: this(pathToNavigationProperty, navigationSource, selectExpandOption, null, null, default(long?), default(long?), default(bool?), null, null)
		{
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00029348 File Offset: 0x00027548
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, LevelsClause levelsOption)
			: base(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.SelectAndExpand = selectAndExpand;
			this.LevelsOption = levelsOption;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00029384 File Offset: 0x00027584
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, LevelsClause levelsOption, ComputeClause computeOption)
			: base(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption, computeOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.SelectAndExpand = selectAndExpand;
			this.LevelsOption = levelsOption;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000293C2 File Offset: 0x000275C2
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x000293CA File Offset: 0x000275CA
		public SelectExpandClause SelectAndExpand { get; private set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x000293D3 File Offset: 0x000275D3
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x000293DB File Offset: 0x000275DB
		public LevelsClause LevelsOption { get; private set; }

		// Token: 0x06000E04 RID: 3588 RVA: 0x000293E4 File Offset: 0x000275E4
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000293ED File Offset: 0x000275ED
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
