using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A4 RID: 932
	internal sealed class StyleInformation
	{
		// Token: 0x060025FE RID: 9726 RVA: 0x000B5350 File Offset: 0x000B3550
		static StyleInformation()
		{
			StyleInformation.StyleNameIndexes.Add("BorderColor", 0);
			StyleInformation.StyleNameIndexes.Add("BorderColorLeft", 1);
			StyleInformation.StyleNameIndexes.Add("BorderColorRight", 2);
			StyleInformation.StyleNameIndexes.Add("BorderColorTop", 3);
			StyleInformation.StyleNameIndexes.Add("BorderColorBottom", 4);
			StyleInformation.StyleNameIndexes.Add("BorderStyle", 5);
			StyleInformation.StyleNameIndexes.Add("BorderStyleLeft", 6);
			StyleInformation.StyleNameIndexes.Add("BorderStyleRight", 7);
			StyleInformation.StyleNameIndexes.Add("BorderStyleTop", 8);
			StyleInformation.StyleNameIndexes.Add("BorderStyleBottom", 9);
			StyleInformation.StyleNameIndexes.Add("BorderWidth", 10);
			StyleInformation.StyleNameIndexes.Add("BorderWidthLeft", 11);
			StyleInformation.StyleNameIndexes.Add("BorderWidthRight", 12);
			StyleInformation.StyleNameIndexes.Add("BorderWidthTop", 13);
			StyleInformation.StyleNameIndexes.Add("BorderWidthBottom", 14);
			StyleInformation.StyleNameIndexes.Add("BackgroundColor", 15);
			StyleInformation.StyleNameIndexes.Add("BackgroundImageSource", 16);
			StyleInformation.StyleNameIndexes.Add("BackgroundImageValue", 17);
			StyleInformation.StyleNameIndexes.Add("BackgroundImageMIMEType", 18);
			StyleInformation.StyleNameIndexes.Add("BackgroundRepeat", 19);
			StyleInformation.StyleNameIndexes.Add("FontStyle", 20);
			StyleInformation.StyleNameIndexes.Add("FontFamily", 21);
			StyleInformation.StyleNameIndexes.Add("FontSize", 22);
			StyleInformation.StyleNameIndexes.Add("FontWeight", 23);
			StyleInformation.StyleNameIndexes.Add("Format", 24);
			StyleInformation.StyleNameIndexes.Add("TextDecoration", 25);
			StyleInformation.StyleNameIndexes.Add("TextAlign", 26);
			StyleInformation.StyleNameIndexes.Add("VerticalAlign", 27);
			StyleInformation.StyleNameIndexes.Add("Color", 28);
			StyleInformation.StyleNameIndexes.Add("PaddingLeft", 29);
			StyleInformation.StyleNameIndexes.Add("PaddingRight", 30);
			StyleInformation.StyleNameIndexes.Add("PaddingTop", 31);
			StyleInformation.StyleNameIndexes.Add("PaddingBottom", 32);
			StyleInformation.StyleNameIndexes.Add("LineHeight", 33);
			StyleInformation.StyleNameIndexes.Add("Direction", 34);
			StyleInformation.StyleNameIndexes.Add("Language", 35);
			StyleInformation.StyleNameIndexes.Add("UnicodeBiDi", 36);
			StyleInformation.StyleNameIndexes.Add("Calendar", 37);
			StyleInformation.StyleNameIndexes.Add("NumeralLanguage", 38);
			StyleInformation.StyleNameIndexes.Add("NumeralVariant", 39);
			StyleInformation.StyleNameIndexes.Add("WritingMode", 40);
			StyleInformation.StyleNameIndexes.Add("BackgroundGradientType", 41);
			StyleInformation.StyleNameIndexes.Add("BackgroundGradientEndColor", 42);
			StyleInformation.StyleNameIndexes.Add("TextEffect", 43);
			StyleInformation.StyleNameIndexes.Add("BackgroundHatchType", 44);
			StyleInformation.StyleNameIndexes.Add("ShadowColor", 45);
			StyleInformation.StyleNameIndexes.Add("ShadowOffset", 46);
			StyleInformation.StyleNameIndexes.Add("TransparentColor", 47);
			StyleInformation.StyleNameIndexes.Add("Position", 48);
			StyleInformation.StyleNameIndexes.Add("EmbeddingMode", 49);
			StyleInformation.StyleNameIndexes.Add("Transparency", 50);
			StyleInformation.StyleNameIndexes.Add("CurrencyLanguage", 51);
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x000B57EF File Offset: 0x000B39EF
		internal List<StyleInformation.StyleInformationAttribute> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000B57F7 File Offset: 0x000B39F7
		internal void AddAttribute(string name, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.AddAttribute(name, expression, Microsoft.ReportingServices.ReportIntermediateFormat.ValueType.Constant);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000B5804 File Offset: 0x000B3A04
		internal void AddAttribute(string name, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportIntermediateFormat.ValueType valueType)
		{
			Global.Tracer.Assert(name != null);
			Global.Tracer.Assert(expression != null);
			this.m_attributes.Add(new StyleInformation.StyleInformationAttribute
			{
				Name = name,
				Value = expression,
				ValueType = valueType
			});
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000B5854 File Offset: 0x000B3A54
		internal void RemoveAttribute(string name)
		{
			Global.Tracer.Assert(name != null);
			this.m_attributes.RemoveAll((StyleInformation.StyleInformationAttribute a) => a.Name == name);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000B589C File Offset: 0x000B3A9C
		internal StyleInformation.StyleInformationAttribute GetAttributeByName(string name)
		{
			Global.Tracer.Assert(name != null);
			return this.m_attributes.SingleOrDefault((StyleInformation.StyleInformationAttribute a) => a.Name == name);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000B58E0 File Offset: 0x000B3AE0
		internal void Filter(StyleOwnerType ownerType, bool hasNoRows)
		{
			int num = this.MapStyleOwnerTypeToIndex(ownerType, hasNoRows);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				if (!this.Allow(this.MapStyleNameToIndex(this.m_attributes[i].Name), num))
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000B593C File Offset: 0x000B3B3C
		internal void FilterChartLegendTitleStyle()
		{
			int num = this.MapStyleOwnerTypeToIndex(StyleOwnerType.Chart, false);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				string name = this.m_attributes[i].Name;
				if (!this.Allow(this.MapStyleNameToIndex(name), num) && name != "TextAlign")
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000B59A8 File Offset: 0x000B3BA8
		internal void FilterChartStripLineStyle()
		{
			int num = this.MapStyleOwnerTypeToIndex(StyleOwnerType.Chart, false);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				string name = this.m_attributes[i].Name;
				if (!this.Allow(this.MapStyleNameToIndex(name), num) && name != "VerticalAlign" && name != "TextAlign")
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000B5A20 File Offset: 0x000B3C20
		internal void FilterChartSeriesStyle()
		{
			this.MapStyleOwnerTypeToIndex(StyleOwnerType.Chart, false);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				string name = this.m_attributes[i].Name;
				if (name != "ShadowColor" && name != "ShadowOffset")
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000B5A88 File Offset: 0x000B3C88
		internal void FilterGaugeLabelStyle()
		{
			int num = this.MapStyleOwnerTypeToIndex(StyleOwnerType.GaugePanel, false);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				string name = this.m_attributes[i].Name;
				if (!this.Allow(this.MapStyleNameToIndex(name), num) && name != "VerticalAlign" && name != "TextAlign")
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000B5B00 File Offset: 0x000B3D00
		internal void FilterMapTitleStyle()
		{
			int num = this.MapStyleOwnerTypeToIndex(StyleOwnerType.Map, false);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				string name = this.m_attributes[i].Name;
				if (!this.Allow(this.MapStyleNameToIndex(name), num) && name != "VerticalAlign" && name != "TextAlign")
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000B5B78 File Offset: 0x000B3D78
		internal void FilterMapLegendTitleStyle()
		{
			int num = this.MapStyleOwnerTypeToIndex(StyleOwnerType.Map, false);
			for (int i = this.m_attributes.Count - 1; i >= 0; i--)
			{
				string name = this.m_attributes[i].Name;
				if (!this.Allow(this.MapStyleNameToIndex(name), num) && name != "TextAlign")
				{
					this.m_attributes.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000B5BE2 File Offset: 0x000B3DE2
		private int MapStyleOwnerTypeToIndex(StyleOwnerType ownerType, bool hasNoRows)
		{
			if (hasNoRows)
			{
				return 0;
			}
			if (ownerType - StyleOwnerType.SubReport <= 1)
			{
				return 0;
			}
			if (ownerType == StyleOwnerType.PageSection)
			{
				return 2;
			}
			return (int)ownerType;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000B5BFA File Offset: 0x000B3DFA
		private int MapStyleNameToIndex(string name)
		{
			return (int)StyleInformation.StyleNameIndexes[name];
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000B5C0C File Offset: 0x000B3E0C
		private bool Allow(int styleName, int ownerType)
		{
			return StyleInformation.AllowStyleAttributeByType[styleName, ownerType];
		}

		// Token: 0x04001625 RID: 5669
		private List<StyleInformation.StyleInformationAttribute> m_attributes = new List<StyleInformation.StyleInformationAttribute>();

		// Token: 0x04001626 RID: 5670
		private static Hashtable StyleNameIndexes = new Hashtable();

		// Token: 0x04001627 RID: 5671
		private static bool[,] AllowStyleAttributeByType = new bool[,]
		{
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, true, true, true, true, true, true, true,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, true, true, false, false, true
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, false, true, false, false, false
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, false, true, false, false, false
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, false, true, false, false, false
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, false, true, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, true, false, false, false, false, false,
				false, false, false, false, true, false, false, false
			},
			{
				true, false, false, false, true, false, false, false, false, false,
				false, false, false, false, true, false, false, false
			},
			{
				true, false, false, false, true, false, false, false, false, false,
				false, false, false, false, true, false, false, false
			},
			{
				true, false, false, false, true, false, false, false, false, false,
				false, false, false, false, true, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, false, true, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, false, true, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false, false
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, false, true, false, false, false
			},
			{
				true, false, true, false, false, false, true, true, true, false,
				true, true, true, false, true, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false, false, true, true
			}
		};

		// Token: 0x02000961 RID: 2401
		internal sealed class StyleInformationAttribute
		{
			// Token: 0x0400408A RID: 16522
			public string Name;

			// Token: 0x0400408B RID: 16523
			public Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo Value;

			// Token: 0x0400408C RID: 16524
			public Microsoft.ReportingServices.ReportIntermediateFormat.ValueType ValueType;
		}
	}
}
