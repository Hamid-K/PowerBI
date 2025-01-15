using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200037A RID: 890
	[DataContract(Name = "ResponseBodyFlags", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal struct ResponseBodyFlags : IBinarySerializable2, IBinarySerializable
	{
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x00060261 File Offset: 0x0005E461
		// (set) Token: 0x06001F7E RID: 8062 RVA: 0x0006026E File Offset: 0x0005E46E
		internal bool TrackingIdPresenceFlag
		{
			get
			{
				return (this._flag & 1U) == 1U;
			}
			set
			{
				if (value)
				{
					this._flag |= 1U;
					return;
				}
				this._flag &= 4294967294U;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00060291 File Offset: 0x0005E491
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x0006029E File Offset: 0x0005E49E
		internal bool ProcessedAtGatewayFlag
		{
			get
			{
				return (this._flag & 2U) == 2U;
			}
			set
			{
				if (value)
				{
					this._flag |= 2U;
					return;
				}
				this._flag &= 4294967293U;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000602C1 File Offset: 0x0005E4C1
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x000602CE File Offset: 0x0005E4CE
		internal bool IsClientRoutingTableStaleFlag
		{
			get
			{
				return (this._flag & 8U) == 8U;
			}
			set
			{
				if (value)
				{
					this._flag |= 8U;
					return;
				}
				this._flag &= 4294967287U;
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000602F1 File Offset: 0x0005E4F1
		public void ReadStream(ISerializationReader reader)
		{
			this._flag = reader.ReadUInt32();
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000602FF File Offset: 0x0005E4FF
		public void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this._flag);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000373C9 File Offset: 0x000355C9
		public int GetSerializedSize()
		{
			return 4;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0006030D File Offset: 0x0005E50D
		public byte[][] WritePayloadDetails(ISerializationWriter writer, out int payloadLength)
		{
			payloadLength = 0;
			return null;
		}

		// Token: 0x040012BB RID: 4795
		private const uint TrackingIdPresenceFlagMask = 1U;

		// Token: 0x040012BC RID: 4796
		private const uint ProcessedAtGatewayFlagMask = 2U;

		// Token: 0x040012BD RID: 4797
		private const uint RefreshLookupTableMask = 8U;

		// Token: 0x040012BE RID: 4798
		[DataMember]
		private uint _flag;
	}
}
