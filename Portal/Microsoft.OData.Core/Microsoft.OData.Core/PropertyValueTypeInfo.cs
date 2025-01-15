using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000022 RID: 34
	internal class PropertyValueTypeInfo
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00003DEC File Offset: 0x00001FEC
		public PropertyValueTypeInfo(string typeName, IEdmTypeReference typeReference)
		{
			this.TypeName = typeName;
			this.TypeReference = typeReference;
			if (typeReference != null)
			{
				this.FullName = typeReference.FullName();
				this.IsPrimitive = typeReference.IsPrimitive();
				this.IsComplex = typeReference.IsComplex();
				this.PrimitiveTypeKind = (this.IsPrimitive ? typeReference.AsPrimitive().PrimitiveKind() : EdmPrimitiveTypeKind.None);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003E50 File Offset: 0x00002050
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00003E58 File Offset: 0x00002058
		public IEdmTypeReference TypeReference { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003E61 File Offset: 0x00002061
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00003E69 File Offset: 0x00002069
		public string FullName { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003E72 File Offset: 0x00002072
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00003E7A File Offset: 0x0000207A
		public bool IsPrimitive { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003E83 File Offset: 0x00002083
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00003E8B File Offset: 0x0000208B
		public bool IsComplex { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003E94 File Offset: 0x00002094
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003E9C File Offset: 0x0000209C
		public string TypeName { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003EA5 File Offset: 0x000020A5
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00003EAD File Offset: 0x000020AD
		public EdmPrimitiveTypeKind PrimitiveTypeKind { get; private set; }
	}
}
