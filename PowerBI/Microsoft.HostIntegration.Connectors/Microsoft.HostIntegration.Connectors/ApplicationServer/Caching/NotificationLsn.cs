using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200025F RID: 607
	[DataContract]
	internal class NotificationLsn : IComparable<NotificationLsn>, IEquatable<NotificationLsn>, IBinarySerializable
	{
		// Token: 0x06001463 RID: 5219 RVA: 0x0003FE3E File Offset: 0x0003E03E
		public NotificationLsn(long epoch, long lsn)
		{
			this.Epoch = epoch;
			this.Lsn = lsn;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00002061 File Offset: 0x00000261
		public NotificationLsn()
		{
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003FE54 File Offset: 0x0003E054
		public int CompareTo(NotificationLsn other)
		{
			if (this.Equals(other))
			{
				return 0;
			}
			if (this.Epoch > other.Epoch)
			{
				return 1;
			}
			if (this.Epoch == other.Epoch && this.Lsn > other.Lsn)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0003FE90 File Offset: 0x0003E090
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			this.Epoch = reader.ReadInt64();
			this.Lsn = reader.ReadInt64();
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0003FEAA File Offset: 0x0003E0AA
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			writer.Write(this.Epoch);
			writer.Write(this.Lsn);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0003FEC4 File Offset: 0x0003E0C4
		public override bool Equals(object other)
		{
			NotificationLsn notificationLsn = (NotificationLsn)other;
			return notificationLsn != null && this.Equals(notificationLsn);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0003FEE4 File Offset: 0x0003E0E4
		public bool Equals(NotificationLsn other)
		{
			return this.Epoch == other.Epoch && this.Lsn == other.Lsn;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0003FF04 File Offset: 0x0003E104
		public static bool operator >(NotificationLsn lsn1, NotificationLsn lsn2)
		{
			return lsn1.CompareTo(lsn2) > 0;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0003FF10 File Offset: 0x0003E110
		public static bool operator >=(NotificationLsn lsn1, NotificationLsn lsn2)
		{
			return lsn1.CompareTo(lsn2) >= 0;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0003FF1F File Offset: 0x0003E11F
		public static bool operator <(NotificationLsn lsn1, NotificationLsn lsn2)
		{
			return lsn1.CompareTo(lsn2) < 0;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0003FF2B File Offset: 0x0003E12B
		public static bool operator <=(NotificationLsn lsn1, NotificationLsn lsn2)
		{
			return lsn1.CompareTo(lsn2) <= 0;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003FF3A File Offset: 0x0003E13A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0003FF44 File Offset: 0x0003E144
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Epoch ={0}:: Lsn = {1} ", new object[]
			{
				this.Epoch.ToString(NumberFormatInfo.InvariantInfo),
				this.Lsn.ToString(NumberFormatInfo.InvariantInfo)
			});
		}

		// Token: 0x04000C36 RID: 3126
		[DataMember]
		public long Epoch;

		// Token: 0x04000C37 RID: 3127
		[DataMember]
		public long Lsn;
	}
}
