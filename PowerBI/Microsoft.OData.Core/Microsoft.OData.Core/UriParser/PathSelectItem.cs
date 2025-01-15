using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019C RID: 412
	public sealed class PathSelectItem : SelectItem
	{
		// Token: 0x060013DC RID: 5084 RVA: 0x0003AA6C File Offset: 0x00038C6C
		public PathSelectItem(ODataSelectPath selectedPath)
			: this(selectedPath, null, null, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003AAA4 File Offset: 0x00038CA4
		public PathSelectItem(ODataSelectPath selectedPath, IEdmNavigationSource navigationSource, SelectExpandClause selectAndExpand, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, bool? countOption, SearchClause searchOption, ComputeClause computeOption)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSelectPath>(selectedPath, "selectedPath");
			this.SelectedPath = selectedPath;
			this.NavigationSource = navigationSource;
			this.SelectAndExpand = selectAndExpand;
			this.FilterOption = filterOption;
			this.OrderByOption = orderByOption;
			this.TopOption = topOption;
			this.SkipOption = skipOption;
			this.CountOption = countOption;
			this.SearchOption = searchOption;
			this.ComputeOption = computeOption;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0003AB10 File Offset: 0x00038D10
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x0003AB18 File Offset: 0x00038D18
		public ODataSelectPath SelectedPath { get; private set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0003AB21 File Offset: 0x00038D21
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x0003AB29 File Offset: 0x00038D29
		public IEdmNavigationSource NavigationSource { get; internal set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0003AB32 File Offset: 0x00038D32
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x0003AB3A File Offset: 0x00038D3A
		public SelectExpandClause SelectAndExpand { get; internal set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0003AB43 File Offset: 0x00038D43
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x0003AB4B File Offset: 0x00038D4B
		public FilterClause FilterOption { get; internal set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0003AB54 File Offset: 0x00038D54
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public OrderByClause OrderByOption { get; internal set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0003AB65 File Offset: 0x00038D65
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x0003AB6D File Offset: 0x00038D6D
		public long? TopOption { get; internal set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0003AB76 File Offset: 0x00038D76
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x0003AB7E File Offset: 0x00038D7E
		public long? SkipOption { get; internal set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0003AB87 File Offset: 0x00038D87
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x0003AB8F File Offset: 0x00038D8F
		public bool? CountOption { get; internal set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0003AB98 File Offset: 0x00038D98
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x0003ABA0 File Offset: 0x00038DA0
		public SearchClause SearchOption { get; internal set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0003ABA9 File Offset: 0x00038DA9
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x0003ABB1 File Offset: 0x00038DB1
		public ComputeClause ComputeOption { get; internal set; }

		// Token: 0x060013F2 RID: 5106 RVA: 0x0003ABBA File Offset: 0x00038DBA
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0003ABC3 File Offset: 0x00038DC3
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}
