using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x0200019A RID: 410
	internal class NLogXmlElement : ILoggingConfigurationElement
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x000329AC File Offset: 0x00030BAC
		public NLogXmlElement(string inputUri)
			: this()
		{
			using (XmlReader xmlReader = XmlReader.Create(inputUri))
			{
				xmlReader.MoveToContent();
				this.Parse(xmlReader, true);
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000329F4 File Offset: 0x00030BF4
		public NLogXmlElement(XmlReader reader)
			: this(reader, false)
		{
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000329FE File Offset: 0x00030BFE
		private NLogXmlElement(XmlReader reader, bool nestedElement)
			: this()
		{
			this.Parse(reader, nestedElement);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00032A0E File Offset: 0x00030C0E
		private NLogXmlElement()
		{
			this.AttributeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.Children = new List<NLogXmlElement>();
			this._parsingErrors = new List<string>();
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00032A3C File Offset: 0x00030C3C
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00032A44 File Offset: 0x00030C44
		public string LocalName { get; private set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00032A4D File Offset: 0x00030C4D
		public Dictionary<string, string> AttributeValues { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00032A55 File Offset: 0x00030C55
		public IList<NLogXmlElement> Children { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00032A5D File Offset: 0x00030C5D
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00032A65 File Offset: 0x00030C65
		public string Value { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00032A6E File Offset: 0x00030C6E
		public string Name
		{
			get
			{
				return this.LocalName;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00032A78 File Offset: 0x00030C78
		public IEnumerable<KeyValuePair<string, string>> Values
		{
			get
			{
				for (int i = 0; i < this.Children.Count; i++)
				{
					if (NLogXmlElement.SingleValueElement(this.Children[i]))
					{
						return (from item in this.Children
							where NLogXmlElement.SingleValueElement(item)
							select new KeyValuePair<string, string>(item.Name, item.Value)).Concat(this.AttributeValues);
					}
				}
				return this.AttributeValues;
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00032B0E File Offset: 0x00030D0E
		private static bool SingleValueElement(NLogXmlElement child)
		{
			return child.Children.Count == 0 && child.AttributeValues.Count == 0 && child.Value != null;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00032B38 File Offset: 0x00030D38
		IEnumerable<ILoggingConfigurationElement> ILoggingConfigurationElement.Children
		{
			get
			{
				for (int i = 0; i < this.Children.Count; i++)
				{
					if (!NLogXmlElement.SingleValueElement(this.Children[i]))
					{
						return this.Children.Where((NLogXmlElement item) => !NLogXmlElement.SingleValueElement(item)).Cast<ILoggingConfigurationElement>();
					}
				}
				return ArrayHelper.Empty<ILoggingConfigurationElement>();
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00032BA4 File Offset: 0x00030DA4
		public IEnumerable<NLogXmlElement> Elements(string elementName)
		{
			List<NLogXmlElement> list = new List<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in this.Children)
			{
				if (nlogXmlElement.LocalName.Equals(elementName, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(nlogXmlElement);
				}
			}
			return list;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00032C08 File Offset: 0x00030E08
		public void AssertName(params string[] allowedNames)
		{
			foreach (string text in allowedNames)
			{
				if (this.LocalName.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Assertion failed. Expected element name '",
				string.Join("|", allowedNames),
				"', actual: '",
				this.LocalName,
				"'."
			}));
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00032C78 File Offset: 0x00030E78
		public IEnumerable<string> GetParsingErrors()
		{
			foreach (string text in this._parsingErrors)
			{
				yield return text;
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			foreach (NLogXmlElement nlogXmlElement in this.Children)
			{
				foreach (string text2 in nlogXmlElement.GetParsingErrors())
				{
					yield return text2;
				}
				IEnumerator<string> enumerator3 = null;
			}
			IEnumerator<NLogXmlElement> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00032C88 File Offset: 0x00030E88
		private void Parse(XmlReader reader, bool nestedElement)
		{
			this.ParseAttributes(reader, nestedElement);
			this.LocalName = reader.LocalName;
			if (!reader.IsEmptyElement)
			{
				while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
				{
					if (reader.NodeType == XmlNodeType.CDATA || reader.NodeType == XmlNodeType.Text)
					{
						this.Value += reader.Value;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						this.Children.Add(new NLogXmlElement(reader, true));
					}
				}
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00032D0C File Offset: 0x00030F0C
		private void ParseAttributes(XmlReader reader, bool nestedElement)
		{
			if (reader.MoveToFirstAttribute())
			{
				do
				{
					if (nestedElement || !NLogXmlElement.IsSpecialXmlAttribute(reader))
					{
						if (!this.AttributeValues.ContainsKey(reader.LocalName))
						{
							this.AttributeValues.Add(reader.LocalName, reader.Value);
						}
						else
						{
							string text = string.Concat(new string[]
							{
								"Duplicate attribute detected. Attribute name: [",
								reader.LocalName,
								"]. Duplicate value:[",
								reader.Value,
								"], Current value:[",
								this.AttributeValues[reader.LocalName],
								"]"
							});
							this._parsingErrors.Add(text);
						}
					}
				}
				while (reader.MoveToNextAttribute());
				reader.MoveToElement();
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00032DD0 File Offset: 0x00030FD0
		private static bool IsSpecialXmlAttribute(XmlReader reader)
		{
			string localName = reader.LocalName;
			if (localName != null && localName.Equals("xmlns", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			string prefix = reader.Prefix;
			if (prefix != null && prefix.Equals("xsi", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			string prefix2 = reader.Prefix;
			return prefix2 != null && prefix2.Equals("xmlns", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000504 RID: 1284
		private readonly List<string> _parsingErrors;
	}
}
