using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000516 RID: 1302
	internal sealed class TextRunFilteredStyle : Style
	{
		// Token: 0x06004547 RID: 17735 RVA: 0x00122688 File Offset: 0x00120888
		internal TextRunFilteredStyle(Style style)
			: base(ConstructionPhase.Deserializing)
		{
			this.m_styleAttributes = style.StyleAttributes;
			this.m_expressionList = style.ExpressionList;
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x001226AC File Offset: 0x001208AC
		internal override bool GetAttributeInfo(string styleAttributeName, out AttributeInfo styleAttribute)
		{
			if (styleAttributeName != null)
			{
				switch (styleAttributeName.Length)
				{
				case 5:
					if (!(styleAttributeName == "Color"))
					{
						goto IL_016A;
					}
					break;
				case 6:
					if (!(styleAttributeName == "Format"))
					{
						goto IL_016A;
					}
					break;
				case 7:
				case 11:
				case 12:
				case 13:
					goto IL_016A;
				case 8:
				{
					char c = styleAttributeName[0];
					if (c != 'C')
					{
						if (c != 'F')
						{
							if (c != 'L')
							{
								goto IL_016A;
							}
							if (!(styleAttributeName == "Language"))
							{
								goto IL_016A;
							}
						}
						else if (!(styleAttributeName == "FontSize"))
						{
							goto IL_016A;
						}
					}
					else if (!(styleAttributeName == "Calendar"))
					{
						goto IL_016A;
					}
					break;
				}
				case 9:
					if (!(styleAttributeName == "FontStyle"))
					{
						goto IL_016A;
					}
					break;
				case 10:
				{
					char c = styleAttributeName[4];
					if (c != 'F')
					{
						if (c != 'W')
						{
							goto IL_016A;
						}
						if (!(styleAttributeName == "FontWeight"))
						{
							goto IL_016A;
						}
					}
					else if (!(styleAttributeName == "FontFamily"))
					{
						goto IL_016A;
					}
					break;
				}
				case 14:
				{
					char c = styleAttributeName[0];
					if (c != 'N')
					{
						if (c != 'T')
						{
							goto IL_016A;
						}
						if (!(styleAttributeName == "TextDecoration"))
						{
							goto IL_016A;
						}
					}
					else if (!(styleAttributeName == "NumeralVariant"))
					{
						goto IL_016A;
					}
					break;
				}
				case 15:
					if (!(styleAttributeName == "NumeralLanguage"))
					{
						goto IL_016A;
					}
					break;
				case 16:
					if (!(styleAttributeName == "CurrencyLanguage"))
					{
						goto IL_016A;
					}
					break;
				default:
					goto IL_016A;
				}
				return base.GetAttributeInfo(styleAttributeName, out styleAttribute);
			}
			IL_016A:
			styleAttribute = null;
			return false;
		}
	}
}
