using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E3 RID: 739
	internal sealed class BooleanFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x0600234C RID: 9036 RVA: 0x000637DA File Offset: 0x000619DA
		public BooleanFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000637E4 File Offset: 0x000619E4
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000637F4 File Offset: 0x000619F4
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
