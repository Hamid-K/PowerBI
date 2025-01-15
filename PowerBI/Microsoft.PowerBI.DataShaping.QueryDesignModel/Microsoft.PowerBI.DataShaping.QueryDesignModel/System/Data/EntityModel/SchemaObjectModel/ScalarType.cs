using System;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000045 RID: 69
	internal sealed class ScalarType : SchemaType
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x0000F4BD File Offset: 0x0000D6BD
		internal ScalarType(Schema parentElement, string typeName, PrimitiveType primitiveType)
			: base(parentElement)
		{
			this.Name = typeName;
			this._primitiveType = primitiveType;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		public bool TryParse(string text, out object value)
		{
			return ScalarUtils.TryParse(this._primitiveType, text, out value);
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000F4E3 File Offset: 0x0000D6E3
		public PrimitiveTypeKind TypeKind
		{
			get
			{
				return this._primitiveType.PrimitiveTypeKind;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		public PrimitiveType Type
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		internal static byte[] ConvertToByteArray(string text)
		{
			return ScalarUtils.ConvertToByteArray(text);
		}

		// Token: 0x04000690 RID: 1680
		private PrimitiveType _primitiveType;
	}
}
