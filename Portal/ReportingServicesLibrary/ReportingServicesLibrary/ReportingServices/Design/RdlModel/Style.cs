using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200040F RID: 1039
	public sealed class Style
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00080242 File Offset: 0x0007E442
		[XmlIgnore]
		public bool IsFontItalic
		{
			get
			{
				return this.FontStyle == "Italic";
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00080254 File Offset: 0x0007E454
		[XmlIgnore]
		public bool IsFontBold
		{
			get
			{
				string fontWeight = this.FontWeight;
				return !string.IsNullOrEmpty(fontWeight) && ((string.Compare(fontWeight, "600", StringComparison.Ordinal) >= 0 && string.Compare(fontWeight, "900", StringComparison.Ordinal) <= 0) || fontWeight.StartsWith("Bold", StringComparison.OrdinalIgnoreCase));
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000802A3 File Offset: 0x0007E4A3
		[XmlIgnore]
		public bool IsFontStrikeout
		{
			get
			{
				return this.TextDecoration == "LineThrough";
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x000802B5 File Offset: 0x0007E4B5
		[XmlIgnore]
		public bool IsFontUnderline
		{
			get
			{
				return this.TextDecoration == "Underline";
			}
		}

		// Token: 0x04000E5F RID: 3679
		public ReportColor Color = new ReportColor(Style.Definition.Color);

		// Token: 0x04000E60 RID: 3680
		public ReportColor BackgroundColor = new ReportColor(Style.Definition.BackgroundColor);

		// Token: 0x04000E61 RID: 3681
		public ReportColor BackgroundGradientEndColor = new ReportColor(Style.Definition.BackgroundColor);

		// Token: 0x04000E62 RID: 3682
		public StyleUnit FontSize = new StyleUnit(Style.Definition.FontSize);

		// Token: 0x04000E63 RID: 3683
		public StyleUnit LineHeight = new StyleUnit(Style.Definition.LineHeight);

		// Token: 0x04000E64 RID: 3684
		public StyleUnit PaddingLeft = new StyleUnit(Style.Definition.Padding);

		// Token: 0x04000E65 RID: 3685
		public StyleUnit PaddingRight = new StyleUnit(Style.Definition.Padding);

		// Token: 0x04000E66 RID: 3686
		public StyleUnit PaddingTop = new StyleUnit(Style.Definition.Padding);

		// Token: 0x04000E67 RID: 3687
		public StyleUnit PaddingBottom = new StyleUnit(Style.Definition.Padding);

		// Token: 0x04000E68 RID: 3688
		public BorderColor BorderColor = new BorderColor();

		// Token: 0x04000E69 RID: 3689
		public BorderStyle BorderStyle = new BorderStyle();

		// Token: 0x04000E6A RID: 3690
		public BorderWidth BorderWidth = new BorderWidth();

		// Token: 0x04000E6B RID: 3691
		[DefaultValue("None")]
		public string BackgroundGradientType;

		// Token: 0x04000E6C RID: 3692
		[DefaultValue(typeof(BackgroundImage), "")]
		public BackgroundImage BackgroundImage;

		// Token: 0x04000E6D RID: 3693
		[DefaultValue("Normal")]
		public string FontStyle = "Normal";

		// Token: 0x04000E6E RID: 3694
		[DefaultValue("Arial")]
		public string FontFamily = "Arial";

		// Token: 0x04000E6F RID: 3695
		[DefaultValue("Normal")]
		public string FontWeight;

		// Token: 0x04000E70 RID: 3696
		[DefaultValue("")]
		public string Format;

		// Token: 0x04000E71 RID: 3697
		[DefaultValue("None")]
		public string TextDecoration = "None";

		// Token: 0x04000E72 RID: 3698
		[DefaultValue("General")]
		public string TextAlign = "General";

		// Token: 0x04000E73 RID: 3699
		[DefaultValue("Top")]
		public string VerticalAlign = "Top";

		// Token: 0x04000E74 RID: 3700
		public string Language;

		// Token: 0x04000E75 RID: 3701
		public string NumeralLanguage;

		// Token: 0x04000E76 RID: 3702
		[DefaultValue("LTR")]
		public string Direction = "LTR";

		// Token: 0x04000E77 RID: 3703
		[DefaultValue("lr-tb")]
		public string WritingMode = "lr-tb";

		// Token: 0x04000E78 RID: 3704
		[DefaultValue("Normal")]
		public string UnicodeBiDi = "Normal";

		// Token: 0x04000E79 RID: 3705
		[DefaultValue("Gregorian")]
		public string Calendar = "Gregorian";

		// Token: 0x04000E7A RID: 3706
		[DefaultValue("1")]
		public string NumeralVariant = "1";

		// Token: 0x02000522 RID: 1314
		public static class GradientTypes
		{
			// Token: 0x0400129F RID: 4767
			public const string None = "None";

			// Token: 0x040012A0 RID: 4768
			public const string LeftRight = "LeftRight";

			// Token: 0x040012A1 RID: 4769
			public const string TopBottom = "TopBottom";

			// Token: 0x040012A2 RID: 4770
			public const string Center = "Center";

			// Token: 0x040012A3 RID: 4771
			public const string DiagonalLeft = "DiagonalLeft";

			// Token: 0x040012A4 RID: 4772
			public const string DiagonalRight = "DiagonalRight";

			// Token: 0x040012A5 RID: 4773
			public const string HorizontalCenter = "HorizontalCenter";

			// Token: 0x040012A6 RID: 4774
			public const string VerticalCenter = "VerticalCenter";
		}

		// Token: 0x02000523 RID: 1315
		public static class TextAlignTypes
		{
			// Token: 0x040012A7 RID: 4775
			public const string General = "General";

			// Token: 0x040012A8 RID: 4776
			public const string Left = "Left";

			// Token: 0x040012A9 RID: 4777
			public const string Center = "Center";

			// Token: 0x040012AA RID: 4778
			public const string Right = "Right";
		}

		// Token: 0x02000524 RID: 1316
		public static class VerticalAlignTypes
		{
			// Token: 0x040012AB RID: 4779
			public const string Top = "Top";

			// Token: 0x040012AC RID: 4780
			public const string Middle = "Middle";

			// Token: 0x040012AD RID: 4781
			public const string Bottom = "Bottom";
		}

		// Token: 0x02000525 RID: 1317
		public class Definition
		{
			// Token: 0x06002522 RID: 9506 RVA: 0x000025F4 File Offset: 0x000007F4
			protected Definition()
			{
			}

			// Token: 0x040012AE RID: 4782
			public static readonly ColorExprPropertyDef Color = new ColorExprPropertyDef("Color", global::System.Drawing.Color.Black);

			// Token: 0x040012AF RID: 4783
			public static readonly ColorExprPropertyDef BackgroundColor = new ColorExprPropertyDef("BackgroundColor", global::System.Drawing.Color.Transparent);

			// Token: 0x040012B0 RID: 4784
			public static readonly ColorExprPropertyDef BorderColor = new ColorExprPropertyDef("BorderColor", global::System.Drawing.Color.Black);

			// Token: 0x040012B1 RID: 4785
			public static readonly UnitExprPropertyDef BorderWidth = new UnitExprPropertyDef("BorderWidth", new Unit(0.25, UnitType.Point), new Unit(20.0, UnitType.Point), new Unit(1.0, UnitType.Point));

			// Token: 0x040012B2 RID: 4786
			public static readonly UnitExprPropertyDef FontSize = new UnitExprPropertyDef("FontSize", new Unit(1.0, UnitType.Point), new Unit(200.0, UnitType.Point), new Unit(10.0, UnitType.Point));

			// Token: 0x040012B3 RID: 4787
			public static readonly StringExprPropertyDef FontFamily = new StringExprPropertyDef("FontFamily", "Arial");

			// Token: 0x040012B4 RID: 4788
			public static readonly UnitExprPropertyDef Padding = new UnitExprPropertyDef("Padding", new Unit(0.0, UnitType.Point), new Unit(1000.0, UnitType.Point), new Unit(0.0, UnitType.Point));

			// Token: 0x040012B5 RID: 4789
			public static readonly UnitExprPropertyDef LineHeight = new UnitExprPropertyDef("LineHeight", new Unit(1.0, UnitType.Point), new Unit(1000.0, UnitType.Point), default(Unit));

			// Token: 0x040012B6 RID: 4790
			public static readonly StringExprPropertyDef Language = new StringExprPropertyDef("Language", null);

			// Token: 0x040012B7 RID: 4791
			public static readonly StringExprPropertyDef NumeralLanguage = new StringExprPropertyDef("NumeralLanguage", null);

			// Token: 0x040012B8 RID: 4792
			public static readonly EnumExprPropertyDef GradientType = new EnumExprPropertyDef("GradientType", "None", new string[] { "None", "LeftRight", "TopBottom", "Center", "DiagonalLeft", "DiagonalRight", "HorizontalCenter", "VerticalCenter" });

			// Token: 0x040012B9 RID: 4793
			public static readonly EnumExprPropertyDef TextAlign = new EnumExprPropertyDef("TextAlign", "General", new string[] { "General", "Left", "Center", "Right" });

			// Token: 0x040012BA RID: 4794
			public static readonly EnumExprPropertyDef VerticalAlign = new EnumExprPropertyDef("VerticalAlign", "Top", new string[] { "Top", "Middle", "Bottom" });
		}
	}
}
