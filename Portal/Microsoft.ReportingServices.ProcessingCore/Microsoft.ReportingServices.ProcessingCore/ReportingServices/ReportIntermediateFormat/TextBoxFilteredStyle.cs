using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000514 RID: 1300
	internal sealed class TextBoxFilteredStyle : Style
	{
		// Token: 0x06004543 RID: 17731 RVA: 0x00122278 File Offset: 0x00120478
		internal TextBoxFilteredStyle(Style style)
			: base(ConstructionPhase.Deserializing)
		{
			this.m_styleAttributes = style.StyleAttributes;
			this.m_expressionList = style.ExpressionList;
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x0012229C File Offset: 0x0012049C
		internal override bool GetAttributeInfo(string styleAttributeName, out AttributeInfo styleAttribute)
		{
			if (styleAttributeName != null)
			{
				switch (styleAttributeName.Length)
				{
				case 9:
					if (!(styleAttributeName == "Direction"))
					{
						goto IL_0391;
					}
					break;
				case 10:
					if (!(styleAttributeName == "PaddingTop"))
					{
						goto IL_0391;
					}
					break;
				case 11:
				{
					char c = styleAttributeName[7];
					if (c <= 'M')
					{
						if (c != 'L')
						{
							if (c != 'M')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "WritingMode"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "PaddingLeft"))
						{
							goto IL_0391;
						}
					}
					else if (c != 'i')
					{
						if (c != 'o')
						{
							if (c != 't')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "BorderStyle"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "BorderColor"))
						{
							goto IL_0391;
						}
					}
					else if (!(styleAttributeName == "BorderWidth"))
					{
						goto IL_0391;
					}
					break;
				}
				case 12:
					if (!(styleAttributeName == "PaddingRight"))
					{
						goto IL_0391;
					}
					break;
				case 13:
				{
					char c = styleAttributeName[0];
					if (c != 'P')
					{
						if (c != 'V')
						{
							goto IL_0391;
						}
						if (!(styleAttributeName == "VerticalAlign"))
						{
							goto IL_0391;
						}
					}
					else if (!(styleAttributeName == "PaddingBottom"))
					{
						goto IL_0391;
					}
					break;
				}
				case 14:
				{
					char c = styleAttributeName[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "BorderWidthTop"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "BorderStyleTop"))
						{
							goto IL_0391;
						}
					}
					else if (!(styleAttributeName == "BorderColorTop"))
					{
						goto IL_0391;
					}
					break;
				}
				case 15:
				{
					char c = styleAttributeName[10];
					if (c <= 'I')
					{
						if (c != 'C')
						{
							if (c != 'I')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "BackgroundImage"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "BackgroundColor"))
						{
							goto IL_0391;
						}
					}
					else if (c != 'e')
					{
						if (c != 'h')
						{
							if (c != 'r')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "BorderColorLeft"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "BorderWidthLeft"))
						{
							goto IL_0391;
						}
					}
					else if (!(styleAttributeName == "BorderStyleLeft"))
					{
						goto IL_0391;
					}
					break;
				}
				case 16:
				{
					char c = styleAttributeName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "BorderStyleRight"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "BorderColorRight"))
						{
							goto IL_0391;
						}
					}
					else if (c != 'W')
					{
						if (c != 'o')
						{
							goto IL_0391;
						}
						if (!(styleAttributeName == "BackgroundRepeat"))
						{
							goto IL_0391;
						}
					}
					else if (!(styleAttributeName == "BorderWidthRight"))
					{
						goto IL_0391;
					}
					break;
				}
				case 17:
				{
					char c = styleAttributeName[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_0391;
							}
							if (!(styleAttributeName == "BorderWidthBottom"))
							{
								goto IL_0391;
							}
						}
						else if (!(styleAttributeName == "BorderStyleBottom"))
						{
							goto IL_0391;
						}
					}
					else if (!(styleAttributeName == "BorderColorBottom"))
					{
						goto IL_0391;
					}
					break;
				}
				case 18:
				case 19:
				case 22:
					goto IL_0391;
				case 20:
					if (!(styleAttributeName == "BackgroundImageValue"))
					{
						goto IL_0391;
					}
					break;
				case 21:
					if (!(styleAttributeName == "BackgroundImageSource"))
					{
						goto IL_0391;
					}
					break;
				case 23:
					if (!(styleAttributeName == "BackgroundImageMIMEType"))
					{
						goto IL_0391;
					}
					break;
				default:
					goto IL_0391;
				}
				return base.GetAttributeInfo(styleAttributeName, out styleAttribute);
			}
			IL_0391:
			styleAttribute = null;
			return false;
		}
	}
}
