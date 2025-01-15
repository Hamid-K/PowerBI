using System;
using System.Threading;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003EF RID: 1007
	internal class SequencedIdGenerator
	{
		// Token: 0x06002361 RID: 9057 RVA: 0x0006C924 File Offset: 0x0006AB24
		private static bool LowerCaseHexCharsToByte(char ch1, char ch2, ref long b)
		{
			if (ch1 >= '0' && ch1 <= '9')
			{
				b = (long)((long)(ch1 - '0') << 4);
			}
			else
			{
				if (ch1 < 'a' || ch1 > 'f')
				{
					return false;
				}
				b = (long)((long)(ch1 - 'a' + '\n') << 4);
			}
			if (ch2 >= '0' && ch2 <= '9')
			{
				b |= (long)((ulong)((byte)(ch2 - '0')));
			}
			else
			{
				if (ch2 < 'a' || ch2 > 'f')
				{
					return false;
				}
				b |= (long)((ulong)((byte)(ch2 - 'a' + '\n')));
			}
			return true;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0006C994 File Offset: 0x0006AB94
		private static bool StringToInteger(string s, int startIndex, out int value)
		{
			value = 0;
			for (int i = startIndex; i < s.Length; i++)
			{
				char c = s[i];
				if (c < '0' || c > '9')
				{
					return false;
				}
				value = value * 10 + (int)(c - '0');
			}
			return true;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
		private static bool IsGuidUri(string stringID, out long first64Bits, out long second64Bits)
		{
			ReleaseAssert.IsTrue(stringID.Trim().Length == stringID.Length);
			first64Bits = (second64Bits = 0L);
			if (stringID.Length < 45 || stringID[0] != 'u' || stringID[1] != 'r' || stringID[2] != 'n' || stringID[3] != ':' || stringID[4] != 'u' || stringID[5] != 'u' || stringID[6] != 'i' || stringID[7] != 'd' || stringID[8] != ':' || stringID[17] != '-' || stringID[22] != '-' || stringID[27] != '-' || stringID[32] != '-')
			{
				return false;
			}
			long num = 0L;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[9], stringID[10], ref num))
			{
				return false;
			}
			first64Bits |= num << 24;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[11], stringID[12], ref num))
			{
				return false;
			}
			first64Bits |= num << 16;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[13], stringID[14], ref num))
			{
				return false;
			}
			first64Bits |= num << 8;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[15], stringID[16], ref num))
			{
				return false;
			}
			first64Bits |= num;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[18], stringID[19], ref num))
			{
				return false;
			}
			first64Bits |= num << 40;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[20], stringID[21], ref num))
			{
				return false;
			}
			first64Bits |= num << 32;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[23], stringID[24], ref num))
			{
				return false;
			}
			first64Bits |= num << 56;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[25], stringID[26], ref num))
			{
				return false;
			}
			first64Bits |= num << 48;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[28], stringID[29], ref num))
			{
				return false;
			}
			second64Bits |= num;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[30], stringID[31], ref num))
			{
				return false;
			}
			second64Bits |= num << 8;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[33], stringID[34], ref num))
			{
				return false;
			}
			second64Bits |= num << 16;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[35], stringID[36], ref num))
			{
				return false;
			}
			second64Bits |= num << 24;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[37], stringID[38], ref num))
			{
				return false;
			}
			second64Bits |= num << 32;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[39], stringID[40], ref num))
			{
				return false;
			}
			second64Bits |= num << 40;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[41], stringID[42], ref num))
			{
				return false;
			}
			second64Bits |= num << 48;
			if (!SequencedIdGenerator.LowerCaseHexCharsToByte(stringID[43], stringID[44], ref num))
			{
				return false;
			}
			second64Bits |= num << 56;
			return true;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0006CD0C File Offset: 0x0006AF0C
		private static bool IsSequencedID(string stringID, out long first64Bits, out long second64Bits, out int offset)
		{
			first64Bits = (second64Bits = (long)(offset = 0));
			return stringID.Length > 49 && stringID[45] == ';' && stringID[46] == 'i' && stringID[47] == 'd' && stringID[48] == '=' && SequencedIdGenerator.IsGuidUri(stringID, out first64Bits, out second64Bits) && SequencedIdGenerator.StringToInteger(stringID, 49, out offset);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0006CD7C File Offset: 0x0006AF7C
		public static SequencedId Create(string id)
		{
			long num;
			long num2;
			int num3;
			bool flag = SequencedIdGenerator.IsSequencedID(id, out num, out num2, out num3);
			if (flag)
			{
				return new SequencedId(id, num, num2, num3);
			}
			return null;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0006CDA4 File Offset: 0x0006AFA4
		public static SequencedId Create(UniqueId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			SequencedId sequencedId = id as SequencedId;
			if (sequencedId == null && !id.IsGuid)
			{
				sequencedId = SequencedIdGenerator.Create(id.ToString());
			}
			return sequencedId;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0006CDEC File Offset: 0x0006AFEC
		internal static byte[] GetGuidBytes(long first64Bits, long second64Bits)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 8; i++)
			{
				array[i] = (byte)(first64Bits >> 8 * i);
				array[8 + i] = (byte)(second64Bits >> 8 * i);
			}
			return array;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0006CE28 File Offset: 0x0006B028
		public static SequencedId StartingIdForSeqeuence(SequencedId sequenceId)
		{
			if (sequenceId == null)
			{
				return null;
			}
			return SequencedIdGenerator.StartingIdForSeqeuence(sequenceId.First64Bits, sequenceId.Second64Bits);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0006CE48 File Offset: 0x0006B048
		public static SequencedId StartingIdForSeqeuence(long first64Bits, long second64Bits)
		{
			byte[] guidBytes = SequencedIdGenerator.GetGuidBytes(first64Bits, second64Bits);
			string text = string.Concat(new object[]
			{
				"urn:uuid:",
				new Guid(guidBytes).ToString(),
				";id=",
				0
			});
			return new SequencedId(text, first64Bits, second64Bits, 0);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0006CEA4 File Offset: 0x0006B0A4
		public SequencedIdGenerator()
			: this(Guid.NewGuid())
		{
			ReleaseAssert.IsTrue(this.m_offset == 0);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0006CEC0 File Offset: 0x0006B0C0
		private SequencedIdGenerator(Guid guid)
		{
			this.m_offset = 0;
			this.m_stringPrefix = "urn:uuid:" + guid.ToString();
			bool flag = SequencedIdGenerator.IsGuidUri(this.m_stringPrefix, out this.m_first64Bits, out this.m_second64Bits);
			ReleaseAssert.IsTrue(flag);
			this.m_stringPrefix += ";id=";
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x0006CF2B File Offset: 0x0006B12B
		public SequencedId StartingId
		{
			get
			{
				return SequencedIdGenerator.StartingIdForSeqeuence(this.m_first64Bits, this.m_second64Bits);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0006CF40 File Offset: 0x0006B140
		public SequencedId Next
		{
			get
			{
				int num = Interlocked.Increment(ref this.m_offset);
				string text = this.m_stringPrefix + num;
				return new SequencedId(text, this.m_first64Bits, this.m_second64Bits, num);
			}
		}

		// Token: 0x0400160B RID: 5643
		internal const string Scheme = "urn:uuid:";

		// Token: 0x0400160C RID: 5644
		internal const string IDParamPrefix = ";id=";

		// Token: 0x0400160D RID: 5645
		internal const int IDParamPrefixIndex = 45;

		// Token: 0x0400160E RID: 5646
		internal const int IdParamIndex = 49;

		// Token: 0x0400160F RID: 5647
		public static readonly SequencedIdGenerator DefaultGenerator = new SequencedIdGenerator();

		// Token: 0x04001610 RID: 5648
		private string m_stringPrefix;

		// Token: 0x04001611 RID: 5649
		private long m_first64Bits;

		// Token: 0x04001612 RID: 5650
		private long m_second64Bits;

		// Token: 0x04001613 RID: 5651
		private int m_offset;
	}
}
