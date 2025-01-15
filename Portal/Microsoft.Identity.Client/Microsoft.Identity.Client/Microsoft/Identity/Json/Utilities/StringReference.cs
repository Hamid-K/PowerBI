using System;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200006B RID: 107
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
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00018D87 File Offset: 0x00016F87
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00018D8F File Offset: 0x00016F8F
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00018D97 File Offset: 0x00016F97
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00018D9F File Offset: 0x00016F9F
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00018DB6 File Offset: 0x00016FB6
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x04000204 RID: 516
		private readonly char[] _chars;

		// Token: 0x04000205 RID: 517
		private readonly int _startIndex;

		// Token: 0x04000206 RID: 518
		private readonly int _length;
	}
}
