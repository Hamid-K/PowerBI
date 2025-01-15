using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000133 RID: 307
	public abstract class SelectExpandTermToken : QueryToken
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0002D11C File Offset: 0x0002B31C
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002D180 File Offset: 0x0002B380
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0002D188 File Offset: 0x0002B388
		public PathSegmentToken PathToProperty { get; internal set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002D191 File Offset: 0x0002B391
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0002D199 File Offset: 0x0002B399
		public QueryToken FilterOption { get; private set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002D1A2 File Offset: 0x0002B3A2
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0002D1AA File Offset: 0x0002B3AA
		public IEnumerable<OrderByToken> OrderByOptions { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002D1B3 File Offset: 0x0002B3B3
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0002D1BB File Offset: 0x0002B3BB
		public QueryToken SearchOption { get; private set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		public long? TopOption { get; private set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002D1D5 File Offset: 0x0002B3D5
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0002D1DD File Offset: 0x0002B3DD
		public long? SkipOption { get; private set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002D1E6 File Offset: 0x0002B3E6
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0002D1EE File Offset: 0x0002B3EE
		public bool? CountQueryOption { get; private set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002D1F7 File Offset: 0x0002B3F7
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0002D1FF File Offset: 0x0002B3FF
		public SelectToken SelectOption { get; internal set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002D208 File Offset: 0x0002B408
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0002D210 File Offset: 0x0002B410
		public ComputeToken ComputeOption { get; private set; }
	}
}
