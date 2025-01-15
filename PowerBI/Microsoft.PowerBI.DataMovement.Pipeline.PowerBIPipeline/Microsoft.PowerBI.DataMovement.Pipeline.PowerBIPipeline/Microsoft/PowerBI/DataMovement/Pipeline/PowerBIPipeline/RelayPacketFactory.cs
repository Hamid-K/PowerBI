using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Common;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;
using Microsoft.PowerBI.DataMovement.Pipeline.JsonSerialization;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000018 RID: 24
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RelayPacketFactory
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004035 File Offset: 0x00002235
		internal static RelayPacket CreateJsonErrorPacket(Exception e)
		{
			return RelayPacketFactory.CreateJsonPacket(new OperationErrorResult
			{
				Exception = e.EnsureGatewayPipelineException()
			}, -1, true);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000404F File Offset: 0x0000224F
		internal static RelayPacket CreateJsonPacket(OperationDataContract payload)
		{
			return RelayPacketFactory.CreateJsonPacket(payload, true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004058 File Offset: 0x00002258
		internal static RelayPacket CreateJsonPacket(OperationDataContract payload, bool isLast)
		{
			return RelayPacketFactory.CreateJsonPacket(payload, 0, isLast);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004064 File Offset: 0x00002264
		internal static RelayPacket CreateJsonPacket(OperationDataContract payload, int index, bool isLast)
		{
			string text = JsonSerializationUtils.JsonSerialize<OperationDataContract>(payload);
			return RelayPacketFactory.CreateFromByteArray(RawDataSerializationContext.StringEncoding.GetBytes(text), index, isLast, DeserializationDirective.Json);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000408C File Offset: 0x0000228C
		internal static RelayPacket CreateFromByteArray(byte[] data, int index, bool isLast, DeserializationDirective deserializationDirective)
		{
			int num = data.Length;
			byte[] array = new byte[num + RelayPacketHeader.Size];
			new RelayPacketHeader
			{
				HasCorrectDataSize = true,
				IsLast = isLast,
				Index = index,
				UncompressedDataSize = num,
				CompressedDataSize = num,
				CompressionAlgorithm = XPress9Level.None,
				DeserializationDirective = deserializationDirective
			}.Serialize(array);
			Array.Copy(data, 0, array, RelayPacketHeader.Size, num);
			return new RelayPacket
			{
				Blob = array
			};
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004100 File Offset: 0x00002300
		internal static RelayPacket CreateFromByteArrayInPlace(byte[] data, int dataPageSize, int index, bool isLast, DeserializationDirective deserializationDirective)
		{
			int num = dataPageSize - RelayPacket.HeaderSize;
			new RelayPacketHeader
			{
				HasCorrectDataSize = true,
				IsLast = isLast,
				Index = index,
				UncompressedDataSize = num,
				CompressedDataSize = num,
				CompressionAlgorithm = XPress9Level.None,
				DeserializationDirective = deserializationDirective
			}.Serialize(data);
			return new RelayPacket
			{
				Blob = data
			};
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000415E File Offset: 0x0000235E
		internal static RelayPacket CreateBinaryRowsetPacket(byte[] binaryPayload, int dataPageSize, int index, bool isLast)
		{
			return RelayPacketFactory.CreateFromByteArrayInPlace(binaryPayload, dataPageSize, index, isLast, DeserializationDirective.BinaryRowset);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000416A File Offset: 0x0000236A
		internal static RelayPacket CreateBinaryVarDataContentPacket(byte[] binaryPayload, int varDataPageSize, int index, bool isLast)
		{
			return RelayPacketFactory.CreateFromByteArrayInPlace(binaryPayload, varDataPageSize, index, isLast, DeserializationDirective.BinaryVarData);
		}
	}
}
