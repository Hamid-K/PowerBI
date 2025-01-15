using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200136E RID: 4974
	public static class Library
	{
		// Token: 0x04004738 RID: 18232
		private static readonly char[] whitespaceChars = new char[]
		{
			' ', '\u00a0', '\u1680', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006',
			'\u2007', '\u2008', '\u2009', '\u200a', '\u202f', '\u205f', '\u3000', '\u2028', '\u2029', '\t',
			'\n', '\v', '\f', '\r', '\u0085'
		};

		// Token: 0x0200136F RID: 4975
		public class LibraryModule : Module
		{
			// Token: 0x060082E7 RID: 33511 RVA: 0x001BBE74 File Offset: 0x001BA074
			private static string GetKey(Library.LibraryModule.Exports export)
			{
				switch (export)
				{
				case Library.LibraryModule.Exports.Any_Type:
					return "Any.Type";
				case Library.LibraryModule.Exports.None_Type:
					return "None.Type";
				case Library.LibraryModule.Exports.Expression_Constant:
					return "Expression.Constant";
				case Library.LibraryModule.Exports.Expression_Evaluate:
					return "Expression.Evaluate";
				case Library.LibraryModule.Exports.Expression_Identifier:
					return "Expression.Identifier";
				case Library.LibraryModule.Exports.TextEncoding_Type:
					return TextEncoding.Type.GetName();
				case Library.LibraryModule.Exports.TextEncoding_Utf8:
					return TextEncoding.Utf8.GetName();
				case Library.LibraryModule.Exports.TextEncoding_Utf16:
					return TextEncoding.Utf16.GetName();
				case Library.LibraryModule.Exports.TextEncoding_Ascii:
					return TextEncoding.Ascii.GetName();
				case Library.LibraryModule.Exports.TextEncoding_Unicode:
					return TextEncoding.Unicode.GetName();
				case Library.LibraryModule.Exports.TextEncoding_BigEndianUnicode:
					return TextEncoding.BigEndianUnicode.GetName();
				case Library.LibraryModule.Exports.TextEncoding_Windows:
					return TextEncoding.Windows.GetName();
				case Library.LibraryModule.Exports.Culture_Current:
					return "Culture.Current";
				case Library.LibraryModule.Exports.Day_Type:
					return Library.Day.Type.GetName();
				case Library.LibraryModule.Exports.Day_Sunday:
					return Library.Day.Sunday.GetName();
				case Library.LibraryModule.Exports.Day_Monday:
					return Library.Day.Monday.GetName();
				case Library.LibraryModule.Exports.Day_Tuesday:
					return Library.Day.Tuesday.GetName();
				case Library.LibraryModule.Exports.Day_Wednesday:
					return Library.Day.Wednesday.GetName();
				case Library.LibraryModule.Exports.Day_Thursday:
					return Library.Day.Thursday.GetName();
				case Library.LibraryModule.Exports.Day_Friday:
					return Library.Day.Friday.GetName();
				case Library.LibraryModule.Exports.Day_Saturday:
					return Library.Day.Saturday.GetName();
				case Library.LibraryModule.Exports.Duration_Type:
					return "Duration.Type";
				case Library.LibraryModule.Exports.Duration_FromText:
					return "Duration.FromText";
				case Library.LibraryModule.Exports.Duration_From:
					return "Duration.From";
				case Library.LibraryModule.Exports.Duration_ToText:
					return "Duration.ToText";
				case Library.LibraryModule.Exports.Duration_ToRecord:
					return "Duration.ToRecord";
				case Library.LibraryModule.Exports.Duration_Days:
					return "Duration.Days";
				case Library.LibraryModule.Exports.Duration_Hours:
					return "Duration.Hours";
				case Library.LibraryModule.Exports.Duration_Minutes:
					return "Duration.Minutes";
				case Library.LibraryModule.Exports.Duration_Seconds:
					return "Duration.Seconds";
				case Library.LibraryModule.Exports.Duration_TotalDays:
					return "Duration.TotalDays";
				case Library.LibraryModule.Exports.Duration_TotalHours:
					return "Duration.TotalHours";
				case Library.LibraryModule.Exports.Duration_TotalMinutes:
					return "Duration.TotalMinutes";
				case Library.LibraryModule.Exports.Duration_TotalSeconds:
					return "Duration.TotalSeconds";
				case Library.LibraryModule.Exports.JoinKind_Type:
					return Library.JoinKind.Type.GetName();
				case Library.LibraryModule.Exports.JoinKind_Inner:
					return Library.JoinKind.Inner.GetName();
				case Library.LibraryModule.Exports.JoinKind_LeftOuter:
					return Library.JoinKind.LeftOuter.GetName();
				case Library.LibraryModule.Exports.JoinKind_RightOuter:
					return Library.JoinKind.RightOuter.GetName();
				case Library.LibraryModule.Exports.JoinKind_FullOuter:
					return Library.JoinKind.FullOuter.GetName();
				case Library.LibraryModule.Exports.JoinKind_LeftAnti:
					return Library.JoinKind.LeftAnti.GetName();
				case Library.LibraryModule.Exports.JoinKind_RightAnti:
					return Library.JoinKind.RightAnti.GetName();
				case Library.LibraryModule.Exports.JoinKind_LeftSemi:
					return Library.JoinKind.LeftSemi.GetName();
				case Library.LibraryModule.Exports.JoinKind_RightSemi:
					return Library.JoinKind.RightSemi.GetName();
				case Library.LibraryModule.Exports.MissingField_Type:
					return Library.MissingField.Type.GetName();
				case Library.LibraryModule.Exports.MissingField_Error:
					return Library.MissingField.Error.GetName();
				case Library.LibraryModule.Exports.MissingField_Ignore:
					return Library.MissingField.Ignore.GetName();
				case Library.LibraryModule.Exports.MissingField_UseNull:
					return Library.MissingField.UseNull.GetName();
				case Library.LibraryModule.Exports.GroupKind_Type:
					return Library.GroupKind.Type.GetName();
				case Library.LibraryModule.Exports.GroupKind_Global:
					return Library.GroupKind.Global.GetName();
				case Library.LibraryModule.Exports.GroupKind_Local:
					return Library.GroupKind.Local.GetName();
				case Library.LibraryModule.Exports.Number_E:
					return "Number.E";
				case Library.LibraryModule.Exports.Number_PI:
					return "Number.PI";
				case Library.LibraryModule.Exports.RoundingMode_Type:
					return Library.RoundingMode.Type.GetName();
				case Library.LibraryModule.Exports.RoundingMode_Up:
					return Library.RoundingMode.Up.GetName();
				case Library.LibraryModule.Exports.RoundingMode_Down:
					return Library.RoundingMode.Down.GetName();
				case Library.LibraryModule.Exports.RoundingMode_AwayFromZero:
					return Library.RoundingMode.AwayFromZero.GetName();
				case Library.LibraryModule.Exports.RoundingMode_TowardZero:
					return Library.RoundingMode.TowardZero.GetName();
				case Library.LibraryModule.Exports.RoundingMode_ToEven:
					return Library.RoundingMode.ToEven.GetName();
				case Library.LibraryModule.Exports.Record_Type:
					return "Record.Type";
				case Library.LibraryModule.Exports.Record_AddField:
					return "Record.AddField";
				case Library.LibraryModule.Exports.Record_Field:
					return "Record.Field";
				case Library.LibraryModule.Exports.Record_FieldCount:
					return "Record.FieldCount";
				case Library.LibraryModule.Exports.Record_FieldNames:
					return "Record.FieldNames";
				case Library.LibraryModule.Exports.Record_FieldOrDefault:
					return "Record.FieldOrDefault";
				case Library.LibraryModule.Exports.Record_FieldValues:
					return "Record.FieldValues";
				case Library.LibraryModule.Exports.Record_FromTable:
					return "Record.FromTable";
				case Library.LibraryModule.Exports.Record_HasFields:
					return "Record.HasFields";
				case Library.LibraryModule.Exports.Record_RemoveFields:
					return "Record.RemoveFields";
				case Library.LibraryModule.Exports.Record_RenameFields:
					return "Record.RenameFields";
				case Library.LibraryModule.Exports.Record_ReorderFields:
					return "Record.ReorderFields";
				case Library.LibraryModule.Exports.Record_SelectFields:
					return "Record.SelectFields";
				case Library.LibraryModule.Exports.Record_ToTable:
					return "Record.ToTable";
				case Library.LibraryModule.Exports.Record_TransformFields:
					return "Record.TransformFields";
				case Library.LibraryModule.Exports.Record_Combine:
					return "Record.Combine";
				case Library.LibraryModule.Exports.Record_FromList:
					return "Record.FromList";
				case Library.LibraryModule.Exports.Record_ToList:
					return "Record.ToList";
				case Library.LibraryModule.Exports.Precision_Type:
					return Library.PrecisionEnum.Type.GetName();
				case Library.LibraryModule.Exports.Precision_Double:
					return Library.PrecisionEnum.Double.GetName();
				case Library.LibraryModule.Exports.Precision_Decimal:
					return Library.PrecisionEnum.Decimal.GetName();
				case Library.LibraryModule.Exports.Number_Type:
					return "Number.Type";
				case Library.LibraryModule.Exports.Number_From:
					return "Number.From";
				case Library.LibraryModule.Exports.Number_FromText:
					return "Number.FromText";
				case Library.LibraryModule.Exports.Number_ToText:
					return "Number.ToText";
				case Library.LibraryModule.Exports.Number_IsNaN:
					return "Number.IsNaN";
				case Library.LibraryModule.Exports.Number_NaN:
					return "Number.NaN";
				case Library.LibraryModule.Exports.Number_NegativeInfinity:
					return "Number.NegativeInfinity";
				case Library.LibraryModule.Exports.Number_PositiveInfinity:
					return "Number.PositiveInfinity";
				case Library.LibraryModule.Exports.Number_Epsilon:
					return "Number.Epsilon";
				case Library.LibraryModule.Exports.Number_BitwiseNot:
					return "Number.BitwiseNot";
				case Library.LibraryModule.Exports.Number_BitwiseOr:
					return "Number.BitwiseOr";
				case Library.LibraryModule.Exports.Number_BitwiseAnd:
					return "Number.BitwiseAnd";
				case Library.LibraryModule.Exports.Number_BitwiseXor:
					return "Number.BitwiseXor";
				case Library.LibraryModule.Exports.Number_BitwiseShiftLeft:
					return "Number.BitwiseShiftLeft";
				case Library.LibraryModule.Exports.Number_BitwiseShiftRight:
					return "Number.BitwiseShiftRight";
				case Library.LibraryModule.Exports.BinaryEncoding_Type:
					return Library.BinaryEncoding.Type.GetName();
				case Library.LibraryModule.Exports.BinaryEncoding_Hex:
					return Library.BinaryEncoding.Hex.GetName();
				case Library.LibraryModule.Exports.BinaryEncoding_Base64:
					return Library.BinaryEncoding.Base64.GetName();
				case Library.LibraryModule.Exports.Binary_ApproximateLength:
					return "Binary.ApproximateLength";
				case Library.LibraryModule.Exports.Binary_Type:
					return "Binary.Type";
				case Library.LibraryModule.Exports.Binary_ToText:
					return "Binary.ToText";
				case Library.LibraryModule.Exports.Binary_From:
					return "Binary.From";
				case Library.LibraryModule.Exports.Binary_FromText:
					return "Binary.FromText";
				case Library.LibraryModule.Exports.Binary_ToList:
					return "Binary.ToList";
				case Library.LibraryModule.Exports.Binary_FromList:
					return "Binary.FromList";
				case Library.LibraryModule.Exports.Binary_Combine:
					return "Binary.Combine";
				case Library.LibraryModule.Exports.Binary_Length:
					return "Binary.Length";
				case Library.LibraryModule.Exports.Binary_Buffer:
					return "Binary.Buffer";
				case Library.LibraryModule.Exports.Binary_Compress:
					return "Binary.Compress";
				case Library.LibraryModule.Exports.Binary_Decompress:
					return "Binary.Decompress";
				case Library.LibraryModule.Exports.Binary_InferContentType:
					return "Binary.InferContentType";
				case Library.LibraryModule.Exports.Binary_Range:
					return "Binary.Range";
				case Library.LibraryModule.Exports.Binary_Split:
					return "Binary.Split";
				case Library.LibraryModule.Exports.Compression_Type:
					return Library.CompressionType.Type.GetName();
				case Library.LibraryModule.Exports.Compression_None:
					return Library.CompressionType.None.GetName();
				case Library.LibraryModule.Exports.Compression_GZip:
					return Library.CompressionType.GZip.GetName();
				case Library.LibraryModule.Exports.Compression_Deflate:
					return Library.CompressionType.Deflate.GetName();
				case Library.LibraryModule.Exports.Compression_Snappy:
					return Library.CompressionType.Snappy.GetName();
				case Library.LibraryModule.Exports.Compression_Brotli:
					return Library.CompressionType.Brotli.GetName();
				case Library.LibraryModule.Exports.Compression_LZ4:
					return Library.CompressionType.LZ4.GetName();
				case Library.LibraryModule.Exports.Compression_Zstandard:
					return Library.CompressionType.Zstandard.GetName();
				case Library.LibraryModule.Exports.Byte_Type:
					return "Byte.Type";
				case Library.LibraryModule.Exports.Character_Type:
					return "Character.Type";
				case Library.LibraryModule.Exports.Character_FromNumber:
					return "Character.FromNumber";
				case Library.LibraryModule.Exports.Character_ToNumber:
					return "Character.ToNumber";
				case Library.LibraryModule.Exports.Text_Type:
					return "Text.Type";
				case Library.LibraryModule.Exports.Text_At:
					return "Text.At";
				case Library.LibraryModule.Exports.Text_From:
					return "Text.From";
				case Library.LibraryModule.Exports.Text_Length:
					return "Text.Length";
				case Library.LibraryModule.Exports.Text_Range:
					return "Text.Range";
				case Library.LibraryModule.Exports.Text_Middle:
					return "Text.Middle";
				case Library.LibraryModule.Exports.Text_Start:
					return "Text.Start";
				case Library.LibraryModule.Exports.Text_End:
					return "Text.End";
				case Library.LibraryModule.Exports.Text_StartsWith:
					return "Text.StartsWith";
				case Library.LibraryModule.Exports.Text_EndsWith:
					return "Text.EndsWith";
				case Library.LibraryModule.Exports.Text_Contains:
					return "Text.Contains";
				case Library.LibraryModule.Exports.Text_Clean:
					return "Text.Clean";
				case Library.LibraryModule.Exports.Text_PositionOf:
					return "Text.PositionOf";
				case Library.LibraryModule.Exports.Text_PositionOfAny:
					return "Text.PositionOfAny";
				case Library.LibraryModule.Exports.Text_Lower:
					return "Text.Lower";
				case Library.LibraryModule.Exports.Text_Upper:
					return "Text.Upper";
				case Library.LibraryModule.Exports.Text_Proper:
					return "Text.Proper";
				case Library.LibraryModule.Exports.Text_Split:
					return "Text.Split";
				case Library.LibraryModule.Exports.Text_SplitAny:
					return "Text.SplitAny";
				case Library.LibraryModule.Exports.Text_Combine:
					return "Text.Combine";
				case Library.LibraryModule.Exports.Text_Repeat:
					return "Text.Repeat";
				case Library.LibraryModule.Exports.Text_Replace:
					return "Text.Replace";
				case Library.LibraryModule.Exports.Text_ReplaceRange:
					return "Text.ReplaceRange";
				case Library.LibraryModule.Exports.Text_Insert:
					return "Text.Insert";
				case Library.LibraryModule.Exports.Text_Remove:
					return "Text.Remove";
				case Library.LibraryModule.Exports.Text_RemoveRange:
					return "Text.RemoveRange";
				case Library.LibraryModule.Exports.Text_Reverse:
					return "Text.Reverse";
				case Library.LibraryModule.Exports.Text_Select:
					return "Text.Select";
				case Library.LibraryModule.Exports.Text_Trim:
					return "Text.Trim";
				case Library.LibraryModule.Exports.Text_TrimStart:
					return "Text.TrimStart";
				case Library.LibraryModule.Exports.Text_TrimEnd:
					return "Text.TrimEnd";
				case Library.LibraryModule.Exports.Text_PadStart:
					return "Text.PadStart";
				case Library.LibraryModule.Exports.Text_PadEnd:
					return "Text.PadEnd";
				case Library.LibraryModule.Exports.Text_ToBinary:
					return "Text.ToBinary";
				case Library.LibraryModule.Exports.Text_ToList:
					return "Text.ToList";
				case Library.LibraryModule.Exports.Text_FromBinary:
					return "Text.FromBinary";
				case Library.LibraryModule.Exports.Text_NewGuid:
					return "Text.NewGuid";
				case Library.LibraryModule.Exports.Text_InferNumberType:
					return "Text.InferNumberType";
				case Library.LibraryModule.Exports.Text_Format:
					return "Text.Format";
				case Library.LibraryModule.Exports.Comparer_FromCulture:
					return "Comparer.FromCulture";
				case Library.LibraryModule.Exports.Comparer_Ordinal:
					return "Comparer.Ordinal";
				case Library.LibraryModule.Exports.Comparer_OrdinalIgnoreCase:
					return "Comparer.OrdinalIgnoreCase";
				case Library.LibraryModule.Exports.Comparer_Equals:
					return "Comparer.Equals";
				case Library.LibraryModule.Exports.Date_FromText:
					return "Date.FromText";
				case Library.LibraryModule.Exports.Date_From:
					return "Date.From";
				case Library.LibraryModule.Exports.Date_ToText:
					return "Date.ToText";
				case Library.LibraryModule.Exports.Date_ToRecord:
					return "Date.ToRecord";
				case Library.LibraryModule.Exports.Date_Year:
					return "Date.Year";
				case Library.LibraryModule.Exports.Date_Month:
					return "Date.Month";
				case Library.LibraryModule.Exports.Date_Day:
					return "Date.Day";
				case Library.LibraryModule.Exports.Date_AddDays:
					return "Date.AddDays";
				case Library.LibraryModule.Exports.Date_AddWeeks:
					return "Date.AddWeeks";
				case Library.LibraryModule.Exports.Date_AddMonths:
					return "Date.AddMonths";
				case Library.LibraryModule.Exports.Date_AddQuarters:
					return "Date.AddQuarters";
				case Library.LibraryModule.Exports.Date_AddYears:
					return "Date.AddYears";
				case Library.LibraryModule.Exports.Date_IsLeapYear:
					return "Date.IsLeapYear";
				case Library.LibraryModule.Exports.Date_StartOfYear:
					return "Date.StartOfYear";
				case Library.LibraryModule.Exports.Date_StartOfQuarter:
					return "Date.StartOfQuarter";
				case Library.LibraryModule.Exports.Date_StartOfMonth:
					return "Date.StartOfMonth";
				case Library.LibraryModule.Exports.Date_StartOfWeek:
					return "Date.StartOfWeek";
				case Library.LibraryModule.Exports.Date_StartOfDay:
					return "Date.StartOfDay";
				case Library.LibraryModule.Exports.Date_EndOfYear:
					return "Date.EndOfYear";
				case Library.LibraryModule.Exports.Date_EndOfQuarter:
					return "Date.EndOfQuarter";
				case Library.LibraryModule.Exports.Date_EndOfMonth:
					return "Date.EndOfMonth";
				case Library.LibraryModule.Exports.Date_EndOfWeek:
					return "Date.EndOfWeek";
				case Library.LibraryModule.Exports.Date_EndOfDay:
					return "Date.EndOfDay";
				case Library.LibraryModule.Exports.Date_DayOfWeek:
					return "Date.DayOfWeek";
				case Library.LibraryModule.Exports.Date_DayOfYear:
					return "Date.DayOfYear";
				case Library.LibraryModule.Exports.Date_DaysInMonth:
					return "Date.DaysInMonth";
				case Library.LibraryModule.Exports.Date_QuarterOfYear:
					return "Date.QuarterOfYear";
				case Library.LibraryModule.Exports.Date_WeekOfMonth:
					return "Date.WeekOfMonth";
				case Library.LibraryModule.Exports.Date_WeekOfYear:
					return "Date.WeekOfYear";
				case Library.LibraryModule.Exports.DateTime_FromText:
					return "DateTime.FromText";
				case Library.LibraryModule.Exports.DateTime_From:
					return "DateTime.From";
				case Library.LibraryModule.Exports.DateTime_ToText:
					return "DateTime.ToText";
				case Library.LibraryModule.Exports.DateTime_ToRecord:
					return "DateTime.ToRecord";
				case Library.LibraryModule.Exports.DateTime_Date:
					return "DateTime.Date";
				case Library.LibraryModule.Exports.DateTime_Time:
					return "DateTime.Time";
				case Library.LibraryModule.Exports.DateTime_AddZone:
					return "DateTime.AddZone";
				case Library.LibraryModule.Exports.DateTime_LocalNow:
					return "DateTime.LocalNow";
				case Library.LibraryModule.Exports.DateTime_FixedLocalNow:
					return "DateTime.FixedLocalNow";
				case Library.LibraryModule.Exports.DateTime_FromFileTime:
					return "DateTime.FromFileTime";
				case Library.LibraryModule.Exports.DateTimeZone_FromText:
					return "DateTimeZone.FromText";
				case Library.LibraryModule.Exports.DateTimeZone_From:
					return "DateTimeZone.From";
				case Library.LibraryModule.Exports.DateTimeZone_ToText:
					return "DateTimeZone.ToText";
				case Library.LibraryModule.Exports.DateTimeZone_ToRecord:
					return "DateTimeZone.ToRecord";
				case Library.LibraryModule.Exports.DateTimeZone_TimezoneHours:
					return "DateTimeZone.ZoneHours";
				case Library.LibraryModule.Exports.DateTimeZone_TimezoneMinutes:
					return "DateTimeZone.ZoneMinutes";
				case Library.LibraryModule.Exports.DateTimeZone_LocalNow:
					return "DateTimeZone.LocalNow";
				case Library.LibraryModule.Exports.DateTimeZone_UtcNow:
					return "DateTimeZone.UtcNow";
				case Library.LibraryModule.Exports.DateTimeZone_FixedLocalNow:
					return "DateTimeZone.FixedLocalNow";
				case Library.LibraryModule.Exports.DateTimeZone_FixedUtcNow:
					return "DateTimeZone.FixedUtcNow";
				case Library.LibraryModule.Exports.DateTimeZone_ToLocal:
					return "DateTimeZone.ToLocal";
				case Library.LibraryModule.Exports.DateTimeZone_ToUtc:
					return "DateTimeZone.ToUtc";
				case Library.LibraryModule.Exports.DateTimeZone_SwitchTimezone:
					return "DateTimeZone.SwitchZone";
				case Library.LibraryModule.Exports.DateTimeZone_RemoveTimezone:
					return "DateTimeZone.RemoveZone";
				case Library.LibraryModule.Exports.DateTimeZone_FromFileTime:
					return "DateTimeZone.FromFileTime";
				case Library.LibraryModule.Exports.Time_FromText:
					return "Time.FromText";
				case Library.LibraryModule.Exports.Time_From:
					return "Time.From";
				case Library.LibraryModule.Exports.Time_ToText:
					return "Time.ToText";
				case Library.LibraryModule.Exports.Time_ToRecord:
					return "Time.ToRecord";
				case Library.LibraryModule.Exports.Time_Hour:
					return "Time.Hour";
				case Library.LibraryModule.Exports.Time_Minute:
					return "Time.Minute";
				case Library.LibraryModule.Exports.Time_Second:
					return "Time.Second";
				case Library.LibraryModule.Exports.Time_StartOfHour:
					return "Time.StartOfHour";
				case Library.LibraryModule.Exports.Time_EndOfHour:
					return "Time.EndOfHour";
				case Library.LibraryModule.Exports.Function_Type:
					return "Function.Type";
				case Library.LibraryModule.Exports.Function_From:
					return "Function.From";
				case Library.LibraryModule.Exports.Function_Invoke:
					return "Function.Invoke";
				case Library.LibraryModule.Exports.Function_InvokeAfter:
					return "Function.InvokeAfter";
				case Library.LibraryModule.Exports.Function_IsDataSource:
					return "Function.IsDataSource";
				case Library.LibraryModule.Exports.Function_ScalarVector:
					return "Function.ScalarVector";
				case Library.LibraryModule.Exports.Null_Type:
					return "Null.Type";
				case Library.LibraryModule.Exports.Number_Abs:
					return "Number.Abs";
				case Library.LibraryModule.Exports.Number_Acos:
					return "Number.Acos";
				case Library.LibraryModule.Exports.Number_Asin:
					return "Number.Asin";
				case Library.LibraryModule.Exports.Number_Atan:
					return "Number.Atan";
				case Library.LibraryModule.Exports.Number_Atan2:
					return "Number.Atan2";
				case Library.LibraryModule.Exports.Number_Combinations:
					return "Number.Combinations";
				case Library.LibraryModule.Exports.Number_Cos:
					return "Number.Cos";
				case Library.LibraryModule.Exports.Number_Cosh:
					return "Number.Cosh";
				case Library.LibraryModule.Exports.Number_Exp:
					return "Number.Exp";
				case Library.LibraryModule.Exports.Number_Factorial:
					return "Number.Factorial";
				case Library.LibraryModule.Exports.Number_IntegerDivide:
					return "Number.IntegerDivide";
				case Library.LibraryModule.Exports.Number_Log:
					return "Number.Log";
				case Library.LibraryModule.Exports.Number_Log10:
					return "Number.Log10";
				case Library.LibraryModule.Exports.Number_Ln:
					return "Number.Ln";
				case Library.LibraryModule.Exports.Number_Mod:
					return "Number.Mod";
				case Library.LibraryModule.Exports.Number_Permutations:
					return "Number.Permutations";
				case Library.LibraryModule.Exports.Number_Power:
					return "Number.Power";
				case Library.LibraryModule.Exports.Number_Random:
					return "Number.Random";
				case Library.LibraryModule.Exports.Number_RandomBetween:
					return "Number.RandomBetween";
				case Library.LibraryModule.Exports.Number_Round:
					return "Number.Round";
				case Library.LibraryModule.Exports.Number_RoundDown:
					return "Number.RoundDown";
				case Library.LibraryModule.Exports.Number_RoundUp:
					return "Number.RoundUp";
				case Library.LibraryModule.Exports.Number_RoundTowardZero:
					return "Number.RoundTowardZero";
				case Library.LibraryModule.Exports.Number_RoundAwayFromZero:
					return "Number.RoundAwayFromZero";
				case Library.LibraryModule.Exports.Number_Sign:
					return "Number.Sign";
				case Library.LibraryModule.Exports.Number_Sin:
					return "Number.Sin";
				case Library.LibraryModule.Exports.Number_Sinh:
					return "Number.Sinh";
				case Library.LibraryModule.Exports.Number_Sqrt:
					return "Number.Sqrt";
				case Library.LibraryModule.Exports.Number_Tan:
					return "Number.Tan";
				case Library.LibraryModule.Exports.Number_Tanh:
					return "Number.Tanh";
				case Library.LibraryModule.Exports.Number_IsEven:
					return "Number.IsEven";
				case Library.LibraryModule.Exports.Number_IsOdd:
					return "Number.IsOdd";
				case Library.LibraryModule.Exports.List_Type:
					return "List.Type";
				case Library.LibraryModule.Exports.List_Contains:
					return "List.Contains";
				case Library.LibraryModule.Exports.List_Difference:
					return "List.Difference";
				case Library.LibraryModule.Exports.List_First:
					return "List.First";
				case Library.LibraryModule.Exports.List_Generate:
					return "List.Generate";
				case Library.LibraryModule.Exports.List_Intersect:
					return "List.Intersect";
				case Library.LibraryModule.Exports.List_IsDistinct:
					return "List.IsDistinct";
				case Library.LibraryModule.Exports.List_Last:
					return "List.Last";
				case Library.LibraryModule.Exports.List_RemoveMatchingItems:
					return "List.RemoveMatchingItems";
				case Library.LibraryModule.Exports.List_RemoveNulls:
					return "List.RemoveNulls";
				case Library.LibraryModule.Exports.List_Repeat:
					return "List.Repeat";
				case Library.LibraryModule.Exports.List_ReplaceMatchingItems:
					return "List.ReplaceMatchingItems";
				case Library.LibraryModule.Exports.List_Reverse:
					return "List.Reverse";
				case Library.LibraryModule.Exports.List_Single:
					return "List.Single";
				case Library.LibraryModule.Exports.List_SingleOrDefault:
					return "List.SingleOrDefault";
				case Library.LibraryModule.Exports.List_Union:
					return "List.Union";
				case Library.LibraryModule.Exports.List_Accumulate:
					return "List.Accumulate";
				case Library.LibraryModule.Exports.List_Buffer:
					return "List.Buffer";
				case Library.LibraryModule.Exports.List_Combine:
					return "List.Combine";
				case Library.LibraryModule.Exports.List_ContainsAll:
					return "List.ContainsAll";
				case Library.LibraryModule.Exports.List_ContainsAny:
					return "List.ContainsAny";
				case Library.LibraryModule.Exports.List_InsertRange:
					return "List.InsertRange";
				case Library.LibraryModule.Exports.List_Max:
					return "List.Max";
				case Library.LibraryModule.Exports.List_MaxN:
					return "List.MaxN";
				case Library.LibraryModule.Exports.List_Min:
					return "List.Min";
				case Library.LibraryModule.Exports.List_MinN:
					return "List.MinN";
				case Library.LibraryModule.Exports.List_PositionOf:
					return "List.PositionOf";
				case Library.LibraryModule.Exports.List_PositionOfAny:
					return "List.PositionOfAny";
				case Library.LibraryModule.Exports.List_Positions:
					return "List.Positions";
				case Library.LibraryModule.Exports.List_RemoveRange:
					return "List.RemoveRange";
				case Library.LibraryModule.Exports.List_ReplaceRange:
					return "List.ReplaceRange";
				case Library.LibraryModule.Exports.List_Alternate:
					return "List.Alternate";
				case Library.LibraryModule.Exports.List_Zip:
					return "List.Zip";
				case Library.LibraryModule.Exports.List_Split:
					return "List.Split";
				case Library.LibraryModule.Exports.List_Average:
					return "List.Average";
				case Library.LibraryModule.Exports.List_Covariance:
					return "List.Covariance";
				case Library.LibraryModule.Exports.List_Median:
					return "List.Median";
				case Library.LibraryModule.Exports.List_Mode:
					return "List.Mode";
				case Library.LibraryModule.Exports.List_Modes:
					return "List.Modes";
				case Library.LibraryModule.Exports.List_Percentile:
					return "List.Percentile";
				case Library.LibraryModule.Exports.List_Product:
					return "List.Product";
				case Library.LibraryModule.Exports.List_Sum:
					return "List.Sum";
				case Library.LibraryModule.Exports.List_StandardDeviation:
					return "List.StandardDeviation";
				case Library.LibraryModule.Exports.List_Numbers:
					return "List.Numbers";
				case Library.LibraryModule.Exports.List_Times:
					return "List.Times";
				case Library.LibraryModule.Exports.List_Dates:
					return "List.Dates";
				case Library.LibraryModule.Exports.List_DateTimes:
					return "List.DateTimes";
				case Library.LibraryModule.Exports.List_DateTimeZones:
					return "List.DateTimeZones";
				case Library.LibraryModule.Exports.List_Durations:
					return "List.Durations";
				case Library.LibraryModule.Exports.List_Random:
					return "List.Random";
				case Library.LibraryModule.Exports._Error_Record:
					return "Error.Record";
				case Library.LibraryModule.Exports._Value_Equals:
					return "Value.Equals";
				case Library.LibraryModule.Exports._Value_NullableEquals:
					return "Value.NullableEquals";
				case Library.LibraryModule.Exports._Value_Compare:
					return "Value.Compare";
				case Library.LibraryModule.Exports._Value_Type:
					return "Value.Type";
				case Library.LibraryModule.Exports._Value_ReplaceType:
					return "Value.ReplaceType";
				case Library.LibraryModule.Exports._Value_RemoveMetadata:
					return "Value.RemoveMetadata";
				case Library.LibraryModule.Exports._Value_ReplaceMetadata:
					return "Value.ReplaceMetadata";
				case Library.LibraryModule.Exports._Value_Metadata:
					return "Value.Metadata";
				case Library.LibraryModule.Exports._Value_FromText:
					return "Value.FromText";
				case Library.LibraryModule.Exports._Value_Add:
					return "Value.Add";
				case Library.LibraryModule.Exports._Value_Subtract:
					return "Value.Subtract";
				case Library.LibraryModule.Exports._Value_Multiply:
					return "Value.Multiply";
				case Library.LibraryModule.Exports._Value_Divide:
					return "Value.Divide";
				case Library.LibraryModule.Exports._Value_As:
					return "Value.As";
				case Library.LibraryModule.Exports._Value_Is:
					return "Value.Is";
				case Library.LibraryModule.Exports._Value_NativeQuery:
					return "Value.NativeQuery";
				case Library.LibraryModule.Exports._Value_Expression:
					return "Value.Expression";
				case Library.LibraryModule.Exports._Value_Optimize:
					return "Value.Optimize";
				case Library.LibraryModule.Exports._Value_Alternates:
					return "Value.Alternates";
				case Library.LibraryModule.Exports._Value_Versions:
					return "Value.Versions";
				case Library.LibraryModule.Exports._Value_VersionIdentity:
					return "Value.VersionIdentity";
				case Library.LibraryModule.Exports._Value_ViewFunction:
					return "Value.ViewFunction";
				case Library.LibraryModule.Exports._Value_ViewError:
					return "Value.ViewError";
				case Library.LibraryModule.Exports.Type_Type:
					return "Type.Type";
				case Library.LibraryModule.Exports.Type_ForRecord:
					return "Type.ForRecord";
				case Library.LibraryModule.Exports.Type_ForFunction:
					return "Type.ForFunction";
				case Library.LibraryModule.Exports.Type_NonNullable:
					return "Type.NonNullable";
				case Library.LibraryModule.Exports.Type_IsNullable:
					return "Type.IsNullable";
				case Library.LibraryModule.Exports.Type_ListItem:
					return "Type.ListItem";
				case Library.LibraryModule.Exports.Type_OpenRecord:
					return "Type.OpenRecord";
				case Library.LibraryModule.Exports.Type_ClosedRecord:
					return "Type.ClosedRecord";
				case Library.LibraryModule.Exports.Type_IsOpenRecord:
					return "Type.IsOpenRecord";
				case Library.LibraryModule.Exports.Type_RecordFields:
					return "Type.RecordFields";
				case Library.LibraryModule.Exports.Type_FunctionParameters:
					return "Type.FunctionParameters";
				case Library.LibraryModule.Exports.Type_FunctionRequiredParameters:
					return "Type.FunctionRequiredParameters";
				case Library.LibraryModule.Exports.Type_FunctionReturn:
					return "Type.FunctionReturn";
				case Library.LibraryModule.Exports.Type_Is:
					return "Type.Is";
				case Library.LibraryModule.Exports.Type_Union:
					return "Type.Union";
				case Library.LibraryModule.Exports.Type_Facets:
					return "Type.Facets";
				case Library.LibraryModule.Exports.Type_ReplaceFacets:
					return "Type.ReplaceFacets";
				case Library.LibraryModule.Exports.Logical_Type:
					return "Logical.Type";
				case Library.LibraryModule.Exports.Logical_FromText:
					return "Logical.FromText";
				case Library.LibraryModule.Exports.Logical_From:
					return "Logical.From";
				case Library.LibraryModule.Exports.Logical_ToText:
					return "Logical.ToText";
				case Library.LibraryModule.Exports.List_AllTrue:
					return "List.AllTrue";
				case Library.LibraryModule.Exports.List_AnyTrue:
					return "List.AnyTrue";
				case Library.LibraryModule.Exports.Order_Type:
					return Library.Order.Type.GetName();
				case Library.LibraryModule.Exports.Order_Ascending:
					return Library.Order.Ascending.GetName();
				case Library.LibraryModule.Exports.Order_Descending:
					return Library.Order.Descending.GetName();
				case Library.LibraryModule.Exports.Occurrence_Type:
					return Library.Occurrence.Type.GetName();
				case Library.LibraryModule.Exports.Occurrence_All:
					return Library.Occurrence.All.GetName();
				case Library.LibraryModule.Exports.Occurrence_First:
					return Library.Occurrence.First.GetName();
				case Library.LibraryModule.Exports.Occurrence_Last:
					return Library.Occurrence.Last.GetName();
				case Library.LibraryModule.Exports.Int8_Type:
					return "Int8.Type";
				case Library.LibraryModule.Exports.Int16_Type:
					return "Int16.Type";
				case Library.LibraryModule.Exports.Int32_Type:
					return "Int32.Type";
				case Library.LibraryModule.Exports.Int64_Type:
					return "Int64.Type";
				case Library.LibraryModule.Exports.Single_Type:
					return "Single.Type";
				case Library.LibraryModule.Exports.Double_Type:
					return "Double.Type";
				case Library.LibraryModule.Exports.Decimal_Type:
					return "Decimal.Type";
				case Library.LibraryModule.Exports.Currency_Type:
					return "Currency.Type";
				case Library.LibraryModule.Exports.Percentage_Type:
					return "Percentage.Type";
				case Library.LibraryModule.Exports.Guid_Type:
					return "Guid.Type";
				case Library.LibraryModule.Exports.Uri_Type:
					return "Uri.Type";
				case Library.LibraryModule.Exports.Password_Type:
					return "Password.Type";
				case Library.LibraryModule.Exports.Byte_From:
					return "Byte.From";
				case Library.LibraryModule.Exports.Int8_From:
					return "Int8.From";
				case Library.LibraryModule.Exports.Int16_From:
					return "Int16.From";
				case Library.LibraryModule.Exports.Int32_From:
					return "Int32.From";
				case Library.LibraryModule.Exports.Int64_From:
					return "Int64.From";
				case Library.LibraryModule.Exports.Single_From:
					return "Single.From";
				case Library.LibraryModule.Exports.Double_From:
					return "Double.From";
				case Library.LibraryModule.Exports.Decimal_From:
					return "Decimal.From";
				case Library.LibraryModule.Exports.Currency_From:
					return "Currency.From";
				case Library.LibraryModule.Exports.Percentage_From:
					return "Percentage.From";
				case Library.LibraryModule.Exports.Guid_From:
					return "Guid.From";
				case Library.LibraryModule.Exports.Date_Type:
					return "Date.Type";
				case Library.LibraryModule.Exports.DateTime_Type:
					return "DateTime.Type";
				case Library.LibraryModule.Exports.DateTimeZone_Type:
					return "DateTimeZone.Type";
				case Library.LibraryModule.Exports.Time_Type:
					return "Time.Type";
				case Library.LibraryModule.Exports.PercentileMode_Type:
					return Library.PercentileModeEnum.Type.GetName();
				case Library.LibraryModule.Exports.PercentileMode_ExcelInc:
					return Library.PercentileModeEnum.ExcelInc.GetName();
				case Library.LibraryModule.Exports.PercentileMode_ExcelExc:
					return Library.PercentileModeEnum.ExcelExc.GetName();
				case Library.LibraryModule.Exports.PercentileMode_SqlDisc:
					return Library.PercentileModeEnum.SqlDisc.GetName();
				case Library.LibraryModule.Exports.PercentileMode_SqlCont:
					return Library.PercentileModeEnum.SqlCont.GetName();
				case Library.LibraryModule.Exports.TimeZone_Current:
					return "TimeZone.Current";
				case Library.LibraryModule.Exports.BufferMode_Type:
					return "BufferMode.Type";
				case Library.LibraryModule.Exports.BufferMode_Eager:
					return "BufferMode.Eager";
				case Library.LibraryModule.Exports.BufferMode_Delayed:
					return "BufferMode.Delayed";
				case Library.LibraryModule.Exports.Progress_DataSourceProgress:
					return "Progress.DataSourceProgress";
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060082E8 RID: 33512 RVA: 0x001BCFD0 File Offset: 0x001BB1D0
			private static string GetSectionKey(Library.LibraryModule.SectionExports export)
			{
				switch (export)
				{
				case Library.LibraryModule.SectionExports.UICulture_GetString:
					return "UICulture.GetString";
				case Library.LibraryModule.SectionExports.List_Normalize:
					return "List.Normalize";
				case Library.LibraryModule.SectionExports.Enumerator_FromList:
					return "Enumerator.FromList";
				case Library.LibraryModule.SectionExports.Enumerator_ToList:
					return "Enumerator.ToList";
				case Library.LibraryModule.SectionExports.Type_Kind:
					return "Type.Kind";
				case Library.LibraryModule.SectionExports.Type_Name:
					return "Type.Name";
				case Library.LibraryModule.SectionExports.Binary_FromHandlers:
					return "Binary.FromHandlers";
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060082E9 RID: 33513 RVA: 0x001BD030 File Offset: 0x001BB230
			private static Value GetValue(Library.LibraryModule.Exports value, IEngineHost host)
			{
				switch (value)
				{
				case Library.LibraryModule.Exports.Any_Type:
					return TypeValue.Any;
				case Library.LibraryModule.Exports.None_Type:
					return TypeValue.None;
				case Library.LibraryModule.Exports.Expression_Constant:
					return Library.Expression.Constant;
				case Library.LibraryModule.Exports.Expression_Evaluate:
					return Library.Expression.Evaluate;
				case Library.LibraryModule.Exports.Expression_Identifier:
					return Library.Expression.Identifier;
				case Library.LibraryModule.Exports.TextEncoding_Type:
					return TextEncoding.Type;
				case Library.LibraryModule.Exports.TextEncoding_Utf8:
					return TextEncoding.Utf8;
				case Library.LibraryModule.Exports.TextEncoding_Utf16:
					return TextEncoding.Utf16;
				case Library.LibraryModule.Exports.TextEncoding_Ascii:
					return TextEncoding.Ascii;
				case Library.LibraryModule.Exports.TextEncoding_Unicode:
					return TextEncoding.Unicode;
				case Library.LibraryModule.Exports.TextEncoding_BigEndianUnicode:
					return TextEncoding.BigEndianUnicode;
				case Library.LibraryModule.Exports.TextEncoding_Windows:
					return TextEncoding.Windows;
				case Library.LibraryModule.Exports.Culture_Current:
					return Culture.GetDefaultCultureName(host);
				case Library.LibraryModule.Exports.Day_Type:
					return Library.Day.Type;
				case Library.LibraryModule.Exports.Day_Sunday:
					return Library.Day.Sunday;
				case Library.LibraryModule.Exports.Day_Monday:
					return Library.Day.Monday;
				case Library.LibraryModule.Exports.Day_Tuesday:
					return Library.Day.Tuesday;
				case Library.LibraryModule.Exports.Day_Wednesday:
					return Library.Day.Wednesday;
				case Library.LibraryModule.Exports.Day_Thursday:
					return Library.Day.Thursday;
				case Library.LibraryModule.Exports.Day_Friday:
					return Library.Day.Friday;
				case Library.LibraryModule.Exports.Day_Saturday:
					return Library.Day.Saturday;
				case Library.LibraryModule.Exports.Duration_Type:
					return TypeValue.Duration;
				case Library.LibraryModule.Exports.Duration_FromText:
					return Library.Duration.FromText;
				case Library.LibraryModule.Exports.Duration_From:
					return Library.Duration.From;
				case Library.LibraryModule.Exports.Duration_ToText:
					return Library.Duration.ToText;
				case Library.LibraryModule.Exports.Duration_ToRecord:
					return Library.Duration.ToRecord;
				case Library.LibraryModule.Exports.Duration_Days:
					return Library.Duration.Days;
				case Library.LibraryModule.Exports.Duration_Hours:
					return Library.Duration.Hours;
				case Library.LibraryModule.Exports.Duration_Minutes:
					return Library.Duration.Minutes;
				case Library.LibraryModule.Exports.Duration_Seconds:
					return Library.Duration.Seconds;
				case Library.LibraryModule.Exports.Duration_TotalDays:
					return Library.Duration.TotalDays;
				case Library.LibraryModule.Exports.Duration_TotalHours:
					return Library.Duration.TotalHours;
				case Library.LibraryModule.Exports.Duration_TotalMinutes:
					return Library.Duration.TotalMinutes;
				case Library.LibraryModule.Exports.Duration_TotalSeconds:
					return Library.Duration.TotalSeconds;
				case Library.LibraryModule.Exports.JoinKind_Type:
					return Library.JoinKind.Type;
				case Library.LibraryModule.Exports.JoinKind_Inner:
					return Library.JoinKind.Inner;
				case Library.LibraryModule.Exports.JoinKind_LeftOuter:
					return Library.JoinKind.LeftOuter;
				case Library.LibraryModule.Exports.JoinKind_RightOuter:
					return Library.JoinKind.RightOuter;
				case Library.LibraryModule.Exports.JoinKind_FullOuter:
					return Library.JoinKind.FullOuter;
				case Library.LibraryModule.Exports.JoinKind_LeftAnti:
					return Library.JoinKind.LeftAnti;
				case Library.LibraryModule.Exports.JoinKind_RightAnti:
					return Library.JoinKind.RightAnti;
				case Library.LibraryModule.Exports.JoinKind_LeftSemi:
					return Library.JoinKind.LeftSemi;
				case Library.LibraryModule.Exports.JoinKind_RightSemi:
					return Library.JoinKind.RightSemi;
				case Library.LibraryModule.Exports.MissingField_Type:
					return Library.MissingField.Type;
				case Library.LibraryModule.Exports.MissingField_Error:
					return Library.MissingField.Error;
				case Library.LibraryModule.Exports.MissingField_Ignore:
					return Library.MissingField.Ignore;
				case Library.LibraryModule.Exports.MissingField_UseNull:
					return Library.MissingField.UseNull;
				case Library.LibraryModule.Exports.GroupKind_Type:
					return Library.GroupKind.Type;
				case Library.LibraryModule.Exports.GroupKind_Global:
					return Library.GroupKind.Global;
				case Library.LibraryModule.Exports.GroupKind_Local:
					return Library.GroupKind.Local;
				case Library.LibraryModule.Exports.Number_E:
					return Library.Number.E;
				case Library.LibraryModule.Exports.Number_PI:
					return Library.Number.PI;
				case Library.LibraryModule.Exports.RoundingMode_Type:
					return Library.RoundingMode.Type;
				case Library.LibraryModule.Exports.RoundingMode_Up:
					return Library.RoundingMode.Up;
				case Library.LibraryModule.Exports.RoundingMode_Down:
					return Library.RoundingMode.Down;
				case Library.LibraryModule.Exports.RoundingMode_AwayFromZero:
					return Library.RoundingMode.AwayFromZero;
				case Library.LibraryModule.Exports.RoundingMode_TowardZero:
					return Library.RoundingMode.TowardZero;
				case Library.LibraryModule.Exports.RoundingMode_ToEven:
					return Library.RoundingMode.ToEven;
				case Library.LibraryModule.Exports.Record_Type:
					return TypeValue.Record;
				case Library.LibraryModule.Exports.Record_AddField:
					return Library.Record.AddField;
				case Library.LibraryModule.Exports.Record_Field:
					return Library.Record.Field;
				case Library.LibraryModule.Exports.Record_FieldCount:
					return Library.Record.FieldCount;
				case Library.LibraryModule.Exports.Record_FieldNames:
					return Library.Record.FieldNames;
				case Library.LibraryModule.Exports.Record_FieldOrDefault:
					return Library.Record.FieldOrDefault;
				case Library.LibraryModule.Exports.Record_FieldValues:
					return Library.Record.FieldValues;
				case Library.LibraryModule.Exports.Record_FromTable:
					return Library.Record.FromTable;
				case Library.LibraryModule.Exports.Record_HasFields:
					return Library.Record.HasFields;
				case Library.LibraryModule.Exports.Record_RemoveFields:
					return Library.Record.RemoveFields;
				case Library.LibraryModule.Exports.Record_RenameFields:
					return Library.Record.RenameFields;
				case Library.LibraryModule.Exports.Record_ReorderFields:
					return Library.Record.ReorderFields;
				case Library.LibraryModule.Exports.Record_SelectFields:
					return Library.Record.SelectFields;
				case Library.LibraryModule.Exports.Record_ToTable:
					return Library.Record.ToTable;
				case Library.LibraryModule.Exports.Record_TransformFields:
					return Library.Record.TransformFields;
				case Library.LibraryModule.Exports.Record_Combine:
					return Library.Record.Combine;
				case Library.LibraryModule.Exports.Record_FromList:
					return Library.Record.FromList;
				case Library.LibraryModule.Exports.Record_ToList:
					return Library.Record.ToList;
				case Library.LibraryModule.Exports.Precision_Type:
					return Library.PrecisionEnum.Type;
				case Library.LibraryModule.Exports.Precision_Double:
					return Library.PrecisionEnum.Double;
				case Library.LibraryModule.Exports.Precision_Decimal:
					return Library.PrecisionEnum.Decimal;
				case Library.LibraryModule.Exports.Number_Type:
					return TypeValue.Number;
				case Library.LibraryModule.Exports.Number_From:
					return new Library.Number.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.Number_FromText:
					return new Library.Number.FromTextFunctionValue(host, null);
				case Library.LibraryModule.Exports.Number_ToText:
					return new Library.Number.ToTextFunctionValue(host);
				case Library.LibraryModule.Exports.Number_IsNaN:
					return Library.Number.IsNaN;
				case Library.LibraryModule.Exports.Number_NaN:
					return Library.Number.NaN;
				case Library.LibraryModule.Exports.Number_NegativeInfinity:
					return Library.Number.NegativeInfinity;
				case Library.LibraryModule.Exports.Number_PositiveInfinity:
					return Library.Number.PositiveInfinity;
				case Library.LibraryModule.Exports.Number_Epsilon:
					return Library.Number.Epsilon;
				case Library.LibraryModule.Exports.Number_BitwiseNot:
					return UnaryOperator.BitwiseNot;
				case Library.LibraryModule.Exports.Number_BitwiseOr:
					return BinaryOperator.BitwiseOr;
				case Library.LibraryModule.Exports.Number_BitwiseAnd:
					return BinaryOperator.BitwiseAnd;
				case Library.LibraryModule.Exports.Number_BitwiseXor:
					return BinaryOperator.BitwiseXor;
				case Library.LibraryModule.Exports.Number_BitwiseShiftLeft:
					return BinaryOperator.ShiftLeft;
				case Library.LibraryModule.Exports.Number_BitwiseShiftRight:
					return BinaryOperator.ShiftRight;
				case Library.LibraryModule.Exports.BinaryEncoding_Type:
					return Library.BinaryEncoding.Type;
				case Library.LibraryModule.Exports.BinaryEncoding_Hex:
					return Library.BinaryEncoding.Hex;
				case Library.LibraryModule.Exports.BinaryEncoding_Base64:
					return Library.BinaryEncoding.Base64;
				case Library.LibraryModule.Exports.Binary_ApproximateLength:
					return Library.Binary.ApproximateLength;
				case Library.LibraryModule.Exports.Binary_Type:
					return TypeValue.Binary;
				case Library.LibraryModule.Exports.Binary_ToText:
					return Library.Binary.ToText;
				case Library.LibraryModule.Exports.Binary_From:
					return Library.Binary.From;
				case Library.LibraryModule.Exports.Binary_FromText:
					return Library.Binary.FromText;
				case Library.LibraryModule.Exports.Binary_ToList:
					return Library.Binary.ToList;
				case Library.LibraryModule.Exports.Binary_FromList:
					return Library.Binary.FromList;
				case Library.LibraryModule.Exports.Binary_Combine:
					return Library.Binary.Combine;
				case Library.LibraryModule.Exports.Binary_Length:
					return Library.Binary.Length;
				case Library.LibraryModule.Exports.Binary_Buffer:
					return Library.Binary.Buffer;
				case Library.LibraryModule.Exports.Binary_Compress:
					return Library.Binary.Compress;
				case Library.LibraryModule.Exports.Binary_Decompress:
					return Library.Binary.Decompress;
				case Library.LibraryModule.Exports.Binary_InferContentType:
					return Library.Binary.InferContentType;
				case Library.LibraryModule.Exports.Binary_Range:
					return Library.Binary.Range;
				case Library.LibraryModule.Exports.Binary_Split:
					return Library.Binary.Split;
				case Library.LibraryModule.Exports.Compression_Type:
					return Library.CompressionType.Type;
				case Library.LibraryModule.Exports.Compression_None:
					return Library.CompressionType.None;
				case Library.LibraryModule.Exports.Compression_GZip:
					return Library.CompressionType.GZip;
				case Library.LibraryModule.Exports.Compression_Deflate:
					return Library.CompressionType.Deflate;
				case Library.LibraryModule.Exports.Compression_Snappy:
					return Library.CompressionType.Snappy;
				case Library.LibraryModule.Exports.Compression_Brotli:
					return Library.CompressionType.Brotli;
				case Library.LibraryModule.Exports.Compression_LZ4:
					return Library.CompressionType.LZ4;
				case Library.LibraryModule.Exports.Compression_Zstandard:
					return Library.CompressionType.Zstandard;
				case Library.LibraryModule.Exports.Byte_Type:
					return TypeValue.Byte;
				case Library.LibraryModule.Exports.Character_Type:
					return TypeValue.Character;
				case Library.LibraryModule.Exports.Character_FromNumber:
					return Library.Character.FromNumber;
				case Library.LibraryModule.Exports.Character_ToNumber:
					return Library.Character.ToNumber;
				case Library.LibraryModule.Exports.Text_Type:
					return TypeValue.Text;
				case Library.LibraryModule.Exports.Text_At:
					return Library.Text.At;
				case Library.LibraryModule.Exports.Text_From:
					return new Library.Text.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.Text_Length:
					return Library.Text.Length;
				case Library.LibraryModule.Exports.Text_Range:
					return Library.Text.Range;
				case Library.LibraryModule.Exports.Text_Middle:
					return Library.Text.Middle;
				case Library.LibraryModule.Exports.Text_Start:
					return Library.Text.Start;
				case Library.LibraryModule.Exports.Text_End:
					return Library.Text.End;
				case Library.LibraryModule.Exports.Text_StartsWith:
					return Library.Text.StartsWith;
				case Library.LibraryModule.Exports.Text_EndsWith:
					return Library.Text.EndsWith;
				case Library.LibraryModule.Exports.Text_Contains:
					return Library.Text.Contains;
				case Library.LibraryModule.Exports.Text_Clean:
					return Library.Text.Clean;
				case Library.LibraryModule.Exports.Text_PositionOf:
					return Library.Text.PositionOf;
				case Library.LibraryModule.Exports.Text_PositionOfAny:
					return Library.Text.PositionOfAny;
				case Library.LibraryModule.Exports.Text_Lower:
					return new Library.Text.LowerFunctionValue(host);
				case Library.LibraryModule.Exports.Text_Upper:
					return new Library.Text.UpperFunctionValue(host);
				case Library.LibraryModule.Exports.Text_Proper:
					return new Library.Text.ProperFunctionValue(host);
				case Library.LibraryModule.Exports.Text_Split:
					return Library.Text.Split;
				case Library.LibraryModule.Exports.Text_SplitAny:
					return Library.Text.SplitAny;
				case Library.LibraryModule.Exports.Text_Combine:
					return Library.Text.Combine;
				case Library.LibraryModule.Exports.Text_Repeat:
					return Library.Text.Repeat;
				case Library.LibraryModule.Exports.Text_Replace:
					return Library.Text.Replace;
				case Library.LibraryModule.Exports.Text_ReplaceRange:
					return Library.Text.ReplaceRange;
				case Library.LibraryModule.Exports.Text_Insert:
					return Library.Text.Insert;
				case Library.LibraryModule.Exports.Text_Remove:
					return Library.Text.Remove;
				case Library.LibraryModule.Exports.Text_RemoveRange:
					return Library.Text.RemoveRange;
				case Library.LibraryModule.Exports.Text_Reverse:
					return Library.Text.Reverse;
				case Library.LibraryModule.Exports.Text_Select:
					return Library.Text.Select;
				case Library.LibraryModule.Exports.Text_Trim:
					return Library.Text.Trim;
				case Library.LibraryModule.Exports.Text_TrimStart:
					return Library.Text.TrimStart;
				case Library.LibraryModule.Exports.Text_TrimEnd:
					return Library.Text.TrimEnd;
				case Library.LibraryModule.Exports.Text_PadStart:
					return Library.Text.PadStart;
				case Library.LibraryModule.Exports.Text_PadEnd:
					return Library.Text.PadEnd;
				case Library.LibraryModule.Exports.Text_ToBinary:
					return Library.Text.ToBinary;
				case Library.LibraryModule.Exports.Text_ToList:
					return Library.Text.ToList;
				case Library.LibraryModule.Exports.Text_FromBinary:
					return Library.Text.FromBinary;
				case Library.LibraryModule.Exports.Text_NewGuid:
					return new Library.Text.NewGuidFunctionValue(host);
				case Library.LibraryModule.Exports.Text_InferNumberType:
					return new Library.Text.InferNumberTypeFunctionValue(host);
				case Library.LibraryModule.Exports.Text_Format:
					return new Library.Text.FormatFunctionValue(host);
				case Library.LibraryModule.Exports.Comparer_FromCulture:
					return new Library.Comparer.FromCultureFunctionValue(host);
				case Library.LibraryModule.Exports.Comparer_Ordinal:
					return Library.Comparer.Ordinal;
				case Library.LibraryModule.Exports.Comparer_OrdinalIgnoreCase:
					return Library.Comparer.OrdinalIgnoreCase;
				case Library.LibraryModule.Exports.Comparer_Equals:
					return Library.Comparer.Equals;
				case Library.LibraryModule.Exports.Date_FromText:
					return new Library.Date.FromTextFunctionValue(host);
				case Library.LibraryModule.Exports.Date_From:
					return new Library.Date.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.Date_ToText:
					return new Library.Date.ToTextFunctionValue(host);
				case Library.LibraryModule.Exports.Date_ToRecord:
					return Library.Date.ToRecord;
				case Library.LibraryModule.Exports.Date_Year:
					return Library.Date.Year;
				case Library.LibraryModule.Exports.Date_Month:
					return Library.Date.Month;
				case Library.LibraryModule.Exports.Date_Day:
					return Library.Date.Day;
				case Library.LibraryModule.Exports.Date_AddDays:
					return Library.Date.AddDays;
				case Library.LibraryModule.Exports.Date_AddWeeks:
					return Library.Date.AddWeeks;
				case Library.LibraryModule.Exports.Date_AddMonths:
					return Library.Date.AddMonths;
				case Library.LibraryModule.Exports.Date_AddQuarters:
					return Library.Date.AddQuarters;
				case Library.LibraryModule.Exports.Date_AddYears:
					return Library.Date.AddYears;
				case Library.LibraryModule.Exports.Date_IsLeapYear:
					return Library.Date.IsLeapYear;
				case Library.LibraryModule.Exports.Date_StartOfYear:
					return Library.Date.StartOfYear;
				case Library.LibraryModule.Exports.Date_StartOfQuarter:
					return Library.Date.StartOfQuarter;
				case Library.LibraryModule.Exports.Date_StartOfMonth:
					return Library.Date.StartOfMonth;
				case Library.LibraryModule.Exports.Date_StartOfWeek:
					return new Library.Date.StartOfWeekFunctionValue(host);
				case Library.LibraryModule.Exports.Date_StartOfDay:
					return Library.Date.StartOfDay;
				case Library.LibraryModule.Exports.Date_EndOfYear:
					return Library.Date.EndOfYear;
				case Library.LibraryModule.Exports.Date_EndOfQuarter:
					return Library.Date.EndOfQuarter;
				case Library.LibraryModule.Exports.Date_EndOfMonth:
					return Library.Date.EndOfMonth;
				case Library.LibraryModule.Exports.Date_EndOfWeek:
					return new Library.Date.EndOfWeekFunctionValue(host);
				case Library.LibraryModule.Exports.Date_EndOfDay:
					return Library.Date.EndOfDay;
				case Library.LibraryModule.Exports.Date_DayOfWeek:
					return new Library.Date.DayOfWeekFunctionValue(host);
				case Library.LibraryModule.Exports.Date_DayOfYear:
					return Library.Date.DayOfYear;
				case Library.LibraryModule.Exports.Date_DaysInMonth:
					return Library.Date.DaysInMonth;
				case Library.LibraryModule.Exports.Date_QuarterOfYear:
					return Library.Date.QuarterOfYear;
				case Library.LibraryModule.Exports.Date_WeekOfMonth:
					return new Library.Date.WeekOfMonthFunctionValue(host);
				case Library.LibraryModule.Exports.Date_WeekOfYear:
					return new Library.Date.WeekOfYearFunctionValue(host);
				case Library.LibraryModule.Exports.DateTime_FromText:
					return new Library.DateTime.FromTextFunctionValue(host);
				case Library.LibraryModule.Exports.DateTime_From:
					return new Library.DateTime.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.DateTime_ToText:
					return new Library.DateTime.ToTextFunctionValue(host);
				case Library.LibraryModule.Exports.DateTime_ToRecord:
					return Library.DateTime.ToRecord;
				case Library.LibraryModule.Exports.DateTime_Date:
					return Library.DateTime.Date;
				case Library.LibraryModule.Exports.DateTime_Time:
					return Library.DateTime.Time;
				case Library.LibraryModule.Exports.DateTime_AddZone:
					return Library.DateTime.AddZone;
				case Library.LibraryModule.Exports.DateTime_LocalNow:
					return new Library.DateTime.LocalNowFunctionValue(host);
				case Library.LibraryModule.Exports.DateTime_FixedLocalNow:
					return new Library.DateTime.FixedLocalNowFunctionValue(host);
				case Library.LibraryModule.Exports.DateTime_FromFileTime:
					return Library.DateTime.FromFileTime;
				case Library.LibraryModule.Exports.DateTimeZone_FromText:
					return new Library.DateTimeZone.FromTextFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_From:
					return new Library.DateTimeZone.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.DateTimeZone_ToText:
					return new Library.DateTimeZone.ToTextFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_ToRecord:
					return Library.DateTimeZone.ToRecord;
				case Library.LibraryModule.Exports.DateTimeZone_TimezoneHours:
					return Library.DateTimeZone.ZoneHours;
				case Library.LibraryModule.Exports.DateTimeZone_TimezoneMinutes:
					return Library.DateTimeZone.ZoneMinutes;
				case Library.LibraryModule.Exports.DateTimeZone_LocalNow:
					return new Library.DateTimeZone.LocalNowFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_UtcNow:
					return new Library.DateTimeZone.UtcNowFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_FixedLocalNow:
					return new Library.DateTimeZone.FixedLocalNowFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_FixedUtcNow:
					return new Library.DateTimeZone.FixedUtcNowFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_ToLocal:
					return new Library.DateTimeZone.ToLocalFunctionValue(host);
				case Library.LibraryModule.Exports.DateTimeZone_ToUtc:
					return Library.DateTimeZone.ToUtc;
				case Library.LibraryModule.Exports.DateTimeZone_SwitchTimezone:
					return Library.DateTimeZone.SwitchZone;
				case Library.LibraryModule.Exports.DateTimeZone_RemoveTimezone:
					return Library.DateTimeZone.RemoveZone;
				case Library.LibraryModule.Exports.DateTimeZone_FromFileTime:
					return Library.DateTimeZone.FromFileTime;
				case Library.LibraryModule.Exports.Time_FromText:
					return new Library.Time.FromTextFunctionValue(host);
				case Library.LibraryModule.Exports.Time_From:
					return new Library.Time.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.Time_ToText:
					return new Library.Time.ToTextFunctionValue(host);
				case Library.LibraryModule.Exports.Time_ToRecord:
					return Library.Time.ToRecord;
				case Library.LibraryModule.Exports.Time_Hour:
					return Library.Time.Hour;
				case Library.LibraryModule.Exports.Time_Minute:
					return Library.Time.Minute;
				case Library.LibraryModule.Exports.Time_Second:
					return Library.Time.Second;
				case Library.LibraryModule.Exports.Time_StartOfHour:
					return Library.Time.StartOfHour;
				case Library.LibraryModule.Exports.Time_EndOfHour:
					return Library.Time.EndOfHour;
				case Library.LibraryModule.Exports.Function_Type:
					return TypeValue.Function;
				case Library.LibraryModule.Exports.Function_From:
					return Library.Function.From;
				case Library.LibraryModule.Exports.Function_Invoke:
					return Library.Function.Invoke;
				case Library.LibraryModule.Exports.Function_InvokeAfter:
					return Library.Function.InvokeAfter;
				case Library.LibraryModule.Exports.Function_IsDataSource:
					return Library.Function.IsDataSource;
				case Library.LibraryModule.Exports.Function_ScalarVector:
					return Library.Function.ScalarVector;
				case Library.LibraryModule.Exports.Null_Type:
					return TypeValue.Null;
				case Library.LibraryModule.Exports.Number_Abs:
					return Library.Number.Abs;
				case Library.LibraryModule.Exports.Number_Acos:
					return Library.Number.Acos;
				case Library.LibraryModule.Exports.Number_Asin:
					return Library.Number.Asin;
				case Library.LibraryModule.Exports.Number_Atan:
					return Library.Number.Atan;
				case Library.LibraryModule.Exports.Number_Atan2:
					return Library.Number.Atan2;
				case Library.LibraryModule.Exports.Number_Combinations:
					return Library.Number.Combinations;
				case Library.LibraryModule.Exports.Number_Cos:
					return Library.Number.Cos;
				case Library.LibraryModule.Exports.Number_Cosh:
					return Library.Number.Cosh;
				case Library.LibraryModule.Exports.Number_Exp:
					return Library.Number.Exp;
				case Library.LibraryModule.Exports.Number_Factorial:
					return Library.Number.Factorial;
				case Library.LibraryModule.Exports.Number_IntegerDivide:
					return Library.Number.IntegerDivide;
				case Library.LibraryModule.Exports.Number_Log:
					return Library.Number.Log;
				case Library.LibraryModule.Exports.Number_Log10:
					return Library.Number.Log10;
				case Library.LibraryModule.Exports.Number_Ln:
					return Library.Number.Ln;
				case Library.LibraryModule.Exports.Number_Mod:
					return Library.Number.Mod;
				case Library.LibraryModule.Exports.Number_Permutations:
					return Library.Number.Permutations;
				case Library.LibraryModule.Exports.Number_Power:
					return Library.Number.Power;
				case Library.LibraryModule.Exports.Number_Random:
					return new Library.Number.RandomFunctionValue(host);
				case Library.LibraryModule.Exports.Number_RandomBetween:
					return new Library.Number.RandomBetweenFunctionValue(host);
				case Library.LibraryModule.Exports.Number_Round:
					return Library.Number.Round;
				case Library.LibraryModule.Exports.Number_RoundDown:
					return Library.Number.RoundDown;
				case Library.LibraryModule.Exports.Number_RoundUp:
					return Library.Number.RoundUp;
				case Library.LibraryModule.Exports.Number_RoundTowardZero:
					return Library.Number.RoundTowardZero;
				case Library.LibraryModule.Exports.Number_RoundAwayFromZero:
					return Library.Number.RoundAwayFromZero;
				case Library.LibraryModule.Exports.Number_Sign:
					return Library.Number.Sign;
				case Library.LibraryModule.Exports.Number_Sin:
					return Library.Number.Sin;
				case Library.LibraryModule.Exports.Number_Sinh:
					return Library.Number.Sinh;
				case Library.LibraryModule.Exports.Number_Sqrt:
					return Library.Number.Sqrt;
				case Library.LibraryModule.Exports.Number_Tan:
					return Library.Number.Tan;
				case Library.LibraryModule.Exports.Number_Tanh:
					return Library.Number.Tanh;
				case Library.LibraryModule.Exports.Number_IsEven:
					return Library.Number.IsEven;
				case Library.LibraryModule.Exports.Number_IsOdd:
					return Library.Number.IsOdd;
				case Library.LibraryModule.Exports.List_Type:
					return TypeValue.List;
				case Library.LibraryModule.Exports.List_Contains:
					return Library.List.Contains;
				case Library.LibraryModule.Exports.List_Difference:
					return Library.List.Difference;
				case Library.LibraryModule.Exports.List_First:
					return Library.List.First;
				case Library.LibraryModule.Exports.List_Generate:
					return Library.List.Generate;
				case Library.LibraryModule.Exports.List_Intersect:
					return Library.List.Intersect;
				case Library.LibraryModule.Exports.List_IsDistinct:
					return Library.List.IsDistinct;
				case Library.LibraryModule.Exports.List_Last:
					return Library.List.Last;
				case Library.LibraryModule.Exports.List_RemoveMatchingItems:
					return Library.List.RemoveMatchingItems;
				case Library.LibraryModule.Exports.List_RemoveNulls:
					return Library.List.RemoveNulls;
				case Library.LibraryModule.Exports.List_Repeat:
					return Library.List.Repeat;
				case Library.LibraryModule.Exports.List_ReplaceMatchingItems:
					return Library.List.ReplaceMatchingItems;
				case Library.LibraryModule.Exports.List_Reverse:
					return Library.List.Reverse;
				case Library.LibraryModule.Exports.List_Single:
					return Library.List.Single;
				case Library.LibraryModule.Exports.List_SingleOrDefault:
					return Library.List.SingleOrDefault;
				case Library.LibraryModule.Exports.List_Union:
					return Library.List.Union;
				case Library.LibraryModule.Exports.List_Accumulate:
					return Library.List.Accumulate;
				case Library.LibraryModule.Exports.List_Buffer:
					return Library.List.Buffer;
				case Library.LibraryModule.Exports.List_Combine:
					return Library.List.Combine;
				case Library.LibraryModule.Exports.List_ContainsAll:
					return Library.List.ContainsAll;
				case Library.LibraryModule.Exports.List_ContainsAny:
					return Library.List.ContainsAny;
				case Library.LibraryModule.Exports.List_InsertRange:
					return Library.List.InsertRange;
				case Library.LibraryModule.Exports.List_Max:
					return Library.List.Max;
				case Library.LibraryModule.Exports.List_MaxN:
					return Library.List.MaxN;
				case Library.LibraryModule.Exports.List_Min:
					return Library.List.Min;
				case Library.LibraryModule.Exports.List_MinN:
					return Library.List.MinN;
				case Library.LibraryModule.Exports.List_PositionOf:
					return Library.List.PositionOf;
				case Library.LibraryModule.Exports.List_PositionOfAny:
					return Library.List.PositionOfAny;
				case Library.LibraryModule.Exports.List_Positions:
					return Library.List.Positions;
				case Library.LibraryModule.Exports.List_RemoveRange:
					return Library.List.RemoveRange;
				case Library.LibraryModule.Exports.List_ReplaceRange:
					return Library.List.ReplaceRange;
				case Library.LibraryModule.Exports.List_Alternate:
					return Library.List.Alternate;
				case Library.LibraryModule.Exports.List_Zip:
					return Library.List.Zip;
				case Library.LibraryModule.Exports.List_Split:
					return Library.List.Split;
				case Library.LibraryModule.Exports.List_Average:
					return Library.List.Average;
				case Library.LibraryModule.Exports.List_Covariance:
					return Library.List.Covariance;
				case Library.LibraryModule.Exports.List_Median:
					return Library.List.Median;
				case Library.LibraryModule.Exports.List_Mode:
					return Library.List.Mode;
				case Library.LibraryModule.Exports.List_Modes:
					return Library.List.Modes;
				case Library.LibraryModule.Exports.List_Percentile:
					return Library.List.Percentile;
				case Library.LibraryModule.Exports.List_Product:
					return Library.List.Product;
				case Library.LibraryModule.Exports.List_Sum:
					return Library.List.Sum;
				case Library.LibraryModule.Exports.List_StandardDeviation:
					return Library.List.StandardDeviation;
				case Library.LibraryModule.Exports.List_Numbers:
					return Library.List.Numbers;
				case Library.LibraryModule.Exports.List_Times:
					return Library.List.Times;
				case Library.LibraryModule.Exports.List_Dates:
					return Library.List.Dates;
				case Library.LibraryModule.Exports.List_DateTimes:
					return Library.List.DateTimes;
				case Library.LibraryModule.Exports.List_DateTimeZones:
					return Library.List.DateTimeZones;
				case Library.LibraryModule.Exports.List_Durations:
					return Library.List.Durations;
				case Library.LibraryModule.Exports.List_Random:
					return new Library.List.RandomFunctionValue(host);
				case Library.LibraryModule.Exports._Error_Record:
					return Library._Error.Record;
				case Library.LibraryModule.Exports._Value_Equals:
					return Library._Value.Equals;
				case Library.LibraryModule.Exports._Value_NullableEquals:
					return Library._Value.NullableEquals;
				case Library.LibraryModule.Exports._Value_Compare:
					return Library._Value.Compare;
				case Library.LibraryModule.Exports._Value_Type:
					return Library._Value.Type;
				case Library.LibraryModule.Exports._Value_ReplaceType:
					return Library._Value.ReplaceType;
				case Library.LibraryModule.Exports._Value_RemoveMetadata:
					return Library._Value.RemoveMetadata;
				case Library.LibraryModule.Exports._Value_ReplaceMetadata:
					return Library._Value.ReplaceMetadata;
				case Library.LibraryModule.Exports._Value_Metadata:
					return Library._Value.Metadata;
				case Library.LibraryModule.Exports._Value_FromText:
					return new Library._Value.FromTextFunctionValue(host);
				case Library.LibraryModule.Exports._Value_Add:
					return Library._Value.Add;
				case Library.LibraryModule.Exports._Value_Subtract:
					return Library._Value.Subtract;
				case Library.LibraryModule.Exports._Value_Multiply:
					return Library._Value.Multiply;
				case Library.LibraryModule.Exports._Value_Divide:
					return Library._Value.Divide;
				case Library.LibraryModule.Exports._Value_As:
					return Library._Value.As;
				case Library.LibraryModule.Exports._Value_Is:
					return Library._Value.Is;
				case Library.LibraryModule.Exports._Value_NativeQuery:
					return Library._Value.NativeQuery;
				case Library.LibraryModule.Exports._Value_Expression:
					return Library._Value.OptimizedExpression;
				case Library.LibraryModule.Exports._Value_Optimize:
					return Library._Value.Optimize;
				case Library.LibraryModule.Exports._Value_Alternates:
					return Library._Value.Alternates;
				case Library.LibraryModule.Exports._Value_Versions:
					return Library._Value.Versions;
				case Library.LibraryModule.Exports._Value_VersionIdentity:
					return Library._Value.VersionIdentity;
				case Library.LibraryModule.Exports._Value_ViewFunction:
					return Library._Value.ViewFunction;
				case Library.LibraryModule.Exports._Value_ViewError:
					return Library._Value.ViewError;
				case Library.LibraryModule.Exports.Type_Type:
					return TypeValue._Type;
				case Library.LibraryModule.Exports.Type_ForRecord:
					return Library.Type.ForRecord;
				case Library.LibraryModule.Exports.Type_ForFunction:
					return Library.Type.FunctionFrom;
				case Library.LibraryModule.Exports.Type_NonNullable:
					return Library.Type.NonNullable;
				case Library.LibraryModule.Exports.Type_IsNullable:
					return Library.Type.IsNullable;
				case Library.LibraryModule.Exports.Type_ListItem:
					return Library.Type.ListItem;
				case Library.LibraryModule.Exports.Type_OpenRecord:
					return Library.Type.OpenRecord;
				case Library.LibraryModule.Exports.Type_ClosedRecord:
					return Library.Type.ClosedRecord;
				case Library.LibraryModule.Exports.Type_IsOpenRecord:
					return Library.Type.IsOpenRecord;
				case Library.LibraryModule.Exports.Type_RecordFields:
					return Library.Type.RecordFields;
				case Library.LibraryModule.Exports.Type_FunctionParameters:
					return Library.Type.FunctionParameters;
				case Library.LibraryModule.Exports.Type_FunctionRequiredParameters:
					return Library.Type.FunctionRequiredParameters;
				case Library.LibraryModule.Exports.Type_FunctionReturn:
					return Library.Type.FunctionReturn;
				case Library.LibraryModule.Exports.Type_Is:
					return Library.Type.Is;
				case Library.LibraryModule.Exports.Type_Union:
					return Library.Type.Union;
				case Library.LibraryModule.Exports.Type_Facets:
					return Library.Type.Facets;
				case Library.LibraryModule.Exports.Type_ReplaceFacets:
					return Library.Type.ReplaceFacets;
				case Library.LibraryModule.Exports.Logical_Type:
					return TypeValue.Logical;
				case Library.LibraryModule.Exports.Logical_FromText:
					return Library.Logical.FromText;
				case Library.LibraryModule.Exports.Logical_From:
					return Library.Logical.From;
				case Library.LibraryModule.Exports.Logical_ToText:
					return Library.Logical.ToText;
				case Library.LibraryModule.Exports.List_AllTrue:
					return Library.List.AllTrue;
				case Library.LibraryModule.Exports.List_AnyTrue:
					return Library.List.AnyTrue;
				case Library.LibraryModule.Exports.Order_Type:
					return Library.Order.Type;
				case Library.LibraryModule.Exports.Order_Ascending:
					return Library.Order.Ascending;
				case Library.LibraryModule.Exports.Order_Descending:
					return Library.Order.Descending;
				case Library.LibraryModule.Exports.Occurrence_Type:
					return Library.Occurrence.Type;
				case Library.LibraryModule.Exports.Occurrence_All:
					return Library.Occurrence.All;
				case Library.LibraryModule.Exports.Occurrence_First:
					return Library.Occurrence.First;
				case Library.LibraryModule.Exports.Occurrence_Last:
					return Library.Occurrence.Last;
				case Library.LibraryModule.Exports.Int8_Type:
					return TypeValue.Int8;
				case Library.LibraryModule.Exports.Int16_Type:
					return TypeValue.Int16;
				case Library.LibraryModule.Exports.Int32_Type:
					return TypeValue.Int32;
				case Library.LibraryModule.Exports.Int64_Type:
					return TypeValue.Int64;
				case Library.LibraryModule.Exports.Single_Type:
					return TypeValue.Single;
				case Library.LibraryModule.Exports.Double_Type:
					return TypeValue.Double;
				case Library.LibraryModule.Exports.Decimal_Type:
					return TypeValue.Decimal;
				case Library.LibraryModule.Exports.Currency_Type:
					return TypeValue.Currency;
				case Library.LibraryModule.Exports.Percentage_Type:
					return TypeValue.Percentage;
				case Library.LibraryModule.Exports.Guid_Type:
					return TypeValue.Guid;
				case Library.LibraryModule.Exports.Uri_Type:
					return TypeValue.Uri;
				case Library.LibraryModule.Exports.Password_Type:
					return TypeValue.Password;
				case Library.LibraryModule.Exports.Byte_From:
					return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Byte, null);
				case Library.LibraryModule.Exports.Int8_From:
					return new Library.NumberAliasTypes.FromIntType(host, TypeCode.SByte, null);
				case Library.LibraryModule.Exports.Int16_From:
					return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Int16, null);
				case Library.LibraryModule.Exports.Int32_From:
					return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Int32, null);
				case Library.LibraryModule.Exports.Int64_From:
					return new Library.NumberAliasTypes.FromIntType(host, TypeCode.Int64, null);
				case Library.LibraryModule.Exports.Single_From:
					return new Library.NumberAliasTypes.FromNonIntType(host, TypeCode.Single, null);
				case Library.LibraryModule.Exports.Double_From:
					return new Library.NumberAliasTypes.FromNonIntType(host, TypeCode.Double, null);
				case Library.LibraryModule.Exports.Decimal_From:
					return new Library.NumberAliasTypes.FromNonIntType(host, TypeCode.Decimal, null);
				case Library.LibraryModule.Exports.Currency_From:
					return new Library.Currency.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.Percentage_From:
					return new Library.Percentage.FromFunctionValue(host, null);
				case Library.LibraryModule.Exports.Guid_From:
					return Library._Guid.From;
				case Library.LibraryModule.Exports.Date_Type:
					return TypeValue.Date;
				case Library.LibraryModule.Exports.DateTime_Type:
					return TypeValue.DateTime;
				case Library.LibraryModule.Exports.DateTimeZone_Type:
					return TypeValue.DateTimeZone;
				case Library.LibraryModule.Exports.Time_Type:
					return TypeValue.Time;
				case Library.LibraryModule.Exports.PercentileMode_Type:
					return Library.PercentileModeEnum.Type;
				case Library.LibraryModule.Exports.PercentileMode_ExcelInc:
					return Library.PercentileModeEnum.ExcelInc;
				case Library.LibraryModule.Exports.PercentileMode_ExcelExc:
					return Library.PercentileModeEnum.ExcelExc;
				case Library.LibraryModule.Exports.PercentileMode_SqlDisc:
					return Library.PercentileModeEnum.SqlDisc;
				case Library.LibraryModule.Exports.PercentileMode_SqlCont:
					return Library.PercentileModeEnum.SqlCont;
				case Library.LibraryModule.Exports.TimeZone_Current:
					return TimeZoneHelpers.GetDefaultTimeZoneName(host);
				case Library.LibraryModule.Exports.BufferMode_Type:
					return Library.BufferModeEnum.Type;
				case Library.LibraryModule.Exports.BufferMode_Eager:
					return Library.BufferModeEnum.Eager;
				case Library.LibraryModule.Exports.BufferMode_Delayed:
					return Library.BufferModeEnum.Delayed;
				case Library.LibraryModule.Exports.Progress_DataSourceProgress:
					return new Library.Progress.DataSourceProgressFunctionValue(host);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060082EA RID: 33514 RVA: 0x001BE0A4 File Offset: 0x001BC2A4
			private static Value GetSectionValue(Library.LibraryModule.SectionExports value, IEngineHost host)
			{
				switch (value)
				{
				case Library.LibraryModule.SectionExports.UICulture_GetString:
					return Library.UICulture.GetString;
				case Library.LibraryModule.SectionExports.List_Normalize:
					return LinesModule.List.Normalize;
				case Library.LibraryModule.SectionExports.Enumerator_FromList:
					return Library.Enumerator.FromList;
				case Library.LibraryModule.SectionExports.Enumerator_ToList:
					return Library.Enumerator.ToList;
				case Library.LibraryModule.SectionExports.Type_Kind:
					return Library.Type.Kind;
				case Library.LibraryModule.SectionExports.Type_Name:
					return Library.Type.Name;
				case Library.LibraryModule.SectionExports.Binary_FromHandlers:
					return new Library.Binary.FromHandlersFunctionValue(host);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002343 RID: 9027
			// (get) Token: 0x060082EB RID: 33515 RVA: 0x001BE105 File Offset: 0x001BC305
			public override string Name
			{
				get
				{
					return "LibraryModule";
				}
			}

			// Token: 0x17002344 RID: 9028
			// (get) Token: 0x060082EC RID: 33516 RVA: 0x001BE10C File Offset: 0x001BC30C
			public override Keys ExportKeys
			{
				get
				{
					if (this.exportKeys == null)
					{
						this.exportKeys = Keys.New(410, (int index) => Library.LibraryModule.GetKey((Library.LibraryModule.Exports)index));
					}
					return this.exportKeys;
				}
			}

			// Token: 0x17002345 RID: 9029
			// (get) Token: 0x060082ED RID: 33517 RVA: 0x001BE14B File Offset: 0x001BC34B
			public override Keys SectionKeys
			{
				get
				{
					if (this.sectionKeys == null)
					{
						this.sectionKeys = Keys.New(7, (int index) => Library.LibraryModule.GetSectionKey((Library.LibraryModule.SectionExports)index));
					}
					return this.sectionKeys;
				}
			}

			// Token: 0x060082EE RID: 33518 RVA: 0x001BE188 File Offset: 0x001BC388
			protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
			{
				return new Library.LibraryModule.ModuleRecordValue(RecordValue.New(this.ExportKeys, (int index) => Library.LibraryModule.GetValue((Library.LibraryModule.Exports)index, hostEnvironment)));
			}

			// Token: 0x060082EF RID: 33519 RVA: 0x001BE1C0 File Offset: 0x001BC3C0
			protected override RecordValue GetSectionExports(RecordValue environment, IEngineHost hostEnvironment)
			{
				return new Library.LibraryModule.ModuleRecordValue(RecordValue.New(this.SectionKeys, (int index) => Library.LibraryModule.GetSectionValue((Library.LibraryModule.SectionExports)index, hostEnvironment)));
			}

			// Token: 0x04004739 RID: 18233
			private Keys exportKeys;

			// Token: 0x0400473A RID: 18234
			private Keys sectionKeys;

			// Token: 0x02001370 RID: 4976
			private enum Exports
			{
				// Token: 0x0400473C RID: 18236
				Any_Type,
				// Token: 0x0400473D RID: 18237
				None_Type,
				// Token: 0x0400473E RID: 18238
				Expression_Constant,
				// Token: 0x0400473F RID: 18239
				Expression_Evaluate,
				// Token: 0x04004740 RID: 18240
				Expression_Identifier,
				// Token: 0x04004741 RID: 18241
				TextEncoding_Type,
				// Token: 0x04004742 RID: 18242
				TextEncoding_Utf8,
				// Token: 0x04004743 RID: 18243
				TextEncoding_Utf16,
				// Token: 0x04004744 RID: 18244
				TextEncoding_Ascii,
				// Token: 0x04004745 RID: 18245
				TextEncoding_Unicode,
				// Token: 0x04004746 RID: 18246
				TextEncoding_BigEndianUnicode,
				// Token: 0x04004747 RID: 18247
				TextEncoding_Windows,
				// Token: 0x04004748 RID: 18248
				Culture_Current,
				// Token: 0x04004749 RID: 18249
				Day_Type,
				// Token: 0x0400474A RID: 18250
				Day_Sunday,
				// Token: 0x0400474B RID: 18251
				Day_Monday,
				// Token: 0x0400474C RID: 18252
				Day_Tuesday,
				// Token: 0x0400474D RID: 18253
				Day_Wednesday,
				// Token: 0x0400474E RID: 18254
				Day_Thursday,
				// Token: 0x0400474F RID: 18255
				Day_Friday,
				// Token: 0x04004750 RID: 18256
				Day_Saturday,
				// Token: 0x04004751 RID: 18257
				Duration_Type,
				// Token: 0x04004752 RID: 18258
				Duration_FromText,
				// Token: 0x04004753 RID: 18259
				Duration_From,
				// Token: 0x04004754 RID: 18260
				Duration_ToText,
				// Token: 0x04004755 RID: 18261
				Duration_ToRecord,
				// Token: 0x04004756 RID: 18262
				Duration_Days,
				// Token: 0x04004757 RID: 18263
				Duration_Hours,
				// Token: 0x04004758 RID: 18264
				Duration_Minutes,
				// Token: 0x04004759 RID: 18265
				Duration_Seconds,
				// Token: 0x0400475A RID: 18266
				Duration_TotalDays,
				// Token: 0x0400475B RID: 18267
				Duration_TotalHours,
				// Token: 0x0400475C RID: 18268
				Duration_TotalMinutes,
				// Token: 0x0400475D RID: 18269
				Duration_TotalSeconds,
				// Token: 0x0400475E RID: 18270
				JoinKind_Type,
				// Token: 0x0400475F RID: 18271
				JoinKind_Inner,
				// Token: 0x04004760 RID: 18272
				JoinKind_LeftOuter,
				// Token: 0x04004761 RID: 18273
				JoinKind_RightOuter,
				// Token: 0x04004762 RID: 18274
				JoinKind_FullOuter,
				// Token: 0x04004763 RID: 18275
				JoinKind_LeftAnti,
				// Token: 0x04004764 RID: 18276
				JoinKind_RightAnti,
				// Token: 0x04004765 RID: 18277
				JoinKind_LeftSemi,
				// Token: 0x04004766 RID: 18278
				JoinKind_RightSemi,
				// Token: 0x04004767 RID: 18279
				MissingField_Type,
				// Token: 0x04004768 RID: 18280
				MissingField_Error,
				// Token: 0x04004769 RID: 18281
				MissingField_Ignore,
				// Token: 0x0400476A RID: 18282
				MissingField_UseNull,
				// Token: 0x0400476B RID: 18283
				GroupKind_Type,
				// Token: 0x0400476C RID: 18284
				GroupKind_Global,
				// Token: 0x0400476D RID: 18285
				GroupKind_Local,
				// Token: 0x0400476E RID: 18286
				Number_E,
				// Token: 0x0400476F RID: 18287
				Number_PI,
				// Token: 0x04004770 RID: 18288
				RoundingMode_Type,
				// Token: 0x04004771 RID: 18289
				RoundingMode_Up,
				// Token: 0x04004772 RID: 18290
				RoundingMode_Down,
				// Token: 0x04004773 RID: 18291
				RoundingMode_AwayFromZero,
				// Token: 0x04004774 RID: 18292
				RoundingMode_TowardZero,
				// Token: 0x04004775 RID: 18293
				RoundingMode_ToEven,
				// Token: 0x04004776 RID: 18294
				Record_Type,
				// Token: 0x04004777 RID: 18295
				Record_AddField,
				// Token: 0x04004778 RID: 18296
				Record_Field,
				// Token: 0x04004779 RID: 18297
				Record_FieldCount,
				// Token: 0x0400477A RID: 18298
				Record_FieldNames,
				// Token: 0x0400477B RID: 18299
				Record_FieldOrDefault,
				// Token: 0x0400477C RID: 18300
				Record_FieldValues,
				// Token: 0x0400477D RID: 18301
				Record_FromTable,
				// Token: 0x0400477E RID: 18302
				Record_HasFields,
				// Token: 0x0400477F RID: 18303
				Record_RemoveFields,
				// Token: 0x04004780 RID: 18304
				Record_RenameFields,
				// Token: 0x04004781 RID: 18305
				Record_ReorderFields,
				// Token: 0x04004782 RID: 18306
				Record_SelectFields,
				// Token: 0x04004783 RID: 18307
				Record_ToTable,
				// Token: 0x04004784 RID: 18308
				Record_TransformFields,
				// Token: 0x04004785 RID: 18309
				Record_Combine,
				// Token: 0x04004786 RID: 18310
				Record_FromList,
				// Token: 0x04004787 RID: 18311
				Record_ToList,
				// Token: 0x04004788 RID: 18312
				Precision_Type,
				// Token: 0x04004789 RID: 18313
				Precision_Double,
				// Token: 0x0400478A RID: 18314
				Precision_Decimal,
				// Token: 0x0400478B RID: 18315
				Number_Type,
				// Token: 0x0400478C RID: 18316
				Number_From,
				// Token: 0x0400478D RID: 18317
				Number_FromText,
				// Token: 0x0400478E RID: 18318
				Number_ToText,
				// Token: 0x0400478F RID: 18319
				Number_IsNaN,
				// Token: 0x04004790 RID: 18320
				Number_NaN,
				// Token: 0x04004791 RID: 18321
				Number_NegativeInfinity,
				// Token: 0x04004792 RID: 18322
				Number_PositiveInfinity,
				// Token: 0x04004793 RID: 18323
				Number_Epsilon,
				// Token: 0x04004794 RID: 18324
				Number_BitwiseNot,
				// Token: 0x04004795 RID: 18325
				Number_BitwiseOr,
				// Token: 0x04004796 RID: 18326
				Number_BitwiseAnd,
				// Token: 0x04004797 RID: 18327
				Number_BitwiseXor,
				// Token: 0x04004798 RID: 18328
				Number_BitwiseShiftLeft,
				// Token: 0x04004799 RID: 18329
				Number_BitwiseShiftRight,
				// Token: 0x0400479A RID: 18330
				BinaryEncoding_Type,
				// Token: 0x0400479B RID: 18331
				BinaryEncoding_Hex,
				// Token: 0x0400479C RID: 18332
				BinaryEncoding_Base64,
				// Token: 0x0400479D RID: 18333
				Binary_ApproximateLength,
				// Token: 0x0400479E RID: 18334
				Binary_Type,
				// Token: 0x0400479F RID: 18335
				Binary_ToText,
				// Token: 0x040047A0 RID: 18336
				Binary_From,
				// Token: 0x040047A1 RID: 18337
				Binary_FromText,
				// Token: 0x040047A2 RID: 18338
				Binary_ToList,
				// Token: 0x040047A3 RID: 18339
				Binary_FromList,
				// Token: 0x040047A4 RID: 18340
				Binary_Combine,
				// Token: 0x040047A5 RID: 18341
				Binary_Length,
				// Token: 0x040047A6 RID: 18342
				Binary_Buffer,
				// Token: 0x040047A7 RID: 18343
				Binary_Compress,
				// Token: 0x040047A8 RID: 18344
				Binary_Decompress,
				// Token: 0x040047A9 RID: 18345
				Binary_InferContentType,
				// Token: 0x040047AA RID: 18346
				Binary_Range,
				// Token: 0x040047AB RID: 18347
				Binary_Split,
				// Token: 0x040047AC RID: 18348
				Compression_Type,
				// Token: 0x040047AD RID: 18349
				Compression_None,
				// Token: 0x040047AE RID: 18350
				Compression_GZip,
				// Token: 0x040047AF RID: 18351
				Compression_Deflate,
				// Token: 0x040047B0 RID: 18352
				Compression_Snappy,
				// Token: 0x040047B1 RID: 18353
				Compression_Brotli,
				// Token: 0x040047B2 RID: 18354
				Compression_LZ4,
				// Token: 0x040047B3 RID: 18355
				Compression_Zstandard,
				// Token: 0x040047B4 RID: 18356
				Byte_Type,
				// Token: 0x040047B5 RID: 18357
				Character_Type,
				// Token: 0x040047B6 RID: 18358
				Character_FromNumber,
				// Token: 0x040047B7 RID: 18359
				Character_ToNumber,
				// Token: 0x040047B8 RID: 18360
				Text_Type,
				// Token: 0x040047B9 RID: 18361
				Text_At,
				// Token: 0x040047BA RID: 18362
				Text_From,
				// Token: 0x040047BB RID: 18363
				Text_Length,
				// Token: 0x040047BC RID: 18364
				Text_Range,
				// Token: 0x040047BD RID: 18365
				Text_Middle,
				// Token: 0x040047BE RID: 18366
				Text_Start,
				// Token: 0x040047BF RID: 18367
				Text_End,
				// Token: 0x040047C0 RID: 18368
				Text_StartsWith,
				// Token: 0x040047C1 RID: 18369
				Text_EndsWith,
				// Token: 0x040047C2 RID: 18370
				Text_Contains,
				// Token: 0x040047C3 RID: 18371
				Text_Clean,
				// Token: 0x040047C4 RID: 18372
				Text_PositionOf,
				// Token: 0x040047C5 RID: 18373
				Text_PositionOfAny,
				// Token: 0x040047C6 RID: 18374
				Text_Lower,
				// Token: 0x040047C7 RID: 18375
				Text_Upper,
				// Token: 0x040047C8 RID: 18376
				Text_Proper,
				// Token: 0x040047C9 RID: 18377
				Text_Split,
				// Token: 0x040047CA RID: 18378
				Text_SplitAny,
				// Token: 0x040047CB RID: 18379
				Text_Combine,
				// Token: 0x040047CC RID: 18380
				Text_Repeat,
				// Token: 0x040047CD RID: 18381
				Text_Replace,
				// Token: 0x040047CE RID: 18382
				Text_ReplaceRange,
				// Token: 0x040047CF RID: 18383
				Text_Insert,
				// Token: 0x040047D0 RID: 18384
				Text_Remove,
				// Token: 0x040047D1 RID: 18385
				Text_RemoveRange,
				// Token: 0x040047D2 RID: 18386
				Text_Reverse,
				// Token: 0x040047D3 RID: 18387
				Text_Select,
				// Token: 0x040047D4 RID: 18388
				Text_Trim,
				// Token: 0x040047D5 RID: 18389
				Text_TrimStart,
				// Token: 0x040047D6 RID: 18390
				Text_TrimEnd,
				// Token: 0x040047D7 RID: 18391
				Text_PadStart,
				// Token: 0x040047D8 RID: 18392
				Text_PadEnd,
				// Token: 0x040047D9 RID: 18393
				Text_ToBinary,
				// Token: 0x040047DA RID: 18394
				Text_ToList,
				// Token: 0x040047DB RID: 18395
				Text_FromBinary,
				// Token: 0x040047DC RID: 18396
				Text_NewGuid,
				// Token: 0x040047DD RID: 18397
				Text_InferNumberType,
				// Token: 0x040047DE RID: 18398
				Text_Format,
				// Token: 0x040047DF RID: 18399
				Comparer_FromCulture,
				// Token: 0x040047E0 RID: 18400
				Comparer_Ordinal,
				// Token: 0x040047E1 RID: 18401
				Comparer_OrdinalIgnoreCase,
				// Token: 0x040047E2 RID: 18402
				Comparer_Equals,
				// Token: 0x040047E3 RID: 18403
				Date_FromText,
				// Token: 0x040047E4 RID: 18404
				Date_From,
				// Token: 0x040047E5 RID: 18405
				Date_ToText,
				// Token: 0x040047E6 RID: 18406
				Date_ToRecord,
				// Token: 0x040047E7 RID: 18407
				Date_Year,
				// Token: 0x040047E8 RID: 18408
				Date_Month,
				// Token: 0x040047E9 RID: 18409
				Date_Day,
				// Token: 0x040047EA RID: 18410
				Date_AddDays,
				// Token: 0x040047EB RID: 18411
				Date_AddWeeks,
				// Token: 0x040047EC RID: 18412
				Date_AddMonths,
				// Token: 0x040047ED RID: 18413
				Date_AddQuarters,
				// Token: 0x040047EE RID: 18414
				Date_AddYears,
				// Token: 0x040047EF RID: 18415
				Date_IsLeapYear,
				// Token: 0x040047F0 RID: 18416
				Date_StartOfYear,
				// Token: 0x040047F1 RID: 18417
				Date_StartOfQuarter,
				// Token: 0x040047F2 RID: 18418
				Date_StartOfMonth,
				// Token: 0x040047F3 RID: 18419
				Date_StartOfWeek,
				// Token: 0x040047F4 RID: 18420
				Date_StartOfDay,
				// Token: 0x040047F5 RID: 18421
				Date_EndOfYear,
				// Token: 0x040047F6 RID: 18422
				Date_EndOfQuarter,
				// Token: 0x040047F7 RID: 18423
				Date_EndOfMonth,
				// Token: 0x040047F8 RID: 18424
				Date_EndOfWeek,
				// Token: 0x040047F9 RID: 18425
				Date_EndOfDay,
				// Token: 0x040047FA RID: 18426
				Date_DayOfWeek,
				// Token: 0x040047FB RID: 18427
				Date_DayOfYear,
				// Token: 0x040047FC RID: 18428
				Date_DaysInMonth,
				// Token: 0x040047FD RID: 18429
				Date_QuarterOfYear,
				// Token: 0x040047FE RID: 18430
				Date_WeekOfMonth,
				// Token: 0x040047FF RID: 18431
				Date_WeekOfYear,
				// Token: 0x04004800 RID: 18432
				DateTime_FromText,
				// Token: 0x04004801 RID: 18433
				DateTime_From,
				// Token: 0x04004802 RID: 18434
				DateTime_ToText,
				// Token: 0x04004803 RID: 18435
				DateTime_ToRecord,
				// Token: 0x04004804 RID: 18436
				DateTime_Date,
				// Token: 0x04004805 RID: 18437
				DateTime_Time,
				// Token: 0x04004806 RID: 18438
				DateTime_AddZone,
				// Token: 0x04004807 RID: 18439
				DateTime_LocalNow,
				// Token: 0x04004808 RID: 18440
				DateTime_FixedLocalNow,
				// Token: 0x04004809 RID: 18441
				DateTime_FromFileTime,
				// Token: 0x0400480A RID: 18442
				DateTimeZone_FromText,
				// Token: 0x0400480B RID: 18443
				DateTimeZone_From,
				// Token: 0x0400480C RID: 18444
				DateTimeZone_ToText,
				// Token: 0x0400480D RID: 18445
				DateTimeZone_ToRecord,
				// Token: 0x0400480E RID: 18446
				DateTimeZone_TimezoneHours,
				// Token: 0x0400480F RID: 18447
				DateTimeZone_TimezoneMinutes,
				// Token: 0x04004810 RID: 18448
				DateTimeZone_LocalNow,
				// Token: 0x04004811 RID: 18449
				DateTimeZone_UtcNow,
				// Token: 0x04004812 RID: 18450
				DateTimeZone_FixedLocalNow,
				// Token: 0x04004813 RID: 18451
				DateTimeZone_FixedUtcNow,
				// Token: 0x04004814 RID: 18452
				DateTimeZone_ToLocal,
				// Token: 0x04004815 RID: 18453
				DateTimeZone_ToUtc,
				// Token: 0x04004816 RID: 18454
				DateTimeZone_SwitchTimezone,
				// Token: 0x04004817 RID: 18455
				DateTimeZone_RemoveTimezone,
				// Token: 0x04004818 RID: 18456
				DateTimeZone_FromFileTime,
				// Token: 0x04004819 RID: 18457
				Time_FromText,
				// Token: 0x0400481A RID: 18458
				Time_From,
				// Token: 0x0400481B RID: 18459
				Time_ToText,
				// Token: 0x0400481C RID: 18460
				Time_ToRecord,
				// Token: 0x0400481D RID: 18461
				Time_Hour,
				// Token: 0x0400481E RID: 18462
				Time_Minute,
				// Token: 0x0400481F RID: 18463
				Time_Second,
				// Token: 0x04004820 RID: 18464
				Time_StartOfHour,
				// Token: 0x04004821 RID: 18465
				Time_EndOfHour,
				// Token: 0x04004822 RID: 18466
				Function_Type,
				// Token: 0x04004823 RID: 18467
				Function_From,
				// Token: 0x04004824 RID: 18468
				Function_Invoke,
				// Token: 0x04004825 RID: 18469
				Function_InvokeAfter,
				// Token: 0x04004826 RID: 18470
				Function_IsDataSource,
				// Token: 0x04004827 RID: 18471
				Function_ScalarVector,
				// Token: 0x04004828 RID: 18472
				Null_Type,
				// Token: 0x04004829 RID: 18473
				Number_Abs,
				// Token: 0x0400482A RID: 18474
				Number_Acos,
				// Token: 0x0400482B RID: 18475
				Number_Asin,
				// Token: 0x0400482C RID: 18476
				Number_Atan,
				// Token: 0x0400482D RID: 18477
				Number_Atan2,
				// Token: 0x0400482E RID: 18478
				Number_Combinations,
				// Token: 0x0400482F RID: 18479
				Number_Cos,
				// Token: 0x04004830 RID: 18480
				Number_Cosh,
				// Token: 0x04004831 RID: 18481
				Number_Exp,
				// Token: 0x04004832 RID: 18482
				Number_Factorial,
				// Token: 0x04004833 RID: 18483
				Number_IntegerDivide,
				// Token: 0x04004834 RID: 18484
				Number_Log,
				// Token: 0x04004835 RID: 18485
				Number_Log10,
				// Token: 0x04004836 RID: 18486
				Number_Ln,
				// Token: 0x04004837 RID: 18487
				Number_Mod,
				// Token: 0x04004838 RID: 18488
				Number_Permutations,
				// Token: 0x04004839 RID: 18489
				Number_Power,
				// Token: 0x0400483A RID: 18490
				Number_Random,
				// Token: 0x0400483B RID: 18491
				Number_RandomBetween,
				// Token: 0x0400483C RID: 18492
				Number_Round,
				// Token: 0x0400483D RID: 18493
				Number_RoundDown,
				// Token: 0x0400483E RID: 18494
				Number_RoundUp,
				// Token: 0x0400483F RID: 18495
				Number_RoundTowardZero,
				// Token: 0x04004840 RID: 18496
				Number_RoundAwayFromZero,
				// Token: 0x04004841 RID: 18497
				Number_Sign,
				// Token: 0x04004842 RID: 18498
				Number_Sin,
				// Token: 0x04004843 RID: 18499
				Number_Sinh,
				// Token: 0x04004844 RID: 18500
				Number_Sqrt,
				// Token: 0x04004845 RID: 18501
				Number_Tan,
				// Token: 0x04004846 RID: 18502
				Number_Tanh,
				// Token: 0x04004847 RID: 18503
				Number_IsEven,
				// Token: 0x04004848 RID: 18504
				Number_IsOdd,
				// Token: 0x04004849 RID: 18505
				List_Type,
				// Token: 0x0400484A RID: 18506
				List_Contains,
				// Token: 0x0400484B RID: 18507
				List_Difference,
				// Token: 0x0400484C RID: 18508
				List_First,
				// Token: 0x0400484D RID: 18509
				List_Generate,
				// Token: 0x0400484E RID: 18510
				List_Intersect,
				// Token: 0x0400484F RID: 18511
				List_IsDistinct,
				// Token: 0x04004850 RID: 18512
				List_Last,
				// Token: 0x04004851 RID: 18513
				List_RemoveMatchingItems,
				// Token: 0x04004852 RID: 18514
				List_RemoveNulls,
				// Token: 0x04004853 RID: 18515
				List_Repeat,
				// Token: 0x04004854 RID: 18516
				List_ReplaceMatchingItems,
				// Token: 0x04004855 RID: 18517
				List_Reverse,
				// Token: 0x04004856 RID: 18518
				List_Single,
				// Token: 0x04004857 RID: 18519
				List_SingleOrDefault,
				// Token: 0x04004858 RID: 18520
				List_Union,
				// Token: 0x04004859 RID: 18521
				List_Accumulate,
				// Token: 0x0400485A RID: 18522
				List_Buffer,
				// Token: 0x0400485B RID: 18523
				List_Combine,
				// Token: 0x0400485C RID: 18524
				List_ContainsAll,
				// Token: 0x0400485D RID: 18525
				List_ContainsAny,
				// Token: 0x0400485E RID: 18526
				List_InsertRange,
				// Token: 0x0400485F RID: 18527
				List_Max,
				// Token: 0x04004860 RID: 18528
				List_MaxN,
				// Token: 0x04004861 RID: 18529
				List_Min,
				// Token: 0x04004862 RID: 18530
				List_MinN,
				// Token: 0x04004863 RID: 18531
				List_PositionOf,
				// Token: 0x04004864 RID: 18532
				List_PositionOfAny,
				// Token: 0x04004865 RID: 18533
				List_Positions,
				// Token: 0x04004866 RID: 18534
				List_RemoveRange,
				// Token: 0x04004867 RID: 18535
				List_ReplaceRange,
				// Token: 0x04004868 RID: 18536
				List_Alternate,
				// Token: 0x04004869 RID: 18537
				List_Zip,
				// Token: 0x0400486A RID: 18538
				List_Split,
				// Token: 0x0400486B RID: 18539
				List_Average,
				// Token: 0x0400486C RID: 18540
				List_Covariance,
				// Token: 0x0400486D RID: 18541
				List_Median,
				// Token: 0x0400486E RID: 18542
				List_Mode,
				// Token: 0x0400486F RID: 18543
				List_Modes,
				// Token: 0x04004870 RID: 18544
				List_Percentile,
				// Token: 0x04004871 RID: 18545
				List_Product,
				// Token: 0x04004872 RID: 18546
				List_Sum,
				// Token: 0x04004873 RID: 18547
				List_StandardDeviation,
				// Token: 0x04004874 RID: 18548
				List_Numbers,
				// Token: 0x04004875 RID: 18549
				List_Times,
				// Token: 0x04004876 RID: 18550
				List_Dates,
				// Token: 0x04004877 RID: 18551
				List_DateTimes,
				// Token: 0x04004878 RID: 18552
				List_DateTimeZones,
				// Token: 0x04004879 RID: 18553
				List_Durations,
				// Token: 0x0400487A RID: 18554
				List_Random,
				// Token: 0x0400487B RID: 18555
				_Error_Record,
				// Token: 0x0400487C RID: 18556
				_Value_Equals,
				// Token: 0x0400487D RID: 18557
				_Value_NullableEquals,
				// Token: 0x0400487E RID: 18558
				_Value_Compare,
				// Token: 0x0400487F RID: 18559
				_Value_Type,
				// Token: 0x04004880 RID: 18560
				_Value_ReplaceType,
				// Token: 0x04004881 RID: 18561
				_Value_RemoveMetadata,
				// Token: 0x04004882 RID: 18562
				_Value_ReplaceMetadata,
				// Token: 0x04004883 RID: 18563
				_Value_Metadata,
				// Token: 0x04004884 RID: 18564
				_Value_FromText,
				// Token: 0x04004885 RID: 18565
				_Value_Add,
				// Token: 0x04004886 RID: 18566
				_Value_Subtract,
				// Token: 0x04004887 RID: 18567
				_Value_Multiply,
				// Token: 0x04004888 RID: 18568
				_Value_Divide,
				// Token: 0x04004889 RID: 18569
				_Value_As,
				// Token: 0x0400488A RID: 18570
				_Value_Is,
				// Token: 0x0400488B RID: 18571
				_Value_NativeQuery,
				// Token: 0x0400488C RID: 18572
				_Value_Expression,
				// Token: 0x0400488D RID: 18573
				_Value_Optimize,
				// Token: 0x0400488E RID: 18574
				_Value_Alternates,
				// Token: 0x0400488F RID: 18575
				_Value_Versions,
				// Token: 0x04004890 RID: 18576
				_Value_VersionIdentity,
				// Token: 0x04004891 RID: 18577
				_Value_ViewFunction,
				// Token: 0x04004892 RID: 18578
				_Value_ViewError,
				// Token: 0x04004893 RID: 18579
				Type_Type,
				// Token: 0x04004894 RID: 18580
				Type_ForRecord,
				// Token: 0x04004895 RID: 18581
				Type_ForFunction,
				// Token: 0x04004896 RID: 18582
				Type_NonNullable,
				// Token: 0x04004897 RID: 18583
				Type_IsNullable,
				// Token: 0x04004898 RID: 18584
				Type_ListItem,
				// Token: 0x04004899 RID: 18585
				Type_OpenRecord,
				// Token: 0x0400489A RID: 18586
				Type_ClosedRecord,
				// Token: 0x0400489B RID: 18587
				Type_IsOpenRecord,
				// Token: 0x0400489C RID: 18588
				Type_RecordFields,
				// Token: 0x0400489D RID: 18589
				Type_FunctionParameters,
				// Token: 0x0400489E RID: 18590
				Type_FunctionRequiredParameters,
				// Token: 0x0400489F RID: 18591
				Type_FunctionReturn,
				// Token: 0x040048A0 RID: 18592
				Type_Is,
				// Token: 0x040048A1 RID: 18593
				Type_Union,
				// Token: 0x040048A2 RID: 18594
				Type_Facets,
				// Token: 0x040048A3 RID: 18595
				Type_ReplaceFacets,
				// Token: 0x040048A4 RID: 18596
				Logical_Type,
				// Token: 0x040048A5 RID: 18597
				Logical_FromText,
				// Token: 0x040048A6 RID: 18598
				Logical_From,
				// Token: 0x040048A7 RID: 18599
				Logical_ToText,
				// Token: 0x040048A8 RID: 18600
				List_AllTrue,
				// Token: 0x040048A9 RID: 18601
				List_AnyTrue,
				// Token: 0x040048AA RID: 18602
				Order_Type,
				// Token: 0x040048AB RID: 18603
				Order_Ascending,
				// Token: 0x040048AC RID: 18604
				Order_Descending,
				// Token: 0x040048AD RID: 18605
				Occurrence_Type,
				// Token: 0x040048AE RID: 18606
				Occurrence_All,
				// Token: 0x040048AF RID: 18607
				Occurrence_First,
				// Token: 0x040048B0 RID: 18608
				Occurrence_Last,
				// Token: 0x040048B1 RID: 18609
				Int8_Type,
				// Token: 0x040048B2 RID: 18610
				Int16_Type,
				// Token: 0x040048B3 RID: 18611
				Int32_Type,
				// Token: 0x040048B4 RID: 18612
				Int64_Type,
				// Token: 0x040048B5 RID: 18613
				Single_Type,
				// Token: 0x040048B6 RID: 18614
				Double_Type,
				// Token: 0x040048B7 RID: 18615
				Decimal_Type,
				// Token: 0x040048B8 RID: 18616
				Currency_Type,
				// Token: 0x040048B9 RID: 18617
				Percentage_Type,
				// Token: 0x040048BA RID: 18618
				Guid_Type,
				// Token: 0x040048BB RID: 18619
				Uri_Type,
				// Token: 0x040048BC RID: 18620
				Password_Type,
				// Token: 0x040048BD RID: 18621
				Byte_From,
				// Token: 0x040048BE RID: 18622
				Int8_From,
				// Token: 0x040048BF RID: 18623
				Int16_From,
				// Token: 0x040048C0 RID: 18624
				Int32_From,
				// Token: 0x040048C1 RID: 18625
				Int64_From,
				// Token: 0x040048C2 RID: 18626
				Single_From,
				// Token: 0x040048C3 RID: 18627
				Double_From,
				// Token: 0x040048C4 RID: 18628
				Decimal_From,
				// Token: 0x040048C5 RID: 18629
				Currency_From,
				// Token: 0x040048C6 RID: 18630
				Percentage_From,
				// Token: 0x040048C7 RID: 18631
				Guid_From,
				// Token: 0x040048C8 RID: 18632
				Date_Type,
				// Token: 0x040048C9 RID: 18633
				DateTime_Type,
				// Token: 0x040048CA RID: 18634
				DateTimeZone_Type,
				// Token: 0x040048CB RID: 18635
				Time_Type,
				// Token: 0x040048CC RID: 18636
				PercentileMode_Type,
				// Token: 0x040048CD RID: 18637
				PercentileMode_ExcelInc,
				// Token: 0x040048CE RID: 18638
				PercentileMode_ExcelExc,
				// Token: 0x040048CF RID: 18639
				PercentileMode_SqlDisc,
				// Token: 0x040048D0 RID: 18640
				PercentileMode_SqlCont,
				// Token: 0x040048D1 RID: 18641
				TimeZone_Current,
				// Token: 0x040048D2 RID: 18642
				BufferMode_Type,
				// Token: 0x040048D3 RID: 18643
				BufferMode_Eager,
				// Token: 0x040048D4 RID: 18644
				BufferMode_Delayed,
				// Token: 0x040048D5 RID: 18645
				Progress_DataSourceProgress,
				// Token: 0x040048D6 RID: 18646
				Count
			}

			// Token: 0x02001371 RID: 4977
			private enum SectionExports
			{
				// Token: 0x040048D8 RID: 18648
				UICulture_GetString,
				// Token: 0x040048D9 RID: 18649
				List_Normalize,
				// Token: 0x040048DA RID: 18650
				Enumerator_FromList,
				// Token: 0x040048DB RID: 18651
				Enumerator_ToList,
				// Token: 0x040048DC RID: 18652
				Type_Kind,
				// Token: 0x040048DD RID: 18653
				Type_Name,
				// Token: 0x040048DE RID: 18654
				Binary_FromHandlers,
				// Token: 0x040048DF RID: 18655
				Count
			}

			// Token: 0x02001372 RID: 4978
			private class ModuleRecordValue : RecordValue
			{
				// Token: 0x060082F1 RID: 33521 RVA: 0x001BE1F6 File Offset: 0x001BC3F6
				public ModuleRecordValue(RecordValue record)
				{
					this.record = record;
				}

				// Token: 0x17002346 RID: 9030
				// (get) Token: 0x060082F2 RID: 33522 RVA: 0x001BE210 File Offset: 0x001BC410
				public override Keys Keys
				{
					get
					{
						return this.record.Keys;
					}
				}

				// Token: 0x17002347 RID: 9031
				// (get) Token: 0x060082F3 RID: 33523 RVA: 0x001BE21D File Offset: 0x001BC41D
				public override TypeValue Type
				{
					get
					{
						if (this.type == null)
						{
							this.type = RecordTypeValue.New(this.Keys);
						}
						return this.type;
					}
				}

				// Token: 0x17002348 RID: 9032
				public override Value this[int index]
				{
					get
					{
						object obj = this.syncRoot;
						Value value;
						lock (obj)
						{
							value = this.record[index];
						}
						return value;
					}
				}

				// Token: 0x060082F5 RID: 33525 RVA: 0x001BE288 File Offset: 0x001BC488
				public override IValueReference GetReference(int index)
				{
					object obj = this.syncRoot;
					IValueReference valueReference;
					lock (obj)
					{
						if (index >= this.Keys.Length)
						{
							throw ValueException.RecordIndexOutOfRange(index, this);
						}
						valueReference = new Library.LibraryModule.ModuleRecordValue.ModuleValueReference(this, index);
					}
					return valueReference;
				}

				// Token: 0x040048E0 RID: 18656
				private TypeValue type;

				// Token: 0x040048E1 RID: 18657
				private readonly RecordValue record;

				// Token: 0x040048E2 RID: 18658
				private readonly object syncRoot = new object();

				// Token: 0x02001373 RID: 4979
				private class ModuleValueReference : IValueReference
				{
					// Token: 0x060082F6 RID: 33526 RVA: 0x001BE2E4 File Offset: 0x001BC4E4
					public ModuleValueReference(Library.LibraryModule.ModuleRecordValue moduleRecord, int index)
					{
						this.moduleRecord = moduleRecord;
						this.reference = moduleRecord.record.GetReference(index);
					}

					// Token: 0x17002349 RID: 9033
					// (get) Token: 0x060082F7 RID: 33527 RVA: 0x001BE308 File Offset: 0x001BC508
					public bool Evaluated
					{
						get
						{
							Library.LibraryModule.ModuleRecordValue moduleRecordValue = this.moduleRecord;
							bool evaluated;
							lock (moduleRecordValue)
							{
								evaluated = this.reference.Evaluated;
							}
							return evaluated;
						}
					}

					// Token: 0x1700234A RID: 9034
					// (get) Token: 0x060082F8 RID: 33528 RVA: 0x001BE350 File Offset: 0x001BC550
					public Value Value
					{
						get
						{
							Library.LibraryModule.ModuleRecordValue moduleRecordValue = this.moduleRecord;
							Value value;
							lock (moduleRecordValue)
							{
								value = this.reference.Value;
							}
							return value;
						}
					}

					// Token: 0x040048E3 RID: 18659
					private readonly Library.LibraryModule.ModuleRecordValue moduleRecord;

					// Token: 0x040048E4 RID: 18660
					private readonly IValueReference reference;
				}
			}
		}

		// Token: 0x02001377 RID: 4983
		public static class Progress
		{
			// Token: 0x02001378 RID: 4984
			public sealed class DataSourceProgressFunctionValue : NativeFunctionValue0<Value>
			{
				// Token: 0x06008301 RID: 33537 RVA: 0x001BE3D0 File Offset: 0x001BC5D0
				public DataSourceProgressFunctionValue(IEngineHost host)
					: base(TypeValue.Any)
				{
					this.host = host;
				}

				// Token: 0x06008302 RID: 33538 RVA: 0x001BE3E4 File Offset: 0x001BC5E4
				public override Value TypedInvoke()
				{
					IGetDataSourceProgress getDataSourceProgress = this.host.QueryService<IGetDataSourceProgress>();
					ArrayBuilder<IValueReference> arrayBuilder = default(ArrayBuilder<IValueReference>);
					foreach (DataSourceProgress2 dataSourceProgress in getDataSourceProgress.GetDataSourceProgress())
					{
						arrayBuilder.Add(Library.Progress.DataSourceProgressFunctionValue.CreateDataSourceRow(dataSourceProgress));
					}
					return ListValue.New(arrayBuilder.ToArray()).ToTable();
				}

				// Token: 0x06008303 RID: 33539 RVA: 0x001BE45C File Offset: 0x001BC65C
				private static RecordValue CreateDataSourceRow(DataSourceProgress2 progress)
				{
					return RecordValue.New(Library.Progress.DataSourceProgressFunctionValue.dataSourceProgressKeys, new Value[]
					{
						TextValue.New(progress.DataSourceType),
						TextValue.New(progress.DataSource),
						NumberValue.New(progress.RowsRead),
						NumberValue.New(progress.RowsWritten),
						NumberValue.New(progress.BytesRead),
						NumberValue.New(progress.BytesWritten)
					});
				}

				// Token: 0x040048EA RID: 18666
				private static readonly Keys dataSourceProgressKeys = Keys.New(new string[] { "Data Source Type", "Data Source", "Rows Read", "Rows Written", "Bytes Read", "Bytes Written" });

				// Token: 0x040048EB RID: 18667
				private readonly IEngineHost host;
			}
		}

		// Token: 0x02001379 RID: 4985
		public static class Logical
		{
			// Token: 0x06008305 RID: 33541 RVA: 0x001BE520 File Offset: 0x001BC720
			private static LogicalValue ConvertFromText(TextValue text)
			{
				bool flag;
				if (bool.TryParse(text.String, out flag))
				{
					return LogicalValue.New(flag);
				}
				throw ValueException.NewExpressionError<Message0>(Strings.Logical_NotConvertibleToLogical, text, null);
			}

			// Token: 0x040048EC RID: 18668
			public static readonly FunctionValue From = new Library.Logical.FromFunctionValue();

			// Token: 0x040048ED RID: 18669
			public static readonly FunctionValue FromText = new Library.Logical.FromTextFunctionValue();

			// Token: 0x040048EE RID: 18670
			public static readonly FunctionValue ToText = new Library.Logical.ToTextFunctionValue();

			// Token: 0x040048EF RID: 18671
			public static readonly TextValue FalseText = TextValue.New("false");

			// Token: 0x040048F0 RID: 18672
			public static readonly TextValue TrueText = TextValue.New("true");

			// Token: 0x0200137A RID: 4986
			private class FromTextFunctionValue : NativeFunctionValue1
			{
				// Token: 0x06008307 RID: 33543 RVA: 0x001BE58D File Offset: 0x001BC78D
				public FromTextFunctionValue()
					: base("text")
				{
				}

				// Token: 0x1700234B RID: 9035
				// (get) Token: 0x06008308 RID: 33544 RVA: 0x001BE59A File Offset: 0x001BC79A
				protected override TypeValue Type0
				{
					get
					{
						return TypeValue.Text.Nullable;
					}
				}

				// Token: 0x1700234C RID: 9036
				// (get) Token: 0x06008309 RID: 33545 RVA: 0x001BE5A6 File Offset: 0x001BC7A6
				protected override TypeValue ReturnType
				{
					get
					{
						return TypeValue.Logical.Nullable;
					}
				}

				// Token: 0x0600830A RID: 33546 RVA: 0x001BE5B2 File Offset: 0x001BC7B2
				public override Value Invoke(Value text)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					return Library.Logical.ConvertFromText(text.AsText);
				}
			}

			// Token: 0x0200137B RID: 4987
			private class FromFunctionValue : NativeFunctionValue1
			{
				// Token: 0x0600830B RID: 33547 RVA: 0x0018D833 File Offset: 0x0018BA33
				public FromFunctionValue()
					: base("value")
				{
				}

				// Token: 0x1700234D RID: 9037
				// (get) Token: 0x0600830C RID: 33548 RVA: 0x001BE5CD File Offset: 0x001BC7CD
				protected override TypeValue Type0
				{
					get
					{
						return TypeValue.Any;
					}
				}

				// Token: 0x1700234E RID: 9038
				// (get) Token: 0x0600830D RID: 33549 RVA: 0x001BE5A6 File Offset: 0x001BC7A6
				protected override TypeValue ReturnType
				{
					get
					{
						return TypeValue.Logical.Nullable;
					}
				}

				// Token: 0x0600830E RID: 33550 RVA: 0x001BE5D4 File Offset: 0x001BC7D4
				public override Value Invoke(Value value)
				{
					ValueKind kind = value.Kind;
					if (kind == ValueKind.Null)
					{
						return value;
					}
					switch (kind)
					{
					case ValueKind.Number:
						if (!value.AsNumber.Equals(NumberValue.Zero))
						{
							return LogicalValue.True;
						}
						return LogicalValue.False;
					case ValueKind.Logical:
						return value;
					case ValueKind.Text:
						return Library.Logical.FromText.Invoke(value);
					default:
						throw ValueException.NewDataFormatError<Message0>(Strings.Logical_NotConvertibleToLogical, value, null);
					}
				}
			}

			// Token: 0x0200137C RID: 4988
			private class ToTextFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600830F RID: 33551 RVA: 0x001BE63B File Offset: 0x001BC83B
				public ToTextFunctionValue()
					: base(NullableTypeValue.Text, "logicalValue", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06008310 RID: 33552 RVA: 0x001BE652 File Offset: 0x001BC852
				public override Value TypedInvoke(Value logicalValue)
				{
					if (logicalValue.IsNull)
					{
						return Value.Null;
					}
					if (!logicalValue.AsLogical.Boolean)
					{
						return Library.Logical.FalseText;
					}
					return Library.Logical.TrueText;
				}
			}
		}

		// Token: 0x0200137D RID: 4989
		public static class Type
		{
			// Token: 0x040048F1 RID: 18673
			public static readonly FunctionValue ForList = new Library.Type.ForListFunctionValue();

			// Token: 0x040048F2 RID: 18674
			public static readonly FunctionValue ForNullable = new Library.Type.ForNullableFunctionValue();

			// Token: 0x040048F3 RID: 18675
			public static readonly FunctionValue ForRecord = new Library.Type.ForRecordFunctionValue();

			// Token: 0x040048F4 RID: 18676
			public static readonly FunctionValue FunctionFrom = new Library.Type.FunctionFromFunctionValue();

			// Token: 0x040048F5 RID: 18677
			public static readonly FunctionValue NonNullable = new Library.Type.NonNullableFunctionValue();

			// Token: 0x040048F6 RID: 18678
			public static readonly FunctionValue IsNullable = new Library.Type.IsNullableFunctionValue();

			// Token: 0x040048F7 RID: 18679
			public static readonly FunctionValue ListItem = new Library.Type.ListItemFunctionValue();

			// Token: 0x040048F8 RID: 18680
			public static readonly FunctionValue ClosedRecord = new Library.Type.ClosedRecordFunctionValue();

			// Token: 0x040048F9 RID: 18681
			public static readonly FunctionValue OpenRecord = new Library.Type.OpenRecordFunctionValue();

			// Token: 0x040048FA RID: 18682
			public static readonly FunctionValue IsOpenRecord = new Library.Type.IsOpenRecordFunctionValue();

			// Token: 0x040048FB RID: 18683
			public static readonly FunctionValue RecordFields = new Library.Type.RecordFieldsFunctionValue();

			// Token: 0x040048FC RID: 18684
			public static readonly FunctionValue FunctionParameters = new Library.Type.FunctionParametersFunctionValue();

			// Token: 0x040048FD RID: 18685
			public static readonly FunctionValue FunctionRequiredParameters = new Library.Type.FunctionRequiredParametersFunctionValue();

			// Token: 0x040048FE RID: 18686
			public static readonly FunctionValue FunctionReturn = new Library.Type.FunctionReturnFunctionValue();

			// Token: 0x040048FF RID: 18687
			public static readonly FunctionValue Is = new Library.Type.IsFunctionValue();

			// Token: 0x04004900 RID: 18688
			public static readonly FunctionValue Domain = new Library.Type.DomainFunctionValue();

			// Token: 0x04004901 RID: 18689
			public static readonly FunctionValue ReplaceDomain = new Library.Type.ReplaceDomainFunctionValue();

			// Token: 0x04004902 RID: 18690
			public static readonly FunctionValue Union = new Library.Type.UnionFunctionValue();

			// Token: 0x04004903 RID: 18691
			public static readonly FunctionValue Facets = new Library.Type.FacetsFunctionValue();

			// Token: 0x04004904 RID: 18692
			public static readonly FunctionValue ReplaceFacets = new Library.Type.ReplaceFacetsFunctionValue();

			// Token: 0x04004905 RID: 18693
			public static readonly FunctionValue Kind = new Library.Type.KindFunctionValue();

			// Token: 0x04004906 RID: 18694
			public static readonly FunctionValue Name = new Library.Type.NameFunctionValue();

			// Token: 0x0200137E RID: 4990
			private class ForListFunctionValue : NativeFunctionValue1<TypeValue, ListValue>
			{
				// Token: 0x06008312 RID: 33554 RVA: 0x001BE765 File Offset: 0x001BC965
				public ForListFunctionValue()
					: base(TypeValue._Type, "itemType", TypeValue.List)
				{
				}

				// Token: 0x06008313 RID: 33555 RVA: 0x001BE77C File Offset: 0x001BC97C
				public override TypeValue TypedInvoke(ListValue itemType)
				{
					return ListTypeValue.New(itemType);
				}
			}

			// Token: 0x0200137F RID: 4991
			private class ForNullableFunctionValue : NativeFunctionValue1<TypeValue, ListValue>
			{
				// Token: 0x06008314 RID: 33556 RVA: 0x001BE765 File Offset: 0x001BC965
				public ForNullableFunctionValue()
					: base(TypeValue._Type, "itemType", TypeValue.List)
				{
				}

				// Token: 0x06008315 RID: 33557 RVA: 0x001BE784 File Offset: 0x001BC984
				public override TypeValue TypedInvoke(ListValue itemType)
				{
					return itemType.Item0.AsType.Nullable;
				}
			}

			// Token: 0x02001380 RID: 4992
			private class ForRecordFunctionValue : NativeFunctionValue2<RecordTypeValue, RecordValue, LogicalValue>
			{
				// Token: 0x06008316 RID: 33558 RVA: 0x001BE796 File Offset: 0x001BC996
				public ForRecordFunctionValue()
					: base(TypeValue._Type, "fields", TypeValue.Record, "open", TypeValue.Logical)
				{
				}

				// Token: 0x06008317 RID: 33559 RVA: 0x001BE7B7 File Offset: 0x001BC9B7
				public override RecordTypeValue TypedInvoke(RecordValue fields, LogicalValue open)
				{
					return RecordTypeValue.New(fields, open.AsBoolean);
				}
			}

			// Token: 0x02001381 RID: 4993
			private class FunctionFromFunctionValue : NativeFunctionValue2<FunctionTypeValue, RecordValue, NumberValue>
			{
				// Token: 0x06008318 RID: 33560 RVA: 0x001BE7C5 File Offset: 0x001BC9C5
				public FunctionFromFunctionValue()
					: base(TypeValue._Type, "signature", TypeValue.Record, "min", TypeValue.Number)
				{
				}

				// Token: 0x06008319 RID: 33561 RVA: 0x001BE7E6 File Offset: 0x001BC9E6
				public override FunctionTypeValue TypedInvoke(RecordValue signature, NumberValue min)
				{
					return FunctionTypeValue.New(signature, min.AsInteger32);
				}
			}

			// Token: 0x02001382 RID: 4994
			private class NonNullableFunctionValue : NativeFunctionValue1<TypeValue, TypeValue>
			{
				// Token: 0x0600831A RID: 33562 RVA: 0x001BE7F4 File Offset: 0x001BC9F4
				public NonNullableFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600831B RID: 33563 RVA: 0x001BE80B File Offset: 0x001BCA0B
				public override TypeValue TypedInvoke(TypeValue type)
				{
					return type.NonNullable;
				}
			}

			// Token: 0x02001383 RID: 4995
			private class IsNullableFunctionValue : NativeFunctionValue1<LogicalValue, TypeValue>
			{
				// Token: 0x0600831C RID: 33564 RVA: 0x001BE813 File Offset: 0x001BCA13
				public IsNullableFunctionValue()
					: base(TypeValue.Logical, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600831D RID: 33565 RVA: 0x001BE82A File Offset: 0x001BCA2A
				public override LogicalValue TypedInvoke(TypeValue type)
				{
					return LogicalValue.New(type.IsNullable);
				}
			}

			// Token: 0x02001384 RID: 4996
			private class ListItemFunctionValue : NativeFunctionValue1<TypeValue, TypeValue>
			{
				// Token: 0x0600831E RID: 33566 RVA: 0x001BE7F4 File Offset: 0x001BC9F4
				public ListItemFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600831F RID: 33567 RVA: 0x001BE837 File Offset: 0x001BCA37
				public override TypeValue TypedInvoke(TypeValue type)
				{
					return type.AsListType.ItemType;
				}
			}

			// Token: 0x02001385 RID: 4997
			private class ClosedRecordFunctionValue : NativeFunctionValue1<TypeValue, TypeValue>
			{
				// Token: 0x06008320 RID: 33568 RVA: 0x001BE7F4 File Offset: 0x001BC9F4
				public ClosedRecordFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008321 RID: 33569 RVA: 0x001BE844 File Offset: 0x001BCA44
				public override TypeValue TypedInvoke(TypeValue type)
				{
					RecordTypeValue asRecordType = type.AsRecordType;
					if (!asRecordType.Open)
					{
						return asRecordType;
					}
					return RecordTypeValue.New(asRecordType.Fields, false);
				}
			}

			// Token: 0x02001386 RID: 4998
			private class OpenRecordFunctionValue : NativeFunctionValue1<TypeValue, TypeValue>
			{
				// Token: 0x06008322 RID: 33570 RVA: 0x001BE7F4 File Offset: 0x001BC9F4
				public OpenRecordFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008323 RID: 33571 RVA: 0x001BE870 File Offset: 0x001BCA70
				public override TypeValue TypedInvoke(TypeValue type)
				{
					RecordTypeValue asRecordType = type.AsRecordType;
					if (!asRecordType.Open)
					{
						return RecordTypeValue.New(asRecordType.Fields, true);
					}
					return asRecordType;
				}
			}

			// Token: 0x02001387 RID: 4999
			private class IsOpenRecordFunctionValue : NativeFunctionValue1<LogicalValue, TypeValue>
			{
				// Token: 0x06008324 RID: 33572 RVA: 0x001BE813 File Offset: 0x001BCA13
				public IsOpenRecordFunctionValue()
					: base(TypeValue.Logical, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008325 RID: 33573 RVA: 0x001BE89A File Offset: 0x001BCA9A
				public override LogicalValue TypedInvoke(TypeValue type)
				{
					return LogicalValue.New(type.AsRecordType.Open);
				}
			}

			// Token: 0x02001388 RID: 5000
			private class RecordFieldsFunctionValue : NativeFunctionValue1<RecordValue, TypeValue>
			{
				// Token: 0x06008326 RID: 33574 RVA: 0x001BE8AC File Offset: 0x001BCAAC
				public RecordFieldsFunctionValue()
					: base(TypeValue.Record, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008327 RID: 33575 RVA: 0x001BE8C3 File Offset: 0x001BCAC3
				public override RecordValue TypedInvoke(TypeValue type)
				{
					return type.AsRecordType.Fields;
				}
			}

			// Token: 0x02001389 RID: 5001
			private class FunctionParametersFunctionValue : NativeFunctionValue1<RecordValue, TypeValue>
			{
				// Token: 0x06008328 RID: 33576 RVA: 0x001BE8AC File Offset: 0x001BCAAC
				public FunctionParametersFunctionValue()
					: base(TypeValue.Record, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008329 RID: 33577 RVA: 0x001BE8D0 File Offset: 0x001BCAD0
				public override RecordValue TypedInvoke(TypeValue type)
				{
					return type.AsFunctionType.Parameters;
				}
			}

			// Token: 0x0200138A RID: 5002
			private class FunctionRequiredParametersFunctionValue : NativeFunctionValue1<NumberValue, TypeValue>
			{
				// Token: 0x0600832A RID: 33578 RVA: 0x001BE8DD File Offset: 0x001BCADD
				public FunctionRequiredParametersFunctionValue()
					: base(TypeValue.Int32, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600832B RID: 33579 RVA: 0x001BE8F4 File Offset: 0x001BCAF4
				public override NumberValue TypedInvoke(TypeValue type)
				{
					return NumberValue.New(type.AsFunctionType.Min);
				}
			}

			// Token: 0x0200138B RID: 5003
			private class FunctionReturnFunctionValue : NativeFunctionValue1<TypeValue, TypeValue>
			{
				// Token: 0x0600832C RID: 33580 RVA: 0x001BE7F4 File Offset: 0x001BC9F4
				public FunctionReturnFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600832D RID: 33581 RVA: 0x001BE906 File Offset: 0x001BCB06
				public override TypeValue TypedInvoke(TypeValue type)
				{
					return type.AsFunctionType.ReturnType;
				}
			}

			// Token: 0x0200138C RID: 5004
			private class IsFunctionValue : NativeFunctionValue2<LogicalValue, TypeValue, TypeValue>
			{
				// Token: 0x0600832E RID: 33582 RVA: 0x001BE913 File Offset: 0x001BCB13
				public IsFunctionValue()
					: base(TypeValue.Logical, "type1", TypeValue._Type, "type2", TypeValue._Type)
				{
				}

				// Token: 0x0600832F RID: 33583 RVA: 0x001BE934 File Offset: 0x001BCB34
				public override LogicalValue TypedInvoke(TypeValue type1, TypeValue type2)
				{
					return LogicalValue.New(type1.IsCompatibleWith(type2));
				}
			}

			// Token: 0x0200138D RID: 5005
			private class DomainFunctionValue : NativeFunctionValue1<Value, TypeValue>
			{
				// Token: 0x06008330 RID: 33584 RVA: 0x001BE942 File Offset: 0x001BCB42
				public DomainFunctionValue()
					: base(TypeValue.List.Nullable, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008331 RID: 33585 RVA: 0x001BE95E File Offset: 0x001BCB5E
				public override Value TypedInvoke(TypeValue type)
				{
					return TypeServices.GetDomain(type);
				}
			}

			// Token: 0x0200138E RID: 5006
			private class ReplaceDomainFunctionValue : NativeFunctionValue2<TypeValue, TypeValue, Value>
			{
				// Token: 0x06008332 RID: 33586 RVA: 0x001BE966 File Offset: 0x001BCB66
				public ReplaceDomainFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type, "domainValues", TypeValue.List.Nullable)
				{
				}

				// Token: 0x06008333 RID: 33587 RVA: 0x001BE98C File Offset: 0x001BCB8C
				public override TypeValue TypedInvoke(TypeValue type, Value domainValues)
				{
					if (domainValues.IsNull)
					{
						return TypeServices.ClearDomain(type);
					}
					return TypeServices.SetDomain(type, domainValues.AsList);
				}
			}

			// Token: 0x0200138F RID: 5007
			private class UnionFunctionValue : NativeFunctionValue1<TypeValue, ListValue>
			{
				// Token: 0x06008334 RID: 33588 RVA: 0x001BE9A9 File Offset: 0x001BCBA9
				public UnionFunctionValue()
					: base(TypeValue._Type, "types", TypeValue.List)
				{
				}

				// Token: 0x06008335 RID: 33589 RVA: 0x001BE9C0 File Offset: 0x001BCBC0
				public override TypeValue TypedInvoke(ListValue types)
				{
					TypeValue typeValue = TypeValue.None;
					foreach (IValueReference valueReference in types)
					{
						typeValue = TypeAlgebra.Union(typeValue, valueReference.Value.AsType);
					}
					return typeValue;
				}
			}

			// Token: 0x02001390 RID: 5008
			private sealed class KindFunctionValue : NativeFunctionValue1<Value, TypeValue>
			{
				// Token: 0x06008336 RID: 33590 RVA: 0x001BEA1C File Offset: 0x001BCC1C
				public KindFunctionValue()
					: base(TypeValue.Text.Nullable, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008337 RID: 33591 RVA: 0x001BEA38 File Offset: 0x001BCC38
				public override Value TypedInvoke(TypeValue type)
				{
					return TextValue.New(TypeValue.GetTypeKind(type));
				}
			}

			// Token: 0x02001391 RID: 5009
			private class NameFunctionValue : NativeFunctionValue1<Value, TypeValue>
			{
				// Token: 0x06008338 RID: 33592 RVA: 0x001BEA1C File Offset: 0x001BCC1C
				public NameFunctionValue()
					: base(TypeValue.Text.Nullable, "type", TypeValue._Type)
				{
				}

				// Token: 0x06008339 RID: 33593 RVA: 0x001BEA45 File Offset: 0x001BCC45
				public override Value TypedInvoke(TypeValue type)
				{
					return TextValue.New(TypeValue.GetTypeName(type));
				}
			}

			// Token: 0x02001392 RID: 5010
			private class FacetsFunctionValue : NativeFunctionValue1<RecordValue, TypeValue>
			{
				// Token: 0x0600833A RID: 33594 RVA: 0x001BEA52 File Offset: 0x001BCC52
				public FacetsFunctionValue()
					: base(TypeFacets.FacetsType, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600833B RID: 33595 RVA: 0x001BEA69 File Offset: 0x001BCC69
				public override RecordValue TypedInvoke(TypeValue type)
				{
					return type.Facets.ToRecord();
				}
			}

			// Token: 0x02001393 RID: 5011
			private class ReplaceFacetsFunctionValue : NativeFunctionValue2<TypeValue, TypeValue, RecordValue>
			{
				// Token: 0x0600833C RID: 33596 RVA: 0x001BEA76 File Offset: 0x001BCC76
				public ReplaceFacetsFunctionValue()
					: base(TypeValue._Type, "type", TypeValue._Type, "facets", TypeValue.Record)
				{
				}

				// Token: 0x0600833D RID: 33597 RVA: 0x001BEA97 File Offset: 0x001BCC97
				public override TypeValue TypedInvoke(TypeValue type, RecordValue facets)
				{
					return type.NewFacets(TypeFacets.FromRecord(facets));
				}
			}
		}

		// Token: 0x02001394 RID: 5012
		public static class Collection
		{
			// Token: 0x04004907 RID: 18695
			public static readonly FunctionValue Field = new Library.Collection.FieldFunctionValue();

			// Token: 0x04004908 RID: 18696
			public static readonly FunctionValue FieldOrNull = new Library.Collection.FieldOrNullFunctionValue();

			// Token: 0x04004909 RID: 18697
			public static readonly FunctionValue ProjectFields = new Library.Collection.ProjectFieldsFunctionValue(false);

			// Token: 0x0400490A RID: 18698
			public static readonly FunctionValue ProjectFieldsIfExistOrNull = new Library.Collection.ProjectFieldsFunctionValue(true);

			// Token: 0x02001395 RID: 5013
			private class FieldFunctionValue : NativeFunctionValue2
			{
				// Token: 0x0600833F RID: 33599 RVA: 0x001BEAD1 File Offset: 0x001BCCD1
				public FieldFunctionValue()
					: base("collection", "fieldName")
				{
				}

				// Token: 0x06008340 RID: 33600 RVA: 0x001BEAE3 File Offset: 0x001BCCE3
				public override Value Invoke(Value collection, Value fieldName)
				{
					return collection[fieldName.AsText.String];
				}
			}

			// Token: 0x02001396 RID: 5014
			private class FieldOrNullFunctionValue : NativeFunctionValue2
			{
				// Token: 0x06008341 RID: 33601 RVA: 0x001BEAD1 File Offset: 0x001BCCD1
				public FieldOrNullFunctionValue()
					: base("collection", "fieldName")
				{
				}

				// Token: 0x06008342 RID: 33602 RVA: 0x001BEAF8 File Offset: 0x001BCCF8
				public override Value Invoke(Value collection, Value fieldName)
				{
					Value value;
					if (!collection.IsNull && collection.TryGetValue(fieldName.AsText.String, out value))
					{
						return value;
					}
					return Value.Null;
				}
			}

			// Token: 0x02001397 RID: 5015
			private class ProjectFieldsFunctionValue : NativeFunctionValue2<Value, Value, ListValue>
			{
				// Token: 0x06008343 RID: 33603 RVA: 0x001BEB29 File Offset: 0x001BCD29
				public ProjectFieldsFunctionValue(bool isOptional)
					: base(TypeValue.Any, "value", TypeValue.Any, "fieldNames", TypeValue.List)
				{
					this.isOptional = isOptional;
				}

				// Token: 0x06008344 RID: 33604 RVA: 0x001BEB54 File Offset: 0x001BCD54
				public override Value TypedInvoke(Value value, ListValue fieldNames)
				{
					if (value.IsNull && this.isOptional)
					{
						return Value.Null;
					}
					if (value.IsTable)
					{
						MissingFieldMode missingFieldMode = (this.isOptional ? MissingFieldMode.UseNull : MissingFieldMode.Error);
						return value.AsTable.SelectColumns(fieldNames, missingFieldMode);
					}
					Value value2 = (this.isOptional ? Library.MissingField.UseNull : Value.Null);
					return Library.Record.SelectFields.Invoke(value, fieldNames, value2);
				}

				// Token: 0x0400490B RID: 18699
				private readonly bool isOptional;
			}
		}

		// Token: 0x02001398 RID: 5016
		public static class FieldsSelector
		{
			// Token: 0x06008345 RID: 33605 RVA: 0x001BEBBC File Offset: 0x001BCDBC
			private static bool TryGetNames(Value fieldNames, out Keys names, out string duplicate)
			{
				names = null;
				duplicate = null;
				if (fieldNames.IsText)
				{
					names = Keys.New(fieldNames.AsString);
					return true;
				}
				if (fieldNames.IsList)
				{
					ListValue asList = fieldNames.AsList;
					KeysBuilder keysBuilder = new KeysBuilder(asList.Count);
					for (int i = 0; i < asList.Count; i++)
					{
						Value value = asList[i];
						if (!value.IsText)
						{
							return false;
						}
						if (!keysBuilder.Union(value.AsString))
						{
							duplicate = value.AsString;
							return false;
						}
					}
					names = keysBuilder.ToKeys();
					return true;
				}
				return false;
			}

			// Token: 0x06008346 RID: 33606 RVA: 0x001BEC4C File Offset: 0x001BCE4C
			public static bool TryGetNames(Value fieldNames, out Keys names)
			{
				string text;
				return Library.FieldsSelector.TryGetNames(fieldNames, out names, out text);
			}

			// Token: 0x06008347 RID: 33607 RVA: 0x001BEC64 File Offset: 0x001BCE64
			public static Keys GetNames(Value fieldNames)
			{
				Keys keys;
				string text;
				if (Library.FieldsSelector.TryGetNames(fieldNames, out keys, out text))
				{
					return keys;
				}
				if (text != null)
				{
					throw ValueException.DuplicateField(text);
				}
				throw ValueException.NewExpressionError<Message0>(Strings.FieldsSelector_FieldsSelectorValueExpectedError, fieldNames, null);
			}
		}

		// Token: 0x02001399 RID: 5017
		public static class RenameOperations
		{
			// Token: 0x06008348 RID: 33608 RVA: 0x001BEC98 File Offset: 0x001BCE98
			public static bool TryGetRenames(Value renameList, out IDictionary<string, string> dictionary)
			{
				if (!renameList.IsList)
				{
					dictionary = null;
					return false;
				}
				string text;
				return Library.RenameOperations.TryGetRenames(renameList.AsList, out dictionary, out text);
			}

			// Token: 0x06008349 RID: 33609 RVA: 0x001BECC0 File Offset: 0x001BCEC0
			public static IDictionary<string, string> GetRenames(ListValue renameList)
			{
				IDictionary<string, string> dictionary;
				string text;
				if (Library.RenameOperations.TryGetRenames(renameList, out dictionary, out text))
				{
					return dictionary;
				}
				if (text != null)
				{
					throw ValueException.DuplicateField(text);
				}
				throw ValueException.NewExpressionError<Message0>(Strings.RenameOperations_RenameOperationsValueExpectedError, renameList, null);
			}

			// Token: 0x0600834A RID: 33610 RVA: 0x001BECF4 File Offset: 0x001BCEF4
			private static bool TryGetRename(ListValue rename, out string oldName, out string newName)
			{
				if (rename.Count == 2)
				{
					Value value = rename[0];
					Value value2 = rename[1];
					if (value.IsText && value2.IsText)
					{
						oldName = value.AsString;
						newName = value2.AsString;
						return true;
					}
				}
				oldName = null;
				newName = null;
				return false;
			}

			// Token: 0x0600834B RID: 33611 RVA: 0x001BED44 File Offset: 0x001BCF44
			private static bool TryGetRenames(ListValue renames, out IDictionary<string, string> dictionary, out string duplicate)
			{
				duplicate = null;
				string text;
				string text2;
				if (Library.RenameOperations.TryGetRename(renames, out text, out text2))
				{
					dictionary = new Dictionary<string, string>(1);
					dictionary.Add(text, text2);
					return true;
				}
				dictionary = new Dictionary<string, string>(renames.Count);
				foreach (IValueReference valueReference in renames)
				{
					Value value = valueReference.Value;
					if (!value.IsList || !Library.RenameOperations.TryGetRename(value.AsList, out text, out text2))
					{
						return false;
					}
					if (dictionary.ContainsKey(text))
					{
						duplicate = text;
						return false;
					}
					dictionary.Add(text, text2);
				}
				return true;
			}
		}

		// Token: 0x0200139A RID: 5018
		public static class TransformOperation
		{
			// Token: 0x0400490C RID: 18700
			public static readonly FunctionValue _TransformOperation = new Library.TransformOperation.TransformOperationFunctionValue();

			// Token: 0x0400490D RID: 18701
			public static readonly FunctionValue IsTransformOperation = new Library.TransformOperation.IsTransformOperationFunctionValue();

			// Token: 0x0200139B RID: 5019
			private class TransformOperationFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x0600834D RID: 33613 RVA: 0x001BEE0E File Offset: 0x001BD00E
				public TransformOperationFunctionValue()
					: base(TypeValue.List, "transform", TypeValue.List)
				{
				}

				// Token: 0x0600834E RID: 33614 RVA: 0x001BEE25 File Offset: 0x001BD025
				public override ListValue TypedInvoke(ListValue transform)
				{
					if (!Library.TransformOperation.IsTransformOperation.Invoke(transform).AsBoolean)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.TransformOperation_TransformOperationValueExpectedError, transform, null);
					}
					return transform;
				}
			}

			// Token: 0x0200139C RID: 5020
			private class IsTransformOperationFunctionValue : NativeFunctionValue1<LogicalValue, Value>
			{
				// Token: 0x0600834F RID: 33615 RVA: 0x001BEE47 File Offset: 0x001BD047
				public IsTransformOperationFunctionValue()
					: base(TypeValue.Logical, "transform", TypeValue.Any)
				{
				}

				// Token: 0x06008350 RID: 33616 RVA: 0x001BEE60 File Offset: 0x001BD060
				public override LogicalValue TypedInvoke(Value transform)
				{
					return LogicalValue.New(transform.IsList && transform.AsList.Count == 2 && transform.AsList[0].IsText && transform.AsList[1].IsFunction);
				}
			}
		}

		// Token: 0x0200139D RID: 5021
		public static class TransformOperations
		{
			// Token: 0x0400490E RID: 18702
			public static readonly FunctionValue _TransformOperations = new Library.TransformOperations.TransformOperationsFunctionValue();

			// Token: 0x0400490F RID: 18703
			public static readonly FunctionValue IsTransformOperations = new Library.TransformOperations.IsTransformOperationsFunctionValue();

			// Token: 0x0200139E RID: 5022
			private class TransformOperationsFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x06008352 RID: 33618 RVA: 0x001BEEC5 File Offset: 0x001BD0C5
				public TransformOperationsFunctionValue()
					: base(TypeValue.List, "transforms", TypeValue.List)
				{
				}

				// Token: 0x06008353 RID: 33619 RVA: 0x001BEEDC File Offset: 0x001BD0DC
				public override ListValue TypedInvoke(ListValue transforms)
				{
					if (!Library.TransformOperations.IsTransformOperations.Invoke(transforms).AsBoolean)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.TransformOperations_TransformOperationsValueExpectedError, transforms, null);
					}
					if (Library.TransformOperation.IsTransformOperation.Invoke(transforms).AsBoolean)
					{
						transforms = ListValue.New(new Value[] { transforms });
					}
					return transforms;
				}
			}

			// Token: 0x0200139F RID: 5023
			private class IsTransformOperationsFunctionValue : NativeFunctionValue1<LogicalValue, Value>
			{
				// Token: 0x06008354 RID: 33620 RVA: 0x001BEF2C File Offset: 0x001BD12C
				public IsTransformOperationsFunctionValue()
					: base(TypeValue.Logical, "transforms", TypeValue.Any)
				{
				}

				// Token: 0x06008355 RID: 33621 RVA: 0x001BEF44 File Offset: 0x001BD144
				public override LogicalValue TypedInvoke(Value transforms)
				{
					bool flag;
					if (!Library.TransformOperation.IsTransformOperation.Invoke(transforms).AsBoolean)
					{
						flag = transforms.IsListOf((Value transform) => Library.TransformOperation.IsTransformOperation.Invoke(transform).AsBoolean);
					}
					else
					{
						flag = true;
					}
					return LogicalValue.New(flag);
				}
			}
		}

		// Token: 0x020013A1 RID: 5025
		public static class Record
		{
			// Token: 0x06008359 RID: 33625 RVA: 0x001BEFAE File Offset: 0x001BD1AE
			private static Value GetDefaultFieldValue(RecordValue record, string field, MissingFieldMode missingFieldMode)
			{
				if (missingFieldMode == MissingFieldMode.Ignore)
				{
					return null;
				}
				if (missingFieldMode != MissingFieldMode.UseNull)
				{
					throw record.MissingField(field);
				}
				return Value.Null;
			}

			// Token: 0x04004912 RID: 18706
			public static readonly Keys NameValueKeys = Keys.New("Name", "Value");

			// Token: 0x04004913 RID: 18707
			public static readonly TypeValue RecordOfNameValuePairType = RecordTypeValue.New(RecordValue.New(Library.Record.NameValueKeys, new Value[]
			{
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Any,
					LogicalValue.False
				})
			}));

			// Token: 0x04004914 RID: 18708
			public static readonly TypeValue ListOfRecordOfNameValuePairType = ListTypeValue.New(Library.Record.RecordOfNameValuePairType);

			// Token: 0x04004915 RID: 18709
			public static readonly FunctionValue AddField = new Library.Record.AddFieldFunctionValue();

			// Token: 0x04004916 RID: 18710
			public static readonly FunctionValue Field = new Library.Record.FieldFunctionValue();

			// Token: 0x04004917 RID: 18711
			public static readonly FunctionValue FieldCount = new Library.Record.FieldCountFunctionValue();

			// Token: 0x04004918 RID: 18712
			public static readonly FunctionValue FieldNames = new Library.Record.FieldNamesFunctionValue();

			// Token: 0x04004919 RID: 18713
			public static readonly FunctionValue FieldOrDefault = new Library.Record.FieldOrDefaultFunctionValue();

			// Token: 0x0400491A RID: 18714
			public static readonly FunctionValue FieldValues = new Library.Record.FieldValuesFunctionValue();

			// Token: 0x0400491B RID: 18715
			public static readonly FunctionValue FromTable = new Library.Record.FromTableFunctionValue();

			// Token: 0x0400491C RID: 18716
			public static readonly FunctionValue FromList = new Library.Record.FromListFunctionValue();

			// Token: 0x0400491D RID: 18717
			public static readonly FunctionValue HasFields = new Library.Record.HasFieldsFunctionValue();

			// Token: 0x0400491E RID: 18718
			public static readonly FunctionValue IsRecordOfNameValuePair = new Library.Record.IsRecordOfNameValuePairFunctionValue();

			// Token: 0x0400491F RID: 18719
			public static readonly FunctionValue RemoveFields = new Library.Record.RemoveFieldsFunctionValue();

			// Token: 0x04004920 RID: 18720
			public static readonly FunctionValue RenameFields = new Library.Record.RenameFieldsFunctionValue();

			// Token: 0x04004921 RID: 18721
			public static readonly FunctionValue ReorderFields = new Library.Record.ReorderFieldsFunctionValue();

			// Token: 0x04004922 RID: 18722
			public static readonly FunctionValue SelectFields = new Library.Record.SelectFieldsFunctionValue();

			// Token: 0x04004923 RID: 18723
			public static readonly FunctionValue SelectFieldsByIndex = new Library.Record.SelectFieldsByIndexFunctionValue();

			// Token: 0x04004924 RID: 18724
			public static readonly FunctionValue ToTable = new Library.Record.ToTableFunctionValue();

			// Token: 0x04004925 RID: 18725
			public static readonly FunctionValue TransformFields = new Library.Record.TransformFieldsFunctionValue();

			// Token: 0x04004926 RID: 18726
			public static readonly FunctionValue Combine = new Library.Record.CombineFunctionValue();

			// Token: 0x04004927 RID: 18727
			public static readonly FunctionValue ToList = new Library.Record.ToListFunctionValue();

			// Token: 0x020013A2 RID: 5026
			private class AddFieldFunctionValue : NativeFunctionValue4<RecordValue, RecordValue, TextValue, Value, Value>
			{
				// Token: 0x0600835B RID: 33627 RVA: 0x001BF11C File Offset: 0x001BD31C
				public AddFieldFunctionValue()
					: base(TypeValue.Record, 3, "record", TypeValue.Record, "fieldName", TypeValue.Text, "value", TypeValue.Any, "delayed", NullableTypeValue.Logical)
				{
				}

				// Token: 0x0600835C RID: 33628 RVA: 0x001BF160 File Offset: 0x001BD360
				public override RecordValue TypedInvoke(RecordValue record, TextValue fieldName, Value value, Value delayed)
				{
					RecordTypeValue asRecordType = record.Type.AsType.AsRecordType;
					RecordBuilder recordBuilder = new RecordBuilder(record.Keys.Length + 1);
					for (int i = 0; i < record.Keys.Length; i++)
					{
						recordBuilder.Add(record.Keys[i], record.GetReference(i), asRecordType.Fields[i]["Type"].AsType);
					}
					TypeValue typeValue = value.Type;
					IValueReference valueReference = value;
					if (!delayed.IsNull && delayed.AsBoolean)
					{
						valueReference = new FunctionValueReference(value.AsFunction);
						typeValue = typeValue.AsFunctionType.ReturnType;
					}
					recordBuilder.Add(fieldName.AsString, valueReference, typeValue);
					return recordBuilder.ToRecord();
				}
			}

			// Token: 0x020013A3 RID: 5027
			private class FieldFunctionValue : NativeFunctionValue2<Value, RecordValue, TextValue>, IInvocationRewriter
			{
				// Token: 0x0600835D RID: 33629 RVA: 0x001BF22D File Offset: 0x001BD42D
				public FieldFunctionValue()
					: base(TypeValue.Any, "record", TypeValue.Record, "field", TypeValue.Text)
				{
				}

				// Token: 0x0600835E RID: 33630 RVA: 0x001BF24E File Offset: 0x001BD44E
				public override Value TypedInvoke(RecordValue record, TextValue field)
				{
					return record.AsRecord[field.String];
				}

				// Token: 0x0600835F RID: 33631 RVA: 0x001BF264 File Offset: 0x001BD464
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					Value value;
					if (invocation.Arguments.Count == 2 && invocation.Arguments[1].TryGetConstant(out value) && value.IsText)
					{
						expression = new RequiredFieldAccessExpressionSyntaxNode(invocation.Arguments[0], Identifier.New(value.AsString));
						return true;
					}
					expression = null;
					return false;
				}
			}

			// Token: 0x020013A4 RID: 5028
			private class FieldCountFunctionValue : NativeFunctionValue1<NumberValue, RecordValue>
			{
				// Token: 0x06008360 RID: 33632 RVA: 0x001BF2C0 File Offset: 0x001BD4C0
				public FieldCountFunctionValue()
					: base(TypeValue.Int32, "record", TypeValue.Record)
				{
				}

				// Token: 0x06008361 RID: 33633 RVA: 0x001BF2D7 File Offset: 0x001BD4D7
				public override NumberValue TypedInvoke(RecordValue record)
				{
					return NumberValue.New(record.Keys.Length);
				}
			}

			// Token: 0x020013A5 RID: 5029
			private class FieldNamesFunctionValue : NativeFunctionValue1<ListValue, RecordValue>
			{
				// Token: 0x06008362 RID: 33634 RVA: 0x001BF2E9 File Offset: 0x001BD4E9
				public FieldNamesFunctionValue()
					: base(TypeValue.List, "record", TypeValue.Record)
				{
				}

				// Token: 0x06008363 RID: 33635 RVA: 0x001BF300 File Offset: 0x001BD500
				public override ListValue TypedInvoke(RecordValue record)
				{
					return new Library.Record.FieldNamesFunctionValue.FieldNamesListValue(record.Keys).NewType(ListTypeValue.Text).AsList;
				}

				// Token: 0x020013A6 RID: 5030
				private class FieldNamesListValue : BufferedListValue
				{
					// Token: 0x06008364 RID: 33636 RVA: 0x001BF31C File Offset: 0x001BD51C
					public FieldNamesListValue(Keys keys)
					{
						this.keys = keys;
					}

					// Token: 0x1700234F RID: 9039
					// (get) Token: 0x06008365 RID: 33637 RVA: 0x001BF32B File Offset: 0x001BD52B
					public override int Count
					{
						get
						{
							return this.keys.Length;
						}
					}

					// Token: 0x17002350 RID: 9040
					public override Value this[int index]
					{
						get
						{
							if (index < 0 || index >= this.keys.Length)
							{
								throw ValueException.ListIndexOutOfRange(index, this);
							}
							return TextValue.New(this.keys[index]);
						}
					}

					// Token: 0x04004928 RID: 18728
					private readonly Keys keys;
				}
			}

			// Token: 0x020013A7 RID: 5031
			private class FieldOrDefaultFunctionValue : NativeFunctionValue3<Value, Value, TextValue, Value>, IInvocationRewriter
			{
				// Token: 0x06008367 RID: 33639 RVA: 0x001BF368 File Offset: 0x001BD568
				public FieldOrDefaultFunctionValue()
					: base(TypeValue.Any, 2, "record", TypeValue.Record.Nullable, "field", TypeValue.Text, "defaultValue", TypeValue.Any)
				{
				}

				// Token: 0x06008368 RID: 33640 RVA: 0x001BF3A4 File Offset: 0x001BD5A4
				public override Value TypedInvoke(Value record, TextValue field, Value defaultValue)
				{
					Value value;
					if (record.IsNull || !record.TryGetValue(field.String, out value))
					{
						value = defaultValue;
					}
					return value;
				}

				// Token: 0x06008369 RID: 33641 RVA: 0x001BF3CC File Offset: 0x001BD5CC
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					int count = invocation.Arguments.Count;
					Value value;
					if ((count == 2 || count == 3) && invocation.Arguments[1].TryGetConstant(out value) && value.IsText)
					{
						TypeValue type = environment.GetType(invocation.Arguments[0]);
						if (type.IsRecordType)
						{
							Value value2;
							if (type.AsRecordType.Fields.TryGetValue(value.AsString, out value2))
							{
								Value value3;
								if (value2.TryGetValue("Optional", out value3) && value3.IsLogical && !value3.AsBoolean)
								{
									expression = new RequiredFieldAccessExpressionSyntaxNode(invocation.Arguments[0], value.AsString);
									return true;
								}
							}
							else if (!type.AsRecordType.Open)
							{
								IExpression expression2;
								if (count != 2)
								{
									expression2 = invocation.Arguments[2];
								}
								else
								{
									IExpression @null = ConstantExpressionSyntaxNode.Null;
									expression2 = @null;
								}
								expression = expression2;
								return true;
							}
						}
					}
					expression = null;
					return false;
				}
			}

			// Token: 0x020013A8 RID: 5032
			private class FieldValuesFunctionValue : NativeFunctionValue1<ListValue, RecordValue>
			{
				// Token: 0x0600836A RID: 33642 RVA: 0x001BF2E9 File Offset: 0x001BD4E9
				public FieldValuesFunctionValue()
					: base(TypeValue.List, "record", TypeValue.Record)
				{
				}

				// Token: 0x0600836B RID: 33643 RVA: 0x001BF4BC File Offset: 0x001BD6BC
				public override ListValue TypedInvoke(RecordValue value)
				{
					return new Library.Record.FieldValuesFunctionValue.FieldValuesListValue(value);
				}

				// Token: 0x020013A9 RID: 5033
				private class FieldValuesListValue : BufferedListValue
				{
					// Token: 0x0600836C RID: 33644 RVA: 0x001BF4C4 File Offset: 0x001BD6C4
					public FieldValuesListValue(RecordValue value)
					{
						this.value = value;
					}

					// Token: 0x17002351 RID: 9041
					// (get) Token: 0x0600836D RID: 33645 RVA: 0x001BF4D3 File Offset: 0x001BD6D3
					public sealed override int Count
					{
						get
						{
							return this.value.Keys.Length;
						}
					}

					// Token: 0x17002352 RID: 9042
					public sealed override Value this[int index]
					{
						get
						{
							this.CheckRange(index);
							return this.value[index];
						}
					}

					// Token: 0x0600836F RID: 33647 RVA: 0x001BF4FA File Offset: 0x001BD6FA
					public sealed override IValueReference GetReference(int index)
					{
						this.CheckRange(index);
						return this.value.GetReference(index);
					}

					// Token: 0x06008370 RID: 33648 RVA: 0x001BF50F File Offset: 0x001BD70F
					private void CheckRange(int index)
					{
						if ((ulong)index >= (ulong)((long)this.Count))
						{
							throw ValueException.ListIndexOutOfRange(index, this);
						}
					}

					// Token: 0x04004929 RID: 18729
					private readonly RecordValue value;
				}
			}

			// Token: 0x020013AA RID: 5034
			private class FromTableFunctionValue : NativeFunctionValue1<RecordValue, TableValue>
			{
				// Token: 0x06008371 RID: 33649 RVA: 0x001BF524 File Offset: 0x001BD724
				public FromTableFunctionValue()
					: base(TypeValue.Record, "table", TypeValue.Table)
				{
				}

				// Token: 0x06008372 RID: 33650 RVA: 0x001BF53C File Offset: 0x001BD73C
				public override RecordValue TypedInvoke(TableValue table)
				{
					ListValue listValue = table.ToRecords();
					KeysBuilder keysBuilder = default(KeysBuilder);
					List<IValueReference> list = new List<IValueReference>();
					foreach (IValueReference valueReference in listValue)
					{
						RecordValue asRecord = valueReference.Value.AsRecord;
						keysBuilder.Add(asRecord["Name"].AsString);
						int num;
						if (!asRecord.Keys.TryGetKeyIndex("Value", out num))
						{
							throw asRecord.MissingField("Value");
						}
						list.Add(asRecord.GetReference(num));
					}
					return RecordValue.New(keysBuilder.ToKeys(), list.ToArray());
				}
			}

			// Token: 0x020013AB RID: 5035
			private class FromListFunctionValue : NativeFunctionValue2<RecordValue, ListValue, Value>
			{
				// Token: 0x06008373 RID: 33651 RVA: 0x001BF5F4 File Offset: 0x001BD7F4
				public FromListFunctionValue()
					: base(TypeValue.Record, 2, "list", TypeValue.List, "fields", TypeValue.Any)
				{
				}

				// Token: 0x06008374 RID: 33652 RVA: 0x001BF618 File Offset: 0x001BD818
				public override RecordValue TypedInvoke(ListValue list, Value fields)
				{
					if (fields.IsType)
					{
						return list.ToRecord(fields.AsType.AsRecordType);
					}
					List<string> list2 = new List<string>();
					foreach (IValueReference valueReference in fields.AsList)
					{
						list2.Add(valueReference.Value.AsString);
					}
					Keys keys = Keys.New(list2.ToArray());
					return list.ToRecord(keys);
				}
			}

			// Token: 0x020013AC RID: 5036
			private class HasFieldsFunctionValue : NativeFunctionValue2<LogicalValue, RecordValue, Value>
			{
				// Token: 0x06008375 RID: 33653 RVA: 0x001BF6A4 File Offset: 0x001BD8A4
				public HasFieldsFunctionValue()
					: base(TypeValue.Logical, "record", TypeValue.Record, "fields", TypeValue.Any)
				{
				}

				// Token: 0x06008376 RID: 33654 RVA: 0x001BF6C8 File Offset: 0x001BD8C8
				public override LogicalValue TypedInvoke(RecordValue record, Value fields)
				{
					Keys names = Library.FieldsSelector.GetNames(fields);
					for (int i = 0; i < names.Length; i++)
					{
						int num;
						if (!record.Keys.TryGetKeyIndex(names[i], out num))
						{
							return LogicalValue.False;
						}
					}
					return LogicalValue.True;
				}
			}

			// Token: 0x020013AD RID: 5037
			private class IsRecordOfNameValuePairFunctionValue : NativeFunctionValue1<LogicalValue, Value>
			{
				// Token: 0x06008377 RID: 33655 RVA: 0x001BF70E File Offset: 0x001BD90E
				public IsRecordOfNameValuePairFunctionValue()
					: base(TypeValue.Logical, "item", TypeValue.Any)
				{
				}

				// Token: 0x06008378 RID: 33656 RVA: 0x001BF725 File Offset: 0x001BD925
				public override LogicalValue TypedInvoke(Value item)
				{
					return LogicalValue.New(item.IsRecord && item.AsRecord.Keys.Equals(Library.Record.NameValueKeys) && item.AsRecord["Name"].IsText);
				}
			}

			// Token: 0x020013AE RID: 5038
			private class RemoveFieldsFunctionValue : NativeFunctionValue3<RecordValue, RecordValue, Value, Value>
			{
				// Token: 0x06008379 RID: 33657 RVA: 0x001BF764 File Offset: 0x001BD964
				public RemoveFieldsFunctionValue()
					: base(TypeValue.Record, 2, "record", TypeValue.Record, "fields", TypeValue.Any, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x0600837A RID: 33658 RVA: 0x001BF7A0 File Offset: 0x001BD9A0
				public override RecordValue TypedInvoke(RecordValue record, Value fields, Value missingField)
				{
					Keys names = Library.FieldsSelector.GetNames(fields);
					MissingFieldMode missingFieldMode = RecordTypeAlgebra.GetMissingFieldMode(missingField);
					RecordTypeValue asRecordType = record.Type.AsType.AsRecordType;
					RecordBuilder recordBuilder = new RecordBuilder(Math.Max(record.Keys.Length - names.Length, 0));
					for (int i = 0; i < record.Keys.Length; i++)
					{
						if (!names.Contains(record.Keys[i]))
						{
							recordBuilder.Add(record.Keys[i], record.GetReference(i), asRecordType.Fields[i]["Type"].AsType);
						}
					}
					if (missingFieldMode == MissingFieldMode.Error)
					{
						foreach (string text in names)
						{
							if (!record.Keys.Contains(text))
							{
								throw record.MissingField(text);
							}
						}
					}
					return recordBuilder.ToRecord();
				}
			}

			// Token: 0x020013AF RID: 5039
			private class RenameFieldsFunctionValue : NativeFunctionValue3<RecordValue, RecordValue, ListValue, Value>
			{
				// Token: 0x0600837B RID: 33659 RVA: 0x001BF8B0 File Offset: 0x001BDAB0
				public RenameFieldsFunctionValue()
					: base(TypeValue.Record, 2, "record", TypeValue.Record, "renames", TypeValue.List, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x0600837C RID: 33660 RVA: 0x001BF8EC File Offset: 0x001BDAEC
				public override RecordValue TypedInvoke(RecordValue record, ListValue renamesList, Value missingField)
				{
					IDictionary<string, string> renames = Library.RenameOperations.GetRenames(renamesList);
					MissingFieldMode missingFieldMode = RecordTypeAlgebra.GetMissingFieldMode(missingField);
					RecordTypeValue asRecordType = record.Type.AsType.AsRecordType;
					RecordBuilder recordBuilder = new RecordBuilder(record.Keys.Length);
					for (int i = 0; i < record.Keys.Length; i++)
					{
						string text = record.Keys[i];
						string text2;
						if (!renames.TryGetValue(text, out text2))
						{
							text2 = text;
						}
						recordBuilder.Add(text2, record.GetReference(i), asRecordType.Fields[i]["Type"].AsType);
					}
					foreach (KeyValuePair<string, string> keyValuePair in renames)
					{
						string key = keyValuePair.Key;
						string value = keyValuePair.Value;
						if (!record.Keys.Contains(key))
						{
							Value defaultFieldValue = Library.Record.GetDefaultFieldValue(record, key, missingFieldMode);
							if (defaultFieldValue != null)
							{
								recordBuilder.Add(value, defaultFieldValue, defaultFieldValue.Type);
							}
						}
					}
					return recordBuilder.ToRecord();
				}
			}

			// Token: 0x020013B0 RID: 5040
			private class ReorderFieldsFunctionValue : NativeFunctionValue3<RecordValue, RecordValue, ListValue, Value>
			{
				// Token: 0x0600837D RID: 33661 RVA: 0x001BFA14 File Offset: 0x001BDC14
				public ReorderFieldsFunctionValue()
					: base(TypeValue.Record, 2, "record", TypeValue.Record, "fieldOrder", TypeValue.List, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x0600837E RID: 33662 RVA: 0x001BFA50 File Offset: 0x001BDC50
				public override RecordValue TypedInvoke(RecordValue record, ListValue fieldOrder, Value missingField)
				{
					ListValue listValue = RecordTypeAlgebra.ComputeFieldOrder(record.Keys, fieldOrder);
					return Library.Record.SelectFields.Invoke(record, listValue, missingField).AsRecord;
				}
			}

			// Token: 0x020013B1 RID: 5041
			private class SelectFieldsFunctionValue : NativeFunctionValue3<RecordValue, RecordValue, Value, Value>
			{
				// Token: 0x0600837F RID: 33663 RVA: 0x001BFA7C File Offset: 0x001BDC7C
				public SelectFieldsFunctionValue()
					: base(TypeValue.Record, 2, "record", TypeValue.Record, "fields", TypeValue.Any, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06008380 RID: 33664 RVA: 0x001BFAB8 File Offset: 0x001BDCB8
				public override RecordValue TypedInvoke(RecordValue record, Value fieldNames, Value missingField)
				{
					Keys names = Library.FieldsSelector.GetNames(fieldNames);
					MissingFieldMode missingFieldMode = RecordTypeAlgebra.GetMissingFieldMode(missingField);
					RecordTypeValue asRecordType = record.Type.AsType.AsRecordType;
					RecordBuilder recordBuilder = new RecordBuilder(names.Length);
					for (int i = 0; i < names.Length; i++)
					{
						string text = names[i];
						int num;
						if (record.Keys.TryGetKeyIndex(text, out num))
						{
							IValueReference valueReference = record.GetReference(num);
							recordBuilder.Add(text, valueReference, asRecordType.Fields[num]["Type"].AsType);
						}
						else
						{
							IValueReference valueReference = Library.Record.GetDefaultFieldValue(record, text, missingFieldMode);
							if (valueReference != null)
							{
								recordBuilder.Add(text, valueReference, TypeValue.Null);
							}
						}
					}
					return recordBuilder.ToRecord();
				}
			}

			// Token: 0x020013B2 RID: 5042
			private class SelectFieldsByIndexFunctionValue : NativeFunctionValue2<RecordValue, RecordValue, ListValue>
			{
				// Token: 0x06008381 RID: 33665 RVA: 0x001BFB7A File Offset: 0x001BDD7A
				public SelectFieldsByIndexFunctionValue()
					: base(TypeValue.Record, "record", TypeValue.Record, "indices", ListTypeValue.Number)
				{
				}

				// Token: 0x06008382 RID: 33666 RVA: 0x001BFB9C File Offset: 0x001BDD9C
				public override RecordValue TypedInvoke(RecordValue record, ListValue indices)
				{
					string[] array = new string[indices.Count];
					IValueReference[] array2 = new IValueReference[indices.Count];
					for (int i = 0; i < array.Length; i++)
					{
						int asInteger = indices[i].AsInteger32;
						array[i] = record.Keys[asInteger];
						array2[i] = record.GetReference(asInteger);
					}
					return RecordValue.New(Keys.New(array), array2);
				}
			}

			// Token: 0x020013B3 RID: 5043
			private class ToTableFunctionValue : NativeFunctionValue1<TableValue, RecordValue>
			{
				// Token: 0x06008383 RID: 33667 RVA: 0x001BFC01 File Offset: 0x001BDE01
				public ToTableFunctionValue()
					: base(TypeValue.Table, "record", TypeValue.Record)
				{
				}

				// Token: 0x06008384 RID: 33668 RVA: 0x001BFC18 File Offset: 0x001BDE18
				public override TableValue TypedInvoke(RecordValue record)
				{
					IValueReference[] array = new Value[record.Keys.Length];
					IValueReference[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = RecordValue.New(Library.Record.NameValueKeys, new IValueReference[]
						{
							TextValue.New(record.Keys[i]),
							record.GetReference(i)
						});
					}
					return ListValue.New(array2).ToTable(Library.Record.ToTableFunctionValue.tableType);
				}

				// Token: 0x0400492A RID: 18730
				private static readonly TableTypeValue tableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(Library.Record.NameValueKeys, new Value[]
				{
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Text,
						LogicalValue.False
					}),
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Any,
						LogicalValue.False
					})
				}), false));
			}

			// Token: 0x020013B4 RID: 5044
			private class CombineFunctionValue : NativeFunctionValue1<RecordValue, ListValue>
			{
				// Token: 0x06008386 RID: 33670 RVA: 0x001BFCFB File Offset: 0x001BDEFB
				public CombineFunctionValue()
					: base(TypeValue.Record, "records", TypeValue.List)
				{
				}

				// Token: 0x06008387 RID: 33671 RVA: 0x001BFD12 File Offset: 0x001BDF12
				public override RecordValue TypedInvoke(ListValue records)
				{
					return RecordValue.Combine(records);
				}
			}

			// Token: 0x020013B5 RID: 5045
			private class TransformFieldsFunctionValue : NativeFunctionValue3<RecordValue, RecordValue, ListValue, Value>
			{
				// Token: 0x06008388 RID: 33672 RVA: 0x001BFD1C File Offset: 0x001BDF1C
				public TransformFieldsFunctionValue()
					: base(TypeValue.Record, 2, "record", TypeValue.Record, "transformOperations", TypeValue.List, "missingField", Library.MissingField.Type.Nullable)
				{
				}

				// Token: 0x06008389 RID: 33673 RVA: 0x001BFD58 File Offset: 0x001BDF58
				public override RecordValue TypedInvoke(RecordValue record, ListValue transformOperations, Value missingField)
				{
					ListValue asList = Library.TransformOperations._TransformOperations.Invoke(transformOperations).AsList;
					MissingFieldMode missingFieldMode = RecordTypeAlgebra.GetMissingFieldMode(missingField);
					Dictionary<string, FunctionValue> dictionary = new Dictionary<string, FunctionValue>();
					for (int i = 0; i < asList.Count; i++)
					{
						string asString = asList[i][0].AsString;
						FunctionValue asFunction = asList[i][1].AsFunction;
						if (dictionary.ContainsKey(asString))
						{
							throw ValueException.DuplicateField(asString);
						}
						dictionary[asString] = asFunction;
					}
					RecordTypeValue asRecordType = record.Type.AsRecordType;
					RecordBuilder recordBuilder = new RecordBuilder(record.Keys.Length);
					for (int j = 0; j < record.Keys.Length; j++)
					{
						IValueReference valueReference = record.GetReference(j);
						FunctionValue functionValue;
						TypeValue typeValue;
						if (dictionary.TryGetValue(record.Keys[j], out functionValue))
						{
							valueReference = new TransformValueReference(valueReference, functionValue);
							typeValue = functionValue.Type.AsFunctionType.ReturnType;
						}
						else
						{
							typeValue = asRecordType.Fields[j]["Type"].AsType;
						}
						recordBuilder.Add(record.Keys[j], valueReference, typeValue);
					}
					foreach (KeyValuePair<string, FunctionValue> keyValuePair in dictionary)
					{
						string key = keyValuePair.Key;
						FunctionValue value = keyValuePair.Value;
						if (!record.Keys.Contains(key))
						{
							Value defaultFieldValue = Library.Record.GetDefaultFieldValue(record, key, missingFieldMode);
							if (defaultFieldValue != null)
							{
								recordBuilder.Add(key, new TransformValueReference(defaultFieldValue, value), value.Type.AsFunctionType.ReturnType);
							}
						}
					}
					return recordBuilder.ToRecord();
				}
			}

			// Token: 0x020013B6 RID: 5046
			private class ToListFunctionValue : NativeFunctionValue1<ListValue, RecordValue>
			{
				// Token: 0x0600838A RID: 33674 RVA: 0x001BF2E9 File Offset: 0x001BD4E9
				public ToListFunctionValue()
					: base(TypeValue.List, "record", TypeValue.Record)
				{
				}

				// Token: 0x0600838B RID: 33675 RVA: 0x001BFF28 File Offset: 0x001BE128
				public override ListValue TypedInvoke(RecordValue record)
				{
					int count = record.Count;
					IValueReference[] array = new IValueReference[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = record.GetReference(i);
					}
					return ListValue.New(array);
				}
			}
		}

		// Token: 0x020013B7 RID: 5047
		public static class PrecisionEnum
		{
			// Token: 0x0400492B RID: 18731
			public static readonly IntEnumTypeValue<Precision> Type = new IntEnumTypeValue<Precision>("Precision.Type");

			// Token: 0x0400492C RID: 18732
			public static readonly NumberValue Double = Library.PrecisionEnum.Type.NewEnumValue("Precision.Double", 0, Precision.Double, null);

			// Token: 0x0400492D RID: 18733
			public static readonly NumberValue Decimal = Library.PrecisionEnum.Type.NewEnumValue("Precision.Decimal", 1, Precision.Decimal, null);
		}

		// Token: 0x020013B8 RID: 5048
		public static class Number
		{
			// Token: 0x0600838D RID: 33677 RVA: 0x001BFFB4 File Offset: 0x001BE1B4
			public static TextValue ConvertToText(NumberValue number, Value format, ICulture culture)
			{
				TextValue textValue;
				try
				{
					string text = (format.IsNull ? "R" : format.AsString);
					if (text.StartsWith("x", StringComparison.Ordinal) || text.StartsWith("X", StringComparison.Ordinal) || text.StartsWith("d", StringComparison.Ordinal) || text.StartsWith("D", StringComparison.Ordinal))
					{
						double asScientific = number.AsScientific64;
						long num = (long)asScientific;
						if (asScientific != (double)num)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Number_FormatFunction_ExpectedIntegerValue, number, null);
						}
						textValue = TextValue.New(number.AsInteger64.ToString(text, culture.Value));
					}
					else
					{
						textValue = TextValue.New(number.ToString(text, culture.Value));
					}
				}
				catch (FormatException ex)
				{
					throw ValueException.NewExpressionError(ex.Message, format, ex);
				}
				catch (NotSupportedException)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.Culture_NotSupported_Parsing(culture.Name), TextValue.New(culture.Name), null);
				}
				return textValue;
			}

			// Token: 0x0600838E RID: 33678 RVA: 0x001C00A8 File Offset: 0x001BE2A8
			private static void ThrowOnNegativeValue(Value value)
			{
				if (value.AsNumber.AsScientific64 < 0.0)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.FunctionOperatesOnlyOnNonNegativeIntegers(value.ToSource()), value, null);
				}
			}

			// Token: 0x0400492E RID: 18734
			private const string defaultFormat = "R";

			// Token: 0x0400492F RID: 18735
			public static readonly NumberValue NaN = NumberValue.New(double.NaN);

			// Token: 0x04004930 RID: 18736
			public static readonly NumberValue NegativeInfinity = NumberValue.New(double.NegativeInfinity);

			// Token: 0x04004931 RID: 18737
			public static readonly NumberValue PositiveInfinity = NumberValue.New(double.PositiveInfinity);

			// Token: 0x04004932 RID: 18738
			public static readonly NumberValue Epsilon = NumberValue.New(double.Epsilon);

			// Token: 0x04004933 RID: 18739
			public static readonly Value IsNaN = new Library.Number.IsNaNFunctionValue();

			// Token: 0x04004934 RID: 18740
			public static readonly Value E = NumberValue.New(2.718281828459045);

			// Token: 0x04004935 RID: 18741
			public static readonly Value PI = NumberValue.New(3.141592653589793);

			// Token: 0x04004936 RID: 18742
			public static readonly FunctionValue Abs = new Library.Number.AbsFunctionValue();

			// Token: 0x04004937 RID: 18743
			public static readonly FunctionValue Acos = new Library.Number.AcosFunctionValue();

			// Token: 0x04004938 RID: 18744
			public static readonly FunctionValue Asin = new Library.Number.AsinFunctionValue();

			// Token: 0x04004939 RID: 18745
			public static readonly FunctionValue Atan = new Library.Number.AtanFunctionValue();

			// Token: 0x0400493A RID: 18746
			public static readonly FunctionValue Atan2 = new Library.Number.Atan2FunctionValue();

			// Token: 0x0400493B RID: 18747
			public static readonly FunctionValue Combinations = new Library.Number.CombinationsFunctionValue();

			// Token: 0x0400493C RID: 18748
			public static readonly FunctionValue Cos = new Library.Number.CosFunctionValue();

			// Token: 0x0400493D RID: 18749
			public static readonly FunctionValue Cosh = new Library.Number.CoshFunctionValue();

			// Token: 0x0400493E RID: 18750
			public static readonly FunctionValue Exp = new Library.Number.ExpFunctionValue();

			// Token: 0x0400493F RID: 18751
			public static readonly FunctionValue Factorial = new Library.Number.FactorialFunctionValue();

			// Token: 0x04004940 RID: 18752
			public static readonly FunctionValue IntegerDivide = new Library.Number.IntegerDivideFunctionValue();

			// Token: 0x04004941 RID: 18753
			public static readonly FunctionValue Log = new Library.Number.LogFunctionValue();

			// Token: 0x04004942 RID: 18754
			public static readonly FunctionValue Log10 = new Library.Number.Log10FunctionValue();

			// Token: 0x04004943 RID: 18755
			public static readonly FunctionValue Ln = new Library.Number.LnFunctionValue();

			// Token: 0x04004944 RID: 18756
			public static readonly FunctionValue Mod = new Library.Number.ModFunctionValue();

			// Token: 0x04004945 RID: 18757
			public static readonly FunctionValue Permutations = new Library.Number.PermutationsFunctionValue();

			// Token: 0x04004946 RID: 18758
			public static readonly FunctionValue Power = new Library.Number.PowerFunctionValue();

			// Token: 0x04004947 RID: 18759
			public static readonly FunctionValue Round = new Library.Number.RoundFunctionValue();

			// Token: 0x04004948 RID: 18760
			public static readonly FunctionValue RoundAwayFromZero = new Library.Number.RoundAwayFromZeroFunctionValue();

			// Token: 0x04004949 RID: 18761
			public static readonly FunctionValue RoundDown = new Library.Number.RoundDownFunctionValue();

			// Token: 0x0400494A RID: 18762
			public static readonly FunctionValue RoundUp = new Library.Number.RoundUpFunctionValue();

			// Token: 0x0400494B RID: 18763
			public static readonly FunctionValue RoundTowardZero = new Library.Number.RoundTowardZeroFunctionValue();

			// Token: 0x0400494C RID: 18764
			public static readonly FunctionValue Sign = new Library.Number.SignFunctionValue();

			// Token: 0x0400494D RID: 18765
			public static readonly FunctionValue Sin = new Library.Number.SinFunctionValue();

			// Token: 0x0400494E RID: 18766
			public static readonly FunctionValue Sinh = new Library.Number.SinhFunctionValue();

			// Token: 0x0400494F RID: 18767
			public static readonly FunctionValue Sqrt = new Library.Number.SqrtFunctionValue();

			// Token: 0x04004950 RID: 18768
			public static readonly FunctionValue Tan = new Library.Number.TanFunctionValue();

			// Token: 0x04004951 RID: 18769
			public static readonly FunctionValue Tanh = new Library.Number.TanhFunctionValue();

			// Token: 0x04004952 RID: 18770
			public static readonly FunctionValue IsEven = new Library.Number.IsEvenFunctionValue();

			// Token: 0x04004953 RID: 18771
			public static readonly FunctionValue IsOdd = new Library.Number.IsOddFunctionValue();

			// Token: 0x020013B9 RID: 5049
			private class IsNaNFunctionValue : NativeFunctionValue1<LogicalValue, NumberValue>
			{
				// Token: 0x06008390 RID: 33680 RVA: 0x001C0289 File Offset: 0x001BE489
				public IsNaNFunctionValue()
					: base(TypeValue.Logical, "number", TypeValue.Number)
				{
				}

				// Token: 0x06008391 RID: 33681 RVA: 0x001C02A0 File Offset: 0x001BE4A0
				public override LogicalValue TypedInvoke(NumberValue number)
				{
					return LogicalValue.New(number.AsNumber.IsNaN);
				}
			}

			// Token: 0x020013BA RID: 5050
			public class FromTextFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008392 RID: 33682 RVA: 0x001C02B4 File Offset: 0x001BE4B4
				public FromTextFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, NullableTypeValue.Number, 1, "text", NullableTypeValue.Text, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x17002353 RID: 9043
				// (get) Token: 0x06008393 RID: 33683 RVA: 0x001C02E3 File Offset: 0x001BE4E3
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.NumberFromText;
					}
				}

				// Token: 0x06008394 RID: 33684 RVA: 0x001C02EC File Offset: 0x001BE4EC
				public override Value TypedInvoke(Value text, Value culture)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					string text2 = text.AsString.Trim(Library.whitespaceChars);
					if (text2.Length == 0)
					{
						return Value.Null;
					}
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					NumberValue numberValue;
					if (NumberValue.TryParse(text2, NumberStyles.Any, cultureInfo, out numberValue))
					{
						return numberValue;
					}
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Number_FromFunction_NotConvertibleToNumber, text, null);
				}
			}

			// Token: 0x020013BB RID: 5051
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008395 RID: 33685 RVA: 0x001C0360 File Offset: 0x001BE560
				public FromFunctionValue(IEngineHost host, ICulture boundCulture = null)
					: base(host, boundCulture, NullableTypeValue.Number, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
					this.fromText = new Library.Number.FromTextFunctionValue(host, boundCulture);
				}

				// Token: 0x17002354 RID: 9044
				// (get) Token: 0x06008396 RID: 33686 RVA: 0x001C039C File Offset: 0x001BE59C
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.NumberFrom;
					}
				}

				// Token: 0x17002355 RID: 9045
				// (get) Token: 0x06008397 RID: 33687 RVA: 0x001C03A3 File Offset: 0x001BE5A3
				protected ITimeZone TimeZone
				{
					get
					{
						if (this.timeZone == null)
						{
							this.timeZone = base.QueryService<ITimeZoneService>().DefaultTimeZone;
						}
						return this.timeZone;
					}
				}

				// Token: 0x06008398 RID: 33688 RVA: 0x001C03C4 File Offset: 0x001BE5C4
				private static Value ToOADateHelper(Value value, global::System.DateTime time)
				{
					if (time.Year < 100)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, value, null);
					}
					Value value2;
					try
					{
						value2 = NumberValue.New(time.ToOADate());
					}
					catch (OverflowException ex)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Date_OutOfRangeError, value, ex);
					}
					return value2;
				}

				// Token: 0x06008399 RID: 33689 RVA: 0x001C0418 File Offset: 0x001BE618
				public override Value TypedInvoke(Value value, Value culture)
				{
					switch (value.Kind)
					{
					case ValueKind.Null:
						return value;
					case ValueKind.Time:
						return NumberValue.New(value.AsTime.AsClrTimeSpan.TotalDays);
					case ValueKind.Date:
						return Library.Number.FromFunctionValue.ToOADateHelper(value, value.AsDate.AsClrDateTime);
					case ValueKind.DateTime:
						return Library.Number.FromFunctionValue.ToOADateHelper(value, value.AsDateTime.AsClrDateTime);
					case ValueKind.DateTimeZone:
						return Library.Number.FromFunctionValue.ToOADateHelper(value, value.AsDateTimeZone.AsClrDateTimeOffset.UtcDateTime.AdjustForTimeZone(this.TimeZone));
					case ValueKind.Duration:
						return NumberValue.New(value.AsDuration.AsClrTimeSpan.TotalDays);
					case ValueKind.Number:
						return value;
					case ValueKind.Logical:
						return NumberValue.New(Convert.ToInt32(value.AsBoolean));
					case ValueKind.Text:
						return this.fromText.Invoke(value, culture);
					default:
						throw ValueException.NewDataFormatError<Message0>(Strings.Number_FromFunction_NotConvertibleToNumber, value, null);
					}
				}

				// Token: 0x04004954 RID: 18772
				private readonly Library.Number.FromTextFunctionValue fromText;

				// Token: 0x04004955 RID: 18773
				private ITimeZone timeZone;
			}

			// Token: 0x020013BC RID: 5052
			public class ToTextFunctionValue : CultureSpecificFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x0600839A RID: 33690 RVA: 0x001C0508 File Offset: 0x001BE708
				public ToTextFunctionValue(IEngineHost host)
					: base(host, null, NullableTypeValue.Text, 1, "number", NullableTypeValue.Number, "format", NullableTypeValue.Text, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600839B RID: 33691 RVA: 0x001C0541 File Offset: 0x001BE741
				public override Value TypedInvoke(Value number, Value format, Value culture)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Library.Number.ConvertToText(number.AsNumber, format, base.GetCulture(culture));
				}
			}

			// Token: 0x020013BD RID: 5053
			private class AbsFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600839C RID: 33692 RVA: 0x001C0564 File Offset: 0x001BE764
				public AbsFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600839D RID: 33693 RVA: 0x001C057B File Offset: 0x001BE77B
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return number.AsNumber.Abs();
				}
			}

			// Token: 0x020013BE RID: 5054
			private class AcosFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600839E RID: 33694 RVA: 0x001C0564 File Offset: 0x001BE764
				public AcosFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600839F RID: 33695 RVA: 0x001C0596 File Offset: 0x001BE796
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Acos(number.AsNumber);
				}
			}

			// Token: 0x020013BF RID: 5055
			private class AsinFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083A0 RID: 33696 RVA: 0x001C0564 File Offset: 0x001BE764
				public AsinFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083A1 RID: 33697 RVA: 0x001C05B6 File Offset: 0x001BE7B6
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Asin(number.AsNumber);
				}
			}

			// Token: 0x020013C0 RID: 5056
			private class AtanFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083A2 RID: 33698 RVA: 0x001C0564 File Offset: 0x001BE764
				public AtanFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083A3 RID: 33699 RVA: 0x001C05D6 File Offset: 0x001BE7D6
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Atan(number.AsNumber);
				}
			}

			// Token: 0x020013C1 RID: 5057
			private class Atan2FunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083A4 RID: 33700 RVA: 0x001C05F6 File Offset: 0x001BE7F6
				public Atan2FunctionValue()
					: base(NullableTypeValue.Number, "y", NullableTypeValue.Number, "x", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083A5 RID: 33701 RVA: 0x001C0617 File Offset: 0x001BE817
				public override Value TypedInvoke(Value y, Value x)
				{
					if (x.IsNull || y.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Atan2(y.AsNumber, x.AsNumber);
				}
			}

			// Token: 0x020013C2 RID: 5058
			private class CombinationsFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083A6 RID: 33702 RVA: 0x001C0645 File Offset: 0x001BE845
				public CombinationsFunctionValue()
					: base(NullableTypeValue.Number, "setSize", NullableTypeValue.Number, "combinationSize", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083A7 RID: 33703 RVA: 0x001C0666 File Offset: 0x001BE866
				public override Value TypedInvoke(Value setSize, Value combinationSize)
				{
					if (setSize.IsNull || combinationSize.IsNull)
					{
						return Value.Null;
					}
					Library.Number.ThrowOnNegativeValue(setSize);
					Library.Number.ThrowOnNegativeValue(combinationSize);
					return Precision.Double.Combinations(setSize.AsNumber, combinationSize.AsNumber);
				}
			}

			// Token: 0x020013C3 RID: 5059
			private class CosFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083A8 RID: 33704 RVA: 0x001C0564 File Offset: 0x001BE764
				public CosFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083A9 RID: 33705 RVA: 0x001C06A0 File Offset: 0x001BE8A0
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Cos(number.AsNumber);
				}
			}

			// Token: 0x020013C4 RID: 5060
			private class CoshFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083AA RID: 33706 RVA: 0x001C0564 File Offset: 0x001BE764
				public CoshFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083AB RID: 33707 RVA: 0x001C06C0 File Offset: 0x001BE8C0
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Cosh(number.AsNumber);
				}
			}

			// Token: 0x020013C5 RID: 5061
			private class ExpFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083AC RID: 33708 RVA: 0x001C0564 File Offset: 0x001BE764
				public ExpFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083AD RID: 33709 RVA: 0x001C06E0 File Offset: 0x001BE8E0
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Exp(number.AsNumber);
				}
			}

			// Token: 0x020013C6 RID: 5062
			private class FactorialFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083AE RID: 33710 RVA: 0x001C0564 File Offset: 0x001BE764
				public FactorialFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083AF RID: 33711 RVA: 0x001C0700 File Offset: 0x001BE900
				public override Value TypedInvoke(Value number)
				{
					return Library.Number.Permutations.Invoke(number, number);
				}
			}

			// Token: 0x020013C7 RID: 5063
			private class IntegerDivideFunctionValue : NativeFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x060083B0 RID: 33712 RVA: 0x001C0710 File Offset: 0x001BE910
				public IntegerDivideFunctionValue()
					: base(NullableTypeValue.Number, 2, "number1", NullableTypeValue.Number, "number2", NullableTypeValue.Number, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}

				// Token: 0x060083B1 RID: 33713 RVA: 0x001C074C File Offset: 0x001BE94C
				public override Value TypedInvoke(Value number1, Value number2, Value precisionEnum)
				{
					if (number1.IsNull || number2.IsNull)
					{
						return Value.Null;
					}
					return (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).IntegerDivide(number1.AsNumber, number2.AsNumber);
				}
			}

			// Token: 0x020013C8 RID: 5064
			private class LogFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083B2 RID: 33714 RVA: 0x001C079F File Offset: 0x001BE99F
				public LogFunctionValue()
					: base(NullableTypeValue.Number, 1, "number", NullableTypeValue.Number, "base", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083B3 RID: 33715 RVA: 0x001C07C1 File Offset: 0x001BE9C1
				public override Value TypedInvoke(Value number, Value newBase)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					if (newBase.IsNull)
					{
						newBase = NumberValue.E;
					}
					return Precision.Double.Log(number.AsNumber, newBase.AsNumber);
				}
			}

			// Token: 0x020013C9 RID: 5065
			private class LnFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083B4 RID: 33716 RVA: 0x001C0564 File Offset: 0x001BE764
				public LnFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083B5 RID: 33717 RVA: 0x001C07F6 File Offset: 0x001BE9F6
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Log(number.AsNumber, NumberValue.E);
				}
			}

			// Token: 0x020013CA RID: 5066
			private class Log10FunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083B6 RID: 33718 RVA: 0x001C0564 File Offset: 0x001BE764
				public Log10FunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083B7 RID: 33719 RVA: 0x001C081B File Offset: 0x001BEA1B
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Log(number.AsNumber, NumberValue.Ten);
				}
			}

			// Token: 0x020013CB RID: 5067
			private class ModFunctionValue : NativeFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x060083B8 RID: 33720 RVA: 0x001C0840 File Offset: 0x001BEA40
				public ModFunctionValue()
					: base(NullableTypeValue.Number, 2, "number", NullableTypeValue.Number, "divisor", NullableTypeValue.Number, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}

				// Token: 0x060083B9 RID: 33721 RVA: 0x001C087C File Offset: 0x001BEA7C
				public override Value TypedInvoke(Value number, Value divisor, Value precisionEnum)
				{
					if (number.IsNull || divisor.IsNull)
					{
						return Value.Null;
					}
					return (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Mod(number.AsNumber, divisor.AsNumber);
				}
			}

			// Token: 0x020013CC RID: 5068
			private class PermutationsFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083BA RID: 33722 RVA: 0x001C08CF File Offset: 0x001BEACF
				public PermutationsFunctionValue()
					: base(NullableTypeValue.Number, "setSize", NullableTypeValue.Number, "permutationSize", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083BB RID: 33723 RVA: 0x001C08F0 File Offset: 0x001BEAF0
				public override Value TypedInvoke(Value setSize, Value permutationSize)
				{
					if (setSize.IsNull || permutationSize.IsNull)
					{
						return Value.Null;
					}
					Library.Number.ThrowOnNegativeValue(setSize);
					Library.Number.ThrowOnNegativeValue(permutationSize);
					return NumberValue.New(Library.Number.PermutationsFunctionValue.Permutations(setSize.AsNumber.AsInteger64, permutationSize.AsNumber.AsInteger64));
				}

				// Token: 0x060083BC RID: 33724 RVA: 0x001C0940 File Offset: 0x001BEB40
				private static double Permutations(long n, long k)
				{
					if (k > n)
					{
						return 0.0;
					}
					if (k == 0L)
					{
						return 1.0;
					}
					if (k == 1L)
					{
						return (double)n;
					}
					double num = 1.0;
					long num2 = n - k + 1L;
					while (n >= num2 && !double.IsInfinity(num))
					{
						num *= (double)n;
						n -= 1L;
					}
					return num;
				}
			}

			// Token: 0x020013CD RID: 5069
			private class PowerFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083BD RID: 33725 RVA: 0x001C099B File Offset: 0x001BEB9B
				public PowerFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number, "power", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083BE RID: 33726 RVA: 0x001C09BC File Offset: 0x001BEBBC
				public override Value TypedInvoke(Value number, Value power)
				{
					if (number.IsNull || power.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Power(number.AsNumber, power.AsNumber);
				}
			}

			// Token: 0x020013CE RID: 5070
			public sealed class RandomFunctionValue : NativeFunctionValue0<NumberValue>
			{
				// Token: 0x060083BF RID: 33727 RVA: 0x001C09EA File Offset: 0x001BEBEA
				public RandomFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Number)
				{
					this.engineHost = engineHost;
					this.listRandom = new Library.List.RandomFunctionValue(engineHost);
				}

				// Token: 0x17002356 RID: 9046
				// (get) Token: 0x060083C0 RID: 33728 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x060083C1 RID: 33729 RVA: 0x001C0A0C File Offset: 0x001BEC0C
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && this.engineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}

				// Token: 0x060083C2 RID: 33730 RVA: 0x001C0A5B File Offset: 0x001BEC5B
				public override NumberValue TypedInvoke()
				{
					return this.listRandom.Invoke(NumberValue.One, Value.Null)[0].AsNumber;
				}

				// Token: 0x04004956 RID: 18774
				private readonly IEngineHost engineHost;

				// Token: 0x04004957 RID: 18775
				private readonly FunctionValue listRandom;
			}

			// Token: 0x020013CF RID: 5071
			public sealed class RandomBetweenFunctionValue : NativeFunctionValue2<NumberValue, NumberValue, NumberValue>, IOpaqueFunctionValue, IFunctionValue, IValue
			{
				// Token: 0x060083C3 RID: 33731 RVA: 0x001C0A7D File Offset: 0x001BEC7D
				public RandomBetweenFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Number, "bottom", TypeValue.Number, "top", TypeValue.Number)
				{
					this.listRandom = new Library.List.RandomFunctionValue(engineHost);
				}

				// Token: 0x17002357 RID: 9047
				// (get) Token: 0x060083C4 RID: 33732 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x060083C5 RID: 33733 RVA: 0x001C0AAC File Offset: 0x001BECAC
				public override NumberValue TypedInvoke(NumberValue bottom, NumberValue top)
				{
					double asDouble = this.listRandom.Invoke(NumberValue.One, Value.Null)[0].AsNumber.AsNumber.AsDouble;
					double asDouble2 = bottom.AsDouble;
					double asDouble3 = top.AsDouble;
					if (asDouble3 < asDouble2)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Number_RandBetween_TopLessThanBottom, top, null);
					}
					return NumberValue.New(asDouble * (asDouble3 - asDouble2) + asDouble2);
				}

				// Token: 0x04004958 RID: 18776
				private readonly FunctionValue listRandom;
			}

			// Token: 0x020013D0 RID: 5072
			private class RoundFunctionValue : NativeFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x060083C6 RID: 33734 RVA: 0x001C0B10 File Offset: 0x001BED10
				public RoundFunctionValue()
					: base(NullableTypeValue.Number, 1, "number", NullableTypeValue.Number, "digits", NullableTypeValue.Number, "roundingMode", Library.RoundingMode.Type.Nullable)
				{
				}

				// Token: 0x060083C7 RID: 33735 RVA: 0x001C0B4C File Offset: 0x001BED4C
				public override Value TypedInvoke(Value number, Value digits, Value roundingMode)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					if (digits.IsNull)
					{
						digits = NumberValue.Zero;
					}
					if (roundingMode.IsNull)
					{
						roundingMode = Library.RoundingMode.ToEven;
					}
					return number.AsNumber.Round(digits.AsNumber, this.GetRoundingMode(roundingMode.AsNumber));
				}

				// Token: 0x060083C8 RID: 33736 RVA: 0x001C0BA2 File Offset: 0x001BEDA2
				private NumberValue.RoundingMode GetRoundingMode(NumberValue roundingMode)
				{
					return Library.RoundingMode.Type.GetValue(roundingMode);
				}
			}

			// Token: 0x020013D1 RID: 5073
			private class RoundAwayFromZeroFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083C9 RID: 33737 RVA: 0x001C0BAF File Offset: 0x001BEDAF
				public RoundAwayFromZeroFunctionValue()
					: base(NullableTypeValue.Number, 1, "number", NullableTypeValue.Number, "digits", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083CA RID: 33738 RVA: 0x001C0BD1 File Offset: 0x001BEDD1
				public override Value TypedInvoke(Value number, Value digits)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					if (number.GreaterThanOrEqual(NumberValue.Zero))
					{
						return Library.Number.RoundUp.Invoke(number, digits);
					}
					return Library.Number.RoundDown.Invoke(number, digits);
				}
			}

			// Token: 0x020013D2 RID: 5074
			private class RoundUpFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083CB RID: 33739 RVA: 0x001C0BAF File Offset: 0x001BEDAF
				public RoundUpFunctionValue()
					: base(NullableTypeValue.Number, 1, "number", NullableTypeValue.Number, "digits", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083CC RID: 33740 RVA: 0x001C0C08 File Offset: 0x001BEE08
				public override Value TypedInvoke(Value number, Value digits)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					int num = (digits.IsNull ? 0 : digits.AsInteger32);
					return number.AsNumber.Ceiling(num);
				}
			}

			// Token: 0x020013D3 RID: 5075
			private class RoundDownFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083CD RID: 33741 RVA: 0x001C0BAF File Offset: 0x001BEDAF
				public RoundDownFunctionValue()
					: base(NullableTypeValue.Number, 1, "number", NullableTypeValue.Number, "digits", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083CE RID: 33742 RVA: 0x001C0C44 File Offset: 0x001BEE44
				public override Value TypedInvoke(Value number, Value digits)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					int num = (digits.IsNull ? 0 : digits.AsInteger32);
					return number.AsNumber.Floor(num);
				}
			}

			// Token: 0x020013D4 RID: 5076
			private class RoundTowardZeroFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083CF RID: 33743 RVA: 0x001C0BAF File Offset: 0x001BEDAF
				public RoundTowardZeroFunctionValue()
					: base(NullableTypeValue.Number, 1, "number", NullableTypeValue.Number, "digits", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083D0 RID: 33744 RVA: 0x001C0C80 File Offset: 0x001BEE80
				public override Value TypedInvoke(Value number, Value digits)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					int num = (digits.IsNull ? 0 : digits.AsInteger32);
					return number.AsNumber.Truncate(num);
				}
			}

			// Token: 0x020013D5 RID: 5077
			private class SignFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083D1 RID: 33745 RVA: 0x001C0CB9 File Offset: 0x001BEEB9
				public SignFunctionValue()
					: base(NullableTypeValue.Int32, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083D2 RID: 33746 RVA: 0x001C0CD0 File Offset: 0x001BEED0
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					if (number.AsNumber.IsNaN)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.InvalidArgument, Library.Number.NaN, null);
					}
					int num = number.CompareTo(NumberValue.Zero);
					if (num < 0)
					{
						return NumberValue.NegativeOne;
					}
					if (num > 0)
					{
						return NumberValue.One;
					}
					return NumberValue.Zero;
				}
			}

			// Token: 0x020013D6 RID: 5078
			private class SinFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083D3 RID: 33747 RVA: 0x001C0564 File Offset: 0x001BE764
				public SinFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083D4 RID: 33748 RVA: 0x001C0D2E File Offset: 0x001BEF2E
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Sin(number.AsNumber);
				}
			}

			// Token: 0x020013D7 RID: 5079
			private class SinhFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083D5 RID: 33749 RVA: 0x001C0564 File Offset: 0x001BE764
				public SinhFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083D6 RID: 33750 RVA: 0x001C0D4E File Offset: 0x001BEF4E
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Sinh(number.AsNumber);
				}
			}

			// Token: 0x020013D8 RID: 5080
			private class SqrtFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083D7 RID: 33751 RVA: 0x001C0564 File Offset: 0x001BE764
				public SqrtFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083D8 RID: 33752 RVA: 0x001C0D6E File Offset: 0x001BEF6E
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Sqrt(number.AsNumber);
				}
			}

			// Token: 0x020013D9 RID: 5081
			private class TanFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083D9 RID: 33753 RVA: 0x001C0564 File Offset: 0x001BE764
				public TanFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083DA RID: 33754 RVA: 0x001C0D8E File Offset: 0x001BEF8E
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return Precision.Double.Tan(number.AsNumber);
				}
			}

			// Token: 0x020013DA RID: 5082
			private class TanhFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060083DB RID: 33755 RVA: 0x001C0564 File Offset: 0x001BE764
				public TanhFunctionValue()
					: base(NullableTypeValue.Number, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x060083DC RID: 33756 RVA: 0x001C0DAE File Offset: 0x001BEFAE
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(Math.Tanh(number.AsNumber.AsDouble));
				}
			}

			// Token: 0x020013DB RID: 5083
			private class IsEvenFunctionValue : NativeFunctionValue1<LogicalValue, NumberValue>
			{
				// Token: 0x060083DD RID: 33757 RVA: 0x001C0289 File Offset: 0x001BE489
				public IsEvenFunctionValue()
					: base(TypeValue.Logical, "number", TypeValue.Number)
				{
				}

				// Token: 0x060083DE RID: 33758 RVA: 0x001C0DD3 File Offset: 0x001BEFD3
				public override LogicalValue TypedInvoke(NumberValue number)
				{
					return LogicalValue.New(Math.Floor(number.AsScientific64) % 2.0 == 0.0);
				}
			}

			// Token: 0x020013DC RID: 5084
			private class IsOddFunctionValue : NativeFunctionValue1<LogicalValue, NumberValue>
			{
				// Token: 0x060083DF RID: 33759 RVA: 0x001C0289 File Offset: 0x001BE489
				public IsOddFunctionValue()
					: base(TypeValue.Logical, "number", TypeValue.Number)
				{
				}

				// Token: 0x060083E0 RID: 33760 RVA: 0x001C0DFA File Offset: 0x001BEFFA
				public override LogicalValue TypedInvoke(NumberValue number)
				{
					return LogicalValue.New(Math.Abs(Math.Floor(number.AsScientific64)) % 2.0 == 1.0);
				}
			}
		}

		// Token: 0x020013DD RID: 5085
		public static class Currency
		{
			// Token: 0x04004959 RID: 18777
			private const decimal MaxValue = 922337203685477.5807m;

			// Token: 0x0400495A RID: 18778
			private const decimal MinValue = -922337203685477.5808m;

			// Token: 0x020013DE RID: 5086
			public class FromFunctionValue : CultureSpecificFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x060083E2 RID: 33762 RVA: 0x001C0E50 File Offset: 0x001BF050
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, TypeValue.Currency.Nullable, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text, "roundingMode", Library.RoundingMode.Type.Nullable)
				{
					this.fromNumber = new Library.Number.FromFunctionValue(engineHost, boundCulture);
				}

				// Token: 0x17002358 RID: 9048
				// (get) Token: 0x060083E3 RID: 33763 RVA: 0x00002139 File Offset: 0x00000339
				protected override int CultureArgumentPosition
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x17002359 RID: 9049
				// (get) Token: 0x060083E4 RID: 33764 RVA: 0x001C0EA0 File Offset: 0x001BF0A0
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.CurrencyFrom;
					}
				}

				// Token: 0x060083E5 RID: 33765 RVA: 0x001C0EA8 File Offset: 0x001BF0A8
				public override Value TypedInvoke(Value value, Value culture, Value roundingMode)
				{
					Value value2 = this.fromNumber.Invoke(value, culture);
					if (value2.IsNull)
					{
						return value2;
					}
					if (roundingMode.IsNull)
					{
						roundingMode = Library.RoundingMode.ToEven;
					}
					decimal asDecimal = Library.Number.Round.Invoke(value2, NumberValue.New(4), roundingMode).AsNumber.AsDecimal;
					if (asDecimal < -922337203685477.5808m || asDecimal > 922337203685477.5807m)
					{
						throw ValueException.NumberOutOfRange<Message2>(Strings.CurrencyOutOfRange(-922337203685477.5808m, 922337203685477.5807m), this, null);
					}
					return NumberValue.New(asDecimal);
				}

				// Token: 0x0400495B RID: 18779
				private readonly FunctionValue fromNumber;
			}
		}

		// Token: 0x020013DF RID: 5087
		public static class Percentage
		{
			// Token: 0x020013E0 RID: 5088
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083E6 RID: 33766 RVA: 0x001C0F60 File Offset: 0x001BF160
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, TypeValue.Percentage.Nullable, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
					this.fromNumber = new Library.Number.FromFunctionValue(engineHost, boundCulture);
				}

				// Token: 0x1700235A RID: 9050
				// (get) Token: 0x060083E7 RID: 33767 RVA: 0x001C0FA1 File Offset: 0x001BF1A1
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.PercentageFrom;
					}
				}

				// Token: 0x060083E8 RID: 33768 RVA: 0x001C0FA8 File Offset: 0x001BF1A8
				public override Value TypedInvoke(Value value, Value culture)
				{
					Value value2 = this.fromNumber.Invoke(value, culture);
					if (value2.IsNull || value2.AsNumber.NumberKind == NumberKind.Decimal)
					{
						return value2;
					}
					return NumberValue.New(value2.AsNumber.ToDecimal());
				}

				// Token: 0x0400495C RID: 18780
				private readonly FunctionValue fromNumber;
			}
		}

		// Token: 0x020013E1 RID: 5089
		public static class _Guid
		{
			// Token: 0x0400495D RID: 18781
			public static readonly FunctionValue From = new Library._Guid.FromFunctionValue();

			// Token: 0x020013E2 RID: 5090
			private class FromFunctionValue : NativeFunctionValue1
			{
				// Token: 0x060083EA RID: 33770 RVA: 0x001C0FF7 File Offset: 0x001BF1F7
				public FromFunctionValue()
					: base(1, "value")
				{
				}

				// Token: 0x1700235B RID: 9051
				// (get) Token: 0x060083EB RID: 33771 RVA: 0x001BE59A File Offset: 0x001BC79A
				protected override TypeValue Type0
				{
					get
					{
						return TypeValue.Text.Nullable;
					}
				}

				// Token: 0x1700235C RID: 9052
				// (get) Token: 0x060083EC RID: 33772 RVA: 0x001C1005 File Offset: 0x001BF205
				protected override TypeValue ReturnType
				{
					get
					{
						return TypeValue.Guid.Nullable;
					}
				}

				// Token: 0x060083ED RID: 33773 RVA: 0x001C1014 File Offset: 0x001BF214
				public override Value Invoke(Value value)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					TextValue asText = value.AsText;
					string text = asText.AsString.Trim(Library.whitespaceChars);
					if (text.Length == 0)
					{
						return Value.Null;
					}
					Value value2;
					try
					{
						value2 = TextValue.New(new Guid(text).ToString());
					}
					catch (FormatException)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.Guid_FromFunction_NotConvertibleToGuid, asText, null);
					}
					catch (OverflowException)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.Guid_FromFunction_NotConvertibleToGuid, asText, null);
					}
					return value2;
				}
			}
		}

		// Token: 0x020013E3 RID: 5091
		public static class ValueAndType
		{
			// Token: 0x020013E4 RID: 5092
			public class FromTextFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083EE RID: 33774 RVA: 0x001C10AC File Offset: 0x001BF2AC
				public FromTextFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Record, 1, "text", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x060083EF RID: 33775 RVA: 0x001C10DC File Offset: 0x001BF2DC
				public override Value TypedInvoke(Value text, Value culture)
				{
					TypeValue typeValue;
					Value value = Library._Value.FromTextFunctionValue.GetValueAndType(text, base.GetCulture(culture).GetCultureInfo(), out typeValue);
					if (value.IsNumber && (typeValue == TypeValue.Double || typeValue == TypeValue.Decimal))
					{
						NumberValue asNumber = value.AsNumber;
						if (asNumber.IsInteger32)
						{
							value = NumberValue.New(asNumber.AsInteger32);
							typeValue = TypeValue.Int32;
						}
						else if (asNumber.IsInteger64)
						{
							value = NumberValue.New(asNumber.AsInteger64);
							typeValue = TypeValue.Int64;
						}
					}
					return RecordValue.New(Library.ValueAndType.FromTextFunctionValue.keys, new Value[] { value, typeValue });
				}

				// Token: 0x0400495E RID: 18782
				private static readonly Keys keys = Keys.New("Value", "Type");
			}
		}

		// Token: 0x020013E5 RID: 5093
		public static class NumberAliasTypes
		{
			// Token: 0x060083F1 RID: 33777 RVA: 0x001C1184 File Offset: 0x001BF384
			private static bool TryReduceNumberFrom(IInvocationExpression invocation, out IExpression reduced)
			{
				IList<IExpression> list;
				if (invocation.Arguments.Count == 1 && invocation.Arguments[0].TryGetInvocation(Library.Logical.From, 1, out list) && Library.NumberAliasTypes.ReturnsZeroOrOne(list[0]))
				{
					reduced = list[0];
					return true;
				}
				reduced = null;
				return false;
			}

			// Token: 0x060083F2 RID: 33778 RVA: 0x001C11D8 File Offset: 0x001BF3D8
			private static bool ReturnsZeroOrOne(IExpression expression)
			{
				ExpressionKind kind = expression.Kind;
				if (kind == ExpressionKind.Constant)
				{
					IConstantExpression2 constantExpression = (IConstantExpression2)expression;
					return constantExpression.Value.Equals(NumberValue.Zero) || constantExpression.Value.Equals(NumberValue.One);
				}
				if (kind != ExpressionKind.If)
				{
					return false;
				}
				IIfExpression ifExpression = (IIfExpression)expression;
				return Library.NumberAliasTypes.ReturnsZeroOrOne(ifExpression.TrueCase) && Library.NumberAliasTypes.ReturnsZeroOrOne(ifExpression.FalseCase);
			}

			// Token: 0x020013E6 RID: 5094
			public class FromIntType : CultureSpecificFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x060083F3 RID: 33779 RVA: 0x001C1248 File Offset: 0x001BF448
				public FromIntType(IEngineHost engineHost, TypeCode typeCode, ICulture boundCulture = null)
					: base(engineHost, boundCulture, Library.NumberAliasTypes.FromIntType.GetReturnType(typeCode), 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text, "roundingMode", Library.RoundingMode.Type.Nullable)
				{
					this.fromNumber = new Library.Number.FromFunctionValue(engineHost, boundCulture);
					this.typeCode = typeCode;
				}

				// Token: 0x060083F4 RID: 33780 RVA: 0x001C129C File Offset: 0x001BF49C
				private static TypeValue GetReturnType(TypeCode typeCode)
				{
					switch (typeCode)
					{
					case TypeCode.SByte:
						return TypeValue.Int8.Nullable;
					case TypeCode.Byte:
						return TypeValue.Byte.Nullable;
					case TypeCode.Int16:
						return TypeValue.Int16.Nullable;
					case TypeCode.Int32:
						return TypeValue.Int32.Nullable;
					case TypeCode.Int64:
						return TypeValue.Int64.Nullable;
					}
					return TypeValue.Number.Nullable;
				}

				// Token: 0x1700235D RID: 9053
				// (get) Token: 0x060083F5 RID: 33781 RVA: 0x00002139 File Offset: 0x00000339
				protected override int CultureArgumentPosition
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x1700235E RID: 9054
				// (get) Token: 0x060083F6 RID: 33782 RVA: 0x001C1310 File Offset: 0x001BF510
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						switch (this.typeCode)
						{
						case TypeCode.SByte:
							return CultureSpecificFunction.Int8From;
						case TypeCode.Byte:
							return CultureSpecificFunction.ByteFrom;
						case TypeCode.Int16:
							return CultureSpecificFunction.Int16From;
						case TypeCode.Int32:
							return CultureSpecificFunction.Int32From;
						case TypeCode.Int64:
							return CultureSpecificFunction.Int64From;
						}
						throw new InvalidOperationException();
					}
				}

				// Token: 0x1700235F RID: 9055
				// (get) Token: 0x060083F7 RID: 33783 RVA: 0x001C136D File Offset: 0x001BF56D
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new Library.NumberAliasTypes.TypeCultureCodeIdentity(base.GetType(), base.BoundCulture, this.typeCode);
					}
				}

				// Token: 0x060083F8 RID: 33784 RVA: 0x001C1388 File Offset: 0x001BF588
				public override Value TypedInvoke(Value value, Value culture, Value roundingMode)
				{
					Value value2 = this.fromNumber.Invoke(value, culture);
					if (value2.IsNull)
					{
						return Value.Null;
					}
					if (roundingMode.IsNull)
					{
						roundingMode = Library.RoundingMode.ToEven;
					}
					NumberValue asNumber = Library.Number.Round.Invoke(value2, NumberValue.Zero, roundingMode).AsNumber;
					switch (this.typeCode)
					{
					case TypeCode.SByte:
						return NumberValue.New((int)asNumber.ToInt8());
					case TypeCode.Byte:
						return NumberValue.New((int)asNumber.ToByte());
					case TypeCode.Int16:
						return NumberValue.New((int)asNumber.ToInt16());
					case TypeCode.Int32:
						return NumberValue.New(asNumber.ToInt32());
					case TypeCode.Int64:
						return NumberValue.New(asNumber.ToInt64());
					}
					return asNumber;
				}

				// Token: 0x060083F9 RID: 33785 RVA: 0x001C1441 File Offset: 0x001BF641
				public override bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					return Library.NumberAliasTypes.TryReduceNumberFrom(invocation, out expression) || base.TryRewriteInvocation(invocation, environment, out expression);
				}

				// Token: 0x0400495F RID: 18783
				private readonly FunctionValue fromNumber;

				// Token: 0x04004960 RID: 18784
				private readonly TypeCode typeCode;
			}

			// Token: 0x020013E7 RID: 5095
			public class FromNonIntType : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060083FA RID: 33786 RVA: 0x001C1458 File Offset: 0x001BF658
				public FromNonIntType(IEngineHost engineHost, TypeCode typeCode, ICulture boundCulture = null)
					: base(engineHost, boundCulture, Library.NumberAliasTypes.FromNonIntType.GetReturnType(typeCode), 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
					this.fromNumber = new Library.Number.FromFunctionValue(engineHost, boundCulture);
					this.typeCode = typeCode;
				}

				// Token: 0x060083FB RID: 33787 RVA: 0x001C149C File Offset: 0x001BF69C
				private static TypeValue GetReturnType(TypeCode typeCode)
				{
					switch (typeCode)
					{
					case TypeCode.Single:
						return TypeValue.Single.Nullable;
					case TypeCode.Double:
						return TypeValue.Double.Nullable;
					case TypeCode.Decimal:
						return TypeValue.Decimal.Nullable;
					default:
						return TypeValue.Number.Nullable;
					}
				}

				// Token: 0x17002360 RID: 9056
				// (get) Token: 0x060083FC RID: 33788 RVA: 0x001C14EB File Offset: 0x001BF6EB
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new Library.NumberAliasTypes.TypeCultureCodeIdentity(base.GetType(), base.BoundCulture, this.typeCode);
					}
				}

				// Token: 0x17002361 RID: 9057
				// (get) Token: 0x060083FD RID: 33789 RVA: 0x001C1504 File Offset: 0x001BF704
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						switch (this.typeCode)
						{
						case TypeCode.Single:
							return CultureSpecificFunction.SingleFrom;
						case TypeCode.Double:
							return CultureSpecificFunction.DoubleFrom;
						case TypeCode.Decimal:
							return CultureSpecificFunction.DecimalFrom;
						default:
							throw new InvalidOperationException();
						}
					}
				}

				// Token: 0x060083FE RID: 33790 RVA: 0x001C1548 File Offset: 0x001BF748
				public override Value TypedInvoke(Value value, Value culture)
				{
					Value value2 = this.fromNumber.Invoke(value, culture);
					if (value2.IsNull)
					{
						return Value.Null;
					}
					switch (this.typeCode)
					{
					case TypeCode.Single:
						return NumberValue.New((double)((float)value2.AsNumber.ToDouble()));
					case TypeCode.Double:
						return NumberValue.New(value2.AsNumber.ToDouble());
					case TypeCode.Decimal:
						return NumberValue.New(value2.AsNumber.ToDecimal());
					default:
						return value2;
					}
				}

				// Token: 0x060083FF RID: 33791 RVA: 0x001C15C5 File Offset: 0x001BF7C5
				public override bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					return Library.NumberAliasTypes.TryReduceNumberFrom(invocation, out expression) || base.TryRewriteInvocation(invocation, environment, out expression);
				}

				// Token: 0x04004961 RID: 18785
				private readonly FunctionValue fromNumber;

				// Token: 0x04004962 RID: 18786
				private readonly TypeCode typeCode;
			}

			// Token: 0x020013E8 RID: 5096
			private class TypeCultureCodeIdentity : IFunctionIdentity, IEquatable<IFunctionIdentity>
			{
				// Token: 0x06008400 RID: 33792 RVA: 0x001C15DB File Offset: 0x001BF7DB
				public TypeCultureCodeIdentity(global::System.Type type, ICulture culture, TypeCode typeCode)
				{
					this.type = type;
					this.culture = ((culture != null) ? culture.Name : null);
					this.typeCode = typeCode;
				}

				// Token: 0x06008401 RID: 33793 RVA: 0x001C1603 File Offset: 0x001BF803
				public override bool Equals(object obj)
				{
					return this.Equals(obj as Library.NumberAliasTypes.TypeCultureCodeIdentity);
				}

				// Token: 0x06008402 RID: 33794 RVA: 0x001C1603 File Offset: 0x001BF803
				public bool Equals(IFunctionIdentity identity)
				{
					return this.Equals(identity as Library.NumberAliasTypes.TypeCultureCodeIdentity);
				}

				// Token: 0x06008403 RID: 33795 RVA: 0x001C1611 File Offset: 0x001BF811
				public bool Equals(Library.NumberAliasTypes.TypeCultureCodeIdentity identity)
				{
					return identity != null && identity.type == this.type && identity.typeCode == this.typeCode && identity.culture == this.culture;
				}

				// Token: 0x06008404 RID: 33796 RVA: 0x001C164A File Offset: 0x001BF84A
				public override int GetHashCode()
				{
					int num = (int)(this.type.GetHashCode() + this.typeCode * TypeCode.Int16);
					string text = this.culture;
					return num + ((text != null) ? text.GetHashCode() : 0) * 17;
				}

				// Token: 0x04004963 RID: 18787
				private readonly global::System.Type type;

				// Token: 0x04004964 RID: 18788
				private readonly string culture;

				// Token: 0x04004965 RID: 18789
				private readonly TypeCode typeCode;
			}
		}

		// Token: 0x020013E9 RID: 5097
		public static class BinaryEncoding
		{
			// Token: 0x04004966 RID: 18790
			public static readonly IntEnumTypeValue<NumberValue> Type = new IntEnumTypeValue<NumberValue>("BinaryEncoding.Type");

			// Token: 0x04004967 RID: 18791
			public static readonly NumberValue Base64 = Library.BinaryEncoding.Type.NewEnumValue("BinaryEncoding.Base64", 0, NumberValue.Zero, null);

			// Token: 0x04004968 RID: 18792
			public static readonly NumberValue Hex = Library.BinaryEncoding.Type.NewEnumValue("BinaryEncoding.Hex", 1, NumberValue.One, null);
		}

		// Token: 0x020013EA RID: 5098
		public static class Binary
		{
			// Token: 0x06008406 RID: 33798 RVA: 0x001C16CC File Offset: 0x001BF8CC
			private static BinaryValue ConvertFromText(TextValue text, Value encodingValue)
			{
				byte[] array;
				if (encodingValue.IsNull || encodingValue.Equals(Library.BinaryEncoding.Base64))
				{
					if (!Base64Encoding.TryFromBase64String(text.String, out array))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Binary_InvalidEncoding, text, null);
					}
				}
				else
				{
					if (!encodingValue.Equals(Library.BinaryEncoding.Hex))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Binary_InvalidEncoding, encodingValue, null);
					}
					array = Bytes.FromHexString(text.String);
				}
				return BinaryValue.New(array);
			}

			// Token: 0x04004969 RID: 18793
			public static readonly FunctionValue ApproximateLength = new Library.Binary.ApproximateLengthFunctionValue();

			// Token: 0x0400496A RID: 18794
			public static readonly FunctionValue From = new Library.Binary.FromFunctionValue();

			// Token: 0x0400496B RID: 18795
			public static readonly FunctionValue ToText = new Library.Binary.ToTextFunctionValue();

			// Token: 0x0400496C RID: 18796
			public static readonly FunctionValue FromText = new Library.Binary.FromTextFunctionValue();

			// Token: 0x0400496D RID: 18797
			public static readonly FunctionValue ToList = new Library.Binary.ToListFunctionValue();

			// Token: 0x0400496E RID: 18798
			public static readonly FunctionValue FromList = new Library.Binary.FromListFunctionValue();

			// Token: 0x0400496F RID: 18799
			public static readonly FunctionValue Combine = new Library.Binary.CombineFunctionValue();

			// Token: 0x04004970 RID: 18800
			public static readonly FunctionValue Length = new Library.Binary.LengthFunctionValue();

			// Token: 0x04004971 RID: 18801
			public static readonly FunctionValue Literal = new Library.Binary.LiteralFunctionValue();

			// Token: 0x04004972 RID: 18802
			public static readonly FunctionValue Buffer = new Library.Binary.BufferFunctionValue();

			// Token: 0x04004973 RID: 18803
			public static readonly FunctionValue Compress = new Library.Binary.CompressFunctionValue();

			// Token: 0x04004974 RID: 18804
			public static readonly FunctionValue Decompress = new Library.Binary.DecompressFunctionValue();

			// Token: 0x04004975 RID: 18805
			public static readonly FunctionValue End = new Library.Binary.EndFunctionValue();

			// Token: 0x04004976 RID: 18806
			public static readonly FunctionValue InferContentType = new Library.Binary.InferContentTypeFunctionValue();

			// Token: 0x04004977 RID: 18807
			public static readonly FunctionValue Range = new Library.Binary.RangeFunctionValue();

			// Token: 0x04004978 RID: 18808
			public static readonly FunctionValue Split = new Library.Binary.SplitFunctionValue();

			// Token: 0x020013EB RID: 5099
			private class ToTextFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008408 RID: 33800 RVA: 0x001C17E5 File Offset: 0x001BF9E5
				public ToTextFunctionValue()
					: base(NullableTypeValue.Text, 1, "binary", NullableTypeValue.Binary, "encoding", Library.BinaryEncoding.Type.Nullable)
				{
				}

				// Token: 0x06008409 RID: 33801 RVA: 0x001C180C File Offset: 0x001BFA0C
				public override Value TypedInvoke(Value value, Value encodingValue)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					string text;
					if (encodingValue.IsNull || encodingValue.Equals(Library.BinaryEncoding.Base64))
					{
						text = Convert.ToBase64String(value.AsBinary.AsBytes, Base64FormattingOptions.None);
					}
					else
					{
						if (!encodingValue.Equals(Library.BinaryEncoding.Hex))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Binary_InvalidEncoding, encodingValue, null);
						}
						text = Bytes.ToHexString(value.AsBinary.AsBytes);
					}
					return TextValue.New(text);
				}
			}

			// Token: 0x020013EC RID: 5100
			private class FromTextFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600840A RID: 33802 RVA: 0x001C1883 File Offset: 0x001BFA83
				public FromTextFunctionValue()
					: base(NullableTypeValue.Binary, 1, "text", NullableTypeValue.Text, "encoding", Library.BinaryEncoding.Type.Nullable)
				{
				}

				// Token: 0x0600840B RID: 33803 RVA: 0x001C18AA File Offset: 0x001BFAAA
				public override Value TypedInvoke(Value text, Value encodingValue)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					return Library.Binary.ConvertFromText(text.AsText, encodingValue);
				}
			}

			// Token: 0x020013ED RID: 5101
			private class ApproximateLengthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600840C RID: 33804 RVA: 0x001C18C6 File Offset: 0x001BFAC6
				public ApproximateLengthFunctionValue()
					: base(NullableTypeValue.Number, "binary", NullableTypeValue.Binary)
				{
				}

				// Token: 0x0600840D RID: 33805 RVA: 0x001C18E0 File Offset: 0x001BFAE0
				public override Value TypedInvoke(Value value)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					long num;
					if (value.AsBinary.TryGetLength(out num))
					{
						return NumberValue.New(num);
					}
					throw ValueException.NewDataSourceError<Message0>(Strings.ValueException_ApproximateLength_Unsupported, TextValue.Empty, null);
				}
			}

			// Token: 0x020013EE RID: 5102
			private class FromFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600840E RID: 33806 RVA: 0x001C1921 File Offset: 0x001BFB21
				public FromFunctionValue()
					: base(NullableTypeValue.Binary, 1, "value", TypeValue.Any, "encoding", Library.BinaryEncoding.Type.Nullable)
				{
				}

				// Token: 0x0600840F RID: 33807 RVA: 0x001C1948 File Offset: 0x001BFB48
				public override Value TypedInvoke(Value value, Value encoding)
				{
					ValueKind kind = value.Kind;
					if (kind == ValueKind.Null)
					{
						return value;
					}
					if (kind == ValueKind.Text)
					{
						return Library.Binary.ConvertFromText(value.AsText, encoding);
					}
					if (kind == ValueKind.Binary)
					{
						return value;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Binary_NotConvertibleToBinary, value, null);
				}
			}

			// Token: 0x020013EF RID: 5103
			private class ToListFunctionValue : NativeFunctionValue1<ListValue, BinaryValue>
			{
				// Token: 0x06008410 RID: 33808 RVA: 0x001C1985 File Offset: 0x001BFB85
				public ToListFunctionValue()
					: base(TypeValue.List, 1, "binary", TypeValue.Binary)
				{
				}

				// Token: 0x06008411 RID: 33809 RVA: 0x001C199D File Offset: 0x001BFB9D
				public override ListValue TypedInvoke(BinaryValue binary)
				{
					return binary.ToList();
				}
			}

			// Token: 0x020013F0 RID: 5104
			private class FromListFunctionValue : NativeFunctionValue1<BinaryValue, ListValue>
			{
				// Token: 0x06008412 RID: 33810 RVA: 0x001C19A5 File Offset: 0x001BFBA5
				public FromListFunctionValue()
					: base(TypeValue.Binary, 1, "list", TypeValue.List)
				{
				}

				// Token: 0x06008413 RID: 33811 RVA: 0x001C19BD File Offset: 0x001BFBBD
				public override BinaryValue TypedInvoke(ListValue list)
				{
					return list.ToBinary();
				}
			}

			// Token: 0x020013F1 RID: 5105
			private class LiteralFunctionValue : NativeFunctionValue1
			{
				// Token: 0x06008414 RID: 33812 RVA: 0x0018D833 File Offset: 0x0018BA33
				public LiteralFunctionValue()
					: base("value")
				{
				}

				// Token: 0x06008415 RID: 33813 RVA: 0x001C19C5 File Offset: 0x001BFBC5
				public override Value Invoke(Value value)
				{
					if (value.IsList)
					{
						return value.AsList.ToBinary();
					}
					if (value.IsText)
					{
						return Library.Binary.ConvertFromText(value.AsText, Value.Null);
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Binary_NotConvertibleToBinary, value, null);
				}

				// Token: 0x06008416 RID: 33814 RVA: 0x001C1A00 File Offset: 0x001BFC00
				public override string ToSource()
				{
					return "#binary";
				}
			}

			// Token: 0x020013F2 RID: 5106
			private class CombineFunctionValue : NativeFunctionValue1<BinaryValue, ListValue>
			{
				// Token: 0x06008417 RID: 33815 RVA: 0x001C1A07 File Offset: 0x001BFC07
				public CombineFunctionValue()
					: base(TypeValue.Binary, 1, "binaries", TypeValue.List)
				{
				}

				// Token: 0x06008418 RID: 33816 RVA: 0x001C1A1F File Offset: 0x001BFC1F
				public override BinaryValue TypedInvoke(ListValue binaries)
				{
					return BinaryValue.Combine(binaries).NewMeta(this.CreateMetadata(binaries)).AsBinary;
				}

				// Token: 0x06008419 RID: 33817 RVA: 0x001C1A38 File Offset: 0x001BFC38
				private RecordValue CreateMetadata(ListValue binaries)
				{
					RecordValue recordValue = RecordValue.Empty;
					IValueReference valueReference = binaries.FirstOrDefault<IValueReference>();
					if (valueReference != null)
					{
						string text = "Content.Type";
						int num = 0;
						if (valueReference.Value.MetaValue.Keys.TryGetKeyIndex(text, out num))
						{
							recordValue = DataSource.CreateDataSourceRecordValue(valueReference.Value.MetaValue[num]);
						}
					}
					return recordValue;
				}
			}

			// Token: 0x020013F3 RID: 5107
			private class LengthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600841A RID: 33818 RVA: 0x001C1A8F File Offset: 0x001BFC8F
				public LengthFunctionValue()
					: base(NullableTypeValue.Int32, "binary", NullableTypeValue.Binary)
				{
				}

				// Token: 0x0600841B RID: 33819 RVA: 0x001C1AA6 File Offset: 0x001BFCA6
				public override Value TypedInvoke(Value value)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(value.AsBinary.Length);
				}
			}

			// Token: 0x020013F4 RID: 5108
			private class BufferFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600841C RID: 33820 RVA: 0x001C1AC6 File Offset: 0x001BFCC6
				public BufferFunctionValue()
					: base(NullableTypeValue.Binary, "binary", NullableTypeValue.Binary)
				{
				}

				// Token: 0x0600841D RID: 33821 RVA: 0x001C1ADD File Offset: 0x001BFCDD
				public override Value TypedInvoke(Value value)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					return BinaryValue.New(value.AsBinary.AsBytes);
				}
			}

			// Token: 0x020013F5 RID: 5109
			private class CompressFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x0600841E RID: 33822 RVA: 0x001C1AFD File Offset: 0x001BFCFD
				public CompressFunctionValue()
					: base(NullableTypeValue.Binary, "binary", NullableTypeValue.Binary, "compressionType", Library.CompressionType.Type)
				{
				}

				// Token: 0x0600841F RID: 33823 RVA: 0x001C1B1E File Offset: 0x001BFD1E
				public override Value TypedInvoke(Value value, NumberValue compressionType)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					return Compression.Compress(value.AsBinary, compressionType);
				}
			}

			// Token: 0x020013F6 RID: 5110
			private class DecompressFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x06008420 RID: 33824 RVA: 0x001C1AFD File Offset: 0x001BFCFD
				public DecompressFunctionValue()
					: base(NullableTypeValue.Binary, "binary", NullableTypeValue.Binary, "compressionType", Library.CompressionType.Type)
				{
				}

				// Token: 0x06008421 RID: 33825 RVA: 0x001C1B3A File Offset: 0x001BFD3A
				public override Value TypedInvoke(Value value, NumberValue compressionType)
				{
					if (value.IsNull)
					{
						return Value.Null;
					}
					return Compression.Decompress(value.AsBinary, compressionType);
				}
			}

			// Token: 0x020013F7 RID: 5111
			private class EndFunctionValue : NativeFunctionValue1<BinaryValue, BinaryValue>
			{
				// Token: 0x06008422 RID: 33826 RVA: 0x001C1B56 File Offset: 0x001BFD56
				public EndFunctionValue()
					: base(TypeValue.Binary, "binary", TypeValue.Binary)
				{
				}

				// Token: 0x06008423 RID: 33827 RVA: 0x001C1B6D File Offset: 0x001BFD6D
				public override BinaryValue TypedInvoke(BinaryValue binary)
				{
					return binary.End;
				}
			}

			// Token: 0x020013F8 RID: 5112
			private class InferContentTypeFunctionValue : NativeFunctionValue1<RecordValue, BinaryValue>
			{
				// Token: 0x06008424 RID: 33828 RVA: 0x001C1B75 File Offset: 0x001BFD75
				public InferContentTypeFunctionValue()
					: base(TypeValue.Record, 1, "source", TypeValue.Binary)
				{
				}

				// Token: 0x06008425 RID: 33829 RVA: 0x001C1B8D File Offset: 0x001BFD8D
				public override RecordValue TypedInvoke(BinaryValue binary)
				{
					return PreviewInference.InferFromBinary(binary, null, null);
				}
			}

			// Token: 0x020013F9 RID: 5113
			private class RangeFunctionValue : NativeFunctionValue3<BinaryValue, BinaryValue, NumberValue, Value>
			{
				// Token: 0x06008426 RID: 33830 RVA: 0x001C1B98 File Offset: 0x001BFD98
				public RangeFunctionValue()
					: base(TypeValue.Binary, 2, "binary", TypeValue.Binary, "offset", TypeValue.Int32, "count", NullableTypeValue.Int32)
				{
				}

				// Token: 0x06008427 RID: 33831 RVA: 0x001C1BD0 File Offset: 0x001BFDD0
				public override BinaryValue TypedInvoke(BinaryValue binary, NumberValue offset, Value count)
				{
					if (offset.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("offset", offset);
					}
					if (count.IsNumber && count.AsNumber.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					RowCount rowCount = (count.IsNull ? RowCount.Infinite : new RowCount(count.AsNumber.AsInteger64));
					return binary.Range(new RowCount(offset.AsNumber.AsInteger64), rowCount);
				}
			}

			// Token: 0x020013FA RID: 5114
			private sealed class SplitFunctionValue : NativeFunctionValue2<ListValue, BinaryValue, NumberValue>
			{
				// Token: 0x06008428 RID: 33832 RVA: 0x001C1C53 File Offset: 0x001BFE53
				public SplitFunctionValue()
					: base(TypeValue.List, "binary", TypeValue.Binary, "pageSize", TypeValue.Int64)
				{
				}

				// Token: 0x06008429 RID: 33833 RVA: 0x001C1C74 File Offset: 0x001BFE74
				public override ListValue TypedInvoke(BinaryValue binary, NumberValue pageSize)
				{
					long asInteger = pageSize.AsInteger64;
					if (asInteger < 1L)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ArgumentOutOfRange("pageSize"), pageSize, null);
					}
					return binary.Split(new RowCount(asInteger));
				}
			}

			// Token: 0x020013FB RID: 5115
			public class FromHandlersFunctionValue : NativeFunctionValue1<BinaryValue, RecordValue>
			{
				// Token: 0x0600842A RID: 33834 RVA: 0x001C1CAB File Offset: 0x001BFEAB
				public FromHandlersFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Binary, "handlers", TypeValue.Record)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x0600842B RID: 33835 RVA: 0x001C1CC9 File Offset: 0x001BFEC9
				public override BinaryValue TypedInvoke(RecordValue handlers)
				{
					return new Library.Binary.FromHandlersFunctionValue.HandlersBinaryValue(this.engineHost, handlers);
				}

				// Token: 0x04004979 RID: 18809
				private readonly IEngineHost engineHost;

				// Token: 0x020013FC RID: 5116
				private class HandlersBinaryValue : StreamedBinaryValue
				{
					// Token: 0x0600842C RID: 33836 RVA: 0x001C1CD7 File Offset: 0x001BFED7
					public HandlersBinaryValue(IEngineHost engineHost, RecordValue handlers)
					{
						this.engineHost = engineHost;
						this.handlers = handlers;
					}

					// Token: 0x17002362 RID: 9058
					// (get) Token: 0x0600842D RID: 33837 RVA: 0x001C1CF0 File Offset: 0x001BFEF0
					public override IExpression Expression
					{
						get
						{
							try
							{
								FunctionValue functionValue;
								if (this.TryGetHandler("GetExpression", out functionValue))
								{
									Value value = functionValue.Invoke();
									if (value.IsNull)
									{
										return null;
									}
									return MAstToExpressionVisitor.ToExpression(value.AsRecord);
								}
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
							}
							return null;
						}
					}

					// Token: 0x0600842E RID: 33838 RVA: 0x001C1D60 File Offset: 0x001BFF60
					public override Stream Open()
					{
						return this.GetStream().Open();
					}

					// Token: 0x0600842F RID: 33839 RVA: 0x001C1D6D File Offset: 0x001BFF6D
					public override Stream Open(bool preferCanSeek)
					{
						return this.GetStream().Open(preferCanSeek);
					}

					// Token: 0x06008430 RID: 33840 RVA: 0x001C1D7B File Offset: 0x001BFF7B
					public override Stream OpenForWrite()
					{
						return this.GetStream().OpenForWrite();
					}

					// Token: 0x06008431 RID: 33841 RVA: 0x001C1D88 File Offset: 0x001BFF88
					public override bool TryGetLength(out long length)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("GetLength", out functionValue))
						{
							try
							{
								length = functionValue.Invoke().AsNumber.AsInteger64;
								return true;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
							}
						}
						return base.TryGetLength(out length);
					}

					// Token: 0x17002363 RID: 9059
					// (get) Token: 0x06008432 RID: 33842 RVA: 0x001C1DF0 File Offset: 0x001BFFF0
					public override long Length
					{
						get
						{
							long num;
							if (this.TryGetLength(out num))
							{
								return num;
							}
							return base.Length;
						}
					}

					// Token: 0x06008433 RID: 33843 RVA: 0x001C1E10 File Offset: 0x001C0010
					public override BinaryValue Range(RowCount offset, RowCount count)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnRange", out functionValue))
						{
							try
							{
								if (!offset.IsInfinite)
								{
									Value value = (count.IsInfinite ? Value.Null : NumberValue.New(count.Value));
									return functionValue.Invoke(NumberValue.New(offset.Value), value).AsBinary;
								}
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
							}
						}
						this.ReportFoldingFailure();
						return base.Range(offset, count);
					}

					// Token: 0x17002364 RID: 9060
					// (get) Token: 0x06008434 RID: 33844 RVA: 0x001C1EAC File Offset: 0x001C00AC
					public override BinaryValue End
					{
						get
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnEnd", out functionValue))
							{
								try
								{
									return functionValue.Invoke().AsBinary;
								}
								catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
								{
								}
							}
							this.ReportFoldingFailure();
							return base.End;
						}
					}

					// Token: 0x06008435 RID: 33845 RVA: 0x001C1F10 File Offset: 0x001C0110
					public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
					{
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnInvoke", out functionValue))
							{
								result = functionValue.Invoke(function, ListValue.New(arguments), NumberValue.New(index));
								return true;
							}
						}
						catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
						{
						}
						this.ReportFoldingFailure();
						result = null;
						return false;
					}

					// Token: 0x06008436 RID: 33846 RVA: 0x001C1F84 File Offset: 0x001C0184
					public override ActionValue Replace(Value value)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnReplace", out functionValue))
						{
							try
							{
								return functionValue.Invoke(value.AsBinary).AsAction;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
							}
						}
						return base.Replace(value);
					}

					// Token: 0x06008437 RID: 33847 RVA: 0x001C1FEC File Offset: 0x001C01EC
					private bool TryGetHandler(string key, out FunctionValue handler)
					{
						Value value;
						if (this.handlers.TryGetValue(key, out value))
						{
							handler = value.AsFunction;
							return true;
						}
						handler = null;
						return false;
					}

					// Token: 0x06008438 RID: 33848 RVA: 0x001C2017 File Offset: 0x001C0217
					private FunctionValue GetHandler(string key)
					{
						return this.handlers[key].AsFunction;
					}

					// Token: 0x06008439 RID: 33849 RVA: 0x001C202C File Offset: 0x001C022C
					private BinaryValue GetStream()
					{
						if (this.stream == null)
						{
							FunctionValue handler = this.GetHandler("GetStream");
							this.stream = handler.Invoke().AsBinary;
						}
						return this.stream;
					}

					// Token: 0x0600843A RID: 33850 RVA: 0x001C2064 File Offset: 0x001C0264
					private void ReportFoldingFailure()
					{
						using (TracingService.CreatePerformanceTrace(this.engineHost, "HandlersBinaryValue/ReportFoldingFailure", TraceEventType.Information, null))
						{
							if (this.engineHost.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure)
							{
								throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
							}
						}
					}

					// Token: 0x0400497A RID: 18810
					private const string GetExpressionKey = "GetExpression";

					// Token: 0x0400497B RID: 18811
					private const string GetLengthKey = "GetLength";

					// Token: 0x0400497C RID: 18812
					private const string GetStreamKey = "GetStream";

					// Token: 0x0400497D RID: 18813
					private const string OnRangeKey = "OnRange";

					// Token: 0x0400497E RID: 18814
					private const string OnEndKey = "OnEnd";

					// Token: 0x0400497F RID: 18815
					private const string OnInvokeKey = "OnInvoke";

					// Token: 0x04004980 RID: 18816
					private const string OnReplaceKey = "OnReplace";

					// Token: 0x04004981 RID: 18817
					private readonly IEngineHost engineHost;

					// Token: 0x04004982 RID: 18818
					private readonly RecordValue handlers;

					// Token: 0x04004983 RID: 18819
					private BinaryValue stream;
				}
			}
		}

		// Token: 0x020013FD RID: 5117
		public static class Character
		{
			// Token: 0x04004984 RID: 18820
			public static readonly FunctionValue FromNumber = new Library.Character.FromNumberFunctionValue();

			// Token: 0x04004985 RID: 18821
			public static readonly FunctionValue ToNumber = new Library.Character.ToNumberFunctionValue();

			// Token: 0x020013FE RID: 5118
			private class FromNumberFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600843C RID: 33852 RVA: 0x001C20D6 File Offset: 0x001C02D6
				public FromNumberFunctionValue()
					: base(NullableTypeValue.Character, "number", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600843D RID: 33853 RVA: 0x001C20F0 File Offset: 0x001C02F0
				public override Value TypedInvoke(Value number)
				{
					if (number.IsNull)
					{
						return Value.Null;
					}
					Value value;
					try
					{
						value = TextValue.New(char.ConvertFromUtf32(number.AsNumber.ToInt32()));
					}
					catch (ArgumentOutOfRangeException)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Character_FromFunction_NotConvertibleToCharacter, number, null);
					}
					return value;
				}
			}

			// Token: 0x020013FF RID: 5119
			private class ToNumberFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600843E RID: 33854 RVA: 0x001C2144 File Offset: 0x001C0344
				public ToNumberFunctionValue()
					: base(NullableTypeValue.Number, "character", NullableTypeValue.Character)
				{
				}

				// Token: 0x0600843F RID: 33855 RVA: 0x001C215C File Offset: 0x001C035C
				public override Value TypedInvoke(Value character)
				{
					if (character.IsNull)
					{
						return Value.Null;
					}
					int length = character.AsString.Length;
					if (length == 0 || length > 2)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.Number_FromFunction_NotConvertibleToNumber, character, null);
					}
					Value value;
					try
					{
						value = NumberValue.New(char.ConvertToUtf32(character.AsString, 0));
					}
					catch (ArgumentException)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.Number_FromFunction_NotConvertibleToNumber, character, null);
					}
					return value;
				}
			}
		}

		// Token: 0x02001400 RID: 5120
		public static class Text
		{
			// Token: 0x04004986 RID: 18822
			public static readonly FunctionValue At = new Library.Text.AtFunctionValue();

			// Token: 0x04004987 RID: 18823
			public static readonly FunctionValue Length = new Library.Text.LengthFunctionValue();

			// Token: 0x04004988 RID: 18824
			public static readonly FunctionValue Range = new Library.Text.SubstringFunctionValue();

			// Token: 0x04004989 RID: 18825
			public static readonly FunctionValue Middle = new Library.Text.MiddleFunctionValue();

			// Token: 0x0400498A RID: 18826
			public static readonly FunctionValue Start = new Library.Text.StartFunctionValue();

			// Token: 0x0400498B RID: 18827
			public static readonly FunctionValue End = new Library.Text.EndFunctionValue();

			// Token: 0x0400498C RID: 18828
			public static readonly FunctionValue StartsWith = new Library.Text.StartsWithFunctionValue();

			// Token: 0x0400498D RID: 18829
			public static readonly FunctionValue EndsWith = new Library.Text.EndsWithFunctionValue();

			// Token: 0x0400498E RID: 18830
			public static readonly FunctionValue Contains = new Library.Text.ContainsFunctionValue();

			// Token: 0x0400498F RID: 18831
			public static readonly FunctionValue Clean = new Library.Text.CleanFunctionValue();

			// Token: 0x04004990 RID: 18832
			public static readonly FunctionValue PositionOf = new Library.Text.PositionOfFunctionValue();

			// Token: 0x04004991 RID: 18833
			public static readonly FunctionValue PositionOfAny = new Library.Text.PositionOfAnyFunctionValue();

			// Token: 0x04004992 RID: 18834
			public static readonly FunctionValue Split = new Library.Text.SplitFunctionValue();

			// Token: 0x04004993 RID: 18835
			public static readonly FunctionValue SplitAny = new Library.Text.SplitAnyFunctionValue();

			// Token: 0x04004994 RID: 18836
			public static readonly FunctionValue SplitBySequence = new Library.Text.SplitBySequenceFunctionValue();

			// Token: 0x04004995 RID: 18837
			public static readonly FunctionValue Combine = new Library.Text.CombineFunctionValue();

			// Token: 0x04004996 RID: 18838
			public static readonly FunctionValue Repeat = new Library.Text.RepeatFunctionValue();

			// Token: 0x04004997 RID: 18839
			public static readonly FunctionValue ReplaceRange = new Library.Text.ReplaceRangeFunctionValue();

			// Token: 0x04004998 RID: 18840
			public static readonly FunctionValue Replace = new Library.Text.ReplaceFunctionValue();

			// Token: 0x04004999 RID: 18841
			public static readonly FunctionValue Insert = new Library.Text.InsertFunctionValue();

			// Token: 0x0400499A RID: 18842
			public static readonly FunctionValue Remove = new Library.Text.RemoveFunctionValue();

			// Token: 0x0400499B RID: 18843
			public static readonly FunctionValue RemoveRange = new Library.Text.RemoveRangeFunctionValue();

			// Token: 0x0400499C RID: 18844
			public static readonly FunctionValue Trim = new Library.Text.TrimFunctionValue();

			// Token: 0x0400499D RID: 18845
			public static readonly FunctionValue TrimStart = new Library.Text.TrimStartFunctionValue();

			// Token: 0x0400499E RID: 18846
			public static readonly FunctionValue TrimEnd = new Library.Text.TrimEndFunctionValue();

			// Token: 0x0400499F RID: 18847
			public static readonly FunctionValue PadStart = new Library.Text.PadStartFunctionValue();

			// Token: 0x040049A0 RID: 18848
			public static readonly FunctionValue PadEnd = new Library.Text.PadEndFunctionValue();

			// Token: 0x040049A1 RID: 18849
			public static readonly FunctionValue ToBinary = new Library.Text.ToBinaryFunctionValue();

			// Token: 0x040049A2 RID: 18850
			public static readonly FunctionValue ToList = new Library.Text.ToListFunctionValue();

			// Token: 0x040049A3 RID: 18851
			public static readonly FunctionValue FromBinary = new Library.Text.FromBinaryFunctionValue();

			// Token: 0x040049A4 RID: 18852
			public static readonly FunctionValue Select = new Library.Text.SelectFunctionValue();

			// Token: 0x040049A5 RID: 18853
			public static readonly FunctionValue Reverse = new Library.Text.ReverseFunctionValue();

			// Token: 0x02001401 RID: 5121
			private class AtFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x06008441 RID: 33857 RVA: 0x001C2319 File Offset: 0x001C0519
				public AtFunctionValue()
					: base(NullableTypeValue.Character, "text", NullableTypeValue.Text, "index", TypeValue.Number)
				{
				}

				// Token: 0x06008442 RID: 33858 RVA: 0x001C233C File Offset: 0x001C053C
				public override Value TypedInvoke(Value text, NumberValue index)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					TextValue asText = text.AsText;
					int asInteger = index.AsInteger32;
					if (asInteger < 0 || asInteger >= asText.Length)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Text_At_IndexOutOfRange, index, null);
					}
					return asText[asInteger];
				}
			}

			// Token: 0x02001402 RID: 5122
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008443 RID: 33859 RVA: 0x001C2388 File Offset: 0x001C0588
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, TypeValue.Text.Nullable, 1, "value", TypeValue.Any, "culture", TypeValue.Text.Nullable)
				{
				}

				// Token: 0x17002365 RID: 9061
				// (get) Token: 0x06008444 RID: 33860 RVA: 0x001C23C1 File Offset: 0x001C05C1
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.TextFrom;
					}
				}

				// Token: 0x06008445 RID: 33861 RVA: 0x001C23C8 File Offset: 0x001C05C8
				public override Value TypedInvoke(Value value, Value culture)
				{
					ICulture culture2 = base.GetCulture(culture);
					culture2.GetCultureInfo();
					Value value2;
					if (!this.TryTextFromValue(value, culture2, out value2))
					{
						throw ValueException.CastTypeMismatch(value, TypeValue.Text);
					}
					return value2;
				}

				// Token: 0x06008446 RID: 33862 RVA: 0x001C2400 File Offset: 0x001C0600
				public bool TryTextFromValue(Value value, ICulture culture, out Value textValue)
				{
					textValue = null;
					switch (value.Kind)
					{
					case ValueKind.Null:
						textValue = value;
						break;
					case ValueKind.Time:
						textValue = Library.Time.ConvertToText(value.AsTime, Value.Null, culture).AsText;
						break;
					case ValueKind.Date:
						textValue = Library.Date.ConvertToText(value.AsDate, Value.Null, culture).AsText;
						break;
					case ValueKind.DateTime:
						textValue = Library.DateTime.ConvertToText(value.AsDateTime, Value.Null, culture).AsText;
						break;
					case ValueKind.DateTimeZone:
						textValue = Library.DateTimeZone.ConvertToText(value.AsDateTimeZone, Value.Null, culture).AsText;
						break;
					case ValueKind.Duration:
						textValue = Library.Duration.ToText.Invoke(value).AsText;
						break;
					case ValueKind.Number:
						textValue = Library.Number.ConvertToText(value.AsNumber, Value.Null, culture).AsText;
						break;
					case ValueKind.Logical:
						textValue = Library.Logical.ToText.Invoke(value).AsText;
						break;
					case ValueKind.Text:
						textValue = value.SubtractMetaValue.AsText;
						break;
					case ValueKind.Binary:
						textValue = Library.Binary.ToText.Invoke(value).AsText;
						break;
					default:
						return false;
					}
					return true;
				}
			}

			// Token: 0x02001403 RID: 5123
			public class InferNumberTypeFunctionValue : CultureSpecificFunctionValue2<TypeValue, TextValue, Value>
			{
				// Token: 0x06008447 RID: 33863 RVA: 0x001C2528 File Offset: 0x001C0728
				public InferNumberTypeFunctionValue(IEngineHost host)
					: base(host, null, TypeValue._Type, 1, "text", TypeValue.Text, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008448 RID: 33864 RVA: 0x001C2557 File Offset: 0x001C0757
				public override TypeValue TypedInvoke(TextValue text, Value culture)
				{
					return Library.Text.InferNumberTypeFunctionValue.GetType(text, base.GetCulture(culture).GetCultureInfo());
				}

				// Token: 0x06008449 RID: 33865 RVA: 0x001C256C File Offset: 0x001C076C
				private static TypeValue GetType(TextValue text, CultureInfo cultureInfo)
				{
					string text2 = text.AsString.Trim(Library.whitespaceChars);
					NumberValue numberValue;
					TypeValue typeValue;
					if (!NumberValue.TryParse(text2, 0, text2.Length, NumberStyles.Any, cultureInfo, out numberValue, out typeValue))
					{
						throw ValueException.NewDataFormatError<Message1>(Strings.UnknownNumberPattern(text), text, null);
					}
					long num;
					if (typeValue != TypeValue.Percentage && typeValue != TypeValue.Currency && numberValue.TryGetInt64(out num))
					{
						if (-128L <= num && num <= 127L)
						{
							typeValue = TypeValue.Int8;
						}
						else if (-32768L <= num && num <= 32767L)
						{
							typeValue = TypeValue.Int16;
						}
						else if (-2147483648L <= num && num <= 2147483647L)
						{
							typeValue = TypeValue.Int32;
						}
						else
						{
							typeValue = TypeValue.Int64;
						}
					}
					return typeValue;
				}
			}

			// Token: 0x02001404 RID: 5124
			public class FormatFunctionValue : NativeFunctionValue3<TextValue, TextValue, Value, Value>
			{
				// Token: 0x0600844A RID: 33866 RVA: 0x001C261C File Offset: 0x001C081C
				public FormatFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Text, 2, "formatString", TypeValue.Text, "arguments", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
					this.textFrom = new Library.Text.FromFunctionValue(engineHost, null);
				}

				// Token: 0x0600844B RID: 33867 RVA: 0x001C2660 File Offset: 0x001C0860
				public override TextValue TypedInvoke(TextValue formatString, Value arguments, Value culture)
				{
					if (!arguments.IsList && !arguments.IsRecord)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.TextFormat_InvalidArguments, arguments.Type, null);
					}
					StringBuilder stringBuilder = new StringBuilder(formatString.String);
					int num = 0;
					foreach (Library.Text.FormatFunctionValue.FormatArgument formatArgument in Library.Text.FormatFunctionValue.GetArguments(formatString.String))
					{
						string text = this.FormatToText(formatArgument.Index(arguments), culture);
						stringBuilder.Remove(formatArgument.start + num, formatArgument.length);
						stringBuilder.Insert(formatArgument.start + num, text);
						num += text.Length - formatArgument.length;
					}
					return TextValue.New(stringBuilder.ToString());
				}

				// Token: 0x0600844C RID: 33868 RVA: 0x001C2730 File Offset: 0x001C0930
				private string FormatToText(Value value, Value culture)
				{
					ValueKind kind = value.Kind;
					if (kind == ValueKind.Null)
					{
						return value.ToSource();
					}
					switch (kind)
					{
					case ValueKind.List:
						return "list";
					case ValueKind.Record:
						return "record";
					case ValueKind.Table:
						return "table";
					case ValueKind.Function:
						return "function";
					case ValueKind.Type:
						return "type";
					case ValueKind.Action:
						return "action";
					default:
						return this.textFrom.Invoke(value, culture).AsText.String;
					}
				}

				// Token: 0x0600844D RID: 33869 RVA: 0x001C27AC File Offset: 0x001C09AC
				private static IEnumerable<Library.Text.FormatFunctionValue.FormatArgument> GetArguments(string formatString)
				{
					int last = formatString.Length - 1;
					int num = 0;
					string text;
					for (;;)
					{
						num = formatString.IndexOf('#', num);
						if (num < 0 || num == last)
						{
							break;
						}
						char c = formatString[num + 1];
						char c2;
						if (c != '[')
						{
							if (c != '{')
							{
								num++;
								continue;
							}
							c2 = '}';
						}
						else
						{
							c2 = ']';
						}
						int lastPosition = formatString.IndexOf(c2, num);
						if (lastPosition < 0)
						{
							goto Block_4;
						}
						int num2 = lastPosition - num + 1;
						text = formatString.Substring(num + 2, num2 - 3);
						if (c2 == ']')
						{
							yield return new Library.Text.FormatFunctionValue.FormatArgument
							{
								start = num,
								length = num2,
								field = text
							};
						}
						else
						{
							int num3;
							if (!int.TryParse(text, out num3))
							{
								goto Block_6;
							}
							yield return new Library.Text.FormatFunctionValue.FormatArgument
							{
								start = num,
								length = num2,
								index = num3
							};
						}
						num = lastPosition + 1;
					}
					yield break;
					Block_4:
					throw ValueException.NewDataFormatError<Message0>(Strings.TextFormat_OpenWithoutClose, NumberValue.New(num), null);
					Block_6:
					throw ValueException.NewExpressionError<Message1>(Strings.TextFormat_InvalidReference(Strings.Number_FormatFunction_ExpectedIntegerValue), TextValue.New(text), null);
					yield break;
				}

				// Token: 0x040049A6 RID: 18854
				private readonly Library.Text.FromFunctionValue textFrom;

				// Token: 0x02001405 RID: 5125
				private struct FormatArgument
				{
					// Token: 0x0600844E RID: 33870 RVA: 0x001C27BC File Offset: 0x001C09BC
					public Value Index(Value value)
					{
						Value value2;
						try
						{
							value2 = ((this.field == null) ? value[this.index] : value[this.field]);
						}
						catch (ValueException ex)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.TextFormat_InvalidReference(ex.MessageString), null, null);
						}
						return value2;
					}

					// Token: 0x040049A7 RID: 18855
					public int start;

					// Token: 0x040049A8 RID: 18856
					public int length;

					// Token: 0x040049A9 RID: 18857
					public string field;

					// Token: 0x040049AA RID: 18858
					public int index;
				}
			}

			// Token: 0x02001407 RID: 5127
			private class LengthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008457 RID: 33879 RVA: 0x001C2A1B File Offset: 0x001C0C1B
				public LengthFunctionValue()
					: base(NullableTypeValue.Int32, "text", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008458 RID: 33880 RVA: 0x001C2A32 File Offset: 0x001C0C32
				public override Value TypedInvoke(Value text)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(text.AsText.Length);
				}
			}

			// Token: 0x02001408 RID: 5128
			private class SubstringFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x06008459 RID: 33881 RVA: 0x001C2A54 File Offset: 0x001C0C54
				public SubstringFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "offset", TypeValue.Number, "count", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600845A RID: 33882 RVA: 0x001C2A8C File Offset: 0x001C0C8C
				public override Value TypedInvoke(Value text, NumberValue offsetValue, Value countValue)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = offsetValue.AsInteger32;
					string asString = text.AsString;
					if (asInteger < 0 || asInteger > asString.Length)
					{
						throw ValueException.ArgumentOutOfRange("offset", offsetValue);
					}
					string text2;
					if (countValue.IsNull)
					{
						text2 = asString.Substring(asInteger);
					}
					else
					{
						int asInteger2 = countValue.AsInteger32;
						if (asInteger2 < 0 || asInteger2 > asString.Length - asInteger)
						{
							throw ValueException.ArgumentOutOfRange("count", countValue);
						}
						text2 = asString.Substring(asInteger, asInteger2);
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x02001409 RID: 5129
			private class MiddleFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x0600845B RID: 33883 RVA: 0x001C2B14 File Offset: 0x001C0D14
				public MiddleFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "start", TypeValue.Number, "count", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600845C RID: 33884 RVA: 0x001C2B4C File Offset: 0x001C0D4C
				public override Value TypedInvoke(Value text, NumberValue startValue, Value countValue)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					string asString = text.AsString;
					int asInteger = startValue.AsInteger32;
					int num = (countValue.IsNull ? asString.Length : countValue.AsInteger32);
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("start", startValue);
					}
					if (num < 0)
					{
						throw ValueException.ArgumentOutOfRange("count", countValue);
					}
					if (asInteger > asString.Length)
					{
						return TextValue.New(string.Empty);
					}
					string text2;
					if (asInteger + num > asString.Length)
					{
						text2 = asString.Substring(asInteger);
					}
					else
					{
						text2 = asString.Substring(asInteger, num);
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x0200140A RID: 5130
			private class StartFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x0600845D RID: 33885 RVA: 0x001C2BE4 File Offset: 0x001C0DE4
				public StartFunctionValue()
					: base(NullableTypeValue.Text, "text", NullableTypeValue.Text, "count", TypeValue.Number)
				{
				}

				// Token: 0x0600845E RID: 33886 RVA: 0x001C2C08 File Offset: 0x001C0E08
				public override Value TypedInvoke(Value text, NumberValue count)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = count.AsInteger32;
					string asString = text.AsString;
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					if (asInteger >= asString.Length)
					{
						return text;
					}
					return TextValue.New(asString.Substring(0, asInteger));
				}
			}

			// Token: 0x0200140B RID: 5131
			private class EndFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x0600845F RID: 33887 RVA: 0x001C2BE4 File Offset: 0x001C0DE4
				public EndFunctionValue()
					: base(NullableTypeValue.Text, "text", NullableTypeValue.Text, "count", TypeValue.Number)
				{
				}

				// Token: 0x06008460 RID: 33888 RVA: 0x001C2C5C File Offset: 0x001C0E5C
				public override Value TypedInvoke(Value text, NumberValue count)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = count.AsInteger32;
					string asString = text.AsString;
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					if (asInteger >= asString.Length)
					{
						return text;
					}
					return TextValue.New(asString.Substring(asString.Length - asInteger));
				}
			}

			// Token: 0x0200140C RID: 5132
			private class StartsWithFunctionValue : NativeFunctionValue3<Value, Value, TextValue, Value>
			{
				// Token: 0x06008461 RID: 33889 RVA: 0x001C2CB4 File Offset: 0x001C0EB4
				public StartsWithFunctionValue()
					: base(NullableTypeValue.Logical, 2, "text", NullableTypeValue.Text, "substring", TypeValue.Text, "comparer", NullableTypeValue.Function)
				{
				}

				// Token: 0x06008462 RID: 33890 RVA: 0x001C2CEC File Offset: 0x001C0EEC
				public override Value TypedInvoke(Value text, TextValue substring, Value comparer)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					if (comparer.IsNull)
					{
						return LogicalValue.New(text.AsString.StartsWith(substring.String, StringComparison.Ordinal));
					}
					CultureInfo cultureInfo;
					bool flag;
					if (!comparer.AsFunction.TryGetCultureCase(out cultureInfo, out flag))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.CultureUnawareComparer, comparer, null);
					}
					if (cultureInfo == null)
					{
						StringComparison stringComparison = (flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
						return LogicalValue.New(text.AsString.StartsWith(substring.String, stringComparison));
					}
					return LogicalValue.New(text.AsString.StartsWith(substring.String, flag, cultureInfo));
				}
			}

			// Token: 0x0200140D RID: 5133
			private class EndsWithFunctionValue : NativeFunctionValue3<Value, Value, TextValue, Value>
			{
				// Token: 0x06008463 RID: 33891 RVA: 0x001C2D80 File Offset: 0x001C0F80
				public EndsWithFunctionValue()
					: base(NullableTypeValue.Logical, 2, "text", NullableTypeValue.Text, "substring", TypeValue.Text, "comparer", NullableTypeValue.Function)
				{
				}

				// Token: 0x06008464 RID: 33892 RVA: 0x001C2DB8 File Offset: 0x001C0FB8
				public override Value TypedInvoke(Value text, TextValue substring, Value comparer)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					if (comparer.IsNull)
					{
						return LogicalValue.New(text.AsString.EndsWith(substring.String, StringComparison.Ordinal));
					}
					CultureInfo cultureInfo;
					bool flag;
					if (!comparer.AsFunction.TryGetCultureCase(out cultureInfo, out flag))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.CultureUnawareComparer, comparer, null);
					}
					if (cultureInfo == null)
					{
						StringComparison stringComparison = (flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
						return LogicalValue.New(text.AsString.EndsWith(substring.String, stringComparison));
					}
					return LogicalValue.New(text.AsString.EndsWith(substring.String, flag, cultureInfo));
				}
			}

			// Token: 0x0200140E RID: 5134
			private class ContainsFunctionValue : NativeFunctionValue3
			{
				// Token: 0x06008465 RID: 33893 RVA: 0x001C2E4C File Offset: 0x001C104C
				public ContainsFunctionValue()
					: base(2, "text", "substring", "comparer")
				{
				}

				// Token: 0x17002368 RID: 9064
				// (get) Token: 0x06008466 RID: 33894 RVA: 0x001BE59A File Offset: 0x001BC79A
				protected override TypeValue Type0
				{
					get
					{
						return TypeValue.Text.Nullable;
					}
				}

				// Token: 0x17002369 RID: 9065
				// (get) Token: 0x06008467 RID: 33895 RVA: 0x001C2E64 File Offset: 0x001C1064
				protected override TypeValue Type1
				{
					get
					{
						return TypeValue.Text;
					}
				}

				// Token: 0x1700236A RID: 9066
				// (get) Token: 0x06008468 RID: 33896 RVA: 0x001C2E6B File Offset: 0x001C106B
				protected override TypeValue Type2
				{
					get
					{
						return TypeValue.Function.Nullable;
					}
				}

				// Token: 0x1700236B RID: 9067
				// (get) Token: 0x06008469 RID: 33897 RVA: 0x001BE5A6 File Offset: 0x001BC7A6
				protected override TypeValue ReturnType
				{
					get
					{
						return TypeValue.Logical.Nullable;
					}
				}

				// Token: 0x0600846A RID: 33898 RVA: 0x001C2E78 File Offset: 0x001C1078
				public override Value Invoke(Value text, Value substring, Value comparer)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					if (comparer.IsNull)
					{
						return LogicalValue.New(text.AsString.Contains(substring.AsString));
					}
					return LogicalValue.New(Library.Text.PositionOf.Invoke(text, substring, Library.Occurrence.First, comparer).GreaterThanOrEqual(NumberValue.Zero));
				}
			}

			// Token: 0x0200140F RID: 5135
			private class CleanFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600846B RID: 33899 RVA: 0x001C2ED3 File Offset: 0x001C10D3
				public CleanFunctionValue()
					: base(NullableTypeValue.Text, "text", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600846C RID: 33900 RVA: 0x001C2EEC File Offset: 0x001C10EC
				public override Value TypedInvoke(Value text)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					string asString = text.AsString;
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < asString.Length; i++)
					{
						if (!char.IsControl(asString[i]))
						{
							stringBuilder.Append(asString[i]);
						}
					}
					return TextValue.New(stringBuilder.ToString());
				}
			}

			// Token: 0x02001410 RID: 5136
			private class PositionOfFunctionValue : NativeFunctionValue4<Value, TextValue, TextValue, Value, Value>
			{
				// Token: 0x0600846D RID: 33901 RVA: 0x001C2F4C File Offset: 0x001C114C
				public PositionOfFunctionValue()
					: base(TypeValue.Any, 2, "text", TypeValue.Text, "substring", TypeValue.Text, "occurrence", Library.Occurrence.Type.Nullable, "comparer", NullableTypeValue.Function)
				{
				}

				// Token: 0x0600846E RID: 33902 RVA: 0x001C2F94 File Offset: 0x001C1194
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					if (arguments.Count < 3)
					{
						return TypeValue.Int32;
					}
					Value value;
					if (!arguments[2].TryGetConstant(out value))
					{
						return TypeValue.Any.NonNullable;
					}
					if (!value.Equals(Library.Occurrence.All))
					{
						return TypeValue.Int32;
					}
					return ListTypeValue.New(TypeValue.Int32);
				}

				// Token: 0x0600846F RID: 33903 RVA: 0x001C2FE8 File Offset: 0x001C11E8
				public override Value TypedInvoke(TextValue text, TextValue substring, Value occurrence, Value comparer)
				{
					CultureInfo cultureInfo = CultureInfo.InvariantCulture;
					CompareOptions compareOptions = CompareOptions.Ordinal;
					if (!comparer.IsNull)
					{
						bool flag;
						if (!comparer.AsFunction.TryGetCultureCase(out cultureInfo, out flag))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.CultureUnawareComparer, comparer, null);
						}
						if (cultureInfo == null)
						{
							cultureInfo = CultureInfo.InvariantCulture;
							compareOptions = (flag ? CompareOptions.OrdinalIgnoreCase : CompareOptions.Ordinal);
						}
						else
						{
							compareOptions = (flag ? CompareOptions.IgnoreCase : CompareOptions.None);
						}
					}
					if (occurrence.IsNull || occurrence.Equals(Library.Occurrence.First))
					{
						return NumberValue.New(cultureInfo.CompareInfo.IndexOf(text.String, substring.String, compareOptions));
					}
					if (occurrence.Equals(Library.Occurrence.Last))
					{
						return NumberValue.New(cultureInfo.CompareInfo.LastIndexOf(text.String, substring.String, compareOptions));
					}
					List<Value> list = new List<Value>();
					int num = 0;
					string @string = text.String;
					string string2 = substring.String;
					while (num >= 0 && num < @string.Length)
					{
						num = cultureInfo.CompareInfo.IndexOf(@string, string2, num, compareOptions);
						if (num >= 0)
						{
							list.Add(NumberValue.New(num));
							num += Math.Max(string2.Length, 1);
						}
					}
					return ListValue.New(list.ToArray());
				}
			}

			// Token: 0x02001411 RID: 5137
			private class PositionOfAnyFunctionValue : NativeFunctionValue3<Value, TextValue, ListValue, Value>
			{
				// Token: 0x06008470 RID: 33904 RVA: 0x001C311C File Offset: 0x001C131C
				public PositionOfAnyFunctionValue()
					: base(TypeValue.Any, 2, "text", TypeValue.Text, "characters", TypeValue.List, "occurrence", Library.Occurrence.Type.Nullable)
				{
				}

				// Token: 0x06008471 RID: 33905 RVA: 0x001C3158 File Offset: 0x001C1358
				public override Value TypedInvoke(TextValue text, ListValue listOfChars, Value occurrence)
				{
					string @string = text.String;
					char[] charArrayFromTextOrList = ValueHelper.GetCharArrayFromTextOrList(listOfChars);
					if (occurrence.IsNull || occurrence.Equals(Library.Occurrence.First))
					{
						return NumberValue.New(@string.IndexOfAny(charArrayFromTextOrList));
					}
					if (occurrence.Equals(Library.Occurrence.Last))
					{
						return NumberValue.New(@string.LastIndexOfAny(charArrayFromTextOrList));
					}
					List<Value> list = new List<Value>();
					int num = 0;
					while (num >= 0 && num < @string.Length)
					{
						num = @string.IndexOfAny(charArrayFromTextOrList, num);
						if (num >= 0)
						{
							list.Add(NumberValue.New(num));
							num++;
						}
					}
					return ListValue.New(list.ToArray());
				}
			}

			// Token: 0x02001412 RID: 5138
			public class UpperFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008472 RID: 33906 RVA: 0x001C31F0 File Offset: 0x001C13F0
				public UpperFunctionValue(IEngineHost host)
					: base(host, null, NullableTypeValue.Text, 1, "text", NullableTypeValue.Text, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008473 RID: 33907 RVA: 0x001C321F File Offset: 0x001C141F
				public override Value TypedInvoke(Value text, Value culture)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					return TextValue.New(text.AsString.ToUpper(base.GetCulture(culture).GetCultureInfo()));
				}
			}

			// Token: 0x02001413 RID: 5139
			public class ProperFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008474 RID: 33908 RVA: 0x001C324C File Offset: 0x001C144C
				public ProperFunctionValue(IEngineHost host)
					: base(host, null, NullableTypeValue.Text, 1, "text", NullableTypeValue.Text, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008475 RID: 33909 RVA: 0x001C327C File Offset: 0x001C147C
				public override Value TypedInvoke(Value text, Value culture)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					bool flag = false;
					StringBuilder stringBuilder = new StringBuilder();
					string asString = text.AsString;
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					foreach (char c in asString)
					{
						if (char.IsLetter(c))
						{
							c = (flag ? char.ToLower(c, cultureInfo) : char.ToUpper(c, cultureInfo));
							flag = true;
						}
						else
						{
							flag = false;
						}
						stringBuilder.Append(c);
					}
					return TextValue.New(stringBuilder.ToString());
				}
			}

			// Token: 0x02001414 RID: 5140
			public class LowerFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008476 RID: 33910 RVA: 0x001C330C File Offset: 0x001C150C
				public LowerFunctionValue(IEngineHost host)
					: base(host, null, NullableTypeValue.Text, 1, "text", NullableTypeValue.Text, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008477 RID: 33911 RVA: 0x001C333B File Offset: 0x001C153B
				public override Value TypedInvoke(Value text, Value culture)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					return TextValue.New(text.AsString.ToLower(base.GetCulture(culture).GetCultureInfo()));
				}
			}

			// Token: 0x02001415 RID: 5141
			private class InsertFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, TextValue>
			{
				// Token: 0x06008478 RID: 33912 RVA: 0x001C3367 File Offset: 0x001C1567
				public InsertFunctionValue()
					: base(NullableTypeValue.Text, "text", NullableTypeValue.Text, "offset", TypeValue.Number, "newText", TypeValue.Text)
				{
				}

				// Token: 0x06008479 RID: 33913 RVA: 0x001C3394 File Offset: 0x001C1594
				public override Value TypedInvoke(Value text, NumberValue offset, TextValue newString)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = offset.AsInteger32;
					if (asInteger < 0 || asInteger > text.AsString.Length)
					{
						throw ValueException.ArgumentOutOfRange("offset", offset);
					}
					return TextValue.New(text.AsString.Insert(offset.AsInteger32, newString.String));
				}
			}

			// Token: 0x02001416 RID: 5142
			private class ReplaceRangeFunctionValue : NativeFunctionValue4<Value, Value, NumberValue, NumberValue, TextValue>
			{
				// Token: 0x0600847A RID: 33914 RVA: 0x001C33F0 File Offset: 0x001C15F0
				public ReplaceRangeFunctionValue()
					: base(NullableTypeValue.Text, "text", NullableTypeValue.Text, "offset", TypeValue.Number, "count", TypeValue.Number, "newText", TypeValue.Text)
				{
				}

				// Token: 0x0600847B RID: 33915 RVA: 0x001C3430 File Offset: 0x001C1630
				public override Value TypedInvoke(Value text, NumberValue offset, NumberValue count, TextValue newText)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int num = offset.AsInteger32;
					int asInteger = count.AsInteger32;
					int num2 = asInteger + num;
					string asString = text.AsString;
					if (num < 0)
					{
						throw ValueException.ArgumentOutOfRange("offset", offset);
					}
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					if (num > asString.Length)
					{
						num = asString.Length;
						num2 = asInteger + num;
					}
					if (num2 > asString.Length)
					{
						num2 = asString.Length;
					}
					return TextValue.New(text.AsString.Substring(0, num) + newText.String + text.AsString.Substring(num2));
				}
			}

			// Token: 0x02001417 RID: 5143
			private class ReplaceFunctionValue : NativeFunctionValue3<Value, Value, TextValue, TextValue>
			{
				// Token: 0x0600847C RID: 33916 RVA: 0x001C34D4 File Offset: 0x001C16D4
				public ReplaceFunctionValue()
					: base(NullableTypeValue.Text, 3, "text", NullableTypeValue.Text, "old", TypeValue.Text, "new", TypeValue.Text)
				{
				}

				// Token: 0x0600847D RID: 33917 RVA: 0x001C350B File Offset: 0x001C170B
				public override Value TypedInvoke(Value text, TextValue oldString, TextValue newString)
				{
					if (text.IsNull || oldString.String.Length == 0)
					{
						return text;
					}
					return TextValue.New(text.AsText.String.Replace(oldString.String, newString.String));
				}
			}

			// Token: 0x02001418 RID: 5144
			private class RemoveFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600847E RID: 33918 RVA: 0x001C3545 File Offset: 0x001C1745
				public RemoveFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "removeChars", TypeValue.Any)
				{
				}

				// Token: 0x0600847F RID: 33919 RVA: 0x001C3568 File Offset: 0x001C1768
				public override Value TypedInvoke(Value text, Value removeChars)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					HashSet<char> hashSet = new HashSet<char>();
					foreach (char c in ValueHelper.GetCharArrayFromTextOrList(removeChars))
					{
						hashSet.Add(c);
					}
					if (hashSet.Count == 0)
					{
						return text;
					}
					string asString = text.AsString;
					StringBuilder stringBuilder = new StringBuilder(asString.Length);
					foreach (char c2 in asString)
					{
						if (!hashSet.Contains(c2))
						{
							stringBuilder.Append(c2);
						}
					}
					return TextValue.New(stringBuilder.ToString());
				}
			}

			// Token: 0x02001419 RID: 5145
			private class RemoveRangeFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x06008480 RID: 33920 RVA: 0x001C360C File Offset: 0x001C180C
				public RemoveRangeFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "offset", TypeValue.Number, "count", NullableTypeValue.Number)
				{
				}

				// Token: 0x06008481 RID: 33921 RVA: 0x001C3644 File Offset: 0x001C1844
				public override Value TypedInvoke(Value text, NumberValue offsetValue, Value countValue)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = offsetValue.AsInteger32;
					int num = (countValue.IsNull ? 1 : countValue.AsInteger32);
					if (asInteger < 0 || asInteger > text.AsString.Length)
					{
						throw ValueException.ArgumentOutOfRange("offset", offsetValue);
					}
					if (num < 0 || num > text.AsString.Length - asInteger)
					{
						throw ValueException.ArgumentOutOfRange("count", countValue);
					}
					return TextValue.New(text.AsString.Remove(asInteger, num));
				}
			}

			// Token: 0x0200141A RID: 5146
			private class ReverseFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008482 RID: 33922 RVA: 0x001C36C8 File Offset: 0x001C18C8
				public ReverseFunctionValue()
					: base(NullableTypeValue.Text, 1, "text", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008483 RID: 33923 RVA: 0x001C36E0 File Offset: 0x001C18E0
				public override Value TypedInvoke(Value text)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					char[] array = text.AsText.AsString.ToCharArray();
					Array.Reverse(array);
					return TextValue.New(new string(array));
				}
			}

			// Token: 0x0200141B RID: 5147
			private class SelectFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008484 RID: 33924 RVA: 0x001C3710 File Offset: 0x001C1910
				public SelectFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "selectChars", TypeValue.Any)
				{
				}

				// Token: 0x06008485 RID: 33925 RVA: 0x001C3734 File Offset: 0x001C1934
				public override Value TypedInvoke(Value text, Value selectChars)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					HashSet<char> hashSet = new HashSet<char>();
					foreach (char c in ValueHelper.GetCharArrayFromTextOrList(selectChars))
					{
						hashSet.Add(c);
					}
					if (hashSet.Count == 0)
					{
						return TextValue.Empty;
					}
					string asString = text.AsString;
					StringBuilder stringBuilder = new StringBuilder(asString.Length);
					foreach (char c2 in asString)
					{
						if (hashSet.Contains(c2))
						{
							stringBuilder.Append(c2);
						}
					}
					return TextValue.New(stringBuilder.ToString());
				}
			}

			// Token: 0x0200141C RID: 5148
			private class ToBinaryFunctionValue : NativeFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x06008486 RID: 33926 RVA: 0x001C37DC File Offset: 0x001C19DC
				public ToBinaryFunctionValue()
					: base(NullableTypeValue.Binary, 1, "text", NullableTypeValue.Text, "encoding", TextEncoding.Type.Nullable, "includeByteOrderMark", NullableTypeValue.Logical)
				{
				}

				// Token: 0x06008487 RID: 33927 RVA: 0x001C3818 File Offset: 0x001C1A18
				public override Value TypedInvoke(Value text, Value encodingValue, Value includeByteOrderMark)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					Encoding encoding = TextEncoding.GetEncoding(encodingValue, includeByteOrderMark, TextEncoding.CodePage.Utf8);
					byte[] preamble = encoding.GetPreamble();
					int byteCount = encoding.GetByteCount(text.AsString);
					byte[] array = new byte[preamble.Length + byteCount];
					Buffer.BlockCopy(preamble, 0, array, 0, preamble.Length);
					encoding.GetBytes(text.AsString, 0, text.AsString.Length, array, preamble.Length);
					return BinaryValue.New(array);
				}
			}

			// Token: 0x0200141D RID: 5149
			private class FromBinaryFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008488 RID: 33928 RVA: 0x001C388C File Offset: 0x001C1A8C
				public FromBinaryFunctionValue()
					: base(NullableTypeValue.Text, 1, "binary", NullableTypeValue.Binary, "encoding", TextEncoding.Type.Nullable)
				{
				}

				// Token: 0x06008489 RID: 33929 RVA: 0x001C38B4 File Offset: 0x001C1AB4
				public override Value TypedInvoke(Value binary, Value encodingValue)
				{
					if (binary.IsNull)
					{
						return Value.Null;
					}
					TextDecoder textDecoder = TextEncoding.GetTextDecoder(encodingValue);
					byte[] asBytes = binary.AsBinary.AsBytes;
					return TextValue.New(textDecoder.Decode(asBytes, 0, asBytes.Length));
				}
			}

			// Token: 0x0200141E RID: 5150
			private class PadStartFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x0600848A RID: 33930 RVA: 0x001C38F0 File Offset: 0x001C1AF0
				public PadStartFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "count", TypeValue.Number, "character", NullableTypeValue.Character)
				{
				}

				// Token: 0x0600848B RID: 33931 RVA: 0x001C3928 File Offset: 0x001C1B28
				public override Value TypedInvoke(Value text, NumberValue count, Value character)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = count.AsInteger32;
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					string text2;
					if (character.IsNull)
					{
						text2 = text.AsString.PadLeft(asInteger);
					}
					else
					{
						text2 = text.AsString.PadLeft(asInteger, character.AsCharacter);
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x0200141F RID: 5151
			private class PadEndFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x0600848C RID: 33932 RVA: 0x001C398C File Offset: 0x001C1B8C
				public PadEndFunctionValue()
					: base(NullableTypeValue.Text, 2, "text", NullableTypeValue.Text, "count", TypeValue.Number, "character", NullableTypeValue.Character)
				{
				}

				// Token: 0x0600848D RID: 33933 RVA: 0x001C39C4 File Offset: 0x001C1BC4
				public override Value TypedInvoke(Value text, NumberValue count, Value character)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					int asInteger = count.AsInteger32;
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					string text2;
					if (character.IsNull)
					{
						text2 = text.AsString.PadRight(asInteger);
					}
					else
					{
						text2 = text.AsString.PadRight(asInteger, character.AsCharacter);
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x02001420 RID: 5152
			private class CombineFunctionValue : NativeFunctionValue2<TextValue, ListValue, Value>, IInvocationRewriter
			{
				// Token: 0x0600848E RID: 33934 RVA: 0x001C3A26 File Offset: 0x001C1C26
				public CombineFunctionValue()
					: base(TypeValue.Text, 1, "texts", TypeValue.List, "separator", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600848F RID: 33935 RVA: 0x001C3A48 File Offset: 0x001C1C48
				public override TextValue TypedInvoke(ListValue strings, Value separator)
				{
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = true;
					foreach (IValueReference valueReference in strings)
					{
						Value value = valueReference.Value;
						if (!value.IsNull)
						{
							if (!flag && !separator.IsNull)
							{
								stringBuilder.Append(separator.AsString);
							}
							stringBuilder.Append(value.AsString);
							flag = false;
						}
					}
					return TextValue.New(stringBuilder.ToString());
				}

				// Token: 0x06008490 RID: 33936 RVA: 0x001C3AD0 File Offset: 0x001C1CD0
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					expression = null;
					IList<IExpression> arguments = invocation.Arguments;
					Value @null = Value.Null;
					IListExpression listExpression;
					if (arguments.Count < 1 || arguments.Count > 2 || !environment.TryAsListExpression(arguments[0], 8 / arguments.Count, out listExpression) || (arguments.Count == 2 && !arguments[1].TryGetConstant(out @null)) || (!@null.IsNull && !@null.IsText))
					{
						return false;
					}
					if (listExpression.Members.Count == 0)
					{
						expression = Library.Text.CombineFunctionValue.literalBlank;
						return true;
					}
					IExpression expression2 = (@null.IsNull ? null : arguments[1]);
					IExpression expression3 = null;
					for (int i = listExpression.Members.Count - 1; i >= 0; i--)
					{
						IExpression expression4 = listExpression.Members[i];
						IExpression expression5 = BinaryExpressionSyntaxNode.New(BinaryOperator2.NotEquals, expression4, ConstantExpressionSyntaxNode.Null, TokenRange.Null);
						IExpression expression6 = new IfExpressionSyntaxNode(expression5, expression4, Library.Text.CombineFunctionValue.literalBlank, TokenRange.Null);
						if (expression == null)
						{
							expression = expression6;
							expression3 = expression5;
						}
						else
						{
							if (expression2 != null)
							{
								expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, new IfExpressionSyntaxNode(BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression5, expression3, TokenRange.Null), expression2, Library.Text.CombineFunctionValue.literalBlank, TokenRange.Null), expression, TokenRange.Null);
								expression3 = BinaryExpressionSyntaxNode.New(BinaryOperator2.Or, expression5, expression3, TokenRange.Null);
							}
							expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, expression6, expression, TokenRange.Null);
						}
					}
					return true;
				}

				// Token: 0x040049B2 RID: 18866
				private const int MaxTextCombineExpandedTerms = 8;

				// Token: 0x040049B3 RID: 18867
				private static readonly IExpression literalBlank = new ConstantExpressionSyntaxNode(TextValue.Empty);
			}

			// Token: 0x02001421 RID: 5153
			private class RepeatFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x06008492 RID: 33938 RVA: 0x001C2BE4 File Offset: 0x001C0DE4
				public RepeatFunctionValue()
					: base(NullableTypeValue.Text, "text", NullableTypeValue.Text, "count", TypeValue.Number)
				{
				}

				// Token: 0x06008493 RID: 33939 RVA: 0x001C3C44 File Offset: 0x001C1E44
				public override Value TypedInvoke(Value text, NumberValue count)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					if (count.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					StringBuilder stringBuilder = new StringBuilder();
					string asString = text.AsString;
					int asInteger = count.AsInteger32;
					for (int i = 0; i < asInteger; i++)
					{
						stringBuilder.Append(asString);
					}
					return TextValue.New(stringBuilder.ToString());
				}
			}

			// Token: 0x02001422 RID: 5154
			private class SplitFunctionValue : NativeFunctionValue2<ListValue, TextValue, TextValue>
			{
				// Token: 0x06008494 RID: 33940 RVA: 0x001C3CAB File Offset: 0x001C1EAB
				public SplitFunctionValue()
					: base(TypeValue.List, "text", TypeValue.Text, "separator", TypeValue.Text)
				{
				}

				// Token: 0x06008495 RID: 33941 RVA: 0x001C3CCC File Offset: 0x001C1ECC
				public override ListValue TypedInvoke(TextValue text, TextValue separator)
				{
					return ListValue.New(text.String.Split(new string[] { separator.String }, StringSplitOptions.None));
				}
			}

			// Token: 0x02001423 RID: 5155
			private class SplitAnyFunctionValue : NativeFunctionValue2<ListValue, TextValue, TextValue>
			{
				// Token: 0x06008496 RID: 33942 RVA: 0x001C3CEE File Offset: 0x001C1EEE
				public SplitAnyFunctionValue()
					: base(TypeValue.List, "text", TypeValue.Text, "separators", TypeValue.Text)
				{
				}

				// Token: 0x06008497 RID: 33943 RVA: 0x001C3D0F File Offset: 0x001C1F0F
				public override ListValue TypedInvoke(TextValue text, TextValue separators)
				{
					return ListValue.New(text.String.Split(separators.String.ToCharArray(), StringSplitOptions.None));
				}
			}

			// Token: 0x02001424 RID: 5156
			private class SplitBySequenceFunctionValue : NativeFunctionValue2<ListValue, TextValue, ListValue>
			{
				// Token: 0x06008498 RID: 33944 RVA: 0x001C3D2D File Offset: 0x001C1F2D
				public SplitBySequenceFunctionValue()
					: base(TypeValue.List, "text", TypeValue.Text, "delimiters", TypeValue.List)
				{
				}

				// Token: 0x06008499 RID: 33945 RVA: 0x001C3D50 File Offset: 0x001C1F50
				public override ListValue TypedInvoke(TextValue text, ListValue delimiters)
				{
					List<Value> list = new List<Value>();
					foreach (IValueReference valueReference in delimiters)
					{
						TextValue asText = valueReference.Value.AsText;
						int asInteger = Library.Text.PositionOf.Invoke(text, asText).AsNumber.AsInteger32;
						if (asInteger < 0)
						{
							break;
						}
						list.Add(Library.Text.Range.Invoke(text, NumberValue.New(0), NumberValue.New(asInteger)));
						int num = asInteger + asText.Length;
						if (num < text.Length)
						{
							text = Library.Text.Range.Invoke(text, NumberValue.New(num)).AsText;
						}
						else
						{
							text = TextValue.Empty;
						}
					}
					if (text.Length >= 0 || list.Count == 0)
					{
						list.Add(text);
					}
					return ListValue.New(list.ToArray());
				}
			}

			// Token: 0x02001425 RID: 5157
			private class TrimFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600849A RID: 33946 RVA: 0x001C3E3C File Offset: 0x001C203C
				public TrimFunctionValue()
					: base(NullableTypeValue.Text, 1, "text", NullableTypeValue.Text, "trim", TypeValue.Any)
				{
				}

				// Token: 0x0600849B RID: 33947 RVA: 0x001C3E60 File Offset: 0x001C2060
				public override Value TypedInvoke(Value text, Value trim)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					string text2;
					if (trim.IsNull)
					{
						text2 = text.AsString.Trim(Library.whitespaceChars);
					}
					else
					{
						text2 = text.AsString.Trim(ValueHelper.GetCharArrayFromTextOrList(trim));
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x02001426 RID: 5158
			private class TrimStartFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600849C RID: 33948 RVA: 0x001C3E3C File Offset: 0x001C203C
				public TrimStartFunctionValue()
					: base(NullableTypeValue.Text, 1, "text", NullableTypeValue.Text, "trim", TypeValue.Any)
				{
				}

				// Token: 0x0600849D RID: 33949 RVA: 0x001C3EB0 File Offset: 0x001C20B0
				public override Value TypedInvoke(Value text, Value trim)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					string text2;
					if (trim.IsNull)
					{
						text2 = text.AsString.TrimStart(Library.whitespaceChars);
					}
					else
					{
						text2 = text.AsString.TrimStart(ValueHelper.GetCharArrayFromTextOrList(trim));
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x02001427 RID: 5159
			private class TrimEndFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600849E RID: 33950 RVA: 0x001C3E3C File Offset: 0x001C203C
				public TrimEndFunctionValue()
					: base(NullableTypeValue.Text, 1, "text", NullableTypeValue.Text, "trim", TypeValue.Any)
				{
				}

				// Token: 0x0600849F RID: 33951 RVA: 0x001C3F00 File Offset: 0x001C2100
				public override Value TypedInvoke(Value text, Value trim)
				{
					if (text.IsNull)
					{
						return Value.Null;
					}
					string text2;
					if (trim.IsNull)
					{
						text2 = text.AsString.TrimEnd(Library.whitespaceChars);
					}
					else
					{
						text2 = text.AsString.TrimEnd(ValueHelper.GetCharArrayFromTextOrList(trim));
					}
					return TextValue.New(text2);
				}
			}

			// Token: 0x02001428 RID: 5160
			private class ToListFunctionValue : NativeFunctionValue1<ListValue, TextValue>
			{
				// Token: 0x060084A0 RID: 33952 RVA: 0x001C3F50 File Offset: 0x001C2150
				public ToListFunctionValue()
					: base(TypeValue.List, "text", TypeValue.Text)
				{
				}

				// Token: 0x060084A1 RID: 33953 RVA: 0x001C3F67 File Offset: 0x001C2167
				public override ListValue TypedInvoke(TextValue text)
				{
					return new Library.Text.ToListFunctionValue.TextListValue(text.String);
				}

				// Token: 0x02001429 RID: 5161
				private class TextListValue : ArrayListValue
				{
					// Token: 0x060084A2 RID: 33954 RVA: 0x001C3F74 File Offset: 0x001C2174
					public TextListValue(string value)
					{
						this.value = value;
					}

					// Token: 0x1700236C RID: 9068
					// (get) Token: 0x060084A3 RID: 33955 RVA: 0x001C3F83 File Offset: 0x001C2183
					public sealed override int Count
					{
						get
						{
							return this.value.Length;
						}
					}

					// Token: 0x1700236D RID: 9069
					public sealed override Value this[int index]
					{
						get
						{
							return TextValue.New(this.value[index]);
						}
					}

					// Token: 0x040049B4 RID: 18868
					private readonly string value;
				}
			}

			// Token: 0x0200142A RID: 5162
			public sealed class NewGuidFunctionValue : NativeFunctionValue0<TextValue>
			{
				// Token: 0x060084A5 RID: 33957 RVA: 0x001C3FA3 File Offset: 0x001C21A3
				public NewGuidFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Guid)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x1700236E RID: 9070
				// (get) Token: 0x060084A6 RID: 33958 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x060084A7 RID: 33959 RVA: 0x001C3FB8 File Offset: 0x001C21B8
				public override TextValue TypedInvoke()
				{
					this.engineHost.CheckVolatileFunctionsAllowed();
					return TextValue.New(Guid.NewGuid().ToString());
				}

				// Token: 0x060084A8 RID: 33960 RVA: 0x001C3FE8 File Offset: 0x001C21E8
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && this.engineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}

				// Token: 0x040049B5 RID: 18869
				private readonly IEngineHost engineHost;
			}

			// Token: 0x0200142B RID: 5163
			public static class TextValidation
			{
				// Token: 0x060084A9 RID: 33961 RVA: 0x001C4038 File Offset: 0x001C2238
				public static bool IsNullOrWhiteSpace(Value textValue)
				{
					if (textValue.IsNull)
					{
						return true;
					}
					string asString = textValue.AsString;
					int length = asString.Length;
					if (length == 0)
					{
						return true;
					}
					int num = -1;
					for (int i = 0; i < length; i++)
					{
						char c = asString[i];
						if (c != ' ' || (c < '\u0080' && !char.IsWhiteSpace(c)))
						{
							num = i;
							break;
						}
					}
					if (num == -1)
					{
						return true;
					}
					int num2 = -1;
					for (int j = length - 1; j >= 0; j--)
					{
						char c2 = asString[j];
						if (c2 != ' ' || (c2 < '\u0080' && !char.IsWhiteSpace(c2)))
						{
							num2 = j;
							break;
						}
					}
					return num2 - num + 1 == 4 && ((asString[num] == 'n' || asString[num] == 'N') && (asString[num + 1] == 'u' || asString[num + 1] == 'U') && (asString[num + 2] == 'l' || asString[num + 2] == 'L') && (asString[num + 3] == 'l' || asString[num + 3] == 'L'));
				}
			}
		}

		// Token: 0x0200142C RID: 5164
		public static class Day
		{
			// Token: 0x040049B6 RID: 18870
			public static readonly IntEnumTypeValue<DayOfWeek> Type = new IntEnumTypeValue<DayOfWeek>("Day.Type");

			// Token: 0x040049B7 RID: 18871
			public static readonly NumberValue Sunday = Library.Day.Type.NewEnumValue("Day.Sunday", 0, DayOfWeek.Sunday, null);

			// Token: 0x040049B8 RID: 18872
			public static readonly NumberValue Monday = Library.Day.Type.NewEnumValue("Day.Monday", 1, DayOfWeek.Monday, null);

			// Token: 0x040049B9 RID: 18873
			public static readonly NumberValue Tuesday = Library.Day.Type.NewEnumValue("Day.Tuesday", 2, DayOfWeek.Tuesday, null);

			// Token: 0x040049BA RID: 18874
			public static readonly NumberValue Wednesday = Library.Day.Type.NewEnumValue("Day.Wednesday", 3, DayOfWeek.Wednesday, null);

			// Token: 0x040049BB RID: 18875
			public static readonly NumberValue Thursday = Library.Day.Type.NewEnumValue("Day.Thursday", 4, DayOfWeek.Thursday, null);

			// Token: 0x040049BC RID: 18876
			public static readonly NumberValue Friday = Library.Day.Type.NewEnumValue("Day.Friday", 5, DayOfWeek.Friday, null);

			// Token: 0x040049BD RID: 18877
			public static readonly NumberValue Saturday = Library.Day.Type.NewEnumValue("Day.Saturday", 6, DayOfWeek.Saturday, null);
		}

		// Token: 0x0200142D RID: 5165
		public static class Date
		{
			// Token: 0x060084AB RID: 33963 RVA: 0x001C4210 File Offset: 0x001C2410
			private static DateValue ConvertFromText(TextValue text, CultureInfo cultureInfo)
			{
				DateValue dateValue = null;
				if (DateValue.TryParseFromText(text.String, cultureInfo, out dateValue))
				{
					return dateValue;
				}
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_CannotParseTextAsDateTimeError, text, null);
			}

			// Token: 0x060084AC RID: 33964 RVA: 0x001C4240 File Offset: 0x001C2440
			private static DateValue ConvertFromText(TextValue text, string format, CultureInfo cultureInfo)
			{
				DateValue dateValue = null;
				if (DateValue.TryParseFromText(text.String, format, cultureInfo, out dateValue))
				{
					return dateValue;
				}
				throw ValueException.NewDataFormatError<Message0>(Strings.Date_CannotParseTextAsDateTimeError, text, null);
			}

			// Token: 0x060084AD RID: 33965 RVA: 0x001C4270 File Offset: 0x001C2470
			public static TextValue ConvertToText(Value date, Value format, ICulture culture)
			{
				if (format.IsNull)
				{
					CultureInfo cultureInfo = culture.GetCultureInfo();
					return TextValue.New(date.AsDate.ToString(cultureInfo.DateTimeFormat.ShortDatePattern, cultureInfo));
				}
				return TextValue.New(date.AsDate.ToString(format.AsString, culture.Value));
			}

			// Token: 0x060084AE RID: 33966 RVA: 0x001C42C5 File Offset: 0x001C24C5
			private static int GetWeekOfYear(global::System.DateTime dateTime, DayOfWeek firstDayOfWeek)
			{
				return DateValue.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, firstDayOfWeek);
			}

			// Token: 0x060084AF RID: 33967 RVA: 0x001C42D4 File Offset: 0x001C24D4
			private static DayOfWeek GetFirstDayOfWeek(DayOfWeek defaultFirstDay, Value firstDayOfWeek)
			{
				DayOfWeek dayOfWeek;
				if (firstDayOfWeek.IsNull)
				{
					dayOfWeek = defaultFirstDay;
				}
				else
				{
					int asInteger = firstDayOfWeek.AsInteger32;
					if (asInteger < 0 || asInteger > 6)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.DateTime_InvalidDayValue, firstDayOfWeek, null);
					}
					dayOfWeek = (DayOfWeek)asInteger;
				}
				return dayOfWeek;
			}

			// Token: 0x040049BE RID: 18878
			public static readonly FunctionValue date = new Library.Date.DateFunctionValue();

			// Token: 0x040049BF RID: 18879
			public static readonly FunctionValue ToRecord = new Library.Date.ToRecordFunctionValue();

			// Token: 0x040049C0 RID: 18880
			public static readonly FunctionValue Year = new Library.Date.YearFunctionValue();

			// Token: 0x040049C1 RID: 18881
			public static readonly FunctionValue Month = new Library.Date.MonthFunctionValue();

			// Token: 0x040049C2 RID: 18882
			public static readonly FunctionValue Day = new Library.Date.DayFunctionValue();

			// Token: 0x040049C3 RID: 18883
			public static readonly FunctionValue AddDays = new Library.Date.AddDaysFunctionValue();

			// Token: 0x040049C4 RID: 18884
			public static readonly FunctionValue AddWeeks = new Library.Date.AddWeeksFunctionValue();

			// Token: 0x040049C5 RID: 18885
			public static readonly FunctionValue AddMonths = new Library.Date.AddMonthsFunctionValue();

			// Token: 0x040049C6 RID: 18886
			public static readonly FunctionValue AddQuarters = new Library.Date.AddQuartersFunctionValue();

			// Token: 0x040049C7 RID: 18887
			public static readonly FunctionValue AddYears = new Library.Date.AddYearsFunctionValue();

			// Token: 0x040049C8 RID: 18888
			public static readonly FunctionValue StartOfYear = new Library.Date.StartOfYearFunctionValue();

			// Token: 0x040049C9 RID: 18889
			public static readonly FunctionValue StartOfQuarter = new Library.Date.StartOfQuarterFunctionValue();

			// Token: 0x040049CA RID: 18890
			public static readonly FunctionValue StartOfMonth = new Library.Date.StartOfMonthFunctionValue();

			// Token: 0x040049CB RID: 18891
			public static readonly FunctionValue StartOfDay = new Library.Date.StartOfDayFunctionValue();

			// Token: 0x040049CC RID: 18892
			public static readonly FunctionValue EndOfYear = new Library.Date.EndOfYearFunctionValue();

			// Token: 0x040049CD RID: 18893
			public static readonly FunctionValue EndOfQuarter = new Library.Date.EndOfQuarterFunctionValue();

			// Token: 0x040049CE RID: 18894
			public static readonly FunctionValue EndOfMonth = new Library.Date.EndOfMonthFunctionValue();

			// Token: 0x040049CF RID: 18895
			public static readonly FunctionValue EndOfDay = new Library.Date.EndOfDayFunctionValue();

			// Token: 0x040049D0 RID: 18896
			public static readonly FunctionValue DayOfYear = new Library.Date.DayOfYearFunctionValue();

			// Token: 0x040049D1 RID: 18897
			public static readonly FunctionValue IsLeapYear = new Library.Date.IsLeapYearFunctionValue();

			// Token: 0x040049D2 RID: 18898
			public static readonly FunctionValue DaysInMonth = new Library.Date.DaysInMonthFunctionValue();

			// Token: 0x040049D3 RID: 18899
			public static readonly FunctionValue QuarterOfYear = new Library.Date.QuarterOfYearFunctionValue();

			// Token: 0x0200142E RID: 5166
			private class DateFunctionValue : NativeFunctionValue3<DateValue, NumberValue, NumberValue, NumberValue>
			{
				// Token: 0x060084B1 RID: 33969 RVA: 0x001C43F5 File Offset: 0x001C25F5
				public DateFunctionValue()
					: base(TypeValue.Date, "year", TypeValue.Number, "month", TypeValue.Number, "day", TypeValue.Number)
				{
				}

				// Token: 0x060084B2 RID: 33970 RVA: 0x001C4420 File Offset: 0x001C2620
				public override DateValue TypedInvoke(NumberValue year, NumberValue month, NumberValue day)
				{
					return DateValue.New(year.AsInteger32, month.AsInteger32, day.AsInteger32);
				}

				// Token: 0x060084B3 RID: 33971 RVA: 0x001C4439 File Offset: 0x001C2639
				public override string ToSource()
				{
					return "#date";
				}
			}

			// Token: 0x0200142F RID: 5167
			public class ToTextFunctionValue : FormatOptionsFunctionValue<TextValue, DateValue, Value, Value>
			{
				// Token: 0x060084B4 RID: 33972 RVA: 0x001C4440 File Offset: 0x001C2640
				public ToTextFunctionValue(IEngineHost host)
					: base(host, ConversionDirection.ToText, "date", TypeValue.Date, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x060084B5 RID: 33973 RVA: 0x001C445E File Offset: 0x001C265E
				protected override TextValue Convert(DateValue date, string format, ICulture culture)
				{
					return Library.Date.ConvertToText(date, TextValue.New(format), culture);
				}

				// Token: 0x060084B6 RID: 33974 RVA: 0x001C4470 File Offset: 0x001C2670
				protected override TextValue TypedInvokeNoOptions(DateValue date, Value format, Value culture)
				{
					ICulture culture2 = base.GetCulture(culture);
					return Library.Date.ConvertToText(date, format, culture2);
				}
			}

			// Token: 0x02001430 RID: 5168
			public class FromTextFunctionValue : FormatOptionsFunctionValue<DateValue, TextValue, Value>
			{
				// Token: 0x060084B7 RID: 33975 RVA: 0x001C448D File Offset: 0x001C268D
				public FromTextFunctionValue(IEngineHost host)
					: base(host, TypeValue.Date, ConversionDirection.FromText, "text")
				{
				}

				// Token: 0x060084B8 RID: 33976 RVA: 0x001C44A4 File Offset: 0x001C26A4
				protected override Value Convert(TextValue text, TextValue format, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					DateValue dateValue;
					if (InstantParser.TryParseDate(text.AsString, format.String, Culture.ResolveCulture(this.engineHost, culture).Value, out dateValue))
					{
						return dateValue;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Date_CannotParseTextAsDateTimeError, RecordValue.New(Keys.New("Input", "Format", "Culture"), new Value[] { text, format, culture }), null);
				}

				// Token: 0x060084B9 RID: 33977 RVA: 0x001C4520 File Offset: 0x001C2720
				protected override Value TypedInvokeNoOptions(TextValue text, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					CultureInfo cultureInfo = Culture.GetCulture(this.engineHost, culture, Culture.GetDefaultCulture(this.engineHost)).GetCultureInfo();
					return Library.Date.ConvertFromText(text.AsText, cultureInfo);
				}
			}

			// Token: 0x02001431 RID: 5169
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060084BA RID: 33978 RVA: 0x001C4564 File Offset: 0x001C2764
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, NullableTypeValue.Date, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x1700236F RID: 9071
				// (get) Token: 0x060084BB RID: 33979 RVA: 0x001C4593 File Offset: 0x001C2793
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.DateFrom;
					}
				}

				// Token: 0x17002370 RID: 9072
				// (get) Token: 0x060084BC RID: 33980 RVA: 0x001C459A File Offset: 0x001C279A
				protected ITimeZone TimeZone
				{
					get
					{
						if (this.timeZone == null)
						{
							this.timeZone = base.QueryService<ITimeZoneService>().DefaultTimeZone;
						}
						return this.timeZone;
					}
				}

				// Token: 0x060084BD RID: 33981 RVA: 0x001C45BC File Offset: 0x001C27BC
				public override Value TypedInvoke(Value value, Value culture)
				{
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					switch (value.Kind)
					{
					case ValueKind.Null:
						return value;
					case ValueKind.Date:
						return value;
					case ValueKind.DateTime:
						return DateValue.New(value.AsDateTime.AsClrDateTime.Date);
					case ValueKind.DateTimeZone:
						return DateValue.New(value.AsDateTimeZone.AsClrDateTimeOffset.UtcDateTime.AdjustForTimeZone(this.TimeZone).Date);
					case ValueKind.Number:
						return DateValue.New(Library.FromCommon.NumberToDateTime(value, Strings.Date_NotConvertibleToDate).Date);
					case ValueKind.Text:
						if (!Library.Text.TextValidation.IsNullOrWhiteSpace(value))
						{
							return Library.Date.ConvertFromText(value.AsText, cultureInfo);
						}
						return Value.Null;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Date_NotConvertibleToDate, value, null);
				}

				// Token: 0x040049D4 RID: 18900
				private ITimeZone timeZone;
			}

			// Token: 0x02001432 RID: 5170
			private class ToRecordFunctionValue : NativeFunctionValue1<RecordValue, DateValue>
			{
				// Token: 0x060084BE RID: 33982 RVA: 0x001C469A File Offset: 0x001C289A
				public ToRecordFunctionValue()
					: base(TypeValue.Record, 1, "date", TypeValue.Date)
				{
				}

				// Token: 0x060084BF RID: 33983 RVA: 0x001C46B2 File Offset: 0x001C28B2
				public override RecordValue TypedInvoke(DateValue date)
				{
					return date.ToRecord();
				}
			}

			// Token: 0x02001433 RID: 5171
			private class AddDaysFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x060084C0 RID: 33984 RVA: 0x001C46BA File Offset: 0x001C28BA
				public AddDaysFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any, "numberOfDays", TypeValue.Number)
				{
				}

				// Token: 0x060084C1 RID: 33985 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084C2 RID: 33986 RVA: 0x001C46EC File Offset: 0x001C28EC
				public override Value TypedInvoke(Value dateTime, NumberValue numberOfDays)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.AddDays(numberOfDays.AsInteger32);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.AddDays(numberOfDays.AsInteger32);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.AddDays(numberOfDays.AsInteger32);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001434 RID: 5172
			private class AddWeeksFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x060084C3 RID: 33987 RVA: 0x001C476B File Offset: 0x001C296B
				public AddWeeksFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any, "numberOfWeeks", TypeValue.Number)
				{
				}

				// Token: 0x060084C4 RID: 33988 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084C5 RID: 33989 RVA: 0x001C478C File Offset: 0x001C298C
				public override Value TypedInvoke(Value dateTime, NumberValue numberOfWeeks)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.AddWeeks(numberOfWeeks.AsInteger32);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.AddWeeks(numberOfWeeks.AsInteger32);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.AddWeeks(numberOfWeeks.AsInteger32);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001435 RID: 5173
			private class AddMonthsFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x060084C6 RID: 33990 RVA: 0x001C480B File Offset: 0x001C2A0B
				public AddMonthsFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any, "numberOfMonths", TypeValue.Number)
				{
				}

				// Token: 0x060084C7 RID: 33991 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084C8 RID: 33992 RVA: 0x001C482C File Offset: 0x001C2A2C
				public override Value TypedInvoke(Value dateTime, NumberValue numberOfMonths)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.AddMonths(numberOfMonths.AsInteger32);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.AddMonths(numberOfMonths.AsInteger32);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.AddMonths(numberOfMonths.AsInteger32);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001436 RID: 5174
			private class AddQuartersFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x060084C9 RID: 33993 RVA: 0x001C48AB File Offset: 0x001C2AAB
				public AddQuartersFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any, "numberOfQuarters", TypeValue.Number)
				{
				}

				// Token: 0x060084CA RID: 33994 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084CB RID: 33995 RVA: 0x001C48CC File Offset: 0x001C2ACC
				public override Value TypedInvoke(Value dateTime, NumberValue numberOfQuarters)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.AddQuarters(numberOfQuarters.AsInteger32);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.AddQuarters(numberOfQuarters.AsInteger32);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.AddQuarters(numberOfQuarters.AsInteger32);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001437 RID: 5175
			private class AddYearsFunctionValue : NativeFunctionValue2<Value, Value, NumberValue>
			{
				// Token: 0x060084CC RID: 33996 RVA: 0x001C494B File Offset: 0x001C2B4B
				public AddYearsFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any, "numberOfYears", TypeValue.Number)
				{
				}

				// Token: 0x060084CD RID: 33997 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084CE RID: 33998 RVA: 0x001C496C File Offset: 0x001C2B6C
				public override Value TypedInvoke(Value dateTime, NumberValue numberOfYears)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.AddYears(numberOfYears.AsInteger32);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.AddYears(numberOfYears.AsInteger32);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.AddYears(numberOfYears.AsInteger32);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001438 RID: 5176
			private class YearFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084CF RID: 33999 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public YearFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084D0 RID: 34000 RVA: 0x001C4A04 File Offset: 0x001C2C04
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return NumberValue.New(dateTime.AsDate.Year);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.Year);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.Year);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001439 RID: 5177
			private class MonthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084D1 RID: 34001 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public MonthFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084D2 RID: 34002 RVA: 0x001C4A80 File Offset: 0x001C2C80
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return NumberValue.New(dateTime.AsDate.Month);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.Month);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.Month);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x0200143A RID: 5178
			private class DayFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084D3 RID: 34003 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public DayFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084D4 RID: 34004 RVA: 0x001C4AFC File Offset: 0x001C2CFC
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return NumberValue.New(dateTime.AsDate.Day);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.Day);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.Day);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x0200143B RID: 5179
			private class StartOfYearFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084D5 RID: 34005 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public StartOfYearFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084D6 RID: 34006 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084D7 RID: 34007 RVA: 0x001C4B90 File Offset: 0x001C2D90
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.StartOfYear();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.StartOfYear();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.StartOfYear();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x0200143C RID: 5180
			private class StartOfQuarterFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084D8 RID: 34008 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public StartOfQuarterFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084D9 RID: 34009 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084DA RID: 34010 RVA: 0x001C4C00 File Offset: 0x001C2E00
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.StartOfQuarter();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.StartOfQuarter();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.StartOfQuarter();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x0200143D RID: 5181
			private class StartOfMonthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084DB RID: 34011 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public StartOfMonthFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084DC RID: 34012 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084DD RID: 34013 RVA: 0x001C4C70 File Offset: 0x001C2E70
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.StartOfMonth();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.StartOfMonth();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.StartOfMonth();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x0200143E RID: 5182
			internal class StartOfWeekFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060084DE RID: 34014 RVA: 0x001C4CE0 File Offset: 0x001C2EE0
				public StartOfWeekFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Any, 1, "dateTime", TypeValue.Any, "firstDayOfWeek", Library.Day.Type.Nullable)
				{
					this.defaultFirstDayOfWeek = base.GetCulture(Value.Null).GetCultureInfo().DateTimeFormat.FirstDayOfWeek;
				}

				// Token: 0x060084DF RID: 34015 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084E0 RID: 34016 RVA: 0x001C4D34 File Offset: 0x001C2F34
				public override Value TypedInvoke(Value dateTime, Value firstDayOfWeek)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					DayOfWeek firstDayOfWeek2 = Library.Date.GetFirstDayOfWeek(this.defaultFirstDayOfWeek, firstDayOfWeek);
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.StartOfWeek(firstDayOfWeek2);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.StartOfWeek(firstDayOfWeek2);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.StartOfWeek(firstDayOfWeek2);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}

				// Token: 0x040049D5 RID: 18901
				private readonly DayOfWeek defaultFirstDayOfWeek;
			}

			// Token: 0x0200143F RID: 5183
			private class StartOfDayFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084E1 RID: 34017 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public StartOfDayFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084E2 RID: 34018 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084E3 RID: 34019 RVA: 0x001C4DB4 File Offset: 0x001C2FB4
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.StartOfDay();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.StartOfDay();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.StartOfDay();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001440 RID: 5184
			private class EndOfYearFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084E4 RID: 34020 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public EndOfYearFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084E5 RID: 34021 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084E6 RID: 34022 RVA: 0x001C4E24 File Offset: 0x001C3024
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.EndOfYear();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.EndOfYear();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.EndOfYear();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001441 RID: 5185
			private class EndOfQuarterFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084E7 RID: 34023 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public EndOfQuarterFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084E8 RID: 34024 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084E9 RID: 34025 RVA: 0x001C4E94 File Offset: 0x001C3094
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.EndOfQuarter();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.EndOfQuarter();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.EndOfQuarter();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001442 RID: 5186
			private class EndOfMonthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084EA RID: 34026 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public EndOfMonthFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084EB RID: 34027 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084EC RID: 34028 RVA: 0x001C4F04 File Offset: 0x001C3104
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.EndOfMonth();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.EndOfMonth();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.EndOfMonth();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001443 RID: 5187
			internal class EndOfWeekFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060084ED RID: 34029 RVA: 0x001C4F74 File Offset: 0x001C3174
				public EndOfWeekFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Any, 1, "dateTime", TypeValue.Any, "firstDayOfWeek", Library.Day.Type.Nullable)
				{
					this.defaultFirstDayOfWeek = base.GetCulture(Value.Null).GetCultureInfo().DateTimeFormat.FirstDayOfWeek;
				}

				// Token: 0x060084EE RID: 34030 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084EF RID: 34031 RVA: 0x001C4FC8 File Offset: 0x001C31C8
				public override Value TypedInvoke(Value dateTime, Value firstDayOfWeek)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					DayOfWeek dayOfWeek = Library.Date.GetFirstDayOfWeek(this.defaultFirstDayOfWeek, firstDayOfWeek) - 1;
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.EndOfWeek(dayOfWeek);
					case ValueKind.DateTime:
						return dateTime.AsDateTime.EndOfWeek(dayOfWeek);
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.EndOfWeek(dayOfWeek);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}

				// Token: 0x040049D6 RID: 18902
				private readonly DayOfWeek defaultFirstDayOfWeek;
			}

			// Token: 0x02001444 RID: 5188
			private class EndOfDayFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084F0 RID: 34032 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public EndOfDayFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084F1 RID: 34033 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x060084F2 RID: 34034 RVA: 0x001C5048 File Offset: 0x001C3248
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime.AsDate.EndOfDay();
					case ValueKind.DateTime:
						return dateTime.AsDateTime.EndOfDay();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.EndOfDay();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001445 RID: 5189
			private class QuarterOfYearFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084F3 RID: 34035 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public QuarterOfYearFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084F4 RID: 34036 RVA: 0x001C50B5 File Offset: 0x001C32B5
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New((int)Math.Ceiling((double)Library.Date.Month.Invoke(dateTime).AsInteger32 / 3.0)).AsNumber;
				}
			}

			// Token: 0x02001446 RID: 5190
			internal class WeekOfYearFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060084F5 RID: 34037 RVA: 0x001C50F0 File Offset: 0x001C32F0
				public WeekOfYearFunctionValue(IEngineHost host)
					: base(host, null, NullableTypeValue.Int32, 1, "dateTime", TypeValue.Any, "firstDayOfWeek", Library.Day.Type.Nullable)
				{
					this.defaultFirstDayOfWeek = base.GetCulture(Value.Null).GetCultureInfo().DateTimeFormat.FirstDayOfWeek;
				}

				// Token: 0x060084F6 RID: 34038 RVA: 0x001C5144 File Offset: 0x001C3344
				public override Value TypedInvoke(Value dateTime, Value firstDayOfWeek)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					global::System.DateTime dateTime2;
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						dateTime2 = dateTime.AsDate.AsClrDateTime;
						break;
					case ValueKind.DateTime:
						dateTime2 = dateTime.AsDateTime.AsClrDateTime;
						break;
					case ValueKind.DateTimeZone:
						dateTime2 = dateTime.AsDateTimeZone.AsClrDateTimeOffset.DateTime;
						break;
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
					return NumberValue.New(Library.Date.GetWeekOfYear(dateTime2, Library.Date.GetFirstDayOfWeek(this.defaultFirstDayOfWeek, firstDayOfWeek))).AsNumber;
				}

				// Token: 0x040049D7 RID: 18903
				private readonly DayOfWeek defaultFirstDayOfWeek;
			}

			// Token: 0x02001447 RID: 5191
			internal class WeekOfMonthFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060084F7 RID: 34039 RVA: 0x001C51DC File Offset: 0x001C33DC
				public WeekOfMonthFunctionValue(IEngineHost host)
					: base(host, null, NullableTypeValue.Int32, 1, "dateTime", TypeValue.Any, "firstDayOfWeek", Library.Day.Type.Nullable)
				{
					this.defaultFirstDayOfWeek = base.GetCulture(Value.Null).GetCultureInfo().DateTimeFormat.FirstDayOfWeek;
				}

				// Token: 0x060084F8 RID: 34040 RVA: 0x001C5230 File Offset: 0x001C3430
				public override Value TypedInvoke(Value dateTime, Value firstDayOfWeek)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					global::System.DateTime dateTime2;
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						dateTime2 = dateTime.AsDate.AsClrDateTime;
						break;
					case ValueKind.DateTime:
						dateTime2 = dateTime.AsDateTime.AsClrDateTime;
						break;
					case ValueKind.DateTimeZone:
						dateTime2 = dateTime.AsDateTimeZone.AsClrDateTimeOffset.DateTime;
						break;
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
					global::System.DateTime dateTime3 = new global::System.DateTime(dateTime2.Year, dateTime2.Month, 1);
					DayOfWeek firstDayOfWeek2 = Library.Date.GetFirstDayOfWeek(this.defaultFirstDayOfWeek, firstDayOfWeek);
					return NumberValue.New(Library.Date.GetWeekOfYear(dateTime2, firstDayOfWeek2) - Library.Date.GetWeekOfYear(dateTime3, firstDayOfWeek2) + 1).AsNumber;
				}

				// Token: 0x040049D8 RID: 18904
				private readonly DayOfWeek defaultFirstDayOfWeek;
			}

			// Token: 0x02001448 RID: 5192
			private class DaysInMonthFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084F9 RID: 34041 RVA: 0x001C52EB File Offset: 0x001C34EB
				public DaysInMonthFunctionValue()
					: base(NullableTypeValue.Int32, 1, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084FA RID: 34042 RVA: 0x001C5304 File Offset: 0x001C3504
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					int num;
					int num2;
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
					{
						DateValue asDate = dateTime.AsDate;
						num = asDate.Month;
						num2 = asDate.Year;
						break;
					}
					case ValueKind.DateTime:
					{
						DateTimeValue asDateTime = dateTime.AsDateTime;
						num = asDateTime.Month;
						num2 = asDateTime.Year;
						break;
					}
					case ValueKind.DateTimeZone:
					{
						DateTimeZoneValue asDateTimeZone = dateTime.AsDateTimeZone;
						num = asDateTimeZone.Month;
						num2 = asDateTimeZone.Year;
						break;
					}
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
					}
					return Library.Date.DaysInMonthFunctionValue.DaysInMonth(num, num2);
				}

				// Token: 0x060084FB RID: 34043 RVA: 0x001C5394 File Offset: 0x001C3594
				private static NumberValue DaysInMonth(int month, int year)
				{
					NumberValue numberValue;
					try
					{
						numberValue = NumberValue.New(global::System.DateTime.DaysInMonth(year, month));
					}
					catch (ArgumentOutOfRangeException ex)
					{
						throw ValueException.NewExpressionError(ex.Message, RecordValue.New(Keys.New("Month", "Year"), new Value[]
						{
							NumberValue.New(month),
							NumberValue.New(year)
						}), ex);
					}
					return numberValue;
				}
			}

			// Token: 0x02001449 RID: 5193
			private class IsLeapYearFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060084FC RID: 34044 RVA: 0x001C53FC File Offset: 0x001C35FC
				public IsLeapYearFunctionValue()
					: base(NullableTypeValue.Logical, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x060084FD RID: 34045 RVA: 0x001C5414 File Offset: 0x001C3614
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return Library.Date.IsLeapYearFunctionValue.IsLeapYear(dateTime.AsDate.Year);
					case ValueKind.DateTime:
						return Library.Date.IsLeapYearFunctionValue.IsLeapYear(dateTime.AsDateTime.Year);
					case ValueKind.DateTimeZone:
						return Library.Date.IsLeapYearFunctionValue.IsLeapYear(dateTime.AsDateTimeZone.Year);
					case ValueKind.Number:
						return Library.Date.IsLeapYearFunctionValue.IsLeapYear(dateTime.AsInteger32);
					}
					throw ValueException.NewExpressionError<Message1>(Strings.Date_DateTimeMissingComponent("Date"), dateTime, null);
				}

				// Token: 0x060084FE RID: 34046 RVA: 0x001C54A4 File Offset: 0x001C36A4
				private static LogicalValue IsLeapYear(int year)
				{
					LogicalValue logicalValue;
					try
					{
						logicalValue = LogicalValue.New(global::System.DateTime.IsLeapYear(year));
					}
					catch (ArgumentOutOfRangeException ex)
					{
						throw ValueException.NewExpressionError(ex.Message, NumberValue.New(year), ex);
					}
					return logicalValue;
				}
			}

			// Token: 0x0200144A RID: 5194
			internal class DayOfWeekFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060084FF RID: 34047 RVA: 0x001C54E4 File Offset: 0x001C36E4
				public DayOfWeekFunctionValue(IEngineHost host)
					: base(host, null, Library.Day.Type.Nullable, 1, "dateTime", TypeValue.Any, "firstDayOfWeek", Library.Day.Type.Nullable)
				{
					this.defaultFirstDayOfWeek = base.GetCulture(Value.Null).GetCultureInfo().DateTimeFormat.FirstDayOfWeek;
				}

				// Token: 0x06008500 RID: 34048 RVA: 0x001C5540 File Offset: 0x001C3740
				public override Value TypedInvoke(Value dateTime, Value firstDayOfWeek)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					DayOfWeek firstDayOfWeek2 = Library.Date.GetFirstDayOfWeek(this.defaultFirstDayOfWeek, firstDayOfWeek);
					DayOfWeek dayOfWeek;
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						dayOfWeek = dateTime.AsDate.DayOfWeek(firstDayOfWeek2);
						break;
					case ValueKind.DateTime:
						dayOfWeek = dateTime.AsDateTime.DayOfWeek(firstDayOfWeek2);
						break;
					case ValueKind.DateTimeZone:
						dayOfWeek = dateTime.AsDateTimeZone.DayOfWeek(firstDayOfWeek2);
						break;
					default:
						throw ValueException.NewExpressionError<Message0>(Strings.Date_DateTimeValueWithDaysPartExpectedError, dateTime, null);
					}
					switch (dayOfWeek)
					{
					case DayOfWeek.Sunday:
						return Library.Day.Sunday;
					case DayOfWeek.Monday:
						return Library.Day.Monday;
					case DayOfWeek.Tuesday:
						return Library.Day.Tuesday;
					case DayOfWeek.Wednesday:
						return Library.Day.Wednesday;
					case DayOfWeek.Thursday:
						return Library.Day.Thursday;
					case DayOfWeek.Friday:
						return Library.Day.Friday;
					case DayOfWeek.Saturday:
						return Library.Day.Saturday;
					default:
						throw ValueException.NewExpressionError<Message0>(Strings.Calendar_InvalidDayOfWeekError, dateTime, null);
					}
				}

				// Token: 0x040049D9 RID: 18905
				private readonly DayOfWeek defaultFirstDayOfWeek;
			}

			// Token: 0x0200144B RID: 5195
			private class DayOfYearFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008501 RID: 34049 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public DayOfYearFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x06008502 RID: 34050 RVA: 0x001C561C File Offset: 0x001C381C
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return NumberValue.New(dateTime.AsDate.AsClrDateTime.DayOfYear);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.AsClrDateTime.DayOfYear);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.AsClrDateTimeOffset.DayOfYear);
					default:
						throw ValueException.NewExpressionError<Message0>(Strings.Date_DateTimeValueWithDaysPartExpectedError, dateTime, null);
					}
				}
			}
		}

		// Token: 0x0200144C RID: 5196
		public static class DateTime
		{
			// Token: 0x06008503 RID: 34051 RVA: 0x001C56AC File Offset: 0x001C38AC
			private static DateTimeValue ConvertFromText(TextValue text, CultureInfo cultureInfo)
			{
				DateTimeValue dateTimeValue = null;
				if (DateTimeValue.TryParseFromText(text.String, cultureInfo, out dateTimeValue))
				{
					return dateTimeValue;
				}
				throw ValueException.NewDataFormatError<Message0>(Strings.DateTime_CannotParseTextAsDateTimeError, text, null);
			}

			// Token: 0x06008504 RID: 34052 RVA: 0x001C56D9 File Offset: 0x001C38D9
			private static bool IsValidFileTime(long fileTime)
			{
				return fileTime >= 0L && fileTime <= 2650467743999999999L;
			}

			// Token: 0x06008505 RID: 34053 RVA: 0x001C56F1 File Offset: 0x001C38F1
			public static TextValue ConvertToText(Value dateTime, Value format, ICulture culture)
			{
				if (!format.IsNull)
				{
					return TextValue.New(dateTime.AsDateTime.ToString(format.AsString, culture.Value));
				}
				return TextValue.New(dateTime.AsDateTime.ToString(culture.Value));
			}

			// Token: 0x040049DA RID: 18906
			public static readonly FunctionValue datetime = new Library.DateTime.DateTimeFunctionValue();

			// Token: 0x040049DB RID: 18907
			public static readonly FunctionValue ToRecord = new Library.DateTime.ToRecordFunctionValue();

			// Token: 0x040049DC RID: 18908
			public static readonly FunctionValue Date = new Library.DateTime.DatePartFunctionValue();

			// Token: 0x040049DD RID: 18909
			public static readonly FunctionValue Time = new Library.DateTime.TimePartFunctionValue();

			// Token: 0x040049DE RID: 18910
			public static readonly FunctionValue AddZone = new Library.DateTime.AddZoneFunctionValue();

			// Token: 0x040049DF RID: 18911
			public static readonly FunctionValue FromFileTime = new Library.DateTime.FromFileTimeFunctionValue();

			// Token: 0x0200144D RID: 5197
			private class DateTimeFunctionValue : NativeFunctionValueN<Value>
			{
				// Token: 0x06008507 RID: 34055 RVA: 0x001C576C File Offset: 0x001C396C
				public DateTimeFunctionValue()
					: base(TypeValue.DateTime, new string[] { "year", "month", "day", "hour", "minute", "second" }, new TypeValue[]
					{
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number
					})
				{
				}

				// Token: 0x06008508 RID: 34056 RVA: 0x001C57F0 File Offset: 0x001C39F0
				protected override Value TypedInvokeN(Value[] args)
				{
					return DateTimeValue.New(args[0].AsInteger32, args[1].AsInteger32, args[2].AsInteger32, args[3].AsInteger32, args[4].AsInteger32, args[5].AsScientific64);
				}

				// Token: 0x06008509 RID: 34057 RVA: 0x001C5827 File Offset: 0x001C3A27
				public override string ToSource()
				{
					return "#datetime";
				}
			}

			// Token: 0x0200144E RID: 5198
			public class ToTextFunctionValue : FormatOptionsFunctionValue<TextValue, DateTimeValue, Value, Value>
			{
				// Token: 0x0600850A RID: 34058 RVA: 0x001C582E File Offset: 0x001C3A2E
				public ToTextFunctionValue(IEngineHost host)
					: base(host, ConversionDirection.ToText, "dateTime", NullableTypeValue.DateTime, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600850B RID: 34059 RVA: 0x001C584C File Offset: 0x001C3A4C
				protected override TextValue Convert(DateTimeValue dateTime, string format, ICulture culture)
				{
					return Library.DateTime.ConvertToText(dateTime, TextValue.New(format), culture);
				}

				// Token: 0x0600850C RID: 34060 RVA: 0x001C585C File Offset: 0x001C3A5C
				protected override TextValue TypedInvokeNoOptions(DateTimeValue dateTime, Value format, Value culture)
				{
					ICulture culture2 = base.GetCulture(culture);
					return Library.DateTime.ConvertToText(dateTime, format, culture2);
				}
			}

			// Token: 0x0200144F RID: 5199
			public class FromTextFunctionValue : FormatOptionsFunctionValue<DateTimeValue, TextValue, Value>
			{
				// Token: 0x0600850D RID: 34061 RVA: 0x001C5879 File Offset: 0x001C3A79
				public FromTextFunctionValue(IEngineHost host)
					: base(host, TypeValue.DateTime, ConversionDirection.FromText, "text")
				{
				}

				// Token: 0x0600850E RID: 34062 RVA: 0x001C5890 File Offset: 0x001C3A90
				protected override Value Convert(TextValue text, TextValue format, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					DateTimeValue dateTimeValue;
					if (InstantParser.TryParseDateTime(text.AsString, format.String, Culture.ResolveCulture(this.engineHost, culture).Value, out dateTimeValue))
					{
						return dateTimeValue;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.DateTime_CannotParseTextAsDateTimeError, RecordValue.New(Keys.New("Format", "Culture"), new Value[] { format, culture }), null);
				}

				// Token: 0x0600850F RID: 34063 RVA: 0x001C5900 File Offset: 0x001C3B00
				protected override Value TypedInvokeNoOptions(TextValue text, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					CultureInfo cultureInfo = Culture.GetCulture(this.engineHost, culture, Culture.GetDefaultCulture(this.engineHost)).GetCultureInfo();
					return Library.DateTime.ConvertFromText(text.AsText, cultureInfo);
				}
			}

			// Token: 0x02001450 RID: 5200
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008510 RID: 34064 RVA: 0x001C5944 File Offset: 0x001C3B44
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, NullableTypeValue.DateTime, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x17002371 RID: 9073
				// (get) Token: 0x06008511 RID: 34065 RVA: 0x001C5973 File Offset: 0x001C3B73
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.DateTimeFrom;
					}
				}

				// Token: 0x17002372 RID: 9074
				// (get) Token: 0x06008512 RID: 34066 RVA: 0x001C597A File Offset: 0x001C3B7A
				protected ITimeZone TimeZone
				{
					get
					{
						if (this.timeZone == null)
						{
							this.timeZone = base.QueryService<ITimeZoneService>().DefaultTimeZone;
						}
						return this.timeZone;
					}
				}

				// Token: 0x06008513 RID: 34067 RVA: 0x001C599C File Offset: 0x001C3B9C
				public override Value TypedInvoke(Value value, Value culture)
				{
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					switch (value.Kind)
					{
					case ValueKind.Null:
						return value;
					case ValueKind.Time:
						return DateTimeValue.New(Library.FromCommon.BaseDate.Add(value.AsTime.AsClrTimeSpan));
					case ValueKind.Date:
						return DateTimeValue.New(value.AsDate.AsClrDateTime);
					case ValueKind.DateTime:
						return value;
					case ValueKind.DateTimeZone:
						return DateTimeValue.New(value.AsDateTimeZone.AsClrDateTimeOffset.UtcDateTime.AdjustForTimeZone(this.TimeZone));
					case ValueKind.Number:
						return DateTimeValue.New(Library.FromCommon.NumberToDateTime(value, Strings.DateTime_NotConvertibleToDateTime));
					case ValueKind.Text:
						if (!Library.Text.TextValidation.IsNullOrWhiteSpace(value))
						{
							return Library.DateTime.ConvertFromText(value.AsText, cultureInfo);
						}
						return Value.Null;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.DateTime_NotConvertibleToDateTime, value, null);
				}

				// Token: 0x040049E0 RID: 18912
				private ITimeZone timeZone;
			}

			// Token: 0x02001451 RID: 5201
			public class FromFileTimeFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008514 RID: 34068 RVA: 0x001C5A80 File Offset: 0x001C3C80
				public FromFileTimeFunctionValue()
					: base(NullableTypeValue.DateTime, "fileTime", NullableTypeValue.Number)
				{
				}

				// Token: 0x06008515 RID: 34069 RVA: 0x001C5A98 File Offset: 0x001C3C98
				public override Value TypedInvoke(Value fileTime)
				{
					if (fileTime.IsNull)
					{
						return Value.Null;
					}
					long asInteger = fileTime.AsNumber.AsInteger64;
					if (Library.DateTime.IsValidFileTime(asInteger))
					{
						return Library.DateTimeZone.RemoveZone.Invoke(DateTimeZoneValue.New(DateTimeOffset.FromFileTime(asInteger)));
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.DateTime_NotConvertibleToDateTime, fileTime, null);
				}
			}

			// Token: 0x02001452 RID: 5202
			private class ToRecordFunctionValue : NativeFunctionValue1<RecordValue, DateTimeValue>
			{
				// Token: 0x06008516 RID: 34070 RVA: 0x001C5AE9 File Offset: 0x001C3CE9
				public ToRecordFunctionValue()
					: base(TypeValue.Record, 1, "dateTime", TypeValue.DateTime)
				{
				}

				// Token: 0x06008517 RID: 34071 RVA: 0x001C5B01 File Offset: 0x001C3D01
				public override RecordValue TypedInvoke(DateTimeValue dateTime)
				{
					return dateTime.ToRecord();
				}
			}

			// Token: 0x02001453 RID: 5203
			private class DatePartFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008518 RID: 34072 RVA: 0x001C5B09 File Offset: 0x001C3D09
				public DatePartFunctionValue()
					: base(NullableTypeValue.Date, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x06008519 RID: 34073 RVA: 0x001C5B20 File Offset: 0x001C3D20
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Date:
						return dateTime;
					case ValueKind.DateTime:
						return DateValue.New(dateTime.AsDateTime.AsClrDateTime);
					case ValueKind.DateTimeZone:
						return DateValue.New(dateTime.AsDateTimeZone.AsClrDateTimeOffset.DateTime);
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.DateTime_DateTimeMissingComponent("Date"), dateTime, null);
					}
				}
			}

			// Token: 0x02001454 RID: 5204
			private class TimePartFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600851A RID: 34074 RVA: 0x001C5B95 File Offset: 0x001C3D95
				public TimePartFunctionValue()
					: base(NullableTypeValue.Time, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x0600851B RID: 34075 RVA: 0x001C5BAC File Offset: 0x001C3DAC
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Time:
						return dateTime;
					case ValueKind.DateTime:
						return TimeValue.New(dateTime.AsDateTime.AsClrDateTime.TimeOfDay);
					case ValueKind.DateTimeZone:
						return TimeValue.New(dateTime.AsDateTimeZone.AsClrDateTimeOffset.TimeOfDay);
					}
					throw ValueException.NewExpressionError<Message1>(Strings.DateTime_DateTimeMissingComponent("Time"), dateTime, null);
				}
			}

			// Token: 0x02001455 RID: 5205
			private class AddZoneFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x0600851C RID: 34076 RVA: 0x001C5C30 File Offset: 0x001C3E30
				public AddZoneFunctionValue()
					: base(NullableTypeValue.DateTimeZone, 2, "dateTime", NullableTypeValue.DateTime, "timezoneHours", TypeValue.Number, "timezoneMinutes", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600851D RID: 34077 RVA: 0x001C5C68 File Offset: 0x001C3E68
				public override Value TypedInvoke(Value dateTime, NumberValue hours, Value minutes)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					int num = (minutes.IsNull ? 0 : minutes.AsInteger32);
					return dateTime.AsDateTime.SetOffset(hours.AsScientific64, num);
				}
			}

			// Token: 0x02001456 RID: 5206
			public class LocalNowFunctionValue : TimeSpecificFunctionValue0<DateTimeValue>
			{
				// Token: 0x0600851E RID: 34078 RVA: 0x001C5CA7 File Offset: 0x001C3EA7
				public LocalNowFunctionValue(IEngineHost engineHost)
					: base(engineHost, TypeValue.DateTime)
				{
				}

				// Token: 0x0600851F RID: 34079 RVA: 0x001C5CB5 File Offset: 0x001C3EB5
				public override DateTimeValue TypedInvoke()
				{
					base.EngineHost.CheckVolatileFunctionsAllowed();
					return DateTimeValue.New(base.DateTimeLocalNow);
				}

				// Token: 0x06008520 RID: 34080 RVA: 0x001C5CD0 File Offset: 0x001C3ED0
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && base.EngineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}
			}

			// Token: 0x02001457 RID: 5207
			public class FixedLocalNowFunctionValue : TimeSpecificFunctionValue0<DateTimeValue>
			{
				// Token: 0x06008521 RID: 34081 RVA: 0x001C5CA7 File Offset: 0x001C3EA7
				public FixedLocalNowFunctionValue(IEngineHost host)
					: base(host, TypeValue.DateTime)
				{
				}

				// Token: 0x06008522 RID: 34082 RVA: 0x001C5D1F File Offset: 0x001C3F1F
				public override DateTimeValue TypedInvoke()
				{
					base.EngineHost.CheckVolatileFunctionsAllowed();
					return DateTimeValue.New(base.FixedDateTimeLocalNow);
				}

				// Token: 0x06008523 RID: 34083 RVA: 0x001C5D38 File Offset: 0x001C3F38
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && base.EngineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}
			}
		}

		// Token: 0x02001458 RID: 5208
		public static class DateTimeZone
		{
			// Token: 0x06008524 RID: 34084 RVA: 0x001C5D88 File Offset: 0x001C3F88
			private static DateTimeZoneValue ConvertFromText(TextValue text, CultureInfo cultureInfo)
			{
				DateTimeZoneValue dateTimeZoneValue = null;
				if (DateTimeZoneValue.TryParseFromText(text.String, cultureInfo, out dateTimeZoneValue))
				{
					return dateTimeZoneValue;
				}
				throw ValueException.NewDataFormatError<Message0>(Strings.DateTimeZone_CannotParseTextAsDateTimeError, text, null);
			}

			// Token: 0x06008525 RID: 34085 RVA: 0x001C56D9 File Offset: 0x001C38D9
			private static bool IsValidFileTime(long fileTime)
			{
				return fileTime >= 0L && fileTime <= 2650467743999999999L;
			}

			// Token: 0x06008526 RID: 34086 RVA: 0x001C5DB5 File Offset: 0x001C3FB5
			public static TextValue ConvertToText(Value dateTimeZone, Value format, ICulture culture)
			{
				if (format.IsNull)
				{
					return TextValue.New(dateTimeZone.AsDateTimeZone.ToString(culture.Value));
				}
				return TextValue.New(dateTimeZone.AsDateTimeZone.ToString(format.AsString, culture.Value));
			}

			// Token: 0x040049E1 RID: 18913
			public static readonly FunctionValue datetimezone = new Library.DateTimeZone.DateTimeZoneFunctionValue();

			// Token: 0x040049E2 RID: 18914
			public static readonly FunctionValue ToRecord = new Library.DateTimeZone.ToRecordFunctionValue();

			// Token: 0x040049E3 RID: 18915
			public static readonly FunctionValue ZoneHours = new Library.DateTimeZone.ZoneHoursFunctionValue();

			// Token: 0x040049E4 RID: 18916
			public static readonly FunctionValue ZoneMinutes = new Library.DateTimeZone.ZoneMinutesFunctionValue();

			// Token: 0x040049E5 RID: 18917
			public static readonly FunctionValue ToUtc = new Library.DateTimeZone.ToUtcFunctionValue();

			// Token: 0x040049E6 RID: 18918
			public static readonly FunctionValue SwitchZone = new Library.DateTimeZone.SwitchZoneFunctionValue();

			// Token: 0x040049E7 RID: 18919
			public static readonly FunctionValue RemoveZone = new Library.DateTimeZone.RemoveZoneFunctionValue();

			// Token: 0x040049E8 RID: 18920
			public static readonly FunctionValue FromFileTime = new Library.DateTimeZone.FromFileTimeFunctionValue();

			// Token: 0x02001459 RID: 5209
			private class DateTimeZoneFunctionValue : NativeFunctionValueN<Value>
			{
				// Token: 0x06008528 RID: 34088 RVA: 0x001C5E54 File Offset: 0x001C4054
				public DateTimeZoneFunctionValue()
					: base(TypeValue.DateTimeZone, new string[] { "year", "month", "day", "hour", "minute", "second", "offsetHours", "offsetMinutes" }, new TypeValue[]
					{
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number,
						TypeValue.Number
					})
				{
				}

				// Token: 0x06008529 RID: 34089 RVA: 0x001C5EF8 File Offset: 0x001C40F8
				protected override Value TypedInvokeN(Value[] args)
				{
					return DateTimeZoneValue.New(args[0].AsInteger32, args[1].AsInteger32, args[2].AsInteger32, args[3].AsInteger32, args[4].AsInteger32, args[5].AsScientific64, args[6].AsScientific64, (double)args[7].AsInteger32);
				}

				// Token: 0x0600852A RID: 34090 RVA: 0x001C5F4B File Offset: 0x001C414B
				public override string ToSource()
				{
					return "#datetimezone";
				}
			}

			// Token: 0x0200145A RID: 5210
			public class ToTextFunctionValue : FormatOptionsFunctionValue<TextValue, DateTimeZoneValue, Value, Value>
			{
				// Token: 0x0600852B RID: 34091 RVA: 0x001C5F52 File Offset: 0x001C4152
				public ToTextFunctionValue(IEngineHost host)
					: base(host, ConversionDirection.ToText, "dateTimeZone", NullableTypeValue.DateTimeZone, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600852C RID: 34092 RVA: 0x001C5F70 File Offset: 0x001C4170
				protected override TextValue Convert(DateTimeZoneValue dateTimeZone, string format, ICulture culture)
				{
					return Library.DateTimeZone.ConvertToText(dateTimeZone, TextValue.New(format), culture);
				}

				// Token: 0x0600852D RID: 34093 RVA: 0x001C5F80 File Offset: 0x001C4180
				protected override TextValue TypedInvokeNoOptions(DateTimeZoneValue dateTimeZone, Value format, Value culture)
				{
					ICulture culture2 = base.GetCulture(culture);
					return Library.DateTimeZone.ConvertToText(dateTimeZone, format, culture2);
				}
			}

			// Token: 0x0200145B RID: 5211
			public class FromTextFunctionValue : FormatOptionsFunctionValue<DateTimeZoneValue, Value, TextValue>
			{
				// Token: 0x0600852E RID: 34094 RVA: 0x001C5F9D File Offset: 0x001C419D
				public FromTextFunctionValue(IEngineHost host)
					: base(host, TypeValue.DateTimeZone, ConversionDirection.FromText, "text")
				{
				}

				// Token: 0x0600852F RID: 34095 RVA: 0x001C5FB4 File Offset: 0x001C41B4
				protected override Value Convert(Value text, TextValue format, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					DateTimeZoneValue dateTimeZoneValue;
					if (InstantParser.TryParseDateTimeZone(text.AsString, format.String, Culture.ResolveCulture(this.engineHost, culture).Value, out dateTimeZoneValue))
					{
						return dateTimeZoneValue;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.DateTimeZone_CannotParseTextAsDateTimeError, RecordValue.New(Keys.New("Text", "Format", "Culture"), new Value[] { text, format, culture }), null);
				}

				// Token: 0x06008530 RID: 34096 RVA: 0x001C6030 File Offset: 0x001C4230
				protected override Value TypedInvokeNoOptions(Value text, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					CultureInfo cultureInfo = Culture.GetCulture(this.engineHost, culture, Culture.GetDefaultCulture(this.engineHost)).GetCultureInfo();
					return Library.DateTimeZone.ConvertFromText(text.AsText, cultureInfo);
				}
			}

			// Token: 0x0200145C RID: 5212
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x06008531 RID: 34097 RVA: 0x001C6074 File Offset: 0x001C4274
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, NullableTypeValue.DateTimeZone, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x17002373 RID: 9075
				// (get) Token: 0x06008532 RID: 34098 RVA: 0x001C60A3 File Offset: 0x001C42A3
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.DateTimeZoneFrom;
					}
				}

				// Token: 0x17002374 RID: 9076
				// (get) Token: 0x06008533 RID: 34099 RVA: 0x001C60AA File Offset: 0x001C42AA
				protected ITimeZone TimeZone
				{
					get
					{
						if (this.timeZone == null)
						{
							this.timeZone = base.QueryService<ITimeZoneService>().DefaultTimeZone;
						}
						return this.timeZone;
					}
				}

				// Token: 0x06008534 RID: 34100 RVA: 0x001C60CC File Offset: 0x001C42CC
				public override Value TypedInvoke(Value value, Value culture)
				{
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					switch (value.Kind)
					{
					case ValueKind.Null:
						return value;
					case ValueKind.Time:
						return this.ConvertFromDateTime(Library.FromCommon.BaseDate.Add(value.AsTime.AsClrTimeSpan), value);
					case ValueKind.Date:
						return this.ConvertFromDateTime(value.AsDate.AsClrDateTime, value);
					case ValueKind.DateTime:
						return this.ConvertFromDateTime(value.AsDateTime.AsClrDateTime, value);
					case ValueKind.DateTimeZone:
						return value;
					case ValueKind.Number:
						return this.ConvertFromDateTime(Library.FromCommon.NumberToDateTime(value, Strings.DateTimeZone_NotConvertibleToDateTimeZone), value);
					case ValueKind.Text:
						if (!Library.Text.TextValidation.IsNullOrWhiteSpace(value))
						{
							return Library.DateTimeZone.ConvertFromText(value.AsText, cultureInfo);
						}
						return Value.Null;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.DateTimeZone_NotConvertibleToDateTimeZone, value, null);
				}

				// Token: 0x06008535 RID: 34101 RVA: 0x001C61A2 File Offset: 0x001C43A2
				private DateTimeZoneValue ConvertFromDateTime(global::System.DateTime dateTime, Value originalValue)
				{
					return DateTimeZoneValue.New(dateTime.AddTimeZone(this.TimeZone, null));
				}

				// Token: 0x040049E9 RID: 18921
				private ITimeZone timeZone;
			}

			// Token: 0x0200145D RID: 5213
			public class FromFileTimeFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008536 RID: 34102 RVA: 0x001C61B6 File Offset: 0x001C43B6
				public FromFileTimeFunctionValue()
					: base(NullableTypeValue.DateTimeZone, "fileTime", NullableTypeValue.Number)
				{
				}

				// Token: 0x06008537 RID: 34103 RVA: 0x001C61D0 File Offset: 0x001C43D0
				public override Value TypedInvoke(Value fileTime)
				{
					if (fileTime.IsNull)
					{
						return Value.Null;
					}
					long asInteger = fileTime.AsNumber.AsInteger64;
					if (Library.DateTimeZone.IsValidFileTime(asInteger))
					{
						return DateTimeZoneValue.New(DateTimeOffset.FromFileTime(asInteger));
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.DateTimeZone_NotConvertibleToDateTimeZone, fileTime, null);
				}
			}

			// Token: 0x0200145E RID: 5214
			private class ToRecordFunctionValue : NativeFunctionValue1<RecordValue, DateTimeZoneValue>
			{
				// Token: 0x06008538 RID: 34104 RVA: 0x001C6217 File Offset: 0x001C4417
				public ToRecordFunctionValue()
					: base(TypeValue.Record, 1, "dateTimeZone", TypeValue.DateTimeZone)
				{
				}

				// Token: 0x06008539 RID: 34105 RVA: 0x001C622F File Offset: 0x001C442F
				public override RecordValue TypedInvoke(DateTimeZoneValue dateTimeZone)
				{
					return dateTimeZone.ToRecord();
				}
			}

			// Token: 0x0200145F RID: 5215
			private class ZoneHoursFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600853A RID: 34106 RVA: 0x001C6237 File Offset: 0x001C4437
				public ZoneHoursFunctionValue()
					: base(NullableTypeValue.Int32, "dateTimeZone", NullableTypeValue.DateTimeZone)
				{
				}

				// Token: 0x0600853B RID: 34107 RVA: 0x001C624E File Offset: 0x001C444E
				public override Value TypedInvoke(Value dateTimeZone)
				{
					if (dateTimeZone.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(dateTimeZone.AsDateTimeZone.OffsetHours).AsNumber;
				}
			}

			// Token: 0x02001460 RID: 5216
			private class ZoneMinutesFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600853C RID: 34108 RVA: 0x001C6237 File Offset: 0x001C4437
				public ZoneMinutesFunctionValue()
					: base(NullableTypeValue.Int32, "dateTimeZone", NullableTypeValue.DateTimeZone)
				{
				}

				// Token: 0x0600853D RID: 34109 RVA: 0x001C6273 File Offset: 0x001C4473
				public override Value TypedInvoke(Value dateTimeZone)
				{
					if (dateTimeZone.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(dateTimeZone.AsDateTimeZone.OffsetMinutes).AsNumber;
				}
			}

			// Token: 0x02001461 RID: 5217
			public class LocalNowFunctionValue : TimeSpecificFunctionValue0<DateTimeZoneValue>
			{
				// Token: 0x0600853E RID: 34110 RVA: 0x001C6298 File Offset: 0x001C4498
				public LocalNowFunctionValue(IEngineHost engineHost)
					: base(engineHost, TypeValue.DateTimeZone)
				{
				}

				// Token: 0x0600853F RID: 34111 RVA: 0x001C62A6 File Offset: 0x001C44A6
				public override DateTimeZoneValue TypedInvoke()
				{
					base.EngineHost.CheckVolatileFunctionsAllowed();
					return DateTimeZoneValue.New(base.DateTimeOffsetLocalNow);
				}

				// Token: 0x06008540 RID: 34112 RVA: 0x001C62C0 File Offset: 0x001C44C0
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && base.EngineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}
			}

			// Token: 0x02001462 RID: 5218
			public class UtcNowFunctionValue : TimeSpecificFunctionValue0<DateTimeZoneValue>
			{
				// Token: 0x06008541 RID: 34113 RVA: 0x001C6298 File Offset: 0x001C4498
				public UtcNowFunctionValue(IEngineHost engineHost)
					: base(engineHost, TypeValue.DateTimeZone)
				{
				}

				// Token: 0x06008542 RID: 34114 RVA: 0x001C630F File Offset: 0x001C450F
				public override DateTimeZoneValue TypedInvoke()
				{
					base.EngineHost.CheckVolatileFunctionsAllowed();
					return DateTimeZoneValue.New(base.DateTimeOffsetUtcNow);
				}

				// Token: 0x06008543 RID: 34115 RVA: 0x001C6328 File Offset: 0x001C4528
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && base.EngineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}
			}

			// Token: 0x02001463 RID: 5219
			public class ToLocalFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008544 RID: 34116 RVA: 0x001C6377 File Offset: 0x001C4577
				public ToLocalFunctionValue(IEngineHost engineHost)
					: base(NullableTypeValue.DateTimeZone, "dateTimeZone", NullableTypeValue.DateTimeZone)
				{
					this.timeZone = engineHost.QueryService<ITimeZoneService>().DefaultTimeZone;
				}

				// Token: 0x17002375 RID: 9077
				// (get) Token: 0x06008545 RID: 34117 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x06008546 RID: 34118 RVA: 0x001C63A0 File Offset: 0x001C45A0
				public override Value TypedInvoke(Value dateTimeZone)
				{
					if (dateTimeZone.IsNull)
					{
						return Value.Null;
					}
					return DateTimeZoneValue.New(dateTimeZone.AsDateTimeZone.AsClrDateTimeOffset.LocalDateTime.AddTimeZone(this.timeZone, null));
				}

				// Token: 0x040049EA RID: 18922
				private readonly ITimeZone timeZone;
			}

			// Token: 0x02001464 RID: 5220
			private class ToUtcFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008547 RID: 34119 RVA: 0x001C63DF File Offset: 0x001C45DF
				public ToUtcFunctionValue()
					: base(NullableTypeValue.DateTimeZone, "dateTimeZone", NullableTypeValue.DateTimeZone)
				{
				}

				// Token: 0x06008548 RID: 34120 RVA: 0x001C63F8 File Offset: 0x001C45F8
				public override Value TypedInvoke(Value dateTimeZone)
				{
					if (dateTimeZone.IsNull)
					{
						return Value.Null;
					}
					return DateTimeZoneValue.New(dateTimeZone.AsDateTimeZone.AsClrDateTimeOffset.UtcDateTime);
				}
			}

			// Token: 0x02001465 RID: 5221
			private class SwitchZoneFunctionValue : NativeFunctionValue3<Value, Value, NumberValue, Value>
			{
				// Token: 0x06008549 RID: 34121 RVA: 0x001C6430 File Offset: 0x001C4630
				public SwitchZoneFunctionValue()
					: base(NullableTypeValue.DateTimeZone, 2, "dateTimeZone", NullableTypeValue.DateTimeZone, "timezoneHours", TypeValue.Number, "timezoneMinutes", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600854A RID: 34122 RVA: 0x001C6468 File Offset: 0x001C4668
				public override Value TypedInvoke(Value dateTimeZone, NumberValue hours, Value minutes)
				{
					if (dateTimeZone.IsNull)
					{
						return Value.Null;
					}
					int num = (minutes.IsNull ? 0 : minutes.AsInteger32);
					return dateTimeZone.AsDateTimeZone.SwitchOffset(hours.AsScientific64, num);
				}
			}

			// Token: 0x02001466 RID: 5222
			private class RemoveZoneFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600854B RID: 34123 RVA: 0x001C64A7 File Offset: 0x001C46A7
				public RemoveZoneFunctionValue()
					: base(NullableTypeValue.DateTime, "dateTimeZone", NullableTypeValue.DateTimeZone)
				{
				}

				// Token: 0x0600854C RID: 34124 RVA: 0x001C64C0 File Offset: 0x001C46C0
				public override Value TypedInvoke(Value dateTimeZone)
				{
					if (dateTimeZone.IsNull)
					{
						return Value.Null;
					}
					return DateTimeValue.New(dateTimeZone.AsDateTimeZone.AsClrDateTimeOffset.DateTime);
				}
			}

			// Token: 0x02001467 RID: 5223
			public class FixedLocalNowFunctionValue : TimeSpecificFunctionValue0<DateTimeZoneValue>
			{
				// Token: 0x0600854D RID: 34125 RVA: 0x001C6298 File Offset: 0x001C4498
				public FixedLocalNowFunctionValue(IEngineHost host)
					: base(host, TypeValue.DateTimeZone)
				{
				}

				// Token: 0x0600854E RID: 34126 RVA: 0x001C64F3 File Offset: 0x001C46F3
				public override DateTimeZoneValue TypedInvoke()
				{
					base.EngineHost.CheckVolatileFunctionsAllowed();
					return DateTimeZoneValue.New(base.FixedDateTimeOffsetLocalNow);
				}

				// Token: 0x0600854F RID: 34127 RVA: 0x001C650C File Offset: 0x001C470C
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && base.EngineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}
			}

			// Token: 0x02001468 RID: 5224
			public class FixedUtcNowFunctionValue : TimeSpecificFunctionValue0<DateTimeZoneValue>
			{
				// Token: 0x06008550 RID: 34128 RVA: 0x001C6298 File Offset: 0x001C4498
				public FixedUtcNowFunctionValue(IEngineHost host)
					: base(host, TypeValue.DateTimeZone)
				{
				}

				// Token: 0x06008551 RID: 34129 RVA: 0x001C655B File Offset: 0x001C475B
				public override DateTimeZoneValue TypedInvoke()
				{
					base.EngineHost.CheckVolatileFunctionsAllowed();
					return DateTimeZoneValue.New(base.FixedDateTimeOffsetUtcNow);
				}

				// Token: 0x06008552 RID: 34130 RVA: 0x001C6574 File Offset: 0x001C4774
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && base.EngineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}
			}
		}

		// Token: 0x02001469 RID: 5225
		public static class Time
		{
			// Token: 0x06008553 RID: 34131 RVA: 0x001C65C4 File Offset: 0x001C47C4
			private static TimeValue ConvertFromText(TextValue text, CultureInfo cultureInfo)
			{
				TimeValue timeValue = null;
				if (TimeValue.TryParseFromText(text.String, cultureInfo, out timeValue))
				{
					return timeValue;
				}
				throw ValueException.NewExpressionError<Message0>(Strings.Time_CannotParseTextAsDateTimeError, text, null);
			}

			// Token: 0x06008554 RID: 34132 RVA: 0x001C65F1 File Offset: 0x001C47F1
			public static TextValue ConvertToText(TimeValue time, Value format, ICulture culture)
			{
				if (format.IsNull)
				{
					return TextValue.New(time.ToString(culture));
				}
				return TextValue.New(time.ToString(format.AsString, culture.Value));
			}

			// Token: 0x040049EB RID: 18923
			public static readonly FunctionValue time = new Library.Time.TimeFunctionValue();

			// Token: 0x040049EC RID: 18924
			public static readonly FunctionValue ToRecord = new Library.Time.ToRecordFunctionValue();

			// Token: 0x040049ED RID: 18925
			public static readonly FunctionValue Hour = new Library.Time.HourFunctionValue();

			// Token: 0x040049EE RID: 18926
			public static readonly FunctionValue Minute = new Library.Time.MinuteFunctionValue();

			// Token: 0x040049EF RID: 18927
			public static readonly FunctionValue Second = new Library.Time.SecondFunctionValue();

			// Token: 0x040049F0 RID: 18928
			public static readonly FunctionValue StartOfHour = new Library.Time.StartOfHourFunctionValue();

			// Token: 0x040049F1 RID: 18929
			public static readonly FunctionValue EndOfHour = new Library.Time.EndOfHourFunctionValue();

			// Token: 0x0200146A RID: 5226
			private class TimeFunctionValue : NativeFunctionValue3<TimeValue, NumberValue, NumberValue, NumberValue>
			{
				// Token: 0x06008556 RID: 34134 RVA: 0x001C6673 File Offset: 0x001C4873
				public TimeFunctionValue()
					: base(TypeValue.Time, "hour", TypeValue.Number, "minute", TypeValue.Number, "second", TypeValue.Number)
				{
				}

				// Token: 0x06008557 RID: 34135 RVA: 0x001C669E File Offset: 0x001C489E
				public override TimeValue TypedInvoke(NumberValue hour, NumberValue minute, NumberValue second)
				{
					return TimeValue.New(hour.AsInteger32, minute.AsInteger32, second.AsScientific64);
				}

				// Token: 0x06008558 RID: 34136 RVA: 0x001C66B7 File Offset: 0x001C48B7
				public override string ToSource()
				{
					return "#time";
				}
			}

			// Token: 0x0200146B RID: 5227
			public class ToTextFunctionValue : FormatOptionsFunctionValue<TextValue, TimeValue, Value, Value>
			{
				// Token: 0x06008559 RID: 34137 RVA: 0x001C66BE File Offset: 0x001C48BE
				public ToTextFunctionValue(IEngineHost host)
					: base(host, ConversionDirection.ToText, "time", NullableTypeValue.Time, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600855A RID: 34138 RVA: 0x001C66DC File Offset: 0x001C48DC
				protected override TextValue Convert(TimeValue time, string format, ICulture culture)
				{
					return Library.Time.ConvertToText(time.AsTime, TextValue.New(format), culture);
				}

				// Token: 0x0600855B RID: 34139 RVA: 0x001C66F0 File Offset: 0x001C48F0
				protected override TextValue TypedInvokeNoOptions(TimeValue time, Value format, Value culture)
				{
					ICulture culture2 = base.GetCulture(culture);
					return Library.Time.ConvertToText(time, format, culture2);
				}
			}

			// Token: 0x0200146C RID: 5228
			public class FromTextFunctionValue : FormatOptionsFunctionValue<TimeValue, TextValue, Value>
			{
				// Token: 0x0600855C RID: 34140 RVA: 0x001C670D File Offset: 0x001C490D
				public FromTextFunctionValue(IEngineHost host)
					: base(host, TypeValue.Time, ConversionDirection.FromText, "text")
				{
				}

				// Token: 0x0600855D RID: 34141 RVA: 0x001C6724 File Offset: 0x001C4924
				protected override Value Convert(TextValue text, TextValue format, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					TimeValue timeValue;
					if (InstantParser.TryParseTime(text.AsString, Culture.ResolveCulture(this.engineHost, culture).Value, out timeValue))
					{
						return timeValue;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Time_CannotParseTextAsDateTimeError, RecordValue.New(Keys.New("Input", "Format", "Culture"), new Value[] { text, format, culture }), null);
				}

				// Token: 0x0600855E RID: 34142 RVA: 0x001C6798 File Offset: 0x001C4998
				protected override Value TypedInvokeNoOptions(TextValue text, Value culture)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					CultureInfo cultureInfo = Culture.GetCulture(this.engineHost, culture, Culture.GetDefaultCulture(this.engineHost)).GetCultureInfo();
					return Library.Time.ConvertFromText(text.AsText, cultureInfo);
				}
			}

			// Token: 0x0200146D RID: 5229
			public class FromFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600855F RID: 34143 RVA: 0x001C67DC File Offset: 0x001C49DC
				public FromFunctionValue(IEngineHost engineHost, ICulture boundCulture = null)
					: base(engineHost, boundCulture, NullableTypeValue.Time, 1, "value", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x17002376 RID: 9078
				// (get) Token: 0x06008560 RID: 34144 RVA: 0x001C680B File Offset: 0x001C4A0B
				protected override FunctionValue CultureNeutralFunction
				{
					get
					{
						return CultureSpecificFunction.TimeFrom;
					}
				}

				// Token: 0x17002377 RID: 9079
				// (get) Token: 0x06008561 RID: 34145 RVA: 0x001C6812 File Offset: 0x001C4A12
				protected ITimeZone TimeZone
				{
					get
					{
						if (this.timeZone == null)
						{
							this.timeZone = base.QueryService<ITimeZoneService>().DefaultTimeZone;
						}
						return this.timeZone;
					}
				}

				// Token: 0x06008562 RID: 34146 RVA: 0x001C6834 File Offset: 0x001C4A34
				public override Value TypedInvoke(Value value, Value culture)
				{
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					switch (value.Kind)
					{
					case ValueKind.Null:
						return value;
					case ValueKind.Time:
						return value;
					case ValueKind.DateTime:
						return TimeValue.New(value.AsDateTime.AsClrDateTime.TimeOfDay);
					case ValueKind.DateTimeZone:
						return TimeValue.New(value.AsDateTimeZone.AsClrDateTimeOffset.UtcDateTime.AdjustForTimeZone(this.TimeZone).TimeOfDay);
					case ValueKind.Number:
					{
						double asDouble = value.AsNumber.AsDouble;
						if (asDouble >= 1.0 || asDouble < 0.0)
						{
							throw ValueException.NewDataFormatError<Message0>(Strings.Time_NotConvertibleToTime, value, null);
						}
						return TimeValue.New((long)Math.Round(value.AsNumber.AsDouble * 864000000000.0));
					}
					case ValueKind.Text:
						if (!Library.Text.TextValidation.IsNullOrWhiteSpace(value))
						{
							return Library.Time.ConvertFromText(value.AsText, cultureInfo);
						}
						return Value.Null;
					}
					throw ValueException.NewDataFormatError<Message0>(Strings.Time_NotConvertibleToTime, value, null);
				}

				// Token: 0x040049F2 RID: 18930
				private ITimeZone timeZone;
			}

			// Token: 0x0200146E RID: 5230
			private class ToRecordFunctionValue : NativeFunctionValue1<RecordValue, TimeValue>
			{
				// Token: 0x06008563 RID: 34147 RVA: 0x001C6947 File Offset: 0x001C4B47
				public ToRecordFunctionValue()
					: base(TypeValue.Record, 1, "time", TypeValue.Time)
				{
				}

				// Token: 0x06008564 RID: 34148 RVA: 0x001C695F File Offset: 0x001C4B5F
				public override RecordValue TypedInvoke(TimeValue time)
				{
					return time.ToRecord();
				}
			}

			// Token: 0x0200146F RID: 5231
			private class HourFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008565 RID: 34149 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public HourFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x06008566 RID: 34150 RVA: 0x001C6968 File Offset: 0x001C4B68
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Time:
						return NumberValue.New(dateTime.AsTime.Hour);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.Hour);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.Hour);
					}
					throw ValueException.NewExpressionError<Message1>(Strings.Time_DateTimeMissingComponent("Time"), dateTime, null);
				}
			}

			// Token: 0x02001470 RID: 5232
			private class MinuteFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008567 RID: 34151 RVA: 0x001C49EB File Offset: 0x001C2BEB
				public MinuteFunctionValue()
					: base(NullableTypeValue.Int32, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x06008568 RID: 34152 RVA: 0x001C69E8 File Offset: 0x001C4BE8
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Time:
						return NumberValue.New(dateTime.AsTime.Minute);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.Minute);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.Minute);
					}
					throw ValueException.NewExpressionError<Message1>(Strings.Time_DateTimeMissingComponent("Time"), dateTime, null);
				}
			}

			// Token: 0x02001471 RID: 5233
			private class SecondFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008569 RID: 34153 RVA: 0x001C6A68 File Offset: 0x001C4C68
				public SecondFunctionValue()
					: base(NullableTypeValue.Number, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x0600856A RID: 34154 RVA: 0x001C6A80 File Offset: 0x001C4C80
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Time:
						return NumberValue.New(dateTime.AsTime.Second);
					case ValueKind.DateTime:
						return NumberValue.New(dateTime.AsDateTime.Second);
					case ValueKind.DateTimeZone:
						return NumberValue.New(dateTime.AsDateTimeZone.Second);
					}
					throw ValueException.NewExpressionError<Message1>(Strings.Time_DateTimeMissingComponent("Time"), dateTime, null);
				}
			}

			// Token: 0x02001472 RID: 5234
			private class StartOfHourFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600856B RID: 34155 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public StartOfHourFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x0600856C RID: 34156 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x0600856D RID: 34157 RVA: 0x001C6B00 File Offset: 0x001C4D00
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Time:
						return dateTime.AsTime.StartOfHour();
					case ValueKind.Date:
						return dateTime;
					case ValueKind.DateTime:
						return dateTime.AsDateTime.StartOfHour();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.StartOfHour();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Time_DateTimeMissingComponent("Time"), dateTime, null);
					}
				}
			}

			// Token: 0x02001473 RID: 5235
			private class EndOfHourFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600856E RID: 34158 RVA: 0x001C4B78 File Offset: 0x001C2D78
				public EndOfHourFunctionValue()
					: base(TypeValue.Any, "dateTime", TypeValue.Any)
				{
				}

				// Token: 0x0600856F RID: 34159 RVA: 0x001C46DB File Offset: 0x001C28DB
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return environment.GetType(arguments[0]);
				}

				// Token: 0x06008570 RID: 34160 RVA: 0x001C6B74 File Offset: 0x001C4D74
				public override Value TypedInvoke(Value dateTime)
				{
					if (dateTime.IsNull)
					{
						return Value.Null;
					}
					switch (dateTime.Kind)
					{
					case ValueKind.Time:
						return dateTime.AsTime.EndOfHour();
					case ValueKind.Date:
						return dateTime;
					case ValueKind.DateTime:
						return dateTime.AsDateTime.EndOfHour();
					case ValueKind.DateTimeZone:
						return dateTime.AsDateTimeZone.EndOfHour();
					default:
						throw ValueException.NewExpressionError<Message1>(Strings.Time_DateTimeMissingComponent("Time"), dateTime, null);
					}
				}
			}
		}

		// Token: 0x02001474 RID: 5236
		public static class Duration
		{
			// Token: 0x040049F3 RID: 18931
			public static readonly FunctionValue From = new Library.Duration.FromFunctionValue();

			// Token: 0x040049F4 RID: 18932
			public static readonly FunctionValue FromText = new Library.Duration.FromTextFunctionValue();

			// Token: 0x040049F5 RID: 18933
			public static readonly FunctionValue ToText = new Library.Duration.ToTextFunctionValue();

			// Token: 0x040049F6 RID: 18934
			public static readonly FunctionValue ToRecord = new Library.Duration.ToRecordFunctionValue();

			// Token: 0x040049F7 RID: 18935
			public static readonly FunctionValue Days = new Library.Duration.DaysFunctionValue();

			// Token: 0x040049F8 RID: 18936
			public static readonly FunctionValue Hours = new Library.Duration.HoursFunctionValue();

			// Token: 0x040049F9 RID: 18937
			public static readonly FunctionValue Minutes = new Library.Duration.MinutesFunctionValue();

			// Token: 0x040049FA RID: 18938
			public static readonly FunctionValue Seconds = new Library.Duration.SecondsFunctionValue();

			// Token: 0x040049FB RID: 18939
			public static readonly FunctionValue TotalDays = new Library.Duration.TotalDaysFunctionValue();

			// Token: 0x040049FC RID: 18940
			public static readonly FunctionValue TotalHours = new Library.Duration.TotalHoursFunctionValue();

			// Token: 0x040049FD RID: 18941
			public static readonly FunctionValue TotalMinutes = new Library.Duration.TotalMinutesFunctionValue();

			// Token: 0x040049FE RID: 18942
			public static readonly FunctionValue TotalSeconds = new Library.Duration.TotalSecondsFunctionValue();

			// Token: 0x040049FF RID: 18943
			public static readonly FunctionValue duration = new Library.Duration.DurationFunctionValue();

			// Token: 0x02001475 RID: 5237
			private class DurationFunctionValue : NativeFunctionValue4<DurationValue, NumberValue, NumberValue, NumberValue, NumberValue>
			{
				// Token: 0x06008572 RID: 34162 RVA: 0x001C6C78 File Offset: 0x001C4E78
				public DurationFunctionValue()
					: base(TypeValue.Duration, "days", TypeValue.Number, "hours", TypeValue.Number, "minutes", TypeValue.Number, "seconds", TypeValue.Number)
				{
				}

				// Token: 0x06008573 RID: 34163 RVA: 0x001C6CB8 File Offset: 0x001C4EB8
				public override DurationValue TypedInvoke(NumberValue days, NumberValue hours, NumberValue minutes, NumberValue seconds)
				{
					return DurationValue.New(days.AsInteger32, hours.AsInteger32, minutes.AsInteger32, seconds.AsScientific64);
				}

				// Token: 0x06008574 RID: 34164 RVA: 0x001C6CD8 File Offset: 0x001C4ED8
				public override string ToSource()
				{
					return "#duration";
				}
			}

			// Token: 0x02001476 RID: 5238
			private class DaysFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008575 RID: 34165 RVA: 0x001C6CDF File Offset: 0x001C4EDF
				public DaysFunctionValue()
					: base(NullableTypeValue.Int32, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x06008576 RID: 34166 RVA: 0x001C6CF6 File Offset: 0x001C4EF6
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.Days).AsNumber;
				}
			}

			// Token: 0x02001477 RID: 5239
			private class HoursFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008577 RID: 34167 RVA: 0x001C6CDF File Offset: 0x001C4EDF
				public HoursFunctionValue()
					: base(NullableTypeValue.Int32, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x06008578 RID: 34168 RVA: 0x001C6D1B File Offset: 0x001C4F1B
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.Hours).AsNumber;
				}
			}

			// Token: 0x02001478 RID: 5240
			private class MinutesFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008579 RID: 34169 RVA: 0x001C6CDF File Offset: 0x001C4EDF
				public MinutesFunctionValue()
					: base(NullableTypeValue.Int32, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x0600857A RID: 34170 RVA: 0x001C6D40 File Offset: 0x001C4F40
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.Minutes).AsNumber;
				}
			}

			// Token: 0x02001479 RID: 5241
			private class SecondsFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600857B RID: 34171 RVA: 0x001C6D65 File Offset: 0x001C4F65
				public SecondsFunctionValue()
					: base(NullableTypeValue.Number, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x0600857C RID: 34172 RVA: 0x001C6D7C File Offset: 0x001C4F7C
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.Seconds).AsNumber;
				}
			}

			// Token: 0x0200147A RID: 5242
			private class ToTextFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x0600857D RID: 34173 RVA: 0x001C6DA1 File Offset: 0x001C4FA1
				public ToTextFunctionValue()
					: base(NullableTypeValue.Text, 1, "duration", NullableTypeValue.Duration, "format", NullableTypeValue.Text)
				{
				}

				// Token: 0x0600857E RID: 34174 RVA: 0x001C6DC3 File Offset: 0x001C4FC3
				public override Value TypedInvoke(Value duration, Value format)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					if (!format.IsNull)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.DurationToTextFormatUnsupported, null, null);
					}
					return TextValue.New(duration.AsDuration.ToString());
				}
			}

			// Token: 0x0200147B RID: 5243
			public class FromTextFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600857F RID: 34175 RVA: 0x001C6DF8 File Offset: 0x001C4FF8
				public FromTextFunctionValue()
					: base(NullableTypeValue.Duration, "text", NullableTypeValue.Text)
				{
				}

				// Token: 0x06008580 RID: 34176 RVA: 0x001C6E0F File Offset: 0x001C500F
				public override Value TypedInvoke(Value text)
				{
					if (Library.Text.TextValidation.IsNullOrWhiteSpace(text))
					{
						return Value.Null;
					}
					return DurationValue.ParseLiteral(text);
				}
			}

			// Token: 0x0200147C RID: 5244
			private class FromFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008581 RID: 34177 RVA: 0x001C6E25 File Offset: 0x001C5025
				public FromFunctionValue()
					: base(NullableTypeValue.Duration, 1, "value", TypeValue.Any)
				{
				}

				// Token: 0x06008582 RID: 34178 RVA: 0x001C6E40 File Offset: 0x001C5040
				public override Value TypedInvoke(Value value)
				{
					ValueKind kind = value.Kind;
					checked
					{
						if (kind != ValueKind.Null)
						{
							switch (kind)
							{
							case ValueKind.Duration:
								return value;
							case ValueKind.Number:
							{
								long num;
								try
								{
									num = (long)Math.Round(unchecked(value.AsNumber.AsDouble * 864000000000.0));
								}
								catch (OverflowException ex)
								{
									throw ValueException.NewDataFormatError<Message0>(Strings.Duration_NotConvertibleToDuration, value, ex);
								}
								return DurationValue.New(num);
							}
							case ValueKind.Text:
								return Library.Duration.FromText.Invoke(value);
							}
							throw ValueException.NewDataFormatError<Message0>(Strings.Duration_NotConvertibleToDuration, value, null);
						}
						return value;
					}
				}
			}

			// Token: 0x0200147D RID: 5245
			private class ToRecordFunctionValue : NativeFunctionValue1<RecordValue, DurationValue>
			{
				// Token: 0x06008583 RID: 34179 RVA: 0x001C6ED4 File Offset: 0x001C50D4
				public ToRecordFunctionValue()
					: base(TypeValue.Record, "duration", TypeValue.Duration)
				{
				}

				// Token: 0x06008584 RID: 34180 RVA: 0x001C6EEB File Offset: 0x001C50EB
				public override RecordValue TypedInvoke(DurationValue duration)
				{
					return DurationValue.ToRecord(duration);
				}
			}

			// Token: 0x0200147E RID: 5246
			private class TotalDaysFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008585 RID: 34181 RVA: 0x001C6D65 File Offset: 0x001C4F65
				public TotalDaysFunctionValue()
					: base(NullableTypeValue.Number, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x06008586 RID: 34182 RVA: 0x001C6EF4 File Offset: 0x001C50F4
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.AsTimeSpan.TotalDays);
				}
			}

			// Token: 0x0200147F RID: 5247
			private class TotalHoursFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008587 RID: 34183 RVA: 0x001C6D65 File Offset: 0x001C4F65
				public TotalHoursFunctionValue()
					: base(NullableTypeValue.Number, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x06008588 RID: 34184 RVA: 0x001C6F28 File Offset: 0x001C5128
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.AsTimeSpan.TotalHours);
				}
			}

			// Token: 0x02001480 RID: 5248
			private class TotalMinutesFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06008589 RID: 34185 RVA: 0x001C6D65 File Offset: 0x001C4F65
				public TotalMinutesFunctionValue()
					: base(NullableTypeValue.Number, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x0600858A RID: 34186 RVA: 0x001C6F5C File Offset: 0x001C515C
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.AsTimeSpan.TotalMinutes);
				}
			}

			// Token: 0x02001481 RID: 5249
			private class TotalSecondsFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x0600858B RID: 34187 RVA: 0x001C6D65 File Offset: 0x001C4F65
				public TotalSecondsFunctionValue()
					: base(NullableTypeValue.Number, "duration", NullableTypeValue.Duration)
				{
				}

				// Token: 0x0600858C RID: 34188 RVA: 0x001C6F90 File Offset: 0x001C5190
				public override Value TypedInvoke(Value duration)
				{
					if (duration.IsNull)
					{
						return Value.Null;
					}
					return NumberValue.New(duration.AsDuration.AsTimeSpan.TotalSeconds);
				}
			}
		}

		// Token: 0x02001482 RID: 5250
		public static class Function
		{
			// Token: 0x04004A00 RID: 18944
			public static readonly FunctionValue From = new Library.Function.FromFunctionValue();

			// Token: 0x04004A01 RID: 18945
			public static readonly FunctionValue Invoke = new Library.Function.InvokeFunctionValue();

			// Token: 0x04004A02 RID: 18946
			public static readonly FunctionValue InvokeAfter = new Library.Function.InvokeAfterFunctionValue();

			// Token: 0x04004A03 RID: 18947
			public static readonly FunctionValue IsDataSource = new Library.Function.IsDataSourceFunctionValue();

			// Token: 0x04004A04 RID: 18948
			public static readonly FunctionValue ScalarVector = new Library.Function.ScalarVectorFunctionValue();

			// Token: 0x04004A05 RID: 18949
			public static readonly FunctionValue PreserveTableLineage = new Library.Function.PreserveTableLineageFunctionValue();

			// Token: 0x02001483 RID: 5251
			private class FromFunctionValue : NativeFunctionValue2<FunctionValue, TypeValue, FunctionValue>
			{
				// Token: 0x0600858E RID: 34190 RVA: 0x001C7001 File Offset: 0x001C5201
				public FromFunctionValue()
					: base(TypeValue.Function, "functionType", TypeValue._Type, "function", TypeValue.Function)
				{
				}

				// Token: 0x0600858F RID: 34191 RVA: 0x001C7022 File Offset: 0x001C5222
				public override FunctionValue TypedInvoke(TypeValue functionType, FunctionValue function)
				{
					return new Library.Function.FromFunctionValue.NonVariadicFunctionValue(functionType.AsFunctionType, function);
				}

				// Token: 0x02001484 RID: 5252
				private sealed class NonVariadicFunctionValue : NativeFunctionValue
				{
					// Token: 0x06008590 RID: 34192 RVA: 0x001C7030 File Offset: 0x001C5230
					public NonVariadicFunctionValue(FunctionTypeValue type, FunctionValue function)
					{
						this.type = type;
						this.function = function;
					}

					// Token: 0x17002378 RID: 9080
					// (get) Token: 0x06008591 RID: 34193 RVA: 0x001C7046 File Offset: 0x001C5246
					public override TypeValue Type
					{
						get
						{
							return this.type;
						}
					}

					// Token: 0x06008592 RID: 34194 RVA: 0x001C704E File Offset: 0x001C524E
					public override Value Invoke()
					{
						return this.function.Invoke(ListValue.Empty);
					}

					// Token: 0x06008593 RID: 34195 RVA: 0x001C7060 File Offset: 0x001C5260
					public override Value Invoke(Value arg0)
					{
						return this.function.Invoke(ListValue.New(new Value[] { arg0 }));
					}

					// Token: 0x06008594 RID: 34196 RVA: 0x001C707C File Offset: 0x001C527C
					public override Value Invoke(Value arg0, Value arg1)
					{
						return this.function.Invoke(ListValue.New(new Value[] { arg0, arg1 }));
					}

					// Token: 0x06008595 RID: 34197 RVA: 0x001C709C File Offset: 0x001C529C
					public override Value Invoke(Value arg0, Value arg1, Value arg2)
					{
						return this.function.Invoke(ListValue.New(new Value[] { arg0, arg1, arg2 }));
					}

					// Token: 0x06008596 RID: 34198 RVA: 0x001C70C0 File Offset: 0x001C52C0
					public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
					{
						return this.function.Invoke(ListValue.New(new Value[] { arg0, arg1, arg2, arg3 }));
					}

					// Token: 0x06008597 RID: 34199 RVA: 0x001C70E9 File Offset: 0x001C52E9
					public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
					{
						return this.function.Invoke(ListValue.New(new Value[] { arg0, arg1, arg2, arg3, arg4 }));
					}

					// Token: 0x06008598 RID: 34200 RVA: 0x001C7117 File Offset: 0x001C5317
					public override Value Invoke(params Value[] args)
					{
						return this.function.Invoke(ListValue.New(args));
					}

					// Token: 0x04004A06 RID: 18950
					private readonly FunctionTypeValue type;

					// Token: 0x04004A07 RID: 18951
					private readonly FunctionValue function;
				}
			}

			// Token: 0x02001485 RID: 5253
			private class InvokeFunctionValue : NativeFunctionValue2<Value, FunctionValue, ListValue>
			{
				// Token: 0x06008599 RID: 34201 RVA: 0x001C712A File Offset: 0x001C532A
				public InvokeFunctionValue()
					: base(TypeValue.Any, "function", TypeValue.Function, "args", TypeValue.List)
				{
				}

				// Token: 0x0600859A RID: 34202 RVA: 0x001C714B File Offset: 0x001C534B
				public override Value TypedInvoke(FunctionValue function, ListValue args)
				{
					return function.Invoke(args.ToArray());
				}
			}

			// Token: 0x02001486 RID: 5254
			private class InvokeAfterFunctionValue : NativeFunctionValue2<Value, FunctionValue, DurationValue>
			{
				// Token: 0x0600859B RID: 34203 RVA: 0x001C7159 File Offset: 0x001C5359
				public InvokeAfterFunctionValue()
					: base(TypeValue.Any, "function", TypeValue.Function, "delay", TypeValue.Duration)
				{
				}

				// Token: 0x0600859C RID: 34204 RVA: 0x001C717C File Offset: 0x001C537C
				public override Value TypedInvoke(FunctionValue function, DurationValue delay)
				{
					try
					{
						using (EngineContext.Leave())
						{
							Thread.Sleep(delay.AsTimeSpan);
						}
					}
					catch (ArgumentOutOfRangeException)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Duration_OutOfRangeError, delay, null);
					}
					return function.Invoke();
				}
			}

			// Token: 0x02001487 RID: 5255
			private class IsDataSourceFunctionValue : NativeFunctionValue1<LogicalValue, FunctionValue>
			{
				// Token: 0x0600859D RID: 34205 RVA: 0x001C71DC File Offset: 0x001C53DC
				public IsDataSourceFunctionValue()
					: base(TypeValue.Logical, "function", TypeValue.Function)
				{
				}

				// Token: 0x0600859E RID: 34206 RVA: 0x001C71F3 File Offset: 0x001C53F3
				public override LogicalValue TypedInvoke(FunctionValue function)
				{
					return LogicalValue.New(function.IsResourceAccessFunction);
				}
			}

			// Token: 0x02001488 RID: 5256
			private class ScalarVectorFunctionValue : NativeFunctionValue2<FunctionValue, TypeValue, FunctionValue>
			{
				// Token: 0x0600859F RID: 34207 RVA: 0x001C7200 File Offset: 0x001C5400
				public ScalarVectorFunctionValue()
					: base(TypeValue.Function, "scalarFunctionType", TypeValue._Type, "vectorFunction", TypeValue.Function)
				{
				}

				// Token: 0x060085A0 RID: 34208 RVA: 0x001C7221 File Offset: 0x001C5421
				public override FunctionValue TypedInvoke(TypeValue scalarFunctionType, FunctionValue vectorFunction)
				{
					return new Library.Function.ScalarVectorFunctionValue.VectorizableScalarFunctionValue(scalarFunctionType.AsFunctionType, vectorFunction);
				}

				// Token: 0x02001489 RID: 5257
				private sealed class VectorizableScalarFunctionValue : NativeFunctionValueN
				{
					// Token: 0x060085A1 RID: 34209 RVA: 0x001C7230 File Offset: 0x001C5430
					public VectorizableScalarFunctionValue(FunctionTypeValue scalarFunctionType, FunctionValue vectorFunction)
						: base(scalarFunctionType)
					{
						if (vectorFunction.Type.AsFunctionType.ParameterCount != 1)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.FunctionScalarVectorVectorFunctionSingleInput, null, null);
						}
						this.vectorFunction = vectorFunction;
						IValueReference[] array = new IValueReference[scalarFunctionType.ParameterCount];
						for (int i = 0; i < scalarFunctionType.ParameterCount; i++)
						{
							array[i] = RecordTypeValue.NewField(scalarFunctionType.ParameterType(i), null);
						}
						RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(scalarFunctionType.Parameters.Keys, array));
						this.vectorFunctionInputType = TableTypeValue.New(recordTypeValue);
					}

					// Token: 0x060085A2 RID: 34210 RVA: 0x001C72BC File Offset: 0x001C54BC
					protected override Value InvokeN(Value[] args)
					{
						RecordValue recordValue = RecordValue.New(this.vectorFunctionInputType.ItemType, args);
						TableValue tableValue = new ListTableValue(ListValue.New(new Value[] { recordValue }), this.vectorFunctionInputType);
						return this.vectorFunction.Invoke(tableValue).AsList.Item0;
					}

					// Token: 0x060085A3 RID: 34211 RVA: 0x001C730C File Offset: 0x001C550C
					public override bool TryVectorizeFunction(out FunctionValue vectorFunction)
					{
						vectorFunction = this.vectorFunction;
						return true;
					}

					// Token: 0x04004A08 RID: 18952
					private readonly FunctionValue vectorFunction;

					// Token: 0x04004A09 RID: 18953
					private readonly TableTypeValue vectorFunctionInputType;
				}
			}

			// Token: 0x0200148A RID: 5258
			private sealed class PreserveTableLineageFunctionValue : NativeFunctionValue1<FunctionValue, FunctionValue>
			{
				// Token: 0x060085A4 RID: 34212 RVA: 0x001C7317 File Offset: 0x001C5517
				public PreserveTableLineageFunctionValue()
					: base(TypeValue.Function, "function", TypeValue.Function)
				{
				}

				// Token: 0x060085A5 RID: 34213 RVA: 0x001C732E File Offset: 0x001C552E
				public override FunctionValue TypedInvoke(FunctionValue function)
				{
					return new Library.Function.TableLineagePreservingFunctionValue(function);
				}
			}

			// Token: 0x0200148B RID: 5259
			private sealed class TableLineagePreservingFunctionValue : DelegatingFunctionValue
			{
				// Token: 0x060085A6 RID: 34214 RVA: 0x001B7181 File Offset: 0x001B5381
				public TableLineagePreservingFunctionValue(FunctionValue function)
					: base(function)
				{
				}

				// Token: 0x060085A7 RID: 34215 RVA: 0x001B719A File Offset: 0x001B539A
				public override Value Invoke()
				{
					return this.Invoke(EmptyArray<Value>.Instance);
				}

				// Token: 0x060085A8 RID: 34216 RVA: 0x00189553 File Offset: 0x00187753
				public override Value Invoke(Value arg0)
				{
					return this.Invoke(new Value[] { arg0 });
				}

				// Token: 0x060085A9 RID: 34217 RVA: 0x00189565 File Offset: 0x00187765
				public override Value Invoke(Value arg0, Value arg1)
				{
					return this.Invoke(new Value[] { arg0, arg1 });
				}

				// Token: 0x060085AA RID: 34218 RVA: 0x0018957B File Offset: 0x0018777B
				public override Value Invoke(Value arg0, Value arg1, Value arg2)
				{
					return this.Invoke(new Value[] { arg0, arg1, arg2 });
				}

				// Token: 0x060085AB RID: 34219 RVA: 0x00189595 File Offset: 0x00187795
				public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
				{
					return this.Invoke(new Value[] { arg0, arg1, arg2, arg3 });
				}

				// Token: 0x060085AC RID: 34220 RVA: 0x001895B4 File Offset: 0x001877B4
				public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
				{
					return this.Invoke(new Value[] { arg0, arg1, arg2, arg3, arg4 });
				}

				// Token: 0x060085AD RID: 34221 RVA: 0x001C7336 File Offset: 0x001C5536
				public override Value Invoke(params Value[] args)
				{
					return TransformedTableValue.New(base.Invoke(args).AsTable, this, args);
				}

				// Token: 0x060085AE RID: 34222 RVA: 0x001C732E File Offset: 0x001C552E
				protected override FunctionValue Wrap(FunctionValue function)
				{
					return new Library.Function.TableLineagePreservingFunctionValue(function);
				}
			}
		}

		// Token: 0x0200148C RID: 5260
		public static class Expression
		{
			// Token: 0x04004A0A RID: 18954
			public static readonly FunctionValue Constant = new Library.Expression.ConstantFunctionValue();

			// Token: 0x04004A0B RID: 18955
			public static readonly FunctionValue Evaluate = new Library.Expression.EvaluateFunctionValue();

			// Token: 0x04004A0C RID: 18956
			public static readonly FunctionValue Identifier = new Library.Expression.IdentifierFunctionValue();

			// Token: 0x04004A0D RID: 18957
			public static readonly FunctionValue NotImplemented = new Library.Expression.NotImplementedFunctionValue();

			// Token: 0x04004A0E RID: 18958
			public static readonly FunctionValue ExecuteQuery = new Library.Expression.ExecuteQueryFunctionValue();

			// Token: 0x04004A0F RID: 18959
			public static readonly FunctionValue SafeErrorRecord = new Library.Expression.SafeErrorRecordFunctionValue();

			// Token: 0x0200148D RID: 5261
			private class ConstantFunctionValue : NativeFunctionValue1<TextValue, Value>
			{
				// Token: 0x060085B0 RID: 34224 RVA: 0x001C7389 File Offset: 0x001C5589
				public ConstantFunctionValue()
					: base(TypeValue.Text, 1, "value", TypeValue.Any)
				{
				}

				// Token: 0x060085B1 RID: 34225 RVA: 0x001C73A4 File Offset: 0x001C55A4
				public override TextValue TypedInvoke(Value value)
				{
					switch (value.Kind)
					{
					case ValueKind.None:
					case ValueKind.Any:
					case ValueKind.Binary:
					case ValueKind.List:
					case ValueKind.Record:
					case ValueKind.Table:
					case ValueKind.Function:
					case ValueKind.Type:
					case ValueKind.Action:
						throw ValueException.NewExpressionError<Message1>(Strings.Constants_ExpectedConstantValue(value), value, null);
					case ValueKind.Null:
					case ValueKind.Time:
					case ValueKind.Date:
					case ValueKind.DateTime:
					case ValueKind.DateTimeZone:
					case ValueKind.Duration:
					case ValueKind.Number:
					case ValueKind.Logical:
					case ValueKind.Text:
						return TextValue.New(value.ToSource());
					default:
						throw new NotImplementedException(value.Kind.ToString());
					}
				}
			}

			// Token: 0x0200148E RID: 5262
			private class EvaluateFunctionValue : NativeFunctionValue2<Value, TextValue, Value>
			{
				// Token: 0x060085B2 RID: 34226 RVA: 0x001C743E File Offset: 0x001C563E
				public EvaluateFunctionValue()
					: base(TypeValue.Any, 1, "document", TypeValue.Text, "environment", NullableTypeValue.Record)
				{
				}

				// Token: 0x060085B3 RID: 34227 RVA: 0x001C7460 File Offset: 0x001C5660
				public override Value TypedInvoke(TextValue document, Value environment)
				{
					return LanguageLibrary.Evaluate(document.String, environment);
				}
			}

			// Token: 0x0200148F RID: 5263
			private class IdentifierFunctionValue : NativeFunctionValue1<TextValue, TextValue>
			{
				// Token: 0x060085B4 RID: 34228 RVA: 0x001C746E File Offset: 0x001C566E
				public IdentifierFunctionValue()
					: base(TypeValue.Text, 1, "name", TypeValue.Text)
				{
				}

				// Token: 0x060085B5 RID: 34229 RVA: 0x001C7486 File Offset: 0x001C5686
				public override TextValue TypedInvoke(TextValue name)
				{
					return TextValue.New(LanguageServices.Identifier.Escape(name.String));
				}
			}

			// Token: 0x02001490 RID: 5264
			private class ExecuteQueryFunctionValue : NativeFunctionValue3<Value, Value, FunctionValue, FunctionValue>
			{
				// Token: 0x060085B6 RID: 34230 RVA: 0x001C7498 File Offset: 0x001C5698
				public ExecuteQueryFunctionValue()
					: base(TypeValue.Any, "from", TypeValue.Any, "query", TypeValue.Function, "expression", TypeValue.Function)
				{
				}

				// Token: 0x060085B7 RID: 34231 RVA: 0x001C74C4 File Offset: 0x001C56C4
				public override Value TypedInvoke(Value from, FunctionValue query, FunctionValue expression)
				{
					Value value;
					if (from.TryGetMetaField("Expression.Query", out value))
					{
						return value.AsFunction.Invoke(expression.Invoke());
					}
					return query.Invoke(from);
				}
			}

			// Token: 0x02001491 RID: 5265
			private class NotImplementedFunctionValue : NativeFunctionValue1<RecordValue, Value>
			{
				// Token: 0x060085B8 RID: 34232 RVA: 0x001C74F9 File Offset: 0x001C56F9
				public NotImplementedFunctionValue()
					: base(TypeValue.Record, 0, "message", NullableTypeValue.Text)
				{
				}

				// Token: 0x060085B9 RID: 34233 RVA: 0x001C7511 File Offset: 0x001C5711
				public override RecordValue TypedInvoke(Value message)
				{
					if (message.IsNull)
					{
						message = TextValue.New(Strings.NotImplementedFunction_NotImplemented);
					}
					return ErrorRecord.New(ValueException.ExpressionError, message.AsText, Value.Null);
				}
			}

			// Token: 0x02001492 RID: 5266
			private class SafeErrorRecordFunctionValue : NativeFunctionValue1
			{
				// Token: 0x060085BA RID: 34234 RVA: 0x001C7544 File Offset: 0x001C5744
				public override Value Invoke(Value error)
				{
					if (error.IsText)
					{
						return ErrorRecord.New(error.AsText);
					}
					RecordValue asRecord = error.AsRecord;
					Value defaultReason;
					if (!asRecord.TryGetValue(ErrorRecord.ReasonKey, out defaultReason))
					{
						defaultReason = ErrorRecord.DefaultReason;
					}
					Value @null;
					if (!asRecord.TryGetValue(ErrorRecord.MessageKey, out @null))
					{
						@null = Value.Null;
					}
					Value null2;
					if (!asRecord.TryGetValue(ErrorRecord.DetailKey, out null2))
					{
						null2 = Value.Null;
					}
					Value null3;
					if (!asRecord.TryGetValue(ErrorRecord.MessageFormatKey, out null3))
					{
						null3 = Value.Null;
					}
					Value null4;
					if (!asRecord.TryGetValue(ErrorRecord.MessageParametersKey, out null4))
					{
						null4 = Value.Null;
					}
					if (null3.IsNull)
					{
						return ErrorRecord.New(defaultReason.AsText, @null, null2, Value.Null, Value.Null);
					}
					return ErrorRecord.New(defaultReason.AsText, null3, null2, null4);
				}
			}
		}

		// Token: 0x02001493 RID: 5267
		public static class RoundingMode
		{
			// Token: 0x04004A10 RID: 18960
			public static readonly IntEnumTypeValue<NumberValue.RoundingMode> Type = new IntEnumTypeValue<NumberValue.RoundingMode>("RoundingMode.Type");

			// Token: 0x04004A11 RID: 18961
			public static readonly NumberValue Up = Library.RoundingMode.Type.NewEnumValue("RoundingMode.Up", 0, NumberValue.RoundingMode.Up, null);

			// Token: 0x04004A12 RID: 18962
			public static readonly NumberValue Down = Library.RoundingMode.Type.NewEnumValue("RoundingMode.Down", 1, NumberValue.RoundingMode.Down, null);

			// Token: 0x04004A13 RID: 18963
			public static readonly NumberValue AwayFromZero = Library.RoundingMode.Type.NewEnumValue("RoundingMode.AwayFromZero", 2, NumberValue.RoundingMode.AwayFromZero, null);

			// Token: 0x04004A14 RID: 18964
			public static readonly NumberValue TowardZero = Library.RoundingMode.Type.NewEnumValue("RoundingMode.TowardZero", 3, NumberValue.RoundingMode.TowardZero, null);

			// Token: 0x04004A15 RID: 18965
			public static readonly NumberValue ToEven = Library.RoundingMode.Type.NewEnumValue("RoundingMode.ToEven", 4, NumberValue.RoundingMode.ToEven, null);
		}

		// Token: 0x02001494 RID: 5268
		public static class JoinKind
		{
			// Token: 0x04004A16 RID: 18966
			public static readonly IntEnumTypeValue<TableTypeAlgebra.JoinKind> Type = new IntEnumTypeValue<TableTypeAlgebra.JoinKind>("JoinKind.Type");

			// Token: 0x04004A17 RID: 18967
			public static readonly NumberValue Inner = Library.JoinKind.Type.NewEnumValue("JoinKind.Inner", 0, TableTypeAlgebra.JoinKind.Inner, null);

			// Token: 0x04004A18 RID: 18968
			public static readonly NumberValue LeftOuter = Library.JoinKind.Type.NewEnumValue("JoinKind.LeftOuter", 1, TableTypeAlgebra.JoinKind.LeftOuter, null);

			// Token: 0x04004A19 RID: 18969
			public static readonly NumberValue RightOuter = Library.JoinKind.Type.NewEnumValue("JoinKind.RightOuter", 2, TableTypeAlgebra.JoinKind.RightOuter, null);

			// Token: 0x04004A1A RID: 18970
			public static readonly NumberValue FullOuter = Library.JoinKind.Type.NewEnumValue("JoinKind.FullOuter", 3, TableTypeAlgebra.JoinKind.FullOuter, null);

			// Token: 0x04004A1B RID: 18971
			public static readonly NumberValue LeftAnti = Library.JoinKind.Type.NewEnumValue("JoinKind.LeftAnti", 4, TableTypeAlgebra.JoinKind.LeftAnti, null);

			// Token: 0x04004A1C RID: 18972
			public static readonly NumberValue RightAnti = Library.JoinKind.Type.NewEnumValue("JoinKind.RightAnti", 5, TableTypeAlgebra.JoinKind.RightAnti, null);

			// Token: 0x04004A1D RID: 18973
			public static readonly NumberValue LeftSemi = Library.JoinKind.Type.NewEnumValue("JoinKind.LeftSemi", 6, TableTypeAlgebra.JoinKind.LeftSemi, null);

			// Token: 0x04004A1E RID: 18974
			public static readonly NumberValue RightSemi = Library.JoinKind.Type.NewEnumValue("JoinKind.RightSemi", 7, TableTypeAlgebra.JoinKind.RightSemi, null);
		}

		// Token: 0x02001495 RID: 5269
		public static class MissingField
		{
			// Token: 0x04004A1F RID: 18975
			public static readonly IntEnumTypeValue<MissingFieldMode> Type = new IntEnumTypeValue<MissingFieldMode>("MissingField.Type");

			// Token: 0x04004A20 RID: 18976
			public static readonly NumberValue Error = Library.MissingField.Type.NewEnumValue("MissingField.Error", 0, MissingFieldMode.Error, null);

			// Token: 0x04004A21 RID: 18977
			public static readonly NumberValue Ignore = Library.MissingField.Type.NewEnumValue("MissingField.Ignore", 1, MissingFieldMode.Ignore, null);

			// Token: 0x04004A22 RID: 18978
			public static readonly NumberValue UseNull = Library.MissingField.Type.NewEnumValue("MissingField.UseNull", 2, MissingFieldMode.UseNull, null);
		}

		// Token: 0x02001496 RID: 5270
		public static class GroupKind
		{
			// Token: 0x04004A23 RID: 18979
			public static readonly IntEnumTypeValue<TableValue.GroupKind> Type = new IntEnumTypeValue<TableValue.GroupKind>("GroupKind.Type");

			// Token: 0x04004A24 RID: 18980
			public static readonly NumberValue Local = Library.GroupKind.Type.NewEnumValue("GroupKind.Local", 0, TableValue.GroupKind.Local, null);

			// Token: 0x04004A25 RID: 18981
			public static readonly NumberValue Global = Library.GroupKind.Type.NewEnumValue("GroupKind.Global", 1, TableValue.GroupKind.Global, null);
		}

		// Token: 0x02001497 RID: 5271
		public static class Order
		{
			// Token: 0x04004A26 RID: 18982
			public static readonly IntEnumTypeValue<NumberValue> Type = new IntEnumTypeValue<NumberValue>("Order.Type");

			// Token: 0x04004A27 RID: 18983
			public static readonly NumberValue Ascending = Library.Order.Type.NewEnumValue("Order.Ascending", 0, NumberValue.Zero, null);

			// Token: 0x04004A28 RID: 18984
			public static readonly NumberValue Descending = Library.Order.Type.NewEnumValue("Order.Descending", 1, NumberValue.One, null);
		}

		// Token: 0x02001498 RID: 5272
		public static class Occurrence
		{
			// Token: 0x04004A29 RID: 18985
			public static readonly IntEnumTypeValue<NumberValue> Type = new IntEnumTypeValue<NumberValue>("Occurrence.Type");

			// Token: 0x04004A2A RID: 18986
			public static readonly NumberValue First = Library.Occurrence.Type.NewEnumValue("Occurrence.First", 0, NumberValue.Zero, null);

			// Token: 0x04004A2B RID: 18987
			public static readonly NumberValue Last = Library.Occurrence.Type.NewEnumValue("Occurrence.Last", 1, NumberValue.One, null);

			// Token: 0x04004A2C RID: 18988
			public static readonly NumberValue All = Library.Occurrence.Type.NewEnumValue("Occurrence.All", 2, NumberValue.New(2), null);
		}

		// Token: 0x02001499 RID: 5273
		public static class CompressionType
		{
			// Token: 0x04004A2D RID: 18989
			public static readonly IntEnumTypeValue<CompressionKind> Type = new IntEnumTypeValue<CompressionKind>("Compression.Type");

			// Token: 0x04004A2E RID: 18990
			public static readonly NumberValue None = Library.CompressionType.Type.NewEnumValue("Compression.None", -1, CompressionKind.None, null);

			// Token: 0x04004A2F RID: 18991
			public static readonly NumberValue GZip = Library.CompressionType.Type.NewEnumValue("Compression.GZip", 0, CompressionKind.GZip, null);

			// Token: 0x04004A30 RID: 18992
			public static readonly NumberValue Deflate = Library.CompressionType.Type.NewEnumValue("Compression.Deflate", 1, CompressionKind.Deflate, null);

			// Token: 0x04004A31 RID: 18993
			public static readonly NumberValue Snappy = Library.CompressionType.Type.NewEnumValue("Compression.Snappy", 2, CompressionKind.Snappy, null);

			// Token: 0x04004A32 RID: 18994
			public static readonly NumberValue Brotli = Library.CompressionType.Type.NewEnumValue("Compression.Brotli", 3, CompressionKind.Brotli, null);

			// Token: 0x04004A33 RID: 18995
			public static readonly NumberValue LZ4 = Library.CompressionType.Type.NewEnumValue("Compression.LZ4", 4, CompressionKind.LZ4, null);

			// Token: 0x04004A34 RID: 18996
			public static readonly NumberValue Zstandard = Library.CompressionType.Type.NewEnumValue("Compression.Zstandard", 5, CompressionKind.Zstandard, null);
		}

		// Token: 0x0200149A RID: 5274
		public static class ListEquationCriteria
		{
			// Token: 0x060085C3 RID: 34243 RVA: 0x001C798C File Offset: 0x001C5B8C
			public static IEqualityComparer<Value> CreateEqualityComparer(Value equationCriteria, bool requiresHashCode)
			{
				IEqualityComparer<Value> equalityComparer = EqualityComparer<Value>.Default;
				if (equationCriteria.IsNull)
				{
					return equalityComparer;
				}
				if (equationCriteria.IsFunction)
				{
					FunctionValue asFunction = equationCriteria.AsFunction;
					if (Library.ListComparisonCriteria.IsKeySelector(asFunction))
					{
						equalityComparer = new Library.ListEquationCriteria.KeySelectorComparer(asFunction, equalityComparer);
					}
					else if (!asFunction.TryGetEqualityComparer(out equalityComparer))
					{
						if (requiresHashCode)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.CustomComparersNotAllowed, asFunction, null);
						}
						equalityComparer = new Library.ListEquationCriteria.EqualityOnlyEqualityComparer(asFunction);
					}
					return equalityComparer;
				}
				if (equationCriteria.IsList)
				{
					ListValue asList = equationCriteria.AsList;
					if (asList.Count == 2 && !asList[1].IsNull)
					{
						FunctionValue asFunction2 = asList[1].AsFunction;
						if (!asFunction2.TryGetEqualityComparer(out equalityComparer))
						{
							if (requiresHashCode)
							{
								throw ValueException.NewExpressionError<Message0>(Strings.CustomComparersNotAllowed, asFunction2, null);
							}
							equalityComparer = new Library.ListEquationCriteria.EqualityOnlyEqualityComparer(asFunction2);
						}
						return new Library.ListEquationCriteria.KeySelectorComparer(asList[0].AsFunction, equalityComparer);
					}
				}
				throw ValueException.NewExpressionError<Message0>(Strings.InvalidComparerFormat, equationCriteria, null);
			}

			// Token: 0x0200149B RID: 5275
			private class KeySelectorComparer : IEqualityComparer<Value>
			{
				// Token: 0x060085C4 RID: 34244 RVA: 0x001C7A63 File Offset: 0x001C5C63
				public KeySelectorComparer(FunctionValue keySelector, IEqualityComparer<Value> comparer)
				{
					this.keySelector = keySelector;
					this.comparer = comparer;
				}

				// Token: 0x060085C5 RID: 34245 RVA: 0x001C7A79 File Offset: 0x001C5C79
				public bool Equals(Value x, Value y)
				{
					return this.comparer.Equals(this.keySelector.Invoke(x), this.keySelector.Invoke(y));
				}

				// Token: 0x060085C6 RID: 34246 RVA: 0x001C7A9E File Offset: 0x001C5C9E
				public int GetHashCode(Value obj)
				{
					return this.comparer.GetHashCode(this.keySelector.Invoke(obj));
				}

				// Token: 0x04004A35 RID: 18997
				private readonly FunctionValue keySelector;

				// Token: 0x04004A36 RID: 18998
				private readonly IEqualityComparer<Value> comparer;
			}

			// Token: 0x0200149C RID: 5276
			private class EqualityOnlyEqualityComparer : IEqualityComparer<Value>
			{
				// Token: 0x060085C7 RID: 34247 RVA: 0x001C7AB7 File Offset: 0x001C5CB7
				public EqualityOnlyEqualityComparer(FunctionValue function)
				{
					this.function = function;
				}

				// Token: 0x060085C8 RID: 34248 RVA: 0x001C7AC6 File Offset: 0x001C5CC6
				public bool Equals(Value x, Value y)
				{
					return this.function.Invoke(x, y).AsBoolean;
				}

				// Token: 0x060085C9 RID: 34249 RVA: 0x0000EE09 File Offset: 0x0000D009
				public int GetHashCode(Value obj)
				{
					throw new InvalidOperationException();
				}

				// Token: 0x04004A37 RID: 18999
				private readonly FunctionValue function;
			}
		}

		// Token: 0x0200149D RID: 5277
		public static class ListReplacementOperations
		{
			// Token: 0x04004A38 RID: 19000
			public static readonly FunctionValue IsListReplacementOperations = new Library.ListReplacementOperations.IsListReplacementOperationsFunctionValue();

			// Token: 0x04004A39 RID: 19001
			public static readonly FunctionValue _ListReplacementOperations = new Library.ListReplacementOperations.ListReplacementOperationsFunctionValue();

			// Token: 0x0200149E RID: 5278
			private sealed class IsListReplacementOperationsFunctionValue : NativeFunctionValue1<LogicalValue, Value>
			{
				// Token: 0x060085CB RID: 34251 RVA: 0x0004815D File Offset: 0x0004635D
				public IsListReplacementOperationsFunctionValue()
					: base(TypeValue.Logical, "value", TypeValue.Any)
				{
				}

				// Token: 0x060085CC RID: 34252 RVA: 0x001C7AF0 File Offset: 0x001C5CF0
				public override LogicalValue TypedInvoke(Value value)
				{
					return LogicalValue.New(value.IsListOf((Value item) => item.IsList && item.AsList.Count == 2));
				}
			}

			// Token: 0x020014A0 RID: 5280
			private sealed class ListReplacementOperationsFunctionValue : NativeFunctionValue1<ListValue, Value>
			{
				// Token: 0x060085D0 RID: 34256 RVA: 0x001C7B42 File Offset: 0x001C5D42
				public ListReplacementOperationsFunctionValue()
					: base(TypeValue.List, "value", TypeValue.Any)
				{
				}

				// Token: 0x060085D1 RID: 34257 RVA: 0x001C7B59 File Offset: 0x001C5D59
				public override ListValue TypedInvoke(Value value)
				{
					if (!Library.ListReplacementOperations.IsListReplacementOperations.Invoke(value).AsBoolean)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ListReplacementOperations_ListReplacementOperationsValueExpectedError, value, null);
					}
					return value.AsList;
				}
			}
		}

		// Token: 0x020014A1 RID: 5281
		public static class ListRuntime
		{
			// Token: 0x04004A3C RID: 19004
			public static readonly FunctionValue Transform = new Library.ListRuntime.TransformFunctionValue();

			// Token: 0x020014A2 RID: 5282
			private class TransformFunctionValue : NativeFunctionValue2<ListValue, ListValue, FunctionValue>
			{
				// Token: 0x060085D3 RID: 34259 RVA: 0x001C7B8C File Offset: 0x001C5D8C
				public TransformFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "transform", TypeValue.Function)
				{
				}

				// Token: 0x060085D4 RID: 34260 RVA: 0x0000EE09 File Offset: 0x0000D009
				public override ListValue TypedInvoke(ListValue list, FunctionValue transform)
				{
					throw new InvalidOperationException();
				}
			}
		}

		// Token: 0x020014A3 RID: 5283
		public static class Comparer
		{
			// Token: 0x04004A3D RID: 19005
			public static readonly FunctionValue Ordinal = new Library.Comparer.ComparerFunctionValue(_ValueComparer.StrictDefault);

			// Token: 0x04004A3E RID: 19006
			public new static readonly FunctionValue Equals = new Library.Comparer.EqualsFunctionValue();

			// Token: 0x04004A3F RID: 19007
			public static readonly FunctionValue OrdinalIgnoreCase = new Library.Comparer.ComparerFunctionValue(_ValueComparer.StrictDefaultIgnoreCase);

			// Token: 0x04004A40 RID: 19008
			public static readonly FunctionValue LaxOrdinalIgnoreCase = new Library.Comparer.ComparerFunctionValue(_ValueComparer.LaxDefaultIgnoreCase);

			// Token: 0x020014A4 RID: 5284
			public class FromCultureFunctionValue : CultureSpecificFunctionValue2<FunctionValue, TextValue, Value>
			{
				// Token: 0x060085D6 RID: 34262 RVA: 0x001C7BE8 File Offset: 0x001C5DE8
				public FromCultureFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Function, 1, "culture", TypeValue.Text, "ignoreCase", NullableTypeValue.Logical)
				{
				}

				// Token: 0x060085D7 RID: 34263 RVA: 0x001C7C18 File Offset: 0x001C5E18
				public override FunctionValue TypedInvoke(TextValue culture, Value ignoreCaseValue)
				{
					CultureInfo cultureInfo = base.GetCulture(culture).GetCultureInfo();
					NumberComparer @double = NumberComparer.Double;
					bool flag = !ignoreCaseValue.IsNull && ignoreCaseValue.AsBoolean;
					return new Library.Comparer.ComparerFunctionValue(new _ValueComparer(StringComparer.Create(cultureInfo, flag), cultureInfo, flag, @double, false));
				}
			}

			// Token: 0x020014A5 RID: 5285
			private class EqualsFunctionValue : NativeFunctionValue3<LogicalValue, FunctionValue, Value, Value>
			{
				// Token: 0x060085D8 RID: 34264 RVA: 0x001C7C5F File Offset: 0x001C5E5F
				public EqualsFunctionValue()
					: base(TypeValue.Logical, "comparer", TypeValue.Function, "x", TypeValue.Any, "y", TypeValue.Any)
				{
				}

				// Token: 0x060085D9 RID: 34265 RVA: 0x001C7C8C File Offset: 0x001C5E8C
				public override LogicalValue TypedInvoke(FunctionValue comparer, Value x, Value y)
				{
					IEqualityComparer<Value> equalityComparer;
					if (comparer.TryGetEqualityComparer(out equalityComparer))
					{
						return LogicalValue.New(equalityComparer.Equals(x, y));
					}
					return LogicalValue.New(comparer.Invoke(x, y).Equals(NumberValue.Zero));
				}
			}

			// Token: 0x020014A6 RID: 5286
			public class ComparerFunctionValue : NativeFunctionValue2<NumberValue, Value, Value>
			{
				// Token: 0x060085DA RID: 34266 RVA: 0x001C7CC8 File Offset: 0x001C5EC8
				public ComparerFunctionValue(_ValueComparer comparer)
					: base(TypeValue.Number, "x", TypeValue.Any, "y", TypeValue.Any)
				{
					this.comparer = comparer;
				}

				// Token: 0x060085DB RID: 34267 RVA: 0x001C7CF0 File Offset: 0x001C5EF0
				public override bool TryGetCultureCase(out CultureInfo cultureInfo, out bool ignoreCase)
				{
					cultureInfo = this.comparer.Culture;
					ignoreCase = this.comparer.IgnoreCase;
					return true;
				}

				// Token: 0x060085DC RID: 34268 RVA: 0x001C7D0D File Offset: 0x001C5F0D
				public override bool TryGetEqualityComparer(out IEqualityComparer<Value> comparer)
				{
					comparer = this.comparer;
					return true;
				}

				// Token: 0x060085DD RID: 34269 RVA: 0x001C7D18 File Offset: 0x001C5F18
				public override NumberValue TypedInvoke(Value arg0, Value arg1)
				{
					return ValueHelper.ComparisonResultToValue(this.comparer.Compare(arg0, arg1));
				}

				// Token: 0x04004A41 RID: 19009
				private readonly _ValueComparer comparer;
			}
		}

		// Token: 0x020014A7 RID: 5287
		public static class ListComparisonCriteria
		{
			// Token: 0x060085DE RID: 34270 RVA: 0x001C7D2C File Offset: 0x001C5F2C
			public static IComparer<Value> CreateComparer(Value comparisonCriteria)
			{
				if (comparisonCriteria.IsNull)
				{
					return _ValueComparer.LaxDefault;
				}
				return new TableSortOrder(Library.ListComparisonCriteria.CreateSortCriteria(comparisonCriteria)).ToComparer();
			}

			// Token: 0x060085DF RID: 34271 RVA: 0x001C7D4C File Offset: 0x001C5F4C
			public static SortOrder[] CreateSortCriteria(Value value)
			{
				if (value.IsList)
				{
					return Library.ListComparisonCriteria.CreateSortCriteria(value.AsList);
				}
				return new SortOrder[] { Library.ListComparisonCriteria.CreateSortCriterion(value) };
			}

			// Token: 0x060085E0 RID: 34272 RVA: 0x001C7D78 File Offset: 0x001C5F78
			private static SortOrder[] CreateSortCriteria(ListValue list)
			{
				if (list.Count >= 2 && (list[1].IsNumber || (list[1].IsFunction && !Library.ListComparisonCriteria.IsKeySelector(list[1].AsFunction))))
				{
					return new SortOrder[] { Library.ListComparisonCriteria.CreateSortCriterion(list) };
				}
				List<SortOrder> list2 = new List<SortOrder>();
				foreach (IValueReference valueReference in list)
				{
					Value value = valueReference.Value;
					if (value.IsList)
					{
						list2.Add(Library.ListComparisonCriteria.CreateSortCriterion(value.AsList));
					}
					else
					{
						list2.Add(new SortOrder(value.AsFunction, null, true));
					}
				}
				return list2.ToArray();
			}

			// Token: 0x060085E1 RID: 34273 RVA: 0x001C7E44 File Offset: 0x001C6044
			public static bool IsKeySelector(FunctionValue value)
			{
				return value.Type.AsFunctionType.ParameterCount == 1;
			}

			// Token: 0x060085E2 RID: 34274 RVA: 0x001C7E5C File Offset: 0x001C605C
			private static SortOrder CreateSortCriterion(Value value)
			{
				if (!value.IsFunction)
				{
					return new SortOrder(null, null, value.IsNull || value.Equals(Library.Order.Ascending));
				}
				FunctionValue asFunction = value.AsFunction;
				if (Library.ListComparisonCriteria.IsKeySelector(asFunction))
				{
					return new SortOrder(asFunction, null, true);
				}
				return new SortOrder(null, asFunction, true);
			}

			// Token: 0x060085E3 RID: 34275 RVA: 0x001C7EB0 File Offset: 0x001C60B0
			private static SortOrder CreateSortCriterion(ListValue list)
			{
				if (list.Count == 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ListComparisonCriteria_ListComparisonCriteriaValueExpectedError, list, null);
				}
				if (list.Count == 1)
				{
					return Library.ListComparisonCriteria.CreateSortCriterion(list[0]);
				}
				FunctionValue asFunction = list[0].AsFunction;
				bool flag = true;
				FunctionValue functionValue;
				if (list.Count == 2)
				{
					if (list[1].IsNumber)
					{
						flag = list[1].Equals(Library.Order.Ascending);
						return new SortOrder(asFunction, null, flag);
					}
					functionValue = list[1].AsFunction;
				}
				else
				{
					flag = list[1].Equals(Library.Order.Ascending);
					functionValue = list[2].AsFunction;
				}
				return new SortOrder(asFunction, functionValue, flag);
			}
		}

		// Token: 0x020014A8 RID: 5288
		private enum PercentileMode
		{
			// Token: 0x04004A43 RID: 19011
			ExcelInc = 1,
			// Token: 0x04004A44 RID: 19012
			ExcelExc,
			// Token: 0x04004A45 RID: 19013
			SqlDisc,
			// Token: 0x04004A46 RID: 19014
			SqlCont
		}

		// Token: 0x020014A9 RID: 5289
		private static class PercentileModeEnum
		{
			// Token: 0x04004A47 RID: 19015
			public static readonly IntEnumTypeValue<Library.PercentileMode> Type = new IntEnumTypeValue<Library.PercentileMode>("PercentileMode.Type");

			// Token: 0x04004A48 RID: 19016
			public static readonly NumberValue ExcelInc = Library.PercentileModeEnum.Type.NewEnumValue("PercentileMode.ExcelInc", 1, Library.PercentileMode.ExcelInc, null);

			// Token: 0x04004A49 RID: 19017
			public static readonly NumberValue ExcelExc = Library.PercentileModeEnum.Type.NewEnumValue("PercentileMode.ExcelExc", 2, Library.PercentileMode.ExcelExc, null);

			// Token: 0x04004A4A RID: 19018
			public static readonly NumberValue SqlDisc = Library.PercentileModeEnum.Type.NewEnumValue("PercentileMode.SqlDisc", 3, Library.PercentileMode.SqlDisc, null);

			// Token: 0x04004A4B RID: 19019
			public static readonly NumberValue SqlCont = Library.PercentileModeEnum.Type.NewEnumValue("PercentileMode.SqlCont", 4, Library.PercentileMode.SqlCont, null);
		}

		// Token: 0x020014AA RID: 5290
		public enum BufferMode
		{
			// Token: 0x04004A4D RID: 19021
			Eager = 1,
			// Token: 0x04004A4E RID: 19022
			Delayed
		}

		// Token: 0x020014AB RID: 5291
		public static class BufferModeEnum
		{
			// Token: 0x04004A4F RID: 19023
			public static readonly IntEnumTypeValue<Library.BufferMode> Type = new IntEnumTypeValue<Library.BufferMode>("BufferMode.Type");

			// Token: 0x04004A50 RID: 19024
			public static readonly NumberValue Eager = Library.BufferModeEnum.Type.NewEnumValue("BufferMode.Eager", 1, Library.BufferMode.Eager, null);

			// Token: 0x04004A51 RID: 19025
			public static readonly NumberValue Delayed = Library.BufferModeEnum.Type.NewEnumValue("BufferMode.Delayed", 2, Library.BufferMode.Delayed, null);
		}

		// Token: 0x020014AC RID: 5292
		public class List
		{
			// Token: 0x060085E6 RID: 34278 RVA: 0x001C801C File Offset: 0x001C621C
			private static TypeValue GetListOperationReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
			{
				if (arguments.Count < 1)
				{
					return TypeValue.Any;
				}
				TypeValue type = environment.GetType(arguments[0]);
				if (!type.IsListType)
				{
					return TypeValue.Any;
				}
				return type.AsListType.ItemType.Nullable;
			}

			// Token: 0x04004A52 RID: 19026
			public static readonly FunctionValue Contains = new Library.List.ContainsFunctionValue();

			// Token: 0x04004A53 RID: 19027
			public static readonly FunctionValue Density = new Library.List.DensityFunctionValue();

			// Token: 0x04004A54 RID: 19028
			public static readonly FunctionValue Difference = new Library.List.DifferenceFunctionValue();

			// Token: 0x04004A55 RID: 19029
			public static readonly FunctionValue First = new Library.List.FirstFunctionValue();

			// Token: 0x04004A56 RID: 19030
			public static readonly FunctionValue Generate = new Library.List.GenerateFunctionValue();

			// Token: 0x04004A57 RID: 19031
			public static readonly FunctionValue Intersect = new Library.List.IntersectFunctionValue();

			// Token: 0x04004A58 RID: 19032
			public static readonly FunctionValue IsDistinct = new Library.List.IsDistinctFunctionValue();

			// Token: 0x04004A59 RID: 19033
			public static readonly FunctionValue Last = new Library.List.LastFunctionValue();

			// Token: 0x04004A5A RID: 19034
			public static readonly FunctionValue RemoveMatchingItems = new Library.List.RemoveMatchingItemsFunctionValue();

			// Token: 0x04004A5B RID: 19035
			public static readonly FunctionValue RemoveNulls = new Library.List.RemoveNullsFunctionValue();

			// Token: 0x04004A5C RID: 19036
			public static readonly FunctionValue Repeat = new Library.List.RepeatFunctionValue();

			// Token: 0x04004A5D RID: 19037
			public static readonly FunctionValue ReplaceMatchingItems = new Library.List.ReplaceMatchingItemsFunctionValue();

			// Token: 0x04004A5E RID: 19038
			public static readonly FunctionValue Reverse = new Library.List.ReverseFunctionValue();

			// Token: 0x04004A5F RID: 19039
			public static readonly FunctionValue Single = new Library.List.SingleFunctionValue();

			// Token: 0x04004A60 RID: 19040
			public static readonly FunctionValue SingleOrDefault = new Library.List.SingleOrDefaultFunctionValue();

			// Token: 0x04004A61 RID: 19041
			public static readonly FunctionValue SkipWhile = new Library.List.SkipWhileFunctionValue();

			// Token: 0x04004A62 RID: 19042
			public static readonly FunctionValue Union = new Library.List.UnionFunctionValue();

			// Token: 0x04004A63 RID: 19043
			public static readonly FunctionValue Accumulate = new Library.List.AccumulateFunctionValue();

			// Token: 0x04004A64 RID: 19044
			public static readonly FunctionValue Buffer = new Library.List.BufferFunctionValue();

			// Token: 0x04004A65 RID: 19045
			public static readonly FunctionValue Combine = new Library.List.CombineFunctionValue();

			// Token: 0x04004A66 RID: 19046
			public static readonly FunctionValue ContainsAll = new Library.List.ContainsAllFunctionValue();

			// Token: 0x04004A67 RID: 19047
			public static readonly FunctionValue ContainsAny = new Library.List.ContainsAnyFunctionValue();

			// Token: 0x04004A68 RID: 19048
			public static readonly FunctionValue ElementWithListCheck = new Library.List.ElementWithListCheckFunctionValue();

			// Token: 0x04004A69 RID: 19049
			public static readonly FunctionValue ElementOrNullWithListCheck = new Library.List.ElementOrNullWithListCheckFunctionValue();

			// Token: 0x04004A6A RID: 19050
			public static readonly FunctionValue InsertRange = new Library.List.InsertRangeFunctionValue();

			// Token: 0x04004A6B RID: 19051
			public static readonly FunctionValue Min = new Library.List.MinMaxFunctionValue(false);

			// Token: 0x04004A6C RID: 19052
			public static readonly FunctionValue MinN = new Library.List.MinMaxNFunctionValue(false);

			// Token: 0x04004A6D RID: 19053
			public static readonly FunctionValue Max = new Library.List.MinMaxFunctionValue(true);

			// Token: 0x04004A6E RID: 19054
			public static readonly FunctionValue MaxN = new Library.List.MinMaxNFunctionValue(true);

			// Token: 0x04004A6F RID: 19055
			public static readonly FunctionValue PositionOf = new Library.List.PositionOfFunctionValue();

			// Token: 0x04004A70 RID: 19056
			public static readonly FunctionValue PositionOfAny = new Library.List.PositionOfAnyFunctionValue();

			// Token: 0x04004A71 RID: 19057
			public static readonly FunctionValue Positions = new Library.List.PositionsFunctionValue();

			// Token: 0x04004A72 RID: 19058
			public static readonly FunctionValue RemoveRange = new Library.List.RemoveRangeFunctionValue();

			// Token: 0x04004A73 RID: 19059
			public static readonly FunctionValue ReplaceRange = new Library.List.ReplaceRangeFunctionValue();

			// Token: 0x04004A74 RID: 19060
			public static readonly FunctionValue Alternate = new Library.List.AlternateFunctionValue();

			// Token: 0x04004A75 RID: 19061
			public static readonly FunctionValue Zip = new Library.List.ZipFunctionValue();

			// Token: 0x04004A76 RID: 19062
			public static readonly FunctionValue Split = new Library.List.SplitFunctionValue();

			// Token: 0x04004A77 RID: 19063
			public static readonly FunctionValue Average = new Library.List.AverageFunctionValue();

			// Token: 0x04004A78 RID: 19064
			public static readonly FunctionValue Covariance = new Library.List.CovarianceFunctionValue();

			// Token: 0x04004A79 RID: 19065
			public static readonly FunctionValue Histogram = new Library.List.HistogramFunctionValue();

			// Token: 0x04004A7A RID: 19066
			public static readonly FunctionValue Mode = new Library.List.ModeFunctionValue();

			// Token: 0x04004A7B RID: 19067
			public static readonly FunctionValue Modes = new Library.List.ModesFunctionValue();

			// Token: 0x04004A7C RID: 19068
			public static readonly FunctionValue Percentile = new Library.List.PercentileFunctionValue();

			// Token: 0x04004A7D RID: 19069
			public static readonly FunctionValue Product = new Library.List.ProductFunctionValue();

			// Token: 0x04004A7E RID: 19070
			public static readonly FunctionValue Sum = new Library.List.SumFunctionValue();

			// Token: 0x04004A7F RID: 19071
			public static readonly FunctionValue StandardDeviation = new Library.List.StandardDeviationFunctionValue();

			// Token: 0x04004A80 RID: 19072
			public static readonly FunctionValue Median = new Library.List.MedianFunctionValue();

			// Token: 0x04004A81 RID: 19073
			public static readonly FunctionValue Numbers = new Library.List.NumbersFunctionValue();

			// Token: 0x04004A82 RID: 19074
			public static readonly FunctionValue Times = new Library.List.TimesFunctionValue();

			// Token: 0x04004A83 RID: 19075
			public static readonly FunctionValue Dates = new Library.List.DatesFunctionValue();

			// Token: 0x04004A84 RID: 19076
			public static readonly FunctionValue DateTimes = new Library.List.DateTimesFunctionValue();

			// Token: 0x04004A85 RID: 19077
			public static readonly FunctionValue DateTimeZones = new Library.List.DateTimeZonesFunctionValue();

			// Token: 0x04004A86 RID: 19078
			public static readonly FunctionValue Durations = new Library.List.DurationsFunctionValue();

			// Token: 0x04004A87 RID: 19079
			public static readonly FunctionValue AllTrue = new Library.List.AllTrueFunctionValue();

			// Token: 0x04004A88 RID: 19080
			public static readonly FunctionValue AnyTrue = new Library.List.AnyTrueFunctionValue();

			// Token: 0x04004A89 RID: 19081
			public static readonly FunctionValue CountOfDistinct = new Library.List.CountOfDistinctFunctionValue();

			// Token: 0x04004A8A RID: 19082
			public static readonly FunctionValue CountOfNull = new Library.List.CountOfNullFunctionValue();

			// Token: 0x04004A8B RID: 19083
			public static readonly FunctionValue CountOfNotNull = new Library.List.CountOfNotNullFunctionValue();

			// Token: 0x04004A8C RID: 19084
			public static readonly FunctionValue CountOfDistinctNull = new Library.List.CountOfDistinctNullFunctionValue();

			// Token: 0x04004A8D RID: 19085
			public static readonly FunctionValue CountOfDistinctNotNull = new Library.List.CountOfDistinctNotNullFunctionValue();

			// Token: 0x04004A8E RID: 19086
			public static readonly FunctionValue TakeWhile = new Library.List.TakeWhileFunctionValue();

			// Token: 0x020014AD RID: 5293
			private class AllTrueFunctionValue : NativeFunctionValue1<LogicalValue, ListValue>
			{
				// Token: 0x060085E9 RID: 34281 RVA: 0x001C82D7 File Offset: 0x001C64D7
				public AllTrueFunctionValue()
					: base(TypeValue.Logical, "list", TypeValue.List)
				{
				}

				// Token: 0x060085EA RID: 34282 RVA: 0x001C82F0 File Offset: 0x001C64F0
				public override LogicalValue TypedInvoke(ListValue list)
				{
					bool flag = true;
					foreach (IValueReference valueReference in list)
					{
						if (!valueReference.Value.IsNull)
						{
							flag &= valueReference.Value.AsBoolean;
						}
					}
					return LogicalValue.New(flag);
				}
			}

			// Token: 0x020014AE RID: 5294
			private class AnyTrueFunctionValue : NativeFunctionValue1<LogicalValue, ListValue>
			{
				// Token: 0x060085EB RID: 34283 RVA: 0x001C82D7 File Offset: 0x001C64D7
				public AnyTrueFunctionValue()
					: base(TypeValue.Logical, "list", TypeValue.List)
				{
				}

				// Token: 0x060085EC RID: 34284 RVA: 0x001C8354 File Offset: 0x001C6554
				public override LogicalValue TypedInvoke(ListValue list)
				{
					bool flag = false;
					foreach (IValueReference valueReference in list)
					{
						if (!valueReference.Value.IsNull)
						{
							flag |= valueReference.Value.AsBoolean;
						}
					}
					return LogicalValue.New(flag);
				}
			}

			// Token: 0x020014AF RID: 5295
			private class RepeatFunctionValue : NativeFunctionValue2<ListValue, ListValue, NumberValue>
			{
				// Token: 0x060085ED RID: 34285 RVA: 0x001C83B8 File Offset: 0x001C65B8
				public RepeatFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "count", TypeValue.Number)
				{
				}

				// Token: 0x060085EE RID: 34286 RVA: 0x001C83D9 File Offset: 0x001C65D9
				public override ListValue TypedInvoke(ListValue list, NumberValue count)
				{
					if (count.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					return new Library.List.RepeatFunctionValue.RepeatListValue(list, count.AsInteger32);
				}

				// Token: 0x020014B0 RID: 5296
				private class RepeatListValue : StreamedListValue
				{
					// Token: 0x060085EF RID: 34287 RVA: 0x001C8400 File Offset: 0x001C6600
					public RepeatListValue(ListValue list, int count)
					{
						this.list = list;
						this.count = ((count < 0) ? 0 : count);
					}

					// Token: 0x060085F0 RID: 34288 RVA: 0x001C841D File Offset: 0x001C661D
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.RepeatFunctionValue.RepeatListValue.RepeatEnumerator(this.list, this.count);
					}

					// Token: 0x04004A8F RID: 19087
					private readonly ListValue list;

					// Token: 0x04004A90 RID: 19088
					private readonly int count;

					// Token: 0x020014B1 RID: 5297
					private class RepeatEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x060085F1 RID: 34289 RVA: 0x001C8430 File Offset: 0x001C6630
						public RepeatEnumerator(ListValue list, int count)
						{
							this.list = list;
							this.count = count;
							this.enumerator = ListValue.Empty.GetEnumerator();
						}

						// Token: 0x17002379 RID: 9081
						// (get) Token: 0x060085F2 RID: 34290 RVA: 0x001C8456 File Offset: 0x001C6656
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x1700237A RID: 9082
						// (get) Token: 0x060085F3 RID: 34291 RVA: 0x001C8463 File Offset: 0x001C6663
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x060085F4 RID: 34292 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060085F5 RID: 34293 RVA: 0x001C846B File Offset: 0x001C666B
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x060085F6 RID: 34294 RVA: 0x001C8478 File Offset: 0x001C6678
						public bool MoveNext()
						{
							while (!this.enumerator.MoveNext())
							{
								if (this.count == 0)
								{
									return false;
								}
								this.enumerator.Dispose();
								this.enumerator = this.list.GetEnumerator();
								this.count--;
							}
							return true;
						}

						// Token: 0x04004A91 RID: 19089
						private readonly ListValue list;

						// Token: 0x04004A92 RID: 19090
						private int count;

						// Token: 0x04004A93 RID: 19091
						private IEnumerator<IValueReference> enumerator;
					}
				}
			}

			// Token: 0x020014B2 RID: 5298
			private class BufferFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x060085F7 RID: 34295 RVA: 0x001C84C9 File Offset: 0x001C66C9
				public BufferFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List)
				{
				}

				// Token: 0x060085F8 RID: 34296 RVA: 0x001C84E0 File Offset: 0x001C66E0
				public override ListValue TypedInvoke(ListValue list)
				{
					return ListValue.New(list.ToArray<IValueReference>()).NewMeta(list.MetaValue).NewType(list.Type)
						.AsList;
				}
			}

			// Token: 0x020014B3 RID: 5299
			private class CombineFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x060085F9 RID: 34297 RVA: 0x001C8508 File Offset: 0x001C6708
				public CombineFunctionValue()
					: base(TypeValue.List, "lists", TypeValue.List)
				{
				}

				// Token: 0x060085FA RID: 34298 RVA: 0x001C8520 File Offset: 0x001C6720
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					TypeValue type = environment.GetType(arguments[0]);
					if (!type.IsListType)
					{
						return TypeValue.List;
					}
					return type.AsListType.ItemType;
				}

				// Token: 0x060085FB RID: 34299 RVA: 0x001C8554 File Offset: 0x001C6754
				public override ListValue TypedInvoke(ListValue lists)
				{
					return ListValue.Combine(lists);
				}
			}

			// Token: 0x020014B4 RID: 5300
			private class ReverseFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x060085FC RID: 34300 RVA: 0x001C84C9 File Offset: 0x001C66C9
				public ReverseFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List)
				{
				}

				// Token: 0x060085FD RID: 34301 RVA: 0x001C855C File Offset: 0x001C675C
				public override ListValue TypedInvoke(ListValue list)
				{
					return new Library.List.ReverseFunctionValue.LazyListValue(delegate
					{
						IValueReference[] array = list.ToArray<IValueReference>();
						for (int i = 0; i < array.Length / 2; i++)
						{
							IValueReference valueReference = array[i];
							array[i] = array[array.Length - i - 1];
							array[array.Length - i - 1] = valueReference;
						}
						return ListValue.New(array);
					});
				}

				// Token: 0x020014B5 RID: 5301
				private class LazyListValue : ListValue
				{
					// Token: 0x060085FE RID: 34302 RVA: 0x001C857A File Offset: 0x001C677A
					public LazyListValue(Func<ListValue> factory)
					{
						this.factory = factory;
					}

					// Token: 0x1700237B RID: 9083
					// (get) Token: 0x060085FF RID: 34303 RVA: 0x001C8589 File Offset: 0x001C6789
					private ListValue List
					{
						get
						{
							if (this.list == null)
							{
								this.list = this.factory();
							}
							return this.list;
						}
					}

					// Token: 0x06008600 RID: 34304 RVA: 0x001C85AA File Offset: 0x001C67AA
					public override bool TryGetProcessor(out QueryProcessor processor)
					{
						return this.List.TryGetProcessor(out processor);
					}

					// Token: 0x1700237C RID: 9084
					// (get) Token: 0x06008601 RID: 34305 RVA: 0x001C85B8 File Offset: 0x001C67B8
					public override bool IsBuffered
					{
						get
						{
							return this.list != null && this.list.IsBuffered;
						}
					}

					// Token: 0x1700237D RID: 9085
					// (get) Token: 0x06008602 RID: 34306 RVA: 0x001C85CF File Offset: 0x001C67CF
					public override int Count
					{
						get
						{
							return this.List.Count;
						}
					}

					// Token: 0x1700237E RID: 9086
					// (get) Token: 0x06008603 RID: 34307 RVA: 0x001C85DC File Offset: 0x001C67DC
					public override long LargeCount
					{
						get
						{
							return this.List.LargeCount;
						}
					}

					// Token: 0x06008604 RID: 34308 RVA: 0x001C85E9 File Offset: 0x001C67E9
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return this.List.GetEnumerator();
					}

					// Token: 0x1700237F RID: 9087
					public override Value this[int key]
					{
						get
						{
							return this.List[key];
						}
					}

					// Token: 0x17002380 RID: 9088
					public override Value this[Value key]
					{
						get
						{
							return this.List[key];
						}
					}

					// Token: 0x06008607 RID: 34311 RVA: 0x001C8612 File Offset: 0x001C6812
					public override bool TryGetValue(Value key, out Value value)
					{
						return this.List.TryGetValue(key, out value);
					}

					// Token: 0x06008608 RID: 34312 RVA: 0x001C8621 File Offset: 0x001C6821
					public override IValueReference GetReference(int index)
					{
						return this.List.GetReference(index);
					}

					// Token: 0x04004A94 RID: 19092
					private readonly Func<ListValue> factory;

					// Token: 0x04004A95 RID: 19093
					private ListValue list;
				}
			}

			// Token: 0x020014B7 RID: 5303
			private class TakeWhileFunctionValue : NativeFunctionValue2<ListValue, ListValue, FunctionValue>
			{
				// Token: 0x0600860B RID: 34315 RVA: 0x001C8679 File Offset: 0x001C6879
				public TakeWhileFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "whilst", TypeValue.Function)
				{
				}

				// Token: 0x0600860C RID: 34316 RVA: 0x001C869A File Offset: 0x001C689A
				public override ListValue TypedInvoke(ListValue list, FunctionValue whilst)
				{
					return new Library.List.TakeWhileFunctionValue.TakeWhileListValue(list, whilst);
				}

				// Token: 0x020014B8 RID: 5304
				private class TakeWhileListValue : StreamedListValue
				{
					// Token: 0x0600860D RID: 34317 RVA: 0x001C86A3 File Offset: 0x001C68A3
					public TakeWhileListValue(ListValue list, FunctionValue whilst)
					{
						this.list = list;
						this.whilst = whilst;
					}

					// Token: 0x17002381 RID: 9089
					// (get) Token: 0x0600860E RID: 34318 RVA: 0x001C86B9 File Offset: 0x001C68B9
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x0600860F RID: 34319 RVA: 0x001C86C6 File Offset: 0x001C68C6
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.TakeWhileFunctionValue.TakeWhileListValue.TakeWhileEnumerator(this.list.GetEnumerator(), this.whilst);
					}

					// Token: 0x04004A97 RID: 19095
					private readonly ListValue list;

					// Token: 0x04004A98 RID: 19096
					private readonly FunctionValue whilst;

					// Token: 0x020014B9 RID: 5305
					private class TakeWhileEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06008610 RID: 34320 RVA: 0x001C86DE File Offset: 0x001C68DE
						public TakeWhileEnumerator(IEnumerator<IValueReference> enumerator, FunctionValue whilst)
						{
							this.enumerator = enumerator;
							this.whilst = whilst;
						}

						// Token: 0x17002382 RID: 9090
						// (get) Token: 0x06008611 RID: 34321 RVA: 0x001C86F4 File Offset: 0x001C68F4
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x17002383 RID: 9091
						// (get) Token: 0x06008612 RID: 34322 RVA: 0x001C8701 File Offset: 0x001C6901
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008613 RID: 34323 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008614 RID: 34324 RVA: 0x001C8709 File Offset: 0x001C6909
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06008615 RID: 34325 RVA: 0x001C8716 File Offset: 0x001C6916
						public bool MoveNext()
						{
							return this.enumerator.MoveNext() && this.whilst.Invoke(this.enumerator.Current.Value).AsBoolean;
						}

						// Token: 0x04004A99 RID: 19097
						private readonly IEnumerator<IValueReference> enumerator;

						// Token: 0x04004A9A RID: 19098
						private readonly FunctionValue whilst;
					}
				}
			}

			// Token: 0x020014BA RID: 5306
			private class SkipWhileFunctionValue : NativeFunctionValue2<ListValue, ListValue, FunctionValue>
			{
				// Token: 0x06008616 RID: 34326 RVA: 0x001C8679 File Offset: 0x001C6879
				public SkipWhileFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "whilst", TypeValue.Function)
				{
				}

				// Token: 0x06008617 RID: 34327 RVA: 0x001C874C File Offset: 0x001C694C
				public override ListValue TypedInvoke(ListValue list, FunctionValue whilst)
				{
					return new Library.List.SkipWhileFunctionValue.SkipWhileListValue(list, whilst);
				}

				// Token: 0x020014BB RID: 5307
				private class SkipWhileListValue : StreamedListValue
				{
					// Token: 0x06008618 RID: 34328 RVA: 0x001C8755 File Offset: 0x001C6955
					public SkipWhileListValue(ListValue list, FunctionValue whilst)
					{
						this.list = list;
						this.whilst = whilst;
					}

					// Token: 0x17002384 RID: 9092
					// (get) Token: 0x06008619 RID: 34329 RVA: 0x001C876B File Offset: 0x001C696B
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x0600861A RID: 34330 RVA: 0x001C8778 File Offset: 0x001C6978
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.SkipWhileFunctionValue.SkipWhileListValue.SkipWhileEnumerator(this.list.GetEnumerator(), this.whilst);
					}

					// Token: 0x04004A9B RID: 19099
					private readonly ListValue list;

					// Token: 0x04004A9C RID: 19100
					private readonly FunctionValue whilst;

					// Token: 0x020014BC RID: 5308
					private class SkipWhileEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600861B RID: 34331 RVA: 0x001C8790 File Offset: 0x001C6990
						public SkipWhileEnumerator(IEnumerator<IValueReference> enumerator, FunctionValue whilst)
						{
							this.enumerator = enumerator;
							this.whilst = whilst;
						}

						// Token: 0x17002385 RID: 9093
						// (get) Token: 0x0600861C RID: 34332 RVA: 0x001C87A6 File Offset: 0x001C69A6
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x17002386 RID: 9094
						// (get) Token: 0x0600861D RID: 34333 RVA: 0x001C87B3 File Offset: 0x001C69B3
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600861E RID: 34334 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x0600861F RID: 34335 RVA: 0x001C87BB File Offset: 0x001C69BB
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06008620 RID: 34336 RVA: 0x001C87C8 File Offset: 0x001C69C8
						public bool MoveNext()
						{
							if (this.whilst != null)
							{
								while (this.enumerator.MoveNext())
								{
									if (!this.whilst.Invoke(this.enumerator.Current.Value).AsBoolean)
									{
										this.whilst = null;
										return true;
									}
								}
								this.whilst = null;
								return false;
							}
							return this.enumerator.MoveNext();
						}

						// Token: 0x04004A9D RID: 19101
						private readonly IEnumerator<IValueReference> enumerator;

						// Token: 0x04004A9E RID: 19102
						private FunctionValue whilst;
					}
				}
			}

			// Token: 0x020014BD RID: 5309
			private class FirstFunctionValue : NativeFunctionValue2<Value, ListValue, Value>, IAccumulableFunction
			{
				// Token: 0x06008621 RID: 34337 RVA: 0x001C882B File Offset: 0x001C6A2B
				public FirstFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "defaultValue", TypeValue.Any)
				{
				}

				// Token: 0x17002387 RID: 9095
				// (get) Token: 0x06008622 RID: 34338 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06008623 RID: 34339 RVA: 0x001C8854 File Offset: 0x001C6A54
				public override Value TypedInvoke(ListValue list, Value defaultValue)
				{
					Value value;
					using (IEnumerator<IValueReference> enumerator = LanguageLibrary.List.Take.Invoke(list, NumberValue.One).AsList.GetEnumerator())
					{
						if (!enumerator.MoveNext())
						{
							value = defaultValue;
						}
						else
						{
							value = enumerator.Current.Value;
						}
					}
					return value;
				}

				// Token: 0x06008624 RID: 34340 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06008625 RID: 34341 RVA: 0x001C88B4 File Offset: 0x001C6AB4
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.FirstFunctionValue.FirstAccumulable(arguments["defaultValue"]);
				}

				// Token: 0x04004A9F RID: 19103
				private const string enumerableParameter = "list";

				// Token: 0x020014BE RID: 5310
				private sealed class FirstAccumulable : IAccumulable
				{
					// Token: 0x06008626 RID: 34342 RVA: 0x001C88C6 File Offset: 0x001C6AC6
					public FirstAccumulable(Value defaultValue)
					{
						this.defaultValue = defaultValue;
					}

					// Token: 0x06008627 RID: 34343 RVA: 0x001C88D5 File Offset: 0x001C6AD5
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.FirstFunctionValue.FirstAccumulable.FirstAccumulator(this);
					}

					// Token: 0x04004AA0 RID: 19104
					private readonly Value defaultValue;

					// Token: 0x020014BF RID: 5311
					private sealed class FirstAccumulator : IAccumulator
					{
						// Token: 0x06008628 RID: 34344 RVA: 0x001C88DD File Offset: 0x001C6ADD
						public FirstAccumulator(Library.List.FirstFunctionValue.FirstAccumulable accumulable)
						{
							this.defaultValue = accumulable.defaultValue;
						}

						// Token: 0x17002388 RID: 9096
						// (get) Token: 0x06008629 RID: 34345 RVA: 0x001C88F1 File Offset: 0x001C6AF1
						public IValueReference Current
						{
							get
							{
								return this.first ?? this.defaultValue;
							}
						}

						// Token: 0x0600862A RID: 34346 RVA: 0x001C8903 File Offset: 0x001C6B03
						public void AccumulateNext(IValueReference next)
						{
							this.first = this.first ?? next;
						}

						// Token: 0x04004AA1 RID: 19105
						private readonly Value defaultValue;

						// Token: 0x04004AA2 RID: 19106
						private IValueReference first;
					}
				}
			}

			// Token: 0x020014C0 RID: 5312
			private class InsertRangeFunctionValue : NativeFunctionValue3<ListValue, ListValue, NumberValue, ListValue>
			{
				// Token: 0x0600862B RID: 34347 RVA: 0x001C8916 File Offset: 0x001C6B16
				public InsertRangeFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "index", TypeValue.Number, "values", TypeValue.List)
				{
				}

				// Token: 0x0600862C RID: 34348 RVA: 0x001C8941 File Offset: 0x001C6B41
				public override ListValue TypedInvoke(ListValue list, NumberValue index, ListValue values)
				{
					return Library.List.ReplaceRange.Invoke(list, index, NumberValue.Zero, values).AsList;
				}
			}

			// Token: 0x020014C1 RID: 5313
			private class RemoveRangeFunctionValue : NativeFunctionValue3<ListValue, ListValue, NumberValue, Value>
			{
				// Token: 0x0600862D RID: 34349 RVA: 0x001C895C File Offset: 0x001C6B5C
				public RemoveRangeFunctionValue()
					: base(TypeValue.List, 2, "list", TypeValue.List, "index", TypeValue.Number, "count", NullableTypeValue.Number)
				{
				}

				// Token: 0x0600862E RID: 34350 RVA: 0x001C8993 File Offset: 0x001C6B93
				public override ListValue TypedInvoke(ListValue list, NumberValue index, Value count)
				{
					if (count.IsNull)
					{
						count = NumberValue.One;
					}
					return Library.List.ReplaceRange.Invoke(list, index, count, ListValue.Empty).AsList;
				}
			}

			// Token: 0x020014C2 RID: 5314
			private class ReplaceRangeFunctionValue : NativeFunctionValue4<ListValue, ListValue, NumberValue, NumberValue, ListValue>
			{
				// Token: 0x0600862F RID: 34351 RVA: 0x001C89BC File Offset: 0x001C6BBC
				public ReplaceRangeFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "index", TypeValue.Number, "count", TypeValue.Number, "replaceWith", TypeValue.List)
				{
				}

				// Token: 0x06008630 RID: 34352 RVA: 0x001C89FC File Offset: 0x001C6BFC
				public override ListValue TypedInvoke(ListValue list, NumberValue index, NumberValue count, ListValue replaceWith)
				{
					if (index.LessThan(NumberValue.Zero))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.List_IndexCannotBeNegative, index, null);
					}
					if (count.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					return new Library.List.ReplaceRangeFunctionValue.ReplaceListValue(list, index.AsInteger32, count.AsInteger32, replaceWith);
				}

				// Token: 0x020014C3 RID: 5315
				private class ReplaceListValue : StreamedListValue
				{
					// Token: 0x06008631 RID: 34353 RVA: 0x001C8A50 File Offset: 0x001C6C50
					public ReplaceListValue(ListValue list, int index, int count, ListValue replace)
					{
						this.list = list;
						this.index = index;
						this.count = count;
						this.replace = replace;
					}

					// Token: 0x06008632 RID: 34354 RVA: 0x001C8A75 File Offset: 0x001C6C75
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator(this.list, this.index, this.count, this.replace);
					}

					// Token: 0x04004AA3 RID: 19107
					private readonly ListValue list;

					// Token: 0x04004AA4 RID: 19108
					private readonly int index;

					// Token: 0x04004AA5 RID: 19109
					private readonly int count;

					// Token: 0x04004AA6 RID: 19110
					private readonly ListValue replace;

					// Token: 0x020014C4 RID: 5316
					private class ReplaceEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06008633 RID: 34355 RVA: 0x001C8A94 File Offset: 0x001C6C94
						public ReplaceEnumerator(ListValue list, int index, int count, ListValue replace)
						{
							this.list = list;
							this.listEnumerator = list.GetEnumerator();
							this.index = index;
							this.count = count;
							this.replaceEnumerator = replace.GetEnumerator();
							this.state = Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Head;
						}

						// Token: 0x17002389 RID: 9097
						// (get) Token: 0x06008634 RID: 34356 RVA: 0x001C8AD1 File Offset: 0x001C6CD1
						public IValueReference Current
						{
							get
							{
								if (this.state != Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Replace)
								{
									return this.listEnumerator.Current;
								}
								return this.replaceEnumerator.Current;
							}
						}

						// Token: 0x1700238A RID: 9098
						// (get) Token: 0x06008635 RID: 34357 RVA: 0x001C8AF3 File Offset: 0x001C6CF3
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008636 RID: 34358 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008637 RID: 34359 RVA: 0x001C8AFB File Offset: 0x001C6CFB
						public void Dispose()
						{
							this.listEnumerator.Dispose();
							this.replaceEnumerator.Dispose();
						}

						// Token: 0x06008638 RID: 34360 RVA: 0x001C8B14 File Offset: 0x001C6D14
						public bool MoveNext()
						{
							for (;;)
							{
								switch (this.state)
								{
								case Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Head:
								{
									if (this.index != 0)
									{
										goto Block_1;
									}
									for (int i = 0; i < this.count; i++)
									{
										if (!this.listEnumerator.MoveNext())
										{
											goto Block_3;
										}
									}
									this.state = Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Replace;
									continue;
								}
								case Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Replace:
									if (this.replaceEnumerator.MoveNext())
									{
										return true;
									}
									this.state = Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Tail;
									continue;
								case Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState.Tail:
									goto IL_00A0;
								}
								break;
							}
							throw new InvalidOperationException();
							Block_1:
							if (this.listEnumerator.MoveNext())
							{
								this.index--;
								return true;
							}
							throw ValueException.InsufficientElements(this.list);
							Block_3:
							throw ValueException.InsufficientElements(this.list);
							IL_00A0:
							return this.listEnumerator.MoveNext();
						}

						// Token: 0x04004AA7 RID: 19111
						private readonly ListValue list;

						// Token: 0x04004AA8 RID: 19112
						private readonly IEnumerator<IValueReference> listEnumerator;

						// Token: 0x04004AA9 RID: 19113
						private int index;

						// Token: 0x04004AAA RID: 19114
						private readonly int count;

						// Token: 0x04004AAB RID: 19115
						private readonly IEnumerator<IValueReference> replaceEnumerator;

						// Token: 0x04004AAC RID: 19116
						private Library.List.ReplaceRangeFunctionValue.ReplaceListValue.ReplaceEnumerator.ReplaceState state;

						// Token: 0x020014C5 RID: 5317
						private enum ReplaceState
						{
							// Token: 0x04004AAE RID: 19118
							Head,
							// Token: 0x04004AAF RID: 19119
							Replace,
							// Token: 0x04004AB0 RID: 19120
							Tail
						}
					}
				}
			}

			// Token: 0x020014C6 RID: 5318
			private class LastFunctionValue : NativeFunctionValue2<Value, ListValue, Value>, IAccumulableFunction
			{
				// Token: 0x06008639 RID: 34361 RVA: 0x001C882B File Offset: 0x001C6A2B
				public LastFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "defaultValue", TypeValue.Any)
				{
				}

				// Token: 0x1700238B RID: 9099
				// (get) Token: 0x0600863A RID: 34362 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x0600863B RID: 34363 RVA: 0x001C8BD2 File Offset: 0x001C6DD2
				public override Value TypedInvoke(ListValue list, Value defaultValue)
				{
					IAccumulator accumulator = new Library.List.LastFunctionValue.LastAccumulable(defaultValue).CreateAccumulator();
					accumulator.AccumulateRange(list);
					return accumulator.Current.Value;
				}

				// Token: 0x0600863C RID: 34364 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x0600863D RID: 34365 RVA: 0x001C8BF0 File Offset: 0x001C6DF0
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.LastFunctionValue.LastAccumulable(arguments["defaultValue"]);
				}

				// Token: 0x04004AB1 RID: 19121
				private const string enumerableParameter = "list";

				// Token: 0x020014C7 RID: 5319
				private sealed class LastAccumulable : IAccumulable
				{
					// Token: 0x0600863E RID: 34366 RVA: 0x001C8C02 File Offset: 0x001C6E02
					public LastAccumulable(Value defaultValue)
					{
						this.defaultValue = defaultValue;
					}

					// Token: 0x0600863F RID: 34367 RVA: 0x001C8C11 File Offset: 0x001C6E11
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.LastFunctionValue.LastAccumulable.LastAccumulator(this);
					}

					// Token: 0x04004AB2 RID: 19122
					private readonly Value defaultValue;

					// Token: 0x020014C8 RID: 5320
					private sealed class LastAccumulator : IAccumulator
					{
						// Token: 0x06008640 RID: 34368 RVA: 0x001C8C19 File Offset: 0x001C6E19
						public LastAccumulator(Library.List.LastFunctionValue.LastAccumulable accumulable)
						{
							this.defaultValue = accumulable.defaultValue;
						}

						// Token: 0x1700238C RID: 9100
						// (get) Token: 0x06008641 RID: 34369 RVA: 0x001C8C2D File Offset: 0x001C6E2D
						public IValueReference Current
						{
							get
							{
								return this.last ?? this.defaultValue;
							}
						}

						// Token: 0x06008642 RID: 34370 RVA: 0x001C8C3F File Offset: 0x001C6E3F
						public void AccumulateNext(IValueReference next)
						{
							this.last = next;
						}

						// Token: 0x04004AB3 RID: 19123
						private readonly Value defaultValue;

						// Token: 0x04004AB4 RID: 19124
						private IValueReference last;
					}
				}
			}

			// Token: 0x020014C9 RID: 5321
			private class SingleFunctionValue : NativeFunctionValue1<Value, ListValue>
			{
				// Token: 0x06008643 RID: 34371 RVA: 0x001C8C48 File Offset: 0x001C6E48
				public SingleFunctionValue()
					: base(TypeValue.Any, "list", TypeValue.List)
				{
				}

				// Token: 0x06008644 RID: 34372 RVA: 0x001C8C60 File Offset: 0x001C6E60
				public override Value TypedInvoke(ListValue list)
				{
					IValueReference valueReference = null;
					foreach (IValueReference valueReference2 in list)
					{
						if (valueReference != null)
						{
							throw ValueException.TooManyElements(list);
						}
						valueReference = valueReference2;
					}
					if (valueReference == null)
					{
						throw ValueException.InsufficientElements(list);
					}
					return valueReference.Value;
				}
			}

			// Token: 0x020014CA RID: 5322
			private class SingleOrDefaultFunctionValue : NativeFunctionValue2<Value, ListValue, Value>
			{
				// Token: 0x06008645 RID: 34373 RVA: 0x001C8CC0 File Offset: 0x001C6EC0
				public SingleOrDefaultFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "default", TypeValue.Any)
				{
				}

				// Token: 0x06008646 RID: 34374 RVA: 0x001C8CE4 File Offset: 0x001C6EE4
				public override Value TypedInvoke(ListValue list, Value defaultValue)
				{
					IValueReference valueReference = null;
					foreach (IValueReference valueReference2 in list)
					{
						if (valueReference != null)
						{
							throw ValueException.TooManyElements(list);
						}
						valueReference = valueReference2;
					}
					if (valueReference == null)
					{
						return defaultValue;
					}
					return valueReference.Value;
				}
			}

			// Token: 0x020014CB RID: 5323
			private class DensityFunctionValue : NativeFunctionValue2<TableValue, ListValue, Value>
			{
				// Token: 0x06008647 RID: 34375 RVA: 0x001C8D3C File Offset: 0x001C6F3C
				public DensityFunctionValue()
					: base(TypeValue.Table, 1, "list", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06008648 RID: 34376 RVA: 0x001C8D60 File Offset: 0x001C6F60
				public override TableValue TypedInvoke(ListValue list, Value equationCriteria)
				{
					Dictionary<Value, Value> dictionary = new Dictionary<Value, Value>(Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true));
					foreach (IValueReference valueReference in list)
					{
						Value value = valueReference.Value;
						Value value2;
						if (dictionary.TryGetValue(value, out value2))
						{
							dictionary[value] = dictionary[value].Add(NumberValue.One);
						}
						else
						{
							dictionary[value] = NumberValue.One;
						}
					}
					Value[] array = new Value[dictionary.Count];
					int num = 0;
					foreach (KeyValuePair<Value, Value> keyValuePair in dictionary)
					{
						array[num++] = RecordValue.New(Library.List.DensityFunctionValue.keys, new Value[] { keyValuePair.Key, keyValuePair.Value });
					}
					return ListValue.New(array).ToTable(Library.List.DensityFunctionValue.tableType);
				}

				// Token: 0x04004AB5 RID: 19125
				private static readonly Keys keys = Keys.New("Value", "Count");

				// Token: 0x04004AB6 RID: 19126
				private static readonly TableTypeValue tableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(Library.List.DensityFunctionValue.keys, new Value[]
				{
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Any,
						LogicalValue.False
					}),
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Number,
						LogicalValue.False
					})
				}), false));
			}

			// Token: 0x020014CC RID: 5324
			private class PositionsFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x0600864A RID: 34378 RVA: 0x001C84C9 File Offset: 0x001C66C9
				public PositionsFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List)
				{
				}

				// Token: 0x0600864B RID: 34379 RVA: 0x001C8EF3 File Offset: 0x001C70F3
				public override ListValue TypedInvoke(ListValue list)
				{
					return new RangeListValue(0, list.Count);
				}
			}

			// Token: 0x020014CD RID: 5325
			private abstract class PositionOfBaseFunctionValue<TInput> : NativeFunctionValue4<Value, ListValue, TInput, Value, Value> where TInput : Value
			{
				// Token: 0x0600864C RID: 34380 RVA: 0x001C8F04 File Offset: 0x001C7104
				protected PositionOfBaseFunctionValue(string inputName, TypeValue inputType)
					: base(TypeValue.Any, 2, "list", TypeValue.List, inputName, inputType, "occurrence", Library.Occurrence.Type.Nullable, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x0600864D RID: 34381 RVA: 0x001C8F44 File Offset: 0x001C7144
				public sealed override Value TypedInvoke(ListValue list, TInput value, Value occurrence, Value equationCriteria)
				{
					IEqualityComparer<Value> equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, false);
					if (occurrence.IsNull || occurrence.Equals(Library.Occurrence.First))
					{
						int num = 0;
						foreach (IValueReference valueReference in list)
						{
							if (this.Equals(equalityComparer, valueReference.Value, value))
							{
								return NumberValue.New(num);
							}
							num++;
						}
						return NumberValue.New(-1);
					}
					if (occurrence.Equals(Library.Occurrence.Last))
					{
						int num2 = -1;
						int num3 = 0;
						foreach (IValueReference valueReference2 in list)
						{
							if (this.Equals(equalityComparer, valueReference2.Value, value))
							{
								num2 = num3;
							}
							num3++;
						}
						return NumberValue.New(num2);
					}
					List<Value> list2 = new List<Value>();
					int num4 = 0;
					foreach (IValueReference valueReference3 in list)
					{
						if (this.Equals(equalityComparer, valueReference3.Value, value))
						{
							list2.Add(NumberValue.New(num4));
						}
						num4++;
					}
					return ListValue.New(list2.ToArray());
				}

				// Token: 0x0600864E RID: 34382
				protected abstract bool Equals(IEqualityComparer<Value> comparer, Value x, TInput y);
			}

			// Token: 0x020014CE RID: 5326
			private class PositionOfFunctionValue : Library.List.PositionOfBaseFunctionValue<Value>
			{
				// Token: 0x0600864F RID: 34383 RVA: 0x001C90A4 File Offset: 0x001C72A4
				public PositionOfFunctionValue()
					: base("value", TypeValue.Any)
				{
				}

				// Token: 0x06008650 RID: 34384 RVA: 0x001C90B6 File Offset: 0x001C72B6
				protected override bool Equals(IEqualityComparer<Value> comparer, Value x, Value y)
				{
					return comparer.Equals(x, y);
				}
			}

			// Token: 0x020014CF RID: 5327
			private class PositionOfAnyFunctionValue : Library.List.PositionOfBaseFunctionValue<ListValue>
			{
				// Token: 0x06008651 RID: 34385 RVA: 0x001C90C0 File Offset: 0x001C72C0
				public PositionOfAnyFunctionValue()
					: base("values", TypeValue.List)
				{
				}

				// Token: 0x06008652 RID: 34386 RVA: 0x001C90D4 File Offset: 0x001C72D4
				protected override bool Equals(IEqualityComparer<Value> comparer, Value x, ListValue y)
				{
					for (int i = 0; i < y.Count; i++)
					{
						if (comparer.Equals(x, y[i]))
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x020014D0 RID: 5328
			private class ContainsFunctionValue : NativeFunctionValue3<LogicalValue, ListValue, Value, Value>
			{
				// Token: 0x06008653 RID: 34387 RVA: 0x001C9108 File Offset: 0x001C7308
				public ContainsFunctionValue()
					: base(TypeValue.Logical, 2, "list", TypeValue.List, "value", TypeValue.Any, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06008654 RID: 34388 RVA: 0x001C9140 File Offset: 0x001C7340
				public override LogicalValue TypedInvoke(ListValue list, Value value, Value equationCriteria)
				{
					IEqualityComparer<Value> equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, false);
					foreach (IValueReference valueReference in list)
					{
						if (equalityComparer.Equals(valueReference.Value, value))
						{
							return LogicalValue.True;
						}
					}
					return LogicalValue.False;
				}
			}

			// Token: 0x020014D1 RID: 5329
			private class ContainsAllFunctionValue : NativeFunctionValue3<LogicalValue, ListValue, ListValue, Value>
			{
				// Token: 0x06008655 RID: 34389 RVA: 0x001C91A8 File Offset: 0x001C73A8
				public ContainsAllFunctionValue()
					: base(TypeValue.Logical, 2, "list", TypeValue.List, "values", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06008656 RID: 34390 RVA: 0x001C91E0 File Offset: 0x001C73E0
				public override LogicalValue TypedInvoke(ListValue list, ListValue values, Value equationCriteria)
				{
					foreach (IValueReference valueReference in values)
					{
						if (!Library.List.Contains.Invoke(list, valueReference.Value, equationCriteria).Equals(LogicalValue.True))
						{
							return LogicalValue.False;
						}
					}
					return LogicalValue.True;
				}
			}

			// Token: 0x020014D2 RID: 5330
			private class ContainsAnyFunctionValue : NativeFunctionValue3<LogicalValue, ListValue, ListValue, Value>
			{
				// Token: 0x06008657 RID: 34391 RVA: 0x001C9250 File Offset: 0x001C7450
				public ContainsAnyFunctionValue()
					: base(TypeValue.Logical, 2, "list", TypeValue.List, "values", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06008658 RID: 34392 RVA: 0x001C9288 File Offset: 0x001C7488
				public override LogicalValue TypedInvoke(ListValue list, ListValue values, Value equationCriteria)
				{
					foreach (IValueReference valueReference in values)
					{
						if (Library.List.Contains.Invoke(list, valueReference.Value, equationCriteria).AsBoolean)
						{
							return LogicalValue.True;
						}
					}
					return LogicalValue.False;
				}
			}

			// Token: 0x020014D3 RID: 5331
			private class IsDistinctFunctionValue : NativeFunctionValue2<LogicalValue, ListValue, Value>
			{
				// Token: 0x06008659 RID: 34393 RVA: 0x001C92F4 File Offset: 0x001C74F4
				public IsDistinctFunctionValue()
					: base(TypeValue.Logical, 1, "list", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x0600865A RID: 34394 RVA: 0x001C9318 File Offset: 0x001C7518
				public override LogicalValue TypedInvoke(ListValue list, Value equationCriteria)
				{
					HashSet<Value> hashSet = new HashSet<Value>(Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true));
					foreach (IValueReference valueReference in list)
					{
						if (!hashSet.Add(valueReference.Value))
						{
							return LogicalValue.False;
						}
					}
					return LogicalValue.True;
				}
			}

			// Token: 0x020014D4 RID: 5332
			private sealed class RemoveNullsFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x0600865B RID: 34395 RVA: 0x001C84C9 File Offset: 0x001C66C9
				public RemoveNullsFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List)
				{
				}

				// Token: 0x0600865C RID: 34396 RVA: 0x001C9384 File Offset: 0x001C7584
				public override ListValue TypedInvoke(ListValue list)
				{
					return new Library.List.RemoveNullsFunctionValue.RemoveNullsListValue(list);
				}

				// Token: 0x020014D5 RID: 5333
				private class RemoveNullsListValue : StreamedListValue
				{
					// Token: 0x0600865D RID: 34397 RVA: 0x001C938C File Offset: 0x001C758C
					public RemoveNullsListValue(ListValue list)
					{
						this.list = list;
					}

					// Token: 0x0600865E RID: 34398 RVA: 0x001C939B File Offset: 0x001C759B
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.RemoveNullsFunctionValue.RemoveNullsListValue.RemoveNullsEnumerator(this.list.GetEnumerator());
					}

					// Token: 0x04004AB7 RID: 19127
					private readonly ListValue list;

					// Token: 0x020014D6 RID: 5334
					private class RemoveNullsEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600865F RID: 34399 RVA: 0x001C93AD File Offset: 0x001C75AD
						public RemoveNullsEnumerator(IEnumerator<IValueReference> enumerator)
						{
							this.enumerator = enumerator;
						}

						// Token: 0x1700238D RID: 9101
						// (get) Token: 0x06008660 RID: 34400 RVA: 0x001C93BC File Offset: 0x001C75BC
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x06008661 RID: 34401 RVA: 0x001C93C9 File Offset: 0x001C75C9
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x1700238E RID: 9102
						// (get) Token: 0x06008662 RID: 34402 RVA: 0x001C93D6 File Offset: 0x001C75D6
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008663 RID: 34403 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008664 RID: 34404 RVA: 0x001C93DE File Offset: 0x001C75DE
						public bool MoveNext()
						{
							while (this.enumerator.MoveNext())
							{
								if (!this.enumerator.Current.Value.IsNull)
								{
									return true;
								}
							}
							return false;
						}

						// Token: 0x04004AB8 RID: 19128
						private readonly IEnumerator<IValueReference> enumerator;
					}
				}
			}

			// Token: 0x020014D7 RID: 5335
			private sealed class RemoveMatchingItemsFunctionValue : Library.List.DifferenceOperationFunctionValue
			{
				// Token: 0x1700238F RID: 9103
				// (get) Token: 0x06008665 RID: 34405 RVA: 0x001C9407 File Offset: 0x001C7607
				protected override Func<int, bool> ShouldRemove
				{
					get
					{
						return new Func<int, bool>(Library.List.RemoveMatchingItemsFunctionValue.RemoveUnlimited);
					}
				}

				// Token: 0x06008666 RID: 34406 RVA: 0x00002139 File Offset: 0x00000339
				private static bool RemoveUnlimited(int countToRemove)
				{
					return true;
				}
			}

			// Token: 0x020014D8 RID: 5336
			private sealed class DifferenceFunctionValue : Library.List.DifferenceOperationFunctionValue
			{
				// Token: 0x17002390 RID: 9104
				// (get) Token: 0x06008668 RID: 34408 RVA: 0x001C941D File Offset: 0x001C761D
				protected override Func<int, bool> ShouldRemove
				{
					get
					{
						return new Func<int, bool>(Library.List.DifferenceFunctionValue.RemoveLimited);
					}
				}

				// Token: 0x06008669 RID: 34409 RVA: 0x001C942B File Offset: 0x001C762B
				private static bool RemoveLimited(int countToRemove)
				{
					return countToRemove > 0;
				}
			}

			// Token: 0x020014D9 RID: 5337
			private abstract class DifferenceOperationFunctionValue : NativeFunctionValue3<ListValue, ListValue, ListValue, Value>
			{
				// Token: 0x0600866B RID: 34411 RVA: 0x001C9434 File Offset: 0x001C7634
				public DifferenceOperationFunctionValue()
					: base(TypeValue.List, 2, "list1", TypeValue.List, "list2", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x0600866C RID: 34412 RVA: 0x001C946C File Offset: 0x001C766C
				public override ListValue TypedInvoke(ListValue list1, ListValue list2, Value equationCriteria)
				{
					IEqualityComparer<Value> equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true);
					return new Library.List.DifferenceOperationFunctionValue.DifferenceOperationListValue(this.ShouldRemove, list1, list2, equalityComparer);
				}

				// Token: 0x17002391 RID: 9105
				// (get) Token: 0x0600866D RID: 34413
				protected abstract Func<int, bool> ShouldRemove { get; }

				// Token: 0x020014DA RID: 5338
				private class DifferenceOperationListValue : StreamedListValue
				{
					// Token: 0x0600866E RID: 34414 RVA: 0x001C948F File Offset: 0x001C768F
					public DifferenceOperationListValue(Func<int, bool> shouldRemove, ListValue list1, ListValue list2, IEqualityComparer<Value> comparer)
					{
						this.shouldRemove = shouldRemove;
						this.list1 = list1;
						this.list2 = list2;
						this.comparer = comparer;
					}

					// Token: 0x0600866F RID: 34415 RVA: 0x001C94B4 File Offset: 0x001C76B4
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						Dictionary<Value, int> dictionary = new Dictionary<Value, int>(this.comparer);
						foreach (IValueReference valueReference in this.list2)
						{
							int num;
							dictionary.TryGetValue(valueReference.Value, out num);
							dictionary[valueReference.Value] = num + 1;
						}
						return new Library.List.DifferenceOperationFunctionValue.DifferenceOperationListValue.DifferenceOperationEnumerator(this.shouldRemove, this.list1.GetEnumerator(), dictionary);
					}

					// Token: 0x04004AB9 RID: 19129
					private readonly Func<int, bool> shouldRemove;

					// Token: 0x04004ABA RID: 19130
					private readonly ListValue list1;

					// Token: 0x04004ABB RID: 19131
					private readonly ListValue list2;

					// Token: 0x04004ABC RID: 19132
					private readonly IEqualityComparer<Value> comparer;

					// Token: 0x020014DB RID: 5339
					private class DifferenceOperationEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06008670 RID: 34416 RVA: 0x001C953C File Offset: 0x001C773C
						public DifferenceOperationEnumerator(Func<int, bool> shouldRemove, IEnumerator<IValueReference> enumerator, Dictionary<Value, int> valuesToRemove)
						{
							this.shouldRemove = shouldRemove;
							this.enumerator = enumerator;
							this.valuesToRemove = valuesToRemove;
						}

						// Token: 0x17002392 RID: 9106
						// (get) Token: 0x06008671 RID: 34417 RVA: 0x001C9559 File Offset: 0x001C7759
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x17002393 RID: 9107
						// (get) Token: 0x06008672 RID: 34418 RVA: 0x001C9566 File Offset: 0x001C7766
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008673 RID: 34419 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008674 RID: 34420 RVA: 0x001C956E File Offset: 0x001C776E
						public void Dispose()
						{
							this.enumerator.Dispose();
							this.valuesToRemove = null;
						}

						// Token: 0x06008675 RID: 34421 RVA: 0x001C9584 File Offset: 0x001C7784
						public bool MoveNext()
						{
							while (this.enumerator.MoveNext())
							{
								Value value = this.enumerator.Current.Value;
								int num;
								if (!this.valuesToRemove.TryGetValue(value, out num) || !this.shouldRemove(num))
								{
									return true;
								}
								this.valuesToRemove[value] = num - 1;
							}
							return false;
						}

						// Token: 0x04004ABD RID: 19133
						private readonly Func<int, bool> shouldRemove;

						// Token: 0x04004ABE RID: 19134
						private readonly IEnumerator<IValueReference> enumerator;

						// Token: 0x04004ABF RID: 19135
						private Dictionary<Value, int> valuesToRemove;
					}
				}
			}

			// Token: 0x020014DC RID: 5340
			private sealed class IntersectFunctionValue : Library.List.BagOperationFunctionValue
			{
				// Token: 0x17002394 RID: 9108
				// (get) Token: 0x06008676 RID: 34422 RVA: 0x001C95E3 File Offset: 0x001C77E3
				protected override Library.List.BagOperationFunctionValue.BagOperationDelegate Operation
				{
					get
					{
						return new Library.List.BagOperationFunctionValue.BagOperationDelegate(Library.List.IntersectFunctionValue.Intersect);
					}
				}

				// Token: 0x06008677 RID: 34423 RVA: 0x001C95F4 File Offset: 0x001C77F4
				private static void Intersect(Dictionary<Value, int> bag1, Dictionary<Value, int> bag2)
				{
					foreach (Value value in bag1.Keys.ToArray<Value>())
					{
						int num;
						bool flag = bag1.TryGetValue(value, out num);
						int num2;
						bag2.TryGetValue(value, out num2);
						int num3 = Math.Min(num, num2);
						if (num3 == 0 && flag)
						{
							bag1.Remove(value);
						}
						else
						{
							bag1[value] = num3;
						}
					}
				}
			}

			// Token: 0x020014DD RID: 5341
			private sealed class UnionFunctionValue : Library.List.BagOperationFunctionValue
			{
				// Token: 0x17002395 RID: 9109
				// (get) Token: 0x06008679 RID: 34425 RVA: 0x001C9663 File Offset: 0x001C7863
				protected override Library.List.BagOperationFunctionValue.BagOperationDelegate Operation
				{
					get
					{
						return new Library.List.BagOperationFunctionValue.BagOperationDelegate(Library.List.UnionFunctionValue.Union);
					}
				}

				// Token: 0x0600867A RID: 34426 RVA: 0x001C9674 File Offset: 0x001C7874
				private static void Union(Dictionary<Value, int> bag1, Dictionary<Value, int> bag2)
				{
					foreach (Value value in bag1.Keys.Concat(bag2.Keys).ToArray<Value>())
					{
						int num;
						bag1.TryGetValue(value, out num);
						int num2;
						bag2.TryGetValue(value, out num2);
						bag1[value] = Math.Max(num, num2);
					}
				}
			}

			// Token: 0x020014DE RID: 5342
			private abstract class BagOperationFunctionValue : NativeFunctionValue2<ListValue, ListValue, Value>
			{
				// Token: 0x0600867C RID: 34428 RVA: 0x001C96CD File Offset: 0x001C78CD
				public BagOperationFunctionValue()
					: base(TypeValue.List, 1, "lists", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x0600867D RID: 34429 RVA: 0x001C96F0 File Offset: 0x001C78F0
				public sealed override ListValue TypedInvoke(ListValue lists, Value equationCriteria)
				{
					IEqualityComparer<Value> equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true);
					return new Library.List.BagOperationFunctionValue.BagOperationListValue(this.Operation, lists, equalityComparer);
				}

				// Token: 0x17002396 RID: 9110
				// (get) Token: 0x0600867E RID: 34430
				protected abstract Library.List.BagOperationFunctionValue.BagOperationDelegate Operation { get; }

				// Token: 0x020014DF RID: 5343
				// (Invoke) Token: 0x06008680 RID: 34432
				protected delegate void BagOperationDelegate(Dictionary<Value, int> bag1, Dictionary<Value, int> bag2);

				// Token: 0x020014E0 RID: 5344
				private sealed class BagOperationListValue : StreamedListValue
				{
					// Token: 0x06008683 RID: 34435 RVA: 0x001C9712 File Offset: 0x001C7912
					public BagOperationListValue(Library.List.BagOperationFunctionValue.BagOperationDelegate operation, ListValue lists, IEqualityComparer<Value> comparer)
					{
						this.operation = operation;
						this.lists = lists;
						this.comparer = comparer;
					}

					// Token: 0x06008684 RID: 34436 RVA: 0x001C9730 File Offset: 0x001C7930
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						Dictionary<Value, int> dictionary = null;
						foreach (IValueReference valueReference in this.lists)
						{
							Dictionary<Value, int> dictionary2 = new Dictionary<Value, int>(this.comparer);
							foreach (IValueReference valueReference2 in valueReference.Value.AsList)
							{
								int num;
								dictionary2.TryGetValue(valueReference2.Value, out num);
								dictionary2[valueReference2.Value] = num + 1;
							}
							if (dictionary == null)
							{
								dictionary = dictionary2;
							}
							else
							{
								this.operation(dictionary, dictionary2);
							}
						}
						return new Library.List.BagOperationFunctionValue.BagOperationListValue.BagOperationEnumerator(ListValue.Combine(this.lists).GetEnumerator(), dictionary);
					}

					// Token: 0x04004AC0 RID: 19136
					private readonly Library.List.BagOperationFunctionValue.BagOperationDelegate operation;

					// Token: 0x04004AC1 RID: 19137
					private readonly ListValue lists;

					// Token: 0x04004AC2 RID: 19138
					private readonly IEqualityComparer<Value> comparer;

					// Token: 0x020014E1 RID: 5345
					private class BagOperationEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06008685 RID: 34437 RVA: 0x001C9808 File Offset: 0x001C7A08
						public BagOperationEnumerator(IEnumerator<IValueReference> enumerator, Dictionary<Value, int> commonItems)
						{
							this.enumerator = enumerator;
							this.commonItems = commonItems;
						}

						// Token: 0x17002397 RID: 9111
						// (get) Token: 0x06008686 RID: 34438 RVA: 0x001C981E File Offset: 0x001C7A1E
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x17002398 RID: 9112
						// (get) Token: 0x06008687 RID: 34439 RVA: 0x001C982B File Offset: 0x001C7A2B
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008688 RID: 34440 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008689 RID: 34441 RVA: 0x001C9833 File Offset: 0x001C7A33
						public void Dispose()
						{
							this.enumerator.Dispose();
							this.commonItems = null;
						}

						// Token: 0x0600868A RID: 34442 RVA: 0x001C9848 File Offset: 0x001C7A48
						public bool MoveNext()
						{
							while (this.enumerator.MoveNext())
							{
								Value value = this.enumerator.Current.Value;
								int num;
								this.commonItems.TryGetValue(value, out num);
								if (num > 0)
								{
									this.commonItems[value] = num - 1;
									return true;
								}
							}
							return false;
						}

						// Token: 0x04004AC3 RID: 19139
						private readonly IEnumerator<IValueReference> enumerator;

						// Token: 0x04004AC4 RID: 19140
						private Dictionary<Value, int> commonItems;
					}
				}
			}

			// Token: 0x020014E2 RID: 5346
			private class ReplaceMatchingItemsFunctionValue : NativeFunctionValue3<ListValue, ListValue, Value, Value>
			{
				// Token: 0x0600868B RID: 34443 RVA: 0x001C989C File Offset: 0x001C7A9C
				public ReplaceMatchingItemsFunctionValue()
					: base(TypeValue.List, 2, "list", TypeValue.List, "replacements", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x0600868C RID: 34444 RVA: 0x001C98D4 File Offset: 0x001C7AD4
				public override ListValue TypedInvoke(ListValue list, Value replacements, Value equationCriteria)
				{
					ListValue asList = Library.ListReplacementOperations._ListReplacementOperations.Invoke(replacements).AsList;
					IEqualityComparer<Value> equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true);
					return new Library.List.ReplaceMatchingItemsFunctionValue.ReplaceAllListValue(list, asList, equalityComparer);
				}

				// Token: 0x020014E3 RID: 5347
				private sealed class ReplaceAllListValue : StreamedListValue
				{
					// Token: 0x0600868D RID: 34445 RVA: 0x001C9902 File Offset: 0x001C7B02
					public ReplaceAllListValue(ListValue list, ListValue replacements, IEqualityComparer<Value> comparer)
					{
						this.list = list;
						this.replacements = replacements;
						this.comparer = comparer;
					}

					// Token: 0x0600868E RID: 34446 RVA: 0x001C9920 File Offset: 0x001C7B20
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						Dictionary<Value, Value> dictionary = new Dictionary<Value, Value>(this.comparer);
						foreach (IValueReference valueReference in this.replacements)
						{
							ListValue asList = valueReference.Value.AsList;
							dictionary.Add(asList[0], asList[1]);
						}
						return new Library.List.ReplaceMatchingItemsFunctionValue.ReplaceAllListValue.ReplaceAllEnumerator(this.list.GetEnumerator(), dictionary);
					}

					// Token: 0x04004AC5 RID: 19141
					private readonly ListValue list;

					// Token: 0x04004AC6 RID: 19142
					private readonly ListValue replacements;

					// Token: 0x04004AC7 RID: 19143
					private readonly IEqualityComparer<Value> comparer;

					// Token: 0x020014E4 RID: 5348
					private class ReplaceAllEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600868F RID: 34447 RVA: 0x001C99A4 File Offset: 0x001C7BA4
						public ReplaceAllEnumerator(IEnumerator<IValueReference> enumerator, IDictionary<Value, Value> valuesToReplace)
						{
							this.enumerator = enumerator;
							this.valuesToReplace = valuesToReplace;
						}

						// Token: 0x17002399 RID: 9113
						// (get) Token: 0x06008690 RID: 34448 RVA: 0x001C99BA File Offset: 0x001C7BBA
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x1700239A RID: 9114
						// (get) Token: 0x06008691 RID: 34449 RVA: 0x001C99C4 File Offset: 0x001C7BC4
						public IValueReference Current
						{
							get
							{
								Value value = this.enumerator.Current.Value;
								Value value2;
								if (this.valuesToReplace.TryGetValue(value, out value2))
								{
									return value2;
								}
								return value;
							}
						}

						// Token: 0x06008692 RID: 34450 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008693 RID: 34451 RVA: 0x001C99F5 File Offset: 0x001C7BF5
						public void Dispose()
						{
							this.enumerator.Dispose();
							this.valuesToReplace = null;
						}

						// Token: 0x06008694 RID: 34452 RVA: 0x001C9A09 File Offset: 0x001C7C09
						public bool MoveNext()
						{
							return this.enumerator.MoveNext();
						}

						// Token: 0x04004AC8 RID: 19144
						private readonly IEnumerator<IValueReference> enumerator;

						// Token: 0x04004AC9 RID: 19145
						private IDictionary<Value, Value> valuesToReplace;
					}
				}
			}

			// Token: 0x020014E5 RID: 5349
			private class GenerateFunctionValue : NativeFunctionValue4<ListValue, FunctionValue, FunctionValue, FunctionValue, Value>
			{
				// Token: 0x06008695 RID: 34453 RVA: 0x001C9A18 File Offset: 0x001C7C18
				public GenerateFunctionValue()
					: base(TypeValue.List, 3, "initial", TypeValue.Function, "condition", TypeValue.Function, "next", TypeValue.Function, "selector", NullableTypeValue.Function)
				{
				}

				// Token: 0x06008696 RID: 34454 RVA: 0x001C9A59 File Offset: 0x001C7C59
				public override ListValue TypedInvoke(FunctionValue initial, FunctionValue condition, FunctionValue next, Value selector)
				{
					return new Library.List.GenerateFunctionValue.GenerateListValue(initial, condition, next, selector);
				}

				// Token: 0x020014E6 RID: 5350
				private class GenerateListValue : StreamedListValue
				{
					// Token: 0x06008697 RID: 34455 RVA: 0x001C9A65 File Offset: 0x001C7C65
					public GenerateListValue(FunctionValue initial, FunctionValue condition, FunctionValue next, Value selector)
					{
						this.initial = initial;
						this.condition = condition;
						this.next = next;
						this.selector = selector;
					}

					// Token: 0x06008698 RID: 34456 RVA: 0x001C9A8A File Offset: 0x001C7C8A
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.GenerateFunctionValue.GenerateListValue.GenerateEnumerator(this.initial, this.condition, this.next, this.selector.IsNull ? null : this.selector.AsFunction);
					}

					// Token: 0x04004ACA RID: 19146
					private readonly FunctionValue initial;

					// Token: 0x04004ACB RID: 19147
					private readonly FunctionValue condition;

					// Token: 0x04004ACC RID: 19148
					private readonly FunctionValue next;

					// Token: 0x04004ACD RID: 19149
					private readonly Value selector;

					// Token: 0x020014E7 RID: 5351
					private class GenerateEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06008699 RID: 34457 RVA: 0x001C9ABE File Offset: 0x001C7CBE
						public GenerateEnumerator(FunctionValue initial, FunctionValue condition, FunctionValue next, FunctionValue selector)
						{
							this.initial = initial;
							this.condition = condition;
							this.next = next;
							this.selector = selector;
							this.value = null;
							this.current = null;
							this.complete = false;
						}

						// Token: 0x1700239B RID: 9115
						// (get) Token: 0x0600869A RID: 34458 RVA: 0x001C9AF8 File Offset: 0x001C7CF8
						public IValueReference Current
						{
							get
							{
								if (this.current == null && !this.complete)
								{
									if (this.selector == null)
									{
										this.current = this.value;
									}
									else
									{
										this.current = new TransformValueReference(this.value, this.selector);
									}
								}
								return this.current;
							}
						}

						// Token: 0x1700239C RID: 9116
						// (get) Token: 0x0600869B RID: 34459 RVA: 0x001C9B48 File Offset: 0x001C7D48
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600869C RID: 34460 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x0600869D RID: 34461 RVA: 0x0000336E File Offset: 0x0000156E
						public void Dispose()
						{
						}

						// Token: 0x0600869E RID: 34462 RVA: 0x001C9B50 File Offset: 0x001C7D50
						public bool MoveNext()
						{
							if (this.complete)
							{
								return false;
							}
							this.current = null;
							if (this.value == null)
							{
								this.value = this.initial.Invoke();
							}
							else
							{
								this.value = this.next.Invoke(this.value);
							}
							if (!this.condition.Invoke(this.value).AsBoolean)
							{
								this.complete = true;
								return false;
							}
							return true;
						}

						// Token: 0x04004ACE RID: 19150
						private readonly FunctionValue initial;

						// Token: 0x04004ACF RID: 19151
						private readonly FunctionValue condition;

						// Token: 0x04004AD0 RID: 19152
						private readonly FunctionValue next;

						// Token: 0x04004AD1 RID: 19153
						private readonly FunctionValue selector;

						// Token: 0x04004AD2 RID: 19154
						private Value value;

						// Token: 0x04004AD3 RID: 19155
						private IValueReference current;

						// Token: 0x04004AD4 RID: 19156
						private bool complete;
					}
				}
			}

			// Token: 0x020014E8 RID: 5352
			private class AlternateFunctionValue : NativeFunctionValue4<ListValue, ListValue, NumberValue, Value, Value>
			{
				// Token: 0x0600869F RID: 34463 RVA: 0x001C9BC4 File Offset: 0x001C7DC4
				public AlternateFunctionValue()
					: base(TypeValue.List, 2, "list", TypeValue.List, "count", TypeValue.Number, "repeatInterval", NullableTypeValue.Number, "offset", NullableTypeValue.Number)
				{
				}

				// Token: 0x060086A0 RID: 34464 RVA: 0x001C9C08 File Offset: 0x001C7E08
				public override ListValue TypedInvoke(ListValue list, NumberValue count, Value repeatInterval, Value offset)
				{
					if (repeatInterval.IsNull && offset.IsNull)
					{
						return LanguageLibrary.List.Skip.Invoke(list, count).AsList;
					}
					if (count.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					if (repeatInterval.AsNumber.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("repeatInterval", repeatInterval);
					}
					if (!offset.IsNull && offset.AsNumber.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("offset", offset);
					}
					return new Library.List.AlternateFunctionValue.AlternateListValue(list, offset.IsNull ? 0 : offset.AsNumber.AsInteger32, count.AsInteger32, repeatInterval.AsInteger32);
				}

				// Token: 0x020014E9 RID: 5353
				private class AlternateListValue : StreamedListValue
				{
					// Token: 0x060086A1 RID: 34465 RVA: 0x001C9CC2 File Offset: 0x001C7EC2
					public AlternateListValue(ListValue list, int initialTake, int skipCount, int takeCount)
					{
						this.list = list;
						this.initialTake = initialTake;
						this.takeCount = takeCount;
						this.skipCount = skipCount;
					}

					// Token: 0x1700239D RID: 9117
					// (get) Token: 0x060086A2 RID: 34466 RVA: 0x001C9CE7 File Offset: 0x001C7EE7
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x060086A3 RID: 34467 RVA: 0x001C9CF4 File Offset: 0x001C7EF4
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.AlternateFunctionValue.AlternateListValue.SkipManyEnumerator(this.list.GetEnumerator(), this.initialTake, this.skipCount, this.takeCount);
					}

					// Token: 0x04004AD5 RID: 19157
					private readonly ListValue list;

					// Token: 0x04004AD6 RID: 19158
					private readonly int initialTake;

					// Token: 0x04004AD7 RID: 19159
					private readonly int takeCount;

					// Token: 0x04004AD8 RID: 19160
					private readonly int skipCount;

					// Token: 0x020014EA RID: 5354
					private class SkipManyEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x060086A4 RID: 34468 RVA: 0x001C9D18 File Offset: 0x001C7F18
						public SkipManyEnumerator(IEnumerator<IValueReference> enumerator, int initialTake, int skipCount, int takeCount)
						{
							this.enumerator = enumerator;
							this.initialTake = initialTake;
							this.takeCount = takeCount;
							this.skipCount = skipCount;
						}

						// Token: 0x1700239E RID: 9118
						// (get) Token: 0x060086A5 RID: 34469 RVA: 0x001C9D3D File Offset: 0x001C7F3D
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x1700239F RID: 9119
						// (get) Token: 0x060086A6 RID: 34470 RVA: 0x001C9D4A File Offset: 0x001C7F4A
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x060086A7 RID: 34471 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060086A8 RID: 34472 RVA: 0x001C9D52 File Offset: 0x001C7F52
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x060086A9 RID: 34473 RVA: 0x001C9D60 File Offset: 0x001C7F60
						public bool MoveNext()
						{
							while (this.initialTake <= 0)
							{
								for (int i = 0; i < this.skipCount; i++)
								{
									if (!this.enumerator.MoveNext())
									{
										return false;
									}
								}
								if (this.takeCount <= 0)
								{
									return false;
								}
								this.initialTake = this.takeCount;
							}
							if (!this.enumerator.MoveNext())
							{
								return false;
							}
							this.initialTake--;
							return true;
						}

						// Token: 0x04004AD9 RID: 19161
						private readonly IEnumerator<IValueReference> enumerator;

						// Token: 0x04004ADA RID: 19162
						private int initialTake;

						// Token: 0x04004ADB RID: 19163
						private readonly int takeCount;

						// Token: 0x04004ADC RID: 19164
						private readonly int skipCount;
					}
				}
			}

			// Token: 0x020014EB RID: 5355
			private class AccumulateFunctionValue : NativeFunctionValue3<Value, ListValue, Value, FunctionValue>, IAccumulableFunction
			{
				// Token: 0x060086AA RID: 34474 RVA: 0x001C9DCD File Offset: 0x001C7FCD
				public AccumulateFunctionValue()
					: base(TypeValue.Any, "list", TypeValue.List, "seed", TypeValue.Any, "accumulator", TypeValue.Function)
				{
				}

				// Token: 0x170023A0 RID: 9120
				// (get) Token: 0x060086AB RID: 34475 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x060086AC RID: 34476 RVA: 0x001C9DF8 File Offset: 0x001C7FF8
				public override Value TypedInvoke(ListValue list, Value seed, FunctionValue accumulator)
				{
					IAccumulator accumulator2 = new Library.List.AccumulateFunctionValue.AccumulateAccumulable(seed, accumulator).CreateAccumulator();
					accumulator2.AccumulateRange(list);
					return accumulator2.Current.Value;
				}

				// Token: 0x060086AD RID: 34477 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x060086AE RID: 34478 RVA: 0x001C9E17 File Offset: 0x001C8017
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.AccumulateFunctionValue.AccumulateAccumulable(arguments["seed"], arguments["accumulator"].AsFunction);
				}

				// Token: 0x04004ADD RID: 19165
				private const string enumerableParameter = "list";

				// Token: 0x020014EC RID: 5356
				private sealed class AccumulateAccumulable : IAccumulable
				{
					// Token: 0x060086AF RID: 34479 RVA: 0x001C9E39 File Offset: 0x001C8039
					public AccumulateAccumulable(Value seed, FunctionValue accumulator)
					{
						this.seed = seed;
						this.accumulator = accumulator;
					}

					// Token: 0x060086B0 RID: 34480 RVA: 0x001C9E4F File Offset: 0x001C804F
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.AccumulateFunctionValue.AccumulateAccumulable.AccumulateAccumulator(this);
					}

					// Token: 0x04004ADE RID: 19166
					private readonly Value seed;

					// Token: 0x04004ADF RID: 19167
					private readonly FunctionValue accumulator;

					// Token: 0x020014ED RID: 5357
					private sealed class AccumulateAccumulator : IAccumulator
					{
						// Token: 0x060086B1 RID: 34481 RVA: 0x001C9E57 File Offset: 0x001C8057
						public AccumulateAccumulator(Library.List.AccumulateFunctionValue.AccumulateAccumulable accumulable)
						{
							this.accumulator = accumulable.accumulator;
							this.value = accumulable.seed;
						}

						// Token: 0x170023A1 RID: 9121
						// (get) Token: 0x060086B2 RID: 34482 RVA: 0x001C9E77 File Offset: 0x001C8077
						public IValueReference Current
						{
							get
							{
								return this.value;
							}
						}

						// Token: 0x060086B3 RID: 34483 RVA: 0x001C9E7F File Offset: 0x001C807F
						public void AccumulateNext(IValueReference next)
						{
							this.value = this.accumulator.Invoke(this.value, next.Value);
						}

						// Token: 0x04004AE0 RID: 19168
						private readonly FunctionValue accumulator;

						// Token: 0x04004AE1 RID: 19169
						private Value value;
					}
				}
			}

			// Token: 0x020014EE RID: 5358
			private class ZipFunctionValue : NativeFunctionValue1<ListValue, ListValue>
			{
				// Token: 0x060086B4 RID: 34484 RVA: 0x001C8508 File Offset: 0x001C6708
				public ZipFunctionValue()
					: base(TypeValue.List, "lists", TypeValue.List)
				{
				}

				// Token: 0x060086B5 RID: 34485 RVA: 0x001C9E9E File Offset: 0x001C809E
				public override ListValue TypedInvoke(ListValue lists)
				{
					return ListValue.New(new Library.List.ZipFunctionValue.ListZipEnumerable(lists));
				}

				// Token: 0x060086B6 RID: 34486 RVA: 0x001C9EAC File Offset: 0x001C80AC
				private static void Dispose(IEnumerator<IValueReference>[] enumerators)
				{
					foreach (IEnumerator<IValueReference> enumerator in enumerators)
					{
						if (enumerator != null)
						{
							try
							{
								enumerator.Dispose();
							}
							catch (RuntimeException)
							{
							}
						}
					}
				}

				// Token: 0x020014EF RID: 5359
				private class ListZipEnumerable : IEnumerable<IValueReference>, IEnumerable
				{
					// Token: 0x060086B7 RID: 34487 RVA: 0x001C9EEC File Offset: 0x001C80EC
					public ListZipEnumerable(ListValue lists)
					{
						this.lists = lists;
					}

					// Token: 0x060086B8 RID: 34488 RVA: 0x001C9EFC File Offset: 0x001C80FC
					public IEnumerator<IValueReference> GetEnumerator()
					{
						IValueReference[] array = this.lists.ToArray();
						IValueReference[] array2 = array;
						if (array2.Length == 0)
						{
							return EmptyEnumerator<IValueReference>.Instance;
						}
						IEnumerator<IValueReference>[] array3 = new IEnumerator<IValueReference>[array2.Length];
						IEnumerator<IValueReference> enumerator;
						try
						{
							for (int i = 0; i < array2.Length; i++)
							{
								array3[i] = array2[i].Value.AsList.GetEnumerator();
							}
							enumerator = new Library.List.ZipFunctionValue.ListZipEnumerator(array3);
						}
						catch
						{
							Library.List.ZipFunctionValue.Dispose(array3);
							throw;
						}
						return enumerator;
					}

					// Token: 0x060086B9 RID: 34489 RVA: 0x001C9F74 File Offset: 0x001C8174
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x04004AE2 RID: 19170
					private readonly ListValue lists;
				}

				// Token: 0x020014F0 RID: 5360
				private class ListZipEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
				{
					// Token: 0x060086BA RID: 34490 RVA: 0x001C9F7C File Offset: 0x001C817C
					public ListZipEnumerator(IEnumerator<IValueReference>[] enumerators)
					{
						this.enumerators = enumerators;
					}

					// Token: 0x170023A2 RID: 9122
					// (get) Token: 0x060086BB RID: 34491 RVA: 0x001C9F8B File Offset: 0x001C818B
					public IValueReference Current
					{
						get
						{
							return this.current;
						}
					}

					// Token: 0x170023A3 RID: 9123
					// (get) Token: 0x060086BC RID: 34492 RVA: 0x001C9F93 File Offset: 0x001C8193
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060086BD RID: 34493 RVA: 0x001C9F9C File Offset: 0x001C819C
					public bool MoveNext()
					{
						if (this.isDone)
						{
							return false;
						}
						IValueReference[] array = null;
						for (int i = 0; i < this.enumerators.Length; i++)
						{
							IEnumerator<IValueReference> enumerator = this.enumerators[i];
							bool flag = enumerator.MoveNext();
							if (flag && array == null)
							{
								array = new IValueReference[this.enumerators.Length];
								for (int j = 0; j < i; j++)
								{
									array[j] = Value.Null;
								}
							}
							if (array != null)
							{
								if (flag)
								{
									array[i] = enumerator.Current;
								}
								else
								{
									array[i] = Value.Null;
								}
							}
						}
						if (array == null)
						{
							this.current = null;
							this.isDone = true;
							return false;
						}
						this.current = ListValue.New(array);
						return true;
					}

					// Token: 0x060086BE RID: 34494 RVA: 0x000091AE File Offset: 0x000073AE
					public void Reset()
					{
						throw new NotImplementedException();
					}

					// Token: 0x060086BF RID: 34495 RVA: 0x001CA03E File Offset: 0x001C823E
					public void Dispose()
					{
						if (this.enumerators != null)
						{
							Library.List.ZipFunctionValue.Dispose(this.enumerators);
							this.enumerators = null;
						}
					}

					// Token: 0x04004AE3 RID: 19171
					private IEnumerator<IValueReference>[] enumerators;

					// Token: 0x04004AE4 RID: 19172
					private bool isDone;

					// Token: 0x04004AE5 RID: 19173
					private IValueReference current;
				}
			}

			// Token: 0x020014F1 RID: 5361
			private sealed class SplitFunctionValue : NativeFunctionValue2<ListValue, ListValue, NumberValue>
			{
				// Token: 0x060086C0 RID: 34496 RVA: 0x001CA05A File Offset: 0x001C825A
				public SplitFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "pageSize", TypeValue.Int32)
				{
				}

				// Token: 0x060086C1 RID: 34497 RVA: 0x001CA07C File Offset: 0x001C827C
				public override ListValue TypedInvoke(ListValue list, NumberValue pageSize)
				{
					int asInteger = pageSize.AsInteger32;
					if (asInteger < 1)
					{
						throw ValueException.ArgumentOutOfRange("pageSize", pageSize);
					}
					return new PagedListValue(new Func<PagedListValue.GetCurrentEnumerator, RowCount, RowCount, Value>(new Library.List.SplitFunctionValue.PageSource(list).GetPage), list, new RowCount((long)asInteger));
				}

				// Token: 0x020014F2 RID: 5362
				private sealed class PageSource
				{
					// Token: 0x060086C2 RID: 34498 RVA: 0x001CA0BE File Offset: 0x001C82BE
					public PageSource(ListValue list)
					{
						this.list = list;
					}

					// Token: 0x060086C3 RID: 34499 RVA: 0x001CA0CD File Offset: 0x001C82CD
					public Value GetPage(PagedListValue.GetCurrentEnumerator getCurrentEnumerator, RowCount offset, RowCount pageSize)
					{
						return new Library.List.SplitFunctionValue.PageSource.Page(this.list, getCurrentEnumerator, offset, pageSize);
					}

					// Token: 0x04004AE6 RID: 19174
					private readonly ListValue list;

					// Token: 0x020014F3 RID: 5363
					private sealed class Page : StreamedListValue
					{
						// Token: 0x060086C4 RID: 34500 RVA: 0x001CA0DD File Offset: 0x001C82DD
						public Page(ListValue list, PagedListValue.GetCurrentEnumerator getCurrentEnumerator, RowCount offset, RowCount pageSize)
						{
							this.list = list;
							this.getCurrentEnumerator = getCurrentEnumerator;
							this.offset = offset;
							this.pageSize = pageSize;
						}

						// Token: 0x170023A4 RID: 9124
						// (get) Token: 0x060086C5 RID: 34501 RVA: 0x001CA102 File Offset: 0x001C8302
						public override TypeValue Type
						{
							get
							{
								return this.list.Type;
							}
						}

						// Token: 0x060086C6 RID: 34502 RVA: 0x001CA110 File Offset: 0x001C8310
						public override IEnumerator<IValueReference> GetEnumerator()
						{
							IEnumerator<IValueReference> enumerator = this.getCurrentEnumerator();
							if (enumerator != null)
							{
								return enumerator;
							}
							return LanguageLibrary.List.Take.Invoke(LanguageLibrary.List.Skip.Invoke(this.list, NumberValue.New(this.offset.Value)), NumberValue.New(this.pageSize.Value)).AsList.GetEnumerator();
						}

						// Token: 0x04004AE7 RID: 19175
						private readonly ListValue list;

						// Token: 0x04004AE8 RID: 19176
						private readonly PagedListValue.GetCurrentEnumerator getCurrentEnumerator;

						// Token: 0x04004AE9 RID: 19177
						private readonly RowCount offset;

						// Token: 0x04004AEA RID: 19178
						private readonly RowCount pageSize;
					}
				}
			}

			// Token: 0x020014F4 RID: 5364
			private class ElementWithListCheckFunctionValue : NativeFunctionValue2
			{
				// Token: 0x060086C7 RID: 34503 RVA: 0x001CA178 File Offset: 0x001C8378
				public ElementWithListCheckFunctionValue()
					: base("collection", "key")
				{
				}

				// Token: 0x060086C8 RID: 34504 RVA: 0x001CA18A File Offset: 0x001C838A
				public override Value Invoke(Value collection, Value key)
				{
					if (!key.IsNumber && !key.IsRecord)
					{
						throw ValueException.ElementAccessIndexTypeMismatch(collection, key);
					}
					if (collection.IsTable)
					{
						return collection.AsTable[key];
					}
					return collection.AsList[key];
				}
			}

			// Token: 0x020014F5 RID: 5365
			private class ElementOrNullWithListCheckFunctionValue : NativeFunctionValue2
			{
				// Token: 0x060086C9 RID: 34505 RVA: 0x001CA178 File Offset: 0x001C8378
				public ElementOrNullWithListCheckFunctionValue()
					: base("collection", "key")
				{
				}

				// Token: 0x060086CA RID: 34506 RVA: 0x001CA1C8 File Offset: 0x001C83C8
				public override Value Invoke(Value collection, Value key)
				{
					if (!key.IsNumber && !key.IsRecord)
					{
						throw ValueException.ElementAccessIndexTypeMismatch(collection, key);
					}
					if (collection.IsTable)
					{
						Value value;
						if (collection.AsTable.TryGetValue(key, out value))
						{
							return value;
						}
						return Value.Null;
					}
					else
					{
						Value value;
						if (!collection.IsNull && collection.AsList.TryGetValue(key, out value))
						{
							return value;
						}
						return Value.Null;
					}
				}
			}

			// Token: 0x020014F6 RID: 5366
			private class MinMaxFunctionValue : NativeFunctionValue4<Value, ListValue, Value, Value, Value>, IAccumulableFunction
			{
				// Token: 0x060086CB RID: 34507 RVA: 0x001CA22C File Offset: 0x001C842C
				public MinMaxFunctionValue(bool max)
					: base(TypeValue.Any, 1, "list", TypeValue.List, "default", TypeValue.Any, "comparisonCriteria", TypeValue.Any, "includeNulls", NullableTypeValue.Logical)
				{
					this.max = max;
				}

				// Token: 0x170023A5 RID: 9125
				// (get) Token: 0x060086CC RID: 34508 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x060086CD RID: 34509 RVA: 0x001CA274 File Offset: 0x001C8474
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return Library.List.GetListOperationReturnType(arguments, environment);
				}

				// Token: 0x060086CE RID: 34510 RVA: 0x001CA280 File Offset: 0x001C8480
				public override Value TypedInvoke(ListValue list, Value defaultValue, Value comparisonCriteria, Value includeNulls)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null && defaultValue.IsNull && comparisonCriteria.IsNull)
					{
						return foldableListValue.Aggregate(this.max ? Library.List.Max : Library.List.Min);
					}
					IAccumulator accumulator = new Library.List.MinMaxFunctionValue.MinMaxAccumulable(this.max, defaultValue, comparisonCriteria, includeNulls).CreateAccumulator();
					accumulator.AccumulateRange(list);
					return accumulator.Current.Value;
				}

				// Token: 0x060086CF RID: 34511 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x060086D0 RID: 34512 RVA: 0x001CA2E7 File Offset: 0x001C84E7
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.MinMaxFunctionValue.MinMaxAccumulable(this.max, arguments["default"], arguments["comparisonCriteria"], arguments["includeNulls"]);
				}

				// Token: 0x04004AEB RID: 19179
				private const string enumerableParameter = "list";

				// Token: 0x04004AEC RID: 19180
				private readonly bool max;

				// Token: 0x020014F7 RID: 5367
				private sealed class MinMaxAccumulable : IAccumulable
				{
					// Token: 0x060086D1 RID: 34513 RVA: 0x001CA318 File Offset: 0x001C8518
					public MinMaxAccumulable(bool max, Value defaultValue, Value comparisonCriteria, Value includeNulls)
					{
						this.defaultValue = defaultValue;
						this.keepNulls = !includeNulls.IsNull && includeNulls.AsBoolean;
						this.sign = (max ? (-1) : 1);
						this.comparer = Library.ListComparisonCriteria.CreateComparer(comparisonCriteria);
					}

					// Token: 0x060086D2 RID: 34514 RVA: 0x001CA364 File Offset: 0x001C8564
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.MinMaxFunctionValue.MinMaxAccumulable.MinMaxAccumulator(this);
					}

					// Token: 0x04004AED RID: 19181
					private readonly Value defaultValue;

					// Token: 0x04004AEE RID: 19182
					private readonly bool keepNulls;

					// Token: 0x04004AEF RID: 19183
					private readonly int sign;

					// Token: 0x04004AF0 RID: 19184
					private readonly IComparer<Value> comparer;

					// Token: 0x020014F8 RID: 5368
					private sealed class MinMaxAccumulator : IAccumulator
					{
						// Token: 0x060086D3 RID: 34515 RVA: 0x001CA36C File Offset: 0x001C856C
						public MinMaxAccumulator(Library.List.MinMaxFunctionValue.MinMaxAccumulable accumulable)
						{
							this.defaultValue = accumulable.defaultValue;
							this.keepNulls = accumulable.keepNulls;
							this.sign = accumulable.sign;
							this.comparer = accumulable.comparer;
						}

						// Token: 0x170023A6 RID: 9126
						// (get) Token: 0x060086D4 RID: 34516 RVA: 0x001CA3A4 File Offset: 0x001C85A4
						public IValueReference Current
						{
							get
							{
								return this.current ?? this.defaultValue;
							}
						}

						// Token: 0x060086D5 RID: 34517 RVA: 0x001CA3B8 File Offset: 0x001C85B8
						public void AccumulateNext(IValueReference next)
						{
							Value value = next.Value;
							if (!this.keepNulls && value.IsNull)
							{
								return;
							}
							if (this.current == null)
							{
								this.current = value;
							}
							if (this.comparer.Compare(value, this.current) * this.sign < 0)
							{
								this.current = value;
							}
						}

						// Token: 0x04004AF1 RID: 19185
						private readonly Value defaultValue;

						// Token: 0x04004AF2 RID: 19186
						private readonly bool keepNulls;

						// Token: 0x04004AF3 RID: 19187
						private readonly int sign;

						// Token: 0x04004AF4 RID: 19188
						private readonly IComparer<Value> comparer;

						// Token: 0x04004AF5 RID: 19189
						private Value current;
					}
				}
			}

			// Token: 0x020014F9 RID: 5369
			private class MinMaxNFunctionValue : NativeFunctionValue4<Value, ListValue, Value, Value, Value>
			{
				// Token: 0x060086D6 RID: 34518 RVA: 0x001CA410 File Offset: 0x001C8610
				public MinMaxNFunctionValue(bool max)
					: base(TypeValue.List, 2, "list", TypeValue.List, "countOrCondition", TypeValue.Any, "comparisonCriteria", TypeValue.Any, "includeNulls", NullableTypeValue.Logical)
				{
					this.max = max;
				}

				// Token: 0x060086D7 RID: 34519 RVA: 0x001CA458 File Offset: 0x001C8658
				public override Value TypedInvoke(ListValue list, Value countOrCondition, Value comparisonCriteria, Value includeNulls)
				{
					if (includeNulls.IsNull || !includeNulls.AsBoolean)
					{
						list = Library.List.RemoveNulls.Invoke(list).AsList;
					}
					if (!countOrCondition.IsNumber)
					{
						Value value = LanguageLibrary.List.Sort.Invoke(list, comparisonCriteria);
						if (this.max)
						{
							value = Library.List.Reverse.Invoke(value);
						}
						return Library.List.TakeWhile.Invoke(value, countOrCondition).AsList;
					}
					int asInteger = countOrCondition.AsInteger32;
					if (asInteger < 0)
					{
						throw ValueException.ArgumentOutOfRange("countOrCondition", countOrCondition);
					}
					IComparer<Value> comparer = Library.ListComparisonCriteria.CreateComparer(comparisonCriteria);
					if (this.max)
					{
						return ListValue.New(list.Select((IValueReference x) => x.Value).MaxK(asInteger, comparer));
					}
					return ListValue.New(list.Select((IValueReference x) => x.Value).MinK(asInteger, comparer));
				}

				// Token: 0x04004AF6 RID: 19190
				private readonly bool max;
			}

			// Token: 0x020014FB RID: 5371
			private class CovarianceFunctionValue : NativeFunctionValue2<Value, ListValue, ListValue>
			{
				// Token: 0x060086DC RID: 34524 RVA: 0x001CA567 File Offset: 0x001C8767
				public CovarianceFunctionValue()
					: base(NullableTypeValue.Number, "numberList1", TypeValue.List, "numberList2", TypeValue.List)
				{
				}

				// Token: 0x060086DD RID: 34525 RVA: 0x001CA588 File Offset: 0x001C8788
				public override Value TypedInvoke(ListValue _numberList1, ListValue _numberList2)
				{
					ListValue asList = Library.List.RemoveNulls.Invoke(_numberList1).AsList;
					ListValue asList2 = Library.List.RemoveNulls.Invoke(_numberList2).AsList;
					Value value7;
					using (IEnumerator<IValueReference> enumerator = asList.GetEnumerator())
					{
						using (IEnumerator<IValueReference> enumerator2 = asList2.GetEnumerator())
						{
							Value value = null;
							Value value2 = null;
							Value value3 = null;
							Value value4 = null;
							bool flag;
							for (;;)
							{
								flag = enumerator.MoveNext();
								bool flag2 = enumerator2.MoveNext();
								if (flag != flag2)
								{
									break;
								}
								if (!flag)
								{
									goto IL_00F6;
								}
								Value value5 = enumerator.Current.Value;
								Value value6 = enumerator2.Current.Value;
								if (value == null)
								{
									value = NumberValue.One;
									value2 = value5.AsNumber;
									value3 = value6.AsNumber;
									value4 = value5.Multiply(value6);
								}
								else
								{
									value = value.AsNumber.Increment();
									value2 = Library.List.CovarianceFunctionValue.ComputeNextMean(value2, value5.AsNumber, value);
									value3 = Library.List.CovarianceFunctionValue.ComputeNextMean(value3, value6.AsNumber, value);
									value4 = Library.List.CovarianceFunctionValue.ComputeNextMean(value4, value5.Multiply(value6), value);
								}
							}
							throw ValueException.InsufficientElements(flag ? asList2 : asList);
							IL_00F6:
							if (value == null)
							{
								value7 = Value.Null;
							}
							else
							{
								value7 = value4.Subtract(value2.Multiply(value3));
							}
						}
					}
					return value7;
				}

				// Token: 0x060086DE RID: 34526 RVA: 0x001CA6E0 File Offset: 0x001C88E0
				private static Value ComputeNextMean(Value runningMean, Value currentValue, Value currentIndex)
				{
					Value value = currentValue.Subtract(runningMean).Divide(currentIndex);
					return runningMean.Add(value);
				}
			}

			// Token: 0x020014FC RID: 5372
			private class HistogramFunctionValue : NativeFunctionValue3<TableValue, ListValue, Value, Value>
			{
				// Token: 0x060086DF RID: 34527 RVA: 0x001CA704 File Offset: 0x001C8904
				public HistogramFunctionValue()
					: base(TypeValue.Table, 1, "list", TypeValue.List, "bucketCount", NullableTypeValue.Number, "startAndEnd", NullableTypeValue.List)
				{
				}

				// Token: 0x060086E0 RID: 34528 RVA: 0x001CA73C File Offset: 0x001C893C
				public override TableValue TypedInvoke(ListValue list, Value bucketCountValue, Value startAndEndValues)
				{
					List<Value> list2 = new List<Value>();
					foreach (IValueReference valueReference in list.AsList)
					{
						list2.Add(valueReference.Value);
					}
					if (list2.Count == 0)
					{
						throw ValueException.InsufficientElements(list);
					}
					if (bucketCountValue.IsNull)
					{
						bucketCountValue = NumberValue.New((int)Math.Ceiling(Math.Sqrt((double)list2.Count)));
					}
					int asInteger = bucketCountValue.AsInteger32;
					if (asInteger <= 0)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_HistogramBucketCountError, NumberValue.New(asInteger), null);
					}
					Value value = list2[0];
					Value value2 = list2[0];
					for (int i = 0; i < list2.Count; i++)
					{
						Value value3 = list2[i];
						if (value3.LessThan(value))
						{
							value = value3;
						}
						if (value3.GreaterThan(value2))
						{
							value2 = value3;
						}
					}
					if (!startAndEndValues.IsNull)
					{
						if (!startAndEndValues.IsList || startAndEndValues.AsList.Count != 2)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.ValueException_HistogramStartAndEnd, startAndEndValues, null);
						}
						value = startAndEndValues[0];
						value2 = startAndEndValues[1];
					}
					if (value.GreaterThanOrEqual(value2))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_HistogramStartLessThanEnd, startAndEndValues, null);
					}
					Value value4 = value2.Subtract(value).Divide(NumberValue.New(asInteger));
					List<Value>[] array = new List<Value>[asInteger];
					for (int j = 0; j < asInteger; j++)
					{
						array[j] = new List<Value>();
					}
					for (int k = 0; k < list2.Count; k++)
					{
						Value value5 = list2[k];
						double num = value5.Subtract(value).Divide(value4).AsScientific64;
						if (num == (double)asInteger)
						{
							num -= 1.0;
						}
						if (num >= 0.0 && num < (double)asInteger)
						{
							array[(int)num].Add(value5);
						}
					}
					Value[] array2 = new Value[asInteger];
					for (int l = 0; l < asInteger; l++)
					{
						array2[l] = RecordValue.New(Library.List.HistogramFunctionValue.keys, new Value[]
						{
							value.Add(value4.Multiply(NumberValue.New(l))),
							value.Add(value4.Multiply(NumberValue.New(l))).Add(value4.Divide(NumberValue.New(2.0))),
							value.Add(value4.Multiply(NumberValue.New(l + 1))),
							NumberValue.New(array[l].Count)
						});
					}
					TypeValue typeValue = OperatorTypeflowModels.Binary(BinaryOperator2.Divide, list.Type.AsListType.ItemType, TypeValue.Number);
					RecordValue recordValue = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						typeValue,
						LogicalValue.False
					});
					TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(Library.List.HistogramFunctionValue.keys, new Value[]
					{
						recordValue,
						recordValue,
						recordValue,
						Library.List.HistogramFunctionValue.countField
					})));
					return ListValue.New(array2).ToTable(tableTypeValue);
				}

				// Token: 0x04004AFA RID: 19194
				private static readonly Keys keys = Keys.New("Start", "Center", "End", "Count");

				// Token: 0x04004AFB RID: 19195
				private static readonly RecordValue countField = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Number,
					LogicalValue.False
				});
			}

			// Token: 0x020014FD RID: 5373
			private class ModeFunctionValue : NativeFunctionValue2<Value, ListValue, Value>
			{
				// Token: 0x060086E2 RID: 34530 RVA: 0x001CAA94 File Offset: 0x001C8C94
				public ModeFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x060086E3 RID: 34531 RVA: 0x001CAAB8 File Offset: 0x001C8CB8
				public override Value TypedInvoke(ListValue list, Value equationCriteria)
				{
					Value value = Library.List.Modes.Invoke(list, equationCriteria);
					return Library.List.Last.Invoke(value);
				}
			}

			// Token: 0x020014FE RID: 5374
			private class ModesFunctionValue : NativeFunctionValue2<ListValue, ListValue, Value>
			{
				// Token: 0x060086E4 RID: 34532 RVA: 0x001CAADD File Offset: 0x001C8CDD
				public ModesFunctionValue()
					: base(TypeValue.List, 1, "list", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x060086E5 RID: 34533 RVA: 0x001CAB00 File Offset: 0x001C8D00
				public override ListValue TypedInvoke(ListValue list, Value equationCriteria)
				{
					IEqualityComparer<Value> equalityComparer = Library.List.ModesFunctionValue.DefaultComparer;
					if (!equationCriteria.IsNull)
					{
						equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true);
					}
					return ListValue.New(Library.List.ModesFunctionValue.SelectMaxima(Library.List.ModesFunctionValue.CountOccurrences(list, equalityComparer)).ToArray<Value>());
				}

				// Token: 0x060086E6 RID: 34534 RVA: 0x001CAB3C File Offset: 0x001C8D3C
				private static IList<Value> SelectMaxima(Dictionary<Value, uint> occurrences)
				{
					List<Value> list = new List<Value>();
					Value value = Library.List.ModesFunctionValue.SelectMaximum(occurrences);
					if (value == null)
					{
						return list;
					}
					uint num = occurrences[value];
					foreach (KeyValuePair<Value, uint> keyValuePair in occurrences)
					{
						if (keyValuePair.Value == num)
						{
							list.Add(keyValuePair.Key);
						}
					}
					return list;
				}

				// Token: 0x060086E7 RID: 34535 RVA: 0x001CABB8 File Offset: 0x001C8DB8
				private static Value SelectMaximum(Dictionary<Value, uint> occurrences)
				{
					Value value = occurrences.Keys.FirstOrDefault<Value>();
					foreach (KeyValuePair<Value, uint> keyValuePair in occurrences)
					{
						if (keyValuePair.Value > occurrences[value])
						{
							value = keyValuePair.Key;
						}
					}
					return value;
				}

				// Token: 0x060086E8 RID: 34536 RVA: 0x001CAC24 File Offset: 0x001C8E24
				private static Dictionary<Value, uint> CountOccurrences(ListValue values, IEqualityComparer<Value> comparer)
				{
					Dictionary<Value, uint> dictionary = new Dictionary<Value, uint>(comparer);
					foreach (IValueReference valueReference in values.AsList)
					{
						Value value = valueReference.Value;
						uint num;
						if (!dictionary.TryGetValue(value, out num))
						{
							dictionary.Add(value, 1U);
						}
						else
						{
							dictionary[value] = num + 1U;
						}
					}
					return dictionary;
				}

				// Token: 0x04004AFC RID: 19196
				private static readonly Library.List.ModesFunctionValue.NaNEqualityComparer DefaultComparer = new Library.List.ModesFunctionValue.NaNEqualityComparer();

				// Token: 0x020014FF RID: 5375
				private class NaNEqualityComparer : IEqualityComparer<Value>
				{
					// Token: 0x060086EA RID: 34538 RVA: 0x001CACA4 File Offset: 0x001C8EA4
					public bool Equals(Value a, Value b)
					{
						return (a.IsNumber && b.IsNumber && a.AsNumber.IsNaN && b.AsNumber.IsNaN) || a.Equals(b);
					}

					// Token: 0x060086EB RID: 34539 RVA: 0x001BAF3C File Offset: 0x001B913C
					public int GetHashCode(Value a)
					{
						return a.GetHashCode();
					}
				}
			}

			// Token: 0x02001500 RID: 5376
			private class ProductFunctionValue : NativeFunctionValue2<Value, ListValue, Value>, IAccumulableFunction
			{
				// Token: 0x060086ED RID: 34541 RVA: 0x001CACD9 File Offset: 0x001C8ED9
				public ProductFunctionValue()
					: base(NullableTypeValue.Number, 1, "numbersList", TypeValue.List, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}

				// Token: 0x170023A7 RID: 9127
				// (get) Token: 0x060086EE RID: 34542 RVA: 0x001CAD00 File Offset: 0x001C8F00
				public string EnumerableParameter
				{
					get
					{
						return "numbersList";
					}
				}

				// Token: 0x060086EF RID: 34543 RVA: 0x001CAD07 File Offset: 0x001C8F07
				public override Value TypedInvoke(ListValue numbersList, Value precisionEnum)
				{
					IAccumulator accumulator = new Library.List.ProductFunctionValue.ProductAccumulable(precisionEnum).CreateAccumulator();
					accumulator.AccumulateRange(numbersList);
					return accumulator.Current.Value;
				}

				// Token: 0x060086F0 RID: 34544 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x060086F1 RID: 34545 RVA: 0x001CAD25 File Offset: 0x001C8F25
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.ProductFunctionValue.ProductAccumulable(arguments["precision"]);
				}

				// Token: 0x04004AFD RID: 19197
				private const string enumerableParameter = "numbersList";

				// Token: 0x02001501 RID: 5377
				private sealed class ProductAccumulable : IAccumulable
				{
					// Token: 0x060086F2 RID: 34546 RVA: 0x001CAD37 File Offset: 0x001C8F37
					public ProductAccumulable(Value precisionEnum)
					{
						this.precision = (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber));
					}

					// Token: 0x060086F3 RID: 34547 RVA: 0x001CAD64 File Offset: 0x001C8F64
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.ProductFunctionValue.ProductAccumulable.ProductAccumulator(this);
					}

					// Token: 0x04004AFE RID: 19198
					private readonly Precision precision;

					// Token: 0x02001502 RID: 5378
					private sealed class ProductAccumulator : IAccumulator
					{
						// Token: 0x060086F4 RID: 34548 RVA: 0x001CAD6C File Offset: 0x001C8F6C
						public ProductAccumulator(Library.List.ProductFunctionValue.ProductAccumulable accumulable)
						{
							this.precision = accumulable.precision;
						}

						// Token: 0x170023A8 RID: 9128
						// (get) Token: 0x060086F5 RID: 34549 RVA: 0x001CAD80 File Offset: 0x001C8F80
						public IValueReference Current
						{
							get
							{
								return this.product ?? Value.Null;
							}
						}

						// Token: 0x060086F6 RID: 34550 RVA: 0x001CAD94 File Offset: 0x001C8F94
						public void AccumulateNext(IValueReference next)
						{
							Value value = next.Value;
							if (value.IsNull)
							{
								return;
							}
							if (this.product == null)
							{
								this.product = value;
								this.precision.Multiply(this.product, this.product);
								return;
							}
							this.product = this.precision.Multiply(this.product, value);
						}

						// Token: 0x04004AFF RID: 19199
						private readonly Precision precision;

						// Token: 0x04004B00 RID: 19200
						private Value product;
					}
				}
			}

			// Token: 0x02001503 RID: 5379
			private class SumFunctionValue : NativeFunctionValue2<Value, ListValue, Value>, IAccumulableFunction
			{
				// Token: 0x060086F7 RID: 34551 RVA: 0x001CADF1 File Offset: 0x001C8FF1
				public SumFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}

				// Token: 0x170023A9 RID: 9129
				// (get) Token: 0x060086F8 RID: 34552 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x060086F9 RID: 34553 RVA: 0x001CA274 File Offset: 0x001C8474
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return Library.List.GetListOperationReturnType(arguments, environment);
				}

				// Token: 0x060086FA RID: 34554 RVA: 0x001CAE18 File Offset: 0x001C9018
				public override Value TypedInvoke(ListValue list, Value precisionEnum)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null && precisionEnum.IsNull)
					{
						return foldableListValue.Aggregate(Library.List.Sum);
					}
					IAccumulator accumulator = new Library.List.SumFunctionValue.SumAccumulable(precisionEnum).CreateAccumulator();
					accumulator.AccumulateRange(list);
					return accumulator.Current.Value;
				}

				// Token: 0x060086FB RID: 34555 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x060086FC RID: 34556 RVA: 0x001CAE5F File Offset: 0x001C905F
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.SumFunctionValue.SumAccumulable(arguments["precision"]);
				}

				// Token: 0x04004B01 RID: 19201
				private const string enumerableParameter = "list";

				// Token: 0x02001504 RID: 5380
				private sealed class SumAccumulable : IAccumulable
				{
					// Token: 0x060086FD RID: 34557 RVA: 0x001CAE71 File Offset: 0x001C9071
					public SumAccumulable(Value precisionEnum)
					{
						this.precision = (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber));
					}

					// Token: 0x060086FE RID: 34558 RVA: 0x001CAE9E File Offset: 0x001C909E
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.SumFunctionValue.SumAccumulable.SumAccumulator(this);
					}

					// Token: 0x04004B02 RID: 19202
					private readonly Precision precision;

					// Token: 0x02001505 RID: 5381
					private sealed class SumAccumulator : IAccumulator
					{
						// Token: 0x060086FF RID: 34559 RVA: 0x001CAEA6 File Offset: 0x001C90A6
						public SumAccumulator(Library.List.SumFunctionValue.SumAccumulable accumulable)
						{
							this.precision = accumulable.precision;
						}

						// Token: 0x170023AA RID: 9130
						// (get) Token: 0x06008700 RID: 34560 RVA: 0x001CAEBA File Offset: 0x001C90BA
						public IValueReference Current
						{
							get
							{
								if (this.accumulator != null)
								{
									return this.accumulator.ToValue();
								}
								return Value.Null;
							}
						}

						// Token: 0x06008701 RID: 34561 RVA: 0x001CAED8 File Offset: 0x001C90D8
						public void AccumulateNext(IValueReference next)
						{
							Value value = next.Value;
							if (value.IsNull)
							{
								return;
							}
							if (this.accumulator == null)
							{
								this.accumulator = value.GetAccumulator(this.precision);
							}
							this.accumulator.Add(value);
						}

						// Token: 0x04004B03 RID: 19203
						private readonly Precision precision;

						// Token: 0x04004B04 RID: 19204
						private Accumulator accumulator;
					}
				}
			}

			// Token: 0x02001506 RID: 5382
			private class AverageFunctionValue : NativeFunctionValue2<Value, ListValue, Value>, IAccumulableFunction
			{
				// Token: 0x06008702 RID: 34562 RVA: 0x001CADF1 File Offset: 0x001C8FF1
				public AverageFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}

				// Token: 0x170023AB RID: 9131
				// (get) Token: 0x06008703 RID: 34563 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06008704 RID: 34564 RVA: 0x001CAF1B File Offset: 0x001C911B
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return Library.List.GetListOperationReturnType(arguments, environment).Nullable;
				}

				// Token: 0x06008705 RID: 34565 RVA: 0x001CAF2C File Offset: 0x001C912C
				public override Value TypedInvoke(ListValue list, Value precisionEnum)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null && precisionEnum.IsNull)
					{
						return foldableListValue.Aggregate(Library.List.Average);
					}
					IAccumulator accumulator = new Library.List.AverageFunctionValue.AverageAccumulable(precisionEnum).CreateAccumulator();
					accumulator.AccumulateRange(list);
					return accumulator.Current.Value;
				}

				// Token: 0x06008706 RID: 34566 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06008707 RID: 34567 RVA: 0x001CAF73 File Offset: 0x001C9173
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.AverageFunctionValue.AverageAccumulable(arguments["precision"]);
				}

				// Token: 0x04004B05 RID: 19205
				private const string enumerableParameter = "list";

				// Token: 0x02001507 RID: 5383
				private sealed class AverageAccumulable : IAccumulable
				{
					// Token: 0x06008708 RID: 34568 RVA: 0x001CAF85 File Offset: 0x001C9185
					public AverageAccumulable(Value precisionEnum)
					{
						this.precision = (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber));
					}

					// Token: 0x06008709 RID: 34569 RVA: 0x001CAFB2 File Offset: 0x001C91B2
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.AverageFunctionValue.AverageAccumulable.AverageAccumulator(this);
					}

					// Token: 0x04004B06 RID: 19206
					private readonly Precision precision;

					// Token: 0x02001508 RID: 5384
					private sealed class AverageAccumulator : IAccumulator
					{
						// Token: 0x0600870A RID: 34570 RVA: 0x001CAFBA File Offset: 0x001C91BA
						public AverageAccumulator(Library.List.AverageFunctionValue.AverageAccumulable accumulable)
						{
							this.precision = accumulable.precision;
						}

						// Token: 0x170023AC RID: 9132
						// (get) Token: 0x0600870B RID: 34571 RVA: 0x001CAFD0 File Offset: 0x001C91D0
						public IValueReference Current
						{
							get
							{
								if (this.baseline == null)
								{
									return Value.Null;
								}
								return this.precision.Add(this.precision.Divide(this.accumulator.ToValue(), NumberValue.New(this.count)), this.baseline);
							}
						}

						// Token: 0x0600870C RID: 34572 RVA: 0x001CB020 File Offset: 0x001C9220
						public void AccumulateNext(IValueReference next)
						{
							Value value = next.Value;
							if (value.IsNull)
							{
								return;
							}
							if (this.baseline == null)
							{
								this.baseline = value;
								this.accumulator = this.baseline.GetAccumulator(this.precision);
								this.count = 1L;
								return;
							}
							try
							{
								this.accumulator.Add(this.precision.Subtract(value, this.baseline));
							}
							catch (ValueException ex)
							{
								if (this.count <= 1L || ex.InnerException == null || !(ex.InnerException is OverflowException))
								{
									throw;
								}
								Value value2 = NumberValue.New(this.count);
								Value value3 = this.accumulator.ToValue().Divide(value2);
								this.baseline = this.baseline.Add(value3);
								this.accumulator.Subtract(value3.Multiply(value2));
								this.accumulator.Add(this.precision.Subtract(value, this.baseline));
							}
							this.count += 1L;
						}

						// Token: 0x04004B07 RID: 19207
						private readonly Precision precision;

						// Token: 0x04004B08 RID: 19208
						private Value baseline;

						// Token: 0x04004B09 RID: 19209
						private Accumulator accumulator;

						// Token: 0x04004B0A RID: 19210
						private long count;
					}
				}
			}

			// Token: 0x02001509 RID: 5385
			private class StandardDeviationFunctionValue : NativeFunctionValue1<Value, ListValue>, IAccumulableFunction
			{
				// Token: 0x0600870D RID: 34573 RVA: 0x001CB134 File Offset: 0x001C9334
				public StandardDeviationFunctionValue()
					: base(NullableTypeValue.Number, "numbersList", TypeValue.List)
				{
				}

				// Token: 0x170023AD RID: 9133
				// (get) Token: 0x0600870E RID: 34574 RVA: 0x001CAD00 File Offset: 0x001C8F00
				public string EnumerableParameter
				{
					get
					{
						return "numbersList";
					}
				}

				// Token: 0x0600870F RID: 34575 RVA: 0x001CB14C File Offset: 0x001C934C
				public override Value TypedInvoke(ListValue numbersList)
				{
					FoldableListValue foldableListValue = numbersList as FoldableListValue;
					if (foldableListValue != null)
					{
						return foldableListValue.Aggregate(Library.List.StandardDeviation);
					}
					IAccumulator accumulator = new Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable().CreateAccumulator();
					accumulator.AccumulateRange(numbersList);
					return accumulator.Current.Value;
				}

				// Token: 0x06008710 RID: 34576 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06008711 RID: 34577 RVA: 0x001CB18A File Offset: 0x001C938A
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable();
				}

				// Token: 0x04004B0B RID: 19211
				private const string enumerableParameter = "numbersList";

				// Token: 0x0200150A RID: 5386
				private sealed class StandardDeviationAccumulable : IAccumulable
				{
					// Token: 0x06008712 RID: 34578 RVA: 0x001CB191 File Offset: 0x001C9391
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator();
					}

					// Token: 0x0200150B RID: 5387
					private sealed class StandardDeviationAccumulator : IAccumulator
					{
						// Token: 0x170023AE RID: 9134
						// (get) Token: 0x06008714 RID: 34580 RVA: 0x001CB198 File Offset: 0x001C9398
						public IValueReference Current
						{
							get
							{
								if (this.count == null)
								{
									return Value.Null;
								}
								Value value = this.count.AsNumber.Decrement();
								if (!Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Equals(value, NumberValue.Zero))
								{
									Value value2 = Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Divide(this.sum, value);
									return Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Sqrt(value2.AsNumber);
								}
								double asDouble = this.mean.AsNumber.AsDouble;
								if (!double.IsInfinity(asDouble) && !double.IsNaN(asDouble))
								{
									return this.sum;
								}
								return Library.Number.NaN;
							}
						}

						// Token: 0x06008715 RID: 34581 RVA: 0x001CB228 File Offset: 0x001C9428
						public void AccumulateNext(IValueReference next)
						{
							Value value = next.Value;
							if (value.IsNull)
							{
								return;
							}
							if (this.count == null)
							{
								this.count = NumberValue.One;
								this.mean = value;
								this.sum = NumberValue.Zero;
								return;
							}
							this.count = this.count.AsNumber.Increment();
							Value value2 = Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Subtract(value, this.mean);
							this.mean = Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Add(this.mean, Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Divide(value2, this.count));
							Value value3 = Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Multiply(Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Subtract(value, this.mean), value2);
							this.sum = Library.List.StandardDeviationFunctionValue.StandardDeviationAccumulable.StandardDeviationAccumulator.precision.Add(this.sum, value3);
						}

						// Token: 0x04004B0C RID: 19212
						private static readonly Precision precision = Precision.Double;

						// Token: 0x04004B0D RID: 19213
						private Value count;

						// Token: 0x04004B0E RID: 19214
						private Value mean;

						// Token: 0x04004B0F RID: 19215
						private Value sum;
					}
				}
			}

			// Token: 0x0200150C RID: 5388
			private class MedianFunctionValue : NativeFunctionValue2<Value, ListValue, Value>
			{
				// Token: 0x06008718 RID: 34584 RVA: 0x001CB2FA File Offset: 0x001C94FA
				public MedianFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "comparisonCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06008719 RID: 34585 RVA: 0x001CB31C File Offset: 0x001C951C
				public override Value TypedInvoke(ListValue list, Value comparisonCriteria)
				{
					ListValue asList = Library.List.RemoveNulls.Invoke(list).AsList;
					IComparer<Value> comparer = Library.ListComparisonCriteria.CreateComparer(comparisonCriteria);
					List<Value> list2 = new List<Value>();
					ValueKind? valueKind = null;
					foreach (IValueReference valueReference in asList)
					{
						if (valueReference.Value.IsNumber && valueReference.Value.AsNumber.IsNaN)
						{
							return Library.Number.NaN;
						}
						list2.Add(valueReference.Value);
						if (valueKind == null)
						{
							valueKind = new ValueKind?(valueReference.Value.Kind);
						}
						else
						{
							ValueKind? valueKind2 = valueKind;
							ValueKind kind = valueReference.Value.Kind;
							if (!((valueKind2.GetValueOrDefault() == kind) & (valueKind2 != null)))
							{
								valueKind = new ValueKind?(ValueKind.Any);
							}
						}
					}
					if (list2.Count == 0)
					{
						return Value.Null;
					}
					if (list2.Count == 1)
					{
						comparer.Compare(list2[0], list2[0]);
						return list2[0];
					}
					Value[] array = list2.ToArray();
					int num = Library.List.MedianFunctionValue.QuickSelect(comparer, array, 0, list2.Count - 1, (list2.Count - 1) / 2);
					return Library.List.MedianFunctionValue.GetElement(valueKind.Value, array, num);
				}

				// Token: 0x0600871A RID: 34586 RVA: 0x001CB47C File Offset: 0x001C967C
				private static Value GetElement(ValueKind kind, Value[] elements, int index)
				{
					if (elements.Length % 2 == 1)
					{
						return elements[index];
					}
					switch (kind)
					{
					case ValueKind.Time:
					case ValueKind.DateTime:
					case ValueKind.Duration:
					case ValueKind.Number:
						return Library.List.Average.Invoke(ListValue.New(new Value[]
						{
							elements[index],
							elements[index + 1]
						}));
					}
					return elements[index];
				}

				// Token: 0x0600871B RID: 34587 RVA: 0x001CB4E0 File Offset: 0x001C96E0
				private static int QuickSelect(IComparer<Value> comparer, Value[] list, int begin, int end, int index)
				{
					while (begin < end)
					{
						int num = Library.List.MedianFunctionValue.ChoosePivotIndex(comparer, list, begin, end);
						Library.List.MedianFunctionValue.Swap(list, begin, num);
						int num2 = Library.List.MedianFunctionValue.PartitionLaxed(comparer, list, begin, end) - 1;
						int num3 = Library.List.MedianFunctionValue.PartitionStrict(comparer, list, begin, num2);
						if (index < num3)
						{
							end = num3 - 1;
						}
						else
						{
							if (index < num2)
							{
								break;
							}
							begin = num2 + 1;
						}
					}
					return index;
				}

				// Token: 0x0600871C RID: 34588 RVA: 0x001CB534 File Offset: 0x001C9734
				private static int ChoosePivotIndex(IComparer<Value> comparer, Value[] list, int begin, int end)
				{
					if (end - begin < 1)
					{
						return begin;
					}
					int num = ((comparer.Compare(list[begin], list[end]) < 0) ? begin : end);
					int num2 = ((num == begin) ? end : begin);
					if (end - begin < 2)
					{
						return num;
					}
					int num3 = begin + (end - begin) / 2;
					if (comparer.Compare(list[num3], list[num]) < 0)
					{
						return num;
					}
					if (comparer.Compare(list[num3], list[num2]) < 0)
					{
						return num3;
					}
					return num2;
				}

				// Token: 0x0600871D RID: 34589 RVA: 0x001CB59C File Offset: 0x001C979C
				private static int PartitionStrict(IComparer<Value> comparer, Value[] list, int begin, int end)
				{
					int num = begin;
					Value value = list[begin];
					for (int i = begin; i <= end; i++)
					{
						if (comparer.Compare(list[i], value) < 0)
						{
							if (num < i)
							{
								Library.List.MedianFunctionValue.Swap(list, i, num);
							}
							num++;
						}
					}
					return num;
				}

				// Token: 0x0600871E RID: 34590 RVA: 0x001CB5DC File Offset: 0x001C97DC
				private static int PartitionLaxed(IComparer<Value> comparer, Value[] list, int begin, int end)
				{
					int num = begin;
					Value value = list[begin];
					for (int i = begin; i <= end; i++)
					{
						if (comparer.Compare(list[i], value) <= 0)
						{
							if (num < i)
							{
								Library.List.MedianFunctionValue.Swap(list, i, num);
							}
							num++;
						}
					}
					return num;
				}

				// Token: 0x0600871F RID: 34591 RVA: 0x001CB61C File Offset: 0x001C981C
				private static void Swap(Value[] list, int indexA, int indexB)
				{
					Value value = list[indexA];
					list[indexA] = list[indexB];
					list[indexB] = value;
				}
			}

			// Token: 0x0200150D RID: 5389
			private class PercentileFunctionValue : NativeFunctionValue3<Value, ListValue, Value, Value>, IAccumulableFunction
			{
				// Token: 0x06008720 RID: 34592 RVA: 0x001CB638 File Offset: 0x001C9838
				public PercentileFunctionValue()
					: base(TypeValue.Any, 2, "list", TypeValue.List, "percentiles", TypeValue.Any, "options", Library.List.PercentileFunctionValue.optionType.Nullable)
				{
				}

				// Token: 0x170023AF RID: 9135
				// (get) Token: 0x06008721 RID: 34593 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06008722 RID: 34594 RVA: 0x001CB674 File Offset: 0x001C9874
				public override Value TypedInvoke(ListValue list, Value percentiles, Value options)
				{
					IAccumulator accumulator = new Library.List.PercentileFunctionValue.PercentileAccumulable(new Library.List.PercentileFunctionValue.PercentileCalculation(percentiles, options)).CreateAccumulator();
					accumulator.AccumulateRange(list);
					return accumulator.Current.Value;
				}

				// Token: 0x06008723 RID: 34595 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06008724 RID: 34596 RVA: 0x001CB698 File Offset: 0x001C9898
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new Library.List.PercentileFunctionValue.PercentileAccumulable(new Library.List.PercentileFunctionValue.PercentileCalculation(arguments["percentiles"], arguments["options"]));
				}

				// Token: 0x04004B10 RID: 19216
				private static readonly OptionRecordDefinition optionRecord = new OptionRecordDefinition(new OptionItem[]
				{
					new OptionItem("PercentileMode", Library.PercentileModeEnum.Type.Nullable, Value.Null, OptionItemOption.None, null, null),
					new OptionItem("Precision", Library.PrecisionEnum.Type.Nullable, Value.Null, OptionItemOption.None, null, null)
				});

				// Token: 0x04004B11 RID: 19217
				private static readonly RecordTypeValue optionType = Library.List.PercentileFunctionValue.optionRecord.CreateRecordType();

				// Token: 0x0200150E RID: 5390
				private sealed class PercentileCalculation
				{
					// Token: 0x06008726 RID: 34598 RVA: 0x001CB728 File Offset: 0x001C9928
					public PercentileCalculation(Value percentiles, Value options)
					{
						if (percentiles.IsList)
						{
							this.percentiles = new double[percentiles.AsList.Count];
							if (this.percentiles.Length == 0)
							{
								throw ValueException.NewParameterError<Message0>(Strings.InvalidArgument, percentiles);
							}
							int num = 0;
							foreach (IValueReference valueReference in percentiles.AsList)
							{
								this.percentiles[num++] = valueReference.Value.AsNumber.AsDouble;
							}
							this.returnScalar = false;
						}
						else
						{
							this.percentiles = new double[] { percentiles.AsNumber.AsDouble };
							this.returnScalar = true;
						}
						foreach (double num2 in this.percentiles)
						{
							if (num2 < 0.0 || num2 > 1.0)
							{
								throw ValueException.NumberOutOfRange<Message0>(Strings.Percentile_ProbabilityOutOfRange, NumberValue.New(num2), null);
							}
						}
						OptionsRecord optionsRecord = Library.List.PercentileFunctionValue.optionRecord.CreateOptions("List.Percentile", options);
						Value value;
						Library.PercentileMode percentileMode = (optionsRecord.TryGetValue("PercentileMode", out value) ? Library.PercentileModeEnum.Type.GetValue(value.AsNumber) : Library.PercentileMode.ExcelInc);
						this.precision = (optionsRecord.TryGetValue("Precision", out value) ? Library.PrecisionEnum.Type.GetValue(value.AsNumber) : Precision.Double);
						switch (percentileMode)
						{
						case Library.PercentileMode.ExcelInc:
						case Library.PercentileMode.SqlCont:
							this.a = 1.0;
							this.b = -1.0;
							this.c = 0.0;
							this.d = 1.0;
							this.exclusive = false;
							return;
						case Library.PercentileMode.ExcelExc:
							this.a = 0.0;
							this.b = 1.0;
							this.c = 0.0;
							this.d = 1.0;
							this.exclusive = true;
							return;
						case Library.PercentileMode.SqlDisc:
							this.a = 0.0;
							this.b = 0.0;
							this.c = 1.0;
							this.d = 0.0;
							this.exclusive = false;
							return;
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					}

					// Token: 0x170023B0 RID: 9136
					// (get) Token: 0x06008727 RID: 34599 RVA: 0x001CB9A0 File Offset: 0x001C9BA0
					public bool ReturnScalar
					{
						get
						{
							return this.returnScalar;
						}
					}

					// Token: 0x170023B1 RID: 9137
					// (get) Token: 0x06008728 RID: 34600 RVA: 0x001CB9A8 File Offset: 0x001C9BA8
					public int Length
					{
						get
						{
							return this.percentiles.Length;
						}
					}

					// Token: 0x170023B2 RID: 9138
					// (get) Token: 0x06008729 RID: 34601 RVA: 0x001CB9B2 File Offset: 0x001C9BB2
					public bool IsDecimalPrecision
					{
						get
						{
							return this.precision == Precision.Decimal;
						}
					}

					// Token: 0x0600872A RID: 34602 RVA: 0x001CB9C4 File Offset: 0x001C9BC4
					public double? GetPosition(int index, int count)
					{
						double num = 1.0;
						double num2 = (double)count;
						double num3 = this.a + ((double)count + this.b) * this.percentiles[index];
						if (num3 < num)
						{
							if (!this.exclusive)
							{
								return new double?(0.0);
							}
							return null;
						}
						else
						{
							if (num3 <= num2)
							{
								return new double?(num3 - num);
							}
							if (!this.exclusive)
							{
								return new double?(num2 - num);
							}
							return null;
						}
					}

					// Token: 0x0600872B RID: 34603 RVA: 0x001CBA44 File Offset: 0x001C9C44
					public double Interpolate(double low, double high, double fract)
					{
						if (low == high)
						{
							return low;
						}
						double num = this.c + this.d * fract;
						if (num == 1.0)
						{
							return high;
						}
						if (!double.IsNegativeInfinity(low))
						{
							return low + (high - low) * num;
						}
						if (!double.IsPositiveInfinity(high))
						{
							return low;
						}
						return double.NaN;
					}

					// Token: 0x0600872C RID: 34604 RVA: 0x001CBA98 File Offset: 0x001C9C98
					public decimal Interpolate(decimal low, decimal high, double fract)
					{
						if (low == high)
						{
							return low;
						}
						double num = this.c + this.d * fract;
						if (num == 1.0)
						{
							return high;
						}
						return low + (high - low) * (decimal)num;
					}

					// Token: 0x0600872D RID: 34605 RVA: 0x001CBAE8 File Offset: 0x001C9CE8
					public long Interpolate(long low, long high, double fract)
					{
						if (low == high)
						{
							return low;
						}
						double num = this.c + this.d * fract;
						if (num == 1.0)
						{
							return high;
						}
						return low + (long)((double)(high - low) * num);
					}

					// Token: 0x04004B12 RID: 19218
					private readonly double[] percentiles;

					// Token: 0x04004B13 RID: 19219
					private readonly double a;

					// Token: 0x04004B14 RID: 19220
					private readonly double b;

					// Token: 0x04004B15 RID: 19221
					private readonly double c;

					// Token: 0x04004B16 RID: 19222
					private readonly double d;

					// Token: 0x04004B17 RID: 19223
					private readonly bool exclusive;

					// Token: 0x04004B18 RID: 19224
					private readonly bool returnScalar;

					// Token: 0x04004B19 RID: 19225
					private readonly Precision precision;
				}

				// Token: 0x0200150F RID: 5391
				private sealed class PercentileAccumulable : IAccumulable
				{
					// Token: 0x0600872E RID: 34606 RVA: 0x001CBB22 File Offset: 0x001C9D22
					public PercentileAccumulable(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
					{
						this.calculation = calculation;
					}

					// Token: 0x0600872F RID: 34607 RVA: 0x001CBB31 File Offset: 0x001C9D31
					public IAccumulator CreateAccumulator()
					{
						return new Library.List.PercentileFunctionValue.PercentileAccumulable.PercentileAccumulator(this.calculation);
					}

					// Token: 0x04004B1A RID: 19226
					private readonly Library.List.PercentileFunctionValue.PercentileCalculation calculation;

					// Token: 0x02001510 RID: 5392
					private sealed class PercentileAccumulator : IAccumulator
					{
						// Token: 0x06008730 RID: 34608 RVA: 0x001CBB3E File Offset: 0x001C9D3E
						public PercentileAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
						{
							this.calculation = calculation;
						}

						// Token: 0x170023B3 RID: 9139
						// (get) Token: 0x06008731 RID: 34609 RVA: 0x001CBB50 File Offset: 0x001C9D50
						public IValueReference Current
						{
							get
							{
								if (this.accumulator == null)
								{
									return Value.Null;
								}
								ListValue currentResult = this.accumulator.GetCurrentResult();
								if (!this.calculation.ReturnScalar)
								{
									return currentResult;
								}
								return currentResult[0];
							}
						}

						// Token: 0x06008732 RID: 34610 RVA: 0x001CBB90 File Offset: 0x001C9D90
						public void AccumulateNext(IValueReference next)
						{
							Value value = next.Value;
							if (value.IsNull)
							{
								return;
							}
							if (this.accumulator == null)
							{
								switch (value.Kind)
								{
								case ValueKind.Time:
									this.accumulator = new Library.List.PercentileFunctionValue.PercentileAccumulable.TimeAccumulator(this.calculation);
									break;
								case ValueKind.Date:
									this.accumulator = new Library.List.PercentileFunctionValue.PercentileAccumulable.DateAccumulator(this.calculation);
									break;
								case ValueKind.DateTime:
									this.accumulator = new Library.List.PercentileFunctionValue.PercentileAccumulable.DateTimeAccumulator(this.calculation);
									break;
								case ValueKind.DateTimeZone:
									this.accumulator = new Library.List.PercentileFunctionValue.PercentileAccumulable.DateTimeZoneAccumulator(this.calculation);
									break;
								case ValueKind.Duration:
									this.accumulator = new Library.List.PercentileFunctionValue.PercentileAccumulable.DurationAccumulator(this.calculation);
									break;
								case ValueKind.Number:
									this.accumulator = (this.calculation.IsDecimalPrecision ? new Library.List.PercentileFunctionValue.PercentileAccumulable.DecimalAccumulator(this.calculation) : new Library.List.PercentileFunctionValue.PercentileAccumulable.DoubleAccumulator(this.calculation));
									break;
								default:
									throw ValueException.NewExpressionError<Message0>(Strings.Percentile_UnsupportedType, value, null);
								}
							}
							if (value.Kind != this.accumulator.ValueKind)
							{
								this.accumulator = null;
								throw ValueException.NewExpressionError<Message0>(Strings.Percentile_MixedType, value, null);
							}
							this.accumulator.AccumulateNext(value);
						}

						// Token: 0x04004B1B RID: 19227
						private readonly Library.List.PercentileFunctionValue.PercentileCalculation calculation;

						// Token: 0x04004B1C RID: 19228
						private Library.List.PercentileFunctionValue.PercentileAccumulable.PercentileAccumulatorBase accumulator;
					}

					// Token: 0x02001511 RID: 5393
					private abstract class PercentileAccumulatorBase
					{
						// Token: 0x06008733 RID: 34611 RVA: 0x001CBCB3 File Offset: 0x001C9EB3
						protected PercentileAccumulatorBase(Library.List.PercentileFunctionValue.PercentileCalculation calculation, ValueKind valueKind)
						{
							this.calculation = calculation;
							this.valueKind = valueKind;
						}

						// Token: 0x170023B4 RID: 9140
						// (get) Token: 0x06008734 RID: 34612 RVA: 0x001CBCC9 File Offset: 0x001C9EC9
						public Library.List.PercentileFunctionValue.PercentileCalculation Calculation
						{
							get
							{
								return this.calculation;
							}
						}

						// Token: 0x170023B5 RID: 9141
						// (get) Token: 0x06008735 RID: 34613 RVA: 0x001CBCD1 File Offset: 0x001C9ED1
						public ValueKind ValueKind
						{
							get
							{
								return this.valueKind;
							}
						}

						// Token: 0x06008736 RID: 34614
						public abstract void AccumulateNext(Value value);

						// Token: 0x06008737 RID: 34615
						public abstract ListValue GetCurrentResult();

						// Token: 0x04004B1D RID: 19229
						private readonly Library.List.PercentileFunctionValue.PercentileCalculation calculation;

						// Token: 0x04004B1E RID: 19230
						private readonly ValueKind valueKind;
					}

					// Token: 0x02001512 RID: 5394
					private abstract class PercentileAccumulator<T> : Library.List.PercentileFunctionValue.PercentileAccumulable.PercentileAccumulatorBase
					{
						// Token: 0x06008738 RID: 34616 RVA: 0x001CBCD9 File Offset: 0x001C9ED9
						public PercentileAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation, ValueKind valueKind)
							: base(calculation, valueKind)
						{
							this.values = new List<T>();
						}

						// Token: 0x06008739 RID: 34617 RVA: 0x001CBCF0 File Offset: 0x001C9EF0
						public override void AccumulateNext(Value next)
						{
							T t = this.FromValue(next);
							if (!this.sorted)
							{
								this.values.Add(t);
								return;
							}
							int num = this.values.BinarySearch(t);
							if (num >= 0)
							{
								this.values.Insert(num, t);
								return;
							}
							this.values.Insert(~num, t);
						}

						// Token: 0x0600873A RID: 34618 RVA: 0x001CBD48 File Offset: 0x001C9F48
						public override ListValue GetCurrentResult()
						{
							if (!this.sorted)
							{
								this.values.Sort();
								this.sorted = true;
							}
							Value[] array = new Value[base.Calculation.Length];
							for (int i = 0; i < base.Calculation.Length; i++)
							{
								double? position = base.Calculation.GetPosition(i, this.values.Count);
								if (position == null)
								{
									array[i] = Value.Null;
								}
								else
								{
									double num = Math.Floor(position.Value);
									double num2 = position.Value - num;
									T t = this.values[(int)num];
									T t2;
									if (num2 == 0.0)
									{
										t2 = t;
									}
									else
									{
										T t3 = this.values[(int)num + 1];
										t2 = this.Interpolate(t, t3, num2);
									}
									array[i] = this.ToValue(t2);
								}
							}
							return ListValue.New(array);
						}

						// Token: 0x0600873B RID: 34619
						protected abstract T FromValue(Value value);

						// Token: 0x0600873C RID: 34620
						protected abstract Value ToValue(T value);

						// Token: 0x0600873D RID: 34621
						protected abstract T Interpolate(T low, T high, double fract);

						// Token: 0x04004B1F RID: 19231
						private readonly List<T> values;

						// Token: 0x04004B20 RID: 19232
						private bool sorted;
					}

					// Token: 0x02001513 RID: 5395
					private sealed class DoubleAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.PercentileAccumulator<double>
					{
						// Token: 0x0600873E RID: 34622 RVA: 0x001CBE31 File Offset: 0x001CA031
						public DoubleAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.Number)
						{
						}

						// Token: 0x0600873F RID: 34623 RVA: 0x001CBE3B File Offset: 0x001CA03B
						protected override double FromValue(Value value)
						{
							NumberValue asNumber = value.AsNumber;
							if (asNumber.IsNaN)
							{
								this.hasNan = true;
							}
							return asNumber.AsDouble;
						}

						// Token: 0x06008740 RID: 34624 RVA: 0x001CBE57 File Offset: 0x001CA057
						protected override Value ToValue(double value)
						{
							if (this.hasNan)
							{
								return NumberValue.NaN;
							}
							return NumberValue.New(value);
						}

						// Token: 0x06008741 RID: 34625 RVA: 0x001CBE6D File Offset: 0x001CA06D
						protected override double Interpolate(double low, double high, double fract)
						{
							return base.Calculation.Interpolate(low, high, fract);
						}

						// Token: 0x04004B21 RID: 19233
						private bool hasNan;
					}

					// Token: 0x02001514 RID: 5396
					private sealed class DecimalAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.PercentileAccumulator<decimal>
					{
						// Token: 0x06008742 RID: 34626 RVA: 0x001CBE7D File Offset: 0x001CA07D
						public DecimalAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.Number)
						{
						}

						// Token: 0x06008743 RID: 34627 RVA: 0x001CBE87 File Offset: 0x001CA087
						protected override decimal FromValue(Value value)
						{
							return value.AsNumber.AsDecimal;
						}

						// Token: 0x06008744 RID: 34628 RVA: 0x00019E51 File Offset: 0x00018051
						protected override Value ToValue(decimal value)
						{
							return NumberValue.New(value);
						}

						// Token: 0x06008745 RID: 34629 RVA: 0x001CBE94 File Offset: 0x001CA094
						protected override decimal Interpolate(decimal low, decimal high, double fract)
						{
							return base.Calculation.Interpolate(low, high, fract);
						}
					}

					// Token: 0x02001515 RID: 5397
					private abstract class LongAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.PercentileAccumulator<long>
					{
						// Token: 0x06008746 RID: 34630 RVA: 0x001CBEA4 File Offset: 0x001CA0A4
						protected LongAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation, ValueKind valueKind)
							: base(calculation, valueKind)
						{
						}

						// Token: 0x06008747 RID: 34631 RVA: 0x001CBEAE File Offset: 0x001CA0AE
						protected override long Interpolate(long low, long high, double fract)
						{
							return base.Calculation.Interpolate(low, high, fract);
						}
					}

					// Token: 0x02001516 RID: 5398
					private sealed class DateAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.LongAccumulator
					{
						// Token: 0x06008748 RID: 34632 RVA: 0x001CBEBE File Offset: 0x001CA0BE
						public DateAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.Date)
						{
						}

						// Token: 0x06008749 RID: 34633 RVA: 0x001CBEC8 File Offset: 0x001CA0C8
						protected override long FromValue(Value value)
						{
							return value.AsDate.AsClrDateTime.Ticks;
						}

						// Token: 0x0600874A RID: 34634 RVA: 0x001CBEE8 File Offset: 0x001CA0E8
						protected override Value ToValue(long value)
						{
							return DateValue.New(value);
						}
					}

					// Token: 0x02001517 RID: 5399
					private sealed class DateTimeAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.LongAccumulator
					{
						// Token: 0x0600874B RID: 34635 RVA: 0x001CBEF0 File Offset: 0x001CA0F0
						public DateTimeAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.DateTime)
						{
						}

						// Token: 0x0600874C RID: 34636 RVA: 0x001CBEFC File Offset: 0x001CA0FC
						protected override long FromValue(Value value)
						{
							return value.AsDateTime.AsClrDateTime.Ticks;
						}

						// Token: 0x0600874D RID: 34637 RVA: 0x001CBF1C File Offset: 0x001CA11C
						protected override Value ToValue(long value)
						{
							return DateTimeValue.New(value);
						}
					}

					// Token: 0x02001518 RID: 5400
					private sealed class DateTimeZoneAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.LongAccumulator
					{
						// Token: 0x0600874E RID: 34638 RVA: 0x001CBF24 File Offset: 0x001CA124
						public DateTimeZoneAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.DateTimeZone)
						{
						}

						// Token: 0x0600874F RID: 34639 RVA: 0x001CBF30 File Offset: 0x001CA130
						protected override long FromValue(Value value)
						{
							DateTimeOffset asClrDateTimeOffset = value.AsDateTimeZone.AsClrDateTimeOffset;
							if (this.offset == null)
							{
								this.offset = new TimeSpan?(asClrDateTimeOffset.Offset);
							}
							return asClrDateTimeOffset.UtcTicks;
						}

						// Token: 0x06008750 RID: 34640 RVA: 0x001CBF70 File Offset: 0x001CA170
						protected override Value ToValue(long value)
						{
							return DateTimeZoneValue.New(new DateTimeOffset(value, TimeSpan.Zero).ToOffset(this.offset.Value));
						}

						// Token: 0x04004B22 RID: 19234
						private TimeSpan? offset;
					}

					// Token: 0x02001519 RID: 5401
					private sealed class DurationAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.LongAccumulator
					{
						// Token: 0x06008751 RID: 34641 RVA: 0x001CBFA0 File Offset: 0x001CA1A0
						public DurationAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.Duration)
						{
						}

						// Token: 0x06008752 RID: 34642 RVA: 0x001CBFAC File Offset: 0x001CA1AC
						protected override long FromValue(Value value)
						{
							return value.AsDuration.AsClrTimeSpan.Ticks;
						}

						// Token: 0x06008753 RID: 34643 RVA: 0x001CBFCC File Offset: 0x001CA1CC
						protected override Value ToValue(long value)
						{
							return DurationValue.New(value);
						}
					}

					// Token: 0x0200151A RID: 5402
					private sealed class TimeAccumulator : Library.List.PercentileFunctionValue.PercentileAccumulable.LongAccumulator
					{
						// Token: 0x06008754 RID: 34644 RVA: 0x001CBFD4 File Offset: 0x001CA1D4
						public TimeAccumulator(Library.List.PercentileFunctionValue.PercentileCalculation calculation)
							: base(calculation, ValueKind.Time)
						{
						}

						// Token: 0x06008755 RID: 34645 RVA: 0x001CBFE0 File Offset: 0x001CA1E0
						protected override long FromValue(Value value)
						{
							return value.AsTime.AsClrTimeSpan.Ticks;
						}

						// Token: 0x06008756 RID: 34646 RVA: 0x001CC000 File Offset: 0x001CA200
						protected override Value ToValue(long value)
						{
							return TimeValue.New(value);
						}
					}
				}
			}

			// Token: 0x0200151B RID: 5403
			private class SequenceListValue : StreamedListValue
			{
				// Token: 0x06008757 RID: 34647 RVA: 0x001CC008 File Offset: 0x001CA208
				public SequenceListValue(Value start, Value count, Value increment)
				{
					if ((count.IsNumber && count.AsNumber.LessThan(NumberValue.Zero)) || (count.IsDuration && count.AsDuration.AsTimeSpan.Ticks < 0L))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					this.start = start;
					this.count = count;
					this.increment = increment;
				}

				// Token: 0x06008758 RID: 34648 RVA: 0x001CC075 File Offset: 0x001CA275
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					return new Library.List.SequenceListValue.SequenceEnumerator(this);
				}

				// Token: 0x04004B23 RID: 19235
				private readonly Value start;

				// Token: 0x04004B24 RID: 19236
				private readonly Value count;

				// Token: 0x04004B25 RID: 19237
				private readonly Value increment;

				// Token: 0x0200151C RID: 5404
				private class SequenceEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
				{
					// Token: 0x06008759 RID: 34649 RVA: 0x001CC07D File Offset: 0x001CA27D
					public SequenceEnumerator(Library.List.SequenceListValue list)
					{
						this.list = list;
						this.index = NumberValue.Zero;
					}

					// Token: 0x170023B6 RID: 9142
					// (get) Token: 0x0600875A RID: 34650 RVA: 0x001CC097 File Offset: 0x001CA297
					public IValueReference Current
					{
						get
						{
							return this.current;
						}
					}

					// Token: 0x170023B7 RID: 9143
					// (get) Token: 0x0600875B RID: 34651 RVA: 0x001CC09F File Offset: 0x001CA29F
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x0600875C RID: 34652 RVA: 0x0000EE09 File Offset: 0x0000D009
					public void Reset()
					{
						throw new InvalidOperationException();
					}

					// Token: 0x0600875D RID: 34653 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}

					// Token: 0x0600875E RID: 34654 RVA: 0x001CC0A8 File Offset: 0x001CA2A8
					public bool MoveNext()
					{
						if (this.index.LessThan(this.list.count))
						{
							this.current = this.list.start.Add(this.list.increment.Multiply(this.index));
							this.index = this.index.Increment();
							return true;
						}
						this.current = null;
						return false;
					}

					// Token: 0x04004B26 RID: 19238
					private readonly Library.List.SequenceListValue list;

					// Token: 0x04004B27 RID: 19239
					private NumberValue index;

					// Token: 0x04004B28 RID: 19240
					private Value current;
				}
			}

			// Token: 0x0200151D RID: 5405
			private class NumbersFunctionValue : NativeFunctionValue3<ListValue, NumberValue, NumberValue, Value>
			{
				// Token: 0x0600875F RID: 34655 RVA: 0x001CC114 File Offset: 0x001CA314
				public NumbersFunctionValue()
					: base(TypeValue.List, 2, "start", TypeValue.Number, "count", TypeValue.Number, "increment", NullableTypeValue.Number)
				{
				}

				// Token: 0x06008760 RID: 34656 RVA: 0x001CC14B File Offset: 0x001CA34B
				public override ListValue TypedInvoke(NumberValue start, NumberValue count, Value increment)
				{
					return new Library.List.SequenceListValue(start, count, increment.IsNull ? NumberValue.One : increment.AsNumber);
				}
			}

			// Token: 0x0200151E RID: 5406
			private class DurationsFunctionValue : NativeFunctionValue3<ListValue, DurationValue, NumberValue, DurationValue>
			{
				// Token: 0x06008761 RID: 34657 RVA: 0x001CC16C File Offset: 0x001CA36C
				public DurationsFunctionValue()
					: base(TypeValue.List, 3, "start", TypeValue.Duration, "count", TypeValue.Number, "step", TypeValue.Duration)
				{
				}

				// Token: 0x06008762 RID: 34658 RVA: 0x001CC1A3 File Offset: 0x001CA3A3
				public override ListValue TypedInvoke(DurationValue start, NumberValue count, DurationValue step)
				{
					return new Library.List.SequenceListValue(start, count, step);
				}
			}

			// Token: 0x0200151F RID: 5407
			private class TimesFunctionValue : NativeFunctionValue3<ListValue, TimeValue, NumberValue, DurationValue>
			{
				// Token: 0x06008763 RID: 34659 RVA: 0x001CC1B0 File Offset: 0x001CA3B0
				public TimesFunctionValue()
					: base(TypeValue.List, 3, "start", TypeValue.Time, "count", TypeValue.Number, "step", TypeValue.Duration)
				{
				}

				// Token: 0x06008764 RID: 34660 RVA: 0x001CC1A3 File Offset: 0x001CA3A3
				public override ListValue TypedInvoke(TimeValue start, NumberValue count, DurationValue step)
				{
					return new Library.List.SequenceListValue(start, count, step);
				}
			}

			// Token: 0x02001520 RID: 5408
			private class DatesFunctionValue : NativeFunctionValue3<ListValue, DateValue, NumberValue, DurationValue>
			{
				// Token: 0x06008765 RID: 34661 RVA: 0x001CC1E8 File Offset: 0x001CA3E8
				public DatesFunctionValue()
					: base(TypeValue.List, 3, "start", TypeValue.Date, "count", TypeValue.Number, "step", TypeValue.Duration)
				{
				}

				// Token: 0x06008766 RID: 34662 RVA: 0x001CC1A3 File Offset: 0x001CA3A3
				public override ListValue TypedInvoke(DateValue start, NumberValue count, DurationValue step)
				{
					return new Library.List.SequenceListValue(start, count, step);
				}
			}

			// Token: 0x02001521 RID: 5409
			private class DateTimesFunctionValue : NativeFunctionValue3<ListValue, DateTimeValue, NumberValue, DurationValue>
			{
				// Token: 0x06008767 RID: 34663 RVA: 0x001CC220 File Offset: 0x001CA420
				public DateTimesFunctionValue()
					: base(TypeValue.List, 3, "start", TypeValue.DateTime, "count", TypeValue.Number, "step", TypeValue.Duration)
				{
				}

				// Token: 0x06008768 RID: 34664 RVA: 0x001CC1A3 File Offset: 0x001CA3A3
				public override ListValue TypedInvoke(DateTimeValue start, NumberValue count, DurationValue step)
				{
					return new Library.List.SequenceListValue(start, count, step);
				}
			}

			// Token: 0x02001522 RID: 5410
			private class DateTimeZonesFunctionValue : NativeFunctionValue3<ListValue, DateTimeZoneValue, NumberValue, DurationValue>
			{
				// Token: 0x06008769 RID: 34665 RVA: 0x001CC258 File Offset: 0x001CA458
				public DateTimeZonesFunctionValue()
					: base(TypeValue.List, 3, "start", TypeValue.DateTimeZone, "count", TypeValue.Number, "step", TypeValue.Duration)
				{
				}

				// Token: 0x0600876A RID: 34666 RVA: 0x001CC1A3 File Offset: 0x001CA3A3
				public override ListValue TypedInvoke(DateTimeZoneValue start, NumberValue count, DurationValue step)
				{
					return new Library.List.SequenceListValue(start, count, step);
				}
			}

			// Token: 0x02001523 RID: 5411
			public sealed class RandomFunctionValue : NativeFunctionValue2<ListValue, NumberValue, Value>
			{
				// Token: 0x0600876B RID: 34667 RVA: 0x001CC28F File Offset: 0x001CA48F
				public RandomFunctionValue(IEngineHost engineHost)
					: base(TypeValue.List, 1, "count", TypeValue.Number, "seed", NullableTypeValue.Number)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x170023B8 RID: 9144
				// (get) Token: 0x0600876C RID: 34668 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x0600876D RID: 34669 RVA: 0x001CC2B8 File Offset: 0x001CA4B8
				public override bool TryGetAs<T>(out T contract)
				{
					if (typeof(T) == typeof(IOpaqueFunctionValue) && this.engineHost.ThrowOnVolatileFunctions())
					{
						contract = (T)((object)FoldableFunctionValue.New(this));
						return true;
					}
					return base.TryGetAs<T>(out contract);
				}

				// Token: 0x0600876E RID: 34670 RVA: 0x001CC307 File Offset: 0x001CA507
				public override ListValue TypedInvoke(NumberValue count, Value seed)
				{
					this.engineHost.CheckVolatileFunctionsAllowed();
					return LanguageLibrary.List.Take.Invoke(new Library.List.RandomFunctionValue.RandomNumberListValue(seed), count).AsList;
				}

				// Token: 0x04004B29 RID: 19241
				private readonly IEngineHost engineHost;

				// Token: 0x02001524 RID: 5412
				private class RandomNumberListValue : StreamedListValue
				{
					// Token: 0x0600876F RID: 34671 RVA: 0x001CC32A File Offset: 0x001CA52A
					public RandomNumberListValue(Value seed)
					{
						this.seed = seed;
					}

					// Token: 0x06008770 RID: 34672 RVA: 0x001CC33C File Offset: 0x001CA53C
					private static int GenerateSeed()
					{
						byte[] array = new byte[4];
						RNGCryptoProvider.Create().GetBytes(array);
						return BitConverter.ToInt32(array, 0);
					}

					// Token: 0x06008771 RID: 34673 RVA: 0x001CC362 File Offset: 0x001CA562
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new Library.List.RandomFunctionValue.RandomNumberListValue.RandomNumberEnumerator(new Random(this.seed.IsNull ? Library.List.RandomFunctionValue.RandomNumberListValue.GenerateSeed() : this.seed.AsInteger32));
					}

					// Token: 0x04004B2A RID: 19242
					private readonly Value seed;

					// Token: 0x02001525 RID: 5413
					private class RandomNumberEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06008772 RID: 34674 RVA: 0x001CC38D File Offset: 0x001CA58D
						public RandomNumberEnumerator(Random random)
						{
							this.random = random;
						}

						// Token: 0x170023B9 RID: 9145
						// (get) Token: 0x06008773 RID: 34675 RVA: 0x001CC39C File Offset: 0x001CA59C
						public IValueReference Current
						{
							get
							{
								return NumberValue.New(this.value);
							}
						}

						// Token: 0x170023BA RID: 9146
						// (get) Token: 0x06008774 RID: 34676 RVA: 0x001CC3A9 File Offset: 0x001CA5A9
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008775 RID: 34677 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008776 RID: 34678 RVA: 0x0000336E File Offset: 0x0000156E
						public void Dispose()
						{
						}

						// Token: 0x06008777 RID: 34679 RVA: 0x001CC3B1 File Offset: 0x001CA5B1
						public bool MoveNext()
						{
							this.value = this.random.NextDouble();
							return true;
						}

						// Token: 0x04004B2B RID: 19243
						private readonly Random random;

						// Token: 0x04004B2C RID: 19244
						private double value;
					}
				}
			}

			// Token: 0x02001526 RID: 5414
			private class CountOfDistinctFunctionValue : NativeFunctionValue1<NumberValue, ListValue>
			{
				// Token: 0x06008778 RID: 34680 RVA: 0x001CC3C5 File Offset: 0x001CA5C5
				public CountOfDistinctFunctionValue()
					: base(TypeValue.Number, "list", TypeValue.List)
				{
				}

				// Token: 0x06008779 RID: 34681 RVA: 0x001CC3DC File Offset: 0x001CA5DC
				public override NumberValue TypedInvoke(ListValue list)
				{
					return LanguageLibrary.List.Count.Invoke(LanguageLibrary.List.Distinct.Invoke(list)).AsNumber;
				}
			}

			// Token: 0x02001527 RID: 5415
			private class CountOfNullFunctionValue : NativeFunctionValue1<NumberValue, ListValue>
			{
				// Token: 0x0600877A RID: 34682 RVA: 0x001CC3C5 File Offset: 0x001CA5C5
				public CountOfNullFunctionValue()
					: base(TypeValue.Number, "list", TypeValue.List)
				{
				}

				// Token: 0x0600877B RID: 34683 RVA: 0x001CC3F8 File Offset: 0x001CA5F8
				public override NumberValue TypedInvoke(ListValue list)
				{
					return LanguageLibrary.List.Count.Invoke(LanguageLibrary.List.Select.Invoke(list, Library.List.CountOfNullFunctionValue.IsNullFunctionValue.Instance)).AsNumber;
				}

				// Token: 0x02001528 RID: 5416
				protected sealed class IsNullFunctionValue : NativeFunctionValue1<LogicalValue, Value>
				{
					// Token: 0x0600877C RID: 34684 RVA: 0x0004815D File Offset: 0x0004635D
					private IsNullFunctionValue()
						: base(TypeValue.Logical, "value", TypeValue.Any)
					{
					}

					// Token: 0x0600877D RID: 34685 RVA: 0x001CC419 File Offset: 0x001CA619
					public override LogicalValue TypedInvoke(Value value)
					{
						return LogicalValue.New(value.IsNull);
					}

					// Token: 0x04004B2D RID: 19245
					public static readonly FunctionValue Instance = new Library.List.CountOfNullFunctionValue.IsNullFunctionValue();
				}
			}

			// Token: 0x02001529 RID: 5417
			private class CountOfDistinctNullFunctionValue : Library.List.CountOfNullFunctionValue
			{
				// Token: 0x06008780 RID: 34688 RVA: 0x001CC43A File Offset: 0x001CA63A
				public override NumberValue TypedInvoke(ListValue list)
				{
					return LanguageLibrary.List.Count.Invoke(LanguageLibrary.List.Distinct.Invoke(LanguageLibrary.List.Select.Invoke(list, Library.List.CountOfNullFunctionValue.IsNullFunctionValue.Instance))).AsNumber;
				}
			}

			// Token: 0x0200152A RID: 5418
			private class CountOfNotNullFunctionValue : NativeFunctionValue1<NumberValue, ListValue>
			{
				// Token: 0x06008781 RID: 34689 RVA: 0x001CC3C5 File Offset: 0x001CA5C5
				public CountOfNotNullFunctionValue()
					: base(TypeValue.Number, "list", TypeValue.List)
				{
				}

				// Token: 0x06008782 RID: 34690 RVA: 0x001CC465 File Offset: 0x001CA665
				public override NumberValue TypedInvoke(ListValue list)
				{
					return LanguageLibrary.List.Count.Invoke(LanguageLibrary.List.Select.Invoke(list, Library.List.CountOfNotNullFunctionValue.IsNotNullFunctionValue.Instance)).AsNumber;
				}

				// Token: 0x0200152B RID: 5419
				protected sealed class IsNotNullFunctionValue : NativeFunctionValue1<LogicalValue, Value>
				{
					// Token: 0x06008783 RID: 34691 RVA: 0x0004815D File Offset: 0x0004635D
					private IsNotNullFunctionValue()
						: base(TypeValue.Logical, "value", TypeValue.Any)
					{
					}

					// Token: 0x06008784 RID: 34692 RVA: 0x000674B8 File Offset: 0x000656B8
					public override LogicalValue TypedInvoke(Value value)
					{
						return LogicalValue.New(!value.IsNull);
					}

					// Token: 0x04004B2E RID: 19246
					public static readonly FunctionValue Instance = new Library.List.CountOfNotNullFunctionValue.IsNotNullFunctionValue();
				}
			}

			// Token: 0x0200152C RID: 5420
			private class CountOfDistinctNotNullFunctionValue : Library.List.CountOfNotNullFunctionValue
			{
				// Token: 0x06008787 RID: 34695 RVA: 0x001CC49A File Offset: 0x001CA69A
				public override NumberValue TypedInvoke(ListValue list)
				{
					return LanguageLibrary.List.Count.Invoke(LanguageLibrary.List.Distinct.Invoke(LanguageLibrary.List.Select.Invoke(list, Library.List.CountOfNotNullFunctionValue.IsNotNullFunctionValue.Instance))).AsNumber;
				}
			}
		}

		// Token: 0x0200152D RID: 5421
		public static class Enumerator
		{
			// Token: 0x04004B2F RID: 19247
			public static readonly FunctionValue ToList = new Library.Enumerator.ToListFunctionValue();

			// Token: 0x04004B30 RID: 19248
			public static readonly FunctionValue FromList = new Library.Enumerator.FromListFunctionValue();

			// Token: 0x0200152E RID: 5422
			private class ToListFunctionValue : NativeFunctionValue1<ListValue, FunctionValue>
			{
				// Token: 0x06008789 RID: 34697 RVA: 0x001CC4DB File Offset: 0x001CA6DB
				public ToListFunctionValue()
					: base(TypeValue.List, "value", TypeValue.Function)
				{
				}

				// Token: 0x0600878A RID: 34698 RVA: 0x001CC4F2 File Offset: 0x001CA6F2
				public override ListValue TypedInvoke(FunctionValue value)
				{
					return new Library.Enumerator.ToListFunctionValue.FunctionListValue(value);
				}

				// Token: 0x0200152F RID: 5423
				private class FunctionListValue : StreamedListValue
				{
					// Token: 0x0600878B RID: 34699 RVA: 0x001CC4FA File Offset: 0x001CA6FA
					public FunctionListValue(FunctionValue function)
					{
						this.function = function;
					}

					// Token: 0x0600878C RID: 34700 RVA: 0x001CC50C File Offset: 0x001CA70C
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						RecordValue asRecord = this.function.Invoke().AsRecord;
						return new Library.Enumerator.ToListFunctionValue.FunctionListValue.FunctionEnumerator(asRecord["GetEnumerator"].AsFunction, asRecord["Dispose"].AsList);
					}

					// Token: 0x04004B31 RID: 19249
					private readonly FunctionValue function;

					// Token: 0x02001530 RID: 5424
					private class FunctionEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600878D RID: 34701 RVA: 0x001CC54F File Offset: 0x001CA74F
						public FunctionEnumerator(FunctionValue function, ListValue disposees)
						{
							this.function = function;
							this.disposees = disposees;
						}

						// Token: 0x170023BB RID: 9147
						// (get) Token: 0x0600878E RID: 34702 RVA: 0x001CC565 File Offset: 0x001CA765
						public IValueReference Current
						{
							get
							{
								return this.current;
							}
						}

						// Token: 0x170023BC RID: 9148
						// (get) Token: 0x0600878F RID: 34703 RVA: 0x001CC56D File Offset: 0x001CA76D
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06008790 RID: 34704 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06008791 RID: 34705 RVA: 0x001CC578 File Offset: 0x001CA778
						public void Dispose()
						{
							if (this.disposees != null)
							{
								foreach (IValueReference valueReference in this.disposees)
								{
									((IDisposable)valueReference.Value).Dispose();
								}
								this.disposees = null;
							}
						}

						// Token: 0x06008792 RID: 34706 RVA: 0x001CC5DC File Offset: 0x001CA7DC
						public bool MoveNext()
						{
							Value value = this.function.Invoke();
							if (value.IsNull)
							{
								this.current = null;
								return false;
							}
							this.function = value["Next"].AsFunction;
							this.current = value.AsRecord.GetReference(value.AsRecord.IndexOf("Value"));
							return true;
						}

						// Token: 0x04004B32 RID: 19250
						private FunctionValue function;

						// Token: 0x04004B33 RID: 19251
						private IValueReference current;

						// Token: 0x04004B34 RID: 19252
						private ListValue disposees;
					}
				}
			}

			// Token: 0x02001531 RID: 5425
			private class FromListFunctionValue : NativeFunctionValue1<FunctionValue, ListValue>
			{
				// Token: 0x06008793 RID: 34707 RVA: 0x001CC63E File Offset: 0x001CA83E
				public FromListFunctionValue()
					: base(TypeValue.Function, "value", TypeValue.List)
				{
				}

				// Token: 0x06008794 RID: 34708 RVA: 0x001CC655 File Offset: 0x001CA855
				public override FunctionValue TypedInvoke(ListValue value)
				{
					return new EnumeratorFunctionValue(value.GetEnumerator());
				}
			}
		}

		// Token: 0x02001532 RID: 5426
		private static class _Error
		{
			// Token: 0x04004B35 RID: 19253
			public static readonly Value Record = new Library._Error.RecordFunctionValue();

			// Token: 0x02001533 RID: 5427
			private class RecordFunctionValue : NativeFunctionValue4<RecordValue, TextValue, Value, Value, Value>
			{
				// Token: 0x06008796 RID: 34710 RVA: 0x001CC670 File Offset: 0x001CA870
				public RecordFunctionValue()
					: base(TypeValue.Record, 1, "reason", TypeValue.Text, "message", NullableTypeValue.Text, "detail", TypeValue.Any, "parameters", NullableTypeValue.List)
				{
				}

				// Token: 0x06008797 RID: 34711 RVA: 0x001CC6B4 File Offset: 0x001CA8B4
				public override RecordValue TypedInvoke(TextValue reason, Value message, Value detail, Value parameters)
				{
					if (parameters.IsNull)
					{
						return ErrorRecord.New(reason, message.IsNull ? TextValue.Empty : message.AsText, detail, Value.Null, Value.Null);
					}
					return ErrorRecord.New(reason, message.IsNull ? TextValue.Empty : message.AsText, detail, parameters);
				}
			}
		}

		// Token: 0x02001534 RID: 5428
		public static class _Value
		{
			// Token: 0x04004B36 RID: 19254
			public new static readonly FunctionValue Equals = new Library._Value.EqualsFunctionValue();

			// Token: 0x04004B37 RID: 19255
			public static readonly FunctionValue NullableEquals = new Library._Value.NullableEqualsFunctionValue();

			// Token: 0x04004B38 RID: 19256
			public static readonly FunctionValue Compare = new Library._Value.CompareFunctionValue();

			// Token: 0x04004B39 RID: 19257
			public static readonly FunctionValue Type = new Library._Value.TypeFunctionValue();

			// Token: 0x04004B3A RID: 19258
			public static readonly FunctionValue ReplaceType = new Library._Value.ReplaceTypeFunctionValue();

			// Token: 0x04004B3B RID: 19259
			public static readonly FunctionValue Is = new Library._Value.IsFunctionValue();

			// Token: 0x04004B3C RID: 19260
			public static readonly FunctionValue As = new Library._Value.AsFunctionValue();

			// Token: 0x04004B3D RID: 19261
			public static readonly FunctionValue Add = new Library._Value.AddFunctionValue();

			// Token: 0x04004B3E RID: 19262
			public static readonly FunctionValue Subtract = new Library._Value.SubtractFunctionValue();

			// Token: 0x04004B3F RID: 19263
			public static readonly FunctionValue Multiply = new Library._Value.MultiplyFunctionValue();

			// Token: 0x04004B40 RID: 19264
			public static readonly FunctionValue Divide = new Library._Value.DivideFunctionValue();

			// Token: 0x04004B41 RID: 19265
			public static readonly FunctionValue ReplaceMetadata = new Library._Value.ReplaceMetadataFunctionValue();

			// Token: 0x04004B42 RID: 19266
			public static readonly FunctionValue RemoveMetadata = new Library._Value.RemoveMetadataFunctionValue();

			// Token: 0x04004B43 RID: 19267
			public static readonly FunctionValue Metadata = new Library._Value.MetadataFunctionValue();

			// Token: 0x04004B44 RID: 19268
			public static readonly FunctionValue NativeQuery = new Library._Value.NativeQueryFunctionValue();

			// Token: 0x04004B45 RID: 19269
			public static readonly FunctionValue Expression = new Library._Value.ExpressionFunctionValue(false);

			// Token: 0x04004B46 RID: 19270
			public static readonly FunctionValue OptimizedExpression = new Library._Value.ExpressionFunctionValue(true);

			// Token: 0x04004B47 RID: 19271
			public static readonly FunctionValue Optimize = new Library._Value.OptimizeFunctionValue();

			// Token: 0x04004B48 RID: 19272
			public static readonly FunctionValue Alternates = new Library._Value.AlternatesFunctionValue();

			// Token: 0x04004B49 RID: 19273
			public static readonly FunctionValue Versions = FoldableFunctionValue.New(new Library._Value.VersionsFunctionValue());

			// Token: 0x04004B4A RID: 19274
			public static readonly FunctionValue VersionIdentity = FoldableFunctionValue.New(new Library._Value.VersionIdentityFunctionValue());

			// Token: 0x04004B4B RID: 19275
			public static readonly FunctionValue ViewFunction = new Library._Value.ViewFunctionFunctionValue();

			// Token: 0x04004B4C RID: 19276
			public static readonly FunctionValue ViewError = new Library._Value.ViewErrorFunctionValue();

			// Token: 0x02001535 RID: 5429
			private class TypeFunctionValue : NativeFunctionValue1<TypeValue, Value>
			{
				// Token: 0x06008799 RID: 34713 RVA: 0x001CC80F File Offset: 0x001CAA0F
				public TypeFunctionValue()
					: base(TypeValue._Type, "value", TypeValue.Any)
				{
				}

				// Token: 0x0600879A RID: 34714 RVA: 0x001CC826 File Offset: 0x001CAA26
				public override TypeValue TypedInvoke(Value value)
				{
					return value.Type;
				}
			}

			// Token: 0x02001536 RID: 5430
			private class ReplaceTypeFunctionValue : NativeFunctionValue2<Value, Value, TypeValue>
			{
				// Token: 0x0600879B RID: 34715 RVA: 0x001CC82E File Offset: 0x001CAA2E
				public ReplaceTypeFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600879C RID: 34716 RVA: 0x001CC84F File Offset: 0x001CAA4F
				public override Value TypedInvoke(Value value, TypeValue type)
				{
					return value.ReplaceType(type);
				}
			}

			// Token: 0x02001537 RID: 5431
			private class IsFunctionValue : NativeFunctionValue2<LogicalValue, Value, TypeValue>
			{
				// Token: 0x0600879D RID: 34717 RVA: 0x001CC858 File Offset: 0x001CAA58
				public IsFunctionValue()
					: base(TypeValue.Logical, "value", TypeValue.Any, "type", TypeValue._Type)
				{
				}

				// Token: 0x0600879E RID: 34718 RVA: 0x001CC879 File Offset: 0x001CAA79
				public override LogicalValue TypedInvoke(Value value, TypeValue type)
				{
					return LogicalValue.New(value.Is(type));
				}
			}

			// Token: 0x02001538 RID: 5432
			private class AsFunctionValue : NativeFunctionValue2<Value, Value, TypeValue>
			{
				// Token: 0x0600879F RID: 34719 RVA: 0x001CC82E File Offset: 0x001CAA2E
				public AsFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any, "type", TypeValue._Type)
				{
				}

				// Token: 0x060087A0 RID: 34720 RVA: 0x001CC888 File Offset: 0x001CAA88
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					Value value;
					if (arguments[1].TryGetConstant(out value) && value.IsType)
					{
						return value.AsType;
					}
					return TypeValue.Any;
				}

				// Token: 0x060087A1 RID: 34721 RVA: 0x001CC8B9 File Offset: 0x001CAAB9
				public override Value TypedInvoke(Value value, TypeValue type)
				{
					return value.As(type);
				}
			}

			// Token: 0x02001539 RID: 5433
			public class FromTextFunctionValue : CultureSpecificFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060087A2 RID: 34722 RVA: 0x001CC8C4 File Offset: 0x001CAAC4
				public FromTextFunctionValue(IEngineHost host)
					: base(host, null, TypeValue.Any, 1, "text", TypeValue.Any, "culture", NullableTypeValue.Text)
				{
				}

				// Token: 0x060087A3 RID: 34723 RVA: 0x001CC8F4 File Offset: 0x001CAAF4
				public override Value TypedInvoke(Value text, Value culture)
				{
					TypeValue typeValue;
					return Library._Value.FromTextFunctionValue.GetValueAndType(text, base.GetCulture(culture).GetCultureInfo(), out typeValue);
				}

				// Token: 0x060087A4 RID: 34724 RVA: 0x001CC918 File Offset: 0x001CAB18
				public static Value GetValueAndType(Value text, CultureInfo cultureInfo, out TypeValue typeValue)
				{
					if (!text.IsText)
					{
						typeValue = TypeValue.Any;
						return text;
					}
					string text2 = text.AsString.Trim(Library.whitespaceChars);
					NumberValue numberValue;
					if (NumberValue.TryParse(text2, 0, text2.Length, NumberStyles.Any, cultureInfo, out numberValue, out typeValue))
					{
						return numberValue;
					}
					LogicalValue logicalValue;
					if (LogicalValue.TryParseFromText(text2, out logicalValue))
					{
						typeValue = TypeValue.Logical;
						return logicalValue;
					}
					if (text2.Length == 0 || string.CompareOrdinal(text2, "null") == 0)
					{
						typeValue = TypeValue.Null;
						return Value.Null;
					}
					DateValue dateValue;
					if (DateValue.TryParseFromText(text2, cultureInfo, out dateValue))
					{
						typeValue = TypeValue.Date;
						return dateValue;
					}
					TimeValue timeValue;
					if (TimeValue.TryParseFromText(text2, cultureInfo, out timeValue))
					{
						typeValue = TypeValue.Time;
						return timeValue;
					}
					DateTimeValue dateTimeValue;
					if (DateTimeValue.TryParseFromText(text2, cultureInfo, out dateTimeValue))
					{
						typeValue = TypeValue.DateTime;
						return dateTimeValue;
					}
					TimeSpan timeSpan;
					if (DurationValue.TryParse(text2, out timeSpan))
					{
						typeValue = TypeValue.Duration;
						return new DurationValue(timeSpan);
					}
					DateTimeZoneValue dateTimeZoneValue;
					if (DateTimeZoneValue.TryParseFromText(text2, cultureInfo, out dateTimeZoneValue))
					{
						typeValue = TypeValue.DateTimeZone;
						return dateTimeZoneValue;
					}
					typeValue = TypeValue.Any;
					return text;
				}
			}

			// Token: 0x0200153A RID: 5434
			private abstract class ArithmeticFunctionValue : NativeFunctionValue3<Value, Value, Value, Value>
			{
				// Token: 0x060087A5 RID: 34725 RVA: 0x001CCA0C File Offset: 0x001CAC0C
				public ArithmeticFunctionValue()
					: base(TypeValue.Any, 2, "value1", TypeValue.Any, "value2", TypeValue.Any, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}

				// Token: 0x060087A6 RID: 34726 RVA: 0x001CCA48 File Offset: 0x001CAC48
				public ArithmeticFunctionValue(TypeValue returnType, string firstParameter, TypeValue firstParameterType, string secondParameter, TypeValue secondParameterType)
					: base(returnType, 2, firstParameter, firstParameterType, secondParameter, secondParameterType, "precision", Library.PrecisionEnum.Type.Nullable)
				{
				}
			}

			// Token: 0x0200153B RID: 5435
			private class EqualsFunctionValue : Library._Value.ArithmeticFunctionValue
			{
				// Token: 0x060087A7 RID: 34727 RVA: 0x001CCA72 File Offset: 0x001CAC72
				public EqualsFunctionValue()
					: base(TypeValue.Logical, "value1", TypeValue.Any, "value2", TypeValue.Any)
				{
				}

				// Token: 0x060087A8 RID: 34728 RVA: 0x001CCA93 File Offset: 0x001CAC93
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					return LogicalValue.New((precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Equals(x, y));
				}
			}

			// Token: 0x0200153C RID: 5436
			private class NullableEqualsFunctionValue : Library._Value.ArithmeticFunctionValue, IInvocationRewriter
			{
				// Token: 0x060087A9 RID: 34729 RVA: 0x001CCAC0 File Offset: 0x001CACC0
				public NullableEqualsFunctionValue()
					: base(NullableTypeValue.Logical, "value1", TypeValue.Any, "value2", TypeValue.Any)
				{
				}

				// Token: 0x060087AA RID: 34730 RVA: 0x001CCAE1 File Offset: 0x001CACE1
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					if (x.IsNull || y.IsNull)
					{
						return Value.Null;
					}
					return Library._Value.Equals.Invoke(x, y, precisionEnum);
				}

				// Token: 0x060087AB RID: 34731 RVA: 0x001CCB08 File Offset: 0x001CAD08
				public bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
				{
					if (invocation.Arguments.Count == 2)
					{
						IExpression expression2 = invocation.Arguments[0];
						IExpression expression3 = invocation.Arguments[1];
						if (!environment.GetType(expression2).IsNullable && !environment.GetType(expression3).IsNullable)
						{
							expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, expression2, expression3, invocation.Range);
							return true;
						}
					}
					expression = null;
					return false;
				}
			}

			// Token: 0x0200153D RID: 5437
			private class CompareFunctionValue : Library._Value.ArithmeticFunctionValue
			{
				// Token: 0x060087AC RID: 34732 RVA: 0x001CCB6F File Offset: 0x001CAD6F
				public CompareFunctionValue()
					: base(TypeValue.Number, "value1", TypeValue.Any, "value2", TypeValue.Any)
				{
				}

				// Token: 0x060087AD RID: 34733 RVA: 0x001CCB90 File Offset: 0x001CAD90
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					return ValueHelper.ComparisonResultToValue((precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Compare(x, y));
				}
			}

			// Token: 0x0200153E RID: 5438
			private class AddFunctionValue : Library._Value.ArithmeticFunctionValue
			{
				// Token: 0x060087AE RID: 34734 RVA: 0x001CCBBD File Offset: 0x001CADBD
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return OperatorTypeflowModels.Binary(BinaryOperator2.Add, environment.GetType(arguments[0]), environment.GetType(arguments[1]));
				}

				// Token: 0x060087AF RID: 34735 RVA: 0x001CCBDF File Offset: 0x001CADDF
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					return (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Add(x, y);
				}
			}

			// Token: 0x0200153F RID: 5439
			private class SubtractFunctionValue : Library._Value.ArithmeticFunctionValue
			{
				// Token: 0x060087B1 RID: 34737 RVA: 0x001CCC0F File Offset: 0x001CAE0F
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return OperatorTypeflowModels.Binary(BinaryOperator2.Subtract, environment.GetType(arguments[0]), environment.GetType(arguments[1]));
				}

				// Token: 0x060087B2 RID: 34738 RVA: 0x001CCC31 File Offset: 0x001CAE31
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					return (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Subtract(x, y);
				}
			}

			// Token: 0x02001540 RID: 5440
			private class MultiplyFunctionValue : Library._Value.ArithmeticFunctionValue
			{
				// Token: 0x060087B4 RID: 34740 RVA: 0x001CCC59 File Offset: 0x001CAE59
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return OperatorTypeflowModels.Binary(BinaryOperator2.Multiply, environment.GetType(arguments[0]), environment.GetType(arguments[1]));
				}

				// Token: 0x060087B5 RID: 34741 RVA: 0x001CCC7B File Offset: 0x001CAE7B
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					return (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Multiply(x, y);
				}
			}

			// Token: 0x02001541 RID: 5441
			private class DivideFunctionValue : Library._Value.ArithmeticFunctionValue
			{
				// Token: 0x060087B7 RID: 34743 RVA: 0x001CCCA3 File Offset: 0x001CAEA3
				public override TypeValue GetTypeflowReturnType(IList<IExpression> arguments, ITypeflowEnvironment environment)
				{
					return OperatorTypeflowModels.Binary(BinaryOperator2.Divide, environment.GetType(arguments[0]), environment.GetType(arguments[1]));
				}

				// Token: 0x060087B8 RID: 34744 RVA: 0x001CCCC5 File Offset: 0x001CAEC5
				public override Value TypedInvoke(Value x, Value y, Value precisionEnum)
				{
					return (precisionEnum.IsNull ? Precision.Double : Library.PrecisionEnum.Type.GetValue(precisionEnum.AsNumber)).Divide(x, y);
				}
			}

			// Token: 0x02001542 RID: 5442
			private class ReplaceMetadataFunctionValue : NativeFunctionValue2
			{
				// Token: 0x060087BA RID: 34746 RVA: 0x001CCCED File Offset: 0x001CAEED
				public ReplaceMetadataFunctionValue()
					: base("value", "metaValue")
				{
				}

				// Token: 0x060087BB RID: 34747 RVA: 0x001CCCFF File Offset: 0x001CAEFF
				public override Value Invoke(Value value, Value metaValue)
				{
					return BinaryOperator.NewMeta.Invoke(value, metaValue);
				}
			}

			// Token: 0x02001543 RID: 5443
			private class RemoveMetadataFunctionValue : NativeFunctionValue2
			{
				// Token: 0x060087BC RID: 34748 RVA: 0x001CCD0D File Offset: 0x001CAF0D
				public RemoveMetadataFunctionValue()
					: base(1, "value", "metaValue")
				{
				}

				// Token: 0x060087BD RID: 34749 RVA: 0x001CCD20 File Offset: 0x001CAF20
				public override Value Invoke(Value value, Value metaValue)
				{
					if (metaValue != null && !metaValue.IsNull)
					{
						return value.NewMeta(Library.Record.RemoveFields.Invoke(value.MetaValue, metaValue.AsList, Library.MissingField.Ignore).AsRecord);
					}
					return value.SubtractMetaValue;
				}
			}

			// Token: 0x02001544 RID: 5444
			private class MetadataFunctionValue : NativeFunctionValue1
			{
				// Token: 0x060087BE RID: 34750 RVA: 0x0018D833 File Offset: 0x0018BA33
				public MetadataFunctionValue()
					: base("value")
				{
				}

				// Token: 0x060087BF RID: 34751 RVA: 0x001CCD5A File Offset: 0x001CAF5A
				public override Value Invoke(Value value)
				{
					return UnaryOperator.Meta.Invoke(value);
				}
			}

			// Token: 0x02001545 RID: 5445
			private sealed class NativeQueryFunctionValue : NativeFunctionValue4<Value, Value, TextValue, Value, Value>
			{
				// Token: 0x060087C0 RID: 34752 RVA: 0x001CCD68 File Offset: 0x001CAF68
				public NativeQueryFunctionValue()
					: base(TypeValue.Any, 2, "target", TypeValue.Any, "query", TypeValue.Text, "parameters", TypeValue.Any, "options", TypeValue.Record.Nullable)
				{
				}

				// Token: 0x060087C1 RID: 34753 RVA: 0x001CCDAE File Offset: 0x001CAFAE
				public override Value TypedInvoke(Value target, TextValue query, Value parameters, Value options)
				{
					return target.NativeQuery(query, parameters, options);
				}
			}

			// Token: 0x02001546 RID: 5446
			private sealed class ExpressionFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060087C2 RID: 34754 RVA: 0x001CCDBA File Offset: 0x001CAFBA
				public ExpressionFunctionValue(bool optimizedOnly)
					: base(TypeValue.Record.Nullable, "value", TypeValue.Any)
				{
					this.optimizedOnly = optimizedOnly;
				}

				// Token: 0x060087C3 RID: 34755 RVA: 0x001CCDE0 File Offset: 0x001CAFE0
				public override Value TypedInvoke(Value value)
				{
					Func<IExpression, Value> func = new Func<IExpression, Value>(ExpressionToSimplifiedMAstVisitor.ToMAst);
					if (this.optimizedOnly)
					{
						if (!(value is IOptimizedValue) && (!value.IsFunction || !value.AsFunction.Is<IOptimizedValue>()))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.ValueExpression_NotSupported, null, null);
						}
						func = new Func<IExpression, Value>(ExpressionToMAstVisitor.ToMAst);
					}
					IExpression expression = value.Expression;
					if (expression != null)
					{
						try
						{
							return func(expression);
						}
						catch (NotSupportedException)
						{
						}
					}
					return Value.Null;
				}

				// Token: 0x04004B4D RID: 19277
				private readonly bool optimizedOnly;
			}

			// Token: 0x02001547 RID: 5447
			public sealed class OptimizeFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060087C4 RID: 34756 RVA: 0x0007E42B File Offset: 0x0007C62B
				public OptimizeFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any)
				{
				}

				// Token: 0x060087C5 RID: 34757 RVA: 0x001CCE6C File Offset: 0x001CB06C
				public override Value TypedInvoke(Value value)
				{
					if (value is IOptimizedValue)
					{
						return value;
					}
					switch (value.Kind)
					{
					case ValueKind.Binary:
						return value.AsBinary.Optimize();
					case ValueKind.Table:
						return value.AsTable.Optimize();
					case ValueKind.Function:
						return value.AsFunction.Optimize();
					case ValueKind.Action:
						return value.AsAction.Optimize();
					}
					return new Library._Value.OptimizedValue(value);
				}
			}

			// Token: 0x02001548 RID: 5448
			private sealed class AlternatesFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060087C6 RID: 34758 RVA: 0x001CCEE7 File Offset: 0x001CB0E7
				public AlternatesFunctionValue()
					: base(TypeValue.Any, "alternates", TypeValue.List)
				{
				}

				// Token: 0x060087C7 RID: 34759 RVA: 0x0004F290 File Offset: 0x0004D490
				public override Value TypedInvoke(Value value)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
				}
			}

			// Token: 0x02001549 RID: 5449
			private sealed class OptimizedValue : DelegatingValue, IOptimizedValue
			{
				// Token: 0x060087C8 RID: 34760 RVA: 0x001CCEFE File Offset: 0x001CB0FE
				public OptimizedValue(Value value)
					: base(value)
				{
				}

				// Token: 0x170023BD RID: 9149
				// (get) Token: 0x060087C9 RID: 34761 RVA: 0x000D4846 File Offset: 0x000D2A46
				public override IExpression Expression
				{
					get
					{
						return new ConstantExpressionSyntaxNode(this);
					}
				}
			}

			// Token: 0x0200154A RID: 5450
			public class VersionsFunctionValue : NativeFunctionValue1<TableValue, Value>
			{
				// Token: 0x060087CA RID: 34762 RVA: 0x001CCF07 File Offset: 0x001CB107
				public VersionsFunctionValue()
					: base(Library._Value.VersionsFunctionValue.ResultTableType, "value", TypeValue.Any)
				{
				}

				// Token: 0x060087CB RID: 34763 RVA: 0x001CCF1E File Offset: 0x001CB11E
				public override TableValue TypedInvoke(Value value)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Value_Versions_NotSupported, value, null);
				}

				// Token: 0x04004B4E RID: 19278
				public const string VersionKey = "Version";

				// Token: 0x04004B4F RID: 19279
				public const string PublishedKey = "Published";

				// Token: 0x04004B50 RID: 19280
				public const string DataKey = "Data";

				// Token: 0x04004B51 RID: 19281
				public const string ModifiedKey = "Modified";

				// Token: 0x04004B52 RID: 19282
				public static readonly TableTypeValue ResultTableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(Keys.New("Version", "Published", "Data", "Modified"), new Value[]
				{
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Text.Nullable,
						LogicalValue.False
					}),
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.Logical,
						LogicalValue.False
					}),
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						NavigationTableServices.ConvertToLink(TypeValue.Any),
						LogicalValue.False
					}),
					RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						TypeValue.DateTime.Nullable,
						LogicalValue.True
					})
				}), true), new TableKey[]
				{
					new TableKey(new int[1], true)
				});
			}

			// Token: 0x0200154B RID: 5451
			private class VersionIdentityFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060087CD RID: 34765 RVA: 0x0007E42B File Offset: 0x0007C62B
				public VersionIdentityFunctionValue()
					: base(TypeValue.Any, "value", TypeValue.Any)
				{
				}

				// Token: 0x060087CE RID: 34766 RVA: 0x00019E42 File Offset: 0x00018042
				public override Value TypedInvoke(Value value)
				{
					return Value.Null;
				}
			}

			// Token: 0x0200154C RID: 5452
			private class ViewFunctionFunctionValue : NativeFunctionValue1<FunctionValue, FunctionValue>, IOpaqueFunctionValue, IFunctionValue, IValue
			{
				// Token: 0x060087CF RID: 34767 RVA: 0x001C7317 File Offset: 0x001C5517
				public ViewFunctionFunctionValue()
					: base(TypeValue.Function, "function", TypeValue.Function)
				{
				}

				// Token: 0x060087D0 RID: 34768 RVA: 0x001CD01D File Offset: 0x001CB21D
				public override FunctionValue TypedInvoke(FunctionValue function)
				{
					return FoldableFunctionValue.New(function);
				}
			}

			// Token: 0x0200154D RID: 5453
			private class ViewErrorFunctionValue : NativeFunctionValue1<RecordValue, RecordValue>, IOpaqueFunctionValue, IFunctionValue, IValue
			{
				// Token: 0x060087D1 RID: 34769 RVA: 0x001CD025 File Offset: 0x001CB225
				public ViewErrorFunctionValue()
					: base(TypeValue.Record, "errorRecord", TypeValue.Record)
				{
				}

				// Token: 0x060087D2 RID: 34770 RVA: 0x001CD03C File Offset: 0x001CB23C
				public override RecordValue TypedInvoke(RecordValue errorRecord)
				{
					return ViewExceptions.Mark(errorRecord);
				}
			}
		}

		// Token: 0x0200154E RID: 5454
		public static class Linker
		{
			// Token: 0x04004B53 RID: 19283
			public static readonly FunctionValue Bind = new Library.Linker.BindFunctionValue();

			// Token: 0x0200154F RID: 5455
			private class BindFunctionValue : NativeFunctionValue3<Value, RecordValue, Value, TextValue>
			{
				// Token: 0x060087D4 RID: 34772 RVA: 0x001CD050 File Offset: 0x001CB250
				public BindFunctionValue()
					: base(TypeValue.Any, "environment", TypeValue.Record, "section", NullableTypeValue.Text, "name", TypeValue.Text)
				{
				}

				// Token: 0x060087D5 RID: 34773 RVA: 0x001CD07C File Offset: 0x001CB27C
				public override Value TypedInvoke(RecordValue environment, Value section, TextValue name)
				{
					if (!section.IsNull)
					{
						return environment["Sections"][section][name];
					}
					Value value = environment["Shared"];
					int num;
					if (value.AsRecord.Keys.TryGetKeyIndex(name.String, out num))
					{
						return value[num];
					}
					throw ValueException.UnknownImport(name.String, Value.Null);
				}
			}
		}

		// Token: 0x02001550 RID: 5456
		private static class UICulture
		{
			// Token: 0x04004B54 RID: 19284
			public static readonly FunctionValue GetString = new Library.UICulture.GetStringFunctionValue();

			// Token: 0x02001551 RID: 5457
			private class GetStringFunctionValue : NativeFunctionValue2<TextValue, TextValue, Value>
			{
				// Token: 0x060087D7 RID: 34775 RVA: 0x00030D59 File Offset: 0x0002EF59
				public GetStringFunctionValue()
					: base(TypeValue.Text, 1, "name", TypeValue.Text, "args", NullableTypeValue.List)
				{
				}

				// Token: 0x060087D8 RID: 34776 RVA: 0x001CD0F4 File Offset: 0x001CB2F4
				public override TextValue TypedInvoke(TextValue name, Value args)
				{
					if (args.IsNull)
					{
						return TextValue.New(Strings.ResourceManager.GetString(name.String));
					}
					List<object> list = new List<object>();
					foreach (IValueReference valueReference in args.AsList)
					{
						list.Add(valueReference.Value);
					}
					return TextValue.New(string.Format(CultureInfo.CurrentCulture, Strings.ResourceManager.GetString(name.String), list.ToArray()));
				}
			}
		}

		// Token: 0x02001552 RID: 5458
		public static class Error
		{
			// Token: 0x04004B55 RID: 19285
			public static readonly FunctionValue ExpressionError = new Library.Error.ExpressionErrorFunctionValue();

			// Token: 0x04004B56 RID: 19286
			public static readonly FunctionValue DataFormatError = new Library.Error.DataFormatErrorFunctionValue();

			// Token: 0x02001553 RID: 5459
			private class ExpressionErrorFunctionValue : NativeFunctionValue2<RecordValue, TextValue, Value>
			{
				// Token: 0x060087DA RID: 34778 RVA: 0x001CD1A6 File Offset: 0x001CB3A6
				public ExpressionErrorFunctionValue()
					: base(TypeValue.Record, "message", TypeValue.Text, "detail", TypeValue.Any)
				{
				}

				// Token: 0x060087DB RID: 34779 RVA: 0x001CD1C7 File Offset: 0x001CB3C7
				public override RecordValue TypedInvoke(TextValue message, Value detail)
				{
					return ErrorRecord.New(ValueException.ExpressionError, message, detail);
				}
			}

			// Token: 0x02001554 RID: 5460
			private class DataFormatErrorFunctionValue : NativeFunctionValue2<RecordValue, TextValue, Value>
			{
				// Token: 0x060087DC RID: 34780 RVA: 0x001CD1A6 File Offset: 0x001CB3A6
				public DataFormatErrorFunctionValue()
					: base(TypeValue.Record, "message", TypeValue.Text, "detail", TypeValue.Any)
				{
				}

				// Token: 0x060087DD RID: 34781 RVA: 0x001CD1D5 File Offset: 0x001CB3D5
				public override RecordValue TypedInvoke(TextValue message, Value detail)
				{
					return ErrorRecord.New(ValueException.DataFormatError, message, detail);
				}
			}
		}

		// Token: 0x02001555 RID: 5461
		public static class FromCommon
		{
			// Token: 0x060087DE RID: 34782 RVA: 0x001CD1E4 File Offset: 0x001CB3E4
			public static global::System.DateTime NumberToDateTime(Value value, string errorString)
			{
				global::System.DateTime dateTime;
				try
				{
					dateTime = global::System.DateTime.FromOADate(value.AsNumber.ToDouble());
				}
				catch (ArgumentException ex)
				{
					throw ValueException.NewDataFormatError(errorString, value, ex);
				}
				return dateTime;
			}

			// Token: 0x04004B57 RID: 19287
			public static readonly global::System.DateTime BaseDate = global::System.DateTime.FromOADate(0.0);
		}
	}
}
