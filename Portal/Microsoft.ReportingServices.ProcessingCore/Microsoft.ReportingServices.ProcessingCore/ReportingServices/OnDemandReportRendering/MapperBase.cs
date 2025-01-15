using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000087 RID: 135
	internal class MapperBase : IDisposable
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x000257E7 File Offset: 0x000239E7
		internal MapperBase(string defaultFontFamily)
		{
			this.m_fontCache = new MapperBase.FontCache(string.IsNullOrEmpty(defaultFontFamily) ? MappingHelper.DefaultFontFamily : defaultFontFamily);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00025820 File Offset: 0x00023A20
		internal Font GetDefaultFont()
		{
			return this.m_fontCache.GetDefaultFont();
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002582D File Offset: 0x00023A2D
		internal Font GetDefaultFontFromCache(int id)
		{
			return this.m_fontCache.GetDefaultFontFromCache(id);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002583B File Offset: 0x00023A3B
		internal Font GetFontFromCache(int id, string fontFamily, float fontSize, FontStyles fontStyle, FontWeights fontWeight, TextDecorations textDecoration)
		{
			return this.m_fontCache.GetFontFromCache(id, fontFamily, fontSize, MappingHelper.GetStyleFontStyle(fontStyle, fontWeight, textDecoration));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00025856 File Offset: 0x00023A56
		internal Font GetFontFromCache(int id, Style style, StyleInstance styleInstance)
		{
			return this.GetFontFromCache(id, MappingHelper.GetStyleFontFamily(style, styleInstance, this.GetDefaultFont().Name), MappingHelper.GetStyleFontSize(style, styleInstance), MappingHelper.GetStyleFontStyle(style, styleInstance), MappingHelper.GetStyleFontWeight(style, styleInstance), MappingHelper.GetStyleFontTextDecoration(style, styleInstance));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002588D File Offset: 0x00023A8D
		protected Font GetFont(Style style, StyleInstance styleInstance)
		{
			return this.m_fontCache.GetFont(MappingHelper.GetStyleFontFamily(style, styleInstance, this.GetDefaultFont().Name), MappingHelper.GetStyleFontSize(style, styleInstance), MappingHelper.GetStyleFontStyle(MappingHelper.GetStyleFontStyle(style, styleInstance), MappingHelper.GetStyleFontWeight(style, styleInstance), MappingHelper.GetStyleFontTextDecoration(style, styleInstance)));
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000258CD File Offset: 0x00023ACD
		private static void ValidatePositiveValue(double value)
		{
			if (double.IsNaN(value) || value <= 0.0 || value > 1.7976931348623157E+308)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { value });
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00025909 File Offset: 0x00023B09
		private static void ValidatePositiveValue(int value)
		{
			if (value <= 0 || value > 2147483647)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { value });
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00025931 File Offset: 0x00023B31
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00025939 File Offset: 0x00023B39
		public float DpiX
		{
			internal get
			{
				return this.m_dpiX;
			}
			set
			{
				this.m_dpiX = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00025942 File Offset: 0x00023B42
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0002594A File Offset: 0x00023B4A
		public float DpiY
		{
			protected get
			{
				return this.m_dpiX;
			}
			set
			{
				this.m_dpiY = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00025953 File Offset: 0x00023B53
		protected int? WidthOverrideInPixels
		{
			get
			{
				return this.m_widthOverrideInPixels;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0002595B File Offset: 0x00023B5B
		protected int? HeightOverrideInPixels
		{
			get
			{
				return this.m_heightOverrideInPixels;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00025964 File Offset: 0x00023B64
		public double? WidthOverride
		{
			set
			{
				if (value != null)
				{
					MapperBase.ValidatePositiveValue(value.Value);
					int num = MappingHelper.ToIntPixels(value.Value, Unit.Millimeter, this.DpiX);
					MapperBase.ValidatePositiveValue(num);
					this.m_widthOverrideInPixels = new int?(num);
					return;
				}
				this.m_widthOverrideInPixels = null;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x000259BC File Offset: 0x00023BBC
		public double? HeightOverride
		{
			set
			{
				if (value != null)
				{
					MapperBase.ValidatePositiveValue(value.Value);
					int num = MappingHelper.ToIntPixels(value.Value, Unit.Millimeter, this.DpiY);
					MapperBase.ValidatePositiveValue(num);
					this.m_heightOverrideInPixels = new int?(num);
					return;
				}
				this.m_heightOverrideInPixels = null;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00025A11 File Offset: 0x00023C11
		public virtual void Dispose()
		{
			this.m_fontCache.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000224 RID: 548
		private MapperBase.FontCache m_fontCache;

		// Token: 0x04000225 RID: 549
		private float m_dpiX = 96f;

		// Token: 0x04000226 RID: 550
		private float m_dpiY = 96f;

		// Token: 0x04000227 RID: 551
		private int? m_widthOverrideInPixels;

		// Token: 0x04000228 RID: 552
		private int? m_heightOverrideInPixels;

		// Token: 0x02000921 RID: 2337
		internal class FontCache : IDisposable
		{
			// Token: 0x06007F42 RID: 32578 RVA: 0x0020CDBF File Offset: 0x0020AFBF
			internal FontCache(string defaultFontFamily)
			{
				this.m_defaultFontFamily = defaultFontFamily;
			}

			// Token: 0x06007F43 RID: 32579 RVA: 0x0020CDF4 File Offset: 0x0020AFF4
			public Font GetFontFromCache(int id, string familyName, float size, FontStyle style)
			{
				MapperBase.FontCache.KeyInfo keyInfo = new MapperBase.FontCache.KeyInfo(id, familyName, size, style);
				if (!this.m_cachedFonts.ContainsKey(keyInfo))
				{
					this.m_cachedFonts.Add(keyInfo, this.CreateSafeFont(familyName, size, style));
				}
				return this.m_cachedFonts[keyInfo];
			}

			// Token: 0x06007F44 RID: 32580 RVA: 0x0020CE3C File Offset: 0x0020B03C
			public Font GetDefaultFontFromCache(int id)
			{
				return this.GetFontFromCache(id, this.m_defaultFontFamily, MapperBase.FontCache.m_defaultFontSize, MappingHelper.GetStyleFontStyle(FontStyles.Normal, FontWeights.Normal, TextDecorations.None));
			}

			// Token: 0x06007F45 RID: 32581 RVA: 0x0020CE58 File Offset: 0x0020B058
			public Font GetFont(string familyName, float size, FontStyle style)
			{
				Font font = null;
				Font font2;
				try
				{
					font = this.CreateSafeFont(familyName, size, style);
					this.m_fonts.Add(font);
					font2 = font;
				}
				catch
				{
					if (font != null && !this.m_fonts.Contains(font))
					{
						font.Dispose();
						font = null;
					}
					throw;
				}
				return font2;
			}

			// Token: 0x06007F46 RID: 32582 RVA: 0x0020CEB0 File Offset: 0x0020B0B0
			public Font GetDefaultFont()
			{
				return this.GetFont(this.m_defaultFontFamily, MapperBase.FontCache.m_defaultFontSize, MappingHelper.GetStyleFontStyle(FontStyles.Normal, FontWeights.Normal, TextDecorations.None));
			}

			// Token: 0x06007F47 RID: 32583 RVA: 0x0020CECC File Offset: 0x0020B0CC
			private FontFamily TryCreateFontFamily(string familyName)
			{
				try
				{
					return new FontFamily(familyName);
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x06007F48 RID: 32584 RVA: 0x0020CEF8 File Offset: 0x0020B0F8
			private FontFamily RegisterFontFamilyWithFallback(string familyName)
			{
				FontFamily fontFamily = this.TryCreateFontFamily(familyName);
				if (fontFamily == null && familyName != this.m_defaultFontFamily)
				{
					fontFamily = this.TryCreateFontFamily(this.m_defaultFontFamily);
				}
				if (fontFamily == null && this.m_defaultFontFamily != "Arial")
				{
					fontFamily = this.TryCreateFontFamily("Arial");
				}
				if (fontFamily != null)
				{
					this.m_fontFamilies.Add(familyName, fontFamily);
				}
				return fontFamily;
			}

			// Token: 0x06007F49 RID: 32585 RVA: 0x0020CF60 File Offset: 0x0020B160
			private FontFamily GetFontFamily(string familyName)
			{
				FontFamily fontFamily = null;
				if (!this.m_fontFamilies.TryGetValue(familyName, out fontFamily))
				{
					fontFamily = this.RegisterFontFamilyWithFallback(familyName);
				}
				return fontFamily;
			}

			// Token: 0x06007F4A RID: 32586 RVA: 0x0020CF88 File Offset: 0x0020B188
			private Font CreateSafeFont(string familyName, float size, FontStyle fontStyle)
			{
				FontFamily fontFamily = this.GetFontFamily(familyName);
				if (fontFamily == null)
				{
					return new Font(familyName, size, fontStyle);
				}
				familyName = fontFamily.Name;
				if (fontFamily.IsStyleAvailable(fontStyle))
				{
					return new Font(familyName, size, fontStyle);
				}
				FontStyle fontStyle2 = FontStyle.Regular;
				foreach (object obj in Enum.GetValues(typeof(FontStyle)))
				{
					FontStyle fontStyle3 = (FontStyle)obj;
					if (fontFamily.IsStyleAvailable(fontStyle3))
					{
						fontStyle2 = fontStyle3;
						break;
					}
				}
				if (fontFamily.IsStyleAvailable(fontStyle | fontStyle2))
				{
					return new Font(familyName, size, fontStyle | fontStyle2);
				}
				return new Font(familyName, size, fontStyle2);
			}

			// Token: 0x06007F4B RID: 32587 RVA: 0x0020D044 File Offset: 0x0020B244
			public void Dispose()
			{
				foreach (FontFamily fontFamily in this.m_fontFamilies.Values)
				{
					fontFamily.Dispose();
				}
				foreach (Font font in this.m_cachedFonts.Values)
				{
					font.Dispose();
				}
				this.m_cachedFonts.Clear();
				foreach (Font font2 in this.m_fonts)
				{
					font2.Dispose();
				}
				this.m_fonts.Clear();
				GC.SuppressFinalize(this);
			}

			// Token: 0x04003F6E RID: 16238
			private string m_defaultFontFamily;

			// Token: 0x04003F6F RID: 16239
			private static float m_defaultFontSize = 10f;

			// Token: 0x04003F70 RID: 16240
			private Dictionary<MapperBase.FontCache.KeyInfo, Font> m_cachedFonts = new Dictionary<MapperBase.FontCache.KeyInfo, Font>(new MapperBase.FontCache.KeyInfo.EqualityComparer());

			// Token: 0x04003F71 RID: 16241
			private List<Font> m_fonts = new List<Font>();

			// Token: 0x04003F72 RID: 16242
			private Dictionary<string, FontFamily> m_fontFamilies = new Dictionary<string, FontFamily>();

			// Token: 0x02000D2F RID: 3375
			internal class TestAgent
			{
				// Token: 0x06008F4B RID: 36683 RVA: 0x00246CC9 File Offset: 0x00244EC9
				public TestAgent(MapperBase.FontCache fontCache)
				{
					this._host = fontCache;
				}

				// Token: 0x17002BF2 RID: 11250
				// (get) Token: 0x06008F4C RID: 36684 RVA: 0x00246CD8 File Offset: 0x00244ED8
				public Dictionary<string, FontFamily> FontFamilies
				{
					get
					{
						return this._host.m_fontFamilies;
					}
				}

				// Token: 0x06008F4D RID: 36685 RVA: 0x00246CE5 File Offset: 0x00244EE5
				public FontFamily TryCreateFont(string familyName)
				{
					return this._host.TryCreateFontFamily(familyName);
				}

				// Token: 0x06008F4E RID: 36686 RVA: 0x00246CF3 File Offset: 0x00244EF3
				public FontFamily RegisterFontFamilyWithFallback(string familyName)
				{
					return this._host.RegisterFontFamilyWithFallback(familyName);
				}

				// Token: 0x06008F4F RID: 36687 RVA: 0x00246D01 File Offset: 0x00244F01
				public FontFamily GetFontFamily(string familyName)
				{
					return this._host.GetFontFamily(familyName);
				}

				// Token: 0x0400506F RID: 20591
				private MapperBase.FontCache _host;
			}

			// Token: 0x02000D30 RID: 3376
			private class KeyInfo
			{
				// Token: 0x06008F50 RID: 36688 RVA: 0x00246D0F File Offset: 0x00244F0F
				public KeyInfo(int id, string family, float size, FontStyle style)
				{
					this.m_id = id;
					this.m_fontFamily = family;
					this.m_size = size;
					this.m_style = style;
				}

				// Token: 0x04005070 RID: 20592
				private int m_id;

				// Token: 0x04005071 RID: 20593
				private string m_fontFamily;

				// Token: 0x04005072 RID: 20594
				private float m_size;

				// Token: 0x04005073 RID: 20595
				private FontStyle m_style;

				// Token: 0x02000D50 RID: 3408
				public class EqualityComparer : IEqualityComparer<MapperBase.FontCache.KeyInfo>
				{
					// Token: 0x06008FE6 RID: 36838 RVA: 0x00247F48 File Offset: 0x00246148
					public bool Equals(MapperBase.FontCache.KeyInfo x, MapperBase.FontCache.KeyInfo y)
					{
						return x.m_id == y.m_id && x.m_size == y.m_size && x.m_style == y.m_style && string.Compare(x.m_fontFamily, y.m_fontFamily, StringComparison.Ordinal) == 0;
					}

					// Token: 0x06008FE7 RID: 36839 RVA: 0x00247F96 File Offset: 0x00246196
					public int GetHashCode(MapperBase.FontCache.KeyInfo obj)
					{
						return obj.m_fontFamily.GetHashCode() ^ obj.m_size.GetHashCode() ^ obj.m_id.GetHashCode() ^ obj.m_style.GetHashCode();
					}
				}
			}
		}
	}
}
