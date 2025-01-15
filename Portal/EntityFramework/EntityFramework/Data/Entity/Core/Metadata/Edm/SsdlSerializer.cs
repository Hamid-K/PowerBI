using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FA RID: 1274
	public class SsdlSerializer
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06003EF4 RID: 16116 RVA: 0x000D14A8 File Offset: 0x000CF6A8
		// (remove) Token: 0x06003EF5 RID: 16117 RVA: 0x000D14E0 File Offset: 0x000CF6E0
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000D1518 File Offset: 0x000CF718
		public virtual bool Serialize(EdmModel dbDatabase, string provider, string providerManifestToken, XmlWriter xmlWriter, bool serializeDefaultNullability = true)
		{
			Check.NotNull<EdmModel>(dbDatabase, "dbDatabase");
			Check.NotEmpty(provider, "provider");
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			if (this.ValidateModel(dbDatabase))
			{
				SsdlSerializer.CreateVisitor(xmlWriter, dbDatabase, serializeDefaultNullability).Visit(dbDatabase, provider, providerManifestToken);
				return true;
			}
			return false;
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000D1574 File Offset: 0x000CF774
		public virtual bool Serialize(EdmModel dbDatabase, string namespaceName, string provider, string providerManifestToken, XmlWriter xmlWriter, bool serializeDefaultNullability = true)
		{
			Check.NotNull<EdmModel>(dbDatabase, "dbDatabase");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotEmpty(provider, "provider");
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			if (this.ValidateModel(dbDatabase))
			{
				SsdlSerializer.CreateVisitor(xmlWriter, dbDatabase, serializeDefaultNullability).Visit(dbDatabase, namespaceName, provider, providerManifestToken);
				return true;
			}
			return false;
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000D15E0 File Offset: 0x000CF7E0
		private bool ValidateModel(EdmModel model)
		{
			bool modelIsValid = true;
			Action<DataModelErrorEventArgs> onErrorAction = delegate(DataModelErrorEventArgs e)
			{
				MetadataItem item = e.Item;
				if (item == null || !MetadataItemHelper.IsInvalid(item))
				{
					modelIsValid = false;
					if (this.OnError != null)
					{
						this.OnError(this, e);
					}
				}
			};
			if (model.NamespaceNames.Count<string>() > 1 || model.Containers.Count<EntityContainer>() != 1)
			{
				onErrorAction(new DataModelErrorEventArgs
				{
					ErrorMessage = Strings.Serializer_OneNamespaceAndOneContainer
				});
			}
			DataModelValidator dataModelValidator = new DataModelValidator();
			dataModelValidator.OnError += delegate(object _, DataModelErrorEventArgs e)
			{
				onErrorAction(e);
			};
			dataModelValidator.Validate(model, true);
			return modelIsValid;
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000D166E File Offset: 0x000CF86E
		private static EdmSerializationVisitor CreateVisitor(XmlWriter xmlWriter, EdmModel dbDatabase, bool serializeDefaultNullability)
		{
			return new EdmSerializationVisitor(xmlWriter, dbDatabase.SchemaVersion, serializeDefaultNullability);
		}
	}
}
