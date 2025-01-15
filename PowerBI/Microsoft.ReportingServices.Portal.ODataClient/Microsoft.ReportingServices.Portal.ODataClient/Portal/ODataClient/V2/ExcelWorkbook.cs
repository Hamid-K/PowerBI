using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200002C RID: 44
	[Key("Id")]
	[EntitySet("ExcelWorkbooks")]
	[OriginalName("ExcelWorkbook")]
	public class ExcelWorkbook : CatalogItem
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00004FB8 File Offset: 0x000031B8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ExcelWorkbook CreateExcelWorkbook(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite)
		{
			return new ExcelWorkbook
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

		// Token: 0x060001D7 RID: 471 RVA: 0x00004FF4 File Offset: 0x000031F4
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<ExcelWorkbook> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<ExcelWorkbook>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}
	}
}
