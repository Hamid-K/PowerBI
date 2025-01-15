using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000373 RID: 883
	[DataContract(Name = "RequestBodyFlags", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal struct RequestBodyFlags : IBinarySerializable
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0005F883 File Offset: 0x0005DA83
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x0005F88C File Offset: 0x0005DA8C
		internal bool GetAndLockFlag
		{
			get
			{
				return this.GetFlag(1U);
			}
			set
			{
				this.SetFlag(value, 1U);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0005F896 File Offset: 0x0005DA96
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x0005F89F File Offset: 0x0005DA9F
		internal bool IsNewVersionRequired
		{
			get
			{
				return this.GetFlag(4U);
			}
			set
			{
				this.SetFlag(value, 4U);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0005F8A9 File Offset: 0x0005DAA9
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x0005F8B2 File Offset: 0x0005DAB2
		internal bool NewElementFlag
		{
			get
			{
				return this.GetFlag(2U);
			}
			set
			{
				this.SetFlag(value, 2U);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0005F8BC File Offset: 0x0005DABC
		// (set) Token: 0x06001F3B RID: 7995 RVA: 0x0005F8C5 File Offset: 0x0005DAC5
		internal bool ReadThroughAddFlag
		{
			get
			{
				return this.GetFlag(8U);
			}
			set
			{
				this.SetFlag(value, 8U);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0005F8CF File Offset: 0x0005DACF
		// (set) Token: 0x06001F3D RID: 7997 RVA: 0x0005F8D9 File Offset: 0x0005DAD9
		internal bool PrimaryRequestTrackingFlag
		{
			get
			{
				return this.GetFlag(32U);
			}
			set
			{
				this.SetFlag(value, 32U);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0005F8E4 File Offset: 0x0005DAE4
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0005F8EE File Offset: 0x0005DAEE
		internal bool ClientRequestTrackingFlag
		{
			get
			{
				return this.GetFlag(64U);
			}
			set
			{
				this.SetFlag(value, 64U);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0005F8F9 File Offset: 0x0005DAF9
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x0005F903 File Offset: 0x0005DB03
		internal bool ReadThroughAttemptedFlag
		{
			get
			{
				return this.GetFlag(16U);
			}
			set
			{
				this.SetFlag(value, 16U);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x0005F90E File Offset: 0x0005DB0E
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x0005F91B File Offset: 0x0005DB1B
		internal bool TrackingIDPresenceFlag
		{
			get
			{
				return this.GetFlag(128U);
			}
			set
			{
				this.SetFlag(value, 128U);
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0005F929 File Offset: 0x0005DB29
		public void ReadStream(ISerializationReader reader)
		{
			this._flag = reader.ReadUInt32();
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0005F937 File Offset: 0x0005DB37
		public void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this._flag);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0005F945 File Offset: 0x0005DB45
		private bool GetFlag(uint mask)
		{
			return (this._flag & mask) == mask;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0005F952 File Offset: 0x0005DB52
		private void SetFlag(bool value, uint mask)
		{
			if (value)
			{
				this._flag |= mask;
				return;
			}
			this._flag &= ~mask;
		}

		// Token: 0x040011FF RID: 4607
		private const uint GetAndLockFlagBitMask = 1U;

		// Token: 0x04001200 RID: 4608
		private const uint NewElementFlagBitMask = 2U;

		// Token: 0x04001201 RID: 4609
		private const uint IsNewVersionRequiredBitMask = 4U;

		// Token: 0x04001202 RID: 4610
		private const uint ReadThroughAddFlagBitMask = 8U;

		// Token: 0x04001203 RID: 4611
		private const uint ReadThroughAttemptedMask = 16U;

		// Token: 0x04001204 RID: 4612
		private const uint PrimaryRequestTrackingMask = 32U;

		// Token: 0x04001205 RID: 4613
		private const uint ClientRequestTrackingMask = 64U;

		// Token: 0x04001206 RID: 4614
		private const uint TrackingIDPresenceFlagMask = 128U;

		// Token: 0x04001207 RID: 4615
		[DataMember]
		private uint _flag;
	}
}
