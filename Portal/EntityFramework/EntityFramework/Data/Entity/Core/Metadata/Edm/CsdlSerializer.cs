using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000495 RID: 1173
	public class CsdlSerializer
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060039F5 RID: 14837 RVA: 0x000BFECC File Offset: 0x000BE0CC
		// (remove) Token: 0x060039F6 RID: 14838 RVA: 0x000BFF04 File Offset: 0x000BE104
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x060039F7 RID: 14839 RVA: 0x000BFF3C File Offset: 0x000BE13C
		public bool Serialize(EdmModel model, XmlWriter xmlWriter, string modelNamespace = null)
		{
			Check.NotNull<EdmModel>(model, "model");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			bool modelIsValid = true;
			Action<DataModelErrorEventArgs> onErrorAction = delegate(DataModelErrorEventArgs e)
			{
				modelIsValid = false;
				if (this.OnError != null)
				{
					this.OnError(this, e);
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
			if (modelIsValid)
			{
				new EdmSerializationVisitor(xmlWriter, model.SchemaVersion, false).Visit(model, modelNamespace);
				return true;
			}
			return false;
		}
	}
}
