using System;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200023F RID: 575
	public sealed class ExpandedNavigationSelectItem : ExpandedReferenceSelectItem
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x00049A9C File Offset: 0x00047C9C
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectExpandOption)
			: this(pathToNavigationProperty, navigationSource, selectExpandOption, null, null, default(long?), default(long?), default(bool?), null, null)
		{
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00049AD4 File Offset: 0x00047CD4
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, LevelsClause levelsOption)
			: base(pathToNavigationProperty, navigationSource, filterOption, orderByOption, topOption, skipOption, countOption, searchOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "pathToNavigationProperty");
			this.SelectAndExpand = selectAndExpand;
			this.LevelsOption = levelsOption;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00049B0F File Offset: 0x00047D0F
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x00049B17 File Offset: 0x00047D17
		public SelectExpandClause SelectAndExpand { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00049B20 File Offset: 0x00047D20
		// (set) Token: 0x0600149A RID: 5274 RVA: 0x00049B28 File Offset: 0x00047D28
		public LevelsClause LevelsOption { get; private set; }

		// Token: 0x0600149B RID: 5275 RVA: 0x00049B31 File Offset: 0x00047D31
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00049B3A File Offset: 0x00047D3A
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
