using System;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000019 RID: 25
	internal sealed class BooleanFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00009A2F File Offset: 0x00007C2F
		public BooleanFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00009A39 File Offset: 0x00007C39
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00009A48 File Offset: 0x00007C48
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			bool flag = false;
			if (base.HandleBoolAttribute(reader, ref flag))
			{
				base.DefaultValue = flag;
			}
		}
	}
}
