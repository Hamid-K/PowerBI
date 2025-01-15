using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000C1 RID: 193
	public sealed class SingleEntityCastNode : SingleEntityNode
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000FE52 File Offset: 0x0000E052
		public SingleEntityCastNode(SingleEntityNode source, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.source = source;
			this.entitySet = ((source != null) ? source.EntitySet : null);
			this.entityTypeReference = new EdmEntityTypeReference(entityType, false);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000FE8B File Offset: 0x0000E08B
		public SingleEntityNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000FE93 File Offset: 0x0000E093
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000FE9B File Offset: 0x0000E09B
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000FEA3 File Offset: 0x0000E0A3
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000FEAB File Offset: 0x0000E0AB
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleEntityCast;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000FEAF File Offset: 0x0000E0AF
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000196 RID: 406
		private readonly SingleEntityNode source;

		// Token: 0x04000197 RID: 407
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x04000198 RID: 408
		private readonly IEdmEntitySet entitySet;
	}
}
