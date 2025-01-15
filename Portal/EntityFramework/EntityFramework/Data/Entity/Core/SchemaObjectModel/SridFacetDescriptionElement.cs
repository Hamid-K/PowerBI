using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200031D RID: 797
	internal sealed class SridFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x060025E8 RID: 9704 RVA: 0x0006C5E2 File Offset: 0x0006A7E2
		public SridFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x0006C5EC File Offset: 0x0006A7EC
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0006C5FC File Offset: 0x0006A7FC
		protected override void HandleDefaultAttribute(XmlReader reader)
		{
			if (reader.Value.Trim() == "Variable")
			{
				base.DefaultValue = EdmConstants.VariableValue;
				return;
			}
			int num = -1;
			if (base.HandleIntAttribute(reader, ref num))
			{
				base.DefaultValue = num;
			}
		}
	}
}
