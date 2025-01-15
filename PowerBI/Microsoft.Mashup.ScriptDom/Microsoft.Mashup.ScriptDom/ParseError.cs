using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	internal sealed class ParseError
	{
		// Token: 0x06002108 RID: 8456 RVA: 0x0015C543 File Offset: 0x0015A743
		public ParseError(int number, int offset, int line, int column, string message)
		{
			this._number = number;
			this._offset = offset;
			this._message = message;
			this._line = line;
			this._column = column;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x0015C570 File Offset: 0x0015A770
		public int Number
		{
			get
			{
				return this._number;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x0015C578 File Offset: 0x0015A778
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x0015C580 File Offset: 0x0015A780
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x0015C588 File Offset: 0x0015A788
		public int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0015C590 File Offset: 0x0015A790
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x040018A6 RID: 6310
		private readonly int _number;

		// Token: 0x040018A7 RID: 6311
		private readonly int _offset;

		// Token: 0x040018A8 RID: 6312
		private readonly int _line;

		// Token: 0x040018A9 RID: 6313
		private readonly int _column;

		// Token: 0x040018AA RID: 6314
		private readonly string _message;
	}
}
