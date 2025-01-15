using System;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A1 RID: 161
	internal sealed class SqlUnicodeEncoding : UnicodeEncoding
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x00026E00 File Offset: 0x00025000
		private SqlUnicodeEncoding()
			: base(false, false, false)
		{
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00026E0B File Offset: 0x0002500B
		public override Decoder GetDecoder()
		{
			return new SqlUnicodeEncoding.SqlUnicodeDecoder();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00026E12 File Offset: 0x00025012
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00026E17 File Offset: 0x00025017
		public static Encoding SqlUnicodeEncodingInstance
		{
			get
			{
				return SqlUnicodeEncoding.s_singletonEncoding;
			}
		}

		// Token: 0x04000361 RID: 865
		private static readonly SqlUnicodeEncoding s_singletonEncoding = new SqlUnicodeEncoding();

		// Token: 0x020001DA RID: 474
		private sealed class SqlUnicodeDecoder : Decoder
		{
			// Token: 0x06001DD9 RID: 7641 RVA: 0x0007B2E4 File Offset: 0x000794E4
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			// Token: 0x06001DDA RID: 7642 RVA: 0x0007B2EC File Offset: 0x000794EC
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				int num;
				int num2;
				bool flag;
				this.Convert(bytes, byteIndex, byteCount, chars, charIndex, chars.Length - charIndex, true, out num, out num2, out flag);
				return num2;
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x0007B315 File Offset: 0x00079515
			public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
			{
				charsUsed = Math.Min(charCount, byteCount / 2);
				bytesUsed = charsUsed * 2;
				completed = bytesUsed == byteCount;
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, bytesUsed);
			}
		}
	}
}
