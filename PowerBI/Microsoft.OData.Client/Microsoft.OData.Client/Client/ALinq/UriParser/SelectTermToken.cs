using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000134 RID: 308
	public sealed class SelectTermToken : SelectExpandTermToken
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x0002D219 File Offset: 0x0002B419
		public SelectTermToken(PathSegmentToken pathToProperty)
			: this(pathToProperty, null)
		{
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002D224 File Offset: 0x0002B424
		public SelectTermToken(PathSegmentToken pathToProperty, SelectToken selectOption)
			: this(pathToProperty, null, null, null, null, null, null, selectOption, null)
		{
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002D258 File Offset: 0x0002B458
		public SelectTermToken(PathSegmentToken pathToProperty, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, QueryToken searchOption, SelectToken selectOption, ComputeToken computeOption)
			: base(pathToProperty, filterOption, orderByOptions, topOption, skipOption, countQueryOption, searchOption, selectOption, computeOption)
		{
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002D27A File Offset: 0x0002B47A
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.SelectTerm;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002D27E File Offset: 0x0002B47E
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
