using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000107 RID: 263
	public sealed class CollectionConstantNode : CollectionNode
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00025EA4 File Offset: 0x000240A4
		public CollectionConstantNode(IEnumerable<object> objectCollection, string literalText, IEdmCollectionTypeReference collectionType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<object>>(objectCollection, "objectCollection");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(literalText, "literalText");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(collectionType, "collectionType");
			this.LiteralText = literalText;
			EdmCollectionType edmCollectionType = collectionType.Definition as EdmCollectionType;
			this.itemType = edmCollectionType.ElementType;
			this.collectionTypeReference = collectionType;
			foreach (object obj in objectCollection)
			{
				this.collection.Add(new ConstantNode(obj, (obj != null) ? obj.ToString() : "null", this.itemType));
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00025F68 File Offset: 0x00024168
		public IList<ConstantNode> Collection
		{
			get
			{
				return new ReadOnlyCollection<ConstantNode>(this.collection);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00025F75 File Offset: 0x00024175
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x00025F7D File Offset: 0x0002417D
		public string LiteralText { get; private set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00025F86 File Offset: 0x00024186
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00025F8E File Offset: 0x0002418E
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00025F96 File Offset: 0x00024196
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionConstant;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00025F9A File Offset: 0x0002419A
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000773 RID: 1907
		private readonly IList<ConstantNode> collection = new List<ConstantNode>();

		// Token: 0x04000774 RID: 1908
		private readonly IEdmTypeReference itemType;

		// Token: 0x04000775 RID: 1909
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
