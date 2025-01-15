using System;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200002C RID: 44
	internal sealed class IntegerFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0000C80F File Offset: 0x0000AA0F
		public IntegerFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000C819 File Offset: 0x0000AA19
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000C828 File Offset: 0x0000AA28
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				base.DefaultValue = num;
			}
		}
	}
}
