using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005EF RID: 1519
	internal class OdbcDirectQueryCapabilities
	{
		// Token: 0x06002FF2 RID: 12274 RVA: 0x00090B88 File Offset: 0x0008ED88
		public static List<Value> From(OdbcDataSourceInfo info)
		{
			List<Value> list = new List<Value>();
			list.Add(CapabilityModule.NewCapability("Core", Value.Null));
			list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(info.MaxParameters)));
			if (info.SupportsTopOrLimit)
			{
				list.Add(CapabilityModule.NewCapability("Table.FirstN", Value.Null));
			}
			list.Add(CapabilityModule.NewCapability("Table.Sort", Value.Null));
			list.Add(CapabilityModule.NewCapability("Table.RowCount", Value.Null));
			list.Add(CapabilityModule.NewCapability("List.Average", Value.Null));
			list.Add(CapabilityModule.NewCapability("List.Max", Value.Null));
			list.Add(CapabilityModule.NewCapability("List.Min", Value.Null));
			list.Add(CapabilityModule.NewCapability("List.Sum", Value.Null));
			OdbcDirectQueryCapabilities.AddCapabilities<Odbc32.SQL_FN_STR>(list, info.SupportedStringFunctions, OdbcDirectQueryCapabilities.StringFunctions);
			OdbcDirectQueryCapabilities.AddCapabilities<Odbc32.SQL_FUN_NUM>(list, info.SupportedNumericFunctions, OdbcDirectQueryCapabilities.NumericFunctions);
			OdbcDirectQueryCapabilities.AddCapabilities<Odbc32.SQL_FN_TD>(list, info.SupportedTimeDateFunctions, OdbcDirectQueryCapabilities.DatePartFunctions);
			OdbcDirectQueryCapabilities.AddTimeSecondCapability(list, info);
			OdbcDirectQueryCapabilities.AddTimeDateStartOfCapabilities(list, info);
			OdbcDirectQueryCapabilities.AddTimeDateEndOfCapabilities(list, info);
			OdbcDirectQueryCapabilities.AddDateAddCapabilities(list, info);
			OdbcDirectQueryCapabilities.AddDurationPartCapabilities(list, info.SupportedNumericFunctions);
			list.Add(CapabilityModule.NewCapability("Duration.From", Value.Null));
			return list;
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00090CD8 File Offset: 0x0008EED8
		private static void AddCapabilities<T>(List<Value> capabilities, T capabilityMask, Dictionary<T, string> map)
		{
			int num = (int)((object)capabilityMask);
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (KeyValuePair<T, string> keyValuePair in map)
			{
				int num2 = (int)((object)keyValuePair.Key);
				if ((num & num2) != 0 && hashSet.Add(keyValuePair.Value))
				{
					capabilities.Add(CapabilityModule.NewCapability(keyValuePair.Value, Value.Null));
				}
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00090D78 File Offset: 0x0008EF78
		private static void AddTimeSecondCapability(List<Value> capabilities, OdbcDataSourceInfo info)
		{
			if (info.FractionalSecondsPerSecond != null && info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPADD) && info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPDIFF) && info.SupportsTimedateAddInterval(Odbc32.SQL_TSI.SQL_TSI_MINUTE) && info.SupportsTimedateDiffInterval(Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND) && info.SupportsTimedateDiffInterval(Odbc32.SQL_TSI.SQL_TSI_MINUTE))
			{
				capabilities.Add(CapabilityModule.NewCapability("Time.Second", Value.Null));
			}
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00090DDF File Offset: 0x0008EFDF
		private static void AddTimeDateStartOfCapabilities(List<Value> capabilities, OdbcDataSourceInfo info)
		{
			if (!info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPADD) || !info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPDIFF))
			{
				return;
			}
			OdbcDirectQueryCapabilities.AddCapabilities<Odbc32.SQL_TSI>(capabilities, info.SupportedTimeDateAddIntervals, OdbcDirectQueryCapabilities.startOfFunctions);
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00090E10 File Offset: 0x0008F010
		private static void AddTimeDateEndOfCapabilities(List<Value> capabilities, OdbcDataSourceInfo info)
		{
			if (!info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPADD) || !info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPDIFF) || !info.SupportsTimedateAddInterval(Odbc32.SQL_TSI.SQL_TSI_FRAC_SECOND) || !info.SupportsTimedateAddInterval(Odbc32.SQL_TSI.SQL_TSI_DAY))
			{
				return;
			}
			Odbc32.SQL_TSI sql_TSI = info.SupportedTimeDateAddIntervals & info.SupportedTimeDateDiffIntervals;
			OdbcDirectQueryCapabilities.AddCapabilities<Odbc32.SQL_TSI>(capabilities, sql_TSI, OdbcDirectQueryCapabilities.endOfFunctions);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00090E65 File Offset: 0x0008F065
		private static void AddDateAddCapabilities(List<Value> capabilities, OdbcDataSourceInfo info)
		{
			if (!info.Supports(Odbc32.SQL_FN_TD.SQL_FN_TD_TIMESTAMPADD))
			{
				return;
			}
			OdbcDirectQueryCapabilities.AddCapabilities<Odbc32.SQL_TSI>(capabilities, info.SupportedTimeDateAddIntervals, OdbcDirectQueryCapabilities.dateAddFunctions);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00090E88 File Offset: 0x0008F088
		private static void AddDurationPartCapabilities(List<Value> capabilities, Odbc32.SQL_FUN_NUM numericFunctions)
		{
			if ((Odbc32.SQL_FUN_NUM.SQL_FN_NUM_MOD & numericFunctions) == Odbc32.SQL_FUN_NUM.SQL_FN_NUM_MOD)
			{
				capabilities.Add(CapabilityModule.NewCapability("Duration.Days", Value.Null));
				capabilities.Add(CapabilityModule.NewCapability("Duration.Hours", Value.Null));
				capabilities.Add(CapabilityModule.NewCapability("Duration.Minutes", Value.Null));
				capabilities.Add(CapabilityModule.NewCapability("Duration.Seconds", Value.Null));
			}
			capabilities.Add(CapabilityModule.NewCapability("Duration.TotalDays", Value.Null));
			capabilities.Add(CapabilityModule.NewCapability("Duration.TotalHours", Value.Null));
			capabilities.Add(CapabilityModule.NewCapability("Duration.TotalMinutes", Value.Null));
			capabilities.Add(CapabilityModule.NewCapability("Duration.TotalSeconds", Value.Null));
		}

		// Token: 0x0400151A RID: 5402
		private static readonly Dictionary<Odbc32.SQL_FN_STR, string> StringFunctions = new Dictionary<Odbc32.SQL_FN_STR, string>
		{
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_LEFT,
				"Text.Start"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_LTRIM,
				"Text.TrimStart"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_LENGTH,
				"Text.Length"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_LCASE,
				"Text.Lower"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_LOCATE_2,
				"Text.PositionOf"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_LOCATE,
				"Text.PositionOf"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_REPLACE,
				"Text.Replace"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_RIGHT,
				"Text.End"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_RTRIM,
				"Text.TrimEnd"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_SUBSTRING,
				"Text.Middle"
			},
			{
				Odbc32.SQL_FN_STR.SQL_FN_STR_UCASE,
				"Text.Upper"
			}
		};

		// Token: 0x0400151B RID: 5403
		private static readonly Dictionary<Odbc32.SQL_FUN_NUM, string> NumericFunctions = new Dictionary<Odbc32.SQL_FUN_NUM, string>
		{
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ABS,
				"Number.Abs"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ACOS,
				"Number.Acos"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ASIN,
				"Number.Asin"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ATAN,
				"Number.Atan"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ATAN2,
				"Number.Atan2"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_CEILING,
				"Number.RoundUp"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_COS,
				"Number.Cos"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_EXP,
				"Number.Exp"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_FLOOR,
				"Number.RoundDown"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_LOG,
				"Number.Log"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_MOD,
				"Number.Mod"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SIGN,
				"Number.Sign"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SIN,
				"Number.Sin"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_SQRT,
				"Number.Sqrt"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_TAN,
				"Number.Tan"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_LOG10,
				"Number.Log10"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_POWER,
				"Number.Power"
			},
			{
				Odbc32.SQL_FUN_NUM.SQL_FN_NUM_ROUND,
				"Number.Round"
			}
		};

		// Token: 0x0400151C RID: 5404
		private static readonly Dictionary<Odbc32.SQL_FN_TD, string> DatePartFunctions = new Dictionary<Odbc32.SQL_FN_TD, string>
		{
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_YEAR,
				"Date.Year"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_MONTH,
				"Date.Month"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_QUARTER,
				"Date.QuarterOfYear"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_WEEK,
				"Date.WeekOfYear"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_DAYOFYEAR,
				"Date.DayOfYear"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_DAYOFMONTH,
				"Date.Day"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_DAYOFWEEK,
				"Date.DayOfWeek"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_HOUR,
				"Time.Hour"
			},
			{
				Odbc32.SQL_FN_TD.SQL_FN_TD_MINUTE,
				"Time.Minute"
			}
		};

		// Token: 0x0400151D RID: 5405
		private static readonly Dictionary<Odbc32.SQL_TSI, string> startOfFunctions = new Dictionary<Odbc32.SQL_TSI, string>
		{
			{
				Odbc32.SQL_TSI.SQL_TSI_YEAR,
				"Date.StartOfYear"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_MONTH,
				"Date.StartOfMonth"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_QUARTER,
				"Date.StartOfQuarter"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_WEEK,
				"Date.StartOfWeek"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_DAY,
				"Date.StartOfDay"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_HOUR,
				"Time.StartOfHour"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_MINUTE,
				"Time.StartOfMinute"
			}
		};

		// Token: 0x0400151E RID: 5406
		private static readonly Dictionary<Odbc32.SQL_TSI, string> endOfFunctions = new Dictionary<Odbc32.SQL_TSI, string>
		{
			{
				Odbc32.SQL_TSI.SQL_TSI_YEAR,
				"Date.EndOfYear"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_MONTH,
				"Date.EndOfMonth"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_QUARTER,
				"Date.EndOfQuarter"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_WEEK,
				"Date.EndOfWeek"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_DAY,
				"Date.EndOfDay"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_HOUR,
				"Time.EndOfHour"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_MINUTE,
				"Time.EndOfMinute"
			}
		};

		// Token: 0x0400151F RID: 5407
		private static readonly Dictionary<Odbc32.SQL_TSI, string> dateAddFunctions = new Dictionary<Odbc32.SQL_TSI, string>
		{
			{
				Odbc32.SQL_TSI.SQL_TSI_YEAR,
				"Date.AddYears"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_MONTH,
				"Date.AddMonths"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_QUARTER,
				"Date.AddQuarters"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_WEEK,
				"Date.AddWeeks"
			},
			{
				Odbc32.SQL_TSI.SQL_TSI_DAY,
				"Date.AddDays"
			}
		};
	}
}
