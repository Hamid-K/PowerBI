using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x02000232 RID: 562
	internal class FunctionDescriptionStrings
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00024863 File Offset: 0x00022A63
		public static ResourceManager ResourceManager
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.Resources;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0002486A File Offset: 0x00022A6A
		public static string _Pound_binary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_binary");
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00024876 File Offset: 0x00022A76
		public static string _Pound_binary_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_binary_Description");
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00024882 File Offset: 0x00022A82
		public static string _Pound_binary_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_binary_Example1");
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002488E File Offset: 0x00022A8E
		public static string _Pound_binary_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_binary_Example2");
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002489A File Offset: 0x00022A9A
		public static string _Pound_date
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_date");
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000248A6 File Offset: 0x00022AA6
		public static string _Pound_date_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_date_Description");
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000248B2 File Offset: 0x00022AB2
		public static string _Pound_datetime
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_datetime");
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000248BE File Offset: 0x00022ABE
		public static string _Pound_datetime_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_datetime_Description");
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000248CA File Offset: 0x00022ACA
		public static string _Pound_datetimezone
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_datetimezone");
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000248D6 File Offset: 0x00022AD6
		public static string _Pound_datetimezone_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_datetimezone_Description");
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000248E2 File Offset: 0x00022AE2
		public static string _Pound_duration
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_duration");
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000248EE File Offset: 0x00022AEE
		public static string _Pound_duration_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_duration_Description");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000248FA File Offset: 0x00022AFA
		public static string _Pound_table
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table");
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00024906 File Offset: 0x00022B06
		public static string _Pound_table_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00024920 File Offset: 0x00022B20
		public static string _Pound_table_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table_Example1");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002492C File Offset: 0x00022B2C
		public static string _Pound_table_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table_Example2");
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00024938 File Offset: 0x00022B38
		public static string _Pound_table_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table_Example3");
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00024944 File Offset: 0x00022B44
		public static string _Pound_table_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table_Example4");
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00024950 File Offset: 0x00022B50
		public static string _Pound_table_Example5
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_table_Example5");
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002495C File Offset: 0x00022B5C
		public static string _Pound_time
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_time");
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00024968 File Offset: 0x00022B68
		public static string _Pound_time_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("_Pound_time_Description");
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00024974 File Offset: 0x00022B74
		public static string Date_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_FromText");
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00024980 File Offset: 0x00022B80
		public static string Date_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002499A File Offset: 0x00022B9A
		public static string Date_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_FromText_Example1");
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000249A6 File Offset: 0x00022BA6
		public static string Date_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_FromText_Example2");
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000249B2 File Offset: 0x00022BB2
		public static string Date_FromText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_FromText_Example3");
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000249BE File Offset: 0x00022BBE
		public static string Date_ToRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToRecord");
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000249CA File Offset: 0x00022BCA
		public static string Date_ToRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToRecord_Description", new object[] { p0 });
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x000249E0 File Offset: 0x00022BE0
		public static string Date_ToRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToRecord_Example1");
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000249EC File Offset: 0x00022BEC
		public static string DateTimeZone_LocalNow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_LocalNow");
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000249F8 File Offset: 0x00022BF8
		public static string DateTimeZone_LocalNow_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_LocalNow_Description");
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00024A04 File Offset: 0x00022C04
		public static string DateTimeZone_FixedLocalNow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FixedLocalNow");
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00024A10 File Offset: 0x00022C10
		public static string DateTimeZone_FixedLocalNow_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FixedLocalNow_Description");
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00024A1C File Offset: 0x00022C1C
		public static string Duration_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_ToText");
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00024A28 File Offset: 0x00022C28
		public static string Duration_ToText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_ToText_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00024A42 File Offset: 0x00022C42
		public static string Duration_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_ToText_Example1");
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00024A4E File Offset: 0x00022C4E
		public static string Duration_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_FromText");
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00024A5A File Offset: 0x00022C5A
		public static string Duration_FromText_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_FromText_Description", new object[] { p0 });
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00024A70 File Offset: 0x00022C70
		public static string Duration_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_FromText_Example1");
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00024A7C File Offset: 0x00022C7C
		public static string Duration_ToRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_ToRecord");
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00024A88 File Offset: 0x00022C88
		public static string Duration_ToRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_ToRecord_Description", new object[] { p0 });
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00024A9E File Offset: 0x00022C9E
		public static string Duration_ToRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_ToRecord_Example1");
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00024AAA File Offset: 0x00022CAA
		public static string Date_DayOfWeekName
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeekName");
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00024AB6 File Offset: 0x00022CB6
		public static string Date_DayOfWeekName_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeekName_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00024AD0 File Offset: 0x00022CD0
		public static string Date_DayOfWeekName_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeekName_Example1");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00024ADC File Offset: 0x00022CDC
		public static string Date_DayOfWeek
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeek");
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00024AE8 File Offset: 0x00022CE8
		public static string Date_DayOfWeek_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeek_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00024B02 File Offset: 0x00022D02
		public static string Date_DayOfWeek_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeek_Example1");
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00024B0E File Offset: 0x00022D0E
		public static string Date_DayOfWeek_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfWeek_Example2");
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00024B1A File Offset: 0x00022D1A
		public static string Date_DayOfYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfYear");
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00024B26 File Offset: 0x00022D26
		public static string Date_DayOfYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfYear_Description", new object[] { p0 });
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00024B3C File Offset: 0x00022D3C
		public static string Date_DayOfYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DayOfYear_Example1");
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00024B48 File Offset: 0x00022D48
		public static string Date_DaysInMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DaysInMonth");
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00024B54 File Offset: 0x00022D54
		public static string Date_DaysInMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DaysInMonth_Description", new object[] { p0 });
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00024B6A File Offset: 0x00022D6A
		public static string Date_DaysInMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_DaysInMonth_Example1");
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00024B76 File Offset: 0x00022D76
		public static string Date_IsLeapYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsLeapYear");
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00024B82 File Offset: 0x00022D82
		public static string Date_IsLeapYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsLeapYear_Description", new object[] { p0 });
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00024B98 File Offset: 0x00022D98
		public static string Date_IsLeapYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsLeapYear_Example1");
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00024BA4 File Offset: 0x00022DA4
		public static string Day_Sunday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Sunday");
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00024BB0 File Offset: 0x00022DB0
		public static string Day_Sunday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Sunday_Description");
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00024BBC File Offset: 0x00022DBC
		public static string Day_Monday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Monday");
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00024BC8 File Offset: 0x00022DC8
		public static string Day_Monday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Monday_Description");
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00024BD4 File Offset: 0x00022DD4
		public static string Day_Tuesday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Tuesday");
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00024BE0 File Offset: 0x00022DE0
		public static string Day_Tuesday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Tuesday_Description");
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00024BEC File Offset: 0x00022DEC
		public static string Day_Wednesday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Wednesday");
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00024BF8 File Offset: 0x00022DF8
		public static string Day_Wednesday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Wednesday_Description");
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00024C04 File Offset: 0x00022E04
		public static string Day_Thursday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Thursday");
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00024C10 File Offset: 0x00022E10
		public static string Day_Thursday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Thursday_Description");
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00024C1C File Offset: 0x00022E1C
		public static string Day_Friday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Friday");
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00024C28 File Offset: 0x00022E28
		public static string Day_Friday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Friday_Description");
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00024C34 File Offset: 0x00022E34
		public static string Day_Saturday
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Saturday");
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00024C40 File Offset: 0x00022E40
		public static string Day_Saturday_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Saturday_Description");
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00024C4C File Offset: 0x00022E4C
		public static string Date_WeekOfMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfMonth");
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00024C58 File Offset: 0x00022E58
		public static string Date_WeekOfMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfMonth_Description", new object[] { p0 });
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00024C6E File Offset: 0x00022E6E
		public static string Date_WeekOfMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfMonth_Example1");
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00024C7A File Offset: 0x00022E7A
		public static string Date_WeekOfYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfYear");
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00024C86 File Offset: 0x00022E86
		public static string Date_WeekOfYear_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfYear_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00024CA0 File Offset: 0x00022EA0
		public static string Date_WeekOfYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfYear_Example1");
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00024CAC File Offset: 0x00022EAC
		public static string Date_WeekOfYear_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_WeekOfYear_Example2");
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00024CB8 File Offset: 0x00022EB8
		public static string Logical_Constants_False
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_Constants_False");
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00024CC4 File Offset: 0x00022EC4
		public static string Logical_Constants_True
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_Constants_True");
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00024CD0 File Offset: 0x00022ED0
		public static string List_AllTrue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_AllTrue");
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00024CDC File Offset: 0x00022EDC
		public static string List_AllTrue_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_AllTrue_Description", new object[] { p0 });
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00024CF2 File Offset: 0x00022EF2
		public static string List_AllTrue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_AllTrue_Example1");
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00024CFE File Offset: 0x00022EFE
		public static string List_AllTrue_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_AllTrue_Example2");
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00024D0A File Offset: 0x00022F0A
		public static string List_AnyTrue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_AnyTrue");
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00024D16 File Offset: 0x00022F16
		public static string List_AnyTrue_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_AnyTrue_Description", new object[] { p0 });
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00024D2C File Offset: 0x00022F2C
		public static string List_AnyTrue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_AnyTrue_Example1");
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00024D38 File Offset: 0x00022F38
		public static string List_AnyTrue_Example2(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_AnyTrue_Example2", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00024D56 File Offset: 0x00022F56
		public static string Logical_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_FromText");
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00024D62 File Offset: 0x00022F62
		public static string Logical_FromText_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_FromText_Description", new object[] { p0 });
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00024D78 File Offset: 0x00022F78
		public static string Logical_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_FromText_Example1");
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00024D84 File Offset: 0x00022F84
		public static string Logical_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_FromText_Example2");
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00024D90 File Offset: 0x00022F90
		public static string List_Average
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Average");
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00024D9C File Offset: 0x00022F9C
		public static string List_Average_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Average_Description", new object[] { p0 });
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00024DB2 File Offset: 0x00022FB2
		public static string List_Average_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Average_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x00024DD4 File Offset: 0x00022FD4
		public static string List_Average_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Average_Example2");
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00024DE0 File Offset: 0x00022FE0
		public static string List_MaxN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_MaxN");
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00024DEC File Offset: 0x00022FEC
		public static string List_MaxN_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MaxN_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00024E0A File Offset: 0x0002300A
		public static string List_MaxN_Example2(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MaxN_Example2", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00024E2C File Offset: 0x0002302C
		public static string List_Median
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Median");
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00024E38 File Offset: 0x00023038
		public static string List_Median_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Median_Description", new object[] { p0 });
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00024E4E File Offset: 0x0002304E
		public static string List_Median_Example1(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Median_Example1", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00024E7A File Offset: 0x0002307A
		public static string List_MinN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_MinN");
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00024E86 File Offset: 0x00023086
		public static string List_MinN_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MinN_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00024EA4 File Offset: 0x000230A4
		public static string List_MinN_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MinN_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00024EC6 File Offset: 0x000230C6
		public static string List_Mode
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Mode");
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00024ED2 File Offset: 0x000230D2
		public static string List_Mode_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Mode_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00024EEC File Offset: 0x000230EC
		public static string List_Mode_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Mode_Example1");
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00024EF8 File Offset: 0x000230F8
		public static string List_Mode_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Mode_Example2");
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00024F04 File Offset: 0x00023104
		public static string List_Modes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Modes");
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00024F10 File Offset: 0x00023110
		public static string List_Modes_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Modes_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00024F2A File Offset: 0x0002312A
		public static string List_Modes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Modes_Example1");
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00024F36 File Offset: 0x00023136
		public static string List_Percentile
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Percentile");
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00024F42 File Offset: 0x00023142
		public static string List_Percentile_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Percentile_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00024F60 File Offset: 0x00023160
		public static string List_Percentile_Example1(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Percentile_Example1", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00024F8C File Offset: 0x0002318C
		public static string List_Percentile_Example2(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Percentile_Example2", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00024FB8 File Offset: 0x000231B8
		public static string PercentileMode_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("PercentileMode_Type");
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00024FC4 File Offset: 0x000231C4
		public static string PercentileMode_ExcelExc
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("PercentileMode_ExcelExc");
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00024FD0 File Offset: 0x000231D0
		public static string PercentileMode_ExcelInc
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("PercentileMode_ExcelInc");
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00024FDC File Offset: 0x000231DC
		public static string PercentileMode_SqlDisc
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("PercentileMode_SqlDisc");
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00024FE8 File Offset: 0x000231E8
		public static string PercentileMode_SqlCont
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("PercentileMode_SqlCont");
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00024FF4 File Offset: 0x000231F4
		public static string List_Product
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Product");
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00025000 File Offset: 0x00023200
		public static string List_Product_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Product_Description", new object[] { p0 });
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00025016 File Offset: 0x00023216
		public static string List_Product_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Product_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00025030 File Offset: 0x00023230
		public static string List_StandardDeviation
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_StandardDeviation");
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002503C File Offset: 0x0002323C
		public static string List_StandardDeviation_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_StandardDeviation_Description", new object[] { p0 });
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00025052 File Offset: 0x00023252
		public static string List_StandardDeviation_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_StandardDeviation_Example1");
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002505E File Offset: 0x0002325E
		public static string List_Sum
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sum");
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002506A File Offset: 0x0002326A
		public static string List_Sum_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sum_Description", new object[] { p0 });
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00025080 File Offset: 0x00023280
		public static string List_Sum_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sum_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0002509A File Offset: 0x0002329A
		public static string Number_Abs
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Abs");
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000250A6 File Offset: 0x000232A6
		public static string Number_Abs_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Abs_Description", new object[] { p0 });
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000250BC File Offset: 0x000232BC
		public static string Number_Abs_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Abs_Example1");
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x000250C8 File Offset: 0x000232C8
		public static string Number_Exp
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Exp");
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000250D4 File Offset: 0x000232D4
		public static string Number_Exp_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Exp_Description", new object[] { p0 });
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000250EA File Offset: 0x000232EA
		public static string Number_Exp_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Exp_Example1");
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000250F6 File Offset: 0x000232F6
		public static string Number_IntegerDivide
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IntegerDivide");
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00025102 File Offset: 0x00023302
		public static string Number_IntegerDivide_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IntegerDivide_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0002511C File Offset: 0x0002331C
		public static string Number_IntegerDivide_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IntegerDivide_Example1");
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00025128 File Offset: 0x00023328
		public static string Number_IntegerDivide_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IntegerDivide_Example2");
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00025134 File Offset: 0x00023334
		public static string Number_Ln
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Ln");
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00025140 File Offset: 0x00023340
		public static string Number_Ln_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Ln_Description", new object[] { p0 });
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00025156 File Offset: 0x00023356
		public static string Number_Ln_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Ln_Example1");
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00025162 File Offset: 0x00023362
		public static string Number_Log
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log");
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002516E File Offset: 0x0002336E
		public static string Number_Log_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00025188 File Offset: 0x00023388
		public static string Number_Log_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log_Example1");
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00025194 File Offset: 0x00023394
		public static string Number_Log_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log_Example2");
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000251A0 File Offset: 0x000233A0
		public static string Number_Log10
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log10");
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000251AC File Offset: 0x000233AC
		public static string Number_Log10_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log10_Description", new object[] { p0 });
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x000251C2 File Offset: 0x000233C2
		public static string Number_Log10_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Log10_Example1");
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000251CE File Offset: 0x000233CE
		public static string Number_Mod
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Mod");
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000251DA File Offset: 0x000233DA
		public static string Number_Mod_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Mod_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x000251F4 File Offset: 0x000233F4
		public static string Number_Mod_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Mod_Example1");
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00025200 File Offset: 0x00023400
		public static string Number_Power
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Power");
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002520C File Offset: 0x0002340C
		public static string Number_Power_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Power_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00025226 File Offset: 0x00023426
		public static string Number_Power_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Power_Example1");
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00025232 File Offset: 0x00023432
		public static string Number_Round
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round");
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002523E File Offset: 0x0002343E
		public static string Number_Round_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0002525C File Offset: 0x0002345C
		public static string Number_Round_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round_Example1");
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00025268 File Offset: 0x00023468
		public static string Number_Round_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round_Example2");
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00025274 File Offset: 0x00023474
		public static string Number_Round_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round_Example3");
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00025280 File Offset: 0x00023480
		public static string Number_Round_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round_Example4");
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002528C File Offset: 0x0002348C
		public static string Number_Round_Example5
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Round_Example5");
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00025298 File Offset: 0x00023498
		public static string Number_RoundDown
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundDown");
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000252A4 File Offset: 0x000234A4
		public static string Number_RoundDown_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundDown_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000252BE File Offset: 0x000234BE
		public static string Number_RoundDown_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundDown_Example1");
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000252CA File Offset: 0x000234CA
		public static string Number_RoundDown_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundDown_Example2");
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x000252D6 File Offset: 0x000234D6
		public static string Number_RoundDown_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundDown_Example3");
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000252E2 File Offset: 0x000234E2
		public static string Number_RoundUp
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundUp");
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000252EE File Offset: 0x000234EE
		public static string Number_RoundUp_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundUp_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00025308 File Offset: 0x00023508
		public static string Number_RoundUp_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundUp_Example1");
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00025314 File Offset: 0x00023514
		public static string Number_RoundUp_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundUp_Example2");
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00025320 File Offset: 0x00023520
		public static string Number_RoundUp_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundUp_Example3");
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0002532C File Offset: 0x0002352C
		public static string Number_Sign
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sign");
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00025338 File Offset: 0x00023538
		public static string Number_Sign_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sign_Description", new object[] { p0 });
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0002534E File Offset: 0x0002354E
		public static string Number_Sign_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sign_Example1");
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002535A File Offset: 0x0002355A
		public static string Number_Sign_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sign_Example2");
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00025366 File Offset: 0x00023566
		public static string Number_Sign_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sign_Example3");
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00025372 File Offset: 0x00023572
		public static string Number_Sqrt
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sqrt");
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002537E File Offset: 0x0002357E
		public static string Number_Sqrt_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sqrt_Description", new object[] { p0 });
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00025394 File Offset: 0x00023594
		public static string Number_Sqrt_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sqrt_Example1");
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x000253A0 File Offset: 0x000235A0
		public static string Number_Sqrt_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sqrt_Example2");
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000253AC File Offset: 0x000235AC
		public static string Number_E
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_E");
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x000253B8 File Offset: 0x000235B8
		public static string Number_E_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_E_Description");
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000253C4 File Offset: 0x000235C4
		public static string Number_Epsilon
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Epsilon");
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x000253D0 File Offset: 0x000235D0
		public static string Number_NaN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_NaN");
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x000253DC File Offset: 0x000235DC
		public static string Number_NegativeInfinity
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_NegativeInfinity");
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x000253E8 File Offset: 0x000235E8
		public static string Number_PI
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_PI");
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x000253F4 File Offset: 0x000235F4
		public static string Number_PI_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_PI_Description");
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00025400 File Offset: 0x00023600
		public static string Number_PositiveInfinity
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_PositiveInfinity");
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002540C File Offset: 0x0002360C
		public static string Number_IsEven
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsEven");
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00025418 File Offset: 0x00023618
		public static string Number_IsEven_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsEven_Description", new object[] { p0 });
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002542E File Offset: 0x0002362E
		public static string Number_IsEven_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsEven_Example1");
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002543A File Offset: 0x0002363A
		public static string Number_IsEven_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsEven_Example2");
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00025446 File Offset: 0x00023646
		public static string Number_IsNaN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsNaN");
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00025452 File Offset: 0x00023652
		public static string Number_IsNaN_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsNaN_Description", new object[] { p0 });
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00025468 File Offset: 0x00023668
		public static string Number_IsNaN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsNaN_Example1");
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00025474 File Offset: 0x00023674
		public static string Number_IsNaN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsNaN_Example2");
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00025480 File Offset: 0x00023680
		public static string Number_IsOdd
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsOdd");
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002548C File Offset: 0x0002368C
		public static string Number_IsOdd_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsOdd_Description", new object[] { p0 });
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000254A2 File Offset: 0x000236A2
		public static string Number_IsOdd_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsOdd_Example1");
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000254AE File Offset: 0x000236AE
		public static string Number_IsOdd_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_IsOdd_Example2");
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000254BA File Offset: 0x000236BA
		public static string List_Numbers
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Numbers");
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000254C6 File Offset: 0x000236C6
		public static string List_Numbers_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Numbers_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x000254E4 File Offset: 0x000236E4
		public static string List_Numbers_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Numbers_Example1");
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000254F0 File Offset: 0x000236F0
		public static string List_Numbers_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Numbers_Example2");
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000254FC File Offset: 0x000236FC
		public static string List_Random
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Random");
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00025508 File Offset: 0x00023708
		public static string List_Random_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Random_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00025522 File Offset: 0x00023722
		public static string List_Random_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Random_Example1");
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002552E File Offset: 0x0002372E
		public static string List_Random_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Random_Example2");
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002553A File Offset: 0x0002373A
		public static string Number_Combinations
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Combinations");
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00025546 File Offset: 0x00023746
		public static string Number_Combinations_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Combinations_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00025560 File Offset: 0x00023760
		public static string Number_Combinations_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Combinations_Example1");
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002556C File Offset: 0x0002376C
		public static string List_Covariance
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Covariance");
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00025578 File Offset: 0x00023778
		public static string List_Covariance_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Covariance_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00025592 File Offset: 0x00023792
		public static string List_Covariance_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Covariance_Example1");
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002559E File Offset: 0x0002379E
		public static string Number_Factorial
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Factorial");
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000255AA File Offset: 0x000237AA
		public static string Number_Factorial_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Factorial_Description", new object[] { p0 });
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000255C0 File Offset: 0x000237C0
		public static string Number_Factorial_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Factorial_Example1");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000255CC File Offset: 0x000237CC
		public static string Number_Permutations
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Permutations");
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000255D8 File Offset: 0x000237D8
		public static string Number_Permutations_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Permutations_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000255F2 File Offset: 0x000237F2
		public static string Number_Permutations_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Permutations_Example1");
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x000255FE File Offset: 0x000237FE
		public static string Number_Random
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Random");
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002560A File Offset: 0x0002380A
		public static string Number_Random_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Random_Description");
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00025616 File Offset: 0x00023816
		public static string Number_Random_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Random_Example1");
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00025622 File Offset: 0x00023822
		public static string Number_RandomBetween
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RandomBetween");
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002562E File Offset: 0x0002382E
		public static string Number_RandomBetween_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RandomBetween_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00025648 File Offset: 0x00023848
		public static string Number_RandomBetween_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RandomBetween_Example1");
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00025654 File Offset: 0x00023854
		public static string Number_Acos
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Acos");
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00025660 File Offset: 0x00023860
		public static string Number_Acos_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Acos_Description", new object[] { p0 });
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00025676 File Offset: 0x00023876
		public static string Number_Asin
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Asin");
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00025682 File Offset: 0x00023882
		public static string Number_Asin_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Asin_Description", new object[] { p0 });
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00025698 File Offset: 0x00023898
		public static string Number_Atan
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Atan");
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000256A4 File Offset: 0x000238A4
		public static string Number_Atan_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Atan_Description", new object[] { p0 });
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x000256BA File Offset: 0x000238BA
		public static string Number_Atan2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Atan2");
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000256C6 File Offset: 0x000238C6
		public static string Number_Atan2_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Atan2_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x000256E0 File Offset: 0x000238E0
		public static string Number_Cos
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Cos");
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000256EC File Offset: 0x000238EC
		public static string Number_Cos_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Cos_Description", new object[] { p0 });
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00025702 File Offset: 0x00023902
		public static string Number_Cos_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Cos_Example1");
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002570E File Offset: 0x0002390E
		public static string Number_Cosh
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Cosh");
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002571A File Offset: 0x0002391A
		public static string Number_Cosh_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Cosh_Description", new object[] { p0 });
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00025730 File Offset: 0x00023930
		public static string Number_Sin
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sin");
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002573C File Offset: 0x0002393C
		public static string Number_Sin_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sin_Description", new object[] { p0 });
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00025752 File Offset: 0x00023952
		public static string Number_Sin_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sin_Example1");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002575E File Offset: 0x0002395E
		public static string Number_Sinh
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sinh");
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002576A File Offset: 0x0002396A
		public static string Number_Sinh_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Sinh_Description", new object[] { p0 });
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00025780 File Offset: 0x00023980
		public static string Number_Tan
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Tan");
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002578C File Offset: 0x0002398C
		public static string Number_Tan_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Tan_Description", new object[] { p0 });
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000257A2 File Offset: 0x000239A2
		public static string Number_Tan_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Tan_Example1");
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x000257AE File Offset: 0x000239AE
		public static string Number_Tanh
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Tanh");
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000257BA File Offset: 0x000239BA
		public static string Number_Tanh_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Tanh_Description", new object[] { p0 });
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x000257D0 File Offset: 0x000239D0
		public static string Number_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_ToText");
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000257DC File Offset: 0x000239DC
		public static string Number_ToText_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_ToText_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x000257FA File Offset: 0x000239FA
		public static string Number_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_ToText_Example1");
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00025806 File Offset: 0x00023A06
		public static string Number_ToText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_ToText_Example2");
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00025812 File Offset: 0x00023A12
		public static string Number_ToText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_ToText_Example3");
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002581E File Offset: 0x00023A1E
		public static string Number_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_FromText");
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002582A File Offset: 0x00023A2A
		public static string Number_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00025844 File Offset: 0x00023A44
		public static string Number_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_FromText_Example1");
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00025850 File Offset: 0x00023A50
		public static string Number_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_FromText_Example2");
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002585C File Offset: 0x00023A5C
		public static string Text_At
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_At");
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00025868 File Offset: 0x00023A68
		public static string Text_At_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_At_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00025882 File Offset: 0x00023A82
		public static string Text_At_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_At_Example1");
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002588E File Offset: 0x00023A8E
		public static string Text_Start
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Start");
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002589A File Offset: 0x00023A9A
		public static string Text_Start_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Start_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x000258B4 File Offset: 0x00023AB4
		public static string Text_Start_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Start_Example1");
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x000258C0 File Offset: 0x00023AC0
		public static string Text_End
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_End");
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000258CC File Offset: 0x00023ACC
		public static string Text_End_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_End_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x000258E6 File Offset: 0x00023AE6
		public static string Text_End_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_End_Example1");
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x000258F2 File Offset: 0x00023AF2
		public static string Text_EndsWith
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_EndsWith");
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000258FE File Offset: 0x00023AFE
		public static string Text_EndsWith_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_EndsWith_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002591C File Offset: 0x00023B1C
		public static string Text_EndsWith_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_EndsWith_Example1");
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00025928 File Offset: 0x00023B28
		public static string Text_EndsWith_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_EndsWith_Example2");
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00025934 File Offset: 0x00023B34
		public static string Text_PositionOf
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOf");
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00025940 File Offset: 0x00023B40
		public static string Text_PositionOf_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOf_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00025962 File Offset: 0x00023B62
		public static string Text_PositionOf_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOf_Example1");
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002596E File Offset: 0x00023B6E
		public static string Text_PositionOf_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOf_Example2");
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002597A File Offset: 0x00023B7A
		public static string Text_PositionOfAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOfAny");
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00025986 File Offset: 0x00023B86
		public static string Text_PositionOfAny_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOfAny_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000259A4 File Offset: 0x00023BA4
		public static string Text_PositionOfAny_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOfAny_Example1");
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x000259B0 File Offset: 0x00023BB0
		public static string Text_PositionOfAny_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PositionOfAny_Example2");
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000259BC File Offset: 0x00023BBC
		public static string Text_Compare
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Compare");
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000259C8 File Offset: 0x00023BC8
		public static string Text_Compare_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Compare_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000259E6 File Offset: 0x00023BE6
		public static string Text_Compare_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Compare_Example1");
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000259F2 File Offset: 0x00023BF2
		public static string Text_Compare_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Compare_Example2");
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000259FE File Offset: 0x00023BFE
		public static string Text_StartsWith
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_StartsWith");
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00025A0A File Offset: 0x00023C0A
		public static string Text_StartsWith_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_StartsWith_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00025A28 File Offset: 0x00023C28
		public static string Text_StartsWith_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_StartsWith_Example1");
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00025A34 File Offset: 0x00023C34
		public static string Text_StartsWith_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_StartsWith_Example2");
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00025A40 File Offset: 0x00023C40
		public static string RelativePosition_FromStart
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RelativePosition_FromStart");
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00025A4C File Offset: 0x00023C4C
		public static string RelativePosition_FromEnd
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RelativePosition_FromEnd");
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00025A58 File Offset: 0x00023C58
		public static string Text_AfterDelimiter_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_AfterDelimiter_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00025A76 File Offset: 0x00023C76
		public static string Text_AfterDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_AfterDelimiter_Example1");
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00025A82 File Offset: 0x00023C82
		public static string Text_AfterDelimiter_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_AfterDelimiter_Example2");
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00025A8E File Offset: 0x00023C8E
		public static string Text_AfterDelimiter_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_AfterDelimiter_Example3");
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00025A9A File Offset: 0x00023C9A
		public static string Text_BeforeDelimiter_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BeforeDelimiter_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00025AB8 File Offset: 0x00023CB8
		public static string Text_BeforeDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BeforeDelimiter_Example1");
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00025AC4 File Offset: 0x00023CC4
		public static string Text_BeforeDelimiter_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BeforeDelimiter_Example2");
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00025AD0 File Offset: 0x00023CD0
		public static string Text_BeforeDelimiter_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BeforeDelimiter_Example3");
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00025ADC File Offset: 0x00023CDC
		public static string Text_BetweenDelimiters_Description(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BetweenDelimiters_Description", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00025B03 File Offset: 0x00023D03
		public static string Text_BetweenDelimiters_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BetweenDelimiters_Example1");
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00025B0F File Offset: 0x00023D0F
		public static string Text_BetweenDelimiters_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BetweenDelimiters_Example2");
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00025B1B File Offset: 0x00023D1B
		public static string Text_BetweenDelimiters_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_BetweenDelimiters_Example3");
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00025B27 File Offset: 0x00023D27
		public static string Text_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Combine");
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00025B33 File Offset: 0x00023D33
		public static string Text_Combine_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Combine_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00025B4D File Offset: 0x00023D4D
		public static string Text_Combine_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Combine_Example1");
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00025B59 File Offset: 0x00023D59
		public static string Text_Combine_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Combine_Example2");
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00025B65 File Offset: 0x00023D65
		public static string Text_Combine_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Combine_Example3");
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00025B71 File Offset: 0x00023D71
		public static string Text_Split
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Split");
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00025B7D File Offset: 0x00023D7D
		public static string Text_Split_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Split_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00025B97 File Offset: 0x00023D97
		public static string Text_Split_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Split_Example1");
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00025BA3 File Offset: 0x00023DA3
		public static string Text_SplitAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_SplitAny");
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00025BAF File Offset: 0x00023DAF
		public static string Text_SplitAny_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_SplitAny_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00025BC9 File Offset: 0x00023DC9
		public static string Text_SplitAny_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_SplitAny_Example1");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00025BD5 File Offset: 0x00023DD5
		public static string Text_Clean
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Clean");
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00025BE1 File Offset: 0x00023DE1
		public static string Text_Clean_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Clean_Description", new object[] { p0 });
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00025BF7 File Offset: 0x00023DF7
		public static string Text_Clean_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Clean_Example1");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00025C03 File Offset: 0x00023E03
		public static string Text_Insert
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Insert");
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00025C0F File Offset: 0x00023E0F
		public static string Text_Insert_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Insert_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00025C2D File Offset: 0x00023E2D
		public static string Text_Insert_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Insert_Example1");
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00025C39 File Offset: 0x00023E39
		public static string Text_Lower
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Lower");
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00025C45 File Offset: 0x00023E45
		public static string Text_Lower_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Lower_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00025C5F File Offset: 0x00023E5F
		public static string Text_Lower_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Lower_Example1");
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00025C6B File Offset: 0x00023E6B
		public static string Text_PadStart
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadStart");
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00025C77 File Offset: 0x00023E77
		public static string Text_PadStart_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadStart_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00025C95 File Offset: 0x00023E95
		public static string Text_PadStart_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadStart_Example1");
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00025CA1 File Offset: 0x00023EA1
		public static string Text_PadStart_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadStart_Example2");
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00025CAD File Offset: 0x00023EAD
		public static string Text_PadEnd
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadEnd");
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00025CB9 File Offset: 0x00023EB9
		public static string Text_PadEnd_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadEnd_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00025CD7 File Offset: 0x00023ED7
		public static string Text_PadEnd_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadEnd_Example1");
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00025CE3 File Offset: 0x00023EE3
		public static string Text_PadEnd_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_PadEnd_Example2");
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00025CEF File Offset: 0x00023EEF
		public static string Text_Proper
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Proper");
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00025CFB File Offset: 0x00023EFB
		public static string Text_Proper_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Proper_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00025D15 File Offset: 0x00023F15
		public static string Text_Proper_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Proper_Example1");
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00025D21 File Offset: 0x00023F21
		public static string Text_Remove
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Remove");
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00025D2D File Offset: 0x00023F2D
		public static string Text_Remove_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Remove_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00025D47 File Offset: 0x00023F47
		public static string Text_Remove_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Remove_Example1");
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00025D53 File Offset: 0x00023F53
		public static string Text_RemoveRange
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_RemoveRange");
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00025D5F File Offset: 0x00023F5F
		public static string Text_RemoveRange_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_RemoveRange_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00025D7D File Offset: 0x00023F7D
		public static string Text_RemoveRange_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_RemoveRange_Example1");
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00025D89 File Offset: 0x00023F89
		public static string Text_RemoveRange_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_RemoveRange_Example2");
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00025D95 File Offset: 0x00023F95
		public static string Text_Repeat
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Repeat");
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00025DA1 File Offset: 0x00023FA1
		public static string Text_Repeat_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Repeat_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00025DBB File Offset: 0x00023FBB
		public static string Text_Repeat_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Repeat_Example1");
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00025DC7 File Offset: 0x00023FC7
		public static string Text_Repeat_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Repeat_Example2");
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00025DD3 File Offset: 0x00023FD3
		public static string Text_ReplaceRange
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ReplaceRange");
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00025DDF File Offset: 0x00023FDF
		public static string Text_ReplaceRange_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ReplaceRange_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00025E01 File Offset: 0x00024001
		public static string Text_ReplaceRange_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ReplaceRange_Example1");
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00025E0D File Offset: 0x0002400D
		public static string Text_Replace
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Replace");
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00025E19 File Offset: 0x00024019
		public static string Text_Replace_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Replace_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00025E37 File Offset: 0x00024037
		public static string Text_Replace_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Replace_Example1");
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00025E43 File Offset: 0x00024043
		public static string Text_Trim
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Trim");
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00025E4F File Offset: 0x0002404F
		public static string Text_Trim_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Trim_Description", new object[] { p0 });
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00025E65 File Offset: 0x00024065
		public static string Text_Trim_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Trim_Example1");
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00025E71 File Offset: 0x00024071
		public static string Text_TrimEnd
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_TrimEnd");
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00025E7D File Offset: 0x0002407D
		public static string Text_TrimEnd_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_TrimEnd_Description", new object[] { p0 });
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00025E93 File Offset: 0x00024093
		public static string Text_TrimEnd_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_TrimEnd_Example1");
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00025E9F File Offset: 0x0002409F
		public static string Text_TrimStart
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_TrimStart");
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00025EAB File Offset: 0x000240AB
		public static string Text_TrimStart_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_TrimStart_Description", new object[] { p0 });
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00025EC1 File Offset: 0x000240C1
		public static string Text_TrimStart_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_TrimStart_Example1");
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00025ECD File Offset: 0x000240CD
		public static string Text_Upper
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Upper");
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00025ED9 File Offset: 0x000240D9
		public static string Text_Upper_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Upper_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00025EF3 File Offset: 0x000240F3
		public static string Text_Upper_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Upper_Example1");
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00025EFF File Offset: 0x000240FF
		public static string Character_FromNumber
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_FromNumber");
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00025F0B File Offset: 0x0002410B
		public static string Character_FromNumber_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Character_FromNumber_Description", new object[] { p0 });
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00025F21 File Offset: 0x00024121
		public static string Character_FromNumber_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_FromNumber_Example1");
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00025F2D File Offset: 0x0002412D
		public static string Character_FromNumber_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_FromNumber_Example2");
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00025F39 File Offset: 0x00024139
		public static string Character_FromNumber_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_FromNumber_Example3");
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00025F45 File Offset: 0x00024145
		public static string Sql_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Sql_Database");
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00025F51 File Offset: 0x00024151
		public static string Sql_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Sql_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00025F6F File Offset: 0x0002416F
		public static string Sql_Databases
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Sql_Databases");
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00025F7B File Offset: 0x0002417B
		public static string Sql_Databases_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Sql_Databases_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00025F95 File Offset: 0x00024195
		public static string File_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("File_Contents");
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00025FA1 File Offset: 0x000241A1
		public static string File_Contents_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("File_Contents_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00025FBB File Offset: 0x000241BB
		public static string Folder_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Folder_Contents");
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00025FC7 File Offset: 0x000241C7
		public static string Folder_Contents_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Folder_Contents_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00025FE1 File Offset: 0x000241E1
		public static string Table_Column
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Column");
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00025FED File Offset: 0x000241ED
		public static string Table_Column_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Column_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00026007 File Offset: 0x00024207
		public static string Table_Column_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Column_Example1");
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00026013 File Offset: 0x00024213
		public static string Record_Field
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Field");
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002601F File Offset: 0x0002421F
		public static string Record_Field_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Field_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00026039 File Offset: 0x00024239
		public static string Record_Field_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Field_Example1");
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00026045 File Offset: 0x00024245
		public static string Record_FieldOrDefault
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldOrDefault");
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00026051 File Offset: 0x00024251
		public static string Record_FieldOrDefault_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldOrDefault_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0002606F File Offset: 0x0002426F
		public static string Record_FieldOrDefault_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldOrDefault_Example1");
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0002607B File Offset: 0x0002427B
		public static string Record_FieldOrDefault_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldOrDefault_Example2");
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00026087 File Offset: 0x00024287
		public static string Record_FieldValues
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldValues");
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00026093 File Offset: 0x00024293
		public static string Record_FieldValues_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldValues_Description", new object[] { p0 });
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x000260A9 File Offset: 0x000242A9
		public static string Record_FieldValues_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldValues_Example1");
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000260B5 File Offset: 0x000242B5
		public static string List_FirstN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_FirstN");
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x000260C1 File Offset: 0x000242C1
		public static string List_FirstN_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_FirstN_Description");
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000260CD File Offset: 0x000242CD
		public static string List_FirstN_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_FirstN_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000260EF File Offset: 0x000242EF
		public static string List_First
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_First");
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000260FB File Offset: 0x000242FB
		public static string List_First_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_First_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00026115 File Offset: 0x00024315
		public static string List_First_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_First_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002612F File Offset: 0x0002432F
		public static string List_First_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_First_Example2");
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002613B File Offset: 0x0002433B
		public static string List_LastN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_LastN");
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00026147 File Offset: 0x00024347
		public static string List_LastN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_LastN_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00026161 File Offset: 0x00024361
		public static string List_LastN_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_LastN_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00026183 File Offset: 0x00024383
		public static string List_LastN_Example2(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_LastN_Example2", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000261A5 File Offset: 0x000243A5
		public static string List_Last
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Last");
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000261B1 File Offset: 0x000243B1
		public static string List_Last_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Last_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000261CB File Offset: 0x000243CB
		public static string List_Last_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Last_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000261E5 File Offset: 0x000243E5
		public static string List_Last_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Last_Example2");
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000261F1 File Offset: 0x000243F1
		public static string List_Range
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Range");
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000261FD File Offset: 0x000243FD
		public static string List_Range_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Range_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00026217 File Offset: 0x00024417
		public static string List_Range_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Range_Example1");
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00026223 File Offset: 0x00024423
		public static string List_Range_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Range_Example2");
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002622F File Offset: 0x0002442F
		public static string List_Transform
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Transform");
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002623B File Offset: 0x0002443B
		public static string List_Transform_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Transform_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00026255 File Offset: 0x00024455
		public static string List_Transform_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Transform_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0002626F File Offset: 0x0002446F
		public static string List_TransformMany
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_TransformMany");
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002627B File Offset: 0x0002447B
		public static string List_TransformMany_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_TransformMany_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00026299 File Offset: 0x00024499
		public static string List_TransformMany_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_TransformMany_Example1");
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x000262A5 File Offset: 0x000244A5
		public static string List_Single
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Single");
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000262B1 File Offset: 0x000244B1
		public static string List_Single_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Single_Description", new object[] { p0 });
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000262C7 File Offset: 0x000244C7
		public static string List_Single_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Single_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000262E1 File Offset: 0x000244E1
		public static string List_Single_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Single_Example2", new object[] { p0, p1 });
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000262FB File Offset: 0x000244FB
		public static string List_SingleOrDefault
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_SingleOrDefault");
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00026307 File Offset: 0x00024507
		public static string List_SingleOrDefault_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_SingleOrDefault_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00026321 File Offset: 0x00024521
		public static string List_SingleOrDefault_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_SingleOrDefault_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0002633B File Offset: 0x0002453B
		public static string List_SingleOrDefault_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_SingleOrDefault_Example2");
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00026347 File Offset: 0x00024547
		public static string List_SingleOrDefault_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_SingleOrDefault_Example3");
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00026353 File Offset: 0x00024553
		public static string List_Count
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Count");
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002635F File Offset: 0x0002455F
		public static string List_Count_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Count_Description", new object[] { p0 });
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00026375 File Offset: 0x00024575
		public static string List_Count_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Count_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0002638F File Offset: 0x0002458F
		public static string List_Difference
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Difference");
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002639B File Offset: 0x0002459B
		public static string List_Difference_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Difference_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000263B9 File Offset: 0x000245B9
		public static string List_Difference_Example1(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Difference_Example1", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000263E0 File Offset: 0x000245E0
		public static string List_Difference_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Difference_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000263FA File Offset: 0x000245FA
		public static string List_Positions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Positions");
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00026406 File Offset: 0x00024606
		public static string List_Positions_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Positions_Description", new object[] { p0 });
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002641C File Offset: 0x0002461C
		public static string List_Positions_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Positions_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00026436 File Offset: 0x00024636
		public static string List_Max
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Max");
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00026442 File Offset: 0x00024642
		public static string List_Max_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Max_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00026460 File Offset: 0x00024660
		public static string List_Max_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Max_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0002647A File Offset: 0x0002467A
		public static string List_Max_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Max_Example2");
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00026486 File Offset: 0x00024686
		public static string List_Min
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Min");
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00026492 File Offset: 0x00024692
		public static string List_Min_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Min_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000264B0 File Offset: 0x000246B0
		public static string List_Min_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Min_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000264CA File Offset: 0x000246CA
		public static string List_Min_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Min_Example2");
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000264D6 File Offset: 0x000246D6
		public static string Record_FieldCount
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldCount");
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000264E2 File Offset: 0x000246E2
		public static string Record_FieldCount_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldCount_Description", new object[] { p0 });
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000264F8 File Offset: 0x000246F8
		public static string Record_FieldCount_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldCount_Example1");
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00026504 File Offset: 0x00024704
		public static string Record_FieldNames
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldNames");
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00026510 File Offset: 0x00024710
		public static string Record_FieldNames_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldNames_Description", new object[] { p0 });
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00026526 File Offset: 0x00024726
		public static string Record_FieldNames_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FieldNames_Example1");
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00026532 File Offset: 0x00024732
		public static string Record_HasFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_HasFields");
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002653E File Offset: 0x0002473E
		public static string Record_HasFields_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_HasFields_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00026558 File Offset: 0x00024758
		public static string Record_HasFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_HasFields_Example1");
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00026564 File Offset: 0x00024764
		public static string Record_HasFields_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_HasFields_Example2");
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00026570 File Offset: 0x00024770
		public static string List_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Combine");
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002657C File Offset: 0x0002477C
		public static string List_Combine_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Combine_Description", new object[] { p0 });
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00026592 File Offset: 0x00024792
		public static string List_Combine_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Combine_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000265B4 File Offset: 0x000247B4
		public static string List_Combine_Example2(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Combine_Example2", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x000265DB File Offset: 0x000247DB
		public static string List_Zip
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Zip");
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000265E7 File Offset: 0x000247E7
		public static string List_Zip_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Zip_Description", new object[] { p0 });
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000265FD File Offset: 0x000247FD
		public static string List_Zip_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Zip_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002661F File Offset: 0x0002481F
		public static string List_Zip_Example2(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Zip_Example2", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00026641 File Offset: 0x00024841
		public static string Table_Join
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Join");
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002664D File Offset: 0x0002484D
		public static string Table_Join_Description(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Join_Description", new object[] { p0, p1, p2, p3, p4, p5, p6 });
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0002667E File Offset: 0x0002487E
		public static string Table_Join_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Join_Example1");
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0002668A File Offset: 0x0002488A
		public static string Table_Join_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Join_Example2");
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00026696 File Offset: 0x00024896
		public static string JoinKind_Inner
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_Inner");
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x000266A2 File Offset: 0x000248A2
		public static string JoinKind_LeftOuter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_LeftOuter");
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x000266AE File Offset: 0x000248AE
		public static string JoinKind_RightOuter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_RightOuter");
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000266BA File Offset: 0x000248BA
		public static string JoinKind_FullOuter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_FullOuter");
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000266C6 File Offset: 0x000248C6
		public static string JoinKind_LeftAnti
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_LeftAnti");
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x000266D2 File Offset: 0x000248D2
		public static string JoinKind_RightAnti
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_RightAnti");
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x000266DE File Offset: 0x000248DE
		public static string JoinKind_LeftSemi
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_LeftSemi");
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000266EA File Offset: 0x000248EA
		public static string JoinKind_RightSemi
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_RightSemi");
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000266F6 File Offset: 0x000248F6
		public static string Record_AddField
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_AddField");
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00026702 File Offset: 0x00024902
		public static string Record_AddField_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_AddField_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00026720 File Offset: 0x00024920
		public static string Record_AddField_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_AddField_Example1");
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0002672C File Offset: 0x0002492C
		public static string List_Repeat
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Repeat");
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00026738 File Offset: 0x00024938
		public static string List_Repeat_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Repeat_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00026752 File Offset: 0x00024952
		public static string List_Repeat_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Repeat_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0002676C File Offset: 0x0002496C
		public static string List_Contains
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Contains");
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00026778 File Offset: 0x00024978
		public static string List_Contains_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Contains_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00026796 File Offset: 0x00024996
		public static string List_Contains_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Contains_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000267B0 File Offset: 0x000249B0
		public static string List_Contains_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Contains_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000267CA File Offset: 0x000249CA
		public static string List_ContainsAll
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAll");
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000267D6 File Offset: 0x000249D6
		public static string List_ContainsAll_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAll_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000267F4 File Offset: 0x000249F4
		public static string List_ContainsAll_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAll_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002680E File Offset: 0x00024A0E
		public static string List_ContainsAll_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAll_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00026828 File Offset: 0x00024A28
		public static string List_ContainsAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAny");
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00026834 File Offset: 0x00024A34
		public static string List_ContainsAny_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAny_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00026852 File Offset: 0x00024A52
		public static string List_ContainsAny_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAny_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002686C File Offset: 0x00024A6C
		public static string List_ContainsAny_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ContainsAny_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00026886 File Offset: 0x00024A86
		public static string List_ReplaceValue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceValue");
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00026892 File Offset: 0x00024A92
		public static string List_ReplaceValue_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceValue_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x000268B0 File Offset: 0x00024AB0
		public static string List_ReplaceValue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceValue_Example1");
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x000268BC File Offset: 0x00024ABC
		public static string List_PositionOf
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_PositionOf");
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000268C8 File Offset: 0x00024AC8
		public static string List_PositionOf_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_PositionOf_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000268E6 File Offset: 0x00024AE6
		public static string List_PositionOf_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_PositionOf_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00026900 File Offset: 0x00024B00
		public static string List_PositionOfAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_PositionOfAny");
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002690C File Offset: 0x00024B0C
		public static string List_PositionOfAny_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_PositionOfAny_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002692A File Offset: 0x00024B2A
		public static string List_PositionOfAny_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_PositionOfAny_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00026944 File Offset: 0x00024B44
		public static string List_FindText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_FindText");
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00026950 File Offset: 0x00024B50
		public static string List_FindText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_FindText_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0002696A File Offset: 0x00024B6A
		public static string List_FindText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_FindText_Example1");
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00026976 File Offset: 0x00024B76
		public static string List_Distinct
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Distinct");
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00026982 File Offset: 0x00024B82
		public static string List_Distinct_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Distinct_Description", new object[] { p0 });
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00026998 File Offset: 0x00024B98
		public static string List_Distinct_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Distinct_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000269B2 File Offset: 0x00024BB2
		public static string List_Index
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Index");
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000269BE File Offset: 0x00024BBE
		public static string List_Index_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Index_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000269D8 File Offset: 0x00024BD8
		public static string List_Index_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Index_Example1");
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000269E4 File Offset: 0x00024BE4
		public static string Order_Ascending
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Order_Ascending");
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x000269F0 File Offset: 0x00024BF0
		public static string Order_Descending
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Order_Descending");
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000269FC File Offset: 0x00024BFC
		public static string List_Skip
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Skip");
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00026A08 File Offset: 0x00024C08
		public static string List_Skip_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Skip_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00026A22 File Offset: 0x00024C22
		public static string List_Skip_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Skip_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00026A3C File Offset: 0x00024C3C
		public static string List_Skip_Example2(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Skip_Example2", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00026A68 File Offset: 0x00024C68
		public static string List_RemoveFirstN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveFirstN");
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00026A74 File Offset: 0x00024C74
		public static string List_RemoveFirstN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveFirstN_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00026A8E File Offset: 0x00024C8E
		public static string List_RemoveFirstN_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveFirstN_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00026AA8 File Offset: 0x00024CA8
		public static string List_RemoveFirstN_Example2(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveFirstN_Example2", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00026AD4 File Offset: 0x00024CD4
		public static string Table_ColumnsOfType
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnsOfType");
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00026AE0 File Offset: 0x00024CE0
		public static string Table_ColumnsOfType_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnsOfType_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00026AFA File Offset: 0x00024CFA
		public static string Table_ColumnsOfType_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnsOfType_Example1");
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00026B06 File Offset: 0x00024D06
		public static string List_RemoveLastN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveLastN");
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00026B12 File Offset: 0x00024D12
		public static string List_RemoveLastN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveLastN_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00026B2C File Offset: 0x00024D2C
		public static string List_RemoveLastN_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveLastN_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00026B46 File Offset: 0x00024D46
		public static string List_RemoveLastN_Example2(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveLastN_Example2", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00026B72 File Offset: 0x00024D72
		public static string List_Alternate
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Alternate");
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00026B7E File Offset: 0x00024D7E
		public static string List_Alternate_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Alternate_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00026BA0 File Offset: 0x00024DA0
		public static string List_Alternate_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Alternate_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00026BBA File Offset: 0x00024DBA
		public static string List_Alternate_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Alternate_Example2", new object[] { p0, p1 });
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00026BD4 File Offset: 0x00024DD4
		public static string List_Alternate_Example3(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Alternate_Example3", new object[] { p0, p1 });
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00026BEE File Offset: 0x00024DEE
		public static string List_Alternate_Example4(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Alternate_Example4", new object[] { p0, p1 });
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00026C08 File Offset: 0x00024E08
		public static string List_Select
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Select");
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00026C14 File Offset: 0x00024E14
		public static string List_Select_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Select_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00026C2E File Offset: 0x00024E2E
		public static string List_Select_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Select_Example1", new object[] { p0, p1 });
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00026C48 File Offset: 0x00024E48
		public static string List_Accumulate
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Accumulate");
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00026C54 File Offset: 0x00024E54
		public static string List_Accumulate_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Accumulate_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00026C72 File Offset: 0x00024E72
		public static string List_Accumulate_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Accumulate_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00026C8C File Offset: 0x00024E8C
		public static string Table_AddFuzzyClusterColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddFuzzyClusterColumn");
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00026C98 File Offset: 0x00024E98
		public static string Table_AddFuzzyClusterColumn_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddFuzzyClusterColumn_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00026CBA File Offset: 0x00024EBA
		public static string Table_AddFuzzyClusterColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddFuzzyClusterColumn_Example1");
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00026CC6 File Offset: 0x00024EC6
		public static string Table_FuzzyGroup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyGroup");
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00026CD2 File Offset: 0x00024ED2
		public static string Table_FuzzyGroup_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyGroup_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00026CF4 File Offset: 0x00024EF4
		public static string Table_FuzzyGroup_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyGroup_Example1");
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00026D00 File Offset: 0x00024F00
		public static string Table_Group
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Group");
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00026D0C File Offset: 0x00024F0C
		public static string Table_Group_Description(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Group_Description", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00026D33 File Offset: 0x00024F33
		public static string Table_Group_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Group_Example1");
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00026D3F File Offset: 0x00024F3F
		public static string List_InsertRange
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_InsertRange");
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00026D4B File Offset: 0x00024F4B
		public static string List_InsertRange_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_InsertRange_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00026D69 File Offset: 0x00024F69
		public static string List_InsertRange_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_InsertRange_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00026D8B File Offset: 0x00024F8B
		public static string List_InsertRange_Example2(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_InsertRange_Example2", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00026DA9 File Offset: 0x00024FA9
		public static string List_RemoveRange
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveRange");
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00026DB5 File Offset: 0x00024FB5
		public static string List_RemoveRange_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveRange_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00026DD3 File Offset: 0x00024FD3
		public static string List_RemoveRange_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveRange_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00026DED File Offset: 0x00024FED
		public static string List_ReplaceRange
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceRange");
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00026DF9 File Offset: 0x00024FF9
		public static string List_ReplaceRange_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceRange_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00026E1B File Offset: 0x0002501B
		public static string List_ReplaceRange_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceRange_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00026E3D File Offset: 0x0002503D
		public static string List_Reverse
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Reverse");
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00026E49 File Offset: 0x00025049
		public static string List_Reverse_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Reverse_Description", new object[] { p0 });
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00026E5F File Offset: 0x0002505F
		public static string List_Reverse_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Reverse_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00026E79 File Offset: 0x00025079
		public static string List_Buffer
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Buffer");
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00026E85 File Offset: 0x00025085
		public static string List_Buffer_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Buffer_Description", new object[] { p0 });
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00026E9B File Offset: 0x0002509B
		public static string List_Buffer_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Buffer_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00026EB5 File Offset: 0x000250B5
		public static string TextEncoding_Ascii
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_Ascii");
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00026EC1 File Offset: 0x000250C1
		public static string Text_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_From");
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00026ECD File Offset: 0x000250CD
		public static string Text_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00026EE7 File Offset: 0x000250E7
		public static string Text_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_From_Example1");
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00026EF3 File Offset: 0x000250F3
		public static string Text_FromBinary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_FromBinary");
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00026EFF File Offset: 0x000250FF
		public static string Text_FromBinary_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_FromBinary_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00026F19 File Offset: 0x00025119
		public static string Text_InferNumberType
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_InferNumberType");
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00026F25 File Offset: 0x00025125
		public static string Text_InferNumberType_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_InferNumberType_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00026F3F File Offset: 0x0002513F
		public static string Text_ToBinary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ToBinary");
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00026F4B File Offset: 0x0002514B
		public static string Text_ToBinary_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ToBinary_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00026F65 File Offset: 0x00025165
		public static string TextEncoding_Utf16
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_Utf16");
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00026F71 File Offset: 0x00025171
		public static string TextEncoding_Utf8
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_Utf8");
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00026F7D File Offset: 0x0002517D
		public static string Number_BitwiseAnd
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseAnd");
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00026F89 File Offset: 0x00025189
		public static string Number_BitwiseAnd_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseAnd_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00026FA3 File Offset: 0x000251A3
		public static string Number_BitwiseNot
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseNot");
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00026FAF File Offset: 0x000251AF
		public static string Number_BitwiseNot_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseNot_Description", new object[] { p0 });
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00026FC5 File Offset: 0x000251C5
		public static string Number_BitwiseOr
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseOr");
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00026FD1 File Offset: 0x000251D1
		public static string Number_BitwiseOr_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseOr_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00026FEB File Offset: 0x000251EB
		public static string Number_BitwiseShiftLeft
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseShiftLeft");
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00026FF7 File Offset: 0x000251F7
		public static string Number_BitwiseShiftLeft_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseShiftLeft_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00027011 File Offset: 0x00025211
		public static string Number_BitwiseShiftRight
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseShiftRight");
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0002701D File Offset: 0x0002521D
		public static string Number_BitwiseShiftRight_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseShiftRight_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00027037 File Offset: 0x00025237
		public static string Number_BitwiseXor
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseXor");
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00027043 File Offset: 0x00025243
		public static string Number_BitwiseXor_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_BitwiseXor_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0002705D File Offset: 0x0002525D
		public static string Binary_InferContentType
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_InferContentType");
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00027069 File Offset: 0x00025269
		public static string Binary_InferContentType_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_InferContentType_Description");
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00027075 File Offset: 0x00025275
		public static string Binary_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_FromText");
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00027081 File Offset: 0x00025281
		public static string Binary_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002709B File Offset: 0x0002529B
		public static string Binary_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_FromText_Example1");
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000270A7 File Offset: 0x000252A7
		public static string Binary_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_FromText_Example2");
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x000270B3 File Offset: 0x000252B3
		public static string Binary_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ToText");
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000270BF File Offset: 0x000252BF
		public static string Binary_ToText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ToText_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x000270D9 File Offset: 0x000252D9
		public static string BinaryEncoding_Base64
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryEncoding_Base64");
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x000270E5 File Offset: 0x000252E5
		public static string BinaryEncoding_Hex
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryEncoding_Hex");
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000270F1 File Offset: 0x000252F1
		public static string Access_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Access_Database");
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x000270FD File Offset: 0x000252FD
		public static string Mashup_Document
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Mashup_Document");
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00027109 File Offset: 0x00025309
		public static string Excel_Workbook
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_Workbook");
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00027115 File Offset: 0x00025315
		public static string Excel_Workbook_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_Workbook_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00027133 File Offset: 0x00025333
		public static string Excel_Workbook_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_Workbook_Example1");
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0002713F File Offset: 0x0002533F
		public static string Json_Document
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Json_Document");
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0002714B File Offset: 0x0002534B
		public static string Json_FromValue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Json_FromValue");
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00027157 File Offset: 0x00025357
		public static string Json_FromValue_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Json_FromValue_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00027171 File Offset: 0x00025371
		public static string Json_FromValue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Json_FromValue_Example1");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0002717D File Offset: 0x0002537D
		public static string Xml_Document
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Xml_Document");
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00027189 File Offset: 0x00025389
		public static string Text_NewGuid
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_NewGuid");
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00027195 File Offset: 0x00025395
		public static string Web_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Contents");
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000271A1 File Offset: 0x000253A1
		public static string Web_Contents_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Contents_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000271BB File Offset: 0x000253BB
		public static string Web_Contents_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Contents_Example1");
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000271C7 File Offset: 0x000253C7
		public static string Web_Contents_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Contents_Example2");
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x000271D3 File Offset: 0x000253D3
		public static string Web_Contents_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Contents_Example3");
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x000271DF File Offset: 0x000253DF
		public static string Web_Headers
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Headers");
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000271EB File Offset: 0x000253EB
		public static string Web_Headers_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Headers_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00027205 File Offset: 0x00025405
		public static string Web_Headers_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Headers_Example1");
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00027211 File Offset: 0x00025411
		public static string WebAction_Request
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebAction_Request");
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002721D File Offset: 0x0002541D
		public static string WebAction_Request_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("WebAction_Request_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0002723B File Offset: 0x0002543B
		public static string WebAction_Request_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebAction_Request_Example1");
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00027247 File Offset: 0x00025447
		public static string WebMethod_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Type");
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00027253 File Offset: 0x00025453
		public static string WebMethod_Delete
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Delete");
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0002725F File Offset: 0x0002545F
		public static string WebMethod_Get
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Get");
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0002726B File Offset: 0x0002546B
		public static string WebMethod_Head
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Head");
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00027277 File Offset: 0x00025477
		public static string WebMethod_Patch
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Patch");
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00027283 File Offset: 0x00025483
		public static string WebMethod_Post
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Post");
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002728F File Offset: 0x0002548F
		public static string WebMethod_Put
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("WebMethod_Put");
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0002729B File Offset: 0x0002549B
		public static string Http_Get
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Http_Get");
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x000272A7 File Offset: 0x000254A7
		public static string Http_Post
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Http_Post");
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x000272B3 File Offset: 0x000254B3
		public static string OData_Feed
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("OData_Feed");
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x000272BF File Offset: 0x000254BF
		public static string Web_Service
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Service");
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x000272CB File Offset: 0x000254CB
		public static string SharePoint_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SharePoint_Contents");
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000272D7 File Offset: 0x000254D7
		public static string SharePoint_Contents_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("SharePoint_Contents_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x000272F1 File Offset: 0x000254F1
		public static string SharePoint_Tables
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SharePoint_Tables");
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000272FD File Offset: 0x000254FD
		public static string SharePoint_Tables_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("SharePoint_Tables_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00027317 File Offset: 0x00025517
		public static string Value_Equals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Equals");
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00027323 File Offset: 0x00025523
		public static string Value_Equals_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Equals_Description", new object[] { p0, p1 });
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0002733D File Offset: 0x0002553D
		public static string Value_Add
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Add");
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00027349 File Offset: 0x00025549
		public static string Value_Add_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Add_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00027367 File Offset: 0x00025567
		public static string Value_Subtract
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Subtract");
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00027373 File Offset: 0x00025573
		public static string Value_Subtract_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Subtract_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00027391 File Offset: 0x00025591
		public static string Value_Divide
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Divide");
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002739D File Offset: 0x0002559D
		public static string Value_Divide_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Divide_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x000273BB File Offset: 0x000255BB
		public static string Value_Multiply
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Multiply");
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000273C7 File Offset: 0x000255C7
		public static string Value_Multiply_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Multiply_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x000273E5 File Offset: 0x000255E5
		public static string Precision_Double
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Precision_Double");
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000273F1 File Offset: 0x000255F1
		public static string Precision_Decimal
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Precision_Decimal");
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000273FD File Offset: 0x000255FD
		public static string Value_RemoveMetadata
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_RemoveMetadata");
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00027409 File Offset: 0x00025609
		public static string Value_RemoveMetadata_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_RemoveMetadata_Example1");
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00027415 File Offset: 0x00025615
		public static string Value_RemoveMetadata_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_RemoveMetadata_Example2");
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00027421 File Offset: 0x00025621
		public static string Value_ReplaceMetadata
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ReplaceMetadata");
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0002742D File Offset: 0x0002562D
		public static string Value_Metadata
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Metadata");
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00027439 File Offset: 0x00025639
		public static string Binary_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Type");
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00027445 File Offset: 0x00025645
		public static string Byte_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Byte_Type");
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00027451 File Offset: 0x00025651
		public static string Int8_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int8_Type");
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0002745D File Offset: 0x0002565D
		public static string Int16_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int16_Type");
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00027469 File Offset: 0x00025669
		public static string Int32_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int32_Type");
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00027475 File Offset: 0x00025675
		public static string Int64_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int64_Type");
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00027481 File Offset: 0x00025681
		public static string Single_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Single_Type");
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0002748D File Offset: 0x0002568D
		public static string Decimal_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Decimal_Type");
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00027499 File Offset: 0x00025699
		public static string Double_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Double_Type");
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x000274A5 File Offset: 0x000256A5
		public static string Currency_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Currency_Type");
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x000274B1 File Offset: 0x000256B1
		public static string Percentage_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Percentage_Type");
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000274BD File Offset: 0x000256BD
		public static string Character_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_Type");
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x000274C9 File Offset: 0x000256C9
		public static string Function_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_Type");
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000274D5 File Offset: 0x000256D5
		public static string Duration_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Type");
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000274E1 File Offset: 0x000256E1
		public static string List_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Type");
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000274ED File Offset: 0x000256ED
		public static string Type_ForList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForList");
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000274F9 File Offset: 0x000256F9
		public static string Logical_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_Type");
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00027505 File Offset: 0x00025705
		public static string Type_ForRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForRecord");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00027511 File Offset: 0x00025711
		public static string Type_ForRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForRecord_Example1");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0002751D File Offset: 0x0002571D
		public static string Null_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Null_Type");
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00027529 File Offset: 0x00025729
		public static string Nullable_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Nullable_Type");
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00027535 File Offset: 0x00025735
		public static string Type_ForNullable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForNullable");
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00027541 File Offset: 0x00025741
		public static string Number_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_Type");
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0002754D File Offset: 0x0002574D
		public static string Record_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Type");
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00027559 File Offset: 0x00025759
		public static string Text_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Type");
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00027565 File Offset: 0x00025765
		public static string Type_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Type");
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00027571 File Offset: 0x00025771
		public static string Any_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Any_Type");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0002757D File Offset: 0x0002577D
		public static string Table_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Type");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00027589 File Offset: 0x00025789
		public static string Action_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Type");
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00027595 File Offset: 0x00025795
		public static string Value_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Type");
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000275A1 File Offset: 0x000257A1
		public static string Value_Compare
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Compare");
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000275AD File Offset: 0x000257AD
		public static string Value_Hash
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Hash");
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000275B9 File Offset: 0x000257B9
		public static string List_Sort
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sort");
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000275C5 File Offset: 0x000257C5
		public static string List_Sort_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sort_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000275DF File Offset: 0x000257DF
		public static string List_Sort_Example1(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sort_Example1", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000275FD File Offset: 0x000257FD
		public static string List_Sort_Example2(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sort_Example2", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002761B File Offset: 0x0002581B
		public static string List_Sort_Example3(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Sort_Example3", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00027639 File Offset: 0x00025839
		public static string List_Split
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Split");
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00027645 File Offset: 0x00025845
		public static string List_Split_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Split_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0002765F File Offset: 0x0002585F
		public static string Character_ToNumber
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_ToNumber");
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002766B File Offset: 0x0002586B
		public static string Character_ToNumber_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Character_ToNumber_Description", new object[] { p0 });
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00027681 File Offset: 0x00025881
		public static string Character_ToNumber_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_ToNumber_Example1");
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0002768D File Offset: 0x0002588D
		public static string Character_ToNumber_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Character_ToNumber_Example2");
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00027699 File Offset: 0x00025899
		public static string Logical_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_ToText");
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000276A5 File Offset: 0x000258A5
		public static string Logical_ToText_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_ToText_Description", new object[] { p0 });
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x000276BB File Offset: 0x000258BB
		public static string Logical_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_ToText_Example1");
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x000276C7 File Offset: 0x000258C7
		public static string Table_FromColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromColumns");
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000276D3 File Offset: 0x000258D3
		public static string Table_FromColumns_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromColumns_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x000276ED File Offset: 0x000258ED
		public static string Table_FromColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromColumns_Example1");
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x000276F9 File Offset: 0x000258F9
		public static string Table_FromColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromColumns_Example2");
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00027705 File Offset: 0x00025905
		public static string Table_ToColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToColumns");
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00027711 File Offset: 0x00025911
		public static string Table_ToColumns_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToColumns_Description", new object[] { p0 });
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00027727 File Offset: 0x00025927
		public static string Table_ToColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToColumns_Example1");
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00027733 File Offset: 0x00025933
		public static string Text_Length
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Length");
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002773F File Offset: 0x0002593F
		public static string Text_Length_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Length_Description", new object[] { p0 });
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00027755 File Offset: 0x00025955
		public static string Text_Length_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Length_Example1");
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00027761 File Offset: 0x00025961
		public static string Language_DateFromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateFromText");
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0002776D File Offset: 0x0002596D
		public static string Language_DateLiteral
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateLiteral");
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00027779 File Offset: 0x00025979
		public static string Language_False
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_False");
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00027785 File Offset: 0x00025985
		public static string Language_Number
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Number");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00027791 File Offset: 0x00025991
		public static string Language_NegativeNumber
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_NegativeNumber");
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0002779D File Offset: 0x0002599D
		public static string Language_ScientificNumber
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_ScientificNumber");
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000277A9 File Offset: 0x000259A9
		public static string Language_Text
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Text");
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000277B5 File Offset: 0x000259B5
		public static string Language_True
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_True");
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000277C1 File Offset: 0x000259C1
		public static string Language_TimeLiteral
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_TimeLiteral");
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000277CD File Offset: 0x000259CD
		public static string Language_DateTimeLiteral
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateTimeLiteral");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000277D9 File Offset: 0x000259D9
		public static string Language_TimeWithZoneLiteral
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_TimeWithZoneLiteral");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x000277E5 File Offset: 0x000259E5
		public static string Language_DateTimeWithZoneLiteral
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateTimeWithZoneLiteral");
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x000277F1 File Offset: 0x000259F1
		public static string Language_Character
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Character");
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x000277FD File Offset: 0x000259FD
		public static string Language_DurationLiteral
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DurationLiteral");
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00027809 File Offset: 0x00025A09
		public static string Language_Lists
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Lists");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00027815 File Offset: 0x00025A15
		public static string Language_Ranges
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Ranges");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00027821 File Offset: 0x00025A21
		public static string Language_MixedRanges
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_MixedRanges");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0002782D File Offset: 0x00025A2D
		public static string Language_Records
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Records");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00027839 File Offset: 0x00025A39
		public static string Language_Tables
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Tables");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00027845 File Offset: 0x00025A45
		public static string Language_Numbers_Math
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Numbers_Math");
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00027851 File Offset: 0x00025A51
		public static string Language_Time_Math
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Time_Math");
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0002785D File Offset: 0x00025A5D
		public static string Language_DateTime_Math
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateTime_Math");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00027869 File Offset: 0x00025A69
		public static string Language_Duration_Add
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Duration_Add");
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00027875 File Offset: 0x00025A75
		public static string Language_Duration_Subtract
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Duration_Subtract");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00027881 File Offset: 0x00025A81
		public static string Language_DateTime_Duration_Add
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateTime_Duration_Add");
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0002788D File Offset: 0x00025A8D
		public static string Language_List_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_List_Combine");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00027899 File Offset: 0x00025A99
		public static string Language_Record_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Record_Combine");
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x000278A5 File Offset: 0x00025AA5
		public static string Language_Text_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Text_Combine");
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000278B1 File Offset: 0x00025AB1
		public static string Language_List_Lookup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_List_Lookup");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x000278BD File Offset: 0x00025ABD
		public static string Language_Record_Lookup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Record_Lookup");
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000278C9 File Offset: 0x00025AC9
		public static string Language_Table_Lookup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Table_Lookup");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000278D5 File Offset: 0x00025AD5
		public static string Language_Table_LookupByKey
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Table_LookupByKey");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000278E1 File Offset: 0x00025AE1
		public static string Language_Table_Project
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Table_Project");
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000278ED File Offset: 0x00025AED
		public static string Language_Equals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Equals");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x000278F9 File Offset: 0x00025AF9
		public static string Language_NotEquals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_NotEquals");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00027905 File Offset: 0x00025B05
		public static string Language_And
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_And");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00027911 File Offset: 0x00025B11
		public static string Language_Or
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Or");
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0002791D File Offset: 0x00025B1D
		public static string Language_Compares
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Compares");
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00027929 File Offset: 0x00025B29
		public static string Language_Text_Compares
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Text_Compares");
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00027935 File Offset: 0x00025B35
		public static string Language_DateTime_Compares
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_DateTime_Compares");
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00027941 File Offset: 0x00025B41
		public static string Language_If
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_If");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002794D File Offset: 0x00025B4D
		public static string Language_Let
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Let");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00027959 File Offset: 0x00025B59
		public static string Language_Invocation
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Invocation");
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00027965 File Offset: 0x00025B65
		public static string Language_Function
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Function");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00027971 File Offset: 0x00025B71
		public static string Language_EachUnderscore
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_EachUnderscore");
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0002797D File Offset: 0x00025B7D
		public static string Language_EachColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_EachColumn");
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00027989 File Offset: 0x00025B89
		public static string Date_AddMonths
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddMonths");
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00027995 File Offset: 0x00025B95
		public static string Date_AddMonths_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddMonths_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x000279AF File Offset: 0x00025BAF
		public static string Date_AddMonths_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddMonths_Example1");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000279BB File Offset: 0x00025BBB
		public static string Date_AddMonths_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddMonths_Example2");
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x000279C7 File Offset: 0x00025BC7
		public static string Date_AddYears
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddYears");
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000279D3 File Offset: 0x00025BD3
		public static string Date_AddYears_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddYears_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000279ED File Offset: 0x00025BED
		public static string Date_AddYears_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddYears_Example1");
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x000279F9 File Offset: 0x00025BF9
		public static string Date_AddYears_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddYears_Example2");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00027A05 File Offset: 0x00025C05
		public static string DateTime_IsInPreviousSecond
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousSecond");
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00027A11 File Offset: 0x00025C11
		public static string DateTime_IsInPreviousSecond_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousSecond_Description", new object[] { p0 });
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00027A27 File Offset: 0x00025C27
		public static string DateTime_IsInPreviousSecond_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousSecond_Example1");
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00027A33 File Offset: 0x00025C33
		public static string DateTime_IsInPreviousNSeconds
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNSeconds");
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00027A3F File Offset: 0x00025C3F
		public static string DateTime_IsInPreviousNSeconds_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNSeconds_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00027A59 File Offset: 0x00025C59
		public static string DateTime_IsInPreviousNSeconds_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNSeconds_Example1");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00027A65 File Offset: 0x00025C65
		public static string DateTime_IsInCurrentSecond
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentSecond");
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00027A71 File Offset: 0x00025C71
		public static string DateTime_IsInCurrentSecond_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentSecond_Description", new object[] { p0 });
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00027A87 File Offset: 0x00025C87
		public static string DateTime_IsInCurrentSecond_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentSecond_Example1");
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00027A93 File Offset: 0x00025C93
		public static string DateTime_IsInNextSecond
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextSecond");
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00027A9F File Offset: 0x00025C9F
		public static string DateTime_IsInNextSecond_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextSecond_Description", new object[] { p0 });
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00027AB5 File Offset: 0x00025CB5
		public static string DateTime_IsInNextSecond_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextSecond_Example1");
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00027AC1 File Offset: 0x00025CC1
		public static string DateTime_IsInNextNSeconds
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNSeconds");
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00027ACD File Offset: 0x00025CCD
		public static string DateTime_IsInNextNSeconds_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNSeconds_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00027AE7 File Offset: 0x00025CE7
		public static string DateTime_IsInNextNSeconds_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNSeconds_Example1");
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00027AF3 File Offset: 0x00025CF3
		public static string DateTime_IsInPreviousMinute
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousMinute");
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00027AFF File Offset: 0x00025CFF
		public static string DateTime_IsInPreviousMinute_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousMinute_Description", new object[] { p0 });
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00027B15 File Offset: 0x00025D15
		public static string DateTime_IsInPreviousMinute_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousMinute_Example1");
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00027B21 File Offset: 0x00025D21
		public static string DateTime_IsInPreviousNMinutes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNMinutes");
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00027B2D File Offset: 0x00025D2D
		public static string DateTime_IsInPreviousNMinutes_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNMinutes_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00027B47 File Offset: 0x00025D47
		public static string DateTime_IsInPreviousNMinutes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNMinutes_Example1");
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00027B53 File Offset: 0x00025D53
		public static string DateTime_IsInCurrentMinute
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentMinute");
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00027B5F File Offset: 0x00025D5F
		public static string DateTime_IsInCurrentMinute_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentMinute_Description", new object[] { p0 });
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00027B75 File Offset: 0x00025D75
		public static string DateTime_IsInCurrentMinute_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentMinute_Example1");
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00027B81 File Offset: 0x00025D81
		public static string DateTime_IsInNextMinute
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextMinute");
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00027B8D File Offset: 0x00025D8D
		public static string DateTime_IsInNextMinute_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextMinute_Description", new object[] { p0 });
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00027BA3 File Offset: 0x00025DA3
		public static string DateTime_IsInNextMinute_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextMinute_Example1");
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00027BAF File Offset: 0x00025DAF
		public static string DateTime_IsInNextNMinutes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNMinutes");
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00027BBB File Offset: 0x00025DBB
		public static string DateTime_IsInNextNMinutes_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNMinutes_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00027BD5 File Offset: 0x00025DD5
		public static string DateTime_IsInNextNMinutes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNMinutes_Example1");
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00027BE1 File Offset: 0x00025DE1
		public static string DateTime_IsInPreviousHour
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousHour");
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00027BED File Offset: 0x00025DED
		public static string DateTime_IsInPreviousHour_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousHour_Description", new object[] { p0 });
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00027C03 File Offset: 0x00025E03
		public static string DateTime_IsInPreviousHour_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousHour_Example1");
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00027C0F File Offset: 0x00025E0F
		public static string DateTime_IsInPreviousNHours
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNHours");
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00027C1B File Offset: 0x00025E1B
		public static string DateTime_IsInPreviousNHours_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNHours_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00027C35 File Offset: 0x00025E35
		public static string DateTime_IsInPreviousNHours_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInPreviousNHours_Example1");
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00027C41 File Offset: 0x00025E41
		public static string DateTime_IsInCurrentHour
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentHour");
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00027C4D File Offset: 0x00025E4D
		public static string DateTime_IsInCurrentHour_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentHour_Description", new object[] { p0 });
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00027C63 File Offset: 0x00025E63
		public static string DateTime_IsInCurrentHour_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInCurrentHour_Example1");
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00027C6F File Offset: 0x00025E6F
		public static string DateTime_IsInNextHour
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextHour");
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00027C7B File Offset: 0x00025E7B
		public static string DateTime_IsInNextHour_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextHour_Description", new object[] { p0 });
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00027C91 File Offset: 0x00025E91
		public static string DateTime_IsInNextHour_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextHour_Example1");
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00027C9D File Offset: 0x00025E9D
		public static string DateTime_IsInNextNHours
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNHours");
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00027CA9 File Offset: 0x00025EA9
		public static string DateTime_IsInNextNHours_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNHours_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00027CC3 File Offset: 0x00025EC3
		public static string DateTime_IsInNextNHours_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_IsInNextNHours_Example1");
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00027CCF File Offset: 0x00025ECF
		public static string Date_IsInPreviousDay
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousDay");
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00027CDB File Offset: 0x00025EDB
		public static string Date_IsInPreviousDay_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousDay_Description", new object[] { p0 });
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00027CF1 File Offset: 0x00025EF1
		public static string Date_IsInPreviousDay_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousDay_Example1");
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00027CFD File Offset: 0x00025EFD
		public static string Date_IsInPreviousNDays
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNDays");
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00027D09 File Offset: 0x00025F09
		public static string Date_IsInPreviousNDays_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNDays_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00027D23 File Offset: 0x00025F23
		public static string Date_IsInPreviousNDays_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNDays_Example1");
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00027D2F File Offset: 0x00025F2F
		public static string Date_IsInCurrentDay
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentDay");
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00027D3B File Offset: 0x00025F3B
		public static string Date_IsInCurrentDay_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentDay_Description", new object[] { p0 });
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00027D51 File Offset: 0x00025F51
		public static string Date_IsInCurrentDay_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentDay_Example1");
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00027D5D File Offset: 0x00025F5D
		public static string Date_IsInNextDay
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextDay");
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00027D69 File Offset: 0x00025F69
		public static string Date_IsInNextDay_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextDay_Description", new object[] { p0 });
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00027D7F File Offset: 0x00025F7F
		public static string Date_IsInNextDay_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextDay_Example1");
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00027D8B File Offset: 0x00025F8B
		public static string Date_IsInNextNDays
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNDays");
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00027D97 File Offset: 0x00025F97
		public static string Date_IsInNextNDays_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNDays_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00027DB1 File Offset: 0x00025FB1
		public static string Date_IsInNextNDays_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNDays_Example1");
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00027DBD File Offset: 0x00025FBD
		public static string Date_IsInPreviousWeek
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousWeek");
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00027DC9 File Offset: 0x00025FC9
		public static string Date_IsInPreviousWeek_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousWeek_Description", new object[] { p0 });
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00027DDF File Offset: 0x00025FDF
		public static string Date_IsInPreviousWeek_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousWeek_Example1");
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00027DEB File Offset: 0x00025FEB
		public static string Date_IsInPreviousNWeeks
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNWeeks");
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00027DF7 File Offset: 0x00025FF7
		public static string Date_IsInPreviousNWeeks_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNWeeks_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00027E11 File Offset: 0x00026011
		public static string Date_IsInPreviousNWeeks_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNWeeks_Example1");
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00027E1D File Offset: 0x0002601D
		public static string Date_IsInCurrentWeek
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentWeek");
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00027E29 File Offset: 0x00026029
		public static string Date_IsInCurrentWeek_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentWeek_Description", new object[] { p0 });
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00027E3F File Offset: 0x0002603F
		public static string Date_IsInCurrentWeek_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentWeek_Example1");
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00027E4B File Offset: 0x0002604B
		public static string Date_IsInNextWeek
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextWeek");
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00027E57 File Offset: 0x00026057
		public static string Date_IsInNextWeek_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextWeek_Description", new object[] { p0 });
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00027E6D File Offset: 0x0002606D
		public static string Date_IsInNextWeek_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextWeek_Example1");
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00027E79 File Offset: 0x00026079
		public static string Date_IsInNextNWeeks
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNWeeks");
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00027E85 File Offset: 0x00026085
		public static string Date_IsInNextNWeeks_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNWeeks_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00027E9F File Offset: 0x0002609F
		public static string Date_IsInNextNWeeks_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNWeeks_Example1");
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00027EAB File Offset: 0x000260AB
		public static string Date_IsInPreviousMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousMonth");
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00027EB7 File Offset: 0x000260B7
		public static string Date_IsInPreviousMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousMonth_Description", new object[] { p0 });
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00027ECD File Offset: 0x000260CD
		public static string Date_IsInPreviousMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousMonth_Example1");
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00027ED9 File Offset: 0x000260D9
		public static string Date_IsInPreviousNMonths
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNMonths");
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00027EE5 File Offset: 0x000260E5
		public static string Date_IsInPreviousNMonths_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNMonths_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00027EFF File Offset: 0x000260FF
		public static string Date_IsInPreviousNMonths_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNMonths_Example1");
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00027F0B File Offset: 0x0002610B
		public static string Date_IsInCurrentMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentMonth");
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00027F17 File Offset: 0x00026117
		public static string Date_IsInCurrentMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentMonth_Description", new object[] { p0 });
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00027F2D File Offset: 0x0002612D
		public static string Date_IsInCurrentMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentMonth_Example1");
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00027F39 File Offset: 0x00026139
		public static string Date_IsInNextMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextMonth");
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00027F45 File Offset: 0x00026145
		public static string Date_IsInNextMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextMonth_Description", new object[] { p0 });
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00027F5B File Offset: 0x0002615B
		public static string Date_IsInNextMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextMonth_Example1");
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00027F67 File Offset: 0x00026167
		public static string Date_IsInNextNMonths
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNMonths");
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00027F73 File Offset: 0x00026173
		public static string Date_IsInNextNMonths_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNMonths_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00027F8D File Offset: 0x0002618D
		public static string Date_IsInNextNMonths_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNMonths_Example1");
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00027F99 File Offset: 0x00026199
		public static string Date_IsInPreviousQuarter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousQuarter");
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00027FA5 File Offset: 0x000261A5
		public static string Date_IsInPreviousQuarter_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousQuarter_Description", new object[] { p0 });
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00027FBB File Offset: 0x000261BB
		public static string Date_IsInPreviousQuarter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousQuarter_Example1");
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00027FC7 File Offset: 0x000261C7
		public static string Date_IsInPreviousNQuarters
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNQuarters");
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00027FD3 File Offset: 0x000261D3
		public static string Date_IsInPreviousNQuarters_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNQuarters_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00027FED File Offset: 0x000261ED
		public static string Date_IsInPreviousNQuarters_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNQuarters_Example1");
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00027FF9 File Offset: 0x000261F9
		public static string Date_IsInCurrentQuarter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentQuarter");
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00028005 File Offset: 0x00026205
		public static string Date_IsInCurrentQuarter_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentQuarter_Description", new object[] { p0 });
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0002801B File Offset: 0x0002621B
		public static string Date_IsInCurrentQuarter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentQuarter_Example1");
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00028027 File Offset: 0x00026227
		public static string Date_IsInNextQuarter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextQuarter");
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00028033 File Offset: 0x00026233
		public static string Date_IsInNextQuarter_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextQuarter_Description", new object[] { p0 });
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00028049 File Offset: 0x00026249
		public static string Date_IsInNextQuarter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextQuarter_Example1");
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00028055 File Offset: 0x00026255
		public static string Date_IsInNextNQuarters
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNQuarters");
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00028061 File Offset: 0x00026261
		public static string Date_IsInNextNQuarters_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNQuarters_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002807B File Offset: 0x0002627B
		public static string Date_IsInNextNQuarters_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNQuarters_Example1");
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00028087 File Offset: 0x00026287
		public static string Date_IsInPreviousYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousYear");
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00028093 File Offset: 0x00026293
		public static string Date_IsInPreviousYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousYear_Description", new object[] { p0 });
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x000280A9 File Offset: 0x000262A9
		public static string Date_IsInPreviousYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousYear_Example1");
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000280B5 File Offset: 0x000262B5
		public static string Date_IsInPreviousNYears
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNYears");
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000280C1 File Offset: 0x000262C1
		public static string Date_IsInPreviousNYears_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNYears_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000280DB File Offset: 0x000262DB
		public static string Date_IsInPreviousNYears_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInPreviousNYears_Example1");
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x000280E7 File Offset: 0x000262E7
		public static string Date_IsInCurrentYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentYear");
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000280F3 File Offset: 0x000262F3
		public static string Date_IsInCurrentYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentYear_Description", new object[] { p0 });
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00028109 File Offset: 0x00026309
		public static string Date_IsInCurrentYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInCurrentYear_Example1");
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00028115 File Offset: 0x00026315
		public static string Date_IsInNextYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextYear");
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00028121 File Offset: 0x00026321
		public static string Date_IsInNextYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextYear_Description", new object[] { p0 });
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00028137 File Offset: 0x00026337
		public static string Date_IsInNextYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextYear_Example1");
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00028143 File Offset: 0x00026343
		public static string Date_IsInNextNYears
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNYears");
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002814F File Offset: 0x0002634F
		public static string Date_IsInNextNYears_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNYears_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00028169 File Offset: 0x00026369
		public static string Date_IsInNextNYears_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInNextNYears_Example1");
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00028175 File Offset: 0x00026375
		public static string Date_IsInYearToDate
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInYearToDate");
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00028181 File Offset: 0x00026381
		public static string Date_IsInYearToDate_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInYearToDate_Description", new object[] { p0 });
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00028197 File Offset: 0x00026397
		public static string Date_IsInYearToDate_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_IsInYearToDate_Example1");
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000281A3 File Offset: 0x000263A3
		public static string Date_AddDays
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddDays");
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000281AF File Offset: 0x000263AF
		public static string Date_AddDays_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddDays_Description", new object[] { p0, p1 });
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x000281C9 File Offset: 0x000263C9
		public static string Date_AddDays_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddDays_Example1");
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000281D5 File Offset: 0x000263D5
		public static string Date_AddWeeks
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddWeeks");
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000281E1 File Offset: 0x000263E1
		public static string Date_AddWeeks_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddWeeks_Description", new object[] { p0, p1 });
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x000281FB File Offset: 0x000263FB
		public static string Date_AddWeeks_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddWeeks_Example1");
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00028207 File Offset: 0x00026407
		public static string Date_AddQuarters
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddQuarters");
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00028213 File Offset: 0x00026413
		public static string Date_AddQuarters_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddQuarters_Description", new object[] { p0, p1 });
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0002822D File Offset: 0x0002642D
		public static string Date_AddQuarters_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_AddQuarters_Example1");
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00028239 File Offset: 0x00026439
		public static string Date_StartOfQuarter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfQuarter");
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00028245 File Offset: 0x00026445
		public static string Date_StartOfQuarter_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfQuarter_Description", new object[] { p0 });
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0002825B File Offset: 0x0002645B
		public static string Date_StartOfQuarter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfQuarter_Example1");
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00028267 File Offset: 0x00026467
		public static string Date_EndOfQuarter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfQuarter");
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00028273 File Offset: 0x00026473
		public static string Date_EndOfQuarter_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfQuarter_Description", new object[] { p0 });
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00028289 File Offset: 0x00026489
		public static string Date_EndOfQuarter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfQuarter_Example1");
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00028295 File Offset: 0x00026495
		public static string Date_Day
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Day");
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000282A1 File Offset: 0x000264A1
		public static string Date_Day_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Day_Description", new object[] { p0 });
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000282B7 File Offset: 0x000264B7
		public static string Date_Day_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Day_Example1");
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000282C3 File Offset: 0x000264C3
		public static string Date_EndOfDay
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfDay");
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x000282CF File Offset: 0x000264CF
		public static string Date_EndOfDay_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfDay_Description", new object[] { p0 });
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x000282E5 File Offset: 0x000264E5
		public static string Date_EndOfDay_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfDay_Example1");
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x000282F1 File Offset: 0x000264F1
		public static string Date_EndOfDay_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfDay_Example2");
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x000282FD File Offset: 0x000264FD
		public static string Time_EndOfHour
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_EndOfHour");
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00028309 File Offset: 0x00026509
		public static string Time_EndOfHour_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_EndOfHour_Description", new object[] { p0 });
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0002831F File Offset: 0x0002651F
		public static string Time_EndOfHour_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_EndOfHour_Example1");
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0002832B File Offset: 0x0002652B
		public static string Time_EndOfHour_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_EndOfHour_Example2");
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00028337 File Offset: 0x00026537
		public static string Date_EndOfMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfMonth");
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00028343 File Offset: 0x00026543
		public static string Date_EndOfMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfMonth_Description", new object[] { p0 });
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00028359 File Offset: 0x00026559
		public static string Date_EndOfMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfMonth_Example1");
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00028365 File Offset: 0x00026565
		public static string Date_EndOfMonth_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfMonth_Example2");
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00028371 File Offset: 0x00026571
		public static string Date_EndOfWeek
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfWeek");
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002837D File Offset: 0x0002657D
		public static string Date_EndOfWeek_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfWeek_Description", new object[] { p0, p1 });
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00028397 File Offset: 0x00026597
		public static string Date_EndOfWeek_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfWeek_Example1");
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x000283A3 File Offset: 0x000265A3
		public static string Date_EndOfWeek_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfWeek_Example2");
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000283AF File Offset: 0x000265AF
		public static string Date_EndOfYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfYear");
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000283BB File Offset: 0x000265BB
		public static string Date_EndOfYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfYear_Description", new object[] { p0 });
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x000283D1 File Offset: 0x000265D1
		public static string Date_EndOfYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfYear_Example1");
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000283DD File Offset: 0x000265DD
		public static string Date_EndOfYear_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_EndOfYear_Example2");
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x000283E9 File Offset: 0x000265E9
		public static string Date_StartOfDay
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfDay");
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000283F5 File Offset: 0x000265F5
		public static string Date_StartOfDay_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfDay_Description", new object[] { p0 });
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0002840B File Offset: 0x0002660B
		public static string Date_StartOfDay_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfDay_Example1");
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00028417 File Offset: 0x00026617
		public static string Time_StartOfHour
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_StartOfHour");
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00028423 File Offset: 0x00026623
		public static string Time_StartOfHour_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_StartOfHour_Description", new object[] { p0 });
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00028439 File Offset: 0x00026639
		public static string Time_StartOfHour_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_StartOfHour_Example1");
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00028445 File Offset: 0x00026645
		public static string Date_StartOfMonth
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfMonth");
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00028451 File Offset: 0x00026651
		public static string Date_StartOfMonth_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfMonth_Description", new object[] { p0 });
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00028467 File Offset: 0x00026667
		public static string Date_StartOfMonth_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfMonth_Example1");
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00028473 File Offset: 0x00026673
		public static string Date_StartOfWeek
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfWeek");
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002847F File Offset: 0x0002667F
		public static string Date_StartOfWeek_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfWeek_Description", new object[] { p0 });
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00028495 File Offset: 0x00026695
		public static string Date_StartOfWeek_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfWeek_Example1");
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000284A1 File Offset: 0x000266A1
		public static string Date_StartOfWeek_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfWeek_Example2");
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x000284AD File Offset: 0x000266AD
		public static string Date_StartOfYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfYear");
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000284B9 File Offset: 0x000266B9
		public static string Date_StartOfYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfYear_Description", new object[] { p0 });
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x000284CF File Offset: 0x000266CF
		public static string Date_StartOfYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_StartOfYear_Example1");
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000284DB File Offset: 0x000266DB
		public static string DateTimeZone_UtcNow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_UtcNow");
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000284E7 File Offset: 0x000266E7
		public static string DateTimeZone_UtcNow_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_UtcNow_Example1");
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x000284F3 File Offset: 0x000266F3
		public static string DateTimeZone_FixedUtcNow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FixedUtcNow");
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000284FF File Offset: 0x000266FF
		public static string Time_Hour_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Hour_Description", new object[] { p0 });
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00028515 File Offset: 0x00026715
		public static string Time_Hour
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Hour");
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00028521 File Offset: 0x00026721
		public static string Time_Hour_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Hour_Example1");
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0002852D File Offset: 0x0002672D
		public static string Time_Minute
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Minute");
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00028539 File Offset: 0x00026739
		public static string Time_Minute_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Minute_Description", new object[] { p0 });
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0002854F File Offset: 0x0002674F
		public static string Time_Minute_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Minute_Example1");
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0002855B File Offset: 0x0002675B
		public static string Date_Month
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Month");
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00028567 File Offset: 0x00026767
		public static string Date_Month_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Month_Description", new object[] { p0 });
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0002857D File Offset: 0x0002677D
		public static string Date_Month_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Month_Example1");
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00028589 File Offset: 0x00026789
		public static string Date_MonthName
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_MonthName");
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00028595 File Offset: 0x00026795
		public static string Date_MonthName_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_MonthName_Description", new object[] { p0, p1 });
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000285AF File Offset: 0x000267AF
		public static string Date_MonthName_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_MonthName_Example1");
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x000285BB File Offset: 0x000267BB
		public static string Date_QuarterOfYear
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_QuarterOfYear");
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000285C7 File Offset: 0x000267C7
		public static string Date_QuarterOfYear_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_QuarterOfYear_Description", new object[] { p0 });
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x000285DD File Offset: 0x000267DD
		public static string Date_QuarterOfYear_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_QuarterOfYear_Example1");
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x000285E9 File Offset: 0x000267E9
		public static string DateTimeZone_RemoveZone
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_RemoveZone");
			}
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x000285F5 File Offset: 0x000267F5
		public static string DateTimeZone_RemoveZone_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_RemoveZone_Description", new object[] { p0 });
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0002860B File Offset: 0x0002680B
		public static string DateTimeZone_RemoveZone_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_RemoveZone_Example1");
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00028617 File Offset: 0x00026817
		public static string Time_Second
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Second");
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00028623 File Offset: 0x00026823
		public static string Time_Second_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Second_Description", new object[] { p0 });
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00028639 File Offset: 0x00026839
		public static string Time_Second_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Second_Example1");
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00028645 File Offset: 0x00026845
		public static string DateTimeZone_SwitchZone
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_SwitchZone");
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00028651 File Offset: 0x00026851
		public static string DateTimeZone_SwitchZone_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_SwitchZone_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002866F File Offset: 0x0002686F
		public static string DateTimeZone_SwitchZone_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_SwitchZone_Example1");
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0002867B File Offset: 0x0002687B
		public static string DateTimeZone_SwitchZone_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_SwitchZone_Example2");
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00028687 File Offset: 0x00026887
		public static string DateTimeZone_ZoneHours
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ZoneHours");
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00028693 File Offset: 0x00026893
		public static string DateTimeZone_ZoneMinutes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ZoneMinutes");
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0002869F File Offset: 0x0002689F
		public static string DateTimeZone_ToLocal
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToLocal");
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000286AB File Offset: 0x000268AB
		public static string DateTimeZone_ToLocal_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToLocal_Description", new object[] { p0 });
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x000286C1 File Offset: 0x000268C1
		public static string DateTimeZone_ToLocal_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToLocal_Example1");
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000286CD File Offset: 0x000268CD
		public static string DateTimeZone_ToUtc
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToUtc");
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000286D9 File Offset: 0x000268D9
		public static string DateTimeZone_ToUtc_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToUtc_Description", new object[] { p0 });
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x000286EF File Offset: 0x000268EF
		public static string DateTimeZone_ToUtc_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToUtc_Example1");
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x000286FB File Offset: 0x000268FB
		public static string Date_Year
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Year");
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00028707 File Offset: 0x00026907
		public static string Date_Year_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Year_Description", new object[] { p0 });
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0002871D File Offset: 0x0002691D
		public static string Date_Year_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Year_Example1");
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00028729 File Offset: 0x00026929
		public static string Duration_Days
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Days");
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00028735 File Offset: 0x00026935
		public static string Duration_Days_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Days_Description", new object[] { p0 });
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002874B File Offset: 0x0002694B
		public static string Duration_Days_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Days_Example1");
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00028757 File Offset: 0x00026957
		public static string Duration_Hours
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Hours");
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00028763 File Offset: 0x00026963
		public static string Duration_Hours_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Hours_Description", new object[] { p0 });
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00028779 File Offset: 0x00026979
		public static string Duration_Hours_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Hours_Example1");
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00028785 File Offset: 0x00026985
		public static string Duration_Minutes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Minutes");
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00028791 File Offset: 0x00026991
		public static string Duration_Minutes_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Minutes_Description", new object[] { p0 });
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x000287A7 File Offset: 0x000269A7
		public static string Duration_Minutes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Minutes_Example1");
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x000287B3 File Offset: 0x000269B3
		public static string Duration_Seconds
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Seconds");
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000287BF File Offset: 0x000269BF
		public static string Duration_Seconds_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Seconds_Description", new object[] { p0 });
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x000287D5 File Offset: 0x000269D5
		public static string Duration_Seconds_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_Seconds_Example1");
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000287E1 File Offset: 0x000269E1
		public static string Duration_TotalDays
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalDays");
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000287ED File Offset: 0x000269ED
		public static string Duration_TotalDays_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalDays_Description", new object[] { p0 });
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00028803 File Offset: 0x00026A03
		public static string Duration_TotalDays_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalDays_Example1");
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0002880F File Offset: 0x00026A0F
		public static string Duration_TotalHours
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalHours");
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002881B File Offset: 0x00026A1B
		public static string Duration_TotalHours_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalHours_Description", new object[] { p0 });
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00028831 File Offset: 0x00026A31
		public static string Duration_TotalHours_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalHours_Example1");
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002883D File Offset: 0x00026A3D
		public static string Duration_TotalMinutes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalMinutes");
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00028849 File Offset: 0x00026A49
		public static string Duration_TotalMinutes_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalMinutes_Description", new object[] { p0 });
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0002885F File Offset: 0x00026A5F
		public static string Duration_TotalMinutes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalMinutes_Example1");
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002886B File Offset: 0x00026A6B
		public static string Duration_TotalSeconds
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalSeconds");
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00028877 File Offset: 0x00026A77
		public static string Duration_TotalSeconds_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalSeconds_Description", new object[] { p0 });
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002888D File Offset: 0x00026A8D
		public static string Duration_TotalSeconds_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_TotalSeconds_Example1");
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00028899 File Offset: 0x00026A99
		public static string TextComparer_Default
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextComparer_Default");
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x000288A5 File Offset: 0x00026AA5
		public static string TextComparer_IgnoreCase
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextComparer_IgnoreCase");
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000288B1 File Offset: 0x00026AB1
		public static string TextEquater_Default
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEquater_Default");
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x000288BD File Offset: 0x00026ABD
		public static string TextEquater_IgnoreCase
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEquater_IgnoreCase");
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x000288C9 File Offset: 0x00026AC9
		public static string Text_Contains
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Contains");
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000288D5 File Offset: 0x00026AD5
		public static string Text_Contains_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Contains_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000288F3 File Offset: 0x00026AF3
		public static string Text_Contains_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Contains_Example1");
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000288FF File Offset: 0x00026AFF
		public static string Text_Contains_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Contains_Example2");
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0002890B File Offset: 0x00026B0B
		public static string Text_Contains_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Contains_Example3");
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00028917 File Offset: 0x00026B17
		public static string Text_Range
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Range");
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00028923 File Offset: 0x00026B23
		public static string Text_Range_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Range_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00028941 File Offset: 0x00026B41
		public static string Text_Range_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Range_Example1");
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0002894D File Offset: 0x00026B4D
		public static string Text_Range_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Range_Example2");
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00028959 File Offset: 0x00026B59
		public static string Text_Middle
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Middle");
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00028965 File Offset: 0x00026B65
		public static string Text_Middle_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Middle_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00028983 File Offset: 0x00026B83
		public static string Text_Middle_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Middle_Example1");
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0002898F File Offset: 0x00026B8F
		public static string Text_Middle_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Middle_Example2");
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0002899B File Offset: 0x00026B9B
		public static string Error_Record
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Error_Record");
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000289A7 File Offset: 0x00026BA7
		public static string List_DateTimes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_DateTimes");
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000289B3 File Offset: 0x00026BB3
		public static string List_DateTimes_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_DateTimes_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x000289D1 File Offset: 0x00026BD1
		public static string List_DateTimes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_DateTimes_Example1");
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x000289DD File Offset: 0x00026BDD
		public static string List_Density
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Density");
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000289E9 File Offset: 0x00026BE9
		public static string List_Density_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Density_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00028A03 File Offset: 0x00026C03
		public static string List_Density_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Density_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00028A1D File Offset: 0x00026C1D
		public static string List_Durations
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Durations");
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00028A29 File Offset: 0x00026C29
		public static string List_Durations_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Durations_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00028A47 File Offset: 0x00026C47
		public static string List_Durations_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Durations_Example1");
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00028A53 File Offset: 0x00026C53
		public static string List_Generate
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Generate");
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00028A5F File Offset: 0x00026C5F
		public static string List_Generate_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Generate_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00028A81 File Offset: 0x00026C81
		public static string List_Generate_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Generate_Example1");
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00028A8D File Offset: 0x00026C8D
		public static string List_Generate_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Generate_Example2");
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00028A99 File Offset: 0x00026C99
		public static string List_Histogram
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Histogram");
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00028AA5 File Offset: 0x00026CA5
		public static string List_Histogram_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Histogram_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00028AC3 File Offset: 0x00026CC3
		public static string List_Histogram_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Histogram_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00028ADD File Offset: 0x00026CDD
		public static string List_Histogram_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Histogram_Example2", new object[] { p0, p1 });
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00028AF7 File Offset: 0x00026CF7
		public static string List_Histogram_Example3(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Histogram_Example3", new object[] { p0, p1 });
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00028B11 File Offset: 0x00026D11
		public static string List_Intersect
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Intersect");
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00028B1D File Offset: 0x00026D1D
		public static string List_Intersect_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Intersect_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00028B37 File Offset: 0x00026D37
		public static string List_Intersect_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Intersect_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00028B59 File Offset: 0x00026D59
		public static string List_IsDistinct
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsDistinct");
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00028B65 File Offset: 0x00026D65
		public static string List_IsDistinct_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsDistinct_Description", new object[] { p0 });
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00028B7B File Offset: 0x00026D7B
		public static string List_IsDistinct_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsDistinct_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00028B95 File Offset: 0x00026D95
		public static string List_IsDistinct_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsDistinct_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00028BAF File Offset: 0x00026DAF
		public static string List_IsEmpty
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsEmpty");
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00028BBB File Offset: 0x00026DBB
		public static string List_IsEmpty_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsEmpty_Description", new object[] { p0 });
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00028BD1 File Offset: 0x00026DD1
		public static string List_IsEmpty_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsEmpty_Example1");
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00028BDD File Offset: 0x00026DDD
		public static string List_IsEmpty_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_IsEmpty_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00028BF7 File Offset: 0x00026DF7
		public static string List_MatchesAll
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAll");
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00028C03 File Offset: 0x00026E03
		public static string List_MatchesAll_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAll_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00028C1D File Offset: 0x00026E1D
		public static string List_MatchesAll_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAll_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00028C37 File Offset: 0x00026E37
		public static string List_MatchesAll_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAll_Example2", new object[] { p0, p1 });
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00028C51 File Offset: 0x00026E51
		public static string List_MatchesAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAny");
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00028C5D File Offset: 0x00026E5D
		public static string List_MatchesAny_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAny_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00028C77 File Offset: 0x00026E77
		public static string List_MatchesAny_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAny_Example1");
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00028C83 File Offset: 0x00026E83
		public static string List_MatchesAny_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_MatchesAny_Example2", new object[] { p0, p1 });
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00028C9D File Offset: 0x00026E9D
		public static string List_RemoveMatchingItems
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveMatchingItems");
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00028CA9 File Offset: 0x00026EA9
		public static string List_RemoveMatchingItems_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveMatchingItems_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00028CC7 File Offset: 0x00026EC7
		public static string List_RemoveMatchingItems_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveMatchingItems_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00028CE1 File Offset: 0x00026EE1
		public static string List_RemoveItems
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveItems");
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00028CED File Offset: 0x00026EED
		public static string List_RemoveItems_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveItems_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00028D07 File Offset: 0x00026F07
		public static string List_RemoveItems_Example1(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveItems_Example1", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00028D25 File Offset: 0x00026F25
		public static string List_RemoveNulls
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveNulls");
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00028D31 File Offset: 0x00026F31
		public static string List_RemoveNulls_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveNulls_Description", new object[] { p0 });
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00028D47 File Offset: 0x00026F47
		public static string List_RemoveNulls_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_RemoveNulls_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00028D61 File Offset: 0x00026F61
		public static string List_ReplaceMatchingItems
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceMatchingItems");
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00028D6D File Offset: 0x00026F6D
		public static string List_ReplaceMatchingItems_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceMatchingItems_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00028D8B File Offset: 0x00026F8B
		public static string List_ReplaceMatchingItems_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ReplaceMatchingItems_Example1", new object[] { p0, p1 });
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00028DA5 File Offset: 0x00026FA5
		public static string List_Union
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Union");
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00028DB1 File Offset: 0x00026FB1
		public static string List_Union_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Union_Description", new object[] { p0, p1 });
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00028DCB File Offset: 0x00026FCB
		public static string List_Union_Example1(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Union_Example1", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00028DED File Offset: 0x00026FED
		public static string Number_RoundAwayFromZero
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundAwayFromZero");
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00028DF9 File Offset: 0x00026FF9
		public static string Number_RoundAwayFromZero_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundAwayFromZero_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00028E13 File Offset: 0x00027013
		public static string Number_RoundAwayFromZero_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundAwayFromZero_Example1");
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00028E1F File Offset: 0x0002701F
		public static string Number_RoundAwayFromZero_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundAwayFromZero_Example2");
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00028E2B File Offset: 0x0002702B
		public static string Number_RoundAwayFromZero_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundAwayFromZero_Example3");
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00028E37 File Offset: 0x00027037
		public static string Number_RoundTowardZero
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundTowardZero");
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00028E43 File Offset: 0x00027043
		public static string Number_RoundTowardZero_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundTowardZero_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00028E5D File Offset: 0x0002705D
		public static string Number_RoundTowardZero_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundTowardZero_Example1");
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00028E69 File Offset: 0x00027069
		public static string Number_RoundTowardZero_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundTowardZero_Example2");
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00028E75 File Offset: 0x00027075
		public static string Number_RoundTowardZero_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_RoundTowardZero_Example3");
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00028E81 File Offset: 0x00027081
		public static string Occurrence_All
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_All");
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00028E8D File Offset: 0x0002708D
		public static string Occurrence_First
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_First");
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00028E99 File Offset: 0x00027099
		public static string Occurrence_Last
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_Last");
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00028EA5 File Offset: 0x000270A5
		public static string Record_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Combine");
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00028EB1 File Offset: 0x000270B1
		public static string Record_Combine_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Combine_Description", new object[] { p0 });
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00028EC7 File Offset: 0x000270C7
		public static string Record_Combine_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_Combine_Example1");
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00028ED3 File Offset: 0x000270D3
		public static string Record_FromTable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromTable");
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00028EDF File Offset: 0x000270DF
		public static string Record_FromTable_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromTable_Description", new object[] { p0 });
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00028EF5 File Offset: 0x000270F5
		public static string Record_FromTable_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromTable_Example1");
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00028F01 File Offset: 0x00027101
		public static string Record_RemoveFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RemoveFields");
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00028F0D File Offset: 0x0002710D
		public static string Record_RemoveFields_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RemoveFields_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00028F27 File Offset: 0x00027127
		public static string Record_RemoveFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RemoveFields_Example1");
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00028F33 File Offset: 0x00027133
		public static string Record_RemoveFields_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RemoveFields_Example2");
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00028F3F File Offset: 0x0002713F
		public static string Record_RenameFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RenameFields");
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00028F4B File Offset: 0x0002714B
		public static string Record_RenameFields_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RenameFields_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00028F65 File Offset: 0x00027165
		public static string Record_RenameFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RenameFields_Example1");
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00028F71 File Offset: 0x00027171
		public static string Record_RenameFields_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_RenameFields_Example2");
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00028F7D File Offset: 0x0002717D
		public static string Record_ReorderFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ReorderFields");
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00028F89 File Offset: 0x00027189
		public static string Record_ReorderFields_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ReorderFields_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00028FA3 File Offset: 0x000271A3
		public static string Record_ReorderFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ReorderFields_Example1");
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00028FAF File Offset: 0x000271AF
		public static string Record_SelectFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_SelectFields");
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00028FBB File Offset: 0x000271BB
		public static string Record_SelectFields_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_SelectFields_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00028FD5 File Offset: 0x000271D5
		public static string Record_SelectFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_SelectFields_Example1");
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00028FE1 File Offset: 0x000271E1
		public static string Record_ToTable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ToTable");
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00028FED File Offset: 0x000271ED
		public static string Record_ToTable_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ToTable_Description", new object[] { p0 });
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00029003 File Offset: 0x00027203
		public static string Record_ToTable_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ToTable_Example1");
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0002900F File Offset: 0x0002720F
		public static string Record_TransformFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_TransformFields");
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0002901B File Offset: 0x0002721B
		public static string Record_TransformFields_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_TransformFields_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00029035 File Offset: 0x00027235
		public static string Record_TransformFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_TransformFields_Example1");
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00029041 File Offset: 0x00027241
		public static string Record_TransformFields_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_TransformFields_Example2");
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0002904D File Offset: 0x0002724D
		public static string Table_AddColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddColumn");
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00029059 File Offset: 0x00027259
		public static string Table_AddColumn_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddColumn_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00029077 File Offset: 0x00027277
		public static string Table_AddColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddColumn_Example1");
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00029083 File Offset: 0x00027283
		public static string Table_DuplicateColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_DuplicateColumn");
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002908F File Offset: 0x0002728F
		public static string Table_DuplicateColumn_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_DuplicateColumn_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000290AD File Offset: 0x000272AD
		public static string Table_DuplicateColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_DuplicateColumn_Example1");
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x000290B9 File Offset: 0x000272B9
		public static string Table_AddIndexColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddIndexColumn");
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000290C5 File Offset: 0x000272C5
		public static string Table_AddIndexColumn_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddIndexColumn_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x000290E7 File Offset: 0x000272E7
		public static string Table_AddIndexColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddIndexColumn_Example1");
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x000290F3 File Offset: 0x000272F3
		public static string Table_AddIndexColumn_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddIndexColumn_Example2");
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000290FF File Offset: 0x000272FF
		public static string Table_AddJoinColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddJoinColumn");
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002910B File Offset: 0x0002730B
		public static string Table_AddJoinColumn_Description(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddJoinColumn_Description", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00029132 File Offset: 0x00027332
		public static string Table_AddJoinColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddJoinColumn_Example1");
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0002913E File Offset: 0x0002733E
		public static string Table_AddRankColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddRankColumn");
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002914A File Offset: 0x0002734A
		public static string Table_AddRankColumn_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddRankColumn_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0002916C File Offset: 0x0002736C
		public static string Table_AddRankColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddRankColumn_Example1");
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00029178 File Offset: 0x00027378
		public static string RankKind_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RankKind_Type");
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00029184 File Offset: 0x00027384
		public static string RankKind_Competition
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RankKind_Competition");
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00029190 File Offset: 0x00027390
		public static string RankKind_Dense
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RankKind_Dense");
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0002919C File Offset: 0x0002739C
		public static string RankKind_Ordinal
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RankKind_Ordinal");
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000291A8 File Offset: 0x000273A8
		public static string Table_NestedJoin
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_NestedJoin");
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000291B4 File Offset: 0x000273B4
		public static string Table_NestedJoin_Description(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_NestedJoin_Description", new object[] { p0, p1, p2, p3, p4, p5, p6 });
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000291E5 File Offset: 0x000273E5
		public static string Table_NestedJoin_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_NestedJoin_Example1");
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000291F1 File Offset: 0x000273F1
		public static string Table_AlternateRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AlternateRows");
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000291FD File Offset: 0x000273FD
		public static string Table_AlternateRows_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AlternateRows_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0002921F File Offset: 0x0002741F
		public static string Table_AlternateRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AlternateRows_Example1");
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0002922B File Offset: 0x0002742B
		public static string Table_Buffer
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Buffer");
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00029237 File Offset: 0x00027437
		public static string Table_Buffer_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Buffer_Description");
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00029243 File Offset: 0x00027443
		public static string Table_Buffer_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Buffer_Example1");
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0002924F File Offset: 0x0002744F
		public static string Table_StopFolding
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_StopFolding");
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002925B File Offset: 0x0002745B
		public static string Table_StopFolding_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_StopFolding_Description", new object[] { p0 });
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00029271 File Offset: 0x00027471
		public static string Table_StopFolding_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_StopFolding_Example1");
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0002927D File Offset: 0x0002747D
		public static string Table_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Combine");
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00029289 File Offset: 0x00027489
		public static string Table_Combine_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Combine_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x000292A3 File Offset: 0x000274A3
		public static string Table_Combine_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Combine_Example1");
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000292AF File Offset: 0x000274AF
		public static string Table_Combine_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Combine_Example2");
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000292BB File Offset: 0x000274BB
		public static string Table_Combine_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Combine_Example3");
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x000292C7 File Offset: 0x000274C7
		public static string Table_ColumnCount
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnCount");
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000292D3 File Offset: 0x000274D3
		public static string Table_ColumnCount_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnCount_Description", new object[] { p0 });
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x000292E9 File Offset: 0x000274E9
		public static string Table_ColumnCount_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnCount_Example1");
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000292F5 File Offset: 0x000274F5
		public static string Table_ColumnNames
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnNames");
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00029301 File Offset: 0x00027501
		public static string Table_ColumnNames_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnNames_Description", new object[] { p0 });
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00029317 File Offset: 0x00027517
		public static string Table_ColumnNames_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ColumnNames_Example1");
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00029323 File Offset: 0x00027523
		public static string Table_Contains
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Contains");
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0002932F File Offset: 0x0002752F
		public static string Table_Contains_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Contains_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002934D File Offset: 0x0002754D
		public static string Table_Contains_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Contains_Example1");
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00029359 File Offset: 0x00027559
		public static string Table_Contains_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Contains_Example2");
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00029365 File Offset: 0x00027565
		public static string Table_Contains_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Contains_Example3");
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00029371 File Offset: 0x00027571
		public static string Table_ContainsAll
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAll");
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002937D File Offset: 0x0002757D
		public static string Table_ContainsAll_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAll_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0002939B File Offset: 0x0002759B
		public static string Table_ContainsAll_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAll_Example1");
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000293A7 File Offset: 0x000275A7
		public static string Table_ContainsAll_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAll_Example2");
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x000293B3 File Offset: 0x000275B3
		public static string Table_ContainsAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAny");
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000293BF File Offset: 0x000275BF
		public static string Table_ContainsAny_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAny_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000293DD File Offset: 0x000275DD
		public static string Table_ContainsAny_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAny_Example1");
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000293E9 File Offset: 0x000275E9
		public static string Table_ContainsAny_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAny_Example2");
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000293F5 File Offset: 0x000275F5
		public static string Table_ContainsAny_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ContainsAny_Example3");
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00029401 File Offset: 0x00027601
		public static string Table_Distinct
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Distinct");
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002940D File Offset: 0x0002760D
		public static string Table_Distinct_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Distinct_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00029427 File Offset: 0x00027627
		public static string Table_Distinct_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Distinct_Example1");
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00029433 File Offset: 0x00027633
		public static string Table_Distinct_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Distinct_Example2");
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0002943F File Offset: 0x0002763F
		public static string Table_HasColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_HasColumns");
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002944B File Offset: 0x0002764B
		public static string Table_HasColumns_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_HasColumns_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00029465 File Offset: 0x00027665
		public static string Table_HasColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_HasColumns_Example1");
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00029471 File Offset: 0x00027671
		public static string Table_HasColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_HasColumns_Example2");
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0002947D File Offset: 0x0002767D
		public static string Table_InsertRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_InsertRows");
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00029489 File Offset: 0x00027689
		public static string Table_InsertRows_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_InsertRows_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x000294A7 File Offset: 0x000276A7
		public static string Table_InsertRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_InsertRows_Example1");
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x000294B3 File Offset: 0x000276B3
		public static string Table_IsEmpty
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsEmpty");
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000294BF File Offset: 0x000276BF
		public static string Table_IsEmpty_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsEmpty_Description", new object[] { p0 });
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000294D5 File Offset: 0x000276D5
		public static string Table_IsEmpty_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsEmpty_Example1");
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000294E1 File Offset: 0x000276E1
		public static string Table_IsEmpty_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsEmpty_Example2");
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000294ED File Offset: 0x000276ED
		public static string Table_IsDistinct
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsDistinct");
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000294F9 File Offset: 0x000276F9
		public static string Table_IsDistinct_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsDistinct_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00029513 File Offset: 0x00027713
		public static string Table_IsDistinct_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsDistinct_Example1");
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0002951F File Offset: 0x0002771F
		public static string Table_IsDistinct_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_IsDistinct_Example2");
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0002952B File Offset: 0x0002772B
		public static string Table_FindText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FindText");
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00029537 File Offset: 0x00027737
		public static string Table_FindText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FindText_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00029551 File Offset: 0x00027751
		public static string Table_FindText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FindText_Example1");
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0002955D File Offset: 0x0002775D
		public static string Table_FillDown
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FillDown");
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00029569 File Offset: 0x00027769
		public static string Table_FillDown_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FillDown_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00029583 File Offset: 0x00027783
		public static string Table_FillDown_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FillDown_Example1");
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0002958F File Offset: 0x0002778F
		public static string Table_FillUp
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FillUp");
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002959B File Offset: 0x0002779B
		public static string Table_FillUp_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FillUp_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x000295B5 File Offset: 0x000277B5
		public static string Table_FillUp_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FillUp_Example1");
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x000295C1 File Offset: 0x000277C1
		public static string Table_FirstN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FirstN");
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000295CD File Offset: 0x000277CD
		public static string Table_FirstN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FirstN_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000295E7 File Offset: 0x000277E7
		public static string Table_FirstN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FirstN_Example1");
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000295F3 File Offset: 0x000277F3
		public static string Table_FirstN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FirstN_Example2");
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000295FF File Offset: 0x000277FF
		public static string Table_First
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_First");
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002960B File Offset: 0x0002780B
		public static string Table_First_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_First_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00029625 File Offset: 0x00027825
		public static string Table_First_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_First_Example1");
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00029631 File Offset: 0x00027831
		public static string Table_First_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_First_Example2");
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0002963D File Offset: 0x0002783D
		public static string Table_SplitAt
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SplitAt");
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00029649 File Offset: 0x00027849
		public static string Table_SplitAt_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SplitAt_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00029663 File Offset: 0x00027863
		public static string Table_SplitAt_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SplitAt_Example1");
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0002966F File Offset: 0x0002786F
		public static string Table_LastN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_LastN");
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002967B File Offset: 0x0002787B
		public static string Table_LastN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_LastN_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00029695 File Offset: 0x00027895
		public static string Table_LastN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_LastN_Example1");
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000296A1 File Offset: 0x000278A1
		public static string Table_LastN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_LastN_Example2");
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000296AD File Offset: 0x000278AD
		public static string Table_Last
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Last");
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000296B9 File Offset: 0x000278B9
		public static string Table_Last_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Last_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x000296D3 File Offset: 0x000278D3
		public static string Table_Last_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Last_Example1");
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x000296DF File Offset: 0x000278DF
		public static string Table_Last_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Last_Example2");
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x000296EB File Offset: 0x000278EB
		public static string Table_MaxN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MaxN");
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000296F7 File Offset: 0x000278F7
		public static string Table_MaxN_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MaxN_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00029715 File Offset: 0x00027915
		public static string Table_MaxN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MaxN_Example1");
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00029721 File Offset: 0x00027921
		public static string Table_MaxN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MaxN_Example2");
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0002972D File Offset: 0x0002792D
		public static string Table_Max
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Max");
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00029739 File Offset: 0x00027939
		public static string Table_Max_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Max_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00029757 File Offset: 0x00027957
		public static string Table_Max_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Max_Example1");
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00029763 File Offset: 0x00027963
		public static string Table_Max_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Max_Example2");
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0002976F File Offset: 0x0002796F
		public static string Table_MatchesAllRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAllRows");
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002977B File Offset: 0x0002797B
		public static string Table_MatchesAllRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAllRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00029795 File Offset: 0x00027995
		public static string Table_MatchesAllRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAllRows_Example1");
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000297A1 File Offset: 0x000279A1
		public static string Table_MatchesAllRows_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAllRows_Example2");
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x000297AD File Offset: 0x000279AD
		public static string Table_MatchesAnyRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAnyRows");
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000297B9 File Offset: 0x000279B9
		public static string Table_MatchesAnyRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAnyRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x000297D3 File Offset: 0x000279D3
		public static string Table_MatchesAnyRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAnyRows_Example1");
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x000297DF File Offset: 0x000279DF
		public static string Table_MatchesAnyRows_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MatchesAnyRows_Example2");
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x000297EB File Offset: 0x000279EB
		public static string Table_MinN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MinN");
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000297F7 File Offset: 0x000279F7
		public static string Table_MinN_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MinN_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00029815 File Offset: 0x00027A15
		public static string Table_MinN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MinN_Example1");
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00029821 File Offset: 0x00027A21
		public static string Table_MinN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_MinN_Example2");
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0002982D File Offset: 0x00027A2D
		public static string Table_Min
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Min");
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00029839 File Offset: 0x00027A39
		public static string Table_Min_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Min_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00029857 File Offset: 0x00027A57
		public static string Table_Min_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Min_Example1");
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00029863 File Offset: 0x00027A63
		public static string Table_Min_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Min_Example2");
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0002986F File Offset: 0x00027A6F
		public static string Table_Partition
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Partition");
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002987B File Offset: 0x00027A7B
		public static string Table_Partition_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Partition_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0002989D File Offset: 0x00027A9D
		public static string Table_Partition_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Partition_Example1");
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x000298A9 File Offset: 0x00027AA9
		public static string Table_PositionOf
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOf");
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000298B5 File Offset: 0x00027AB5
		public static string Table_PositionOf_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOf_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x000298D7 File Offset: 0x00027AD7
		public static string Table_PositionOf_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOf_Example1");
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x000298E3 File Offset: 0x00027AE3
		public static string Table_PositionOf_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOf_Example2");
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x000298EF File Offset: 0x00027AEF
		public static string Table_PositionOf_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOf_Example3");
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000298FB File Offset: 0x00027AFB
		public static string Table_PositionOfAny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOfAny");
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00029907 File Offset: 0x00027B07
		public static string Table_PositionOfAny_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOfAny_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00029929 File Offset: 0x00027B29
		public static string Table_PositionOfAny_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOfAny_Example1");
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00029935 File Offset: 0x00027B35
		public static string Table_PositionOfAny_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PositionOfAny_Example2");
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00029941 File Offset: 0x00027B41
		public static string Table_PrefixColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PrefixColumns");
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0002994D File Offset: 0x00027B4D
		public static string Table_PrefixColumns_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PrefixColumns_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00029967 File Offset: 0x00027B67
		public static string Table_PrefixColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PrefixColumns_Example1");
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00029973 File Offset: 0x00027B73
		public static string Table_Product
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Product");
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0002997F File Offset: 0x00027B7F
		public static string Table_Product_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Product_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00029999 File Offset: 0x00027B99
		public static string Table_Product_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Product_Example1");
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x000299A5 File Offset: 0x00027BA5
		public static string Table_Product_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Product_Example2");
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x000299B1 File Offset: 0x00027BB1
		public static string Table_PromoteHeaders
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PromoteHeaders");
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x000299BD File Offset: 0x00027BBD
		public static string Table_PromoteHeaders_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PromoteHeaders_Description");
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x000299C9 File Offset: 0x00027BC9
		public static string Table_PromoteHeaders_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PromoteHeaders_Example1");
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x000299D5 File Offset: 0x00027BD5
		public static string Table_PromoteHeaders_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PromoteHeaders_Example2");
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000299E1 File Offset: 0x00027BE1
		public static string Table_DemoteHeaders
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_DemoteHeaders");
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x000299ED File Offset: 0x00027BED
		public static string Table_DemoteHeaders_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_DemoteHeaders_Description");
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x000299F9 File Offset: 0x00027BF9
		public static string Table_DemoteHeaders_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_DemoteHeaders_Example1");
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00029A05 File Offset: 0x00027C05
		public static string Table_Range
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Range");
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00029A11 File Offset: 0x00027C11
		public static string Table_Range_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Range_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00029A2F File Offset: 0x00027C2F
		public static string Table_Range_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Range_Example1");
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00029A3B File Offset: 0x00027C3B
		public static string Table_Range_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Range_Example2");
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00029A47 File Offset: 0x00027C47
		public static string Table_RemoveColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveColumns");
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00029A53 File Offset: 0x00027C53
		public static string Table_RemoveColumns_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveColumns_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00029A71 File Offset: 0x00027C71
		public static string Table_RemoveColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveColumns_Example1");
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00029A7D File Offset: 0x00027C7D
		public static string Table_RemoveColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveColumns_Example2");
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00029A89 File Offset: 0x00027C89
		public static string Table_RemoveRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRows");
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00029A95 File Offset: 0x00027C95
		public static string Table_RemoveRows_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRows_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00029AB3 File Offset: 0x00027CB3
		public static string Table_RemoveRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRows_Example1");
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00029ABF File Offset: 0x00027CBF
		public static string Table_RemoveRows_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRows_Example2");
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00029ACB File Offset: 0x00027CCB
		public static string Table_RemoveRows_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRows_Example3");
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00029AD7 File Offset: 0x00027CD7
		public static string Table_RemoveMatchingRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveMatchingRows");
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00029AE3 File Offset: 0x00027CE3
		public static string Table_RemoveMatchingRows_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveMatchingRows_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00029B01 File Offset: 0x00027D01
		public static string Table_RemoveMatchingRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveMatchingRows_Example1");
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00029B0D File Offset: 0x00027D0D
		public static string Table_TransformColumnNames
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnNames");
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00029B19 File Offset: 0x00027D19
		public static string Table_TransformColumnNames_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnNames_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00029B33 File Offset: 0x00027D33
		public static string Table_TransformColumnNames_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnNames_Example1");
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00029B3F File Offset: 0x00027D3F
		public static string Table_TransformColumnNames_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnNames_Example2");
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00029B4B File Offset: 0x00027D4B
		public static string Table_RenameColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RenameColumns");
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00029B57 File Offset: 0x00027D57
		public static string Table_RenameColumns_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RenameColumns_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00029B75 File Offset: 0x00027D75
		public static string Table_RenameColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RenameColumns_Example1");
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00029B81 File Offset: 0x00027D81
		public static string Table_RenameColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RenameColumns_Example2");
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00029B8D File Offset: 0x00027D8D
		public static string Table_RenameColumns_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RenameColumns_Example3");
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00029B99 File Offset: 0x00027D99
		public static string Table_ReorderColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReorderColumns");
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00029BA5 File Offset: 0x00027DA5
		public static string Table_ReorderColumns_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReorderColumns_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00029BC3 File Offset: 0x00027DC3
		public static string Table_ReorderColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReorderColumns_Example1");
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00029BCF File Offset: 0x00027DCF
		public static string Table_ReorderColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReorderColumns_Example2");
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00029BDB File Offset: 0x00027DDB
		public static string Table_Repeat
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Repeat");
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00029BE7 File Offset: 0x00027DE7
		public static string Table_Repeat_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Repeat_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00029C01 File Offset: 0x00027E01
		public static string Table_Repeat_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Repeat_Example1");
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00029C0D File Offset: 0x00027E0D
		public static string Table_ReplaceRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceRows");
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00029C19 File Offset: 0x00027E19
		public static string Table_ReplaceRows_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceRows_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00029C3B File Offset: 0x00027E3B
		public static string Table_ReplaceRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceRows_Example1");
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00029C47 File Offset: 0x00027E47
		public static string Table_ReplaceMatchingRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceMatchingRows");
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00029C53 File Offset: 0x00027E53
		public static string Table_ReplaceMatchingRows_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceMatchingRows_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00029C71 File Offset: 0x00027E71
		public static string Table_ReplaceMatchingRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceMatchingRows_Example1");
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00029C7D File Offset: 0x00027E7D
		public static string Table_ReplaceErrorValues
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceErrorValues");
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00029C89 File Offset: 0x00027E89
		public static string Table_ReplaceErrorValues_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceErrorValues_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00029CA3 File Offset: 0x00027EA3
		public static string Table_ReplaceErrorValues_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceErrorValues_Example1");
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00029CAF File Offset: 0x00027EAF
		public static string Table_ReplaceErrorValues_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceErrorValues_Example2");
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00029CBB File Offset: 0x00027EBB
		public static string Table_ReplaceValue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceValue");
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00029CC7 File Offset: 0x00027EC7
		public static string Table_ReplaceValue_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceValue_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00029CE5 File Offset: 0x00027EE5
		public static string Table_ReplaceValue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceValue_Example1");
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00029CF1 File Offset: 0x00027EF1
		public static string Table_ReplaceValue_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceValue_Example2");
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00029CFD File Offset: 0x00027EFD
		public static string Table_ReplaceValue_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceValue_Example3");
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00029D09 File Offset: 0x00027F09
		public static string Table_ReplaceValue_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceValue_Example4");
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00029D15 File Offset: 0x00027F15
		public static string Table_ReverseRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReverseRows");
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00029D21 File Offset: 0x00027F21
		public static string Table_ReverseRows_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReverseRows_Description", new object[] { p0 });
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00029D37 File Offset: 0x00027F37
		public static string Table_ReverseRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReverseRows_Example1");
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00029D43 File Offset: 0x00027F43
		public static string Table_RowCount
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RowCount");
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00029D4F File Offset: 0x00027F4F
		public static string Table_RowCount_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RowCount_Description", new object[] { p0 });
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00029D65 File Offset: 0x00027F65
		public static string Table_RowCount_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RowCount_Example1");
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00029D71 File Offset: 0x00027F71
		public static string Table_ApproximateRowCount
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ApproximateRowCount");
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00029D7D File Offset: 0x00027F7D
		public static string Table_ApproximateRowCount_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ApproximateRowCount_Description", new object[] { p0 });
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00029D93 File Offset: 0x00027F93
		public static string Table_ApproximateRowCount_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ApproximateRowCount_Example1");
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00029D9F File Offset: 0x00027F9F
		public static string Table_SelectColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectColumns");
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00029DAB File Offset: 0x00027FAB
		public static string Table_SelectColumns_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectColumns_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00029DC9 File Offset: 0x00027FC9
		public static string Table_SelectColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectColumns_Example1");
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00029DD5 File Offset: 0x00027FD5
		public static string Table_SelectColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectColumns_Example2");
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00029DE1 File Offset: 0x00027FE1
		public static string Table_SelectColumns_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectColumns_Example3");
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00029DED File Offset: 0x00027FED
		public static string Table_SelectColumns_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectColumns_Example4");
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00029DF9 File Offset: 0x00027FF9
		public static string Table_SelectRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectRows");
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00029E05 File Offset: 0x00028005
		public static string Table_SelectRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00029E1F File Offset: 0x0002801F
		public static string Table_SelectRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectRows_Example1");
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00029E2B File Offset: 0x0002802B
		public static string Table_SelectRows_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectRows_Example2");
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x00029E37 File Offset: 0x00028037
		public static string Table_SingleRow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SingleRow");
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00029E43 File Offset: 0x00028043
		public static string Table_SingleRow_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SingleRow_Description", new object[] { p0 });
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00029E59 File Offset: 0x00028059
		public static string Table_SingleRow_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SingleRow_Example1");
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00029E65 File Offset: 0x00028065
		public static string Table_Skip
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Skip");
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00029E71 File Offset: 0x00028071
		public static string Table_Skip_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Skip_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00029E8B File Offset: 0x0002808B
		public static string Table_Skip_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Skip_Example1");
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00029E97 File Offset: 0x00028097
		public static string Table_Skip_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Skip_Example2");
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00029EA3 File Offset: 0x000280A3
		public static string Table_Skip_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Skip_Example3");
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00029EAF File Offset: 0x000280AF
		public static string Table_RemoveFirstN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveFirstN");
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00029EBB File Offset: 0x000280BB
		public static string Table_RemoveFirstN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveFirstN_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00029ED5 File Offset: 0x000280D5
		public static string Table_RemoveFirstN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveFirstN_Example1");
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00029EE1 File Offset: 0x000280E1
		public static string Table_RemoveFirstN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveFirstN_Example2");
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00029EED File Offset: 0x000280ED
		public static string Table_RemoveFirstN_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveFirstN_Example3");
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00029EF9 File Offset: 0x000280F9
		public static string Table_RemoveLastN
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveLastN");
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00029F05 File Offset: 0x00028105
		public static string Table_RemoveLastN_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveLastN_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00029F1F File Offset: 0x0002811F
		public static string Table_RemoveLastN_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveLastN_Example1");
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00029F2B File Offset: 0x0002812B
		public static string Table_RemoveLastN_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveLastN_Example2");
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00029F37 File Offset: 0x00028137
		public static string Table_Sort
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Sort");
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00029F43 File Offset: 0x00028143
		public static string Table_Sort_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Sort_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00029F5D File Offset: 0x0002815D
		public static string Table_Sort_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Sort_Example1");
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00029F69 File Offset: 0x00028169
		public static string Table_Sort_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Sort_Example2");
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00029F75 File Offset: 0x00028175
		public static string Table_Sort_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Sort_Example3");
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00029F81 File Offset: 0x00028181
		public static string Table_ExpandRecordColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandRecordColumn");
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00029F8D File Offset: 0x0002818D
		public static string Table_ExpandRecordColumn_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandRecordColumn_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00029FAF File Offset: 0x000281AF
		public static string Table_ExpandRecordColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandRecordColumn_Example1");
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00029FBB File Offset: 0x000281BB
		public static string Table_ExpandListColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandListColumn");
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00029FC7 File Offset: 0x000281C7
		public static string Table_ExpandListColumn_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandListColumn_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00029FE1 File Offset: 0x000281E1
		public static string Table_ExpandListColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandListColumn_Example1");
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00029FED File Offset: 0x000281ED
		public static string Table_ExpandListColumn_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandListColumn_Example2");
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00029FF9 File Offset: 0x000281F9
		public static string Table_TransformColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumns");
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0002A005 File Offset: 0x00028205
		public static string Table_TransformColumns_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumns_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0002A027 File Offset: 0x00028227
		public static string Table_TransformColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumns_Example1");
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0002A033 File Offset: 0x00028233
		public static string Table_TransformColumns_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumns_Example2");
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0002A03F File Offset: 0x0002823F
		public static string Table_TransformColumns_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumns_Example3");
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0002A04B File Offset: 0x0002824B
		public static string Table_TransformColumns_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumns_Example4");
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0002A057 File Offset: 0x00028257
		public static string Table_TransformColumnTypes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnTypes");
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0002A063 File Offset: 0x00028263
		public static string Table_TransformColumnTypes_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnTypes_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0002A081 File Offset: 0x00028281
		public static string Table_TransformColumnTypes_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformColumnTypes_Example1");
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0002A08D File Offset: 0x0002828D
		public static string Table_TransformRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformRows");
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0002A099 File Offset: 0x00028299
		public static string Table_TransformRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0002A0B3 File Offset: 0x000282B3
		public static string Table_TransformRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformRows_Example1");
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0002A0BF File Offset: 0x000282BF
		public static string Table_TransformRows_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TransformRows_Example2");
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0002A0CB File Offset: 0x000282CB
		public static string Table_Transpose
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Transpose");
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0002A0D7 File Offset: 0x000282D7
		public static string Table_Transpose_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Transpose_Description");
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0002A0E3 File Offset: 0x000282E3
		public static string Table_Transpose_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Transpose_Example1");
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0002A0EF File Offset: 0x000282EF
		public static string Table_TypeFromColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TypeFromColumns");
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0002A0FB File Offset: 0x000282FB
		public static string Table_TypeFromColumns_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TypeFromColumns_Description", new object[] { p0 });
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0002A111 File Offset: 0x00028311
		public static string Table_TypeFromColumns_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TypeFromColumns_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0002A12B File Offset: 0x0002832B
		public static string Table_TypeFromList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TypeFromList");
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0002A137 File Offset: 0x00028337
		public static string Table_TypeFromList_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TypeFromList_Description", new object[] { p0, p1 });
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0002A151 File Offset: 0x00028351
		public static string Table_TypeFromList_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_TypeFromList_Example1");
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0002A15D File Offset: 0x0002835D
		public static string Date_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToText");
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0002A169 File Offset: 0x00028369
		public static string Date_ToText_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToText_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0002A187 File Offset: 0x00028387
		public static string Date_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToText_Example1");
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0002A193 File Offset: 0x00028393
		public static string Date_ToText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToText_Example2");
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0002A19F File Offset: 0x0002839F
		public static string Date_ToText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_ToText_Example3");
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0002A1AB File Offset: 0x000283AB
		public static string Language_Date_Time_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Date_Time_Combine");
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0002A1B7 File Offset: 0x000283B7
		public static string Language_List_OptionalLookup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_List_OptionalLookup");
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0002A1C3 File Offset: 0x000283C3
		public static string Language_Not
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Not");
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0002A1CF File Offset: 0x000283CF
		public static string Language_Record_MultiLookup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Record_MultiLookup");
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0002A1DB File Offset: 0x000283DB
		public static string Language_Record_OptionalLookup
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Record_OptionalLookup");
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0002A1E7 File Offset: 0x000283E7
		public static string Language_Table_MultiProject
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Language_Table_MultiProject");
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0002A1F3 File Offset: 0x000283F3
		public static string DateTime_AddZone
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_AddZone");
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0002A1FF File Offset: 0x000283FF
		public static string DateTime_AddZone_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_AddZone_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0002A21D File Offset: 0x0002841D
		public static string DateTime_AddZone_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_AddZone_Example1");
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0002A229 File Offset: 0x00028429
		public static string DateTime_Date
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Date");
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0002A235 File Offset: 0x00028435
		public static string DateTime_Date_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Date_Description", new object[] { p0 });
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0002A24B File Offset: 0x0002844B
		public static string DateTime_Date_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Date_Example1");
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0002A257 File Offset: 0x00028457
		public static string DateTime_Time
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Time");
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0002A263 File Offset: 0x00028463
		public static string DateTime_Time_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Time_Description", new object[] { p0 });
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0002A279 File Offset: 0x00028479
		public static string DateTime_Time_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Time_Example1");
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0002A285 File Offset: 0x00028485
		public static string Uri_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Combine");
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0002A291 File Offset: 0x00028491
		public static string Uri_Combine_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Combine_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0002A2AB File Offset: 0x000284AB
		public static string Uri_Parts
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Parts");
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0002A2B7 File Offset: 0x000284B7
		public static string Uri_Parts_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Parts_Description", new object[] { p0 });
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0002A2CD File Offset: 0x000284CD
		public static string Uri_Parts_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Parts_Example1");
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0002A2D9 File Offset: 0x000284D9
		public static string Uri_Parts_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Parts_Example2");
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0002A2E5 File Offset: 0x000284E5
		public static string Uri_BuildQueryString
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_BuildQueryString");
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0002A2F1 File Offset: 0x000284F1
		public static string Uri_BuildQueryString_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_BuildQueryString_Description", new object[] { p0 });
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0002A307 File Offset: 0x00028507
		public static string Uri_BuildQueryString_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_BuildQueryString_Example1");
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0002A313 File Offset: 0x00028513
		public static string Uri_EscapeDataString
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_EscapeDataString");
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0002A31F File Offset: 0x0002851F
		public static string Uri_EscapeDataString_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_EscapeDataString_Description", new object[] { p0 });
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0002A335 File Offset: 0x00028535
		public static string Uri_EscapeDataString_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_EscapeDataString_Example1");
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0002A341 File Offset: 0x00028541
		public static string Text_ToList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ToList");
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0002A34D File Offset: 0x0002854D
		public static string Text_ToList_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ToList_Description", new object[] { p0 });
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0002A363 File Offset: 0x00028563
		public static string Text_ToList_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_ToList_Example1");
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0002A36F File Offset: 0x0002856F
		public static string DateTime_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_Type");
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0002A37B File Offset: 0x0002857B
		public static string DateTimeZone_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_Type");
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0002A387 File Offset: 0x00028587
		public static string Date_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_Type");
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0002A393 File Offset: 0x00028593
		public static string Time_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_Type");
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0002A39F File Offset: 0x0002859F
		public static string Table_ExpandTableColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandTableColumn");
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0002A3AB File Offset: 0x000285AB
		public static string Table_ExpandTableColumn_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandTableColumn_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0002A3CD File Offset: 0x000285CD
		public static string Table_ExpandTableColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ExpandTableColumn_Example1");
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0002A3D9 File Offset: 0x000285D9
		public static string Function_Invoke
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_Invoke");
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0002A3E5 File Offset: 0x000285E5
		public static string Function_Invoke_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_Invoke_Description");
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0002A3F1 File Offset: 0x000285F1
		public static string Function_Invoke_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_Invoke_Example1");
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0002A3FD File Offset: 0x000285FD
		public static string Function_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_From");
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0002A409 File Offset: 0x00028609
		public static string Function_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Function_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0002A423 File Offset: 0x00028623
		public static string Function_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_From_Example1");
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0002A42F File Offset: 0x0002862F
		public static string Function_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_From_Example2");
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0002A43B File Offset: 0x0002863B
		public static string Table_FromRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRows");
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0002A447 File Offset: 0x00028647
		public static string Table_FromRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0002A461 File Offset: 0x00028661
		public static string Table_FromRows_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRows_Example1", new object[] { p0, p1 });
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0002A47B File Offset: 0x0002867B
		public static string Table_FromRows_Example2(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRows_Example2", new object[] { p0, p1 });
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0002A495 File Offset: 0x00028695
		public static string Table_ToRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToRows");
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0002A4A1 File Offset: 0x000286A1
		public static string Table_ToRows_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToRows_Description", new object[] { p0 });
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0002A4B7 File Offset: 0x000286B7
		public static string Table_ToRows_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToRows_Example1");
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0002A4C3 File Offset: 0x000286C3
		public static string Table_AddKey
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddKey");
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0002A4CF File Offset: 0x000286CF
		public static string Table_AddKey_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddKey_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0002A4ED File Offset: 0x000286ED
		public static string Table_AddKey_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AddKey_Example1");
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0002A4F9 File Offset: 0x000286F9
		public static string Web_Page
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_Page");
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0002A505 File Offset: 0x00028705
		public static string Lines_ToBinary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Lines_ToBinary");
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0002A511 File Offset: 0x00028711
		public static string Lines_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Lines_FromText");
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0002A51D File Offset: 0x0002871D
		public static string Lines_FromText_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Lines_FromText_Description");
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0002A529 File Offset: 0x00028729
		public static string Table_ToList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToList");
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0002A535 File Offset: 0x00028735
		public static string Table_ToList_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToList_Example1");
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0002A541 File Offset: 0x00028741
		public static string Table_FromList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromList");
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0002A54D File Offset: 0x0002874D
		public static string Table_FromList_Description(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromList_Description", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0002A574 File Offset: 0x00028774
		public static string Table_FromList_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromList_Example1");
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0002A580 File Offset: 0x00028780
		public static string Table_FromList_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromList_Example2");
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0002A58C File Offset: 0x0002878C
		public static string Table_FromList_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromList_Example3");
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0002A598 File Offset: 0x00028798
		public static string Table_FromValue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromValue");
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0002A5A4 File Offset: 0x000287A4
		public static string Table_FromValue_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromValue_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0002A5BE File Offset: 0x000287BE
		public static string Table_FromValue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromValue_Example1");
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0002A5CA File Offset: 0x000287CA
		public static string Table_FromValue_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromValue_Example2");
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0002A5D6 File Offset: 0x000287D6
		public static string Table_FromValue_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromValue_Example3");
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0002A5E2 File Offset: 0x000287E2
		public static string Table_FirstValue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FirstValue");
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0002A5EE File Offset: 0x000287EE
		public static string Table_FirstValue_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FirstValue_Description", new object[] { p0 });
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0002A604 File Offset: 0x00028804
		public static string Lines_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Lines_ToText");
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0002A610 File Offset: 0x00028810
		public static string Combiner_CombineTextByDelimiter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByDelimiter");
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0002A61C File Offset: 0x0002881C
		public static string Combiner_CombineTextByDelimiter_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByDelimiter_Description");
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0002A628 File Offset: 0x00028828
		public static string Combiner_CombineTextByDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByDelimiter_Example1");
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0002A634 File Offset: 0x00028834
		public static string Combiner_CombineTextByDelimiter_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByDelimiter_Example2");
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0002A640 File Offset: 0x00028840
		public static string Combiner_CombineTextByRanges
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByRanges");
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0002A64C File Offset: 0x0002884C
		public static string Combiner_CombineTextByRanges_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByRanges_Description");
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0002A658 File Offset: 0x00028858
		public static string Combiner_CombineTextByRanges_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByRanges_Example1");
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0002A664 File Offset: 0x00028864
		public static string Splitter_SplitTextByCharacterTransition(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByCharacterTransition", new object[] { p0, p1 });
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0002A67E File Offset: 0x0002887E
		public static string Splitter_SplitTextByCharacterTransition_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByCharacterTransition_Example1");
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0002A68A File Offset: 0x0002888A
		public static string Splitter_SplitTextByDelimiter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByDelimiter");
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0002A696 File Offset: 0x00028896
		public static string Splitter_SplitTextByDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByDelimiter_Example1");
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0002A6A2 File Offset: 0x000288A2
		public static string Splitter_SplitTextByRanges
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRanges");
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0002A6AE File Offset: 0x000288AE
		public static string Splitter_SplitTextByRanges_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRanges_Description");
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0002A6BA File Offset: 0x000288BA
		public static string Splitter_SplitTextByRanges_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRanges_Example1");
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0002A6C6 File Offset: 0x000288C6
		public static string Splitter_SplitTextByRanges_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRanges_Example2");
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0002A6D2 File Offset: 0x000288D2
		public static string Splitter_SplitTextByRanges_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRanges_Example3");
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0002A6DE File Offset: 0x000288DE
		public static string Lines_FromBinary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Lines_FromBinary");
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0002A6EA File Offset: 0x000288EA
		public static string ExtraValues_Error
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ExtraValues_Error");
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0002A6F6 File Offset: 0x000288F6
		public static string ExtraValues_Ignore
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ExtraValues_Ignore");
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0002A702 File Offset: 0x00028902
		public static string ExtraValues_List
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ExtraValues_List");
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0002A70E File Offset: 0x0002890E
		public static string QuoteStyle_Csv
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("QuoteStyle_Csv");
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0002A71A File Offset: 0x0002891A
		public static string QuoteStyle_None
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("QuoteStyle_None");
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0002A726 File Offset: 0x00028926
		public static string Table_FromRecords
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRecords");
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0002A732 File Offset: 0x00028932
		public static string Table_FromRecords_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRecords_Description", new object[] { p0 });
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0002A748 File Offset: 0x00028948
		public static string Table_FromRecords_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRecords_Example1");
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0002A754 File Offset: 0x00028954
		public static string Table_FromRecords_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromRecords_Example2");
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0002A760 File Offset: 0x00028960
		public static string Table_ToRecords
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToRecords");
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0002A76C File Offset: 0x0002896C
		public static string Table_ToRecords_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToRecords_Description", new object[] { p0 });
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0002A782 File Offset: 0x00028982
		public static string Table_ToRecords_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ToRecords_Example1");
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0002A78E File Offset: 0x0002898E
		public static string Combiner_CombineTextByEachDelimiter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByEachDelimiter");
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0002A79A File Offset: 0x0002899A
		public static string Combiner_CombineTextByEachDelimiter_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByEachDelimiter_Description");
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0002A7A6 File Offset: 0x000289A6
		public static string Combiner_CombineTextByEachDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByEachDelimiter_Example1");
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0002A7B2 File Offset: 0x000289B2
		public static string Combiner_CombineTextByLengths
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByLengths");
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0002A7BE File Offset: 0x000289BE
		public static string Combiner_CombineTextByLengths_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByLengths_Description");
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0002A7CA File Offset: 0x000289CA
		public static string Combiner_CombineTextByLengths_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByLengths_Example1");
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0002A7D6 File Offset: 0x000289D6
		public static string Combiner_CombineTextByLengths_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByLengths_Example2");
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0002A7E2 File Offset: 0x000289E2
		public static string Combiner_CombineTextByPositions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByPositions");
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0002A7EE File Offset: 0x000289EE
		public static string Combiner_CombineTextByPositions_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByPositions_Description");
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0002A7FA File Offset: 0x000289FA
		public static string Combiner_CombineTextByPositions_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Combiner_CombineTextByPositions_Example1");
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0002A806 File Offset: 0x00028A06
		public static string List_Normalize
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Normalize");
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0002A812 File Offset: 0x00028A12
		public static string Splitter_SplitTextByAnyDelimiter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByAnyDelimiter");
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0002A81E File Offset: 0x00028A1E
		public static string Splitter_SplitTextByAnyDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByAnyDelimiter_Example1");
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0002A82A File Offset: 0x00028A2A
		public static string Splitter_SplitTextByAnyDelimiter_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByAnyDelimiter_Example2");
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0002A836 File Offset: 0x00028A36
		public static string Splitter_SplitTextByEachDelimiter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByEachDelimiter");
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0002A842 File Offset: 0x00028A42
		public static string Splitter_SplitTextByEachDelimiter_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByEachDelimiter_Example1");
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0002A84E File Offset: 0x00028A4E
		public static string Splitter_SplitTextByEachDelimiter_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByEachDelimiter_Example2");
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0002A85A File Offset: 0x00028A5A
		public static string Splitter_SplitTextByLengths
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByLengths");
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0002A866 File Offset: 0x00028A66
		public static string Splitter_SplitTextByLengths_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByLengths_Example1");
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0002A872 File Offset: 0x00028A72
		public static string Splitter_SplitTextByLengths_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByLengths_Example2");
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0002A87E File Offset: 0x00028A7E
		public static string Splitter_SplitTextByRepeatedLengths
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRepeatedLengths");
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0002A88A File Offset: 0x00028A8A
		public static string Splitter_SplitTextByRepeatedLengths_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRepeatedLengths_Example1");
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0002A896 File Offset: 0x00028A96
		public static string Splitter_SplitTextByRepeatedLengths_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByRepeatedLengths_Example2");
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0002A8A2 File Offset: 0x00028AA2
		public static string Splitter_SplitTextByPositions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByPositions");
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0002A8AE File Offset: 0x00028AAE
		public static string Splitter_SplitTextByPositions_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByPositions_Example1");
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0002A8BA File Offset: 0x00028ABA
		public static string Splitter_SplitTextByPositions_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByPositions_Example2");
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0002A8C6 File Offset: 0x00028AC6
		public static string Splitter_SplitTextByWhitespace
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByWhitespace");
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0002A8D2 File Offset: 0x00028AD2
		public static string Splitter_SplitTextByWhitespace_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitTextByWhitespace_Example1");
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0002A8DE File Offset: 0x00028ADE
		public static string Table_CombineColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_CombineColumns");
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0002A8EA File Offset: 0x00028AEA
		public static string Table_CombineColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_CombineColumns_Example1");
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0002A8F6 File Offset: 0x00028AF6
		public static string Table_CombineColumnsToRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_CombineColumnsToRecord");
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0002A902 File Offset: 0x00028B02
		public static string Table_CombineColumnsToRecord_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_CombineColumnsToRecord_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0002A924 File Offset: 0x00028B24
		public static string Table_Split
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Split");
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0002A930 File Offset: 0x00028B30
		public static string Table_Split_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Split_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0002A94A File Offset: 0x00028B4A
		public static string Table_Split_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Split_Example1");
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0002A956 File Offset: 0x00028B56
		public static string Table_SplitColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SplitColumn");
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0002A962 File Offset: 0x00028B62
		public static string Table_SplitColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SplitColumn_Example1");
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0002A96E File Offset: 0x00028B6E
		public static string Csv_Document
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Csv_Document");
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0002A97A File Offset: 0x00028B7A
		public static string Csv_Document_Description(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Csv_Document_Description", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0002A9A1 File Offset: 0x00028BA1
		public static string Csv_Document_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Csv_Document_Example1");
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0002A9AD File Offset: 0x00028BAD
		public static string Csv_Document_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Csv_Document_Example2");
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0002A9B9 File Offset: 0x00028BB9
		public static string Splitter_SplitByNothing
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Splitter_SplitByNothing");
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0002A9C5 File Offset: 0x00028BC5
		public static string Embedded_Value
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Embedded_Value");
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0002A9D1 File Offset: 0x00028BD1
		public static string Value_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_FromText");
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0002A9DD File Offset: 0x00028BDD
		public static string Value_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0002A9F7 File Offset: 0x00028BF7
		public static string Binary_FromList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_FromList");
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0002AA03 File Offset: 0x00028C03
		public static string Binary_ToList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ToList");
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0002AA0F File Offset: 0x00028C0F
		public static string Folder_Files
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Folder_Files");
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0002AA1B File Offset: 0x00028C1B
		public static string Folder_Files_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Folder_Files_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0002AA35 File Offset: 0x00028C35
		public static string Hdfs_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Hdfs_Contents");
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0002AA41 File Offset: 0x00028C41
		public static string Hdfs_Contents_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Hdfs_Contents_Description", new object[] { p0 });
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0002AA57 File Offset: 0x00028C57
		public static string Hdfs_Files
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Hdfs_Files");
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0002AA63 File Offset: 0x00028C63
		public static string Hdfs_Files_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Hdfs_Files_Description", new object[] { p0 });
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0002AA79 File Offset: 0x00028C79
		public static string HdInsight_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("HdInsight_Contents");
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0002AA85 File Offset: 0x00028C85
		public static string HdInsight_Contents_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("HdInsight_Contents_Description", new object[] { p0 });
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0002AA9B File Offset: 0x00028C9B
		public static string HdInsight_Containers
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("HdInsight_Containers");
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0002AAA7 File Offset: 0x00028CA7
		public static string HdInsight_Containers_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("HdInsight_Containers_Description", new object[] { p0 });
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0002AABD File Offset: 0x00028CBD
		public static string AzureStorage_Blobs
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_Blobs");
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0002AAC9 File Offset: 0x00028CC9
		public static string AzureStorage_Blobs_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_Blobs_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0002AAE3 File Offset: 0x00028CE3
		public static string AzureStorage_BlobContents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_BlobContents");
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0002AAEF File Offset: 0x00028CEF
		public static string AzureStorage_BlobContents_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_BlobContents_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x0002AB09 File Offset: 0x00028D09
		public static string AzureStorage_DataLake
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_DataLake");
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0002AB15 File Offset: 0x00028D15
		public static string AzureStorage_DataLake_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_DataLake_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0002AB2F File Offset: 0x00028D2F
		public static string AzureStorage_DataLakeContents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_DataLakeContents");
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0002AB3B File Offset: 0x00028D3B
		public static string AzureStorage_DataLakeContents_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_DataLakeContents_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0002AB55 File Offset: 0x00028D55
		public static string HdInsight_Files
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("HdInsight_Files");
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0002AB61 File Offset: 0x00028D61
		public static string HdInsight_Files_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("HdInsight_Files_Description", new object[] { p0 });
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0002AB77 File Offset: 0x00028D77
		public static string AzureStorage_Tables
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_Tables");
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0002AB83 File Offset: 0x00028D83
		public static string AzureStorage_Tables_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AzureStorage_Tables_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0002AB9D File Offset: 0x00028D9D
		public static string SharePoint_Files
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SharePoint_Files");
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0002ABA9 File Offset: 0x00028DA9
		public static string SharePoint_Files_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("SharePoint_Files_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0002ABC3 File Offset: 0x00028DC3
		public static string ActiveDirectory_Domains
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ActiveDirectory_Domains");
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0002ABCF File Offset: 0x00028DCF
		public static string Exchange_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Exchange_Contents");
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0002ABDB File Offset: 0x00028DDB
		public static string Exchange_Contents_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Exchange_Contents_Description", new object[] { p0 });
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0002ABF1 File Offset: 0x00028DF1
		public static string Binary_Combine
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Combine");
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0002ABFD File Offset: 0x00028DFD
		public static string List_NonNullCount
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_NonNullCount");
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0002AC09 File Offset: 0x00028E09
		public static string List_NonNullCount_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_NonNullCount_Description", new object[] { p0 });
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0002AC1F File Offset: 0x00028E1F
		public static string Table_AggregateTableColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AggregateTableColumn");
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0002AC2B File Offset: 0x00028E2B
		public static string Table_AggregateTableColumn_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AggregateTableColumn_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0002AC49 File Offset: 0x00028E49
		public static string Table_AggregateTableColumn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_AggregateTableColumn_Example1");
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0002AC55 File Offset: 0x00028E55
		public static string Binary_Length
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Length");
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0002AC61 File Offset: 0x00028E61
		public static string Culture_Current
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Culture_Current");
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0002AC6D File Offset: 0x00028E6D
		public static string Oracle_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Oracle_Database");
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0002AC79 File Offset: 0x00028E79
		public static string Oracle_Database_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Oracle_Database_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0002AC93 File Offset: 0x00028E93
		public static string Excel_CurrentWorkbook
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_CurrentWorkbook");
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0002AC9F File Offset: 0x00028E9F
		public static string Excel_CurrentWorkbook_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_CurrentWorkbook_Description");
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0002ACAB File Offset: 0x00028EAB
		public static string DataSource_TestConnection
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DataSource_TestConnection");
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0002ACB7 File Offset: 0x00028EB7
		public static string DataSource_TestConnection_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DataSource_TestConnection_Description", new object[] { p0 });
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0002ACCD File Offset: 0x00028ECD
		public static string DateTimeZone_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromText");
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0002ACD9 File Offset: 0x00028ED9
		public static string DateTimeZone_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0002ACF3 File Offset: 0x00028EF3
		public static string DateTimeZone_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromText_Example1");
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0002ACFF File Offset: 0x00028EFF
		public static string DateTimeZone_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromText_Example2");
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0002AD0B File Offset: 0x00028F0B
		public static string DateTimeZone_FromText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromText_Example3");
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0002AD17 File Offset: 0x00028F17
		public static string DateTimeZone_ToRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToRecord");
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0002AD23 File Offset: 0x00028F23
		public static string DateTimeZone_ToRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToRecord_Description", new object[] { p0 });
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0002AD39 File Offset: 0x00028F39
		public static string DateTimeZone_ToRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToRecord_Example1");
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0002AD45 File Offset: 0x00028F45
		public static string DateTimeZone_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToText");
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0002AD51 File Offset: 0x00028F51
		public static string DateTimeZone_ToText_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToText_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0002AD6F File Offset: 0x00028F6F
		public static string DateTimeZone_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToText_Example1");
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0002AD7B File Offset: 0x00028F7B
		public static string DateTimeZone_ToText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToText_Example2");
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0002AD87 File Offset: 0x00028F87
		public static string DateTimeZone_ToText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_ToText_Example3");
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0002AD93 File Offset: 0x00028F93
		public static string DateTime_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromText");
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0002AD9F File Offset: 0x00028F9F
		public static string DateTime_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0002ADB9 File Offset: 0x00028FB9
		public static string DateTime_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromText_Example1");
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0002ADC5 File Offset: 0x00028FC5
		public static string DateTime_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromText_Example2");
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0002ADD1 File Offset: 0x00028FD1
		public static string DateTime_FromText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromText_Example3");
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0002ADDD File Offset: 0x00028FDD
		public static string DateTime_FromText_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromText_Example4");
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0002ADE9 File Offset: 0x00028FE9
		public static string DateTime_FromFileTime
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromFileTime");
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0002ADF5 File Offset: 0x00028FF5
		public static string DateTime_FromFileTime_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromFileTime_Description", new object[] { p0 });
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0002AE0B File Offset: 0x0002900B
		public static string DateTime_FromFileTime_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FromFileTime_Example1");
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0002AE17 File Offset: 0x00029017
		public static string DateTimeZone_FromFileTime
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromFileTime");
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0002AE23 File Offset: 0x00029023
		public static string DateTimeZone_FromFileTime_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromFileTime_Description", new object[] { p0 });
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0002AE39 File Offset: 0x00029039
		public static string DateTimeZone_FromFileTime_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_FromFileTime_Example1");
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0002AE45 File Offset: 0x00029045
		public static string DateTime_ToRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToRecord");
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0002AE51 File Offset: 0x00029051
		public static string DateTime_ToRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToRecord_Description", new object[] { p0 });
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0002AE67 File Offset: 0x00029067
		public static string DateTime_ToRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToRecord_Example1");
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0002AE73 File Offset: 0x00029073
		public static string DateTime_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToText");
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0002AE7F File Offset: 0x0002907F
		public static string DateTime_ToText_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToText_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0002AE9D File Offset: 0x0002909D
		public static string DateTime_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToText_Example1");
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0002AEA9 File Offset: 0x000290A9
		public static string DateTime_ToText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToText_Example2");
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0002AEB5 File Offset: 0x000290B5
		public static string DateTime_ToText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_ToText_Example3");
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0002AEC1 File Offset: 0x000290C1
		public static string Time_FromText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_FromText");
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0002AECD File Offset: 0x000290CD
		public static string Time_FromText_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_FromText_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0002AEE7 File Offset: 0x000290E7
		public static string Time_FromText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_FromText_Example1");
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0002AEF3 File Offset: 0x000290F3
		public static string Time_FromText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_FromText_Example2");
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0002AEFF File Offset: 0x000290FF
		public static string Time_FromText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_FromText_Example3");
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0002AF0B File Offset: 0x0002910B
		public static string Time_ToRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToRecord");
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0002AF17 File Offset: 0x00029117
		public static string Time_ToRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToRecord_Description", new object[] { p0 });
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0002AF2D File Offset: 0x0002912D
		public static string Time_ToRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToRecord_Example1");
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0002AF39 File Offset: 0x00029139
		public static string Time_ToText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToText");
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0002AF45 File Offset: 0x00029145
		public static string Time_ToText_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToText_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0002AF63 File Offset: 0x00029163
		public static string Time_ToText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToText_Example1");
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0002AF6F File Offset: 0x0002916F
		public static string Time_ToText_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToText_Example2");
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0002AF7B File Offset: 0x0002917B
		public static string Time_ToText_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_ToText_Example3");
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0002AF87 File Offset: 0x00029187
		public static string List_Dates
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Dates");
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0002AF93 File Offset: 0x00029193
		public static string List_Dates_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Dates_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0002AFB1 File Offset: 0x000291B1
		public static string List_Dates_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Dates_Example1");
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0002AFBD File Offset: 0x000291BD
		public static string List_DateTimeZones
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_DateTimeZones");
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0002AFC9 File Offset: 0x000291C9
		public static string List_DateTimeZones_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_DateTimeZones_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0002AFE7 File Offset: 0x000291E7
		public static string List_DateTimeZones_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_DateTimeZones_Example1");
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0002AFF3 File Offset: 0x000291F3
		public static string List_Times
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Times");
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0002AFFF File Offset: 0x000291FF
		public static string List_Times_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_Times_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0002B01D File Offset: 0x0002921D
		public static string List_Times_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_Times_Example1");
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0002B029 File Offset: 0x00029229
		public static string DB2_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DB2_Database");
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0002B035 File Offset: 0x00029235
		public static string DB2_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DB2_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0002B053 File Offset: 0x00029253
		public static string Informix_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Informix_Database");
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0002B05F File Offset: 0x0002925F
		public static string Informix_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Informix_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0002B07D File Offset: 0x0002927D
		public static string MQ_Queue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("MQ_Queue");
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0002B089 File Offset: 0x00029289
		public static string MQ_Queue_Description(object p0, object p1, object p2, object p3, object p4)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("MQ_Queue_Description", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0002B0B0 File Offset: 0x000292B0
		public static string MySQL_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("MySQL_Database");
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0002B0BC File Offset: 0x000292BC
		public static string MySQL_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("MySQL_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0002B0DA File Offset: 0x000292DA
		public static string Xml_Tables
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Xml_Tables");
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0002B0E6 File Offset: 0x000292E6
		public static string Xml_Tables_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Xml_Tables_Example1");
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0002B0F2 File Offset: 0x000292F2
		public static string PostgreSQL_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("PostgreSQL_Database");
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0002B0FE File Offset: 0x000292FE
		public static string PostgreSQL_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("PostgreSQL_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0002B11C File Offset: 0x0002931C
		public static string Sybase_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Sybase_Database");
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0002B128 File Offset: 0x00029328
		public static string Sybase_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Sybase_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0002B146 File Offset: 0x00029346
		public static string Teradata_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Teradata_Database");
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0002B152 File Offset: 0x00029352
		public static string Teradata_Database_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Teradata_Database_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0002B16C File Offset: 0x0002936C
		public static string Number_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_From");
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0002B178 File Offset: 0x00029378
		public static string Number_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Number_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0002B192 File Offset: 0x00029392
		public static string Number_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_From_Example1");
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0002B19E File Offset: 0x0002939E
		public static string Number_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_From_Example2");
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0002B1AA File Offset: 0x000293AA
		public static string Number_From_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Number_From_Example3");
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0002B1B6 File Offset: 0x000293B6
		public static string Binary_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_From");
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0002B1C2 File Offset: 0x000293C2
		public static string Binary_From_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_From_Description", new object[] { p0 });
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0002B1D8 File Offset: 0x000293D8
		public static string DateTimeZone_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_From");
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0002B1E4 File Offset: 0x000293E4
		public static string DateTimeZone_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0002B1FE File Offset: 0x000293FE
		public static string DateTime_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_From");
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0002B20A File Offset: 0x0002940A
		public static string DateTime_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0002B224 File Offset: 0x00029424
		public static string Date_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_From");
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0002B230 File Offset: 0x00029430
		public static string Date_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Date_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0002B24A File Offset: 0x0002944A
		public static string Duration_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_From");
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0002B256 File Offset: 0x00029456
		public static string Duration_From_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_From_Description", new object[] { p0 });
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0002B26C File Offset: 0x0002946C
		public static string Logical_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_From");
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0002B278 File Offset: 0x00029478
		public static string Logical_From_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_From_Description", new object[] { p0 });
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0002B28E File Offset: 0x0002948E
		public static string Time_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_From");
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0002B29A File Offset: 0x0002949A
		public static string Time_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Time_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0002B2B4 File Offset: 0x000294B4
		public static string Binary_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_From_Example1");
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0002B2C0 File Offset: 0x000294C0
		public static string DateTimeZone_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTimeZone_From_Example1");
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0002B2CC File Offset: 0x000294CC
		public static string DateTime_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_From_Example1");
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0002B2D8 File Offset: 0x000294D8
		public static string DateTime_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_From_Example2");
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0002B2E4 File Offset: 0x000294E4
		public static string Date_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_From_Example1");
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0002B2F0 File Offset: 0x000294F0
		public static string Date_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Date_From_Example2");
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0002B2FC File Offset: 0x000294FC
		public static string Type_AddTableKey
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_AddTableKey");
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0002B308 File Offset: 0x00029508
		public static string Type_ReplaceTableKeys
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ReplaceTableKeys");
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0002B314 File Offset: 0x00029514
		public static string Type_ReplaceTableKeys_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ReplaceTableKeys_Description");
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0002B320 File Offset: 0x00029520
		public static string Type_ReplaceTableKeys_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ReplaceTableKeys_Example1");
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0002B32C File Offset: 0x0002952C
		public static string Type_ReplaceTableKeys_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ReplaceTableKeys_Example2");
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0002B338 File Offset: 0x00029538
		public static string Type_TableKeys
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableKeys");
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0002B344 File Offset: 0x00029544
		public static string Type_TableKeys_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableKeys_Description");
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0002B350 File Offset: 0x00029550
		public static string Type_TableKeys_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableKeys_Example1");
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0002B35C File Offset: 0x0002955C
		public static string Duration_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Duration_From_Example1");
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0002B368 File Offset: 0x00029568
		public static string Logical_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Logical_From_Example1");
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0002B374 File Offset: 0x00029574
		public static string Time_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_From_Example1");
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0002B380 File Offset: 0x00029580
		public static string Time_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Time_From_Example2");
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0002B38C File Offset: 0x0002958C
		public static string Table_UnpivotOtherColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_UnpivotOtherColumns");
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0002B398 File Offset: 0x00029598
		public static string Table_UnpivotOtherColumns_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_UnpivotOtherColumns_Description");
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0002B3A4 File Offset: 0x000295A4
		public static string Table_UnpivotOtherColumns_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_UnpivotOtherColumns_Example1");
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0002B3B0 File Offset: 0x000295B0
		public static string Table_Unpivot
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Unpivot");
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0002B3BC File Offset: 0x000295BC
		public static string Table_Unpivot_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Unpivot_Description");
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0002B3C8 File Offset: 0x000295C8
		public static string Table_Unpivot_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Unpivot_Example1");
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0002B3D4 File Offset: 0x000295D4
		public static string DateTime_LocalNow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_LocalNow");
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0002B3E0 File Offset: 0x000295E0
		public static string DateTime_LocalNow_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_LocalNow_Description");
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0002B3EC File Offset: 0x000295EC
		public static string DateTime_FixedLocalNow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FixedLocalNow");
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0002B3F8 File Offset: 0x000295F8
		public static string DateTime_FixedLocalNow_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DateTime_FixedLocalNow_Description");
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0002B404 File Offset: 0x00029604
		public static string String
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("String");
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0002B410 File Offset: 0x00029610
		public static string Table_RemoveRowsWithErrors
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRowsWithErrors");
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0002B41C File Offset: 0x0002961C
		public static string Table_RemoveRowsWithErrors_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_RemoveRowsWithErrors_Example1");
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0002B428 File Offset: 0x00029628
		public static string Table_SelectRowsWithErrors
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectRowsWithErrors");
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0002B434 File Offset: 0x00029634
		public static string Table_SelectRowsWithErrors_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_SelectRowsWithErrors_Example1");
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0002B440 File Offset: 0x00029640
		public static string MissingField_Error
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("MissingField_Error");
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0002B44C File Offset: 0x0002964C
		public static string MissingField_Ignore
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("MissingField_Ignore");
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x0002B458 File Offset: 0x00029658
		public static string MissingField_UseNull
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("MissingField_UseNull");
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0002B464 File Offset: 0x00029664
		public static string Replacer_ReplaceText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Replacer_ReplaceText");
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0002B470 File Offset: 0x00029670
		public static string Replacer_ReplaceText_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Replacer_ReplaceText_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0002B48E File Offset: 0x0002968E
		public static string Replacer_ReplaceText_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Replacer_ReplaceText_Example1");
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0002B49A File Offset: 0x0002969A
		public static string Replacer_ReplaceValue
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Replacer_ReplaceValue");
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0002B4A6 File Offset: 0x000296A6
		public static string Replacer_ReplaceValue_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Replacer_ReplaceValue_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0002B4C4 File Offset: 0x000296C4
		public static string Replacer_ReplaceValue_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Replacer_ReplaceValue_Example1");
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0002B4D0 File Offset: 0x000296D0
		public static string TextEncoding_BigEndianUnicode
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_BigEndianUnicode");
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0002B4DC File Offset: 0x000296DC
		public static string TextEncoding_Unicode
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_Unicode");
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0002B4E8 File Offset: 0x000296E8
		public static string TextEncoding_Windows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_Windows");
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0002B4F4 File Offset: 0x000296F4
		public static string Table_Pivot
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Pivot");
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0002B500 File Offset: 0x00029700
		public static string Table_Pivot_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Pivot_Description");
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0002B50C File Offset: 0x0002970C
		public static string Table_Pivot_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Pivot_Example1");
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0002B518 File Offset: 0x00029718
		public static string Table_Pivot_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Pivot_Example2");
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0002B524 File Offset: 0x00029724
		public static string BinaryFormat_7BitEncodedSignedInteger
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_7BitEncodedSignedInteger");
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0002B530 File Offset: 0x00029730
		public static string BinaryFormat_7BitEncodedUnsignedInteger
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_7BitEncodedUnsignedInteger");
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0002B53C File Offset: 0x0002973C
		public static string BinaryFormat_Binary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Binary");
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0002B548 File Offset: 0x00029748
		public static string BinaryFormat_Binary_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Binary_Description", new object[] { p0 });
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0002B55E File Offset: 0x0002975E
		public static string BinaryFormat_Byte
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Byte");
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0002B56A File Offset: 0x0002976A
		public static string BinaryFormat_ByteOrder
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_ByteOrder");
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0002B576 File Offset: 0x00029776
		public static string BinaryFormat_ByteOrder_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_ByteOrder_Description", new object[] { p0 });
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0002B58C File Offset: 0x0002978C
		public static string BinaryFormat_Choice
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Choice");
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0002B598 File Offset: 0x00029798
		public static string BinaryFormat_Choice_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Choice_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0002B5B6 File Offset: 0x000297B6
		public static string BinaryFormat_Choice_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Choice_Example1");
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0002B5C2 File Offset: 0x000297C2
		public static string BinaryFormat_Choice_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Choice_Example2");
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0002B5CE File Offset: 0x000297CE
		public static string BinaryFormat_Choice_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Choice_Example3");
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0002B5DA File Offset: 0x000297DA
		public static string BinaryFormat_Decimal
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Decimal");
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0002B5E6 File Offset: 0x000297E6
		public static string BinaryFormat_Double
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Double");
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0002B5F2 File Offset: 0x000297F2
		public static string BinaryFormat_Length
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Length");
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0002B5FE File Offset: 0x000297FE
		public static string BinaryFormat_Length_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Length_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0002B618 File Offset: 0x00029818
		public static string BinaryFormat_Length_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Length_Example1");
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0002B624 File Offset: 0x00029824
		public static string BinaryFormat_List
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_List");
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0002B630 File Offset: 0x00029830
		public static string BinaryFormat_List_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_List_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0002B64A File Offset: 0x0002984A
		public static string BinaryFormat_List_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_List_Example1");
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0002B656 File Offset: 0x00029856
		public static string BinaryFormat_List_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_List_Example2");
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0002B662 File Offset: 0x00029862
		public static string BinaryFormat_List_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_List_Example3");
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0002B66E File Offset: 0x0002986E
		public static string BinaryFormat_Record
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Record");
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0002B67A File Offset: 0x0002987A
		public static string BinaryFormat_Record_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Record_Description", new object[] { p0 });
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0002B690 File Offset: 0x00029890
		public static string BinaryFormat_Record_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Record_Example1");
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0002B69C File Offset: 0x0002989C
		public static string BinaryFormat_SignedInteger16
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_SignedInteger16");
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0002B6A8 File Offset: 0x000298A8
		public static string BinaryFormat_SignedInteger32
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_SignedInteger32");
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0002B6B4 File Offset: 0x000298B4
		public static string BinaryFormat_SignedInteger64
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_SignedInteger64");
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0002B6C0 File Offset: 0x000298C0
		public static string BinaryFormat_Single
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Single");
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0002B6CC File Offset: 0x000298CC
		public static string BinaryFormat_Text
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Text");
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0002B6D8 File Offset: 0x000298D8
		public static string BinaryFormat_Text_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Text_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0002B6F2 File Offset: 0x000298F2
		public static string BinaryFormat_Text_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Text_Example1");
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0002B6FE File Offset: 0x000298FE
		public static string BinaryFormat_Transform
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Transform");
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0002B70A File Offset: 0x0002990A
		public static string BinaryFormat_Transform_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Transform_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0002B724 File Offset: 0x00029924
		public static string BinaryFormat_Transform_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Transform_Example1");
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0002B730 File Offset: 0x00029930
		public static string BinaryFormat_UnsignedInteger16
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_UnsignedInteger16");
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0002B73C File Offset: 0x0002993C
		public static string BinaryFormat_UnsignedInteger32
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_UnsignedInteger32");
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0002B748 File Offset: 0x00029948
		public static string BinaryFormat_UnsignedInteger64
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_UnsignedInteger64");
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0002B754 File Offset: 0x00029954
		public static string ByteOrder_BigEndian
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ByteOrder_BigEndian");
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0002B760 File Offset: 0x00029960
		public static string ByteOrder_LittleEndian
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ByteOrder_LittleEndian");
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0002B76C File Offset: 0x0002996C
		public static string Table_FromPartitions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromPartitions");
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0002B778 File Offset: 0x00029978
		public static string Table_FromPartitions_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromPartitions_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0002B796 File Offset: 0x00029996
		public static string Table_FromPartitions_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromPartitions_Example1");
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0002B7A2 File Offset: 0x000299A2
		public static string Value_NullableEquals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_NullableEquals");
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0002B7AE File Offset: 0x000299AE
		public static string Value_NullableEquals_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_NullableEquals_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0002B7C8 File Offset: 0x000299C8
		public static string BinaryFormat_Group
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Group");
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0002B7D4 File Offset: 0x000299D4
		public static string BinaryFormat_Group_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Group_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0002B7F2 File Offset: 0x000299F2
		public static string BinaryFormat_Group_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Group_Example1");
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0002B7FE File Offset: 0x000299FE
		public static string BinaryFormat_Group_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Group_Example2");
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0002B80A File Offset: 0x00029A0A
		public static string BinaryFormat_Length_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Length_Example2");
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0002B816 File Offset: 0x00029A16
		public static string BinaryFormat_Text_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Text_Example2");
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0002B822 File Offset: 0x00029A22
		public static string Binary_ApproximateLength
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ApproximateLength");
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0002B82E File Offset: 0x00029A2E
		public static string Binary_ApproximateLength_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ApproximateLength_Description", new object[] { p0 });
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0002B844 File Offset: 0x00029A44
		public static string Binary_ApproximateLength_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ApproximateLength_Example1");
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0002B850 File Offset: 0x00029A50
		public static string Binary_Buffer
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Buffer");
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0002B85C File Offset: 0x00029A5C
		public static string Binary_Buffer_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Buffer_Description");
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0002B868 File Offset: 0x00029A68
		public static string Binary_Buffer_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Buffer_Example1");
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0002B874 File Offset: 0x00029A74
		public static string Binary_Compress
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Compress");
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0002B880 File Offset: 0x00029A80
		public static string Binary_Compress_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Compress_Description");
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0002B88C File Offset: 0x00029A8C
		public static string Binary_Compress_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Compress_Example1");
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0002B898 File Offset: 0x00029A98
		public static string Binary_Decompress
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Decompress");
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0002B8A4 File Offset: 0x00029AA4
		public static string Binary_Decompress_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Decompress_Description");
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		public static string Binary_Decompress_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Decompress_Example1");
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public static string Compression_GZip
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_GZip");
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0002B8C8 File Offset: 0x00029AC8
		public static string Compression_Deflate
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_Deflate");
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		public static string BinaryOccurrence_Optional
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryOccurrence_Optional");
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0002B8E0 File Offset: 0x00029AE0
		public static string BinaryOccurrence_Repeating
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryOccurrence_Repeating");
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0002B8EC File Offset: 0x00029AEC
		public static string BinaryOccurrence_Required
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryOccurrence_Required");
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0002B8F8 File Offset: 0x00029AF8
		public static string Occurrence_Optional
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_Optional");
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0002B904 File Offset: 0x00029B04
		public static string Occurrence_Repeating
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_Repeating");
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0002B910 File Offset: 0x00029B10
		public static string Occurrence_Required
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_Required");
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0002B91C File Offset: 0x00029B1C
		public static string Record_FromList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromList");
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0002B928 File Offset: 0x00029B28
		public static string Record_FromList_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromList_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0002B942 File Offset: 0x00029B42
		public static string Record_FromList_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromList_Example1");
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0002B94E File Offset: 0x00029B4E
		public static string Record_FromList_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_FromList_Example2");
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0002B95A File Offset: 0x00029B5A
		public static string Record_ToList
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ToList");
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0002B966 File Offset: 0x00029B66
		public static string Record_ToList_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ToList_Description", new object[] { p0 });
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0002B97C File Offset: 0x00029B7C
		public static string Record_ToList_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Record_ToList_Example1");
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0002B988 File Offset: 0x00029B88
		public static string Type_TableColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableColumn");
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0002B994 File Offset: 0x00029B94
		public static string Type_TableColumn_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableColumn_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0002B9AE File Offset: 0x00029BAE
		public static string BinaryFormat_Null
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Null");
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0002B9BA File Offset: 0x00029BBA
		public static string BinaryFormat_Null_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryFormat_Null_Description");
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0002B9C6 File Offset: 0x00029BC6
		public static string Table_PartitionValues
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PartitionValues");
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0002B9D2 File Offset: 0x00029BD2
		public static string Table_PartitionValues_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_PartitionValues_Description");
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0002B9DE File Offset: 0x00029BDE
		public static string Soda_Feed
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Soda_Feed");
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0002B9EA File Offset: 0x00029BEA
		public static string Soda_Feed_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Soda_Feed_Description", new object[] { p0 });
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0002BA00 File Offset: 0x00029C00
		public static string Cube_DisplayFolders
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_DisplayFolders");
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0002BA0C File Offset: 0x00029C0C
		public static string Cube_DisplayFolders_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_DisplayFolders_Description", new object[] { p0 });
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0002BA22 File Offset: 0x00029C22
		public static string Cube_Dimensions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Dimensions");
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0002BA2E File Offset: 0x00029C2E
		public static string Cube_Dimensions_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Dimensions_Description", new object[] { p0 });
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0002BA44 File Offset: 0x00029C44
		public static string Cube_Measures
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Measures");
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0002BA50 File Offset: 0x00029C50
		public static string Cube_Measures_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Measures_Description", new object[] { p0 });
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0002BA66 File Offset: 0x00029C66
		public static string Cube_AddMeasureColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AddMeasureColumn");
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0002BA72 File Offset: 0x00029C72
		public static string Cube_AddMeasureColumn_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AddMeasureColumn_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0002BA90 File Offset: 0x00029C90
		public static string Cube_AddAndExpandDimensionColumn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AddAndExpandDimensionColumn");
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0002BA9C File Offset: 0x00029C9C
		public static string Cube_AddAndExpandDimensionColumn_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AddAndExpandDimensionColumn_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0002BABE File Offset: 0x00029CBE
		public static string Cube_CollapseAndRemoveColumns
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_CollapseAndRemoveColumns");
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0002BACA File Offset: 0x00029CCA
		public static string Cube_CollapseAndRemoveColumns_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_CollapseAndRemoveColumns_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0002BAE4 File Offset: 0x00029CE4
		public static string Cube_Properties
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Properties");
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		public static string Cube_Properties_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Properties_Description");
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0002BAFC File Offset: 0x00029CFC
		public static string Cube_MeasureProperties
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_MeasureProperties");
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0002BB08 File Offset: 0x00029D08
		public static string Cube_MeasureProperties_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_MeasureProperties_Description");
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0002BB14 File Offset: 0x00029D14
		public static string Cube_Transform
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Transform");
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0002BB20 File Offset: 0x00029D20
		public static string Cube_Transform_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Transform_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0002BB3A File Offset: 0x00029D3A
		public static string Salesforce_Data
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Salesforce_Data");
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0002BB46 File Offset: 0x00029D46
		public static string Salesforce_Data_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Salesforce_Data_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0002BB60 File Offset: 0x00029D60
		public static string Salesforce_Reports
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Salesforce_Reports");
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0002BB6C File Offset: 0x00029D6C
		public static string Salesforce_Reports_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Salesforce_Reports_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0002BB86 File Offset: 0x00029D86
		public static string Byte_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Byte_From");
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0002BB92 File Offset: 0x00029D92
		public static string Byte_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Byte_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0002BBAC File Offset: 0x00029DAC
		public static string Byte_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Byte_From_Example1");
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0002BBB8 File Offset: 0x00029DB8
		public static string Byte_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Byte_From_Example2");
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		public static string Int8_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int8_From");
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0002BBD0 File Offset: 0x00029DD0
		public static string Int8_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Int8_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0002BBEA File Offset: 0x00029DEA
		public static string Int8_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int8_From_Example1");
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0002BBF6 File Offset: 0x00029DF6
		public static string Int8_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int8_From_Example2");
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0002BC02 File Offset: 0x00029E02
		public static string Int16_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int16_From");
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0002BC0E File Offset: 0x00029E0E
		public static string Int16_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Int16_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0002BC28 File Offset: 0x00029E28
		public static string Int16_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int16_From_Example1");
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0002BC34 File Offset: 0x00029E34
		public static string Int16_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int16_From_Example2");
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0002BC40 File Offset: 0x00029E40
		public static string Int32_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int32_From");
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0002BC4C File Offset: 0x00029E4C
		public static string Int32_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Int32_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0002BC66 File Offset: 0x00029E66
		public static string Int32_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int32_From_Example1");
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0002BC72 File Offset: 0x00029E72
		public static string Int32_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int32_From_Example2");
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0002BC7E File Offset: 0x00029E7E
		public static string Int64_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int64_From");
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0002BC8A File Offset: 0x00029E8A
		public static string Int64_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Int64_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0002BCA4 File Offset: 0x00029EA4
		public static string Int64_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int64_From_Example1");
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0002BCB0 File Offset: 0x00029EB0
		public static string Int64_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Int64_From_Example2");
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0002BCBC File Offset: 0x00029EBC
		public static string Single_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Single_From");
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0002BCC8 File Offset: 0x00029EC8
		public static string Single_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Single_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0002BCE2 File Offset: 0x00029EE2
		public static string Single_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Single_From_Example1");
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0002BCEE File Offset: 0x00029EEE
		public static string Decimal_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Decimal_From");
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0002BCFA File Offset: 0x00029EFA
		public static string Decimal_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Decimal_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0002BD14 File Offset: 0x00029F14
		public static string Decimal_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Decimal_From_Example1");
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0002BD20 File Offset: 0x00029F20
		public static string Double_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Double_From");
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0002BD2C File Offset: 0x00029F2C
		public static string Double_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Double_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0002BD46 File Offset: 0x00029F46
		public static string Double_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Double_From_Example1");
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0002BD52 File Offset: 0x00029F52
		public static string Currency_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Currency_From");
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0002BD5E File Offset: 0x00029F5E
		public static string Currency_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Currency_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0002BD78 File Offset: 0x00029F78
		public static string Currency_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Currency_From_Example1");
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0002BD84 File Offset: 0x00029F84
		public static string Currency_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Currency_From_Example2");
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0002BD90 File Offset: 0x00029F90
		public static string Percentage_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Percentage_From");
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0002BD9C File Offset: 0x00029F9C
		public static string Percentage_From_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Percentage_From_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0002BDB6 File Offset: 0x00029FB6
		public static string Percentage_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Percentage_From_Example1");
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0002BDC2 File Offset: 0x00029FC2
		public static string OData_Feed_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("OData_Feed_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public static string OData_Feed_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("OData_Feed_Example1");
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0002BDEC File Offset: 0x00029FEC
		public static string Comparer_Equals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_Equals");
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0002BDF8 File Offset: 0x00029FF8
		public static string Comparer_Equals_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_Equals_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0002BE16 File Offset: 0x0002A016
		public static string Comparer_Equals_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_Equals_Example1");
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0002BE22 File Offset: 0x0002A022
		public static string Comparer_FromCulture
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_FromCulture");
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0002BE2E File Offset: 0x0002A02E
		public static string Comparer_FromCulture_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_FromCulture_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0002BE48 File Offset: 0x0002A048
		public static string Comparer_FromCulture_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_FromCulture_Example1");
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0002BE54 File Offset: 0x0002A054
		public static string Comparer_FromCulture_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_FromCulture_Example2");
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0002BE60 File Offset: 0x0002A060
		public static string Comparer_Ordinal
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_Ordinal");
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0002BE6C File Offset: 0x0002A06C
		public static string Comparer_Ordinal_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_Ordinal_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0002BE86 File Offset: 0x0002A086
		public static string Comparer_Ordinal_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_Ordinal_Example1");
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0002BE92 File Offset: 0x0002A092
		public static string Comparer_OrdinalIgnoreCase
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_OrdinalIgnoreCase");
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0002BE9E File Offset: 0x0002A09E
		public static string Comparer_OrdinalIgnoreCase_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_OrdinalIgnoreCase_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		public static string Comparer_OrdinalIgnoreCase_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Comparer_OrdinalIgnoreCase_Example1");
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0002BEC4 File Offset: 0x0002A0C4
		public static string Type_FunctionParameters
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionParameters");
			}
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public static string Type_FunctionParameters_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionParameters_Description", new object[] { p0 });
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0002BEE6 File Offset: 0x0002A0E6
		public static string Type_FunctionParameters_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionParameters_Example1");
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0002BEF2 File Offset: 0x0002A0F2
		public static string Type_FunctionReturn
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionReturn");
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0002BEFE File Offset: 0x0002A0FE
		public static string Type_FunctionReturn_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionReturn_Description", new object[] { p0 });
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0002BF14 File Offset: 0x0002A114
		public static string Type_FunctionReturn_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionReturn_Example1");
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0002BF20 File Offset: 0x0002A120
		public static string Type_FunctionRequiredParameters
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionRequiredParameters");
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0002BF2C File Offset: 0x0002A12C
		public static string Type_FunctionRequiredParameters_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionRequiredParameters_Description", new object[] { p0 });
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0002BF42 File Offset: 0x0002A142
		public static string Type_FunctionRequiredParameters_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_FunctionRequiredParameters_Example1");
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0002BF4E File Offset: 0x0002A14E
		public static string Type_IsNullable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsNullable");
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0002BF5A File Offset: 0x0002A15A
		public static string Type_IsNullable_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsNullable_Description");
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0002BF66 File Offset: 0x0002A166
		public static string Type_IsNullable_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsNullable_Example1");
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0002BF72 File Offset: 0x0002A172
		public static string Type_IsNullable_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsNullable_Example2");
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0002BF7E File Offset: 0x0002A17E
		public static string Type_NonNullable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_NonNullable");
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0002BF8A File Offset: 0x0002A18A
		public static string Type_NonNullable_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_NonNullable_Description", new object[] { p0 });
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public static string Type_NonNullable_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_NonNullable_Example1");
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0002BFAC File Offset: 0x0002A1AC
		public static string Type_ClosedRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ClosedRecord");
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
		public static string Type_ClosedRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ClosedRecord_Description", new object[] { p0 });
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0002BFCE File Offset: 0x0002A1CE
		public static string Type_ClosedRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ClosedRecord_Example1");
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0002BFDA File Offset: 0x0002A1DA
		public static string Type_OpenRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_OpenRecord");
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0002BFE6 File Offset: 0x0002A1E6
		public static string Type_OpenRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_OpenRecord_Description", new object[] { p0 });
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		public static string Type_OpenRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_OpenRecord_Example1");
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0002C008 File Offset: 0x0002A208
		public static string Type_IsOpenRecord
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsOpenRecord");
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0002C014 File Offset: 0x0002A214
		public static string Type_IsOpenRecord_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsOpenRecord_Description", new object[] { p0 });
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0002C02A File Offset: 0x0002A22A
		public static string Type_IsOpenRecord_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_IsOpenRecord_Example1");
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0002C036 File Offset: 0x0002A236
		public static string Type_RecordFields
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_RecordFields");
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0002C042 File Offset: 0x0002A242
		public static string Type_RecordFields_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_RecordFields_Description", new object[] { p0 });
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0002C058 File Offset: 0x0002A258
		public static string Type_RecordFields_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_RecordFields_Example1");
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0002C064 File Offset: 0x0002A264
		public static string Type_ForFunction
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForFunction");
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0002C070 File Offset: 0x0002A270
		public static string Type_ForFunction_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForFunction_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0002C08A File Offset: 0x0002A28A
		public static string Type_ForFunction_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ForFunction_Example1");
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0002C096 File Offset: 0x0002A296
		public static string Type_ListItem
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ListItem");
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0002C0A2 File Offset: 0x0002A2A2
		public static string Type_ListItem_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ListItem_Description", new object[] { p0 });
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0002C0B8 File Offset: 0x0002A2B8
		public static string Type_ListItem_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ListItem_Example1");
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
		public static string AnalysisServices_Databases
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AnalysisServices_Databases");
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		public static string AnalysisServices_Databases_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AnalysisServices_Databases_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0002C0EA File Offset: 0x0002A2EA
		public static string AnalysisServices_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AnalysisServices_Database");
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0002C0F6 File Offset: 0x0002A2F6
		public static string AnalysisServices_Database_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AnalysisServices_Database_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0002C114 File Offset: 0x0002A314
		public static string AdobeAnalytics_Cubes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AdobeAnalytics_Cubes");
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0002C120 File Offset: 0x0002A320
		public static string AdobeAnalytics_Cubes_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AdobeAnalytics_Cubes_Description", new object[] { p0 });
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0002C136 File Offset: 0x0002A336
		public static string Cube_AttributeMemberId
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AttributeMemberId");
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0002C142 File Offset: 0x0002A342
		public static string Cube_AttributeMemberId_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AttributeMemberId_Description", new object[] { p0 });
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0002C158 File Offset: 0x0002A358
		public static string Cube_AttributeMemberProperty
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AttributeMemberProperty");
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0002C164 File Offset: 0x0002A364
		public static string Cube_AttributeMemberProperty_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_AttributeMemberProperty_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0002C17E File Offset: 0x0002A37E
		public static string Cube_PropertyKey
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_PropertyKey");
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0002C18A File Offset: 0x0002A38A
		public static string Cube_PropertyKey_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_PropertyKey_Description", new object[] { p0 });
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		public static string Cube_MeasureProperty
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_MeasureProperty");
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		public static string Cube_MeasureProperty_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_MeasureProperty_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0002C1C6 File Offset: 0x0002A3C6
		public static string Odbc_Query
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_Query");
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0002C1D2 File Offset: 0x0002A3D2
		public static string Odbc_Query_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_Query_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0002C1F0 File Offset: 0x0002A3F0
		public static string Odbc_Query_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_Query_Example1");
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0002C1FC File Offset: 0x0002A3FC
		public static string Odbc_InferOptions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_InferOptions");
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0002C208 File Offset: 0x0002A408
		public static string Odbc_InferOptions_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_InferOptions_Description", new object[] { p0 });
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0002C21E File Offset: 0x0002A41E
		public static string Odbc_InferOptions_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_InferOptions_Example1");
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0002C22A File Offset: 0x0002A42A
		public static string LimitClauseKind_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LimitClauseKind_Type");
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0002C236 File Offset: 0x0002A436
		public static string LimitClauseKind_None
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LimitClauseKind_None");
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0002C242 File Offset: 0x0002A442
		public static string LimitClauseKind_Top
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LimitClauseKind_Top");
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0002C24E File Offset: 0x0002A44E
		public static string LimitClauseKind_LimitOffset
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LimitClauseKind_LimitOffset");
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0002C25A File Offset: 0x0002A45A
		public static string LimitClauseKind_Limit
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LimitClauseKind_Limit");
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0002C266 File Offset: 0x0002A466
		public static string LimitClauseKind_AnsiSql2008
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LimitClauseKind_AnsiSql2008");
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0002C272 File Offset: 0x0002A472
		public static string OleDb_Query
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("OleDb_Query");
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0002C27E File Offset: 0x0002A47E
		public static string OleDb_Query_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("OleDb_Query_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0002C29C File Offset: 0x0002A49C
		public static string BufferMode_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BufferMode_Type");
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
		public static string BufferMode_Eager
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BufferMode_Eager");
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		public static string BufferMode_Delayed
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BufferMode_Delayed");
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0002C2C0 File Offset: 0x0002A4C0
		public static string BufferMode_Streaming
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BufferMode_Streaming");
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		public static string Function_InvokeAfter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_InvokeAfter");
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		public static string Function_InvokeAfter_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Function_InvokeAfter_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0002C2F2 File Offset: 0x0002A4F2
		public static string Diagnostics_Trace
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Diagnostics_Trace");
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0002C2FE File Offset: 0x0002A4FE
		public static string Diagnostics_Trace_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Diagnostics_Trace_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0002C320 File Offset: 0x0002A520
		public static string Diagnostics_Trace_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Diagnostics_Trace_Example1");
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0002C32C File Offset: 0x0002A52C
		public static string Diagnostics_ActivityId
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Diagnostics_ActivityId");
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0002C338 File Offset: 0x0002A538
		public static string Diagnostics_CorrelationId
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Diagnostics_CorrelationId");
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0002C344 File Offset: 0x0002A544
		public static string TraceLevel_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Type");
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0002C350 File Offset: 0x0002A550
		public static string TraceLevel_Type_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Type_Description");
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0002C35C File Offset: 0x0002A55C
		public static string TraceLevel_Critical
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Critical");
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0002C368 File Offset: 0x0002A568
		public static string TraceLevel_Critical_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Critical_Description");
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0002C374 File Offset: 0x0002A574
		public static string TraceLevel_Error
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Error");
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0002C380 File Offset: 0x0002A580
		public static string TraceLevel_Error_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Error_Description");
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0002C38C File Offset: 0x0002A58C
		public static string TraceLevel_Warning
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Warning");
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0002C398 File Offset: 0x0002A598
		public static string TraceLevel_Warning_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Warning_Description");
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0002C3A4 File Offset: 0x0002A5A4
		public static string TraceLevel_Information
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Information");
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0002C3B0 File Offset: 0x0002A5B0
		public static string TraceLevel_Information_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Information_Description");
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0002C3BC File Offset: 0x0002A5BC
		public static string TraceLevel_Verbose
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Verbose");
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0002C3C8 File Offset: 0x0002A5C8
		public static string TraceLevel_Verbose_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TraceLevel_Verbose_Description");
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		public static string Function_IsDataSource
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_IsDataSource");
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
		public static string Function_IsDataSource_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Function_IsDataSource_Description", new object[] { p0 });
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0002C3F6 File Offset: 0x0002A5F6
		public static string Type_Union
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Union");
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0002C402 File Offset: 0x0002A602
		public static string Type_Union_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Union_Description", new object[] { p0 });
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0002C418 File Offset: 0x0002A618
		public static string BinaryOccurrence_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryOccurrence_Type");
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0002C424 File Offset: 0x0002A624
		public static string BinaryEncoding_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("BinaryEncoding_Type");
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0002C430 File Offset: 0x0002A630
		public static string ByteOrder_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ByteOrder_Type");
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0002C43C File Offset: 0x0002A63C
		public static string Compression_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_Type");
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0002C448 File Offset: 0x0002A648
		public static string Day_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Day_Type");
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0002C454 File Offset: 0x0002A654
		public static string ExtraValues_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ExtraValues_Type");
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0002C460 File Offset: 0x0002A660
		public static string GroupKind_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GroupKind_Type");
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0002C46C File Offset: 0x0002A66C
		public static string GroupKind_Type_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GroupKind_Type_Description");
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0002C478 File Offset: 0x0002A678
		public static string GroupKind_Local
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GroupKind_Local");
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0002C484 File Offset: 0x0002A684
		public static string GroupKind_Global
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GroupKind_Global");
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0002C490 File Offset: 0x0002A690
		public static string JoinAlgorithm_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_Type");
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0002C49C File Offset: 0x0002A69C
		public static string JoinAlgorithm_Dynamic
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_Dynamic");
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0002C4A8 File Offset: 0x0002A6A8
		public static string JoinAlgorithm_LeftHash
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_LeftHash");
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0002C4B4 File Offset: 0x0002A6B4
		public static string JoinAlgorithm_LeftIndex
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_LeftIndex");
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
		public static string JoinAlgorithm_PairwiseHash
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_PairwiseHash");
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		public static string JoinAlgorithm_RightHash
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_RightHash");
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		public static string JoinAlgorithm_RightIndex
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_RightIndex");
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		public static string JoinAlgorithm_SortMerge
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinAlgorithm_SortMerge");
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0002C4F0 File Offset: 0x0002A6F0
		public static string JoinKind_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinKind_Type");
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0002C4FC File Offset: 0x0002A6FC
		public static string MissingField_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("MissingField_Type");
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0002C508 File Offset: 0x0002A708
		public static string Occurrence_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Occurrence_Type");
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0002C514 File Offset: 0x0002A714
		public static string Order_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Order_Type");
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0002C520 File Offset: 0x0002A720
		public static string Precision_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Precision_Type");
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0002C52C File Offset: 0x0002A72C
		public static string QuoteStyle_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("QuoteStyle_Type");
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0002C538 File Offset: 0x0002A738
		public static string RoundingMode_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RoundingMode_Type");
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0002C544 File Offset: 0x0002A744
		public static string RoundingMode_AwayFromZero
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RoundingMode_AwayFromZero");
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0002C550 File Offset: 0x0002A750
		public static string RoundingMode_Down
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RoundingMode_Down");
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0002C55C File Offset: 0x0002A75C
		public static string RoundingMode_ToEven
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RoundingMode_ToEven");
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0002C568 File Offset: 0x0002A768
		public static string RoundingMode_TowardZero
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RoundingMode_TowardZero");
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0002C574 File Offset: 0x0002A774
		public static string RoundingMode_Up
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RoundingMode_Up");
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0002C580 File Offset: 0x0002A780
		public static string CryptoAlgorithm_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CryptoAlgorithm_Type");
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0002C58C File Offset: 0x0002A78C
		public static string LogLevel_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("LogLevel_Type");
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0002C598 File Offset: 0x0002A798
		public static string TextEncoding_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TextEncoding_Type");
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0002C5A4 File Offset: 0x0002A7A4
		public static string GoogleAnalytics_Accounts
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GoogleAnalytics_Accounts");
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
		public static string GoogleAnalytics_Accounts_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GoogleAnalytics_Accounts_Description");
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0002C5BC File Offset: 0x0002A7BC
		public static string CsvStyle_QuoteAfterDelimiter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CsvStyle_QuoteAfterDelimiter");
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0002C5C8 File Offset: 0x0002A7C8
		public static string CsvStyle_QuoteAlways
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CsvStyle_QuoteAlways");
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
		public static string CsvStyle_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CsvStyle_Type");
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		public static string Extension_Contents
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_Contents");
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0002C5EC File Offset: 0x0002A7EC
		public static string Extension_Contents_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_Contents_Description", new object[] { p0 });
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0002C602 File Offset: 0x0002A802
		public static string Extension_LoadString
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_LoadString");
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0002C60E File Offset: 0x0002A80E
		public static string Extension_LoadString_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_LoadString_Description", new object[] { p0 });
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0002C624 File Offset: 0x0002A824
		public static string Extension_InvokeWithCredentials
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_InvokeWithCredentials");
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0002C630 File Offset: 0x0002A830
		public static string Extension_InvokeWithCredentials_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_InvokeWithCredentials_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0002C64A File Offset: 0x0002A84A
		public static string Extension_InvokeWithPermissions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_InvokeWithPermissions");
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0002C656 File Offset: 0x0002A856
		public static string Extension_InvokeWithPermissions_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_InvokeWithPermissions_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0002C670 File Offset: 0x0002A870
		public static string Extension_InvokeVolatileFunction
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_InvokeVolatileFunction");
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0002C67C File Offset: 0x0002A87C
		public static string Extension_HasPermission(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_HasPermission", new object[] { p0 });
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0002C692 File Offset: 0x0002A892
		public static string Extension_HasPermission_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_HasPermission_Description", new object[] { p0 });
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0002C6A8 File Offset: 0x0002A8A8
		public static string Extension_Cache
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_Cache");
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0002C6B4 File Offset: 0x0002A8B4
		public static string Extension_Cache_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Extension_Cache_Description");
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0002C6C0 File Offset: 0x0002A8C0
		public static string Uri_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Uri_Type");
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0002C6CC File Offset: 0x0002A8CC
		public static string Password_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Password_Type");
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0002C6D8 File Offset: 0x0002A8D8
		public static string Odbc_DataSource
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_DataSource");
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		public static string Odbc_DataSource_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_DataSource_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0002C6FE File Offset: 0x0002A8FE
		public static string Odbc_DataSource_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Odbc_DataSource_Example1");
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0002C70A File Offset: 0x0002A90A
		public static string AdoDotNet_Query
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AdoDotNet_Query");
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0002C716 File Offset: 0x0002A916
		public static string AdoDotNet_Query_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AdoDotNet_Query_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0002C738 File Offset: 0x0002A938
		public static string AdoDotNet_DataSource
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AdoDotNet_DataSource");
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0002C744 File Offset: 0x0002A944
		public static string AdoDotNet_DataSource_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AdoDotNet_DataSource_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0002C762 File Offset: 0x0002A962
		public static string Action_Sequence
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Sequence");
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0002C76E File Offset: 0x0002A96E
		public static string Action_Sequence_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Sequence_Description", new object[] { p0 });
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0002C784 File Offset: 0x0002A984
		public static string Action_Sequence_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Sequence_Example1");
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0002C790 File Offset: 0x0002A990
		public static string Action_DoNothing
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_DoNothing");
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0002C79C File Offset: 0x0002A99C
		public static string Action_DoNothing_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_DoNothing_Description");
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0002C7A8 File Offset: 0x0002A9A8
		public static string Action_Return
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Return");
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
		public static string Action_Return_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Return_Description", new object[] { p0 });
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0002C7CA File Offset: 0x0002A9CA
		public static string Action_Return_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Return_Example1");
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0002C7D6 File Offset: 0x0002A9D6
		public static string Action_Try
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Try");
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0002C7E2 File Offset: 0x0002A9E2
		public static string Action_Try_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Try_Description", new object[] { p0 });
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		public static string Action_Try_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Try_Example1");
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0002C804 File Offset: 0x0002AA04
		public static string Action_Try_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_Try_Example2");
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0002C810 File Offset: 0x0002AA10
		public static string ValueAction_Replace
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ValueAction_Replace");
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0002C81C File Offset: 0x0002AA1C
		public static string ValueAction_Replace_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("ValueAction_Replace_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0002C836 File Offset: 0x0002AA36
		public static string TableAction_DeleteRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_DeleteRows");
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0002C842 File Offset: 0x0002AA42
		public static string TableAction_DeleteRows_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_DeleteRows_Description", new object[] { p0 });
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0002C858 File Offset: 0x0002AA58
		public static string TableAction_InsertRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_InsertRows");
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0002C864 File Offset: 0x0002AA64
		public static string TableAction_InsertRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_InsertRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0002C87E File Offset: 0x0002AA7E
		public static string TableAction_UpdateRows
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_UpdateRows");
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0002C88A File Offset: 0x0002AA8A
		public static string TableAction_UpdateRows_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_UpdateRows_Description", new object[] { p0, p1 });
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
		public static string ValueAction_NativeStatement
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ValueAction_NativeStatement");
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0002C8B0 File Offset: 0x0002AAB0
		public static string ValueAction_NativeStatement_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("ValueAction_NativeStatement_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0002C8D2 File Offset: 0x0002AAD2
		public static string Value_NativeQuery
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_NativeQuery");
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0002C8DE File Offset: 0x0002AADE
		public static string Value_NativeQuery_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_NativeQuery_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0002C900 File Offset: 0x0002AB00
		public static string Binary_End
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_End");
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0002C90C File Offset: 0x0002AB0C
		public static string Binary_End_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_End_Description", new object[] { p0 });
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0002C922 File Offset: 0x0002AB22
		public static string Binary_End_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_End_Example1");
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0002C92E File Offset: 0x0002AB2E
		public static string Text_Format
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Format");
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0002C93A File Offset: 0x0002AB3A
		public static string Text_Format_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Format_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0002C958 File Offset: 0x0002AB58
		public static string Text_Format_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Format_Example1");
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0002C964 File Offset: 0x0002AB64
		public static string Text_Format_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Format_Example2");
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0002C970 File Offset: 0x0002AB70
		public static string Table_FromColumns_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FromColumns_Example3");
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0002C97C File Offset: 0x0002AB7C
		public static string Table_InsertRows_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_InsertRows_Example2");
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0002C988 File Offset: 0x0002AB88
		public static string Tables_GetRelationships
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Tables_GetRelationships");
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0002C994 File Offset: 0x0002AB94
		public static string Tables_GetRelationships_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Tables_GetRelationships_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0002C9AE File Offset: 0x0002ABAE
		public static string Table_View
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_View");
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0002C9BA File Offset: 0x0002ABBA
		public static string Table_View_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_View_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
		public static string Table_View_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_View_Example1");
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		public static string Binary_View
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_View");
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		public static string Binary_View_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_View_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0002CA06 File Offset: 0x0002AC06
		public static string Binary_View_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_View_Example1");
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0002CA12 File Offset: 0x0002AC12
		public static string Action_View
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_View");
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0002CA1E File Offset: 0x0002AC1E
		public static string Action_View_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Action_View_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0002CA38 File Offset: 0x0002AC38
		public static string Cube_ApplyParameter
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_ApplyParameter");
			}
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0002CA44 File Offset: 0x0002AC44
		public static string Cube_ApplyParameter_Description(object p0, object p1, object p2)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_ApplyParameter_Description", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0002CA62 File Offset: 0x0002AC62
		public static string Cube_Parameters
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Parameters");
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0002CA6E File Offset: 0x0002AC6E
		public static string Cube_Parameters_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_Parameters_Description", new object[] { p0 });
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0002CA84 File Offset: 0x0002AC84
		public static string RowExpression_Column
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_Column");
			}
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0002CA90 File Offset: 0x0002AC90
		public static string RowExpression_Column_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_Column_Description", new object[] { p0 });
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0002CAA6 File Offset: 0x0002ACA6
		public static string RowExpression_Column_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_Column_Example1");
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0002CAB2 File Offset: 0x0002ACB2
		public static string RowExpression_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_From");
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0002CABE File Offset: 0x0002ACBE
		public static string RowExpression_From_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_From_Description", new object[] { p0 });
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0002CAD4 File Offset: 0x0002ACD4
		public static string RowExpression_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_From_Example1");
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		public static string RowExpression_Row
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_Row");
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0002CAEC File Offset: 0x0002ACEC
		public static string RowExpression_Row_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RowExpression_Row_Description");
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
		public static string ItemExpression_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ItemExpression_From");
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0002CB04 File Offset: 0x0002AD04
		public static string ItemExpression_From_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("ItemExpression_From_Description", new object[] { p0 });
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0002CB1A File Offset: 0x0002AD1A
		public static string ItemExpression_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ItemExpression_From_Example1");
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0002CB26 File Offset: 0x0002AD26
		public static string ItemExpression_Item
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ItemExpression_Item");
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0002CB32 File Offset: 0x0002AD32
		public static string ItemExpression_Item_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ItemExpression_Item_Description");
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0002CB3E File Offset: 0x0002AD3E
		public static string SapHana_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHana_Database");
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0002CB4A File Offset: 0x0002AD4A
		public static string SapHana_Database_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("SapHana_Database_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0002CB64 File Offset: 0x0002AD64
		public static string SapHanaRangeOperator_Equals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_Equals");
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0002CB70 File Offset: 0x0002AD70
		public static string SapHanaRangeOperator_GreaterThan
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_GreaterThan");
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0002CB7C File Offset: 0x0002AD7C
		public static string SapHanaRangeOperator_GreaterThanOrEquals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_GreaterThanOrEquals");
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0002CB88 File Offset: 0x0002AD88
		public static string SapHanaRangeOperator_LessThan
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_LessThan");
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0002CB94 File Offset: 0x0002AD94
		public static string SapHanaRangeOperator_LessThanOrEquals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_LessThanOrEquals");
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
		public static string SapHanaRangeOperator_NotEquals
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_NotEquals");
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0002CBAC File Offset: 0x0002ADAC
		public static string SapHanaRangeOperator_Between
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_Between");
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0002CBB8 File Offset: 0x0002ADB8
		public static string SapHanaRangeOperator_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaRangeOperator_Type");
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
		public static string SapHanaDistribution_Off
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaDistribution_Off");
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		public static string SapHanaDistribution_Connection
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaDistribution_Connection");
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0002CBDC File Offset: 0x0002ADDC
		public static string SapHanaDistribution_Statement
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaDistribution_Statement");
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0002CBE8 File Offset: 0x0002ADE8
		public static string SapHanaDistribution_All
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaDistribution_All");
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		public static string SapHanaDistribution_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapHanaDistribution_Type");
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0002CC00 File Offset: 0x0002AE00
		public static string SapBusinessWarehouse_Cubes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapBusinessWarehouse_Cubes");
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		public static string SapBusinessWarehouse_Cubes_Description(object p0, object p1, object p2, object p3)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("SapBusinessWarehouse_Cubes_Description", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0002CC2E File Offset: 0x0002AE2E
		public static string SapBusinessWarehouseExecutionMode_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapBusinessWarehouseExecutionMode_Type");
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0002CC3A File Offset: 0x0002AE3A
		public static string SapBusinessWarehouseExecutionMode_DataStream
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapBusinessWarehouseExecutionMode_DataStream");
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0002CC46 File Offset: 0x0002AE46
		public static string SapBusinessWarehouseExecutionMode_BasXml
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapBusinessWarehouseExecutionMode_BasXml");
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0002CC52 File Offset: 0x0002AE52
		public static string SapBusinessWarehouseExecutionMode_BasXmlGzip
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SapBusinessWarehouseExecutionMode_BasXmlGzip");
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0002CC5E File Offset: 0x0002AE5E
		public static string RData_FromBinary
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("RData_FromBinary");
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0002CC6A File Offset: 0x0002AE6A
		public static string Table_Schema
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Schema");
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0002CC76 File Offset: 0x0002AE76
		public static string Table_Schema_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Schema_Description", new object[] { p0 });
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0002CC8C File Offset: 0x0002AE8C
		public static string Type_Facets
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Facets");
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0002CC98 File Offset: 0x0002AE98
		public static string Type_Facets_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Facets_Description", new object[] { p0 });
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0002CCAE File Offset: 0x0002AEAE
		public static string Type_ReplaceFacets
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ReplaceFacets");
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0002CCBA File Offset: 0x0002AEBA
		public static string Type_ReplaceFacets_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_ReplaceFacets_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0002CCD4 File Offset: 0x0002AED4
		public static string Table_Profile
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Profile");
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		public static string Table_Profile_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Profile_Description", new object[] { p0 });
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0002CCF6 File Offset: 0x0002AEF6
		public static string Type_TableSchema
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableSchema");
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0002CD02 File Offset: 0x0002AF02
		public static string Type_TableSchema_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableSchema_Description", new object[] { p0 });
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0002CD18 File Offset: 0x0002AF18
		public static string Access_Database_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Access_Database_Description", new object[] { p0, p1 });
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0002CD32 File Offset: 0x0002AF32
		public static string OleDb_DataSource
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("OleDb_DataSource");
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0002CD3E File Offset: 0x0002AF3E
		public static string OleDb_DataSource_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("OleDb_DataSource_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0002CD58 File Offset: 0x0002AF58
		public static string Option_Culture_FuzzyGroup_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_FuzzyGroup_Description");
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0002CD64 File Offset: 0x0002AF64
		public static string Option_Culture_FuzzyGroup_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_FuzzyGroup_Caption");
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0002CD70 File Offset: 0x0002AF70
		public static string Option_Culture_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		public static string Option_Culture_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_FuzzyMatch_Caption");
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0002CD88 File Offset: 0x0002AF88
		public static string Option_MaxDegreeOfParallelism_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_MaxDegreeOfParallelism_Description");
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0002CD94 File Offset: 0x0002AF94
		public static string Option_MaxDegreeOfParallelism_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_MaxDegreeOfParallelism_Caption");
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		public static string Option_CreateNavigationProperties_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CreateNavigationProperties_Description");
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0002CDAC File Offset: 0x0002AFAC
		public static string Option_CreateNavigationProperties_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CreateNavigationProperties_Caption");
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
		public static string Option_CreateNavigationProperties_False_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CreateNavigationProperties_False_Description");
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		public static string Option_CreateNavigationProperties_False_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CreateNavigationProperties_False_Caption");
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0002CDD0 File Offset: 0x0002AFD0
		public static string Option_NavigationPropertyNameGenerator_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_NavigationPropertyNameGenerator_Description");
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0002CDDC File Offset: 0x0002AFDC
		public static string Option_NavigationPropertyNameGenerator_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_NavigationPropertyNameGenerator_Caption");
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
		public static string Option_Query_SQL_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Query_SQL_Description");
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		public static string Option_Query_SQL_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Query_SQL_Caption");
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0002CE00 File Offset: 0x0002B000
		public static string Option_CommandTimeout_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CommandTimeout_Description");
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0002CE0C File Offset: 0x0002B00C
		public static string Option_CommandTimeout_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CommandTimeout_Caption");
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0002CE18 File Offset: 0x0002B018
		public static string Option_CommandTimeout_AS_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CommandTimeout_AS_Description");
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0002CE24 File Offset: 0x0002B024
		public static string Option_CommandTimeout_AS_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_CommandTimeout_AS_Caption");
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0002CE30 File Offset: 0x0002B030
		public static string Option_ConcurrentRequests_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConcurrentRequests_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0002CE3C File Offset: 0x0002B03C
		public static string Option_ConcurrentRequests_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConcurrentRequests_FuzzyMatch_Caption");
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0002CE48 File Offset: 0x0002B048
		public static string Option_ConnectionTimeout_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConnectionTimeout_Description");
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0002CE54 File Offset: 0x0002B054
		public static string Option_ConnectionTimeout_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConnectionTimeout_Caption");
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0002CE60 File Offset: 0x0002B060
		public static string Option_ConnectionTimeout_ODBC_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConnectionTimeout_ODBC_Description");
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0002CE6C File Offset: 0x0002B06C
		public static string Option_ConnectionTimeout_ODBC_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConnectionTimeout_ODBC_Caption");
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0002CE78 File Offset: 0x0002B078
		public static string Option_EnableCrossDatabaseFolding_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableCrossDatabaseFolding_Description");
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0002CE84 File Offset: 0x0002B084
		public static string Option_EnableCrossDatabaseFolding_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableCrossDatabaseFolding_Caption");
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0002CE90 File Offset: 0x0002B090
		public static string Option_HierarchicalNavigation_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_HierarchicalNavigation_Description");
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0002CE9C File Offset: 0x0002B09C
		public static string Option_HierarchicalNavigation_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_HierarchicalNavigation_Caption");
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0002CEA8 File Offset: 0x0002B0A8
		public static string Option_HierarchicalNavigation_True_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_HierarchicalNavigation_True_Description");
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0002CEB4 File Offset: 0x0002B0B4
		public static string Option_HierarchicalNavigation_True_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_HierarchicalNavigation_True_Caption");
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0002CEC0 File Offset: 0x0002B0C0
		public static string Option_IgnoreCase_FuzzyGroup_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreCase_FuzzyGroup_Description");
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0002CECC File Offset: 0x0002B0CC
		public static string Option_IgnoreCase_FuzzyGroup_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreCase_FuzzyGroup_Caption");
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		public static string Option_IgnoreCase_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreCase_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0002CEE4 File Offset: 0x0002B0E4
		public static string Option_IgnoreCase_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreCase_FuzzyMatch_Caption");
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public static string Option_IgnoreSpace_FuzzyGroup_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreSpace_FuzzyGroup_Description");
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		public static string Option_IgnoreSpace_FuzzyGroup_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreSpace_FuzzyGroup_Caption");
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0002CF08 File Offset: 0x0002B108
		public static string Option_IgnoreSpace_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreSpace_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0002CF14 File Offset: 0x0002B114
		public static string Option_IgnoreSpace_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IgnoreSpace_FuzzyMatch_Caption");
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0002CF20 File Offset: 0x0002B120
		public static string Option_MultiSubnetFailover_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_MultiSubnetFailover_Description");
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0002CF2C File Offset: 0x0002B12C
		public static string Option_MultiSubnetFailover_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_MultiSubnetFailover_Caption");
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0002CF38 File Offset: 0x0002B138
		public static string Option_NumberOfMatches_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_NumberOfMatches_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0002CF44 File Offset: 0x0002B144
		public static string Option_NumberOfMatches_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_NumberOfMatches_FuzzyMatch_Caption");
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0002CF50 File Offset: 0x0002B150
		public static string Option_ContextInfo_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ContextInfo_Description");
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0002CF5C File Offset: 0x0002B15C
		public static string Option_ContextInfo_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ContextInfo_Caption");
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0002CF68 File Offset: 0x0002B168
		public static string Option_EnableBulkInsert_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableBulkInsert_Description");
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0002CF74 File Offset: 0x0002B174
		public static string Option_EnableBulkInsert_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableBulkInsert_Caption");
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0002CF80 File Offset: 0x0002B180
		public static string Option_Encoding_MySQL_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Encoding_MySQL_Description");
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0002CF8C File Offset: 0x0002B18C
		public static string Option_Encoding_MySQL_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Encoding_MySQL_Caption");
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0002CF98 File Offset: 0x0002B198
		public static string Option_SimilarityColumnName_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_SimilarityColumnName_Description");
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0002CFA4 File Offset: 0x0002B1A4
		public static string Option_Threshold_FuzzyGroup_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Threshold_FuzzyGroup_Description");
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0002CFB0 File Offset: 0x0002B1B0
		public static string Option_Threshold_FuzzyGroup_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Threshold_FuzzyGroup_Caption");
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0002CFBC File Offset: 0x0002B1BC
		public static string Option_Threshold_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Threshold_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
		public static string Option_Threshold_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Threshold_FuzzyMatch_Caption");
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
		public static string Option_TransformationTable_FuzzyGroup_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TransformationTable_FuzzyGroup_Description");
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		public static string Option_TransformationTable_FuzzyGroup_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TransformationTable_FuzzyGroup_Caption");
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0002CFEC File Offset: 0x0002B1EC
		public static string Option_TransformationTable_FuzzyMatch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TransformationTable_FuzzyMatch_Description");
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0002CFF8 File Offset: 0x0002B1F8
		public static string Option_TransformationTable_FuzzyMatch_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TransformationTable_FuzzyMatch_Caption");
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0002D004 File Offset: 0x0002B204
		public static string Option_TreatTinyAsBoolean_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TreatTinyAsBoolean_Description");
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0002D010 File Offset: 0x0002B210
		public static string Option_TreatTinyAsBoolean_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TreatTinyAsBoolean_Caption");
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0002D01C File Offset: 0x0002B21C
		public static string Option_SqlCompatibleWindowsAuth_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_SqlCompatibleWindowsAuth_Description");
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0002D028 File Offset: 0x0002B228
		public static string Option_SqlCompatibleWindowsAuth_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_SqlCompatibleWindowsAuth_Caption");
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0002D034 File Offset: 0x0002B234
		public static string Option_OldGuids_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_OldGuids_Description");
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0002D040 File Offset: 0x0002B240
		public static string Option_OldGuids_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_OldGuids_Caption");
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0002D04C File Offset: 0x0002B24C
		public static string Option_ReturnSingleDatabase_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ReturnSingleDatabase_Description");
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0002D058 File Offset: 0x0002B258
		public static string Option_ReturnSingleDatabase_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ReturnSingleDatabase_Caption");
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0002D064 File Offset: 0x0002B264
		public static string Option_Implementation_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_Description");
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0002D070 File Offset: 0x0002B270
		public static string Option_Implementation_AdobeAnalytics_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_AdobeAnalytics_Description");
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0002D07C File Offset: 0x0002B27C
		public static string Option_Implementation_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_Caption");
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0002D088 File Offset: 0x0002B288
		public static string Option_Implementation_HANA_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_HANA_Description");
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0002D094 File Offset: 0x0002B294
		public static string Option_Implementation_HANA_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_HANA_Caption");
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0002D0A0 File Offset: 0x0002B2A0
		public static string Option_BinaryCodePage_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_BinaryCodePage_Description");
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0002D0AC File Offset: 0x0002B2AC
		public static string Option_BinaryCodePage_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_BinaryCodePage_Caption");
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
		public static string Option_PackageCollection_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_PackageCollection_Description");
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
		public static string Option_PackageCollection_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_PackageCollection_Caption");
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0002D0D0 File Offset: 0x0002B2D0
		public static string Option_UseDb2ConnectGateway_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_UseDb2ConnectGateway_Description");
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		public static string Option_UseDb2ConnectGateway_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_UseDb2ConnectGateway_Caption");
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
		public static string Option_LanguageCode_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_LanguageCode_Description");
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		public static string Option_LanguageCode_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_LanguageCode_Caption");
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0002D100 File Offset: 0x0002B300
		public static string Option_Culture_SAP_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_SAP_Description");
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0002D10C File Offset: 0x0002B30C
		public static string Option_Culture_SAP_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_SAP_Caption");
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0002D118 File Offset: 0x0002B318
		public static string Option_Culture_AS_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_AS_Description");
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0002D124 File Offset: 0x0002B324
		public static string Option_Culture_AS_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Culture_AS_Caption");
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0002D130 File Offset: 0x0002B330
		public static string Option_TypedMeasureColumns_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TypedMeasureColumns_Description");
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0002D13C File Offset: 0x0002B33C
		public static string Option_TypedMeasureColumns_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_TypedMeasureColumns_Caption");
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0002D148 File Offset: 0x0002B348
		public static string Option_Query_MDX_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Query_MDX_Description");
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0002D154 File Offset: 0x0002B354
		public static string Option_Query_MDX_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Query_MDX_Caption");
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0002D160 File Offset: 0x0002B360
		public static string Option_ScaleMeasures_SAPBW_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ScaleMeasures_SAPBW_Description");
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0002D16C File Offset: 0x0002B36C
		public static string Option_ScaleMeasures_SAPBW_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ScaleMeasures_SAPBW_Caption");
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0002D178 File Offset: 0x0002B378
		public static string Option_EnableStructures_SAPBW_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableStructures_SAPBW_Description");
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0002D184 File Offset: 0x0002B384
		public static string Option_EnableStructures_SAPBW_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableStructures_SAPBW_Caption");
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0002D190 File Offset: 0x0002B390
		public static string Option_Implementation_SAPBW_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_SAPBW_Description");
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0002D19C File Offset: 0x0002B39C
		public static string Option_Implementation_SAPBW_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_SAPBW_Caption");
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		public static string Option_ExecutionMode_SAPBW_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ExecutionMode_SAPBW_Description");
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
		public static string Option_ExecutionMode_SAPBW_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ExecutionMode_SAPBW_Caption");
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0002D1C0 File Offset: 0x0002B3C0
		public static string Option_BatchSize_SAPBW_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_BatchSize_SAPBW_Description");
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		public static string Option_BatchSize_SAPBW_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_BatchSize_SAPBW_Caption");
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0002D1D8 File Offset: 0x0002B3D8
		public static string Option_ApiVersion_Salesforce_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ApiVersion_Salesforce_Description");
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		public static string Option_ApiVersion_Salesforce_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ApiVersion_Salesforce_Caption");
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0002D1F0 File Offset: 0x0002B3F0
		public static string Option_ApiVersion_SharePoint_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ApiVersion_SharePoint_Description");
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0002D1FC File Offset: 0x0002B3FC
		public static string Option_ApiVersion_SharePoint_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ApiVersion_SharePoint_Caption");
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0002D208 File Offset: 0x0002B408
		public static string Option_Implementation_SharePoint_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_SharePoint_Description");
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0002D214 File Offset: 0x0002B414
		public static string Option_Implementation_SharePoint_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Implementation_SharePoint_Caption");
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0002D220 File Offset: 0x0002B420
		public static string Option_ViewMode_SharePoint_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ViewMode_SharePoint_Description");
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0002D22C File Offset: 0x0002B42C
		public static string Option_ViewMode_SharePoint_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ViewMode_SharePoint_Caption");
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0002D238 File Offset: 0x0002B438
		public static string Option_DisableAppendNoteColumns_SharePoint_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_DisableAppendNoteColumns_SharePoint_Description");
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0002D244 File Offset: 0x0002B444
		public static string Option_SubQueries_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_SubQueries_Description");
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0002D250 File Offset: 0x0002B450
		public static string Option_SubQueries_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_SubQueries_Caption");
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0002D25C File Offset: 0x0002B45C
		public static string Option_MaxRetryCount_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_MaxRetryCount_Caption");
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0002D268 File Offset: 0x0002B468
		public static string Option_MaxRetryCount_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_MaxRetryCount_Description");
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0002D274 File Offset: 0x0002B474
		public static string Option_RetryInterval_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_RetryInterval_Caption");
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0002D280 File Offset: 0x0002B480
		public static string Option_RetryInterval_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_RetryInterval_Description");
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0002D28C File Offset: 0x0002B48C
		public static string Option_Distribution_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Distribution_Caption");
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0002D298 File Offset: 0x0002B498
		public static string Option_Distribution_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Distribution_Description");
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0002D2A4 File Offset: 0x0002B4A4
		public static string Option_EnableColumnBinding_HANA_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableColumnBinding_HANA_Caption");
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		public static string Option_EnableColumnBinding_HANA_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_EnableColumnBinding_HANA_Description");
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0002D2BC File Offset: 0x0002B4BC
		public static string Option_BlockSize_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_BlockSize_Caption");
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		public static string Option_BlockSize_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_BlockSize_Description");
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0002D2D4 File Offset: 0x0002B4D4
		public static string Option_RequestSize_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_RequestSize_Caption");
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		public static string Option_RequestSize_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_RequestSize_Description");
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0002D2EC File Offset: 0x0002B4EC
		public static string Option_ConcurrentRequests_Stream_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConcurrentRequests_Stream_Caption");
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
		public static string Option_ConcurrentRequests_Stream_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ConcurrentRequests_Stream_Description");
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0002D304 File Offset: 0x0002B504
		public static string Option_HierarchicalNavigation_Directory_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_HierarchicalNavigation_Directory_Caption");
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0002D310 File Offset: 0x0002B510
		public static string Option_HierarchicalNavigation_Directory_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_HierarchicalNavigation_Directory_Description");
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0002D31C File Offset: 0x0002B51C
		public static string Value_ViewFunction
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ViewFunction");
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0002D328 File Offset: 0x0002B528
		public static string Value_ViewFunction_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ViewFunction_Description");
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0002D334 File Offset: 0x0002B534
		public static string Value_ViewError
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ViewError");
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0002D340 File Offset: 0x0002B540
		public static string Value_ViewError_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ViewError_Description");
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0002D34C File Offset: 0x0002B54C
		public static string Table_ViewFunction
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ViewFunction");
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0002D358 File Offset: 0x0002B558
		public static string Table_ViewFunction_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ViewFunction_Description", new object[] { p0 });
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0002D36E File Offset: 0x0002B56E
		public static string Table_ViewError
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ViewError");
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0002D37A File Offset: 0x0002B57A
		public static string Table_ViewError_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ViewError_Description", new object[] { p0 });
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0002D390 File Offset: 0x0002B590
		public static string Binary_ViewFunction
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ViewFunction");
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0002D39C File Offset: 0x0002B59C
		public static string Binary_ViewFunction_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ViewFunction_Description", new object[] { p0 });
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0002D3B2 File Offset: 0x0002B5B2
		public static string Binary_ViewError
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ViewError");
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0002D3BE File Offset: 0x0002B5BE
		public static string Binary_ViewError_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_ViewError_Description", new object[] { p0 });
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		public static string Action_ViewFunction
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_ViewFunction");
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0002D3E0 File Offset: 0x0002B5E0
		public static string Action_ViewFunction_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Action_ViewFunction_Description", new object[] { p0 });
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0002D3F6 File Offset: 0x0002B5F6
		public static string Action_ViewError
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_ViewError");
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0002D402 File Offset: 0x0002B602
		public static string Action_ViewError_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Action_ViewError_Description", new object[] { p0 });
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0002D418 File Offset: 0x0002B618
		public static string JoinSide_Left
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinSide_Left");
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0002D424 File Offset: 0x0002B624
		public static string JoinSide_Right
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinSide_Right");
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0002D430 File Offset: 0x0002B630
		public static string JoinSide_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("JoinSide_Type");
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0002D43C File Offset: 0x0002B63C
		public static string Guid_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_Type");
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0002D448 File Offset: 0x0002B648
		public static string Guid_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_From");
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0002D454 File Offset: 0x0002B654
		public static string Guid_From_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_From_Description", new object[] { p0 });
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0002D46A File Offset: 0x0002B66A
		public static string Guid_From_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_From_Example1");
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0002D476 File Offset: 0x0002B676
		public static string Guid_From_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_From_Example2");
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0002D482 File Offset: 0x0002B682
		public static string Guid_From_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_From_Example3");
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0002D48E File Offset: 0x0002B68E
		public static string Guid_From_Example4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Guid_From_Example4");
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0002D49A File Offset: 0x0002B69A
		public static string Text_Select
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Select");
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0002D4A6 File Offset: 0x0002B6A6
		public static string Text_Select_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Select_Description", new object[] { p0, p1 });
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0002D4C0 File Offset: 0x0002B6C0
		public static string Text_Select_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Select_Example1");
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0002D4CC File Offset: 0x0002B6CC
		public static string OpenApi_Document
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("OpenApi_Document");
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0002D4D8 File Offset: 0x0002B6D8
		public static string OpenApi_Document_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("OpenApi_Document_Description");
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0002D4E4 File Offset: 0x0002B6E4
		public static string Environment_Libraries
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Environment_Libraries");
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		public static string Environment_Libraries_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Environment_Libraries_Description");
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0002D4FC File Offset: 0x0002B6FC
		public static string Environment_Configuration
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Environment_Configuration");
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0002D508 File Offset: 0x0002B708
		public static string Environment_Configuration_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Environment_Configuration_Description");
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0002D514 File Offset: 0x0002B714
		public static string Environment_FeatureSwitch
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Environment_FeatureSwitch");
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0002D520 File Offset: 0x0002B720
		public static string Environment_FeatureSwitch_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Environment_FeatureSwitch_Description");
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0002D52C File Offset: 0x0002B72C
		public static string Text_Reverse_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Reverse_Description", new object[] { p0 });
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0002D542 File Offset: 0x0002B742
		public static string Text_Reverse_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Text_Reverse_Example1");
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0002D54E File Offset: 0x0002B74E
		public static string List_ParallelInvoke_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ParallelInvoke_Description", new object[] { p0, p1 });
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0002D568 File Offset: 0x0002B768
		public static string List_ParallelInvoke_Example1(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("List_ParallelInvoke_Example1", new object[] { p0, p1 });
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0002D582 File Offset: 0x0002B782
		public static string TableAction_Tee
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_Tee");
			}
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0002D58E File Offset: 0x0002B78E
		public static string TableAction_Tee_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("TableAction_Tee_Description", new object[] { p0, p1 });
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
		public static string Table_FuzzyJoin
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyJoin");
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		public static string Table_FuzzyJoin_Description(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyJoin_Description", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		public static string Table_FuzzyJoin_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyJoin_Example1");
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0002D5EC File Offset: 0x0002B7EC
		public static string Table_FuzzyNestedJoin
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyNestedJoin");
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0002D5F8 File Offset: 0x0002B7F8
		public static string Table_FuzzyNestedJoin_Description(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyNestedJoin_Description", new object[] { p0, p1, p2, p3, p4, p5, p6 });
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0002D629 File Offset: 0x0002B829
		public static string Table_FuzzyNestedJoin_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FuzzyNestedJoin_Example1");
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0002D635 File Offset: 0x0002B835
		public static string Function_ScalarVector
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_ScalarVector");
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0002D641 File Offset: 0x0002B841
		public static string Function_ScalarVector_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Function_ScalarVector_Description", new object[] { p0, p1 });
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0002D65B File Offset: 0x0002B85B
		public static string Essbase_Cubes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Essbase_Cubes");
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0002D667 File Offset: 0x0002B867
		public static string Essbase_Cubes_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Essbase_Cubes_Description", new object[] { p0, p1 });
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0002D681 File Offset: 0x0002B881
		public static string ODataOmitValues_Nulls
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ODataOmitValues_Nulls");
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0002D68D File Offset: 0x0002B88D
		public static string ODataOmitValues_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("ODataOmitValues_Type");
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0002D699 File Offset: 0x0002B899
		public static string AccessControlEntry_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlEntry_Type");
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0002D6A5 File Offset: 0x0002B8A5
		public static string AccessControlEntry_Type_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlEntry_Type_Description");
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0002D6B1 File Offset: 0x0002B8B1
		public static string AccessControlEntry_ConditionContextType
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlEntry_ConditionContextType");
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0002D6BD File Offset: 0x0002B8BD
		public static string AccessControlEntry_ConditionToIdentities
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlEntry_ConditionToIdentities");
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0002D6C9 File Offset: 0x0002B8C9
		public static string AccessControlEntry_ConditionToIdentities_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlEntry_ConditionToIdentities_Description", new object[] { p0, p1 });
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0002D6E3 File Offset: 0x0002B8E3
		public static string AccessControlKind_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlKind_Type");
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0002D6EF File Offset: 0x0002B8EF
		public static string AccessControlKind_Allow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlKind_Allow");
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0002D6FB File Offset: 0x0002B8FB
		public static string AccessControlKind_Deny
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("AccessControlKind_Deny");
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0002D707 File Offset: 0x0002B907
		public static string IdentityProvider_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("IdentityProvider_Type");
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0002D713 File Offset: 0x0002B913
		public static string IdentityProvider_Default
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("IdentityProvider_Default");
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0002D71F File Offset: 0x0002B91F
		public static string Identity_Type
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Identity_Type");
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0002D72B File Offset: 0x0002B92B
		public static string Identity_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Identity_From");
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0002D737 File Offset: 0x0002B937
		public static string Identity_IsMemberOf
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Identity_IsMemberOf");
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0002D743 File Offset: 0x0002B943
		public static string Expression_Identifier
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Identifier");
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0002D74F File Offset: 0x0002B94F
		public static string Expression_Identifier_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Identifier_Description", new object[] { p0 });
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0002D765 File Offset: 0x0002B965
		public static string Expression_Identifier_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Identifier_Example1");
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0002D771 File Offset: 0x0002B971
		public static string Expression_Identifier_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Identifier_Example2");
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0002D77D File Offset: 0x0002B97D
		public static string Expression_Constant
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Constant");
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0002D789 File Offset: 0x0002B989
		public static string Expression_Constant_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Constant_Description");
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0002D795 File Offset: 0x0002B995
		public static string Expression_Constant_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Constant_Example1");
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0002D7A1 File Offset: 0x0002B9A1
		public static string Expression_Constant_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Constant_Example2");
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0002D7AD File Offset: 0x0002B9AD
		public static string Expression_Constant_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Constant_Example3");
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0002D7B9 File Offset: 0x0002B9B9
		public static string Expression_Evaluate
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Evaluate");
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0002D7C5 File Offset: 0x0002B9C5
		public static string Expression_Evaluate_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Evaluate_Description", new object[] { p0, p1 });
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0002D7DF File Offset: 0x0002B9DF
		public static string Expression_Evaluate_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Evaluate_Example1");
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0002D7EB File Offset: 0x0002B9EB
		public static string Expression_Evaluate_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Evaluate_Example2");
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0002D7F7 File Offset: 0x0002B9F7
		public static string Expression_Evaluate_Example3
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Expression_Evaluate_Example3");
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0002D803 File Offset: 0x0002BA03
		public static string Option_Timeout_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Timeout_Caption");
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0002D80F File Offset: 0x0002BA0F
		public static string Option_Timeout_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Timeout_Description");
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0002D81B File Offset: 0x0002BA1B
		public static string Cdm_Contents_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cdm_Contents_Description");
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0002D827 File Offset: 0x0002BA27
		public static string DeltaLake_Table_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DeltaLake_Table_Description");
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0002D833 File Offset: 0x0002BA33
		public static string Html_Table_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Html_Table_Description");
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0002D83F File Offset: 0x0002BA3F
		public static string Parquet_Document_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Parquet_Document_Description");
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x0002D84B File Offset: 0x0002BA4B
		public static string Parquet_Metadata_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Parquet_Metadata_Description");
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0002D857 File Offset: 0x0002BA57
		public static string Pdf_Tables_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Pdf_Tables_Description");
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0002D863 File Offset: 0x0002BA63
		public static string Web_BrowserContents_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_BrowserContents_Description");
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0002D86F File Offset: 0x0002BA6F
		public static string Cdpa_Database
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cdpa_Database");
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0002D87B File Offset: 0x0002BA7B
		public static string Cdpa_Database_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cdpa_Database_Description");
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0002D887 File Offset: 0x0002BA87
		public static string Table_ConformToPageReader
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ConformToPageReader");
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0002D893 File Offset: 0x0002BA93
		public static string Table_ConformToPageReader_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ConformToPageReader_Description");
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0002D89F File Offset: 0x0002BA9F
		public static string List_ConformToPageReader
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ConformToPageReader");
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0002D8AB File Offset: 0x0002BAAB
		public static string List_ConformToPageReader_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("List_ConformToPageReader_Description");
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0002D8B7 File Offset: 0x0002BAB7
		public static string Graph_Nodes
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Graph_Nodes");
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0002D8C3 File Offset: 0x0002BAC3
		public static string Graph_Nodes_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Graph_Nodes_Description");
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0002D8CF File Offset: 0x0002BACF
		public static string Value_Lineage
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Lineage");
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0002D8DB File Offset: 0x0002BADB
		public static string Value_Lineage_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Lineage_Description");
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0002D8E7 File Offset: 0x0002BAE7
		public static string Value_Traits
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Traits");
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0002D8F3 File Offset: 0x0002BAF3
		public static string Value_Traits_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Traits_Description");
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0002D8FF File Offset: 0x0002BAFF
		public static string Value_Expression
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Expression");
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0002D90B File Offset: 0x0002BB0B
		public static string Value_Expression_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Expression_Description");
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0002D917 File Offset: 0x0002BB17
		public static string Value_Optimize
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Optimize");
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0002D923 File Offset: 0x0002BB23
		public static string Value_Optimize_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Optimize_Description", new object[] { p0 });
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0002D939 File Offset: 0x0002BB39
		public static string Value_Alternates
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Alternates");
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0002D945 File Offset: 0x0002BB45
		public static string Value_Alternates_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Alternates_Description");
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0002D951 File Offset: 0x0002BB51
		public static string Compression_Brotli
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_Brotli");
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0002D95D File Offset: 0x0002BB5D
		public static string Compression_LZ4
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_LZ4");
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0002D969 File Offset: 0x0002BB69
		public static string Compression_None
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_None");
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0002D975 File Offset: 0x0002BB75
		public static string Compression_Snappy
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_Snappy");
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0002D981 File Offset: 0x0002BB81
		public static string Compression_Zstandard
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Compression_Zstandard");
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0002D98D File Offset: 0x0002BB8D
		public static string GeographyPoint_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GeographyPoint_From");
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0002D999 File Offset: 0x0002BB99
		public static string GeographyPoint_From_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GeographyPoint_From_Description");
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0002D9A5 File Offset: 0x0002BBA5
		public static string Geography_FromWellKnownText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geography_FromWellKnownText");
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0002D9B1 File Offset: 0x0002BBB1
		public static string Geography_FromWellKnownText_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geography_FromWellKnownText_Description");
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0002D9BD File Offset: 0x0002BBBD
		public static string Geography_ToWellKnownText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geography_ToWellKnownText");
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0002D9C9 File Offset: 0x0002BBC9
		public static string Geography_ToWellKnownText_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geography_ToWellKnownText_Description");
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0002D9D5 File Offset: 0x0002BBD5
		public static string GeometryPoint_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GeometryPoint_From");
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0002D9E1 File Offset: 0x0002BBE1
		public static string GeometryPoint_From_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("GeometryPoint_From_Description");
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0002D9ED File Offset: 0x0002BBED
		public static string Geometry_FromWellKnownText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geometry_FromWellKnownText");
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0002D9F9 File Offset: 0x0002BBF9
		public static string Geometry_FromWellKnownText_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geometry_FromWellKnownText_Description");
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0002DA05 File Offset: 0x0002BC05
		public static string Geometry_ToWellKnownText
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geometry_ToWellKnownText");
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0002DA11 File Offset: 0x0002BC11
		public static string Geometry_ToWellKnownText_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Geometry_ToWellKnownText_Description");
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0002DA1D File Offset: 0x0002BC1D
		public static string Option_Query_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Query_Web_Caption");
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0002DA29 File Offset: 0x0002BC29
		public static string Option_Query_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Query_Web_Description");
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0002DA35 File Offset: 0x0002BC35
		public static string Option_ApiKeyName_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ApiKeyName_Web_Caption");
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0002DA41 File Offset: 0x0002BC41
		public static string Option_ApiKeyName_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ApiKeyName_Web_Description");
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0002DA4D File Offset: 0x0002BC4D
		public static string Option_Content_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Content_Web_Caption");
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0002DA59 File Offset: 0x0002BC59
		public static string Option_Content_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Content_Web_Description");
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0002DA65 File Offset: 0x0002BC65
		public static string Option_Content_WebAction_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Content_WebAction_Caption");
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0002DA71 File Offset: 0x0002BC71
		public static string Option_Content_WebAction_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Content_WebAction_Description");
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0002DA7D File Offset: 0x0002BC7D
		public static string Option_Headers_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Headers_Web_Caption");
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0002DA89 File Offset: 0x0002BC89
		public static string Option_Headers_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Headers_Web_Description");
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0002DA95 File Offset: 0x0002BC95
		public static string Option_Timeout_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Timeout_Web_Caption");
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0002DAA1 File Offset: 0x0002BCA1
		public static string Option_Timeout_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_Timeout_Web_Description");
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0002DAAD File Offset: 0x0002BCAD
		public static string Option_ExcludedFromCacheKey_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ExcludedFromCacheKey_Web_Caption");
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0002DAB9 File Offset: 0x0002BCB9
		public static string Option_ExcludedFromCacheKey_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ExcludedFromCacheKey_Web_Description");
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0002DAC5 File Offset: 0x0002BCC5
		public static string Option_IsRetry_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IsRetry_Web_Caption");
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0002DAD1 File Offset: 0x0002BCD1
		public static string Option_IsRetry_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_IsRetry_Web_Description");
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0002DADD File Offset: 0x0002BCDD
		public static string Option_ManualStatusHandling_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ManualStatusHandling_Web_Caption");
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0002DAE9 File Offset: 0x0002BCE9
		public static string Option_ManualStatusHandling_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_ManualStatusHandling_Web_Description");
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0002DAF5 File Offset: 0x0002BCF5
		public static string Option_RelativePath_Web_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_RelativePath_Web_Caption");
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0002DB01 File Offset: 0x0002BD01
		public static string Option_RelativePath_Web_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_RelativePath_Web_Description");
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0002DB0D File Offset: 0x0002BD0D
		public static string Option_OmitSRID_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_OmitSRID_Caption");
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0002DB19 File Offset: 0x0002BD19
		public static string Option_OmitSRID_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_OmitSRID_Description");
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0002DB25 File Offset: 0x0002BD25
		public static string Option_UnsafeTypeConversions_Caption
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_UnsafeTypeConversions_Caption");
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0002DB31 File Offset: 0x0002BD31
		public static string Option_UnsafeTypeConversions_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Option_UnsafeTypeConversions_Description");
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0002DB3D File Offset: 0x0002BD3D
		public static string Binary_Range
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Range");
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0002DB49 File Offset: 0x0002BD49
		public static string Binary_Range_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Range_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0002DB63 File Offset: 0x0002BD63
		public static string Binary_Range_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Range_Example1");
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0002DB6F File Offset: 0x0002BD6F
		public static string Binary_Range_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Range_Example2");
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0002DB7B File Offset: 0x0002BD7B
		public static string Binary_Split
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Split");
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0002DB87 File Offset: 0x0002BD87
		public static string Binary_Split_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Binary_Split_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0002DBA1 File Offset: 0x0002BDA1
		public static string SqlDatabase_View
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SqlDatabase_View");
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0002DBAD File Offset: 0x0002BDAD
		public static string SqlDatabase_View_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SqlDatabase_View_Description");
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0002DBB9 File Offset: 0x0002BDB9
		public static string TimeZone_Current
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("TimeZone_Current");
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0002DBC5 File Offset: 0x0002BDC5
		public static string Table_FilterWithDataTable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FilterWithDataTable");
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0002DBD1 File Offset: 0x0002BDD1
		public static string Table_FilterWithDataTable_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_FilterWithDataTable_Description");
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0002DBDD File Offset: 0x0002BDDD
		public static string Value_As
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_As");
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0002DBE9 File Offset: 0x0002BDE9
		public static string Value_As_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_As_Description");
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0002DBF5 File Offset: 0x0002BDF5
		public static string Value_As_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_As_Example1");
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0002DC01 File Offset: 0x0002BE01
		public static string Value_As_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_As_Example2");
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0002DC0D File Offset: 0x0002BE0D
		public static string Value_Is
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Is");
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0002DC19 File Offset: 0x0002BE19
		public static string Value_Is_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Is_Description");
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0002DC25 File Offset: 0x0002BE25
		public static string Value_Is_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Is_Example1");
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0002DC31 File Offset: 0x0002BE31
		public static string Type_Is
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Is");
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0002DC3D File Offset: 0x0002BE3D
		public static string Type_Is_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Is_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0002DC57 File Offset: 0x0002BE57
		public static string Type_Is_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Is_Example1");
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0002DC63 File Offset: 0x0002BE63
		public static string Type_Is_Example2
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_Is_Example2");
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0002DC6F File Offset: 0x0002BE6F
		public static string Type_TableRow
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableRow");
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0002DC7B File Offset: 0x0002BE7B
		public static string Type_TableRow_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableRow_Description");
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0002DC87 File Offset: 0x0002BE87
		public static string Type_TableRow_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Type_TableRow_Example1");
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0002DC93 File Offset: 0x0002BE93
		public static string Value_Firewall
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Firewall");
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0002DC9F File Offset: 0x0002BE9F
		public static string Value_Firewall_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_Firewall_Description");
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0002DCAB File Offset: 0x0002BEAB
		public static string Progress_DataSourceProgress
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Progress_DataSourceProgress");
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0002DCB7 File Offset: 0x0002BEB7
		public static string Progress_DataSourceProgress_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Progress_DataSourceProgress_Description");
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0002DCC3 File Offset: 0x0002BEC3
		public static string DirectQueryCapabilities_From
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DirectQueryCapabilities_From");
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0002DCCF File Offset: 0x0002BECF
		public static string DirectQueryCapabilities_From_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("DirectQueryCapabilities_From_Description");
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0002DCDB File Offset: 0x0002BEDB
		public static string SqlExpression_ToExpression
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SqlExpression_ToExpression");
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0002DCE7 File Offset: 0x0002BEE7
		public static string SqlExpression_ToExpression_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("SqlExpression_ToExpression_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0002DD01 File Offset: 0x0002BF01
		public static string SqlExpression_SchemaFrom
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SqlExpression_SchemaFrom");
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0002DD0D File Offset: 0x0002BF0D
		public static string SqlExpression_SchemaFrom_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("SqlExpression_SchemaFrom_Description");
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0002DD19 File Offset: 0x0002BF19
		public static string Variable_Value
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Variable_Value");
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0002DD25 File Offset: 0x0002BF25
		public static string Variable_Value_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Variable_Value_Description");
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0002DD31 File Offset: 0x0002BF31
		public static string Cube_ReplaceDimensions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_ReplaceDimensions");
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0002DD3D File Offset: 0x0002BF3D
		public static string Cube_ReplaceDimensions_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Cube_ReplaceDimensions_Description");
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0002DD49 File Offset: 0x0002BF49
		public static string Excel_ShapeTable
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_ShapeTable");
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0002DD55 File Offset: 0x0002BF55
		public static string Excel_ShapeTable_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Excel_ShapeTable_Description");
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0002DD61 File Offset: 0x0002BF61
		public static string Web_DefaultProxy
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Web_DefaultProxy");
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0002DD6D File Offset: 0x0002BF6D
		public static string Web_DefaultProxy_Description(object p0)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Web_DefaultProxy_Description", new object[] { p0 });
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0002DD83 File Offset: 0x0002BF83
		public static string Table_Keys
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Keys");
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0002DD8F File Offset: 0x0002BF8F
		public static string Table_Keys_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_Keys_Example1");
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0002DD9B File Offset: 0x0002BF9B
		public static string Table_ReplaceKeys
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceKeys");
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0002DDA7 File Offset: 0x0002BFA7
		public static string Table_ReplaceKeys_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceKeys_Example1");
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0002DDB3 File Offset: 0x0002BFB3
		public static string Value_ReplaceType
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ReplaceType");
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0002DDBF File Offset: 0x0002BFBF
		public static string Value_ReplaceType_Description(object p0, object p1)
		{
			return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ReplaceType_Description", new object[] { p0, p1 });
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0002DDD9 File Offset: 0x0002BFD9
		public static string Value_ReplaceType_Example1
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Value_ReplaceType_Example1");
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0002DDE5 File Offset: 0x0002BFE5
		public static string Table_ReplaceRelationshipIdentity
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_ReplaceRelationshipIdentity");
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0002DDF1 File Offset: 0x0002BFF1
		public static string Table_WithErrorContext
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_WithErrorContext");
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0002DDFD File Offset: 0x0002BFFD
		public static string Table_WithErrorContext_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Table_WithErrorContext_Description");
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0002DE09 File Offset: 0x0002C009
		public static string Action_WithErrorContext
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_WithErrorContext");
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0002DE15 File Offset: 0x0002C015
		public static string Action_WithErrorContext_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Action_WithErrorContext_Description");
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0002DE21 File Offset: 0x0002C021
		public static string Function_InvokeWithErrorContext
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_InvokeWithErrorContext");
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0002DE2D File Offset: 0x0002C02D
		public static string Function_InvokeWithErrorContext_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Function_InvokeWithErrorContext_Description");
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0002DE39 File Offset: 0x0002C039
		public static string Module_Versions
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Module_Versions");
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0002DE45 File Offset: 0x0002C045
		public static string Module_Versions_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("Module_Versions_Description");
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0002DE51 File Offset: 0x0002C051
		public static string CacheManager_Cache
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CacheManager_Cache");
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0002DE5D File Offset: 0x0002C05D
		public static string CacheManager_Cache_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CacheManager_Cache_Description");
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0002DE69 File Offset: 0x0002C069
		public static string CacheManager_Caches
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CacheManager_Caches");
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0002DE75 File Offset: 0x0002C075
		public static string CacheManager_Caches_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CacheManager_Caches_Description");
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0002DE81 File Offset: 0x0002C081
		public static string CacheManager_InvokeWithCaches
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CacheManager_InvokeWithCaches");
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0002DE8D File Offset: 0x0002C08D
		public static string CacheManager_InvokeWithCaches_Description
		{
			get
			{
				return FunctionDescriptionStrings.ResourceLoader.GetString("CacheManager_InvokeWithCaches_Description");
			}
		}

		// Token: 0x02000233 RID: 563
		private class ResourceLoader
		{
			// Token: 0x06001569 RID: 5481 RVA: 0x0002DE99 File Offset: 0x0002C099
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Engine1.FunctionDescriptionStrings", base.GetType().Assembly);
			}

			// Token: 0x0600156A RID: 5482 RVA: 0x0002DEBC File Offset: 0x0002C0BC
			private static FunctionDescriptionStrings.ResourceLoader GetLoader()
			{
				if (FunctionDescriptionStrings.ResourceLoader.instance == null)
				{
					FunctionDescriptionStrings.ResourceLoader resourceLoader = new FunctionDescriptionStrings.ResourceLoader();
					Interlocked.CompareExchange<FunctionDescriptionStrings.ResourceLoader>(ref FunctionDescriptionStrings.ResourceLoader.instance, resourceLoader, null);
				}
				return FunctionDescriptionStrings.ResourceLoader.instance;
			}

			// Token: 0x17000A68 RID: 2664
			// (get) Token: 0x0600156B RID: 5483 RVA: 0x000020FA File Offset: 0x000002FA
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000A69 RID: 2665
			// (get) Token: 0x0600156C RID: 5484 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
			public static ResourceManager Resources
			{
				get
				{
					return FunctionDescriptionStrings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600156D RID: 5485 RVA: 0x0002DEF4 File Offset: 0x0002C0F4
			public static string GetString(string name, params object[] args)
			{
				FunctionDescriptionStrings.ResourceLoader loader = FunctionDescriptionStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, FunctionDescriptionStrings.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x0600156E RID: 5486 RVA: 0x0002DF34 File Offset: 0x0002C134
			public static string GetString(string name)
			{
				FunctionDescriptionStrings.ResourceLoader loader = FunctionDescriptionStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, FunctionDescriptionStrings.ResourceLoader.Culture);
			}

			// Token: 0x0600156F RID: 5487 RVA: 0x0002DF60 File Offset: 0x0002C160
			public static object GetObject(string name)
			{
				FunctionDescriptionStrings.ResourceLoader loader = FunctionDescriptionStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, FunctionDescriptionStrings.ResourceLoader.Culture);
			}

			// Token: 0x06001570 RID: 5488 RVA: 0x0002DF8C File Offset: 0x0002C18C
			public static T GetObject<T>(string name) where T : class
			{
				FunctionDescriptionStrings.ResourceLoader loader = FunctionDescriptionStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, FunctionDescriptionStrings.ResourceLoader.Culture));
			}

			// Token: 0x04000696 RID: 1686
			private static FunctionDescriptionStrings.ResourceLoader instance;

			// Token: 0x04000697 RID: 1687
			private ResourceManager resources;
		}
	}
}
