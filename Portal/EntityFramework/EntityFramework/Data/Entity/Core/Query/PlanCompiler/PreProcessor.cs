using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200035A RID: 858
	internal class PreProcessor : SubqueryTrackingVisitor
	{
		// Token: 0x0600297C RID: 10620 RVA: 0x00085034 File Offset: 0x00083234
		private PreProcessor(PlanCompiler planCompilerState)
			: base(planCompilerState)
		{
			this.m_relPropertyHelper = new RelPropertyHelper(base.m_command.MetadataWorkspace, base.m_command.ReferencedRelProperties);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000850CC File Offset: 0x000832CC
		internal static void Process(PlanCompiler planCompilerState, out StructuredTypeInfo typeInfo, out Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			PreProcessor preProcessor = new PreProcessor(planCompilerState);
			preProcessor.Process(out tvfResultKeys);
			StructuredTypeInfo.Process(planCompilerState.Command, preProcessor.m_referencedTypes, preProcessor.m_referencedEntitySets, preProcessor.m_freeFloatingEntityConstructorTypes, preProcessor.m_suppressDiscriminatorMaps ? null : preProcessor.m_discriminatorMaps, preProcessor.m_relPropertyHelper, preProcessor.m_typesNeedingNullSentinel, out typeInfo);
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x00085124 File Offset: 0x00083324
		internal void Process(out Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			base.m_command.Root = base.VisitNode(base.m_command.Root);
			foreach (Var var in base.m_command.Vars)
			{
				this.AddTypeReference(var.Type);
			}
			if (this.m_referencedTypes.Count > 0)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NTE);
				((PhysicalProjectOp)base.m_command.Root.Op).ColumnMap.Accept<HashSet<string>>(StructuredTypeNullabilityAnalyzer.Instance, this.m_typesNeedingNullSentinel);
			}
			tvfResultKeys = this.m_tvfResultKeys;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000851E4 File Offset: 0x000833E4
		private void AddEntitySetReference(EntitySet entitySet)
		{
			this.m_referencedEntitySets.Add(entitySet);
			if (!this.m_referencedEntityContainers.Contains(entitySet.EntityContainer))
			{
				this.m_referencedEntityContainers.Add(entitySet.EntityContainer);
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00085218 File Offset: 0x00083418
		private void AddTypeReference(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type) || TypeUtils.IsCollectionType(type) || TypeUtils.IsEnumerationType(type))
			{
				this.m_referencedTypes.Add(type);
			}
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x00085240 File Offset: 0x00083440
		private List<RelationshipSet> GetRelationshipSets(RelationshipType relType)
		{
			List<RelationshipSet> list = new List<RelationshipSet>();
			foreach (EntityContainer entityContainer in this.m_referencedEntityContainers)
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					RelationshipSet relationshipSet = entitySetBase as RelationshipSet;
					if (relationshipSet != null && relationshipSet.ElementType.Equals(relType))
					{
						list.Add(relationshipSet);
					}
				}
			}
			return list;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000852EC File Offset: 0x000834EC
		private List<EntitySet> GetEntitySets(TypeUsage entityType)
		{
			List<EntitySet> list = new List<EntitySet>();
			foreach (EntityContainer entityContainer in this.m_referencedEntityContainers)
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					EntitySet entitySet = entitySetBase as EntitySet;
					if (entitySet != null && (entitySet.ElementType.Equals(entityType.EdmType) || TypeSemantics.IsSubTypeOf(entityType.EdmType, entitySet.ElementType) || TypeSemantics.IsSubTypeOf(entitySet.ElementType, entityType.EdmType)))
					{
						list.Add(entitySet);
					}
				}
			}
			return list;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000853C4 File Offset: 0x000835C4
		private Node ExpandView(ScanTableOp scanTableOp, ref IsOfOp typeFilter)
		{
			EntitySetBase extent = scanTableOp.Table.TableMetadata.Extent;
			PlanCompiler.Assert(extent != null, "The target of a ScanTableOp must reference an EntitySet to be used with ExpandView");
			PlanCompiler.Assert(extent.EntityContainer.DataSpace == DataSpace.CSpace, "Store entity sets cannot have Query Mapping Views and should not be used with ExpandView");
			if (typeFilter != null && !typeFilter.IsOfOnly && TypeSemantics.IsSubTypeOf(extent.ElementType, typeFilter.IsOfType.EdmType))
			{
				typeFilter = null;
			}
			GeneratedView generatedView = null;
			EntityTypeBase entityTypeBase = scanTableOp.Table.TableMetadata.Extent.ElementType;
			bool flag = true;
			if (typeFilter != null)
			{
				entityTypeBase = (EntityTypeBase)typeFilter.IsOfType.EdmType;
				flag = !typeFilter.IsOfOnly;
				if (base.m_command.MetadataWorkspace.TryGetGeneratedViewOfType(extent, entityTypeBase, flag, out generatedView))
				{
					typeFilter = null;
				}
			}
			if (generatedView == null)
			{
				generatedView = base.m_command.MetadataWorkspace.GetGeneratedView(extent);
			}
			PlanCompiler.Assert(generatedView != null, Strings.ADP_NoQueryMappingView(extent.EntityContainer.Name, extent.Name));
			Node internalTree = generatedView.GetInternalTree(base.m_command);
			this.DetermineDiscriminatorMapUsage(internalTree, extent, entityTypeBase, flag);
			ScanViewOp scanViewOp = base.m_command.CreateScanViewOp(scanTableOp.Table);
			return base.m_command.CreateNode(scanViewOp, internalTree);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000854FC File Offset: 0x000836FC
		private void DetermineDiscriminatorMapUsage(Node viewNode, EntitySetBase entitySet, EntityTypeBase rootEntityType, bool includeSubtypes)
		{
			ExplicitDiscriminatorMap explicitDiscriminatorMap = null;
			if (viewNode.Op.OpType == OpType.Project)
			{
				DiscriminatedNewEntityOp discriminatedNewEntityOp = viewNode.Child1.Child0.Child0.Op as DiscriminatedNewEntityOp;
				if (discriminatedNewEntityOp != null)
				{
					explicitDiscriminatorMap = discriminatedNewEntityOp.DiscriminatorMap;
				}
			}
			DiscriminatorMapInfo discriminatorMapInfo;
			if (!this.m_discriminatorMaps.TryGetValue(entitySet, out discriminatorMapInfo))
			{
				if (rootEntityType == null)
				{
					rootEntityType = entitySet.ElementType;
					includeSubtypes = true;
				}
				discriminatorMapInfo = new DiscriminatorMapInfo(rootEntityType, includeSubtypes, explicitDiscriminatorMap);
				this.m_discriminatorMaps.Add(entitySet, discriminatorMapInfo);
				return;
			}
			discriminatorMapInfo.Merge(rootEntityType, includeSubtypes, explicitDiscriminatorMap);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00085580 File Offset: 0x00083780
		private Node RewriteNavigateOp(Node navigateOpNode, NavigateOp navigateOp, out Var outputVar)
		{
			outputVar = null;
			if (!Helper.IsAssociationType(navigateOp.Relationship))
			{
				throw new NotSupportedException(Strings.Cqt_RelNav_NoCompositions);
			}
			if (navigateOpNode.Child0.Op.OpType == OpType.GetEntityRef && (navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.One))
			{
				bool flag = base.m_command.IsRelPropertyReferenced(navigateOp.RelProperty);
				string text = "Unreferenced rel property? ";
				RelProperty relProperty = navigateOp.RelProperty;
				PlanCompiler.Assert(flag, text + ((relProperty != null) ? relProperty.ToString() : null));
				Op op = base.m_command.CreateRelPropertyOp(navigateOp.RelProperty);
				return base.m_command.CreateNode(op, navigateOpNode.Child0.Child0);
			}
			List<RelationshipSet> relationshipSets = this.GetRelationshipSets(navigateOp.Relationship);
			if (relationshipSets.Count != 0)
			{
				List<Node> list = new List<Node>();
				List<Var> list2 = new List<Var>();
				foreach (RelationshipSet relationshipSet in relationshipSets)
				{
					TableMD tableMD = Command.CreateTableDefinition(relationshipSet);
					ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(tableMD);
					Node node = base.m_command.CreateNode(scanTableOp);
					Var var = scanTableOp.Table.Columns[0];
					list2.Add(var);
					list.Add(node);
				}
				Node node2 = null;
				Var var2;
				base.m_command.BuildUnionAllLadder(list, list2, out node2, out var2);
				Node node3 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(navigateOp.ToEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var2)));
				Node node4 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(navigateOp.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var2)));
				Node node5 = base.m_command.BuildComparison(OpType.EQ, navigateOpNode.Child0, node4, true);
				Node node6 = base.m_command.CreateNode(base.m_command.CreateFilterOp(), node2, node5);
				Var var3;
				Node node7 = base.m_command.BuildProject(node6, node3, out var3);
				Node node8;
				if (navigateOp.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					node8 = base.m_command.BuildCollect(node7, var3);
				}
				else
				{
					node8 = node7;
					outputVar = var3;
				}
				return node8;
			}
			if (navigateOp.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				return base.m_command.CreateNode(base.m_command.CreateNullOp(navigateOp.Type));
			}
			return base.m_command.CreateNode(base.m_command.CreateNewMultisetOp(navigateOp.Type));
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x00085814 File Offset: 0x00083A14
		private Node BuildOfTypeTable(EntitySetBase entitySet, TypeUsage ofType, out Var resultVar)
		{
			TableMD tableMD = Command.CreateTableDefinition(entitySet);
			ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(tableMD);
			Node node = base.m_command.CreateNode(scanTableOp);
			Var var = scanTableOp.Table.Columns[0];
			Node node2;
			if (ofType != null && !entitySet.ElementType.EdmEquals(ofType.EdmType))
			{
				base.m_command.BuildOfTypeTree(node, var, ofType, true, out node2, out resultVar);
			}
			else
			{
				node2 = node;
				resultVar = var;
			}
			return node2;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x00085888 File Offset: 0x00083A88
		private Node RewriteDerefOp(Node derefOpNode, DerefOp derefOp, out Var outputVar)
		{
			TypeUsage type = derefOp.Type;
			List<EntitySet> entitySets = this.GetEntitySets(type);
			if (entitySets.Count == 0)
			{
				outputVar = null;
				return base.m_command.CreateNode(base.m_command.CreateNullOp(type));
			}
			List<Node> list = new List<Node>();
			List<Var> list2 = new List<Var>();
			foreach (EntitySet entitySet in entitySets)
			{
				Var var;
				Node node = this.BuildOfTypeTable(entitySet, type, out var);
				list.Add(node);
				list2.Add(var);
			}
			Node node2;
			Var var2;
			base.m_command.BuildUnionAllLadder(list, list2, out node2, out var2);
			Node node3 = base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(derefOpNode.Child0.Op.Type), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var2)));
			Node node4 = base.m_command.BuildComparison(OpType.EQ, derefOpNode.Child0, node3, true);
			Node node5 = base.m_command.CreateNode(base.m_command.CreateFilterOp(), node2, node4);
			outputVar = var2;
			return node5;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000859B4 File Offset: 0x00083BB4
		private static EntitySetBase FindTargetEntitySet(RelationshipSet relationshipSet, RelationshipEndMember targetEnd)
		{
			EntitySetBase entitySetBase = null;
			AssociationSet associationSet = (AssociationSet)relationshipSet;
			entitySetBase = null;
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				if (associationSetEnd.CorrespondingAssociationEndMember.EdmEquals(targetEnd))
				{
					entitySetBase = associationSetEnd.EntitySet;
					break;
				}
			}
			PlanCompiler.Assert(entitySetBase != null, "Could not find entity set for relationship set " + ((relationshipSet != null) ? relationshipSet.ToString() : null) + ";association end " + ((targetEnd != null) ? targetEnd.ToString() : null));
			return entitySetBase;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x00085A54 File Offset: 0x00083C54
		private Node BuildJoinForNavProperty(RelationshipSet relSet, RelationshipEndMember end, out Var rsVar, out Var esVar)
		{
			EntitySetBase entitySetBase = PreProcessor.FindTargetEntitySet(relSet, end);
			Node node = this.BuildOfTypeTable(relSet, null, out rsVar);
			Node node2 = this.BuildOfTypeTable(entitySetBase, TypeHelpers.GetElementTypeUsage(end.TypeUsage), out esVar);
			Node node3 = base.m_command.BuildComparison(OpType.EQ, base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(end.TypeUsage), base.m_command.CreateNode(base.m_command.CreateVarRefOp(esVar))), base.m_command.CreateNode(base.m_command.CreatePropertyOp(end), base.m_command.CreateNode(base.m_command.CreateVarRefOp(rsVar))), true);
			return base.m_command.CreateNode(base.m_command.CreateInnerJoinOp(), node, node2, node3);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x00085B14 File Offset: 0x00083D14
		private Node RewriteManyToOneNavigationProperty(RelProperty relProperty, Node sourceEntityNode, TypeUsage resultType)
		{
			RelPropertyOp relPropertyOp = base.m_command.CreateRelPropertyOp(relProperty);
			Node node = base.m_command.CreateNode(relPropertyOp, sourceEntityNode);
			DerefOp derefOp = base.m_command.CreateDerefOp(resultType);
			return base.m_command.CreateNode(derefOp, node);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x00085B58 File Offset: 0x00083D58
		private Node RewriteOneToManyNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			Var var;
			Node node = this.RewriteFromOneNavigationProperty(relProperty, relationshipSets, sourceRefNode, out var);
			return base.m_command.BuildCollect(node, var);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x00085B80 File Offset: 0x00083D80
		private Node RewriteOneToOneNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			Var var;
			Node node = this.RewriteFromOneNavigationProperty(relProperty, relationshipSets, sourceRefNode, out var);
			node = base.VisitNode(node);
			return base.AddSubqueryToParentRelOp(var, node);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x00085BAC File Offset: 0x00083DAC
		private Node RewriteFromOneNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode, out Var outputVar)
		{
			PlanCompiler.Assert(relationshipSets.Count > 0, "expected at least one relationship set here");
			PlanCompiler.Assert(relProperty.FromEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many, "Expected source end multiplicity to be one. Found 'Many' instead " + ((relProperty != null) ? relProperty.ToString() : null));
			TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(relProperty.ToEnd.TypeUsage);
			List<Node> list = new List<Node>(relationshipSets.Count);
			List<Var> list2 = new List<Var>(relationshipSets.Count);
			foreach (RelationshipSet relationshipSet in relationshipSets)
			{
				EntitySetBase entitySetBase = PreProcessor.FindTargetEntitySet(relationshipSet, relProperty.ToEnd);
				Var var;
				Node node = this.BuildOfTypeTable(entitySetBase, elementTypeUsage, out var);
				list.Add(node);
				list2.Add(var);
			}
			Node node2;
			base.m_command.BuildUnionAllLadder(list, list2, out node2, out outputVar);
			RelProperty relProperty2 = new RelProperty(relProperty.Relationship, relProperty.ToEnd, relProperty.FromEnd);
			bool flag = base.m_command.IsRelPropertyReferenced(relProperty2);
			string text = "Unreferenced rel property? ";
			RelProperty relProperty3 = relProperty2;
			PlanCompiler.Assert(flag, text + ((relProperty3 != null) ? relProperty3.ToString() : null));
			Node node3 = base.m_command.CreateNode(base.m_command.CreateRelPropertyOp(relProperty2), base.m_command.CreateNode(base.m_command.CreateVarRefOp(outputVar)));
			Node node4 = base.m_command.BuildComparison(OpType.EQ, sourceRefNode, node3, true);
			return base.m_command.CreateNode(base.m_command.CreateFilterOp(), node2, node4);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00085D3C File Offset: 0x00083F3C
		private Node RewriteManyToManyNavigationProperty(RelProperty relProperty, List<RelationshipSet> relationshipSets, Node sourceRefNode)
		{
			PlanCompiler.Assert(relationshipSets.Count > 0, "expected at least one relationship set here");
			PlanCompiler.Assert(relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many, "Expected target end multiplicity to be 'many'. Found " + ((relProperty != null) ? relProperty.ToString() : null) + "; multiplicity = " + relProperty.ToEnd.RelationshipMultiplicity.ToString());
			List<Node> list = new List<Node>(relationshipSets.Count);
			List<Var> list2 = new List<Var>(relationshipSets.Count * 2);
			foreach (RelationshipSet relationshipSet in relationshipSets)
			{
				Var var;
				Var var2;
				Node node = this.BuildJoinForNavProperty(relationshipSet, relProperty.ToEnd, out var, out var2);
				list.Add(node);
				list2.Add(var);
				list2.Add(var2);
			}
			Node node2;
			IList<Var> list3;
			base.m_command.BuildUnionAllLadder(list, list2, out node2, out list3);
			Node node3 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(list3[0])));
			Node node4 = base.m_command.BuildComparison(OpType.EQ, sourceRefNode, node3, true);
			Node node5 = base.m_command.CreateNode(base.m_command.CreateFilterOp(), node2, node4);
			Node node6 = base.m_command.BuildProject(node5, new Var[] { list3[1] }, new Node[0]);
			return base.m_command.BuildCollect(node6, list3[1]);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00085EEC File Offset: 0x000840EC
		private Node RewriteNavigationProperty(NavigationProperty navProperty, Node sourceEntityNode, TypeUsage resultType)
		{
			RelProperty relProperty = new RelProperty(navProperty.RelationshipType, navProperty.FromEndMember, navProperty.ToEndMember);
			bool flag = base.m_command.IsRelPropertyReferenced(relProperty) || relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many;
			string text = "Unreferenced rel property? ";
			RelProperty relProperty2 = relProperty;
			PlanCompiler.Assert(flag, text + ((relProperty2 != null) ? relProperty2.ToString() : null));
			if (relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				return this.RewriteManyToOneNavigationProperty(relProperty, sourceEntityNode, resultType);
			}
			List<RelationshipSet> relationshipSets = this.GetRelationshipSets(relProperty.Relationship);
			if (relationshipSets.Count == 0)
			{
				if (relProperty.ToEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					return base.m_command.CreateNode(base.m_command.CreateNewMultisetOp(resultType));
				}
				return base.m_command.CreateNode(base.m_command.CreateNullOp(resultType));
			}
			else
			{
				Node node = base.m_command.CreateNode(base.m_command.CreateGetEntityRefOp(relProperty.FromEnd.TypeUsage), sourceEntityNode);
				if (relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					return this.RewriteOneToOneNavigationProperty(relProperty, relationshipSets, node);
				}
				if (relProperty.FromEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					return this.RewriteManyToManyNavigationProperty(relProperty, relationshipSets, node);
				}
				return this.RewriteOneToManyNavigationProperty(relProperty, relationshipSets, node);
			}
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00086021 File Offset: 0x00084221
		protected override Node VisitScalarOpDefault(ScalarOp op, Node n)
		{
			this.VisitChildren(n);
			this.AddTypeReference(op.Type);
			return n;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00086038 File Offset: 0x00084238
		public override Node Visit(DerefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var;
			Node node = this.RewriteDerefOp(n, op, out var);
			node = base.VisitNode(node);
			if (var != null)
			{
				node = base.AddSubqueryToParentRelOp(var, node);
			}
			return node;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00086070 File Offset: 0x00084270
		public override Node Visit(ElementOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Node child = n.Child0;
			ProjectOp projectOp = (ProjectOp)child.Op;
			PlanCompiler.Assert(projectOp.Outputs.Count == 1, "input to ElementOp has more than one output var?");
			Var first = projectOp.Outputs.First;
			return base.AddSubqueryToParentRelOp(first, child);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000860C3 File Offset: 0x000842C3
		public override Node Visit(ExistsOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
			return base.Visit(op, n);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000860DC File Offset: 0x000842DC
		public override Node Visit(FunctionOp op, Node n)
		{
			if (!op.Function.IsFunctionImport)
			{
				PlanCompiler.Assert(op.Function.EntitySet == null, "Entity type scope is not supported on functions that aren't mapped.");
				if (TypeSemantics.IsCollectionType(op.Type) || PlanCompilerUtil.IsCollectionAggregateFunction(op, n))
				{
					this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NestPullup);
					this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
				}
				return base.Visit(op, n);
			}
			PlanCompiler.Assert(op.Function.IsComposableAttribute, "Cannot process a non-composable function inside query tree composition.");
			FunctionImportMapping functionImportMapping = null;
			if (!base.m_command.MetadataWorkspace.TryGetFunctionImportMapping(op.Function, out functionImportMapping))
			{
				throw new MetadataException(Strings.EntityClient_UnmappedFunctionImport(op.Function.FullName));
			}
			PlanCompiler.Assert(functionImportMapping is FunctionImportMappingComposable, "Composable function import must have corresponding mapping.");
			FunctionImportMappingComposable functionImportMappingComposable = (FunctionImportMappingComposable)functionImportMapping;
			this.VisitChildren(n);
			Node node = functionImportMappingComposable.GetInternalTree(base.m_command, n.Children);
			if (op.Function.EntitySet != null)
			{
				this.m_entityTypeScopes.Push(op.Function.EntitySet);
				this.AddEntitySetReference(op.Function.EntitySet);
				PlanCompiler.Assert(functionImportMappingComposable.TvfKeys != null && functionImportMappingComposable.TvfKeys.Length != 0, "Function imports returning entities must have inferred keys.");
				if (!this.m_tvfResultKeys.ContainsKey(functionImportMappingComposable.TargetFunction))
				{
					this.m_tvfResultKeys.Add(functionImportMappingComposable.TargetFunction, functionImportMappingComposable.TvfKeys);
				}
			}
			node = base.VisitNode(node);
			if (op.Function.EntitySet != null)
			{
				PlanCompiler.Assert(this.m_entityTypeScopes.Pop() == op.Function.EntitySet, "m_entityTypeScopes stack is broken");
			}
			return node;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00086278 File Offset: 0x00084478
		public override Node Visit(CaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			bool flag;
			if (PlanCompilerUtil.IsRowTypeCaseOpWithNullability(op, n, out flag))
			{
				this.m_typesNeedingNullSentinel.Add(op.Type.EdmType.Identity);
			}
			return n;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000862B6 File Offset: 0x000844B6
		public override Node Visit(ConditionalOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.ProcessConditionalOp(op, n);
			return n;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000862CC File Offset: 0x000844CC
		private void ProcessConditionalOp(ConditionalOp op, Node n)
		{
			if ((op.OpType == OpType.IsNull && TypeSemantics.IsRowType(n.Child0.Op.Type)) || TypeSemantics.IsComplexType(n.Child0.Op.Type))
			{
				StructuredTypeNullabilityAnalyzer.MarkAsNeedingNullSentinel(this.m_typesNeedingNullSentinel, n.Child0.Op.Type);
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0008632C File Offset: 0x0008452C
		private static void ValidateNavPropertyOp(PropertyOp op)
		{
			NavigationProperty navigationProperty = (NavigationProperty)op.PropertyInfo;
			TypeUsage typeUsage = navigationProperty.ToEndMember.TypeUsage;
			if (TypeSemantics.IsReferenceType(typeUsage))
			{
				typeUsage = TypeHelpers.GetElementTypeUsage(typeUsage);
			}
			if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				typeUsage = TypeUsage.Create(typeUsage.EdmType.GetCollectionType());
			}
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(typeUsage, op.Type))
			{
				throw new MetadataException(Strings.EntityClient_IncompatibleNavigationPropertyResult(navigationProperty.DeclaringType.FullName, navigationProperty.Name));
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000863AC File Offset: 0x000845AC
		private Node VisitNavPropertyOp(PropertyOp op, Node n)
		{
			PreProcessor.ValidateNavPropertyOp(op);
			if (!PreProcessor.IsNavigationPropertyOverVarRef(n.Child0))
			{
				this.VisitScalarOpDefault(op, n);
			}
			PreProcessor.NavigationPropertyOpInfo navigationPropertyOpInfo = new PreProcessor.NavigationPropertyOpInfo(n, base.FindRelOpAncestor(), base.m_command);
			Node node;
			if (this._navigationPropertyOpRewrites.TryGetValue(navigationPropertyOpInfo, out node))
			{
				return OpCopier.Copy(base.m_command, node);
			}
			navigationPropertyOpInfo.Seal();
			node = this.RewriteNavigationProperty((NavigationProperty)op.PropertyInfo, n.Child0, op.Type);
			node = base.VisitNode(node);
			this._navigationPropertyOpRewrites.Add(navigationPropertyOpInfo, node);
			return node;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00086440 File Offset: 0x00084640
		private static bool IsNavigationPropertyOverVarRef(Node n)
		{
			if (n.Op.OpType != OpType.Property || !Helper.IsNavigationProperty(((PropertyOp)n.Op).PropertyInfo))
			{
				return false;
			}
			Node node = n.Child0;
			if (node.Op.OpType == OpType.SoftCast)
			{
				node = node.Child0;
			}
			return node.Op.OpType == OpType.VarRef;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000864A0 File Offset: 0x000846A0
		public override Node Visit(PropertyOp op, Node n)
		{
			Node node;
			if (Helper.IsNavigationProperty(op.PropertyInfo))
			{
				node = this.VisitNavPropertyOp(op, n);
			}
			else
			{
				node = this.VisitScalarOpDefault(op, n);
			}
			return node;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000864CF File Offset: 0x000846CF
		public override Node Visit(RefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.AddEntitySetReference(op.EntitySet);
			return n;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000864E7 File Offset: 0x000846E7
		public override Node Visit(TreatOp op, Node n)
		{
			n = base.Visit(op, n);
			if (this.CanRewriteTypeTest(op.Type.EdmType, n.Child0.Op.Type.EdmType))
			{
				return n.Child0;
			}
			return n;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00086524 File Offset: 0x00084724
		public override Node Visit(IsOfOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			this.AddTypeReference(op.IsOfType);
			if (this.CanRewriteTypeTest(op.IsOfType.EdmType, n.Child0.Op.Type.EdmType))
			{
				n = this.RewriteIsOfAsIsNull(op, n);
			}
			if (op.IsOfOnly && op.IsOfType.EdmType.Abstract)
			{
				this.m_suppressDiscriminatorMaps = true;
			}
			return n;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0008659C File Offset: 0x0008479C
		private bool CanRewriteTypeTest(EdmType testType, EdmType argumentType)
		{
			if (!testType.EdmEquals(argumentType))
			{
				return false;
			}
			if (testType.BaseType != null)
			{
				return false;
			}
			int num = 0;
			foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf(testType, base.m_command.MetadataWorkspace, true))
			{
				num++;
				if (2 == num)
				{
					break;
				}
			}
			return 1 == num;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00086614 File Offset: 0x00084814
		private Node RewriteIsOfAsIsNull(IsOfOp op, Node n)
		{
			ConditionalOp conditionalOp = base.m_command.CreateConditionalOp(OpType.IsNull);
			Node node = base.m_command.CreateNode(conditionalOp, n.Child0);
			this.ProcessConditionalOp(conditionalOp, node);
			ConditionalOp conditionalOp2 = base.m_command.CreateConditionalOp(OpType.Not);
			Node node2 = base.m_command.CreateNode(conditionalOp2, node);
			ConstantBaseOp constantBaseOp = base.m_command.CreateConstantOp(op.Type, true);
			Node node3 = base.m_command.CreateNode(constantBaseOp);
			NullOp nullOp = base.m_command.CreateNullOp(op.Type);
			Node node4 = base.m_command.CreateNode(nullOp);
			CaseOp caseOp = base.m_command.CreateCaseOp(op.Type);
			Node node5 = base.m_command.CreateNode(caseOp, node2, node3, node4);
			ComparisonOp comparisonOp = base.m_command.CreateComparisonOp(OpType.EQ, false);
			return base.m_command.CreateNode(comparisonOp, node5, node3);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000866F8 File Offset: 0x000848F8
		public override Node Visit(NavigateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var;
			Node node = this.RewriteNavigateOp(n, op, out var);
			node = base.VisitNode(node);
			if (var != null)
			{
				node = base.AddSubqueryToParentRelOp(var, node);
			}
			return node;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0008672E File Offset: 0x0008492E
		private EntitySet GetCurrentEntityTypeScope()
		{
			if (this.m_entityTypeScopes.Count == 0)
			{
				return null;
			}
			return this.m_entityTypeScopes.Peek();
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x0008674C File Offset: 0x0008494C
		private static RelationshipSet FindRelationshipSet(EntitySetBase entitySet, RelProperty relProperty)
		{
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				AssociationSet associationSet = entitySetBase as AssociationSet;
				if (associationSet != null && associationSet.ElementType.EdmEquals(relProperty.Relationship) && associationSet.AssociationSetEnds[relProperty.FromEnd.Identity].EntitySet.EdmEquals(entitySet))
				{
					return associationSet;
				}
			}
			return null;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000867E4 File Offset: 0x000849E4
		private static int FindPosition(EdmType type, EdmMember member)
		{
			int num = 0;
			using (IEnumerator enumerator = TypeHelpers.GetAllStructuralMembers(type).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((EdmMember)enumerator.Current).EdmEquals(member))
					{
						return num;
					}
					num++;
				}
			}
			PlanCompiler.Assert(false, "Could not find property " + ((member != null) ? member.ToString() : null) + " in type " + type.Name);
			return -1;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00086874 File Offset: 0x00084A74
		private Node BuildKeyExpressionForNewEntityOp(Op op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.NewEntity || op.OpType == OpType.DiscriminatedNewEntity, "BuildKeyExpression: Unexpected OpType:" + op.OpType.ToString());
			int num = ((op.OpType == OpType.DiscriminatedNewEntity) ? 1 : 0);
			EntityTypeBase entityTypeBase = (EntityTypeBase)op.Type.EdmType;
			List<Node> list = new List<Node>();
			List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
			foreach (EdmMember edmMember in entityTypeBase.KeyMembers)
			{
				int num2 = PreProcessor.FindPosition(entityTypeBase, edmMember) + num;
				PlanCompiler.Assert(n.Children.Count > num2, "invalid position " + num2.ToString() + "; total count = " + n.Children.Count.ToString());
				list.Add(n.Children[num2]);
				list2.Add(new KeyValuePair<string, TypeUsage>(edmMember.Name, edmMember.TypeUsage));
			}
			TypeUsage typeUsage = TypeHelpers.CreateRowTypeUsage(list2);
			NewRecordOp newRecordOp = base.m_command.CreateNewRecordOp(typeUsage);
			return base.m_command.CreateNode(newRecordOp, list);
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000869C8 File Offset: 0x00084BC8
		private Node BuildRelPropertyExpression(EntitySetBase entitySet, RelProperty relProperty, Node keyExpr)
		{
			keyExpr = OpCopier.Copy(base.m_command, keyExpr);
			RelationshipSet relationshipSet = PreProcessor.FindRelationshipSet(entitySet, relProperty);
			if (relationshipSet == null)
			{
				return base.m_command.CreateNode(base.m_command.CreateNullOp(relProperty.ToEnd.TypeUsage));
			}
			ScanTableOp scanTableOp = base.m_command.CreateScanTableOp(Command.CreateTableDefinition(relationshipSet));
			bool flag = scanTableOp.Table.Columns.Count == 1;
			string text = "Unexpected column count for table:";
			EntitySetBase extent = scanTableOp.Table.TableMetadata.Extent;
			PlanCompiler.Assert(flag, text + ((extent != null) ? extent.ToString() : null) + "=" + scanTableOp.Table.Columns.Count.ToString());
			Var var = scanTableOp.Table.Columns[0];
			Node node = base.m_command.CreateNode(scanTableOp);
			Node node2 = base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.FromEnd), base.m_command.CreateNode(base.m_command.CreateVarRefOp(var)));
			Node node3 = base.m_command.BuildComparison(OpType.EQ, keyExpr, base.m_command.CreateNode(base.m_command.CreateGetRefKeyOp(keyExpr.Op.Type), node2), true);
			Node node4 = base.m_command.CreateNode(base.m_command.CreateFilterOp(), node, node3);
			Node node5 = base.VisitNode(node4);
			node5 = base.AddSubqueryToParentRelOp(var, node5);
			return base.m_command.CreateNode(base.m_command.CreatePropertyOp(relProperty.ToEnd), node5);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00086B58 File Offset: 0x00084D58
		private IEnumerable<Node> BuildAllRelPropertyExpressions(EntitySetBase entitySet, List<RelProperty> relPropertyList, Dictionary<RelProperty, Node> prebuiltExpressions, Node keyExpr)
		{
			foreach (RelProperty relProperty in relPropertyList)
			{
				Node node;
				if (!prebuiltExpressions.TryGetValue(relProperty, out node))
				{
					node = this.BuildRelPropertyExpression(entitySet, relProperty, keyExpr);
				}
				yield return node;
			}
			List<RelProperty>.Enumerator enumerator = default(List<RelProperty>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00086B88 File Offset: 0x00084D88
		public override Node Visit(NewEntityOp op, Node n)
		{
			if (op.Scoped || op.Type.EdmType.BuiltInTypeKind != BuiltInTypeKind.EntityType)
			{
				return base.Visit(op, n);
			}
			EntityType entityType = (EntityType)op.Type.EdmType;
			EntitySet currentEntityTypeScope = this.GetCurrentEntityTypeScope();
			List<RelProperty> list;
			List<Node> list2;
			if (currentEntityTypeScope == null)
			{
				this.m_freeFloatingEntityConstructorTypes.Add(entityType);
				PlanCompiler.Assert(op.RelationshipProperties == null || op.RelationshipProperties.Count == 0, "Related Entities cannot be specified for Entity constructors that are not part of the Query Mapping View for an Entity Set.");
				this.VisitScalarOpDefault(op, n);
				list = op.RelationshipProperties;
				list2 = n.Children;
			}
			else
			{
				list = new List<RelProperty>(this.m_relPropertyHelper.GetRelProperties(entityType));
				int num = op.RelationshipProperties.Count - 1;
				List<RelProperty> list3 = new List<RelProperty>(op.RelationshipProperties);
				int i = n.Children.Count - 1;
				while (i >= entityType.Properties.Count)
				{
					if (!list.Contains(op.RelationshipProperties[num]))
					{
						n.Children.RemoveAt(i);
						list3.RemoveAt(num);
					}
					i--;
					num--;
				}
				this.VisitScalarOpDefault(op, n);
				Node node = this.BuildKeyExpressionForNewEntityOp(op, n);
				Dictionary<RelProperty, Node> dictionary = new Dictionary<RelProperty, Node>();
				num = 0;
				int j = entityType.Properties.Count;
				while (j < n.Children.Count)
				{
					dictionary[list3[num]] = n.Children[j];
					j++;
					num++;
				}
				list2 = new List<Node>();
				for (int k = 0; k < entityType.Properties.Count; k++)
				{
					list2.Add(n.Children[k]);
				}
				foreach (Node node2 in this.BuildAllRelPropertyExpressions(currentEntityTypeScope, list, dictionary, node))
				{
					list2.Add(node2);
				}
			}
			Op op2 = base.m_command.CreateScopedNewEntityOp(op.Type, list, currentEntityTypeScope);
			return base.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00086DB0 File Offset: 0x00084FB0
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			HashSet<RelProperty> hashSet = new HashSet<RelProperty>();
			List<RelProperty> list = new List<RelProperty>();
			foreach (KeyValuePair<object, EntityType> keyValuePair in op.DiscriminatorMap.TypeMap)
			{
				EntityTypeBase value = keyValuePair.Value;
				this.AddTypeReference(TypeUsage.Create(value));
				foreach (RelProperty relProperty in this.m_relPropertyHelper.GetRelProperties(value))
				{
					hashSet.Add(relProperty);
				}
			}
			list = new List<RelProperty>(hashSet);
			this.VisitScalarOpDefault(op, n);
			Node node = this.BuildKeyExpressionForNewEntityOp(op, n);
			List<Node> list2 = new List<Node>();
			int num = n.Children.Count - op.RelationshipProperties.Count;
			for (int i = 0; i < num; i++)
			{
				list2.Add(n.Children[i]);
			}
			Dictionary<RelProperty, Node> dictionary = new Dictionary<RelProperty, Node>();
			int j = num;
			int num2 = 0;
			while (j < n.Children.Count)
			{
				dictionary[op.RelationshipProperties[num2]] = n.Children[j];
				j++;
				num2++;
			}
			foreach (Node node2 in this.BuildAllRelPropertyExpressions(op.EntitySet, list, dictionary, node))
			{
				list2.Add(node2);
			}
			Op op2 = base.m_command.CreateDiscriminatedNewEntityOp(op.Type, op.DiscriminatorMap, op.EntitySet, list);
			return base.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00086F94 File Offset: 0x00085194
		public override Node Visit(NewMultisetOp op, Node n)
		{
			Node node = null;
			Var var = null;
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(op.Type);
			if (!n.HasChild0)
			{
				Node node2 = base.m_command.CreateNode(base.m_command.CreateSingleRowTableOp());
				Node node3 = base.m_command.CreateNode(base.m_command.CreateFilterOp(), node2, base.m_command.CreateNode(base.m_command.CreateFalseOp()));
				Node node4 = base.m_command.CreateNode(base.m_command.CreateNullOp(edmType.TypeUsage));
				Var var2;
				node = base.m_command.BuildProject(node3, node4, out var2);
				var = var2;
			}
			else if (n.Children.Count == 1 || PreProcessor.AreAllConstantsOrNulls(n.Children))
			{
				List<Node> list = new List<Node>();
				List<Var> list2 = new List<Var>();
				foreach (Node node5 in n.Children)
				{
					Node node6 = base.m_command.CreateNode(base.m_command.CreateSingleRowTableOp());
					Var var3;
					Node node7 = base.m_command.BuildProject(node6, node5, out var3);
					list.Add(node7);
					list2.Add(var3);
				}
				base.m_command.BuildUnionAllLadder(list, list2, out node, out var);
			}
			else
			{
				List<Node> list3 = new List<Node>();
				List<Var> list4 = new List<Var>();
				for (int i = 0; i < n.Children.Count; i++)
				{
					Node node8 = base.m_command.CreateNode(base.m_command.CreateSingleRowTableOp());
					Node node9 = base.m_command.CreateNode(base.m_command.CreateInternalConstantOp(base.m_command.IntegerType, i));
					Var var4;
					Node node10 = base.m_command.BuildProject(node8, node9, out var4);
					list3.Add(node10);
					list4.Add(var4);
				}
				base.m_command.BuildUnionAllLadder(list3, list4, out node, out var);
				List<Node> list5 = new List<Node>(n.Children.Count * 2 + 1);
				for (int j = 0; j < n.Children.Count; j++)
				{
					if (j != n.Children.Count - 1)
					{
						ComparisonOp comparisonOp = base.m_command.CreateComparisonOp(OpType.EQ, false);
						Node node11 = base.m_command.CreateNode(comparisonOp, base.m_command.CreateNode(base.m_command.CreateVarRefOp(var)), base.m_command.CreateNode(base.m_command.CreateConstantOp(base.m_command.IntegerType, j)));
						list5.Add(node11);
					}
					list5.Add(n.Children[j]);
				}
				Node node12 = base.m_command.CreateNode(base.m_command.CreateCaseOp(edmType.TypeUsage), list5);
				node = base.m_command.BuildProject(node, node12, out var);
			}
			PhysicalProjectOp physicalProjectOp = base.m_command.CreatePhysicalProjectOp(var);
			Node node13 = base.m_command.CreateNode(physicalProjectOp, node);
			CollectOp collectOp = base.m_command.CreateCollectOp(op.Type);
			Node node14 = base.m_command.CreateNode(collectOp, node13);
			return base.VisitNode(node14);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000872DC File Offset: 0x000854DC
		private static bool AreAllConstantsOrNulls(List<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				if (node.Op.OpType != OpType.Constant && node.Op.OpType != OpType.Null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x00087348 File Offset: 0x00085548
		public override Node Visit(CollectOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NestPullup);
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00087360 File Offset: 0x00085560
		private void HandleTableOpMetadata(ScanTableBaseOp op)
		{
			EntitySet entitySet = op.Table.TableMetadata.Extent as EntitySet;
			if (entitySet != null)
			{
				this.AddEntitySetReference(entitySet);
			}
			TypeUsage typeUsage = TypeUsage.Create(op.Table.TableMetadata.Extent.ElementType);
			this.AddTypeReference(typeUsage);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000873B0 File Offset: 0x000855B0
		private Node ProcessScanTable(Node scanTableNode, ScanTableOp scanTableOp, ref IsOfOp typeFilter)
		{
			this.HandleTableOpMetadata(scanTableOp);
			PlanCompiler.Assert(scanTableOp.Table.TableMetadata.Extent != null, "ScanTableOp must reference a table with an extent");
			if (scanTableOp.Table.TableMetadata.Extent.EntityContainer.DataSpace == DataSpace.SSpace)
			{
				return scanTableNode;
			}
			Node node = this.ExpandView(scanTableOp, ref typeFilter);
			return base.VisitNode(node);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00087414 File Offset: 0x00085614
		public override Node Visit(ScanTableOp op, Node n)
		{
			IsOfOp isOfOp = null;
			return this.ProcessScanTable(n, op, ref isOfOp);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00087430 File Offset: 0x00085630
		public override Node Visit(ScanViewOp op, Node n)
		{
			bool flag = false;
			if (op.Table.TableMetadata.Extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
			{
				this.m_entityTypeScopes.Push((EntitySet)op.Table.TableMetadata.Extent);
				flag = true;
			}
			this.HandleTableOpMetadata(op);
			this.VisitRelOpDefault(op, n);
			if (flag)
			{
				PlanCompiler.Assert(this.m_entityTypeScopes.Pop() == op.Table.TableMetadata.Extent, "m_entityTypeScopes stack is broken");
			}
			return n;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000874B5 File Offset: 0x000856B5
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (op.OpType == OpType.InnerJoin || op.OpType == OpType.LeftOuterJoin)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			}
			if (base.ProcessJoinOp(n))
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.Normalization);
			}
			return n;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000874ED File Offset: 0x000856ED
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x00087503 File Offset: 0x00085703
		private bool IsSortUnnecessary()
		{
			Node node = this.m_ancestors.Peek();
			PlanCompiler.Assert(node != null, "unexpected SortOp as root node?");
			return node.Op.OpType != OpType.PhysicalProject;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x0008752F File Offset: 0x0008572F
		public override Node Visit(SortOp op, Node n)
		{
			if (this.IsSortUnnecessary())
			{
				return base.VisitNode(n.Child0);
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x00087550 File Offset: 0x00085750
		private static bool IsOfTypeOverScanTable(Node n, out IsOfOp typeFilter)
		{
			typeFilter = null;
			IsOfOp isOfOp = n.Child1.Op as IsOfOp;
			if (isOfOp == null)
			{
				return false;
			}
			ScanTableOp scanTableOp = n.Child0.Op as ScanTableOp;
			if (scanTableOp == null || scanTableOp.Table.Columns.Count != 1)
			{
				return false;
			}
			VarRefOp varRefOp = n.Child1.Child0.Op as VarRefOp;
			if (varRefOp == null || varRefOp.Var != scanTableOp.Table.Columns[0])
			{
				return false;
			}
			typeFilter = isOfOp;
			return true;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000875D8 File Offset: 0x000857D8
		public override Node Visit(FilterOp op, Node n)
		{
			IsOfOp isOfOp;
			if (PreProcessor.IsOfTypeOverScanTable(n, out isOfOp))
			{
				Node node = this.ProcessScanTable(n.Child0, (ScanTableOp)n.Child0.Op, ref isOfOp);
				if (isOfOp != null)
				{
					n.Child1 = base.VisitNode(n.Child1);
					n.Child0 = node;
					node = n;
				}
				return node;
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00087638 File Offset: 0x00085838
		public override Node Visit(ProjectOp op, Node n)
		{
			PlanCompiler.Assert(n.HasChild0, "projectOp without input?");
			if (OpType.Sort == n.Child0.Op.OpType || OpType.ConstrainedSort == n.Child0.Op.OpType)
			{
				SortBaseOp sortBaseOp = (SortBaseOp)n.Child0.Op;
				if (sortBaseOp.Keys.Count > 0)
				{
					IList<Node> list = new List<Node>();
					list.Add(n);
					for (int i = 1; i < n.Child0.Children.Count; i++)
					{
						list.Add(n.Child0.Children[i]);
					}
					n.Child0 = n.Child0.Child0;
					foreach (SortKey sortKey in sortBaseOp.Keys)
					{
						op.Outputs.Set(sortKey.Var);
					}
					return base.VisitNode(base.m_command.CreateNode(sortBaseOp, list));
				}
			}
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00087760 File Offset: 0x00085960
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.AggregatePushdown);
			return base.Visit(op, n);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00087776 File Offset: 0x00085976
		public override Node Visit(ComparisonOp op, Node n)
		{
			if (op.OpType == OpType.EQ || op.OpType == OpType.NE)
			{
				this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.NullSemantics);
			}
			return base.Visit(op, n);
		}

		// Token: 0x04000E58 RID: 3672
		private readonly Stack<EntitySet> m_entityTypeScopes = new Stack<EntitySet>();

		// Token: 0x04000E59 RID: 3673
		private readonly HashSet<EntityContainer> m_referencedEntityContainers = new HashSet<EntityContainer>();

		// Token: 0x04000E5A RID: 3674
		private readonly HashSet<EntitySet> m_referencedEntitySets = new HashSet<EntitySet>();

		// Token: 0x04000E5B RID: 3675
		private readonly HashSet<TypeUsage> m_referencedTypes = new HashSet<TypeUsage>();

		// Token: 0x04000E5C RID: 3676
		private readonly HashSet<EntityType> m_freeFloatingEntityConstructorTypes = new HashSet<EntityType>();

		// Token: 0x04000E5D RID: 3677
		private readonly HashSet<string> m_typesNeedingNullSentinel = new HashSet<string>();

		// Token: 0x04000E5E RID: 3678
		private readonly Dictionary<EdmFunction, EdmProperty[]> m_tvfResultKeys = new Dictionary<EdmFunction, EdmProperty[]>();

		// Token: 0x04000E5F RID: 3679
		private readonly RelPropertyHelper m_relPropertyHelper;

		// Token: 0x04000E60 RID: 3680
		private bool m_suppressDiscriminatorMaps;

		// Token: 0x04000E61 RID: 3681
		private readonly Dictionary<EntitySetBase, DiscriminatorMapInfo> m_discriminatorMaps = new Dictionary<EntitySetBase, DiscriminatorMapInfo>();

		// Token: 0x04000E62 RID: 3682
		private readonly Dictionary<PreProcessor.NavigationPropertyOpInfo, Node> _navigationPropertyOpRewrites = new Dictionary<PreProcessor.NavigationPropertyOpInfo, Node>();

		// Token: 0x020009F2 RID: 2546
		private class NavigationPropertyOpInfo
		{
			// Token: 0x0600600B RID: 24587 RVA: 0x0014A474 File Offset: 0x00148674
			public NavigationPropertyOpInfo(Node node, Node root, Command command)
			{
				this._node = node;
				this._root = root;
				this._command = command;
				this._hashCode = (((((this._root != null) ? RuntimeHelpers.GetHashCode(this._root) : 0) * 397) ^ RuntimeHelpers.GetHashCode(PreProcessor.NavigationPropertyOpInfo.GetProperty(this._node))) * 397) ^ this._node.GetNodeInfo(this._command).HashValue;
			}

			// Token: 0x0600600C RID: 24588 RVA: 0x0014A4EC File Offset: 0x001486EC
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x0600600D RID: 24589 RVA: 0x0014A4F4 File Offset: 0x001486F4
			public override bool Equals(object obj)
			{
				PreProcessor.NavigationPropertyOpInfo navigationPropertyOpInfo = obj as PreProcessor.NavigationPropertyOpInfo;
				return navigationPropertyOpInfo != null && this._root != null && this._root == navigationPropertyOpInfo._root && PreProcessor.NavigationPropertyOpInfo.GetProperty(this._node) == PreProcessor.NavigationPropertyOpInfo.GetProperty(navigationPropertyOpInfo._node) && this._node.IsEquivalent(navigationPropertyOpInfo._node);
			}

			// Token: 0x0600600E RID: 24590 RVA: 0x0014A54C File Offset: 0x0014874C
			public void Seal()
			{
				this._node = OpCopier.Copy(this._command, this._node);
			}

			// Token: 0x0600600F RID: 24591 RVA: 0x0014A565 File Offset: 0x00148765
			private static EdmMember GetProperty(Node node)
			{
				return ((PropertyOp)node.Op).PropertyInfo;
			}

			// Token: 0x040028B3 RID: 10419
			private Node _node;

			// Token: 0x040028B4 RID: 10420
			private readonly Node _root;

			// Token: 0x040028B5 RID: 10421
			private readonly Command _command;

			// Token: 0x040028B6 RID: 10422
			private readonly int _hashCode;
		}
	}
}
