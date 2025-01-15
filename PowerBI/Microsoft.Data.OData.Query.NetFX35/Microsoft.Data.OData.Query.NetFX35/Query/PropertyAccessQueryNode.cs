using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000039 RID: 57
	public sealed class PropertyAccessQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000087FB File Offset: 0x000069FB
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00008803 File Offset: 0x00006A03
		public SingleValueQueryNode Source { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000880C File Offset: 0x00006A0C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00008814 File Offset: 0x00006A14
		public IEdmProperty Property { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000881D File Offset: 0x00006A1D
		public override IEdmTypeReference TypeReference
		{
			get
			{
				if (this.Property == null)
				{
					return null;
				}
				return this.Property.Type;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008834 File Offset: 0x00006A34
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.PropertyAccess;
			}
		}
	}
}
