using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000C9 RID: 201
	internal sealed class DataShapeValidator : DataShapeVisitor
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		private DataShapeValidator(DataShape dataShape, ScopeTree scopeTree, IdentifierValidator idValidator, TranslationErrorContext errorContext)
		{
			this.m_errorContext = errorContext;
			this.m_idValidator = idValidator;
			this.m_expressionValidator = new ExpressionValidator(this.m_errorContext);
			this.m_scopeTree = scopeTree;
			this.m_dataShapeParents = new Stack<IScope>();
			this.m_ancestorIndependentDataShapes = new Stack<DataShape>();
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001FF53 File Offset: 0x0001E153
		public WritableExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionValidator.ExpressionTable;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001FF60 File Offset: 0x0001E160
		public bool InTopLevelDataShape
		{
			get
			{
				return this.m_dataShapeDepth == 0;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001FF6B File Offset: 0x0001E16B
		public static ReadOnlyExpressionTable Validate(DataShape dataShape, ScopeTree scopeTree, IdentifierValidator idValidator, TranslationErrorContext errorContext)
		{
			DataShapeValidator dataShapeValidator = new DataShapeValidator(dataShape, scopeTree, idValidator, errorContext);
			dataShapeValidator.Visit(dataShape);
			return dataShapeValidator.ExpressionTable.AsReadOnly();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001FF88 File Offset: 0x0001E188
		protected override void Enter(DataShape dataShape)
		{
			this.m_dataShapeDepth++;
			if (this.InTopLevelDataShape || dataShape.IsIndependent)
			{
				this.m_ancestorIndependentDataShapes.Push(dataShape);
			}
			this.ValidateContextOnlyDataShape(dataShape);
			this.ValidateDataShapeParent(dataShape);
			this.m_dataShapeParents.Push(dataShape);
			this.m_idValidator.ValidateId(dataShape);
			this.ValidateDataSourceId(dataShape);
			this.ValidateRequestedPrimaryLeafCount(dataShape);
			this.ValidateContextFilterCondition(dataShape);
			this.ValidateFilterEmptyGroups(dataShape);
			this.ValidateGroupSynchronization(dataShape);
			if (!this.ValidateDataHierarchy(dataShape.PrimaryHierarchy, "PrimaryHierarchy") || !this.ValidateDataHierarchy(dataShape.SecondaryHierarchy, "SecondaryHierarchy") || !this.ValidateDataShapeStructure(dataShape))
			{
				dataShape.PrimaryHierarchy = null;
				dataShape.SecondaryHierarchy = null;
				dataShape.DataRows = null;
			}
			this.ValidateDynamicLimits(dataShape);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00020053 File Offset: 0x0001E253
		protected override void Exit(DataShape dataShape)
		{
			if (this.InTopLevelDataShape || dataShape.IsIndependent)
			{
				this.m_ancestorIndependentDataShapes.Pop();
			}
			this.m_dataShapeParents.Pop();
			this.m_dataShapeDepth--;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002008B File Offset: 0x0001E28B
		protected override void Visit(DataTransform transform)
		{
			this.m_idValidator.ValidateId(transform);
			base.Visit(transform);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000200A0 File Offset: 0x0001E2A0
		protected override void Visit(DataTransformTable table)
		{
			this.m_idValidator.ValidateId(table);
			base.Visit(table);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000200B5 File Offset: 0x0001E2B5
		protected override void Visit(DataTransformTableColumn column)
		{
			this.m_expressionValidator.ValidateRequiredExpression(column.Value, ObjectType.DataTransformTableColumn, column.Id, "Value");
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000200D5 File Offset: 0x0001E2D5
		protected override void Visit(DataTransformParameter param)
		{
			this.m_expressionValidator.ValidateRequiredExpression(param.Value, ObjectType.DataTransformParameter, param.Id, "Value");
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000200F8 File Offset: 0x0001E2F8
		protected override void Visit(ModelParameter modelParameter)
		{
			foreach (Expression expression in modelParameter.Values)
			{
				this.m_expressionValidator.ValidateRequiredExpression(expression, ObjectType.ModelParameter, modelParameter.Name, "Values");
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00020164 File Offset: 0x0001E364
		private void ValidateDynamicLimits(DataShape dataShape)
		{
			DynamicLimits dynamicLimits = dataShape.DynamicLimits;
			if (dynamicLimits == null)
			{
				return;
			}
			dynamicLimits.TargetIntersectionCount.ValidateCandidateValue(this.m_errorContext, ObjectType.DynamicLimits, dataShape.Id, "TargetIntersectionCount");
			bool flag = true;
			int num = 1;
			if (dynamicLimits.Blocks.IsNullOrEmpty<DynamicLimitBlock>())
			{
				this.m_expressionValidator.ValidateExpression(dynamicLimits.IntersectionLimit, ObjectType.DynamicLimits, dataShape.Id, "IntersectionLimit");
				this.ValidateRequiredDynamicLimitRecommendation(dataShape.Id, "Primary", dynamicLimits.Primary);
				this.ValidateRequiredDynamicLimitRecommendation(dataShape.Id, "Secondary", dynamicLimits.Secondary);
				flag = (dynamicLimits.Primary == null || dynamicLimits.Primary.IsMandatoryConstraint) && (dynamicLimits.Secondary == null || dynamicLimits.Secondary.IsMandatoryConstraint);
				DynamicLimitRecommendation primary = dynamicLimits.Primary;
				if (primary != null && primary.IsMandatoryConstraint)
				{
					num *= dynamicLimits.Primary.Max.Value;
				}
				DynamicLimitRecommendation secondary = dynamicLimits.Secondary;
				if (secondary != null && secondary.IsMandatoryConstraint)
				{
					num *= dynamicLimits.Secondary.Max.Value;
				}
			}
			else
			{
				if (dynamicLimits.IntersectionLimit != null)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidDynamicLimitsStructure(EngineMessageSeverity.Error, ObjectType.DynamicLimits, dataShape.Id, "IntersectionLimit"));
				}
				if (dynamicLimits.Primary != null)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidDynamicLimitsStructure(EngineMessageSeverity.Error, ObjectType.DynamicLimits, dataShape.Id, "Primary"));
				}
				if (dynamicLimits.Secondary != null)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidDynamicLimitsStructure(EngineMessageSeverity.Error, ObjectType.DynamicLimits, dataShape.Id, "Secondary"));
				}
				int num2 = 0;
				int count = dynamicLimits.Blocks.Count;
				for (int i = 0; i < count; i++)
				{
					DynamicLimitBlock dynamicLimitBlock = dynamicLimits.Blocks[i];
					if (dynamicLimitBlock.Count == null && i != count - 1)
					{
						this.m_errorContext.Register(TranslationMessages.InvalidUnlimitedBlock(EngineMessageSeverity.Error, ObjectType.DynamicLimits, dataShape.Id, "Blocks"));
						return;
					}
					bool flag2;
					int num3;
					bool flag3;
					this.ValidateDynamicLimitBlock(dynamicLimitBlock, dataShape.Id, out flag2, out num3, out flag3);
					flag = flag && flag3;
					num *= num3;
					if (flag2)
					{
						num2++;
					}
					if (num2 > 1)
					{
						this.m_errorContext.Register(TranslationMessages.MoreThanOnePrimarySecondaryBlock(EngineMessageSeverity.Error, ObjectType.DynamicLimits, dataShape.Id, "Blocks"));
						return;
					}
				}
			}
			if (dynamicLimits.TargetIntersectionCount.IsValid && num > dynamicLimits.TargetIntersectionCount.Value)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidMandatoryLimitCountProduct(EngineMessageSeverity.Error, ObjectType.DynamicLimits, dataShape.Id, "TargetIntersectionCount"));
				return;
			}
			if (flag)
			{
				this.m_errorContext.Register(TranslationMessages.AllMandatoryConstraintsInDynamicLimits(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape.Id, "DynamicLimits"));
				return;
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000203FC File Offset: 0x0001E5FC
		private void ValidateDynamicLimitBlock(DynamicLimitBlock block, Identifier objectId, out bool isPrimarySecondaryBlock, out int mandatoryLimitProduct, out bool blockAllMandatory)
		{
			this.ValidateDynamicLimitRecommendation(objectId, block.Count);
			mandatoryLimitProduct = 1;
			isPrimarySecondaryBlock = false;
			DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = block as DynamicLimitEvenDistributionBlock;
			if (dynamicLimitEvenDistributionBlock != null)
			{
				this.ValidateDynamicLimitEvenDistributionBlock(dynamicLimitEvenDistributionBlock, objectId, out mandatoryLimitProduct, out blockAllMandatory);
				return;
			}
			DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = block as DynamicLimitPrimarySecondaryBlock;
			if (dynamicLimitPrimarySecondaryBlock == null)
			{
				blockAllMandatory = false;
				this.m_errorContext.Register(TranslationMessages.InvalidDynamicLimitBlockType(EngineMessageSeverity.Error, ObjectType.DynamicLimits, objectId, "Blocks", block.GetType().Name));
				return;
			}
			isPrimarySecondaryBlock = true;
			this.ValidateDynamicLimitPrimarySecondaryBlock(dynamicLimitPrimarySecondaryBlock, objectId, out mandatoryLimitProduct, out blockAllMandatory);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00020478 File Offset: 0x0001E678
		private void ValidateDynamicLimitEvenDistributionBlock(DynamicLimitEvenDistributionBlock block, Identifier objectId, out int mandatoryLimitProduct, out bool blockAllMandatory)
		{
			mandatoryLimitProduct = 1;
			if (block.Limits.IsNullOrEmpty<DynamicLimit>())
			{
				blockAllMandatory = false;
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.DynamicLimitEvenDistributionBlock, objectId, "Limits"));
				return;
			}
			blockAllMandatory = true;
			int num = 0;
			foreach (DynamicLimit dynamicLimit in block.Limits)
			{
				this.ValidateDynamicLimit(dynamicLimit, ObjectType.DynamicLimitEvenDistributionBlock, objectId, "Limits", ref mandatoryLimitProduct, ref num);
			}
			blockAllMandatory = num == block.Limits.Count;
			if (block.Count != null && mandatoryLimitProduct > block.Count.Max.Value)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidMandatoryLimitCountProductInBlock(EngineMessageSeverity.Error, ObjectType.DynamicLimitEvenDistributionBlock, objectId, "Blocks"));
				return;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00020554 File Offset: 0x0001E754
		private void ValidateDynamicLimitPrimarySecondaryBlock(DynamicLimitPrimarySecondaryBlock block, Identifier objectId, out int mandatoryLimitProduct, out bool blockAllMandatory)
		{
			int num = 0;
			mandatoryLimitProduct = 1;
			this.ValidateDynamicLimit(block.Primary, ObjectType.DynamicLimitPrimarySecondaryBlock, objectId, "Primary", ref mandatoryLimitProduct, ref num);
			this.ValidateDynamicLimit(block.Secondary, ObjectType.DynamicLimitPrimarySecondaryBlock, objectId, "Secondary", ref mandatoryLimitProduct, ref num);
			blockAllMandatory = num == 2;
			if (block.Count != null && mandatoryLimitProduct > block.Count.Max.Value)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidMandatoryLimitCountProductInBlock(EngineMessageSeverity.Error, ObjectType.DynamicLimitPrimarySecondaryBlock, objectId, "Blocks"));
				return;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000205D4 File Offset: 0x0001E7D4
		private void ValidateDynamicLimit(DynamicLimit limit, ObjectType objectType, Identifier objectId, string propertyName, ref int mandatoryLimitProduct, ref int numMandatoryLimits)
		{
			if (limit == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, objectType, objectId, propertyName));
				return;
			}
			DynamicLimitRecommendation count = limit.Count;
			if (count != null && count.IsMandatoryConstraint)
			{
				mandatoryLimitProduct *= limit.Count.Max.Value;
				numMandatoryLimits++;
			}
			this.m_expressionValidator.ValidateRequiredExpression(limit.LimitRef, ObjectType.DynamicLimit, objectId, "LimitRef");
			this.ValidateRequiredDynamicLimitRecommendation(objectId, "Count", limit.Count);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00020658 File Offset: 0x0001E858
		private void ValidateRequiredDynamicLimitRecommendation(Identifier objectId, string propertyName, DynamicLimitRecommendation recommendation)
		{
			if (recommendation == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.DynamicLimits, objectId, propertyName));
				return;
			}
			this.ValidateDynamicLimitRecommendation(objectId, recommendation);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002067B File Offset: 0x0001E87B
		private void ValidateDynamicLimitRecommendation(Identifier objectId, DynamicLimitRecommendation recommendation)
		{
			if (recommendation == null)
			{
				return;
			}
			recommendation.Min.ValidateRequiredCandidateValue(this.m_errorContext, ObjectType.DynamicLimits, objectId, "Min");
			recommendation.Max.ValidateRequiredCandidateValue(this.m_errorContext, ObjectType.DynamicLimits, objectId, "Max");
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000206B4 File Offset: 0x0001E8B4
		private void ValidateDataShapeParent(DataShape dataShape)
		{
			if (this.m_dataShapeDepth > 0)
			{
				IScope scope = this.m_dataShapeParents.Peek();
				if (scope.ObjectType != ObjectType.DataIntersection && scope.ObjectType != ObjectType.DataShape)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidParent(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, null, scope.ObjectType, scope.Id));
				}
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00020714 File Offset: 0x0001E914
		protected override void TraverseDataShapeStructure(DataShape dataShape)
		{
			bool inPrimaryHierarchy = this.m_inPrimaryHierarchy;
			this.m_inPrimaryHierarchy = true;
			base.Visit(dataShape.PrimaryHierarchy);
			this.m_inPrimaryHierarchy = false;
			base.Visit(dataShape.SecondaryHierarchy);
			this.m_inPrimaryHierarchy = inPrimaryHierarchy;
			base.Visit<DataRow>(dataShape.DataRows, new Action<DataRow>(base.Visit));
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002076D File Offset: 0x0001E96D
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			FilterValidator.Validate(filter, dataShapeId, this.m_errorContext, this.m_idValidator, this.m_expressionValidator, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape));
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00020794 File Offset: 0x0001E994
		protected override void Visit(VisualAxis visualAxis, Identifier dataShapeId)
		{
			VisualAxisValidator.Validate(visualAxis, dataShapeId, this.m_expressionValidator);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000207A3 File Offset: 0x0001E9A3
		private void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			this.Visit(dataShape);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000207AC File Offset: 0x0001E9AC
		protected override void Visit(Limit limit, DataShape dataShape)
		{
			LimitValidator.Validate(limit, dataShape.Id, this.m_errorContext, this.m_idValidator, this.m_expressionValidator);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000207CC File Offset: 0x0001E9CC
		private bool ValidateDataShapeStructure(DataShape dataShape)
		{
			bool flag = dataShape.DataRows == null && dataShape.PrimaryHierarchy != null && dataShape.SecondaryHierarchy != null;
			if ((dataShape.DataRows != null && (dataShape.PrimaryHierarchy == null || dataShape.SecondaryHierarchy == null)) || flag)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidDataShapeStructure(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "DataRows"));
				return false;
			}
			if (dataShape.DataRows != null && !this.ValidateIntersectionGrid(dataShape))
			{
				return false;
			}
			if (DataShapeValidator.HasPeerGroups(dataShape.PrimaryHierarchy) || DataShapeValidator.HasPeerGroups(dataShape.SecondaryHierarchy))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidPeerGroups(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "DataMembers"));
				return false;
			}
			return true;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002088F File Offset: 0x0001EA8F
		private static bool HasPeerDataShapes(List<DataShape> dataShapes)
		{
			return dataShapes != null && dataShapes.Count > 1;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000208A0 File Offset: 0x0001EAA0
		private static bool HasPeerGroups(DataHierarchy hierarchy)
		{
			bool flag;
			return hierarchy != null && DataShapeValidator.HasPeerGroups(hierarchy.DataMembers, out flag);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000208C0 File Offset: 0x0001EAC0
		private static bool HasPeerGroups(List<DataMember> members, out bool hasAnyGroup)
		{
			hasAnyGroup = false;
			if (members != null)
			{
				foreach (DataMember dataMember in members)
				{
					bool flag = false;
					if (DataShapeValidator.HasPeerGroups(dataMember.DataMembers, out flag))
					{
						hasAnyGroup = true;
						return true;
					}
					if (dataMember.IsDynamic || flag)
					{
						if (hasAnyGroup)
						{
							return true;
						}
						hasAnyGroup = true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00020940 File Offset: 0x0001EB40
		private bool ValidateIntersectionGrid(DataShape dataShape)
		{
			int num = dataShape.PrimaryHierarchy.DataMembers.ComputeLeafCount();
			int num2 = dataShape.SecondaryHierarchy.DataMembers.ComputeLeafCount();
			if (dataShape.DataRows.Count != num)
			{
				this.m_errorContext.Register(TranslationMessages.WrongNumberOfDataRows(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "DataRows"));
				return false;
			}
			bool flag = true;
			for (int i = 0; i < dataShape.DataRows.Count; i++)
			{
				DataRow dataRow = dataShape.DataRows[i];
				if (dataRow == null || dataRow.Intersections == null || dataRow.Intersections.Count != num2)
				{
					this.m_errorContext.Register(TranslationMessages.WrongNumberOfIntersections(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "DataIntersections", i));
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00020A0E File Offset: 0x0001EC0E
		protected override void Enter(DataMember dataMember)
		{
			this.m_dataShapeParents.Push(dataMember);
			this.m_idValidator.ValidateId(dataMember);
			this.ValidateDataMemberStructure(dataMember);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00020A2F File Offset: 0x0001EC2F
		protected override void Exit(DataMember dataMember)
		{
			this.m_dataShapeParents.Pop();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00020A40 File Offset: 0x0001EC40
		private void ValidateDataMemberStructure(DataMember member)
		{
			this.ValidateGroup(member);
			if (DataShapeValidator.HasPeerDataShapes(member.DataShapes))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidPeerDataShapes(EngineMessageSeverity.Error, member.ObjectType, member.Id, "DataShapes"));
			}
			this.Visit(member.InstanceFilters, member);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00020A90 File Offset: 0x0001EC90
		private void Visit(List<FilterCondition> instanceFilters, DataMember dataMember)
		{
			if (instanceFilters == null)
			{
				return;
			}
			foreach (FilterCondition filterCondition in instanceFilters)
			{
				FilterValidator.Validate(filterCondition, dataMember.Id, this.m_errorContext, this.m_idValidator, this.m_expressionValidator, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape));
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00020B04 File Offset: 0x0001ED04
		private void ValidateGroup(DataMember member)
		{
			Group group = member.Group;
			if (group == null)
			{
				return;
			}
			this.ValidateGroupWithKeys(member, group);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00020B24 File Offset: 0x0001ED24
		private void ValidateGroupWithKeys(DataMember member, Group group)
		{
			this.ValidateGroupKeys(member, group);
			this.ValidateSortKeys(member, group);
			ScopeIdDefinition scopeIdDefinition = group.ScopeIdDefinition;
			if (scopeIdDefinition != null)
			{
				this.ValidateScopeIdDefinition(member, scopeIdDefinition);
			}
			this.ValidateDetailGroupIdentity(member, group);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00020B5C File Offset: 0x0001ED5C
		private void ValidateGroupKeys(DataMember member, Group group)
		{
			List<GroupKey> groupKeys = group.GroupKeys;
			if (groupKeys == null || groupKeys.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrEmptyGroupKeys(EngineMessageSeverity.Error, ObjectType.Group, member.Id, "GroupKeys"));
				return;
			}
			foreach (GroupKey groupKey in groupKeys)
			{
				this.m_idValidator.ValidateOptionalId(groupKey);
				this.m_expressionValidator.ValidateRequiredExpression(groupKey.Value, ObjectType.GroupKey, member.Id, "Value");
				this.m_expressionValidator.ValidateCandidateValue<bool>(groupKey.ShowItemsWithNoData, ObjectType.GroupKey, member.Id, "ShowItemsWithNoData");
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00020C1C File Offset: 0x0001EE1C
		private void ValidateSortKeys(DataMember member, Group group)
		{
			List<SortKey> sortKeys = group.SortKeys;
			if (sortKeys != null)
			{
				foreach (SortKey sortKey in sortKeys)
				{
					this.m_idValidator.ValidateOptionalId(sortKey);
					this.m_expressionValidator.ValidateRequiredExpression(sortKey.Value, ObjectType.SortKey, member.Id, "Value");
					this.m_expressionValidator.ValidateRequiredCandidateValue<SortDirection>(sortKey.SortDirection, ObjectType.SortKey, member.Id, "SortDirection");
				}
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		private void ValidateScopeIdDefinition(DataMember member, ScopeIdDefinition scopeIdDefinition)
		{
			List<ScopeValueDefinition> values = scopeIdDefinition.Values;
			if (values == null || values.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.Group, member.Id, "Values"));
				return;
			}
			List<SortKey> sortKeys = member.Group.SortKeys;
			if (sortKeys == null || sortKeys.Count != values.Count)
			{
				this.m_errorContext.Register(TranslationMessages.WrongNumberOfScopeValueDefinitions(EngineMessageSeverity.Error, ObjectType.Group, member.Id.Value, "ScopeIdDefinition"));
			}
			foreach (ScopeValueDefinition scopeValueDefinition in values)
			{
				this.m_idValidator.ValidateOptionalId(scopeValueDefinition);
				this.m_expressionValidator.ValidateRequiredExpression(scopeValueDefinition.Value, ObjectType.ScopeValueDefinition, member.Id, "Value");
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		private void ValidateDetailGroupIdentity(DataMember member, Group group)
		{
			if (group.DetailGroupIdentity == null)
			{
				return;
			}
			this.m_idValidator.ValidateOptionalId(group.DetailGroupIdentity);
			this.m_expressionValidator.ValidateRequiredExpression(group.DetailGroupIdentity.Value, ObjectType.DetailGroupIdentity, member.Id, "Value");
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00020DE0 File Offset: 0x0001EFE0
		protected override void Enter(DataIntersection dataIntersection)
		{
			this.m_dataShapeParents.Push(dataIntersection);
			this.m_idValidator.ValidateId(dataIntersection);
			if (DataShapeValidator.HasPeerDataShapes(dataIntersection.DataShapes))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidPeerDataShapes(EngineMessageSeverity.Error, dataIntersection.ObjectType, dataIntersection.Id, "DataShapes"));
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00020E34 File Offset: 0x0001F034
		protected override void Exit(DataIntersection dataIntersection)
		{
			this.m_dataShapeParents.Pop();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00020E44 File Offset: 0x0001F044
		protected override void Visit(Calculation calculation)
		{
			this.m_idValidator.ValidateId(calculation);
			this.m_expressionValidator.ValidateRequiredExpression(calculation.Value, calculation.ObjectType, calculation.Id, "Value");
			this.m_expressionValidator.ValidateCandidateValue<bool>(calculation.SuppressJoinPredicate, calculation.ObjectType, calculation.Id, "SuppressJoinPredicate");
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		private void ValidateDataSourceId(DataShape dataShape)
		{
			if ((this.InTopLevelDataShape && dataShape.DataSourceId == null) || (!this.InTopLevelDataShape && dataShape.DataSourceId != null))
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "DataSourceId"));
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00020F00 File Offset: 0x0001F100
		private void ValidateRequestedPrimaryLeafCount(DataShape dataShape)
		{
			Candidate<int> requestedPrimaryLeafCount = dataShape.RequestedPrimaryLeafCount;
			if (requestedPrimaryLeafCount == null)
			{
				return;
			}
			this.m_expressionValidator.ValidateCandidateValue<int>(requestedPrimaryLeafCount, dataShape.ObjectType, dataShape.Id, "RequestedPrimaryLeafCount");
			if (requestedPrimaryLeafCount.IsValid && requestedPrimaryLeafCount.Value <= 0)
			{
				this.m_errorContext.Register(TranslationMessages.PositiveIntegerValueRequired(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "RequestedPrimaryLeafCount"));
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00020F70 File Offset: 0x0001F170
		private void ValidateFilterEmptyGroups(DataShape dataShape)
		{
			if (this.m_dataShapeDepth > 0 && !dataShape.ContextOnly.GetValueOrDefault<bool>() && dataShape.Usage != DataShapeUsage.Synchronization && dataShape.HasFilterEmptyGroups() != this.m_ancestorIndependentDataShapes.Peek().HasFilterEmptyGroups())
			{
				this.m_errorContext.Register(TranslationMessages.InconsistentFilterEmptyGroups(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "FilterEmptyGroups"));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00020FD6 File Offset: 0x0001F1D6
		private void ValidateGroupSynchronization(DataShape dataShape)
		{
			DataShapeUsage usage = dataShape.Usage;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		private void ValidateContextFilterCondition(DataShape dataShape)
		{
			if (dataShape.Filters != null)
			{
				if (dataShape.Filters.Count((Filter f) => f.Condition != null && f.Condition.ObjectType == ObjectType.ContextFilterCondition) > 1)
				{
					this.m_errorContext.Register(TranslationMessages.WrongNumberOfFiltersWithContextFilterCondition(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "ContextFilterCondition"));
				}
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00021048 File Offset: 0x0001F248
		private void ValidateContextOnlyDataShape(DataShape dataShape)
		{
			if (this.InTopLevelDataShape && dataShape.ContextOnly.IsSpecified<bool>() && dataShape.ContextOnly.Value)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidContextOnlyDataShape(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "ContextOnly"));
			}
			this.m_expressionValidator.ValidateCandidateValue<bool>(dataShape.ContextOnly, dataShape.ObjectType, dataShape.Id, "ContextOnly");
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000210BB File Offset: 0x0001F2BB
		private bool ValidateDataHierarchy(DataHierarchy hierarchy, string hierarchyType)
		{
			if (hierarchy != null && (hierarchy.DataMembers == null || hierarchy.DataMembers.Count == 0))
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.DataHierarchy, hierarchyType, "DataMembers"));
				return false;
			}
			return true;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000210F6 File Offset: 0x0001F2F6
		protected override void VisitExtensionEntity(ExtensionEntity extensionEntity)
		{
			base.TraverseExtensionEntityContents(extensionEntity);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000210FF File Offset: 0x0001F2FF
		protected override void VisitExtensionColumn(ExtensionColumn extensionColumn)
		{
			this.m_expressionValidator.ValidateExpression(extensionColumn.Expression, ObjectType.ExtensionColumn, extensionColumn.Name, "Expression");
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00021124 File Offset: 0x0001F324
		protected override void VisitExtensionMeasure(ExtensionMeasure extensionMeasure)
		{
			this.m_expressionValidator.ValidateExpression(extensionMeasure.Expression, ObjectType.ExtensionMeasure, extensionMeasure.Name, "Expression");
		}

		// Token: 0x0400041F RID: 1055
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000420 RID: 1056
		private readonly IdentifierValidator m_idValidator;

		// Token: 0x04000421 RID: 1057
		private readonly ExpressionValidator m_expressionValidator;

		// Token: 0x04000422 RID: 1058
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000423 RID: 1059
		private readonly Stack<IScope> m_dataShapeParents;

		// Token: 0x04000424 RID: 1060
		private readonly Stack<DataShape> m_ancestorIndependentDataShapes;

		// Token: 0x04000425 RID: 1061
		private bool m_inPrimaryHierarchy;

		// Token: 0x04000426 RID: 1062
		private int m_dataShapeDepth = -1;
	}
}
