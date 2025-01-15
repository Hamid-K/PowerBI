using System;
using System.Collections.ObjectModel;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200009E RID: 158
	public sealed class AnyNode : LambdaNode
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000BD79 File Offset: 0x00009F79
		public AnyNode(Collection<RangeVariable> parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BD83 File Offset: 0x00009F83
		public AnyNode(Collection<RangeVariable> parameters, RangeVariable currentRangeVariable)
			: base(parameters, currentRangeVariable)
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000BD8D File Offset: 0x00009F8D
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000BD9A File Offset: 0x00009F9A
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Any;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BD9E File Offset: 0x00009F9E
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
