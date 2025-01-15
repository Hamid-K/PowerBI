using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010D RID: 269
	public sealed class InNode : SingleValueNode
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x00026278 File Offset: 0x00024478
		public InNode(SingleValueNode left, CollectionNode right)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<CollectionNode>(right, "right");
			this.left = left;
			this.right = right;
			if (!this.left.GetEdmTypeReference().IsAssignableFrom(this.right.ItemType) && !this.right.ItemType.IsAssignableFrom(this.left.GetEdmTypeReference()))
			{
				throw new ArgumentException(Strings.Nodes_InNode_CollectionItemTypeMustBeSameAsSingleItemType(this.right.ItemType.FullName(), this.left.GetEdmTypeReference().FullName()));
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0002632B File Offset: 0x0002452B
		public SingleValueNode Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00026333 File Offset: 0x00024533
		public CollectionNode Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002633B File Offset: 0x0002453B
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.boolTypeReference;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00026343 File Offset: 0x00024543
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.In;
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00026347 File Offset: 0x00024547
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000780 RID: 1920
		private readonly IEdmTypeReference boolTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof(bool));

		// Token: 0x04000781 RID: 1921
		private readonly SingleValueNode left;

		// Token: 0x04000782 RID: 1922
		private readonly CollectionNode right;
	}
}
