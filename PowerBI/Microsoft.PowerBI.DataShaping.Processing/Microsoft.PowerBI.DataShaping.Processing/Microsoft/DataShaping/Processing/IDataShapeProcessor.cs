using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000015 RID: 21
	internal interface IDataShapeProcessor
	{
		// Token: 0x060000A7 RID: 167
		Task Process(DataShapeProcessorContext context, CancellationToken cancelToken);
	}
}
