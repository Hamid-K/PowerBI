using System;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000076 RID: 118
	public class HtmlToken
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00017151 File Offset: 0x00015351
		public HtmlToken(HtmlTokenType type, TextPosition position, string name = null)
		{
			this._type = type;
			this._position = position;
			this._name = name;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001716E File Offset: 0x0001536E
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this._name);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0001717C File Offset: 0x0001537C
		public bool HasContent
		{
			get
			{
				for (int i = 0; i < this._name.Length; i++)
				{
					if (!this._name[i].IsSpaceCharacter())
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000171B5 File Offset: 0x000153B5
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000171BD File Offset: 0x000153BD
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000171B5 File Offset: 0x000153B5
		public string Data
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000171C6 File Offset: 0x000153C6
		public TextPosition Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000171CE File Offset: 0x000153CE
		public bool IsHtmlCompatible
		{
			get
			{
				return this._type == HtmlTokenType.StartTag || this._type == HtmlTokenType.Character;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000171E4 File Offset: 0x000153E4
		public bool IsSvg
		{
			get
			{
				return this.IsStartTag(TagNames.Svg);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000171F1 File Offset: 0x000153F1
		public bool IsMathCompatible
		{
			get
			{
				return (!this.IsStartTag("mglyph") && !this.IsStartTag("malignmark")) || this._type == HtmlTokenType.Character;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00017218 File Offset: 0x00015418
		public HtmlTokenType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00017220 File Offset: 0x00015420
		public string TrimStart()
		{
			int num = 0;
			while (num < this._name.Length && this._name[num].IsSpaceCharacter())
			{
				num++;
			}
			string text = this._name.Substring(0, num);
			this._name = this._name.Substring(num);
			return text;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00017277 File Offset: 0x00015477
		public void RemoveNewLine()
		{
			if (this._name.Has('\n', 0))
			{
				this._name = this._name.Substring(1);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001729B File Offset: 0x0001549B
		public HtmlTagToken AsTag()
		{
			return (HtmlTagToken)this;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000172A3 File Offset: 0x000154A3
		public bool IsStartTag(string name)
		{
			return this._type == HtmlTokenType.StartTag && this._name.Is(name);
		}

		// Token: 0x040002CF RID: 719
		private readonly HtmlTokenType _type;

		// Token: 0x040002D0 RID: 720
		private readonly TextPosition _position;

		// Token: 0x040002D1 RID: 721
		private string _name;
	}
}
