using System;
using System.Data.Entity.Utilities;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E2 RID: 1250
	internal class MslSerializer
	{
		// Token: 0x06003E39 RID: 15929 RVA: 0x000CEB56 File Offset: 0x000CCD56
		public virtual bool Serialize(DbDatabaseMapping databaseMapping, XmlWriter xmlWriter)
		{
			Check.NotNull<DbDatabaseMapping>(databaseMapping, "databaseMapping");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			new MslXmlSchemaWriter(xmlWriter, databaseMapping.Model.SchemaVersion).WriteSchema(databaseMapping);
			return true;
		}
	}
}
