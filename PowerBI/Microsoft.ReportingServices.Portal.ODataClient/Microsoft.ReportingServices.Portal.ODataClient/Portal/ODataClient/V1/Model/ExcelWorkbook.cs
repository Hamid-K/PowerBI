using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F5 RID: 245
	[Key("Id")]
	[OriginalName("ExcelWorkbook")]
	public class ExcelWorkbook : CatalogItem
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001566E File Offset: 0x0001386E
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
	}
}
