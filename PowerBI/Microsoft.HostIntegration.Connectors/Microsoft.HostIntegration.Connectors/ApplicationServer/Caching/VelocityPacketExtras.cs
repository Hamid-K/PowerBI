using System;
using System.Globalization;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200017E RID: 382
	internal struct VelocityPacketExtras
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x00028708 File Offset: 0x00026908
		public void ReadFrom(VelocityPacketExtrasFlags flags, byte[] array, int offset, int length, string cacheName)
		{
			if (flags == VelocityPacketExtrasFlags.None && length > 0)
			{
				throw new VelocityPacketFormatException("Extras flags is 0, but {0} bytes extras are defined", new object[] { length });
			}
			int length2 = (int)VelocityPacketExtras.GetLength(flags);
			if (length < length2)
			{
				throw new VelocityPacketFormatException("Invalid flags length. Expected: {0} for {1}, Actual: {2}", new object[] { length2, flags, length });
			}
			if ((flags & VelocityPacketExtrasFlags.Version) != VelocityPacketExtrasFlags.None)
			{
				long num = BitConverter.ToInt64(array, offset);
				long num2 = BitConverter.ToInt64(array, offset + 8);
				this.Version = new DataCacheItemVersion(new InternalCacheItemVersion(num, num2));
				offset += 16;
			}
			else
			{
				this.Version = null;
			}
			if ((flags & VelocityPacketExtrasFlags.ExpiryTTL) != VelocityPacketExtrasFlags.None)
			{
				this.ExpiryTTL = new uint?(BitConverter.ToUInt32(array, offset));
				offset += 4;
			}
			else
			{
				this.ExpiryTTL = null;
			}
			if ((flags & VelocityPacketExtrasFlags.LockHandle) != VelocityPacketExtrasFlags.None)
			{
				long num3 = BitConverter.ToInt64(array, offset);
				long num4 = BitConverter.ToInt64(array, offset + 8);
				InternalCacheItemVersion internalCacheItemVersion = new InternalCacheItemVersion(num3, num4);
				offset += 16;
				DMLockHandle dmlockHandle = new DMLockHandle
				{
					LockID = BitConverter.ToInt32(array, offset)
				};
				if (dmlockHandle.IsValid)
				{
					this.LockHandle = new DataCacheLockHandle(dmlockHandle, internalCacheItemVersion);
				}
				else
				{
					this.LockHandle = null;
				}
				offset += 4;
			}
			else
			{
				this.LockHandle = null;
			}
			if ((flags & VelocityPacketExtrasFlags.PartitionKey) != VelocityPacketExtrasFlags.None)
			{
				int num5 = BitConverter.ToInt32(array, offset);
				offset += 4;
				int num6 = BitConverter.ToInt32(array, offset);
				offset += 4;
				this.PartitionKey = new PartitionId(cacheName, num5, num6);
			}
			if ((flags & VelocityPacketExtrasFlags.CacheItemMask) != VelocityPacketExtrasFlags.None)
			{
				this.CacheItemMask = BitConverter.ToInt32(array, offset);
				offset += 4;
			}
			VelocityWireProtocol.TraceBytesOnVerbose("VelocityPacketExtras.ReadFrom", array, offset, length, this);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000288B8 File Offset: 0x00026AB8
		public int GetLength(out VelocityPacketExtrasFlags flags)
		{
			int num = 0;
			flags = VelocityPacketExtrasFlags.None;
			if (this.Version != null && this.Version.InternalVersion != InternalCacheItemVersion.Null)
			{
				flags |= VelocityPacketExtrasFlags.Version;
				num += 16;
			}
			if (this.ExpiryTTL != null)
			{
				flags |= VelocityPacketExtrasFlags.ExpiryTTL;
				num += 4;
			}
			if (this.LockHandle != null && this.LockHandle.Handle.IsValid)
			{
				flags |= VelocityPacketExtrasFlags.LockHandle;
				num += 20;
			}
			if (!object.ReferenceEquals(this.PartitionKey, null))
			{
				flags |= VelocityPacketExtrasFlags.PartitionKey;
				num += 8;
			}
			if (this.CacheItemMask != 0)
			{
				flags |= VelocityPacketExtrasFlags.CacheItemMask;
				num += 4;
			}
			return num;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00028968 File Offset: 0x00026B68
		public VelocityPacketExtrasFlags WriteTo(byte[] array, ref int offset)
		{
			int num = offset;
			VelocityPacketExtrasFlags velocityPacketExtrasFlags = VelocityPacketExtrasFlags.None;
			if (this.Version != null && this.Version.InternalVersion != InternalCacheItemVersion.Null)
			{
				velocityPacketExtrasFlags |= VelocityPacketExtrasFlags.Version;
				byte[] array2 = BitConverter.GetBytes(this.Version.InternalVersion.Epoch);
				array2.CopyTo(array, offset);
				offset += array2.Length;
				array2 = BitConverter.GetBytes(this.Version.InternalVersion.Lsn);
				array2.CopyTo(array, offset);
				offset += array2.Length;
			}
			if (this.ExpiryTTL != null)
			{
				velocityPacketExtrasFlags |= VelocityPacketExtrasFlags.ExpiryTTL;
				byte[] array2 = BitConverter.GetBytes(this.ExpiryTTL.Value);
				array2.CopyTo(array, offset);
				offset += array2.Length;
			}
			if (this.LockHandle != null && this.LockHandle.Handle.IsValid)
			{
				velocityPacketExtrasFlags |= VelocityPacketExtrasFlags.LockHandle;
				byte[] array2 = BitConverter.GetBytes(this.LockHandle.Version.Epoch);
				array2.CopyTo(array, offset);
				offset += array2.Length;
				array2 = BitConverter.GetBytes(this.LockHandle.Version.Lsn);
				array2.CopyTo(array, offset);
				offset += array2.Length;
				array2 = BitConverter.GetBytes(this.LockHandle.Handle.LockID);
				array2.CopyTo(array, offset);
				offset += array2.Length;
			}
			if (!object.ReferenceEquals(this.PartitionKey, null))
			{
				velocityPacketExtrasFlags |= VelocityPacketExtrasFlags.PartitionKey;
				byte[] array2 = BitConverter.GetBytes(this.PartitionKey.LowKey);
				array2.CopyTo(array, offset);
				offset += array2.Length;
				array2 = BitConverter.GetBytes(this.PartitionKey.HighKey);
				array2.CopyTo(array, offset);
				offset += array2.Length;
			}
			if (this.CacheItemMask != 0)
			{
				velocityPacketExtrasFlags |= VelocityPacketExtrasFlags.CacheItemMask;
				byte[] array2 = BitConverter.GetBytes(this.CacheItemMask);
				array2.CopyTo(array, offset);
				offset += array2.Length;
			}
			VelocityWireProtocol.TraceBytesOnVerbose("VelocityPacketExtras.WriteTo", array, num, offset - num, this);
			return velocityPacketExtrasFlags;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00028B78 File Offset: 0x00026D78
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00028B80 File Offset: 0x00026D80
		public DataCacheItemVersion Version { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00028B89 File Offset: 0x00026D89
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00028B91 File Offset: 0x00026D91
		public uint? ExpiryTTL { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00028B9A File Offset: 0x00026D9A
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00028BA2 File Offset: 0x00026DA2
		public int CacheItemMask { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00028BAB File Offset: 0x00026DAB
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00028BB3 File Offset: 0x00026DB3
		public DataCacheLockHandle LockHandle { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00028BBC File Offset: 0x00026DBC
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00028BC4 File Offset: 0x00026DC4
		public PartitionId PartitionKey { get; set; }

		// Token: 0x06000C32 RID: 3122 RVA: 0x00028BD0 File Offset: 0x00026DD0
		private static byte GetLength(VelocityPacketExtrasFlags flags)
		{
			byte b = 0;
			for (int i = 0; i < VelocityPacketExtras._lengths.Length; i++)
			{
				int num = (int)(flags & (VelocityPacketExtrasFlags)(1 << i));
				if (num != 0)
				{
					b += VelocityPacketExtras._lengths[i];
				}
			}
			return b;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00028C0C File Offset: 0x00026E0C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "VelocityPacketExtras: {0}, Expiry: {1}, Lock: {2}, PKey: {3}, Mask: {4}", new object[]
			{
				(this.Version != null) ? this.Version.InternalVersion : InternalCacheItemVersion.Null,
				this.ExpiryTTL,
				this.LockHandle,
				this.PartitionKey,
				this.CacheItemMask
			});
		}

		// Token: 0x0400089A RID: 2202
		private const byte _versionEpochLength = 8;

		// Token: 0x0400089B RID: 2203
		private const byte _versionLsnLength = 8;

		// Token: 0x0400089C RID: 2204
		private const byte _versionTotalLength = 16;

		// Token: 0x0400089D RID: 2205
		private const byte _expiryLength = 4;

		// Token: 0x0400089E RID: 2206
		private const byte _lockIdLength = 4;

		// Token: 0x0400089F RID: 2207
		private const byte _lockHandleLength = 20;

		// Token: 0x040008A0 RID: 2208
		private const byte _partitionKeyLength = 8;

		// Token: 0x040008A1 RID: 2209
		private const byte _cacheItemMaskLength = 4;

		// Token: 0x040008A2 RID: 2210
		private static readonly byte[] _lengths = new byte[] { 16, 4, 20, 8, 4 };
	}
}
