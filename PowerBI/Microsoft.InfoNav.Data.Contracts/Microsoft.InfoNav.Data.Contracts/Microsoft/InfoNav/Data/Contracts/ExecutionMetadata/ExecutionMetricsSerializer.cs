using System;
using System.IO;
using Microsoft.InfoNav.Data.Contracts.Utils;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F4 RID: 244
	public static class ExecutionMetricsSerializer
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0000D6A5 File Offset: 0x0000B8A5
		public static string Serialize(ExecutionMetrics executionMetrics)
		{
			return JsonConvert.SerializeObject(executionMetrics);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000D6AD File Offset: 0x0000B8AD
		public static void Serialize(ExecutionMetrics executionMetrics, Stream stream)
		{
			NewtonsoftJsonSerializationUtil.ToJsonStream<ExecutionMetrics>(executionMetrics, stream);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000D6B6 File Offset: 0x0000B8B6
		public static ExecutionMetrics Deserialize(string rawMetrics)
		{
			return JsonConvert.DeserializeObject<ExecutionMetrics>(rawMetrics);
		}
	}
}
