using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F6 RID: 758
	internal sealed class IntegerFacetDescriptionElement : FacetDescriptionElement
	{
		// Token: 0x0600242A RID: 9258 RVA: 0x000668A1 File Offset: 0x00064AA1
		public IntegerFacetDescriptionElement(TypeElement type, string name)
			: base(type, name)
		{
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x000668AB File Offset: 0x00064AAB
		public override EdmType FacetType
		{
			get
			{
				return MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000668BC File Offset: 0x00064ABC
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
