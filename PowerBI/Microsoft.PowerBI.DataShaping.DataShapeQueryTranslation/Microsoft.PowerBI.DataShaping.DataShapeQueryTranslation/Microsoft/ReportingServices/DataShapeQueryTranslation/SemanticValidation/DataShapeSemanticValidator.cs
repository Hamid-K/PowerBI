using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000062 RID: 98
	internal sealed class DataShapeSemanticValidator : DataShapeVisitor
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x0000FC70 File Offset: 0x0000DE70
		private DataShapeSemanticValidator(ExpressionTable expressionTable, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, DataShapeQueryTranslationOptions translationOptions)
		{
			this.m_expressionTable = expressionTable;
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_schema = schema;
			this.m_translationOptions = translationOptions;
			this.m_expressionValidator = new ExpressionSemanticValidator(this.m_errorContext, this.m_expressionTable, this.m_scopeTree, this.m_annotations, this.DaxCapabilitiesAnnotation);
			this.m_limitValidator = new LimitSemanticValidator(this.m_expressionTable, this.m_scopeTree, this.m_errorContext, this.m_annotations, this.m_expressionValidator);
			this.m_calculationValidationResults = new Dictionary<Identifier, ExpressionValidationResult>();
			this.m_dataShapesWithSubtotals = new HashSet<DataShape>();
			this.m_parentDataShapes = new Stack<DataShape>();
			this.m_contextDataShapes = new Stack<DataShape>();
			this.m_parents = new Stack<IIdentifiable>();
			this.m_entitiesRelatedToOneCache = new Dictionary<IConceptualEntity, IReadOnlyList<IConceptualEntity>>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			this.m_primaryGroupKeyExpressionNodes = new Stack<HashSet<ExpressionNode>>();
			this.m_secondaryGroupKeyExpressionNodes = new Stack<HashSet<ExpressionNode>>();
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000FD66 File Offset: 0x0000DF66
		public static ReadOnlyExpressionTable Validate(DataShape dataShape, IFederatedConceptualSchema schema, ExpressionTable expressionTable, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, DataShapeQueryTranslationOptions translationOptions)
		{
			DataShapeSemanticValidator dataShapeSemanticValidator = new DataShapeSemanticValidator(expressionTable, schema, annotations, scopeTree, errorContext, translationOptions);
			dataShapeSemanticValidator.Visit(dataShape);
			return dataShapeSemanticValidator.m_expressionValidator.OutputExpressionTable;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000FD87 File Offset: 0x0000DF87
		public bool InTopLevelDataShape
		{
			get
			{
				return this.m_parentDataShapes.Count <= 1;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		protected override void Visit(DataShape dataShape)
		{
			int scopeFilterCount = this.m_scopeFilterCount;
			int contextFilterCount = this.m_contextFilterCount;
			if (dataShape.IsIndependent)
			{
				this.m_scopeFilterCount = 0;
				this.m_contextFilterCount = 0;
			}
			base.Visit(dataShape);
			if (dataShape.IsIndependent)
			{
				this.m_scopeFilterCount = scopeFilterCount;
				this.m_contextFilterCount = contextFilterCount;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		protected override void Enter(DataShape dataShape)
		{
			this.m_parentDataShapes.Push(dataShape);
			this.m_parents.Push(dataShape);
			this.PopulateGroupKeyCollection(dataShape.PrimaryHierarchy, this.m_primaryGroupKeyExpressionNodes);
			this.PopulateGroupKeyCollection(dataShape.SecondaryHierarchy, this.m_secondaryGroupKeyExpressionNodes);
			if (this.ContainedByContextFilterDataShape)
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterDataShapeDoesNotAllowNestedDataShapes(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape.Id, "DataShape", this.m_contextDataShapes.Peek().Id));
			}
			if (dataShape.IsFilterContextDataShape(this.m_scopeTree, this.m_annotations))
			{
				this.m_contextDataShapes.Push(dataShape);
				this.ValidateFilterContextDataShape(dataShape);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000FE94 File Offset: 0x0000E094
		protected override void Exit(DataShape dataShape)
		{
			this.CheckDuplicateGroupKeysOnOppositeHierarchies(dataShape);
			this.CheckGroupSynchronization(dataShape);
			this.CheckDuplicateFilterTargets(dataShape);
			this.CheckDepthOfComplexSlicers(dataShape);
			this.m_limitValidator.ValidateTargetsAreCovered(dataShape.PrimaryHierarchy.GetAllDynamicMembers());
			this.m_limitValidator.ValidateTargetsAreCovered(dataShape.SecondaryHierarchy.GetAllDynamicMembers());
			this.CheckVisualAxisSorts(dataShape);
			this.m_primaryGroupKeyExpressionNodes.Pop();
			this.m_secondaryGroupKeyExpressionNodes.Pop();
			if (this.IsContextFilterDataShape)
			{
				this.m_contextDataShapes.Pop();
			}
			this.m_parentDataShapes.Pop();
			this.m_parents.Pop();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000FF34 File Offset: 0x0000E134
		private void CheckVisualAxisSorts(DataShape dataShape)
		{
			if (dataShape.VisualCalculationMetadata.IsNullOrEmpty<VisualAxis>())
			{
				return;
			}
			foreach (VisualAxis visualAxis in dataShape.VisualCalculationMetadata)
			{
				bool flag = false;
				List<VisualAxisGroup> groups = visualAxis.Groups;
				int num = 0;
				while (!flag && num < groups.Count)
				{
					flag = !groups[num].Member.GetResolvedMember(this.m_expressionTable).Group.SortKeys.IsNullOrEmpty<SortKey>();
					num++;
				}
				if (!flag)
				{
					this.m_errorContext.Register(TranslationMessages.VisualAxisWithoutSort(EngineMessageSeverity.Error, ObjectType.VisualAxis, dataShape.Id, "SortKeys", visualAxis.Name));
				}
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010004 File Offset: 0x0000E204
		protected override void Exit(DataIntersection dataIntersection)
		{
			base.Exit(dataIntersection);
			this.CheckNoOrOnlySubtotalCalculations(dataIntersection.Calculations, ObjectType.DataIntersection, dataIntersection.Id);
			this.CheckFilterContextDataShapeIntersection(dataIntersection);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00010028 File Offset: 0x0000E228
		private void CheckNoOrOnlySubtotalCalculations(List<Calculation> calculations, ObjectType objectType, Identifier id)
		{
			if (calculations == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < calculations.Count; i++)
			{
				if (this.m_annotations.IsSubtotal(calculations[i]))
				{
					flag = true;
				}
				else
				{
					flag2 = true;
				}
				if (flag && flag2)
				{
					this.m_errorContext.Register(TranslationMessages.SubtotalAndNonSubtotalCalculations(EngineMessageSeverity.Error, objectType, id, "Calculations"));
					return;
				}
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00010088 File Offset: 0x0000E288
		private void CheckFilterContextDataShapeIntersection(DataIntersection dataIntersection)
		{
			if (this.IsContextFilterDataShape && dataIntersection.Calculations.EmptyIfNull<Calculation>().Any<Calculation>())
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterDataShapeIntersectionMustBeEmpty(EngineMessageSeverity.Error, this.ParentDataShape.ObjectType, this.ParentDataShape.Id, "DataIntersections"));
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000100DB File Offset: 0x0000E2DB
		private DataShape ParentDataShape
		{
			get
			{
				return this.m_parentDataShapes.Peek();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000100E8 File Offset: 0x0000E2E8
		private bool ContainedByContextFilterDataShape
		{
			get
			{
				return this.m_contextDataShapes.Any<DataShape>();
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000100F5 File Offset: 0x0000E2F5
		private bool IsContextFilterDataShape
		{
			get
			{
				return this.m_contextDataShapes.Any<DataShape>() && this.ParentDataShape == this.m_contextDataShapes.Peek();
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001011C File Offset: 0x0000E31C
		private void PopulateGroupKeyCollection(DataHierarchy hierarchy, Stack<HashSet<ExpressionNode>> expressionNodes)
		{
			if (hierarchy != null)
			{
				IEnumerable<Expression> allGroupKeyExpressions = hierarchy.GetAllGroupKeyExpressions(this.m_annotations);
				expressionNodes.Push(new HashSet<ExpressionNode>(allGroupKeyExpressions.Select((Expression expr) => this.m_expressionTable.GetNode(expr))));
				return;
			}
			expressionNodes.Push(new HashSet<ExpressionNode>());
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00010162 File Offset: 0x0000E362
		protected override void Visit(DataTransformTableColumn column)
		{
			this.m_expressionValidator.Validate(column.Value, ExpressionFeatures.DataTransformTableColumnValue, column.ObjectType, column.Id, "Value", this.ParentDataShape);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00010194 File Offset: 0x0000E394
		protected override void Visit(Calculation calculation)
		{
			bool flag = this.IsContainingScopeDataShape(calculation);
			IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
			ExpressionValidationResult expressionValidationResult = this.m_expressionValidator.Validate(calculation.Value, flag ? ExpressionFeatures.DataShapeCalculationValue : ExpressionFeatures.CalculationValue, calculation.ObjectType, calculation.Id, "Value", containingScope);
			this.m_calculationValidationResults[calculation.Id] = expressionValidationResult;
			if (this.CheckSubtotalUsage(calculation, containingScope))
			{
				this.ValidateSubtotalInDataShape(this.ParentDataShape);
			}
			this.CheckCircularReferences(calculation, calculation);
			this.CheckFilterContextDataShapeCalculation(calculation);
			this.CheckComplexSlicer(calculation);
			this.CheckDetailTableCalculation(calculation, containingScope);
			this.CheckSynchronizationIndexCalculation(calculation, containingScope);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00010238 File Offset: 0x0000E438
		private void CheckSynchronizationIndexCalculation(Calculation calculation, IScope containingScope)
		{
			if (this.m_annotations.IsSynchronizationIndex(calculation))
			{
				DataMember dataMember = containingScope as DataMember;
				this.m_scopeTree.GetParentScope(dataMember);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00010268 File Offset: 0x0000E468
		private void CheckDetailTableCalculation(Calculation calculation, IScope containingScope)
		{
			DataMember dataMember = containingScope as DataMember;
			if (dataMember == null || dataMember.Group == null || dataMember.Group.DetailGroupIdentity == null)
			{
				return;
			}
			ExpressionNode node = this.m_expressionTable.GetNode(calculation.Value);
			if (!(node is FunctionCallExpressionNode) && !(node is ResolvedPropertyExpressionNode))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidDetailTableExpression(EngineMessageSeverity.Error, calculation.ObjectType, calculation.Id, "Value", node.Kind));
				return;
			}
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = this.m_expressionTable.GetNode(calculation.Value) as ResolvedPropertyExpressionNode;
			if (MeasureAnalyzer.IsModelMeasure(node))
			{
				this.m_errorContext.Register(TranslationMessages.ModelMeasuresNotSupportedForDetailTable(EngineMessageSeverity.Error, calculation.ObjectType, calculation.Id, "Value", (resolvedPropertyExpressionNode == null) ? null : TranslationMessageUtils.GetPropertyNameForError(resolvedPropertyExpressionNode)));
			}
			if (resolvedPropertyExpressionNode != null)
			{
				this.ValidateDetailTablePropertyRefOutsideAggregate(dataMember, resolvedPropertyExpressionNode, calculation, "Value");
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00010340 File Offset: 0x0000E540
		private IReadOnlyList<IConceptualEntity> GetValidDetailGroupEntities(DetailGroupIdentity detailIdentity)
		{
			IConceptualEntity conceptualEntity;
			if (!detailIdentity.TryGetDetailGroupIdentityEntity(this.m_expressionTable, out conceptualEntity))
			{
				return null;
			}
			IReadOnlyList<IConceptualEntity> relatedToOneEntities;
			if (this.m_entitiesRelatedToOneCache.TryGetValue(conceptualEntity, out relatedToOneEntities))
			{
				return relatedToOneEntities;
			}
			IConceptualEntity baseModelEntity = conceptualEntity.GetBaseModelEntity();
			IConceptualSchema defaultSchema = this.m_schema.GetDefaultSchema();
			relatedToOneEntities = QueryAlgorithms.GetRelatedToOneEntities(baseModelEntity, defaultSchema);
			this.m_entitiesRelatedToOneCache.Add(conceptualEntity, relatedToOneEntities);
			return relatedToOneEntities;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00010398 File Offset: 0x0000E598
		private void CheckComplexSlicer(Calculation calculation)
		{
			if (this.m_annotations.IsMeasure(calculation))
			{
				foreach (DataShape dataShape in this.m_parentDataShapes)
				{
					if (this.m_annotations.HasComplexSlicer(dataShape))
					{
						this.m_errorContext.Register(TranslationMessages.ComplexSlicerNotAllowedWithMeasures(EngineMessageSeverity.Error, dataShape.Id));
					}
				}
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00010418 File Offset: 0x0000E618
		private bool IsContainingScopeDataShape(Calculation calculation)
		{
			return this.m_scopeTree.GetContainingScope(calculation).ObjectType == ObjectType.DataShape;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00010430 File Offset: 0x0000E630
		private void CheckFilterContextDataShapeCalculation(Calculation calculation)
		{
			if (this.IsContextFilterDataShape && this.m_annotations.IsMeasure(calculation))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidMeasureInContextFilterDataShape(EngineMessageSeverity.Error, this.ParentDataShape.ObjectType, this.ParentDataShape.Id, "Calculations", calculation.Id));
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00010488 File Offset: 0x0000E688
		private void ValidateFilterContextDataShape(DataShape dataShape)
		{
			IEnumerable<DataMember> allDynamicMembers = dataShape.PrimaryHierarchy.GetAllDynamicMembers();
			IEnumerable<DataMember> allDynamicMembers2 = dataShape.SecondaryHierarchy.GetAllDynamicMembers();
			if (!allDynamicMembers.Any<DataMember>() && !allDynamicMembers2.Any<DataMember>())
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterDataShapeMustHaveHierarchyMembers(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape.Id));
				return;
			}
			DataShape parentDataShape = dataShape.GetParentDataShape(this.m_scopeTree, this.m_annotations);
			IEnumerable<DataMember> allDynamicMembers3 = parentDataShape.PrimaryHierarchy.GetAllDynamicMembers();
			IEnumerable<DataMember> allDynamicMembers4 = parentDataShape.SecondaryHierarchy.GetAllDynamicMembers();
			if (!allDynamicMembers3.Any<DataMember>() && !allDynamicMembers4.Any<DataMember>())
			{
				this.m_errorContext.Register(TranslationMessages.DataShapeWithContextFilterMustHaveHierarchyMembers(EngineMessageSeverity.Error, ObjectType.DataShape, parentDataShape.Id));
				return;
			}
			if (!this.VerifyMergingFilterContextMembers(allDynamicMembers.Evaluate<DataMember>(), allDynamicMembers3.Evaluate<DataMember>()))
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterDataShapeCannotBeMerged(EngineMessageSeverity.Error, ObjectType.DataHierarchy, "PrimaryHierarchy", "DataShape", parentDataShape.Id, dataShape.Id));
			}
			if (!this.VerifyMergingFilterContextMembers(allDynamicMembers2.Evaluate<DataMember>(), allDynamicMembers4.Evaluate<DataMember>()))
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterDataShapeCannotBeMerged(EngineMessageSeverity.Error, ObjectType.DataHierarchy, "SecondaryHierarchy", "DataShape", parentDataShape.Id, dataShape.Id));
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000105B4 File Offset: 0x0000E7B4
		private bool VerifyMergingFilterContextMembers(IList<DataMember> contextMembers, IList<DataMember> parentDataShapeMembers)
		{
			if (contextMembers.Count < parentDataShapeMembers.Count)
			{
				return false;
			}
			if (contextMembers.Any<DataMember>() && parentDataShapeMembers.Any<DataMember>())
			{
				IList<DataMember> list = parentDataShapeMembers.SkipWhile((DataMember m) => !DataShapeQueryUtils.SameGroupKeys(contextMembers.First<DataMember>(), m, this.m_annotations, this.m_expressionTable)).Evaluate<DataMember>();
				if (!list.Any<DataMember>())
				{
					return false;
				}
				for (int i = 0; i < list.Count; i++)
				{
					if (!DataShapeQueryUtils.SameGroupKeys(list[i], contextMembers[i], this.m_annotations, this.m_expressionTable))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001065C File Offset: 0x0000E85C
		private void CheckDuplicateGroupKeysOnOppositeHierarchies(DataShape dataShape)
		{
			if (dataShape.PrimaryHierarchy != null && dataShape.SecondaryHierarchy != null && this.m_primaryGroupKeyExpressionNodes.Peek().Intersect(this.m_secondaryGroupKeyExpressionNodes.Peek()).Any<ExpressionNode>())
			{
				this.m_errorContext.Register(TranslationMessages.OverlappingKeysOnOppositeHierarchies(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "Value"));
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000106C0 File Offset: 0x0000E8C0
		private void CheckGroupSynchronization(DataShape dataShape)
		{
			bool flag = this.m_parentDataShapes.Count == 1;
			IReadOnlyList<Calculation> readOnlyList = this.m_annotations.GetHierarchySyncCalcs(dataShape, true).ConcatNullable(this.m_annotations.GetHierarchySyncCalcs(dataShape, false)).EvaluateReadOnly<Calculation>();
			IReadOnlyList<DataShape> synchronizationDataShapes = DataShapeContext.Create(dataShape, this.m_annotations, this.m_scopeTree).SynchronizationDataShapes;
			if (flag && !readOnlyList.IsNullOrEmpty<Calculation>())
			{
				HashSet<DataShape> hashSet = new HashSet<DataShape>(ReferenceEqualityComparer<DataShape>.Instance);
				foreach (Calculation calculation in readOnlyList)
				{
					DataShape dataShape2 = this.m_annotations.GetReferencedScopes(calculation).Single("Only a single scope should be referenced in a sync index.", Array.Empty<string>()) as DataShape;
					hashSet.Add(dataShape2);
				}
				int count = synchronizationDataShapes.Count;
				int count2 = hashSet.Count;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001079C File Offset: 0x0000E99C
		private string GetDataShapeId(DataShape arg)
		{
			return arg.Id.Value;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000107A9 File Offset: 0x0000E9A9
		private void CheckDepthOfComplexSlicers(DataShape dataShape)
		{
			if (this.m_annotations.ComplexSlicerExceededMaxDepth(dataShape))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidDeepComplexSlicer(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape.Id, "Filters", 3));
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000107D8 File Offset: 0x0000E9D8
		private void CheckDuplicateFilterTargets(DataShape dataShape)
		{
			if (dataShape.Filters == null)
			{
				return;
			}
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Filter> filters = dataShape.Filters;
			List<IIdentifiable> list = new List<IIdentifiable>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.Filter filter in filters)
			{
				IIdentifiable resolvedTargetStructure = filter.Target.GetResolvedTargetStructure(this.m_expressionTable);
				bool flag6 = filter.Condition.ObjectType == ObjectType.FilterEmptyGroupsCondition;
				bool flag7 = filter.Condition.ObjectType == ObjectType.ContextFilterCondition;
				bool flag8 = filter.Condition.ObjectType == ObjectType.ApplyFilterCondition;
				bool flag9 = filter.Condition.ObjectType == ObjectType.ExistsFilterCondition;
				bool flag10 = filter.Condition.ObjectType == ObjectType.AnyValueFilterCondition && !((AnyValueFilterCondition)filter.Condition).DefaultValueOverridesAncestors;
				bool flag11 = filter.Condition.ObjectType == ObjectType.AnyValueFilterCondition && ((AnyValueFilterCondition)filter.Condition).DefaultValueOverridesAncestors;
				bool flag12 = filter.Condition.ObjectType == ObjectType.DefaultValueFilterCondition;
				bool flag13 = this.m_annotations.IsDataShapeValueFilter(filter);
				if ((flag6 && flag) || (flag9 && flag2) || (flag10 && flag3) || (flag11 && flag4) || (flag12 && flag5) || (list.Contains(resolvedTargetStructure) && !flag6 && !flag7 && !flag8 && !flag9 && !flag13 && !flag10 && !flag11 && !flag12))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidMultipleFiltersSameTarget(EngineMessageSeverity.Error, ObjectType.Filter, resolvedTargetStructure.Id, "Target", resolvedTargetStructure.ObjectType));
				}
				else if (flag6)
				{
					flag = true;
				}
				else if (flag9)
				{
					flag2 = true;
				}
				else if (flag10)
				{
					flag3 = true;
				}
				else if (flag11)
				{
					flag4 = true;
				}
				else if (flag12)
				{
					flag5 = true;
				}
				else if (!flag13 && !flag7 && !flag8)
				{
					list.Add(resolvedTargetStructure);
				}
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000109CC File Offset: 0x0000EBCC
		private bool CheckSubtotalUsage(Calculation calculation, IScope containingScope)
		{
			Calculation calculation2;
			bool flag = this.m_annotations.IsSubtotal(calculation, out calculation2);
			if (flag)
			{
				IScope containingScope2 = this.m_scopeTree.GetContainingScope(calculation2);
				IEnumerable<DataMember> enumerable = this.m_scopeTree.GetAllParentScopes(containingScope2).OfType<DataMember>();
				IEnumerable<DataMember> enumerable2 = this.m_scopeTree.GetAllParentScopes(containingScope).OfType<DataMember>();
				if (!enumerable.Except(enumerable2).Any<DataMember>())
				{
					this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, calculation.ObjectType, calculation.Id, "Value", TranslationMessagePhrases.InvalidSubtotalTarget(calculation2.Id)));
				}
				if (!this.m_annotations.IsMeasure(calculation2) && !this.m_annotations.IsVisualCalculation(calculation2))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, calculation.ObjectType, calculation.Id, "Value", TranslationMessagePhrases.InvalidSubtotalTarget(calculation2.Id)));
				}
			}
			return flag;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		private void CheckCircularReferences(Calculation targetCalc, Calculation currentCalc)
		{
			ExpressionValidationResult expressionValidationResult;
			if (!this.m_calculationValidationResults.TryGetValue(currentCalc.Id, out expressionValidationResult))
			{
				return;
			}
			if (expressionValidationResult.ReferencedCalculations == null)
			{
				return;
			}
			foreach (Calculation calculation in expressionValidationResult.ReferencedCalculations)
			{
				if (calculation.Id == targetCalc.Id)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, targetCalc.ObjectType, targetCalc.Id, "Value", TranslationMessagePhrases.CircularReference()));
				}
				else
				{
					this.CheckCircularReferences(targetCalc, calculation);
				}
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00010B50 File Offset: 0x0000ED50
		protected override void Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Filter filter, Identifier dataShapeId)
		{
			IIdentifiable target = (this.m_expressionTable.GetNode(filter.Target) as ResolvedStructureReferenceExpressionNode).Target;
			bool flag;
			FilterSemanticValidator.Validate(filter, this.m_expressionValidator, this.m_errorContext, dataShapeId, this.ParentDataShape, target, this.m_expressionTable, this.m_scopeTree, this.m_annotations, this.m_schema, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape), this.m_schema.SupportsHierarchicalFilterDisjunction(), out flag);
			ObjectType objectType = filter.Condition.ObjectType;
			if (flag)
			{
				if (this.m_annotations.HasFilterContextDataShape(this.ParentDataShape))
				{
					this.m_errorContext.Register(TranslationMessages.ContextFilterDoesNotAllowScopeFilterPeer(EngineMessageSeverity.Error, ObjectType.Filter, this.ParentDataShape.Id, "DataShape"));
					return;
				}
				this.m_scopeFilterCount++;
				if (this.m_scopeFilterCount > 1)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidMultipleScopeFilters(EngineMessageSeverity.Error, ObjectType.Filter, target.Id, "Target"));
					return;
				}
			}
			else if (objectType == ObjectType.ContextFilterCondition)
			{
				this.m_contextFilterCount++;
				if (!this.m_allowContextFilters)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidNestedFilterCondition(EngineMessageSeverity.Error, ObjectType.Filter, this.ParentDataShape.Id, objectType));
					return;
				}
				if (this.m_contextFilterCount > 1)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidMultipleScopeFilters(EngineMessageSeverity.Error, ObjectType.Filter, this.ParentDataShape.Id, "DataShape"));
					return;
				}
			}
			else if (this.IsContextFilterDataShape && objectType != ObjectType.FilterEmptyGroupsCondition)
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterOnlyAllowsScopeFilterInContextDataShape(EngineMessageSeverity.Error, ObjectType.Filter, this.ParentDataShape.Id, "DataShape"));
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00010CDB File Offset: 0x0000EEDB
		private void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			this.Visit(dataShape);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		protected override void Enter(DataMember dataMember)
		{
			bool flag = this.m_annotations.IsPrimaryMember(dataMember);
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			bool flag2 = this.m_annotations.SubtotalAnnotations.TryGetSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation);
			if (dataMember.Group != null)
			{
				this.ValidateGroupWithKeys(dataMember, flag);
			}
			if (!flag2 && dataMember.SubtotalStartPosition.IsSpecified<bool>())
			{
				this.m_errorContext.Register(TranslationMessages.SubtotalStartPositionOnNonSubtotal(EngineMessageSeverity.Error, ObjectType.DataMember, dataMember.Id, "StartPosition"));
			}
			else if (flag2 && dataMember.SubtotalStartPosition.GetValueOrDefault<bool>())
			{
				DataMember dataMember2 = batchSubtotalAnnotation.StopScope as DataMember;
				if (dataMember2 != null)
				{
					DataMember dataMember3 = this.m_scopeTree.GetParentScope(dataMember2) as DataMember;
					if (dataMember3 != null)
					{
						this.ValidateContainingMembersHaveStartPosition(dataMember3);
					}
				}
			}
			if (!dataMember.InstanceFilters.IsNullOrEmpty<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>())
			{
				HashSet<ExpressionNode> hashSet = (flag ? this.m_primaryGroupKeyExpressionNodes.Peek() : this.m_secondaryGroupKeyExpressionNodes.Peek());
				this.ValidateInstanceFilters(dataMember.InstanceFilters, dataMember.Id, hashSet);
			}
			this.ValidateContextOnly(dataMember);
			this.m_parents.Push(dataMember);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010DE1 File Offset: 0x0000EFE1
		protected override void Exit(DataMember dataMember)
		{
			this.m_parents.Pop();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		private void ValidateContextOnly(DataMember dataMember)
		{
			IIdentifiable identifiable = this.m_parents.Peek();
			if (identifiable is DataShape)
			{
				if (dataMember.IsDynamic && dataMember.ContextOnly)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidContextOnlyDataMemberParentIsDataShape(EngineMessageSeverity.Error, ObjectType.DataMember, dataMember.Id, "ContextOnly"));
					return;
				}
			}
			else
			{
				DataMember dataMember2 = identifiable as DataMember;
				if (dataMember2 != null && !dataMember.ContextOnly && dataMember2.ContextOnly)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidContextOnlyDataMemberParentIsNotContextOnly(EngineMessageSeverity.Error, ObjectType.DataMember, dataMember.Id, "ContextOnly"));
				}
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00010E78 File Offset: 0x0000F078
		private void ValidateInstanceFilters(IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> instanceFilters, Identifier id, HashSet<ExpressionNode> groupExpressionNodes)
		{
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition in instanceFilters)
			{
				foreach (FilterExpressionInfo filterExpressionInfo in FilterExpressionCollector.CollectNonMeasureModelExpressions(filterCondition, id, this.m_expressionTable, this.m_errorContext))
				{
					ExpressionNode node = this.m_expressionTable.GetNode(filterExpressionInfo.Expression);
					if (!groupExpressionNodes.Contains(node))
					{
						this.m_errorContext.Register(TranslationMessages.InvalidInstanceFilters(EngineMessageSeverity.Error, filterExpressionInfo.Context.ObjectType, filterExpressionInfo.Context.ObjectId, filterExpressionInfo.Context.PropertyName, new ScrubbedString(filterExpressionInfo.Expression.OriginalNode.ToString(ExpressionStringBuilderFactory.Create(null, false)))));
					}
				}
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00010F70 File Offset: 0x0000F170
		private void ValidateGroupWithKeys(DataMember dataMember, bool isInPrimaryHierarchy)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			this.ValidateDetailGroupIdentity(group.DetailGroupIdentity, dataMember);
			List<ExpressionNode> list = this.ValidateSortKeysAndScopeIdDefinition(dataMember);
			this.ValidateGroupKeys(dataMember, list);
			if (group.StartPosition != null)
			{
				if (group.SortKeys == null)
				{
					this.m_errorContext.Register(TranslationMessages.StartPositionRequiresSortKeys(EngineMessageSeverity.Error, ObjectType.Group, dataMember.Id, "SortKeys"));
				}
				if (!isInPrimaryHierarchy)
				{
					this.m_errorContext.Register(TranslationMessages.StartPositionInSecondaryHierarchy(EngineMessageSeverity.Error, ObjectType.Group, dataMember.Id, "StartPosition"));
				}
				this.ValidateStartPosition(dataMember);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		private void ValidateDetailGroupIdentity(DetailGroupIdentity detailGroupIdentity, DataMember member)
		{
			if (detailGroupIdentity == null)
			{
				return;
			}
			ExpressionNode node = this.m_expressionTable.GetNode(detailGroupIdentity.Value);
			ResolvedEntitySetExpressionNode resolvedEntitySetExpressionNode = node as ResolvedEntitySetExpressionNode;
			if (resolvedEntitySetExpressionNode == null)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, member.ObjectType, member.Id, "DetailGroupIdentity", TranslationMessagePhrases.InvalidDetailGroupIdentity(node.Kind)));
				return;
			}
			if (this.HasOrIsExtensionEntityWithColumns(resolvedEntitySetExpressionNode.Entity))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, member.ObjectType, member.Id, "DetailGroupIdentity", TranslationMessagePhrases.InvalidDetailGroupIdentityExtensionEntity()));
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00011088 File Offset: 0x0000F288
		private bool HasOrIsExtensionEntityWithColumns(IConceptualEntity entity)
		{
			IExtensionConceptualEntity extensionConceptualEntity = entity as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null)
			{
				return entity.Properties.Any((IConceptualProperty p) => p is IConceptualColumn);
			}
			if (this.m_schema.Schemas.Count == 1)
			{
				return false;
			}
			foreach (IConceptualSchema conceptualSchema in this.m_schema.Schemas)
			{
				if (!(conceptualSchema.SchemaId == entity.Schema.SchemaId))
				{
					IExtensionConceptualSchema extensionConceptualSchema = conceptualSchema as IExtensionConceptualSchema;
					if (extensionConceptualSchema != null)
					{
						IConceptualSchema extendedSchema = extensionConceptualSchema.ExtendedSchema;
						if (((extendedSchema != null) ? extendedSchema.SchemaId : null) == entity.Schema.SchemaId)
						{
							foreach (IConceptualEntity conceptualEntity in conceptualSchema.Entities)
							{
								if (ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(entity, conceptualEntity))
								{
									return conceptualEntity.Properties.Any((IConceptualProperty p) => p is IConceptualColumn);
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000111F4 File Offset: 0x0000F3F4
		private List<ExpressionNode> ValidateSortKeysAndScopeIdDefinition(DataMember dataMember)
		{
			List<ExpressionNode> list = new List<ExpressionNode>();
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			List<SortKey> sortKeys = group.SortKeys;
			bool flag = dataMember.Group.DetailGroupIdentity != null;
			ExpressionFeatureFlags expressionFeatureFlags = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
			{
				ExpressionFeatures.SortExpression,
				flag ? ExpressionFeatureFlags.BinaryOrImageFieldReference : ExpressionFeatureFlags.None
			});
			List<ScopeValueDefinition> list2 = null;
			if (group.ScopeIdDefinition != null)
			{
				list2 = group.ScopeIdDefinition.Values;
			}
			if (sortKeys.IsNullOrEmpty<SortKey>())
			{
				if (group.StartPosition != null && group.StartPosition.Values != null)
				{
					this.m_errorContext.Register(TranslationMessages.StartPositionNoSortKeys(EngineMessageSeverity.Error, ObjectType.Group, dataMember.Id, "StartPosition"));
				}
				return list;
			}
			List<DataMember> list3 = this.m_scopeTree.GetAllParentScopes(dataMember).OfType<DataMember>().Except(new DataMember[] { dataMember })
				.ToList<DataMember>();
			IList<ExpressionNode> list4 = (from s in list3.GetAllSortKeys(this.m_annotations)
				select s.Value into expr
				select this.m_expressionTable.GetNode(expr)).Evaluate<ExpressionNode>();
			IList<ScopeValue> list5 = list3.GetAllStartPositions().SelectMany((ScopeId s) => s.Values).Evaluate<ScopeValue>();
			for (int i = 0; i < sortKeys.Count; i++)
			{
				SortKey sortKey = sortKeys[i];
				this.m_expressionValidator.Validate(sortKey.Value, expressionFeatureFlags, dataMember.ObjectType, dataMember.Id, "Value", dataMember);
				ExpressionNode node = this.m_expressionTable.GetNode(sortKey.Value);
				ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = node as ResolvedPropertyExpressionNode;
				if (resolvedPropertyExpressionNode != null)
				{
					this.ValidateDetailTablePropertyRefOutsideAggregate(dataMember, resolvedPropertyExpressionNode, dataMember, "SortKey");
				}
				if (DataShapeSemanticValidator.IsDuplicateExpressionNode(node, list))
				{
					this.m_errorContext.Register(TranslationMessages.SortKeysDuplicateExpressions(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id.Value, "Value"));
				}
				if (list2 != null)
				{
					ScopeValueDefinition scopeValueDefinition = list2[i];
					this.m_expressionValidator.Validate(scopeValueDefinition.Value, expressionFeatureFlags, dataMember.ObjectType, dataMember.Id, "Value", dataMember);
					ExpressionNode node2 = this.m_expressionTable.GetNode(scopeValueDefinition.Value);
					if (!node.Equals(node2))
					{
						this.m_errorContext.Register(TranslationMessages.WrongOrderOfScopeValueDefinitions(EngineMessageSeverity.Error, ObjectType.Group, dataMember.Id, "ScopeIdDefinition"));
					}
				}
				ScopeValue scopeValue = null;
				if (group.StartPosition != null && group.StartPosition.Values != null && i < group.StartPosition.Values.Count)
				{
					scopeValue = group.StartPosition.Values[i];
				}
				this.ValidateDuplicateSortKeysWithInconsistentStartPositions(dataMember, sortKey, node, scopeValue, list4, list5);
			}
			return list;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000114B0 File Offset: 0x0000F6B0
		private void ValidateDuplicateSortKeysWithInconsistentStartPositions(DataMember dataMember, SortKey sortKey, ExpressionNode sortNode, ScopeValue startPosition, IList<ExpressionNode> parentSortNodes, IList<ScopeValue> parentStartPositions)
		{
			if (startPosition == null)
			{
				return;
			}
			for (int i = 0; i < parentSortNodes.Count; i++)
			{
				if (i < parentStartPositions.Count)
				{
					ExpressionNode expressionNode = parentSortNodes[i];
					if (!parentStartPositions[i].Value.Value.Equals(startPosition.Value.Value) && expressionNode.Equals(sortNode))
					{
						SortByMeasureInfoCollection sortByMeasureInfos = this.m_annotations.DataMemberAnnotations.GetSortByMeasureInfos(dataMember);
						if (sortByMeasureInfos == null || !sortByMeasureInfos.ContainsKey(sortKey))
						{
							this.m_errorContext.Register(TranslationMessages.SortKeysInconsistentStartPosition(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id, "StartPosition"));
						}
					}
				}
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00011564 File Offset: 0x0000F764
		private void ValidateGroupKeys(DataMember dataMember, List<ExpressionNode> resolvedSortingExpressionNodes)
		{
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey> groupKeys = dataMember.Group.GroupKeys;
			IList<DataMember> list = this.m_scopeTree.GetAllParentScopes(dataMember).Except(new IScope[] { dataMember }).OfType<DataMember>()
				.Evaluate<DataMember>();
			bool flag = dataMember.Group.DetailGroupIdentity != null;
			ExpressionFeatureFlags expressionFeatureFlags = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
			{
				ExpressionFeatures.GroupExpression,
				flag ? ExpressionFeatureFlags.BinaryOrImageFieldReference : ExpressionFeatureFlags.None
			});
			List<IConceptualColumn> list2 = new List<IConceptualColumn>(groupKeys.Count);
			HashSet<IConceptualColumn> hashSet = new HashSet<IConceptualColumn>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey in groupKeys)
			{
				this.m_expressionValidator.Validate(groupKey.Value, expressionFeatureFlags, dataMember.ObjectType, dataMember.Id, "Value", dataMember);
				ExpressionNode groupKeyNode = this.m_expressionTable.GetNode(groupKey.Value);
				ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = groupKeyNode as ResolvedPropertyExpressionNode;
				if (resolvedPropertyExpressionNode != null)
				{
					IConceptualColumn conceptualColumn = resolvedPropertyExpressionNode.Property.AsColumn();
					if (conceptualColumn == null || (!flag && !conceptualColumn.CanGroupOnValue()))
					{
						this.m_errorContext.Register(TranslationMessages.InvalidGroupExpression(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id, "Value"));
					}
					else
					{
						this.ValidateDetailTablePropertyRefOutsideAggregate(dataMember, resolvedPropertyExpressionNode, dataMember, "GroupKey");
						list2.Add(conceptualColumn);
						hashSet.UnionWith(conceptualColumn.Grouping.QueryGroupColumns);
					}
				}
				else if (!(groupKeyNode is ResolvedDataTransformTableColumnReferenceExpressionNode) && !(groupKeyNode is ResolvedCalculationReferenceExpressionNode))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidGroupExpression(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id, "Value"));
				}
				bool showItemsWithNoData = groupKey.ShowItemsWithNoData.GetValueOrDefault<bool>();
				if (showItemsWithNoData && this.m_translationOptions.ApplyTransformsInQuery.Value)
				{
					this.m_errorContext.Register(TranslationMessages.ShowAllWithDataTransform(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id, "ShowItemsWithNoData", groupKey.ObjectType));
				}
				Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey, bool> <>9__0;
				foreach (DataMember dataMember2 in list)
				{
					IEnumerable<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey> groupKeys2 = dataMember2.GetGroupKeys(this.m_annotations);
					Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey k) => showItemsWithNoData != k.ShowItemsWithNoData.GetValueOrDefault<bool>() && groupKeyNode.Equals(this.m_expressionTable.GetNode(k.Value)));
					}
					if (groupKeys2.Where(func).Any<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey>())
					{
						this.m_errorContext.Register(TranslationMessages.InconsistentShowItemsWithNoDataValue(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id, "ShowItemsWithNoData", groupKey.ObjectType, dataMember2.ObjectType, dataMember2.Id));
					}
				}
			}
			if ((!this.m_translationOptions.SuppressModelGrouping || this.DaxCapabilitiesAnnotation.EnforcesGroupByValidation) && list2.Count > 0 && !list2.IsSupersetOf(hashSet))
			{
				foreach (IConceptualColumn conceptualColumn2 in list2)
				{
					if (!list2.IsSupersetOf(conceptualColumn2.Grouping.QueryGroupColumns))
					{
						this.m_errorContext.Register(TranslationMessages.ModelGroupingInstructionsIgnored(EngineMessageSeverity.Warning, dataMember.ObjectType, dataMember.Id, "Value", new ScrubbedString(conceptualColumn2.Name)));
					}
				}
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00011914 File Offset: 0x0000FB14
		private void ValidateDetailTablePropertyRefOutsideAggregate(DataMember dataMember, ResolvedPropertyExpressionNode node, IIdentifiable propertyRefSource, string propertyRefSourceName)
		{
			IConceptualProperty property = node.Property;
			IReadOnlyList<IConceptualEntity> validDetailGroupEntities = this.GetValidDetailGroupEntities(dataMember.Group.DetailGroupIdentity);
			IConceptualEntity baseModelEntity = property.Entity.GetBaseModelEntity();
			if (validDetailGroupEntities != null && !validDetailGroupEntities.Contains(baseModelEntity))
			{
				this.m_errorContext.Register(TranslationMessages.IsRelatedToManyNotSupportedForDetailTable(EngineMessageSeverity.Error, propertyRefSource.ObjectType, propertyRefSource.Id, propertyRefSourceName, TranslationMessageUtils.GetPropertyNameForError(node)));
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001197B File Offset: 0x0000FB7B
		private static bool IsDuplicateExpressionNode(ExpressionNode node, List<ExpressionNode> nodesList)
		{
			if (!nodesList.Contains(node))
			{
				nodesList.Add(node);
				return false;
			}
			return true;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00011990 File Offset: 0x0000FB90
		private void ValidateStartPosition(DataMember member)
		{
			List<ScopeValue> values = member.Group.StartPosition.Values;
			if (values == null || values.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.Group, member.Id, "Values"));
			}
			else
			{
				List<SortKey> sortKeys = member.Group.SortKeys;
				if (sortKeys != null && values.Count != sortKeys.Count)
				{
					this.m_errorContext.Register(TranslationMessages.WrongNumberOfStartPositionValues(EngineMessageSeverity.Error, ObjectType.Group, member.Id, "StartPosition"));
				}
				foreach (ScopeValue scopeValue in values)
				{
					scopeValue.Value.ValidateRequiredCandidateValue(this.m_errorContext, ObjectType.ScopeValue, member.Id, "Value");
					this.ValidateScalarValue(scopeValue.Value, ObjectType.ScopeValue, member.Id, "StartPosition");
				}
			}
			this.ValidateContainingMembersHaveStartPosition(member);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00011A90 File Offset: 0x0000FC90
		private void ValidateContainingMembersHaveStartPosition(DataMember member)
		{
			if (!this.m_scopeTree.HasScope(member.Id))
			{
				return;
			}
			this.m_scopeTree.TraverseUp(member, delegate(IScope scope)
			{
				DataMember dataMember = scope as DataMember;
				if (dataMember != null && dataMember.Group.StartPosition == null)
				{
					this.m_errorContext.Register(TranslationMessages.MissingContainingMemberStartPosition(EngineMessageSeverity.Error, ObjectType.DataMember, member.Id, "StartPosition"));
					return false;
				}
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				IList<IIdentifiable> list;
				if (dataMember != member && this.m_annotations.TryGetBatchSubtotalAnnotation(member, out batchSubtotalAnnotation) && this.m_annotations.SubtotalAnnotations.TryGetSubtotalAnnotationSources(batchSubtotalAnnotation, out list))
				{
					foreach (IIdentifiable identifiable in list)
					{
						DataMember dataMember2 = identifiable as DataMember;
						if (dataMember2 != null && !dataMember2.SubtotalStartPosition.IsSpecified<bool>())
						{
							this.m_errorContext.Register(TranslationMessages.MissingContainingMemberStartPosition(EngineMessageSeverity.Error, ObjectType.DataMember, member.Id, "StartPosition"));
							return false;
						}
					}
					return true;
				}
				return true;
			});
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00011AE8 File Offset: 0x0000FCE8
		private void ValidateScalarValue(Candidate<ScalarValue> scalarValue, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (scalarValue == null || !scalarValue.IsValid)
			{
				return;
			}
			if (scalarValue.Value.IsOfType<DateTime>())
			{
				ExpressionNodeSemanticValidator.ValidateDateTimeLiteral((DateTime)scalarValue.Value.Value, this.m_errorContext, objectType, objectId, propertyName, this.DaxCapabilitiesAnnotation);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00011B40 File Offset: 0x0000FD40
		private void ValidateSubtotalInDataShape(DataShape dataShape)
		{
			foreach (DataShape dataShape2 in this.m_dataShapesWithSubtotals)
			{
				if (this.m_scopeTree.IsParentScope(dataShape2, dataShape))
				{
					this.m_errorContext.Register(TranslationMessages.NestedDataShapeWithSubtotal(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape.Id, null, dataShape2.Id));
				}
				else if (this.m_scopeTree.IsParentScope(dataShape, dataShape2))
				{
					this.m_errorContext.Register(TranslationMessages.NestedDataShapeWithSubtotal(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape2.Id, null, dataShape.Id));
				}
			}
			this.m_dataShapesWithSubtotals.Add(this.ParentDataShape);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00011C00 File Offset: 0x0000FE00
		protected override void Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit, DataShape dataShape)
		{
			this.m_limitValidator.ValidateLimit(limit, dataShape);
			if (this.IsContextFilterDataShape)
			{
				this.m_errorContext.Register(TranslationMessages.ContextFilterDataShapeDoesNotAllowLimits(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "DataShape", dataShape.Id));
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00011C3B File Offset: 0x0000FE3B
		protected override void VisitExtensionEntity(ExtensionEntity extensionEntity)
		{
			base.TraverseExtensionEntityContents(extensionEntity);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00011C44 File Offset: 0x0000FE44
		protected override void VisitExtensionColumn(ExtensionColumn extensionColumn)
		{
			this.m_expressionValidator.Validate(extensionColumn.Expression, ExpressionFeatures.ExtensionPropertyExpression, ObjectType.ExtensionColumn, extensionColumn.Name, "Expression", this.ParentDataShape);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00011C75 File Offset: 0x0000FE75
		protected override void VisitExtensionMeasure(ExtensionMeasure extensionMeasure)
		{
			this.m_expressionValidator.Validate(extensionMeasure.Expression, ExpressionFeatures.ExtensionPropertyExpression, ObjectType.ExtensionMeasure, extensionMeasure.Name, "Expression", this.ParentDataShape);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00011CA6 File Offset: 0x0000FEA6
		private DaxCapabilitiesAnnotation DaxCapabilitiesAnnotation
		{
			get
			{
				return this.m_schema.GetDefaultSchemaDaxCapabilitiesAnnotation();
			}
		}

		// Token: 0x0400025D RID: 605
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400025E RID: 606
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400025F RID: 607
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000260 RID: 608
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000261 RID: 609
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x04000262 RID: 610
		private readonly DataShapeQueryTranslationOptions m_translationOptions;

		// Token: 0x04000263 RID: 611
		private readonly ExpressionSemanticValidator m_expressionValidator;

		// Token: 0x04000264 RID: 612
		private readonly LimitSemanticValidator m_limitValidator;

		// Token: 0x04000265 RID: 613
		private readonly Dictionary<Identifier, ExpressionValidationResult> m_calculationValidationResults;

		// Token: 0x04000266 RID: 614
		private readonly HashSet<DataShape> m_dataShapesWithSubtotals;

		// Token: 0x04000267 RID: 615
		private readonly Stack<DataShape> m_parentDataShapes;

		// Token: 0x04000268 RID: 616
		private readonly Dictionary<IConceptualEntity, IReadOnlyList<IConceptualEntity>> m_entitiesRelatedToOneCache;

		// Token: 0x04000269 RID: 617
		private readonly Stack<DataShape> m_contextDataShapes;

		// Token: 0x0400026A RID: 618
		private readonly Stack<IIdentifiable> m_parents;

		// Token: 0x0400026B RID: 619
		private int m_contextFilterCount;

		// Token: 0x0400026C RID: 620
		private int m_scopeFilterCount;

		// Token: 0x0400026D RID: 621
		private bool m_allowContextFilters = true;

		// Token: 0x0400026E RID: 622
		private Stack<HashSet<ExpressionNode>> m_primaryGroupKeyExpressionNodes;

		// Token: 0x0400026F RID: 623
		private Stack<HashSet<ExpressionNode>> m_secondaryGroupKeyExpressionNodes;
	}
}
