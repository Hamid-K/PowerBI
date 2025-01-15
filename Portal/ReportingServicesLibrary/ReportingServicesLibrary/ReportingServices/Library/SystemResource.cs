using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200024E RID: 590
	internal class SystemResource
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x0005545E File Offset: 0x0005365E
		internal SystemResource()
		{
			this.Items = new Dictionary<string, string>();
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x00055471 File Offset: 0x00053671
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x00055479 File Offset: 0x00053679
		internal Guid Id { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00055482 File Offset: 0x00053682
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x0005548A File Offset: 0x0005368A
		internal virtual IDictionary<string, string> Items { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00055493 File Offset: 0x00053693
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0005549B File Offset: 0x0005369B
		internal string Name { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x000554A4 File Offset: 0x000536A4
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x000554AC File Offset: 0x000536AC
		internal Guid PackageId { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x000554B5 File Offset: 0x000536B5
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x000554BD File Offset: 0x000536BD
		internal string TypeName { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000554C6 File Offset: 0x000536C6
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x000554CE File Offset: 0x000536CE
		internal string Version { get; set; }

		// Token: 0x040007D8 RID: 2008
		internal const string ContentsFolderName = "fbac82c8-9bad-4dba-929f-c04e7ca4111f";

		// Token: 0x040007D9 RID: 2009
		internal const string MetadataContentFileContentTypeAttrName = "contentType";

		// Token: 0x040007DA RID: 2010
		internal const string MetadataContentFileKeyAttrName = "key";

		// Token: 0x040007DB RID: 2011
		internal const string MetadataContentFilePathAttrName = "path";

		// Token: 0x040007DC RID: 2012
		internal const string MetadataContentFilePropertyName = "Item";

		// Token: 0x040007DD RID: 2013
		internal const string MetadataContentsPropertyName = "Contents";

		// Token: 0x040007DE RID: 2014
		internal const string MetadataFileName = "metadata.xml";

		// Token: 0x040007DF RID: 2015
		internal const string MetadataNameAttrName = "name";

		// Token: 0x040007E0 RID: 2016
		internal const string MetadataRootElementName = "SystemResourcePackage";

		// Token: 0x040007E1 RID: 2017
		internal const string MetadataSchemaNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata";

		// Token: 0x040007E2 RID: 2018
		internal const string MetadataTypeAttrName = "type";

		// Token: 0x040007E3 RID: 2019
		internal const string MetadataVersionAttrName = "version";

		// Token: 0x040007E4 RID: 2020
		internal const string RootFolderFriendlyName = "System Resources";

		// Token: 0x040007E5 RID: 2021
		internal const string RootFolderName = "68f0607b-9378-4bbb-9e70-4da3d7d66838";

		// Token: 0x040007E6 RID: 2022
		internal const string RootFolderPath = "/68f0607b-9378-4bbb-9e70-4da3d7d66838";

		// Token: 0x040007E7 RID: 2023
		internal const string SoapPropertyItemPropertyNamePrefix = "Item.";

		// Token: 0x040007E8 RID: 2024
		internal const string SoapPropertyNamePropertyName = "Resource.Name";

		// Token: 0x040007E9 RID: 2025
		internal const string SoapPropertyPackageIdPropertyName = "Resource.PackageId";

		// Token: 0x040007EA RID: 2026
		internal const string SoapPropertyTypePropertyName = "Resource.Type";

		// Token: 0x040007EB RID: 2027
		internal const string SoapPropertyVersionPropertyName = "Resource.Version";
	}
}
