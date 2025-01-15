using System;
using System.Drawing;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000AA RID: 170
	public static class Constants
	{
		// Token: 0x04000122 RID: 290
		public const string DesignerNamespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";

		// Token: 0x04000123 RID: 291
		public const string ComponentLibraryNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition";

		// Token: 0x04000124 RID: 292
		public const string SharedDataSetNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition";

		// Token: 0x04000125 RID: 293
		public const string Microversioned2011DefinitionNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition";

		// Token: 0x04000126 RID: 294
		public const string DefinitionNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition";

		// Token: 0x04000127 RID: 295
		public const string DefaultFontFamilyNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily";

		// Token: 0x04000128 RID: 296
		public const string WebAuthoringNamespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring";

		// Token: 0x04000129 RID: 297
		public const string AuthoringMetadataNamespace = "http://schemas.microsoft.com/sqlserver/reporting/authoringmetadata";

		// Token: 0x0400012A RID: 298
		public const string AccessibilityPropertiesNamespace = "http://schemas.microsoft.com/sqlserver/reporting/accessibilityproperties";

		// Token: 0x0400012B RID: 299
		public const string MustUnderstandAttributeName = "MustUnderstand";

		// Token: 0x0400012C RID: 300
		public const char ExpressionPrefix = '=';

		// Token: 0x0400012D RID: 301
		public static readonly ReportSize DefaultPageHeight = new ReportSize(11.0, SizeTypes.Inch);

		// Token: 0x0400012E RID: 302
		public static readonly ReportSize DefaultPageWidth = new ReportSize(8.5, SizeTypes.Inch);

		// Token: 0x0400012F RID: 303
		public static readonly ReportSize DefaultColumnSpacing = new ReportSize(0.5, SizeTypes.Inch);

		// Token: 0x04000130 RID: 304
		public static readonly ReportSize DefaultEmptySize = default(ReportSize);

		// Token: 0x04000131 RID: 305
		public static readonly ReportSize DefaultZeroSize = new ReportSize(0.0);

		// Token: 0x04000132 RID: 306
		public static readonly ReportColor DefaultEmptyColor = ReportColor.Empty;

		// Token: 0x04000133 RID: 307
		public const string DefaultDefaultFontFamily = "Arial";

		// Token: 0x04000134 RID: 308
		public const string DefaultFontFamily = "Arial";

		// Token: 0x04000135 RID: 309
		public const string DefaultGaugeIndicatorNonNumericString = "-";

		// Token: 0x04000136 RID: 310
		public const string DefaultGaugeIndicatorOutOfRangeString = "#Error";

		// Token: 0x04000137 RID: 311
		public static readonly ReportSize DefaultFontSize = new ReportSize(10.0, SizeTypes.Point);

		// Token: 0x04000138 RID: 312
		public static readonly ReportSize MinimumFontSize = new ReportSize(1.0, SizeTypes.Point);

		// Token: 0x04000139 RID: 313
		public static readonly ReportSize MaximumFontSize = new ReportSize(200.0, SizeTypes.Point);

		// Token: 0x0400013A RID: 314
		public static readonly ReportSize MinimumPadding = new ReportSize(0.0, SizeTypes.Point);

		// Token: 0x0400013B RID: 315
		public static readonly ReportSize MaximumPadding = new ReportSize(1000.0, SizeTypes.Point);

		// Token: 0x0400013C RID: 316
		public static readonly ReportSize MinimumLineHeight = new ReportSize(1.0, SizeTypes.Point);

		// Token: 0x0400013D RID: 317
		public static readonly ReportSize MaximumLineHeight = new ReportSize(1000.0, SizeTypes.Point);

		// Token: 0x0400013E RID: 318
		public static readonly ReportColor DefaultColor = new ReportColor(Color.Black);

		// Token: 0x0400013F RID: 319
		public static readonly ReportColor DefaultBorderColor = new ReportColor(Color.Black);

		// Token: 0x04000140 RID: 320
		public static readonly ReportColor DefaultDigitColor = new ReportColor(Color.SteelBlue);

		// Token: 0x04000141 RID: 321
		public static readonly ReportColor DefaultDecimalDigitColor = new ReportColor(Color.Firebrick);

		// Token: 0x04000142 RID: 322
		public static readonly ReportColor DefaultSeparatorColor = new ReportColor(Color.DimGray);

		// Token: 0x04000143 RID: 323
		public static readonly ReportSize DefaultBorderWidth = new ReportSize(1.0, SizeTypes.Point);

		// Token: 0x04000144 RID: 324
		public static readonly ReportSize MinimumBorderWidth = new ReportSize(0.25, SizeTypes.Point);

		// Token: 0x04000145 RID: 325
		public static readonly ReportSize MaximumBorderWidth = new ReportSize(20.0, SizeTypes.Point);

		// Token: 0x04000146 RID: 326
		public static readonly ReportSize MaximumMargin = new ReportSize(455.0, SizeTypes.Inch);

		// Token: 0x04000147 RID: 327
		public static readonly ReportSize MinimumMargin = new ReportSize(0.0, SizeTypes.Inch);

		// Token: 0x04000148 RID: 328
		public static readonly ReportSize MinimumItemSize = new ReportSize(0.0, SizeTypes.Inch);

		// Token: 0x04000149 RID: 329
		public static readonly ReportSize MaximumItemSize = new ReportSize(455.0, SizeTypes.Inch);
	}
}
