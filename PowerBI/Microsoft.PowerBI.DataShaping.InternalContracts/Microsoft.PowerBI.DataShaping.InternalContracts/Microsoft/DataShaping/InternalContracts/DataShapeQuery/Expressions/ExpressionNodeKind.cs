using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000CE RID: 206
	internal enum ExpressionNodeKind
	{
		// Token: 0x0400023D RID: 573
		AggregatableCurrentGroupExpression,
		// Token: 0x0400023E RID: 574
		AggregatableSubQueryExpression,
		// Token: 0x0400023F RID: 575
		ApplyContextFilter,
		// Token: 0x04000240 RID: 576
		BatchColumnReference,
		// Token: 0x04000241 RID: 577
		BatchColumnReferenceByExpressionId,
		// Token: 0x04000242 RID: 578
		BatchFilterInlinedDeclarationCalculation,
		// Token: 0x04000243 RID: 579
		BatchScalarDeclarationReference,
		// Token: 0x04000244 RID: 580
		BatchSubQuery,
		// Token: 0x04000245 RID: 581
		BinaryOperator,
		// Token: 0x04000246 RID: 582
		DataSetFieldReference,
		// Token: 0x04000247 RID: 583
		DataTransformTableColumnReference,
		// Token: 0x04000248 RID: 584
		DaxText,
		// Token: 0x04000249 RID: 585
		EntitySet,
		// Token: 0x0400024A RID: 586
		FunctionCall,
		// Token: 0x0400024B RID: 587
		FilterInlinedCalculation,
		// Token: 0x0400024C RID: 588
		Literal,
		// Token: 0x0400024D RID: 589
		Placeholder,
		// Token: 0x0400024E RID: 590
		Property,
		// Token: 0x0400024F RID: 591
		RelatedResolvedProperty,
		// Token: 0x04000250 RID: 592
		RemoveGroupings,
		// Token: 0x04000251 RID: 593
		ResolvedCalculationReference,
		// Token: 0x04000252 RID: 594
		ResolvedDataTransformTableColumnReference,
		// Token: 0x04000253 RID: 595
		ResolvedEntitySet,
		// Token: 0x04000254 RID: 596
		ResolvedFilterConditionReference,
		// Token: 0x04000255 RID: 597
		ResolvedGroupKeyReference,
		// Token: 0x04000256 RID: 598
		ResolvedLimitReference,
		// Token: 0x04000257 RID: 599
		ResolvedProperty,
		// Token: 0x04000258 RID: 600
		ResolvedScopeReference,
		// Token: 0x04000259 RID: 601
		ResolvedScopeValueDefinitionReference,
		// Token: 0x0400025A RID: 602
		ResolvedSortKeyReference,
		// Token: 0x0400025B RID: 603
		ResolvedRollup,
		// Token: 0x0400025C RID: 604
		Rollup,
		// Token: 0x0400025D RID: 605
		SingleValue,
		// Token: 0x0400025E RID: 606
		StructureReference,
		// Token: 0x0400025F RID: 607
		SubExpression,
		// Token: 0x04000260 RID: 608
		TableSubQueryExpression,
		// Token: 0x04000261 RID: 609
		UnaryOperator,
		// Token: 0x04000262 RID: 610
		VisualCalculation,
		// Token: 0x04000263 RID: 611
		QueryParameterReference
	}
}
