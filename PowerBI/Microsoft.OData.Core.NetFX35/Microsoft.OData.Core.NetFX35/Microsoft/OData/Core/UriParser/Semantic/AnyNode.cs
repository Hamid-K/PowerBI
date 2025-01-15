using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000222 RID: 546
	public sealed class AnyNode : LambdaNode
	{
		// Token: 0x060013C0 RID: 5056 RVA: 0x00048AEE File Offset: 0x00046CEE
		public AnyNode(Collection<RangeVariable> parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00048AF8 File Offset: 0x00046CF8
		public AnyNode(Collection<RangeVariable> parameters, RangeVariable currentRangeVariable)
			: base(parameters, currentRangeVariable)
		{
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00048B02 File Offset: 0x00046D02
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00048B0F File Offset: 0x00046D0F
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Any;
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00048B13 File Offset: 0x00046D13
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
