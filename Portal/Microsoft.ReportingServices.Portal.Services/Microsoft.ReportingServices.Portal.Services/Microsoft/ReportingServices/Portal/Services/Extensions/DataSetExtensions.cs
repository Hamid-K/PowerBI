using System;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Library.Soap2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x0200005F RID: 95
	internal static class DataSetExtensions
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00012DC7 File Offset: 0x00010FC7
		internal static ItemReference ToItemReference2010(this DataSet dataSet)
		{
			return new ItemReference
			{
				Name = dataSet.Name,
				Reference = dataSet.Path
			};
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012DE8 File Offset: 0x00010FE8
		internal static DataSet ToDataSet(this DataSetEntity datasetEntity)
		{
			return new DataSet
			{
				Id = datasetEntity.LinkID.GetValueOrDefault(),
				Name = datasetEntity.Name,
				Path = datasetEntity.Path
			};
		}
	}
}
