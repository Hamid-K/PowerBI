using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000026 RID: 38
	internal static class ExprNodes
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00006A4F File Offset: 0x00004C4F
		public static EntitySetExpressionNode EntitySet(string container, string entitySet)
		{
			return ExpressionNodeBuilder.EntitySet(container, entitySet, null);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006A59 File Offset: 0x00004C59
		public static PropertyExpressionNode ModelProperty(string container, string entitySet, string propertyName)
		{
			return ExpressionNodeBuilder.ModelProperty(container, entitySet, propertyName, null);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006A64 File Offset: 0x00004C64
		public static LiteralExpressionNode Literal(object value)
		{
			return ExpressionNodeBuilder.Literal(value);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006A6C File Offset: 0x00004C6C
		public static FunctionCallExpressionNode BinColumnMin(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("BinColumnMin", FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006A7A File Offset: 0x00004C7A
		public static FunctionCallExpressionNode BinColumnMax(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Function("BinColumnMax", FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006A88 File Offset: 0x00004C88
		public static FunctionCallExpressionNode Max(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Max(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006A91 File Offset: 0x00004C91
		public static FunctionCallExpressionNode Max(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Max(usageKind, args);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006A9A File Offset: 0x00004C9A
		public static FunctionCallExpressionNode Min(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Min(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006AA3 File Offset: 0x00004CA3
		public static FunctionCallExpressionNode Min(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Min(usageKind, args);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006AAC File Offset: 0x00004CAC
		public static FunctionCallExpressionNode SingleValue(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.SingleValue(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006AB5 File Offset: 0x00004CB5
		public static FunctionCallExpressionNode SingleValue(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.SingleValue(usageKind, args);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006ABE File Offset: 0x00004CBE
		public static FunctionCallExpressionNode Sum(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Sum(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006AC7 File Offset: 0x00004CC7
		public static FunctionCallExpressionNode Sum(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Sum(usageKind, args);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static FunctionCallExpressionNode PercentileInc(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.PercentileInc(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006AD9 File Offset: 0x00004CD9
		public static FunctionCallExpressionNode PercentileInc(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.PercentileInc(usageKind, args);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006AE2 File Offset: 0x00004CE2
		public static FunctionCallExpressionNode PercentileExc(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.PercentileExc(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006AEB File Offset: 0x00004CEB
		public static FunctionCallExpressionNode PercentileExc(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.PercentileExc(usageKind, args);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static FunctionCallExpressionNode Median(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Median(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006AFD File Offset: 0x00004CFD
		public static FunctionCallExpressionNode Median(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Median(usageKind, args);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006B06 File Offset: 0x00004D06
		public static FunctionCallExpressionNode StandardDeviation(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.StandardDeviation(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006B0F File Offset: 0x00004D0F
		public static FunctionCallExpressionNode StandardDeviation(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.StandardDeviation(usageKind, args);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006B18 File Offset: 0x00004D18
		public static FunctionCallExpressionNode Variance(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Variance(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006B21 File Offset: 0x00004D21
		public static FunctionCallExpressionNode Variance(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Variance(usageKind, args);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006B2A File Offset: 0x00004D2A
		public static FunctionCallExpressionNode PositiveValues(ExpressionNode arg)
		{
			return arg.PositiveValues(FunctionUsageKind.Query);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006B33 File Offset: 0x00004D33
		public static FunctionCallExpressionNode NegativeValues(ExpressionNode arg)
		{
			return arg.NegativeValues(FunctionUsageKind.Query);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006B3C File Offset: 0x00004D3C
		public static FunctionCallExpressionNode Average(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Average(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006B45 File Offset: 0x00004D45
		public static FunctionCallExpressionNode Average(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Average(usageKind, args);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006B4E File Offset: 0x00004D4E
		public static FunctionCallExpressionNode Count(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Count(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006B57 File Offset: 0x00004D57
		public static FunctionCallExpressionNode Count(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Count(usageKind, args);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006B60 File Offset: 0x00004D60
		public static FunctionCallExpressionNode DistinctCount(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.DistinctCount(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006B69 File Offset: 0x00004D69
		public static FunctionCallExpressionNode DistinctCount(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.DistinctCount(usageKind, args);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006B72 File Offset: 0x00004D72
		public static FunctionCallExpressionNode CountRows(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.CountRows(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006B7B File Offset: 0x00004D7B
		public static FunctionCallExpressionNode CountRows(ExpressionNode arg, bool excludeBlankRow)
		{
			return arg.CountRows(excludeBlankRow, FunctionUsageKind.Query);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006B85 File Offset: 0x00004D85
		public static FunctionCallExpressionNode CountRows(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.CountRows(usageKind, args);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006B8E File Offset: 0x00004D8E
		public static FunctionCallExpressionNode Between(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Between(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006B97 File Offset: 0x00004D97
		public static FunctionCallExpressionNode Between(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Between(usageKind, args);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public static FunctionCallExpressionNode Ceiling(ExpressionNode number, ExpressionNode significance = null)
		{
			return number.Ceiling(significance, FunctionUsageKind.Query);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006BAA File Offset: 0x00004DAA
		public static FunctionCallExpressionNode Any(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Any(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006BB3 File Offset: 0x00004DB3
		public static FunctionCallExpressionNode Any(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Any(usageKind, args);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006BBC File Offset: 0x00004DBC
		public static ExpressionNode ScopeKeys(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.ScopeKeys(usageKind, args);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006BC5 File Offset: 0x00004DC5
		public static ExpressionNode MinValue(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.MinValue(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006BCE File Offset: 0x00004DCE
		public static ExpressionNode MinValue(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.MinValue(usageKind, args);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006BD7 File Offset: 0x00004DD7
		public static ExpressionNode MaxValue(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.MaxValue(FunctionUsageKind.Query, args);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public static ExpressionNode MaxValue(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.MaxValue(usageKind, args);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006BE9 File Offset: 0x00004DE9
		public static ExpressionNode Sqrt(ExpressionNode node)
		{
			return node.Sqrt(FunctionUsageKind.Query);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006BF2 File Offset: 0x00004DF2
		public static ExpressionNode Evaluate(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Evaluate(FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006BFB File Offset: 0x00004DFB
		public static ExpressionNode Evaluate(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Evaluate(usageKind, args);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006C04 File Offset: 0x00004E04
		public static ExpressionNode Floor(ExpressionNode number, ExpressionNode significance)
		{
			return number.Floor(significance, FunctionUsageKind.Query);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006C0E File Offset: 0x00004E0E
		public static ExpressionNode Subtotal(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Subtotal(FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006C17 File Offset: 0x00004E17
		public static ExpressionNode Subtotal(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Subtotal(usageKind, args);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006C20 File Offset: 0x00004E20
		public static ExpressionNode Rollup(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Rollup(FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006C29 File Offset: 0x00004E29
		public static ExpressionNode Rollup(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Rollup(usageKind, args);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006C32 File Offset: 0x00004E32
		public static ExpressionNode Intersect(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Intersect(FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006C3B File Offset: 0x00004E3B
		public static FunctionCallExpressionNode Intersect(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Intersect(usageKind, args);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006C44 File Offset: 0x00004E44
		public static FunctionCallExpressionNode ScopeOf(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.ScopeOf(FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006C4D File Offset: 0x00004E4D
		public static FunctionCallExpressionNode ScopeOf(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.ScopeOf(usageKind, args);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006C56 File Offset: 0x00004E56
		public static FunctionCallExpressionNode Scope(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Scope(FunctionUsageKind.DataShapeQueryTranslation, args);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006C5F File Offset: 0x00004E5F
		public static FunctionCallExpressionNode Scope(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Scope(usageKind, args);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006C68 File Offset: 0x00004E68
		public static ExpressionNode Comparable(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Comparable(FunctionUsageKind.Processing, args);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006C71 File Offset: 0x00004E71
		public static ExpressionNode Comparable(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Comparable(usageKind, args);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006C7A File Offset: 0x00004E7A
		public static ExpressionNode Array(params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Array(FunctionUsageKind.Processing, args);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006C83 File Offset: 0x00004E83
		public static ExpressionNode Array(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.Array(usageKind, args);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006C8C File Offset: 0x00004E8C
		public static FunctionCallExpressionNode IsNull(ExpressionNode arg)
		{
			return arg.IsNull(FunctionUsageKind.Query);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006C95 File Offset: 0x00004E95
		public static FunctionCallExpressionNode IsEmptyTable(ExpressionNode arg)
		{
			return ExprNodes.Function("IsEmptyTable", FunctionUsageKind.Query, new ExpressionNode[] { arg });
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006CAC File Offset: 0x00004EAC
		public static FunctionCallExpressionNode IsNull(FunctionUsageKind usageKind, ExpressionNode arg)
		{
			return arg.IsNull(usageKind);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006CB5 File Offset: 0x00004EB5
		public static FunctionCallExpressionNode IsZero(ExpressionNode arg, bool isNullable)
		{
			return arg.IsZero(isNullable, FunctionUsageKind.Query);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006CBF File Offset: 0x00004EBF
		public static ExpressionNode Log10(ExpressionNode arg)
		{
			return arg.Log10(FunctionUsageKind.Query);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public static ExpressionNode Power(ExpressionNode number, ExpressionNode power)
		{
			return number.Power(power, FunctionUsageKind.Query);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006CD2 File Offset: 0x00004ED2
		public static FunctionCallExpressionNode Function(string name, FunctionUsageKind usageKind, params ExpressionNode[] arguments)
		{
			return ExpressionNodeBuilder.Function(name, usageKind, arguments);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006CDC File Offset: 0x00004EDC
		public static StructureReferenceExpressionNode StructureReference(Identifier targetId)
		{
			return targetId.StructureReference();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public static ExpressionNode DataTransformTableColumn(string table, string column)
		{
			return ExpressionNodeBuilder.DataTransformTableColumn(table, column);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006CED File Offset: 0x00004EED
		public static ExpressionNode TransformOutputRoleRef(string roleName)
		{
			return roleName.TransformOutputRoleRef(FunctionUsageKind.DataShapeQueryTranslation);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006CF6 File Offset: 0x00004EF6
		public static ResolvedEntitySetExpressionNode EntitySet(IConceptualEntity entity)
		{
			return new ResolvedEntitySetExpressionNode(entity);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006CFE File Offset: 0x00004EFE
		public static ResolvedPropertyExpressionNode ModelProperty(IConceptualProperty property)
		{
			return new ResolvedPropertyExpressionNode(property);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006D06 File Offset: 0x00004F06
		public static RelatedResolvedPropertyExpressionNode RelatedModelProperty(IConceptualProperty property)
		{
			return new RelatedResolvedPropertyExpressionNode(property);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006D0E File Offset: 0x00004F0E
		public static ExpressionNode If(this ExpressionNode condition, ExpressionNode thenEx, ExpressionNode elseEx)
		{
			return ExprNodes.If(FunctionUsageKind.Query, condition, thenEx, elseEx);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006D1C File Offset: 0x00004F1C
		public static ExpressionNode If(FunctionUsageKind usageKind, ExpressionNode condition, ExpressionNode thenEx, ExpressionNode elseEx)
		{
			if (thenEx == elseEx)
			{
				return thenEx;
			}
			LiteralExpressionNode literalExpressionNode = condition as LiteralExpressionNode;
			if (literalExpressionNode != null)
			{
				if (literalExpressionNode.Value == true)
				{
					return thenEx;
				}
				if (literalExpressionNode.Value == false)
				{
					return elseEx;
				}
			}
			return ExprNodes.Function("If", usageKind, new ExpressionNode[] { condition, thenEx, elseEx });
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006D80 File Offset: 0x00004F80
		public static ExpressionNode IfError(this ExpressionNode value, ExpressionNode valueIfError)
		{
			return ExprNodes.IfError(FunctionUsageKind.Query, value, valueIfError);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006D8A File Offset: 0x00004F8A
		public static ExpressionNode IfError(FunctionUsageKind usageKind, ExpressionNode value, ExpressionNode valueIfError)
		{
			return ExprNodes.Function("IfError", usageKind, new ExpressionNode[] { value, valueIfError });
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public static ExpressionNode Minus(this ExpressionNode arg)
		{
			return new UnaryOperatorExpressionNode(UnaryOperatorKind.Minus, arg);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006DAE File Offset: 0x00004FAE
		public static ExpressionNode SingleValue(this ExpressionNode input)
		{
			return new SingleValueExpressionNode(input);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public static FunctionCallExpressionNode SynchronizationIndex(ExpressionNode arg)
		{
			return ExpressionNodeBuilder.SynchronizationIndex(FunctionUsageKind.DataShapeQueryTranslation, new ExpressionNode[] { arg });
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public static FunctionCallExpressionNode SynchronizationIndex(FunctionUsageKind usageKind, params ExpressionNode[] args)
		{
			return ExpressionNodeBuilder.SynchronizationIndex(usageKind, args);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006DD1 File Offset: 0x00004FD1
		public static UnaryOperatorExpressionNode Not(this ExpressionNode arg)
		{
			return new UnaryOperatorExpressionNode(UnaryOperatorKind.Not, arg);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006DDA File Offset: 0x00004FDA
		public static ResolvedCalculationReferenceExpressionNode CalculationReference(Calculation calculation)
		{
			return new ResolvedCalculationReferenceExpressionNode(calculation);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006DE2 File Offset: 0x00004FE2
		public static ResolvedLimitReferenceExpressionNode LimitReference(Limit limit)
		{
			return new ResolvedLimitReferenceExpressionNode(limit);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006DEA File Offset: 0x00004FEA
		public static ResolvedRollupExpressionNode ResolvedRollup(Calculation calculation)
		{
			return new ResolvedRollupExpressionNode(new ResolvedCalculationReferenceExpressionNode(calculation));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006DF7 File Offset: 0x00004FF7
		public static ResolvedScopeReferenceExpressionNode ScopeReference(IScope scope)
		{
			return new ResolvedScopeReferenceExpressionNode(scope);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006DFF File Offset: 0x00004FFF
		public static ExpressionNode Field(IDataSetPlan dataSetPlan, string fieldName, PlanNamedTable tablePlan = null)
		{
			return new DataSetFieldReferenceExpressionNode(dataSetPlan, fieldName, tablePlan);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006E09 File Offset: 0x00005009
		public static ExpressionNode SubExpression(ExpressionNode targetNode, WritableExpressionTable expressionTable)
		{
			return expressionTable.CreateSubExpression(targetNode);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006E12 File Offset: 0x00005012
		public static ExpressionNode SubExpression(ExpressionId targetId)
		{
			return new SubExpressionNode(targetId);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006E1C File Offset: 0x0000501C
		public static ExpressionNode AggregatableSubQuery(Expression expression, DataSetPlan dataSetPlan)
		{
			return new AggregatableSubQueryExpressionNode(expression.ExpressionId.Value, dataSetPlan, null);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006E40 File Offset: 0x00005040
		public static ExpressionNode SubQuery(Expression expression, PlanOperation table)
		{
			return new AggregatableSubQueryExpressionNode(expression.ExpressionId.Value, null, table);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006E62 File Offset: 0x00005062
		public static ExpressionNode TableSubQuery(DataSetPlan dataSetPlan, IContextItem source, string name)
		{
			return new TableSubQueryExpressionNode(dataSetPlan, source, name);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006E6C File Offset: 0x0000506C
		public static ExpressionNode CurrentGroup(ExpressionNode projection)
		{
			return new AggregatableCurrentGroupExpressionNode(projection);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006E74 File Offset: 0x00005074
		public static ExpressionNode RemoveGroupings(ExpressionNode expression, params ExpressionNode[] groupKeysToRemove)
		{
			return new RemoveGroupingsExpressionNode(expression, groupKeysToRemove);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006E7D File Offset: 0x0000507D
		public static ExpressionNode Placeholder()
		{
			return new PlaceholderExpressionNode();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006E84 File Offset: 0x00005084
		public static ExpressionNode Filter(Calculation calculation, FilterCondition filterCondition, ExpressionNode innerExpression, ExpressionNode filterExpression = null)
		{
			return new FilterInlinedCalculationExpressionNode(innerExpression, calculation, filterCondition, filterExpression);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006E8F File Offset: 0x0000508F
		public static ExpressionNode ResolvedGroupKeyReference(GroupKey groupKey)
		{
			return new ResolvedGroupKeyReferenceExpressionNode(groupKey);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006E97 File Offset: 0x00005097
		public static ExpressionNode ResolvedSortKeyReference(SortKey sortKey)
		{
			return new ResolvedSortKeyReferenceExpressionNode(sortKey);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006E9F File Offset: 0x0000509F
		public static ExpressionNode ResolvedScopeValueDefinitionReference(ScopeValueDefinition scopeValueDefinition)
		{
			return new ResolvedScopeValueDefinitionReferenceExpressionNode(scopeValueDefinition);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006EA7 File Offset: 0x000050A7
		public static ExpressionNode ResolvedScopeReference(IScope scope)
		{
			return new ResolvedScopeReferenceExpressionNode(scope);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006EAF File Offset: 0x000050AF
		public static ExpressionNode BatchColumnReference(string columnName)
		{
			return new BatchColumnReferenceExpressionNode(columnName);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006EB7 File Offset: 0x000050B7
		public static ExpressionNode BatchScalarDeclarationReference(string declarationName)
		{
			return new BatchScalarDeclarationReferenceExpressionNode(declarationName);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006EBF File Offset: 0x000050BF
		public static ExpressionNode BatchSubQuery(PlanOperation table)
		{
			return new BatchSubQueryExpressionNode(table);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006EC7 File Offset: 0x000050C7
		public static ExpressionNode ApplyContextFilter(ExpressionNode node, PlanOperation contextTable)
		{
			return new ApplyContextFilterExpressionNode(node, new PlanOperation[] { contextTable }.ToReadOnlyCollection<PlanOperation>());
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006EDE File Offset: 0x000050DE
		public static ExpressionNode DataTransformTableColumn(DataTransformTable table, DataTransformTableColumn column)
		{
			return new ResolvedDataTransformTableColumnReferenceExpressionNode(table, column);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006EE7 File Offset: 0x000050E7
		public static ExpressionNode QueryParameterReference(string name)
		{
			return new QueryParameterReferenceExpressionNode(name);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006EEF File Offset: 0x000050EF
		public static ExpressionNode NativeVisualCalculation(string daxExpression)
		{
			return ExprNodes.VisualCalculation(new DaxTextExpressionNode(daxExpression));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006EFC File Offset: 0x000050FC
		public static ExpressionNode VisualCalculation(ExpressionNode expression)
		{
			return new VisualCalculationExpressionNode(expression);
		}
	}
}
