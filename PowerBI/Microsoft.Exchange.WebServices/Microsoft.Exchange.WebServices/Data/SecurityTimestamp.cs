using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000300 RID: 768
	internal sealed class SecurityTimestamp
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x00048FD8 File Offset: 0x00047FD8
		public SecurityTimestamp(DateTime creationTimeUtc, DateTime expiryTimeUtc, string id)
			: this(creationTimeUtc, expiryTimeUtc, id, null, null)
		{
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00048FE8 File Offset: 0x00047FE8
		internal SecurityTimestamp(DateTime creationTimeUtc, DateTime expiryTimeUtc, string id, string digestAlgorithm, byte[] digest)
		{
			EwsUtilities.Assert(creationTimeUtc.Kind == 1, "SecurityTimestamp.ctor", "creation time must be in UTC");
			EwsUtilities.Assert(expiryTimeUtc.Kind == 1, "SecurityTimestamp.ctor", "expiry time must be in UTC");
			if (creationTimeUtc > expiryTimeUtc)
			{
				throw new ArgumentOutOfRangeException("recordedExpiryTime");
			}
			this.creationTimeUtc = creationTimeUtc;
			this.expiryTimeUtc = expiryTimeUtc;
			this.id = id;
			this.digestAlgorithm = digestAlgorithm;
			this.digest = digest;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00049066 File Offset: 0x00048066
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.creationTimeUtc;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0004906E File Offset: 0x0004806E
		public DateTime ExpiryTimeUtc
		{
			get
			{
				return this.expiryTimeUtc;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00049076 File Offset: 0x00048076
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0004907E File Offset: 0x0004807E
		public string DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00049086 File Offset: 0x00048086
		internal byte[] GetDigest()
		{
			return this.digest;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0004908E File Offset: 0x0004808E
		internal char[] GetCreationTimeChars()
		{
			if (this.computedCreationTimeUtc == null)
			{
				this.computedCreationTimeUtc = SecurityTimestamp.ToChars(ref this.creationTimeUtc);
			}
			return this.computedCreationTimeUtc;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000490AF File Offset: 0x000480AF
		internal char[] GetExpiryTimeChars()
		{
			if (this.computedExpiryTimeUtc == null)
			{
				this.computedExpiryTimeUtc = SecurityTimestamp.ToChars(ref this.expiryTimeUtc);
			}
			return this.computedExpiryTimeUtc;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000490D0 File Offset: 0x000480D0
		private static char[] ToChars(ref DateTime utcTime)
		{
			char[] array = new char["yyyy-MM-ddTHH:mm:ss.fffZ".Length];
			int num = 0;
			SecurityTimestamp.ToChars(utcTime.Year, array, ref num, 4);
			array[num++] = '-';
			SecurityTimestamp.ToChars(utcTime.Month, array, ref num, 2);
			array[num++] = '-';
			SecurityTimestamp.ToChars(utcTime.Day, array, ref num, 2);
			array[num++] = 'T';
			SecurityTimestamp.ToChars(utcTime.Hour, array, ref num, 2);
			array[num++] = ':';
			SecurityTimestamp.ToChars(utcTime.Minute, array, ref num, 2);
			array[num++] = ':';
			SecurityTimestamp.ToChars(utcTime.Second, array, ref num, 2);
			array[num++] = '.';
			SecurityTimestamp.ToChars(utcTime.Millisecond, array, ref num, 3);
			array[num++] = 'Z';
			return array;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00049198 File Offset: 0x00048198
		private static void ToChars(int n, char[] buffer, ref int offset, int count)
		{
			for (int i = offset + count - 1; i >= offset; i--)
			{
				buffer[i] = (char)(48 + n % 10);
				n /= 10;
			}
			EwsUtilities.Assert(n == 0, "SecurityTimestamp.ToChars", "Overflow in encoding timestamp field");
			offset += count;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000491E4 File Offset: 0x000481E4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SecurityTimestamp: Id={0}, CreationTimeUtc={1}, ExpirationTimeUtc={2}", new object[]
			{
				this.Id,
				XmlConvert.ToString(this.CreationTimeUtc, 3),
				XmlConvert.ToString(this.ExpiryTimeUtc, 3)
			});
		}

		// Token: 0x04001449 RID: 5193
		internal const string DefaultTimestampValidityDurationString = "00:05:00";

		// Token: 0x0400144A RID: 5194
		internal const string DefaultFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

		// Token: 0x0400144B RID: 5195
		internal static readonly TimeSpan DefaultTimestampValidityDuration = TimeSpan.Parse("00:05:00");

		// Token: 0x0400144C RID: 5196
		internal static readonly TimeSpan DefaultTimeToLive = SecurityTimestamp.DefaultTimestampValidityDuration;

		// Token: 0x0400144D RID: 5197
		private readonly string id;

		// Token: 0x0400144E RID: 5198
		private readonly string digestAlgorithm;

		// Token: 0x0400144F RID: 5199
		private readonly byte[] digest;

		// Token: 0x04001450 RID: 5200
		private char[] computedCreationTimeUtc;

		// Token: 0x04001451 RID: 5201
		private char[] computedExpiryTimeUtc;

		// Token: 0x04001452 RID: 5202
		private DateTime creationTimeUtc;

		// Token: 0x04001453 RID: 5203
		private DateTime expiryTimeUtc;
	}
}
