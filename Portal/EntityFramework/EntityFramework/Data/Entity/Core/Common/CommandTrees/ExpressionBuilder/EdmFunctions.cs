using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x020006F7 RID: 1783
	public static class EdmFunctions
	{
		// Token: 0x06005377 RID: 21367 RVA: 0x0012C65C File Offset: 0x0012A85C
		private static EdmFunction ResolveCanonicalFunction(string functionName, TypeUsage[] argumentTypes)
		{
			List<EdmFunction> list = new List<EdmFunction>(from func in EdmProviderManifest.Instance.GetStoreFunctions()
				where string.Equals(func.Name, functionName, StringComparison.Ordinal)
				select func);
			EdmFunction edmFunction = null;
			bool flag = false;
			if (list.Count > 0)
			{
				edmFunction = FunctionOverloadResolver.ResolveFunctionOverloads(list, argumentTypes, false, out flag);
				if (flag)
				{
					throw new ArgumentException(Strings.Cqt_Function_CanonicalFunction_AmbiguousMatch(functionName));
				}
			}
			if (edmFunction == null)
			{
				throw new ArgumentException(Strings.Cqt_Function_CanonicalFunction_NotFound(functionName));
			}
			return edmFunction;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0012C6D8 File Offset: 0x0012A8D8
		internal static DbFunctionExpression InvokeCanonicalFunction(string functionName, params DbExpression[] arguments)
		{
			TypeUsage[] array = new TypeUsage[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				array[i] = arguments[i].ResultType;
			}
			return EdmFunctions.ResolveCanonicalFunction(functionName, array).Invoke(arguments);
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0012C714 File Offset: 0x0012A914
		public static DbFunctionExpression Average(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Avg", new DbExpression[] { collection });
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0012C736 File Offset: 0x0012A936
		public static DbFunctionExpression Count(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Count", new DbExpression[] { collection });
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0012C758 File Offset: 0x0012A958
		public static DbFunctionExpression LongCount(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("BigCount", new DbExpression[] { collection });
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0012C77A File Offset: 0x0012A97A
		public static DbFunctionExpression Max(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Max", new DbExpression[] { collection });
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0012C79C File Offset: 0x0012A99C
		public static DbFunctionExpression Min(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Min", new DbExpression[] { collection });
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0012C7BE File Offset: 0x0012A9BE
		public static DbFunctionExpression Sum(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Sum", new DbExpression[] { collection });
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0012C7E0 File Offset: 0x0012A9E0
		public static DbFunctionExpression StDev(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("StDev", new DbExpression[] { collection });
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0012C802 File Offset: 0x0012AA02
		public static DbFunctionExpression StDevP(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("StDevP", new DbExpression[] { collection });
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0012C824 File Offset: 0x0012AA24
		public static DbFunctionExpression Var(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Var", new DbExpression[] { collection });
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0012C846 File Offset: 0x0012AA46
		public static DbFunctionExpression VarP(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("VarP", new DbExpression[] { collection });
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x0012C868 File Offset: 0x0012AA68
		public static DbFunctionExpression Concat(this DbExpression string1, DbExpression string2)
		{
			Check.NotNull<DbExpression>(string1, "string1");
			Check.NotNull<DbExpression>(string2, "string2");
			return EdmFunctions.InvokeCanonicalFunction("Concat", new DbExpression[] { string1, string2 });
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0012C89A File Offset: 0x0012AA9A
		public static DbExpression Contains(this DbExpression searchedString, DbExpression searchedForString)
		{
			Check.NotNull<DbExpression>(searchedString, "searchedString");
			Check.NotNull<DbExpression>(searchedForString, "searchedForString");
			return EdmFunctions.InvokeCanonicalFunction("Contains", new DbExpression[] { searchedString, searchedForString });
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0012C8CC File Offset: 0x0012AACC
		public static DbFunctionExpression EndsWith(this DbExpression stringArgument, DbExpression suffix)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(suffix, "suffix");
			return EdmFunctions.InvokeCanonicalFunction("EndsWith", new DbExpression[] { stringArgument, suffix });
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0012C8FE File Offset: 0x0012AAFE
		public static DbFunctionExpression IndexOf(this DbExpression searchString, DbExpression stringToFind)
		{
			Check.NotNull<DbExpression>(searchString, "searchString");
			Check.NotNull<DbExpression>(stringToFind, "stringToFind");
			return EdmFunctions.InvokeCanonicalFunction("IndexOf", new DbExpression[] { stringToFind, searchString });
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0012C930 File Offset: 0x0012AB30
		public static DbFunctionExpression Left(this DbExpression stringArgument, DbExpression length)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Left", new DbExpression[] { stringArgument, length });
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0012C962 File Offset: 0x0012AB62
		public static DbFunctionExpression Length(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Length", new DbExpression[] { stringArgument });
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0012C984 File Offset: 0x0012AB84
		public static DbFunctionExpression Replace(this DbExpression stringArgument, DbExpression toReplace, DbExpression replacement)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(toReplace, "toReplace");
			Check.NotNull<DbExpression>(replacement, "replacement");
			return EdmFunctions.InvokeCanonicalFunction("Replace", new DbExpression[] { stringArgument, toReplace, replacement });
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x0012C9D1 File Offset: 0x0012ABD1
		public static DbFunctionExpression Reverse(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Reverse", new DbExpression[] { stringArgument });
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0012C9F3 File Offset: 0x0012ABF3
		public static DbFunctionExpression Right(this DbExpression stringArgument, DbExpression length)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Right", new DbExpression[] { stringArgument, length });
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0012CA25 File Offset: 0x0012AC25
		public static DbFunctionExpression StartsWith(this DbExpression stringArgument, DbExpression prefix)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(prefix, "prefix");
			return EdmFunctions.InvokeCanonicalFunction("StartsWith", new DbExpression[] { stringArgument, prefix });
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0012CA58 File Offset: 0x0012AC58
		public static DbFunctionExpression Substring(this DbExpression stringArgument, DbExpression start, DbExpression length)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(start, "start");
			Check.NotNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Substring", new DbExpression[] { stringArgument, start, length });
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0012CAA5 File Offset: 0x0012ACA5
		public static DbFunctionExpression ToLower(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("ToLower", new DbExpression[] { stringArgument });
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0012CAC7 File Offset: 0x0012ACC7
		public static DbFunctionExpression ToUpper(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("ToUpper", new DbExpression[] { stringArgument });
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0012CAE9 File Offset: 0x0012ACE9
		public static DbFunctionExpression Trim(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Trim", new DbExpression[] { stringArgument });
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0012CB0B File Offset: 0x0012AD0B
		public static DbFunctionExpression TrimEnd(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("RTrim", new DbExpression[] { stringArgument });
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0012CB2D File Offset: 0x0012AD2D
		public static DbFunctionExpression TrimStart(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("LTrim", new DbExpression[] { stringArgument });
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0012CB4F File Offset: 0x0012AD4F
		public static DbFunctionExpression Year(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Year", new DbExpression[] { dateValue });
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0012CB71 File Offset: 0x0012AD71
		public static DbFunctionExpression Month(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Month", new DbExpression[] { dateValue });
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0012CB93 File Offset: 0x0012AD93
		public static DbFunctionExpression Day(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Day", new DbExpression[] { dateValue });
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0012CBB5 File Offset: 0x0012ADB5
		public static DbFunctionExpression DayOfYear(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("DayOfYear", new DbExpression[] { dateValue });
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0012CBD7 File Offset: 0x0012ADD7
		public static DbFunctionExpression Hour(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Hour", new DbExpression[] { timeValue });
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x0012CBF9 File Offset: 0x0012ADF9
		public static DbFunctionExpression Minute(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Minute", new DbExpression[] { timeValue });
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0012CC1B File Offset: 0x0012AE1B
		public static DbFunctionExpression Second(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Second", new DbExpression[] { timeValue });
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x0012CC3D File Offset: 0x0012AE3D
		public static DbFunctionExpression Millisecond(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Millisecond", new DbExpression[] { timeValue });
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0012CC5F File Offset: 0x0012AE5F
		public static DbFunctionExpression GetTotalOffsetMinutes(this DbExpression dateTimeOffsetArgument)
		{
			Check.NotNull<DbExpression>(dateTimeOffsetArgument, "dateTimeOffsetArgument");
			return EdmFunctions.InvokeCanonicalFunction("GetTotalOffsetMinutes", new DbExpression[] { dateTimeOffsetArgument });
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0012CC81 File Offset: 0x0012AE81
		public static DbFunctionExpression LocalDateTime(this DbExpression dateTimeOffsetArgument)
		{
			Check.NotNull<DbExpression>(dateTimeOffsetArgument, "dateTimeOffsetArgument");
			return EdmFunctions.InvokeCanonicalFunction("LocalDateTime", new DbExpression[] { dateTimeOffsetArgument });
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0012CCA3 File Offset: 0x0012AEA3
		public static DbFunctionExpression UtcDateTime(this DbExpression dateTimeOffsetArgument)
		{
			Check.NotNull<DbExpression>(dateTimeOffsetArgument, "dateTimeOffsetArgument");
			return EdmFunctions.InvokeCanonicalFunction("UtcDateTime", new DbExpression[] { dateTimeOffsetArgument });
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0012CCC5 File Offset: 0x0012AEC5
		public static DbFunctionExpression CurrentDateTime()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentDateTime", new DbExpression[0]);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0012CCD7 File Offset: 0x0012AED7
		public static DbFunctionExpression CurrentDateTimeOffset()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentDateTimeOffset", new DbExpression[0]);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0012CCE9 File Offset: 0x0012AEE9
		public static DbFunctionExpression CurrentUtcDateTime()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentUtcDateTime", new DbExpression[0]);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0012CCFB File Offset: 0x0012AEFB
		public static DbFunctionExpression TruncateTime(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("TruncateTime", new DbExpression[] { dateValue });
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x0012CD20 File Offset: 0x0012AF20
		public static DbFunctionExpression CreateDateTime(DbExpression year, DbExpression month, DbExpression day, DbExpression hour, DbExpression minute, DbExpression second)
		{
			Check.NotNull<DbExpression>(year, "year");
			Check.NotNull<DbExpression>(month, "month");
			Check.NotNull<DbExpression>(day, "day");
			Check.NotNull<DbExpression>(hour, "hour");
			Check.NotNull<DbExpression>(minute, "minute");
			Check.NotNull<DbExpression>(second, "second");
			return EdmFunctions.InvokeCanonicalFunction("CreateDateTime", new DbExpression[] { year, month, day, hour, minute, second });
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0012CDA4 File Offset: 0x0012AFA4
		public static DbFunctionExpression CreateDateTimeOffset(DbExpression year, DbExpression month, DbExpression day, DbExpression hour, DbExpression minute, DbExpression second, DbExpression timeZoneOffset)
		{
			Check.NotNull<DbExpression>(year, "year");
			Check.NotNull<DbExpression>(month, "month");
			Check.NotNull<DbExpression>(day, "day");
			Check.NotNull<DbExpression>(hour, "hour");
			Check.NotNull<DbExpression>(minute, "minute");
			Check.NotNull<DbExpression>(second, "second");
			Check.NotNull<DbExpression>(timeZoneOffset, "timeZoneOffset");
			return EdmFunctions.InvokeCanonicalFunction("CreateDateTimeOffset", new DbExpression[] { year, month, day, hour, minute, second, timeZoneOffset });
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0012CE38 File Offset: 0x0012B038
		public static DbFunctionExpression CreateTime(DbExpression hour, DbExpression minute, DbExpression second)
		{
			Check.NotNull<DbExpression>(hour, "hour");
			Check.NotNull<DbExpression>(minute, "minute");
			Check.NotNull<DbExpression>(second, "second");
			return EdmFunctions.InvokeCanonicalFunction("CreateTime", new DbExpression[] { hour, minute, second });
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0012CE85 File Offset: 0x0012B085
		public static DbFunctionExpression AddYears(this DbExpression dateValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddYears", new DbExpression[] { dateValue, addValue });
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x0012CEB7 File Offset: 0x0012B0B7
		public static DbFunctionExpression AddMonths(this DbExpression dateValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMonths", new DbExpression[] { dateValue, addValue });
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x0012CEE9 File Offset: 0x0012B0E9
		public static DbFunctionExpression AddDays(this DbExpression dateValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddDays", new DbExpression[] { dateValue, addValue });
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x0012CF1B File Offset: 0x0012B11B
		public static DbFunctionExpression AddHours(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddHours", new DbExpression[] { timeValue, addValue });
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x0012CF4D File Offset: 0x0012B14D
		public static DbFunctionExpression AddMinutes(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMinutes", new DbExpression[] { timeValue, addValue });
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x0012CF7F File Offset: 0x0012B17F
		public static DbFunctionExpression AddSeconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddSeconds", new DbExpression[] { timeValue, addValue });
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x0012CFB1 File Offset: 0x0012B1B1
		public static DbFunctionExpression AddMilliseconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMilliseconds", new DbExpression[] { timeValue, addValue });
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x0012CFE3 File Offset: 0x0012B1E3
		public static DbFunctionExpression AddMicroseconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMicroseconds", new DbExpression[] { timeValue, addValue });
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0012D015 File Offset: 0x0012B215
		public static DbFunctionExpression AddNanoseconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddNanoseconds", new DbExpression[] { timeValue, addValue });
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x0012D047 File Offset: 0x0012B247
		public static DbFunctionExpression DiffYears(this DbExpression dateValue1, DbExpression dateValue2)
		{
			Check.NotNull<DbExpression>(dateValue1, "dateValue1");
			Check.NotNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffYears", new DbExpression[] { dateValue1, dateValue2 });
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x0012D079 File Offset: 0x0012B279
		public static DbFunctionExpression DiffMonths(this DbExpression dateValue1, DbExpression dateValue2)
		{
			Check.NotNull<DbExpression>(dateValue1, "dateValue1");
			Check.NotNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMonths", new DbExpression[] { dateValue1, dateValue2 });
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x0012D0AB File Offset: 0x0012B2AB
		public static DbFunctionExpression DiffDays(this DbExpression dateValue1, DbExpression dateValue2)
		{
			Check.NotNull<DbExpression>(dateValue1, "dateValue1");
			Check.NotNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffDays", new DbExpression[] { dateValue1, dateValue2 });
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x0012D0DD File Offset: 0x0012B2DD
		public static DbFunctionExpression DiffHours(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffHours", new DbExpression[] { timeValue1, timeValue2 });
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x0012D10F File Offset: 0x0012B30F
		public static DbFunctionExpression DiffMinutes(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMinutes", new DbExpression[] { timeValue1, timeValue2 });
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0012D141 File Offset: 0x0012B341
		public static DbFunctionExpression DiffSeconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffSeconds", new DbExpression[] { timeValue1, timeValue2 });
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x0012D173 File Offset: 0x0012B373
		public static DbFunctionExpression DiffMilliseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMilliseconds", new DbExpression[] { timeValue1, timeValue2 });
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0012D1A5 File Offset: 0x0012B3A5
		public static DbFunctionExpression DiffMicroseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMicroseconds", new DbExpression[] { timeValue1, timeValue2 });
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0012D1D7 File Offset: 0x0012B3D7
		public static DbFunctionExpression DiffNanoseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffNanoseconds", new DbExpression[] { timeValue1, timeValue2 });
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0012D209 File Offset: 0x0012B409
		public static DbFunctionExpression Round(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Round", new DbExpression[] { value });
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x0012D22B File Offset: 0x0012B42B
		public static DbFunctionExpression Round(this DbExpression value, DbExpression digits)
		{
			Check.NotNull<DbExpression>(value, "value");
			Check.NotNull<DbExpression>(digits, "digits");
			return EdmFunctions.InvokeCanonicalFunction("Round", new DbExpression[] { value, digits });
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x0012D25D File Offset: 0x0012B45D
		public static DbFunctionExpression Floor(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Floor", new DbExpression[] { value });
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0012D27F File Offset: 0x0012B47F
		public static DbFunctionExpression Ceiling(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Ceiling", new DbExpression[] { value });
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0012D2A1 File Offset: 0x0012B4A1
		public static DbFunctionExpression Abs(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Abs", new DbExpression[] { value });
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0012D2C3 File Offset: 0x0012B4C3
		public static DbFunctionExpression Truncate(this DbExpression value, DbExpression digits)
		{
			Check.NotNull<DbExpression>(value, "value");
			Check.NotNull<DbExpression>(digits, "digits");
			return EdmFunctions.InvokeCanonicalFunction("Truncate", new DbExpression[] { value, digits });
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0012D2F5 File Offset: 0x0012B4F5
		public static DbFunctionExpression Power(this DbExpression baseArgument, DbExpression exponent)
		{
			Check.NotNull<DbExpression>(baseArgument, "baseArgument");
			Check.NotNull<DbExpression>(exponent, "exponent");
			return EdmFunctions.InvokeCanonicalFunction("Power", new DbExpression[] { baseArgument, exponent });
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x0012D327 File Offset: 0x0012B527
		public static DbFunctionExpression BitwiseAnd(this DbExpression value1, DbExpression value2)
		{
			Check.NotNull<DbExpression>(value1, "value1");
			Check.NotNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseAnd", new DbExpression[] { value1, value2 });
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0012D359 File Offset: 0x0012B559
		public static DbFunctionExpression BitwiseOr(this DbExpression value1, DbExpression value2)
		{
			Check.NotNull<DbExpression>(value1, "value1");
			Check.NotNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseOr", new DbExpression[] { value1, value2 });
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0012D38B File Offset: 0x0012B58B
		public static DbFunctionExpression BitwiseNot(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseNot", new DbExpression[] { value });
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0012D3AD File Offset: 0x0012B5AD
		public static DbFunctionExpression BitwiseXor(this DbExpression value1, DbExpression value2)
		{
			Check.NotNull<DbExpression>(value1, "value1");
			Check.NotNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseXor", new DbExpression[] { value1, value2 });
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0012D3DF File Offset: 0x0012B5DF
		public static DbFunctionExpression NewGuid()
		{
			return EdmFunctions.InvokeCanonicalFunction("NewGuid", new DbExpression[0]);
		}
	}
}
