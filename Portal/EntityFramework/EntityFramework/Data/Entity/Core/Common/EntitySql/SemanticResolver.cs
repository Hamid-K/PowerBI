using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200066A RID: 1642
	internal sealed class SemanticResolver
	{
		// Token: 0x06004E8B RID: 20107 RVA: 0x0011E11A File Offset: 0x0011C31A
		internal static SemanticResolver Create(Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return new SemanticResolver(parserOptions, SemanticResolver.ProcessParameters(parameters, parserOptions), SemanticResolver.ProcessVariables(variables, parserOptions), new TypeResolver(perspective, parserOptions));
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x0011E137 File Offset: 0x0011C337
		internal SemanticResolver CloneForInlineFunctionConversion()
		{
			return new SemanticResolver(this._parserOptions, this._parameters, this._variables, this._typeResolver);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x0011E158 File Offset: 0x0011C358
		private SemanticResolver(ParserOptions parserOptions, Dictionary<string, DbParameterReferenceExpression> parameters, Dictionary<string, DbVariableReferenceExpression> variables, TypeResolver typeResolver)
		{
			this._parserOptions = parserOptions;
			this._parameters = parameters;
			this._variables = variables;
			this._typeResolver = typeResolver;
			this._scopeManager = new ScopeManager(this.NameComparer);
			this.EnterScopeRegion();
			foreach (DbVariableReferenceExpression dbVariableReferenceExpression in this._variables.Values)
			{
				this.CurrentScope.Add(dbVariableReferenceExpression.VariableName, new FreeVariableScopeEntry(dbVariableReferenceExpression));
			}
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x0011E208 File Offset: 0x0011C408
		private static Dictionary<string, DbParameterReferenceExpression> ProcessParameters(IEnumerable<DbParameterReferenceExpression> paramDefs, ParserOptions parserOptions)
		{
			Dictionary<string, DbParameterReferenceExpression> dictionary = new Dictionary<string, DbParameterReferenceExpression>(parserOptions.NameComparer);
			if (paramDefs != null)
			{
				foreach (DbParameterReferenceExpression dbParameterReferenceExpression in paramDefs)
				{
					if (dictionary.ContainsKey(dbParameterReferenceExpression.ParameterName))
					{
						throw new EntitySqlException(Strings.MultipleDefinitionsOfParameter(dbParameterReferenceExpression.ParameterName));
					}
					dictionary.Add(dbParameterReferenceExpression.ParameterName, dbParameterReferenceExpression);
				}
			}
			return dictionary;
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x0011E288 File Offset: 0x0011C488
		private static Dictionary<string, DbVariableReferenceExpression> ProcessVariables(IEnumerable<DbVariableReferenceExpression> varDefs, ParserOptions parserOptions)
		{
			Dictionary<string, DbVariableReferenceExpression> dictionary = new Dictionary<string, DbVariableReferenceExpression>(parserOptions.NameComparer);
			if (varDefs != null)
			{
				foreach (DbVariableReferenceExpression dbVariableReferenceExpression in varDefs)
				{
					if (dictionary.ContainsKey(dbVariableReferenceExpression.VariableName))
					{
						throw new EntitySqlException(Strings.MultipleDefinitionsOfVariable(dbVariableReferenceExpression.VariableName));
					}
					dictionary.Add(dbVariableReferenceExpression.VariableName, dbVariableReferenceExpression);
				}
			}
			return dictionary;
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x0011E308 File Offset: 0x0011C508
		internal Dictionary<string, DbParameterReferenceExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x0011E310 File Offset: 0x0011C510
		internal Dictionary<string, DbVariableReferenceExpression> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x0011E318 File Offset: 0x0011C518
		internal TypeResolver TypeResolver
		{
			get
			{
				return this._typeResolver;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x0011E320 File Offset: 0x0011C520
		internal ParserOptions ParserOptions
		{
			get
			{
				return this._parserOptions;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x0011E328 File Offset: 0x0011C528
		internal StringComparer NameComparer
		{
			get
			{
				return this._parserOptions.NameComparer;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x0011E335 File Offset: 0x0011C535
		internal IEnumerable<ScopeRegion> ScopeRegions
		{
			get
			{
				return this._scopeRegions;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x0011E33D File Offset: 0x0011C53D
		internal ScopeRegion CurrentScopeRegion
		{
			get
			{
				return this._scopeRegions[this._scopeRegions.Count - 1];
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x0011E357 File Offset: 0x0011C557
		internal Scope CurrentScope
		{
			get
			{
				return this._scopeManager.CurrentScope;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x0011E364 File Offset: 0x0011C564
		internal int CurrentScopeIndex
		{
			get
			{
				return this._scopeManager.CurrentScopeIndex;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x0011E371 File Offset: 0x0011C571
		internal GroupAggregateInfo CurrentGroupAggregateInfo
		{
			get
			{
				return this._currentGroupAggregateInfo;
			}
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0011E37C File Offset: 0x0011C57C
		private DbExpression GetExpressionFromScopeEntry(ScopeEntry scopeEntry, int scopeIndex, string varName, ErrorContext errCtx)
		{
			DbExpression dbExpression = scopeEntry.GetExpression(varName, errCtx);
			if (this._currentGroupAggregateInfo != null)
			{
				ScopeRegion definingScopeRegion = this.GetDefiningScopeRegion(scopeIndex);
				if (definingScopeRegion.ScopeRegionIndex <= this._currentGroupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex)
				{
					this._currentGroupAggregateInfo.UpdateScopeIndex(scopeIndex, this);
					IGroupExpressionExtendedInfo groupExpressionExtendedInfo = scopeEntry as IGroupExpressionExtendedInfo;
					if (groupExpressionExtendedInfo != null)
					{
						GroupAggregateInfo groupAggregateInfo = this._currentGroupAggregateInfo;
						while (groupAggregateInfo != null && groupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex >= definingScopeRegion.ScopeRegionIndex && groupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex != definingScopeRegion.ScopeRegionIndex)
						{
							groupAggregateInfo = groupAggregateInfo.ContainingAggregate;
						}
						if (groupAggregateInfo == null || groupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex < definingScopeRegion.ScopeRegionIndex)
						{
							groupAggregateInfo = this._currentGroupAggregateInfo;
						}
						switch (groupAggregateInfo.AggregateKind)
						{
						case GroupAggregateKind.Function:
							if (groupExpressionExtendedInfo.GroupVarBasedExpression != null)
							{
								dbExpression = groupExpressionExtendedInfo.GroupVarBasedExpression;
							}
							break;
						case GroupAggregateKind.Partition:
							if (groupExpressionExtendedInfo.GroupAggBasedExpression != null)
							{
								dbExpression = groupExpressionExtendedInfo.GroupAggBasedExpression;
							}
							break;
						}
					}
				}
			}
			return dbExpression;
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0011E471 File Offset: 0x0011C671
		internal IDisposable EnterIgnoreEntityContainerNameResolution()
		{
			this._ignoreEntityContainerNameResolution = true;
			return new Disposer(delegate
			{
				this._ignoreEntityContainerNameResolution = false;
			});
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0011E48C File Offset: 0x0011C68C
		internal ExpressionResolution ResolveSimpleName(string name, bool leftHandSideOfMemberAccess, ErrorContext errCtx)
		{
			ScopeEntry scopeEntry;
			int num;
			if (this.TryScopeLookup(name, out scopeEntry, out num))
			{
				if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar && ((SourceScopeEntry)scopeEntry).IsJoinClauseLeftExpr)
				{
					string invalidJoinLeftCorrelation = Strings.InvalidJoinLeftCorrelation;
					throw EntitySqlException.Create(errCtx, invalidJoinLeftCorrelation, null);
				}
				this.SetScopeRegionCorrelationFlag(num);
				return new ValueExpression(this.GetExpressionFromScopeEntry(scopeEntry, num, name, errCtx));
			}
			else
			{
				EntityContainer defaultContainer = this.TypeResolver.Perspective.GetDefaultContainer();
				ExpressionResolution expressionResolution;
				if (defaultContainer != null && this.TryResolveEntityContainerMemberAccess(defaultContainer, name, out expressionResolution))
				{
					return expressionResolution;
				}
				EntityContainer entityContainer;
				if (!this._ignoreEntityContainerNameResolution && this.TypeResolver.Perspective.TryGetEntityContainer(name, this._parserOptions.NameComparisonCaseInsensitive, out entityContainer))
				{
					return new EntityContainerExpression(entityContainer);
				}
				return this.TypeResolver.ResolveUnqualifiedName(name, leftHandSideOfMemberAccess, errCtx);
			}
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0011E544 File Offset: 0x0011C744
		internal MetadataMember ResolveSimpleFunctionName(string name, ErrorContext errCtx)
		{
			MetadataMember metadataMember = this.TypeResolver.ResolveUnqualifiedName(name, false, errCtx);
			if (metadataMember.MetadataMemberClass == MetadataMemberClass.Namespace)
			{
				EntityContainer defaultContainer = this.TypeResolver.Perspective.GetDefaultContainer();
				ExpressionResolution expressionResolution;
				if (defaultContainer != null && this.TryResolveEntityContainerMemberAccess(defaultContainer, name, out expressionResolution) && expressionResolution.ExpressionClass == ExpressionResolutionClass.MetadataMember)
				{
					metadataMember = (MetadataMember)expressionResolution;
				}
			}
			return metadataMember;
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0011E59C File Offset: 0x0011C79C
		private bool TryScopeLookup(string key, out ScopeEntry scopeEntry, out int scopeIndex)
		{
			scopeEntry = null;
			scopeIndex = -1;
			for (int i = this.CurrentScopeIndex; i >= 0; i--)
			{
				if (this._scopeManager.GetScopeByIndex(i).TryLookup(key, out scopeEntry))
				{
					scopeIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0011E5DB File Offset: 0x0011C7DB
		internal MetadataMember ResolveMetadataMemberName(string[] name, ErrorContext errCtx)
		{
			return this.TypeResolver.ResolveMetadataMemberName(name, errCtx);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0011E5EC File Offset: 0x0011C7EC
		internal ValueExpression ResolvePropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx)
		{
			DbExpression dbExpression;
			if (this.TryResolveAsPropertyAccess(valueExpr, name, out dbExpression))
			{
				return new ValueExpression(dbExpression);
			}
			if (this.TryResolveAsRefPropertyAccess(valueExpr, name, errCtx, out dbExpression))
			{
				return new ValueExpression(dbExpression);
			}
			if (TypeSemantics.IsCollectionType(valueExpr.ResultType))
			{
				string text = Strings.NotAMemberOfCollection(name, valueExpr.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, text, null);
			}
			string text2 = Strings.NotAMemberOfType(name, valueExpr.ResultType.EdmType.FullName);
			throw EntitySqlException.Create(errCtx, text2, null);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0011E66C File Offset: 0x0011C86C
		private bool TryResolveAsPropertyAccess(DbExpression valueExpr, string name, out DbExpression propertyExpr)
		{
			propertyExpr = null;
			EdmMember edmMember;
			if (Helper.IsStructuralType(valueExpr.ResultType.EdmType) && this.TypeResolver.Perspective.TryGetMember((StructuralType)valueExpr.ResultType.EdmType, name, this._parserOptions.NameComparisonCaseInsensitive, out edmMember))
			{
				propertyExpr = DbExpressionBuilder.CreatePropertyExpressionFromMember(valueExpr, edmMember);
				return true;
			}
			return false;
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0011E6CC File Offset: 0x0011C8CC
		private bool TryResolveAsRefPropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx, out DbExpression propertyExpr)
		{
			propertyExpr = null;
			if (!TypeSemantics.IsReferenceType(valueExpr.ResultType))
			{
				return false;
			}
			DbExpression dbExpression = valueExpr.Deref();
			TypeUsage resultType = dbExpression.ResultType;
			if (this.TryResolveAsPropertyAccess(dbExpression, name, out propertyExpr))
			{
				return true;
			}
			string text = Strings.InvalidDeRefProperty(name, resultType.EdmType.FullName, valueExpr.ResultType.EdmType.FullName);
			throw EntitySqlException.Create(errCtx, text, null);
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0011E734 File Offset: 0x0011C934
		internal ExpressionResolution ResolveEntityContainerMemberAccess(EntityContainer entityContainer, string name, ErrorContext errCtx)
		{
			ExpressionResolution expressionResolution;
			if (this.TryResolveEntityContainerMemberAccess(entityContainer, name, out expressionResolution))
			{
				return expressionResolution;
			}
			string text = Strings.MemberDoesNotBelongToEntityContainer(name, entityContainer.Name);
			throw EntitySqlException.Create(errCtx, text, null);
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0011E764 File Offset: 0x0011C964
		private bool TryResolveEntityContainerMemberAccess(EntityContainer entityContainer, string name, out ExpressionResolution resolution)
		{
			EntitySetBase entitySetBase;
			if (this.TypeResolver.Perspective.TryGetExtent(entityContainer, name, this._parserOptions.NameComparisonCaseInsensitive, out entitySetBase))
			{
				resolution = new ValueExpression(entitySetBase.Scan());
				return true;
			}
			EdmFunction edmFunction;
			if (this.TypeResolver.Perspective.TryGetFunctionImport(entityContainer, name, this._parserOptions.NameComparisonCaseInsensitive, out edmFunction))
			{
				resolution = new MetadataFunctionGroup(edmFunction.FullName, new EdmFunction[] { edmFunction });
				return true;
			}
			resolution = null;
			return false;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x0011E7DF File Offset: 0x0011C9DF
		internal MetadataMember ResolveMetadataMemberAccess(MetadataMember metadataMember, string name, ErrorContext errCtx)
		{
			return this.TypeResolver.ResolveMetadataMemberAccess(metadataMember, name, errCtx);
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0011E7F0 File Offset: 0x0011C9F0
		internal bool TryResolveInternalAggregateName(string name, ErrorContext errCtx, out DbExpression dbExpression)
		{
			ScopeEntry scopeEntry;
			int num;
			if (this.TryScopeLookup(name, out scopeEntry, out num))
			{
				this.SetScopeRegionCorrelationFlag(num);
				dbExpression = scopeEntry.GetExpression(name, errCtx);
				return true;
			}
			dbExpression = null;
			return false;
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0011E824 File Offset: 0x0011CA24
		internal bool TryResolveDotExprAsGroupKeyAlternativeName(DotExpr dotExpr, out ValueExpression groupKeyResolution)
		{
			groupKeyResolution = null;
			string[] array;
			ScopeEntry scopeEntry;
			int num;
			if (this.IsInAnyGroupScope() && dotExpr.IsMultipartIdentifier(out array) && this.TryScopeLookup(TypeResolver.GetFullName(array), out scopeEntry, out num))
			{
				IGetAlternativeName getAlternativeName = scopeEntry as IGetAlternativeName;
				if (getAlternativeName != null && getAlternativeName.AlternativeName != null && array.SequenceEqual(getAlternativeName.AlternativeName, this.NameComparer))
				{
					this.SetScopeRegionCorrelationFlag(num);
					groupKeyResolution = new ValueExpression(this.GetExpressionFromScopeEntry(scopeEntry, num, TypeResolver.GetFullName(array), dotExpr.ErrCtx));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x0011E8A4 File Offset: 0x0011CAA4
		internal string GenerateInternalName(string hint)
		{
			string text = "_##";
			uint namegenCounter = this._namegenCounter;
			this._namegenCounter = namegenCounter + 1U;
			return text + hint + namegenCounter.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0011E8D8 File Offset: 0x0011CAD8
		private string CreateNewAlias(DbExpression expr)
		{
			DbScanExpression dbScanExpression = expr as DbScanExpression;
			if (dbScanExpression != null)
			{
				return dbScanExpression.Target.Name;
			}
			DbPropertyExpression dbPropertyExpression = expr as DbPropertyExpression;
			if (dbPropertyExpression != null)
			{
				return dbPropertyExpression.Property.Name;
			}
			DbVariableReferenceExpression dbVariableReferenceExpression = expr as DbVariableReferenceExpression;
			if (dbVariableReferenceExpression != null)
			{
				return dbVariableReferenceExpression.VariableName;
			}
			return this.GenerateInternalName(string.Empty);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0011E930 File Offset: 0x0011CB30
		internal string InferAliasName(AliasedExpr aliasedExpr, DbExpression convertedExpression)
		{
			if (aliasedExpr.Alias != null)
			{
				return aliasedExpr.Alias.Name;
			}
			Identifier identifier = aliasedExpr.Expr as Identifier;
			if (identifier != null)
			{
				return identifier.Name;
			}
			DotExpr dotExpr = aliasedExpr.Expr as DotExpr;
			string[] array;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
			{
				return array[array.Length - 1];
			}
			return this.CreateNewAlias(convertedExpression);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0011E990 File Offset: 0x0011CB90
		internal IDisposable EnterScopeRegion()
		{
			this._scopeManager.EnterScope();
			ScopeRegion scopeRegion = new ScopeRegion(this._scopeManager, this.CurrentScopeIndex, this._scopeRegions.Count);
			this._scopeRegions.Add(scopeRegion);
			return new Disposer(delegate
			{
				this.CurrentScopeRegion.GroupAggregateInfos.Each(delegate(GroupAggregateInfo groupAggregateInfo)
				{
					groupAggregateInfo.DetachFromAstNode();
				});
				this.CurrentScopeRegion.RollbackAllScopes();
				this._scopeRegions.Remove(this.CurrentScopeRegion);
			});
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x0011E9E2 File Offset: 0x0011CBE2
		internal void RollbackToScope(int scopeIndex)
		{
			this._scopeManager.RollbackToScope(scopeIndex);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x0011E9F0 File Offset: 0x0011CBF0
		internal void EnterScope()
		{
			this._scopeManager.EnterScope();
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x0011E9FD File Offset: 0x0011CBFD
		internal void LeaveScope()
		{
			this._scopeManager.LeaveScope();
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x0011EA0C File Offset: 0x0011CC0C
		internal bool IsInAnyGroupScope()
		{
			for (int i = 0; i < this._scopeRegions.Count; i++)
			{
				if (this._scopeRegions[i].IsAggregating)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x0011EA48 File Offset: 0x0011CC48
		internal ScopeRegion GetDefiningScopeRegion(int scopeIndex)
		{
			for (int i = this._scopeRegions.Count - 1; i >= 0; i--)
			{
				if (this._scopeRegions[i].ContainsScope(scopeIndex))
				{
					return this._scopeRegions[i];
				}
			}
			return null;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x0011EA8F File Offset: 0x0011CC8F
		private void SetScopeRegionCorrelationFlag(int scopeIndex)
		{
			this.GetDefiningScopeRegion(scopeIndex).WasResolutionCorrelated = true;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0011EA9E File Offset: 0x0011CC9E
		internal IDisposable EnterFunctionAggregate(MethodExpr methodExpr, ErrorContext errCtx, out FunctionAggregateInfo aggregateInfo)
		{
			aggregateInfo = new FunctionAggregateInfo(methodExpr, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0011EABD File Offset: 0x0011CCBD
		internal IDisposable EnterGroupPartition(GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, out GroupPartitionInfo aggregateInfo)
		{
			aggregateInfo = new GroupPartitionInfo(groupPartitionExpr, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x0011EADC File Offset: 0x0011CCDC
		internal IDisposable EnterGroupKeyDefinition(GroupAggregateKind aggregateKind, ErrorContext errCtx, out GroupKeyAggregateInfo aggregateInfo)
		{
			aggregateInfo = new GroupKeyAggregateInfo(aggregateKind, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x0011EAFC File Offset: 0x0011CCFC
		private IDisposable EnterGroupAggregate(GroupAggregateInfo aggregateInfo)
		{
			this._currentGroupAggregateInfo = aggregateInfo;
			return new Disposer(delegate
			{
				this._currentGroupAggregateInfo = aggregateInfo.ContainingAggregate;
				aggregateInfo.ValidateAndComputeEvaluatingScopeRegion(this);
			});
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0011EB3A File Offset: 0x0011CD3A
		internal static EdmFunction ResolveFunctionOverloads(IList<EdmFunction> functionsMetadata, IList<TypeUsage> argTypes, bool isGroupAggregateFunction, out bool isAmbiguous)
		{
			return FunctionOverloadResolver.ResolveFunctionOverloads(functionsMetadata, argTypes, new Func<TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenArgumentType), new Func<TypeUsage, TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenParameterType), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsPromotableTo), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsStructurallyEqual), isGroupAggregateFunction, out isAmbiguous);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0011EB78 File Offset: 0x0011CD78
		internal static TFunctionMetadata ResolveFunctionOverloads<TFunctionMetadata, TFunctionParameterMetadata>(IList<TFunctionMetadata> functionsMetadata, IList<TypeUsage> argTypes, Func<TFunctionMetadata, IList<TFunctionParameterMetadata>> getSignatureParams, Func<TFunctionParameterMetadata, TypeUsage> getParameterTypeUsage, Func<TFunctionParameterMetadata, ParameterMode> getParameterMode, bool isGroupAggregateFunction, out bool isAmbiguous) where TFunctionMetadata : class
		{
			return FunctionOverloadResolver.ResolveFunctionOverloads<TFunctionMetadata, TFunctionParameterMetadata>(functionsMetadata, argTypes, getSignatureParams, getParameterTypeUsage, getParameterMode, new Func<TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenArgumentType), new Func<TypeUsage, TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenParameterType), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsPromotableTo), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsStructurallyEqual), isGroupAggregateFunction, out isAmbiguous);
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0011EBC4 File Offset: 0x0011CDC4
		private static IEnumerable<TypeUsage> UntypedNullAwareFlattenArgumentType(TypeUsage argType)
		{
			if (argType == null)
			{
				return new TypeUsage[1];
			}
			return TypeSemantics.FlattenType(argType);
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x0011EBE4 File Offset: 0x0011CDE4
		private static IEnumerable<TypeUsage> UntypedNullAwareFlattenParameterType(TypeUsage paramType, TypeUsage argType)
		{
			if (argType == null)
			{
				return new TypeUsage[] { paramType };
			}
			return TypeSemantics.FlattenType(paramType);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0011EC07 File Offset: 0x0011CE07
		private static bool UntypedNullAwareIsPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			if (fromType == null)
			{
				return !Helper.IsCollectionType(toType.EdmType);
			}
			return TypeSemantics.IsPromotableTo(fromType, toType);
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0011EC22 File Offset: 0x0011CE22
		private static bool UntypedNullAwareIsStructurallyEqual(TypeUsage fromType, TypeUsage toType)
		{
			if (fromType == null)
			{
				return SemanticResolver.UntypedNullAwareIsPromotableTo(fromType, toType);
			}
			return TypeSemantics.IsStructurallyEqual(fromType, toType);
		}

		// Token: 0x04001C6C RID: 7276
		private readonly ParserOptions _parserOptions;

		// Token: 0x04001C6D RID: 7277
		private readonly Dictionary<string, DbParameterReferenceExpression> _parameters;

		// Token: 0x04001C6E RID: 7278
		private readonly Dictionary<string, DbVariableReferenceExpression> _variables;

		// Token: 0x04001C6F RID: 7279
		private readonly TypeResolver _typeResolver;

		// Token: 0x04001C70 RID: 7280
		private readonly ScopeManager _scopeManager;

		// Token: 0x04001C71 RID: 7281
		private readonly List<ScopeRegion> _scopeRegions = new List<ScopeRegion>();

		// Token: 0x04001C72 RID: 7282
		private bool _ignoreEntityContainerNameResolution;

		// Token: 0x04001C73 RID: 7283
		private GroupAggregateInfo _currentGroupAggregateInfo;

		// Token: 0x04001C74 RID: 7284
		private uint _namegenCounter;
	}
}
