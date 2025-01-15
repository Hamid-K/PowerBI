using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016F RID: 367
	public sealed class ExpandTermToken : QueryToken
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0002BD6D File Offset: 0x00029F6D
		public ExpandTermToken(PathSegmentToken pathToNavigationProp)
			: this(pathToNavigationProp, null, null)
		{
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002BD78 File Offset: 0x00029F78
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavigationProp, null, null, default(long?), default(long?), default(bool?), default(long?), null, selectOption, expandOption)
		{
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002BDB8 File Offset: 0x00029FB8
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, levelsOption, searchOption, selectOption, expandOption, null)
		{
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption, ComputeToken computeOption)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentToken>(pathToNavigationProp, "property");
			this.pathToNavigationProp = pathToNavigationProp;
			this.filterOption = filterOption;
			this.orderByOptions = orderByOptions;
			this.topOption = topOption;
			this.skipOption = skipOption;
			this.countQueryOption = countQueryOption;
			this.levelsOption = levelsOption;
			this.searchOption = searchOption;
			this.selectOption = selectOption;
			this.computeOption = computeOption;
			this.expandOption = expandOption;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002BE54 File Offset: 0x0002A054
		public PathSegmentToken PathToNavigationProp
		{
			get
			{
				return this.pathToNavigationProp;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0002BE5C File Offset: 0x0002A05C
		public QueryToken FilterOption
		{
			get
			{
				return this.filterOption;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0002BE64 File Offset: 0x0002A064
		public IEnumerable<OrderByToken> OrderByOptions
		{
			get
			{
				return this.orderByOptions;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0002BE6C File Offset: 0x0002A06C
		public long? TopOption
		{
			get
			{
				return this.topOption;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0002BE74 File Offset: 0x0002A074
		public long? SkipOption
		{
			get
			{
				return this.skipOption;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0002BE7C File Offset: 0x0002A07C
		public bool? CountQueryOption
		{
			get
			{
				return this.countQueryOption;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002BE84 File Offset: 0x0002A084
		public long? LevelsOption
		{
			get
			{
				return this.levelsOption;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0002BE8C File Offset: 0x0002A08C
		public QueryToken SearchOption
		{
			get
			{
				return this.searchOption;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002BE94 File Offset: 0x0002A094
		public SelectToken SelectOption
		{
			get
			{
				return this.selectOption;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002BE9C File Offset: 0x0002A09C
		public ComputeToken ComputeOption
		{
			get
			{
				return this.computeOption;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0002BEA4 File Offset: 0x0002A0A4
		public ExpandToken ExpandOption
		{
			get
			{
				return this.expandOption;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0002981D File Offset: 0x00027A1D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ExpandTerm;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002BEAC File Offset: 0x0002A0AC
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007BB RID: 1979
		private readonly PathSegmentToken pathToNavigationProp;

		// Token: 0x040007BC RID: 1980
		private readonly QueryToken filterOption;

		// Token: 0x040007BD RID: 1981
		private readonly IEnumerable<OrderByToken> orderByOptions;

		// Token: 0x040007BE RID: 1982
		private readonly long? topOption;

		// Token: 0x040007BF RID: 1983
		private readonly long? skipOption;

		// Token: 0x040007C0 RID: 1984
		private readonly bool? countQueryOption;

		// Token: 0x040007C1 RID: 1985
		private readonly long? levelsOption;

		// Token: 0x040007C2 RID: 1986
		private readonly QueryToken searchOption;

		// Token: 0x040007C3 RID: 1987
		private readonly SelectToken selectOption;

		// Token: 0x040007C4 RID: 1988
		private readonly ComputeToken computeOption;

		// Token: 0x040007C5 RID: 1989
		private readonly ExpandToken expandOption;
	}
}
