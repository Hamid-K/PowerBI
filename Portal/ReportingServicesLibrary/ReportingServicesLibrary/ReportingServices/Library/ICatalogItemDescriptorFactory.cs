using System;
using System.Data;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000031 RID: 49
	internal interface ICatalogItemDescriptorFactory
	{
		// Token: 0x06000182 RID: 386
		bool BuildFromDbRow(IDataReader reader, out CatalogItemDescriptor itemDescriptor);
	}
}
