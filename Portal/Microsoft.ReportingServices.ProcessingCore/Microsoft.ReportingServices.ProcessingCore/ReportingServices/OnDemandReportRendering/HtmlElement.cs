using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001FF RID: 511
	internal class HtmlElement
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x0004DFF6 File Offset: 0x0004C1F6
		internal HtmlElement(HtmlElement.HtmlNodeType nodeType, HtmlElement.HtmlElementType elemntType, int characterPosition)
			: this(nodeType, elemntType, null, true, characterPosition)
		{
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004E003 File Offset: 0x0004C203
		internal HtmlElement(HtmlElement.HtmlNodeType nodeType, HtmlElement.HtmlElementType elemntType, string value, int characterPosition)
			: this(nodeType, elemntType, null, false, characterPosition)
		{
			this.m_value = value;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004E018 File Offset: 0x0004C218
		internal HtmlElement(HtmlElement.HtmlNodeType nodeType, HtmlElement.HtmlElementType type, string attributesAsString, bool isEmpty, int characterPosition)
		{
			this.m_nodeType = nodeType;
			this.m_elementType = type;
			this.m_isEmptyElement = isEmpty;
			this.m_characterPosition = characterPosition;
			if (!string.IsNullOrEmpty(attributesAsString))
			{
				this.m_attributesAsString = attributesAsString;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0004E04D File Offset: 0x0004C24D
		internal bool IsEmptyElement
		{
			get
			{
				return this.m_isEmptyElement;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0004E055 File Offset: 0x0004C255
		internal HtmlElement.HtmlNodeType NodeType
		{
			get
			{
				return this.m_nodeType;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0004E05D File Offset: 0x0004C25D
		internal HtmlElement.HtmlElementType ElementType
		{
			get
			{
				return this.m_elementType;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0004E065 File Offset: 0x0004C265
		internal Dictionary<string, string> Attributes
		{
			get
			{
				this.ParseAttributes();
				return this.m_parsedAttributes;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0004E074 File Offset: 0x0004C274
		internal Dictionary<string, string> CssStyle
		{
			get
			{
				if (this.HasAttributes)
				{
					this.ParseAttributes();
					string text;
					if (this.m_parsedAttributes.TryGetValue("style", out text) && !string.IsNullOrEmpty(text))
					{
						this.ParseCssStyle(text);
					}
				}
				return this.m_parsedCssStyleValues;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004E0B8 File Offset: 0x0004C2B8
		internal bool HasAttributes
		{
			get
			{
				return this.m_attributesAsString != null || this.m_parsedAttributes != null;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004E0CD File Offset: 0x0004C2CD
		internal string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004E0D5 File Offset: 0x0004C2D5
		internal int CharacterPosition
		{
			get
			{
				return this.m_characterPosition;
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004E0E0 File Offset: 0x0004C2E0
		private void ParseCssStyle(string cssStyles)
		{
			string[] array = cssStyles.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			this.m_parsedCssStyleValues = new Dictionary<string, string>(array.Length, StringEqualityComparer.Instance);
			foreach (string text in array)
			{
				string text2 = string.Empty;
				string text3 = string.Empty;
				int num = text.IndexOf(':');
				if (num == -1)
				{
					text2 = text.Trim();
				}
				else if (num > 0)
				{
					text2 = text.Substring(0, num).Trim();
					if (num + 1 < text.Length)
					{
						text3 = text.Substring(num + 1).Trim();
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_parsedCssStyleValues[text2.ToLowerInvariant()] = HtmlEntityResolver.ResolveEntities(text3).ToLowerInvariant();
				}
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0004E1A8 File Offset: 0x0004C3A8
		private void ParseAttributes()
		{
			if (this.m_attributesAsString != null)
			{
				MatchCollection matchCollection = HtmlElement.m_AttributeRegEx.Matches(this.m_attributesAsString.Trim());
				if (matchCollection.Count > 0)
				{
					this.m_parsedAttributes = new Dictionary<string, string>(matchCollection.Count, StringEqualityComparer.Instance);
					for (int i = 0; i < matchCollection.Count; i++)
					{
						Match match = matchCollection[i];
						string text = null;
						Group group = match.Groups["name"];
						if (group.Length > 0)
						{
							string value = group.Value;
							group = match.Groups["quotedvalue"];
							if (group.Length > 0)
							{
								text = group.Value;
							}
							else
							{
								group = match.Groups["singlequotedvalue"];
								if (group.Length > 0)
								{
									text = group.Value;
								}
								else
								{
									group = match.Groups["value"];
									if (group.Length > 0)
									{
										text = group.Value;
									}
								}
							}
							this.m_parsedAttributes[value.ToLowerInvariant()] = HtmlEntityResolver.ResolveEntities(text);
						}
					}
				}
			}
			this.m_attributesAsString = null;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004E2D8 File Offset: 0x0004C4D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_nodeType.ToString());
			stringBuilder.Append(" Type = ");
			stringBuilder.Append(this.m_elementType.ToString());
			if (!this.m_isEmptyElement)
			{
				if (this.HasAttributes)
				{
					this.GetDictionaryAsString("Attributes", this.Attributes, stringBuilder);
					this.GetDictionaryAsString("CssStyle", this.CssStyle, stringBuilder);
				}
				if (this.m_value != null)
				{
					stringBuilder.Append("; Value = \"");
					stringBuilder.Append(this.m_value);
					stringBuilder.Append("\"");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004E390 File Offset: 0x0004C590
		private void GetDictionaryAsString(string name, Dictionary<string, string> dict, StringBuilder sb)
		{
			if (dict != null)
			{
				sb.Append("; ");
				sb.Append(name);
				sb.Append(" = { ");
				int num = 0;
				foreach (KeyValuePair<string, string> keyValuePair in dict)
				{
					if (num > 0)
					{
						sb.Append(", ");
					}
					num++;
					sb.Append(keyValuePair.Key);
					sb.Append("=\"");
					sb.Append(keyValuePair.Value);
					sb.Append("\"");
				}
				sb.Append(" }");
			}
		}

		// Token: 0x04000931 RID: 2353
		private string m_value;

		// Token: 0x04000932 RID: 2354
		private bool m_isEmptyElement;

		// Token: 0x04000933 RID: 2355
		private HtmlElement.HtmlNodeType m_nodeType;

		// Token: 0x04000934 RID: 2356
		private HtmlElement.HtmlElementType m_elementType;

		// Token: 0x04000935 RID: 2357
		private string m_attributesAsString;

		// Token: 0x04000936 RID: 2358
		private Dictionary<string, string> m_parsedAttributes;

		// Token: 0x04000937 RID: 2359
		private Dictionary<string, string> m_parsedCssStyleValues;

		// Token: 0x04000938 RID: 2360
		private int m_characterPosition;

		// Token: 0x04000939 RID: 2361
		private static Regex m_AttributeRegEx = new Regex("((?<name>\\w+)(\\s*=\\s*((\"(?<quotedvalue>[^\"]*)\")|('(?<singlequotedvalue>[^']*)')|(?<value>[^ =]+))?)?)*", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x02000938 RID: 2360
		internal enum HtmlNodeType
		{
			// Token: 0x04003FBE RID: 16318
			Element,
			// Token: 0x04003FBF RID: 16319
			EndElement,
			// Token: 0x04003FC0 RID: 16320
			Text,
			// Token: 0x04003FC1 RID: 16321
			Comment,
			// Token: 0x04003FC2 RID: 16322
			ScriptText,
			// Token: 0x04003FC3 RID: 16323
			StyleText
		}

		// Token: 0x02000939 RID: 2361
		internal enum HtmlElementType
		{
			// Token: 0x04003FC5 RID: 16325
			None,
			// Token: 0x04003FC6 RID: 16326
			Unsupported,
			// Token: 0x04003FC7 RID: 16327
			SCRIPT,
			// Token: 0x04003FC8 RID: 16328
			STYLE,
			// Token: 0x04003FC9 RID: 16329
			P,
			// Token: 0x04003FCA RID: 16330
			DIV,
			// Token: 0x04003FCB RID: 16331
			BR,
			// Token: 0x04003FCC RID: 16332
			UL,
			// Token: 0x04003FCD RID: 16333
			OL,
			// Token: 0x04003FCE RID: 16334
			LI,
			// Token: 0x04003FCF RID: 16335
			SPAN,
			// Token: 0x04003FD0 RID: 16336
			FONT,
			// Token: 0x04003FD1 RID: 16337
			A,
			// Token: 0x04003FD2 RID: 16338
			STRONG,
			// Token: 0x04003FD3 RID: 16339
			STRIKE,
			// Token: 0x04003FD4 RID: 16340
			B,
			// Token: 0x04003FD5 RID: 16341
			I,
			// Token: 0x04003FD6 RID: 16342
			U,
			// Token: 0x04003FD7 RID: 16343
			S,
			// Token: 0x04003FD8 RID: 16344
			EM,
			// Token: 0x04003FD9 RID: 16345
			H1,
			// Token: 0x04003FDA RID: 16346
			H2,
			// Token: 0x04003FDB RID: 16347
			H3,
			// Token: 0x04003FDC RID: 16348
			H4,
			// Token: 0x04003FDD RID: 16349
			H5,
			// Token: 0x04003FDE RID: 16350
			H6,
			// Token: 0x04003FDF RID: 16351
			DD,
			// Token: 0x04003FE0 RID: 16352
			DT,
			// Token: 0x04003FE1 RID: 16353
			BLOCKQUOTE,
			// Token: 0x04003FE2 RID: 16354
			TITLE
		}
	}
}
