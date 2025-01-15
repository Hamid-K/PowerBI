using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000561 RID: 1377
	internal static class ViewValidator
	{
		// Token: 0x0600432C RID: 17196 RVA: 0x000E6F58 File Offset: 0x000E5158
		internal static IEnumerable<EdmSchemaError> ValidateQueryView(DbQueryCommandTree view, EntitySetBaseMapping setMapping, EntityTypeBase elementType, bool includeSubtypes)
		{
			ViewValidator.ViewExpressionValidator viewExpressionValidator = new ViewValidator.ViewExpressionValidator(setMapping, elementType, includeSubtypes);
			viewExpressionValidator.VisitExpression(view.Query);
			if (viewExpressionValidator.Errors.Count<EdmSchemaError>() == 0 && setMapping.Set.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
			{
				ViewValidator.AssociationSetViewValidator associationSetViewValidator = new ViewValidator.AssociationSetViewValidator(setMapping);
				associationSetViewValidator.VisitExpression(view.Query);
				return associationSetViewValidator.Errors;
			}
			return viewExpressionValidator.Errors;
		}

		// Token: 0x02000B69 RID: 2921
		private sealed class ViewExpressionValidator : BasicExpressionVisitor
		{
			// Token: 0x170010F8 RID: 4344
			// (get) Token: 0x060065DB RID: 26075 RVA: 0x0015E54A File Offset: 0x0015C74A
			private EdmItemCollection EdmItemCollection
			{
				get
				{
					return this._setMapping.EntityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
				}
			}

			// Token: 0x170010F9 RID: 4345
			// (get) Token: 0x060065DC RID: 26076 RVA: 0x0015E561 File Offset: 0x0015C761
			private StoreItemCollection StoreItemCollection
			{
				get
				{
					return this._setMapping.EntityContainerMapping.StorageMappingItemCollection.StoreItemCollection;
				}
			}

			// Token: 0x060065DD RID: 26077 RVA: 0x0015E578 File Offset: 0x0015C778
			internal ViewExpressionValidator(EntitySetBaseMapping setMapping, EntityTypeBase elementType, bool includeSubtypes)
			{
				this._setMapping = setMapping;
				this._elementType = elementType;
				this._includeSubtypes = includeSubtypes;
				this._errors = new List<EdmSchemaError>();
			}

			// Token: 0x170010FA RID: 4346
			// (get) Token: 0x060065DE RID: 26078 RVA: 0x0015E5A0 File Offset: 0x0015C7A0
			internal IEnumerable<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x060065DF RID: 26079 RVA: 0x0015E5A8 File Offset: 0x0015C7A8
			public override void VisitExpression(DbExpression expression)
			{
				Check.NotNull<DbExpression>(expression, "expression");
				this.ValidateExpressionKind(expression.ExpressionKind);
				base.VisitExpression(expression);
			}

			// Token: 0x060065E0 RID: 26080 RVA: 0x0015E5CC File Offset: 0x0015C7CC
			private void ValidateExpressionKind(DbExpressionKind expressionKind)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.And:
				case DbExpressionKind.Case:
				case DbExpressionKind.Cast:
				case DbExpressionKind.Constant:
				case DbExpressionKind.EntityRef:
				case DbExpressionKind.Equals:
				case DbExpressionKind.Filter:
				case DbExpressionKind.FullOuterJoin:
				case DbExpressionKind.Function:
				case DbExpressionKind.GreaterThan:
				case DbExpressionKind.GreaterThanOrEquals:
				case DbExpressionKind.InnerJoin:
				case DbExpressionKind.IsNull:
				case DbExpressionKind.LeftOuterJoin:
				case DbExpressionKind.LessThan:
				case DbExpressionKind.LessThanOrEquals:
				case DbExpressionKind.NewInstance:
				case DbExpressionKind.Not:
				case DbExpressionKind.NotEquals:
				case DbExpressionKind.Null:
				case DbExpressionKind.Or:
				case DbExpressionKind.Project:
				case DbExpressionKind.Property:
				case DbExpressionKind.Ref:
					return;
				case DbExpressionKind.Any:
				case DbExpressionKind.CrossApply:
				case DbExpressionKind.CrossJoin:
				case DbExpressionKind.Deref:
				case DbExpressionKind.Distinct:
				case DbExpressionKind.Divide:
				case DbExpressionKind.Element:
				case DbExpressionKind.Except:
				case DbExpressionKind.GroupBy:
				case DbExpressionKind.Intersect:
				case DbExpressionKind.IsEmpty:
				case DbExpressionKind.IsOf:
				case DbExpressionKind.IsOfOnly:
				case DbExpressionKind.Like:
				case DbExpressionKind.Limit:
				case DbExpressionKind.Minus:
				case DbExpressionKind.Modulo:
				case DbExpressionKind.Multiply:
				case DbExpressionKind.OfType:
				case DbExpressionKind.OfTypeOnly:
				case DbExpressionKind.OuterApply:
				case DbExpressionKind.ParameterReference:
				case DbExpressionKind.Plus:
					break;
				default:
					if (expressionKind == DbExpressionKind.Scan || expressionKind - DbExpressionKind.UnionAll <= 1)
					{
						return;
					}
					break;
				}
				string text;
				if (!this._includeSubtypes)
				{
					text = this._elementType.ToString();
				}
				else
				{
					string text2 = "IsTypeOf(";
					EntityTypeBase elementType = this._elementType;
					text = text2 + ((elementType != null) ? elementType.ToString() : null) + ")";
				}
				string text3 = text;
				this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedExpressionKind_QueryView(this._setMapping.Set.Name, text3, expressionKind), 2071, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
			}

			// Token: 0x060065E1 RID: 26081 RVA: 0x0015E740 File Offset: 0x0015C940
			public override void Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				base.Visit(expression);
				if (expression.Property.BuiltInTypeKind != BuiltInTypeKind.EdmProperty)
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedPropertyKind_QueryView(this._setMapping.Set.Name, expression.Property.Name, expression.Property.BuiltInTypeKind), 2073, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x060065E2 RID: 26082 RVA: 0x0015E7DC File Offset: 0x0015C9DC
			public override void Visit(DbNewInstanceExpression expression)
			{
				Check.NotNull<DbNewInstanceExpression>(expression, "expression");
				base.Visit(expression);
				EdmType edmType = expression.ResultType.EdmType;
				if (edmType.BuiltInTypeKind != BuiltInTypeKind.RowType && edmType != this._elementType && (!this._includeSubtypes || !this._elementType.IsAssignableFrom(edmType)) && (edmType.BuiltInTypeKind != BuiltInTypeKind.ComplexType || !this.GetComplexTypes().Contains((ComplexType)edmType)))
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedInitialization_QueryView(this._setMapping.Set.Name, edmType.FullName), 2074, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x060065E3 RID: 26083 RVA: 0x0015E8AC File Offset: 0x0015CAAC
			private IEnumerable<ComplexType> GetComplexTypes()
			{
				IEnumerable<EdmProperty> enumerable = this.GetEntityTypes().SelectMany((EntityType entityType) => entityType.Properties).Distinct<EdmProperty>();
				return this.GetComplexTypes(enumerable);
			}

			// Token: 0x060065E4 RID: 26084 RVA: 0x0015E8F0 File Offset: 0x0015CAF0
			private IEnumerable<ComplexType> GetComplexTypes(IEnumerable<EdmProperty> properties)
			{
				foreach (ComplexType complexType in properties.Select((EdmProperty p) => p.TypeUsage.EdmType).OfType<ComplexType>())
				{
					yield return complexType;
					foreach (ComplexType complexType2 in this.GetComplexTypes(complexType.Properties))
					{
						yield return complexType2;
					}
					IEnumerator<ComplexType> enumerator2 = null;
					complexType = null;
				}
				IEnumerator<ComplexType> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060065E5 RID: 26085 RVA: 0x0015E908 File Offset: 0x0015CB08
			private IEnumerable<EntityType> GetEntityTypes()
			{
				if (this._includeSubtypes)
				{
					return MetadataHelper.GetTypeAndSubtypesOf(this._elementType, this.EdmItemCollection, true).OfType<EntityType>();
				}
				if (this._elementType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
				{
					return Enumerable.Repeat<EntityType>((EntityType)this._elementType, 1);
				}
				return Enumerable.Empty<EntityType>();
			}

			// Token: 0x060065E6 RID: 26086 RVA: 0x0015E95C File Offset: 0x0015CB5C
			public override void Visit(DbFunctionExpression expression)
			{
				Check.NotNull<DbFunctionExpression>(expression, "expression");
				base.Visit(expression);
				if (!ViewValidator.ViewExpressionValidator.IsStoreSpaceOrCanonicalFunction(this.StoreItemCollection, expression.Function))
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedFunctionCall_QueryView(this._setMapping.Set.Name, expression.Function.Identity), 2112, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x060065E7 RID: 26087 RVA: 0x0015E9EB File Offset: 0x0015CBEB
			internal static bool IsStoreSpaceOrCanonicalFunction(StoreItemCollection sSpace, EdmFunction function)
			{
				return TypeHelpers.IsCanonicalFunction(function) || sSpace.GetCTypeFunctions(function.FullName, false).Contains(function);
			}

			// Token: 0x060065E8 RID: 26088 RVA: 0x0015EA0C File Offset: 0x0015CC0C
			public override void Visit(DbScanExpression expression)
			{
				Check.NotNull<DbScanExpression>(expression, "expression");
				base.Visit(expression);
				EntitySetBase target = expression.Target;
				if (target.EntityContainer.DataSpace != DataSpace.SSpace)
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedScanTarget_QueryView(this._setMapping.Set.Name, target.Name), 2072, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x04002DBD RID: 11709
			private readonly EntitySetBaseMapping _setMapping;

			// Token: 0x04002DBE RID: 11710
			private readonly List<EdmSchemaError> _errors;

			// Token: 0x04002DBF RID: 11711
			private readonly EntityTypeBase _elementType;

			// Token: 0x04002DC0 RID: 11712
			private readonly bool _includeSubtypes;
		}

		// Token: 0x02000B6A RID: 2922
		private class AssociationSetViewValidator : DbExpressionVisitor<ViewValidator.DbExpressionEntitySetInfo>
		{
			// Token: 0x060065E9 RID: 26089 RVA: 0x0015EA98 File Offset: 0x0015CC98
			internal AssociationSetViewValidator(EntitySetBaseMapping setMapping)
			{
				this._setMapping = setMapping;
			}

			// Token: 0x170010FB RID: 4347
			// (get) Token: 0x060065EA RID: 26090 RVA: 0x0015EABD File Offset: 0x0015CCBD
			internal List<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x060065EB RID: 26091 RVA: 0x0015EAC5 File Offset: 0x0015CCC5
			internal ViewValidator.DbExpressionEntitySetInfo VisitExpression(DbExpression expression)
			{
				return expression.Accept<ViewValidator.DbExpressionEntitySetInfo>(this);
			}

			// Token: 0x060065EC RID: 26092 RVA: 0x0015EACE File Offset: 0x0015CCCE
			private ViewValidator.DbExpressionEntitySetInfo VisitExpressionBinding(DbExpressionBinding binding)
			{
				if (binding != null)
				{
					return this.VisitExpression(binding.Expression);
				}
				return null;
			}

			// Token: 0x060065ED RID: 26093 RVA: 0x0015EAE4 File Offset: 0x0015CCE4
			private void VisitExpressionBindingEnterScope(DbExpressionBinding binding)
			{
				ViewValidator.DbExpressionEntitySetInfo dbExpressionEntitySetInfo = this.VisitExpressionBinding(binding);
				this.variableScopes.Push(new KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>(binding.VariableName, dbExpressionEntitySetInfo));
			}

			// Token: 0x060065EE RID: 26094 RVA: 0x0015EB10 File Offset: 0x0015CD10
			private void VisitExpressionBindingExitScope()
			{
				this.variableScopes.Pop();
			}

			// Token: 0x060065EF RID: 26095 RVA: 0x0015EB20 File Offset: 0x0015CD20
			private void ValidateEntitySetsMappedForAssociationSetMapping(ViewValidator.DbExpressionStructuralTypeEntitySetInfo setInfos)
			{
				AssociationSet associationSet = this._setMapping.Set as AssociationSet;
				int num = 0;
				if (setInfos.SetInfos.All((KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo> it) => it.Value != null && it.Value is ViewValidator.DbExpressionSimpleTypeEntitySetInfo) && setInfos.SetInfos.Count<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>>() == 2)
				{
					foreach (ViewValidator.DbExpressionEntitySetInfo dbExpressionEntitySetInfo in setInfos.SetInfos.Select((KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo> it) => it.Value))
					{
						ViewValidator.DbExpressionSimpleTypeEntitySetInfo dbExpressionSimpleTypeEntitySetInfo = (ViewValidator.DbExpressionSimpleTypeEntitySetInfo)dbExpressionEntitySetInfo;
						AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[num];
						EntitySet entitySet = associationSetEnd.EntitySet;
						if (!entitySet.Equals(dbExpressionSimpleTypeEntitySetInfo.EntitySet))
						{
							this._errors.Add(new EdmSchemaError(Strings.Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView(dbExpressionSimpleTypeEntitySetInfo.EntitySet.Name, entitySet.Name, associationSetEnd.Name, this._setMapping.Set.Name), 2074, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
						}
						num++;
					}
				}
			}

			// Token: 0x060065F0 RID: 26096 RVA: 0x0015EC7C File Offset: 0x0015CE7C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbExpression expression)
			{
				Check.NotNull<DbExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065F1 RID: 26097 RVA: 0x0015EC8C File Offset: 0x0015CE8C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbVariableReferenceExpression expression)
			{
				Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
				return (from it in this.variableScopes
					where it.Key == expression.VariableName
					select it.Value).FirstOrDefault<ViewValidator.DbExpressionEntitySetInfo>();
			}

			// Token: 0x060065F2 RID: 26098 RVA: 0x0015ECF8 File Offset: 0x0015CEF8
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				ViewValidator.DbExpressionStructuralTypeEntitySetInfo dbExpressionStructuralTypeEntitySetInfo = this.VisitExpression(expression.Instance) as ViewValidator.DbExpressionStructuralTypeEntitySetInfo;
				if (dbExpressionStructuralTypeEntitySetInfo != null)
				{
					return dbExpressionStructuralTypeEntitySetInfo.GetEntitySetInfoForMember(expression.Property.Name);
				}
				return null;
			}

			// Token: 0x060065F3 RID: 26099 RVA: 0x0015ED39 File Offset: 0x0015CF39
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbProjectExpression expression)
			{
				Check.NotNull<DbProjectExpression>(expression, "expression");
				this.VisitExpressionBindingEnterScope(expression.Input);
				ViewValidator.DbExpressionEntitySetInfo dbExpressionEntitySetInfo = this.VisitExpression(expression.Projection);
				this.VisitExpressionBindingExitScope();
				return dbExpressionEntitySetInfo;
			}

			// Token: 0x060065F4 RID: 26100 RVA: 0x0015ED68 File Offset: 0x0015CF68
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNewInstanceExpression expression)
			{
				Check.NotNull<DbNewInstanceExpression>(expression, "expression");
				ViewValidator.DbExpressionMemberCollectionEntitySetInfo dbExpressionMemberCollectionEntitySetInfo = this.VisitExpressionList(expression.Arguments);
				StructuralType structuralType = expression.ResultType.EdmType as StructuralType;
				if (dbExpressionMemberCollectionEntitySetInfo != null && structuralType != null)
				{
					ViewValidator.DbExpressionStructuralTypeEntitySetInfo dbExpressionStructuralTypeEntitySetInfo = new ViewValidator.DbExpressionStructuralTypeEntitySetInfo();
					int num = 0;
					foreach (ViewValidator.DbExpressionEntitySetInfo dbExpressionEntitySetInfo in dbExpressionMemberCollectionEntitySetInfo.entitySetInfos)
					{
						dbExpressionStructuralTypeEntitySetInfo.Add(structuralType.Members[num].Name, dbExpressionEntitySetInfo);
						num++;
					}
					if (expression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.AssociationType)
					{
						this.ValidateEntitySetsMappedForAssociationSetMapping(dbExpressionStructuralTypeEntitySetInfo);
					}
					return dbExpressionStructuralTypeEntitySetInfo;
				}
				return null;
			}

			// Token: 0x060065F5 RID: 26101 RVA: 0x0015EE28 File Offset: 0x0015D028
			private ViewValidator.DbExpressionMemberCollectionEntitySetInfo VisitExpressionList(IList<DbExpression> list)
			{
				return new ViewValidator.DbExpressionMemberCollectionEntitySetInfo(list.Select((DbExpression it) => this.VisitExpression(it)));
			}

			// Token: 0x060065F6 RID: 26102 RVA: 0x0015EE41 File Offset: 0x0015D041
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRefExpression expression)
			{
				Check.NotNull<DbRefExpression>(expression, "expression");
				return new ViewValidator.DbExpressionSimpleTypeEntitySetInfo(expression.EntitySet);
			}

			// Token: 0x060065F7 RID: 26103 RVA: 0x0015EE5A File Offset: 0x0015D05A
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbComparisonExpression expression)
			{
				Check.NotNull<DbComparisonExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065F8 RID: 26104 RVA: 0x0015EE69 File Offset: 0x0015D069
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLikeExpression expression)
			{
				Check.NotNull<DbLikeExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065F9 RID: 26105 RVA: 0x0015EE78 File Offset: 0x0015D078
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLimitExpression expression)
			{
				Check.NotNull<DbLimitExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065FA RID: 26106 RVA: 0x0015EE87 File Offset: 0x0015D087
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsNullExpression expression)
			{
				Check.NotNull<DbIsNullExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065FB RID: 26107 RVA: 0x0015EE96 File Offset: 0x0015D096
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbArithmeticExpression expression)
			{
				Check.NotNull<DbArithmeticExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065FC RID: 26108 RVA: 0x0015EEA5 File Offset: 0x0015D0A5
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbAndExpression expression)
			{
				Check.NotNull<DbAndExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065FD RID: 26109 RVA: 0x0015EEB4 File Offset: 0x0015D0B4
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbOrExpression expression)
			{
				Check.NotNull<DbOrExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065FE RID: 26110 RVA: 0x0015EEC3 File Offset: 0x0015D0C3
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbInExpression expression)
			{
				Check.NotNull<DbInExpression>(expression, "expression");
				return null;
			}

			// Token: 0x060065FF RID: 26111 RVA: 0x0015EED2 File Offset: 0x0015D0D2
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNotExpression expression)
			{
				Check.NotNull<DbNotExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006600 RID: 26112 RVA: 0x0015EEE1 File Offset: 0x0015D0E1
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbDistinctExpression expression)
			{
				Check.NotNull<DbDistinctExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006601 RID: 26113 RVA: 0x0015EEF0 File Offset: 0x0015D0F0
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbElementExpression expression)
			{
				Check.NotNull<DbElementExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006602 RID: 26114 RVA: 0x0015EEFF File Offset: 0x0015D0FF
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsEmptyExpression expression)
			{
				Check.NotNull<DbIsEmptyExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006603 RID: 26115 RVA: 0x0015EF0E File Offset: 0x0015D10E
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbUnionAllExpression expression)
			{
				Check.NotNull<DbUnionAllExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006604 RID: 26116 RVA: 0x0015EF1D File Offset: 0x0015D11D
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIntersectExpression expression)
			{
				Check.NotNull<DbIntersectExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006605 RID: 26117 RVA: 0x0015EF2C File Offset: 0x0015D12C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbExceptExpression expression)
			{
				Check.NotNull<DbExceptExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006606 RID: 26118 RVA: 0x0015EF3B File Offset: 0x0015D13B
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbTreatExpression expression)
			{
				Check.NotNull<DbTreatExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006607 RID: 26119 RVA: 0x0015EF4A File Offset: 0x0015D14A
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsOfExpression expression)
			{
				Check.NotNull<DbIsOfExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006608 RID: 26120 RVA: 0x0015EF59 File Offset: 0x0015D159
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCastExpression expression)
			{
				Check.NotNull<DbCastExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006609 RID: 26121 RVA: 0x0015EF68 File Offset: 0x0015D168
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCaseExpression expression)
			{
				Check.NotNull<DbCaseExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600660A RID: 26122 RVA: 0x0015EF77 File Offset: 0x0015D177
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbOfTypeExpression expression)
			{
				Check.NotNull<DbOfTypeExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600660B RID: 26123 RVA: 0x0015EF86 File Offset: 0x0015D186
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRelationshipNavigationExpression expression)
			{
				Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600660C RID: 26124 RVA: 0x0015EF95 File Offset: 0x0015D195
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbDerefExpression expression)
			{
				Check.NotNull<DbDerefExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600660D RID: 26125 RVA: 0x0015EFA4 File Offset: 0x0015D1A4
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRefKeyExpression expression)
			{
				Check.NotNull<DbRefKeyExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600660E RID: 26126 RVA: 0x0015EFB3 File Offset: 0x0015D1B3
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbEntityRefExpression expression)
			{
				Check.NotNull<DbEntityRefExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600660F RID: 26127 RVA: 0x0015EFC2 File Offset: 0x0015D1C2
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbScanExpression expression)
			{
				Check.NotNull<DbScanExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006610 RID: 26128 RVA: 0x0015EFD1 File Offset: 0x0015D1D1
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbFilterExpression expression)
			{
				Check.NotNull<DbFilterExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006611 RID: 26129 RVA: 0x0015EFE0 File Offset: 0x0015D1E0
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbConstantExpression expression)
			{
				Check.NotNull<DbConstantExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006612 RID: 26130 RVA: 0x0015EFEF File Offset: 0x0015D1EF
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNullExpression expression)
			{
				Check.NotNull<DbNullExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006613 RID: 26131 RVA: 0x0015EFFE File Offset: 0x0015D1FE
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCrossJoinExpression expression)
			{
				Check.NotNull<DbCrossJoinExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006614 RID: 26132 RVA: 0x0015F00D File Offset: 0x0015D20D
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbJoinExpression expression)
			{
				Check.NotNull<DbJoinExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006615 RID: 26133 RVA: 0x0015F01C File Offset: 0x0015D21C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbParameterReferenceExpression expression)
			{
				Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006616 RID: 26134 RVA: 0x0015F02B File Offset: 0x0015D22B
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbFunctionExpression expression)
			{
				Check.NotNull<DbFunctionExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006617 RID: 26135 RVA: 0x0015F03A File Offset: 0x0015D23A
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLambdaExpression expression)
			{
				Check.NotNull<DbLambdaExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006618 RID: 26136 RVA: 0x0015F049 File Offset: 0x0015D249
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbApplyExpression expression)
			{
				Check.NotNull<DbApplyExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06006619 RID: 26137 RVA: 0x0015F058 File Offset: 0x0015D258
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbGroupByExpression expression)
			{
				Check.NotNull<DbGroupByExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600661A RID: 26138 RVA: 0x0015F067 File Offset: 0x0015D267
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbSkipExpression expression)
			{
				Check.NotNull<DbSkipExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600661B RID: 26139 RVA: 0x0015F076 File Offset: 0x0015D276
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbSortExpression expression)
			{
				Check.NotNull<DbSortExpression>(expression, "expression");
				return null;
			}

			// Token: 0x0600661C RID: 26140 RVA: 0x0015F085 File Offset: 0x0015D285
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbQuantifierExpression expression)
			{
				Check.NotNull<DbQuantifierExpression>(expression, "expression");
				return null;
			}

			// Token: 0x04002DC1 RID: 11713
			private readonly Stack<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>> variableScopes = new Stack<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>>();

			// Token: 0x04002DC2 RID: 11714
			private readonly EntitySetBaseMapping _setMapping;

			// Token: 0x04002DC3 RID: 11715
			private readonly List<EdmSchemaError> _errors = new List<EdmSchemaError>();
		}

		// Token: 0x02000B6B RID: 2923
		internal abstract class DbExpressionEntitySetInfo
		{
		}

		// Token: 0x02000B6C RID: 2924
		private class DbExpressionSimpleTypeEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x170010FC RID: 4348
			// (get) Token: 0x0600661F RID: 26143 RVA: 0x0015F0A5 File Offset: 0x0015D2A5
			internal EntitySet EntitySet
			{
				get
				{
					return this.m_entitySet;
				}
			}

			// Token: 0x06006620 RID: 26144 RVA: 0x0015F0AD File Offset: 0x0015D2AD
			internal DbExpressionSimpleTypeEntitySetInfo(EntitySet entitySet)
			{
				this.m_entitySet = entitySet;
			}

			// Token: 0x04002DC4 RID: 11716
			private readonly EntitySet m_entitySet;
		}

		// Token: 0x02000B6D RID: 2925
		private class DbExpressionStructuralTypeEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x06006621 RID: 26145 RVA: 0x0015F0BC File Offset: 0x0015D2BC
			internal DbExpressionStructuralTypeEntitySetInfo()
			{
				this.m_entitySetInfos = new Dictionary<string, ViewValidator.DbExpressionEntitySetInfo>();
			}

			// Token: 0x06006622 RID: 26146 RVA: 0x0015F0CF File Offset: 0x0015D2CF
			internal void Add(string key, ViewValidator.DbExpressionEntitySetInfo value)
			{
				this.m_entitySetInfos.Add(key, value);
			}

			// Token: 0x170010FD RID: 4349
			// (get) Token: 0x06006623 RID: 26147 RVA: 0x0015F0DE File Offset: 0x0015D2DE
			internal IEnumerable<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>> SetInfos
			{
				get
				{
					return this.m_entitySetInfos;
				}
			}

			// Token: 0x06006624 RID: 26148 RVA: 0x0015F0E6 File Offset: 0x0015D2E6
			internal ViewValidator.DbExpressionEntitySetInfo GetEntitySetInfoForMember(string memberName)
			{
				return this.m_entitySetInfos[memberName];
			}

			// Token: 0x04002DC5 RID: 11717
			private readonly Dictionary<string, ViewValidator.DbExpressionEntitySetInfo> m_entitySetInfos;
		}

		// Token: 0x02000B6E RID: 2926
		private class DbExpressionMemberCollectionEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x06006625 RID: 26149 RVA: 0x0015F0F4 File Offset: 0x0015D2F4
			internal DbExpressionMemberCollectionEntitySetInfo(IEnumerable<ViewValidator.DbExpressionEntitySetInfo> entitySetInfos)
			{
				this.m_entitySets = entitySetInfos;
			}

			// Token: 0x170010FE RID: 4350
			// (get) Token: 0x06006626 RID: 26150 RVA: 0x0015F103 File Offset: 0x0015D303
			internal IEnumerable<ViewValidator.DbExpressionEntitySetInfo> entitySetInfos
			{
				get
				{
					return this.m_entitySets;
				}
			}

			// Token: 0x04002DC6 RID: 11718
			private readonly IEnumerable<ViewValidator.DbExpressionEntitySetInfo> m_entitySets;
		}
	}
}
