using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000E0 RID: 224
	public sealed class ODataStreamItem : ODataItem
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x0001C30B File Offset: 0x0001A50B
		public ODataStreamItem(EdmPrimitiveTypeKind primitiveTypeKind)
			: this(primitiveTypeKind, null)
		{
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001C315 File Offset: 0x0001A515
		public ODataStreamItem(EdmPrimitiveTypeKind primitiveTypeKind, string contentType)
		{
			this.PrimitiveTypeKind = primitiveTypeKind;
			this.ContentType = contentType;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001C32B File Offset: 0x0001A52B
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0001C333 File Offset: 0x0001A533
		public EdmPrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return this.typeKind;
			}
			private set
			{
				if (this.typeKind != EdmPrimitiveTypeKind.String && this.typeKind != EdmPrimitiveTypeKind.Binary && this.typeKind != EdmPrimitiveTypeKind.None)
				{
					throw new ODataException(Strings.StreamItemInvalidPrimitiveKind(value));
				}
				this.typeKind = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0001C368 File Offset: 0x0001A568
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0001C370 File Offset: 0x0001A570
		public string ContentType { get; private set; }

		// Token: 0x040003C8 RID: 968
		private EdmPrimitiveTypeKind typeKind;
	}
}
