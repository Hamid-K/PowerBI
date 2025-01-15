using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000173 RID: 371
	public sealed class CollectionOpenPropertyAccessNode : CollectionNode
	{
		// Token: 0x06001295 RID: 4757 RVA: 0x0003873F File Offset: 0x0003693F
		public CollectionOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<string>(openPropertyName, "openPropertyName");
			this.source = source;
			this.name = openPropertyName;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0003876D File Offset: 0x0003696D
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00038775 File Offset: 0x00036975
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmTypeReference ItemType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0003877D File Offset: 0x0003697D
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionOpenPropertyAccess;
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00038781 File Offset: 0x00036981
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000862 RID: 2146
		private readonly SingleValueNode source;

		// Token: 0x04000863 RID: 2147
		private readonly string name;
	}
}
