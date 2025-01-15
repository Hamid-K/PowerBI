using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006EB RID: 1771
	internal abstract class ExpressionDumper : DbExpressionVisitor
	{
		// Token: 0x06005200 RID: 20992 RVA: 0x0012587E File Offset: 0x00123A7E
		internal void Begin(string name)
		{
			this.Begin(name, null);
		}

		// Token: 0x06005201 RID: 20993
		internal abstract void Begin(string name, Dictionary<string, object> attrs);

		// Token: 0x06005202 RID: 20994
		internal abstract void End(string name);

		// Token: 0x06005203 RID: 20995 RVA: 0x00125888 File Offset: 0x00123A88
		internal void Dump(DbExpression target)
		{
			target.Accept(this);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x00125891 File Offset: 0x00123A91
		internal void Dump(DbExpression e, string name)
		{
			this.Begin(name);
			this.Dump(e);
			this.End(name);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x001258A8 File Offset: 0x00123AA8
		internal void Dump(DbExpressionBinding binding, string name)
		{
			this.Begin(name);
			this.Dump(binding);
			this.End(name);
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x001258C0 File Offset: 0x00123AC0
		internal void Dump(DbExpressionBinding binding)
		{
			this.Begin("DbExpressionBinding", "VariableName", binding.VariableName);
			this.Begin("Expression");
			this.Dump(binding.Expression);
			this.End("Expression");
			this.End("DbExpressionBinding");
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00125910 File Offset: 0x00123B10
		internal void Dump(DbGroupExpressionBinding binding, string name)
		{
			this.Begin(name);
			this.Dump(binding);
			this.End(name);
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x00125928 File Offset: 0x00123B28
		internal void Dump(DbGroupExpressionBinding binding)
		{
			this.Begin("DbGroupExpressionBinding", "VariableName", binding.VariableName, "GroupVariableName", binding.GroupVariableName);
			this.Begin("Expression");
			this.Dump(binding.Expression);
			this.End("Expression");
			this.End("DbGroupExpressionBinding");
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00125984 File Offset: 0x00123B84
		internal void Dump(IEnumerable<DbExpression> exprs, string pluralName, string singularName)
		{
			this.Begin(pluralName);
			foreach (DbExpression dbExpression in exprs)
			{
				this.Begin(singularName);
				this.Dump(dbExpression);
				this.End(singularName);
			}
			this.End(pluralName);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x001259E8 File Offset: 0x00123BE8
		internal void Dump(IEnumerable<FunctionParameter> paramList)
		{
			this.Begin("Parameters");
			foreach (FunctionParameter functionParameter in paramList)
			{
				this.Begin("Parameter", "Name", functionParameter.Name);
				this.Dump(functionParameter.TypeUsage, "ParameterType");
				this.End("Parameter");
			}
			this.End("Parameters");
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00125A74 File Offset: 0x00123C74
		internal void Dump(TypeUsage type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00125A8C File Offset: 0x00123C8C
		internal void Dump(TypeUsage type)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (Facet facet in type.Facets)
			{
				dictionary.Add(facet.Name, facet.Value);
			}
			this.Begin("TypeUsage", dictionary);
			this.Dump(type.EdmType);
			this.End("TypeUsage");
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x00125B14 File Offset: 0x00123D14
		internal void Dump(EdmType type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00125B2C File Offset: 0x00123D2C
		internal void Dump(EdmType type)
		{
			this.Begin("EdmType", "BuiltInTypeKind", Enum.GetName(typeof(BuiltInTypeKind), type.BuiltInTypeKind), "Namespace", type.NamespaceName, "Name", type.Name);
			this.End("EdmType");
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00125B84 File Offset: 0x00123D84
		internal void Dump(RelationshipType type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00125B9B File Offset: 0x00123D9B
		internal void Dump(RelationshipType type)
		{
			this.Begin("RelationshipType", "Namespace", type.NamespaceName, "Name", type.Name);
			this.End("RelationshipType");
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00125BCC File Offset: 0x00123DCC
		internal void Dump(EdmFunction function)
		{
			this.Begin("Function", "Name", function.Name, "Namespace", function.NamespaceName);
			this.Dump(function.Parameters);
			if (function.ReturnParameters.Count == 1)
			{
				this.Dump(function.ReturnParameters[0].TypeUsage, "ReturnType");
			}
			else
			{
				this.Begin("ReturnTypes");
				foreach (FunctionParameter functionParameter in function.ReturnParameters)
				{
					this.Dump(functionParameter.TypeUsage, functionParameter.Name);
				}
				this.End("ReturnTypes");
			}
			this.End("Function");
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00125CA4 File Offset: 0x00123EA4
		internal void Dump(EdmProperty prop)
		{
			this.Begin("Property", "Name", prop.Name, "Nullable", prop.Nullable);
			this.Dump(prop.DeclaringType, "DeclaringType");
			this.Dump(prop.TypeUsage, "PropertyType");
			this.End("Property");
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00125D04 File Offset: 0x00123F04
		internal void Dump(RelationshipEndMember end, string name)
		{
			this.Begin(name);
			this.Begin("RelationshipEndMember", "Name", end.Name, "RelationshipMultiplicity", Enum.GetName(typeof(RelationshipMultiplicity), end.RelationshipMultiplicity));
			this.Dump(end.DeclaringType, "DeclaringRelation");
			this.Dump(end.TypeUsage, "EndType");
			this.End("RelationshipEndMember");
			this.End(name);
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00125D84 File Offset: 0x00123F84
		internal void Dump(NavigationProperty navProp, string name)
		{
			this.Begin(name);
			this.Begin("NavigationProperty", "Name", navProp.Name, "RelationshipTypeName", navProp.RelationshipType.FullName, "ToEndMemberName", navProp.ToEndMember.Name);
			this.Dump(navProp.DeclaringType, "DeclaringType");
			this.Dump(navProp.TypeUsage, "PropertyType");
			this.End("NavigationProperty");
			this.End(name);
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00125E04 File Offset: 0x00124004
		internal void Dump(DbLambda lambda)
		{
			this.Begin("DbLambda");
			this.Dump(lambda.Variables.Cast<DbExpression>(), "Variables", "Variable");
			this.Dump(lambda.Body, "Body");
			this.End("DbLambda");
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00125E53 File Offset: 0x00124053
		private void Begin(DbExpression expr)
		{
			this.Begin(expr, new Dictionary<string, object>());
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00125E64 File Offset: 0x00124064
		private void Begin(DbExpression expr, Dictionary<string, object> attrs)
		{
			attrs.Add("DbExpressionKind", Enum.GetName(typeof(DbExpressionKind), expr.ExpressionKind));
			this.Begin(expr.GetType().Name, attrs);
			this.Dump(expr.ResultType, "ResultType");
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00125EBC File Offset: 0x001240BC
		private void Begin(DbExpression expr, string attributeName, object attributeValue)
		{
			this.Begin(expr, new Dictionary<string, object> { { attributeName, attributeValue } });
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00125EE0 File Offset: 0x001240E0
		private void Begin(string expr, string attributeName, object attributeValue)
		{
			this.Begin(expr, new Dictionary<string, object> { { attributeName, attributeValue } });
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x00125F04 File Offset: 0x00124104
		private void Begin(string expr, string attributeName1, object attributeValue1, string attributeName2, object attributeValue2)
		{
			this.Begin(expr, new Dictionary<string, object>
			{
				{ attributeName1, attributeValue1 },
				{ attributeName2, attributeValue2 }
			});
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00125F34 File Offset: 0x00124134
		private void Begin(string expr, string attributeName1, object attributeValue1, string attributeName2, object attributeValue2, string attributeName3, object attributeValue3)
		{
			this.Begin(expr, new Dictionary<string, object>
			{
				{ attributeName1, attributeValue1 },
				{ attributeName2, attributeValue2 },
				{ attributeName3, attributeValue3 }
			});
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00125F6B File Offset: 0x0012416B
		private void End(DbExpression expr)
		{
			this.End(expr.GetType().Name);
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x00125F7E File Offset: 0x0012417E
		private void BeginUnary(DbUnaryExpression e)
		{
			this.Begin(e);
			this.Begin("Argument");
			this.Dump(e.Argument);
			this.End("Argument");
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x00125FAC File Offset: 0x001241AC
		private void BeginBinary(DbBinaryExpression e)
		{
			this.Begin(e);
			this.Begin("Left");
			this.Dump(e.Left);
			this.End("Left");
			this.Begin("Right");
			this.Dump(e.Right);
			this.End("Right");
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x00126004 File Offset: 0x00124204
		public override void Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			this.Begin(e);
			this.End(e);
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00126020 File Offset: 0x00124220
		public override void Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			this.Begin(e, new Dictionary<string, object> { { "Value", e.Value } });
			this.End(e);
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0012605F File Offset: 0x0012425F
		public override void Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			this.Begin(e);
			this.End(e);
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0012607C File Offset: 0x0012427C
		public override void Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			this.Begin(e, new Dictionary<string, object> { { "VariableName", e.VariableName } });
			this.End(e);
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x001260BC File Offset: 0x001242BC
		public override void Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			this.Begin(e, new Dictionary<string, object> { { "ParameterName", e.ParameterName } });
			this.End(e);
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x001260FB File Offset: 0x001242FB
		public override void Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Function);
			this.Dump(e.Arguments, "Arguments", "Argument");
			this.End(e);
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x00126139 File Offset: 0x00124339
		public override void Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			this.Begin(expression);
			this.Dump(expression.Lambda);
			this.Dump(expression.Arguments, "Arguments", "Argument");
			this.End(expression);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00126178 File Offset: 0x00124378
		public override void Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			this.Begin(e);
			RelationshipEndMember relationshipEndMember = e.Property as RelationshipEndMember;
			if (relationshipEndMember != null)
			{
				this.Dump(relationshipEndMember, "Property");
			}
			else if (Helper.IsNavigationProperty(e.Property))
			{
				this.Dump((NavigationProperty)e.Property, "Property");
			}
			else
			{
				this.Dump((EdmProperty)e.Property);
			}
			if (e.Instance != null)
			{
				this.Dump(e.Instance, "Instance");
			}
			this.End(e);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0012620B File Offset: 0x0012440B
		public override void Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x00126228 File Offset: 0x00124428
		public override void Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Argument, "Argument");
			this.Dump(e.Pattern, "Pattern");
			this.Dump(e.Escape, "Escape");
			this.End(e);
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00126284 File Offset: 0x00124484
		public override void Visit(DbLimitExpression e)
		{
			Check.NotNull<DbLimitExpression>(e, "e");
			this.Begin(e, "WithTies", e.WithTies);
			this.Dump(e.Argument, "Argument");
			this.Dump(e.Limit, "Limit");
			this.End(e);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x001262DD File Offset: 0x001244DD
		public override void Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x001262F9 File Offset: 0x001244F9
		public override void Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Arguments, "Arguments", "Argument");
			this.End(e);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0012632B File Offset: 0x0012452B
		public override void Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00126347 File Offset: 0x00124547
		public override void Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00126363 File Offset: 0x00124563
		public override void Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Item);
			this.Dump(e.List, "List", "Item");
			this.End(e);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x001263A1 File Offset: 0x001245A1
		public override void Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x001263BD File Offset: 0x001245BD
		public override void Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x001263D9 File Offset: 0x001245D9
		public override void Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x001263F5 File Offset: 0x001245F5
		public override void Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00126411 File Offset: 0x00124611
		public override void Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0012642D File Offset: 0x0012462D
		public override void Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x00126449 File Offset: 0x00124649
		public override void Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00126465 File Offset: 0x00124665
		public override void Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00126481 File Offset: 0x00124681
		public override void Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			this.BeginUnary(e);
			this.Dump(e.OfType, "OfType");
			this.End(e);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x001264AE File Offset: 0x001246AE
		public override void Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x001264CC File Offset: 0x001246CC
		public override void Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.When, "Whens", "When");
			this.Dump(e.Then, "Thens", "Then");
			this.Dump(e.Else, "Else");
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00126529 File Offset: 0x00124729
		public override void Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			this.BeginUnary(e);
			this.Dump(e.OfType, "OfType");
			this.End(e);
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00126558 File Offset: 0x00124758
		public override void Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Arguments, "Arguments", "Argument");
			if (e.HasRelatedEntityReferences)
			{
				this.Begin("RelatedEntityReferences");
				foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
				{
					this.Begin("DbRelatedEntityRef");
					this.Dump(dbRelatedEntityRef.SourceEnd, "SourceEnd");
					this.Dump(dbRelatedEntityRef.TargetEnd, "TargetEnd");
					this.Dump(dbRelatedEntityRef.TargetEntityReference, "TargetEntityReference");
					this.End("DbRelatedEntityRef");
				}
				this.End("RelatedEntityReferences");
			}
			this.End(e);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00126638 File Offset: 0x00124838
		public override void Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.NavigateFrom, "NavigateFrom");
			this.Dump(e.NavigateTo, "NavigateTo");
			this.Dump(e.Relationship, "Relationship");
			this.Dump(e.NavigationSource, "NavigationSource");
			this.End(e);
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x001266A3 File Offset: 0x001248A3
		public override void Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x001266BF File Offset: 0x001248BF
		public override void Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x001266DB File Offset: 0x001248DB
		public override void Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x001266F7 File Offset: 0x001248F7
		public override void Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x00126714 File Offset: 0x00124914
		public override void Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			this.Begin(e);
			this.Begin("Target", "Name", e.Target.Name, "Container", e.Target.EntityContainer.Name);
			this.Dump(e.Target.ElementType, "TargetElementType");
			this.End("Target");
			this.End(e);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x0012678C File Offset: 0x0012498C
		public override void Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Predicate, "Predicate");
			this.End(e);
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x001267CA File Offset: 0x001249CA
		public override void Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Projection, "Projection");
			this.End(e);
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x00126808 File Offset: 0x00124A08
		public override void Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			this.Begin(e);
			this.Begin("Inputs");
			foreach (DbExpressionBinding dbExpressionBinding in e.Inputs)
			{
				this.Dump(dbExpressionBinding, "Input");
			}
			this.End("Inputs");
			this.End(e);
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0012688C File Offset: 0x00124A8C
		public override void Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Left, "Left");
			this.Dump(e.Right, "Right");
			this.Dump(e.JoinCondition, "JoinCondition");
			this.End(e);
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x001268E6 File Offset: 0x00124AE6
		public override void Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Apply, "Apply");
			this.End(e);
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x00126924 File Offset: 0x00124B24
		public override void Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Keys, "Keys", "Key");
			this.Begin("Aggregates");
			foreach (DbAggregate dbAggregate in e.Aggregates)
			{
				DbFunctionAggregate dbFunctionAggregate = dbAggregate as DbFunctionAggregate;
				if (dbFunctionAggregate != null)
				{
					this.Begin("DbFunctionAggregate");
					this.Dump(dbFunctionAggregate.Function);
					this.Dump(dbFunctionAggregate.Arguments, "Arguments", "Argument");
					this.End("DbFunctionAggregate");
				}
				else
				{
					DbGroupAggregate dbGroupAggregate = dbAggregate as DbGroupAggregate;
					this.Begin("DbGroupAggregate");
					this.Dump(dbGroupAggregate.Arguments, "Arguments", "Argument");
					this.End("DbGroupAggregate");
				}
			}
			this.End("Aggregates");
			this.End(e);
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x00126A3C File Offset: 0x00124C3C
		protected virtual void Dump(IList<DbSortClause> sortOrder)
		{
			this.Begin("SortOrder");
			foreach (DbSortClause dbSortClause in sortOrder)
			{
				string text = dbSortClause.Collation;
				if (text == null)
				{
					text = "";
				}
				this.Begin("DbSortClause", "Ascending", dbSortClause.Ascending, "Collation", text);
				this.Dump(dbSortClause.Expression, "Expression");
				this.End("DbSortClause");
			}
			this.End("SortOrder");
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x00126AE0 File Offset: 0x00124CE0
		public override void Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.SortOrder);
			this.Dump(e.Count, "Count");
			this.End(e);
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x00126B35 File Offset: 0x00124D35
		public override void Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.SortOrder);
			this.End(e);
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x00126B6E File Offset: 0x00124D6E
		public override void Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Predicate, "Predicate");
			this.End(e);
		}
	}
}
