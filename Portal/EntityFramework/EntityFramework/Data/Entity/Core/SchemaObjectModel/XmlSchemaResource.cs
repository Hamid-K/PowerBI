using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000327 RID: 807
	internal struct XmlSchemaResource
	{
		// Token: 0x06002668 RID: 9832 RVA: 0x0006EA1F File Offset: 0x0006CC1F
		public XmlSchemaResource(string namespaceUri, string resourceName, XmlSchemaResource[] importedSchemas)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = importedSchemas;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x0006EA36 File Offset: 0x0006CC36
		public XmlSchemaResource(string namespaceUri, string resourceName)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = XmlSchemaResource._emptyImportList;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x0006EA51 File Offset: 0x0006CC51
		internal static Dictionary<string, XmlSchemaResource> GetMetadataSchemaResourceMap(double schemaVersion)
		{
			Dictionary<string, XmlSchemaResource> dictionary = new Dictionary<string, XmlSchemaResource>(StringComparer.Ordinal);
			XmlSchemaResource.AddEdmSchemaResourceMapEntries(dictionary, schemaVersion);
			XmlSchemaResource.AddStoreSchemaResourceMapEntries(dictionary, schemaVersion);
			return dictionary;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0006EA6C File Offset: 0x0006CC6C
		internal static void AddStoreSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource[] array = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator", "System.Data.Resources.EntityStoreSchemaGenerator.xsd")
			};
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm/ssdl", "System.Data.Resources.SSDLSchema.xsd", array);
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/ssdl", "System.Data.Resources.SSDLSchema_2.xsd", array);
				schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/edm/ssdl", "System.Data.Resources.SSDLSchema_3.xsd", array);
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
			XmlSchemaResource xmlSchemaResource4 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm/providermanifest", "System.Data.Resources.ProviderServices.ProviderManifest.xsd");
			schemaResourceMap.Add(xmlSchemaResource4.NamespaceUri, xmlSchemaResource4);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0006EB2C File Offset: 0x0006CD2C
		internal static void AddMappingSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("urn:schemas-microsoft-com:windows:storage:mapping:CS", "System.Data.Resources.CSMSL_1.xsd");
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/mapping/cs", "System.Data.Resources.CSMSL_2.xsd");
				schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/mapping/cs", "System.Data.Resources.CSMSL_3.xsd");
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0006EBAC File Offset: 0x0006CDAC
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
			XmlSchemaResource[] array3 = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/codegeneration", "System.Data.Resources.CodeGenerationSchema.xsd"),
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/annotation", "System.Data.Resources.AnnotationSchema.xsd")
			};
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm", "System.Data.Resources.CSDLSchema_1.xsd", array);
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/05/edm", "System.Data.Resources.CSDLSchema_1_1.xsd", array);
			schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/edm", "System.Data.Resources.CSDLSchema_2.xsd", array2);
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource xmlSchemaResource4 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/edm", "System.Data.Resources.CSDLSchema_3.xsd", array3);
				schemaResourceMap.Add(xmlSchemaResource4.NamespaceUri, xmlSchemaResource4);
			}
		}

		// Token: 0x04000D6C RID: 3436
		private static readonly XmlSchemaResource[] _emptyImportList = new XmlSchemaResource[0];

		// Token: 0x04000D6D RID: 3437
		internal string NamespaceUri;

		// Token: 0x04000D6E RID: 3438
		internal string ResourceName;

		// Token: 0x04000D6F RID: 3439
		internal XmlSchemaResource[] ImportedSchemas;
	}
}
