using System;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200023E RID: 574
	public class ExpandedReferenceSelectItem : SelectItem
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x00049970 File Offset: 0x00047B70
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource)
			: this(pathToNavigationProperty, navigationSource, null, null, default(long?), default(long?), default(bool?), null)
		{
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000499A4 File Offset: 0x00047BA4
		public ExpandedReferenceSelectItem(ODataExpandPath pathToNavigationProperty, IEdmNavigationSource navigationSource, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption)
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
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000499FF File Offset: 0x00047BFF
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00049A07 File Offset: 0x00047C07
		public ODataExpandPath PathToNavigationProperty { get; private set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00049A10 File Offset: 0x00047C10
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00049A18 File Offset: 0x00047C18
		public IEdmNavigationSource NavigationSource { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00049A21 File Offset: 0x00047C21
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00049A29 File Offset: 0x00047C29
		public FilterClause FilterOption { get; private set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00049A32 File Offset: 0x00047C32
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00049A3A File Offset: 0x00047C3A
		public SearchClause SearchOption { get; private set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00049A43 File Offset: 0x00047C43
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00049A4B File Offset: 0x00047C4B
		public OrderByClause OrderByOption { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00049A54 File Offset: 0x00047C54
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00049A5C File Offset: 0x00047C5C
		public long? TopOption { get; private set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00049A65 File Offset: 0x00047C65
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00049A6D File Offset: 0x00047C6D
		public long? SkipOption { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00049A76 File Offset: 0x00047C76
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00049A7E File Offset: 0x00047C7E
		public bool? CountOption { get; private set; }

		// Token: 0x06001493 RID: 5267 RVA: 0x00049A87 File Offset: 0x00047C87
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00049A90 File Offset: 0x00047C90
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
