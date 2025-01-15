using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016B RID: 363
	public sealed class AllNode : LambdaNode
	{
		// Token: 0x06001259 RID: 4697 RVA: 0x000381F3 File Offset: 0x000363F3
		public AllNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000381FD File Offset: 0x000363FD
		public AllNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
			: base(rangeVariables, currentRangeVariable)
		{
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00038207 File Offset: 0x00036407
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00038214 File Offset: 0x00036414
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.All;
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00038218 File Offset: 0x00036418
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
