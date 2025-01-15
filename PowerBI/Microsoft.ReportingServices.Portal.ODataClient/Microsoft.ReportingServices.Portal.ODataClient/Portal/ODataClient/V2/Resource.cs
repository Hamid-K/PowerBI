using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200004C RID: 76
	[Key("Id")]
	[EntitySet("Resources")]
	[OriginalName("Resource")]
	public class Resource : CatalogItem
	{
		// Token: 0x06000362 RID: 866 RVA: 0x000080C3 File Offset: 0x000062C3
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

		// Token: 0x06000363 RID: 867 RVA: 0x00008100 File Offset: 0x00006300
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<Resource> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<Resource>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}
	}
}
