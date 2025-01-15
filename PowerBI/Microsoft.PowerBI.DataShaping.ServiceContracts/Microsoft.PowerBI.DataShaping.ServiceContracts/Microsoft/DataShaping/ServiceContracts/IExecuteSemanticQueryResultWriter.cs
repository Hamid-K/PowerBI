using System;
using System.IO;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000013 RID: 19
	public interface IExecuteSemanticQueryResultWriter
	{
		// Token: 0x06000064 RID: 100
		void WriteQueryBindingDescriptor(QueryBindingDescriptor descriptor);

		// Token: 0x06000065 RID: 101
		Stream GetDataShapeResultStream();

		// Token: 0x06000066 RID: 102
		Stream GetRawDataStream();

		// Token: 0x06000067 RID: 103
		void WriteExecutionMetrics(ExecutionMetrics metrics);
	}
}
