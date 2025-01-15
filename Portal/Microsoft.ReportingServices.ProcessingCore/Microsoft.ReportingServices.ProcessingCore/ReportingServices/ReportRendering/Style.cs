using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000021 RID: 33
	public sealed class Style : StyleBase
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000AA7A File Offset: 0x00008C7A
		public Style()
		{
			Global.Tracer.Assert(base.IsCustomControl);
			this.m_styleDefaults = Style.NormalStyleDefaults;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		internal Style(ReportItem reportItem, ReportItem reportItemDef, RenderingContext context)
			: base(context)
		{
			Global.Tracer.Assert(!base.IsCustomControl);
			this.m_reportItem = reportItem;
			this.m_reportItemDef = reportItemDef;
			if (reportItem is Line)
			{
				this.m_styleDefaults = Style.LineStyleDefaults;
				return;
			}
			this.m_styleDefaults = Style.NormalStyleDefaults;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public override int Count
		{
			get
			{
				if (base.IsCustomControl)
				{
					return base.Count;
				}
				if (this.m_reportItemDef.StyleClass == null)
				{
					return 0;
				}
				return base.Count;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000AB1A File Offset: 0x00008D1A
		public override ICollection Keys
		{
			get
			{
				if (base.IsCustomControl)
				{
					return base.Keys;
				}
				if (this.m_reportItemDef.StyleClass == null)
				{
					return null;
				}
				return base.Keys;
			}
		}

		// Token: 0x1700035B RID: 859
		public override object this[string styleName]
		{
			get
			{
				if (base.IsCustomControl)
				{
					object obj = null;
					if (this.m_nonSharedProperties != null)
					{
						obj = this.m_nonSharedProperties[styleName];
					}
					if (obj == null && this.m_sharedProperties != null)
					{
						obj = this.m_sharedProperties[styleName];
					}
					return this.CreatePropertyOrReturnDefault(styleName, obj);
				}
				Global.Tracer.Assert(!base.IsCustomControl);
				if (this.m_reportItem.HeadingInstance == null && this.m_reportItemDef.StyleClass == null)
				{
					return this.m_styleDefaults[styleName];
				}
				StyleAttributeHashtable styleAttributeHashtable = null;
				if (this.m_reportItemDef.StyleClass != null)
				{
					styleAttributeHashtable = this.m_reportItemDef.StyleClass.StyleAttributes;
				}
				StyleAttributeHashtable styleAttributeHashtable2 = null;
				if (this.m_reportItem.HeadingInstance != null)
				{
					Global.Tracer.Assert(this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
					styleAttributeHashtable2 = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass.StyleAttributes;
				}
				if ("BackgroundImage" == styleName)
				{
					Image.SourceType sourceType = Image.SourceType.External;
					object obj2 = null;
					object obj3 = null;
					bool flag = false;
					if (styleAttributeHashtable2 != null)
					{
						bool flag2;
						base.GetBackgroundImageProperties(styleAttributeHashtable2["BackgroundImageSource"], styleAttributeHashtable2["BackgroundImageValue"], styleAttributeHashtable2["BackgroundImageMIMEType"], out sourceType, out obj2, out flag2, out obj3, out flag);
					}
					if (obj2 == null && styleAttributeHashtable != null)
					{
						bool flag2;
						base.GetBackgroundImageProperties(styleAttributeHashtable["BackgroundImageSource"], styleAttributeHashtable["BackgroundImageValue"], styleAttributeHashtable["BackgroundImageMIMEType"], out sourceType, out obj2, out flag2, out obj3, out flag);
					}
					if (obj2 != null)
					{
						string text = null;
						if (!flag)
						{
							text = (string)obj3;
						}
						return new BackgroundImage(this.m_renderingContext, sourceType, obj2, text);
					}
				}
				else
				{
					if (styleAttributeHashtable2 != null)
					{
						AttributeInfo attributeInfo = styleAttributeHashtable2[styleName];
						if (attributeInfo != null)
						{
							return this.CreatePropertyOrReturnDefault(styleName, this.GetStyleAttributeValue(styleName, attributeInfo));
						}
					}
					if (styleAttributeHashtable != null)
					{
						AttributeInfo attributeInfo2 = styleAttributeHashtable[styleName];
						if (attributeInfo2 != null)
						{
							return this.CreatePropertyOrReturnDefault(styleName, this.GetStyleAttributeValue(styleName, attributeInfo2));
						}
					}
				}
				return this.m_styleDefaults[styleName];
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000AD3A File Offset: 0x00008F3A
		public override StyleProperties SharedProperties
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.m_sharedProperties;
				}
				if (this.NeedPopulateSharedProps())
				{
					this.PopulateStyleProperties(false);
					this.m_reportItem.ReportItemDef.SharedStyleProperties = this.m_sharedProperties;
				}
				return this.m_sharedProperties;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000AD78 File Offset: 0x00008F78
		public override StyleProperties NonSharedProperties
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.m_nonSharedProperties;
				}
				if (this.NeedPopulateNonSharedProps())
				{
					this.PopulateNonSharedStyleProperties();
					if (this.m_nonSharedProperties == null || this.m_nonSharedProperties.Count == 0)
					{
						this.m_reportItemDef.NoNonSharedStyleProps = true;
					}
				}
				return this.m_nonSharedProperties;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000ADCC File Offset: 0x00008FCC
		internal bool HasBackgroundImage(out bool isExpressionBased)
		{
			isExpressionBased = false;
			if (this.m_reportItem.HeadingInstance == null && this.m_reportItemDef.StyleClass == null)
			{
				return false;
			}
			if (this.GetStyleDefinition("BackgroundImageValue") == null)
			{
				return false;
			}
			StyleAttributeHashtable styleAttributeHashtable = null;
			if (this.m_reportItemDef.StyleClass != null)
			{
				styleAttributeHashtable = this.m_reportItemDef.StyleClass.StyleAttributes;
			}
			StyleAttributeHashtable styleAttributeHashtable2 = null;
			if (this.m_reportItem.HeadingInstance != null)
			{
				Global.Tracer.Assert(this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
				styleAttributeHashtable2 = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass.StyleAttributes;
			}
			Image.SourceType sourceType = Image.SourceType.External;
			object obj = null;
			object obj2 = null;
			object obj3 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (styleAttributeHashtable2 != null)
			{
				base.GetBackgroundImageProperties(styleAttributeHashtable2["BackgroundImageSource"], styleAttributeHashtable2["BackgroundImageValue"], styleAttributeHashtable2["BackgroundImageMIMEType"], styleAttributeHashtable2["BackgroundRepeat"], out sourceType, out obj, out flag, out obj2, out flag2, out obj3, out flag3);
			}
			if (obj == null && styleAttributeHashtable != null)
			{
				base.GetBackgroundImageProperties(styleAttributeHashtable["BackgroundImageSource"], styleAttributeHashtable["BackgroundImageValue"], styleAttributeHashtable["BackgroundImageMIMEType"], styleAttributeHashtable["BackgroundRepeat"], out sourceType, out obj, out flag, out obj2, out flag2, out obj3, out flag3);
			}
			if (obj != null)
			{
				isExpressionBased = flag || flag2 || flag3;
				return true;
			}
			return false;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000AF2C File Offset: 0x0000912C
		internal AttributeInfo GetStyleDefinition(string styleName)
		{
			string text = null;
			return this.GetStyleDefinition(styleName, out text);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000AF44 File Offset: 0x00009144
		internal AttributeInfo GetStyleDefinition(string styleName, out string expressionString)
		{
			expressionString = null;
			if (base.IsCustomControl)
			{
				return null;
			}
			if (this.m_reportItem.HeadingInstance == null && this.m_reportItemDef.StyleClass == null)
			{
				return null;
			}
			StyleAttributeHashtable styleAttributeHashtable = null;
			ExpressionInfoList expressionInfoList = null;
			if (this.m_reportItem.HeadingInstance != null)
			{
				Global.Tracer.Assert(this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
				styleAttributeHashtable = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass.StyleAttributes;
				expressionInfoList = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass.ExpressionList;
			}
			AttributeInfo attributeInfo = null;
			if ((styleAttributeHashtable == null || !styleAttributeHashtable.ContainsKey(styleName)) && this.m_reportItemDef.StyleClass != null)
			{
				styleAttributeHashtable = this.m_reportItemDef.StyleClass.StyleAttributes;
				expressionInfoList = this.m_reportItemDef.StyleClass.ExpressionList;
			}
			if (styleAttributeHashtable != null)
			{
				attributeInfo = styleAttributeHashtable[styleName];
				if (attributeInfo != null && attributeInfo.IsExpression)
				{
					expressionString = expressionInfoList[attributeInfo.IntValue].OriginalText;
				}
			}
			return attributeInfo;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000B05C File Offset: 0x0000925C
		private bool NeedPopulateSharedProps()
		{
			if (base.IsCustomControl)
			{
				return false;
			}
			if (this.m_reportItem.HeadingInstance != null)
			{
				return true;
			}
			if (this.m_sharedProperties != null)
			{
				return false;
			}
			if (this.m_reportItemDef.SharedStyleProperties == null)
			{
				return true;
			}
			if (42 != this.m_reportItemDef.SharedStyleProperties.Count + ((this.m_nonSharedProperties == null) ? 0 : this.m_nonSharedProperties.Count))
			{
				return true;
			}
			this.m_sharedProperties = this.m_reportItemDef.SharedStyleProperties;
			return false;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B0DA File Offset: 0x000092DA
		private bool NeedPopulateNonSharedProps()
		{
			return !base.IsCustomControl && (this.m_reportItem.HeadingInstance != null || (this.m_nonSharedProperties == null && !this.m_reportItemDef.NoNonSharedStyleProps));
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000B10D File Offset: 0x0000930D
		internal static object GetStyleValue(string styleName, Style styleDef, object[] styleAttributeValues)
		{
			return Style.GetStyleValue(styleName, styleDef, styleAttributeValues, true);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000B118 File Offset: 0x00009318
		internal static object GetStyleValue(string styleName, Style styleDef, object[] styleAttributeValues, bool returnDefaultStyle)
		{
			object styleValueBase = StyleBase.GetStyleValueBase(styleName, styleDef, styleAttributeValues);
			if (styleValueBase != null)
			{
				return styleValueBase;
			}
			if (returnDefaultStyle)
			{
				return Style.NormalStyleDefaults[styleName];
			}
			return null;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000B144 File Offset: 0x00009344
		internal override object GetStyleAttributeValue(string styleName, AttributeInfo attribute)
		{
			if (this.m_reportItem.HeadingInstance != null)
			{
				Style styleClass = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass;
				Global.Tracer.Assert(styleClass != null);
				AttributeInfo attributeInfo = styleClass.StyleAttributes[styleName];
				if (attributeInfo != null)
				{
					if (attributeInfo.IsExpression)
					{
						MatrixSubtotalHeadingInstanceInfo matrixSubtotalHeadingInstanceInfo = this.m_reportItem.HeadingInstance.GetInstanceInfo(this.m_reportItem.RenderingContext.ChunkManager) as MatrixSubtotalHeadingInstanceInfo;
						Global.Tracer.Assert(matrixSubtotalHeadingInstanceInfo != null);
						Global.Tracer.Assert(matrixSubtotalHeadingInstanceInfo.StyleAttributeValues != null);
						Global.Tracer.Assert(0 <= attributeInfo.IntValue && attributeInfo.IntValue < matrixSubtotalHeadingInstanceInfo.StyleAttributeValues.Length);
						return matrixSubtotalHeadingInstanceInfo.StyleAttributeValues[attributeInfo.IntValue];
					}
					if ("NumeralVariant" == styleName)
					{
						return attributeInfo.IntValue;
					}
					return attributeInfo.Value;
				}
			}
			if (attribute.IsExpression)
			{
				ReportItemInstanceInfo instanceInfo = this.m_reportItem.InstanceInfo;
				if (instanceInfo != null)
				{
					return instanceInfo.GetStyleAttributeValue(attribute.IntValue);
				}
				return null;
			}
			else
			{
				if ("NumeralVariant" == styleName)
				{
					return attribute.IntValue;
				}
				return attribute.Value;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000B288 File Offset: 0x00009488
		internal override void PopulateStyleProperties(bool populateAll)
		{
			if (base.IsCustomControl)
			{
				return;
			}
			bool flag = true;
			bool flag2 = false;
			if (populateAll)
			{
				flag = this.NeedPopulateSharedProps();
				flag2 = this.NeedPopulateNonSharedProps();
				if (!flag && !flag2)
				{
					return;
				}
			}
			Style styleClass = this.m_reportItemDef.StyleClass;
			StyleAttributeHashtable styleAttributeHashtable = null;
			if (styleClass != null)
			{
				styleAttributeHashtable = styleClass.StyleAttributes;
			}
			StyleAttributeHashtable styleAttributeHashtable2 = null;
			if (this.m_reportItem.HeadingInstance != null)
			{
				Global.Tracer.Assert(this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
				styleAttributeHashtable2 = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass.StyleAttributes;
			}
			for (int i = 0; i < 42; i++)
			{
				string name = this.m_styleDefaults.GetName(i);
				if (styleAttributeHashtable == null && styleAttributeHashtable2 == null)
				{
					base.AddStyleProperty(name, false, flag2, flag, this.m_styleDefaults[i]);
				}
				else if (styleAttributeHashtable2 != null && styleAttributeHashtable2.ContainsKey(name))
				{
					AttributeInfo attributeInfo = styleAttributeHashtable2[name];
					base.AddStyleProperty(name, true, true, false, this.CreatePropertyOrReturnDefault(name, this.GetStyleAttributeValue(name, attributeInfo)));
				}
				else if (styleAttributeHashtable != null && styleAttributeHashtable.ContainsKey(name))
				{
					AttributeInfo attributeInfo2 = styleAttributeHashtable[name];
					base.AddStyleProperty(name, attributeInfo2.IsExpression, flag2, flag, this.CreatePropertyOrReturnDefault(name, this.GetStyleAttributeValue(name, attributeInfo2)));
				}
				else if ("BackgroundImage" == name)
				{
					Image.SourceType sourceType = Image.SourceType.External;
					object obj = null;
					object obj2 = null;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					if (styleAttributeHashtable2 != null)
					{
						flag5 = base.GetBackgroundImageProperties(styleAttributeHashtable2["BackgroundImageSource"], styleAttributeHashtable2["BackgroundImageValue"], styleAttributeHashtable2["BackgroundImageMIMEType"], out sourceType, out obj, out flag3, out obj2, out flag4);
					}
					if (!flag5 && styleAttributeHashtable != null)
					{
						flag5 = base.GetBackgroundImageProperties(styleAttributeHashtable["BackgroundImageSource"], styleAttributeHashtable["BackgroundImageValue"], styleAttributeHashtable["BackgroundImageMIMEType"], out sourceType, out obj, out flag3, out obj2, out flag4);
					}
					object obj3;
					if (obj != null)
					{
						string text = null;
						if (!flag4)
						{
							text = (string)obj2;
						}
						obj3 = new BackgroundImage(this.m_renderingContext, sourceType, obj, text);
					}
					else
					{
						obj3 = this.m_styleDefaults[i];
					}
					base.AddStyleProperty(name, flag3 || flag4, flag2, flag, obj3);
				}
				else
				{
					base.AddStyleProperty(name, false, flag2, flag, this.m_styleDefaults[i]);
				}
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000B4EC File Offset: 0x000096EC
		private void PopulateNonSharedStyleProperties()
		{
			if (base.IsCustomControl)
			{
				return;
			}
			Style styleClass = this.m_reportItemDef.StyleClass;
			if (styleClass != null)
			{
				StyleAttributeHashtable styleAttributes = styleClass.StyleAttributes;
				Global.Tracer.Assert(styleAttributes != null);
				this.InternalPopulateNonSharedStyleProperties(styleAttributes, false);
			}
			if (this.m_reportItem.HeadingInstance != null)
			{
				Global.Tracer.Assert(this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
				StyleAttributeHashtable styleAttributes2 = this.m_reportItem.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass.StyleAttributes;
				this.InternalPopulateNonSharedStyleProperties(styleAttributes2, true);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000B58C File Offset: 0x0000978C
		private void InternalPopulateNonSharedStyleProperties(StyleAttributeHashtable styleAttributes, bool isSubtotal)
		{
			if (base.IsCustomControl)
			{
				return;
			}
			if (styleAttributes == null)
			{
				return;
			}
			IDictionaryEnumerator enumerator = styleAttributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AttributeInfo attributeInfo = (AttributeInfo)enumerator.Value;
				string text = (string)enumerator.Key;
				if ("BackgroundImageSource" == text)
				{
					Image.SourceType sourceType;
					object obj;
					bool flag;
					object obj2;
					bool flag2;
					if (base.GetBackgroundImageProperties(attributeInfo, styleAttributes["BackgroundImageValue"], styleAttributes["BackgroundImageMIMEType"], out sourceType, out obj, out flag, out obj2, out flag2) && (flag || flag2))
					{
						object obj3;
						if (obj != null)
						{
							string text2 = null;
							if (!flag2)
							{
								text2 = (string)obj2;
							}
							obj3 = new BackgroundImage(this.m_renderingContext, sourceType, obj, text2);
						}
						else
						{
							obj3 = this.m_styleDefaults["BackgroundImage"];
						}
						base.SetStyleProperty("BackgroundImage", true, true, false, obj3);
					}
				}
				else if (!("BackgroundImageValue" == text) && !("BackgroundImageMIMEType" == text) && (isSubtotal || attributeInfo.IsExpression))
				{
					base.SetStyleProperty(text, true, true, false, this.CreatePropertyOrReturnDefault(text, this.GetStyleAttributeValue(text, attributeInfo)));
				}
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000B6A5 File Offset: 0x000098A5
		private object CreatePropertyOrReturnDefault(string styleName, object styleValue)
		{
			if (styleValue == null)
			{
				return this.m_styleDefaults[styleName];
			}
			return StyleBase.CreateStyleProperty(styleName, styleValue);
		}

		// Token: 0x040000B3 RID: 179
		private ReportItem m_reportItem;

		// Token: 0x040000B4 RID: 180
		private ReportItem m_reportItemDef;

		// Token: 0x040000B5 RID: 181
		private Style.StyleDefaults m_styleDefaults;

		// Token: 0x040000B6 RID: 182
		private static Style.StyleDefaults NormalStyleDefaults = new Style.StyleDefaults(false);

		// Token: 0x040000B7 RID: 183
		private static Style.StyleDefaults LineStyleDefaults = new Style.StyleDefaults(true);

		// Token: 0x0200090B RID: 2315
		public enum StyleName
		{
			// Token: 0x04003EC2 RID: 16066
			BorderColor,
			// Token: 0x04003EC3 RID: 16067
			BorderColorTop,
			// Token: 0x04003EC4 RID: 16068
			BorderColorLeft,
			// Token: 0x04003EC5 RID: 16069
			BorderColorRight,
			// Token: 0x04003EC6 RID: 16070
			BorderColorBottom,
			// Token: 0x04003EC7 RID: 16071
			BorderStyle,
			// Token: 0x04003EC8 RID: 16072
			BorderStyleTop,
			// Token: 0x04003EC9 RID: 16073
			BorderStyleLeft,
			// Token: 0x04003ECA RID: 16074
			BorderStyleRight,
			// Token: 0x04003ECB RID: 16075
			BorderStyleBottom,
			// Token: 0x04003ECC RID: 16076
			BorderWidth,
			// Token: 0x04003ECD RID: 16077
			BorderWidthTop,
			// Token: 0x04003ECE RID: 16078
			BorderWidthLeft,
			// Token: 0x04003ECF RID: 16079
			BorderWidthRight,
			// Token: 0x04003ED0 RID: 16080
			BorderWidthBottom,
			// Token: 0x04003ED1 RID: 16081
			BackgroundColor,
			// Token: 0x04003ED2 RID: 16082
			FontStyle,
			// Token: 0x04003ED3 RID: 16083
			FontFamily,
			// Token: 0x04003ED4 RID: 16084
			FontSize,
			// Token: 0x04003ED5 RID: 16085
			FontWeight,
			// Token: 0x04003ED6 RID: 16086
			Format,
			// Token: 0x04003ED7 RID: 16087
			TextDecoration,
			// Token: 0x04003ED8 RID: 16088
			TextAlign,
			// Token: 0x04003ED9 RID: 16089
			VerticalAlign,
			// Token: 0x04003EDA RID: 16090
			Color,
			// Token: 0x04003EDB RID: 16091
			PaddingLeft,
			// Token: 0x04003EDC RID: 16092
			PaddingRight,
			// Token: 0x04003EDD RID: 16093
			PaddingTop,
			// Token: 0x04003EDE RID: 16094
			PaddingBottom,
			// Token: 0x04003EDF RID: 16095
			LineHeight,
			// Token: 0x04003EE0 RID: 16096
			Direction,
			// Token: 0x04003EE1 RID: 16097
			WritingMode,
			// Token: 0x04003EE2 RID: 16098
			Language,
			// Token: 0x04003EE3 RID: 16099
			UnicodeBiDi,
			// Token: 0x04003EE4 RID: 16100
			Calendar,
			// Token: 0x04003EE5 RID: 16101
			NumeralLanguage,
			// Token: 0x04003EE6 RID: 16102
			NumeralVariant
		}

		// Token: 0x0200090C RID: 2316
		internal sealed class StyleDefaults
		{
			// Token: 0x06007F0B RID: 32523 RVA: 0x0020BBA8 File Offset: 0x00209DA8
			internal StyleDefaults(bool isLine)
			{
				this.m_nameMap = new Hashtable(42);
				this.m_keyCollection = new string[42];
				this.m_valueCollection = new object[42];
				int num = 0;
				this.m_nameMap["BorderColor"] = num;
				this.m_keyCollection[num] = "BorderColor";
				this.m_valueCollection[num++] = new ReportColor("Black", false);
				this.m_nameMap["BorderColorTop"] = num;
				this.m_keyCollection[num] = "BorderColorTop";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderColorLeft"] = num;
				this.m_keyCollection[num] = "BorderColorLeft";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderColorRight"] = num;
				this.m_keyCollection[num] = "BorderColorRight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderColorBottom"] = num;
				this.m_keyCollection[num] = "BorderColorBottom";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyle"] = num;
				this.m_keyCollection[num] = "BorderStyle";
				if (!isLine)
				{
					this.m_valueCollection[num++] = "None";
				}
				else
				{
					this.m_valueCollection[num++] = "Solid";
				}
				this.m_nameMap["BorderStyleTop"] = num;
				this.m_keyCollection[num] = "BorderStyleTop";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyleLeft"] = num;
				this.m_keyCollection[num] = "BorderStyleLeft";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyleRight"] = num;
				this.m_keyCollection[num] = "BorderStyleRight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyleBottom"] = num;
				this.m_keyCollection[num] = "BorderStyleBottom";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidth"] = num;
				this.m_keyCollection[num] = "BorderWidth";
				this.m_valueCollection[num++] = new ReportSize("1pt", false);
				this.m_nameMap["BorderWidthTop"] = num;
				this.m_keyCollection[num] = "BorderWidthTop";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidthLeft"] = num;
				this.m_keyCollection[num] = "BorderWidthLeft";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidthRight"] = num;
				this.m_keyCollection[num] = "BorderWidthRight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidthBottom"] = num;
				this.m_keyCollection[num] = "BorderWidthBottom";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BackgroundColor"] = num;
				this.m_keyCollection[num] = "BackgroundColor";
				this.m_valueCollection[num++] = new ReportColor("Transparent", false);
				this.m_nameMap["BackgroundGradientType"] = num;
				this.m_keyCollection[num] = "BackgroundGradientType";
				this.m_valueCollection[num++] = "None";
				this.m_nameMap["BackgroundGradientEndColor"] = num;
				this.m_keyCollection[num] = "BackgroundGradientEndColor";
				this.m_valueCollection[num++] = new ReportColor("Transparent", false);
				this.m_nameMap["BackgroundImage"] = num;
				this.m_keyCollection[num] = "BackgroundImage";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BackgroundRepeat"] = num;
				this.m_keyCollection[num] = "BackgroundRepeat";
				this.m_valueCollection[num++] = "Repeat";
				this.m_nameMap["FontStyle"] = num;
				this.m_keyCollection[num] = "FontStyle";
				this.m_valueCollection[num++] = "Normal";
				this.m_nameMap["FontFamily"] = num;
				this.m_keyCollection[num] = "FontFamily";
				this.m_valueCollection[num++] = "Arial";
				this.m_nameMap["FontSize"] = num;
				this.m_keyCollection[num] = "FontSize";
				this.m_valueCollection[num++] = new ReportSize("10pt", false);
				this.m_nameMap["FontWeight"] = num;
				this.m_keyCollection[num] = "FontWeight";
				this.m_valueCollection[num++] = "Normal";
				this.m_nameMap["Format"] = num;
				this.m_keyCollection[num] = "Format";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["TextDecoration"] = num;
				this.m_keyCollection[num] = "TextDecoration";
				this.m_valueCollection[num++] = "None";
				this.m_nameMap["TextAlign"] = num;
				this.m_keyCollection[num] = "TextAlign";
				this.m_valueCollection[num++] = "General";
				this.m_nameMap["VerticalAlign"] = num;
				this.m_keyCollection[num] = "VerticalAlign";
				this.m_valueCollection[num++] = "Top";
				this.m_nameMap["Color"] = num;
				this.m_keyCollection[num] = "Color";
				this.m_valueCollection[num++] = new ReportColor("Black", false);
				this.m_nameMap["PaddingLeft"] = num;
				this.m_keyCollection[num] = "PaddingLeft";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["PaddingRight"] = num;
				this.m_keyCollection[num] = "PaddingRight";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["PaddingTop"] = num;
				this.m_keyCollection[num] = "PaddingTop";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["PaddingBottom"] = num;
				this.m_keyCollection[num] = "PaddingBottom";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["LineHeight"] = num;
				this.m_keyCollection[num] = "LineHeight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["Direction"] = num;
				this.m_keyCollection[num] = "Direction";
				this.m_valueCollection[num++] = "LTR";
				this.m_nameMap["WritingMode"] = num;
				this.m_keyCollection[num] = "WritingMode";
				this.m_valueCollection[num++] = "lr-tb";
				this.m_nameMap["Language"] = num;
				this.m_keyCollection[num] = "Language";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["UnicodeBiDi"] = num;
				this.m_keyCollection[num] = "UnicodeBiDi";
				this.m_valueCollection[num++] = "Normal";
				this.m_nameMap["Calendar"] = num;
				this.m_keyCollection[num] = "Calendar";
				this.m_valueCollection[num++] = "Gregorian";
				this.m_nameMap["NumeralLanguage"] = num;
				this.m_keyCollection[num] = "NumeralLanguage";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["NumeralVariant"] = num;
				this.m_keyCollection[num] = "NumeralVariant";
				this.m_valueCollection[num++] = 1;
				this.m_nameMap["CurrencyLanguage"] = num;
				this.m_keyCollection[num] = "CurrencyLanguage";
				this.m_valueCollection[num++] = null;
				Global.Tracer.Assert(42 == num);
			}

			// Token: 0x17002952 RID: 10578
			internal object this[int index]
			{
				get
				{
					return this.m_valueCollection[index];
				}
			}

			// Token: 0x17002953 RID: 10579
			internal object this[string styleName]
			{
				get
				{
					return this.m_valueCollection[(int)this.m_nameMap[styleName]];
				}
			}

			// Token: 0x06007F0E RID: 32526 RVA: 0x0020C4CA File Offset: 0x0020A6CA
			internal string GetName(int index)
			{
				return this.m_keyCollection[index];
			}

			// Token: 0x04003EE7 RID: 16103
			private Hashtable m_nameMap;

			// Token: 0x04003EE8 RID: 16104
			private string[] m_keyCollection;

			// Token: 0x04003EE9 RID: 16105
			private object[] m_valueCollection;
		}
	}
}
