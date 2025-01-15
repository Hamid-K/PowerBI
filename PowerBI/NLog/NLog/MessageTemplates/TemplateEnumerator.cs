using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NLog.MessageTemplates
{
	// Token: 0x02000088 RID: 136
	internal struct TemplateEnumerator : IEnumerator<LiteralHole>, IDisposable, IEnumerator
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x00019040 File Offset: 0x00017240
		public TemplateEnumerator(string template)
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			this._template = template;
			this._length = this._template.Length;
			this._position = 0;
			this._literalLength = 0;
			this._current = default(LiteralHole);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001908E File Offset: 0x0001728E
		public LiteralHole Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00019096 File Offset: 0x00017296
		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000190A3 File Offset: 0x000172A3
		public void Dispose()
		{
			this._template = string.Empty;
			this._length = 0;
			this.Reset();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000190BD File Offset: 0x000172BD
		public void Reset()
		{
			this._position = 0;
			this._literalLength = 0;
			this._current = default(LiteralHole);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000190DC File Offset: 0x000172DC
		public bool MoveNext()
		{
			bool flag;
			try
			{
				while (this._position < this._length)
				{
					char c = this.Peek();
					if (c == '{')
					{
						this.ParseOpenBracketPart();
						return true;
					}
					if (c == '}')
					{
						this.ParseCloseBracketPart();
						return true;
					}
					this.ParseTextPart();
				}
				if (this._literalLength != 0)
				{
					this.AddLiteral();
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new TemplateParserException("Unexpected end of template.", this._position, this._template);
			}
			return flag;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00019164 File Offset: 0x00017364
		private void AddLiteral()
		{
			this._current = new LiteralHole(new Literal
			{
				Print = this._literalLength,
				Skip = 0
			}, default(Hole));
			this._literalLength = 0;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000191AA File Offset: 0x000173AA
		private void ParseTextPart()
		{
			this._literalLength = (int)((short)this.SkipUntil(TemplateEnumerator.TextDelimiters, false));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000191C0 File Offset: 0x000173C0
		private void ParseOpenBracketPart()
		{
			this.Skip('{');
			char c = this.Peek();
			if (c == '$')
			{
				this.Skip('$');
				this.ParseHole(CaptureType.Stringify);
				return;
			}
			if (c == '@')
			{
				this.Skip('@');
				this.ParseHole(CaptureType.Serialize);
				return;
			}
			if (c == '{')
			{
				this.Skip('{');
				this._literalLength++;
				this.AddLiteral();
				return;
			}
			this.ParseHole(CaptureType.Normal);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00019230 File Offset: 0x00017430
		private void ParseCloseBracketPart()
		{
			this.Skip('}');
			if (this.Read() != '}')
			{
				throw new TemplateParserException("Unexpected '}}' ", this._position - 2, this._template);
			}
			this._literalLength++;
			this.AddLiteral();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001927C File Offset: 0x0001747C
		private void ParseHole(CaptureType type)
		{
			int position = this._position;
			int num;
			string text = this.ParseName(out num);
			int num2 = 0;
			string text2 = null;
			if (this.Peek() != '}')
			{
				num2 = ((this.Peek() == ',') ? this.ParseAlignment() : 0);
				text2 = ((this.Peek() == ':') ? this.ParseFormat() : null);
				this.Skip('}');
			}
			else
			{
				this._position++;
			}
			int num3 = this._position - position + ((type == CaptureType.Normal) ? 1 : 2);
			this._current = new LiteralHole(new Literal
			{
				Print = this._literalLength,
				Skip = (short)num3
			}, new Hole(text, text2, type, (short)num, (short)num2));
			this._literalLength = 0;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001933C File Offset: 0x0001753C
		private string ParseName(out int parameterIndex)
		{
			parameterIndex = -1;
			char c = this.Peek();
			if (c >= '0' && c <= '9')
			{
				int position = this._position;
				int num = this.ReadInt();
				c = this.Peek();
				if (num >= 0)
				{
					if (c == '}' || c == ':' || c == ',')
					{
						parameterIndex = num;
						return TemplateEnumerator.ParameterIndexToString(parameterIndex);
					}
					if (c == ' ')
					{
						this.SkipSpaces();
						c = this.Peek();
						if (c == '}' || c == ':' || c == ',')
						{
							parameterIndex = num;
						}
					}
				}
				this._position = position;
			}
			return this.ReadUntil(TemplateEnumerator.HoleDelimiters, true);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000193C8 File Offset: 0x000175C8
		private static string ParameterIndexToString(int parameterIndex)
		{
			switch (parameterIndex)
			{
			case 0:
				return "0";
			case 1:
				return "1";
			case 2:
				return "2";
			case 3:
				return "3";
			case 4:
				return "4";
			case 5:
				return "5";
			case 6:
				return "6";
			case 7:
				return "7";
			case 8:
				return "8";
			case 9:
				return "9";
			default:
				return parameterIndex.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00019450 File Offset: 0x00017650
		private string ParseFormat()
		{
			this.Skip(':');
			string text = this.ReadUntil(TemplateEnumerator.TextDelimiters, true);
			char c2;
			for (;;)
			{
				char c = this.Read();
				if (c != '{')
				{
					if (c == '}')
					{
						if (this._position >= this._length || this.Peek() != '}')
						{
							break;
						}
						this.Skip('}');
						text += "}";
					}
				}
				else
				{
					c2 = this.Peek();
					if (c2 != '{')
					{
						goto IL_0089;
					}
					this.Skip('{');
					text += "{";
				}
				text += this.ReadUntil(TemplateEnumerator.TextDelimiters, true);
			}
			this._position--;
			return text;
			IL_0089:
			throw new TemplateParserException(string.Format("Expected '{{' but found '{0}' instead in format.", c2), this._position, this._template);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00019520 File Offset: 0x00017720
		private int ParseAlignment()
		{
			this.Skip(',');
			this.SkipSpaces();
			int num = this.ReadInt();
			this.SkipSpaces();
			char c = this.Peek();
			if (c != ':' && c != '}')
			{
				throw new TemplateParserException(string.Format("Expected ':' or '}}' but found '{0}' instead.", c), this._position, this._template);
			}
			return num;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001957A File Offset: 0x0001777A
		private char Peek()
		{
			return this._template[this._position];
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00019590 File Offset: 0x00017790
		private char Read()
		{
			string template = this._template;
			int position = this._position;
			this._position = position + 1;
			return template[position];
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000195B9 File Offset: 0x000177B9
		private void Skip(char c)
		{
			this._position++;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000195C9 File Offset: 0x000177C9
		private void SkipSpaces()
		{
			while (this._template[this._position] == ' ')
			{
				this._position++;
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000195F0 File Offset: 0x000177F0
		private int SkipUntil(char[] search, bool required = true)
		{
			int position = this._position;
			int num = this._template.IndexOfAny(search, this._position);
			if (num == -1 && required)
			{
				string text = string.Join(", ", search.Select((char c) => "'" + c.ToString() + "'").ToArray<string>());
				throw new TemplateParserException("Reached end of template while expecting one of " + text + ".", this._position, this._template);
			}
			this._position = ((num == -1) ? this._length : num);
			return this._position - position;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00019694 File Offset: 0x00017894
		private int ReadInt()
		{
			bool flag = false;
			int num = 0;
			for (int i = 0; i < 12; i++)
			{
				char c = this.Peek();
				int num2 = (int)(c - '0');
				if (num2 < 0 || num2 > 9)
				{
					if ((i > 0 && !flag) || i > 1)
					{
						if (!flag)
						{
							return num;
						}
						return -num;
					}
					else
					{
						if (i != 0 || c != '-')
						{
							break;
						}
						flag = true;
						this._position++;
					}
				}
				else
				{
					this._position++;
					num = num * 10 + num2;
				}
			}
			throw new TemplateParserException("An integer is expected", this._position, this._template);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00019724 File Offset: 0x00017924
		private string ReadUntil(char[] search, bool required = true)
		{
			int position = this._position;
			return this._template.Substring(position, this.SkipUntil(search, required));
		}

		// Token: 0x0400023E RID: 574
		private static readonly char[] HoleDelimiters = new char[] { '}', ':', ',' };

		// Token: 0x0400023F RID: 575
		private static readonly char[] TextDelimiters = new char[] { '{', '}' };

		// Token: 0x04000240 RID: 576
		private string _template;

		// Token: 0x04000241 RID: 577
		private int _length;

		// Token: 0x04000242 RID: 578
		private int _position;

		// Token: 0x04000243 RID: 579
		private int _literalLength;

		// Token: 0x04000244 RID: 580
		private LiteralHole _current;

		// Token: 0x04000245 RID: 581
		private const short Zero = 0;
	}
}
