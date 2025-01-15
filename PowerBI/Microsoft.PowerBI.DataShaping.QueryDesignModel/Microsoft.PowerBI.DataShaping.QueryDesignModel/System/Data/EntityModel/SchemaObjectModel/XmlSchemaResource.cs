using System;
using System.Collections.Generic;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200005B RID: 91
	internal struct XmlSchemaResource
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x00013A2F File Offset: 0x00011C2F
		public XmlSchemaResource(string namespaceUri, string resourceName, XmlSchemaResource[] importedSchemas)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = importedSchemas;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00013A46 File Offset: 0x00011C46
		public XmlSchemaResource(string namespaceUri, string resourceName)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = XmlSchemaResource.EmptyImportList;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00013A61 File Offset: 0x00011C61
		internal static Dictionary<string, XmlSchemaResource> GetMetadataSchemaResourceMap(double schemaVersion)
		{
			Dictionary<string, XmlSchemaResource> dictionary = new Dictionary<string, XmlSchemaResource>(StringComparer.Ordinal);
			XmlSchemaResource.AddEdmSchemaResourceMapEntries(dictionary, schemaVersion);
			return dictionary;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00013A74 File Offset: 0x00011C74
		internal static void AddEdmSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource[] array = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/codegeneration", "System.Data.Resources.CodeGenerationSchema.xsd")
			};
			XmlSchemaResource[] array2 = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/codegeneration", "System.Data.Resources.CodeGenerationSchema.xsd"),
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/annotation", "System.Data.Resources.AnnotationSchema.xsd")
			};
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm", "System.Data.Resources.CSDLSchema_1.xsd", array);
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/05/edm", "System.Data.Resources.CSDLSchema_1_1.xsd", array);
			schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			if (schemaVersion == 2.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/edm", "System.Data.Resources.CSDLSchema_2.xsd", array2);
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
		}

		// Token: 0x040006E3 RID: 1763
		private static XmlSchemaResource[] EmptyImportList = new XmlSchemaResource[0];

		// Token: 0x040006E4 RID: 1764
		internal string NamespaceUri;

		// Token: 0x040006E5 RID: 1765
		internal string ResourceName;

		// Token: 0x040006E6 RID: 1766
		internal XmlSchemaResource[] ImportedSchemas;
	}
}
