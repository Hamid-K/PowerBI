using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006F4 RID: 1780
	internal class ViewSimplifier
	{
		// Token: 0x060052BC RID: 21180 RVA: 0x00128FF0 File Offset: 0x001271F0
		internal static DbQueryCommandTree SimplifyView(EntitySetBase extent, DbQueryCommandTree view)
		{
			view = new ViewSimplifier(extent).Simplify(view);
			return view;
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00129001 File Offset: 0x00127201
		private ViewSimplifier(EntitySetBase viewTarget)
		{
			this.extent = viewTarget;
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x00129010 File Offset: 0x00127210
		private DbQueryCommandTree Simplify(DbQueryCommandTree view)
		{
			Func<DbExpression, DbExpression> func = PatternMatchRuleProcessor.Create(new PatternMatchRule[]
			{
				PatternMatchRule.Create(ViewSimplifier._patternCollapseNestedProjection, new Func<DbExpression, DbExpression>(ViewSimplifier.CollapseNestedProjection)),
				PatternMatchRule.Create(ViewSimplifier._patternCase, new Func<DbExpression, DbExpression>(ViewSimplifier.SimplifyCaseStatement)),
				PatternMatchRule.Create(ViewSimplifier._patternNestedTphDiscriminator, new Func<DbExpression, DbExpression>(ViewSimplifier.SimplifyNestedTphDiscriminator)),
				PatternMatchRule.Create(ViewSimplifier._patternEntityConstructor, new Func<DbExpression, DbExpression>(this.AddFkRelatedEntityRefs))
			});
			DbExpression dbExpression = view.Query;
			dbExpression = func(dbExpression);
			view = DbQueryCommandTree.FromValidExpression(view.MetadataWorkspace, view.DataSpace, dbExpression, view.UseDatabaseNullSemantics, view.DisableFilterOverProjectionSimplificationForCustomFunctions);
			return view;
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x001290BC File Offset: 0x001272BC
		private DbExpression AddFkRelatedEntityRefs(DbExpression viewConstructor)
		{
			if (this.doNotProcess)
			{
				return null;
			}
			if (this.extent.BuiltInTypeKind != BuiltInTypeKind.EntitySet || this.extent.EntityContainer.DataSpace != DataSpace.CSpace)
			{
				this.doNotProcess = true;
				return null;
			}
			EntitySet targetSet = (EntitySet)this.extent;
			Func<AssociationSetEnd, bool> <>9__2;
			List<AssociationSet> list = targetSet.EntityContainer.BaseEntitySets.Where((EntitySetBase es) => es.BuiltInTypeKind == BuiltInTypeKind.AssociationSet).Cast<AssociationSet>().Where(delegate(AssociationSet assocSet)
			{
				if (assocSet.ElementType.IsForeignKey)
				{
					IEnumerable<AssociationSetEnd> associationSetEnds = assocSet.AssociationSetEnds;
					Func<AssociationSetEnd, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (AssociationSetEnd se) => se.EntitySet == targetSet);
					}
					return associationSetEnds.Any(func);
				}
				return false;
			})
				.ToList<AssociationSet>();
			if (list.Count == 0)
			{
				this.doNotProcess = true;
				return null;
			}
			HashSet<Tuple<EntityType, AssociationSetEnd, ReferentialConstraint>> hashSet = new HashSet<Tuple<EntityType, AssociationSetEnd, ReferentialConstraint>>();
			foreach (AssociationSet associationSet in list)
			{
				ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints[0];
				AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name];
				if (associationSetEnd.EntitySet == targetSet)
				{
					EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(associationSetEnd.CorrespondingAssociationEndMember.TypeUsage).ElementType;
					AssociationSetEnd associationSetEnd2 = associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name];
					hashSet.Add(Tuple.Create<EntityType, AssociationSetEnd, ReferentialConstraint>(entityType, associationSetEnd2, referentialConstraint));
				}
			}
			if (hashSet.Count == 0)
			{
				this.doNotProcess = true;
				return null;
			}
			DbProjectExpression dbProjectExpression = (DbProjectExpression)viewConstructor;
			List<DbNewInstanceExpression> list2 = new List<DbNewInstanceExpression>();
			List<DbExpression> list3 = null;
			if (dbProjectExpression.Projection.ExpressionKind == DbExpressionKind.Case)
			{
				DbCaseExpression dbCaseExpression = (DbCaseExpression)dbProjectExpression.Projection;
				list3 = new List<DbExpression>(dbCaseExpression.When.Count);
				for (int i = 0; i < dbCaseExpression.When.Count; i++)
				{
					list3.Add(dbCaseExpression.When[i]);
					list2.Add((DbNewInstanceExpression)dbCaseExpression.Then[i]);
				}
				list2.Add((DbNewInstanceExpression)dbCaseExpression.Else);
			}
			else
			{
				list2.Add((DbNewInstanceExpression)dbProjectExpression.Projection);
			}
			bool flag = false;
			for (int j = 0; j < list2.Count; j++)
			{
				DbNewInstanceExpression entityConstructor = list2[j];
				EntityType constructedEntityType = TypeHelpers.GetEdmType<EntityType>(entityConstructor.ResultType);
				List<DbRelatedEntityRef> list4 = (from psdt in hashSet
					where constructedEntityType == psdt.Item1 || constructedEntityType.IsSubtypeOf(psdt.Item1)
					select ViewSimplifier.RelatedEntityRefFromAssociationSetEnd(constructedEntityType, entityConstructor, psdt.Item2, psdt.Item3)).ToList<DbRelatedEntityRef>();
				if (list4.Count > 0)
				{
					if (entityConstructor.HasRelatedEntityReferences)
					{
						list4 = entityConstructor.RelatedEntityReferences.Concat(list4).ToList<DbRelatedEntityRef>();
					}
					entityConstructor = DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression(constructedEntityType, entityConstructor.Arguments, list4);
					list2[j] = entityConstructor;
					flag = true;
				}
			}
			DbExpression dbExpression = null;
			if (flag)
			{
				if (list3 != null)
				{
					List<DbExpression> list5 = new List<DbExpression>(list3.Count);
					List<DbExpression> list6 = new List<DbExpression>(list3.Count);
					for (int k = 0; k < list3.Count; k++)
					{
						list5.Add(list3[k]);
						list6.Add(list2[k]);
					}
					dbExpression = dbProjectExpression.Input.Project(DbExpressionBuilder.Case(list5, list6, list2[list3.Count]));
				}
				else
				{
					dbExpression = dbProjectExpression.Input.Project(list2[0]);
				}
			}
			this.doNotProcess = true;
			return dbExpression;
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0012948C File Offset: 0x0012768C
		private static DbRelatedEntityRef RelatedEntityRefFromAssociationSetEnd(EntityType constructedEntityType, DbNewInstanceExpression entityConstructor, AssociationSetEnd principalSetEnd, ReferentialConstraint fkConstraint)
		{
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(fkConstraint.FromRole.TypeUsage).ElementType;
			IEnumerable<Tuple<string, DbExpression>> enumerable = from pv in constructedEntityType.Properties.Select((EdmProperty p, int idx) => Tuple.Create<EdmProperty, DbExpression>(p, entityConstructor.Arguments[idx]))
				join ft in fkConstraint.FromProperties.Select((EdmProperty fp, int idx) => Tuple.Create<EdmProperty, EdmProperty>(fp, fkConstraint.ToProperties[idx])) on pv.Item1 equals ft.Item2
				select Tuple.Create<string, DbExpression>(ft.Item1.Name, pv.Item2);
			IList<DbExpression> list;
			if (fkConstraint.FromProperties.Count == 1)
			{
				Tuple<string, DbExpression> tuple = enumerable.Single<Tuple<string, DbExpression>>();
				list = new DbExpression[] { tuple.Item2 };
			}
			else
			{
				Dictionary<string, DbExpression> keyValueMap = enumerable.ToDictionary((Tuple<string, DbExpression> pav) => pav.Item1, (Tuple<string, DbExpression> pav) => pav.Item2, StringComparer.Ordinal);
				list = entityType.KeyMemberNames.Select((string memberName) => keyValueMap[memberName]).ToList<DbExpression>();
			}
			DbRefExpression dbRefExpression = principalSetEnd.EntitySet.CreateRef(entityType, list);
			return DbExpressionBuilder.CreateRelatedEntityRef(fkConstraint.ToRole, fkConstraint.FromRole, dbRefExpression);
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x00129634 File Offset: 0x00127834
		private static DbExpression SimplifyNestedTphDiscriminator(DbExpression expression)
		{
			DbProjectExpression dbProjectExpression = (DbProjectExpression)expression;
			DbFilterExpression booleanColumnFilter = (DbFilterExpression)dbProjectExpression.Input.Expression;
			DbProjectExpression dbProjectExpression2 = (DbProjectExpression)booleanColumnFilter.Input.Expression;
			DbFilterExpression dbFilterExpression = (DbFilterExpression)dbProjectExpression2.Input.Expression;
			List<DbExpression> list = ViewSimplifier.FlattenOr(booleanColumnFilter.Predicate).ToList<DbExpression>();
			List<DbPropertyExpression> list2 = (from px in list.OfType<DbPropertyExpression>()
				where px.Instance.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)px.Instance).VariableName == booleanColumnFilter.Input.VariableName
				select px).ToList<DbPropertyExpression>();
			if (list.Count != list2.Count)
			{
				return null;
			}
			List<string> list3 = list2.Select((DbPropertyExpression px) => px.Property.Name).ToList<string>();
			Dictionary<object, DbComparisonExpression> discriminatorPredicates = new Dictionary<object, DbComparisonExpression>();
			if (!TypeSemantics.IsEntityType(dbFilterExpression.Input.VariableType) || !ViewSimplifier.TryMatchDiscriminatorPredicate(dbFilterExpression, delegate(DbComparisonExpression compEx, object discValue)
			{
				discriminatorPredicates.Add(discValue, compEx);
			}))
			{
				return null;
			}
			EdmProperty edmProperty = (EdmProperty)((DbPropertyExpression)discriminatorPredicates.First<KeyValuePair<object, DbComparisonExpression>>().Value.Left).Property;
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression2.Projection;
			RowType edmType = TypeHelpers.GetEdmType<RowType>(dbNewInstanceExpression.ResultType);
			Dictionary<string, DbComparisonExpression> dictionary = new Dictionary<string, DbComparisonExpression>();
			Dictionary<string, DbComparisonExpression> dictionary2 = new Dictionary<string, DbComparisonExpression>();
			Dictionary<string, DbExpression> dictionary3 = new Dictionary<string, DbExpression>(dbNewInstanceExpression.Arguments.Count);
			for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
			{
				string name = edmType.Properties[i].Name;
				DbExpression dbExpression = dbNewInstanceExpression.Arguments[i];
				if (list3.Contains(name))
				{
					if (dbExpression.ExpressionKind != DbExpressionKind.Case)
					{
						return null;
					}
					DbCaseExpression dbCaseExpression = (DbCaseExpression)dbExpression;
					if (dbCaseExpression.When.Count != 1 || !TypeSemantics.IsBooleanType(dbCaseExpression.Then[0].ResultType) || !TypeSemantics.IsBooleanType(dbCaseExpression.Else.ResultType) || dbCaseExpression.Then[0].ExpressionKind != DbExpressionKind.Constant || dbCaseExpression.Else.ExpressionKind != DbExpressionKind.Constant || !(bool)((DbConstantExpression)dbCaseExpression.Then[0]).Value || (bool)((DbConstantExpression)dbCaseExpression.Else).Value)
					{
						return null;
					}
					DbPropertyExpression dbPropertyExpression;
					object obj;
					if (!ViewSimplifier.TryMatchPropertyEqualsValue(dbCaseExpression.When[0], dbProjectExpression2.Input.VariableName, out dbPropertyExpression, out obj) || dbPropertyExpression.Property != edmProperty || !discriminatorPredicates.ContainsKey(obj))
					{
						return null;
					}
					dictionary.Add(name, discriminatorPredicates[obj]);
					dictionary2.Add(name, (DbComparisonExpression)dbCaseExpression.When[0]);
				}
				else
				{
					dictionary3.Add(name, dbExpression);
				}
			}
			DbExpression dbExpression2 = Helpers.BuildBalancedTreeInPlace<DbExpression>(new List<DbExpression>(dictionary.Values), (DbExpression left, DbExpression right) => left.Or(right));
			dbFilterExpression = dbFilterExpression.Input.Filter(dbExpression2);
			DbCaseExpression dbCaseExpression2 = (DbCaseExpression)dbProjectExpression.Projection;
			List<DbExpression> list4 = new List<DbExpression>(dbCaseExpression2.When.Count);
			List<DbExpression> list5 = new List<DbExpression>(dbCaseExpression2.Then.Count);
			for (int j = 0; j < dbCaseExpression2.When.Count; j++)
			{
				DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)dbCaseExpression2.When[j];
				DbNewInstanceExpression dbNewInstanceExpression2 = (DbNewInstanceExpression)dbCaseExpression2.Then[j];
				DbComparisonExpression dbComparisonExpression;
				if (!dictionary2.TryGetValue(dbPropertyExpression2.Property.Name, out dbComparisonExpression))
				{
					return null;
				}
				list4.Add(dbComparisonExpression);
				DbExpression dbExpression3 = ViewSimplifier.ValueSubstituter.Substitute(dbNewInstanceExpression2, dbProjectExpression.Input.VariableName, dictionary3);
				list5.Add(dbExpression3);
			}
			DbExpression dbExpression4 = ViewSimplifier.ValueSubstituter.Substitute(dbCaseExpression2.Else, dbProjectExpression.Input.VariableName, dictionary3);
			DbCaseExpression dbCaseExpression3 = DbExpressionBuilder.Case(list4, list5, dbExpression4);
			return dbFilterExpression.BindAs(dbProjectExpression2.Input.VariableName).Project(dbCaseExpression3);
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00129A50 File Offset: 0x00127C50
		private static DbExpression SimplifyCaseStatement(DbExpression expression)
		{
			DbCaseExpression dbCaseExpression = (DbCaseExpression)expression;
			bool flag = false;
			List<DbExpression> list = new List<DbExpression>(dbCaseExpression.When.Count);
			foreach (DbExpression dbExpression in dbCaseExpression.When)
			{
				DbExpression dbExpression2;
				if (ViewSimplifier.TrySimplifyPredicate(dbExpression, out dbExpression2))
				{
					list.Add(dbExpression2);
					flag = true;
				}
				else
				{
					list.Add(dbExpression);
				}
			}
			if (!flag)
			{
				return null;
			}
			dbCaseExpression = DbExpressionBuilder.Case(list, dbCaseExpression.Then, dbCaseExpression.Else);
			return dbCaseExpression;
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00129AEC File Offset: 0x00127CEC
		private static bool TrySimplifyPredicate(DbExpression predicate, out DbExpression simplified)
		{
			simplified = null;
			if (predicate.ExpressionKind != DbExpressionKind.Case)
			{
				return false;
			}
			DbCaseExpression dbCaseExpression = (DbCaseExpression)predicate;
			if (dbCaseExpression.Then.Count != 1 && dbCaseExpression.Then[0].ExpressionKind == DbExpressionKind.Constant)
			{
				return false;
			}
			DbConstantExpression dbConstantExpression = (DbConstantExpression)dbCaseExpression.Then[0];
			if (!true.Equals(dbConstantExpression.Value))
			{
				return false;
			}
			if (dbCaseExpression.Else != null)
			{
				if (dbCaseExpression.Else.ExpressionKind != DbExpressionKind.Constant)
				{
					return false;
				}
				DbConstantExpression dbConstantExpression2 = (DbConstantExpression)dbCaseExpression.Else;
				if (true.Equals(dbConstantExpression2.Value))
				{
					return false;
				}
			}
			simplified = dbCaseExpression.When[0];
			return true;
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x00129BA0 File Offset: 0x00127DA0
		private static DbExpression CollapseNestedProjection(DbExpression expression)
		{
			DbProjectExpression dbProjectExpression = (DbProjectExpression)expression;
			DbExpression projection = dbProjectExpression.Projection;
			DbProjectExpression dbProjectExpression2 = (DbProjectExpression)dbProjectExpression.Input.Expression;
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression2.Projection;
			Dictionary<string, DbExpression> dictionary = new Dictionary<string, DbExpression>(dbNewInstanceExpression.Arguments.Count);
			RowType rowType = (RowType)dbNewInstanceExpression.ResultType.EdmType;
			for (int i = 0; i < rowType.Members.Count; i++)
			{
				dictionary[rowType.Members[i].Name] = dbNewInstanceExpression.Arguments[i];
			}
			ViewSimplifier.ProjectionCollapser projectionCollapser = new ViewSimplifier.ProjectionCollapser(dictionary, dbProjectExpression.Input);
			DbExpression dbExpression = projectionCollapser.CollapseProjection(projection);
			if (projectionCollapser.IsDoomed)
			{
				return null;
			}
			return dbProjectExpression2.Input.Project(dbExpression);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00129C6C File Offset: 0x00127E6C
		internal static IEnumerable<DbExpression> FlattenOr(DbExpression expression)
		{
			return Helpers.GetLeafNodes<DbExpression>(expression, (DbExpression exp) => exp.ExpressionKind != DbExpressionKind.Or, delegate(DbExpression exp)
			{
				DbOrExpression dbOrExpression = (DbOrExpression)exp;
				return new DbExpression[] { dbOrExpression.Left, dbOrExpression.Right };
			});
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00129CC0 File Offset: 0x00127EC0
		internal static bool TryMatchDiscriminatorPredicate(DbFilterExpression filter, Action<DbComparisonExpression, object> onMatchedComparison)
		{
			EdmProperty edmProperty = null;
			foreach (DbExpression dbExpression in ViewSimplifier.FlattenOr(filter.Predicate))
			{
				DbPropertyExpression dbPropertyExpression;
				object obj;
				if (!ViewSimplifier.TryMatchPropertyEqualsValue(dbExpression, filter.Input.VariableName, out dbPropertyExpression, out obj))
				{
					return false;
				}
				if (edmProperty == null)
				{
					edmProperty = (EdmProperty)dbPropertyExpression.Property;
				}
				else if (edmProperty != dbPropertyExpression.Property)
				{
					return false;
				}
				onMatchedComparison((DbComparisonExpression)dbExpression, obj);
			}
			return true;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00129D5C File Offset: 0x00127F5C
		internal static bool TryMatchPropertyEqualsValue(DbExpression expression, string propertyVariable, out DbPropertyExpression property, out object value)
		{
			property = null;
			value = null;
			if (expression.ExpressionKind != DbExpressionKind.Equals)
			{
				return false;
			}
			DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)expression;
			if (dbBinaryExpression.Left.ExpressionKind != DbExpressionKind.Property)
			{
				return false;
			}
			property = (DbPropertyExpression)dbBinaryExpression.Left;
			return ViewSimplifier.TryMatchConstant(dbBinaryExpression.Right, out value) && property.Instance.ExpressionKind == DbExpressionKind.VariableReference && !(((DbVariableReferenceExpression)property.Instance).VariableName != propertyVariable);
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00129DDC File Offset: 0x00127FDC
		private static bool TryMatchConstant(DbExpression expression, out object value)
		{
			if (expression.ExpressionKind == DbExpressionKind.Constant)
			{
				value = ((DbConstantExpression)expression).Value;
				return true;
			}
			if (expression.ExpressionKind == DbExpressionKind.Cast && expression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && ViewSimplifier.TryMatchConstant(((DbCastExpression)expression).Argument, out value))
			{
				PrimitiveType primitiveType = (PrimitiveType)expression.ResultType.EdmType;
				value = Convert.ChangeType(value, primitiveType.ClrEquivalentType, CultureInfo.InvariantCulture);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04001DE7 RID: 7655
		private readonly EntitySetBase extent;

		// Token: 0x04001DE8 RID: 7656
		private static readonly Func<DbExpression, bool> _patternEntityConstructor = Patterns.MatchProject(Patterns.AnyExpression, Patterns.And(Patterns.MatchEntityType, Patterns.Or(Patterns.MatchNewInstance(), Patterns.MatchCase(Patterns.AnyExpressions, Patterns.MatchForAll(Patterns.MatchNewInstance()), Patterns.MatchNewInstance()))));

		// Token: 0x04001DE9 RID: 7657
		private bool doNotProcess;

		// Token: 0x04001DEA RID: 7658
		private static readonly Func<DbExpression, bool> _patternNestedTphDiscriminator = Patterns.MatchProject(Patterns.MatchFilter(Patterns.MatchProject(Patterns.MatchFilter(Patterns.AnyExpression, Patterns.Or(Patterns.MatchKind(DbExpressionKind.Equals), Patterns.MatchKind(DbExpressionKind.Or))), Patterns.And(Patterns.MatchRowType, Patterns.MatchNewInstance(Patterns.MatchForAll(Patterns.Or(Patterns.And(Patterns.MatchNewInstance(), Patterns.MatchComplexType), Patterns.MatchKind(DbExpressionKind.Property), Patterns.MatchKind(DbExpressionKind.Case)))))), Patterns.Or(Patterns.MatchKind(DbExpressionKind.Property), Patterns.MatchKind(DbExpressionKind.Or))), Patterns.And(Patterns.MatchEntityType, Patterns.MatchCase(Patterns.MatchForAll(Patterns.MatchKind(DbExpressionKind.Property)), Patterns.MatchForAll(Patterns.MatchKind(DbExpressionKind.NewInstance)), Patterns.MatchKind(DbExpressionKind.NewInstance))));

		// Token: 0x04001DEB RID: 7659
		private static readonly Func<DbExpression, bool> _patternCase = Patterns.MatchKind(DbExpressionKind.Case);

		// Token: 0x04001DEC RID: 7660
		private static readonly Func<DbExpression, bool> _patternCollapseNestedProjection = Patterns.MatchProject(Patterns.MatchProject(Patterns.AnyExpression, Patterns.MatchKind(DbExpressionKind.NewInstance)), Patterns.AnyExpression);

		// Token: 0x02000CA5 RID: 3237
		private class ValueSubstituter : DefaultExpressionVisitor
		{
			// Token: 0x06006C54 RID: 27732 RVA: 0x0017228A File Offset: 0x0017048A
			internal static DbExpression Substitute(DbExpression original, string referencedVariable, Dictionary<string, DbExpression> propertyValues)
			{
				return new ViewSimplifier.ValueSubstituter(referencedVariable, propertyValues).VisitExpression(original);
			}

			// Token: 0x06006C55 RID: 27733 RVA: 0x00172299 File Offset: 0x00170499
			private ValueSubstituter(string varName, Dictionary<string, DbExpression> replValues)
			{
				this.variableName = varName;
				this.replacements = replValues;
			}

			// Token: 0x06006C56 RID: 27734 RVA: 0x001722B0 File Offset: 0x001704B0
			public override DbExpression Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				DbExpression dbExpression;
				DbExpression dbExpression2;
				if (expression.Instance.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)expression.Instance).VariableName == this.variableName && this.replacements.TryGetValue(expression.Property.Name, out dbExpression))
				{
					dbExpression2 = dbExpression;
				}
				else
				{
					dbExpression2 = base.Visit(expression);
				}
				return dbExpression2;
			}

			// Token: 0x040031DB RID: 12763
			private readonly string variableName;

			// Token: 0x040031DC RID: 12764
			private readonly Dictionary<string, DbExpression> replacements;
		}

		// Token: 0x02000CA6 RID: 3238
		private class ProjectionCollapser : DefaultExpressionVisitor
		{
			// Token: 0x06006C57 RID: 27735 RVA: 0x0017231E File Offset: 0x0017051E
			internal ProjectionCollapser(Dictionary<string, DbExpression> varRefMemberBindings, DbExpressionBinding outerBinding)
			{
				this.m_varRefMemberBindings = varRefMemberBindings;
				this.m_outerBinding = outerBinding;
			}

			// Token: 0x06006C58 RID: 27736 RVA: 0x00172334 File Offset: 0x00170534
			internal DbExpression CollapseProjection(DbExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x06006C59 RID: 27737 RVA: 0x00172340 File Offset: 0x00170540
			public override DbExpression Visit(DbPropertyExpression property)
			{
				Check.NotNull<DbPropertyExpression>(property, "property");
				if (property.Instance.ExpressionKind == DbExpressionKind.VariableReference && this.IsOuterBindingVarRef((DbVariableReferenceExpression)property.Instance))
				{
					return this.m_varRefMemberBindings[property.Property.Name];
				}
				return base.Visit(property);
			}

			// Token: 0x06006C5A RID: 27738 RVA: 0x00172399 File Offset: 0x00170599
			public override DbExpression Visit(DbVariableReferenceExpression varRef)
			{
				Check.NotNull<DbVariableReferenceExpression>(varRef, "varRef");
				if (this.IsOuterBindingVarRef(varRef))
				{
					this.m_doomed = true;
				}
				return base.Visit(varRef);
			}

			// Token: 0x06006C5B RID: 27739 RVA: 0x001723BE File Offset: 0x001705BE
			private bool IsOuterBindingVarRef(DbVariableReferenceExpression varRef)
			{
				return varRef.VariableName == this.m_outerBinding.VariableName;
			}

			// Token: 0x17001190 RID: 4496
			// (get) Token: 0x06006C5C RID: 27740 RVA: 0x001723D6 File Offset: 0x001705D6
			internal bool IsDoomed
			{
				get
				{
					return this.m_doomed;
				}
			}

			// Token: 0x040031DD RID: 12765
			private readonly Dictionary<string, DbExpression> m_varRefMemberBindings;

			// Token: 0x040031DE RID: 12766
			private readonly DbExpressionBinding m_outerBinding;

			// Token: 0x040031DF RID: 12767
			private bool m_doomed;
		}
	}
}
