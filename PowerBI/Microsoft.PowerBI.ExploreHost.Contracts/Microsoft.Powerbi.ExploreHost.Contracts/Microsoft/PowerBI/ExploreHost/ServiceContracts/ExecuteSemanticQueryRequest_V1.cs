using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.ServiceContracts
{
	// Token: 0x02000005 RID: 5
	[DataContract(Name = "ExecuteSemanticQueryRequest")]
	public sealed class ExecuteSemanticQueryRequest_V1
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<DataQuery> Queries { get; set; }
	}
}
