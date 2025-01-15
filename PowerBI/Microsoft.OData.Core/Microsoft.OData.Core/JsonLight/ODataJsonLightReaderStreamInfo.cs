using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000224 RID: 548
	internal sealed class ODataJsonLightReaderStreamInfo
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x00044875 File Offset: 0x00042A75
		internal ODataJsonLightReaderStreamInfo(EdmPrimitiveTypeKind primitiveTypeKind)
		{
			this.PrimitiveTypeKind = primitiveTypeKind;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00044884 File Offset: 0x00042A84
		internal ODataJsonLightReaderStreamInfo(EdmPrimitiveTypeKind primitiveTypeKind, string contentType)
		{
			this.PrimitiveTypeKind = primitiveTypeKind;
			this.ContentType = contentType;
			if (contentType.Contains("application/json"))
			{
				this.PrimitiveTypeKind = EdmPrimitiveTypeKind.String;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000448AF File Offset: 0x00042AAF
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x000448B7 File Offset: 0x00042AB7
		internal EdmPrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return this.primitiveTypeKind;
			}
			set
			{
				this.primitiveTypeKind = ((value == EdmPrimitiveTypeKind.Stream) ? EdmPrimitiveTypeKind.Binary : value);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x000448C8 File Offset: 0x00042AC8
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x000448D0 File Offset: 0x00042AD0
		internal string ContentType { get; private set; }

		// Token: 0x04000AB6 RID: 2742
		private EdmPrimitiveTypeKind primitiveTypeKind;
	}
}
