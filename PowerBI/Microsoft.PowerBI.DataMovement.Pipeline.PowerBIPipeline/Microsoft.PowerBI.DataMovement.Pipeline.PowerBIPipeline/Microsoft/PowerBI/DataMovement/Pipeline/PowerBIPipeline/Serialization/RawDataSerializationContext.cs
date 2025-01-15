using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;
using Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline.Serialization
{
	// Token: 0x0200001C RID: 28
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class RawDataSerializationContext
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00004640 File Offset: 0x00002840
		internal RawDataSerializationContext()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004648 File Offset: 0x00002848
		internal RawDataSerializationContext(RawDataSerializationContext original)
		{
			this.Packet = original.Packet;
			this.QueryResult = original.QueryResult;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004668 File Offset: 0x00002868
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004670 File Offset: 0x00002870
		internal RelayPacket Packet { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004679 File Offset: 0x00002879
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004681 File Offset: 0x00002881
		internal OleDbQueryResult QueryResult { get; set; }

		// Token: 0x060000B5 RID: 181 RVA: 0x0000468A File Offset: 0x0000288A
		internal RawDataFlowContext CloneForPipeline()
		{
			return new RawDataFlowContext();
		}

		// Token: 0x0400006C RID: 108
		internal static readonly Encoding StringEncoding = Encoding.Unicode;
	}
}
