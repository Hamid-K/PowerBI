using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F3 RID: 499
	internal class SRErrorsWrapper
	{
		// Token: 0x060010B7 RID: 4279 RVA: 0x00027339 File Offset: 0x00025539
		public static string InvalidParamGreaterThan(string name, object min)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidParamGreaterThan", name, min);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00027347 File Offset: 0x00025547
		public static string InvalidParamLessThan(string name, object max)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidParamLessThan", name, max);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00027355 File Offset: 0x00025555
		public static string InvalidParamBetween(string name, object min, object max)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidParamBetween", name, min, max);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00027364 File Offset: 0x00025564
		public static string InvalidParam(string name, object value)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidParam", name, value);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00027372 File Offset: 0x00025572
		public static string InvalidIdentifier(string name)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidIdentifier", name);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0002737F File Offset: 0x0002557F
		public static string UnitParseNumericPart(string value, string numericPart, string type)
		{
			return SRErrorsWrapper.Keys.GetString("UnitParseNumericPart", value, numericPart, type);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0002738E File Offset: 0x0002558E
		public static string UnitParseNoDigits(string value)
		{
			return SRErrorsWrapper.Keys.GetString("UnitParseNoDigits", value);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002739B File Offset: 0x0002559B
		public static string UnitParseNoUnit(string value)
		{
			return SRErrorsWrapper.Keys.GetString("UnitParseNoUnit", value);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000273A8 File Offset: 0x000255A8
		public static string TextParseFailedFormat(string text, string format)
		{
			return SRErrorsWrapper.Keys.GetString("TextParseFailedFormat", text, format);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000273B6 File Offset: 0x000255B6
		public static string InvalidColor(string value)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidColor", value);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000273C3 File Offset: 0x000255C3
		public static string InvalidUnitType(string value)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidUnitType", value);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000273D0 File Offset: 0x000255D0
		public static string InvalidValue(string value)
		{
			return SRErrorsWrapper.Keys.GetString("InvalidValue", value);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000273DD File Offset: 0x000255DD
		public static string DeserializationFailedMethod(string methodName)
		{
			return SRErrorsWrapper.Keys.GetString("DeserializationFailedMethod", methodName);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000273EA File Offset: 0x000255EA
		public static string DeserializationFailed(string message)
		{
			return SRErrorsWrapper.Keys.GetString("DeserializationFailed", message);
		}

		// Token: 0x020003FA RID: 1018
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060018BC RID: 6332 RVA: 0x0003BB9F File Offset: 0x00039D9F
			private Keys()
			{
			}

			// Token: 0x1700074D RID: 1869
			// (get) Token: 0x060018BD RID: 6333 RVA: 0x0003BBA7 File Offset: 0x00039DA7
			// (set) Token: 0x060018BE RID: 6334 RVA: 0x0003BBAE File Offset: 0x00039DAE
			public static CultureInfo Culture
			{
				get
				{
					return SRErrorsWrapper.Keys._culture;
				}
				set
				{
					SRErrorsWrapper.Keys._culture = value;
				}
			}

			// Token: 0x060018BF RID: 6335 RVA: 0x0003BBB6 File Offset: 0x00039DB6
			public static string GetString(string key)
			{
				return SRErrorsWrapper.Keys.resourceManager.GetString(key, SRErrorsWrapper.Keys._culture);
			}

			// Token: 0x060018C0 RID: 6336 RVA: 0x0003BBC8 File Offset: 0x00039DC8
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrorsWrapper.Keys.resourceManager.GetString(key, SRErrorsWrapper.Keys._culture), arg0);
			}

			// Token: 0x060018C1 RID: 6337 RVA: 0x0003BBE5 File Offset: 0x00039DE5
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrorsWrapper.Keys.resourceManager.GetString(key, SRErrorsWrapper.Keys._culture), arg0, arg1);
			}

			// Token: 0x060018C2 RID: 6338 RVA: 0x0003BC03 File Offset: 0x00039E03
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrorsWrapper.Keys.resourceManager.GetString(key, SRErrorsWrapper.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x040007B0 RID: 1968
			private static ResourceManager resourceManager = SRErrors.ResourceManager;

			// Token: 0x040007B1 RID: 1969
			private static CultureInfo _culture = null;

			// Token: 0x040007B2 RID: 1970
			public const string InvalidParamGreaterThan = "InvalidParamGreaterThan";

			// Token: 0x040007B3 RID: 1971
			public const string InvalidParamLessThan = "InvalidParamLessThan";

			// Token: 0x040007B4 RID: 1972
			public const string InvalidParamBetween = "InvalidParamBetween";

			// Token: 0x040007B5 RID: 1973
			public const string InvalidParam = "InvalidParam";

			// Token: 0x040007B6 RID: 1974
			public const string InvalidIdentifier = "InvalidIdentifier";

			// Token: 0x040007B7 RID: 1975
			public const string UnitParseNumericPart = "UnitParseNumericPart";

			// Token: 0x040007B8 RID: 1976
			public const string UnitParseNoDigits = "UnitParseNoDigits";

			// Token: 0x040007B9 RID: 1977
			public const string UnitParseNoUnit = "UnitParseNoUnit";

			// Token: 0x040007BA RID: 1978
			public const string TextParseFailedFormat = "TextParseFailedFormat";

			// Token: 0x040007BB RID: 1979
			public const string InvalidColor = "InvalidColor";

			// Token: 0x040007BC RID: 1980
			public const string InvalidUnitType = "InvalidUnitType";

			// Token: 0x040007BD RID: 1981
			public const string InvalidValue = "InvalidValue";

			// Token: 0x040007BE RID: 1982
			public const string NoRootElement = "NoRootElement";

			// Token: 0x040007BF RID: 1983
			public const string DeserializationFailedMethod = "DeserializationFailedMethod";

			// Token: 0x040007C0 RID: 1984
			public const string DeserializationFailed = "DeserializationFailed";
		}
	}
}
