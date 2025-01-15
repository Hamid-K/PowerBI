using System;
using System.Collections.Generic;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000067 RID: 103
	internal sealed class XmlTagToken : XmlToken
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		public XmlTagToken(XmlTokenType type, TextPosition position)
			: base(type, position)
		{
			this._name = string.Empty;
			this._attributes = new List<KeyValuePair<string, string>>();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000ED94 File Offset: 0x0000CF94
		public bool IsSelfClosing
		{
			get
			{
				return this._selfClosing;
			}
			set
			{
				this._selfClosing = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000ED9D File Offset: 0x0000CF9D
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000EDA5 File Offset: 0x0000CFA5
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		public List<KeyValuePair<string, string>> Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000EDB6 File Offset: 0x0000CFB6
		public void AddAttribute(string name)
		{
			this._attributes.Add(new KeyValuePair<string, string>(name, string.Empty));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000EDCE File Offset: 0x0000CFCE
		public void AddAttribute(string name, string value)
		{
			this._attributes.Add(new KeyValuePair<string, string>(name, value));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		public void SetAttributeValue(string value)
		{
			this._attributes[this._attributes.Count - 1] = new KeyValuePair<string, string>(this._attributes[this._attributes.Count - 1].Key, value);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EE30 File Offset: 0x0000D030
		public string GetAttribute(string name)
		{
			for (int num = 0; num != this._attributes.Count; num++)
			{
				if (this._attributes[num].Key == name)
				{
					return this._attributes[num].Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x0400022B RID: 555
		private readonly List<KeyValuePair<string, string>> _attributes;

		// Token: 0x0400022C RID: 556
		private string _name;

		// Token: 0x0400022D RID: 557
		private bool _selfClosing;
	}
}
