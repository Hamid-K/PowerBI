using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000FB RID: 251
	[Key("Id")]
	[OriginalName("Resource")]
	public class Resource : CatalogItem
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x00015C9E File Offset: 0x00013E9E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Resource CreateResource(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite)
		{
			return new Resource
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite
			};
		}
	}
}
