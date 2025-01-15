using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200006B RID: 107
	public sealed class ExpandedNavigationSelectItem : SelectItem
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmEntitySet entitySet, SelectExpandClause selectExpandOption)
			: this(pathToNavigationProperty, entitySet, null, null, default(long?), default(long?), default(InlineCountKind?), selectExpandOption)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A308 File Offset: 0x00008508
		internal ExpandedNavigationSelectItem(ODataExpandPath pathToNavigationProperty, IEdmEntitySet entitySet, FilterClause filterOption, OrderByClause orderByOption, long? topOption, long? skipOption, InlineCountKind? inlineCountOption, SelectExpandClause selectAndExpand)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataExpandPath>(pathToNavigationProperty, "navigationProperty");
			this.pathToNavigationProperty = pathToNavigationProperty;
			this.entitySet = entitySet;
			this.filterOption = filterOption;
			this.orderByOption = orderByOption;
			this.topOption = topOption;
			this.skipOption = skipOption;
			this.inlineCountOption = inlineCountOption;
			this.selectAndExpand = selectAndExpand;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000A363 File Offset: 0x00008563
		public ODataExpandPath PathToNavigationProperty
		{
			get
			{
				return this.pathToNavigationProperty;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000A36B File Offset: 0x0000856B
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000A373 File Offset: 0x00008573
		public SelectExpandClause SelectAndExpand
		{
			get
			{
				return this.selectAndExpand;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A37B File Offset: 0x0000857B
		internal FilterClause FilterOption
		{
			get
			{
				return this.filterOption;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000A383 File Offset: 0x00008583
		internal OrderByClause OrderByOption
		{
			get
			{
				return this.orderByOption;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000A38B File Offset: 0x0000858B
		internal long? TopOption
		{
			get
			{
				return this.topOption;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A393 File Offset: 0x00008593
		internal long? SkipOption
		{
			get
			{
				return this.skipOption;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A39B File Offset: 0x0000859B
		internal InlineCountKind? InlineCountOption
		{
			get
			{
				return this.inlineCountOption;
			}
		}

		// Token: 0x040000AC RID: 172
		private readonly ODataExpandPath pathToNavigationProperty;

		// Token: 0x040000AD RID: 173
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040000AE RID: 174
		private readonly FilterClause filterOption;

		// Token: 0x040000AF RID: 175
		private readonly OrderByClause orderByOption;

		// Token: 0x040000B0 RID: 176
		private readonly long? topOption;

		// Token: 0x040000B1 RID: 177
		private readonly long? skipOption;

		// Token: 0x040000B2 RID: 178
		private readonly InlineCountKind? inlineCountOption;

		// Token: 0x040000B3 RID: 179
		private readonly SelectExpandClause selectAndExpand;
	}
}
