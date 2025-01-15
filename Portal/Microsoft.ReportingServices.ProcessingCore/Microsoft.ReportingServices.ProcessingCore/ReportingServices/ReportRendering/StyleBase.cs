using System;
using System.Collections;
using System.Security.Permissions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000020 RID: 32
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class StyleBase
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00009E3F File Offset: 0x0000803F
		protected StyleBase()
		{
			this.m_isCustomControlGenerated = true;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00009E4E File Offset: 0x0000804E
		internal StyleBase(Microsoft.ReportingServices.ReportRendering.RenderingContext context)
		{
			this.m_isCustomControlGenerated = false;
			this.m_renderingContext = context;
		}

		// Token: 0x17000352 RID: 850
		public object this[int index]
		{
			get
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				this.PopulateStyleProperties(true);
				int num = 0;
				if (this.m_sharedProperties != null)
				{
					num = this.m_sharedProperties.Count;
				}
				if (index < num)
				{
					return this.m_sharedProperties[index];
				}
				Global.Tracer.Assert(this.m_nonSharedProperties != null);
				return this.m_nonSharedProperties[index - num];
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00009F00 File Offset: 0x00008100
		public virtual int Count
		{
			get
			{
				this.PopulateStyleProperties(true);
				int num = 0;
				if (this.m_sharedProperties != null)
				{
					num += this.m_sharedProperties.Count;
				}
				if (this.m_nonSharedProperties != null)
				{
					num += this.m_nonSharedProperties.Count;
				}
				return num;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00009F44 File Offset: 0x00008144
		public virtual ICollection Keys
		{
			get
			{
				string[] array = new string[this.Count];
				if (this.m_sharedProperties != null)
				{
					this.m_sharedProperties.Keys.CopyTo(array, 0);
				}
				if (this.m_nonSharedProperties != null)
				{
					this.m_nonSharedProperties.Keys.CopyTo(array, this.m_sharedProperties.Count);
				}
				return array;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00009F9C File Offset: 0x0000819C
		public bool ContainStyleProperty(string styleName)
		{
			bool flag = false;
			if (this.Count == 0)
			{
				return flag;
			}
			if (this.m_sharedProperties != null)
			{
				flag = this.m_sharedProperties.ContainStyleProperty(styleName);
			}
			if (!flag && this.m_nonSharedProperties != null)
			{
				flag = this.m_nonSharedProperties.ContainStyleProperty(styleName);
			}
			return flag;
		}

		// Token: 0x17000355 RID: 853
		public abstract object this[string styleName] { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00009FE3 File Offset: 0x000081E3
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00009FEB File Offset: 0x000081EB
		public virtual StyleProperties SharedProperties
		{
			get
			{
				return this.m_sharedProperties;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_nonSharedProperties = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000A007 File Offset: 0x00008207
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000A00F File Offset: 0x0000820F
		public virtual StyleProperties NonSharedProperties
		{
			get
			{
				return this.m_nonSharedProperties;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_nonSharedProperties = value;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000A02B File Offset: 0x0000822B
		public IEnumerator GetEnumerator()
		{
			this.PopulateStyleProperties(true);
			return new StyleEnumerator(this.m_sharedProperties, this.m_nonSharedProperties);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000A048 File Offset: 0x00008248
		internal static object GetStyleValueBase(string styleName, Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			if (styleDef != null)
			{
				AttributeInfo attributeInfo = styleDef.StyleAttributes[styleName];
				if (attributeInfo != null)
				{
					object obj;
					if (attributeInfo.IsExpression)
					{
						if (styleAttributeValues != null)
						{
							Global.Tracer.Assert(0 <= attributeInfo.IntValue && attributeInfo.IntValue < styleAttributeValues.Length);
							obj = styleAttributeValues[attributeInfo.IntValue];
						}
						else
						{
							obj = null;
						}
					}
					else if ("NumeralVariant" == styleName)
					{
						obj = attributeInfo.IntValue;
					}
					else
					{
						obj = attributeInfo.Value;
					}
					if (obj != null)
					{
						return StyleBase.CreateStyleProperty(styleName, obj);
					}
				}
			}
			return null;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000A0D4 File Offset: 0x000082D4
		internal static object CreateStyleProperty(string styleName, object styleValue)
		{
			if (styleName != null)
			{
				switch (styleName.Length)
				{
				case 5:
					if (!(styleName == "Color"))
					{
						goto IL_056A;
					}
					break;
				case 6:
					if (!(styleName == "Format"))
					{
						goto IL_056A;
					}
					return styleValue;
				case 7:
				case 18:
				case 19:
				case 20:
				case 21:
				case 23:
				case 24:
				case 25:
					goto IL_056A;
				case 8:
				{
					char c = styleName[0];
					if (c != 'C')
					{
						if (c != 'F')
						{
							if (c != 'L')
							{
								goto IL_056A;
							}
							if (!(styleName == "Language"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else
						{
							if (!(styleName == "FontSize"))
							{
								goto IL_056A;
							}
							goto IL_055B;
						}
					}
					else
					{
						if (!(styleName == "Calendar"))
						{
							goto IL_056A;
						}
						return styleValue;
					}
					break;
				}
				case 9:
				{
					char c = styleName[0];
					if (c != 'D')
					{
						if (c != 'F')
						{
							if (c != 'T')
							{
								goto IL_056A;
							}
							if (!(styleName == "TextAlign"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else
						{
							if (!(styleName == "FontStyle"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
					}
					else
					{
						if (!(styleName == "Direction"))
						{
							goto IL_056A;
						}
						return styleValue;
					}
					break;
				}
				case 10:
				{
					char c = styleName[4];
					if (c <= 'H')
					{
						if (c != 'F')
						{
							if (c != 'H')
							{
								goto IL_056A;
							}
							if (!(styleName == "LineHeight"))
							{
								goto IL_056A;
							}
							goto IL_055B;
						}
						else
						{
							if (!(styleName == "FontFamily"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
					}
					else if (c != 'W')
					{
						if (c != 'i')
						{
							goto IL_056A;
						}
						if (!(styleName == "PaddingTop"))
						{
							goto IL_056A;
						}
						goto IL_055B;
					}
					else
					{
						if (!(styleName == "FontWeight"))
						{
							goto IL_056A;
						}
						return styleValue;
					}
					break;
				}
				case 11:
				{
					char c = styleName[7];
					if (c <= 'M')
					{
						if (c != 'B')
						{
							if (c != 'L')
							{
								if (c != 'M')
								{
									goto IL_056A;
								}
								if (!(styleName == "WritingMode"))
								{
									goto IL_056A;
								}
								return styleValue;
							}
							else
							{
								if (!(styleName == "PaddingLeft"))
								{
									goto IL_056A;
								}
								goto IL_055B;
							}
						}
						else
						{
							if (!(styleName == "UnicodeBiDi"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
					}
					else if (c != 'i')
					{
						if (c != 'o')
						{
							if (c != 't')
							{
								goto IL_056A;
							}
							if (!(styleName == "BorderStyle"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else if (!(styleName == "BorderColor"))
						{
							goto IL_056A;
						}
					}
					else
					{
						if (!(styleName == "BorderWidth"))
						{
							goto IL_056A;
						}
						goto IL_055B;
					}
					break;
				}
				case 12:
					if (!(styleName == "PaddingRight"))
					{
						goto IL_056A;
					}
					goto IL_055B;
				case 13:
				{
					char c = styleName[0];
					if (c != 'P')
					{
						if (c != 'V')
						{
							goto IL_056A;
						}
						if (!(styleName == "VerticalAlign"))
						{
							goto IL_056A;
						}
						return styleValue;
					}
					else
					{
						if (!(styleName == "PaddingBottom"))
						{
							goto IL_056A;
						}
						goto IL_055B;
					}
					break;
				}
				case 14:
				{
					char c = styleName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_056A;
							}
							if (!(styleName == "BorderStyleTop"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else if (!(styleName == "BorderColorTop"))
						{
							goto IL_056A;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_056A;
							}
							if (!(styleName == "NumeralVariant"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else
						{
							if (!(styleName == "TextDecoration"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
					}
					else
					{
						if (!(styleName == "BorderWidthTop"))
						{
							goto IL_056A;
						}
						goto IL_055B;
					}
					break;
				}
				case 15:
				{
					char c = styleName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_056A;
							}
							if (!(styleName == "BorderStyleLeft"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else if (!(styleName == "BorderColorLeft"))
						{
							goto IL_056A;
						}
					}
					else if (c != 'W')
					{
						if (c != 'l')
						{
							if (c != 'o')
							{
								goto IL_056A;
							}
							if (!(styleName == "BackgroundColor"))
							{
								goto IL_056A;
							}
						}
						else
						{
							if (!(styleName == "NumeralLanguage"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
					}
					else
					{
						if (!(styleName == "BorderWidthLeft"))
						{
							goto IL_056A;
						}
						goto IL_055B;
					}
					break;
				}
				case 16:
				{
					char c = styleName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_056A;
							}
							if (!(styleName == "BorderStyleRight"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
						else if (!(styleName == "BorderColorRight"))
						{
							goto IL_056A;
						}
					}
					else if (c != 'W')
					{
						if (c != 'o')
						{
							goto IL_056A;
						}
						if (!(styleName == "BackgroundRepeat"))
						{
							goto IL_056A;
						}
						return styleValue;
					}
					else
					{
						if (!(styleName == "BorderWidthRight"))
						{
							goto IL_056A;
						}
						goto IL_055B;
					}
					break;
				}
				case 17:
				{
					char c = styleName[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_056A;
							}
							if (!(styleName == "BorderWidthBottom"))
							{
								goto IL_056A;
							}
							goto IL_055B;
						}
						else
						{
							if (!(styleName == "BorderStyleBottom"))
							{
								goto IL_056A;
							}
							return styleValue;
						}
					}
					else if (!(styleName == "BorderColorBottom"))
					{
						goto IL_056A;
					}
					break;
				}
				case 22:
					if (!(styleName == "BackgroundGradientType"))
					{
						goto IL_056A;
					}
					return styleValue;
				case 26:
					if (!(styleName == "BackgroundGradientEndColor"))
					{
						goto IL_056A;
					}
					break;
				default:
					goto IL_056A;
				}
				return new ReportColor((string)styleValue, false);
				IL_055B:
				return new ReportSize((string)styleValue, false);
			}
			IL_056A:
			return null;
		}

		// Token: 0x060003E5 RID: 997
		internal abstract object GetStyleAttributeValue(string styleName, AttributeInfo attribute);

		// Token: 0x060003E6 RID: 998 RVA: 0x0000A64C File Offset: 0x0000884C
		internal bool GetBackgroundImageSource(AttributeInfo sourceAttribute, out Microsoft.ReportingServices.ReportRendering.Image.SourceType imageSource)
		{
			if (sourceAttribute == null)
			{
				imageSource = Microsoft.ReportingServices.ReportRendering.Image.SourceType.External;
				return false;
			}
			Global.Tracer.Assert(!sourceAttribute.IsExpression);
			imageSource = (Microsoft.ReportingServices.ReportRendering.Image.SourceType)sourceAttribute.IntValue;
			return true;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A672 File Offset: 0x00008872
		internal bool GetBackgroundImageValue(AttributeInfo valueAttribute, out object imageValue, out bool isExpression)
		{
			if (valueAttribute == null)
			{
				imageValue = null;
				isExpression = false;
				return false;
			}
			imageValue = this.GetStyleAttributeValue("BackgroundImageValue", valueAttribute);
			isExpression = valueAttribute.IsExpression;
			return true;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A696 File Offset: 0x00008896
		internal bool GetBackgroundImageMIMEType(AttributeInfo mimeTypeAttribute, out object mimeType, out bool isExpression)
		{
			if (mimeTypeAttribute == null)
			{
				mimeType = null;
				isExpression = false;
				return false;
			}
			mimeType = this.GetStyleAttributeValue("BackgroundImageMIMEType", mimeTypeAttribute);
			isExpression = mimeTypeAttribute.IsExpression;
			return true;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A6BA File Offset: 0x000088BA
		internal bool GetBackgroundImageRepeat(AttributeInfo repeatAttribute, out object repeat, out bool isExpression)
		{
			if (repeatAttribute == null)
			{
				repeat = null;
				isExpression = false;
				return false;
			}
			repeat = this.GetStyleAttributeValue("BackgroundRepeat", repeatAttribute);
			isExpression = repeatAttribute.IsExpression;
			return true;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000A6DE File Offset: 0x000088DE
		internal bool GetBackgroundImageProperties(AttributeInfo sourceAttribute, AttributeInfo valueAttribute, AttributeInfo mimeTypeAttribute, out Microsoft.ReportingServices.ReportRendering.Image.SourceType imageSource, out object imageValue, out bool isValueExpression, out object mimeType, out bool isMimeTypeExpression)
		{
			this.GetBackgroundImageValue(valueAttribute, out imageValue, out isValueExpression);
			this.GetBackgroundImageMIMEType(mimeTypeAttribute, out mimeType, out isMimeTypeExpression);
			return this.GetBackgroundImageSource(sourceAttribute, out imageSource);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000A701 File Offset: 0x00008901
		internal bool GetBackgroundImageProperties(AttributeInfo sourceAttribute, AttributeInfo valueAttribute, AttributeInfo mimeTypeAttribute, AttributeInfo repeatAttribute, out Microsoft.ReportingServices.ReportRendering.Image.SourceType imageSource, out object imageValue, out bool isValueExpression, out object mimeType, out bool isMimeTypeExpression, out object repeat, out bool isRepeatExpression)
		{
			this.GetBackgroundImageValue(valueAttribute, out imageValue, out isValueExpression);
			this.GetBackgroundImageMIMEType(mimeTypeAttribute, out mimeType, out isMimeTypeExpression);
			this.GetBackgroundImageRepeat(repeatAttribute, out repeat, out isRepeatExpression);
			return this.GetBackgroundImageSource(sourceAttribute, out imageSource);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000A734 File Offset: 0x00008934
		public void SetStyle(Microsoft.ReportingServices.ReportRendering.Style.StyleName style, object value, bool isShared)
		{
			object obj = null;
			bool flag;
			switch (style)
			{
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderColor:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderColorTop:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderColorLeft:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderColorRight:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderColorBottom:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BackgroundColor:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.Color:
				if (value is ReportColor)
				{
					obj = value as ReportColor;
				}
				else if (value is string)
				{
					obj = new ReportColor(value as string);
				}
				if (obj == null)
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidStyleArgumentType, new object[] { "ReportColor" });
				}
				obj = ((ReportColor)obj).ToString();
				flag = true;
				goto IL_01E8;
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderStyle:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderStyleTop:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderStyleLeft:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderStyleRight:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderStyleBottom:
				if (value != null)
				{
					obj = value as string;
					if (obj == null)
					{
						throw new ReportRenderingException(ErrorCode.rrInvalidStyleArgumentType, new object[] { "String" });
					}
					string text;
					if (!Validator.ValidateBorderStyle(obj as string, out text))
					{
						throw new ReportRenderingException(ErrorCode.rrInvalidBorderStyle, new object[] { obj });
					}
				}
				flag = true;
				goto IL_01E8;
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderWidth:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderWidthTop:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderWidthLeft:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderWidthRight:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.BorderWidthBottom:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.FontSize:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.PaddingLeft:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.PaddingRight:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.PaddingTop:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.PaddingBottom:
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.LineHeight:
				if (value is ReportSize)
				{
					obj = value as ReportSize;
				}
				else if (value is string)
				{
					obj = new ReportSize(value as string);
				}
				if (obj == null)
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidStyleArgumentType, new object[] { "ReportSize" });
				}
				flag = true;
				goto IL_01E8;
			case Microsoft.ReportingServices.ReportRendering.Style.StyleName.NumeralVariant:
			{
				int num;
				if (!int.TryParse(value as string, out num))
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidStyleArgumentType, new object[] { "Int32" });
				}
				obj = num;
				flag = true;
				goto IL_01E8;
			}
			}
			if (value != null)
			{
				obj = value as string;
				if (obj == null)
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidStyleArgumentType, new object[] { "String" });
				}
			}
			flag = true;
			IL_01E8:
			if (flag)
			{
				if (isShared)
				{
					if (this.m_sharedProperties == null)
					{
						this.m_sharedProperties = new StyleProperties();
					}
					this.m_sharedProperties.Set(style.ToString(), obj);
					return;
				}
				if (this.m_nonSharedProperties == null)
				{
					this.m_nonSharedProperties = new StyleProperties();
				}
				this.m_nonSharedProperties.Set(style.ToString(), obj);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000A988 File Offset: 0x00008B88
		internal void AddStyleProperty(string styleName, bool isExpression, bool needNonSharedProps, bool needSharedProps, object styleProperty)
		{
			if (isExpression)
			{
				if (needNonSharedProps)
				{
					if (this.m_nonSharedProperties == null)
					{
						this.m_nonSharedProperties = new StyleProperties();
					}
					this.m_nonSharedProperties.Add(styleName, styleProperty);
					return;
				}
			}
			else if (needSharedProps)
			{
				if (this.m_sharedProperties == null)
				{
					this.m_sharedProperties = new StyleProperties(42);
				}
				this.m_sharedProperties.Add(styleName, styleProperty);
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		internal void SetStyleProperty(string styleName, bool isExpression, bool needNonSharedProps, bool needSharedProps, object styleProperty)
		{
			if (isExpression)
			{
				if (needNonSharedProps)
				{
					if (this.m_nonSharedProperties == null)
					{
						this.m_nonSharedProperties = new StyleProperties();
					}
					this.m_nonSharedProperties.Set(styleName, styleProperty);
					return;
				}
			}
			else if (needSharedProps)
			{
				if (this.m_sharedProperties == null)
				{
					this.m_sharedProperties = new StyleProperties(42);
				}
				this.m_sharedProperties.Set(styleName, styleProperty);
			}
		}

		// Token: 0x060003EF RID: 1007
		internal abstract void PopulateStyleProperties(bool populateAll);

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000AA40 File Offset: 0x00008C40
		protected bool IsCustomControl
		{
			get
			{
				return this.m_isCustomControlGenerated;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000AA48 File Offset: 0x00008C48
		internal void ExtractRenderStyles(out DataValueInstanceList sharedStyles, out DataValueInstanceList nonSharedStyles)
		{
			sharedStyles = null;
			nonSharedStyles = null;
			if (this.m_sharedProperties != null)
			{
				sharedStyles = this.m_sharedProperties.ExtractRenderStyles();
			}
			if (this.m_nonSharedProperties != null)
			{
				nonSharedStyles = this.m_nonSharedProperties.ExtractRenderStyles();
			}
		}

		// Token: 0x04000081 RID: 129
		protected const int StyleAttributeCount = 42;

		// Token: 0x04000082 RID: 130
		protected const string BorderColor = "BorderColor";

		// Token: 0x04000083 RID: 131
		protected const string BorderColorLeft = "BorderColorLeft";

		// Token: 0x04000084 RID: 132
		protected const string BorderColorRight = "BorderColorRight";

		// Token: 0x04000085 RID: 133
		protected const string BorderColorTop = "BorderColorTop";

		// Token: 0x04000086 RID: 134
		protected const string BorderColorBottom = "BorderColorBottom";

		// Token: 0x04000087 RID: 135
		protected const string BorderStyle = "BorderStyle";

		// Token: 0x04000088 RID: 136
		protected const string BorderStyleLeft = "BorderStyleLeft";

		// Token: 0x04000089 RID: 137
		protected const string BorderStyleRight = "BorderStyleRight";

		// Token: 0x0400008A RID: 138
		protected const string BorderStyleTop = "BorderStyleTop";

		// Token: 0x0400008B RID: 139
		protected const string BorderStyleBottom = "BorderStyleBottom";

		// Token: 0x0400008C RID: 140
		protected const string BorderWidth = "BorderWidth";

		// Token: 0x0400008D RID: 141
		protected const string BorderWidthLeft = "BorderWidthLeft";

		// Token: 0x0400008E RID: 142
		protected const string BorderWidthRight = "BorderWidthRight";

		// Token: 0x0400008F RID: 143
		protected const string BorderWidthTop = "BorderWidthTop";

		// Token: 0x04000090 RID: 144
		protected const string BorderWidthBottom = "BorderWidthBottom";

		// Token: 0x04000091 RID: 145
		protected const string BackgroundImage = "BackgroundImage";

		// Token: 0x04000092 RID: 146
		protected const string BackgroundImageSource = "BackgroundImageSource";

		// Token: 0x04000093 RID: 147
		protected const string BackgroundImageValue = "BackgroundImageValue";

		// Token: 0x04000094 RID: 148
		protected const string BackgroundImageMIMEType = "BackgroundImageMIMEType";

		// Token: 0x04000095 RID: 149
		protected const string BackgroundColor = "BackgroundColor";

		// Token: 0x04000096 RID: 150
		protected const string BackgroundGradientEndColor = "BackgroundGradientEndColor";

		// Token: 0x04000097 RID: 151
		protected const string BackgroundGradientType = "BackgroundGradientType";

		// Token: 0x04000098 RID: 152
		protected const string BackgroundRepeat = "BackgroundRepeat";

		// Token: 0x04000099 RID: 153
		protected const string FontStyle = "FontStyle";

		// Token: 0x0400009A RID: 154
		protected const string FontFamily = "FontFamily";

		// Token: 0x0400009B RID: 155
		protected const string FontSize = "FontSize";

		// Token: 0x0400009C RID: 156
		protected const string FontWeight = "FontWeight";

		// Token: 0x0400009D RID: 157
		protected const string Format = "Format";

		// Token: 0x0400009E RID: 158
		protected const string TextDecoration = "TextDecoration";

		// Token: 0x0400009F RID: 159
		protected const string TextAlign = "TextAlign";

		// Token: 0x040000A0 RID: 160
		protected const string VerticalAlign = "VerticalAlign";

		// Token: 0x040000A1 RID: 161
		protected const string Color = "Color";

		// Token: 0x040000A2 RID: 162
		protected const string PaddingLeft = "PaddingLeft";

		// Token: 0x040000A3 RID: 163
		protected const string PaddingRight = "PaddingRight";

		// Token: 0x040000A4 RID: 164
		protected const string PaddingTop = "PaddingTop";

		// Token: 0x040000A5 RID: 165
		protected const string PaddingBottom = "PaddingBottom";

		// Token: 0x040000A6 RID: 166
		protected const string LineHeight = "LineHeight";

		// Token: 0x040000A7 RID: 167
		protected const string Direction = "Direction";

		// Token: 0x040000A8 RID: 168
		protected const string WritingMode = "WritingMode";

		// Token: 0x040000A9 RID: 169
		protected const string Language = "Language";

		// Token: 0x040000AA RID: 170
		protected const string UnicodeBiDi = "UnicodeBiDi";

		// Token: 0x040000AB RID: 171
		protected const string Calendar = "Calendar";

		// Token: 0x040000AC RID: 172
		protected const string CurrencyLanguage = "CurrencyLanguage";

		// Token: 0x040000AD RID: 173
		protected const string NumeralLanguage = "NumeralLanguage";

		// Token: 0x040000AE RID: 174
		protected const string NumeralVariant = "NumeralVariant";

		// Token: 0x040000AF RID: 175
		internal Microsoft.ReportingServices.ReportRendering.RenderingContext m_renderingContext;

		// Token: 0x040000B0 RID: 176
		protected StyleProperties m_sharedProperties;

		// Token: 0x040000B1 RID: 177
		protected StyleProperties m_nonSharedProperties;

		// Token: 0x040000B2 RID: 178
		protected bool m_isCustomControlGenerated;
	}
}
