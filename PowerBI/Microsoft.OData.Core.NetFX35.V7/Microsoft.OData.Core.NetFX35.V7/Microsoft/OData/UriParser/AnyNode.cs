using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000127 RID: 295
	public sealed class AnyNode : LambdaNode
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x000288F9 File Offset: 0x00026AF9
		public AnyNode(Collection<RangeVariable> parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000288C9 File Offset: 0x00026AC9
		public AnyNode(Collection<RangeVariable> parameters, RangeVariable currentRangeVariable)
			: base(parameters, currentRangeVariable)
		{
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000288D3 File Offset: 0x00026AD3
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00028903 File Offset: 0x00026B03
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Any;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00028907 File Offset: 0x00026B07
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
