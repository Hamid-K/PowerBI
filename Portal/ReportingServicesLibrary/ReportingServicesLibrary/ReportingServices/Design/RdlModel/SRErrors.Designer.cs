using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200042E RID: 1070
	[CompilerGenerated]
	internal class SRErrors
	{
		// Token: 0x060022B3 RID: 8883 RVA: 0x000025F4 File Offset: 0x000007F4
		protected SRErrors()
		{
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0008250D File Offset: 0x0008070D
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x00082514 File Offset: 0x00080714
		public static CultureInfo Culture
		{
			get
			{
				return SRErrors.Keys.Culture;
			}
			set
			{
				SRErrors.Keys.Culture = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0008251C File Offset: 0x0008071C
		public static string NoRootElement
		{
			get
			{
				return SRErrors.Keys.GetString("NoRootElement");
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00082528 File Offset: 0x00080728
		public static string InvalidParamGreaterThan(object min)
		{
			return SRErrors.Keys.GetString("InvalidParamGreaterThan", min);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00082535 File Offset: 0x00080735
		public static string InvalidParamLessThan(object max)
		{
			return SRErrors.Keys.GetString("InvalidParamLessThan", max);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00082542 File Offset: 0x00080742
		public static string InvalidParamBetween(object min, object max)
		{
			return SRErrors.Keys.GetString("InvalidParamBetween", min, max);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00082550 File Offset: 0x00080750
		public static string InvalidIdentifier(string name)
		{
			return SRErrors.Keys.GetString("InvalidIdentifier", name);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0008255D File Offset: 0x0008075D
		public static string UnitParseNumericPart(string value, string numericPart, string type)
		{
			return SRErrors.Keys.GetString("UnitParseNumericPart", value, numericPart, type);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0008256C File Offset: 0x0008076C
		public static string UnitParseNoDigits(string value)
		{
			return SRErrors.Keys.GetString("UnitParseNoDigits", value);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00082579 File Offset: 0x00080779
		public static string UnitParseNoUnit(string value)
		{
			return SRErrors.Keys.GetString("UnitParseNoUnit", value);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00082586 File Offset: 0x00080786
		public static string TextParseFailedFormat(string text, string format)
		{
			return SRErrors.Keys.GetString("TextParseFailedFormat", text, format);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00082594 File Offset: 0x00080794
		public static string InvalidColor(string value)
		{
			return SRErrors.Keys.GetString("InvalidColor", value);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000825A1 File Offset: 0x000807A1
		public static string InvalidUnitType(string value)
		{
			return SRErrors.Keys.GetString("InvalidUnitType", value);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000825AE File Offset: 0x000807AE
		public static string InvalidValue(string value)
		{
			return SRErrors.Keys.GetString("InvalidValue", value);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000825BB File Offset: 0x000807BB
		public static string DeserializationFailedMethod(string methodName)
		{
			return SRErrors.Keys.GetString("DeserializationFailedMethod", methodName);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000825C8 File Offset: 0x000807C8
		public static string DeserializationFailed(string message)
		{
			return SRErrors.Keys.GetString("DeserializationFailed", message);
		}

		// Token: 0x0200052E RID: 1326
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06002541 RID: 9537 RVA: 0x000025F4 File Offset: 0x000007F4
			private Keys()
			{
			}

			// Token: 0x17000ABD RID: 2749
			// (get) Token: 0x06002542 RID: 9538 RVA: 0x00087E76 File Offset: 0x00086076
			// (set) Token: 0x06002543 RID: 9539 RVA: 0x00087E7D File Offset: 0x0008607D
			public static CultureInfo Culture
			{
				get
				{
					return SRErrors.Keys._culture;
				}
				set
				{
					SRErrors.Keys._culture = value;
				}
			}

			// Token: 0x06002544 RID: 9540 RVA: 0x00087E85 File Offset: 0x00086085
			public static string GetString(string key)
			{
				return SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture);
			}

			// Token: 0x06002545 RID: 9541 RVA: 0x00087E97 File Offset: 0x00086097
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), arg0);
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x00087EB4 File Offset: 0x000860B4
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), arg0, arg1);
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x00087ED2 File Offset: 0x000860D2
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04001359 RID: 4953
			private static ResourceManager resourceManager = new ResourceManager(typeof(SRErrors).FullName, typeof(SRErrors).Module.Assembly);

			// Token: 0x0400135A RID: 4954
			private static CultureInfo _culture = null;

			// Token: 0x0400135B RID: 4955
			public const string InvalidParamGreaterThan = "InvalidParamGreaterThan";

			// Token: 0x0400135C RID: 4956
			public const string InvalidParamLessThan = "InvalidParamLessThan";

			// Token: 0x0400135D RID: 4957
			public const string InvalidParamBetween = "InvalidParamBetween";

			// Token: 0x0400135E RID: 4958
			public const string InvalidIdentifier = "InvalidIdentifier";

			// Token: 0x0400135F RID: 4959
			public const string UnitParseNumericPart = "UnitParseNumericPart";

			// Token: 0x04001360 RID: 4960
			public const string UnitParseNoDigits = "UnitParseNoDigits";

			// Token: 0x04001361 RID: 4961
			public const string UnitParseNoUnit = "UnitParseNoUnit";

			// Token: 0x04001362 RID: 4962
			public const string TextParseFailedFormat = "TextParseFailedFormat";

			// Token: 0x04001363 RID: 4963
			public const string InvalidColor = "InvalidColor";

			// Token: 0x04001364 RID: 4964
			public const string InvalidUnitType = "InvalidUnitType";

			// Token: 0x04001365 RID: 4965
			public const string InvalidValue = "InvalidValue";

			// Token: 0x04001366 RID: 4966
			public const string NoRootElement = "NoRootElement";

			// Token: 0x04001367 RID: 4967
			public const string DeserializationFailedMethod = "DeserializationFailedMethod";

			// Token: 0x04001368 RID: 4968
			public const string DeserializationFailed = "DeserializationFailed";
		}
	}
}
