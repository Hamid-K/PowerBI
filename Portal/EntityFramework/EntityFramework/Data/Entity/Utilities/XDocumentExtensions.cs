using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Edm;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000086 RID: 134
	internal static class XDocumentExtensions
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x00010424 File Offset: 0x0000E624
		public static StorageMappingItemCollection GetStorageMappingItemCollection(this XDocument model, out DbProviderInfo providerInfo)
		{
			EdmItemCollection edmItemCollection = new EdmItemCollection(new XmlReader[] { model.Descendants(EdmXNames.Csdl.SchemaNames).Single<XElement>().CreateReader() });
			XElement xelement = model.Descendants(EdmXNames.Ssdl.SchemaNames).Single<XElement>();
			providerInfo = new DbProviderInfo(xelement.ProviderAttribute(), xelement.ProviderManifestTokenAttribute());
			StoreItemCollection storeItemCollection = new StoreItemCollection(new XmlReader[] { xelement.CreateReader() });
			return new StorageMappingItemCollection(edmItemCollection, storeItemCollection, new XmlReader[] { new XElement(model.Descendants(EdmXNames.Msl.MappingNames).Single<XElement>()).CreateReader() });
		}
	}
}
