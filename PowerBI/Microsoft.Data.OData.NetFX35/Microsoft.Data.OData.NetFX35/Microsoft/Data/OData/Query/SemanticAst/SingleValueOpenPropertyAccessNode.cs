using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020001EC RID: 492
	public sealed class SingleValueOpenPropertyAccessNode : SingleValueNode
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x00033D56 File Offset: 0x00031F56
		public SingleValueOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(openPropertyName, "openPropertyName");
			this.name = openPropertyName;
			this.source = source;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00033D82 File Offset: 0x00031F82
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00033D8A File Offset: 0x00031F8A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00033D92 File Offset: 0x00031F92
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00033D95 File Offset: 0x00031F95
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueOpenPropertyAccess;
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00033D99 File Offset: 0x00031F99
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000536 RID: 1334
		private readonly SingleValueNode source;

		// Token: 0x04000537 RID: 1335
		private readonly string name;
	}
}
