using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000CD RID: 205
	internal static class ExpressionNodeBuilder
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0000B019 File Offset: 0x00009219
		public static EntitySetExpressionNode EntitySet(string container, string entitySet, IConceptualEntity entity = null)
		{
			return new EntitySetExpressionNode(container, entitySet, entity);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000B023 File Offset: 0x00009223
		public static PropertyExpressionNode ModelProperty(string container, string entitySet, string propertyName, IConceptualProperty property = null)
		{
			return new PropertyExpressionNode(ExpressionNodeBuilder.EntitySet(container, entitySet, (property != null) ? property.Entity : null), propertyName, property);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000B03F File Offset: 0x0000923F
		public static PropertyExpressionNode ModelProperty(EntitySetExpressionNode entitySet, string propertyName, IConceptualProperty property = null)
		{
			return new PropertyExpressionNode(entitySet, propertyName, property);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000B049 File Offset: 0x00009249
		public static LiteralExpressionNode Literal(object value)
		{
			return new LiteralExpressionNode((value == null) ? ScalarValue.Null : new ScalarValue(value));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000B060 File Offset: 0x00009260
		public static FunctionCallExpressionNode LimitProperty(this ExpressionNode value, string propertyName, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			if (propertyName == null)
			{
				return ExpressionNodeBuilder.Function("LimitProperty", functionUsageKind, new ExpressionNode[] { value });
			}
			return ExpressionNodeBuilder.Function("LimitProperty", functionUsageKind, new ExpressionNode[]
			{
				value,
				ExpressionNodeBuilder.Literal(propertyName)
			});
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000B099 File Offset: 0x00009299
		public static FunctionCallExpressionNode Max(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Max(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000B0A2 File Offset: 0x000092A2
		public static FunctionCallExpressionNode Max(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Max(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public static FunctionCallExpressionNode Max(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Max", usageKind, args);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000B0C2 File Offset: 0x000092C2
		public static FunctionCallExpressionNode Min(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Min(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000B0CB File Offset: 0x000092CB
		public static FunctionCallExpressionNode Min(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Min(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000B0DD File Offset: 0x000092DD
		public static FunctionCallExpressionNode Min(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Min", usageKind, args);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000B0EB File Offset: 0x000092EB
		public static FunctionCallExpressionNode SingleValue(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.SingleValue(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000B0FD File Offset: 0x000092FD
		public static FunctionCallExpressionNode SingleValue(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("SingleValue", usageKind, args);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000B10B File Offset: 0x0000930B
		public static FunctionCallExpressionNode SynchronizationIndex(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.SynchronizationIndex(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000B11D File Offset: 0x0000931D
		public static FunctionCallExpressionNode SynchronizationIndex(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("SynchronizationIndex", usageKind, args);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000B12B File Offset: 0x0000932B
		public static FunctionCallExpressionNode Sum(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Sum(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000B134 File Offset: 0x00009334
		public static FunctionCallExpressionNode Sum(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Sum(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000B146 File Offset: 0x00009346
		public static FunctionCallExpressionNode Sum(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Sum", usageKind, args);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000B154 File Offset: 0x00009354
		public static FunctionCallExpressionNode PercentileInc(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.PercentileInc(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000B15D File Offset: 0x0000935D
		public static FunctionCallExpressionNode PercentileInc(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("PercentileInc", usageKind, args);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000B16B File Offset: 0x0000936B
		public static FunctionCallExpressionNode PercentileInc(this ExpressionNode arg, double k, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.PercentileInc(functionUsageKind, new ExpressionNode[]
			{
				arg,
				ExpressionNodeBuilder.Literal(k)
			});
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000B18B File Offset: 0x0000938B
		public static FunctionCallExpressionNode PercentileExc(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.PercentileExc(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000B194 File Offset: 0x00009394
		public static FunctionCallExpressionNode PercentileExc(this ExpressionNode arg, double k, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.PercentileExc(functionUsageKind, new ExpressionNode[]
			{
				arg,
				ExpressionNodeBuilder.Literal(k)
			});
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000B1B4 File Offset: 0x000093B4
		public static FunctionCallExpressionNode PercentileExc(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("PercentileExc", usageKind, args);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000B1C2 File Offset: 0x000093C2
		public static FunctionCallExpressionNode Median(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Median(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000B1CB File Offset: 0x000093CB
		public static FunctionCallExpressionNode Median(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Median(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000B1DD File Offset: 0x000093DD
		public static FunctionCallExpressionNode Median(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Median", usageKind, args);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000B1EB File Offset: 0x000093EB
		public static FunctionCallExpressionNode StandardDeviation(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.StandardDeviation(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public static FunctionCallExpressionNode StandardDeviation(this ExpressionNode arg)
		{
			return ExpressionNodeBuilder.StandardDeviation(FunctionUsageKind.Unassigned, new ExpressionNode[] { arg });
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000B206 File Offset: 0x00009406
		public static FunctionCallExpressionNode StandardDeviation(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("StandardDeviation", usageKind, args);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000B214 File Offset: 0x00009414
		public static FunctionCallExpressionNode Variance(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Variance(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000B21D File Offset: 0x0000941D
		public static FunctionCallExpressionNode Variance(this ExpressionNode arg)
		{
			return ExpressionNodeBuilder.Variance(FunctionUsageKind.Unassigned, new ExpressionNode[] { arg });
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000B22F File Offset: 0x0000942F
		public static FunctionCallExpressionNode Variance(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Variance", usageKind, args);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000B23D File Offset: 0x0000943D
		public static FunctionCallExpressionNode PositiveValues(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("PositiveValues", functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000B254 File Offset: 0x00009454
		public static FunctionCallExpressionNode NegativeValues(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("NegativeValues", functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000B26B File Offset: 0x0000946B
		public static FunctionCallExpressionNode Average(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Average(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000B274 File Offset: 0x00009474
		public static FunctionCallExpressionNode Average(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Average(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000B286 File Offset: 0x00009486
		public static FunctionCallExpressionNode Average(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Average", usageKind, args);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000B294 File Offset: 0x00009494
		public static FunctionCallExpressionNode Count(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Count(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000B29D File Offset: 0x0000949D
		public static FunctionCallExpressionNode Count(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Count(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000B2AF File Offset: 0x000094AF
		public static FunctionCallExpressionNode Count(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Count", usageKind, args);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000B2BD File Offset: 0x000094BD
		public static FunctionCallExpressionNode DistinctCount(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.DistinctCount(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000B2C6 File Offset: 0x000094C6
		public static FunctionCallExpressionNode DistinctCount(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.DistinctCount(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000B2D8 File Offset: 0x000094D8
		public static FunctionCallExpressionNode DistinctCount(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("DistinctCount", usageKind, args);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000B2E6 File Offset: 0x000094E6
		public static FunctionCallExpressionNode CountRows(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.CountRows(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000B2EF File Offset: 0x000094EF
		public static FunctionCallExpressionNode CountRows(this ExpressionNode arg, bool excludeBlankRow = false, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.CountRows(functionUsageKind, new ExpressionNode[]
			{
				arg,
				ExpressionNodeBuilder.Literal(excludeBlankRow)
			});
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000B30F File Offset: 0x0000950F
		public static FunctionCallExpressionNode CountRows(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("CountRows", usageKind, args);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000B31D File Offset: 0x0000951D
		public static FunctionCallExpressionNode Ceiling(this ExpressionNode number, ExpressionNode significance = null, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			significance = significance ?? LiteralExpressionNode.OneInt64;
			return ExpressionNodeBuilder.Function("Ceiling", functionUsageKind, new ExpressionNode[] { number, significance });
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000B344 File Offset: 0x00009544
		public static FunctionCallExpressionNode Floor(this ExpressionNode number, ExpressionNode significance, FunctionUsageKind usageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("Floor", usageKind, new ExpressionNode[] { number, significance });
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000B35F File Offset: 0x0000955F
		public static FunctionCallExpressionNode Format(this ExpressionNode field, string format, FunctionUsageKind usageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("Format", usageKind, new ExpressionNode[]
			{
				field,
				ExpressionNodeBuilder.Literal(format)
			});
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000B37F File Offset: 0x0000957F
		public static FunctionCallExpressionNode Sqrt(this ExpressionNode number, FunctionUsageKind usageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("Sqrt", usageKind, new ExpressionNode[] { number });
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000B396 File Offset: 0x00009596
		public static FunctionCallExpressionNode Log10(this ExpressionNode number, FunctionUsageKind usageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("Log10", usageKind, new ExpressionNode[] { number });
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000B3AD File Offset: 0x000095AD
		public static FunctionCallExpressionNode Power(this ExpressionNode number, ExpressionNode power, FunctionUsageKind usageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("Power", usageKind, new ExpressionNode[] { number, power });
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000B3C8 File Offset: 0x000095C8
		public static FunctionCallExpressionNode Between(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Between(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000B3D1 File Offset: 0x000095D1
		public static FunctionCallExpressionNode Between(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Between", usageKind, args);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000B3DF File Offset: 0x000095DF
		public static FunctionCallExpressionNode Any(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Any(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public static FunctionCallExpressionNode Any(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Any", usageKind, args);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000B3F6 File Offset: 0x000095F6
		public static ExpressionNode ScopeKeys(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("ScopeKeys", usageKind, args);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000B404 File Offset: 0x00009604
		public static ExpressionNode MinValue(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.MinValue(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000B40D File Offset: 0x0000960D
		public static ExpressionNode MinValue(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("MinValue", usageKind, args);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000B41B File Offset: 0x0000961B
		public static ExpressionNode MaxValue(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.MaxValue(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000B424 File Offset: 0x00009624
		public static ExpressionNode MaxValue(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("MaxValue", usageKind, args);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000B432 File Offset: 0x00009632
		public static FunctionCallExpressionNode Evaluate(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Evaluate", FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000B440 File Offset: 0x00009640
		public static FunctionCallExpressionNode Evaluate(this ExpressionNode arg)
		{
			return ExpressionNodeBuilder.Function("Evaluate", FunctionUsageKind.Unassigned, new ExpressionNode[] { arg });
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000B457 File Offset: 0x00009657
		public static FunctionCallExpressionNode Evaluate(this ExpressionNode arg, ExpressionNode arg2)
		{
			return ExpressionNodeBuilder.Function("Evaluate", FunctionUsageKind.Unassigned, new ExpressionNode[] { arg, arg2 });
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000B472 File Offset: 0x00009672
		public static FunctionCallExpressionNode Evaluate(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Evaluate", usageKind, args);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000B480 File Offset: 0x00009680
		public static FunctionCallExpressionNode Subtotal(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Subtotal(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000B489 File Offset: 0x00009689
		public static FunctionCallExpressionNode Subtotal(this ExpressionNode arg, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Subtotal(functionUsageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000B49B File Offset: 0x0000969B
		public static FunctionCallExpressionNode Subtotal(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Subtotal", usageKind, args);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000B4A9 File Offset: 0x000096A9
		public static ExpressionNode Rollup(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Rollup", FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public static ExpressionNode Rollup(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Rollup", usageKind, args);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000B4C5 File Offset: 0x000096C5
		public static ExpressionNode Intersect(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Intersect(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000B4CE File Offset: 0x000096CE
		public static ExpressionNode Intersect(this ExpressionNode arg, ExpressionNode arg2)
		{
			return ExpressionNodeBuilder.Intersect(FunctionUsageKind.Unassigned, new ExpressionNode[] { arg, arg2 });
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public static FunctionCallExpressionNode Intersect(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Intersect", usageKind, args);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000B4F2 File Offset: 0x000096F2
		public static FunctionCallExpressionNode ScopeOf(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.ScopeOf(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000B4FB File Offset: 0x000096FB
		public static FunctionCallExpressionNode ScopeOf(this ExpressionNode arg)
		{
			return ExpressionNodeBuilder.ScopeOf(FunctionUsageKind.Unassigned, new ExpressionNode[] { arg });
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000B50D File Offset: 0x0000970D
		public static FunctionCallExpressionNode ScopeOf(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("ScopeOf", usageKind, args);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000B51B File Offset: 0x0000971B
		public static FunctionCallExpressionNode Scope(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Scope(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000B524 File Offset: 0x00009724
		public static FunctionCallExpressionNode Scope(this ExpressionNode arg)
		{
			return ExpressionNodeBuilder.Scope(FunctionUsageKind.Unassigned, new ExpressionNode[] { arg });
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000B536 File Offset: 0x00009736
		public static FunctionCallExpressionNode Scope(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Scope", usageKind, args);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000B544 File Offset: 0x00009744
		public static ExpressionNode Comparable(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Comparable(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000B54D File Offset: 0x0000974D
		public static ExpressionNode Comparable(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Comparable", usageKind, args);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000B55B File Offset: 0x0000975B
		public static ExpressionNode Array(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Array(FunctionUsageKind.Unassigned, args);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000B564 File Offset: 0x00009764
		public static ExpressionNode Array(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("Array", usageKind, args);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000B572 File Offset: 0x00009772
		public static FunctionCallExpressionNode IsNull(this ExpressionNode arg)
		{
			return arg.IsNull(FunctionUsageKind.Unassigned);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000B57B File Offset: 0x0000977B
		public static FunctionCallExpressionNode IsNull(this ExpressionNode arg, FunctionUsageKind usageKind)
		{
			return ExpressionNodeBuilder.Function("IsNull", usageKind, new ExpressionNode[] { arg });
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000B592 File Offset: 0x00009792
		public static FunctionCallExpressionNode IsZero(this ExpressionNode arg, bool isNullable, FunctionUsageKind usageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("IsZero", usageKind, new ExpressionNode[]
			{
				arg,
				ExpressionNodeBuilder.Literal(isNullable)
			});
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000B5B7 File Offset: 0x000097B7
		public static FunctionCallExpressionNode If(this ExpressionNode condition, ExpressionNode trueValue, ExpressionNode falseValue)
		{
			return ExpressionNodeBuilder.Function("If", FunctionUsageKind.Unassigned, new ExpressionNode[] { condition, trueValue, falseValue });
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000B5D6 File Offset: 0x000097D6
		public static ExpressionNode Add(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.Add, arg1, arg2);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public static ExpressionNode Subtract(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.Subtract, arg1, arg2);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000B5EB File Offset: 0x000097EB
		public static ExpressionNode Multiply(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.Multiply, arg1, arg2);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000B5F5 File Offset: 0x000097F5
		public static ExpressionNode Divide(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.Divide, arg1, arg2);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000B5FF File Offset: 0x000097FF
		public static ExpressionNode And(this ExpressionNode arg1, ExpressionNode arg2)
		{
			if (arg1 == null)
			{
				return arg2;
			}
			if (arg2 == null)
			{
				return arg1;
			}
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.And, arg1, arg2);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000B613 File Offset: 0x00009813
		public static ExpressionNode Or(this ExpressionNode arg1, ExpressionNode arg2)
		{
			if (arg1 == null)
			{
				return arg2;
			}
			if (arg2 == null)
			{
				return arg1;
			}
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.Or, arg1, arg2);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000B628 File Offset: 0x00009828
		public static ExpressionNode GreaterThan(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.GreaterThan, arg1, arg2);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000B632 File Offset: 0x00009832
		public static ExpressionNode GreaterThanOrEqual(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.GreaterThanOrEqual, arg1, arg2);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000B63C File Offset: 0x0000983C
		public static ExpressionNode LessThan(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.LessThan, arg1, arg2);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000B646 File Offset: 0x00009846
		public static ExpressionNode LessThanOrEqual(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.LessThanOrEqual, arg1, arg2);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000B650 File Offset: 0x00009850
		public static ExpressionNode Equal(this ExpressionNode arg1, ExpressionNode arg2)
		{
			return new BinaryOperatorExpressionNode(BinaryOperatorKind.Equal, arg1, arg2);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000B65A File Offset: 0x0000985A
		public static FunctionCallExpressionNode BinColumnMin(ExpressionNode column, ExpressionNode count)
		{
			return ExpressionNodeBuilder.Function("BinColumnMin", FunctionUsageKind.Unassigned, new ExpressionNode[] { column, count });
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000B675 File Offset: 0x00009875
		public static FunctionCallExpressionNode BinColumnMax(ExpressionNode column, ExpressionNode count, ExpressionNode binMinColumn)
		{
			return ExpressionNodeBuilder.Function("BinColumnMax", FunctionUsageKind.Unassigned, new ExpressionNode[] { column, count, binMinColumn });
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000B694 File Offset: 0x00009894
		public static FunctionCallExpressionNode Function(string name, FunctionUsageKind usageKind, params ExpressionNode[] arguments)
		{
			return new FunctionCallExpressionNode(FunctionDescriptorFactory.GetDescriptor(name), usageKind, arguments);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000B6A3 File Offset: 0x000098A3
		public static StructureReferenceExpressionNode StructureReference(this Identifier targetId)
		{
			return new StructureReferenceExpressionNode(targetId);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000B6AB File Offset: 0x000098AB
		public static StructureReferenceExpressionNode StructureReference(this string targetId)
		{
			return new StructureReferenceExpressionNode(targetId);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public static ExpressionNode TransformTableColumnReference(this StructureReferenceExpressionNode table, StructureReferenceExpressionNode column)
		{
			return new DataTransformTableColumnReferenceExpressionNode(table, column);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000B6C1 File Offset: 0x000098C1
		public static ExpressionNode DataTransformTableColumn(string table, string column)
		{
			return new DataTransformTableColumnReferenceExpressionNode(new StructureReferenceExpressionNode(table), new StructureReferenceExpressionNode(column));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000B6DE File Offset: 0x000098DE
		public static ExpressionNode TransformOutputRoleRef(this string roleName, FunctionUsageKind functionUsageKind = FunctionUsageKind.Unassigned)
		{
			return ExpressionNodeBuilder.Function("TransformOutputRoleRef", functionUsageKind, new ExpressionNode[] { ExpressionNodeBuilder.Literal(roleName) });
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000B6FA File Offset: 0x000098FA
		public static DaxTextExpressionNode DaxText(this string daxText)
		{
			return new DaxTextExpressionNode(daxText);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000B702 File Offset: 0x00009902
		public static QueryParameterReferenceExpressionNode QueryParameter(this string name)
		{
			return new QueryParameterReferenceExpressionNode(name);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000B70A File Offset: 0x0000990A
		public static VisualCalculationExpressionNode VisualCalculation(this string daxExpression)
		{
			return daxExpression.DaxText().VisualCalculation();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000B717 File Offset: 0x00009917
		public static VisualCalculationExpressionNode VisualCalculation(this ExpressionNode expression)
		{
			return new VisualCalculationExpressionNode(expression);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000B720 File Offset: 0x00009920
		internal static ExpressionNode DsqExpression(this IConceptualProperty property)
		{
			IConceptualEntity entity = property.Entity;
			string text = "";
			if (entity.Schema != null && entity.Schema.SchemaId != null)
			{
				text = entity.Schema.SchemaId;
			}
			if (text == "")
			{
				return ExpressionNodeBuilder.ModelProperty(entity.DsqExpression(), property.EdmName, property);
			}
			return ExpressionNodeBuilder.ModelProperty(text, entity.Name, property.Name, property);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000B78E File Offset: 0x0000998E
		internal static EntitySetExpressionNode DsqExpression(this IConceptualEntity entity)
		{
			return ExpressionNodeBuilder.EntitySet(entity.EntityContainerName, entity.EdmName, entity);
		}
	}
}
