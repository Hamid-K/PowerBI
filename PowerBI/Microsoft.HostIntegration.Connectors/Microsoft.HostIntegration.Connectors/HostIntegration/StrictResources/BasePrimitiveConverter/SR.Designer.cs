using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.BasePrimitiveConverter
{
	// Token: 0x020004DE RID: 1246
	internal class SR
	{
		// Token: 0x06002A93 RID: 10899 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x00093710 File Offset: 0x00091910
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.BasePrimitiveConverter.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x0009373C File Offset: 0x0009193C
		// (set) Token: 0x06002A96 RID: 10902 RVA: 0x00093743 File Offset: 0x00091943
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x0009374B File Offset: 0x0009194B
		internal static string ExpectedInputNotFound
		{
			get
			{
				return SR.ResourceManager.GetString("ExpectedInputNotFound", SR.Culture);
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x00093761 File Offset: 0x00091961
		internal static string FloatMantissaOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("FloatMantissaOverflow", SR.Culture);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x00093777 File Offset: 0x00091977
		internal static string FloatExponentOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("FloatExponentOverflow", SR.Culture);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0009378D File Offset: 0x0009198D
		internal static string DoubleMantissaOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("DoubleMantissaOverflow", SR.Culture);
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000937A3 File Offset: 0x000919A3
		internal static string DoubleExponentOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("DoubleExponentOverflow", SR.Culture);
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x000937B9 File Offset: 0x000919B9
		internal static string NumericOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("NumericOverflow", SR.Culture);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x000937CF File Offset: 0x000919CF
		internal static string DecimalOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("DecimalOverflow", SR.Culture);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x000937E5 File Offset: 0x000919E5
		internal static string UnsignedWindows
		{
			get
			{
				return SR.ResourceManager.GetString("UnsignedWindows", SR.Culture);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x000937FB File Offset: 0x000919FB
		internal static string ValueNegativeNoSeparateSign
		{
			get
			{
				return SR.ResourceManager.GetString("ValueNegativeNoSeparateSign", SR.Culture);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x00093811 File Offset: 0x00091A11
		internal static string UnsignedHost
		{
			get
			{
				return SR.ResourceManager.GetString("UnsignedHost", SR.Culture);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x00093827 File Offset: 0x00091A27
		internal static string BadPackedDec
		{
			get
			{
				return SR.ResourceManager.GetString("BadPackedDec", SR.Culture);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x0009383D File Offset: 0x00091A3D
		internal static string BadZonedDec
		{
			get
			{
				return SR.ResourceManager.GetString("BadZonedDec", SR.Culture);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x00093853 File Offset: 0x00091A53
		internal static string NotSignSeparateOrUnsigned
		{
			get
			{
				return SR.ResourceManager.GetString("NotSignSeparateOrUnsigned", SR.Culture);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x00093869 File Offset: 0x00091A69
		internal static string ScaleTooLarge
		{
			get
			{
				return SR.ResourceManager.GetString("ScaleTooLarge", SR.Culture);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x0009387F File Offset: 0x00091A7F
		internal static string UnsupportedCharConversion
		{
			get
			{
				return SR.ResourceManager.GetString("UnsupportedCharConversion", SR.Culture);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x00093895 File Offset: 0x00091A95
		internal static string DateConvertError2
		{
			get
			{
				return SR.ResourceManager.GetString("DateConvertError2", SR.Culture);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x000938AB File Offset: 0x00091AAB
		internal static string CharStringTooBig
		{
			get
			{
				return SR.ResourceManager.GetString("CharStringTooBig", SR.Culture);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000938C1 File Offset: 0x00091AC1
		internal static string OutputBufferOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("OutputBufferOverflow", SR.Culture);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000938D7 File Offset: 0x00091AD7
		internal static string DateConvertError
		{
			get
			{
				return SR.ResourceManager.GetString("DateConvertError", SR.Culture);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000938ED File Offset: 0x00091AED
		internal static string DateConvertError3
		{
			get
			{
				return SR.ResourceManager.GetString("DateConvertError3", SR.Culture);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x00093903 File Offset: 0x00091B03
		internal static string DateConvertError4
		{
			get
			{
				return SR.ResourceManager.GetString("DateConvertError4", SR.Culture);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x00093919 File Offset: 0x00091B19
		internal static string PrecisionOverflow
		{
			get
			{
				return SR.ResourceManager.GetString("PrecisionOverflow", SR.Culture);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x0009392F File Offset: 0x00091B2F
		internal static string InputBufferExhausted
		{
			get
			{
				return SR.ResourceManager.GetString("InputBufferExhausted", SR.Culture);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x00093945 File Offset: 0x00091B45
		internal static string PrecisionTooLarge
		{
			get
			{
				return SR.ResourceManager.GetString("PrecisionTooLarge", SR.Culture);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x0009395B File Offset: 0x00091B5B
		internal static string PrecisionLoss
		{
			get
			{
				return SR.ResourceManager.GetString("PrecisionLoss", SR.Culture);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x00093971 File Offset: 0x00091B71
		internal static string StringTooShortPaddingNotAllowed
		{
			get
			{
				return SR.ResourceManager.GetString("StringTooShortPaddingNotAllowed", SR.Culture);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x00093987 File Offset: 0x00091B87
		internal static string InvalidDateEditMask
		{
			get
			{
				return SR.ResourceManager.GetString("InvalidDateEditMask", SR.Culture);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x0009399D File Offset: 0x00091B9D
		internal static string CorruptedHashTable
		{
			get
			{
				return SR.ResourceManager.GetString("CorruptedHashTable", SR.Culture);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000939B3 File Offset: 0x00091BB3
		internal static string DecimalFloatFailedInfinityNegative
		{
			get
			{
				return SR.ResourceManager.GetString("DecimalFloatFailedInfinityNegative", SR.Culture);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x000939C9 File Offset: 0x00091BC9
		internal static string DecimalFloatFailedInfinityPositive
		{
			get
			{
				return SR.ResourceManager.GetString("DecimalFloatFailedInfinityPositive", SR.Culture);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000939DF File Offset: 0x00091BDF
		internal static string DecimalFloatFailedNaN
		{
			get
			{
				return SR.ResourceManager.GetString("DecimalFloatFailedNaN", SR.Culture);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x000939F5 File Offset: 0x00091BF5
		internal static string DecimalFloatFailedSignificand
		{
			get
			{
				return SR.ResourceManager.GetString("DecimalFloatFailedSignificand", SR.Culture);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00093A0B File Offset: 0x00091C0B
		internal static string ToDecimalFloatFailedSignificand
		{
			get
			{
				return SR.ResourceManager.GetString("ToDecimalFloatFailedSignificand", SR.Culture);
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x00093A21 File Offset: 0x00091C21
		internal static string InvalidDBCSString
		{
			get
			{
				return SR.ResourceManager.GetString("InvalidDBCSString", SR.Culture);
			}
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x00093A37 File Offset: 0x00091C37
		internal static string ExceptionOccurred(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ExceptionOccurred", SR.Culture), param0);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x00093A58 File Offset: 0x00091C58
		internal static string InvalidCodepage(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidCodepage", SR.Culture), param0);
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x00093A79 File Offset: 0x00091C79
		internal static string CharConversionFailed(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("CharConversionFailed", SR.Culture), param0);
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x00093A9A File Offset: 0x00091C9A
		internal static string BadInputNumericEdited(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("BadInputNumericEdited", SR.Culture), param0, param1, param2);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00093ABD File Offset: 0x00091CBD
		internal static string UnsupportedConversion(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnsupportedConversion", SR.Culture), param0);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00093ADE File Offset: 0x00091CDE
		internal static string InvalidEditedDate(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidEditedDate", SR.Culture), param0);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x00093AFF File Offset: 0x00091CFF
		internal static string SbcsConversionLengthError(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("SbcsConversionLengthError", SR.Culture), param0, param1);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00093B21 File Offset: 0x00091D21
		internal static string DbcsLengthError(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DbcsLengthError", SR.Culture), param0, param1);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00093B43 File Offset: 0x00091D43
		internal static string DbcsConversionLengthError(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DbcsConversionLengthError", SR.Culture), param0, param1, param2);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00093B66 File Offset: 0x00091D66
		internal static string MbcsConversionLengthError(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("MbcsConversionLengthError", SR.Culture), param0, param1, param2);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00093B89 File Offset: 0x00091D89
		internal static string InvalidEditedDateCharacter(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidEditedDateCharacter", SR.Culture), param0, param1);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00093BAB File Offset: 0x00091DAB
		internal static string InvalidEditedTimeSpan(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidEditedTimeSpan", SR.Culture), param0);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x00093BCC File Offset: 0x00091DCC
		internal static string InvalidEditedTimeSpanCharacter(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidEditedTimeSpanCharacter", SR.Culture), param0, param1);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x00093BEE File Offset: 0x00091DEE
		internal static string DecimalFloatFailedPositiveExponent(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DecimalFloatFailedPositiveExponent", SR.Culture), param0);
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00093C0F File Offset: 0x00091E0F
		internal static string DecimalFloatFailedExponent(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DecimalFloatFailedExponent", SR.Culture), param0);
		}

		// Token: 0x040019E7 RID: 6631
		private static ResourceManager resourceManager;

		// Token: 0x040019E8 RID: 6632
		private static CultureInfo resourceCulture;
	}
}
