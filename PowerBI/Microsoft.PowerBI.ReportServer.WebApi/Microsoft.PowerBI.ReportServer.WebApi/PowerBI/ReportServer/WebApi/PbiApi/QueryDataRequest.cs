using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x0200002E RID: 46
	[DataContract]
	public class QueryDataRequest
	{
		// Token: 0x0400009D RID: 157
		[DataMember(IsRequired = true, Name = "modelId")]
		public string ModelId;

		// Token: 0x0400009E RID: 158
		[DataMember(IsRequired = true, Name = "queries")]
		public IList<DataQueryRequest> Queries;
	}
}
