using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200000E RID: 14
	[DataContract]
	public class EvaluationTraceContext
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002430 File Offset: 0x00000630
		public override string ToString()
		{
			if (this.ServiceTraceContexts == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ServiceTraceContext serviceTraceContext in this.ServiceTraceContexts)
			{
				stringBuilder.AppendLine("Service name: " + serviceTraceContext.ServiceName);
				stringBuilder.Append("Trace ids: [");
				if (serviceTraceContext.TraceIds != null)
				{
					stringBuilder.Append(string.Join<KeyValue>(", ", serviceTraceContext.TraceIds));
				}
				stringBuilder.AppendLine("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000031 RID: 49
		[DataMember(Name = "serviceTraceContexts")]
		public ServiceTraceContext[] ServiceTraceContexts;
	}
}
