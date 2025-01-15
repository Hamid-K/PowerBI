using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000301 RID: 769
	internal class PrimitiveSchema : Schema
	{
		// Token: 0x06002483 RID: 9347 RVA: 0x000676AC File Offset: 0x000658AC
		public PrimitiveSchema(SchemaManager schemaManager)
			: base(schemaManager)
		{
			base.Schema = this;
			DbProviderManifest providerManifest = base.ProviderManifest;
			if (providerManifest == null)
			{
				base.AddError(new EdmSchemaError(Strings.FailedToRetrieveProviderManifest, 168, EdmSchemaErrorSeverity.Error));
				return;
			}
			IList<PrimitiveType> list = providerManifest.GetStoreTypes();
			if (schemaManager.DataModel == SchemaDataModelOption.EntityDataModel && schemaManager.SchemaVersion < 3.0)
			{
				list = list.Where((PrimitiveType t) => !Helper.IsSpatialType(t)).ToList<PrimitiveType>();
			}
			foreach (PrimitiveType primitiveType in list)
			{
				base.TryAddType(new ScalarType(this, primitiveType.Name, primitiveType), false);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0006777C File Offset: 0x0006597C
		internal override string Alias
		{
			get
			{
				return base.ProviderManifest.NamespaceName;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x00067789 File Offset: 0x00065989
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

		// Token: 0x06002486 RID: 9350 RVA: 0x000677A4 File Offset: 0x000659A4
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}
	}
}
