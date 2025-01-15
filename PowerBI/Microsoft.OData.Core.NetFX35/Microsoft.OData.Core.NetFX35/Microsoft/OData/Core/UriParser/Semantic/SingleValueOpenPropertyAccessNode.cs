using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000264 RID: 612
	public sealed class SingleValueOpenPropertyAccessNode : SingleValueNode
	{
		// Token: 0x06001592 RID: 5522 RVA: 0x0004BBE6 File Offset: 0x00049DE6
		public SingleValueOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(openPropertyName, "openPropertyName");
			this.name = openPropertyName;
			this.source = source;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0004BC12 File Offset: 0x00049E12
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0004BC1A File Offset: 0x00049E1A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0004BC22 File Offset: 0x00049E22
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0004BC25 File Offset: 0x00049E25
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueOpenPropertyAccess;
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0004BC29 File Offset: 0x00049E29
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008FB RID: 2299
		private readonly SingleValueNode source;

		// Token: 0x040008FC RID: 2300
		private readonly string name;
	}
}
