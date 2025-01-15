using System;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200001A RID: 26
	internal sealed class ByteFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00009A6E File Offset: 0x00007C6E
		public ByteFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00009A78 File Offset: 0x00007C78
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte);
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00009A88 File Offset: 0x00007C88
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			byte b = 0;
			if (base.HandleByteAttribute(reader, ref b))
			{
				base.DefaultValue = b;
			}
		}
	}
}
