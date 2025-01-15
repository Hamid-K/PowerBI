using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000240 RID: 576
	public static class EdmxReader
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x00054608 File Offset: 0x00052808
		public static DbCompiledModel Read(XmlReader reader, string defaultSchema)
		{
			Check.NotNull<XmlReader>(reader, "reader");
			DbProviderInfo dbProviderInfo;
			return new DbCompiledModel(CodeFirstCachedMetadataWorkspace.Create(XDocument.Load(reader).GetStorageMappingItemCollection(out dbProviderInfo), dbProviderInfo), defaultSchema);
		}
	}
}
