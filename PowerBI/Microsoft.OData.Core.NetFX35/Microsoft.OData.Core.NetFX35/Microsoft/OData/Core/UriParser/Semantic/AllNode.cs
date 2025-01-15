using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000221 RID: 545
	public sealed class AllNode : LambdaNode
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x00048AB5 File Offset: 0x00046CB5
		public AllNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00048ABF File Offset: 0x00046CBF
		public AllNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
			: base(rangeVariables, currentRangeVariable)
		{
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00048AC9 File Offset: 0x00046CC9
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00048AD6 File Offset: 0x00046CD6
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.All;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00048ADA File Offset: 0x00046CDA
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
