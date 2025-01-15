using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001CA RID: 458
	public sealed class SelectTermToken : SelectExpandTermToken
	{
		// Token: 0x0600150F RID: 5391 RVA: 0x0003C3D5 File Offset: 0x0003A5D5
		public SelectTermToken(PathSegmentToken pathToProperty)
			: this(pathToProperty, null)
		{
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		public SelectTermToken(PathSegmentToken pathToProperty, SelectToken selectOption)
			: this(pathToProperty, null, null, null, null, null, null, selectOption, null)
		{
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0003C414 File Offset: 0x0003A614
		public SelectTermToken(PathSegmentToken pathToProperty, QueryToken filterOption, IEnumerable<OrderByToken> orderByOptions, long? topOption, long? skipOption, bool? countQueryOption, QueryToken searchOption, SelectToken selectOption, ComputeToken computeOption)
			: base(pathToProperty, filterOption, orderByOptions, topOption, skipOption, countQueryOption, searchOption, selectOption, computeOption)
		{
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00025CEB File Offset: 0x00023EEB
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.SelectTerm;
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0003C436 File Offset: 0x0003A636
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
