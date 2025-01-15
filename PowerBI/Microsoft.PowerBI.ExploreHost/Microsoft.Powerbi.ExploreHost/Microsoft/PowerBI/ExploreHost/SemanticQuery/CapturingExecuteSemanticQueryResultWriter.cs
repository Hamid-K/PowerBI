using System;
using System.IO;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x0200003E RID: 62
	internal sealed class CapturingExecuteSemanticQueryResultWriter : IExecuteSemanticQueryResultWriter
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00006875 File Offset: 0x00004A75
		internal CapturingExecuteSemanticQueryResultWriter(Stream stream, StreamFormat streamFormat = StreamFormat.DataShapeResult)
		{
			this._resultStream = stream;
			this._streamFormat = streamFormat;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000688B File Offset: 0x00004A8B
		public void WriteQueryBindingDescriptor(QueryBindingDescriptor descriptor)
		{
			this.Descriptor = descriptor;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006894 File Offset: 0x00004A94
		public void WriteExecutionMetrics(ExecutionMetrics metrics)
		{
			this.Metrics = metrics;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000689D File Offset: 0x00004A9D
		public Stream GetDataShapeResultStream()
		{
			if (this._streamFormat != StreamFormat.DataShapeResult)
			{
				throw new InvalidOperationException("DataShapeResult is not supported");
			}
			return this._resultStream;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000068B8 File Offset: 0x00004AB8
		public Stream GetRawDataStream()
		{
			if (this._streamFormat != StreamFormat.RawData)
			{
				throw new InvalidOperationException("RawData is not supported");
			}
			return this._resultStream;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000068D4 File Offset: 0x00004AD4
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000068DC File Offset: 0x00004ADC
		internal QueryBindingDescriptor Descriptor { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000068E5 File Offset: 0x00004AE5
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000068ED File Offset: 0x00004AED
		internal ExecutionMetrics Metrics { get; private set; }

		// Token: 0x040000C4 RID: 196
		private readonly Stream _resultStream;

		// Token: 0x040000C5 RID: 197
		private readonly StreamFormat _streamFormat;
	}
}
