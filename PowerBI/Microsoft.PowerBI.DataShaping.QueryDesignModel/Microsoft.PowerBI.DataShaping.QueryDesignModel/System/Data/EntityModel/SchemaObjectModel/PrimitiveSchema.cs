using System;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000038 RID: 56
	internal class PrimitiveSchema : Schema
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public PrimitiveSchema(SchemaManager schemaManager)
			: base(schemaManager)
		{
			base.Schema = this;
			foreach (PrimitiveType primitiveType in EdmProviderManifest.Instance.GetStoreTypes())
			{
				base.TryAddType(new ScalarType(this, primitiveType.Name, primitiveType), false);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000D650 File Offset: 0x0000B850
		internal override string Alias
		{
			get
			{
				return base.ProviderManifest.NamespaceName;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0000D65D File Offset: 0x0000B85D
		internal override string Namespace
		{
			get
			{
				if (base.ProviderManifest != null)
				{
					return base.ProviderManifest.NamespaceName;
				}
				return string.Empty;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000D678 File Offset: 0x0000B878
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}
	}
}
