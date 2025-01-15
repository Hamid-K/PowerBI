using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.ServiceContracts
{
	// Token: 0x02000006 RID: 6
	[DataContract(Name = "ExecuteSemanticQueryRequest")]
	public sealed class ExecuteSemanticQueryRequest : VersionedJsonProtocolBase
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		public ExecuteSemanticQueryRequest()
		{
			base.Version = "2.0.0";
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002093 File Offset: 0x00000293
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000209B File Offset: 0x0000029B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<DataQueryRequest> Queries { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020AC File Offset: 0x000002AC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<CancelQueryRequest> CancelQueries { get; set; }

		// Token: 0x0600000B RID: 11 RVA: 0x000020B5 File Offset: 0x000002B5
		internal static ExecuteSemanticQueryRequest Upgrade(ExecuteSemanticQueryRequest_V1 v1request)
		{
			ExecuteSemanticQueryRequest executeSemanticQueryRequest = new ExecuteSemanticQueryRequest();
			executeSemanticQueryRequest.Queries = v1request.Queries.Select((DataQuery q) => new DataQueryRequest
			{
				Query = q
			}).AsList<DataQueryRequest>();
			return executeSemanticQueryRequest;
		}

		// Token: 0x0400002A RID: 42
		public const string ProtocolVersion = "2.0.0";
	}
}
