using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000084 RID: 132
	[Key("Id")]
	[OriginalName("Component")]
	public class Component : Resource
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x0000B83C File Offset: 0x00009A3C
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
