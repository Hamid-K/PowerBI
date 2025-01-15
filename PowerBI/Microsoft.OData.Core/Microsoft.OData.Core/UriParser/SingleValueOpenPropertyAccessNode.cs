using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AD RID: 429
	public sealed class SingleValueOpenPropertyAccessNode : SingleValueNode
	{
		// Token: 0x06001452 RID: 5202 RVA: 0x0003B828 File Offset: 0x00039A28
		public SingleValueOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(openPropertyName, "openPropertyName");
			this.name = openPropertyName;
			this.source = source;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0003B855 File Offset: 0x00039A55
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0003B85D File Offset: 0x00039A5D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0003B865 File Offset: 0x00039A65
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueOpenPropertyAccess;
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0003B869 File Offset: 0x00039A69
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008EF RID: 2287
		private readonly SingleValueNode source;

		// Token: 0x040008F0 RID: 2288
		private readonly string name;
	}
}
