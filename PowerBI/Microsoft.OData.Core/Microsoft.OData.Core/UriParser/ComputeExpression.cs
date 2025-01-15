using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000109 RID: 265
	public sealed class ComputeExpression
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x00025FE4 File Offset: 0x000241E4
		public ComputeExpression(SingleValueNode expression, string alias, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00026019 File Offset: 0x00024219
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00026021 File Offset: 0x00024221
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00026029 File Offset: 0x00024229
		public IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x04000778 RID: 1912
		private readonly SingleValueNode expression;

		// Token: 0x04000779 RID: 1913
		private readonly string alias;

		// Token: 0x0400077A RID: 1914
		private readonly IEdmTypeReference typeReference;
	}
}
