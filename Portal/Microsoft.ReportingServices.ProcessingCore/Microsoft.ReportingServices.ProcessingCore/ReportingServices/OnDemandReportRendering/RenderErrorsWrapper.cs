using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000076 RID: 118
	[CompilerGenerated]
	internal class RenderErrorsWrapper
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0001BB19 File Offset: 0x00019D19
		public static string rrInvalidPageNumber(int totalNumPages)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidPageNumber", totalNumPages);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001BB2B File Offset: 0x00019D2B
		public static string rrExpectedTopLevelElement(string elementName)
		{
			return RenderErrorsWrapper.Keys.GetString("rrExpectedTopLevelElement", elementName);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001BB38 File Offset: 0x00019D38
		public static string rrInvalidDeviceInfo(string detail)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidDeviceInfo", detail);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001BB45 File Offset: 0x00019D45
		public static string rrInvalidParamValue(string paramName)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidParamValue", paramName);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001BB52 File Offset: 0x00019D52
		public static string rrExpectedEndElement(string elementName)
		{
			return RenderErrorsWrapper.Keys.GetString("rrExpectedEndElement", elementName);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001BB5F File Offset: 0x00019D5F
		public static string rrInvalidColor(string color)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidColor", color);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001BB6C File Offset: 0x00019D6C
		public static string rrInvalidSize(string size)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidSize", size);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001BB79 File Offset: 0x00019D79
		public static string rrInvalidMeasurementUnit(string size)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidMeasurementUnit", size);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001BB86 File Offset: 0x00019D86
		public static string rrNegativeSize(string size)
		{
			return RenderErrorsWrapper.Keys.GetString("rrNegativeSize", size);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001BB93 File Offset: 0x00019D93
		public static string rrOutOfRange(string size)
		{
			return RenderErrorsWrapper.Keys.GetString("rrOutOfRange", size);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		public static string rrInvalidStyleArgumentType(string argumentType)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidStyleArgumentType", argumentType);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001BBAD File Offset: 0x00019DAD
		public static string rrInvalidBorderStyle(string style)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidBorderStyle", style);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001BBBA File Offset: 0x00019DBA
		public static string rrInvalidMimeType(string mimeType)
		{
			return RenderErrorsWrapper.Keys.GetString("rrInvalidMimeType", mimeType);
		}

		// Token: 0x02000916 RID: 2326
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06007F16 RID: 32534 RVA: 0x0020C52D File Offset: 0x0020A72D
			private Keys()
			{
			}

			// Token: 0x17002958 RID: 10584
			// (get) Token: 0x06007F17 RID: 32535 RVA: 0x0020C535 File Offset: 0x0020A735
			// (set) Token: 0x06007F18 RID: 32536 RVA: 0x0020C53C File Offset: 0x0020A73C
			public static CultureInfo Culture
			{
				get
				{
					return RenderErrorsWrapper.Keys._culture;
				}
				set
				{
					RenderErrorsWrapper.Keys._culture = value;
				}
			}

			// Token: 0x06007F19 RID: 32537 RVA: 0x0020C544 File Offset: 0x0020A744
			public static string GetString(string key)
			{
				return RenderErrorsWrapper.Keys.resourceManager.GetString(key, RenderErrorsWrapper.Keys._culture);
			}

			// Token: 0x06007F1A RID: 32538 RVA: 0x0020C556 File Offset: 0x0020A756
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RenderErrorsWrapper.Keys.resourceManager.GetString(key, RenderErrorsWrapper.Keys._culture), arg0);
			}

			// Token: 0x04003F09 RID: 16137
			private static ResourceManager resourceManager = RenderErrors.ResourceManager;

			// Token: 0x04003F0A RID: 16138
			private static CultureInfo _culture = null;

			// Token: 0x04003F0B RID: 16139
			public const string rrInvalidPageNumber = "rrInvalidPageNumber";

			// Token: 0x04003F0C RID: 16140
			public const string rrRenderStyleError = "rrRenderStyleError";

			// Token: 0x04003F0D RID: 16141
			public const string rrRenderSectionInstanceError = "rrRenderSectionInstanceError";

			// Token: 0x04003F0E RID: 16142
			public const string rrRenderResultNull = "rrRenderResultNull";

			// Token: 0x04003F0F RID: 16143
			public const string rrRenderStreamNull = "rrRenderStreamNull";

			// Token: 0x04003F10 RID: 16144
			public const string rrRenderDeviceNull = "rrRenderDeviceNull";

			// Token: 0x04003F11 RID: 16145
			public const string rrRenderReportNull = "rrRenderReportNull";

			// Token: 0x04003F12 RID: 16146
			public const string rrRenderReportNameNull = "rrRenderReportNameNull";

			// Token: 0x04003F13 RID: 16147
			public const string rrRenderUnknownReportItem = "rrRenderUnknownReportItem";

			// Token: 0x04003F14 RID: 16148
			public const string rrRenderStyleName = "rrRenderStyleName";

			// Token: 0x04003F15 RID: 16149
			public const string rrRenderTextBox = "rrRenderTextBox";

			// Token: 0x04003F16 RID: 16150
			public const string rrRenderingError = "rrRenderingError";

			// Token: 0x04003F17 RID: 16151
			public const string rrUnexpectedError = "rrUnexpectedError";

			// Token: 0x04003F18 RID: 16152
			public const string rrControlInvalidTag = "rrControlInvalidTag";

			// Token: 0x04003F19 RID: 16153
			public const string rrPageNamespaceInvalid = "rrPageNamespaceInvalid";

			// Token: 0x04003F1A RID: 16154
			public const string rrInvalidAttribute = "rrInvalidAttribute";

			// Token: 0x04003F1B RID: 16155
			public const string rrInvalidProperty = "rrInvalidProperty";

			// Token: 0x04003F1C RID: 16156
			public const string rrInvalidStyleName = "rrInvalidStyleName";

			// Token: 0x04003F1D RID: 16157
			public const string rrInvalidControl = "rrInvalidControl";

			// Token: 0x04003F1E RID: 16158
			public const string rrParameterExpected = "rrParameterExpected";

			// Token: 0x04003F1F RID: 16159
			public const string rrExpectedTopLevelElement = "rrExpectedTopLevelElement";

			// Token: 0x04003F20 RID: 16160
			public const string rrInvalidDeviceInfo = "rrInvalidDeviceInfo";

			// Token: 0x04003F21 RID: 16161
			public const string rrInvalidParamValue = "rrInvalidParamValue";

			// Token: 0x04003F22 RID: 16162
			public const string rrExpectedEndElement = "rrExpectedEndElement";

			// Token: 0x04003F23 RID: 16163
			public const string rrReportNameNull = "rrReportNameNull";

			// Token: 0x04003F24 RID: 16164
			public const string rrReportParamsNull = "rrReportParamsNull";

			// Token: 0x04003F25 RID: 16165
			public const string rrRendererParamsNull = "rrRendererParamsNull";

			// Token: 0x04003F26 RID: 16166
			public const string rrMeasurementUnitError = "rrMeasurementUnitError";

			// Token: 0x04003F27 RID: 16167
			public const string rrInvalidOWCRequest = "rrInvalidOWCRequest";

			// Token: 0x04003F28 RID: 16168
			public const string rrInvalidColor = "rrInvalidColor";

			// Token: 0x04003F29 RID: 16169
			public const string rrInvalidSize = "rrInvalidSize";

			// Token: 0x04003F2A RID: 16170
			public const string rrInvalidMeasurementUnit = "rrInvalidMeasurementUnit";

			// Token: 0x04003F2B RID: 16171
			public const string rrNegativeSize = "rrNegativeSize";

			// Token: 0x04003F2C RID: 16172
			public const string rrOutOfRange = "rrOutOfRange";

			// Token: 0x04003F2D RID: 16173
			public const string rrInvalidStyleArgumentType = "rrInvalidStyleArgumentType";

			// Token: 0x04003F2E RID: 16174
			public const string rrInvalidBorderStyle = "rrInvalidBorderStyle";

			// Token: 0x04003F2F RID: 16175
			public const string rrInvalidUniqueName = "rrInvalidUniqueName";

			// Token: 0x04003F30 RID: 16176
			public const string rrInvalidActionLabel = "rrInvalidActionLabel";

			// Token: 0x04003F31 RID: 16177
			public const string rrInvalidMimeType = "rrInvalidMimeType";
		}
	}
}
