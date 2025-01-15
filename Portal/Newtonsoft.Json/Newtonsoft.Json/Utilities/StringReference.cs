using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006B RID: 107
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StringReference
	{
		// Token: 0x170000D3 RID: 211
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001933B File Offset: 0x0001753B
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00019343 File Offset: 0x00017543
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001934B File Offset: 0x0001754B
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00019353 File Offset: 0x00017553
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001936A File Offset: 0x0001756A
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x0400021E RID: 542
		private readonly char[] _chars;

		// Token: 0x0400021F RID: 543
		private readonly int _startIndex;

		// Token: 0x04000220 RID: 544
		private readonly int _length;
	}
}
