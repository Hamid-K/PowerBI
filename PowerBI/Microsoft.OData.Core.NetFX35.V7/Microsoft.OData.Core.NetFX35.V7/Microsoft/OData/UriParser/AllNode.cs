using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000126 RID: 294
	public sealed class AllNode : LambdaNode
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x000288BF File Offset: 0x00026ABF
		public AllNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000288C9 File Offset: 0x00026AC9
		public AllNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
			: base(rangeVariables, currentRangeVariable)
		{
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000288D3 File Offset: 0x00026AD3
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x000288E0 File Offset: 0x00026AE0
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.All;
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000288E4 File Offset: 0x00026AE4
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
