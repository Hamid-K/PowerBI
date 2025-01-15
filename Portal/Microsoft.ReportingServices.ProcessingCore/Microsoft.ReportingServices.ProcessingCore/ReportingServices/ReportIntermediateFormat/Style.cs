using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000513 RID: 1299
	[Serializable]
	public class Style : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004526 RID: 17702 RVA: 0x00120900 File Offset: 0x0011EB00
		internal static string GetStyleString(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId styleId)
		{
			switch (styleId)
			{
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColor:
				return "BorderColor";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorTop:
				return "BorderColorTop";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorLeft:
				return "BorderColorLeft";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorRight:
				return "BorderColorRight";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorBottom:
				return "BorderColorBottom";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyle:
				return "BorderStyle";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleTop:
				return "BorderStyleTop";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleLeft:
				return "BorderStyleLeft";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleRight:
				return "BorderStyleRight";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleBottom:
				return "BorderStyleBottom";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidth:
				return "BorderWidth";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthTop:
				return "BorderWidthTop";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthLeft:
				return "BorderWidthLeft";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthRight:
				return "BorderWidthRight";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthBottom:
				return "BorderWidthBottom";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundColor:
				return "BackgroundColor";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontStyle:
				return "FontStyle";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontFamily:
				return "FontFamily";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontSize:
				return "FontSize";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontWeight:
				return "FontWeight";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Format:
				return "Format";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextDecoration:
				return "TextDecoration";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextAlign:
				return "TextAlign";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.VerticalAlign:
				return "VerticalAlign";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Color:
				return "Color";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingLeft:
				return "PaddingLeft";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingRight:
				return "PaddingRight";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingTop:
				return "PaddingTop";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingBottom:
				return "PaddingBottom";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.LineHeight:
				return "LineHeight";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Direction:
				return "Direction";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.WritingMode:
				return "WritingMode";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Language:
				return "Language";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.UnicodeBiDi:
				return "UnicodeBiDi";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Calendar:
				return "Calendar";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralLanguage:
				return "NumeralLanguage";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralVariant:
				return "NumeralVariant";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundGradientType:
				return "BackgroundGradientType";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundGradientEndColor:
				return "BackgroundGradientEndColor";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundHatchType:
				return "BackgroundHatchType";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TransparentColor:
				return "TransparentColor";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.ShadowColor:
				return "ShadowColor";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.ShadowOffset:
				return "ShadowOffset";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Position:
				return "Position";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextEffect:
				return "TextEffect";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImage:
				return "BackgroundImage";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageRepeat:
				return "BackgroundRepeat";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageSource:
				return "BackgroundImageSource";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageValue:
				return "BackgroundImageValue";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageMimeType:
				return "BackgroundImageMIMEType";
			case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.CurrencyLanguage:
				return "CurrencyLanguage";
			default:
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x00120B20 File Offset: 0x0011ED20
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId GetStyleId(string styleString)
		{
			if (styleString != null)
			{
				switch (styleString.Length)
				{
				case 5:
					if (styleString == "Color")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Color;
					}
					break;
				case 6:
					if (styleString == "Format")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Format;
					}
					break;
				case 8:
				{
					char c = styleString[0];
					if (c != 'C')
					{
						if (c != 'F')
						{
							if (c == 'L')
							{
								if (styleString == "Language")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Language;
								}
							}
						}
						else if (styleString == "FontSize")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontSize;
						}
					}
					else if (styleString == "Calendar")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Calendar;
					}
					break;
				}
				case 9:
				{
					char c = styleString[0];
					if (c != 'D')
					{
						if (c != 'F')
						{
							if (c == 'T')
							{
								if (styleString == "TextAlign")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextAlign;
								}
							}
						}
						else if (styleString == "FontStyle")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontStyle;
						}
					}
					else if (styleString == "Direction")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Direction;
					}
					break;
				}
				case 10:
				{
					char c = styleString[4];
					switch (c)
					{
					case 'E':
						if (styleString == "TextEffect")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextEffect;
						}
						break;
					case 'F':
						if (styleString == "FontFamily")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontFamily;
						}
						break;
					case 'G':
						break;
					case 'H':
						if (styleString == "LineHeight")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.LineHeight;
						}
						break;
					default:
						if (c != 'W')
						{
							if (c == 'i')
							{
								if (styleString == "PaddingTop")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingTop;
								}
							}
						}
						else if (styleString == "FontWeight")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontWeight;
						}
						break;
					}
					break;
				}
				case 11:
				{
					char c = styleString[7];
					if (c <= 'M')
					{
						if (c != 'B')
						{
							if (c != 'L')
							{
								if (c == 'M')
								{
									if (styleString == "WritingMode")
									{
										return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.WritingMode;
									}
								}
							}
							else if (styleString == "PaddingLeft")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingLeft;
							}
						}
						else if (styleString == "UnicodeBiDi")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.UnicodeBiDi;
						}
					}
					else if (c != 'i')
					{
						if (c != 'o')
						{
							if (c == 't')
							{
								if (styleString == "BorderStyle")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyle;
								}
							}
						}
						else
						{
							if (styleString == "BorderColor")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColor;
							}
							if (styleString == "ShadowColor")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.ShadowColor;
							}
						}
					}
					else if (styleString == "BorderWidth")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidth;
					}
					break;
				}
				case 12:
				{
					char c = styleString[0];
					if (c != 'P')
					{
						if (c == 'S')
						{
							if (styleString == "ShadowOffset")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.ShadowOffset;
							}
						}
					}
					else if (styleString == "PaddingRight")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingRight;
					}
					break;
				}
				case 13:
				{
					char c = styleString[0];
					if (c != 'P')
					{
						if (c == 'V')
						{
							if (styleString == "VerticalAlign")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.VerticalAlign;
							}
						}
					}
					else if (styleString == "PaddingBottom")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingBottom;
					}
					break;
				}
				case 14:
				{
					char c = styleString[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c == 'S')
							{
								if (styleString == "BorderStyleTop")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleTop;
								}
							}
						}
						else if (styleString == "BorderColorTop")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorTop;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c == 'l')
							{
								if (styleString == "NumeralVariant")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralVariant;
								}
							}
						}
						else if (styleString == "TextDecoration")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextDecoration;
						}
					}
					else if (styleString == "BorderWidthTop")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthTop;
					}
					break;
				}
				case 15:
				{
					char c = styleString[10];
					if (c <= 'I')
					{
						if (c != 'C')
						{
							if (c == 'I')
							{
								if (styleString == "BackgroundImage")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImage;
								}
							}
						}
						else if (styleString == "BackgroundColor")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundColor;
						}
					}
					else
					{
						switch (c)
						{
						case 'e':
							if (styleString == "BorderStyleLeft")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleLeft;
							}
							break;
						case 'f':
							break;
						case 'g':
							if (styleString == "NumeralLanguage")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralLanguage;
							}
							break;
						case 'h':
							if (styleString == "BorderWidthLeft")
							{
								return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthLeft;
							}
							break;
						default:
							if (c == 'r')
							{
								if (styleString == "BorderColorLeft")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorLeft;
								}
							}
							break;
						}
					}
					break;
				}
				case 16:
				{
					char c = styleString[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c == 'S')
							{
								if (styleString == "BorderStyleRight")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleRight;
								}
							}
						}
						else if (styleString == "BorderColorRight")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorRight;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c == 'o')
							{
								if (styleString == "BackgroundRepeat")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageRepeat;
								}
							}
						}
						else if (styleString == "CurrencyLanguage")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.CurrencyLanguage;
						}
					}
					else if (styleString == "BorderWidthRight")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthRight;
					}
					break;
				}
				case 17:
				{
					char c = styleString[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c == 'W')
							{
								if (styleString == "BorderWidthBottom")
								{
									return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthBottom;
								}
							}
						}
						else if (styleString == "BorderStyleBottom")
						{
							return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleBottom;
						}
					}
					else if (styleString == "BorderColorBottom")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorBottom;
					}
					break;
				}
				case 19:
					if (styleString == "BackgroundHatchType")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundHatchType;
					}
					break;
				case 20:
					if (styleString == "BackgroundImageValue")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageValue;
					}
					break;
				case 21:
					if (styleString == "BackgroundImageSource")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageSource;
					}
					break;
				case 22:
					if (styleString == "BackgroundGradientType")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundGradientType;
					}
					break;
				case 23:
					if (styleString == "BackgroundImageMIMEType")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageMimeType;
					}
					break;
				case 26:
					if (styleString == "BackgroundGradientEndColor")
					{
						return Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundGradientEndColor;
					}
					break;
				}
			}
			throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x00121220 File Offset: 0x0011F420
		internal Style(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_styleAttributes = new Dictionary<string, AttributeInfo>();
			}
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x0012123D File Offset: 0x0011F43D
		// (set) Token: 0x0600452A RID: 17706 RVA: 0x00121245 File Offset: 0x0011F445
		internal Dictionary<string, AttributeInfo> StyleAttributes
		{
			get
			{
				return this.m_styleAttributes;
			}
			set
			{
				this.m_styleAttributes = value;
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0012124E File Offset: 0x0011F44E
		// (set) Token: 0x0600452C RID: 17708 RVA: 0x00121256 File Offset: 0x0011F456
		internal List<ExpressionInfo> ExpressionList
		{
			get
			{
				return this.m_expressionList;
			}
			set
			{
				this.m_expressionList = value;
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x0012125F File Offset: 0x0011F45F
		internal StyleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x00121267 File Offset: 0x0011F467
		// (set) Token: 0x0600452F RID: 17711 RVA: 0x0012126F File Offset: 0x0011F46F
		internal int CustomSharedStyleCount
		{
			get
			{
				return this.m_customSharedStyleCount;
			}
			set
			{
				this.m_customSharedStyleCount = value;
			}
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x00121278 File Offset: 0x0011F478
		internal void SetStyleExprHost(StyleExprHost exprHost)
		{
			Global.Tracer.Assert(exprHost != null, "(null != exprHost)");
			this.m_exprHost = exprHost;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x00121294 File Offset: 0x0011F494
		internal int GetStyleAttribute(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string styleAttributeName, OnDemandProcessingContext context, ref bool sharedFormatSettings, out string styleStringValue)
		{
			styleStringValue = null;
			int num = 0;
			object obj = null;
			AttributeInfo attributeInfo = null;
			if (this.GetAttributeInfo(styleAttributeName, out attributeInfo))
			{
				if (attributeInfo.IsExpression)
				{
					num = 1;
					sharedFormatSettings = false;
					obj = this.EvaluateStyle(objectType, objectName, styleAttributeName, context);
				}
				else
				{
					num = 2;
					obj = attributeInfo.Value;
				}
			}
			if (obj != null)
			{
				styleStringValue = (string)obj;
			}
			return num;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x001212E8 File Offset: 0x0011F4E8
		internal void GetStyleAttribute(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string styleAttributeName, OnDemandProcessingContext context, ref bool sharedFormatSettings, out int styleIntValue)
		{
			styleIntValue = 0;
			AttributeInfo attributeInfo = null;
			if (this.GetAttributeInfo(styleAttributeName, out attributeInfo))
			{
				if (attributeInfo.IsExpression)
				{
					sharedFormatSettings = false;
					object obj = this.EvaluateStyle(objectType, objectName, styleAttributeName, context);
					if (obj != null)
					{
						styleIntValue = (int)obj;
						return;
					}
				}
				else
				{
					styleIntValue = attributeInfo.IntValue;
				}
			}
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x00121334 File Offset: 0x0011F534
		internal virtual bool GetAttributeInfo(string styleAttributeName, out AttributeInfo styleAttribute)
		{
			return this.m_styleAttributes.TryGetValue(styleAttributeName, out styleAttribute) && styleAttribute != null;
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0012134C File Offset: 0x0011F54C
		internal object EvaluateStyle(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId styleId, OnDemandProcessingContext context)
		{
			AttributeInfo attributeInfo = null;
			if (this.GetAttributeInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Style.GetStyleString(styleId), out attributeInfo))
			{
				return this.EvaluateStyle(objectType, objectName, attributeInfo, styleId, context);
			}
			return null;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x0012137C File Offset: 0x0011F57C
		internal object EvaluateStyle(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string styleAttributeName, OnDemandProcessingContext context)
		{
			AttributeInfo attributeInfo = null;
			if (this.GetAttributeInfo(styleAttributeName, out attributeInfo))
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId styleId = Microsoft.ReportingServices.ReportIntermediateFormat.Style.GetStyleId(styleAttributeName);
				return this.EvaluateStyle(objectType, objectName, attributeInfo, styleId, context);
			}
			return null;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x001213AC File Offset: 0x0011F5AC
		internal object EvaluateStyle(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, AttributeInfo attribute, Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId styleId, OnDemandProcessingContext context)
		{
			if (attribute != null)
			{
				if (!attribute.IsExpression)
				{
					if (Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralLanguage == styleId || Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageSource == styleId)
					{
						return attribute.IntValue;
					}
					if (attribute.Value != null && attribute.Value.Length != 0)
					{
						return attribute.Value;
					}
					return null;
				}
				else
				{
					switch (styleId)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColor:
						return context.ReportRuntime.EvaluateStyleBorderColor(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorTop:
						return context.ReportRuntime.EvaluateStyleBorderColorTop(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorLeft:
						return context.ReportRuntime.EvaluateStyleBorderColorLeft(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorRight:
						return context.ReportRuntime.EvaluateStyleBorderColorRight(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderColorBottom:
						return context.ReportRuntime.EvaluateStyleBorderColorBottom(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyle:
						return context.ReportRuntime.EvaluateStyleBorderStyle(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleTop:
						return context.ReportRuntime.EvaluateStyleBorderStyleTop(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleLeft:
						return context.ReportRuntime.EvaluateStyleBorderStyleLeft(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleRight:
						return context.ReportRuntime.EvaluateStyleBorderStyleRight(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderStyleBottom:
						return context.ReportRuntime.EvaluateStyleBorderStyleBottom(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidth:
						return context.ReportRuntime.EvaluateStyleBorderWidth(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthTop:
						return context.ReportRuntime.EvaluateStyleBorderWidthTop(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthLeft:
						return context.ReportRuntime.EvaluateStyleBorderWidthLeft(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthRight:
						return context.ReportRuntime.EvaluateStyleBorderWidthRight(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BorderWidthBottom:
						return context.ReportRuntime.EvaluateStyleBorderWidthBottom(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundColor:
						return context.ReportRuntime.EvaluateStyleBackgroundColor(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontStyle:
						return context.ReportRuntime.EvaluateStyleFontStyle(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontFamily:
						return context.ReportRuntime.EvaluateStyleFontFamily(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontSize:
						return context.ReportRuntime.EvaluateStyleFontSize(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.FontWeight:
						return context.ReportRuntime.EvaluateStyleFontWeight(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Format:
						return context.ReportRuntime.EvaluateStyleFormat(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextDecoration:
						return context.ReportRuntime.EvaluateStyleTextDecoration(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextAlign:
						return context.ReportRuntime.EvaluateStyleTextAlign(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.VerticalAlign:
						return context.ReportRuntime.EvaluateStyleVerticalAlign(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Color:
						return context.ReportRuntime.EvaluateStyleColor(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingLeft:
						return context.ReportRuntime.EvaluateStylePaddingLeft(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingRight:
						return context.ReportRuntime.EvaluateStylePaddingRight(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingTop:
						return context.ReportRuntime.EvaluateStylePaddingTop(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.PaddingBottom:
						return context.ReportRuntime.EvaluateStylePaddingBottom(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.LineHeight:
						return context.ReportRuntime.EvaluateStyleLineHeight(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Direction:
						return context.ReportRuntime.EvaluateStyleDirection(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.WritingMode:
						return context.ReportRuntime.EvaluateStyleWritingMode(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Language:
						return context.ReportRuntime.EvaluateStyleLanguage(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.UnicodeBiDi:
						return context.ReportRuntime.EvaluateStyleUnicodeBiDi(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Calendar:
						return context.ReportRuntime.EvaluateStyleCalendar(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralLanguage:
						return context.ReportRuntime.EvaluateStyleNumeralLanguage(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.NumeralVariant:
						return context.ReportRuntime.EvaluateStyleNumeralVariant(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundGradientType:
						return context.ReportRuntime.EvaluateStyleBackgroundGradientType(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundGradientEndColor:
						return context.ReportRuntime.EvaluateStyleBackgroundGradientEndColor(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundHatchType:
						return context.ReportRuntime.EvaluateStyleBackgroundHatchType(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TransparentColor:
						return context.ReportRuntime.EvaluateTransparentColor(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.ShadowColor:
						return context.ReportRuntime.EvaluateStyleShadowColor(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.ShadowOffset:
						return context.ReportRuntime.EvaluateStyleShadowOffset(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Position:
						return context.ReportRuntime.EvaluatePosition(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.TextEffect:
						return context.ReportRuntime.EvaluateStyleTextEffect(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageRepeat:
						return context.ReportRuntime.EvaluateStyleBackgroundRepeat(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageSource:
						return null;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageValue:
					{
						AttributeInfo attributeInfo = this.m_styleAttributes["BackgroundImageSource"];
						if (attributeInfo == null)
						{
							return null;
						}
						switch (attributeInfo.IntValue)
						{
						case 0:
							return context.ReportRuntime.EvaluateStyleBackgroundUrlImageValue(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
						case 1:
							return context.ReportRuntime.EvaluateStyleBackgroundEmbeddedImageValue(this, this.m_expressionList[attribute.IntValue], context.EmbeddedImages, objectType, objectName);
						case 2:
							return context.ReportRuntime.EvaluateStyleBackgroundDatabaseImageValue(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
						}
						break;
					}
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageMimeType:
						return context.ReportRuntime.EvaluateStyleBackgroundImageMIMEType(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					case Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.CurrencyLanguage:
						return context.ReportRuntime.EvaluateStyleCurrencyLanguage(this, this.m_expressionList[attribute.IntValue], objectType, objectName);
					}
				}
			}
			return null;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x00121BF6 File Offset: 0x0011FDF6
		internal void AddAttribute(string name, ExpressionInfo expressionInfo)
		{
			this.AddAttribute(name, expressionInfo, ValueType.Constant);
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00121C01 File Offset: 0x0011FE01
		internal void AddAttribute(StyleInformation.StyleInformationAttribute attribute)
		{
			this.AddAttribute(attribute.Name, attribute.Value, attribute.ValueType);
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x00121C1C File Offset: 0x0011FE1C
		internal void AddAttribute(string name, ExpressionInfo expressionInfo, ValueType valueType)
		{
			AttributeInfo attributeInfo = new AttributeInfo();
			attributeInfo.ValueType = valueType;
			attributeInfo.IsExpression = ExpressionInfo.Types.Constant != expressionInfo.Type;
			if (attributeInfo.IsExpression)
			{
				if (this.m_expressionList == null)
				{
					this.m_expressionList = new List<ExpressionInfo>();
				}
				this.m_expressionList.Add(expressionInfo);
				attributeInfo.IntValue = this.m_expressionList.Count - 1;
			}
			else
			{
				attributeInfo.Value = expressionInfo.StringValue;
				attributeInfo.BoolValue = expressionInfo.BoolValue;
				attributeInfo.IntValue = expressionInfo.IntValue;
				attributeInfo.FloatValue = expressionInfo.FloatValue;
			}
			Global.Tracer.Assert(this.m_styleAttributes != null, "(null != m_styleAttributes)");
			this.m_styleAttributes.Add(name, attributeInfo);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x00121CDC File Offset: 0x0011FEDC
		internal void Initialize(InitializationContext context)
		{
			Global.Tracer.Assert(this.m_styleAttributes != null, "(null != m_styleAttributes)");
			IDictionaryEnumerator dictionaryEnumerator = this.m_styleAttributes.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				string text = (string)dictionaryEnumerator.Key;
				AttributeInfo attributeInfo = (AttributeInfo)dictionaryEnumerator.Value;
				Global.Tracer.Assert(text != null, "(null != name)");
				Global.Tracer.Assert(attributeInfo != null, "(null != attribute)");
				if (attributeInfo.IsExpression)
				{
					string text2 = text;
					if (text != null)
					{
						switch (text.Length)
						{
						case 14:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_022A;
									}
									if (!(text == "BorderWidthTop"))
									{
										goto IL_022A;
									}
									goto IL_0224;
								}
								else
								{
									if (!(text == "BorderStyleTop"))
									{
										goto IL_022A;
									}
									goto IL_021C;
								}
							}
							else if (!(text == "BorderColorTop"))
							{
								goto IL_022A;
							}
							break;
						}
						case 15:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_022A;
									}
									if (!(text == "BorderWidthLeft"))
									{
										goto IL_022A;
									}
									goto IL_0224;
								}
								else
								{
									if (!(text == "BorderStyleLeft"))
									{
										goto IL_022A;
									}
									goto IL_021C;
								}
							}
							else if (!(text == "BorderColorLeft"))
							{
								goto IL_022A;
							}
							break;
						}
						case 16:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_022A;
									}
									if (!(text == "BorderWidthRight"))
									{
										goto IL_022A;
									}
									goto IL_0224;
								}
								else
								{
									if (!(text == "BorderStyleRight"))
									{
										goto IL_022A;
									}
									goto IL_021C;
								}
							}
							else if (!(text == "BorderColorRight"))
							{
								goto IL_022A;
							}
							break;
						}
						case 17:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_022A;
									}
									if (!(text == "BorderWidthBottom"))
									{
										goto IL_022A;
									}
									goto IL_0224;
								}
								else
								{
									if (!(text == "BorderStyleBottom"))
									{
										goto IL_022A;
									}
									goto IL_021C;
								}
							}
							else if (!(text == "BorderColorBottom"))
							{
								goto IL_022A;
							}
							break;
						}
						default:
							goto IL_022A;
						}
						text = "BorderColor";
						goto IL_022A;
						IL_021C:
						text = "BorderStyle";
						goto IL_022A;
						IL_0224:
						text = "BorderWidth";
					}
					IL_022A:
					Global.Tracer.Assert(this.m_expressionList != null, "(null != m_expressionList)");
					ExpressionInfo expressionInfo = this.m_expressionList[attributeInfo.IntValue];
					expressionInfo.Initialize(text, context);
					context.ExprHostBuilder.StyleAttribute(text2, expressionInfo);
				}
			}
			AttributeInfo attributeInfo2;
			this.m_styleAttributes.TryGetValue("BackgroundImageSource", out attributeInfo2);
			if (attributeInfo2 != null)
			{
				Global.Tracer.Assert(!attributeInfo2.IsExpression, "(!source.IsExpression)");
				Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType intValue = (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType)attributeInfo2.IntValue;
				AttributeInfo attributeInfo3;
				if (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Embedded == intValue && (!this.m_styleAttributes.TryGetValue("EmbeddingMode", out attributeInfo3) || attributeInfo3.IntValue != 1))
				{
					AttributeInfo attributeInfo4 = this.m_styleAttributes["BackgroundImageValue"];
					Global.Tracer.Assert(attributeInfo4 != null, "(null != embeddedImageName)");
					Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateEmbeddedImageName(attributeInfo4, context.EmbeddedImages, context.ObjectType, context.ObjectName, "BackgroundImageValue", context.ErrorContext);
				}
			}
			context.CheckInternationalSettings(this.m_styleAttributes);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x0012201C File Offset: 0x0012021C
		public object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Style style = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)base.MemberwiseClone();
			if (this.m_styleAttributes != null)
			{
				style.m_styleAttributes = new Dictionary<string, AttributeInfo>(this.m_styleAttributes.Count);
				foreach (KeyValuePair<string, AttributeInfo> keyValuePair in this.m_styleAttributes)
				{
					style.m_styleAttributes.Add(keyValuePair.Key, keyValuePair.Value.PublishClone(context));
				}
			}
			if (this.m_expressionList != null)
			{
				style.m_expressionList = new List<ExpressionInfo>(this.m_expressionList.Count);
				foreach (ExpressionInfo expressionInfo in this.m_expressionList)
				{
					style.m_expressionList.Add((ExpressionInfo)expressionInfo.PublishClone(context));
				}
			}
			return style;
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x00122124 File Offset: 0x00120324
		internal void InitializeForCRIGeneratedReportItem()
		{
			this.SetStyleExprHost(Microsoft.ReportingServices.ReportIntermediateFormat.Style.EmptyStyleExprHost.Instance);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x00122134 File Offset: 0x00120334
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StyleAttributes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AttributeInfo),
				new MemberInfo(MemberName.ExpressionList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x00122180 File Offset: 0x00120380
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Style.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StyleAttributes)
				{
					if (memberName != MemberName.ExpressionList)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write<ExpressionInfo>(this.m_expressionList);
					}
				}
				else
				{
					writer.WriteStringRIFObjectDictionary<AttributeInfo>(this.m_styleAttributes);
				}
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x001221EC File Offset: 0x001203EC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Style.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StyleAttributes)
				{
					if (memberName != MemberName.ExpressionList)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_expressionList = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
					}
				}
				else
				{
					this.m_styleAttributes = reader.ReadStringRIFObjectDictionary<AttributeInfo>();
				}
			}
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x00122258 File Offset: 0x00120458
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x00122265 File Offset: 0x00120465
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style;
		}

		// Token: 0x04001F36 RID: 7990
		protected Dictionary<string, AttributeInfo> m_styleAttributes;

		// Token: 0x04001F37 RID: 7991
		protected List<ExpressionInfo> m_expressionList;

		// Token: 0x04001F38 RID: 7992
		[NonSerialized]
		private StyleExprHost m_exprHost;

		// Token: 0x04001F39 RID: 7993
		[NonSerialized]
		private int m_customSharedStyleCount = -1;

		// Token: 0x04001F3A RID: 7994
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Style.GetDeclaration();

		// Token: 0x02000981 RID: 2433
		internal enum StyleId
		{
			// Token: 0x04004111 RID: 16657
			BorderColor,
			// Token: 0x04004112 RID: 16658
			BorderColorTop,
			// Token: 0x04004113 RID: 16659
			BorderColorLeft,
			// Token: 0x04004114 RID: 16660
			BorderColorRight,
			// Token: 0x04004115 RID: 16661
			BorderColorBottom,
			// Token: 0x04004116 RID: 16662
			BorderStyle,
			// Token: 0x04004117 RID: 16663
			BorderStyleTop,
			// Token: 0x04004118 RID: 16664
			BorderStyleLeft,
			// Token: 0x04004119 RID: 16665
			BorderStyleRight,
			// Token: 0x0400411A RID: 16666
			BorderStyleBottom,
			// Token: 0x0400411B RID: 16667
			BorderWidth,
			// Token: 0x0400411C RID: 16668
			BorderWidthTop,
			// Token: 0x0400411D RID: 16669
			BorderWidthLeft,
			// Token: 0x0400411E RID: 16670
			BorderWidthRight,
			// Token: 0x0400411F RID: 16671
			BorderWidthBottom,
			// Token: 0x04004120 RID: 16672
			BackgroundColor,
			// Token: 0x04004121 RID: 16673
			FontStyle,
			// Token: 0x04004122 RID: 16674
			FontFamily,
			// Token: 0x04004123 RID: 16675
			FontSize,
			// Token: 0x04004124 RID: 16676
			FontWeight,
			// Token: 0x04004125 RID: 16677
			Format,
			// Token: 0x04004126 RID: 16678
			TextDecoration,
			// Token: 0x04004127 RID: 16679
			TextAlign,
			// Token: 0x04004128 RID: 16680
			VerticalAlign,
			// Token: 0x04004129 RID: 16681
			Color,
			// Token: 0x0400412A RID: 16682
			PaddingLeft,
			// Token: 0x0400412B RID: 16683
			PaddingRight,
			// Token: 0x0400412C RID: 16684
			PaddingTop,
			// Token: 0x0400412D RID: 16685
			PaddingBottom,
			// Token: 0x0400412E RID: 16686
			LineHeight,
			// Token: 0x0400412F RID: 16687
			Direction,
			// Token: 0x04004130 RID: 16688
			WritingMode,
			// Token: 0x04004131 RID: 16689
			Language,
			// Token: 0x04004132 RID: 16690
			UnicodeBiDi,
			// Token: 0x04004133 RID: 16691
			Calendar,
			// Token: 0x04004134 RID: 16692
			NumeralLanguage,
			// Token: 0x04004135 RID: 16693
			NumeralVariant,
			// Token: 0x04004136 RID: 16694
			BackgroundGradientType,
			// Token: 0x04004137 RID: 16695
			BackgroundGradientEndColor,
			// Token: 0x04004138 RID: 16696
			BackgroundHatchType,
			// Token: 0x04004139 RID: 16697
			TransparentColor,
			// Token: 0x0400413A RID: 16698
			ShadowColor,
			// Token: 0x0400413B RID: 16699
			ShadowOffset,
			// Token: 0x0400413C RID: 16700
			Position,
			// Token: 0x0400413D RID: 16701
			TextEffect,
			// Token: 0x0400413E RID: 16702
			BackgroundImage,
			// Token: 0x0400413F RID: 16703
			BackgroundImageRepeat,
			// Token: 0x04004140 RID: 16704
			BackgroundImageSource,
			// Token: 0x04004141 RID: 16705
			BackgroundImageValue,
			// Token: 0x04004142 RID: 16706
			BackgroundImageMimeType,
			// Token: 0x04004143 RID: 16707
			CurrencyLanguage
		}

		// Token: 0x02000982 RID: 2434
		private sealed class EmptyStyleExprHost : StyleExprHost
		{
			// Token: 0x06008081 RID: 32897 RVA: 0x00211334 File Offset: 0x0020F534
			private EmptyStyleExprHost()
			{
			}

			// Token: 0x04004144 RID: 16708
			internal static Microsoft.ReportingServices.ReportIntermediateFormat.Style.EmptyStyleExprHost Instance = new Microsoft.ReportingServices.ReportIntermediateFormat.Style.EmptyStyleExprHost();
		}
	}
}
