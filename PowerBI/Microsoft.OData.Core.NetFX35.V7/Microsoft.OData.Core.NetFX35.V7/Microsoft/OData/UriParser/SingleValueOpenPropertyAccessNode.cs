using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015F RID: 351
	public sealed class SingleValueOpenPropertyAccessNode : SingleValueNode
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x0002B77C File Offset: 0x0002997C
		public SingleValueOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(openPropertyName, "openPropertyName");
			this.name = openPropertyName;
			this.source = source;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002B7A9 File Offset: 0x000299A9
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002B7B1 File Offset: 0x000299B1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0000B41B File Offset: 0x0000961B
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002B7B9 File Offset: 0x000299B9
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueOpenPropertyAccess;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002B7BD File Offset: 0x000299BD
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040007A2 RID: 1954
		private readonly SingleValueNode source;

		// Token: 0x040007A3 RID: 1955
		private readonly string name;
	}
}
