using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000275 RID: 629
	internal sealed class ExpandTermToken : QueryToken
	{
		// Token: 0x060015E7 RID: 5607 RVA: 0x0004C157 File Offset: 0x0004A357
		public ExpandTermToken(PathSegmentToken pathToNavProp)
			: this(pathToNavProp, null, null)
		{
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0004C164 File Offset: 0x0004A364
		public ExpandTermToken(PathSegmentToken pathToNavProp, SelectToken selectOption, ExpandToken expandOption)
			: this(pathToNavProp, null, null, default(long?), default(long?), default(bool?), default(long?), null, selectOption, expandOption)
		{
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0004C1A4 File Offset: 0x0004A3A4
		public ExpandTermToken(PathSegmentToken pathToNavProp, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, long? levelsOption, QueryToken searchOption, SelectToken selectOption, ExpandToken expandOption)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentToken>(pathToNavProp, "property");
			this.pathToNavProp = pathToNavProp;
			this.filterOption = filterOption;
			this.orderByOptions = orderByOptions;
			this.topOption = topOption;
			this.skipOption = skipOption;
			this.countQueryOption = countQueryOption;
			this.levelsOption = levelsOption;
			this.searchOption = searchOption;
			this.selectOption = selectOption;
			this.expandOption = expandOption;
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0004C20F File Offset: 0x0004A40F
		public PathSegmentToken PathToNavProp
		{
			get
			{
				return this.pathToNavProp;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0004C217 File Offset: 0x0004A417
		public QueryToken FilterOption
		{
			get
			{
				return this.filterOption;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0004C21F File Offset: 0x0004A41F
		public IEnumerable<OrderByToken> OrderByOptions
		{
			get
			{
				return this.orderByOptions;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0004C227 File Offset: 0x0004A427
		public long? TopOption
		{
			get
			{
				return this.topOption;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0004C22F File Offset: 0x0004A42F
		public long? SkipOption
		{
			get
			{
				return this.skipOption;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0004C237 File Offset: 0x0004A437
		public bool? CountQueryOption
		{
			get
			{
				return this.countQueryOption;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0004C23F File Offset: 0x0004A43F
		public long? LevelsOption
		{
			get
			{
				return this.levelsOption;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0004C247 File Offset: 0x0004A447
		public QueryToken SearchOption
		{
			get
			{
				return this.searchOption;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0004C24F File Offset: 0x0004A44F
		public SelectToken SelectOption
		{
			get
			{
				return this.selectOption;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0004C257 File Offset: 0x0004A457
		public ExpandToken ExpandOption
		{
			get
			{
				return this.expandOption;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0004C25F File Offset: 0x0004A45F
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ExpandTerm;
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0004C263 File Offset: 0x0004A463
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000915 RID: 2325
		private readonly PathSegmentToken pathToNavProp;

		// Token: 0x04000916 RID: 2326
		private readonly QueryToken filterOption;

		// Token: 0x04000917 RID: 2327
		private readonly IEnumerable<OrderByToken> orderByOptions;

		// Token: 0x04000918 RID: 2328
		private readonly long? topOption;

		// Token: 0x04000919 RID: 2329
		private readonly long? skipOption;

		// Token: 0x0400091A RID: 2330
		private readonly bool? countQueryOption;

		// Token: 0x0400091B RID: 2331
		private readonly long? levelsOption;

		// Token: 0x0400091C RID: 2332
		private readonly QueryToken searchOption;

		// Token: 0x0400091D RID: 2333
		private readonly SelectToken selectOption;

		// Token: 0x0400091E RID: 2334
		private readonly ExpandToken expandOption;
	}
}
