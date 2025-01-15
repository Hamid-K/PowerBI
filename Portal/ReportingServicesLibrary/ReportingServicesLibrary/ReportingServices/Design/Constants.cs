using System;
using Microsoft.ReportingServices.Design.RdlModel;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x02000386 RID: 902
	public sealed class Constants
	{
		// Token: 0x06001DF4 RID: 7668 RVA: 0x000025F4 File Offset: 0x000007F4
		private Constants()
		{
		}

		// Token: 0x04000C9B RID: 3227
		public const char ConstExpressionPrefixChar = '=';

		// Token: 0x04000C9C RID: 3228
		public const string DefaultLayoutDirection = "LTR";

		// Token: 0x04000C9D RID: 3229
		public const string DefaultSubtotalPosition = "After";

		// Token: 0x04000C9E RID: 3230
		public const string ConstDefaultDataSourceType = "SQL";

		// Token: 0x04000C9F RID: 3231
		public const int ConstDefaultCommandTimeout = 0;

		// Token: 0x04000CA0 RID: 3232
		public static readonly Unit MaxUnits = new Unit(8192);

		// Token: 0x04000CA1 RID: 3233
		public static readonly Unit MaxSectionHeightUnits = new Unit(160.0, UnitType.Inch);

		// Token: 0x04000CA2 RID: 3234
		public const string SqlType = "SQL";

		// Token: 0x04000CA3 RID: 3235
		public const string OleDBType = "OLEDB";

		// Token: 0x04000CA4 RID: 3236
		public const string MdxType = "OLEDB-MD";

		// Token: 0x04000CA5 RID: 3237
		public const int DefaultDataSourceTypeIndex = 0;

		// Token: 0x04000CA6 RID: 3238
		public const string DefaultDataSourceType = "SQL";

		// Token: 0x04000CA7 RID: 3239
		internal static readonly string[] DataSourceTypes = new string[] { "SQL", "OLEDB-MD", "OLEDB" };

		// Token: 0x04000CA8 RID: 3240
		public const string SqlTypeDisplayString = "SQL Server";

		// Token: 0x04000CA9 RID: 3241
		public const string OleDBTypeDisplayString = "OLE DB";

		// Token: 0x04000CAA RID: 3242
		public const string MdxTypeDisplayString = "Analysis Services";

		// Token: 0x04000CAB RID: 3243
		internal static readonly string[] DataSourceTypeDisplayStrings = new string[] { "SQL Server", "Analysis Services", "OLE DB" };

		// Token: 0x04000CAC RID: 3244
		public const string AutoString = "Auto";

		// Token: 0x04000CAD RID: 3245
		public const string TrueString = "True";

		// Token: 0x04000CAE RID: 3246
		public const string FalseString = "False";

		// Token: 0x04000CAF RID: 3247
		internal static readonly string[] AutoTrueFalseTypes = new string[] { "Auto", "True", "False" };

		// Token: 0x04000CB0 RID: 3248
		internal static readonly string[] AutoTrueFalseStrings = new string[] { "Default", "True", "False" };

		// Token: 0x04000CB1 RID: 3249
		internal static readonly string[] CollationTypes = new string[]
		{
			"Default", "Albanian", "Arabic", "Chinese_PRC", "Chinese_PRC_Stroke", "Chinese_Taiwan_Bopomofo", "Chinese_Taiwan_Stroke", "Croatian", "Cyrillic_General", "Czech",
			"Danish_Norwegian", "Estonian", "Finnish_Swedish", "French", "Georgian_Modern_Sort", "German_PhoneBook", "Greek", "Hebrew", "Hindi ", "Hungarian",
			"Hungarian_Technical", "Icelandic", "Japanese", "Japanese_Unicode", "Korean_Wansung", "Korean_Wansung_Unicode", "Latin1_General", "Latvian", "Lithuanian", "Lithuanian_Classic",
			"FYRO Macedonian", "Mexican_Trad_Spanish", "Modern_Spanish", "Polish", "Romanian", "Slovak", "Slovenian", "Thai", "Turkish", "Ukrainian",
			"Vietnamese"
		};

		// Token: 0x04000CB2 RID: 3250
		public const string DefinitionNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";

		// Token: 0x04000CB3 RID: 3251
		public const string DesignerNamespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";
	}
}
