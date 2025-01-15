using System;

namespace NLog.Internal
{
	// Token: 0x0200013E RID: 318
	internal class SimpleStringReader
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x000279F0 File Offset: 0x00025BF0
		public SimpleStringReader(string text)
		{
			this._text = text;
			this.Position = 0;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00027A06 File Offset: 0x00025C06
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00027A0E File Offset: 0x00025C0E
		internal int Position { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00027A17 File Offset: 0x00025C17
		internal string Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00027A1F File Offset: 0x00025C1F
		internal int Peek()
		{
			if (this.Position < this._text.Length)
			{
				return (int)this._text[this.Position];
			}
			return -1;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00027A48 File Offset: 0x00025C48
		internal int Read()
		{
			if (this.Position < this._text.Length)
			{
				string text = this._text;
				int position = this.Position;
				this.Position = position + 1;
				return (int)text[position];
			}
			return -1;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00027A86 File Offset: 0x00025C86
		internal string Substring(int startIndex, int endIndex)
		{
			return this._text.Substring(startIndex, endIndex - startIndex);
		}

		// Token: 0x0400042D RID: 1069
		private readonly string _text;
	}
}
