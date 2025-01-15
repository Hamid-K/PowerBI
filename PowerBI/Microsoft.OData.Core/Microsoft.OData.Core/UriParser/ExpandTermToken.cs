using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BB RID: 443
	public sealed class ExpandTermToken : SelectExpandTermToken
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0003BD69 File Offset: 0x00039F69
		public ExpandTermToken(PathSegmentToken pathToNavigationProp)
			: this(pathToNavigationProp, null, null)
		{
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0003BD74 File Offset: 0x00039F74
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavigationProp, null, null, null, null, null, null, null, selectOption, expandOption)
		{
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, levelsOption, searchOption, selectOption, expandOption, null)
		{
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0003BDDC File Offset: 0x00039FDC
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption, ComputeToken computeOption)
			: this(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, levelsOption, searchOption, selectOption, expandOption, computeOption, null)
		{
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0003BE04 File Offset: 0x0003A004
		public ExpandTermToken(PathSegmentToken pathToNavigationProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption, ComputeToken computeOption, IEnumerable<QueryToken> applyOptions)
			: base(pathToNavigationProp, filterOption, orderByOptions, topOption, skipOption, countQueryOption, searchOption, selectOption, computeOption)
		{
			this.ExpandOption = expandOption;
			this.LevelsOption = levelsOption;
			this.ApplyOptions = applyOptions;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0003BE3E File Offset: 0x0003A03E
		public PathSegmentToken PathToNavigationProp
		{
			get
			{
				return base.PathToProperty;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0003BE46 File Offset: 0x0003A046
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x0003BE4E File Offset: 0x0003A04E
		public ExpandToken ExpandOption { get; internal set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0003BE57 File Offset: 0x0003A057
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x0003BE5F File Offset: 0x0003A05F
		public long? LevelsOption { get; private set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0003BE68 File Offset: 0x0003A068
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x0003BE70 File Offset: 0x0003A070
		public IEnumerable<QueryToken> ApplyOptions { get; private set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000394E5 File Offset: 0x000376E5
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ExpandTerm;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0003BE79 File Offset: 0x0003A079
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
