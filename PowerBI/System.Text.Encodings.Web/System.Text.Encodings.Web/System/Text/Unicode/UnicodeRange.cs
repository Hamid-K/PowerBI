using System;
using System.Runtime.CompilerServices;

namespace System.Text.Unicode
{
	// Token: 0x02000020 RID: 32
	public sealed class UnicodeRange
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000035E0 File Offset: 0x000017E0
		public UnicodeRange(int firstCodePoint, int length)
		{
			if (firstCodePoint < 0 || firstCodePoint > 65535)
			{
				throw new ArgumentOutOfRangeException("firstCodePoint");
			}
			if (length < 0 || (long)firstCodePoint + (long)length > 65536L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.FirstCodePoint = firstCodePoint;
			this.Length = length;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003634 File Offset: 0x00001834
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000363C File Offset: 0x0000183C
		public int FirstCodePoint { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003645 File Offset: 0x00001845
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000364D File Offset: 0x0000184D
		public int Length { get; private set; }

		// Token: 0x06000098 RID: 152 RVA: 0x00003656 File Offset: 0x00001856
		[NullableContext(1)]
		public static UnicodeRange Create(char firstCharacter, char lastCharacter)
		{
			if (lastCharacter < firstCharacter)
			{
				throw new ArgumentOutOfRangeException("lastCharacter");
			}
			return new UnicodeRange((int)firstCharacter, (int)('\u0001' + (lastCharacter - firstCharacter)));
		}
	}
}
