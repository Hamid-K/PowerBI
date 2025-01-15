using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization
{
	// Token: 0x0200001B RID: 27
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OleDbBinaryWriterSerializer
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004489 File Offset: 0x00002689
		internal OleDbBinaryWriterSerializer(ITargetBlock<RawDataFlowContext> dataSinkBlock, IMultipleResults multipleResults)
		{
			RuntimeChecks.CheckValue(dataSinkBlock, "dataSinkBlock");
			RuntimeChecks.CheckValue(multipleResults, "multipleResults");
			this.m_dataSinkBlock = dataSinkBlock;
			this.m_multipleResults = multipleResults;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000044B5 File Offset: 0x000026B5
		internal OleDbBinaryWriterSerializer(ITargetBlock<RawDataFlowContext> dataSinkBlock, IPageReader reader)
		{
			RuntimeChecks.CheckValue(dataSinkBlock, "dataSinkBlock");
			RuntimeChecks.CheckValue(reader, "reader");
			this.m_dataSinkBlock = dataSinkBlock;
			this.m_reader = reader;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000044E4 File Offset: 0x000026E4
		internal void Serialize(RawDataSerializationContext rawDataSerializationContext)
		{
			using (OleDbBinaryPacketWriteStream oleDbBinaryPacketWriteStream = new OleDbBinaryPacketWriteStream(rawDataSerializationContext, this.m_dataSinkBlock))
			{
				this.Serialize(oleDbBinaryPacketWriteStream);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004524 File Offset: 0x00002724
		internal void Serialize(RawDataSerializationContext rawDataSerializationContext, int pageSize, int pageLargeSize)
		{
			using (OleDbBinaryPacketWriteStream oleDbBinaryPacketWriteStream = new OleDbBinaryPacketWriteStream(rawDataSerializationContext, this.m_dataSinkBlock, pageSize, pageLargeSize))
			{
				this.Serialize(oleDbBinaryPacketWriteStream);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004564 File Offset: 0x00002764
		private void Serialize(OleDbBinaryPacketWriteStream oleDbPacketStream)
		{
			if (this.m_reader != null)
			{
				OleDbBinaryWriterSerializer.SerializeStream(oleDbPacketStream, this.m_reader);
			}
			else
			{
				for (;;)
				{
					IRowset result = this.m_multipleResults.GetResult();
					if (result == null)
					{
						break;
					}
					using (RowsetPageReader rowsetPageReader = new RowsetPageReader(result))
					{
						oleDbPacketStream.WriteByte(1);
						OleDbBinaryWriterSerializer.SerializeStream(oleDbPacketStream, rowsetPageReader);
					}
				}
				oleDbPacketStream.WriteByte(0);
			}
			oleDbPacketStream.SendIsLastPacketOnClose();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000045DC File Offset: 0x000027DC
		private static void SerializeStream(OleDbBinaryPacketWriteStream oleDbPacketStream, IPageReader reader)
		{
			using (OleDbPageWriter oleDbPageWriter = new OleDbPageWriter(oleDbPacketStream, reader.SchemaTable, false, false))
			{
				IPage page = reader.CreatePage();
				bool flag = false;
				while (!flag)
				{
					reader.Read(page);
					flag = page.RowCount == 0;
					oleDbPageWriter.Write(page);
				}
				oleDbPageWriter.Flush();
			}
		}

		// Token: 0x04000069 RID: 105
		private readonly ITargetBlock<RawDataFlowContext> m_dataSinkBlock;

		// Token: 0x0400006A RID: 106
		private readonly IPageReader m_reader;

		// Token: 0x0400006B RID: 107
		private readonly IMultipleResults m_multipleResults;
	}
}
