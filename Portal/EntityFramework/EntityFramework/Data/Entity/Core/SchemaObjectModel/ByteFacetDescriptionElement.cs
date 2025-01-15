using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E4 RID: 740
	internal sealed class ByteFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x0006381A File Offset: 0x00061A1A
		public ByteFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00063824 File Offset: 0x00061A24
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte);
			}
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00063834 File Offset: 0x00061A34
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
