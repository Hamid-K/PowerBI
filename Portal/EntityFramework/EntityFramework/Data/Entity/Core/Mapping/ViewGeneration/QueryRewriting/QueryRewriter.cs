using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000589 RID: 1417
	internal class QueryRewriter
	{
		// Token: 0x0600446B RID: 17515 RVA: 0x000F0FCC File Offset: 0x000EF1CC
		internal QueryRewriter(EdmType generatedType, ViewgenContext context, ViewGenMode typesGenerationMode)
		{
			this._typesGenerationMode = typesGenerationMode;
			this._context = context;
			this._generatedType = generatedType;
			this._domainMap = context.MemberMaps.LeftDomainMap;
			this._config = context.Config;
			this._identifiers = context.CqlIdentifiers;
			this._qp = new RewritingProcessor<Tile<FragmentQuery>>(new DefaultTileProcessor<FragmentQuery>(context.LeftFragmentQP));
			this._extentPath = new MemberPath(context.Extent);
			this._keyAttributes = new List<MemberPath>(MemberPath.GetKeyMembers(context.Extent, this._domainMap));
			foreach (LeftCellWrapper leftCellWrapper in this._context.AllWrappersForExtent)
			{
				FragmentQuery fragmentQuery = leftCellWrapper.FragmentQuery;
				Tile<FragmentQuery> tile = QueryRewriter.CreateTile(fragmentQuery);
				this._fragmentQueries.Add(fragmentQuery);
				this._views.Add(tile);
			}
			this.AdjustMemberDomainsForUpdateViews();
			this._domainQuery = this.GetDomainQuery(this.FragmentQueries, generatedType);
			this._usedViews = new HashSet<FragmentQuery>();
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x000F1130 File Offset: 0x000EF330
		internal void GenerateViewComponents()
		{
			this.EnsureExtentIsFullyMapped(this._usedViews);
			this.GenerateCaseStatements(this._domainMap.ConditionMembers(this._extentPath.Extent), this._usedViews);
			this.AddTrivialCaseStatementsForConditionMembers();
			if (this._usedViews.Count == 0 || this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._config);
			}
			this._topLevelWhereClause = this.GetTopLevelWhereClause(this._usedViews);
			ViewTarget viewTarget = this._context.ViewTarget;
			this._usedCells = this.RemapFromVariables();
			BasicViewGenerator basicViewGenerator = new BasicViewGenerator(this._context.MemberMaps.ProjectedSlotMap, this._usedCells, this._domainQuery, this._context, this._domainMap, this._errorLog, this._config);
			this._basicView = basicViewGenerator.CreateViewExpression();
			if (this._context.LeftFragmentQP.IsContainedIn(this._basicView.LeftFragmentQuery, this._domainQuery))
			{
				this._topLevelWhereClause = BoolExpression.True;
			}
			if (this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._config);
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x000F125A File Offset: 0x000EF45A
		internal ViewgenContext ViewgenContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x000F1262 File Offset: 0x000EF462
		internal Dictionary<MemberPath, CaseStatement> CaseStatements
		{
			get
			{
				return this._caseStatements;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x000F126A File Offset: 0x000EF46A
		internal BoolExpression TopLevelWhereClause
		{
			get
			{
				return this._topLevelWhereClause;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x000F1272 File Offset: 0x000EF472
		internal CellTreeNode BasicView
		{
			get
			{
				return this._basicView.MakeCopy();
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x000F127F File Offset: 0x000EF47F
		internal List<LeftCellWrapper> UsedCells
		{
			get
			{
				return this._usedCells;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x000F1287 File Offset: 0x000EF487
		private IEnumerable<FragmentQuery> FragmentQueries
		{
			get
			{
				return this._fragmentQueries;
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x000F1290 File Offset: 0x000EF490
		private IEnumerable<Constant> GetDomain(MemberPath currentPath)
		{
			if (this._context.ViewTarget == ViewTarget.QueryView && MemberPath.EqualityComparer.Equals(currentPath, this._extentPath))
			{
				IEnumerable<EdmType> enumerable;
				if (this._typesGenerationMode == ViewGenMode.OfTypeOnlyViews)
				{
					enumerable = new HashSet<EdmType> { this._generatedType };
				}
				else
				{
					enumerable = MetadataHelper.GetTypeAndSubtypesOf(this._generatedType, this._context.EdmItemCollection, false);
				}
				return QueryRewriter.GetTypeConstants(enumerable);
			}
			return this._domainMap.GetDomain(currentPath);
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000F1308 File Offset: 0x000EF508
		private void AdjustMemberDomainsForUpdateViews()
		{
			if (this._context.ViewTarget == ViewTarget.UpdateView)
			{
				using (List<MemberPath>.Enumerator enumerator = new List<MemberPath>(this._domainMap.ConditionMembers(this._extentPath.Extent)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MemberPath currentPath = enumerator.Current;
						Constant constant = this._domainMap.GetDomain(currentPath).FirstOrDefault((Constant domainValue) => QueryRewriter.IsDefaultValue(domainValue, currentPath));
						if (constant != null)
						{
							this.RemoveUnusedValueFromStoreDomain(constant, currentPath);
						}
						Constant constant2 = this._domainMap.GetDomain(currentPath).FirstOrDefault((Constant domainValue) => domainValue is NegatedConstant);
						if (constant2 != null)
						{
							this.RemoveUnusedValueFromStoreDomain(constant2, currentPath);
						}
					}
				}
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000F1404 File Offset: 0x000EF604
		private void RemoveUnusedValueFromStoreDomain(Constant domainValue, MemberPath currentPath)
		{
			BoolExpression boolExpression = this.CreateMemberCondition(currentPath, domainValue);
			HashSet<FragmentQuery> hashSet = new HashSet<FragmentQuery>();
			bool flag = false;
			Tile<FragmentQuery> tile;
			if (this.FindRewritingAndUsedViews(this._keyAttributes, boolExpression, hashSet, out tile))
			{
				flag = !QueryRewriter.TileToCellTree(tile, this._context).IsEmptyRightFragmentQuery;
			}
			if (!flag)
			{
				Set<Constant> set = new Set<Constant>(this._domainMap.GetDomain(currentPath), Constant.EqualityComparer);
				set.Remove(domainValue);
				this._domainMap.UpdateConditionMemberDomain(currentPath, set);
				foreach (FragmentQuery fragmentQuery in this._fragmentQueries)
				{
					fragmentQuery.Condition.FixDomainMap(this._domainMap);
				}
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x000F14CC File Offset: 0x000EF6CC
		internal FragmentQuery GetDomainQuery(IEnumerable<FragmentQuery> fragmentQueries, EdmType generatedType)
		{
			if (this._context.ViewTarget == ViewTarget.QueryView)
			{
				BoolExpression boolExpression;
				if (generatedType == null)
				{
					boolExpression = BoolExpression.True;
				}
				else
				{
					IEnumerable<EdmType> enumerable;
					if (this._typesGenerationMode == ViewGenMode.OfTypeOnlyViews)
					{
						enumerable = new HashSet<EdmType> { this._generatedType };
					}
					else
					{
						enumerable = MetadataHelper.GetTypeAndSubtypesOf(generatedType, this._context.EdmItemCollection, false);
					}
					Domain domain = new Domain(QueryRewriter.GetTypeConstants(enumerable), this._domainMap.GetDomain(this._extentPath));
					boolExpression = BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(this._extentPath), domain), this._domainMap);
				}
				return FragmentQuery.Create(this._keyAttributes, boolExpression);
			}
			BoolExpression boolExpression2 = BoolExpression.CreateOr(fragmentQueries.Select((FragmentQuery fragmentQuery) => fragmentQuery.Condition).ToArray<BoolExpression>());
			return FragmentQuery.Create(this._keyAttributes, boolExpression2);
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000F15AC File Offset: 0x000EF7AC
		private bool AddRewritingToCaseStatement(Tile<FragmentQuery> rewriting, CaseStatement caseStatement, MemberPath currentPath, Constant domainValue)
		{
			BoolExpression boolExpression = BoolExpression.True;
			bool flag = this._qp.IsContainedIn(QueryRewriter.CreateTile(this._domainQuery), rewriting);
			if (this._qp.IsDisjointFrom(QueryRewriter.CreateTile(this._domainQuery), rewriting))
			{
				return false;
			}
			ProjectedSlot projectedSlot;
			if (domainValue.HasNotNull())
			{
				projectedSlot = new MemberProjectedSlot(currentPath);
			}
			else
			{
				projectedSlot = new ConstantProjectedSlot(domainValue);
			}
			if (!flag)
			{
				boolExpression = QueryRewriter.TileToBoolExpr(rewriting);
			}
			else
			{
				boolExpression = BoolExpression.True;
			}
			caseStatement.AddWhenThen(boolExpression, projectedSlot);
			return flag;
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000F162C File Offset: 0x000EF82C
		private void EnsureConfigurationIsFullyMapped(MemberPath currentPath, BoolExpression currentWhereClause, HashSet<FragmentQuery> outputUsedViews, ErrorLog errorLog)
		{
			foreach (Constant constant in this.GetDomain(currentPath))
			{
				if (constant != Constant.Undefined)
				{
					BoolExpression boolExpression = this.CreateMemberCondition(currentPath, constant);
					BoolExpression boolExpression2 = BoolExpression.CreateAnd(new BoolExpression[] { currentWhereClause, boolExpression });
					Tile<FragmentQuery> tile;
					if (!this.FindRewritingAndUsedViews(this._keyAttributes, boolExpression2, outputUsedViews, out tile))
					{
						if (!ErrorPatternMatcher.FindMappingErrors(this._context, this._domainMap, this._errorLog))
						{
							StringBuilder stringBuilder = new StringBuilder();
							string text = StringUtil.FormatInvariant("{0}", new object[] { this._extentPath });
							BoolExpression condition = tile.Query.Condition;
							condition.ExpensiveSimplify();
							if (condition.RepresentsAllTypeConditions)
							{
								string viewGen_Extent = Strings.ViewGen_Extent;
								stringBuilder.AppendLine(Strings.ViewGen_Cannot_Recover_Types(viewGen_Extent, text));
							}
							else
							{
								string viewGen_Entities = Strings.ViewGen_Entities;
								stringBuilder.AppendLine(Strings.ViewGen_Cannot_Disambiguate_MultiConstant(viewGen_Entities, text));
							}
							RewritingValidator.EntityConfigurationToUserString(condition, stringBuilder);
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.AmbiguousMultiConstants, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
							errorLog.AddEntry(record);
						}
					}
					else
					{
						TypeConstant typeConstant = constant as TypeConstant;
						if (typeConstant != null)
						{
							EdmType edmType = typeConstant.EdmType;
							List<MemberPath> list = QueryRewriter.GetNonConditionalScalarMembers(edmType, currentPath, this._domainMap).Union(QueryRewriter.GetNonConditionalComplexMembers(edmType, currentPath, this._domainMap)).ToList<MemberPath>();
							IEnumerable<MemberPath> enumerable;
							if (list.Count > 0 && !this.FindRewritingAndUsedViews(list, boolExpression2, outputUsedViews, out tile, out enumerable))
							{
								list = new List<MemberPath>(list.Where((MemberPath a) => !a.IsPartOfKey));
								this.AddUnrecoverableAttributesError(enumerable, boolExpression, errorLog);
							}
							else
							{
								foreach (MemberPath memberPath in QueryRewriter.GetConditionalComplexMembers(edmType, currentPath, this._domainMap))
								{
									this.EnsureConfigurationIsFullyMapped(memberPath, boolExpression2, outputUsedViews, errorLog);
								}
								foreach (MemberPath memberPath2 in QueryRewriter.GetConditionalScalarMembers(edmType, currentPath, this._domainMap))
								{
									this.EnsureConfigurationIsFullyMapped(memberPath2, boolExpression2, outputUsedViews, errorLog);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x000F18D4 File Offset: 0x000EFAD4
		private static List<string> GetTypeBasedMemberPathList(IEnumerable<MemberPath> nonConditionalScalarAttributes)
		{
			List<string> list = new List<string>();
			foreach (MemberPath memberPath in nonConditionalScalarAttributes)
			{
				EdmMember leafEdmMember = memberPath.LeafEdmMember;
				List<string> list2 = list;
				string name = leafEdmMember.DeclaringType.Name;
				string text = ".";
				EdmMember edmMember = leafEdmMember;
				list2.Add(name + text + ((edmMember != null) ? edmMember.ToString() : null));
			}
			return list;
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000F194C File Offset: 0x000EFB4C
		private void AddUnrecoverableAttributesError(IEnumerable<MemberPath> attributes, BoolExpression domainAddedWhereClause, ErrorLog errorLog)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = StringUtil.FormatInvariant("{0}", new object[] { this._extentPath });
			string viewGen_Extent = Strings.ViewGen_Extent;
			string text2 = StringUtil.ToCommaSeparatedString(QueryRewriter.GetTypeBasedMemberPathList(attributes));
			stringBuilder.AppendLine(Strings.ViewGen_Cannot_Recover_Attributes(text2, viewGen_Extent, text));
			RewritingValidator.EntityConfigurationToUserString(domainAddedWhereClause, stringBuilder);
			ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.AttributesUnrecoverable, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
			errorLog.AddEntry(record);
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x000F19CC File Offset: 0x000EFBCC
		private void GenerateCaseStatements(IEnumerable<MemberPath> members, HashSet<FragmentQuery> outputUsedViews)
		{
			IEnumerable<LeftCellWrapper> enumerable = this._context.AllWrappersForExtent.Where((LeftCellWrapper w) => this._usedViews.Contains(w.FragmentQuery));
			ViewgenContext context = this._context;
			CellTreeOpType cellTreeOpType = CellTreeOpType.Union;
			CellTreeNode[] array = enumerable.Select((LeftCellWrapper wrapper) => new LeafCellTreeNode(this._context, wrapper)).ToArray<LeafCellTreeNode>();
			CellTreeNode cellTreeNode = new OpCellTreeNode(context, cellTreeOpType, array);
			foreach (MemberPath memberPath in members)
			{
				List<Constant> list = this.GetDomain(memberPath).ToList<Constant>();
				CaseStatement caseStatement = new CaseStatement(memberPath);
				Tile<FragmentQuery> tile = null;
				bool flag = list.Count != 2 || !list.Contains(Constant.Null, Constant.EqualityComparer) || !list.Contains(Constant.NotNull, Constant.EqualityComparer);
				foreach (Constant constant in list)
				{
					if (constant == Constant.Undefined && this._context.ViewTarget == ViewTarget.QueryView)
					{
						caseStatement.AddWhenThen(BoolExpression.False, new ConstantProjectedSlot(Constant.Undefined));
					}
					else
					{
						FragmentQuery fragmentQuery = this.CreateMemberConditionQuery(memberPath, constant);
						Tile<FragmentQuery> tile2;
						if (this.FindRewritingAndUsedViews(fragmentQuery.Attributes, fragmentQuery.Condition, outputUsedViews, out tile2))
						{
							if (this._context.ViewTarget == ViewTarget.UpdateView)
							{
								tile = ((tile != null) ? this._qp.Union(tile, tile2) : tile2);
							}
							if (flag && this.AddRewritingToCaseStatement(tile2, caseStatement, memberPath, constant))
							{
								break;
							}
						}
						else if (!QueryRewriter.IsDefaultValue(constant, memberPath) && !ErrorPatternMatcher.FindMappingErrors(this._context, this._domainMap, this._errorLog))
						{
							StringBuilder stringBuilder = new StringBuilder();
							string text = StringUtil.FormatInvariant("{0}", new object[] { this._extentPath });
							string text2 = ((this._context.ViewTarget == ViewTarget.QueryView) ? Strings.ViewGen_Entities : Strings.ViewGen_Tuples);
							if (this._context.ViewTarget == ViewTarget.QueryView)
							{
								stringBuilder.AppendLine(Strings.Viewgen_CannotGenerateQueryViewUnderNoValidation(text));
							}
							else
							{
								stringBuilder.AppendLine(Strings.ViewGen_Cannot_Disambiguate_MultiConstant(text2, text));
							}
							RewritingValidator.EntityConfigurationToUserString(fragmentQuery.Condition, stringBuilder, this._context.ViewTarget == ViewTarget.UpdateView);
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.AmbiguousMultiConstants, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
							this._errorLog.AddEntry(record);
						}
					}
				}
				if (this._errorLog.Count == 0)
				{
					if (this._context.ViewTarget == ViewTarget.UpdateView && flag)
					{
						this.AddElseDefaultToCaseStatement(memberPath, caseStatement, list, cellTreeNode, tile);
					}
					if (caseStatement.Clauses.Count > 0)
					{
						this._caseStatements[memberPath] = caseStatement;
					}
				}
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x000F1CCC File Offset: 0x000EFECC
		private void AddElseDefaultToCaseStatement(MemberPath currentPath, CaseStatement caseStatement, List<Constant> domain, CellTreeNode rightDomainQuery, Tile<FragmentQuery> unionCaseRewriting)
		{
			Constant constant;
			bool flag = Domain.TryGetDefaultValueForMemberPath(currentPath, out constant);
			if (!flag || !domain.Contains(constant))
			{
				CellTreeNode cellTreeNode = QueryRewriter.TileToCellTree(unionCaseRewriting, this._context);
				FragmentQuery fragmentQuery = this._context.RightFragmentQP.Difference(rightDomainQuery.RightFragmentQuery, cellTreeNode.RightFragmentQuery);
				if (this._context.RightFragmentQP.IsSatisfiable(fragmentQuery))
				{
					if (flag)
					{
						caseStatement.AddWhenThen(BoolExpression.True, new ConstantProjectedSlot(constant));
						return;
					}
					fragmentQuery.Condition.ExpensiveSimplify();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(Strings.ViewGen_No_Default_Value_For_Configuration(currentPath.PathToString(new bool?(false))));
					this._errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.NoDefaultValue, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty));
				}
			}
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000F1D9C File Offset: 0x000EFF9C
		private BoolExpression GetTopLevelWhereClause(HashSet<FragmentQuery> outputUsedViews)
		{
			BoolExpression boolExpression = BoolExpression.True;
			Tile<FragmentQuery> tile;
			if (this._context.ViewTarget == ViewTarget.QueryView && !this._domainQuery.Condition.IsTrue && this.FindRewritingAndUsedViews(this._keyAttributes, this._domainQuery.Condition, outputUsedViews, out tile))
			{
				boolExpression = QueryRewriter.TileToBoolExpr(tile);
				boolExpression.ExpensiveSimplify();
			}
			return boolExpression;
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000F1DF8 File Offset: 0x000EFFF8
		internal void EnsureExtentIsFullyMapped(HashSet<FragmentQuery> outputUsedViews)
		{
			if (this._context.ViewTarget == ViewTarget.QueryView && this._config.IsValidationEnabled)
			{
				this.EnsureConfigurationIsFullyMapped(this._extentPath, BoolExpression.True, outputUsedViews, this._errorLog);
				if (this._errorLog.Count > 0)
				{
					ExceptionHelpers.ThrowMappingException(this._errorLog, this._config);
					return;
				}
			}
			else
			{
				if (this._config.IsValidationEnabled)
				{
					foreach (MemberPath memberPath in this._context.MemberMaps.ProjectedSlotMap.Members)
					{
						Constant constant;
						if (memberPath.IsScalarType() && !memberPath.IsPartOfKey && !this._domainMap.IsConditionMember(memberPath) && !Domain.TryGetDefaultValueForMemberPath(memberPath, out constant))
						{
							HashSet<MemberPath> hashSet = new HashSet<MemberPath>(this._keyAttributes);
							hashSet.Add(memberPath);
							foreach (LeftCellWrapper leftCellWrapper in this._context.AllWrappersForExtent)
							{
								FragmentQuery fragmentQuery = leftCellWrapper.FragmentQuery;
								FragmentQuery fragmentQuery2 = new FragmentQuery(fragmentQuery.Description, fragmentQuery.FromVariable, hashSet, fragmentQuery.Condition);
								Tile<FragmentQuery> tile = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(fragmentQuery.Condition)));
								Tile<FragmentQuery> tile2;
								IEnumerable<MemberPath> enumerable;
								if (!this.RewriteQuery(QueryRewriter.CreateTile(fragmentQuery2), tile, out tile2, out enumerable, false))
								{
									Domain.GetDefaultValueForMemberPath(memberPath, new LeftCellWrapper[] { leftCellWrapper }, this._config);
								}
							}
						}
					}
				}
				using (List<Tile<FragmentQuery>>.Enumerator enumerator3 = this._views.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Tile<FragmentQuery> toFill = enumerator3.Current;
						Tile<FragmentQuery> tile3 = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(toFill.Query.Condition)));
						Tile<FragmentQuery> tile4;
						IEnumerable<MemberPath> enumerable2;
						if (!this.RewriteQuery(toFill, tile3, out tile4, out enumerable2, true))
						{
							LeftCellWrapper leftCellWrapper2 = this._context.AllWrappersForExtent.First((LeftCellWrapper lcr) => lcr.FragmentQuery.Equals(toFill.Query));
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ImpossibleCondition, Strings.Viewgen_QV_RewritingNotFound(leftCellWrapper2.RightExtent.ToString()), leftCellWrapper2.Cells, string.Empty);
							this._errorLog.AddEntry(record);
						}
						else
						{
							outputUsedViews.UnionWith(tile4.GetNamedQueries());
						}
					}
				}
			}
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x000F20CC File Offset: 0x000F02CC
		private List<LeftCellWrapper> RemapFromVariables()
		{
			List<LeftCellWrapper> list = new List<LeftCellWrapper>();
			int num = 0;
			Dictionary<BoolLiteral, BoolLiteral> dictionary = new Dictionary<BoolLiteral, BoolLiteral>(BoolLiteral.EqualityIdentifierComparer);
			foreach (LeftCellWrapper leftCellWrapper in this._context.AllWrappersForExtent)
			{
				if (this._usedViews.Contains(leftCellWrapper.FragmentQuery))
				{
					list.Add(leftCellWrapper);
					int cellNumber = leftCellWrapper.OnlyInputCell.CellNumber;
					if (num != cellNumber)
					{
						dictionary[new CellIdBoolean(this._identifiers, cellNumber)] = new CellIdBoolean(this._identifiers, num);
					}
					num++;
				}
			}
			if (dictionary.Count > 0)
			{
				this._topLevelWhereClause = this._topLevelWhereClause.RemapLiterals(dictionary);
				Dictionary<MemberPath, CaseStatement> dictionary2 = new Dictionary<MemberPath, CaseStatement>();
				foreach (KeyValuePair<MemberPath, CaseStatement> keyValuePair in this._caseStatements)
				{
					CaseStatement caseStatement = new CaseStatement(keyValuePair.Key);
					foreach (CaseStatement.WhenThen whenThen in keyValuePair.Value.Clauses)
					{
						caseStatement.AddWhenThen(whenThen.Condition.RemapLiterals(dictionary), whenThen.Value);
					}
					dictionary2[keyValuePair.Key] = caseStatement;
				}
				this._caseStatements = dictionary2;
			}
			return list;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000F2270 File Offset: 0x000F0470
		internal void AddTrivialCaseStatementsForConditionMembers()
		{
			for (int i = 0; i < this._context.MemberMaps.ProjectedSlotMap.Count; i++)
			{
				MemberPath memberPath = this._context.MemberMaps.ProjectedSlotMap[i];
				if (!memberPath.IsScalarType() && !this._caseStatements.ContainsKey(memberPath))
				{
					Constant constant = new TypeConstant(memberPath.EdmType);
					CaseStatement caseStatement = new CaseStatement(memberPath);
					caseStatement.AddWhenThen(BoolExpression.True, new ConstantProjectedSlot(constant));
					this._caseStatements[memberPath] = caseStatement;
				}
			}
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000F22FC File Offset: 0x000F04FC
		private bool FindRewritingAndUsedViews(IEnumerable<MemberPath> attributes, BoolExpression whereClause, HashSet<FragmentQuery> outputUsedViews, out Tile<FragmentQuery> rewriting)
		{
			IEnumerable<MemberPath> enumerable;
			return this.FindRewritingAndUsedViews(attributes, whereClause, outputUsedViews, out rewriting, out enumerable);
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x000F2316 File Offset: 0x000F0516
		private bool FindRewritingAndUsedViews(IEnumerable<MemberPath> attributes, BoolExpression whereClause, HashSet<FragmentQuery> outputUsedViews, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes)
		{
			if (this.FindRewriting(attributes, whereClause, out rewriting, out notCoveredAttributes))
			{
				outputUsedViews.UnionWith(rewriting.GetNamedQueries());
				return true;
			}
			return false;
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000F2338 File Offset: 0x000F0538
		private bool FindRewriting(IEnumerable<MemberPath> attributes, BoolExpression whereClause, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes)
		{
			Tile<FragmentQuery> tile = QueryRewriter.CreateTile(FragmentQuery.Create(attributes, whereClause));
			Tile<FragmentQuery> tile2 = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(whereClause)));
			bool flag = this._context.ViewTarget == ViewTarget.UpdateView;
			return this.RewriteQuery(tile, tile2, out rewriting, out notCoveredAttributes, flag);
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x000F2384 File Offset: 0x000F0584
		private bool RewriteQuery(Tile<FragmentQuery> toFill, Tile<FragmentQuery> toAvoid, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes, bool isRelaxed)
		{
			notCoveredAttributes = new List<MemberPath>();
			FragmentQuery fragmentQuery = toFill.Query;
			if (this._context.TryGetCachedRewriting(fragmentQuery, out rewriting))
			{
				return true;
			}
			IEnumerable<Tile<FragmentQuery>> relevantViews = this.GetRelevantViews(fragmentQuery);
			FragmentQuery fragmentQuery2 = fragmentQuery;
			if (!this.RewriteQueryCached(QueryRewriter.CreateTile(FragmentQuery.Create(fragmentQuery.Condition)), toAvoid, relevantViews, out rewriting))
			{
				if (!isRelaxed)
				{
					return false;
				}
				fragmentQuery = FragmentQuery.Create(fragmentQuery.Attributes, BoolExpression.CreateAndNot(fragmentQuery.Condition, rewriting.Query.Condition));
				if (this._qp.IsEmpty(QueryRewriter.CreateTile(fragmentQuery)) || !this.RewriteQueryCached(QueryRewriter.CreateTile(FragmentQuery.Create(fragmentQuery.Condition)), toAvoid, relevantViews, out rewriting))
				{
					return false;
				}
			}
			if (fragmentQuery.Attributes.Count == 0)
			{
				return true;
			}
			Dictionary<MemberPath, FragmentQuery> dictionary = new Dictionary<MemberPath, FragmentQuery>();
			foreach (MemberPath memberPath in QueryRewriter.NonKeys(fragmentQuery.Attributes))
			{
				dictionary[memberPath] = fragmentQuery;
			}
			if (dictionary.Count == 0 || this.CoverAttributes(ref rewriting, dictionary))
			{
				this.GetUsedViewsAndRemoveTrueSurrogate(ref rewriting);
				this._context.SetCachedRewriting(fragmentQuery2, rewriting);
				return true;
			}
			if (isRelaxed)
			{
				foreach (MemberPath memberPath2 in QueryRewriter.NonKeys(fragmentQuery.Attributes))
				{
					FragmentQuery fragmentQuery3;
					if (dictionary.TryGetValue(memberPath2, out fragmentQuery3))
					{
						dictionary[memberPath2] = FragmentQuery.Create(BoolExpression.CreateAndNot(fragmentQuery.Condition, fragmentQuery3.Condition));
					}
					else
					{
						dictionary[memberPath2] = fragmentQuery;
					}
				}
				if (this.CoverAttributes(ref rewriting, dictionary))
				{
					this.GetUsedViewsAndRemoveTrueSurrogate(ref rewriting);
					this._context.SetCachedRewriting(fragmentQuery2, rewriting);
					return true;
				}
			}
			notCoveredAttributes = dictionary.Keys;
			return false;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000F2564 File Offset: 0x000F0764
		private bool RewriteQueryCached(Tile<FragmentQuery> toFill, Tile<FragmentQuery> toAvoid, IEnumerable<Tile<FragmentQuery>> views, out Tile<FragmentQuery> rewriting)
		{
			if (!this._context.TryGetCachedRewriting(toFill.Query, out rewriting))
			{
				bool flag = this._qp.RewriteQuery(toFill, toAvoid, views, out rewriting);
				if (flag)
				{
					this._context.SetCachedRewriting(toFill.Query, rewriting);
				}
				return flag;
			}
			return true;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x000F25A4 File Offset: 0x000F07A4
		private bool CoverAttributes(ref Tile<FragmentQuery> rewriting, Dictionary<MemberPath, FragmentQuery> attributeConditions)
		{
			foreach (FragmentQuery fragmentQuery in new HashSet<FragmentQuery>(rewriting.GetNamedQueries()))
			{
				foreach (MemberPath memberPath in QueryRewriter.NonKeys(fragmentQuery.Attributes))
				{
					this.CoverAttribute(memberPath, fragmentQuery, attributeConditions);
				}
				if (attributeConditions.Count == 0)
				{
					return true;
				}
			}
			Tile<FragmentQuery> tile = null;
			foreach (FragmentQuery fragmentQuery2 in this._fragmentQueries)
			{
				foreach (MemberPath memberPath2 in QueryRewriter.NonKeys(fragmentQuery2.Attributes))
				{
					if (this.CoverAttribute(memberPath2, fragmentQuery2, attributeConditions))
					{
						tile = ((tile == null) ? QueryRewriter.CreateTile(fragmentQuery2) : this._qp.Union(tile, QueryRewriter.CreateTile(fragmentQuery2)));
					}
				}
				if (attributeConditions.Count == 0)
				{
					break;
				}
			}
			if (attributeConditions.Count == 0)
			{
				rewriting = this._qp.Join(rewriting, tile);
				return true;
			}
			return false;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x000F271C File Offset: 0x000F091C
		private bool CoverAttribute(MemberPath projectedAttribute, FragmentQuery view, Dictionary<MemberPath, FragmentQuery> attributeConditions)
		{
			FragmentQuery fragmentQuery;
			if (attributeConditions.TryGetValue(projectedAttribute, out fragmentQuery))
			{
				fragmentQuery = FragmentQuery.Create(BoolExpression.CreateAndNot(fragmentQuery.Condition, view.Condition));
				if (this._qp.IsEmpty(QueryRewriter.CreateTile(fragmentQuery)))
				{
					attributeConditions.Remove(projectedAttribute);
				}
				else
				{
					attributeConditions[projectedAttribute] = fragmentQuery;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000F2774 File Offset: 0x000F0974
		private IEnumerable<Tile<FragmentQuery>> GetRelevantViews(FragmentQuery query)
		{
			Set<MemberPath> variables = QueryRewriter.GetVariables(query);
			Tile<FragmentQuery> tile = null;
			List<Tile<FragmentQuery>> list = new List<Tile<FragmentQuery>>();
			Tile<FragmentQuery> tile2 = null;
			foreach (Tile<FragmentQuery> tile3 in this._views)
			{
				if (QueryRewriter.GetVariables(tile3.Query).Overlaps(variables))
				{
					tile = ((tile == null) ? tile3 : this._qp.Union(tile, tile3));
					list.Add(tile3);
				}
				else if (this.IsTrue(tile3.Query) && tile2 == null)
				{
					tile2 = tile3;
				}
			}
			if (tile != null && this.IsTrue(tile.Query))
			{
				return list;
			}
			if (tile2 == null)
			{
				Tile<FragmentQuery> tile4 = null;
				foreach (FragmentQuery fragmentQuery in this._fragmentQueries)
				{
					tile4 = ((tile4 == null) ? QueryRewriter.CreateTile(fragmentQuery) : this._qp.Union(tile4, QueryRewriter.CreateTile(fragmentQuery)));
					if (this.IsTrue(tile4.Query))
					{
						tile2 = QueryRewriter._trueViewSurrogate;
						break;
					}
				}
			}
			if (tile2 != null)
			{
				list.Add(tile2);
				return list;
			}
			return this._views;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000F28C0 File Offset: 0x000F0AC0
		private HashSet<FragmentQuery> GetUsedViewsAndRemoveTrueSurrogate(ref Tile<FragmentQuery> rewriting)
		{
			HashSet<FragmentQuery> hashSet = new HashSet<FragmentQuery>(rewriting.GetNamedQueries());
			if (!hashSet.Contains(QueryRewriter._trueViewSurrogate.Query))
			{
				return hashSet;
			}
			hashSet.Remove(QueryRewriter._trueViewSurrogate.Query);
			Tile<FragmentQuery> tile = null;
			foreach (FragmentQuery fragmentQuery in hashSet.Concat(this._fragmentQueries))
			{
				tile = ((tile == null) ? QueryRewriter.CreateTile(fragmentQuery) : this._qp.Union(tile, QueryRewriter.CreateTile(fragmentQuery)));
				hashSet.Add(fragmentQuery);
				if (this.IsTrue(tile.Query))
				{
					rewriting = rewriting.Replace(QueryRewriter._trueViewSurrogate, tile);
					return hashSet;
				}
			}
			return hashSet;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000F298C File Offset: 0x000F0B8C
		private BoolExpression CreateMemberCondition(MemberPath path, Constant domainValue)
		{
			return FragmentQuery.CreateMemberCondition(path, domainValue, this._domainMap);
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000F299B File Offset: 0x000F0B9B
		private FragmentQuery CreateMemberConditionQuery(MemberPath currentPath, Constant domainValue)
		{
			return QueryRewriter.CreateMemberConditionQuery(currentPath, domainValue, this._keyAttributes, this._domainMap);
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x000F29B0 File Offset: 0x000F0BB0
		internal static FragmentQuery CreateMemberConditionQuery(MemberPath currentPath, Constant domainValue, IEnumerable<MemberPath> keyAttributes, MemberDomainMap domainMap)
		{
			BoolExpression boolExpression = FragmentQuery.CreateMemberCondition(currentPath, domainValue, domainMap);
			IEnumerable<MemberPath> enumerable = keyAttributes;
			if (domainValue is NegatedConstant)
			{
				enumerable = keyAttributes.Concat(new MemberPath[] { currentPath });
			}
			return FragmentQuery.Create(enumerable, boolExpression);
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x000F29E8 File Offset: 0x000F0BE8
		private static TileNamed<FragmentQuery> CreateTile(FragmentQuery query)
		{
			return new TileNamed<FragmentQuery>(query);
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000F29F0 File Offset: 0x000F0BF0
		private static IEnumerable<Constant> GetTypeConstants(IEnumerable<EdmType> types)
		{
			foreach (EdmType edmType in types)
			{
				yield return new TypeConstant(edmType);
			}
			IEnumerator<EdmType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x000F2A00 File Offset: 0x000F0C00
		private static IEnumerable<MemberPath> GetNonConditionalScalarMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(true), new bool?(false), null, domainMap);
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000F2A2C File Offset: 0x000F0C2C
		private static IEnumerable<MemberPath> GetConditionalComplexMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(false), new bool?(true), null, domainMap);
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000F2A58 File Offset: 0x000F0C58
		private static IEnumerable<MemberPath> GetNonConditionalComplexMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(false), new bool?(false), null, domainMap);
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000F2A84 File Offset: 0x000F0C84
		private static IEnumerable<MemberPath> GetConditionalScalarMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(true), new bool?(true), null, domainMap);
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x000F2AAE File Offset: 0x000F0CAE
		private static IEnumerable<MemberPath> NonKeys(IEnumerable<MemberPath> attributes)
		{
			return attributes.Where((MemberPath attr) => !attr.IsPartOfKey);
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x000F2AD8 File Offset: 0x000F0CD8
		internal static CellTreeNode TileToCellTree(Tile<FragmentQuery> tile, ViewgenContext context)
		{
			if (tile.OpKind == TileOpKind.Named)
			{
				FragmentQuery view = ((TileNamed<FragmentQuery>)tile).NamedQuery;
				LeftCellWrapper leftCellWrapper = context.AllWrappersForExtent.First((LeftCellWrapper w) => w.FragmentQuery == view);
				return new LeafCellTreeNode(context, leftCellWrapper);
			}
			CellTreeOpType cellTreeOpType;
			switch (tile.OpKind)
			{
			case TileOpKind.Union:
				cellTreeOpType = CellTreeOpType.Union;
				break;
			case TileOpKind.Join:
				cellTreeOpType = CellTreeOpType.IJ;
				break;
			case TileOpKind.AntiSemiJoin:
				cellTreeOpType = CellTreeOpType.LASJ;
				break;
			default:
				return null;
			}
			return new OpCellTreeNode(context, cellTreeOpType, new CellTreeNode[]
			{
				QueryRewriter.TileToCellTree(tile.Arg1, context),
				QueryRewriter.TileToCellTree(tile.Arg2, context)
			});
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000F2B7C File Offset: 0x000F0D7C
		private static BoolExpression TileToBoolExpr(Tile<FragmentQuery> tile)
		{
			switch (tile.OpKind)
			{
			case TileOpKind.Union:
				return BoolExpression.CreateOr(new BoolExpression[]
				{
					QueryRewriter.TileToBoolExpr(tile.Arg1),
					QueryRewriter.TileToBoolExpr(tile.Arg2)
				});
			case TileOpKind.Join:
				return BoolExpression.CreateAnd(new BoolExpression[]
				{
					QueryRewriter.TileToBoolExpr(tile.Arg1),
					QueryRewriter.TileToBoolExpr(tile.Arg2)
				});
			case TileOpKind.AntiSemiJoin:
				return BoolExpression.CreateAnd(new BoolExpression[]
				{
					QueryRewriter.TileToBoolExpr(tile.Arg1),
					BoolExpression.CreateNot(QueryRewriter.TileToBoolExpr(tile.Arg2))
				});
			case TileOpKind.Named:
			{
				FragmentQuery namedQuery = ((TileNamed<FragmentQuery>)tile).NamedQuery;
				if (namedQuery.Condition.IsAlwaysTrue())
				{
					return BoolExpression.True;
				}
				return namedQuery.FromVariable;
			}
			default:
				return null;
			}
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x000F2C4F File Offset: 0x000F0E4F
		private static bool IsDefaultValue(Constant domainValue, MemberPath path)
		{
			return (domainValue.IsNull() && path.IsNullable) || (path.DefaultValue != null && (domainValue as ScalarConstant).Value == path.DefaultValue);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x000F2C80 File Offset: 0x000F0E80
		private static Set<MemberPath> GetVariables(FragmentQuery query)
		{
			return new Set<MemberPath>(from domainConstraint in query.Condition.VariableConstraints
				where domainConstraint.Variable.Identifier is MemberRestriction && !domainConstraint.Variable.Domain.All((Constant constant) => domainConstraint.Range.Contains(constant))
				select ((MemberRestriction)domainConstraint.Variable.Identifier).RestrictedMemberSlot.MemberPath, MemberPath.EqualityComparer);
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000F2CEA File Offset: 0x000F0EEA
		private bool IsTrue(FragmentQuery query)
		{
			return !this._context.LeftFragmentQP.IsSatisfiable(FragmentQuery.Create(BoolExpression.CreateNot(query.Condition)));
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000F2D10 File Offset: 0x000F0F10
		[Conditional("DEBUG")]
		private void PrintStatistics(RewritingProcessor<Tile<FragmentQuery>> qp)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			qp.GetStatistics(out num, out num2, out num3, out num4, out num5);
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000F2D2D File Offset: 0x000F0F2D
		[Conditional("DEBUG")]
		internal void TraceVerbose(string msg, params object[] parameters)
		{
			if (this._config.IsVerboseTracing)
			{
				Helpers.FormatTraceLine(msg, parameters);
			}
		}

		// Token: 0x040018A0 RID: 6304
		private readonly MemberPath _extentPath;

		// Token: 0x040018A1 RID: 6305
		private readonly MemberDomainMap _domainMap;

		// Token: 0x040018A2 RID: 6306
		private readonly ConfigViewGenerator _config;

		// Token: 0x040018A3 RID: 6307
		private readonly CqlIdentifiers _identifiers;

		// Token: 0x040018A4 RID: 6308
		private readonly ViewgenContext _context;

		// Token: 0x040018A5 RID: 6309
		private readonly RewritingProcessor<Tile<FragmentQuery>> _qp;

		// Token: 0x040018A6 RID: 6310
		private readonly List<MemberPath> _keyAttributes;

		// Token: 0x040018A7 RID: 6311
		private readonly List<FragmentQuery> _fragmentQueries = new List<FragmentQuery>();

		// Token: 0x040018A8 RID: 6312
		private readonly List<Tile<FragmentQuery>> _views = new List<Tile<FragmentQuery>>();

		// Token: 0x040018A9 RID: 6313
		private readonly FragmentQuery _domainQuery;

		// Token: 0x040018AA RID: 6314
		private readonly EdmType _generatedType;

		// Token: 0x040018AB RID: 6315
		private readonly HashSet<FragmentQuery> _usedViews = new HashSet<FragmentQuery>();

		// Token: 0x040018AC RID: 6316
		private List<LeftCellWrapper> _usedCells = new List<LeftCellWrapper>();

		// Token: 0x040018AD RID: 6317
		private BoolExpression _topLevelWhereClause;

		// Token: 0x040018AE RID: 6318
		private CellTreeNode _basicView;

		// Token: 0x040018AF RID: 6319
		private Dictionary<MemberPath, CaseStatement> _caseStatements = new Dictionary<MemberPath, CaseStatement>();

		// Token: 0x040018B0 RID: 6320
		private readonly ErrorLog _errorLog = new ErrorLog();

		// Token: 0x040018B1 RID: 6321
		private readonly ViewGenMode _typesGenerationMode;

		// Token: 0x040018B2 RID: 6322
		private static readonly Tile<FragmentQuery> _trueViewSurrogate = QueryRewriter.CreateTile(FragmentQuery.Create(BoolExpression.True));
	}
}
