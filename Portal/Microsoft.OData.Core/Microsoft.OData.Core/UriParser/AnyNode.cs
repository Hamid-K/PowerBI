using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016C RID: 364
	public sealed class AnyNode : LambdaNode
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0003822D File Offset: 0x0003642D
		public AnyNode(Collection<RangeVariable> parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000381FD File Offset: 0x000363FD
		public AnyNode(Collection<RangeVariable> parameters, RangeVariable currentRangeVariable)
			: base(parameters, currentRangeVariable)
		{
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00038207 File Offset: 0x00036407
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00038237 File Offset: 0x00036437
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Any;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0003823B File Offset: 0x0003643B
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
