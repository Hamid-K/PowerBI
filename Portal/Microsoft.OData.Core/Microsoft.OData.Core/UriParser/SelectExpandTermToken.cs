using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C9 RID: 457
	public abstract class SelectExpandTermToken : QueryToken
	{
		// Token: 0x060014FC RID: 5372 RVA: 0x0003C2D8 File Offset: 0x0003A4D8
		protected SelectExpandTermToken(PathSegmentToken pathToProperty, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, QueryToken searchOption, SelectToken selectOption, ComputeToken computeOption)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentToken>(pathToProperty, "property");
			this.PathToProperty = pathToProperty;
			this.FilterOption = filterOption;
			this.OrderByOptions = orderByOptions;
			this.TopOption = topOption;
			this.SkipOption = skipOption;
			this.CountQueryOption = countQueryOption;
			this.SearchOption = searchOption;
			this.SelectOption = selectOption;
			this.ComputeOption = computeOption;
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0003C33C File Offset: 0x0003A53C
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0003C344 File Offset: 0x0003A544
		public PathSegmentToken PathToProperty { get; internal set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0003C34D File Offset: 0x0003A54D
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x0003C355 File Offset: 0x0003A555
		public QueryToken FilterOption { get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0003C35E File Offset: 0x0003A55E
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x0003C366 File Offset: 0x0003A566
		public IEnumerable<OrderByToken> OrderByOptions { get; private set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0003C36F File Offset: 0x0003A56F
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x0003C377 File Offset: 0x0003A577
		public QueryToken SearchOption { get; private set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0003C380 File Offset: 0x0003A580
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x0003C388 File Offset: 0x0003A588
		public long? TopOption { get; private set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0003C391 File Offset: 0x0003A591
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x0003C399 File Offset: 0x0003A599
		public long? SkipOption { get; private set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0003C3A2 File Offset: 0x0003A5A2
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x0003C3AA File Offset: 0x0003A5AA
		public bool? CountQueryOption { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0003C3B3 File Offset: 0x0003A5B3
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0003C3BB File Offset: 0x0003A5BB
		public SelectToken SelectOption { get; internal set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0003C3C4 File Offset: 0x0003A5C4
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		public ComputeToken ComputeOption { get; private set; }
	}
}
