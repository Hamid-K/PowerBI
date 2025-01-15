using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Common.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;
using Microsoft.PowerBI.DataMovement.Pipeline.JsonClient;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000011 RID: 17
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PowerBIRawDataRowsetReadStream<[global::System.Runtime.CompilerServices.Nullable(0)] T> : RowsetReadStreamBase where T : RawDataFlowContext, new()
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003A1E File Offset: 0x00001C1E
		internal PowerBIRawDataRowsetReadStream(PowerBIRelayPacketStreamReader<T> dataSource, ISourceBlock<T> dataSink, ActivityInfo externalActivityInfo, Func<Exception, Exception> exceptionTransformCallback = null, bool readThrough = false)
			: base(externalActivityInfo)
		{
			RuntimeChecks.CheckValue(dataSource, "dataSource");
			RuntimeChecks.CheckValue(dataSink, "dataSink");
			this.m_dataSource = dataSource;
			this.m_dataSink = dataSink;
			this.m_exceptionTransformCallback = exceptionTransformCallback;
			this.m_readThrough = readThrough;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003A5B File Offset: 0x00001C5B
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003A63 File Offset: 0x00001C63
		internal Exception OperationException { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003A6C File Offset: 0x00001C6C
		protected override int PacketHeaderSize
		{
			get
			{
				if (!this.m_readThrough)
				{
					return RelayPacketHeader.Size;
				}
				return 0;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003A7D File Offset: 0x00001C7D
		protected override bool HasCurrentPacket
		{
			get
			{
				return this.m_currentPacket != null;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003A88 File Offset: 0x00001C88
		protected override byte[] CurrentPacketBlob
		{
			get
			{
				if (this.m_currentPacket == null)
				{
					return null;
				}
				return this.m_currentPacket.Blob;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003A9F File Offset: 0x00001C9F
		protected override bool IsLastPacket
		{
			get
			{
				return this.m_currentPacket != null && this.m_currentPacket.Header.IsLast;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003ABB File Offset: 0x00001CBB
		protected override bool RowsetPacketLimitReached
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003ABE File Offset: 0x00001CBE
		protected override void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (disposing)
			{
				while (!this.IsLastPacket)
				{
					this.ReceiveBinaryRowsetPacketAsync(false).Wait();
				}
			}
			this.m_disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003AF0 File Offset: 0x00001CF0
		protected override Task ReceiveBinaryRowsetPacketAsync()
		{
			return this.ReceiveBinaryRowsetPacketAsync(true);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003AFC File Offset: 0x00001CFC
		private Task ReceiveBinaryRowsetPacketAsync(bool throwExceptionOnInvalidPacket)
		{
			PowerBIRawDataRowsetReadStream<T>.<>c__DisplayClass23_0 CS$<>8__locals1 = new PowerBIRawDataRowsetReadStream<T>.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.throwExceptionOnInvalidPacket = throwExceptionOnInvalidPacket;
			return DiagnosticsContext.TelemetryService.ExecuteInActivity(PipelineActivityType.MOGP, delegate
			{
				PowerBIRawDataRowsetReadStream<T>.<>c__DisplayClass23_0.<<ReceiveBinaryRowsetPacketAsync>b__0>d <<ReceiveBinaryRowsetPacketAsync>b__0>d;
				<<ReceiveBinaryRowsetPacketAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ReceiveBinaryRowsetPacketAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ReceiveBinaryRowsetPacketAsync>b__0>d.<>1__state = -1;
				<<ReceiveBinaryRowsetPacketAsync>b__0>d.<>t__builder.Start<PowerBIRawDataRowsetReadStream<T>.<>c__DisplayClass23_0.<<ReceiveBinaryRowsetPacketAsync>b__0>d>(ref <<ReceiveBinaryRowsetPacketAsync>b__0>d);
				return <<ReceiveBinaryRowsetPacketAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003B38 File Offset: 0x00001D38
		private void ValidateBinaryRowsetPacket(bool throwExceptionOnInvalidPacket)
		{
			if (this.m_currentPacket.Header.DeserializationDirective == DeserializationDirective.BinaryRowset)
			{
				return;
			}
			this.OperationException = new InvalidDataException("Received an invalid packet");
			if (this.m_currentPacket.Header.DeserializationDirective == DeserializationDirective.Json)
			{
				OperationDataContract operationDataContract = PowerBIRawDataRowsetReadStream<T>.ParseJsonPayload(this.m_currentPacket);
				if (operationDataContract is OperationErrorResult)
				{
					OperationErrorResult operationErrorResult = operationDataContract as OperationErrorResult;
					this.OperationException = operationErrorResult.Exception;
				}
			}
			if (this.m_exceptionTransformCallback != null)
			{
				this.OperationException = this.m_exceptionTransformCallback(this.OperationException);
			}
			if (throwExceptionOnInvalidPacket)
			{
				throw this.OperationException;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003BCC File Offset: 0x00001DCC
		internal static OperationDataContract ParseJsonPayload(RelayPacket packet)
		{
			RelayPacketHeader header = packet.Header;
			return SerializationUtils.JsonDeserialize<OperationDataContract>(RawDataSerializationContext.StringEncoding.GetString(packet.Blob, RelayPacketHeader.Size, header.UncompressedDataSize));
		}

		// Token: 0x0400004A RID: 74
		private readonly PowerBIRelayPacketStreamReader<T> m_dataSource;

		// Token: 0x0400004B RID: 75
		private readonly ISourceBlock<T> m_dataSink;

		// Token: 0x0400004C RID: 76
		private readonly Func<Exception, Exception> m_exceptionTransformCallback;

		// Token: 0x0400004D RID: 77
		private readonly bool m_readThrough;

		// Token: 0x0400004E RID: 78
		private RelayPacket m_currentPacket;

		// Token: 0x0400004F RID: 79
		private bool m_disposed;
	}
}
