using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000181 RID: 385
	public sealed class ExpandedNavigationSelectItem : ExpandedReferenceSelectItem
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x00038F5C File Offset: 0x0003715C
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectExpandOption)
			: this(pathToNavigationProperty, navigationSource, selectExpandOption, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00038F94 File Offset: 0x00037194
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, LevelsClause levelsOption)
			: base(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.SelectAndExpand = selectAndExpand;
			this.LevelsOption = levelsOption;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00038FD0 File Offset: 0x000371D0
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, LevelsClause levelsOption, ComputeClause computeOption)
			: base(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption, computeOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.SelectAndExpand = selectAndExpand;
			this.LevelsOption = levelsOption;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00039010 File Offset: 0x00037210
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, LevelsClause levelsOption, ComputeClause computeOption, ApplyClause applyOption)
			: base(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption, computeOption, applyOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.SelectAndExpand = selectAndExpand;
			this.LevelsOption = levelsOption;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00039050 File Offset: 0x00037250
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00039058 File Offset: 0x00037258
		public SelectExpandClause SelectAndExpand { get; private set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00039061 File Offset: 0x00037261
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00039069 File Offset: 0x00037269
		public LevelsClause LevelsOption { get; private set; }

		// Token: 0x06001307 RID: 4871 RVA: 0x00039072 File Offset: 0x00037272
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003907B File Offset: 0x0003727B
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
