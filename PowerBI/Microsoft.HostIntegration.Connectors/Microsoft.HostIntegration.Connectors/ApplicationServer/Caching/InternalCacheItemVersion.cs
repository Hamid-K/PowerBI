using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E8 RID: 744
	[DataContract(Name = "InternalCacheItemVersion", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal struct InternalCacheItemVersion : IComparable<InternalCacheItemVersion>, IEquatable<InternalCacheItemVersion>
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x00053E80 File Offset: 0x00052080
		public override bool Equals(object obj)
		{
			InternalCacheItemVersion internalCacheItemVersion = (InternalCacheItemVersion)obj;
			return this.Equals(ref internalCacheItemVersion);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00053E9C File Offset: 0x0005209C
		public bool Equals(InternalCacheItemVersion ver)
		{
			return this.Equals(ref ver);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00053EA6 File Offset: 0x000520A6
		private bool Equals(ref InternalCacheItemVersion obj)
		{
			return obj.Epoch == this._epoch && obj.Lsn == this._lsn;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00053EC6 File Offset: 0x000520C6
		public static bool operator ==(InternalCacheItemVersion first, InternalCacheItemVersion second)
		{
			return first.Equals(ref second);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00053ED1 File Offset: 0x000520D1
		public static bool operator !=(InternalCacheItemVersion first, InternalCacheItemVersion second)
		{
			return !first.Equals(ref second);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00053EE0 File Offset: 0x000520E0
		public int CompareTo(InternalCacheItemVersion version)
		{
			if (this.Epoch == version.Epoch)
			{
				return this.Lsn.CompareTo(version.Lsn);
			}
			return this.Epoch.CompareTo(version.Epoch);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00053F27 File Offset: 0x00052127
		public void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this._epoch);
			writer.Write(this._lsn);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00053F41 File Offset: 0x00052141
		public void ReadStream(ISerializationReader reader)
		{
			this._epoch = reader.ReadInt64();
			this._lsn = reader.ReadInt64();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0003A92B File Offset: 0x00038B2B
		public int GetSerializedSize()
		{
			return 16;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00053F5B File Offset: 0x0005215B
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x00053F63 File Offset: 0x00052163
		internal long Epoch
		{
			get
			{
				return this._epoch;
			}
			set
			{
				this._epoch = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00053F6C File Offset: 0x0005216C
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00053F74 File Offset: 0x00052174
		internal long Lsn
		{
			get
			{
				return this._lsn;
			}
			set
			{
				this._lsn = value;
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00053F7D File Offset: 0x0005217D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00053F8F File Offset: 0x0005218F
		internal InternalCacheItemVersion(InternalCacheItemVersion old)
		{
			this._epoch = old.Epoch;
			this._lsn = ((old.Lsn == 0L) ? 1L : (old.Lsn + 1L));
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00053FBD File Offset: 0x000521BD
		internal InternalCacheItemVersion(long epoch, long seqNo)
		{
			this._epoch = epoch;
			this._lsn = seqNo;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00053FCD File Offset: 0x000521CD
		internal bool MemcacheEquals(InternalCacheItemVersion version)
		{
			return this.GetMemcacheVersion() == version.Lsn;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00053FE0 File Offset: 0x000521E0
		internal long GetMemcacheVersion()
		{
			long num = ((1099494850560L & this._epoch) >> 24) + (65535L & this._epoch);
			return (num & 65535L) | (this._lsn << 16);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00054021 File Offset: 0x00052221
		internal static InternalCacheItemVersion Null
		{
			get
			{
				return InternalCacheItemVersion.NullVersion;
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00054028 File Offset: 0x00052228
		public override string ToString()
		{
			return string.Concat(new object[] { "Version = ", this._epoch, ":", this._lsn });
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0003A92B File Offset: 0x00038B2B
		public int Size
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x04000EC2 RID: 3778
		[DataMember]
		private long _epoch;

		// Token: 0x04000EC3 RID: 3779
		[DataMember]
		private long _lsn;

		// Token: 0x04000EC4 RID: 3780
		private static InternalCacheItemVersion NullVersion = default(InternalCacheItemVersion);
	}
}
