using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200011F RID: 287
	public sealed class ExpandTermToken : SelectExpandTermToken
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002CB7A File Offset: 0x0002AD7A
		public ExpandTermToken(PathSegmentToken pathToNavigationProp)
			: this(pathToNavigationProp, null, null)
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002CB88 File Offset: 0x0002AD88
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavigationProp, null, null, null, null, null, null, null, selectOption, expandOption)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, levelsOption, searchOption, selectOption, expandOption, null)
		{
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002CBF0 File Offset: 0x0002ADF0
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption, ComputeToken computeOption)
			: this(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, levelsOption, searchOption, selectOption, expandOption, computeOption, null)
		{
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002CC18 File Offset: 0x0002AE18
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption, ComputeToken computeOption, IEnumerable<QueryToken> applyOptions)
			: base(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, searchOption, selectOption, computeOption)
		{
			this.ExpandOption = expandOption;
			this.LevelsOption = levelsOption;
			this.ApplyOptions = applyOptions;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0002CC52 File Offset: 0x0002AE52
		public PathSegmentToken PathToNavigationProp
		{
			get
			{
				return base.PathToProperty;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002CC5A File Offset: 0x0002AE5A
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0002CC62 File Offset: 0x0002AE62
		public ExpandToken ExpandOption { get; internal set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002CC6B File Offset: 0x0002AE6B
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0002CC73 File Offset: 0x0002AE73
		public long? LevelsOption { get; private set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002CC7C File Offset: 0x0002AE7C
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0002CC84 File Offset: 0x0002AE84
		public IEnumerable<QueryToken> ApplyOptions { get; private set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002CC8D File Offset: 0x0002AE8D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ExpandTerm;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002CC91 File Offset: 0x0002AE91
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
