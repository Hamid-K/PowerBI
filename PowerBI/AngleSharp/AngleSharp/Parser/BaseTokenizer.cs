using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Extensions;

namespace AngleSharp.Parser
{
	// Token: 0x02000056 RID: 86
	internal abstract class BaseTokenizer : IDisposable
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000CD7D File Offset: 0x0000AF7D
		public BaseTokenizer(TextSource source)
		{
			this.StringBuffer = Pool.NewStringBuilder();
			this._columns = new Stack<ushort>();
			this._source = source;
			this._current = '\0';
			this._column = 0;
			this._row = 1;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000CDB7 File Offset: 0x0000AFB7
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		private protected StringBuilder StringBuffer { protected get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		public TextSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		public int InsertionPoint
		{
			get
			{
				return this._source.Index;
			}
			protected set
			{
				int i;
				for (i = this._source.Index - value; i > 0; i--)
				{
					this.BackUnsafe();
				}
				while (i < 0)
				{
					this.AdvanceUnsafe();
					i++;
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000CE1B File Offset: 0x0000B01B
		public ushort Line
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000CE23 File Offset: 0x0000B023
		public ushort Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		public int Position
		{
			get
			{
				return this._source.Index;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000CE2B File Offset: 0x0000B02B
		protected char Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000CE33 File Offset: 0x0000B033
		public string FlushBuffer()
		{
			string text = this.StringBuffer.ToString();
			this.StringBuffer.Clear();
			return text;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public void Dispose()
		{
			if (this.StringBuffer != null)
			{
				TextSource source = this._source;
				if (source != null)
				{
					((IDisposable)source).Dispose();
				}
				this.StringBuffer.Clear().ToPool();
				this.StringBuffer = null;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000CE82 File Offset: 0x0000B082
		public TextPosition GetCurrentPosition()
		{
			return new TextPosition(this._row, this._column, this.Position);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000CE9C File Offset: 0x0000B09C
		protected bool ContinuesWithInsensitive(string s)
		{
			string text = this.PeekString(s.Length);
			return text.Length == s.Length && text.Isi(s);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		protected bool ContinuesWithSensitive(string s)
		{
			string text = this.PeekString(s.Length);
			return text.Length == s.Length && text.Isi(s);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000CF04 File Offset: 0x0000B104
		protected string PeekString(int length)
		{
			int index = this._source.Index;
			TextSource source = this._source;
			int index2 = source.Index;
			source.Index = index2 - 1;
			string text = this._source.ReadCharacters(length);
			this._source.Index = index;
			return text;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000CF4C File Offset: 0x0000B14C
		protected char SkipSpaces()
		{
			char c = this.GetNext();
			while (c.IsSpaceCharacter())
			{
				c = this.GetNext();
			}
			return c;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000CF72 File Offset: 0x0000B172
		protected char GetNext()
		{
			this.Advance();
			return this._current;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000CF80 File Offset: 0x0000B180
		protected char GetPrevious()
		{
			this.Back();
			return this._current;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000CF8E File Offset: 0x0000B18E
		protected void Advance()
		{
			if (this._current != '\uffff')
			{
				this.AdvanceUnsafe();
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		protected void Advance(int n)
		{
			while (n-- > 0 && this._current != '\uffff')
			{
				this.AdvanceUnsafe();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000CFC3 File Offset: 0x0000B1C3
		protected void Back()
		{
			if (this.InsertionPoint > 0)
			{
				this.BackUnsafe();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		protected void Back(int n)
		{
			while (n-- > 0 && this.InsertionPoint > 0)
			{
				this.BackUnsafe();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		private void AdvanceUnsafe()
		{
			if (this._current == '\n')
			{
				this._columns.Push(this._column);
				this._column = 1;
				this._row += 1;
			}
			else
			{
				this._column += 1;
			}
			this._current = this.NormalizeForward(this._source.ReadCharacter());
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D058 File Offset: 0x0000B258
		private void BackUnsafe()
		{
			this._source.Index--;
			if (this._source.Index == 0)
			{
				this._column = 0;
				this._current = '\0';
				return;
			}
			char c = this.NormalizeBackward(this._source[this._source.Index - 1]);
			if (c == '\n')
			{
				this._column = ((this._columns.Count != 0) ? this._columns.Pop() : 1);
				this._row -= 1;
				this._current = c;
				return;
			}
			if (c != '\0')
			{
				this._current = c;
				this._column -= 1;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000D10C File Offset: 0x0000B30C
		private char NormalizeForward(char p)
		{
			if (p != '\r')
			{
				return p;
			}
			if (this._source.ReadCharacter() != '\n')
			{
				TextSource source = this._source;
				int index = source.Index;
				source.Index = index - 1;
			}
			return '\n';
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000D148 File Offset: 0x0000B348
		private char NormalizeBackward(char p)
		{
			if (p != '\r')
			{
				return p;
			}
			if (this._source.Index < this._source.Length && this._source[this._source.Index] == '\n')
			{
				this.BackUnsafe();
				return '\0';
			}
			return '\n';
		}

		// Token: 0x040001D3 RID: 467
		private readonly Stack<ushort> _columns;

		// Token: 0x040001D4 RID: 468
		private readonly TextSource _source;

		// Token: 0x040001D5 RID: 469
		private ushort _column;

		// Token: 0x040001D6 RID: 470
		private ushort _row;

		// Token: 0x040001D7 RID: 471
		private char _current;
	}
}
