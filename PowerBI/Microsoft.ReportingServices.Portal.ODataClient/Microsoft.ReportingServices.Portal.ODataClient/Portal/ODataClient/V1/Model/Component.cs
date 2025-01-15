using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000FD RID: 253
	[Key("Id")]
	[OriginalName("Component")]
	public class Component : Resource
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x00015E32 File Offset: 0x00014032
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Component CreateComponent(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite)
		{
			return new Component
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
