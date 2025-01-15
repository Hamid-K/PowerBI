using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.QueryDesignModel.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000131 RID: 305
	internal static class DaxFunctions
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x0002D795 File Offset: 0x0002B995
		internal static DaxExpression And(DaxExpression left, DaxExpression right)
		{
			return DaxFunctions.InvokeScalar("AND", new DaxExpression[] { left, right });
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002D7AF File Offset: 0x0002B9AF
		internal static DaxExpression Blank()
		{
			return DaxFunctions.InvokeScalar("BLANK", Array.Empty<DaxExpression>());
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002D7C0 File Offset: 0x0002B9C0
		internal static DaxExpression Calculate(DaxExpression inputExpr, DaxExpression[] filters)
		{
			return DaxFunctions.InvokeScalar("CALCULATE", DaxFunctions.ToArray<DaxExpression>(inputExpr).Concat(filters).ToArray<DaxExpression>());
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002D7DD File Offset: 0x0002B9DD
		internal static DaxExpression Currency(DaxExpression argument)
		{
			return DaxFunctions.InvokeScalar("CURRENCY", new DaxExpression[] { argument });
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002D7F3 File Offset: 0x0002B9F3
		internal static DaxExpression Date(DaxExpression year, DaxExpression month, DaxExpression day)
		{
			return DaxFunctions.InvokeScalar("DATE", new DaxExpression[] { year, month, day });
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002D811 File Offset: 0x0002BA11
		internal static DaxExpression Year(DaxExpression year)
		{
			return DaxFunctions.InvokeScalar("YEAR", new DaxExpression[] { year });
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0002D827 File Offset: 0x0002BA27
		internal static DaxExpression Month(DaxExpression month)
		{
			return DaxFunctions.InvokeScalar("MONTH", new DaxExpression[] { month });
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002D83D File Offset: 0x0002BA3D
		internal static DaxExpression Day(DaxExpression day)
		{
			return DaxFunctions.InvokeScalar("DAY", new DaxExpression[] { day });
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002D853 File Offset: 0x0002BA53
		internal static DaxExpression Hour(DaxExpression hour)
		{
			return DaxFunctions.InvokeScalar("HOUR", new DaxExpression[] { hour });
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002D869 File Offset: 0x0002BA69
		internal static DaxExpression Minute(DaxExpression minute)
		{
			return DaxFunctions.InvokeScalar("MINUTE", new DaxExpression[] { minute });
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002D87F File Offset: 0x0002BA7F
		internal static DaxExpression Second(DaxExpression second)
		{
			return DaxFunctions.InvokeScalar("SECOND", new DaxExpression[] { second });
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0002D895 File Offset: 0x0002BA95
		internal static DaxExpression EDate(DaxExpression startDate, DaxExpression months)
		{
			return DaxFunctions.InvokeScalar("EDATE", new DaxExpression[] { startDate, months });
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002D8B0 File Offset: 0x0002BAB0
		internal static DaxExpression DateDiff(DaxExpression startDate, DaxExpression endDate, TimeUnit timeUnit)
		{
			string[] array = new string[]
			{
				startDate.Text,
				endDate.Text,
				timeUnit.ToString()
			};
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("DATEDIFF", false).Invoke(array));
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		internal static DaxExpression SampleAxisWithLocalMinMax(DaxExpression maxTargetPointCount, DaxExpression input, DaxColumnRef axis, IReadOnlyList<DaxColumnRef> measuresExpressions, DaxExpression minPointsResolution, IReadOnlyList<DaxColumnRef> seriesExpressions, DynamicSeriesSelectionCriteria dynamicSeriesSelectionCriteria, SortDirection dynamicSeriesSelectionCriteriaOrder, DaxExpression maxPointsResolution, DaxExpression maxDynamicSeriesCount)
		{
			List<string> list = new List<string>(8 + measuresExpressions.Count + ((seriesExpressions != null) ? seriesExpressions.Count : 1));
			list.Add(maxTargetPointCount.Text);
			list.Add(input.Text);
			list.Add(axis.ToString());
			foreach (DaxColumnRef daxColumnRef in measuresExpressions)
			{
				list.Add(daxColumnRef.ToString());
			}
			list.Add(minPointsResolution.Text);
			if (seriesExpressions != null)
			{
				using (IEnumerator<DaxColumnRef> enumerator = seriesExpressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DaxColumnRef daxColumnRef2 = enumerator.Current;
						list.Add(daxColumnRef2.ToString());
					}
					goto IL_00D3;
				}
			}
			list.Add(string.Empty);
			IL_00D3:
			list.Add(DaxFunctions.ToDaxAsStringDynamicSeriesSelectionCriteria(dynamicSeriesSelectionCriteria));
			list.Add(DaxFunctions.ToDaxAsStringDirection(dynamicSeriesSelectionCriteriaOrder));
			list.Add(maxPointsResolution.Text);
			list.Add(maxDynamicSeriesCount.Text);
			return DaxExpression.Table(new DaxFunctions.DaxFunction("SAMPLEAXISWITHLOCALMINMAX", false).Invoke(list), input.ResultColumns, false);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002DA4C File Offset: 0x0002BC4C
		internal static DaxExpression SampleCartesianPointsByCover(DaxExpression maxTargetPointCount, DaxExpression input, DaxExpression x, DaxExpression y, DaxExpression radius, DaxExpression maxMinRatio, DaxExpression maxBlankRatio)
		{
			List<string> list = new List<string>(4 + ((radius != null) ? 3 : 0));
			list.Add(maxTargetPointCount.Text);
			list.Add(input.Text);
			list.Add(x.ToString());
			list.Add(y.ToString());
			if (radius != null)
			{
				list.Add(radius.ToString());
				list.Add(maxMinRatio.ToString());
				list.Add(maxBlankRatio.ToString());
			}
			return DaxExpression.Table(new DaxFunctions.DaxFunction("SAMPLECARTESIANPOINTSBYCOVER", false).Invoke(list), input.ResultColumns, false);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0002DAE4 File Offset: 0x0002BCE4
		internal static DaxExpression TopNPerLevel(DaxExpression count, DaxExpression inputTable, DaxExpression levelsTable, DaxExpression pathTable, DaxExpression windowTable, DaxResultColumn restartIndicatorColumn)
		{
			DaxFunctions.DaxFunction daxFunction = new DaxFunctions.DaxFunction("TOPNPERLEVEL", false);
			List<string> list = new List<string>(5);
			list.Add(count.Text);
			list.Add(inputTable.Text);
			list.Add(levelsTable.Text);
			list.Add(pathTable.Text);
			list.Add(windowTable.Text);
			list.Add(DaxLiteral.FromString(restartIndicatorColumn.QueryFieldName).Text);
			List<DaxResultColumn> list2 = inputTable.ResultColumns.Concat(new DaxResultColumn[] { restartIndicatorColumn }).ToList<DaxResultColumn>();
			return DaxExpression.Table(daxFunction.Invoke(list), list2, false);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002DB86 File Offset: 0x0002BD86
		internal static DaxExpression If(DaxExpression condition, DaxExpression trueValue, DaxExpression falseValue)
		{
			return DaxFunctions.InvokeScalar("IF", new DaxExpression[] { condition, trueValue, falseValue });
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
		internal static DaxExpression If(DaxExpression condition, DaxExpression trueValue)
		{
			return DaxFunctions.InvokeScalar("IF", new DaxExpression[] { condition, trueValue });
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002DBBE File Offset: 0x0002BDBE
		internal static DaxExpression IfError(DaxExpression value, DaxExpression valueIfError)
		{
			return DaxFunctions.InvokeScalar("IFERROR", new DaxExpression[] { value, valueIfError });
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
		internal static DaxExpression Divide(DaxExpression numerator, DaxExpression denominator, DaxExpression alternateresult)
		{
			return DaxFunctions.InvokeScalar("DIVIDE", new DaxExpression[] { numerator, denominator, alternateresult });
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002DBF6 File Offset: 0x0002BDF6
		internal static DaxExpression IsBlank(DaxExpression argument)
		{
			return DaxFunctions.InvokeScalar("ISBLANK", new DaxExpression[] { argument });
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		internal static DaxExpression IsEmpty(DaxExpression argument)
		{
			return DaxFunctions.InvokeScalar("ISEMPTY", new DaxExpression[] { argument });
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0002DC24 File Offset: 0x0002BE24
		internal static DaxExpression IsOnOrAfter(IEnumerable<Tuple<DaxExpression, DaxExpression, SortDirection>> args)
		{
			Func<Tuple<DaxExpression, DaxExpression, SortDirection>, IEnumerable<string>> func;
			if ((func = DaxFunctions.<>O.<0>__FlattenIsOnOrAfterArgument) == null)
			{
				func = (DaxFunctions.<>O.<0>__FlattenIsOnOrAfterArgument = new Func<Tuple<DaxExpression, DaxExpression, SortDirection>, IEnumerable<string>>(DaxFunctions.FlattenIsOnOrAfterArgument));
			}
			IEnumerable<string> enumerable = args.SelectMany(func);
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("ISONORAFTER", false).Invoke(enumerable));
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		internal static DaxExpression IsAfter(IEnumerable<Tuple<DaxExpression, DaxExpression, SortDirection>> args)
		{
			Func<Tuple<DaxExpression, DaxExpression, SortDirection>, IEnumerable<string>> func;
			if ((func = DaxFunctions.<>O.<0>__FlattenIsOnOrAfterArgument) == null)
			{
				func = (DaxFunctions.<>O.<0>__FlattenIsOnOrAfterArgument = new Func<Tuple<DaxExpression, DaxExpression, SortDirection>, IEnumerable<string>>(DaxFunctions.FlattenIsOnOrAfterArgument));
			}
			IEnumerable<string> enumerable = args.SelectMany(func);
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("ISAFTER", false).Invoke(enumerable));
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0002DCB1 File Offset: 0x0002BEB1
		private static IEnumerable<string> FlattenIsOnOrAfterArgument(Tuple<DaxExpression, DaxExpression, SortDirection> arg)
		{
			yield return arg.Item1.Text;
			yield return arg.Item2.Text;
			yield return DaxFunctions.ToDaxAsStringDirection(arg.Item3);
			yield break;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0002DCC1 File Offset: 0x0002BEC1
		internal static DaxExpression Not(DaxExpression argument)
		{
			return DaxFunctions.InvokeScalar("NOT", new DaxExpression[] { argument });
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0002DCD7 File Offset: 0x0002BED7
		internal static DaxExpression Or(DaxExpression left, DaxExpression right)
		{
			return DaxFunctions.InvokeScalar("OR", new DaxExpression[] { left, right });
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0002DCF1 File Offset: 0x0002BEF1
		internal static DaxExpression Search(DaxExpression findText, DaxExpression withinText, DaxExpression startPosition, DaxExpression defaultValue)
		{
			return DaxFunctions.InvokeScalar("SEARCH", new DaxExpression[] { findText, withinText, startPosition, defaultValue });
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0002DD13 File Offset: 0x0002BF13
		internal static DaxExpression Switch(params DaxExpression[] args)
		{
			return DaxFunctions.InvokeScalar("SWITCH", args);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0002DD20 File Offset: 0x0002BF20
		internal static DaxExpression Time(DaxExpression hour, DaxExpression minute, DaxExpression second)
		{
			return DaxFunctions.InvokeScalar("TIME", new DaxExpression[] { hour, minute, second });
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0002DD3E File Offset: 0x0002BF3E
		internal static DaxExpression Earlier(params DaxExpression[] args)
		{
			return DaxFunctions.InvokeScalar("EARLIER", args);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0002DD4B File Offset: 0x0002BF4B
		internal static DaxExpression Concatenate(DaxExpression left, DaxExpression right)
		{
			return DaxFunctions.InvokeScalar("CONCATENATE", new DaxExpression[] { left, right });
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0002DD68 File Offset: 0x0002BF68
		internal static DaxExpression ConcatenateX(DaxExpression tableExpression, DaxExpression expression, DaxExpression delimiter = null, DaxSortItem? daxSortItem = null)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("CONCATENATEX", true);
			List<string> list = new List<string>();
			list.Add(tableExpression.Text);
			list.Add(expression.Text);
			if (delimiter != null)
			{
				list.Add(delimiter.Text);
			}
			if (daxSortItem != null)
			{
				if (delimiter == null)
				{
					list.Add(string.Empty);
				}
				DaxFunctions.AddSortArg(list, daxSortItem.Value, true);
			}
			return DaxExpression.Scalar(daxFunctionBase.Invoke(list));
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0002DDDD File Offset: 0x0002BFDD
		internal static DaxExpression Format(params DaxExpression[] args)
		{
			return DaxFunctions.InvokeScalar("FORMAT", args);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0002DDEA File Offset: 0x0002BFEA
		internal static DaxExpression Len(DaxExpression arg)
		{
			return DaxFunctions.InvokeScalar("LEN", new DaxExpression[] { arg });
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0002DE00 File Offset: 0x0002C000
		private static DaxExpression InvokeScalar(string functionName, params DaxExpression[] arguments)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction(functionName, false).Invoke(arguments));
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0002DE14 File Offset: 0x0002C014
		internal static DaxExpression CountRows(DaxExpression inputTable)
		{
			return DaxFunctions.InvokeScalar("COUNTROWS", new DaxExpression[] { inputTable });
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0002DE2A File Offset: 0x0002C02A
		internal static DaxExpression Sum(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("SUM", columnRef);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0002DE37 File Offset: 0x0002C037
		internal static DaxExpression SumX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("SUMX", inputTable, argument);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0002DE45 File Offset: 0x0002C045
		internal static DaxExpression Median(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("MEDIAN", columnRef);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0002DE52 File Offset: 0x0002C052
		internal static DaxExpression MedianX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("MEDIANX", inputTable, argument);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002DE60 File Offset: 0x0002C060
		internal static DaxExpression StdevP(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("STDEV.P", columnRef);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0002DE6D File Offset: 0x0002C06D
		internal static DaxExpression StdevPX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("STDEVX.P", inputTable, argument);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0002DE7B File Offset: 0x0002C07B
		internal static DaxExpression VarP(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("VAR.P", columnRef);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0002DE88 File Offset: 0x0002C088
		internal static DaxExpression VarPX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("VARX.P", inputTable, argument);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0002DE96 File Offset: 0x0002C096
		internal static DaxExpression CountA(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("COUNTA", columnRef);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0002DEA3 File Offset: 0x0002C0A3
		internal static DaxExpression CountAX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("COUNTAX", inputTable, argument);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002DEB1 File Offset: 0x0002C0B1
		internal static DaxExpression DistinctCount(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("DISTINCTCOUNT", columnRef);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002DEBE File Offset: 0x0002C0BE
		internal static DaxExpression Average(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("AVERAGE", columnRef);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002DECB File Offset: 0x0002C0CB
		internal static DaxExpression AverageX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("AVERAGEX", inputTable, argument);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002DED9 File Offset: 0x0002C0D9
		internal static DaxExpression Min(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("MIN", columnRef);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0002DEE6 File Offset: 0x0002C0E6
		internal static DaxExpression MinX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("MINX", inputTable, argument);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0002DEF4 File Offset: 0x0002C0F4
		internal static DaxExpression Max(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("MAX", columnRef);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0002DF01 File Offset: 0x0002C101
		internal static DaxExpression MaxX(DaxExpression inputTable, DaxExpression argument)
		{
			return DaxFunctions.InvokeTableAggregate("MAXX", inputTable, argument);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0002DF0F File Offset: 0x0002C10F
		internal static DaxExpression PercentileInc(DaxColumnRef columnRef, DaxExpression k)
		{
			return DaxFunctions.InvokeColumnAggregate("PERCENTILE.INC", columnRef, k);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0002DF1D File Offset: 0x0002C11D
		internal static DaxExpression PercentileXInc(DaxExpression inputTable, DaxExpression argument, DaxExpression k)
		{
			return DaxFunctions.InvokeTableAggregate("PERCENTILEX.INC", inputTable, argument, k);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0002DF2C File Offset: 0x0002C12C
		internal static DaxExpression PercentileExc(DaxColumnRef columnRef, DaxExpression k)
		{
			return DaxFunctions.InvokeColumnAggregate("PERCENTILE.EXC", columnRef, k);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0002DF3A File Offset: 0x0002C13A
		internal static DaxExpression PercentileXExc(DaxExpression inputTable, DaxExpression argument, DaxExpression k)
		{
			return DaxFunctions.InvokeTableAggregate("PERCENTILEX.EXC", inputTable, argument, k);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0002DF49 File Offset: 0x0002C149
		internal static DaxExpression IsSubtotal(DaxColumnRef columnRef)
		{
			return DaxFunctions.InvokeColumnAggregate("ISSUBTOTAL", columnRef);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0002DF56 File Offset: 0x0002C156
		private static DaxExpression InvokeColumnAggregate(string functionName, DaxColumnRef columnRef)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction(functionName, false).Invoke(new string[] { columnRef.ToString() }));
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0002DF7F File Offset: 0x0002C17F
		private static DaxExpression InvokeColumnAggregate(string functionName, DaxColumnRef columnRef, DaxExpression arg)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction(functionName, false).Invoke(new string[]
			{
				columnRef.ToString(),
				arg.ToString()
			}));
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0002DFB1 File Offset: 0x0002C1B1
		private static DaxExpression InvokeTableAggregate(string functionName, DaxExpression inputTable, DaxExpression argument)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction(functionName, false).Invoke(new DaxExpression[] { inputTable, argument }));
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0002DFD2 File Offset: 0x0002C1D2
		private static DaxExpression InvokeTableAggregate(string functionName, DaxExpression inputTable, DaxExpression argument, DaxExpression argument2)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction(functionName, false).Invoke(new DaxExpression[] { inputTable, argument, argument2 }));
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0002DFF7 File Offset: 0x0002C1F7
		internal static DaxExpression InvokeExtensionTableValuedFunction(string functionName, IEnumerable<DaxExpression> arguments, IReadOnlyList<DaxResultColumn> resultColumns)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction(functionName, false).Invoke(arguments), resultColumns, false);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0002E00D File Offset: 0x0002C20D
		internal static DaxExpression InvokeExtensionScalarValuedFunction(string functionName, IEnumerable<DaxExpression> arguments)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction(functionName, false).Invoke(arguments));
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0002E024 File Offset: 0x0002C224
		internal static DaxExpression AddColumns(DaxExpression inputTable, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("ADDCOLUMNS", true);
			IEnumerable<string> enumerable;
			IEnumerable<DaxResultColumn> enumerable2;
			DaxFunctions.BuildColumnArgs(newColumns, out enumerable, out enumerable2);
			IEnumerable<string> enumerable3 = DaxFunctions.ToArray<string>(inputTable.Text).Concat(enumerable);
			DaxResultColumn[] array = inputTable.ResultColumns.Concat(enumerable2).ToArray<DaxResultColumn>();
			return DaxExpression.Table(daxFunctionBase.Invoke(enumerable3), array, false);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0002E078 File Offset: 0x0002C278
		internal static DaxExpression AddMissingItems(IEnumerable<DaxExpression> showAllColumns, DaxExpression table, IEnumerable<DaxExpression> groups, IEnumerable<DaxExpression> contextTables)
		{
			string text = new DaxFunctions.DaxFunction("ADDMISSINGITEMS", false).Invoke(showAllColumns.Concat(new DaxExpression[] { table }).Concat(groups).Concat(contextTables));
			IReadOnlyList<DaxResultColumn> resultColumns = table.ResultColumns;
			return DaxExpression.Table(text, resultColumns, false);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0002E0BF File Offset: 0x0002C2BF
		internal static DaxExpression All(IReadOnlyList<DaxResultColumn> columns)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("ALL", false).Invoke(columns.Select((DaxResultColumn c) => c.DaxColumnRef.ToString())), columns, false);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0002E0FD File Offset: 0x0002C2FD
		internal static DaxExpression All(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("ALL", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0002E125 File Offset: 0x0002C325
		internal static DaxExpression AllSelected(IReadOnlyList<DaxResultColumn> columns)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("ALLSELECTED", false).Invoke(columns.Select((DaxResultColumn c) => c.DaxColumnRef.ToString())), columns, false);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0002E163 File Offset: 0x0002C363
		internal static DaxExpression AllSelected(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("ALLSELECTED", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0002E18B File Offset: 0x0002C38B
		internal static DaxExpression All(IReadOnlyList<DaxResultColumn> columns, bool isTableScan)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("ALL", false).Invoke(new string[] { string.Empty }), columns, isTableScan);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0002E1B2 File Offset: 0x0002C3B2
		internal static DaxExpression AllSelected(IReadOnlyList<DaxResultColumn> columns, bool isTableScan)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("ALLSELECTED", false).Invoke(new string[] { string.Empty }), columns, isTableScan);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0002E1DC File Offset: 0x0002C3DC
		internal static DaxExpression CalculateTable(DaxExpression inputTable, params DaxExpression[] filters)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("CALCULATETABLE", filters != null && filters.Length != 0);
			IEnumerable<DaxExpression> enumerable = DaxFunctions.ToArray<DaxExpression>(inputTable).Concat(filters);
			return DaxExpression.Table(daxFunctionBase.Invoke(enumerable), inputTable.ResultColumns, false);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0002E220 File Offset: 0x0002C420
		internal static DaxExpression CrossJoin(DaxExpression[] inputTables)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("CROSSJOIN", true);
			IReadOnlyList<DaxResultColumn> readOnlyList = DaxFunctions.MergeResultColumns(inputTables);
			return DaxExpression.Table(daxFunctionBase.Invoke(inputTables), readOnlyList, false);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0002E24C File Offset: 0x0002C44C
		internal static DaxExpression CurrentGroup(IReadOnlyList<DaxResultColumn> resultColumns)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("CURRENTGROUP", false).Invoke(Microsoft.Reporting.Util.EmptyArray<string>()), resultColumns, false);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0002E26A File Offset: 0x0002C46A
		internal static DaxExpression Filter(DaxExpression inputTable, DaxExpression predicate)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("FILTER", false).Invoke(new DaxExpression[] { inputTable, predicate }), inputTable.ResultColumns, false);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0002E296 File Offset: 0x0002C496
		internal static DaxExpression Generate(DaxExpression inputTable, DaxExpression applyTable)
		{
			return DaxFunctions.InvokeGenerate("GENERATE", inputTable, applyTable);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		internal static DaxExpression GenerateAll(DaxExpression inputTable, DaxExpression applyTable)
		{
			return DaxFunctions.InvokeGenerate("GENERATEALL", inputTable, applyTable);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0002E2B4 File Offset: 0x0002C4B4
		private static DaxExpression InvokeGenerate(string functionName, DaxExpression inputTable, DaxExpression applyTable)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction(functionName, true);
			DaxResultColumn[] array = inputTable.ResultColumns.Concat(applyTable.ResultColumns).ToArray<DaxResultColumn>();
			return DaxExpression.Table(daxFunctionBase.Invoke(new DaxExpression[] { inputTable, applyTable }), array, false);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0002E2FC File Offset: 0x0002C4FC
		internal static DaxExpression GroupBy(DaxExpression inputTable, IDaxGroupItem[] groupBy, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns)
		{
			DaxFunctions.DaxFunction daxFunction = new DaxFunctions.DaxFunction("GROUPBY", false);
			return DaxFunctions.BuildGroupByCore(inputTable, groupBy, newColumns, daxFunction);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0002E320 File Offset: 0x0002C520
		internal static DaxExpression FullOuterJoin(IEnumerable<DaxExpression> inputTables)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("FULLOUTERJOIN", true);
			IList<DaxResultColumn> list = (from rc in DaxFunctions.MergeResultColumns(inputTables)
				group rc by rc.QueryFieldName into @group
				select @group.First<DaxResultColumn>()).Evaluate<DaxResultColumn>();
			return DaxExpression.Table(daxFunctionBase.Invoke(inputTables), list.ToReadOnlyList<DaxResultColumn>(), false);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0002E39E File Offset: 0x0002C59E
		internal static DaxExpression NaturalInnerJoin(DaxExpression leftTable, DaxExpression rightTable)
		{
			return DaxFunctions.NaturalJoin(new DaxFunctions.DaxFunction("NATURALINNERJOIN", true), leftTable, rightTable);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0002E3B2 File Offset: 0x0002C5B2
		internal static DaxExpression NaturalLeftOuterJoin(DaxExpression leftTable, DaxExpression rightTable)
		{
			return DaxFunctions.NaturalJoin(new DaxFunctions.DaxFunction("NATURALLEFTOUTERJOIN", true), leftTable, rightTable);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		private static DaxExpression NaturalJoin(DaxFunctions.DaxFunction func, DaxExpression leftTable, DaxExpression rightTable)
		{
			IEnumerable<DaxResultColumn> enumerable = DaxFunctions.DetermineNaturalJoinResultColumns<DaxResultColumn>(leftTable.ResultColumns, rightTable.ResultColumns, (DaxResultColumn c) => c.QueryFieldName);
			return DaxExpression.Table(func.Invoke(new DaxExpression[] { leftTable, rightTable }), enumerable.EvaluateReadOnly<DaxResultColumn>(), false);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0002E426 File Offset: 0x0002C626
		internal static IEnumerable<TColumn> DetermineNaturalJoinResultColumns<TColumn>(IEnumerable<TColumn> leftColumns, IEnumerable<TColumn> rightColumns, Func<TColumn, string> getColumnName)
		{
			return leftColumns.Union(rightColumns, new ColumnByNameComparer<TColumn>(getColumnName));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0002E435 File Offset: 0x0002C635
		internal static IEnumerable<TColumn> DetermineImplicitJoinResultColumns<TColumn>(IEnumerable<TColumn> primaryColumns, IEnumerable<IEnumerable<TColumn>> secondaryColumns)
		{
			return primaryColumns.Concat(secondaryColumns.SelectMany((IEnumerable<TColumn> c) => c));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0002E464 File Offset: 0x0002C664
		internal static DaxExpression Union(DaxExpression[] inputTables)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("UNION", false);
			IReadOnlyList<DaxResultColumn> resultColumns = inputTables.First<DaxExpression>().ResultColumns;
			return DaxExpression.Table(daxFunctionBase.Invoke(inputTables), resultColumns, false);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0002E498 File Offset: 0x0002C698
		internal static DaxExpression UnionVariant(DaxExpression[] inputTables)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("UNIONVARIANT", false);
			IReadOnlyList<DaxResultColumn> resultColumns = inputTables.First<DaxExpression>().ResultColumns;
			return DaxExpression.Table(daxFunctionBase.Invoke(inputTables), resultColumns, false);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0002E4CC File Offset: 0x0002C6CC
		internal static DaxExpression SubstituteWithIndex(DaxExpression table, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression> indexColumn, DaxExpression indexTable, IEnumerable<DaxSortItem> indexTableSortOrder)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("SUBSTITUTEWITHINDEX", true);
			List<string> list = DaxFunctions.CreateSortArgs(indexTableSortOrder, true);
			List<string> list2 = new List<string>(list.Count + 3);
			list2.Add(table.Text);
			list2.Add(indexColumn.Item2.Text);
			list2.Add(indexTable.Text);
			list2.AddRange(list);
			IEnumerable<DaxResultColumn> enumerable = DaxFunctions.DetermineSubstituteWithIndexResultColumns<DaxResultColumn>(table.ResultColumns, indexColumn.Item1, indexTable.ResultColumns, (DaxResultColumn c) => c.QueryFieldName);
			return DaxExpression.Table(daxFunctionBase.Invoke(list2), enumerable.EvaluateReadOnly<DaxResultColumn>(), false);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0002E573 File Offset: 0x0002C773
		internal static IEnumerable<TColumn> DetermineSubstituteWithIndexResultColumns<TColumn>(IEnumerable<TColumn> tableColumns, TColumn indexColumn, IEnumerable<TColumn> indexTableColumns, Func<TColumn, string> getColumnName)
		{
			return tableColumns.Except(indexTableColumns, new ColumnByNameComparer<TColumn>(getColumnName)).Concat(new TColumn[] { indexColumn });
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0002E595 File Offset: 0x0002C795
		internal static DaxExpression Ignore(DaxExpression expression)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("IGNORE", false).Invoke(new DaxExpression[] { expression }));
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0002E5B6 File Offset: 0x0002C7B6
		internal static DaxExpression KeepFilters(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("KEEPFILTERS", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0002E5DE File Offset: 0x0002C7DE
		internal static DaxExpression NonVisual(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("NONVISUAL", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0002E608 File Offset: 0x0002C808
		internal static DaxExpression Row([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] columns)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("ROW", true);
			IEnumerable<string> enumerable;
			IEnumerable<DaxResultColumn> enumerable2;
			DaxFunctions.BuildColumnArgs(columns, out enumerable, out enumerable2);
			return DaxExpression.Table(daxFunctionBase.Invoke(enumerable), enumerable2.ToArray<DaxResultColumn>(), false);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0002E63C File Offset: 0x0002C83C
		internal static DaxExpression Related(DaxColumnRef column)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("RELATED", false).Invoke(new string[] { column.ToString() }));
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0002E669 File Offset: 0x0002C869
		internal static DaxExpression LookupValue(DaxExpression[] args)
		{
			return DaxFunctions.InvokeScalar("LOOKUPVALUE", args);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0002E676 File Offset: 0x0002C876
		internal static IDaxGroupItem Rollup(IDaxGroupItem[] groupItems, DaxExpression[] contextTables)
		{
			return new DaxFunctions.DaxRollupGroupItem(new DaxFunctions.DaxFunction("ROLLUP", false), groupItems, contextTables);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0002E68A File Offset: 0x0002C88A
		internal static IDaxGroupItem RollupAddIsSubtotal(IDaxGroupItem[] rollupGroupItems, DaxExpression[] contextTables)
		{
			return new DaxFunctions.DaxRollupGroupItem(new DaxFunctions.DaxFunction("ROLLUPADDISSUBTOTAL", false), rollupGroupItems, contextTables);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0002E69E File Offset: 0x0002C89E
		internal static IDaxGroupItem RollupIsSubtotal(IDaxGroupItem[] rollupGroupItems, DaxExpression[] contextTables)
		{
			return new DaxFunctions.DaxRollupGroupItem(new DaxFunctions.DaxFunction("ROLLUPISSUBTOTAL", false), rollupGroupItems, contextTables);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0002E6B2 File Offset: 0x0002C8B2
		internal static IDaxGroupItem NamedRollupGroup(IDaxGroupItem[] groupColumns, string name, DaxExpression[] contextTables)
		{
			if (groupColumns.Length == 1)
			{
				return new DaxFunctions.DaxNamedRollupGroupItem(groupColumns[0], name, contextTables);
			}
			return new DaxFunctions.DaxNamedRollupGroupItem(new DaxFunctions.DaxFunction("ROLLUPGROUP", false), groupColumns, name, contextTables);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0002E6D8 File Offset: 0x0002C8D8
		internal static IDaxGroupItem RollupGroup(DaxResultColumn[] groupColumns, DaxExpression[] contextTables)
		{
			return new DaxFunctions.DaxRollupGroupItem(new DaxFunctions.DaxFunction("ROLLUPGROUP", false), groupColumns.Cast<IDaxGroupItem>().ToArray<IDaxGroupItem>(), contextTables);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0002E6F6 File Offset: 0x0002C8F6
		internal static DaxExpression RollupGroup(DaxExpression[] args)
		{
			if (args.Length == 1)
			{
				return args[0];
			}
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("ROLLUPGROUP", false).Invoke(args));
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0002E718 File Offset: 0x0002C918
		internal static DaxExpression RollupIsSubtotal(DaxExpression[] args)
		{
			return DaxExpression.Scalar(new DaxFunctions.DaxFunction("ROLLUPISSUBTOTAL", false).Invoke(args));
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0002E730 File Offset: 0x0002C930
		internal static DaxExpression SelectColumns(DaxExpression inputTable, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] columns)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("SELECTCOLUMNS", true);
			IEnumerable<string> enumerable;
			IEnumerable<DaxResultColumn> enumerable2;
			DaxFunctions.BuildColumnArgs(columns, out enumerable, out enumerable2);
			IEnumerable<string> enumerable3 = DaxFunctions.ToArray<string>(inputTable.Text).Concat(enumerable);
			return DaxExpression.Table(daxFunctionBase.Invoke(enumerable3), enumerable2.EvaluateReadOnly<DaxResultColumn>(), false);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0002E778 File Offset: 0x0002C978
		internal static DaxExpression Summarize(DaxExpression inputTable, IDaxGroupItem[] groupBy, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns = null)
		{
			DaxFunctions.DaxFunction daxFunction = new DaxFunctions.DaxFunction("SUMMARIZE", false);
			return DaxFunctions.BuildGroupByCore(inputTable, groupBy, newColumns ?? Microsoft.Reporting.Util.EmptyArray<global::System.ValueTuple<DaxResultColumn, DaxExpression>>(), daxFunction);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		private static DaxExpression BuildGroupByCore(DaxExpression inputTable, IDaxGroupItem[] groupBy, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns, DaxFunctions.DaxFunction func)
		{
			IEnumerable<string> enumerable;
			IEnumerable<DaxResultColumn> enumerable2;
			DaxFunctions.BuildGroupByItems(groupBy, out enumerable, out enumerable2);
			IEnumerable<string> enumerable3;
			IEnumerable<DaxResultColumn> enumerable4;
			DaxFunctions.BuildColumnArgs(newColumns, out enumerable3, out enumerable4);
			IEnumerable<string> enumerable5 = DaxFunctions.ToArray<string>(inputTable.Text).Concat(enumerable).Concat(enumerable3);
			DaxResultColumn[] array = enumerable2.Concat(enumerable4).ToArray<DaxResultColumn>();
			return DaxExpression.Table(func.Invoke(enumerable5), array, false);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		private static void BuildGroupByItem(IDaxGroupItem groupItem, out string groupByArg, out IEnumerable<DaxResultColumn> resultColumns)
		{
			DaxExpression daxExpression = groupItem.ToExpression();
			groupByArg = daxExpression.ToString();
			resultColumns = DaxFunctions.MergeResultColumns(daxExpression);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0002E820 File Offset: 0x0002CA20
		private static void BuildGroupByItems(IDaxGroupItem[] groupItems, out IEnumerable<string> groupByArgs, out IEnumerable<DaxResultColumn> resultColumns)
		{
			DaxExpression[] array = groupItems.Select((IDaxGroupItem g) => g.ToExpression()).ToArray<DaxExpression>();
			groupByArgs = array.Select((DaxExpression g) => g.ToString());
			resultColumns = DaxFunctions.MergeResultColumns(array);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0002E888 File Offset: 0x0002CA88
		internal static DaxExpression SummarizeColumns(IDaxGroupItem[] groupBy, IEnumerable<DaxExpression> contextExpressions, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns)
		{
			DaxFunctionBase daxFunctionBase = new DaxFunctions.DaxFunction("SUMMARIZECOLUMNS", false);
			IEnumerable<string> enumerable;
			IEnumerable<DaxResultColumn> enumerable2;
			DaxFunctions.BuildGroupByItems(groupBy, out enumerable, out enumerable2);
			IEnumerable<string> enumerable3;
			IEnumerable<DaxResultColumn> enumerable4;
			DaxFunctions.BuildColumnArgs(newColumns, out enumerable3, out enumerable4);
			IEnumerable<string> enumerable5 = from e in contextExpressions.EmptyIfNull<DaxExpression>()
				select e.Text;
			IEnumerable<string> enumerable6 = enumerable.Concat(enumerable5).Concat(enumerable3);
			DaxResultColumn[] array = enumerable2.Concat(enumerable4).ToArray<DaxResultColumn>();
			return DaxExpression.Table(daxFunctionBase.Invoke(enumerable6), array, false);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0002E910 File Offset: 0x0002CB10
		internal static DaxExpression TreatAs(DaxExpression inputTable, IReadOnlyList<DaxExpression> columnExpressions, IReadOnlyList<DaxResultColumn> resultColumns)
		{
			DaxFunctions.DaxFunction daxFunction = new DaxFunctions.DaxFunction("TREATAS", false);
			List<string> list = new List<string>(columnExpressions.Count);
			foreach (DaxExpression daxExpression in columnExpressions)
			{
				list.Add(daxExpression.Text);
			}
			IEnumerable<string> enumerable = list.Prepend(inputTable.Text);
			return DaxExpression.Table(daxFunction.Invoke(enumerable), resultColumns, false);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0002E994 File Offset: 0x0002CB94
		internal static DaxExpression Distinct(DaxResultColumn column)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("DISTINCT", false).Invoke(new string[] { column.DaxColumnRef.ToString() }), column);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0002E9D5 File Offset: 0x0002CBD5
		internal static DaxExpression Distinct(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("DISTINCT", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0002EA00 File Offset: 0x0002CC00
		internal static DaxExpression Values(DaxResultColumn column)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("VALUES", false).Invoke(new string[] { column.DaxColumnRef.ToString() }), column);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0002EA41 File Offset: 0x0002CC41
		internal static DaxExpression Values(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxFunctions.DaxFunction("VALUES", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0002EA69 File Offset: 0x0002CC69
		internal static DaxExpression Sample(DaxExpression count, DaxExpression inputTable, ReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return DaxFunctions.InvokeLimit("SAMPLE", count, inputTable, sortOrder);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0002EA78 File Offset: 0x0002CC78
		internal static DaxExpression TopN(DaxExpression count, DaxExpression inputTable, ReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return DaxFunctions.InvokeLimit("TOPN", count, inputTable, sortOrder);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0002EA87 File Offset: 0x0002CC87
		internal static DaxExpression TopNSkip(DaxExpression count, DaxExpression skipCount, DaxExpression inputTable, ReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return DaxFunctions.InvokeLimitWithSkip("TOPNSKIP", count, skipCount, inputTable, sortOrder);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0002EA97 File Offset: 0x0002CC97
		private static DaxExpression InvokeLimit(string functionName, DaxExpression count, DaxExpression inputTable, ReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return DaxFunctions.InvokeLimitWithSkip(functionName, count, null, inputTable, sortOrder);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0002EAA4 File Offset: 0x0002CCA4
		private static DaxExpression InvokeLimitWithSkip(string functionName, DaxExpression count, DaxExpression skipCount, DaxExpression inputTable, ReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			DaxFunctions.DaxFunction daxFunction = new DaxFunctions.DaxFunction(functionName, false);
			List<string> daxSortArguments = DaxFunctions.GetDaxSortArguments(inputTable, sortOrder, false);
			IEnumerable<string> enumerable = Microsoft.Reporting.Util.AsEnumerable<string>(count.Text);
			if (skipCount != null)
			{
				enumerable = enumerable.Concat(new string[] { skipCount.Text });
			}
			enumerable = enumerable.Concat(new string[] { inputTable.Text }).Concat(daxSortArguments);
			return DaxExpression.Table(daxFunction.Invoke(enumerable), inputTable.ResultColumns, false);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0002EB18 File Offset: 0x0002CD18
		private static List<string> GetDaxSortArguments(DaxExpression inputDax, IList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder, bool useAscDesc)
		{
			List<string> list = new List<string>(sortOrder.Count);
			foreach (Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause querySortClause in sortOrder)
			{
				string text;
				string daxSortArguments = DaxFunctions.GetDaxSortArguments(inputDax, querySortClause, useAscDesc, out text);
				list.Add(daxSortArguments);
				list.Add(text);
			}
			return list;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0002EB80 File Offset: 0x0002CD80
		internal static string GetDaxSortArguments(DaxExpression inputDax, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause sortOrderItem, bool useAscDesc, out string direction)
		{
			string text = null;
			QueryFieldExpression queryFieldExpression = sortOrderItem.Expression as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				text = inputDax.GetResultColumnReference(queryFieldExpression.Column).ToString();
			}
			else
			{
				QueryRelatedColumnExpression queryRelatedColumnExpression = sortOrderItem.Expression as QueryRelatedColumnExpression;
				if (queryRelatedColumnExpression != null)
				{
					if (queryRelatedColumnExpression.Column != null)
					{
						IConceptualColumn column = queryRelatedColumnExpression.Column;
						text = DaxFunctions.Related(DaxRef.Column(column.ConceptualTypeColumn, null, column.Entity)).ToString();
					}
					else
					{
						EdmFieldInstance field = queryRelatedColumnExpression.Field;
						text = DaxFunctions.Related(DaxRef.Column(field.Field.Column, field.Entity, null)).ToString();
					}
				}
			}
			if (useAscDesc)
			{
				direction = DaxFunctions.ToDaxAsStringDirection(sortOrderItem.Direction);
			}
			else
			{
				direction = DaxFunctions.ToDaxAsNumericDirection(sortOrderItem.Direction);
			}
			return text;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0002EC48 File Offset: 0x0002CE48
		private static List<string> CreateSortArgs(IEnumerable<DaxSortItem> sortOrder, bool useAscDesc)
		{
			List<string> list = new List<string>();
			foreach (DaxSortItem daxSortItem in sortOrder)
			{
				DaxFunctions.AddSortArg(list, daxSortItem, useAscDesc);
			}
			return list;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0002EC98 File Offset: 0x0002CE98
		private static void AddSortArg(IList<string> sortArgs, DaxSortItem sortOrderItem, bool useAscDesc)
		{
			sortArgs.Add(sortOrderItem.Column.ToString());
			string text;
			if (useAscDesc)
			{
				text = DaxFunctions.ToDaxAsStringDirection(sortOrderItem.Direction);
			}
			else
			{
				text = DaxFunctions.ToDaxAsNumericDirection(sortOrderItem.Direction);
			}
			sortArgs.Add(text);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0002ECE6 File Offset: 0x0002CEE6
		internal static string ToDaxAsNumericDirection(SortDirection direction)
		{
			if (direction != SortDirection.Descending)
			{
				return "1";
			}
			return "0";
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0002ECF7 File Offset: 0x0002CEF7
		internal static string ToDaxAsStringDirection(SortDirection direction)
		{
			if (direction != SortDirection.Descending)
			{
				return "ASC";
			}
			return "DESC";
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0002ED08 File Offset: 0x0002CF08
		private static string ToDaxAsStringDynamicSeriesSelectionCriteria(DynamicSeriesSelectionCriteria selectionCriteria)
		{
			if (selectionCriteria == DynamicSeriesSelectionCriteria.Alphabetical)
			{
				return "ALPHABETICAL";
			}
			if (selectionCriteria != DynamicSeriesSelectionCriteria.Random)
			{
				throw new InvalidOperationException(Microsoft.Reporting.StringUtil.FormatInvariant("Unknown DynamicSeriesSelectionCriteria: {0}", new object[] { selectionCriteria }));
			}
			return "RANDOM";
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0002ED3D File Offset: 0x0002CF3D
		internal static DaxExpression Ceiling(DaxExpression number, DaxExpression significance)
		{
			return DaxFunctions.InvokeScalar("CEILING", new DaxExpression[] { number, significance });
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0002ED57 File Offset: 0x0002CF57
		internal static DaxExpression Floor(DaxExpression number, DaxExpression significance)
		{
			return DaxFunctions.InvokeScalar("FLOOR", new DaxExpression[] { number, significance });
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0002ED71 File Offset: 0x0002CF71
		internal static DaxExpression Sqrt(DaxExpression number)
		{
			return DaxFunctions.InvokeScalar("SQRT", new DaxExpression[] { number });
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0002ED87 File Offset: 0x0002CF87
		internal static DaxExpression Min(DaxExpression value1, DaxExpression value2)
		{
			return DaxFunctions.InvokeScalar("MIN", new DaxExpression[] { value1, value2 });
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002EDA1 File Offset: 0x0002CFA1
		internal static DaxExpression Max(DaxExpression value1, DaxExpression value2)
		{
			return DaxFunctions.InvokeScalar("MAX", new DaxExpression[] { value1, value2 });
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0002EDBB File Offset: 0x0002CFBB
		internal static DaxExpression Int(DaxExpression column)
		{
			return DaxFunctions.InvokeScalar("INT", new DaxExpression[] { column });
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0002EDD1 File Offset: 0x0002CFD1
		internal static DaxExpression RoundDown(DaxExpression column, DaxExpression digit)
		{
			return DaxFunctions.InvokeScalar("ROUNDDOWN", new DaxExpression[] { column, digit });
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0002EDEB File Offset: 0x0002CFEB
		internal static DaxExpression RoundUp(DaxExpression column, DaxExpression digit)
		{
			return DaxFunctions.InvokeScalar("ROUNDUP", new DaxExpression[] { column, digit });
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0002EE05 File Offset: 0x0002D005
		internal static DaxExpression Log10(DaxExpression number)
		{
			return DaxFunctions.InvokeScalar("Log10", new DaxExpression[] { number });
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0002EE1B File Offset: 0x0002D01B
		internal static DaxExpression Power(DaxExpression number, DaxExpression power)
		{
			return DaxFunctions.InvokeScalar("Power", new DaxExpression[] { number, power });
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0002EE38 File Offset: 0x0002D038
		private static void BuildColumnArgs([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] IReadOnlyList<global::System.ValueTuple<DaxResultColumn, DaxExpression>> columns, out IEnumerable<string> columnArgs, out IEnumerable<DaxResultColumn> resultColumns)
		{
			columnArgs = columns.Select(delegate([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression> c)
			{
				DaxExpression daxExpression = DaxLiteral.FromString(c.Item1.ToIntermediateResultColumnName());
				return ((daxExpression != null) ? daxExpression.ToString() : null) + ", " + c.Item2.Text;
			});
			resultColumns = columns.Select(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression> c) => c.Item1);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0002EE93 File Offset: 0x0002D093
		private static T[] ToArray<T>(T item)
		{
			return new T[] { item };
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0002EEA3 File Offset: 0x0002D0A3
		private static IList<DaxResultColumn> MergeResultColumns(DaxExpression queryExpression)
		{
			return queryExpression.ResultColumns.Evaluate<DaxResultColumn>();
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0002EEB0 File Offset: 0x0002D0B0
		private static IReadOnlyList<DaxResultColumn> MergeResultColumns(IEnumerable<DaxExpression> queryExpressions)
		{
			return queryExpressions.SelectMany((DaxExpression t) => t.ResultColumns).EvaluateReadOnly<DaxResultColumn>();
		}

		// Token: 0x02000372 RID: 882
		private sealed class DaxFunction : DaxFunctionBase
		{
			// Token: 0x06001F69 RID: 8041 RVA: 0x00056754 File Offset: 0x00054954
			internal DaxFunction(string functionName, bool alwaysMultiline = false)
				: base(functionName, alwaysMultiline)
			{
			}

			// Token: 0x06001F6A RID: 8042 RVA: 0x00056760 File Offset: 0x00054960
			protected override string InvokeCore(string formattedArgs, bool multiline)
			{
				if (multiline)
				{
					return string.Concat(new object[]
					{
						base.Name,
						"(",
						DaxFormat.NewLine,
						'\t',
						formattedArgs,
						DaxFormat.NewLine,
						")"
					});
				}
				return base.Name + "(" + formattedArgs + ")";
			}
		}

		// Token: 0x02000373 RID: 883
		private sealed class DaxRollupGroupItem : IDaxGroupItem
		{
			// Token: 0x06001F6B RID: 8043 RVA: 0x000567CC File Offset: 0x000549CC
			internal DaxRollupGroupItem(DaxFunctions.DaxFunction function, IDaxGroupItem[] groupItems, DaxExpression[] contextTables)
			{
				IEnumerable<string> enumerable;
				IEnumerable<DaxResultColumn> enumerable2;
				DaxFunctions.BuildGroupByItems(groupItems, out enumerable, out enumerable2);
				IEnumerable<string> enumerable3 = enumerable;
				if (!contextTables.IsNullOrEmpty<DaxExpression>())
				{
					enumerable3 = contextTables.Select((DaxExpression ct) => ct.ToString()).Concat(enumerable);
				}
				this._expr = DaxExpression.Table(function.Invoke(enumerable3), enumerable2.ToArray<DaxResultColumn>(), false);
			}

			// Token: 0x06001F6C RID: 8044 RVA: 0x00056838 File Offset: 0x00054A38
			public DaxExpression ToExpression()
			{
				return this._expr;
			}

			// Token: 0x0400126F RID: 4719
			private readonly DaxExpression _expr;
		}

		// Token: 0x02000374 RID: 884
		private sealed class DaxNamedRollupGroupItem : IDaxGroupItem
		{
			// Token: 0x06001F6D RID: 8045 RVA: 0x00056840 File Offset: 0x00054A40
			internal DaxNamedRollupGroupItem(IDaxGroupItem groupItem, string name, IReadOnlyList<DaxExpression> contextTables)
			{
				string text;
				IEnumerable<DaxResultColumn> enumerable;
				DaxFunctions.BuildGroupByItem(groupItem, out text, out enumerable);
				DaxExpression daxExpression = DaxExpression.Table(text, enumerable.ToArray<DaxResultColumn>(), false);
				DaxResultColumn daxResultColumn = new DaxResultColumn(name, DaxRef.Column(DaxUniqueNameGenerator.MakeUniqueColumnName(name, daxExpression.ResultColumns)));
				string finalText = DaxFunctions.DaxNamedRollupGroupItem.GetFinalText(daxExpression.Text, DaxLiteral.FromString(name).Text, contextTables);
				this._expr = DaxExpression.Table(finalText, daxExpression.ResultColumns.Concat(new DaxResultColumn[] { daxResultColumn }).ToArray<DaxResultColumn>(), false);
			}

			// Token: 0x06001F6E RID: 8046 RVA: 0x000568CC File Offset: 0x00054ACC
			internal DaxNamedRollupGroupItem(DaxFunctions.DaxFunction function, IDaxGroupItem[] groupItems, string name, IReadOnlyList<DaxExpression> contextTables)
			{
				IEnumerable<string> enumerable;
				IEnumerable<DaxResultColumn> enumerable2;
				DaxFunctions.BuildGroupByItems(groupItems, out enumerable, out enumerable2);
				DaxExpression daxExpression = DaxExpression.Table(function.Invoke(enumerable), enumerable2.ToArray<DaxResultColumn>(), false);
				DaxResultColumn daxResultColumn = new DaxResultColumn(name, DaxRef.Column(DaxUniqueNameGenerator.MakeUniqueColumnName(name, daxExpression.ResultColumns)));
				string finalText = DaxFunctions.DaxNamedRollupGroupItem.GetFinalText(daxExpression.Text, DaxLiteral.FromString(name).Text, contextTables);
				this._expr = DaxExpression.Table(finalText, daxExpression.ResultColumns.Concat(new DaxResultColumn[] { daxResultColumn }).ToArray<DaxResultColumn>(), false);
			}

			// Token: 0x06001F6F RID: 8047 RVA: 0x0005695C File Offset: 0x00054B5C
			private static string GetFinalText(string rollupText, string subtotalText, IReadOnlyList<DaxExpression> contextTables)
			{
				if (contextTables == null)
				{
					return rollupText + ", " + subtotalText;
				}
				List<string> list = new List<string>(2 + contextTables.Count) { rollupText, subtotalText };
				if (contextTables != null)
				{
					for (int i = 0; i < contextTables.Count; i++)
					{
						list.Add(contextTables[i].ToString());
					}
				}
				bool flag = false;
				return DaxFunctionBase.FormatArguments(list.ToArray(), ref flag, true, true);
			}

			// Token: 0x06001F70 RID: 8048 RVA: 0x000569CC File Offset: 0x00054BCC
			public DaxExpression ToExpression()
			{
				return this._expr;
			}

			// Token: 0x04001270 RID: 4720
			private readonly DaxExpression _expr;
		}

		// Token: 0x02000375 RID: 885
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001271 RID: 4721
			public static Func<Tuple<DaxExpression, DaxExpression, SortDirection>, IEnumerable<string>> <0>__FlattenIsOnOrAfterArgument;
		}
	}
}
