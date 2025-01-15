using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000CB RID: 203
	internal class PropertyValueTypeInfo
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00015B5C File Offset: 0x00013D5C
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

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00015BC0 File Offset: 0x00013DC0
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x00015BC8 File Offset: 0x00013DC8
		public IEdmTypeReference TypeReference { get; private set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00015BD1 File Offset: 0x00013DD1
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00015BD9 File Offset: 0x00013DD9
		public string FullName { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00015BE2 File Offset: 0x00013DE2
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00015BEA File Offset: 0x00013DEA
		public bool IsPrimitive { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00015BF3 File Offset: 0x00013DF3
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00015BFB File Offset: 0x00013DFB
		public bool IsComplex { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00015C04 File Offset: 0x00013E04
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00015C0C File Offset: 0x00013E0C
		public string TypeName { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00015C15 File Offset: 0x00013E15
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00015C1D File Offset: 0x00013E1D
		public EdmPrimitiveTypeKind PrimitiveTypeKind { get; private set; }
	}
}
