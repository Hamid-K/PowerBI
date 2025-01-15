using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200045F RID: 1119
	internal sealed class ExpressionConverter
	{
		// Token: 0x060036D3 RID: 14035 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		internal ExpressionConverter(Funcletizer funcletizer, Expression expression)
		{
			this._funcletizer = funcletizer;
			expression = funcletizer.Funcletize(expression, out this._recompileRequired);
			LinqExpressionNormalizer linqExpressionNormalizer = new LinqExpressionNormalizer();
			this._expression = linqExpressionNormalizer.Visit(expression);
			this._perspective = funcletizer.RootContext.Perspective;
			this._bindingContext = new BindingContext();
			this._ignoreInclude = 0;
			this._orderByLifter = new ExpressionConverter.OrderByLifter(this._aliasGenerator);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000B1548 File Offset: 0x000AF748
		private static Dictionary<ExpressionType, ExpressionConverter.Translator> InitializeTranslators()
		{
			Dictionary<ExpressionType, ExpressionConverter.Translator> dictionary = new Dictionary<ExpressionType, ExpressionConverter.Translator>();
			foreach (ExpressionConverter.Translator translator in ExpressionConverter.GetTranslators())
			{
				foreach (ExpressionType expressionType in translator.NodeTypes)
				{
					dictionary.Add(expressionType, translator);
				}
			}
			return dictionary;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000B15D4 File Offset: 0x000AF7D4
		private static IEnumerable<ExpressionConverter.Translator> GetTranslators()
		{
			yield return new ExpressionConverter.AndAlsoTranslator();
			yield return new ExpressionConverter.OrElseTranslator();
			yield return new ExpressionConverter.LessThanTranslator();
			yield return new ExpressionConverter.LessThanOrEqualsTranslator();
			yield return new ExpressionConverter.GreaterThanTranslator();
			yield return new ExpressionConverter.GreaterThanOrEqualsTranslator();
			yield return new ExpressionConverter.EqualsTranslator();
			yield return new ExpressionConverter.NotEqualsTranslator();
			yield return new ExpressionConverter.ConvertTranslator();
			yield return new ExpressionConverter.ConstantTranslator();
			yield return new ExpressionConverter.NotTranslator();
			yield return new ExpressionConverter.MemberAccessTranslator();
			yield return new ExpressionConverter.ParameterTranslator();
			yield return new ExpressionConverter.MemberInitTranslator();
			yield return new ExpressionConverter.NewTranslator();
			yield return new ExpressionConverter.AddTranslator();
			yield return new ExpressionConverter.ConditionalTranslator();
			yield return new ExpressionConverter.DivideTranslator();
			yield return new ExpressionConverter.ModuloTranslator();
			yield return new ExpressionConverter.SubtractTranslator();
			yield return new ExpressionConverter.MultiplyTranslator();
			yield return new ExpressionConverter.PowerTranslator();
			yield return new ExpressionConverter.NegateTranslator();
			yield return new ExpressionConverter.UnaryPlusTranslator();
			yield return new ExpressionConverter.MethodCallTranslator();
			yield return new ExpressionConverter.CoalesceTranslator();
			yield return new ExpressionConverter.AsTranslator();
			yield return new ExpressionConverter.IsTranslator();
			yield return new ExpressionConverter.QuoteTranslator();
			yield return new ExpressionConverter.AndTranslator();
			yield return new ExpressionConverter.OrTranslator();
			yield return new ExpressionConverter.ExclusiveOrTranslator();
			yield return new ExpressionConverter.ExtensionTranslator();
			yield return new ExpressionConverter.NewArrayInitTranslator();
			yield return new ExpressionConverter.ListInitTranslator();
			yield return new ExpressionConverter.NotSupportedTranslator(new ExpressionType[]
			{
				ExpressionType.LeftShift,
				ExpressionType.RightShift,
				ExpressionType.ArrayLength,
				ExpressionType.ArrayIndex,
				ExpressionType.Invoke,
				ExpressionType.Lambda,
				ExpressionType.NewArrayBounds
			});
			yield break;
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000B15DD File Offset: 0x000AF7DD
		private EdmItemCollection EdmItemCollection
		{
			get
			{
				return (EdmItemCollection)this._funcletizer.RootContext.MetadataWorkspace.GetItemCollection(DataSpace.CSpace, true);
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000B15FB File Offset: 0x000AF7FB
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return ((StoreItemCollection)this._funcletizer.RootContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)).ProviderManifest;
			}
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000B161D File Offset: 0x000AF81D
		internal IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> GetParameters()
		{
			if (this._parameters != null)
			{
				return this._parameters;
			}
			return null;
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000B162F File Offset: 0x000AF82F
		internal MergeOption? PropagatedMergeOption
		{
			get
			{
				return this._mergeOption;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000B1637 File Offset: 0x000AF837
		internal Span PropagatedSpan
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000B163F File Offset: 0x000AF83F
		internal Func<bool> RecompileRequired
		{
			get
			{
				return this._recompileRequired;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x000B1647 File Offset: 0x000AF847
		// (set) Token: 0x060036DD RID: 14045 RVA: 0x000B164F File Offset: 0x000AF84F
		internal int IgnoreInclude
		{
			get
			{
				return this._ignoreInclude;
			}
			set
			{
				this._ignoreInclude = value;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000B1658 File Offset: 0x000AF858
		internal AliasGenerator AliasGenerator
		{
			get
			{
				return this._aliasGenerator;
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000B1660 File Offset: 0x000AF860
		internal DbExpression Convert()
		{
			DbExpression dbExpression = this.TranslateExpression(this._expression);
			if (!this.TryGetSpan(dbExpression, out this._span))
			{
				this._span = null;
			}
			return dbExpression;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000B1691 File Offset: 0x000AF891
		internal static bool CanFuncletizePropertyInfo(PropertyInfo propertyInfo)
		{
			return ExpressionConverter.MemberAccessTranslator.CanFuncletizePropertyInfo(propertyInfo);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000B1699 File Offset: 0x000AF899
		internal bool CanIncludeSpanInfo()
		{
			return this._ignoreInclude == 0;
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000B16A4 File Offset: 0x000AF8A4
		private void NotifyMergeOption(MergeOption mergeOption)
		{
			if (this._mergeOption == null)
			{
				this._mergeOption = new MergeOption?(mergeOption);
			}
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000B16C0 File Offset: 0x000AF8C0
		internal void ValidateInitializerMetadata(InitializerMetadata metadata)
		{
			InitializerMetadata initializerMetadata;
			if (this._initializers != null && this._initializers.TryGetValue(metadata.ClrType, out initializerMetadata))
			{
				if (!metadata.Equals(initializerMetadata))
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedHeterogeneousInitializers(ExpressionConverter.DescribeClrType(metadata.ClrType)));
				}
			}
			else
			{
				if (this._initializers == null)
				{
					this._initializers = new Dictionary<Type, InitializerMetadata>();
				}
				this._initializers.Add(metadata.ClrType, metadata);
			}
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000B1730 File Offset: 0x000AF930
		private void AddParameter(QueryParameterExpression queryParameter)
		{
			if (this._parameters == null)
			{
				this._parameters = new List<Tuple<ObjectParameter, QueryParameterExpression>>();
			}
			if (!this._parameters.Select((Tuple<ObjectParameter, QueryParameterExpression> p) => p.Item2).Contains(queryParameter))
			{
				ObjectParameter objectParameter = new ObjectParameter(queryParameter.ParameterReference.ParameterName, queryParameter.Type);
				this._parameters.Add(new Tuple<ObjectParameter, QueryParameterExpression>(objectParameter, queryParameter));
			}
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000B17AB File Offset: 0x000AF9AB
		private bool IsQueryRoot(Expression Expression)
		{
			return this._expression == Expression;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B17B8 File Offset: 0x000AF9B8
		private DbExpression AddSpanMapping(DbExpression expression, Span span)
		{
			if (span != null && this.CanIncludeSpanInfo())
			{
				if (this._spanMappings == null)
				{
					this._spanMappings = new Dictionary<DbExpression, Span>();
				}
				Span span2 = null;
				if (this._spanMappings.TryGetValue(expression, out span2))
				{
					foreach (Span.SpanPath spanPath in span.SpanList)
					{
						span2.AddSpanPath(spanPath);
					}
					this._spanMappings[expression] = span2;
				}
				else
				{
					this._spanMappings[expression] = span;
				}
			}
			return expression;
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000B1858 File Offset: 0x000AFA58
		private bool TryGetSpan(DbExpression expression, out Span span)
		{
			if (this._spanMappings != null)
			{
				return this._spanMappings.TryGetValue(expression, out span);
			}
			span = null;
			return false;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000B1874 File Offset: 0x000AFA74
		private void ApplySpanMapping(DbExpression from, DbExpression to)
		{
			Span span;
			if (this.TryGetSpan(from, out span))
			{
				this.AddSpanMapping(to, span);
			}
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000B1898 File Offset: 0x000AFA98
		private void UnifySpanMappings(DbExpression left, DbExpression right, DbExpression to)
		{
			Span span = null;
			Span span2 = null;
			bool flag = this.TryGetSpan(left, out span);
			bool flag2 = this.TryGetSpan(right, out span2);
			if (!flag && !flag2)
			{
				return;
			}
			this.AddSpanMapping(to, Span.CopyUnion(span, span2));
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000B18D4 File Offset: 0x000AFAD4
		private DbDistinctExpression Distinct(DbExpression argument)
		{
			DbDistinctExpression dbDistinctExpression = argument.Distinct();
			this.ApplySpanMapping(argument, dbDistinctExpression);
			return dbDistinctExpression;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000B18F4 File Offset: 0x000AFAF4
		private DbExceptExpression Except(DbExpression left, DbExpression right)
		{
			DbExceptExpression dbExceptExpression = left.Except(right);
			this.ApplySpanMapping(left, dbExceptExpression);
			return dbExceptExpression;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000B1914 File Offset: 0x000AFB14
		private DbExpression Filter(DbExpressionBinding input, DbExpression predicate)
		{
			DbExpression dbExpression = this._orderByLifter.Filter(input, predicate);
			this.ApplySpanMapping(input.Expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000B1940 File Offset: 0x000AFB40
		private DbIntersectExpression Intersect(DbExpression left, DbExpression right)
		{
			DbIntersectExpression dbIntersectExpression = left.Intersect(right);
			this.UnifySpanMappings(left, right, dbIntersectExpression);
			return dbIntersectExpression;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000B1960 File Offset: 0x000AFB60
		private DbExpression Limit(DbExpression argument, DbExpression limit)
		{
			DbExpression dbExpression = this._orderByLifter.Limit(argument, limit);
			this.ApplySpanMapping(argument, dbExpression);
			return dbExpression;
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000B1984 File Offset: 0x000AFB84
		private DbExpression OfType(DbExpression argument, TypeUsage ofType)
		{
			DbExpression dbExpression = this._orderByLifter.OfType(argument, ofType);
			this.ApplySpanMapping(argument, dbExpression);
			return dbExpression;
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000B19A8 File Offset: 0x000AFBA8
		private DbExpression Project(DbExpressionBinding input, DbExpression projection)
		{
			DbExpression dbExpression = this._orderByLifter.Project(input, projection);
			if (projection.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)projection).VariableName.Equals(input.VariableName, StringComparison.Ordinal))
			{
				this.ApplySpanMapping(input.Expression, dbExpression);
			}
			return dbExpression;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000B19F4 File Offset: 0x000AFBF4
		private DbSortExpression Sort(DbExpressionBinding input, IList<DbSortClause> keys)
		{
			DbSortExpression dbSortExpression = input.Sort(keys);
			this.ApplySpanMapping(input.Expression, dbSortExpression);
			return dbSortExpression;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000B1A18 File Offset: 0x000AFC18
		private DbExpression Skip(DbExpressionBinding input, DbExpression skipCount)
		{
			DbExpression dbExpression = this._orderByLifter.Skip(input, skipCount);
			this.ApplySpanMapping(input.Expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000B1A44 File Offset: 0x000AFC44
		private DbUnionAllExpression UnionAll(DbExpression left, DbExpression right)
		{
			DbUnionAllExpression dbUnionAllExpression = left.UnionAll(right);
			this.UnifySpanMappings(left, right, dbUnionAllExpression);
			return dbUnionAllExpression;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000B1A64 File Offset: 0x000AFC64
		private TypeUsage GetCastTargetType(TypeUsage fromType, Type toClrType, Type fromClrType, bool preserveCastForDateTime)
		{
			if (fromClrType != null && fromClrType.IsGenericType() && toClrType.IsGenericType() && (fromClrType.GetGenericTypeDefinition() == typeof(ObjectQuery<>) || fromClrType.GetGenericTypeDefinition() == typeof(IQueryable<>) || fromClrType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)) && (toClrType.GetGenericTypeDefinition() == typeof(ObjectQuery<>) || toClrType.GetGenericTypeDefinition() == typeof(IQueryable<>) || toClrType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)) && fromClrType.GetGenericArguments()[0] == toClrType.GetGenericArguments()[0])
			{
				return null;
			}
			if (fromClrType != null && TypeSystem.GetNonNullableType(fromClrType).IsEnum && toClrType == typeof(Enum))
			{
				return null;
			}
			TypeUsage typeUsage;
			if (this.TryGetValueLayerType(toClrType, out typeUsage) && ExpressionConverter.CanOmitCast(fromType, typeUsage, preserveCastForDateTime))
			{
				return null;
			}
			typeUsage = ExpressionConverter.ValidateAndAdjustCastTypes(typeUsage, fromType, toClrType, fromClrType);
			return typeUsage;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000B1B84 File Offset: 0x000AFD84
		private static TypeUsage ValidateAndAdjustCastTypes(TypeUsage toType, TypeUsage fromType, Type toClrType, Type fromClrType)
		{
			if (toType == null || !TypeSemantics.IsScalarType(toType) || !TypeSemantics.IsScalarType(fromType))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedCast(ExpressionConverter.DescribeClrType(fromClrType), ExpressionConverter.DescribeClrType(toClrType)));
			}
			PrimitiveTypeKind primitiveTypeKind = Helper.AsPrimitive(fromType.EdmType).PrimitiveTypeKind;
			if (Helper.AsPrimitive(toType.EdmType).PrimitiveTypeKind == PrimitiveTypeKind.Decimal)
			{
				if (primitiveTypeKind != PrimitiveTypeKind.Byte && primitiveTypeKind - PrimitiveTypeKind.SByte > 3)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedCastToDecimal);
				}
				toType = TypeUsage.CreateDecimalTypeUsage((PrimitiveType)toType.EdmType, 19, 0);
			}
			return toType;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000B1C0C File Offset: 0x000AFE0C
		private static bool CanOmitCast(TypeUsage fromType, TypeUsage toType, bool preserveCastForDateTime)
		{
			bool flag = TypeSemantics.IsPrimitiveType(fromType);
			if (flag && preserveCastForDateTime && ((PrimitiveType)fromType.EdmType).PrimitiveTypeKind == PrimitiveTypeKind.DateTime)
			{
				return false;
			}
			if (ExpressionConverter.TypeUsageEquals(fromType, toType))
			{
				return true;
			}
			if (flag)
			{
				return fromType.EdmType.EdmEquals(toType.EdmType);
			}
			return TypeSemantics.IsSubTypeOf(fromType, toType);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000B1C64 File Offset: 0x000AFE64
		private TypeUsage GetIsOrAsTargetType(ExpressionType operationType, Type toClrType, Type fromClrType)
		{
			TypeUsage typeUsage;
			if (!this.TryGetValueLayerType(toClrType, out typeUsage) || (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsComplexType(typeUsage)))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedIsOrAs(operationType, ExpressionConverter.DescribeClrType(fromClrType), ExpressionConverter.DescribeClrType(toClrType)));
			}
			return typeUsage;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000B1CAC File Offset: 0x000AFEAC
		private DbExpression TranslateInlineQueryOfT(ObjectQuery inlineQuery)
		{
			if (this._funcletizer.RootContext != inlineQuery.QueryState.ObjectContext)
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedDifferentContexts);
			}
			if (this._inlineEntitySqlQueries == null)
			{
				this._inlineEntitySqlQueries = new HashSet<ObjectQuery>();
			}
			bool flag = this._inlineEntitySqlQueries.Add(inlineQuery);
			EntitySqlQueryState entitySqlQueryState = (EntitySqlQueryState)inlineQuery.QueryState;
			ObjectParameterCollection parameters = inlineQuery.QueryState.Parameters;
			DbExpression dbExpression;
			if (!this._funcletizer.IsCompiledQuery || parameters == null || parameters.Count == 0)
			{
				if (flag && parameters != null)
				{
					if (this._parameters == null)
					{
						this._parameters = new List<Tuple<ObjectParameter, QueryParameterExpression>>();
					}
					foreach (ObjectParameter objectParameter in inlineQuery.QueryState.Parameters)
					{
						this._parameters.Add(new Tuple<ObjectParameter, QueryParameterExpression>(objectParameter.ShallowCopy(), null));
					}
				}
				dbExpression = entitySqlQueryState.Parse();
			}
			else
			{
				dbExpression = entitySqlQueryState.Parse();
				dbExpression = ExpressionConverter.ParameterReferenceRemover.RemoveParameterReferences(dbExpression, parameters);
			}
			return dbExpression;
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000B1DBC File Offset: 0x000AFFBC
		private DbExpression CreateCastExpression(DbExpression source, Type toClrType, Type fromClrType)
		{
			DbExpression dbExpression = this.NormalizeSetSource(source);
			if (source != dbExpression && this.GetCastTargetType(dbExpression.ResultType, toClrType, fromClrType, true) == null)
			{
				return source;
			}
			TypeUsage castTargetType = this.GetCastTargetType(source.ResultType, toClrType, fromClrType, true);
			if (castTargetType == null)
			{
				return source;
			}
			return source.CastTo(castTargetType);
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000B1E04 File Offset: 0x000B0004
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, out DbExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			binding = input.BindAs(this._aliasGenerator.Next());
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000B1E30 File Offset: 0x000B0030
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, string bindingName, out DbExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			binding = input.BindAs(bindingName);
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000B1E54 File Offset: 0x000B0054
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, out DbGroupExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			string text = this._aliasGenerator.Next();
			binding = input.GroupBindAs(text, string.Format(CultureInfo.InvariantCulture, "Group{0}", new object[] { text }));
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000B1EA8 File Offset: 0x000B00A8
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input)
		{
			Binding binding = new Binding(lambda.Parameters[0], input);
			this._bindingContext.PushBindingScope(binding);
			this._ignoreInclude++;
			DbExpression dbExpression = this.TranslateExpression(lambda.Body);
			this._ignoreInclude--;
			this._bindingContext.PopBindingScope();
			return dbExpression;
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000B1F08 File Offset: 0x000B0108
		private DbExpression NormalizeSetSource(DbExpression input)
		{
			Span span;
			if (input.ExpressionKind == DbExpressionKind.Project && !this.TryGetSpan(input, out span))
			{
				DbProjectExpression dbProjectExpression = (DbProjectExpression)input;
				if (dbProjectExpression.Projection == dbProjectExpression.Input.Variable)
				{
					input = dbProjectExpression.Input.Expression;
				}
			}
			InitializerMetadata initializerMetadata;
			if (InitializerMetadata.TryGetInitializerMetadata(input.ResultType, out initializerMetadata))
			{
				if (initializerMetadata.Kind == InitializerMetadataKind.Grouping)
				{
					input = input.Property("Group");
				}
				else if (initializerMetadata.Kind == InitializerMetadataKind.EntityCollection)
				{
					input = input.Property("Elements");
				}
			}
			return input;
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000B1F90 File Offset: 0x000B0190
		private LambdaExpression GetLambdaExpression(MethodCallExpression callExpression, int argumentOrdinal)
		{
			Expression expression = callExpression.Arguments[argumentOrdinal];
			return (LambdaExpression)this.GetLambdaExpression(expression);
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000B1FB8 File Offset: 0x000B01B8
		private Expression GetLambdaExpression(Expression argument)
		{
			if (ExpressionType.Lambda == argument.NodeType)
			{
				return argument;
			}
			if (ExpressionType.Quote == argument.NodeType)
			{
				return this.GetLambdaExpression(((UnaryExpression)argument).Operand);
			}
			if (ExpressionType.Call == argument.NodeType)
			{
				if (typeof(Expression).IsAssignableFrom(argument.Type))
				{
					Func<Expression> func = Expression.Lambda<Func<Expression>>(argument, new ParameterExpression[0]).Compile();
					return this.GetLambdaExpression(func());
				}
			}
			else if (ExpressionType.Invoke == argument.NodeType && typeof(Expression).IsAssignableFrom(argument.Type))
			{
				Func<Expression> func2 = Expression.Lambda<Func<Expression>>(argument, new ParameterExpression[0]).Compile();
				return this.GetLambdaExpression(func2());
			}
			throw new InvalidOperationException(Strings.ADP_InternalProviderError(1025));
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000B2080 File Offset: 0x000B0280
		private DbExpression TranslateSet(Expression linq)
		{
			return this.NormalizeSetSource(this.TranslateExpression(linq));
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000B2090 File Offset: 0x000B0290
		private DbExpression TranslateExpression(Expression linq)
		{
			DbExpression dbExpression;
			if (!this._bindingContext.TryGetBoundExpression(linq, out dbExpression))
			{
				ExpressionConverter.Translator translator;
				if (!ExpressionConverter._translators.TryGetValue(linq.NodeType, out translator))
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownLinqNodeType, -1, linq.NodeType.ToString());
				}
				dbExpression = translator.Translate(this, linq);
			}
			return dbExpression;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000B20F0 File Offset: 0x000B02F0
		private DbExpression AlignTypes(DbExpression cqt, Type toClrType)
		{
			Type type = null;
			TypeUsage castTargetType = this.GetCastTargetType(cqt.ResultType, toClrType, type, false);
			if (castTargetType != null)
			{
				return cqt.CastTo(castTargetType);
			}
			return cqt;
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000B211C File Offset: 0x000B031C
		private void CheckInitializerType(Type type)
		{
			TypeUsage typeUsage;
			if (this._funcletizer.RootContext.Perspective.TryGetType(type, out typeUsage))
			{
				BuiltInTypeKind builtInTypeKind = typeUsage.EdmType.BuiltInTypeKind;
				if (BuiltInTypeKind.EntityType == builtInTypeKind || BuiltInTypeKind.ComplexType == builtInTypeKind)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedNominalType(typeUsage.EdmType.FullName));
				}
			}
			if (TypeSystem.IsSequenceType(type))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedEnumerableType(ExpressionConverter.DescribeClrType(type)));
			}
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000B2188 File Offset: 0x000B0388
		private static bool TypeUsageEquals(TypeUsage left, TypeUsage right)
		{
			if (left.EdmType.EdmEquals(right.EdmType))
			{
				return true;
			}
			if (BuiltInTypeKind.CollectionType == left.EdmType.BuiltInTypeKind && BuiltInTypeKind.CollectionType == right.EdmType.BuiltInTypeKind)
			{
				return ExpressionConverter.TypeUsageEquals(((CollectionType)left.EdmType).TypeUsage, ((CollectionType)right.EdmType).TypeUsage);
			}
			return BuiltInTypeKind.PrimitiveType == left.EdmType.BuiltInTypeKind && BuiltInTypeKind.PrimitiveType == right.EdmType.BuiltInTypeKind && ((PrimitiveType)left.EdmType).ClrEquivalentType.Equals(((PrimitiveType)right.EdmType).ClrEquivalentType);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000B2234 File Offset: 0x000B0434
		private TypeUsage GetValueLayerType(Type linqType)
		{
			TypeUsage typeUsage;
			if (!this.TryGetValueLayerType(linqType, out typeUsage))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedType(linqType));
			}
			return typeUsage;
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000B225C File Offset: 0x000B045C
		private bool TryGetValueLayerType(Type linqType, out TypeUsage type)
		{
			Type type2 = TypeSystem.GetNonNullableType(linqType);
			if (type2.IsEnum() && this.EdmItemCollection.EdmVersion < 3.0)
			{
				type2 = type2.GetEnumUnderlyingType();
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.TryGetPrimitiveTypeKind(type2, out primitiveTypeKind))
			{
				type = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
				return true;
			}
			Type elementType = TypeSystem.GetElementType(type2);
			TypeUsage typeUsage;
			if (elementType != type2 && this.TryGetValueLayerType(elementType, out typeUsage))
			{
				type = TypeHelpers.CreateCollectionTypeUsage(typeUsage);
				return true;
			}
			this._perspective.MetadataWorkspace.ImplicitLoadAssemblyForType(linqType, null);
			if (!this._perspective.TryGetTypeByName(type2.FullNameWithNesting(), false, out type) && type2.IsEnum() && ClrProviderManifest.TryGetPrimitiveTypeKind(type2.GetEnumUnderlyingType(), out primitiveTypeKind))
			{
				type = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
			}
			return type != null;
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000B2324 File Offset: 0x000B0524
		private static void VerifyTypeSupportedForComparison(Type clrType, TypeUsage edmType, Stack<EdmMember> memberPath, bool isNullComparison)
		{
			BuiltInTypeKind builtInTypeKind = edmType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
			{
				if (builtInTypeKind - BuiltInTypeKind.EntityType > 1 && builtInTypeKind != BuiltInTypeKind.PrimitiveType)
				{
					goto IL_005F;
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
			{
				if (builtInTypeKind != BuiltInTypeKind.RowType)
				{
					goto IL_005F;
				}
				InitializerMetadata initializerMetadata;
				if (!InitializerMetadata.TryGetInitializerMetadata(edmType, out initializerMetadata) || initializerMetadata.Kind == InitializerMetadataKind.ProjectionInitializer || initializerMetadata.Kind == InitializerMetadataKind.ProjectionNew)
				{
					if (!isNullComparison)
					{
						ExpressionConverter.VerifyRowTypeSupportedForComparison(clrType, (RowType)edmType.EdmType, memberPath, isNullComparison);
					}
					return;
				}
				goto IL_005F;
			}
			return;
			IL_005F:
			if (memberPath == null)
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedComparison(ExpressionConverter.DescribeClrType(clrType)));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EdmMember edmMember in memberPath)
			{
				stringBuilder.Append(Strings.ELinq_UnsupportedRowMemberComparison(edmMember.Name));
			}
			stringBuilder.Append(Strings.ELinq_UnsupportedRowTypeComparison(ExpressionConverter.DescribeClrType(clrType)));
			throw new NotSupportedException(Strings.ELinq_UnsupportedRowComparison(stringBuilder.ToString()));
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000B241C File Offset: 0x000B061C
		private static void VerifyRowTypeSupportedForComparison(Type clrType, RowType rowType, Stack<EdmMember> memberPath, bool isNullComparison)
		{
			foreach (EdmMember edmMember in rowType.Properties)
			{
				if (memberPath == null)
				{
					memberPath = new Stack<EdmMember>();
				}
				memberPath.Push(edmMember);
				ExpressionConverter.VerifyTypeSupportedForComparison(clrType, edmMember.TypeUsage, memberPath, isNullComparison);
				memberPath.Pop();
			}
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000B2490 File Offset: 0x000B0690
		internal static string DescribeClrType(Type clrType)
		{
			if (ExpressionConverter.IsCSharpGeneratedClass(clrType.Name, "DisplayClass") || ExpressionConverter.IsVBGeneratedClass(clrType.Name, "Closure"))
			{
				return Strings.ELinq_ClosureType;
			}
			if (ExpressionConverter.IsCSharpGeneratedClass(clrType.Name, "AnonymousType") || ExpressionConverter.IsVBGeneratedClass(clrType.Name, "AnonymousType"))
			{
				return Strings.ELinq_AnonymousType;
			}
			return clrType.FullName;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000B24F7 File Offset: 0x000B06F7
		private static bool IsCSharpGeneratedClass(string typeName, string pattern)
		{
			return typeName.Contains("<>") && typeName.Contains("__") && typeName.Contains(pattern);
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000B251C File Offset: 0x000B071C
		private static bool IsVBGeneratedClass(string typeName, string pattern)
		{
			return typeName.Contains("_") && typeName.Contains("$") && typeName.Contains(pattern);
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000B2541 File Offset: 0x000B0741
		private static DbExpression CreateIsNullExpression(DbExpression operand, Type operandClrType)
		{
			ExpressionConverter.VerifyTypeSupportedForComparison(operandClrType, operand.ResultType, null, true);
			return operand.IsNull();
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000B2558 File Offset: 0x000B0758
		private DbExpression CreateEqualsExpression(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern, Type leftClrType, Type rightClrType)
		{
			ExpressionConverter.VerifyTypeSupportedForComparison(leftClrType, left.ResultType, null, false);
			ExpressionConverter.VerifyTypeSupportedForComparison(rightClrType, right.ResultType, null, false);
			TypeUsage resultType = left.ResultType;
			TypeUsage resultType2 = right.ResultType;
			TypeUsage typeUsage;
			if (resultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.RefType && resultType2.EdmType.BuiltInTypeKind == BuiltInTypeKind.RefType && !TypeSemantics.TryGetCommonType(resultType, resultType2, out typeUsage))
			{
				RefType refType = left.ResultType.EdmType as RefType;
				RefType refType2 = right.ResultType.EdmType as RefType;
				throw new NotSupportedException(Strings.ELinq_UnsupportedRefComparison(refType.ElementType.FullName, refType2.ElementType.FullName));
			}
			return this.RecursivelyRewriteEqualsExpression(left, right, pattern);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000B2604 File Offset: 0x000B0804
		private DbExpression RecursivelyRewriteEqualsExpression(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			RowType rowType = left.ResultType.EdmType as RowType;
			RowType rowType2 = right.ResultType.EdmType as RowType;
			if (rowType != null || rowType2 != null)
			{
				if (rowType != null && rowType2 != null)
				{
					DbExpression dbExpression = null;
					foreach (EdmProperty edmProperty in rowType.Properties)
					{
						DbPropertyExpression dbPropertyExpression = left.Property(edmProperty);
						DbPropertyExpression dbPropertyExpression2 = right.Property(edmProperty);
						DbExpression dbExpression2 = this.RecursivelyRewriteEqualsExpression(dbPropertyExpression, dbPropertyExpression2, pattern);
						if (dbExpression == null)
						{
							dbExpression = dbExpression2;
						}
						else
						{
							dbExpression = dbExpression.And(dbExpression2);
						}
					}
					return dbExpression;
				}
				return DbExpressionBuilder.False;
			}
			else
			{
				if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					return this.ImplementEquality(left, right, pattern);
				}
				return this.ImplementEquality(left, right, ExpressionConverter.EqualsPattern.Store);
			}
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000B26E8 File Offset: 0x000B08E8
		private DbExpression ImplementEquality(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			DbExpressionKind expressionKind = left.ExpressionKind;
			if (expressionKind != DbExpressionKind.Constant)
			{
				if (expressionKind != DbExpressionKind.Null)
				{
					DbExpressionKind dbExpressionKind = right.ExpressionKind;
					if (dbExpressionKind == DbExpressionKind.Constant)
					{
						return this.ImplementEqualityConstantAndUnknown((DbConstantExpression)right, left, pattern);
					}
					if (dbExpressionKind != DbExpressionKind.Null)
					{
						return this.ImplementEqualityUnknownArguments(left, right, pattern);
					}
					return left.IsNull();
				}
				else
				{
					DbExpressionKind dbExpressionKind = right.ExpressionKind;
					if (dbExpressionKind == DbExpressionKind.Constant)
					{
						return DbExpressionBuilder.False;
					}
					if (dbExpressionKind != DbExpressionKind.Null)
					{
						return right.IsNull();
					}
					return DbExpressionBuilder.True;
				}
			}
			else
			{
				DbExpressionKind dbExpressionKind = right.ExpressionKind;
				if (dbExpressionKind == DbExpressionKind.Constant)
				{
					return left.Equal(right);
				}
				if (dbExpressionKind != DbExpressionKind.Null)
				{
					return this.ImplementEqualityConstantAndUnknown((DbConstantExpression)left, right, pattern);
				}
				return DbExpressionBuilder.False;
			}
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000B278C File Offset: 0x000B098C
		private DbExpression ImplementEqualityConstantAndUnknown(DbConstantExpression constant, DbExpression unknown, ExpressionConverter.EqualsPattern pattern)
		{
			if (pattern <= ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable)
			{
				return constant.Equal(unknown);
			}
			if (pattern != ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable)
			{
				return null;
			}
			if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
			{
				return constant.Equal(unknown);
			}
			return constant.Equal(unknown).And(unknown.IsNull().Not());
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000B27E4 File Offset: 0x000B09E4
		private DbExpression ImplementEqualityUnknownArguments(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			switch (pattern)
			{
			case ExpressionConverter.EqualsPattern.Store:
				return left.Equal(right);
			case ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable:
				return left.Equal(right).Or(left.IsNull().And(right.IsNull()));
			case ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable:
			{
				DbComparisonExpression dbComparisonExpression = left.Equal(right);
				DbAndExpression dbAndExpression = left.IsNull().And(right.IsNull());
				if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					return dbComparisonExpression.Or(dbAndExpression);
				}
				DbOrExpression dbOrExpression = left.IsNull().Or(right.IsNull());
				return dbComparisonExpression.And(dbOrExpression.Not()).Or(dbAndExpression);
			}
			default:
				return null;
			}
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000B2890 File Offset: 0x000B0A90
		private DbExpression TranslateLike(MethodCallExpression call)
		{
			char c;
			bool flag = this.ProviderManifest.SupportsEscapingLikeArgument(out c);
			Expression expression = call.Arguments[0];
			Expression expression2 = call.Arguments[1];
			Expression expression3 = ((call.Arguments.Count > 2) ? call.Arguments[2] : null);
			if (!flag && expression3 != null)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportEscapingLikeArgument);
			}
			DbExpression dbExpression = this.TranslateExpression(expression2);
			DbExpression dbExpression2 = ((expression3 != null) ? this.TranslateExpression(expression3) : null);
			DbExpression dbExpression3 = this.TranslateExpression(expression);
			if (expression3 == null)
			{
				return dbExpression3.Like(dbExpression);
			}
			return dbExpression3.Like(dbExpression, dbExpression2);
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000B292C File Offset: 0x000B0B2C
		private DbExpression TranslateFunctionIntoLike(MethodCallExpression call, bool insertPercentAtStart, bool insertPercentAtEnd, Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression> defaultTranslator)
		{
			char c;
			bool flag = this.ProviderManifest.SupportsEscapingLikeArgument(out c);
			bool flag2 = false;
			bool flag3 = true;
			Expression expression = call.Arguments[0];
			Expression @object = call.Object;
			QueryParameterExpression queryParameterExpression = expression as QueryParameterExpression;
			if (flag && queryParameterExpression != null)
			{
				flag2 = true;
				MethodInfo method = typeof(ExpressionConverter).GetMethod("PreparePattern", BindingFlags.Static | BindingFlags.NonPublic);
				ParameterExpression parameterExpression;
				Expression<Func<string, Tuple<string, bool>>> expression2 = Expression.Lambda<Func<string, Tuple<string, bool>>>(Expression.Call(method, parameterExpression, Expression.Constant(insertPercentAtStart), Expression.Constant(insertPercentAtEnd), Expression.Constant(this.ProviderManifest)), new ParameterExpression[] { parameterExpression });
				expression = queryParameterExpression.EscapeParameterForLike(expression2);
			}
			DbExpression dbExpression = this.TranslateExpression(expression);
			DbExpression dbExpression2 = this.TranslateExpression(@object);
			if (flag && dbExpression.ExpressionKind == DbExpressionKind.Constant)
			{
				flag2 = true;
				DbConstantExpression dbConstantExpression = (DbConstantExpression)dbExpression;
				Tuple<string, bool> tuple = ExpressionConverter.PreparePattern((string)dbConstantExpression.Value, insertPercentAtStart, insertPercentAtEnd, this.ProviderManifest);
				string item = tuple.Item1;
				flag3 = tuple.Item2;
				dbExpression = dbConstantExpression.ResultType.Constant(item);
			}
			DbExpression dbExpression3;
			if (flag2)
			{
				if (flag3)
				{
					DbConstantExpression dbConstantExpression2 = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String).Constant(new string(new char[] { c }));
					dbExpression3 = dbExpression2.Like(dbExpression, dbConstantExpression2);
				}
				else
				{
					dbExpression3 = dbExpression2.Like(dbExpression);
				}
			}
			else
			{
				dbExpression3 = defaultTranslator(this, call, dbExpression, dbExpression2);
			}
			return dbExpression3;
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000B2A98 File Offset: 0x000B0C98
		private static Tuple<string, bool> PreparePattern(string patternValue, bool insertPercentAtStart, bool insertPercentAtEnd, DbProviderManifest providerManifest)
		{
			if (patternValue == null)
			{
				return new Tuple<string, bool>(null, false);
			}
			string text = providerManifest.EscapeLikeArgument(patternValue);
			if (text == null)
			{
				throw new ProviderIncompatibleException(Strings.ProviderEscapeLikeArgumentReturnedNull);
			}
			bool flag = patternValue != text;
			StringBuilder stringBuilder = new StringBuilder();
			if (insertPercentAtStart)
			{
				stringBuilder.Append("%");
			}
			stringBuilder.Append(text);
			if (insertPercentAtEnd)
			{
				stringBuilder.Append("%");
			}
			return new Tuple<string, bool>(stringBuilder.ToString(), flag);
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000B2B08 File Offset: 0x000B0D08
		private DbFunctionExpression TranslateIntoCanonicalFunction(string functionName, Expression Expression, params Expression[] linqArguments)
		{
			DbExpression[] array = new DbExpression[linqArguments.Length];
			for (int i = 0; i < linqArguments.Length; i++)
			{
				array[i] = this.TranslateExpression(linqArguments[i]);
			}
			return this.CreateCanonicalFunction(functionName, Expression, array);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000B2B44 File Offset: 0x000B0D44
		private DbFunctionExpression CreateCanonicalFunction(string functionName, Expression Expression, params DbExpression[] translatedArguments)
		{
			List<TypeUsage> list = new List<TypeUsage>(translatedArguments.Length);
			foreach (DbExpression dbExpression in translatedArguments)
			{
				list.Add(dbExpression.ResultType);
			}
			return this.FindCanonicalFunction(functionName, list, false, Expression).Invoke(translatedArguments);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000B2B8A File Offset: 0x000B0D8A
		private EdmFunction FindCanonicalFunction(string functionName, IList<TypeUsage> argumentTypes, bool isGroupAggregateFunction, Expression Expression)
		{
			return this.FindFunction("Edm", functionName, argumentTypes, isGroupAggregateFunction, Expression);
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000B2B9C File Offset: 0x000B0D9C
		private EdmFunction FindFunction(string namespaceName, string functionName, IList<TypeUsage> argumentTypes, bool isGroupAggregateFunction, Expression Expression)
		{
			IList<EdmFunction> list;
			if (!this._perspective.TryGetFunctionByName(namespaceName, functionName, false, out list))
			{
				ExpressionConverter.ThrowUnresolvableFunction(Expression);
			}
			bool flag;
			EdmFunction edmFunction = FunctionOverloadResolver.ResolveFunctionOverloads(list, argumentTypes, isGroupAggregateFunction, out flag);
			if (flag || edmFunction == null)
			{
				ExpressionConverter.ThrowUnresolvableFunctionOverload(Expression, flag);
			}
			return edmFunction;
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000B2BE0 File Offset: 0x000B0DE0
		private static void ThrowUnresolvableFunction(Expression Expression)
		{
			if (Expression.NodeType == ExpressionType.Call)
			{
				MethodInfo method = ((MethodCallExpression)Expression).Method;
				throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMethod(method, method.DeclaringType));
			}
			if (Expression.NodeType == ExpressionType.MemberAccess)
			{
				string text;
				Type type;
				MemberInfo memberInfo = TypeSystem.PropertyOrField(((MemberExpression)Expression).Member, out text, out type);
				throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMember(memberInfo, memberInfo.DeclaringType));
			}
			throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForExpression(Expression.NodeType));
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000B2C5C File Offset: 0x000B0E5C
		private static void ThrowUnresolvableFunctionOverload(Expression Expression, bool isAmbiguous)
		{
			if (Expression.NodeType == ExpressionType.Call)
			{
				MethodInfo method = ((MethodCallExpression)Expression).Method;
				if (isAmbiguous)
				{
					throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMethodAmbiguousMatch(method, method.DeclaringType));
				}
				throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMethodNotFound(method, method.DeclaringType));
			}
			else
			{
				if (Expression.NodeType == ExpressionType.MemberAccess)
				{
					string text;
					Type type;
					MemberInfo memberInfo = TypeSystem.PropertyOrField(((MemberExpression)Expression).Member, out text, out type);
					throw new NotSupportedException(Strings.ELinq_UnresolvableStoreFunctionForMember(memberInfo, memberInfo.DeclaringType));
				}
				throw new NotSupportedException(Strings.ELinq_UnresolvableStoreFunctionForExpression(Expression.NodeType));
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000B2CEC File Offset: 0x000B0EEC
		private static DbNewInstanceExpression CreateNewRowExpression(List<KeyValuePair<string, DbExpression>> columns, InitializerMetadata initializerMetadata)
		{
			List<DbExpression> list = new List<DbExpression>(columns.Count);
			List<EdmProperty> list2 = new List<EdmProperty>(columns.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				KeyValuePair<string, DbExpression> keyValuePair = columns[i];
				list.Add(keyValuePair.Value);
				list2.Add(new EdmProperty(keyValuePair.Key, keyValuePair.Value.ResultType));
			}
			return TypeUsage.Create(new RowType(list2, initializerMetadata)).New(list);
		}

		// Token: 0x040011CF RID: 4559
		private readonly Funcletizer _funcletizer;

		// Token: 0x040011D0 RID: 4560
		private readonly Perspective _perspective;

		// Token: 0x040011D1 RID: 4561
		private readonly Expression _expression;

		// Token: 0x040011D2 RID: 4562
		private readonly BindingContext _bindingContext;

		// Token: 0x040011D3 RID: 4563
		private Func<bool> _recompileRequired;

		// Token: 0x040011D4 RID: 4564
		private List<Tuple<ObjectParameter, QueryParameterExpression>> _parameters;

		// Token: 0x040011D5 RID: 4565
		private Dictionary<DbExpression, Span> _spanMappings;

		// Token: 0x040011D6 RID: 4566
		private MergeOption? _mergeOption;

		// Token: 0x040011D7 RID: 4567
		private Dictionary<Type, InitializerMetadata> _initializers;

		// Token: 0x040011D8 RID: 4568
		private Span _span;

		// Token: 0x040011D9 RID: 4569
		private HashSet<ObjectQuery> _inlineEntitySqlQueries;

		// Token: 0x040011DA RID: 4570
		private int _ignoreInclude;

		// Token: 0x040011DB RID: 4571
		private readonly AliasGenerator _aliasGenerator = new AliasGenerator("LQ", 0);

		// Token: 0x040011DC RID: 4572
		private readonly ExpressionConverter.OrderByLifter _orderByLifter;

		// Token: 0x040011DD RID: 4573
		private const string s_visualBasicAssemblyFullName = "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x040011DE RID: 4574
		private static readonly Dictionary<ExpressionType, ExpressionConverter.Translator> _translators = ExpressionConverter.InitializeTranslators();

		// Token: 0x040011DF RID: 4575
		internal const string KeyColumnName = "Key";

		// Token: 0x040011E0 RID: 4576
		internal const string GroupColumnName = "Group";

		// Token: 0x040011E1 RID: 4577
		internal const string EntityCollectionOwnerColumnName = "Owner";

		// Token: 0x040011E2 RID: 4578
		internal const string EntityCollectionElementsColumnName = "Elements";

		// Token: 0x040011E3 RID: 4579
		internal const string EdmNamespaceName = "Edm";

		// Token: 0x040011E4 RID: 4580
		private const string Concat = "Concat";

		// Token: 0x040011E5 RID: 4581
		private const string IndexOf = "IndexOf";

		// Token: 0x040011E6 RID: 4582
		private const string Length = "Length";

		// Token: 0x040011E7 RID: 4583
		private const string Right = "Right";

		// Token: 0x040011E8 RID: 4584
		private const string Substring = "Substring";

		// Token: 0x040011E9 RID: 4585
		private const string ToUpper = "ToUpper";

		// Token: 0x040011EA RID: 4586
		private const string ToLower = "ToLower";

		// Token: 0x040011EB RID: 4587
		private const string Trim = "Trim";

		// Token: 0x040011EC RID: 4588
		private const string LTrim = "LTrim";

		// Token: 0x040011ED RID: 4589
		private const string RTrim = "RTrim";

		// Token: 0x040011EE RID: 4590
		private const string Reverse = "Reverse";

		// Token: 0x040011EF RID: 4591
		private const string BitwiseAnd = "BitwiseAnd";

		// Token: 0x040011F0 RID: 4592
		private const string BitwiseOr = "BitwiseOr";

		// Token: 0x040011F1 RID: 4593
		private const string BitwiseNot = "BitwiseNot";

		// Token: 0x040011F2 RID: 4594
		private const string BitwiseXor = "BitwiseXor";

		// Token: 0x040011F3 RID: 4595
		private const string CurrentUtcDateTime = "CurrentUtcDateTime";

		// Token: 0x040011F4 RID: 4596
		private const string CurrentDateTimeOffset = "CurrentDateTimeOffset";

		// Token: 0x040011F5 RID: 4597
		private const string CurrentDateTime = "CurrentDateTime";

		// Token: 0x040011F6 RID: 4598
		private const string Year = "Year";

		// Token: 0x040011F7 RID: 4599
		private const string Month = "Month";

		// Token: 0x040011F8 RID: 4600
		private const string Day = "Day";

		// Token: 0x040011F9 RID: 4601
		private const string Hour = "Hour";

		// Token: 0x040011FA RID: 4602
		private const string Minute = "Minute";

		// Token: 0x040011FB RID: 4603
		private const string Second = "Second";

		// Token: 0x040011FC RID: 4604
		private const string Millisecond = "Millisecond";

		// Token: 0x040011FD RID: 4605
		private const string Like = "Like";

		// Token: 0x040011FE RID: 4606
		private const string AsUnicode = "AsUnicode";

		// Token: 0x040011FF RID: 4607
		private const string AsNonUnicode = "AsNonUnicode";

		// Token: 0x02000A70 RID: 2672
		private class ParameterReferenceRemover : DefaultExpressionVisitor
		{
			// Token: 0x060061D6 RID: 25046 RVA: 0x00153669 File Offset: 0x00151869
			internal static DbExpression RemoveParameterReferences(DbExpression expression, ObjectParameterCollection availableParameters)
			{
				return new ExpressionConverter.ParameterReferenceRemover(availableParameters).VisitExpression(expression);
			}

			// Token: 0x060061D7 RID: 25047 RVA: 0x00153677 File Offset: 0x00151877
			private ParameterReferenceRemover(ObjectParameterCollection availableParams)
			{
				this.objectParameters = availableParams;
			}

			// Token: 0x060061D8 RID: 25048 RVA: 0x00153688 File Offset: 0x00151888
			public override DbExpression Visit(DbParameterReferenceExpression expression)
			{
				Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
				if (!this.objectParameters.Contains(expression.ParameterName))
				{
					return expression;
				}
				ObjectParameter objectParameter = this.objectParameters[expression.ParameterName];
				if (objectParameter.Value == null)
				{
					return expression.ResultType.Null();
				}
				return expression.ResultType.Constant(objectParameter.Value);
			}

			// Token: 0x04002B18 RID: 11032
			private readonly ObjectParameterCollection objectParameters;
		}

		// Token: 0x02000A71 RID: 2673
		private enum EqualsPattern
		{
			// Token: 0x04002B1A RID: 11034
			Store,
			// Token: 0x04002B1B RID: 11035
			PositiveNullEqualityNonComposable,
			// Token: 0x04002B1C RID: 11036
			PositiveNullEqualityComposable
		}

		// Token: 0x02000A72 RID: 2674
		internal sealed class MethodCallTranslator : ExpressionConverter.TypedTranslator<MethodCallExpression>
		{
			// Token: 0x060061D9 RID: 25049 RVA: 0x001536ED File Offset: 0x001518ED
			internal MethodCallTranslator()
				: base(new ExpressionType[] { ExpressionType.Call })
			{
			}

			// Token: 0x060061DA RID: 25050 RVA: 0x00153700 File Offset: 0x00151900
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MethodCallExpression linq)
			{
				SequenceMethod sequenceMethod;
				ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator sequenceMethodTranslator;
				if (ReflectionUtil.TryIdentifySequenceMethod(linq.Method, out sequenceMethod) && ExpressionConverter.MethodCallTranslator._sequenceTranslators.TryGetValue(sequenceMethod, out sequenceMethodTranslator))
				{
					return sequenceMethodTranslator.Translate(parent, linq, sequenceMethod);
				}
				ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator;
				if (ExpressionConverter.MethodCallTranslator.TryGetCallTranslator(linq.Method, out callTranslator))
				{
					return callTranslator.Translate(parent, linq);
				}
				ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator objectQueryCallTranslator;
				if (ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.IsCandidateMethod(linq.Method) && ExpressionConverter.MethodCallTranslator._objectQueryTranslators.TryGetValue(linq.Method.Name, out objectQueryCallTranslator))
				{
					return objectQueryCallTranslator.Translate(parent, linq);
				}
				DbFunctionAttribute dbFunctionAttribute = linq.Method.GetCustomAttributes(false).FirstOrDefault<DbFunctionAttribute>();
				if (dbFunctionAttribute != null)
				{
					return ExpressionConverter.MethodCallTranslator._functionCallTranslator.TranslateFunctionCall(parent, linq, dbFunctionAttribute);
				}
				string name = linq.Method.Name;
				Type[] array;
				if (name != null && name == "Contains" && linq.Method.GetParameters().Count<ParameterInfo>() == 1 && linq.Method.ReturnType.Equals(typeof(bool)) && linq.Method.IsImplementationOfGenericInterfaceMethod(typeof(ICollection<>), out array))
				{
					return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContains(parent, linq.Object, linq.Arguments[0]);
				}
				return ExpressionConverter.MethodCallTranslator._defaultTranslator.Translate(parent, linq);
			}

			// Token: 0x060061DB RID: 25051 RVA: 0x0015382C File Offset: 0x00151A2C
			private static Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> InitializeMethodTranslators()
			{
				Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> dictionary = new Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator>();
				foreach (ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator in ExpressionConverter.MethodCallTranslator.GetCallTranslators())
				{
					foreach (MethodInfo methodInfo in callTranslator.Methods)
					{
						dictionary.Add(methodInfo, callTranslator);
					}
				}
				return dictionary;
			}

			// Token: 0x060061DC RID: 25052 RVA: 0x001538B8 File Offset: 0x00151AB8
			private static Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> InitializeSequenceMethodTranslators()
			{
				Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> dictionary = new Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator>();
				foreach (ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator sequenceMethodTranslator in ExpressionConverter.MethodCallTranslator.GetSequenceMethodTranslators())
				{
					foreach (SequenceMethod sequenceMethod in sequenceMethodTranslator.Methods)
					{
						dictionary.Add(sequenceMethod, sequenceMethodTranslator);
					}
				}
				return dictionary;
			}

			// Token: 0x060061DD RID: 25053 RVA: 0x00153944 File Offset: 0x00151B44
			private static Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> InitializeObjectQueryTranslators()
			{
				Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> dictionary = new Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator>(StringComparer.Ordinal);
				foreach (ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator objectQueryCallTranslator in ExpressionConverter.MethodCallTranslator.GetObjectQueryCallTranslators())
				{
					dictionary[objectQueryCallTranslator.MethodName] = objectQueryCallTranslator;
				}
				return dictionary;
			}

			// Token: 0x060061DE RID: 25054 RVA: 0x001539A4 File Offset: 0x00151BA4
			private static bool TryGetCallTranslator(MethodInfo methodInfo, out ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator)
			{
				if (ExpressionConverter.MethodCallTranslator._methodTranslators.TryGetValue(methodInfo, out callTranslator))
				{
					return true;
				}
				if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == methodInfo.DeclaringType.Assembly().FullName)
				{
					object vbInitializerLock = ExpressionConverter.MethodCallTranslator._vbInitializerLock;
					lock (vbInitializerLock)
					{
						if (!ExpressionConverter.MethodCallTranslator.s_vbMethodsInitialized)
						{
							ExpressionConverter.MethodCallTranslator.InitializeVBMethods(methodInfo.DeclaringType.Assembly());
							ExpressionConverter.MethodCallTranslator.s_vbMethodsInitialized = true;
						}
						return ExpressionConverter.MethodCallTranslator._methodTranslators.TryGetValue(methodInfo, out callTranslator);
					}
				}
				callTranslator = null;
				return false;
			}

			// Token: 0x060061DF RID: 25055 RVA: 0x00153A3C File Offset: 0x00151C3C
			private static void InitializeVBMethods(Assembly vbAssembly)
			{
				foreach (ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator in ExpressionConverter.MethodCallTranslator.GetVisualBasicCallTranslators(vbAssembly))
				{
					foreach (MethodInfo methodInfo in callTranslator.Methods)
					{
						ExpressionConverter.MethodCallTranslator._methodTranslators.Add(methodInfo, callTranslator);
					}
				}
			}

			// Token: 0x060061E0 RID: 25056 RVA: 0x00153AC4 File Offset: 0x00151CC4
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.CallTranslator> GetVisualBasicCallTranslators(Assembly vbAssembly)
			{
				yield return new ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionDefaultTranslator(vbAssembly);
				yield return new ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator(vbAssembly);
				yield return new ExpressionConverter.MethodCallTranslator.VBDatePartTranslator(vbAssembly);
				yield break;
			}

			// Token: 0x060061E1 RID: 25057 RVA: 0x00153AD4 File Offset: 0x00151CD4
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.CallTranslator> GetCallTranslators()
			{
				return new ExpressionConverter.MethodCallTranslator.CallTranslator[]
				{
					new ExpressionConverter.MethodCallTranslator.CanonicalFunctionDefaultTranslator(),
					new ExpressionConverter.MethodCallTranslator.AsUnicodeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.AsNonUnicodeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.MathTruncateTranslator(),
					new ExpressionConverter.MethodCallTranslator.MathPowerTranslator(),
					new ExpressionConverter.MethodCallTranslator.GuidNewGuidTranslator(),
					new ExpressionConverter.MethodCallTranslator.LikeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.StringContainsTranslator(),
					new ExpressionConverter.MethodCallTranslator.StartsWithTranslator(),
					new ExpressionConverter.MethodCallTranslator.EndsWithTranslator(),
					new ExpressionConverter.MethodCallTranslator.IndexOfTranslator(),
					new ExpressionConverter.MethodCallTranslator.SubstringTranslator(),
					new ExpressionConverter.MethodCallTranslator.RemoveTranslator(),
					new ExpressionConverter.MethodCallTranslator.InsertTranslator(),
					new ExpressionConverter.MethodCallTranslator.IsNullOrEmptyTranslator(),
					new ExpressionConverter.MethodCallTranslator.StringConcatTranslator(),
					new ExpressionConverter.MethodCallTranslator.TrimTranslator(),
					new ExpressionConverter.MethodCallTranslator.TrimStartTranslator(),
					new ExpressionConverter.MethodCallTranslator.TrimEndTranslator(),
					new ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator(),
					new ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator(),
					new ExpressionConverter.MethodCallTranslator.HasFlagTranslator(),
					new ExpressionConverter.MethodCallTranslator.ToStringTranslator()
				};
			}

			// Token: 0x060061E2 RID: 25058 RVA: 0x00153BAE File Offset: 0x00151DAE
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> GetSequenceMethodTranslators()
			{
				yield return new ExpressionConverter.MethodCallTranslator.ConcatTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.UnionTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.IntersectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ExceptTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.DistinctTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.WhereTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SelectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.OrderByTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.OrderByDescendingTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ThenByTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ThenByDescendingTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SelectManyTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AnyTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AnyPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AllTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.JoinTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.GroupByTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.MaxTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.MinTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AverageTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SumTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.CountTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.LongCountTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.CastMethodTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.GroupJoinTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.OfTypeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.PassthroughTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.DefaultIfEmptyTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstOrDefaultTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstOrDefaultPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.TakeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SkipTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SingleTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SinglePredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SingleOrDefaultTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SingleOrDefaultPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ContainsTranslator();
				yield break;
			}

			// Token: 0x060061E3 RID: 25059 RVA: 0x00153BB7 File Offset: 0x00151DB7
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> GetObjectQueryCallTranslators()
			{
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderDistinctTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderExceptTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderFirstTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderToListTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryIncludeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderIntersectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderOfTypeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderUnionTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryMergeAsTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryIncludeSpanTranslator();
				yield break;
			}

			// Token: 0x060061E4 RID: 25060 RVA: 0x00153BC0 File Offset: 0x00151DC0
			private static bool IsTrivialRename(LambdaExpression selectorLambda, ExpressionConverter converter, out string leftName, out string rightName, out InitializerMetadata initializerMetadata)
			{
				leftName = null;
				rightName = null;
				initializerMetadata = null;
				if (selectorLambda.Parameters.Count != 2 || selectorLambda.Body.NodeType != ExpressionType.New)
				{
					return false;
				}
				NewExpression newExpression = (NewExpression)selectorLambda.Body;
				if (newExpression.Arguments.Count != 2)
				{
					return false;
				}
				if (newExpression.Arguments[0] != selectorLambda.Parameters[0] || newExpression.Arguments[1] != selectorLambda.Parameters[1])
				{
					return false;
				}
				leftName = newExpression.Members[0].Name;
				rightName = newExpression.Members[1].Name;
				initializerMetadata = InitializerMetadata.CreateProjectionInitializer(converter.EdmItemCollection, newExpression);
				converter.ValidateInitializerMetadata(initializerMetadata);
				return true;
			}

			// Token: 0x04002B1D RID: 11037
			private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

			// Token: 0x04002B1E RID: 11038
			private static readonly ExpressionConverter.MethodCallTranslator.CallTranslator _defaultTranslator = new ExpressionConverter.MethodCallTranslator.DefaultTranslator();

			// Token: 0x04002B1F RID: 11039
			private static readonly ExpressionConverter.MethodCallTranslator.FunctionCallTranslator _functionCallTranslator = new ExpressionConverter.MethodCallTranslator.FunctionCallTranslator();

			// Token: 0x04002B20 RID: 11040
			private static readonly Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> _methodTranslators = ExpressionConverter.MethodCallTranslator.InitializeMethodTranslators();

			// Token: 0x04002B21 RID: 11041
			private static readonly Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> _sequenceTranslators = ExpressionConverter.MethodCallTranslator.InitializeSequenceMethodTranslators();

			// Token: 0x04002B22 RID: 11042
			private static readonly Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> _objectQueryTranslators = ExpressionConverter.MethodCallTranslator.InitializeObjectQueryTranslators();

			// Token: 0x04002B23 RID: 11043
			private static bool s_vbMethodsInitialized;

			// Token: 0x04002B24 RID: 11044
			private static readonly object _vbInitializerLock = new object();

			// Token: 0x02000CE3 RID: 3299
			internal sealed class HierarchyIdMethodCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006CEB RID: 27883 RVA: 0x001732B8 File Offset: 0x001714B8
				internal HierarchyIdMethodCallTranslator()
					: base(ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetSupportedMethods())
				{
				}

				// Token: 0x06006CEC RID: 27884 RVA: 0x001732C5 File Offset: 0x001714C5
				private static MethodInfo GetStaticMethod<TResult>(Expression<Func<TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x06006CED RID: 27885 RVA: 0x001732D7 File Offset: 0x001714D7
				private static MethodInfo GetInstanceMethod<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x06006CEE RID: 27886 RVA: 0x001732E9 File Offset: 0x001714E9
				private static IEnumerable<MethodInfo> GetSupportedMethods()
				{
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetStaticMethod<HierarchyId>(() => HierarchyId.GetRoot());
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetStaticMethod<HierarchyId>(() => HierarchyId.Parse(null));
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, HierarchyId>((HierarchyId h) => h.GetAncestor(0));
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, HierarchyId>((HierarchyId h) => h.GetDescendant(null, null));
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, short>((HierarchyId h) => h.GetLevel());
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, bool>((HierarchyId h) => h.IsDescendantOf(null));
					yield return ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, HierarchyId>((HierarchyId h) => h.GetReparentedValue(null, null));
					yield break;
				}

				// Token: 0x06006CEF RID: 27887 RVA: 0x001732F4 File Offset: 0x001714F4
				private static Dictionary<MethodInfo, string> GetRenamedMethodFunctions()
				{
					return new Dictionary<MethodInfo, string>
					{
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetStaticMethod<HierarchyId>(() => HierarchyId.GetRoot()),
							"HierarchyIdGetRoot"
						},
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetStaticMethod<HierarchyId>(() => HierarchyId.Parse(null)),
							"HierarchyIdParse"
						},
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, HierarchyId>((HierarchyId h) => h.GetAncestor(0)),
							"GetAncestor"
						},
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, HierarchyId>((HierarchyId h) => h.GetDescendant(null, null)),
							"GetDescendant"
						},
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, short>((HierarchyId h) => h.GetLevel()),
							"GetLevel"
						},
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, bool>((HierarchyId h) => h.IsDescendantOf(null)),
							"IsDescendantOf"
						},
						{
							ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetInstanceMethod<HierarchyId, HierarchyId>((HierarchyId h) => h.GetReparentedValue(null, null)),
							"GetReparentedValue"
						}
					};
				}

				// Token: 0x06006CF0 RID: 27888 RVA: 0x0017358C File Offset: 0x0017178C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo method = call.Method;
					string name;
					if (!ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator._methodFunctionRenames.TryGetValue(method, out name))
					{
						name = method.Name;
					}
					Expression[] array;
					if (method.IsStatic)
					{
						array = call.Arguments.ToArray<Expression>();
					}
					else
					{
						array = new Expression[] { call.Object }.Concat(call.Arguments).ToArray<Expression>();
					}
					return parent.TranslateIntoCanonicalFunction(name, call, array);
				}

				// Token: 0x0400326F RID: 12911
				private static readonly Dictionary<MethodInfo, string> _methodFunctionRenames = ExpressionConverter.MethodCallTranslator.HierarchyIdMethodCallTranslator.GetRenamedMethodFunctions();
			}

			// Token: 0x02000CE4 RID: 3300
			internal abstract class CallTranslator
			{
				// Token: 0x06006CF2 RID: 27890 RVA: 0x00173601 File Offset: 0x00171801
				protected CallTranslator(params MethodInfo[] methods)
				{
					this._methods = methods;
				}

				// Token: 0x06006CF3 RID: 27891 RVA: 0x00173610 File Offset: 0x00171810
				protected CallTranslator(IEnumerable<MethodInfo> methods)
				{
					this._methods = methods;
				}

				// Token: 0x17001195 RID: 4501
				// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x0017361F File Offset: 0x0017181F
				internal IEnumerable<MethodInfo> Methods
				{
					get
					{
						return this._methods;
					}
				}

				// Token: 0x06006CF5 RID: 27893
				internal abstract DbExpression Translate(ExpressionConverter parent, MethodCallExpression call);

				// Token: 0x06006CF6 RID: 27894 RVA: 0x00173627 File Offset: 0x00171827
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x04003270 RID: 12912
				private readonly IEnumerable<MethodInfo> _methods;
			}

			// Token: 0x02000CE5 RID: 3301
			private abstract class ObjectQueryCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006CF7 RID: 27895 RVA: 0x00173634 File Offset: 0x00171834
				internal static bool IsCandidateMethod(MethodInfo method)
				{
					Type declaringType = method.DeclaringType;
					return (method.IsPublic || (method.IsAssembly && (method.Name == "MergeAs" || method.Name == "IncludeSpan"))) && null != declaringType && declaringType.IsGenericType() && typeof(ObjectQuery<>) == declaringType.GetGenericTypeDefinition();
				}

				// Token: 0x06006CF8 RID: 27896 RVA: 0x001736A4 File Offset: 0x001718A4
				internal static Expression RemoveConvertToObjectQuery(Expression queryExpression)
				{
					if (queryExpression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)queryExpression;
						Type type = unaryExpression.Operand.Type;
						if (type.IsGenericType() && (typeof(IQueryable<>) == type.GetGenericTypeDefinition() || typeof(IOrderedQueryable<>) == type.GetGenericTypeDefinition()))
						{
							queryExpression = unaryExpression.Operand;
						}
					}
					return queryExpression;
				}

				// Token: 0x06006CF9 RID: 27897 RVA: 0x0017370D File Offset: 0x0017190D
				protected ObjectQueryCallTranslator(string methodName)
					: base(new MethodInfo[0])
				{
					this._methodName = methodName;
				}

				// Token: 0x17001196 RID: 4502
				// (get) Token: 0x06006CFA RID: 27898 RVA: 0x00173722 File Offset: 0x00171922
				internal string MethodName
				{
					get
					{
						return this._methodName;
					}
				}

				// Token: 0x04003271 RID: 12913
				private readonly string _methodName;
			}

			// Token: 0x02000CE6 RID: 3302
			private abstract class ObjectQueryBuilderCallTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06006CFB RID: 27899 RVA: 0x0017372A File Offset: 0x0017192A
				protected ObjectQueryBuilderCallTranslator(string methodName, SequenceMethod sequenceEquivalent)
					: base(methodName)
				{
					ExpressionConverter.MethodCallTranslator._sequenceTranslators.TryGetValue(sequenceEquivalent, out this._translator);
				}

				// Token: 0x06006CFC RID: 27900 RVA: 0x00173745 File Offset: 0x00171945
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return this._translator.Translate(parent, call);
				}

				// Token: 0x04003272 RID: 12914
				private readonly ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator _translator;
			}

			// Token: 0x02000CE7 RID: 3303
			private sealed class ObjectQueryBuilderUnionTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006CFD RID: 27901 RVA: 0x00173754 File Offset: 0x00171954
				internal ObjectQueryBuilderUnionTranslator()
					: base("Union", SequenceMethod.Union)
				{
				}
			}

			// Token: 0x02000CE8 RID: 3304
			private sealed class ObjectQueryBuilderIntersectTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006CFE RID: 27902 RVA: 0x00173763 File Offset: 0x00171963
				internal ObjectQueryBuilderIntersectTranslator()
					: base("Intersect", SequenceMethod.Intersect)
				{
				}
			}

			// Token: 0x02000CE9 RID: 3305
			private sealed class ObjectQueryBuilderExceptTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006CFF RID: 27903 RVA: 0x00173772 File Offset: 0x00171972
				internal ObjectQueryBuilderExceptTranslator()
					: base("Except", SequenceMethod.Except)
				{
				}
			}

			// Token: 0x02000CEA RID: 3306
			private sealed class ObjectQueryBuilderDistinctTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006D00 RID: 27904 RVA: 0x00173781 File Offset: 0x00171981
				internal ObjectQueryBuilderDistinctTranslator()
					: base("Distinct", SequenceMethod.Distinct)
				{
				}
			}

			// Token: 0x02000CEB RID: 3307
			private sealed class ObjectQueryBuilderOfTypeTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006D01 RID: 27905 RVA: 0x00173790 File Offset: 0x00171990
				internal ObjectQueryBuilderOfTypeTranslator()
					: base("OfType", SequenceMethod.OfType)
				{
				}
			}

			// Token: 0x02000CEC RID: 3308
			private sealed class ObjectQueryBuilderFirstTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006D02 RID: 27906 RVA: 0x0017379E File Offset: 0x0017199E
				internal ObjectQueryBuilderFirstTranslator()
					: base("First", SequenceMethod.First)
				{
				}
			}

			// Token: 0x02000CED RID: 3309
			private sealed class ObjectQueryBuilderToListTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06006D03 RID: 27907 RVA: 0x001737AD File Offset: 0x001719AD
				internal ObjectQueryBuilderToListTranslator()
					: base("ToList", SequenceMethod.ToList)
				{
				}
			}

			// Token: 0x02000CEE RID: 3310
			private sealed class ObjectQueryIncludeTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06006D04 RID: 27908 RVA: 0x001737BF File Offset: 0x001719BF
				internal ObjectQueryIncludeTranslator()
					: base("Include")
				{
				}

				// Token: 0x06006D05 RID: 27909 RVA: 0x001737CC File Offset: 0x001719CC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Object);
					Span span;
					if (!parent.TryGetSpan(dbExpression, out span))
					{
						span = null;
					}
					DbExpression dbExpression2 = parent.TranslateExpression(call.Arguments[0]);
					if (dbExpression2.ExpressionKind == DbExpressionKind.Constant)
					{
						string text = (string)((DbConstantExpression)dbExpression2).Value;
						if (parent.CanIncludeSpanInfo())
						{
							span = Span.IncludeIn(span, text);
						}
						return parent.AddSpanMapping(dbExpression, span);
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedInclude);
				}
			}

			// Token: 0x02000CEF RID: 3311
			private sealed class ObjectQueryMergeAsTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06006D06 RID: 27910 RVA: 0x00173847 File Offset: 0x00171A47
				internal ObjectQueryMergeAsTranslator()
					: base("MergeAs")
				{
				}

				// Token: 0x06006D07 RID: 27911 RVA: 0x00173854 File Offset: 0x00171A54
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Arguments[0].NodeType != ExpressionType.Constant)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedMergeAs);
					}
					MergeOption mergeOption = (MergeOption)((ConstantExpression)call.Arguments[0]).Value;
					EntityUtil.CheckArgumentMergeOption(mergeOption);
					parent.NotifyMergeOption(mergeOption);
					Expression expression = ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.RemoveConvertToObjectQuery(call.Object);
					DbExpression dbExpression = parent.TranslateExpression(expression);
					Span span;
					if (!parent.TryGetSpan(dbExpression, out span))
					{
						span = null;
					}
					return parent.AddSpanMapping(dbExpression, span);
				}
			}

			// Token: 0x02000CF0 RID: 3312
			private sealed class ObjectQueryIncludeSpanTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06006D08 RID: 27912 RVA: 0x001738D3 File Offset: 0x00171AD3
				internal ObjectQueryIncludeSpanTranslator()
					: base("IncludeSpan")
				{
				}

				// Token: 0x06006D09 RID: 27913 RVA: 0x001738E0 File Offset: 0x00171AE0
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					Span span = (Span)((ConstantExpression)call.Arguments[0]).Value;
					Expression expression = ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.RemoveConvertToObjectQuery(call.Object);
					DbExpression dbExpression = parent.TranslateExpression(expression);
					if (!parent.CanIncludeSpanInfo())
					{
						span = null;
					}
					return parent.AddSpanMapping(dbExpression, span);
				}
			}

			// Token: 0x02000CF1 RID: 3313
			internal sealed class DefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D0A RID: 27914 RVA: 0x00173930 File Offset: 0x00171B30
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo method = call.Method;
					if (method.DeclaringType.Assembly().FullName == "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" && method.Name == "Mid")
					{
						if (new Type[]
						{
							typeof(string),
							typeof(int)
						}.SequenceEqual(from p in method.GetParameters()
							select p.ParameterType))
						{
							throw new NotSupportedException(Strings.ELinq_UnsupportedMethodSuggestedAlternative(method, "System.String Mid(System.String, Int32, Int32)"));
						}
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedMethod(method));
				}

				// Token: 0x06006D0B RID: 27915 RVA: 0x001739DF File Offset: 0x00171BDF
				public DefaultTranslator()
					: base(new MethodInfo[0])
				{
				}
			}

			// Token: 0x02000CF2 RID: 3314
			private sealed class FunctionCallTranslator
			{
				// Token: 0x06006D0C RID: 27916 RVA: 0x001739F0 File Offset: 0x00171BF0
				internal DbExpression TranslateFunctionCall(ExpressionConverter parent, MethodCallExpression call, DbFunctionAttribute functionAttribute)
				{
					List<DbExpression> list = (from a in call.Arguments
						select this.UnwrapNoOpConverts(a) into b
						select this.NormalizeAllSetSources(parent, parent.TranslateExpression(b))).ToList<DbExpression>();
					List<TypeUsage> list2 = list.Select((DbExpression a) => a.ResultType).ToList<TypeUsage>();
					EdmFunction edmFunction = parent.FindFunction(functionAttribute.NamespaceName, functionAttribute.FunctionName, list2, false, call);
					if (!edmFunction.IsComposableAttribute)
					{
						throw new NotSupportedException(Strings.CannotCallNoncomposableFunction(edmFunction.FullName));
					}
					DbExpression dbExpression = edmFunction.Invoke(list);
					return this.ValidateReturnType(dbExpression, dbExpression.ResultType, parent, call, call.Type, false);
				}

				// Token: 0x06006D0D RID: 27917 RVA: 0x00173AC4 File Offset: 0x00171CC4
				private DbExpression NormalizeAllSetSources(ExpressionConverter parent, DbExpression argumentExpr)
				{
					DbExpression dbExpression = null;
					BuiltInTypeKind builtInTypeKind = argumentExpr.ResultType.EdmType.BuiltInTypeKind;
					if (builtInTypeKind != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind == BuiltInTypeKind.RowType)
						{
							List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
							RowType rowType = argumentExpr.ResultType.EdmType as RowType;
							bool flag = false;
							foreach (EdmProperty edmProperty in rowType.Properties)
							{
								DbPropertyExpression dbPropertyExpression = argumentExpr.Property(edmProperty);
								dbExpression = this.NormalizeAllSetSources(parent, dbPropertyExpression);
								if (dbExpression != dbPropertyExpression)
								{
									flag = true;
									list.Add(new KeyValuePair<string, DbExpression>(dbPropertyExpression.Property.Name, dbExpression));
								}
								else
								{
									list.Add(new KeyValuePair<string, DbExpression>(dbPropertyExpression.Property.Name, dbPropertyExpression));
								}
							}
							if (flag)
							{
								dbExpression = DbExpressionBuilder.NewRow(list);
							}
							else
							{
								dbExpression = argumentExpr;
							}
						}
					}
					else
					{
						DbExpressionBinding dbExpressionBinding = argumentExpr.BindAs(parent.AliasGenerator.Next());
						DbExpression dbExpression2 = this.NormalizeAllSetSources(parent, dbExpressionBinding.Variable);
						if (dbExpression2 != dbExpressionBinding.Variable)
						{
							dbExpression = dbExpressionBinding.Project(dbExpression2);
						}
					}
					if (dbExpression != null && dbExpression != argumentExpr)
					{
						return parent.NormalizeSetSource(dbExpression);
					}
					return parent.NormalizeSetSource(argumentExpr);
				}

				// Token: 0x06006D0E RID: 27918 RVA: 0x00173BFC File Offset: 0x00171DFC
				private Expression UnwrapNoOpConverts(Expression expression)
				{
					if (expression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)expression;
						Expression expression2 = this.UnwrapNoOpConverts(unaryExpression.Operand);
						if (expression.Type.IsAssignableFrom(expression2.Type))
						{
							return expression2;
						}
					}
					return expression;
				}

				// Token: 0x06006D0F RID: 27919 RVA: 0x00173C40 File Offset: 0x00171E40
				private DbExpression ValidateReturnType(DbExpression result, TypeUsage actualReturnType, ExpressionConverter parent, MethodCallExpression call, Type clrReturnType, bool isElementOfCollection)
				{
					BuiltInTypeKind builtInTypeKind = actualReturnType.EdmType.BuiltInTypeKind;
					if (builtInTypeKind != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind != BuiltInTypeKind.RefType)
						{
							if (builtInTypeKind != BuiltInTypeKind.RowType)
							{
								if (isElementOfCollection && parent.GetCastTargetType(actualReturnType, clrReturnType, null, false) != null)
								{
									throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
								}
								TypeUsage valueLayerType = parent.GetValueLayerType(clrReturnType);
								if (!TypeSemantics.IsPromotableTo(actualReturnType, valueLayerType))
								{
									throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
								}
								if (!isElementOfCollection)
								{
									result = parent.AlignTypes(result, clrReturnType);
								}
							}
							else if (clrReturnType != typeof(DbDataRecord))
							{
								throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
							}
						}
						else if (clrReturnType != typeof(EntityKey))
						{
							throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
					}
					else
					{
						if (!clrReturnType.IsGenericType())
						{
							throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
						Type genericTypeDefinition = clrReturnType.GetGenericTypeDefinition();
						if (genericTypeDefinition != typeof(IEnumerable<>) && genericTypeDefinition != typeof(IQueryable<>))
						{
							throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
						Type type = clrReturnType.GetGenericArguments()[0];
						result = this.ValidateReturnType(result, TypeHelpers.GetElementTypeUsage(actualReturnType), parent, call, type, true);
					}
					return result;
				}
			}

			// Token: 0x02000CF3 RID: 3315
			internal sealed class CanonicalFunctionDefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D11 RID: 27921 RVA: 0x00173DE5 File Offset: 0x00171FE5
				internal CanonicalFunctionDefaultTranslator()
					: base(ExpressionConverter.MethodCallTranslator.CanonicalFunctionDefaultTranslator.GetMethods())
				{
				}

				// Token: 0x06006D12 RID: 27922 RVA: 0x00173DF4 File Offset: 0x00171FF4
				private static IEnumerable<MethodInfo> GetMethods()
				{
					List<MethodInfo> list = new List<MethodInfo>
					{
						typeof(Math).GetDeclaredMethod("Ceiling", new Type[] { typeof(decimal) }),
						typeof(Math).GetDeclaredMethod("Ceiling", new Type[] { typeof(double) }),
						typeof(Math).GetDeclaredMethod("Floor", new Type[] { typeof(decimal) }),
						typeof(Math).GetDeclaredMethod("Floor", new Type[] { typeof(double) }),
						typeof(Math).GetDeclaredMethod("Round", new Type[] { typeof(decimal) }),
						typeof(Math).GetDeclaredMethod("Round", new Type[] { typeof(double) }),
						typeof(Math).GetDeclaredMethod("Round", new Type[]
						{
							typeof(decimal),
							typeof(int)
						}),
						typeof(Math).GetDeclaredMethod("Round", new Type[]
						{
							typeof(double),
							typeof(int)
						}),
						typeof(decimal).GetDeclaredMethod("Floor", new Type[] { typeof(decimal) }),
						typeof(decimal).GetDeclaredMethod("Ceiling", new Type[] { typeof(decimal) }),
						typeof(decimal).GetDeclaredMethod("Round", new Type[] { typeof(decimal) }),
						typeof(decimal).GetDeclaredMethod("Round", new Type[]
						{
							typeof(decimal),
							typeof(int)
						}),
						typeof(string).GetDeclaredMethod("Replace", new Type[]
						{
							typeof(string),
							typeof(string)
						}),
						typeof(string).GetDeclaredMethod("ToLower", new Type[0]),
						typeof(string).GetDeclaredMethod("ToUpper", new Type[0]),
						typeof(string).GetDeclaredMethod("Trim", new Type[0])
					};
					list.AddRange(new Type[]
					{
						typeof(decimal),
						typeof(double),
						typeof(float),
						typeof(int),
						typeof(long),
						typeof(sbyte),
						typeof(short)
					}.Select((Type a) => typeof(Math).GetDeclaredMethod("Abs", new Type[] { a })));
					return list;
				}

				// Token: 0x06006D13 RID: 27923 RVA: 0x00174174 File Offset: 0x00172374
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					Expression[] array;
					if (!call.Method.IsStatic)
					{
						List<Expression> list = new List<Expression>(call.Arguments.Count + 1);
						list.Add(call.Object);
						list.AddRange(call.Arguments);
						array = list.ToArray();
					}
					else
					{
						array = call.Arguments.ToArray<Expression>();
					}
					return parent.TranslateIntoCanonicalFunction(call.Method.Name, call, array);
				}
			}

			// Token: 0x02000CF4 RID: 3316
			internal sealed class LikeFunctionTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D14 RID: 27924 RVA: 0x001741DF File Offset: 0x001723DF
				internal LikeFunctionTranslator()
					: base(ExpressionConverter.MethodCallTranslator.LikeFunctionTranslator.GetMethods())
				{
				}

				// Token: 0x06006D15 RID: 27925 RVA: 0x001741EC File Offset: 0x001723EC
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(DbFunctions).GetDeclaredMethod("Like", new Type[]
					{
						typeof(string),
						typeof(string)
					});
					yield return typeof(DbFunctions).GetDeclaredMethod("Like", new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					});
					yield return typeof(EntityFunctions).GetDeclaredMethod("Like", new Type[]
					{
						typeof(string),
						typeof(string)
					});
					yield return typeof(EntityFunctions).GetDeclaredMethod("Like", new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					});
					yield break;
				}

				// Token: 0x06006D16 RID: 27926 RVA: 0x001741F5 File Offset: 0x001723F5
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateLike(call);
				}
			}

			// Token: 0x02000CF5 RID: 3317
			internal abstract class AsUnicodeNonUnicodeBaseFunctionTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D17 RID: 27927 RVA: 0x001741FE File Offset: 0x001723FE
				protected AsUnicodeNonUnicodeBaseFunctionTranslator(IEnumerable<MethodInfo> methods, bool isUnicode)
					: base(methods)
				{
					this._isUnicode = isUnicode;
				}

				// Token: 0x06006D18 RID: 27928 RVA: 0x00174210 File Offset: 0x00172410
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					TypeUsage typeUsage = dbExpression.ResultType.ShallowCopy(new FacetValues
					{
						Unicode = new bool?(this._isUnicode)
					});
					DbExpressionKind expressionKind = dbExpression.ExpressionKind;
					DbExpression dbExpression2;
					if (expressionKind != DbExpressionKind.Constant)
					{
						if (expressionKind != DbExpressionKind.Null)
						{
							if (expressionKind != DbExpressionKind.ParameterReference)
							{
								throw new NotSupportedException(Strings.ELinq_UnsupportedAsUnicodeAndAsNonUnicode(call.Method));
							}
							dbExpression2 = typeUsage.Parameter(((DbParameterReferenceExpression)dbExpression).ParameterName);
						}
						else
						{
							dbExpression2 = typeUsage.Null();
						}
					}
					else
					{
						dbExpression2 = typeUsage.Constant(((DbConstantExpression)dbExpression).Value);
					}
					return dbExpression2;
				}

				// Token: 0x04003273 RID: 12915
				private readonly bool _isUnicode;
			}

			// Token: 0x02000CF6 RID: 3318
			internal sealed class AsUnicodeFunctionTranslator : ExpressionConverter.MethodCallTranslator.AsUnicodeNonUnicodeBaseFunctionTranslator
			{
				// Token: 0x06006D19 RID: 27929 RVA: 0x001742B1 File Offset: 0x001724B1
				internal AsUnicodeFunctionTranslator()
					: base(ExpressionConverter.MethodCallTranslator.AsUnicodeFunctionTranslator.GetMethods(), true)
				{
				}

				// Token: 0x06006D1A RID: 27930 RVA: 0x001742BF File Offset: 0x001724BF
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(DbFunctions).GetDeclaredMethod("AsUnicode", new Type[] { typeof(string) });
					yield return typeof(EntityFunctions).GetDeclaredMethod("AsUnicode", new Type[] { typeof(string) });
					yield break;
				}
			}

			// Token: 0x02000CF7 RID: 3319
			internal sealed class AsNonUnicodeFunctionTranslator : ExpressionConverter.MethodCallTranslator.AsUnicodeNonUnicodeBaseFunctionTranslator
			{
				// Token: 0x06006D1B RID: 27931 RVA: 0x001742C8 File Offset: 0x001724C8
				internal AsNonUnicodeFunctionTranslator()
					: base(ExpressionConverter.MethodCallTranslator.AsNonUnicodeFunctionTranslator.GetMethods(), false)
				{
				}

				// Token: 0x06006D1C RID: 27932 RVA: 0x001742D6 File Offset: 0x001724D6
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(DbFunctions).GetDeclaredMethod("AsNonUnicode", new Type[] { typeof(string) });
					yield return typeof(EntityFunctions).GetDeclaredMethod("AsNonUnicode", new Type[] { typeof(string) });
					yield break;
				}
			}

			// Token: 0x02000CF8 RID: 3320
			internal sealed class HasFlagTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D1D RID: 27933 RVA: 0x001742DF File Offset: 0x001724DF
				internal HasFlagTranslator()
					: base(new MethodInfo[] { ExpressionConverter.MethodCallTranslator.HasFlagTranslator._hasFlagMethod })
				{
				}

				// Token: 0x06006D1E RID: 27934 RVA: 0x001742F8 File Offset: 0x001724F8
				private static DbExpression TranslateHasFlag(ExpressionConverter parent, Expression sourceExpression, Expression valueExpression)
				{
					if (valueExpression.NodeType == ExpressionType.Constant && ((ConstantExpression)valueExpression).Value == null)
					{
						throw new ArgumentNullException("flag");
					}
					DbExpression dbExpression = parent.TranslateExpression(valueExpression);
					DbExpression dbExpression2 = parent.TranslateExpression(sourceExpression);
					if (dbExpression2.ResultType.EdmType != dbExpression.ResultType.EdmType)
					{
						throw new NotSupportedException(Strings.ELinq_HasFlagArgumentAndSourceTypeMismatch(dbExpression.ResultType.EdmType.Name, dbExpression2.ResultType.EdmType.Name));
					}
					TypeUsage typeUsage = TypeHelpers.CreateEnumUnderlyingTypeUsage(dbExpression2.ResultType);
					DbCastExpression dbCastExpression = dbExpression.CastTo(typeUsage);
					return dbExpression2.CastTo(typeUsage).BitwiseAnd(dbCastExpression).Equal(dbCastExpression);
				}

				// Token: 0x06006D1F RID: 27935 RVA: 0x001743A1 File Offset: 0x001725A1
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.MethodCallTranslator.HasFlagTranslator.TranslateHasFlag(parent, call.Object, call.Arguments[0]);
				}

				// Token: 0x04003274 RID: 12916
				private static readonly MethodInfo _hasFlagMethod = typeof(Enum).GetDeclaredMethod("HasFlag", new Type[] { typeof(Enum) });
			}

			// Token: 0x02000CF9 RID: 3321
			internal sealed class MathTruncateTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D21 RID: 27937 RVA: 0x001743EC File Offset: 0x001725EC
				internal MathTruncateTranslator()
					: base(new MethodInfo[]
					{
						typeof(Math).GetDeclaredMethod("Truncate", new Type[] { typeof(decimal) }),
						typeof(Math).GetDeclaredMethod("Truncate", new Type[] { typeof(double) })
					})
				{
				}

				// Token: 0x06006D22 RID: 27938 RVA: 0x0017445C File Offset: 0x0017265C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					DbConstantExpression dbConstantExpression = DbExpressionBuilder.Constant(0);
					return dbExpression.Truncate(dbConstantExpression);
				}
			}

			// Token: 0x02000CFA RID: 3322
			internal sealed class MathPowerTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D23 RID: 27939 RVA: 0x00174490 File Offset: 0x00172690
				internal MathPowerTranslator()
					: base(new MethodInfo[] { typeof(Math).GetDeclaredMethod("Pow", new Type[]
					{
						typeof(double),
						typeof(double)
					}) })
				{
				}

				// Token: 0x06006D24 RID: 27940 RVA: 0x001744E0 File Offset: 0x001726E0
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Arguments[1]);
					return dbExpression.Power(dbExpression2);
				}
			}

			// Token: 0x02000CFB RID: 3323
			internal sealed class GuidNewGuidTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D25 RID: 27941 RVA: 0x00174518 File Offset: 0x00172718
				internal GuidNewGuidTranslator()
					: base(new MethodInfo[] { typeof(Guid).GetDeclaredMethod("NewGuid", new Type[0]) })
				{
				}

				// Token: 0x06006D26 RID: 27942 RVA: 0x00174543 File Offset: 0x00172743
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return EdmFunctions.NewGuid();
				}
			}

			// Token: 0x02000CFC RID: 3324
			internal sealed class StringContainsTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D27 RID: 27943 RVA: 0x0017454A File Offset: 0x0017274A
				internal StringContainsTranslator()
					: base(ExpressionConverter.MethodCallTranslator.StringContainsTranslator.GetMethods())
				{
				}

				// Token: 0x06006D28 RID: 27944 RVA: 0x00174557 File Offset: 0x00172757
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Contains", new Type[] { typeof(string) });
					yield break;
				}

				// Token: 0x06006D29 RID: 27945 RVA: 0x00174560 File Offset: 0x00172760
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, true, true, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.StringContainsTranslator.CreateDefaultTranslation));
				}

				// Token: 0x06006D2A RID: 27946 RVA: 0x00174577 File Offset: 0x00172777
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					return parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[] { patternExpression, inputExpression }).GreaterThan(DbExpressionBuilder.Constant(0));
				}
			}

			// Token: 0x02000CFD RID: 3325
			internal sealed class IndexOfTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D2B RID: 27947 RVA: 0x001745A3 File Offset: 0x001727A3
				internal IndexOfTranslator()
					: base(ExpressionConverter.MethodCallTranslator.IndexOfTranslator.GetMethods())
				{
				}

				// Token: 0x06006D2C RID: 27948 RVA: 0x001745B0 File Offset: 0x001727B0
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("IndexOf", new Type[] { typeof(string) });
					yield break;
				}

				// Token: 0x06006D2D RID: 27949 RVA: 0x001745B9 File Offset: 0x001727B9
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction("IndexOf", call, new Expression[]
					{
						call.Arguments[0],
						call.Object
					}).Minus(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x02000CFE RID: 3326
			internal sealed class StartsWithTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D2E RID: 27950 RVA: 0x001745F5 File Offset: 0x001727F5
				internal StartsWithTranslator()
					: base(ExpressionConverter.MethodCallTranslator.StartsWithTranslator.GetMethods())
				{
				}

				// Token: 0x06006D2F RID: 27951 RVA: 0x00174602 File Offset: 0x00172802
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("StartsWith", new Type[] { typeof(string) });
					yield break;
				}

				// Token: 0x06006D30 RID: 27952 RVA: 0x0017460B File Offset: 0x0017280B
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, false, true, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.StartsWithTranslator.CreateDefaultTranslation));
				}

				// Token: 0x06006D31 RID: 27953 RVA: 0x00174622 File Offset: 0x00172822
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					return parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[] { patternExpression, inputExpression }).Equal(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x02000CFF RID: 3327
			internal sealed class EndsWithTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D32 RID: 27954 RVA: 0x0017464E File Offset: 0x0017284E
				internal EndsWithTranslator()
					: base(ExpressionConverter.MethodCallTranslator.EndsWithTranslator.GetMethods())
				{
				}

				// Token: 0x06006D33 RID: 27955 RVA: 0x0017465B File Offset: 0x0017285B
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("EndsWith", new Type[] { typeof(string) });
					yield break;
				}

				// Token: 0x06006D34 RID: 27956 RVA: 0x00174664 File Offset: 0x00172864
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, true, false, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.EndsWithTranslator.CreateDefaultTranslation));
				}

				// Token: 0x06006D35 RID: 27957 RVA: 0x0017467C File Offset: 0x0017287C
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					DbFunctionExpression dbFunctionExpression = parent.CreateCanonicalFunction("Reverse", call, new DbExpression[] { patternExpression });
					DbFunctionExpression dbFunctionExpression2 = parent.CreateCanonicalFunction("Reverse", call, new DbExpression[] { inputExpression });
					return parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[] { dbFunctionExpression, dbFunctionExpression2 }).Equal(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x02000D00 RID: 3328
			internal sealed class SubstringTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D36 RID: 27958 RVA: 0x001746E1 File Offset: 0x001728E1
				internal SubstringTranslator()
					: base(ExpressionConverter.MethodCallTranslator.SubstringTranslator.GetMethods())
				{
				}

				// Token: 0x06006D37 RID: 27959 RVA: 0x001746EE File Offset: 0x001728EE
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Substring", new Type[] { typeof(int) });
					yield return typeof(string).GetDeclaredMethod("Substring", new Type[]
					{
						typeof(int),
						typeof(int)
					});
					yield break;
				}

				// Token: 0x06006D38 RID: 27960 RVA: 0x001746F8 File Offset: 0x001728F8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Object);
					DbExpression dbExpression3 = dbExpression.Plus(DbExpressionBuilder.Constant(1));
					DbExpression dbExpression4;
					if (call.Arguments.Count == 1)
					{
						dbExpression4 = parent.CreateCanonicalFunction("Length", call, new DbExpression[] { dbExpression2 }).Minus(dbExpression);
					}
					else
					{
						dbExpression4 = parent.TranslateExpression(call.Arguments[1]);
					}
					return parent.CreateCanonicalFunction("Substring", call, new DbExpression[] { dbExpression2, dbExpression3, dbExpression4 });
				}
			}

			// Token: 0x02000D01 RID: 3329
			internal sealed class RemoveTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D39 RID: 27961 RVA: 0x00174795 File Offset: 0x00172995
				internal RemoveTranslator()
					: base(ExpressionConverter.MethodCallTranslator.RemoveTranslator.GetMethods())
				{
				}

				// Token: 0x06006D3A RID: 27962 RVA: 0x001747A2 File Offset: 0x001729A2
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Remove", new Type[] { typeof(int) });
					yield return typeof(string).GetDeclaredMethod("Remove", new Type[]
					{
						typeof(int),
						typeof(int)
					});
					yield break;
				}

				// Token: 0x06006D3B RID: 27963 RVA: 0x001747AC File Offset: 0x001729AC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Object);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression3 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression,
						DbExpressionBuilder.Constant(1),
						dbExpression2
					});
					if (call.Arguments.Count == 2)
					{
						DbExpression dbExpression4 = parent.TranslateExpression(call.Arguments[1]);
						if (!ExpressionConverter.MethodCallTranslator.RemoveTranslator.IsNonNegativeIntegerConstant(dbExpression4))
						{
							throw new NotSupportedException(Strings.ELinq_UnsupportedStringRemoveCase(call.Method, call.Method.GetParameters()[1].Name));
						}
						DbExpression dbExpression5 = dbExpression2.Plus(dbExpression4).Plus(DbExpressionBuilder.Constant(1));
						DbExpression dbExpression6 = parent.CreateCanonicalFunction("Length", call, new DbExpression[] { dbExpression }).Minus(dbExpression2.Plus(dbExpression4));
						DbExpression dbExpression7 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[] { dbExpression, dbExpression5, dbExpression6 });
						dbExpression3 = parent.CreateCanonicalFunction("Concat", call, new DbExpression[] { dbExpression3, dbExpression7 });
					}
					return dbExpression3;
				}

				// Token: 0x06006D3C RID: 27964 RVA: 0x001748CD File Offset: 0x00172ACD
				private static bool IsNonNegativeIntegerConstant(DbExpression argument)
				{
					return argument.ExpressionKind == DbExpressionKind.Constant && TypeSemantics.IsPrimitiveType(argument.ResultType, PrimitiveTypeKind.Int32) && (int)((DbConstantExpression)argument).Value >= 0;
				}
			}

			// Token: 0x02000D02 RID: 3330
			internal sealed class InsertTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D3D RID: 27965 RVA: 0x001748FF File Offset: 0x00172AFF
				internal InsertTranslator()
					: base(ExpressionConverter.MethodCallTranslator.InsertTranslator.GetMethods())
				{
				}

				// Token: 0x06006D3E RID: 27966 RVA: 0x0017490C File Offset: 0x00172B0C
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Insert", new Type[]
					{
						typeof(int),
						typeof(string)
					});
					yield break;
				}

				// Token: 0x06006D3F RID: 27967 RVA: 0x00174918 File Offset: 0x00172B18
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Object);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression3 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression,
						DbExpressionBuilder.Constant(1),
						dbExpression2
					});
					DbExpression dbExpression4 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression,
						dbExpression2.Plus(DbExpressionBuilder.Constant(1)),
						parent.CreateCanonicalFunction("Length", call, new DbExpression[] { dbExpression }).Minus(dbExpression2)
					});
					DbExpression dbExpression5 = parent.TranslateExpression(call.Arguments[1]);
					return parent.CreateCanonicalFunction("Concat", call, new DbExpression[]
					{
						parent.CreateCanonicalFunction("Concat", call, new DbExpression[] { dbExpression3, dbExpression5 }),
						dbExpression4
					});
				}
			}

			// Token: 0x02000D03 RID: 3331
			internal sealed class IsNullOrEmptyTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D40 RID: 27968 RVA: 0x00174A00 File Offset: 0x00172C00
				internal IsNullOrEmptyTranslator()
					: base(ExpressionConverter.MethodCallTranslator.IsNullOrEmptyTranslator.GetMethods())
				{
				}

				// Token: 0x06006D41 RID: 27969 RVA: 0x00174A0D File Offset: 0x00172C0D
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("IsNullOrEmpty", new Type[] { typeof(string) });
					yield break;
				}

				// Token: 0x06006D42 RID: 27970 RVA: 0x00174A18 File Offset: 0x00172C18
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression2 = dbExpression.IsNull();
					DbExpression dbExpression3 = parent.CreateCanonicalFunction("Length", call, new DbExpression[] { dbExpression }).Equal(DbExpressionBuilder.Constant(0));
					return dbExpression2.Or(dbExpression3);
				}
			}

			// Token: 0x02000D04 RID: 3332
			internal sealed class StringConcatTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D43 RID: 27971 RVA: 0x00174A6D File Offset: 0x00172C6D
				internal StringConcatTranslator()
					: base(ExpressionConverter.MethodCallTranslator.StringConcatTranslator.GetMethods())
				{
				}

				// Token: 0x06006D44 RID: 27972 RVA: 0x00174A7A File Offset: 0x00172C7A
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object),
						typeof(object)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object),
						typeof(object),
						typeof(object)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object),
						typeof(object),
						typeof(object),
						typeof(object)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[] { typeof(object[]) });
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[] { typeof(string[]) });
					yield break;
				}

				// Token: 0x06006D45 RID: 27973 RVA: 0x00174A84 File Offset: 0x00172C84
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					Expression[] array;
					if (call.Arguments.Count == 1 && (call.Arguments.First<Expression>().Type == typeof(object[]) || call.Arguments.First<Expression>().Type == typeof(string[])))
					{
						if (call.Arguments[0] is NewArrayExpression)
						{
							array = ((NewArrayExpression)call.Arguments[0]).Expressions.ToArray<Expression>();
						}
						else
						{
							ConstantExpression constantExpression = (ConstantExpression)call.Arguments[0];
							if (constantExpression.Value == null)
							{
								throw new ArgumentNullException((constantExpression.Type == typeof(object[])) ? "args" : "values");
							}
							Expression[] array2 = ((object[])constantExpression.Value).Select((object v) => Expression.Constant(v)).ToArray<ConstantExpression>();
							array = array2;
						}
					}
					else
					{
						array = call.Arguments.ToArray<Expression>();
					}
					return ExpressionConverter.StringTranslatorUtil.ConcatArgs(parent, call, array);
				}
			}

			// Token: 0x02000D05 RID: 3333
			internal sealed class ToStringTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D46 RID: 27974 RVA: 0x00174BAB File Offset: 0x00172DAB
				internal ToStringTranslator()
					: base(ExpressionConverter.MethodCallTranslator.ToStringTranslator._methods)
				{
				}

				// Token: 0x06006D47 RID: 27975 RVA: 0x00174BB8 File Offset: 0x00172DB8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.StringTranslatorUtil.ConvertToString(parent, call.Object);
				}

				// Token: 0x04003275 RID: 12917
				private static readonly MethodInfo[] _methods = new MethodInfo[]
				{
					typeof(string).GetDeclaredMethod("ToString", new Type[0]),
					typeof(byte).GetDeclaredMethod("ToString", new Type[0]),
					typeof(sbyte).GetDeclaredMethod("ToString", new Type[0]),
					typeof(short).GetDeclaredMethod("ToString", new Type[0]),
					typeof(int).GetDeclaredMethod("ToString", new Type[0]),
					typeof(long).GetDeclaredMethod("ToString", new Type[0]),
					typeof(double).GetDeclaredMethod("ToString", new Type[0]),
					typeof(float).GetDeclaredMethod("ToString", new Type[0]),
					typeof(Guid).GetDeclaredMethod("ToString", new Type[0]),
					typeof(DateTime).GetDeclaredMethod("ToString", new Type[0]),
					typeof(DateTimeOffset).GetDeclaredMethod("ToString", new Type[0]),
					typeof(TimeSpan).GetDeclaredMethod("ToString", new Type[0]),
					typeof(decimal).GetDeclaredMethod("ToString", new Type[0]),
					typeof(bool).GetDeclaredMethod("ToString", new Type[0]),
					typeof(object).GetDeclaredMethod("ToString", new Type[0])
				};
			}

			// Token: 0x02000D06 RID: 3334
			internal abstract class TrimBaseTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D49 RID: 27977 RVA: 0x00174D9A File Offset: 0x00172F9A
				protected TrimBaseTranslator(IEnumerable<MethodInfo> methods, string canonicalFunctionName)
					: base(methods)
				{
					this._canonicalFunctionName = canonicalFunctionName;
				}

				// Token: 0x06006D4A RID: 27978 RVA: 0x00174DAC File Offset: 0x00172FAC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Arguments.Count != 0 && !ExpressionConverter.MethodCallTranslator.TrimBaseTranslator.IsEmptyArray(call.Arguments[0]))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedTrimStartTrimEndCase(call.Method));
					}
					return parent.TranslateIntoCanonicalFunction(this._canonicalFunctionName, call, new Expression[] { call.Object });
				}

				// Token: 0x06006D4B RID: 27979 RVA: 0x00174E08 File Offset: 0x00173008
				internal static bool IsEmptyArray(Expression expression)
				{
					NewArrayExpression newArrayExpression = expression as NewArrayExpression;
					if (expression.NodeType == ExpressionType.NewArrayInit)
					{
						if (newArrayExpression.Expressions.Count == 0)
						{
							return true;
						}
					}
					else if (expression.NodeType == ExpressionType.NewArrayBounds && newArrayExpression.Expressions.Count == 1 && newArrayExpression.Expressions[0].NodeType == ExpressionType.Constant)
					{
						return object.Equals(((ConstantExpression)newArrayExpression.Expressions[0]).Value, 0);
					}
					return false;
				}

				// Token: 0x04003276 RID: 12918
				private readonly string _canonicalFunctionName;
			}

			// Token: 0x02000D07 RID: 3335
			internal sealed class TrimTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x06006D4C RID: 27980 RVA: 0x00174E85 File Offset: 0x00173085
				internal TrimTranslator()
					: base(ExpressionConverter.MethodCallTranslator.TrimTranslator.GetMethods(), "Trim")
				{
				}

				// Token: 0x06006D4D RID: 27981 RVA: 0x00174E97 File Offset: 0x00173097
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Trim", new Type[] { typeof(char[]) });
					yield break;
				}
			}

			// Token: 0x02000D08 RID: 3336
			internal sealed class TrimStartTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x06006D4E RID: 27982 RVA: 0x00174EA0 File Offset: 0x001730A0
				internal TrimStartTranslator()
					: base(ExpressionConverter.MethodCallTranslator.TrimStartTranslator.GetMethods(), "LTrim")
				{
				}

				// Token: 0x06006D4F RID: 27983 RVA: 0x00174EB2 File Offset: 0x001730B2
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("TrimStart", new Type[] { typeof(char[]) });
					yield break;
				}
			}

			// Token: 0x02000D09 RID: 3337
			internal sealed class TrimEndTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x06006D50 RID: 27984 RVA: 0x00174EBB File Offset: 0x001730BB
				internal TrimEndTranslator()
					: base(ExpressionConverter.MethodCallTranslator.TrimEndTranslator.GetMethods(), "RTrim")
				{
				}

				// Token: 0x06006D51 RID: 27985 RVA: 0x00174ECD File Offset: 0x001730CD
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("TrimEnd", new Type[] { typeof(char[]) });
					yield break;
				}
			}

			// Token: 0x02000D0A RID: 3338
			internal sealed class VBCanonicalFunctionDefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D52 RID: 27986 RVA: 0x00174ED6 File Offset: 0x001730D6
				internal VBCanonicalFunctionDefaultTranslator(Assembly vbAssembly)
					: base(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionDefaultTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x06006D53 RID: 27987 RVA: 0x00174EE4 File Offset: 0x001730E4
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type stringsType = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					yield return stringsType.GetDeclaredMethod("Trim", new Type[] { typeof(string) });
					yield return stringsType.GetDeclaredMethod("LTrim", new Type[] { typeof(string) });
					yield return stringsType.GetDeclaredMethod("RTrim", new Type[] { typeof(string) });
					yield return stringsType.GetDeclaredMethod("Left", new Type[]
					{
						typeof(string),
						typeof(int)
					});
					yield return stringsType.GetDeclaredMethod("Right", new Type[]
					{
						typeof(string),
						typeof(int)
					});
					Type dateTimeType = vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime");
					yield return dateTimeType.GetDeclaredMethod("Year", new Type[] { typeof(DateTime) });
					yield return dateTimeType.GetDeclaredMethod("Month", new Type[] { typeof(DateTime) });
					yield return dateTimeType.GetDeclaredMethod("Day", new Type[] { typeof(DateTime) });
					yield return dateTimeType.GetDeclaredMethod("Hour", new Type[] { typeof(DateTime) });
					yield return dateTimeType.GetDeclaredMethod("Minute", new Type[] { typeof(DateTime) });
					yield return dateTimeType.GetDeclaredMethod("Second", new Type[] { typeof(DateTime) });
					yield break;
				}

				// Token: 0x06006D54 RID: 27988 RVA: 0x00174EF4 File Offset: 0x001730F4
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(call.Method.Name, call, call.Arguments.ToArray<Expression>());
				}

				// Token: 0x04003277 RID: 12919
				private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

				// Token: 0x04003278 RID: 12920
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";
			}

			// Token: 0x02000D0B RID: 3339
			internal sealed class VBCanonicalFunctionRenameTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D55 RID: 27989 RVA: 0x00174F13 File Offset: 0x00173113
				internal VBCanonicalFunctionRenameTranslator(Assembly vbAssembly)
					: base(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethods(vbAssembly).ToArray<MethodInfo>())
				{
				}

				// Token: 0x06006D56 RID: 27990 RVA: 0x00174F26 File Offset: 0x00173126
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type stringsType = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "Len", "Length", new Type[] { typeof(string) });
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "Mid", "Substring", new Type[]
					{
						typeof(string),
						typeof(int),
						typeof(int)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "UCase", "ToUpper", new Type[] { typeof(string) });
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "LCase", "ToLower", new Type[] { typeof(string) });
					yield break;
				}

				// Token: 0x06006D57 RID: 27991 RVA: 0x00174F38 File Offset: 0x00173138
				private static MethodInfo GetMethodInfo(Type declaringType, string methodName, string canonicalFunctionName, Type[] argumentTypes)
				{
					MethodInfo declaredMethod = declaringType.GetDeclaredMethod(methodName, argumentTypes);
					ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.s_methodNameMap.Add(declaredMethod, canonicalFunctionName);
					return declaredMethod;
				}

				// Token: 0x06006D58 RID: 27992 RVA: 0x00174F5B File Offset: 0x0017315B
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.s_methodNameMap[call.Method], call, call.Arguments.ToArray<Expression>());
				}

				// Token: 0x04003279 RID: 12921
				private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

				// Token: 0x0400327A RID: 12922
				private static readonly Dictionary<MethodInfo, string> s_methodNameMap = new Dictionary<MethodInfo, string>(4);
			}

			// Token: 0x02000D0C RID: 3340
			internal sealed class VBDatePartTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006D5A RID: 27994 RVA: 0x00174F8C File Offset: 0x0017318C
				internal VBDatePartTranslator(Assembly vbAssembly)
					: base(ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x06006D5B RID: 27995 RVA: 0x00174F9A File Offset: 0x0017319A
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type type = vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime");
					Type type2 = vbAssembly.GetType("Microsoft.VisualBasic.DateInterval");
					Type type3 = vbAssembly.GetType("Microsoft.VisualBasic.FirstDayOfWeek");
					Type type4 = vbAssembly.GetType("Microsoft.VisualBasic.FirstWeekOfYear");
					yield return type.GetDeclaredMethod("DatePart", new Type[]
					{
						type2,
						typeof(DateTime),
						type3,
						type4
					});
					yield break;
				}

				// Token: 0x06006D5C RID: 27996 RVA: 0x00174FAC File Offset: 0x001731AC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					ConstantExpression constantExpression = call.Arguments[0] as ConstantExpression;
					if (constantExpression == null)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedVBDatePartNonConstantInterval(call.Method, call.Method.GetParameters()[0].Name));
					}
					string text = constantExpression.Value.ToString();
					if (!ExpressionConverter.MethodCallTranslator.VBDatePartTranslator._supportedIntervals.Contains(text))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedVBDatePartInvalidInterval(call.Method, call.Method.GetParameters()[0].Name, text));
					}
					return parent.TranslateIntoCanonicalFunction(text, call, new Expression[] { call.Arguments[1] });
				}

				// Token: 0x0400327B RID: 12923
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";

				// Token: 0x0400327C RID: 12924
				private const string s_DateIntervalFullName = "Microsoft.VisualBasic.DateInterval";

				// Token: 0x0400327D RID: 12925
				private const string s_FirstDayOfWeekFullName = "Microsoft.VisualBasic.FirstDayOfWeek";

				// Token: 0x0400327E RID: 12926
				private const string s_FirstWeekOfYearFullName = "Microsoft.VisualBasic.FirstWeekOfYear";

				// Token: 0x0400327F RID: 12927
				private static readonly HashSet<string> _supportedIntervals = new HashSet<string> { "Year", "Month", "Day", "Hour", "Minute", "Second" };
			}

			// Token: 0x02000D0D RID: 3341
			private abstract class SequenceMethodTranslator
			{
				// Token: 0x06006D5E RID: 27998 RVA: 0x001750AB File Offset: 0x001732AB
				protected SequenceMethodTranslator(params SequenceMethod[] methods)
				{
					this._methods = methods;
				}

				// Token: 0x17001197 RID: 4503
				// (get) Token: 0x06006D5F RID: 27999 RVA: 0x001750BA File Offset: 0x001732BA
				internal IEnumerable<SequenceMethod> Methods
				{
					get
					{
						return this._methods;
					}
				}

				// Token: 0x06006D60 RID: 28000 RVA: 0x001750C2 File Offset: 0x001732C2
				internal virtual DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
				{
					return this.Translate(parent, call);
				}

				// Token: 0x06006D61 RID: 28001
				internal abstract DbExpression Translate(ExpressionConverter parent, MethodCallExpression call);

				// Token: 0x06006D62 RID: 28002 RVA: 0x001750CC File Offset: 0x001732CC
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x04003280 RID: 12928
				private readonly IEnumerable<SequenceMethod> _methods;
			}

			// Token: 0x02000D0E RID: 3342
			private abstract class PagingTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06006D63 RID: 28003 RVA: 0x001750D9 File Offset: 0x001732D9
				protected PagingTranslator(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006D64 RID: 28004 RVA: 0x001750E4 File Offset: 0x001732E4
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					Expression expression = call.Arguments[1];
					DbExpression dbExpression = parent.TranslateExpression(expression);
					return this.TranslatePagingOperator(parent, operand, dbExpression);
				}

				// Token: 0x06006D65 RID: 28005
				protected abstract DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count);
			}

			// Token: 0x02000D0F RID: 3343
			private sealed class TakeTranslator : ExpressionConverter.MethodCallTranslator.PagingTranslator
			{
				// Token: 0x06006D66 RID: 28006 RVA: 0x0017510F File Offset: 0x0017330F
				internal TakeTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Take })
				{
				}

				// Token: 0x06006D67 RID: 28007 RVA: 0x00175124 File Offset: 0x00173324
				protected override DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count)
				{
					DbConstantExpression dbConstantExpression = count as DbConstantExpression;
					if (dbConstantExpression != null && dbConstantExpression.Value.Equals(0))
					{
						return parent.Filter(operand.BindAs(parent.AliasGenerator.Next()), DbExpressionBuilder.False);
					}
					return parent.Limit(operand, count);
				}
			}

			// Token: 0x02000D10 RID: 3344
			private sealed class SkipTranslator : ExpressionConverter.MethodCallTranslator.PagingTranslator
			{
				// Token: 0x06006D68 RID: 28008 RVA: 0x00175173 File Offset: 0x00173373
				internal SkipTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Skip })
				{
				}

				// Token: 0x06006D69 RID: 28009 RVA: 0x00175186 File Offset: 0x00173386
				protected override DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count)
				{
					return parent.Skip(operand.BindAs(parent.AliasGenerator.Next()), count);
				}
			}

			// Token: 0x02000D11 RID: 3345
			private sealed class JoinTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006D6A RID: 28010 RVA: 0x001751A0 File Offset: 0x001733A0
				internal JoinTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Join })
				{
				}

				// Token: 0x06006D6B RID: 28011 RVA: 0x001751B4 File Offset: 0x001733B4
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					DbExpression dbExpression2 = parent.TranslateSet(call.Arguments[1]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 2);
					LambdaExpression lambdaExpression2 = parent.GetLambdaExpression(call, 3);
					LambdaExpression lambdaExpression3 = parent.GetLambdaExpression(call, 4);
					string text;
					string text2;
					InitializerMetadata initializerMetadata;
					bool flag = ExpressionConverter.MethodCallTranslator.IsTrivialRename(lambdaExpression3, parent, out text, out text2, out initializerMetadata);
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression3 = (flag ? parent.TranslateLambda(lambdaExpression, dbExpression, text, out dbExpressionBinding) : parent.TranslateLambda(lambdaExpression, dbExpression, out dbExpressionBinding));
					DbExpressionBinding dbExpressionBinding2;
					DbExpression dbExpression4 = (flag ? parent.TranslateLambda(lambdaExpression2, dbExpression2, text2, out dbExpressionBinding2) : parent.TranslateLambda(lambdaExpression2, dbExpression2, out dbExpressionBinding2));
					if (!TypeSemantics.IsEqualComparable(dbExpression3.ResultType) || !TypeSemantics.IsEqualComparable(dbExpression4.ResultType))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
					}
					DbExpression dbExpression5 = parent.CreateEqualsExpression(dbExpression3, dbExpression4, ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable, lambdaExpression.Body.Type, lambdaExpression2.Body.Type);
					if (flag)
					{
						TypeUsage typeUsage = TypeUsage.Create(TypeHelpers.CreateRowType(new List<KeyValuePair<string, TypeUsage>>
						{
							new KeyValuePair<string, TypeUsage>(dbExpressionBinding.VariableName, dbExpressionBinding.VariableType),
							new KeyValuePair<string, TypeUsage>(dbExpressionBinding2.VariableName, dbExpressionBinding2.VariableType)
						}, initializerMetadata));
						return new DbJoinExpression(DbExpressionKind.InnerJoin, TypeUsage.Create(TypeHelpers.CreateCollectionType(typeUsage)), dbExpressionBinding, dbExpressionBinding2, dbExpression5);
					}
					DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding.InnerJoin(dbExpressionBinding2, dbExpression5).BindAs(parent.AliasGenerator.Next());
					DbPropertyExpression dbPropertyExpression = dbExpressionBinding3.Variable.Property(dbExpressionBinding.VariableName);
					DbPropertyExpression dbPropertyExpression2 = dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName);
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[0], dbPropertyExpression));
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[1], dbPropertyExpression2));
					DbExpression dbExpression6 = parent.TranslateExpression(lambdaExpression3.Body);
					parent._bindingContext.PopBindingScope();
					parent._bindingContext.PopBindingScope();
					return dbExpressionBinding3.Project(dbExpression6);
				}
			}

			// Token: 0x02000D12 RID: 3346
			private abstract class BinarySequenceMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006D6C RID: 28012 RVA: 0x001753AE File Offset: 0x001735AE
				protected BinarySequenceMethodTranslator(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006D6D RID: 28013 RVA: 0x001753B7 File Offset: 0x001735B7
				private static DbExpression TranslateLeft(ExpressionConverter parent, Expression expr)
				{
					return parent.TranslateSet(expr);
				}

				// Token: 0x06006D6E RID: 28014 RVA: 0x001753C0 File Offset: 0x001735C0
				protected virtual DbExpression TranslateRight(ExpressionConverter parent, Expression expr)
				{
					return parent.TranslateSet(expr);
				}

				// Token: 0x06006D6F RID: 28015 RVA: 0x001753CC File Offset: 0x001735CC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Object != null)
					{
						DbExpression dbExpression = ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator.TranslateLeft(parent, call.Object);
						DbExpression dbExpression2 = this.TranslateRight(parent, call.Arguments[0]);
						return this.TranslateBinary(parent, dbExpression, dbExpression2);
					}
					DbExpression dbExpression3 = ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator.TranslateLeft(parent, call.Arguments[0]);
					DbExpression dbExpression4 = this.TranslateRight(parent, call.Arguments[1]);
					return this.TranslateBinary(parent, dbExpression3, dbExpression4);
				}

				// Token: 0x06006D70 RID: 28016
				protected abstract DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right);
			}

			// Token: 0x02000D13 RID: 3347
			private class ConcatTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06006D71 RID: 28017 RVA: 0x0017543C File Offset: 0x0017363C
				internal ConcatTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Concat })
				{
				}

				// Token: 0x06006D72 RID: 28018 RVA: 0x0017544F File Offset: 0x0017364F
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.UnionAll(left, right);
				}
			}

			// Token: 0x02000D14 RID: 3348
			private sealed class UnionTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06006D73 RID: 28019 RVA: 0x00175459 File Offset: 0x00173659
				internal UnionTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Union })
				{
				}

				// Token: 0x06006D74 RID: 28020 RVA: 0x0017546C File Offset: 0x0017366C
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Distinct(parent.UnionAll(left, right));
				}
			}

			// Token: 0x02000D15 RID: 3349
			private sealed class IntersectTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06006D75 RID: 28021 RVA: 0x0017547C File Offset: 0x0017367C
				internal IntersectTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Intersect })
				{
				}

				// Token: 0x06006D76 RID: 28022 RVA: 0x0017548F File Offset: 0x0017368F
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Intersect(left, right);
				}
			}

			// Token: 0x02000D16 RID: 3350
			private sealed class ExceptTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06006D77 RID: 28023 RVA: 0x00175499 File Offset: 0x00173699
				internal ExceptTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Except })
				{
				}

				// Token: 0x06006D78 RID: 28024 RVA: 0x001754AC File Offset: 0x001736AC
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Except(left, right);
				}

				// Token: 0x06006D79 RID: 28025 RVA: 0x001754B8 File Offset: 0x001736B8
				protected override DbExpression TranslateRight(ExpressionConverter parent, Expression expr)
				{
					int num = parent.IgnoreInclude;
					parent.IgnoreInclude = num + 1;
					DbExpression dbExpression = base.TranslateRight(parent, expr);
					num = parent.IgnoreInclude;
					parent.IgnoreInclude = num - 1;
					return dbExpression;
				}
			}

			// Token: 0x02000D17 RID: 3351
			private abstract class AggregateTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006D7A RID: 28026 RVA: 0x001754ED File Offset: 0x001736ED
				protected AggregateTranslator(string functionName, bool takesPredicate, params SequenceMethod[] methods)
					: base(methods)
				{
					this._takesPredicate = takesPredicate;
					this._functionName = functionName;
				}

				// Token: 0x06006D7B RID: 28027 RVA: 0x00175504 File Offset: 0x00173704
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					bool flag = 1 == call.Arguments.Count;
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					if (!flag)
					{
						LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
						DbExpressionBinding dbExpressionBinding;
						DbExpression dbExpression2 = parent.TranslateLambda(lambdaExpression, dbExpression, out dbExpressionBinding);
						if (this._takesPredicate)
						{
							dbExpression = parent.Filter(dbExpressionBinding, dbExpression2);
						}
						else
						{
							dbExpression = dbExpressionBinding.Project(dbExpression2);
						}
					}
					TypeUsage returnType = this.GetReturnType(parent, call);
					EdmFunction edmFunction = this.FindFunction(parent, call, returnType);
					dbExpression = this.WrapCollectionOperand(parent, dbExpression, returnType);
					DbExpression dbExpression3 = edmFunction.Invoke(new List<DbExpression>(1) { dbExpression });
					return parent.AlignTypes(dbExpression3, call.Type);
				}

				// Token: 0x06006D7C RID: 28028 RVA: 0x001755AA File Offset: 0x001737AA
				protected virtual TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.GetValueLayerType(call.Type);
				}

				// Token: 0x06006D7D RID: 28029 RVA: 0x001755B8 File Offset: 0x001737B8
				protected virtual DbExpression WrapCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					if (!ExpressionConverter.TypeUsageEquals(returnType, ((CollectionType)operand.ResultType.EdmType).TypeUsage))
					{
						DbExpressionBinding dbExpressionBinding = operand.BindAs(parent.AliasGenerator.Next());
						operand = dbExpressionBinding.Project(dbExpressionBinding.Variable.CastTo(returnType));
					}
					return operand;
				}

				// Token: 0x06006D7E RID: 28030 RVA: 0x00175607 File Offset: 0x00173807
				protected virtual DbExpression WrapNonCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					if (!ExpressionConverter.TypeUsageEquals(returnType, operand.ResultType))
					{
						operand = operand.CastTo(returnType);
					}
					return operand;
				}

				// Token: 0x06006D7F RID: 28031 RVA: 0x00175624 File Offset: 0x00173824
				protected virtual EdmFunction FindFunction(ExpressionConverter parent, MethodCallExpression call, TypeUsage argumentType)
				{
					List<TypeUsage> list = new List<TypeUsage>(1);
					list.Add(argumentType);
					return parent.FindCanonicalFunction(this._functionName, list, true, call);
				}

				// Token: 0x04003281 RID: 12929
				private readonly string _functionName;

				// Token: 0x04003282 RID: 12930
				private readonly bool _takesPredicate;
			}

			// Token: 0x02000D18 RID: 3352
			private sealed class MaxTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06006D80 RID: 28032 RVA: 0x0017564E File Offset: 0x0017384E
				internal MaxTranslator()
					: base("Max", false, new SequenceMethod[]
					{
						SequenceMethod.Max,
						SequenceMethod.MaxSelector,
						SequenceMethod.MaxInt,
						SequenceMethod.MaxIntSelector,
						SequenceMethod.MaxDecimal,
						SequenceMethod.MaxDecimalSelector,
						SequenceMethod.MaxDouble,
						SequenceMethod.MaxDoubleSelector,
						SequenceMethod.MaxLong,
						SequenceMethod.MaxLongSelector,
						SequenceMethod.MaxSingle,
						SequenceMethod.MaxSingleSelector,
						SequenceMethod.MaxNullableDecimal,
						SequenceMethod.MaxNullableDecimalSelector,
						SequenceMethod.MaxNullableDouble,
						SequenceMethod.MaxNullableDoubleSelector,
						SequenceMethod.MaxNullableInt,
						SequenceMethod.MaxNullableIntSelector,
						SequenceMethod.MaxNullableLong,
						SequenceMethod.MaxNullableLongSelector,
						SequenceMethod.MaxNullableSingle,
						SequenceMethod.MaxNullableSingleSelector
					})
				{
				}

				// Token: 0x06006D81 RID: 28033 RVA: 0x00175670 File Offset: 0x00173870
				protected override TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					TypeUsage returnType = base.GetReturnType(parent, call);
					if (!TypeSemantics.IsEnumerationType(returnType))
					{
						return returnType;
					}
					return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(returnType.EdmType), returnType.Facets);
				}
			}

			// Token: 0x02000D19 RID: 3353
			private sealed class MinTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06006D82 RID: 28034 RVA: 0x001756A6 File Offset: 0x001738A6
				internal MinTranslator()
					: base("Min", false, new SequenceMethod[]
					{
						SequenceMethod.Min,
						SequenceMethod.MinSelector,
						SequenceMethod.MinDecimal,
						SequenceMethod.MinDecimalSelector,
						SequenceMethod.MinDouble,
						SequenceMethod.MinDoubleSelector,
						SequenceMethod.MinInt,
						SequenceMethod.MinIntSelector,
						SequenceMethod.MinLong,
						SequenceMethod.MinLongSelector,
						SequenceMethod.MinNullableDecimal,
						SequenceMethod.MinSingle,
						SequenceMethod.MinSingleSelector,
						SequenceMethod.MinNullableDecimalSelector,
						SequenceMethod.MinNullableDouble,
						SequenceMethod.MinNullableDoubleSelector,
						SequenceMethod.MinNullableInt,
						SequenceMethod.MinNullableIntSelector,
						SequenceMethod.MinNullableLong,
						SequenceMethod.MinNullableLongSelector,
						SequenceMethod.MinNullableSingle,
						SequenceMethod.MinNullableSingleSelector
					})
				{
				}

				// Token: 0x06006D83 RID: 28035 RVA: 0x001756C8 File Offset: 0x001738C8
				protected override TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					TypeUsage returnType = base.GetReturnType(parent, call);
					if (!TypeSemantics.IsEnumerationType(returnType))
					{
						return returnType;
					}
					return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(returnType.EdmType), returnType.Facets);
				}
			}

			// Token: 0x02000D1A RID: 3354
			private sealed class AverageTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06006D84 RID: 28036 RVA: 0x001756FE File Offset: 0x001738FE
				internal AverageTranslator()
					: base("Avg", false, new SequenceMethod[]
					{
						SequenceMethod.AverageDecimal,
						SequenceMethod.AverageDecimalSelector,
						SequenceMethod.AverageDouble,
						SequenceMethod.AverageDoubleSelector,
						SequenceMethod.AverageInt,
						SequenceMethod.AverageIntSelector,
						SequenceMethod.AverageLong,
						SequenceMethod.AverageLongSelector,
						SequenceMethod.AverageSingle,
						SequenceMethod.AverageSingleSelector,
						SequenceMethod.AverageNullableDecimal,
						SequenceMethod.AverageNullableDecimalSelector,
						SequenceMethod.AverageNullableDouble,
						SequenceMethod.AverageNullableDoubleSelector,
						SequenceMethod.AverageNullableInt,
						SequenceMethod.AverageNullableIntSelector,
						SequenceMethod.AverageNullableLong,
						SequenceMethod.AverageNullableLongSelector,
						SequenceMethod.AverageNullableSingle,
						SequenceMethod.AverageNullableSingleSelector
					})
				{
				}
			}

			// Token: 0x02000D1B RID: 3355
			private sealed class SumTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06006D85 RID: 28037 RVA: 0x0017571E File Offset: 0x0017391E
				internal SumTranslator()
					: base("Sum", false, new SequenceMethod[]
					{
						SequenceMethod.SumDecimal,
						SequenceMethod.SumDecimalSelector,
						SequenceMethod.SumDouble,
						SequenceMethod.SumDoubleSelector,
						SequenceMethod.SumInt,
						SequenceMethod.SumIntSelector,
						SequenceMethod.SumLong,
						SequenceMethod.SumLongSelector,
						SequenceMethod.SumSingle,
						SequenceMethod.SumSingleSelector,
						SequenceMethod.SumNullableDecimal,
						SequenceMethod.SumNullableDecimalSelector,
						SequenceMethod.SumNullableDouble,
						SequenceMethod.SumNullableDoubleSelector,
						SequenceMethod.SumNullableInt,
						SequenceMethod.SumNullableIntSelector,
						SequenceMethod.SumNullableLong,
						SequenceMethod.SumNullableLongSelector,
						SequenceMethod.SumNullableSingle,
						SequenceMethod.SumNullableSingleSelector
					})
				{
				}
			}

			// Token: 0x02000D1C RID: 3356
			private abstract class CountTranslatorBase : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06006D86 RID: 28038 RVA: 0x0017573E File Offset: 0x0017393E
				protected CountTranslatorBase(string functionName, params SequenceMethod[] methods)
					: base(functionName, true, methods)
				{
				}

				// Token: 0x06006D87 RID: 28039 RVA: 0x00175749 File Offset: 0x00173949
				protected override DbExpression WrapCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					return operand.BindAs(parent.AliasGenerator.Next()).Project(DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06006D88 RID: 28040 RVA: 0x0017576C File Offset: 0x0017396C
				protected override DbExpression WrapNonCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					DbExpression dbExpression = DbExpressionBuilder.Constant(1);
					if (!ExpressionConverter.TypeUsageEquals(dbExpression.ResultType, returnType))
					{
						dbExpression = dbExpression.CastTo(returnType);
					}
					return dbExpression;
				}

				// Token: 0x06006D89 RID: 28041 RVA: 0x0017579C File Offset: 0x0017399C
				protected override EdmFunction FindFunction(ExpressionConverter parent, MethodCallExpression call, TypeUsage argumentType)
				{
					TypeUsage typeUsage = TypeUsage.CreateDefaultTypeUsage(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.Int32));
					return base.FindFunction(parent, call, typeUsage);
				}
			}

			// Token: 0x02000D1D RID: 3357
			private sealed class CountTranslator : ExpressionConverter.MethodCallTranslator.CountTranslatorBase
			{
				// Token: 0x06006D8A RID: 28042 RVA: 0x001757C4 File Offset: 0x001739C4
				internal CountTranslator()
					: base("Count", new SequenceMethod[]
					{
						SequenceMethod.Count,
						SequenceMethod.CountPredicate
					})
				{
				}
			}

			// Token: 0x02000D1E RID: 3358
			private sealed class LongCountTranslator : ExpressionConverter.MethodCallTranslator.CountTranslatorBase
			{
				// Token: 0x06006D8B RID: 28043 RVA: 0x001757E1 File Offset: 0x001739E1
				internal LongCountTranslator()
					: base("BigCount", new SequenceMethod[]
					{
						SequenceMethod.LongCount,
						SequenceMethod.LongCountPredicate
					})
				{
				}
			}

			// Token: 0x02000D1F RID: 3359
			private abstract class UnarySequenceMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006D8C RID: 28044 RVA: 0x001757FE File Offset: 0x001739FE
				protected UnarySequenceMethodTranslator(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006D8D RID: 28045 RVA: 0x00175808 File Offset: 0x00173A08
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Object != null)
					{
						DbExpression dbExpression = parent.TranslateSet(call.Object);
						return this.TranslateUnary(parent, dbExpression, call);
					}
					DbExpression dbExpression2 = parent.TranslateSet(call.Arguments[0]);
					return this.TranslateUnary(parent, dbExpression2, call);
				}

				// Token: 0x06006D8E RID: 28046
				protected abstract DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call);
			}

			// Token: 0x02000D20 RID: 3360
			private sealed class PassthroughTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06006D8F RID: 28047 RVA: 0x00175850 File Offset: 0x00173A50
				internal PassthroughTranslator()
					: base(new SequenceMethod[]
					{
						SequenceMethod.AsQueryableGeneric,
						SequenceMethod.AsQueryable,
						SequenceMethod.AsEnumerable,
						SequenceMethod.ToList
					})
				{
				}

				// Token: 0x06006D90 RID: 28048 RVA: 0x00175869 File Offset: 0x00173A69
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (TypeSemantics.IsCollectionType(operand.ResultType))
					{
						return operand;
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedPassthrough(call.Method.Name, operand.ResultType.EdmType.Name));
				}
			}

			// Token: 0x02000D21 RID: 3361
			private sealed class OfTypeTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06006D91 RID: 28049 RVA: 0x0017589F File Offset: 0x00173A9F
				internal OfTypeTranslator()
					: base(new SequenceMethod[] { SequenceMethod.OfType })
				{
				}

				// Token: 0x06006D92 RID: 28050 RVA: 0x001758B4 File Offset: 0x00173AB4
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					Type type = call.Method.GetGenericArguments()[0];
					TypeUsage typeUsage;
					if (!parent.TryGetValueLayerType(type, out typeUsage) || (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsComplexType(typeUsage)))
					{
						throw new NotSupportedException(Strings.ELinq_InvalidOfTypeResult(ExpressionConverter.DescribeClrType(type)));
					}
					return parent.OfType(operand, typeUsage);
				}
			}

			// Token: 0x02000D22 RID: 3362
			private sealed class DistinctTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06006D93 RID: 28051 RVA: 0x00175903 File Offset: 0x00173B03
				internal DistinctTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Distinct })
				{
				}

				// Token: 0x06006D94 RID: 28052 RVA: 0x00175916 File Offset: 0x00173B16
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					return parent.Distinct(operand);
				}
			}

			// Token: 0x02000D23 RID: 3363
			private sealed class AnyTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06006D95 RID: 28053 RVA: 0x0017591F File Offset: 0x00173B1F
				internal AnyTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Any })
				{
				}

				// Token: 0x06006D96 RID: 28054 RVA: 0x00175932 File Offset: 0x00173B32
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					return operand.IsEmpty().Not();
				}
			}

			// Token: 0x02000D24 RID: 3364
			private abstract class OneLambdaTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006D97 RID: 28055 RVA: 0x0017593F File Offset: 0x00173B3F
				internal OneLambdaTranslator(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006D98 RID: 28056 RVA: 0x00175948 File Offset: 0x00173B48
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression;
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression2;
					return this.Translate(parent, call, out dbExpression, out dbExpressionBinding, out dbExpression2);
				}

				// Token: 0x06006D99 RID: 28057 RVA: 0x00175964 File Offset: 0x00173B64
				protected DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, out DbExpression source, out DbExpressionBinding sourceBinding, out DbExpression lambda)
				{
					source = parent.TranslateExpression(call.Arguments[0]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					lambda = parent.TranslateLambda(lambdaExpression, source, out sourceBinding);
					return this.TranslateOneLambda(parent, sourceBinding, lambda);
				}

				// Token: 0x06006D9A RID: 28058
				protected abstract DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda);
			}

			// Token: 0x02000D25 RID: 3365
			private sealed class AnyPredicateTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006D9B RID: 28059 RVA: 0x001759A9 File Offset: 0x00173BA9
				internal AnyPredicateTranslator()
					: base(new SequenceMethod[] { SequenceMethod.AnyPredicate })
				{
				}

				// Token: 0x06006D9C RID: 28060 RVA: 0x001759BC File Offset: 0x00173BBC
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return sourceBinding.Any(lambda);
				}
			}

			// Token: 0x02000D26 RID: 3366
			private sealed class AllTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006D9D RID: 28061 RVA: 0x001759C5 File Offset: 0x00173BC5
				internal AllTranslator()
					: base(new SequenceMethod[] { SequenceMethod.All })
				{
				}

				// Token: 0x06006D9E RID: 28062 RVA: 0x001759D8 File Offset: 0x00173BD8
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return sourceBinding.All(lambda);
				}
			}

			// Token: 0x02000D27 RID: 3367
			private sealed class WhereTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006D9F RID: 28063 RVA: 0x001759E1 File Offset: 0x00173BE1
				internal WhereTranslator()
					: base(new SequenceMethod[1])
				{
				}

				// Token: 0x06006DA0 RID: 28064 RVA: 0x001759EF File Offset: 0x00173BEF
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Filter(sourceBinding, lambda);
				}
			}

			// Token: 0x02000D28 RID: 3368
			private sealed class SelectTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006DA1 RID: 28065 RVA: 0x001759F9 File Offset: 0x00173BF9
				internal SelectTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Select })
				{
				}

				// Token: 0x06006DA2 RID: 28066 RVA: 0x00175A0C File Offset: 0x00173C0C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression;
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression2;
					return base.Translate(parent, call, out dbExpression, out dbExpressionBinding, out dbExpression2);
				}

				// Token: 0x06006DA3 RID: 28067 RVA: 0x00175A27 File Offset: 0x00173C27
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Project(sourceBinding, lambda);
				}
			}

			// Token: 0x02000D29 RID: 3369
			private sealed class DefaultIfEmptyTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006DA4 RID: 28068 RVA: 0x00175A31 File Offset: 0x00173C31
				internal DefaultIfEmptyTranslator()
					: base(new SequenceMethod[]
					{
						SequenceMethod.DefaultIfEmpty,
						SequenceMethod.DefaultIfEmptyValue
					})
				{
				}

				// Token: 0x06006DA5 RID: 28069 RVA: 0x00175A4C File Offset: 0x00173C4C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					DbExpression dbExpression2 = ((call.Arguments.Count == 2) ? parent.TranslateExpression(call.Arguments[1]) : ExpressionConverter.MethodCallTranslator.DefaultIfEmptyTranslator.GetDefaultValue(parent, call.Type));
					DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.NewCollection(new DbExpression[]
					{
						new byte?((byte)1)
					}).BindAs(parent.AliasGenerator.Next());
					bool flag = dbExpression2 != null && dbExpression2.ExpressionKind != DbExpressionKind.Null;
					if (flag)
					{
						DbExpressionBinding dbExpressionBinding2 = dbExpression.BindAs(parent.AliasGenerator.Next());
						dbExpression = dbExpressionBinding2.Project(new Row(new byte?((byte)1).As("sentinel"), new KeyValuePair<string, DbExpression>[] { dbExpressionBinding2.Variable.As("value") }));
					}
					DbExpressionBinding dbExpressionBinding3 = dbExpression.BindAs(parent.AliasGenerator.Next());
					DbExpressionBinding dbExpressionBinding4 = dbExpressionBinding.LeftOuterJoin(dbExpressionBinding3, new bool?(true)).BindAs(parent.AliasGenerator.Next());
					DbExpression dbExpression3 = dbExpressionBinding4.Variable.Property(dbExpressionBinding3.VariableName);
					if (flag)
					{
						dbExpression3 = DbExpressionBuilder.Case(new DbIsNullExpression[] { dbExpression3.Property("sentinel").IsNull() }, new DbExpression[] { dbExpression2 }, dbExpression3.Property("value"));
					}
					DbExpression dbExpression4 = dbExpressionBinding4.Project(dbExpression3);
					parent.ApplySpanMapping(dbExpression, dbExpression4);
					return dbExpression4;
				}

				// Token: 0x06006DA6 RID: 28070 RVA: 0x00175BD4 File Offset: 0x00173DD4
				private static DbExpression GetDefaultValue(ExpressionConverter parent, Type resultType)
				{
					Type elementType = TypeSystem.GetElementType(resultType);
					object defaultValue = TypeSystem.GetDefaultValue(elementType);
					if (defaultValue != null)
					{
						return parent.TranslateExpression(Expression.Constant(defaultValue, elementType));
					}
					return null;
				}
			}

			// Token: 0x02000D2A RID: 3370
			private sealed class ContainsTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006DA7 RID: 28071 RVA: 0x00175C01 File Offset: 0x00173E01
				internal ContainsTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Contains })
				{
				}

				// Token: 0x06006DA8 RID: 28072 RVA: 0x00175C14 File Offset: 0x00173E14
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContains(parent, call.Arguments[0], call.Arguments[1]);
				}

				// Token: 0x06006DA9 RID: 28073 RVA: 0x00175C34 File Offset: 0x00173E34
				private static DbExpression TranslateContainsHelper(ExpressionConverter parent, DbExpression left, IEnumerable<DbExpression> rightList, ExpressionConverter.EqualsPattern pattern, Type leftType, Type rightType)
				{
					return Helpers.BuildBalancedTreeInPlace<DbExpression>(new List<DbExpression>(rightList.Select((DbExpression argument) => parent.CreateEqualsExpression(left, argument, pattern, leftType, rightType))), (DbExpression prev, DbExpression next) => prev.Or(next));
				}

				// Token: 0x06006DAA RID: 28074 RVA: 0x00175CA8 File Offset: 0x00173EA8
				internal static DbExpression TranslateContains(ExpressionConverter parent, Expression sourceExpression, Expression valueExpression)
				{
					DbExpression dbExpression = parent.NormalizeSetSource(parent.TranslateExpression(sourceExpression));
					DbExpression dbExpression2 = parent.TranslateExpression(valueExpression);
					Type elementType = TypeSystem.GetElementType(sourceExpression.Type);
					if (dbExpression.ExpressionKind != DbExpressionKind.NewInstance)
					{
						DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(parent.AliasGenerator.Next());
						ExpressionConverter.EqualsPattern equalsPattern = ExpressionConverter.EqualsPattern.Store;
						if (parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
						{
							equalsPattern = ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable;
						}
						return dbExpressionBinding.Filter(parent.CreateEqualsExpression(dbExpressionBinding.Variable, dbExpression2, equalsPattern, elementType, valueExpression.Type)).Exists();
					}
					IList<DbExpression> arguments = ((DbNewInstanceExpression)dbExpression).Arguments;
					if (arguments.Count <= 0)
					{
						return new bool?(false);
					}
					bool useCSharpNullComparisonBehavior = parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior;
					bool flag = parent.ProviderManifest.SupportsInExpression();
					if (!useCSharpNullComparisonBehavior && !flag)
					{
						return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, arguments, ExpressionConverter.EqualsPattern.Store, elementType, valueExpression.Type);
					}
					List<DbExpression> list = new List<DbExpression>();
					List<DbExpression> list2 = new List<DbExpression>();
					foreach (DbExpression dbExpression3 in arguments)
					{
						((dbExpression3.ExpressionKind == DbExpressionKind.Constant) ? list : list2).Add(dbExpression3);
					}
					DbExpression dbExpression4 = null;
					if (list.Count > 0)
					{
						ExpressionConverter.EqualsPattern equalsPattern2 = (useCSharpNullComparisonBehavior ? ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable : ExpressionConverter.EqualsPattern.Store);
						dbExpression4 = (flag ? DbExpressionBuilder.CreateInExpression(dbExpression2, list) : ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, list, equalsPattern2, elementType, valueExpression.Type));
						if (useCSharpNullComparisonBehavior)
						{
							dbExpression4 = dbExpression4.And(dbExpression2.IsNull().Not());
						}
					}
					DbExpression dbExpression5 = null;
					if (list2.Count > 0)
					{
						ExpressionConverter.EqualsPattern equalsPattern3 = (useCSharpNullComparisonBehavior ? ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable : ExpressionConverter.EqualsPattern.Store);
						dbExpression5 = ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, list2, equalsPattern3, elementType, valueExpression.Type);
					}
					if (dbExpression4 == null)
					{
						return dbExpression5;
					}
					if (dbExpression5 == null)
					{
						return dbExpression4;
					}
					return dbExpression4.Or(dbExpression5);
				}
			}

			// Token: 0x02000D2B RID: 3371
			private abstract class FirstTranslatorBase : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06006DAB RID: 28075 RVA: 0x00175E8C File Offset: 0x0017408C
				protected FirstTranslatorBase(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006DAC RID: 28076 RVA: 0x00175E95 File Offset: 0x00174095
				protected virtual DbExpression LimitResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06006DAD RID: 28077 RVA: 0x00175EAC File Offset: 0x001740AC
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					DbExpression dbExpression = this.LimitResult(parent, operand);
					if (!parent.IsQueryRoot(call))
					{
						dbExpression = dbExpression.Element();
						dbExpression = ExpressionConverter.MethodCallTranslator.FirstTranslatorBase.AddDefaultCase(dbExpression, call.Type);
					}
					Span span = null;
					if (parent.TryGetSpan(operand, out span))
					{
						parent.AddSpanMapping(dbExpression, span);
					}
					return dbExpression;
				}

				// Token: 0x06006DAE RID: 28078 RVA: 0x00175EF8 File Offset: 0x001740F8
				internal static DbExpression AddDefaultCase(DbExpression element, Type elementType)
				{
					object defaultValue = TypeSystem.GetDefaultValue(elementType);
					if (defaultValue == null)
					{
						return element;
					}
					return DbExpressionBuilder.Case(new List<DbExpression>(1) { ExpressionConverter.CreateIsNullExpression(element, elementType) }, new List<DbExpression>(1) { element.ResultType.Constant(defaultValue) }, element);
				}
			}

			// Token: 0x02000D2C RID: 3372
			private sealed class FirstTranslator : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x06006DAF RID: 28079 RVA: 0x00175F44 File Offset: 0x00174144
				internal FirstTranslator()
					: base(new SequenceMethod[] { SequenceMethod.First })
				{
				}

				// Token: 0x06006DB0 RID: 28080 RVA: 0x00175F57 File Offset: 0x00174157
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedFirst);
					}
					return base.TranslateUnary(parent, operand, call);
				}
			}

			// Token: 0x02000D2D RID: 3373
			private sealed class FirstOrDefaultTranslator : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x06006DB1 RID: 28081 RVA: 0x00175F76 File Offset: 0x00174176
				internal FirstOrDefaultTranslator()
					: base(new SequenceMethod[] { SequenceMethod.FirstOrDefault })
				{
				}
			}

			// Token: 0x02000D2E RID: 3374
			private abstract class SingleTranslatorBase : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x06006DB2 RID: 28082 RVA: 0x00175F89 File Offset: 0x00174189
				protected SingleTranslatorBase(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006DB3 RID: 28083 RVA: 0x00175F92 File Offset: 0x00174192
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedSingle);
					}
					return base.TranslateUnary(parent, operand, call);
				}

				// Token: 0x06006DB4 RID: 28084 RVA: 0x00175FB1 File Offset: 0x001741B1
				protected override DbExpression LimitResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(2));
				}
			}

			// Token: 0x02000D2F RID: 3375
			private sealed class SingleTranslator : ExpressionConverter.MethodCallTranslator.SingleTranslatorBase
			{
				// Token: 0x06006DB5 RID: 28085 RVA: 0x00175FC5 File Offset: 0x001741C5
				internal SingleTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Single })
				{
				}
			}

			// Token: 0x02000D30 RID: 3376
			private sealed class SingleOrDefaultTranslator : ExpressionConverter.MethodCallTranslator.SingleTranslatorBase
			{
				// Token: 0x06006DB6 RID: 28086 RVA: 0x00175FD8 File Offset: 0x001741D8
				internal SingleOrDefaultTranslator()
					: base(new SequenceMethod[] { SequenceMethod.SingleOrDefault })
				{
				}
			}

			// Token: 0x02000D31 RID: 3377
			private abstract class FirstPredicateTranslatorBase : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006DB7 RID: 28087 RVA: 0x00175FEB File Offset: 0x001741EB
				protected FirstPredicateTranslatorBase(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006DB8 RID: 28088 RVA: 0x00175FF4 File Offset: 0x001741F4
				protected virtual DbExpression RestrictResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06006DB9 RID: 28089 RVA: 0x00176008 File Offset: 0x00174208
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = base.Translate(parent, call);
					if (parent.IsQueryRoot(call))
					{
						return this.RestrictResult(parent, dbExpression);
					}
					dbExpression = this.RestrictResult(parent, dbExpression);
					DbExpression dbExpression2 = dbExpression.Element();
					dbExpression2 = ExpressionConverter.MethodCallTranslator.FirstTranslatorBase.AddDefaultCase(dbExpression2, call.Type);
					Span span = null;
					if (parent.TryGetSpan(dbExpression, out span))
					{
						parent.AddSpanMapping(dbExpression2, span);
					}
					return dbExpression2;
				}

				// Token: 0x06006DBA RID: 28090 RVA: 0x00176064 File Offset: 0x00174264
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Filter(sourceBinding, lambda);
				}
			}

			// Token: 0x02000D32 RID: 3378
			private sealed class FirstPredicateTranslator : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x06006DBB RID: 28091 RVA: 0x0017606E File Offset: 0x0017426E
				internal FirstPredicateTranslator()
					: base(new SequenceMethod[] { SequenceMethod.FirstPredicate })
				{
				}

				// Token: 0x06006DBC RID: 28092 RVA: 0x00176081 File Offset: 0x00174281
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedFirst);
					}
					return base.Translate(parent, call);
				}
			}

			// Token: 0x02000D33 RID: 3379
			private sealed class FirstOrDefaultPredicateTranslator : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x06006DBD RID: 28093 RVA: 0x0017609F File Offset: 0x0017429F
				internal FirstOrDefaultPredicateTranslator()
					: base(new SequenceMethod[] { SequenceMethod.FirstOrDefaultPredicate })
				{
				}
			}

			// Token: 0x02000D34 RID: 3380
			private abstract class SinglePredicateTranslatorBase : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x06006DBE RID: 28094 RVA: 0x001760B2 File Offset: 0x001742B2
				protected SinglePredicateTranslatorBase(params SequenceMethod[] methods)
					: base(methods)
				{
				}

				// Token: 0x06006DBF RID: 28095 RVA: 0x001760BB File Offset: 0x001742BB
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedSingle);
					}
					return base.Translate(parent, call);
				}

				// Token: 0x06006DC0 RID: 28096 RVA: 0x001760D9 File Offset: 0x001742D9
				protected override DbExpression RestrictResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(2));
				}
			}

			// Token: 0x02000D35 RID: 3381
			private sealed class SinglePredicateTranslator : ExpressionConverter.MethodCallTranslator.SinglePredicateTranslatorBase
			{
				// Token: 0x06006DC1 RID: 28097 RVA: 0x001760ED File Offset: 0x001742ED
				internal SinglePredicateTranslator()
					: base(new SequenceMethod[] { SequenceMethod.SinglePredicate })
				{
				}
			}

			// Token: 0x02000D36 RID: 3382
			private sealed class SingleOrDefaultPredicateTranslator : ExpressionConverter.MethodCallTranslator.SinglePredicateTranslatorBase
			{
				// Token: 0x06006DC2 RID: 28098 RVA: 0x00176100 File Offset: 0x00174300
				internal SingleOrDefaultPredicateTranslator()
					: base(new SequenceMethod[] { SequenceMethod.SingleOrDefaultPredicate })
				{
				}
			}

			// Token: 0x02000D37 RID: 3383
			private sealed class SelectManyTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006DC3 RID: 28099 RVA: 0x00176113 File Offset: 0x00174313
				internal SelectManyTranslator()
					: base(new SequenceMethod[]
					{
						SequenceMethod.SelectMany,
						SequenceMethod.SelectManyResultSelector
					})
				{
				}

				// Token: 0x06006DC4 RID: 28100 RVA: 0x0017612C File Offset: 0x0017432C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					LambdaExpression lambdaExpression = ((call.Arguments.Count == 3) ? parent.GetLambdaExpression(call, 2) : null);
					DbExpression dbExpression = base.Translate(parent, call);
					DbExpressionBinding dbExpressionBinding;
					EdmProperty edmProperty;
					if (ExpressionConverter.MethodCallTranslator.SelectManyTranslator.IsLeftOuterJoin(dbExpression, out dbExpressionBinding, out edmProperty))
					{
						string text;
						string text2;
						InitializerMetadata initializerMetadata;
						if (lambdaExpression != null && ExpressionConverter.MethodCallTranslator.IsTrivialRename(lambdaExpression, parent, out text, out text2, out initializerMetadata))
						{
							DbExpressionBinding dbExpressionBinding2 = dbExpressionBinding.Expression.BindAs(text);
							DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding2.Variable.Property(edmProperty.Name).BindAs(text2);
							TypeUsage typeUsage = TypeUsage.Create(TypeHelpers.CreateRowType(new List<KeyValuePair<string, TypeUsage>>
							{
								new KeyValuePair<string, TypeUsage>(dbExpressionBinding2.VariableName, dbExpressionBinding2.VariableType),
								new KeyValuePair<string, TypeUsage>(dbExpressionBinding3.VariableName, dbExpressionBinding3.VariableType)
							}, initializerMetadata));
							return new DbApplyExpression(DbExpressionKind.OuterApply, TypeUsage.Create(TypeHelpers.CreateCollectionType(typeUsage)), dbExpressionBinding2, dbExpressionBinding3);
						}
						dbExpression = dbExpressionBinding.OuterApply(dbExpressionBinding.Variable.Property(edmProperty).BindAs(parent.AliasGenerator.Next()));
					}
					DbExpressionBinding dbExpressionBinding4 = dbExpression.BindAs(parent.AliasGenerator.Next());
					RowType rowType = (RowType)dbExpressionBinding4.Variable.ResultType.EdmType;
					DbExpression dbExpression2 = dbExpressionBinding4.Variable.Property(rowType.Properties[1]);
					DbExpression dbExpression4;
					if (lambdaExpression != null)
					{
						DbExpression dbExpression3 = dbExpressionBinding4.Variable.Property(rowType.Properties[0]);
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[0], dbExpression3));
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[1], dbExpression2));
						dbExpression4 = parent.TranslateSet(lambdaExpression.Body);
						parent._bindingContext.PopBindingScope();
						parent._bindingContext.PopBindingScope();
					}
					else
					{
						dbExpression4 = dbExpression2;
					}
					return dbExpressionBinding4.Project(dbExpression4);
				}

				// Token: 0x06006DC5 RID: 28101 RVA: 0x00176300 File Offset: 0x00174500
				private static bool IsLeftOuterJoin(DbExpression cqtExpression, out DbExpressionBinding crossApplyInput, out EdmProperty lojRightInput)
				{
					crossApplyInput = null;
					lojRightInput = null;
					if (cqtExpression.ExpressionKind != DbExpressionKind.CrossApply)
					{
						return false;
					}
					DbApplyExpression dbApplyExpression = (DbApplyExpression)cqtExpression;
					if (dbApplyExpression.Input.VariableType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
					{
						return false;
					}
					RowType rowType = (RowType)dbApplyExpression.Input.VariableType.EdmType;
					if (dbApplyExpression.Apply.Expression.ExpressionKind != DbExpressionKind.Project)
					{
						return false;
					}
					DbProjectExpression dbProjectExpression = (DbProjectExpression)dbApplyExpression.Apply.Expression;
					if (dbProjectExpression.Input.Expression.ExpressionKind != DbExpressionKind.LeftOuterJoin)
					{
						return false;
					}
					DbJoinExpression dbJoinExpression = (DbJoinExpression)dbProjectExpression.Input.Expression;
					if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.Property)
					{
						return false;
					}
					DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbProjectExpression.Projection;
					if (dbPropertyExpression.Instance != dbProjectExpression.Input.Variable || dbPropertyExpression.Property.Name != dbJoinExpression.Right.VariableName || dbJoinExpression.JoinCondition.ExpressionKind != DbExpressionKind.Constant)
					{
						return false;
					}
					DbConstantExpression dbConstantExpression = (DbConstantExpression)dbJoinExpression.JoinCondition;
					if (!(dbConstantExpression.Value is bool) || !(bool)dbConstantExpression.Value)
					{
						return false;
					}
					if (dbJoinExpression.Left.Expression.ExpressionKind != DbExpressionKind.NewInstance)
					{
						return false;
					}
					DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbJoinExpression.Left.Expression;
					if (dbNewInstanceExpression.Arguments.Count != 1 || dbNewInstanceExpression.Arguments[0].ExpressionKind != DbExpressionKind.Constant)
					{
						return false;
					}
					if (dbJoinExpression.Right.Expression.ExpressionKind != DbExpressionKind.Property)
					{
						return false;
					}
					DbPropertyExpression lojRight = (DbPropertyExpression)dbJoinExpression.Right.Expression;
					if (lojRight.Instance != dbApplyExpression.Input.Variable)
					{
						return false;
					}
					EdmProperty edmProperty = rowType.Properties.SingleOrDefault((EdmProperty p) => p.Name == lojRight.Property.Name);
					if (edmProperty == null)
					{
						return false;
					}
					crossApplyInput = dbApplyExpression.Input;
					lojRightInput = edmProperty;
					return true;
				}

				// Token: 0x06006DC6 RID: 28102 RVA: 0x00176500 File Offset: 0x00174700
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					lambda = parent.NormalizeSetSource(lambda);
					DbExpressionBinding dbExpressionBinding = lambda.BindAs(parent.AliasGenerator.Next());
					return sourceBinding.CrossApply(dbExpressionBinding);
				}
			}

			// Token: 0x02000D38 RID: 3384
			private sealed class CastMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006DC7 RID: 28103 RVA: 0x0017652F File Offset: 0x0017472F
				internal CastMethodTranslator()
					: base(new SequenceMethod[] { SequenceMethod.Cast })
				{
				}

				// Token: 0x06006DC8 RID: 28104 RVA: 0x00176544 File Offset: 0x00174744
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					Type elementType = TypeSystem.GetElementType(call.Type);
					Type elementType2 = TypeSystem.GetElementType(call.Arguments[0].Type);
					DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(parent.AliasGenerator.Next());
					DbExpression dbExpression2 = parent.CreateCastExpression(dbExpressionBinding.Variable, elementType, elementType2);
					return parent.Project(dbExpressionBinding, dbExpression2);
				}
			}

			// Token: 0x02000D39 RID: 3385
			private sealed class GroupByTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006DC9 RID: 28105 RVA: 0x001765AE File Offset: 0x001747AE
				internal GroupByTranslator()
					: base(new SequenceMethod[]
					{
						SequenceMethod.GroupBy,
						SequenceMethod.GroupByElementSelector,
						SequenceMethod.GroupByElementSelectorResultSelector,
						SequenceMethod.GroupByResultSelector
					})
				{
				}

				// Token: 0x06006DCA RID: 28106 RVA: 0x001765C8 File Offset: 0x001747C8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					DbGroupExpressionBinding dbGroupExpressionBinding;
					DbExpression dbExpression2 = parent.TranslateLambda(lambdaExpression, dbExpression, out dbGroupExpressionBinding);
					if (!TypeSemantics.IsEqualComparable(dbExpression2.ResultType))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
					}
					List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
					List<KeyValuePair<string, DbAggregate>> list2 = new List<KeyValuePair<string, DbAggregate>>();
					list.Add(new KeyValuePair<string, DbExpression>("Key", dbExpression2));
					list2.Add(new KeyValuePair<string, DbAggregate>("Group", dbGroupExpressionBinding.GroupAggregate));
					DbExpressionBinding dbExpressionBinding = dbGroupExpressionBinding.GroupBy(list, list2).BindAs(parent.AliasGenerator.Next());
					DbExpression dbExpression3 = dbExpressionBinding.Variable.Property("Group");
					if (sequenceMethod == SequenceMethod.GroupByElementSelector || sequenceMethod == SequenceMethod.GroupByElementSelectorResultSelector)
					{
						LambdaExpression lambdaExpression2 = parent.GetLambdaExpression(call, 2);
						DbExpressionBinding dbExpressionBinding2;
						DbExpression dbExpression4 = parent.TranslateLambda(lambdaExpression2, dbExpression3, out dbExpressionBinding2);
						dbExpression3 = dbExpressionBinding2.Project(dbExpression4);
					}
					DbExpression[] array = new DbExpression[]
					{
						dbExpressionBinding.Variable.Property("Key"),
						dbExpression3
					};
					List<EdmProperty> list3 = new List<EdmProperty>(2);
					list3.Add(new EdmProperty("Key", array[0].ResultType));
					list3.Add(new EdmProperty("Group", array[1].ResultType));
					InitializerMetadata initializerMetadata = InitializerMetadata.CreateGroupingInitializer(parent.EdmItemCollection, TypeSystem.GetElementType(call.Type));
					TypeUsage typeUsage = TypeUsage.Create(new RowType(list3, initializerMetadata));
					DbExpression dbExpression5 = dbExpressionBinding.Project(typeUsage.New(array));
					DbExpression dbExpression6 = dbExpression5;
					return ExpressionConverter.MethodCallTranslator.GroupByTranslator.ProcessResultSelector(parent, call, sequenceMethod, dbExpression5, dbExpression6);
				}

				// Token: 0x06006DCB RID: 28107 RVA: 0x00176760 File Offset: 0x00174960
				private static DbExpression ProcessResultSelector(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod, DbExpression topLevelProject, DbExpression result)
				{
					LambdaExpression lambdaExpression = null;
					if (sequenceMethod == SequenceMethod.GroupByResultSelector)
					{
						lambdaExpression = parent.GetLambdaExpression(call, 2);
					}
					else if (sequenceMethod == SequenceMethod.GroupByElementSelectorResultSelector)
					{
						lambdaExpression = parent.GetLambdaExpression(call, 3);
					}
					if (lambdaExpression != null)
					{
						DbExpressionBinding dbExpressionBinding = topLevelProject.BindAs(parent.AliasGenerator.Next());
						DbPropertyExpression dbPropertyExpression = dbExpressionBinding.Variable.Property("Key");
						DbPropertyExpression dbPropertyExpression2 = dbExpressionBinding.Variable.Property("Group");
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[0], dbPropertyExpression));
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[1], dbPropertyExpression2));
						DbExpression dbExpression = parent.TranslateExpression(lambdaExpression.Body);
						result = dbExpressionBinding.Project(dbExpression);
						parent._bindingContext.PopBindingScope();
						parent._bindingContext.PopBindingScope();
					}
					return result;
				}

				// Token: 0x06006DCC RID: 28108 RVA: 0x0017682D File Offset: 0x00174A2D
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return null;
				}
			}

			// Token: 0x02000D3A RID: 3386
			private sealed class GroupJoinTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006DCD RID: 28109 RVA: 0x00176830 File Offset: 0x00174A30
				internal GroupJoinTranslator()
					: base(new SequenceMethod[] { SequenceMethod.GroupJoin })
				{
				}

				// Token: 0x06006DCE RID: 28110 RVA: 0x00176844 File Offset: 0x00174A44
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					DbExpression dbExpression2 = parent.TranslateSet(call.Arguments[1]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 2);
					LambdaExpression lambdaExpression2 = parent.GetLambdaExpression(call, 3);
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression3 = parent.TranslateLambda(lambdaExpression, dbExpression, out dbExpressionBinding);
					DbExpressionBinding dbExpressionBinding2;
					DbExpression dbExpression4 = parent.TranslateLambda(lambdaExpression2, dbExpression2, out dbExpressionBinding2);
					if (!TypeSemantics.IsEqualComparable(dbExpression3.ResultType) || !TypeSemantics.IsEqualComparable(dbExpression4.ResultType))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
					}
					DbExpression dbExpression5 = parent.Filter(dbExpressionBinding2, parent.CreateEqualsExpression(dbExpression3, dbExpression4, ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable, lambdaExpression.Body.Type, lambdaExpression2.Body.Type));
					DbExpression dbExpression6 = DbExpressionBuilder.NewRow(new List<KeyValuePair<string, DbExpression>>(2)
					{
						new KeyValuePair<string, DbExpression>("o", dbExpressionBinding.Variable),
						new KeyValuePair<string, DbExpression>("i", dbExpression5)
					});
					DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding.Project(dbExpression6).BindAs(parent.AliasGenerator.Next());
					DbExpression dbExpression7 = dbExpressionBinding3.Variable.Property("o");
					DbExpression dbExpression8 = dbExpressionBinding3.Variable.Property("i");
					LambdaExpression lambdaExpression3 = parent.GetLambdaExpression(call, 4);
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[0], dbExpression7));
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[1], dbExpression8));
					DbExpression dbExpression9 = parent.TranslateExpression(lambdaExpression3.Body);
					parent._bindingContext.PopBindingScope();
					parent._bindingContext.PopBindingScope();
					return ExpressionConverter.MethodCallTranslator.GroupJoinTranslator.CollapseTrivialRenamingProjection(dbExpressionBinding3.Project(dbExpression9));
				}

				// Token: 0x06006DCF RID: 28111 RVA: 0x001769F0 File Offset: 0x00174BF0
				private static DbExpression CollapseTrivialRenamingProjection(DbExpression cqtExpression)
				{
					if (cqtExpression.ExpressionKind != DbExpressionKind.Project)
					{
						return cqtExpression;
					}
					DbProjectExpression dbProjectExpression = (DbProjectExpression)cqtExpression;
					if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.NewInstance || dbProjectExpression.Projection.ResultType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
					{
						return cqtExpression;
					}
					DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression.Projection;
					RowType rowType = (RowType)dbNewInstanceExpression.ResultType.EdmType;
					List<Tuple<EdmProperty, string>> list = new List<Tuple<EdmProperty, string>>();
					for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
					{
						if (dbNewInstanceExpression.Arguments[i].ExpressionKind != DbExpressionKind.Property)
						{
							return cqtExpression;
						}
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbNewInstanceExpression.Arguments[i];
						if (dbPropertyExpression.Instance != dbProjectExpression.Input.Variable)
						{
							return cqtExpression;
						}
						list.Add(Tuple.Create<EdmProperty, string>((EdmProperty)dbPropertyExpression.Property, rowType.Properties[i].Name));
					}
					if (dbProjectExpression.Input.Expression.ExpressionKind != DbExpressionKind.Project)
					{
						return cqtExpression;
					}
					DbProjectExpression dbProjectExpression2 = (DbProjectExpression)dbProjectExpression.Input.Expression;
					if (dbProjectExpression2.Projection.ExpressionKind != DbExpressionKind.NewInstance || dbProjectExpression2.Projection.ResultType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
					{
						return cqtExpression;
					}
					DbNewInstanceExpression dbNewInstanceExpression2 = (DbNewInstanceExpression)dbProjectExpression2.Projection;
					RowType rowType2 = (RowType)dbNewInstanceExpression2.ResultType.EdmType;
					List<DbExpression> list2 = new List<DbExpression>();
					foreach (Tuple<EdmProperty, string> tuple in list)
					{
						int num = rowType2.Properties.IndexOf(tuple.Item1);
						list2.Add(dbNewInstanceExpression2.Arguments[num]);
					}
					DbNewInstanceExpression dbNewInstanceExpression3 = dbNewInstanceExpression.ResultType.New(list2);
					return dbProjectExpression2.Input.Project(dbNewInstanceExpression3);
				}
			}

			// Token: 0x02000D3B RID: 3387
			private abstract class OrderByTranslatorBase : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06006DD0 RID: 28112 RVA: 0x00176BE4 File Offset: 0x00174DE4
				protected OrderByTranslatorBase(bool ascending, params SequenceMethod[] methods)
					: base(methods)
				{
					this._ascending = ascending;
				}

				// Token: 0x06006DD1 RID: 28113 RVA: 0x00176BF4 File Offset: 0x00174DF4
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					List<DbSortClause> list = new List<DbSortClause>(1);
					DbSortClause dbSortClause = (this._ascending ? lambda.ToSortClause() : lambda.ToSortClauseDescending());
					list.Add(dbSortClause);
					return parent.Sort(sourceBinding, list);
				}

				// Token: 0x04003283 RID: 12931
				private readonly bool _ascending;
			}

			// Token: 0x02000D3C RID: 3388
			private sealed class OrderByTranslator : ExpressionConverter.MethodCallTranslator.OrderByTranslatorBase
			{
				// Token: 0x06006DD2 RID: 28114 RVA: 0x00176C2E File Offset: 0x00174E2E
				internal OrderByTranslator()
					: base(true, new SequenceMethod[] { SequenceMethod.OrderBy })
				{
				}
			}

			// Token: 0x02000D3D RID: 3389
			private sealed class OrderByDescendingTranslator : ExpressionConverter.MethodCallTranslator.OrderByTranslatorBase
			{
				// Token: 0x06006DD3 RID: 28115 RVA: 0x00176C42 File Offset: 0x00174E42
				internal OrderByDescendingTranslator()
					: base(false, new SequenceMethod[] { SequenceMethod.OrderByDescending })
				{
				}
			}

			// Token: 0x02000D3E RID: 3390
			private abstract class ThenByTranslatorBase : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06006DD4 RID: 28116 RVA: 0x00176C56 File Offset: 0x00174E56
				protected ThenByTranslatorBase(bool ascending, params SequenceMethod[] methods)
					: base(methods)
				{
					this._ascending = ascending;
				}

				// Token: 0x06006DD5 RID: 28117 RVA: 0x00176C68 File Offset: 0x00174E68
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					if (DbExpressionKind.Sort != dbExpression.ExpressionKind)
					{
						throw new InvalidOperationException(Strings.ELinq_ThenByDoesNotFollowOrderBy);
					}
					DbSortExpression dbSortExpression = (DbSortExpression)dbExpression;
					DbExpressionBinding input = dbSortExpression.Input;
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					ParameterExpression parameterExpression = lambdaExpression.Parameters[0];
					parent._bindingContext.PushBindingScope(new Binding(parameterExpression, input.Variable));
					DbExpression dbExpression2 = parent.TranslateExpression(lambdaExpression.Body);
					parent._bindingContext.PopBindingScope();
					return parent.Sort(input, new List<DbSortClause>(dbSortExpression.SortOrder)
					{
						new DbSortClause(dbExpression2, this._ascending, null)
					});
				}

				// Token: 0x04003284 RID: 12932
				private readonly bool _ascending;
			}

			// Token: 0x02000D3F RID: 3391
			private sealed class ThenByTranslator : ExpressionConverter.MethodCallTranslator.ThenByTranslatorBase
			{
				// Token: 0x06006DD6 RID: 28118 RVA: 0x00176D1A File Offset: 0x00174F1A
				internal ThenByTranslator()
					: base(true, new SequenceMethod[] { SequenceMethod.ThenBy })
				{
				}
			}

			// Token: 0x02000D40 RID: 3392
			private sealed class ThenByDescendingTranslator : ExpressionConverter.MethodCallTranslator.ThenByTranslatorBase
			{
				// Token: 0x06006DD7 RID: 28119 RVA: 0x00176D2E File Offset: 0x00174F2E
				internal ThenByDescendingTranslator()
					: base(false, new SequenceMethod[] { SequenceMethod.ThenByDescending })
				{
				}
			}

			// Token: 0x02000D41 RID: 3393
			private sealed class SpatialMethodCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06006DD8 RID: 28120 RVA: 0x00176D42 File Offset: 0x00174F42
				internal SpatialMethodCallTranslator()
					: base(ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetSupportedMethods())
				{
				}

				// Token: 0x06006DD9 RID: 28121 RVA: 0x00176D4F File Offset: 0x00174F4F
				private static MethodInfo GetStaticMethod<TResult>(Expression<Func<TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x06006DDA RID: 28122 RVA: 0x00176D61 File Offset: 0x00174F61
				private static MethodInfo GetInstanceMethod<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x06006DDB RID: 28123 RVA: 0x00176D73 File Offset: 0x00174F73
				private static IEnumerable<MethodInfo> GetSupportedMethods()
				{
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, byte[]>((DbGeography geo) => geo.AsBinary());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsGml());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsText());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.SpatialEquals(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Disjoint(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Intersects(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Buffer((double?)0.0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, double?>((DbGeography geo) => geo.Distance(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Intersection(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Union(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Difference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.SymmetricDifference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.ElementAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.PointAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, byte[]>((DbGeometry geo) => geo.AsBinary());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsGml());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsText());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.SpatialEquals(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Disjoint(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Intersects(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Touches(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Crosses(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Within(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Contains(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Overlaps(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Relate(null, null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Buffer((double?)0.0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, double?>((DbGeometry geo) => geo.Distance(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Intersection(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Union(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Difference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.SymmetricDifference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ElementAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.InteriorRingAt(0));
					yield break;
				}

				// Token: 0x06006DDC RID: 28124 RVA: 0x00176D7C File Offset: 0x00174F7C
				private static Dictionary<MethodInfo, string> GetRenamedMethodFunctions()
				{
					return new Dictionary<MethodInfo, string>
					{
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null)),
							"GeographyFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null, 0)),
							"GeographyFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromText(null, 0)),
							"GeographyPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromText(null, 0)),
							"GeographyLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromText(null, 0)),
							"GeographyPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromText(null, 0)),
							"GeographyMultiPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromText(null, 0)),
							"GeographyMultiLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromText(null, 0)),
							"GeographyMultiPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromText(null, 0)),
							"GeographyCollectionFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null, 0)),
							"GeographyFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null)),
							"GeographyFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromBinary(null, 0)),
							"GeographyPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromBinary(null, 0)),
							"GeographyLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromBinary(null, 0)),
							"GeographyPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromBinary(null, 0)),
							"GeographyMultiPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromBinary(null, 0)),
							"GeographyMultiLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromBinary(null, 0)),
							"GeographyMultiPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromBinary(null, 0)),
							"GeographyCollectionFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null)),
							"GeographyFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null, 0)),
							"GeographyFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, byte[]>((DbGeography geo) => geo.AsBinary()),
							"AsBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsGml()),
							"AsGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsText()),
							"AsText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.SpatialEquals(null)),
							"SpatialEquals"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Disjoint(null)),
							"SpatialDisjoint"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Intersects(null)),
							"SpatialIntersects"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Buffer((double?)0.0)),
							"SpatialBuffer"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, double?>((DbGeography geo) => geo.Distance(null)),
							"Distance"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Intersection(null)),
							"SpatialIntersection"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Union(null)),
							"SpatialUnion"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Difference(null)),
							"SpatialDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.SymmetricDifference(null)),
							"SpatialSymmetricDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.ElementAt(0)),
							"SpatialElementAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.PointAt(0)),
							"PointAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null)),
							"GeometryFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null, 0)),
							"GeometryFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromText(null, 0)),
							"GeometryPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromText(null, 0)),
							"GeometryLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromText(null, 0)),
							"GeometryPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromText(null, 0)),
							"GeometryMultiPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromText(null, 0)),
							"GeometryMultiLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromText(null, 0)),
							"GeometryMultiPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromText(null, 0)),
							"GeometryCollectionFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null)),
							"GeometryFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null, 0)),
							"GeometryFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromBinary(null, 0)),
							"GeometryPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromBinary(null, 0)),
							"GeometryLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromBinary(null, 0)),
							"GeometryPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromBinary(null, 0)),
							"GeometryMultiPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromBinary(null, 0)),
							"GeometryMultiLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromBinary(null, 0)),
							"GeometryMultiPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromBinary(null, 0)),
							"GeometryCollectionFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null)),
							"GeometryFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null, 0)),
							"GeometryFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, byte[]>((DbGeometry geo) => geo.AsBinary()),
							"AsBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsGml()),
							"AsGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsText()),
							"AsText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.SpatialEquals(null)),
							"SpatialEquals"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Disjoint(null)),
							"SpatialDisjoint"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Intersects(null)),
							"SpatialIntersects"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Touches(null)),
							"SpatialTouches"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Crosses(null)),
							"SpatialCrosses"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Within(null)),
							"SpatialWithin"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Contains(null)),
							"SpatialContains"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Overlaps(null)),
							"SpatialOverlaps"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Relate(null, null)),
							"SpatialRelate"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Buffer((double?)0.0)),
							"SpatialBuffer"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, double?>((DbGeometry geo) => geo.Distance(null)),
							"Distance"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Intersection(null)),
							"SpatialIntersection"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Union(null)),
							"SpatialUnion"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Difference(null)),
							"SpatialDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.SymmetricDifference(null)),
							"SpatialSymmetricDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ElementAt(0)),
							"SpatialElementAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointAt(0)),
							"PointAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.InteriorRingAt(0)),
							"InteriorRingAt"
						}
					};
				}

				// Token: 0x06006DDD RID: 28125 RVA: 0x00178980 File Offset: 0x00176B80
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo method = call.Method;
					string text;
					if (!ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator._methodFunctionRenames.TryGetValue(method, out text))
					{
						text = "ST" + method.Name;
					}
					Expression[] array;
					if (method.IsStatic)
					{
						array = call.Arguments.ToArray<Expression>();
					}
					else
					{
						array = new Expression[] { call.Object }.Concat(call.Arguments).ToArray<Expression>();
					}
					return parent.TranslateIntoCanonicalFunction(text, call, array);
				}

				// Token: 0x04003285 RID: 12933
				private static readonly Dictionary<MethodInfo, string> _methodFunctionRenames = ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetRenamedMethodFunctions();
			}
		}

		// Token: 0x02000A73 RID: 2675
		private sealed class OrderByLifter
		{
			// Token: 0x060061E6 RID: 25062 RVA: 0x00153CC5 File Offset: 0x00151EC5
			internal OrderByLifter(AliasGenerator aliasGenerator)
			{
				this._aliasGenerator = aliasGenerator;
			}

			// Token: 0x060061E7 RID: 25063 RVA: 0x00153CD4 File Offset: 0x00151ED4
			internal DbExpression Project(DbExpressionBinding input, DbExpression projection)
			{
				return this.GetLifter(input.Expression).Project(input.Project(projection));
			}

			// Token: 0x060061E8 RID: 25064 RVA: 0x00153CEE File Offset: 0x00151EEE
			internal DbExpression Filter(DbExpressionBinding input, DbExpression predicate)
			{
				return this.GetLifter(input.Expression).Filter(input.Filter(predicate));
			}

			// Token: 0x060061E9 RID: 25065 RVA: 0x00153D08 File Offset: 0x00151F08
			internal DbExpression OfType(DbExpression argument, TypeUsage type)
			{
				return this.GetLifter(argument).OfType(type);
			}

			// Token: 0x060061EA RID: 25066 RVA: 0x00153D17 File Offset: 0x00151F17
			internal DbExpression Skip(DbExpressionBinding input, DbExpression skipCount)
			{
				return this.GetLifter(input.Expression).Skip(skipCount);
			}

			// Token: 0x060061EB RID: 25067 RVA: 0x00153D2B File Offset: 0x00151F2B
			internal DbExpression Limit(DbExpression argument, DbExpression limit)
			{
				return this.GetLifter(argument).Limit(limit);
			}

			// Token: 0x060061EC RID: 25068 RVA: 0x00153D3A File Offset: 0x00151F3A
			private ExpressionConverter.OrderByLifter.OrderByLifterBase GetLifter(DbExpression root)
			{
				return ExpressionConverter.OrderByLifter.OrderByLifterBase.GetLifter(root, this._aliasGenerator);
			}

			// Token: 0x04002B25 RID: 11045
			private readonly AliasGenerator _aliasGenerator;

			// Token: 0x02000D45 RID: 3397
			private abstract class OrderByLifterBase
			{
				// Token: 0x06006DF7 RID: 28151 RVA: 0x0017925F File Offset: 0x0017745F
				protected OrderByLifterBase(DbExpression root, AliasGenerator aliasGenerator)
				{
					this._root = root;
					this._aliasGenerator = aliasGenerator;
				}

				// Token: 0x06006DF8 RID: 28152 RVA: 0x00179278 File Offset: 0x00177478
				internal static ExpressionConverter.OrderByLifter.OrderByLifterBase GetLifter(DbExpression source, AliasGenerator aliasGenerator)
				{
					if (source.ExpressionKind == DbExpressionKind.Sort)
					{
						return new ExpressionConverter.OrderByLifter.SortLifter((DbSortExpression)source, aliasGenerator);
					}
					if (source.ExpressionKind == DbExpressionKind.Project)
					{
						DbProjectExpression dbProjectExpression = (DbProjectExpression)source;
						DbExpression expression = dbProjectExpression.Input.Expression;
						if (expression.ExpressionKind == DbExpressionKind.Sort)
						{
							return new ExpressionConverter.OrderByLifter.ProjectSortLifter(dbProjectExpression, (DbSortExpression)expression, aliasGenerator);
						}
						if (expression.ExpressionKind == DbExpressionKind.Skip)
						{
							return new ExpressionConverter.OrderByLifter.ProjectSkipLifter(dbProjectExpression, (DbSkipExpression)expression, aliasGenerator);
						}
						if (expression.ExpressionKind == DbExpressionKind.Limit)
						{
							DbLimitExpression dbLimitExpression = (DbLimitExpression)expression;
							DbExpression argument = dbLimitExpression.Argument;
							if (argument.ExpressionKind == DbExpressionKind.Sort)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSortLifter(dbProjectExpression, dbLimitExpression, (DbSortExpression)argument, aliasGenerator);
							}
							if (argument.ExpressionKind == DbExpressionKind.Skip)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSkipLifter(dbProjectExpression, dbLimitExpression, (DbSkipExpression)argument, aliasGenerator);
							}
						}
					}
					if (source.ExpressionKind == DbExpressionKind.Skip)
					{
						return new ExpressionConverter.OrderByLifter.SkipLifter((DbSkipExpression)source, aliasGenerator);
					}
					if (source.ExpressionKind == DbExpressionKind.Limit)
					{
						DbLimitExpression dbLimitExpression2 = (DbLimitExpression)source;
						DbExpression argument2 = dbLimitExpression2.Argument;
						if (argument2.ExpressionKind == DbExpressionKind.Sort)
						{
							return new ExpressionConverter.OrderByLifter.LimitSortLifter(dbLimitExpression2, (DbSortExpression)argument2, aliasGenerator);
						}
						if (argument2.ExpressionKind == DbExpressionKind.Skip)
						{
							return new ExpressionConverter.OrderByLifter.LimitSkipLifter(dbLimitExpression2, (DbSkipExpression)argument2, aliasGenerator);
						}
						if (argument2.ExpressionKind == DbExpressionKind.Project)
						{
							DbProjectExpression dbProjectExpression2 = (DbProjectExpression)argument2;
							DbExpression expression2 = dbProjectExpression2.Input.Expression;
							if (expression2.ExpressionKind == DbExpressionKind.Sort)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSortLifter(dbProjectExpression2, dbLimitExpression2, (DbSortExpression)expression2, aliasGenerator);
							}
							if (expression2.ExpressionKind == DbExpressionKind.Skip)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSkipLifter(dbProjectExpression2, dbLimitExpression2, (DbSkipExpression)expression2, aliasGenerator);
							}
						}
					}
					return new ExpressionConverter.OrderByLifter.PassthroughOrderByLifter(source, aliasGenerator);
				}

				// Token: 0x06006DF9 RID: 28153
				internal abstract DbExpression Project(DbProjectExpression project);

				// Token: 0x06006DFA RID: 28154
				internal abstract DbExpression Filter(DbFilterExpression filter);

				// Token: 0x06006DFB RID: 28155 RVA: 0x00179404 File Offset: 0x00177604
				internal virtual DbExpression OfType(TypeUsage type)
				{
					DbExpressionBinding dbExpressionBinding = this._root.BindAs(this._aliasGenerator.Next());
					DbExpression dbExpression = this.Filter(dbExpressionBinding.Filter(dbExpressionBinding.Variable.IsOf(type)));
					ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = ExpressionConverter.OrderByLifter.OrderByLifterBase.GetLifter(dbExpression, this._aliasGenerator);
					DbExpressionBinding dbExpressionBinding2 = dbExpression.BindAs(this._aliasGenerator.Next());
					return lifter.Project(dbExpressionBinding2.Project(dbExpressionBinding2.Variable.TreatAs(type)));
				}

				// Token: 0x06006DFC RID: 28156
				internal abstract DbExpression Limit(DbExpression k);

				// Token: 0x06006DFD RID: 28157
				internal abstract DbExpression Skip(DbExpression k);

				// Token: 0x06006DFE RID: 28158 RVA: 0x00179478 File Offset: 0x00177678
				protected static DbProjectExpression ComposeProject(DbExpression input, DbProjectExpression first, DbProjectExpression second)
				{
					DbLambda dbLambda = DbExpressionBuilder.Lambda(second.Projection, new DbVariableReferenceExpression[] { second.Input.Variable });
					DbProjectExpression dbProjectExpression = first.Input.Project(dbLambda.Invoke(new DbExpression[] { first.Projection }));
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(input, dbProjectExpression);
				}

				// Token: 0x06006DFF RID: 28159 RVA: 0x001794D0 File Offset: 0x001776D0
				protected static DbFilterExpression ComposeFilter(DbExpression input, DbProjectExpression first, DbFilterExpression second)
				{
					DbLambda dbLambda = DbExpressionBuilder.Lambda(second.Predicate, new DbVariableReferenceExpression[] { second.Input.Variable });
					DbFilterExpression dbFilterExpression = first.Input.Filter(dbLambda.Invoke(new DbExpression[] { first.Projection }));
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindFilter(input, dbFilterExpression);
				}

				// Token: 0x06006E00 RID: 28160 RVA: 0x00179528 File Offset: 0x00177728
				protected static DbSkipExpression AddToSkip(DbExpression input, DbSkipExpression skip, DbExpression plusK)
				{
					DbExpression dbExpression = ExpressionConverter.OrderByLifter.OrderByLifterBase.CombineIntegers(skip.Count, plusK, (int l, int r) => l + r);
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(input, skip, dbExpression);
				}

				// Token: 0x06006E01 RID: 28161 RVA: 0x0017956C File Offset: 0x0017776C
				protected static DbLimitExpression SubtractFromLimit(DbExpression input, DbLimitExpression limit, DbExpression minusK)
				{
					DbExpression dbExpression = ExpressionConverter.OrderByLifter.OrderByLifterBase.CombineIntegers(limit.Limit, minusK, delegate(int l, int r)
					{
						if (r <= l)
						{
							return l - r;
						}
						return 0;
					});
					return input.Limit(dbExpression);
				}

				// Token: 0x06006E02 RID: 28162 RVA: 0x001795AC File Offset: 0x001777AC
				protected static DbLimitExpression MinimumLimit(DbExpression input, DbLimitExpression limit, DbExpression k)
				{
					DbExpression dbExpression = ExpressionConverter.OrderByLifter.OrderByLifterBase.CombineIntegers(limit.Limit, k, new Func<int, int, int>(Math.Min));
					return input.Limit(dbExpression);
				}

				// Token: 0x06006E03 RID: 28163 RVA: 0x001795DC File Offset: 0x001777DC
				private static DbExpression CombineIntegers(DbExpression left, DbExpression right, Func<int, int, int> combineConstants)
				{
					if (left.ExpressionKind == DbExpressionKind.Constant && right.ExpressionKind == DbExpressionKind.Constant)
					{
						object value = ((DbConstantExpression)left).Value;
						object value2 = ((DbConstantExpression)right).Value;
						if (value is int && value2 is int)
						{
							return left.ResultType.Constant(combineConstants((int)value, (int)value2));
						}
					}
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1025));
				}

				// Token: 0x06006E04 RID: 28164 RVA: 0x0017965A File Offset: 0x0017785A
				protected static DbProjectExpression RebindProject(DbExpression input, DbProjectExpression project)
				{
					return input.BindAs(project.Input.VariableName).Project(project.Projection);
				}

				// Token: 0x06006E05 RID: 28165 RVA: 0x00179678 File Offset: 0x00177878
				protected static DbFilterExpression RebindFilter(DbExpression input, DbFilterExpression filter)
				{
					return input.BindAs(filter.Input.VariableName).Filter(filter.Predicate);
				}

				// Token: 0x06006E06 RID: 28166 RVA: 0x00179696 File Offset: 0x00177896
				protected static DbSortExpression RebindSort(DbExpression input, DbSortExpression sort)
				{
					return input.BindAs(sort.Input.VariableName).Sort(sort.SortOrder);
				}

				// Token: 0x06006E07 RID: 28167 RVA: 0x001796B4 File Offset: 0x001778B4
				protected static DbSortExpression ApplySkipOrderToSort(DbExpression input, DbSkipExpression sortSpec)
				{
					return input.BindAs(sortSpec.Input.VariableName).Sort(sortSpec.SortOrder);
				}

				// Token: 0x06006E08 RID: 28168 RVA: 0x001796D2 File Offset: 0x001778D2
				protected static DbSkipExpression ApplySortOrderToSkip(DbExpression input, DbSortExpression sort, DbExpression k)
				{
					return input.BindAs(sort.Input.VariableName).Skip(sort.SortOrder, k);
				}

				// Token: 0x06006E09 RID: 28169 RVA: 0x001796F1 File Offset: 0x001778F1
				protected static DbSkipExpression RebindSkip(DbExpression input, DbSkipExpression skip, DbExpression k)
				{
					return input.BindAs(skip.Input.VariableName).Skip(skip.SortOrder, k);
				}

				// Token: 0x04003291 RID: 12945
				protected readonly DbExpression _root;

				// Token: 0x04003292 RID: 12946
				protected readonly AliasGenerator _aliasGenerator;
			}

			// Token: 0x02000D46 RID: 3398
			private class LimitSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E0A RID: 28170 RVA: 0x00179710 File Offset: 0x00177910
				internal LimitSkipLifter(DbLimitExpression limit, DbSkipExpression skip, AliasGenerator aliasGenerator)
					: base(limit, aliasGenerator)
				{
					this._limit = limit;
					this._skip = skip;
				}

				// Token: 0x06006E0B RID: 28171 RVA: 0x00179728 File Offset: 0x00177928
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(filter, this._skip);
				}

				// Token: 0x06006E0C RID: 28172 RVA: 0x00179736 File Offset: 0x00177936
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06006E0D RID: 28173 RVA: 0x0017973C File Offset: 0x0017793C
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._skip, this._limit, k);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(this._limit, this._skip).Limit(k);
				}

				// Token: 0x06006E0E RID: 28174 RVA: 0x0017978F File Offset: 0x0017798F
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._limit, this._skip, k);
				}

				// Token: 0x04003293 RID: 12947
				private readonly DbLimitExpression _limit;

				// Token: 0x04003294 RID: 12948
				private readonly DbSkipExpression _skip;
			}

			// Token: 0x02000D47 RID: 3399
			private class LimitSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E0F RID: 28175 RVA: 0x001797A3 File Offset: 0x001779A3
				internal LimitSortLifter(DbLimitExpression limit, DbSortExpression sort, AliasGenerator aliasGenerator)
					: base(limit, aliasGenerator)
				{
					this._limit = limit;
					this._sort = sort;
				}

				// Token: 0x06006E10 RID: 28176 RVA: 0x001797BB File Offset: 0x001779BB
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(filter, this._sort);
				}

				// Token: 0x06006E11 RID: 28177 RVA: 0x001797C9 File Offset: 0x001779C9
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06006E12 RID: 28178 RVA: 0x001797CC File Offset: 0x001779CC
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._sort, this._limit, k);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(this._limit, this._sort).Limit(k);
				}

				// Token: 0x06006E13 RID: 28179 RVA: 0x0017981F File Offset: 0x00177A1F
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._limit, this._sort, k);
				}

				// Token: 0x04003295 RID: 12949
				private readonly DbLimitExpression _limit;

				// Token: 0x04003296 RID: 12950
				private readonly DbSortExpression _sort;
			}

			// Token: 0x02000D48 RID: 3400
			private class ProjectLimitSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E14 RID: 28180 RVA: 0x00179833 File Offset: 0x00177A33
				internal ProjectLimitSkipLifter(DbProjectExpression project, DbLimitExpression limit, DbSkipExpression skip, AliasGenerator aliasGenerator)
					: base(project, aliasGenerator)
				{
					this._project = project;
					this._limit = limit;
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x06006E15 RID: 28181 RVA: 0x00179864 File Offset: 0x00177A64
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._skip.Limit(this._limit.Limit), this._project, filter), this._skip), this._project);
				}

				// Token: 0x06006E16 RID: 28182 RVA: 0x0017989E File Offset: 0x00177A9E
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._skip.Limit(this._limit.Limit), this._project, project);
				}

				// Token: 0x06006E17 RID: 28183 RVA: 0x001798C4 File Offset: 0x00177AC4
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._skip, this._limit, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(this._skip.Limit(this._limit.Limit), this._skip).Limit(k), this._project);
				}

				// Token: 0x06006E18 RID: 28184 RVA: 0x00179940 File Offset: 0x00177B40
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.SubtractFromLimit(ExpressionConverter.OrderByLifter.OrderByLifterBase.AddToSkip(this._source, this._skip, k), this._limit, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._skip.Limit(this._limit.Limit), this._skip, k), this._project);
				}

				// Token: 0x04003297 RID: 12951
				private readonly DbProjectExpression _project;

				// Token: 0x04003298 RID: 12952
				private readonly DbLimitExpression _limit;

				// Token: 0x04003299 RID: 12953
				private readonly DbSkipExpression _skip;

				// Token: 0x0400329A RID: 12954
				private readonly DbExpression _source;
			}

			// Token: 0x02000D49 RID: 3401
			private class ProjectLimitSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E19 RID: 28185 RVA: 0x001799D3 File Offset: 0x00177BD3
				internal ProjectLimitSortLifter(DbProjectExpression project, DbLimitExpression limit, DbSortExpression sort, AliasGenerator aliasGenerator)
					: base(project, aliasGenerator)
				{
					this._project = project;
					this._limit = limit;
					this._sort = sort;
				}

				// Token: 0x06006E1A RID: 28186 RVA: 0x001799F3 File Offset: 0x00177BF3
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._sort.Limit(this._limit.Limit), this._project, filter), this._sort), this._project);
				}

				// Token: 0x06006E1B RID: 28187 RVA: 0x00179A2D File Offset: 0x00177C2D
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._sort.Limit(this._limit.Limit), this._project, project);
				}

				// Token: 0x06006E1C RID: 28188 RVA: 0x00179A54 File Offset: 0x00177C54
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._sort, this._limit, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(this._sort.Limit(this._limit.Limit), this._sort).Limit(k), this._project);
				}

				// Token: 0x06006E1D RID: 28189 RVA: 0x00179ACD File Offset: 0x00177CCD
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._sort.Limit(this._limit.Limit), this._sort, k), this._project);
				}

				// Token: 0x0400329B RID: 12955
				private readonly DbProjectExpression _project;

				// Token: 0x0400329C RID: 12956
				private readonly DbLimitExpression _limit;

				// Token: 0x0400329D RID: 12957
				private readonly DbSortExpression _sort;
			}

			// Token: 0x02000D4A RID: 3402
			private class ProjectSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E1E RID: 28190 RVA: 0x00179AFC File Offset: 0x00177CFC
				internal ProjectSkipLifter(DbProjectExpression project, DbSkipExpression skip, AliasGenerator aliasGenerator)
					: base(project, aliasGenerator)
				{
					this._project = project;
					this._skip = skip;
					this._source = this._skip.Input.Expression;
				}

				// Token: 0x06006E1F RID: 28191 RVA: 0x00179B2A File Offset: 0x00177D2A
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._skip, this._project, filter), this._skip), this._project);
				}

				// Token: 0x06006E20 RID: 28192 RVA: 0x00179B54 File Offset: 0x00177D54
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06006E21 RID: 28193 RVA: 0x00179B62 File Offset: 0x00177D62
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._skip, this._project, project);
				}

				// Token: 0x06006E22 RID: 28194 RVA: 0x00179B78 File Offset: 0x00177D78
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.AddToSkip(this._source, this._skip, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._skip, this._skip, k), this._project);
				}

				// Token: 0x0400329E RID: 12958
				private readonly DbProjectExpression _project;

				// Token: 0x0400329F RID: 12959
				private readonly DbSkipExpression _skip;

				// Token: 0x040032A0 RID: 12960
				private readonly DbExpression _source;
			}

			// Token: 0x02000D4B RID: 3403
			private class SkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E23 RID: 28195 RVA: 0x00179BDC File Offset: 0x00177DDC
				internal SkipLifter(DbSkipExpression skip, AliasGenerator aliasGenerator)
					: base(skip, aliasGenerator)
				{
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x06006E24 RID: 28196 RVA: 0x00179BFE File Offset: 0x00177DFE
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(filter, this._skip);
				}

				// Token: 0x06006E25 RID: 28197 RVA: 0x00179C0C File Offset: 0x00177E0C
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06006E26 RID: 28198 RVA: 0x00179C0F File Offset: 0x00177E0F
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06006E27 RID: 28199 RVA: 0x00179C20 File Offset: 0x00177E20
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.AddToSkip(this._source, this._skip, k);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._skip, this._skip, k);
				}

				// Token: 0x040032A1 RID: 12961
				private readonly DbSkipExpression _skip;

				// Token: 0x040032A2 RID: 12962
				private readonly DbExpression _source;
			}

			// Token: 0x02000D4C RID: 3404
			private class ProjectSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E28 RID: 28200 RVA: 0x00179C6E File Offset: 0x00177E6E
				internal ProjectSortLifter(DbProjectExpression project, DbSortExpression sort, AliasGenerator aliasGenerator)
					: base(project, aliasGenerator)
				{
					this._project = project;
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x06006E29 RID: 28201 RVA: 0x00179C97 File Offset: 0x00177E97
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._sort, this._project, project);
				}

				// Token: 0x06006E2A RID: 28202 RVA: 0x00179CAB File Offset: 0x00177EAB
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._source, this._project, filter), this._sort), this._project);
				}

				// Token: 0x06006E2B RID: 28203 RVA: 0x00179CD5 File Offset: 0x00177ED5
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06006E2C RID: 28204 RVA: 0x00179CE3 File Offset: 0x00177EE3
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._source, this._sort, k), this._project);
				}

				// Token: 0x040032A3 RID: 12963
				private readonly DbProjectExpression _project;

				// Token: 0x040032A4 RID: 12964
				private readonly DbSortExpression _sort;

				// Token: 0x040032A5 RID: 12965
				private readonly DbExpression _source;
			}

			// Token: 0x02000D4D RID: 3405
			private class SortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E2D RID: 28205 RVA: 0x00179D02 File Offset: 0x00177F02
				internal SortLifter(DbSortExpression sort, AliasGenerator aliasGenerator)
					: base(sort, aliasGenerator)
				{
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x06006E2E RID: 28206 RVA: 0x00179D24 File Offset: 0x00177F24
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06006E2F RID: 28207 RVA: 0x00179D27 File Offset: 0x00177F27
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindFilter(this._source, filter), this._sort);
				}

				// Token: 0x06006E30 RID: 28208 RVA: 0x00179D40 File Offset: 0x00177F40
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06006E31 RID: 28209 RVA: 0x00179D4E File Offset: 0x00177F4E
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._source, this._sort, k);
				}

				// Token: 0x040032A6 RID: 12966
				private readonly DbSortExpression _sort;

				// Token: 0x040032A7 RID: 12967
				private readonly DbExpression _source;
			}

			// Token: 0x02000D4E RID: 3406
			private class PassthroughOrderByLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06006E32 RID: 28210 RVA: 0x00179D62 File Offset: 0x00177F62
				internal PassthroughOrderByLifter(DbExpression source, AliasGenerator aliasGenerator)
					: base(source, aliasGenerator)
				{
				}

				// Token: 0x06006E33 RID: 28211 RVA: 0x00179D6C File Offset: 0x00177F6C
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06006E34 RID: 28212 RVA: 0x00179D6F File Offset: 0x00177F6F
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return filter;
				}

				// Token: 0x06006E35 RID: 28213 RVA: 0x00179D72 File Offset: 0x00177F72
				internal override DbExpression OfType(TypeUsage type)
				{
					return this._root.OfType(type);
				}

				// Token: 0x06006E36 RID: 28214 RVA: 0x00179D80 File Offset: 0x00177F80
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06006E37 RID: 28215 RVA: 0x00179D8E File Offset: 0x00177F8E
				internal override DbExpression Skip(DbExpression k)
				{
					throw new NotSupportedException(Strings.ELinq_SkipWithoutOrder);
				}
			}
		}

		// Token: 0x02000A74 RID: 2676
		internal sealed class MemberAccessTranslator : ExpressionConverter.TypedTranslator<MemberExpression>
		{
			// Token: 0x060061ED RID: 25069 RVA: 0x00153D48 File Offset: 0x00151F48
			internal MemberAccessTranslator()
				: base(new ExpressionType[] { ExpressionType.MemberAccess })
			{
			}

			// Token: 0x060061EE RID: 25070 RVA: 0x00153D5C File Offset: 0x00151F5C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MemberExpression linq)
			{
				string text;
				Type type;
				MemberInfo memberInfo = TypeSystem.PropertyOrField(linq.Member, out text, out type);
				if (linq.Expression != null)
				{
					if (ExpressionType.Constant == linq.Expression.NodeType && ((ConstantExpression)linq.Expression).Type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).FirstOrDefault<object>() != null)
					{
						Delegate @delegate = Expression.Lambda(linq, new ParameterExpression[0]).Compile();
						return parent.TranslateExpression(Expression.Constant(@delegate.DynamicInvoke(new object[0])));
					}
					DbExpression dbExpression = parent.TranslateExpression(linq.Expression);
					DbExpression dbExpression2;
					if (ExpressionConverter.MemberAccessTranslator.TryResolveAsProperty(parent, memberInfo, dbExpression.ResultType, dbExpression, out dbExpression2))
					{
						return dbExpression2;
					}
				}
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator;
				if (memberInfo.MemberType == MemberTypes.Property && ExpressionConverter.MemberAccessTranslator.TryGetTranslator((PropertyInfo)memberInfo, out propertyTranslator))
				{
					return propertyTranslator.Translate(parent, linq);
				}
				throw new NotSupportedException(Strings.ELinq_UnrecognizedMember(linq.Member.Name));
			}

			// Token: 0x060061EF RID: 25071 RVA: 0x00153E44 File Offset: 0x00152044
			static MemberAccessTranslator()
			{
				foreach (ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator in ExpressionConverter.MemberAccessTranslator.GetPropertyTranslators())
				{
					foreach (PropertyInfo propertyInfo in propertyTranslator.Properties)
					{
						ExpressionConverter.MemberAccessTranslator._propertyTranslators.Add(propertyInfo, propertyTranslator);
					}
				}
			}

			// Token: 0x060061F0 RID: 25072 RVA: 0x00153EE0 File Offset: 0x001520E0
			private static bool TryGetTranslator(PropertyInfo propertyInfo, out ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator)
			{
				PropertyInfo propertyInfo2 = propertyInfo;
				if (propertyInfo.DeclaringType.IsGenericType())
				{
					try
					{
						propertyInfo = propertyInfo.DeclaringType.GetGenericTypeDefinition().GetDeclaredProperty(propertyInfo.Name);
					}
					catch (AmbiguousMatchException)
					{
						propertyTranslator = null;
						return false;
					}
					if (propertyInfo == null)
					{
						propertyTranslator = null;
						return false;
					}
				}
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator2;
				if (ExpressionConverter.MemberAccessTranslator._propertyTranslators.TryGetValue(propertyInfo, out propertyTranslator2))
				{
					propertyTranslator = propertyTranslator2;
					return true;
				}
				if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == propertyInfo.DeclaringType.Assembly().FullName)
				{
					object vbInitializerLock = ExpressionConverter.MemberAccessTranslator._vbInitializerLock;
					lock (vbInitializerLock)
					{
						if (!ExpressionConverter.MemberAccessTranslator._vbPropertiesInitialized)
						{
							ExpressionConverter.MemberAccessTranslator.InitializeVBProperties(propertyInfo.DeclaringType.Assembly());
							ExpressionConverter.MemberAccessTranslator._vbPropertiesInitialized = true;
						}
						if (ExpressionConverter.MemberAccessTranslator._propertyTranslators.TryGetValue(propertyInfo, out propertyTranslator2))
						{
							propertyTranslator = propertyTranslator2;
							return true;
						}
						propertyTranslator = null;
						return false;
					}
				}
				if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.TryGetPropertyTranslator(propertyInfo2, out propertyTranslator))
				{
					return true;
				}
				propertyTranslator = null;
				return false;
			}

			// Token: 0x060061F1 RID: 25073 RVA: 0x00153FE4 File Offset: 0x001521E4
			private static bool TryResolveAsProperty(ExpressionConverter parent, MemberInfo clrMember, TypeUsage definingType, DbExpression instance, out DbExpression propertyExpression)
			{
				RowType rowType = definingType.EdmType as RowType;
				string name = clrMember.Name;
				if (rowType == null)
				{
					StructuralType structuralType = definingType.EdmType as StructuralType;
					if (structuralType != null)
					{
						EdmMember edmMember = null;
						if (parent._perspective.TryGetMember(structuralType, name, false, out edmMember) && edmMember != null)
						{
							if (edmMember.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
							{
								NavigationProperty navigationProperty = (NavigationProperty)edmMember;
								propertyExpression = ExpressionConverter.MemberAccessTranslator.TranslateNavigationProperty(parent, clrMember, instance, navigationProperty);
								return true;
							}
							propertyExpression = instance.Property(name);
							return true;
						}
					}
					if (name == "Key" && DbExpressionKind.Property == instance.ExpressionKind)
					{
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)instance;
						InitializerMetadata initializerMetadata;
						if (dbPropertyExpression.Property.Name == "Group" && InitializerMetadata.TryGetInitializerMetadata(dbPropertyExpression.Instance.ResultType, out initializerMetadata) && initializerMetadata.Kind == InitializerMetadataKind.Grouping)
						{
							propertyExpression = dbPropertyExpression.Instance.Property("Key");
							return true;
						}
					}
					propertyExpression = null;
					return false;
				}
				EdmMember edmMember2;
				if (rowType.Members.TryGetValue(name, false, out edmMember2))
				{
					propertyExpression = instance.Property(name);
					return true;
				}
				propertyExpression = null;
				return false;
			}

			// Token: 0x060061F2 RID: 25074 RVA: 0x001540F4 File Offset: 0x001522F4
			private static DbExpression TranslateNavigationProperty(ExpressionConverter parent, MemberInfo clrMember, DbExpression instance, NavigationProperty navProp)
			{
				DbExpression dbExpression = instance.Property(navProp);
				if (BuiltInTypeKind.CollectionType == dbExpression.ResultType.EdmType.BuiltInTypeKind)
				{
					Type propertyType = ((PropertyInfo)clrMember).PropertyType;
					if (propertyType.IsGenericType() && propertyType.GetGenericTypeDefinition() == typeof(EntityCollection<>))
					{
						dbExpression = ExpressionConverter.CreateNewRowExpression(new List<KeyValuePair<string, DbExpression>>(2)
						{
							new KeyValuePair<string, DbExpression>("Owner", instance),
							new KeyValuePair<string, DbExpression>("Elements", dbExpression)
						}, InitializerMetadata.CreateEntityCollectionInitializer(parent.EdmItemCollection, propertyType, navProp));
					}
				}
				return dbExpression;
			}

			// Token: 0x060061F3 RID: 25075 RVA: 0x00154184 File Offset: 0x00152384
			private static DbExpression TranslateCount(ExpressionConverter parent, Type sequenceElementType, Expression sequence)
			{
				MethodInfo methodInfo;
				ReflectionUtil.TryLookupMethod(SequenceMethod.Count, out methodInfo);
				methodInfo = methodInfo.MakeGenericMethod(new Type[] { sequenceElementType });
				Expression expression = Expression.Call(methodInfo, sequence);
				return parent.TranslateExpression(expression);
			}

			// Token: 0x060061F4 RID: 25076 RVA: 0x001541BC File Offset: 0x001523BC
			private static void InitializeVBProperties(Assembly vbAssembly)
			{
				foreach (ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator in ExpressionConverter.MemberAccessTranslator.GetVisualBasicPropertyTranslators(vbAssembly))
				{
					foreach (PropertyInfo propertyInfo in propertyTranslator.Properties)
					{
						ExpressionConverter.MemberAccessTranslator._propertyTranslators.Add(propertyInfo, propertyTranslator);
					}
				}
			}

			// Token: 0x060061F5 RID: 25077 RVA: 0x00154244 File Offset: 0x00152444
			private static IEnumerable<ExpressionConverter.MemberAccessTranslator.PropertyTranslator> GetVisualBasicPropertyTranslators(Assembly vbAssembly)
			{
				return new ExpressionConverter.MemberAccessTranslator.PropertyTranslator[]
				{
					new ExpressionConverter.MemberAccessTranslator.VBDateAndTimeNowTranslator(vbAssembly)
				};
			}

			// Token: 0x060061F6 RID: 25078 RVA: 0x00154255 File Offset: 0x00152455
			private static IEnumerable<ExpressionConverter.MemberAccessTranslator.PropertyTranslator> GetPropertyTranslators()
			{
				return new ExpressionConverter.MemberAccessTranslator.PropertyTranslator[]
				{
					new ExpressionConverter.MemberAccessTranslator.DefaultCanonicalFunctionPropertyTranslator(),
					new ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator(),
					new ExpressionConverter.MemberAccessTranslator.EntityCollectionCountTranslator(),
					new ExpressionConverter.MemberAccessTranslator.NullableHasValueTranslator(),
					new ExpressionConverter.MemberAccessTranslator.NullableValueTranslator(),
					new ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator()
				};
			}

			// Token: 0x060061F7 RID: 25079 RVA: 0x00154290 File Offset: 0x00152490
			internal static bool CanFuncletizePropertyInfo(PropertyInfo propertyInfo)
			{
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator;
				return ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.TryGetPropertyTranslator(propertyInfo, out propertyTranslator) || !ExpressionConverter.MemberAccessTranslator.TryGetTranslator(propertyInfo, out propertyTranslator);
			}

			// Token: 0x04002B26 RID: 11046
			private static readonly Dictionary<PropertyInfo, ExpressionConverter.MemberAccessTranslator.PropertyTranslator> _propertyTranslators = new Dictionary<PropertyInfo, ExpressionConverter.MemberAccessTranslator.PropertyTranslator>();

			// Token: 0x04002B27 RID: 11047
			private static bool _vbPropertiesInitialized;

			// Token: 0x04002B28 RID: 11048
			private static readonly object _vbInitializerLock = new object();

			// Token: 0x02000D4F RID: 3407
			private sealed class SpatialPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E38 RID: 28216 RVA: 0x00179D9A File Offset: 0x00177F9A
				internal SpatialPropertyTranslator()
					: base(ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetSupportedProperties())
				{
				}

				// Token: 0x06006E39 RID: 28217 RVA: 0x00179DB2 File Offset: 0x00177FB2
				private static PropertyInfo GetProperty<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					return (PropertyInfo)((MemberExpression)lambda.Body).Member;
				}

				// Token: 0x06006E3A RID: 28218 RVA: 0x00179DC9 File Offset: 0x00177FC9
				private static IEnumerable<PropertyInfo> GetSupportedProperties()
				{
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.CoordinateSystemId);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, string>((DbGeography geo) => geo.SpatialTypeName);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.Dimension);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool>((DbGeography geo) => geo.IsEmpty);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.ElementCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Latitude);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Longitude);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Elevation);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Measure);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Length);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.StartPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.EndPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool?>((DbGeography geo) => geo.IsClosed);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.PointCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Area);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.CoordinateSystemId);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, string>((DbGeometry geo) => geo.SpatialTypeName);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.Dimension);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Envelope);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsEmpty);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsSimple);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Boundary);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsValid);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ConvexHull);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.ElementCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.XCoordinate);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.YCoordinate);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Elevation);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Measure);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Length);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.StartPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.EndPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsClosed);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsRing);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.PointCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Area);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Centroid);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointOnSurface);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ExteriorRing);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.InteriorRingCount);
					yield break;
				}

				// Token: 0x06006E3B RID: 28219 RVA: 0x00179DD4 File Offset: 0x00177FD4
				private static Dictionary<PropertyInfo, string> GetRenamedPropertyFunctions()
				{
					return new Dictionary<PropertyInfo, string>
					{
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.CoordinateSystemId),
							"CoordinateSystemId"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, string>((DbGeography geo) => geo.SpatialTypeName),
							"SpatialTypeName"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.Dimension),
							"SpatialDimension"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool>((DbGeography geo) => geo.IsEmpty),
							"IsEmptySpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.ElementCount),
							"SpatialElementCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Latitude),
							"Latitude"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Longitude),
							"Longitude"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Elevation),
							"Elevation"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Measure),
							"Measure"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Length),
							"SpatialLength"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.StartPoint),
							"StartPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.EndPoint),
							"EndPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool?>((DbGeography geo) => geo.IsClosed),
							"IsClosedSpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.PointCount),
							"PointCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Area),
							"Area"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.CoordinateSystemId),
							"CoordinateSystemId"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, string>((DbGeometry geo) => geo.SpatialTypeName),
							"SpatialTypeName"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.Dimension),
							"SpatialDimension"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Envelope),
							"SpatialEnvelope"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsEmpty),
							"IsEmptySpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsSimple),
							"IsSimpleGeometry"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Boundary),
							"SpatialBoundary"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsValid),
							"IsValidGeometry"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ConvexHull),
							"SpatialConvexHull"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.ElementCount),
							"SpatialElementCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.XCoordinate),
							"XCoordinate"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.YCoordinate),
							"YCoordinate"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Elevation),
							"Elevation"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Measure),
							"Measure"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Length),
							"SpatialLength"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.StartPoint),
							"StartPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.EndPoint),
							"EndPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsClosed),
							"IsClosedSpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsRing),
							"IsRing"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.PointCount),
							"PointCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Area),
							"Area"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Centroid),
							"Centroid"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointOnSurface),
							"PointOnSurface"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ExteriorRing),
							"ExteriorRing"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.InteriorRingCount),
							"InteriorRingCount"
						}
					};
				}

				// Token: 0x06006E3C RID: 28220 RVA: 0x0017A950 File Offset: 0x00178B50
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					PropertyInfo propertyInfo = (PropertyInfo)call.Member;
					string text;
					if (!this.propertyFunctionRenames.TryGetValue(propertyInfo, out text))
					{
						text = "ST" + propertyInfo.Name;
					}
					return parent.TranslateIntoCanonicalFunction(text, call, new Expression[] { call.Expression });
				}

				// Token: 0x040032A8 RID: 12968
				private readonly Dictionary<PropertyInfo, string> propertyFunctionRenames = ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetRenamedPropertyFunctions();
			}

			// Token: 0x02000D50 RID: 3408
			private sealed class GenericICollectionTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E3D RID: 28221 RVA: 0x0017A9A1 File Offset: 0x00178BA1
				private GenericICollectionTranslator(Type elementType)
					: base(Enumerable.Empty<PropertyInfo>())
				{
					this._elementType = elementType;
				}

				// Token: 0x06006E3E RID: 28222 RVA: 0x0017A9B5 File Offset: 0x00178BB5
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.MemberAccessTranslator.TranslateCount(parent, this._elementType, call.Expression);
				}

				// Token: 0x06006E3F RID: 28223 RVA: 0x0017A9CC File Offset: 0x00178BCC
				internal static bool TryGetPropertyTranslator(PropertyInfo propertyInfo, out ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator)
				{
					if (propertyInfo.Name == "Count" && propertyInfo.PropertyType.Equals(typeof(int)))
					{
						foreach (KeyValuePair<Type, Type> keyValuePair in ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.GetImplementedICollections(propertyInfo.DeclaringType))
						{
							Type key = keyValuePair.Key;
							Type value = keyValuePair.Value;
							if (propertyInfo.IsImplementationOf(key))
							{
								propertyTranslator = new ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator(value);
								return true;
							}
						}
					}
					propertyTranslator = null;
					return false;
				}

				// Token: 0x06006E40 RID: 28224 RVA: 0x0017AA70 File Offset: 0x00178C70
				private static bool IsICollection(Type candidateType, out Type elementType)
				{
					if (candidateType.IsGenericType() && candidateType.GetGenericTypeDefinition().Equals(typeof(ICollection<>)))
					{
						elementType = candidateType.GetGenericArguments()[0];
						return true;
					}
					elementType = null;
					return false;
				}

				// Token: 0x06006E41 RID: 28225 RVA: 0x0017AAA1 File Offset: 0x00178CA1
				private static IEnumerable<KeyValuePair<Type, Type>> GetImplementedICollections(Type type)
				{
					Type type2;
					if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.IsICollection(type, out type2))
					{
						yield return new KeyValuePair<Type, Type>(type, type2);
					}
					else
					{
						foreach (Type type3 in type.GetInterfaces())
						{
							if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.IsICollection(type3, out type2))
							{
								yield return new KeyValuePair<Type, Type>(type3, type2);
							}
						}
						Type[] array = null;
					}
					yield break;
				}

				// Token: 0x040032A9 RID: 12969
				private readonly Type _elementType;
			}

			// Token: 0x02000D51 RID: 3409
			internal abstract class PropertyTranslator
			{
				// Token: 0x06006E42 RID: 28226 RVA: 0x0017AAB1 File Offset: 0x00178CB1
				protected PropertyTranslator(params PropertyInfo[] properties)
				{
					this._properties = properties;
				}

				// Token: 0x06006E43 RID: 28227 RVA: 0x0017AAC0 File Offset: 0x00178CC0
				protected PropertyTranslator(IEnumerable<PropertyInfo> properties)
				{
					this._properties = properties;
				}

				// Token: 0x1700119E RID: 4510
				// (get) Token: 0x06006E44 RID: 28228 RVA: 0x0017AACF File Offset: 0x00178CCF
				internal IEnumerable<PropertyInfo> Properties
				{
					get
					{
						return this._properties;
					}
				}

				// Token: 0x06006E45 RID: 28229
				internal abstract DbExpression Translate(ExpressionConverter parent, MemberExpression call);

				// Token: 0x06006E46 RID: 28230 RVA: 0x0017AAD7 File Offset: 0x00178CD7
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x040032AA RID: 12970
				private readonly IEnumerable<PropertyInfo> _properties;
			}

			// Token: 0x02000D52 RID: 3410
			internal sealed class DefaultCanonicalFunctionPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E47 RID: 28231 RVA: 0x0017AAE4 File Offset: 0x00178CE4
				internal DefaultCanonicalFunctionPropertyTranslator()
					: base(ExpressionConverter.MemberAccessTranslator.DefaultCanonicalFunctionPropertyTranslator.GetProperties())
				{
				}

				// Token: 0x06006E48 RID: 28232 RVA: 0x0017AAF4 File Offset: 0x00178CF4
				private static IEnumerable<PropertyInfo> GetProperties()
				{
					return new PropertyInfo[]
					{
						typeof(string).GetDeclaredProperty("Length"),
						typeof(DateTime).GetDeclaredProperty("Year"),
						typeof(DateTime).GetDeclaredProperty("Month"),
						typeof(DateTime).GetDeclaredProperty("Day"),
						typeof(DateTime).GetDeclaredProperty("Hour"),
						typeof(DateTime).GetDeclaredProperty("Minute"),
						typeof(DateTime).GetDeclaredProperty("Second"),
						typeof(DateTime).GetDeclaredProperty("Millisecond"),
						typeof(DateTimeOffset).GetDeclaredProperty("Year"),
						typeof(DateTimeOffset).GetDeclaredProperty("Month"),
						typeof(DateTimeOffset).GetDeclaredProperty("Day"),
						typeof(DateTimeOffset).GetDeclaredProperty("Hour"),
						typeof(DateTimeOffset).GetDeclaredProperty("Minute"),
						typeof(DateTimeOffset).GetDeclaredProperty("Second"),
						typeof(DateTimeOffset).GetDeclaredProperty("Millisecond"),
						typeof(DateTimeOffset).GetDeclaredProperty("LocalDateTime"),
						typeof(DateTimeOffset).GetDeclaredProperty("UtcDateTime")
					};
				}

				// Token: 0x06006E49 RID: 28233 RVA: 0x0017AC97 File Offset: 0x00178E97
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(call.Member.Name, call, new Expression[] { call.Expression });
				}
			}

			// Token: 0x02000D53 RID: 3411
			internal sealed class RenameCanonicalFunctionPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E4A RID: 28234 RVA: 0x0017ACBA File Offset: 0x00178EBA
				internal RenameCanonicalFunctionPropertyTranslator()
					: base(ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperties())
				{
				}

				// Token: 0x06006E4B RID: 28235 RVA: 0x0017ACC8 File Offset: 0x00178EC8
				private static IEnumerable<PropertyInfo> GetProperties()
				{
					return new PropertyInfo[]
					{
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTime), "Now", "CurrentDateTime"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTime), "UtcNow", "CurrentUtcDateTime"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTimeOffset), "Now", "CurrentDateTimeOffset"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Hours", "Hour"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Minutes", "Minute"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Seconds", "Second"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Milliseconds", "Millisecond")
					};
				}

				// Token: 0x06006E4C RID: 28236 RVA: 0x0017ADA0 File Offset: 0x00178FA0
				private static PropertyInfo GetProperty(Type declaringType, string propertyName, string canonicalFunctionName)
				{
					PropertyInfo declaredProperty = declaringType.GetDeclaredProperty(propertyName);
					ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator._propertyRenameMap[declaredProperty] = canonicalFunctionName;
					return declaredProperty;
				}

				// Token: 0x06006E4D RID: 28237 RVA: 0x0017ADC4 File Offset: 0x00178FC4
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					PropertyInfo propertyInfo = (PropertyInfo)call.Member;
					string text = ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator._propertyRenameMap[propertyInfo];
					DbExpression dbExpression;
					if (call.Expression == null)
					{
						dbExpression = parent.TranslateIntoCanonicalFunction(text, call, new Expression[0]);
					}
					else
					{
						dbExpression = parent.TranslateIntoCanonicalFunction(text, call, new Expression[] { call.Expression });
					}
					return dbExpression;
				}

				// Token: 0x040032AB RID: 12971
				private static readonly Dictionary<PropertyInfo, string> _propertyRenameMap = new Dictionary<PropertyInfo, string>(2);
			}

			// Token: 0x02000D54 RID: 3412
			internal sealed class VBDateAndTimeNowTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E4F RID: 28239 RVA: 0x0017AE28 File Offset: 0x00179028
				internal VBDateAndTimeNowTranslator(Assembly vbAssembly)
					: base(new PropertyInfo[] { ExpressionConverter.MemberAccessTranslator.VBDateAndTimeNowTranslator.GetProperty(vbAssembly) })
				{
				}

				// Token: 0x06006E50 RID: 28240 RVA: 0x0017AE3F File Offset: 0x0017903F
				private static PropertyInfo GetProperty(Assembly vbAssembly)
				{
					return vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime").GetDeclaredProperty("Now");
				}

				// Token: 0x06006E51 RID: 28241 RVA: 0x0017AE56 File Offset: 0x00179056
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateIntoCanonicalFunction("CurrentDateTime", call, new Expression[0]);
				}

				// Token: 0x040032AC RID: 12972
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";
			}

			// Token: 0x02000D55 RID: 3413
			internal sealed class EntityCollectionCountTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E52 RID: 28242 RVA: 0x0017AE6A File Offset: 0x0017906A
				internal EntityCollectionCountTranslator()
					: base(new PropertyInfo[] { ExpressionConverter.MemberAccessTranslator.EntityCollectionCountTranslator.GetProperty() })
				{
				}

				// Token: 0x06006E53 RID: 28243 RVA: 0x0017AE80 File Offset: 0x00179080
				private static PropertyInfo GetProperty()
				{
					return typeof(EntityCollection<>).GetDeclaredProperty("Count");
				}

				// Token: 0x06006E54 RID: 28244 RVA: 0x0017AE96 File Offset: 0x00179096
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.MemberAccessTranslator.TranslateCount(parent, call.Member.DeclaringType.GetGenericArguments()[0], call.Expression);
				}
			}

			// Token: 0x02000D56 RID: 3414
			internal sealed class NullableHasValueTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E55 RID: 28245 RVA: 0x0017AEB6 File Offset: 0x001790B6
				internal NullableHasValueTranslator()
					: base(new PropertyInfo[] { ExpressionConverter.MemberAccessTranslator.NullableHasValueTranslator.GetProperty() })
				{
				}

				// Token: 0x06006E56 RID: 28246 RVA: 0x0017AECC File Offset: 0x001790CC
				private static PropertyInfo GetProperty()
				{
					return typeof(Nullable<>).GetDeclaredProperty("HasValue");
				}

				// Token: 0x06006E57 RID: 28247 RVA: 0x0017AEE2 File Offset: 0x001790E2
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.CreateIsNullExpression(parent.TranslateExpression(call.Expression), call.Expression.Type).Not();
				}
			}

			// Token: 0x02000D57 RID: 3415
			internal sealed class NullableValueTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06006E58 RID: 28248 RVA: 0x0017AF05 File Offset: 0x00179105
				internal NullableValueTranslator()
					: base(new PropertyInfo[] { ExpressionConverter.MemberAccessTranslator.NullableValueTranslator.GetProperty() })
				{
				}

				// Token: 0x06006E59 RID: 28249 RVA: 0x0017AF1B File Offset: 0x0017911B
				private static PropertyInfo GetProperty()
				{
					return typeof(Nullable<>).GetDeclaredProperty("Value");
				}

				// Token: 0x06006E5A RID: 28250 RVA: 0x0017AF31 File Offset: 0x00179131
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateExpression(call.Expression);
				}
			}
		}

		// Token: 0x02000A75 RID: 2677
		internal static class StringTranslatorUtil
		{
			// Token: 0x060061F8 RID: 25080 RVA: 0x001542B4 File Offset: 0x001524B4
			internal static IEnumerable<Expression> GetConcatArgs(Expression linq)
			{
				if (linq.IsStringAddExpression())
				{
					foreach (Expression expression in ExpressionConverter.StringTranslatorUtil.GetConcatArgs((BinaryExpression)linq))
					{
						yield return expression;
					}
					IEnumerator<Expression> enumerator = null;
				}
				else
				{
					yield return linq;
				}
				yield break;
				yield break;
			}

			// Token: 0x060061F9 RID: 25081 RVA: 0x001542C4 File Offset: 0x001524C4
			internal static IEnumerable<Expression> GetConcatArgs(BinaryExpression linq)
			{
				foreach (Expression expression in ExpressionConverter.StringTranslatorUtil.GetConcatArgs(linq.Left))
				{
					yield return expression;
				}
				IEnumerator<Expression> enumerator = null;
				foreach (Expression expression2 in ExpressionConverter.StringTranslatorUtil.GetConcatArgs(linq.Right))
				{
					yield return expression2;
				}
				enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060061FA RID: 25082 RVA: 0x001542D4 File Offset: 0x001524D4
			internal static DbExpression ConcatArgs(ExpressionConverter parent, BinaryExpression linq)
			{
				return ExpressionConverter.StringTranslatorUtil.ConcatArgs(parent, linq, ExpressionConverter.StringTranslatorUtil.GetConcatArgs(linq).ToArray<Expression>());
			}

			// Token: 0x060061FB RID: 25083 RVA: 0x001542E8 File Offset: 0x001524E8
			internal static DbExpression ConcatArgs(ExpressionConverter parent, Expression linq, Expression[] linqArgs)
			{
				DbExpression[] array = (from arg in linqArgs
					where !arg.IsNullConstant()
					select ExpressionConverter.StringTranslatorUtil.ConvertToString(parent, arg)).ToArray<DbExpression>();
				if (array.Length == 0)
				{
					return DbExpressionBuilder.Constant(string.Empty);
				}
				DbExpression dbExpression = array.First<DbExpression>();
				foreach (DbExpression dbExpression2 in array.Skip(1))
				{
					dbExpression = parent.CreateCanonicalFunction("Concat", linq, new DbExpression[] { dbExpression, dbExpression2 });
				}
				return dbExpression;
			}

			// Token: 0x060061FC RID: 25084 RVA: 0x001543B4 File Offset: 0x001525B4
			internal static DbExpression StripNull(Expression sourceExpression, DbExpression inputExpression, DbExpression outputExpression, bool useDatabaseNullSemantics)
			{
				if (sourceExpression.IsNullConstant())
				{
					return DbExpressionBuilder.Constant(string.Empty);
				}
				if (sourceExpression.NodeType == ExpressionType.Constant)
				{
					return outputExpression;
				}
				if (useDatabaseNullSemantics)
				{
					return outputExpression;
				}
				return DbExpressionBuilder.Case(new DbIsNullExpression[] { inputExpression.IsNull() }, new DbConstantExpression[] { DbExpressionBuilder.Constant(string.Empty) }, outputExpression);
			}

			// Token: 0x060061FD RID: 25085 RVA: 0x00154410 File Offset: 0x00152610
			internal static DbExpression ConvertToString(ExpressionConverter parent, Expression linqExpression)
			{
				if (linqExpression.Type == typeof(object))
				{
					ConstantExpression constantExpression = linqExpression as ConstantExpression;
					linqExpression = ((constantExpression != null) ? Expression.Constant(constantExpression.Value) : linqExpression.RemoveConvert());
				}
				DbExpression expression = parent.TranslateExpression(linqExpression);
				Type nonNullableType = TypeSystem.GetNonNullableType(linqExpression.Type);
				bool flag = !parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior;
				if (nonNullableType.IsEnum)
				{
					if (Attribute.IsDefined(nonNullableType, typeof(FlagsAttribute)))
					{
						throw new NotSupportedException(Strings.Elinq_ToStringNotSupportedForEnumsWithFlags);
					}
					if (linqExpression.IsNullConstant())
					{
						return DbExpressionBuilder.Constant(string.Empty);
					}
					if (linqExpression.NodeType == ExpressionType.Constant)
					{
						object value = ((ConstantExpression)linqExpression).Value;
						return DbExpressionBuilder.Constant(Enum.GetName(nonNullableType, value) ?? value.ToString());
					}
					Type integralType = nonNullableType.GetEnumUnderlyingType();
					TypeUsage type = parent.GetValueLayerType(integralType);
					IEnumerable<DbExpression> enumerable = (from object v in nonNullableType.GetEnumValues()
						select global::System.Convert.ChangeType(v, integralType, CultureInfo.InvariantCulture) into v
						select DbExpressionBuilder.Constant(v) into c
						select expression.CastTo(type).Equal(c)).Concat(new DbIsNullExpression[] { expression.CastTo(type).IsNull() });
					IEnumerable<DbConstantExpression> enumerable2 = (from s in nonNullableType.GetEnumNames()
						select DbExpressionBuilder.Constant(s)).Concat(new DbConstantExpression[] { DbExpressionBuilder.Constant(string.Empty) });
					UnaryExpression unaryExpression = Expression.Convert(linqExpression, integralType);
					DbCastExpression dbCastExpression = parent.TranslateExpression(unaryExpression).CastTo(parent.GetValueLayerType(typeof(string)));
					return DbExpressionBuilder.Case(enumerable, enumerable2, dbCastExpression);
				}
				else
				{
					if (TypeSemantics.IsPrimitiveType(expression.ResultType, PrimitiveTypeKind.String))
					{
						return ExpressionConverter.StringTranslatorUtil.StripNull(linqExpression, expression, expression, flag);
					}
					if (TypeSemantics.IsPrimitiveType(expression.ResultType, PrimitiveTypeKind.Guid))
					{
						return ExpressionConverter.StringTranslatorUtil.StripNull(linqExpression, expression, expression.CastTo(parent.GetValueLayerType(typeof(string))).ToLower(), flag);
					}
					if (TypeSemantics.IsPrimitiveType(expression.ResultType, PrimitiveTypeKind.Boolean))
					{
						if (linqExpression.IsNullConstant())
						{
							return DbExpressionBuilder.Constant(string.Empty);
						}
						if (linqExpression.NodeType == ExpressionType.Constant)
						{
							return DbExpressionBuilder.Constant(((ConstantExpression)linqExpression).Value.ToString());
						}
						DbComparisonExpression dbComparisonExpression = expression.Equal(DbExpressionBuilder.True);
						DbComparisonExpression dbComparisonExpression2 = expression.Equal(DbExpressionBuilder.False);
						DbConstantExpression dbConstantExpression = DbExpressionBuilder.Constant(true.ToString());
						DbConstantExpression dbConstantExpression2 = DbExpressionBuilder.Constant(false.ToString());
						return DbExpressionBuilder.Case(new DbComparisonExpression[] { dbComparisonExpression, dbComparisonExpression2 }, new DbConstantExpression[] { dbConstantExpression, dbConstantExpression2 }, DbExpressionBuilder.Constant(string.Empty));
					}
					else
					{
						if (!ExpressionConverter.StringTranslatorUtil.SupportsCastToString(expression.ResultType))
						{
							throw new NotSupportedException(Strings.Elinq_ToStringNotSupportedForType(expression.ResultType.EdmType.Name));
						}
						return ExpressionConverter.StringTranslatorUtil.StripNull(linqExpression, expression, expression.CastTo(parent.GetValueLayerType(typeof(string))), flag);
					}
				}
			}

			// Token: 0x060061FE RID: 25086 RVA: 0x0015478C File Offset: 0x0015298C
			internal static bool SupportsCastToString(TypeUsage typeUsage)
			{
				return TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.String) || TypeSemantics.IsNumericType(typeUsage) || TypeSemantics.IsBooleanType(typeUsage) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.DateTime) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.DateTimeOffset) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.Time) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.Guid);
			}
		}

		// Token: 0x02000A76 RID: 2678
		internal abstract class Translator
		{
			// Token: 0x060061FF RID: 25087 RVA: 0x001547D9 File Offset: 0x001529D9
			protected Translator(params ExpressionType[] nodeTypes)
			{
				this._nodeTypes = nodeTypes;
			}

			// Token: 0x170010B7 RID: 4279
			// (get) Token: 0x06006200 RID: 25088 RVA: 0x001547E8 File Offset: 0x001529E8
			internal IEnumerable<ExpressionType> NodeTypes
			{
				get
				{
					return this._nodeTypes;
				}
			}

			// Token: 0x06006201 RID: 25089
			internal abstract DbExpression Translate(ExpressionConverter parent, Expression linq);

			// Token: 0x06006202 RID: 25090 RVA: 0x001547F0 File Offset: 0x001529F0
			public override string ToString()
			{
				return base.GetType().Name;
			}

			// Token: 0x04002B29 RID: 11049
			private readonly ExpressionType[] _nodeTypes;
		}

		// Token: 0x02000A77 RID: 2679
		internal abstract class TypedTranslator<T_Linq> : ExpressionConverter.Translator where T_Linq : Expression
		{
			// Token: 0x06006203 RID: 25091 RVA: 0x001547FD File Offset: 0x001529FD
			protected TypedTranslator(params ExpressionType[] nodeTypes)
				: base(nodeTypes)
			{
			}

			// Token: 0x06006204 RID: 25092 RVA: 0x00154806 File Offset: 0x00152A06
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				return this.TypedTranslate(parent, (T_Linq)((object)linq));
			}

			// Token: 0x06006205 RID: 25093
			protected abstract DbExpression TypedTranslate(ExpressionConverter parent, T_Linq linq);
		}

		// Token: 0x02000A78 RID: 2680
		private sealed class ConstantTranslator : ExpressionConverter.TypedTranslator<ConstantExpression>
		{
			// Token: 0x06006206 RID: 25094 RVA: 0x00154815 File Offset: 0x00152A15
			internal ConstantTranslator()
				: base(new ExpressionType[] { ExpressionType.Constant })
			{
			}

			// Token: 0x06006207 RID: 25095 RVA: 0x00154828 File Offset: 0x00152A28
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ConstantExpression linq)
			{
				if (linq == parent._funcletizer.RootContextExpression)
				{
					throw new InvalidOperationException(Strings.ELinq_UnsupportedUseOfContextParameter(parent._funcletizer.RootContextParameter.Name));
				}
				ObjectQuery objectQuery = (linq.Value as IQueryable).TryGetObjectQuery();
				if (objectQuery != null)
				{
					return parent.TranslateInlineQueryOfT(objectQuery);
				}
				IEnumerable enumerable = linq.Value as IEnumerable;
				if (enumerable != null)
				{
					Type elementType = TypeSystem.GetElementType(linq.Type);
					if (elementType != null && elementType != linq.Type)
					{
						List<Expression> list = new List<Expression>();
						foreach (object obj in enumerable)
						{
							list.Add(Expression.Constant(obj, elementType));
						}
						parent._recompileRequired = () => true;
						return parent.TranslateExpression(Expression.NewArrayInit(elementType, list));
					}
				}
				bool flag = linq.Value == null;
				bool flag2 = false;
				Type type = linq.Type;
				if (type == typeof(Enum))
				{
					type = linq.Value.GetType();
				}
				TypeUsage typeUsage;
				if (parent.TryGetValueLayerType(type, out typeUsage) && (Helper.IsScalarType(typeUsage.EdmType) || (flag && Helper.IsEntityType(typeUsage.EdmType))))
				{
					flag2 = true;
				}
				if (!flag2)
				{
					if (flag)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNullConstant(ExpressionConverter.DescribeClrType(linq.Type)));
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstant(ExpressionConverter.DescribeClrType(linq.Type)));
				}
				else
				{
					if (flag)
					{
						return typeUsage.Null();
					}
					object obj2 = linq.Value;
					if (Helper.IsPrimitiveType(typeUsage.EdmType))
					{
						Type nonNullableType = TypeSystem.GetNonNullableType(type);
						if (nonNullableType.IsEnum())
						{
							obj2 = global::System.Convert.ChangeType(linq.Value, nonNullableType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture);
						}
					}
					return typeUsage.Constant(obj2);
				}
			}
		}

		// Token: 0x02000A79 RID: 2681
		private sealed class ParameterTranslator : ExpressionConverter.TypedTranslator<ParameterExpression>
		{
			// Token: 0x06006208 RID: 25096 RVA: 0x00154A2C File Offset: 0x00152C2C
			internal ParameterTranslator()
				: base(new ExpressionType[] { ExpressionType.Parameter })
			{
			}

			// Token: 0x06006209 RID: 25097 RVA: 0x00154A3F File Offset: 0x00152C3F
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ParameterExpression linq)
			{
				throw new InvalidOperationException(Strings.ELinq_UnboundParameterExpression(linq.Name));
			}
		}

		// Token: 0x02000A7A RID: 2682
		private sealed class NewTranslator : ExpressionConverter.TypedTranslator<NewExpression>
		{
			// Token: 0x0600620A RID: 25098 RVA: 0x00154A51 File Offset: 0x00152C51
			internal NewTranslator()
				: base(new ExpressionType[] { ExpressionType.New })
			{
			}

			// Token: 0x0600620B RID: 25099 RVA: 0x00154A90 File Offset: 0x00152C90
			protected override DbExpression TypedTranslate(ExpressionConverter parent, NewExpression linq)
			{
				int num = ((linq.Members == null) ? 0 : linq.Members.Count);
				if (linq.Arguments.Count == 1 && this._castableTypes.Any((Tuple<Type, Type> cast) => cast.Item1 == linq.Constructor.DeclaringType && cast.Item2 == linq.Arguments[0].Type))
				{
					return parent.CreateCastExpression(parent.TranslateExpression(linq.Arguments[0]), linq.Constructor.DeclaringType, linq.Arguments[0].Type);
				}
				if (null == linq.Constructor || linq.Arguments.Count != num)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstructor);
				}
				parent.CheckInitializerType(linq.Type);
				List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(num + 1);
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				for (int i = 0; i < num; i++)
				{
					string text;
					Type type;
					TypeSystem.PropertyOrField(linq.Members[i], out text, out type);
					DbExpression dbExpression = parent.TranslateExpression(linq.Arguments[i]);
					hashSet.Add(text);
					list.Add(new KeyValuePair<string, DbExpression>(text, dbExpression));
				}
				InitializerMetadata initializerMetadata;
				if (num == 0)
				{
					list.Add(DbExpressionBuilder.True.As("Key"));
					initializerMetadata = InitializerMetadata.CreateEmptyProjectionInitializer(parent.EdmItemCollection, linq);
				}
				else
				{
					initializerMetadata = InitializerMetadata.CreateProjectionInitializer(parent.EdmItemCollection, linq);
				}
				parent.ValidateInitializerMetadata(initializerMetadata);
				return ExpressionConverter.CreateNewRowExpression(list, initializerMetadata);
			}

			// Token: 0x04002B2A RID: 11050
			private List<Tuple<Type, Type>> _castableTypes = new List<Tuple<Type, Type>>
			{
				new Tuple<Type, Type>(typeof(Guid), typeof(string))
			};
		}

		// Token: 0x02000A7B RID: 2683
		private sealed class NewArrayInitTranslator : ExpressionConverter.TypedTranslator<NewArrayExpression>
		{
			// Token: 0x0600620C RID: 25100 RVA: 0x00154C3F File Offset: 0x00152E3F
			internal NewArrayInitTranslator()
				: base(new ExpressionType[] { ExpressionType.NewArrayInit })
			{
			}

			// Token: 0x0600620D RID: 25101 RVA: 0x00154C54 File Offset: 0x00152E54
			protected override DbExpression TypedTranslate(ExpressionConverter parent, NewArrayExpression linq)
			{
				if (linq.Expressions.Count > 0)
				{
					return DbExpressionBuilder.NewCollection(linq.Expressions.Select((Expression e) => parent.TranslateExpression(e)));
				}
				TypeUsage typeUsage2;
				if (typeof(byte[]) == linq.Type)
				{
					TypeUsage typeUsage;
					if (parent.TryGetValueLayerType(typeof(byte), out typeUsage))
					{
						typeUsage2 = TypeHelpers.CreateCollectionTypeUsage(typeUsage);
						return typeUsage2.NewEmptyCollection();
					}
				}
				else if (parent.TryGetValueLayerType(linq.Type, out typeUsage2))
				{
					return typeUsage2.NewEmptyCollection();
				}
				throw new NotSupportedException(Strings.ELinq_UnsupportedType(ExpressionConverter.DescribeClrType(linq.Type)));
			}
		}

		// Token: 0x02000A7C RID: 2684
		private sealed class ListInitTranslator : ExpressionConverter.TypedTranslator<ListInitExpression>
		{
			// Token: 0x0600620E RID: 25102 RVA: 0x00154D08 File Offset: 0x00152F08
			internal ListInitTranslator()
				: base(new ExpressionType[] { ExpressionType.ListInit })
			{
			}

			// Token: 0x0600620F RID: 25103 RVA: 0x00154D1C File Offset: 0x00152F1C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ListInitExpression linq)
			{
				if (linq.NewExpression.Constructor != null && linq.NewExpression.Constructor.GetParameters().Length != 0)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstructor);
				}
				if (linq.Initializers.Any((ElementInit i) => i.Arguments.Count != 1))
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedInitializers);
				}
				return DbExpressionBuilder.NewCollection(linq.Initializers.Select((ElementInit i) => parent.TranslateExpression(i.Arguments[0])));
			}
		}

		// Token: 0x02000A7D RID: 2685
		private sealed class MemberInitTranslator : ExpressionConverter.TypedTranslator<MemberInitExpression>
		{
			// Token: 0x06006210 RID: 25104 RVA: 0x00154DBA File Offset: 0x00152FBA
			internal MemberInitTranslator()
				: base(new ExpressionType[] { ExpressionType.MemberInit })
			{
			}

			// Token: 0x06006211 RID: 25105 RVA: 0x00154DD0 File Offset: 0x00152FD0
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MemberInitExpression linq)
			{
				if (null == linq.NewExpression.Constructor || linq.NewExpression.Constructor.GetParameters().Length != 0)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstructor);
				}
				parent.CheckInitializerType(linq.Type);
				List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(linq.Bindings.Count + 1);
				MemberInfo[] array = new MemberInfo[linq.Bindings.Count];
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				for (int i = 0; i < linq.Bindings.Count; i++)
				{
					MemberAssignment memberAssignment = linq.Bindings[i] as MemberAssignment;
					if (memberAssignment == null)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedBinding);
					}
					string text;
					Type type;
					MemberInfo memberInfo = TypeSystem.PropertyOrField(memberAssignment.Member, out text, out type);
					DbExpression dbExpression = parent.TranslateExpression(memberAssignment.Expression);
					hashSet.Add(text);
					array[i] = memberInfo;
					list.Add(new KeyValuePair<string, DbExpression>(text, dbExpression));
				}
				InitializerMetadata initializerMetadata;
				if (list.Count == 0)
				{
					list.Add(DbExpressionBuilder.Constant(true).As("Key"));
					initializerMetadata = InitializerMetadata.CreateEmptyProjectionInitializer(parent.EdmItemCollection, linq.NewExpression);
				}
				else
				{
					initializerMetadata = InitializerMetadata.CreateProjectionInitializer(parent.EdmItemCollection, linq);
				}
				parent.ValidateInitializerMetadata(initializerMetadata);
				return ExpressionConverter.CreateNewRowExpression(list, initializerMetadata);
			}
		}

		// Token: 0x02000A7E RID: 2686
		private sealed class ConditionalTranslator : ExpressionConverter.TypedTranslator<ConditionalExpression>
		{
			// Token: 0x06006212 RID: 25106 RVA: 0x00154F17 File Offset: 0x00153117
			internal ConditionalTranslator()
				: base(new ExpressionType[] { ExpressionType.Conditional })
			{
			}

			// Token: 0x06006213 RID: 25107 RVA: 0x00154F2C File Offset: 0x0015312C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ConditionalExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Test);
				DbExpression dbExpression2;
				DbExpression dbExpression3;
				if (!linq.IfTrue.IsNullConstant())
				{
					dbExpression2 = parent.TranslateExpression(linq.IfTrue);
					dbExpression3 = ((!linq.IfFalse.IsNullConstant()) ? parent.TranslateExpression(linq.IfFalse) : dbExpression2.ResultType.Null());
				}
				else
				{
					if (linq.IfFalse.IsNullConstant())
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNullConstant(ExpressionConverter.DescribeClrType(linq.Type)));
					}
					dbExpression3 = parent.TranslateExpression(linq.IfFalse);
					dbExpression2 = dbExpression3.ResultType.Null();
				}
				return DbExpressionBuilder.Case(new List<DbExpression> { dbExpression }, new List<DbExpression> { dbExpression2 }, dbExpression3);
			}
		}

		// Token: 0x02000A7F RID: 2687
		private sealed class NotSupportedTranslator : ExpressionConverter.Translator
		{
			// Token: 0x06006214 RID: 25108 RVA: 0x00154FE5 File Offset: 0x001531E5
			internal NotSupportedTranslator(params ExpressionType[] nodeTypes)
				: base(nodeTypes)
			{
			}

			// Token: 0x06006215 RID: 25109 RVA: 0x00154FEE File Offset: 0x001531EE
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedExpressionType(linq.NodeType));
			}
		}

		// Token: 0x02000A80 RID: 2688
		private sealed class ExtensionTranslator : ExpressionConverter.Translator
		{
			// Token: 0x06006216 RID: 25110 RVA: 0x00155005 File Offset: 0x00153205
			internal ExtensionTranslator()
				: base(new ExpressionType[] { (ExpressionType)(-1) })
			{
			}

			// Token: 0x06006217 RID: 25111 RVA: 0x00155018 File Offset: 0x00153218
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				QueryParameterExpression queryParameterExpression = linq as QueryParameterExpression;
				if (queryParameterExpression == null)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedExpressionType(linq.NodeType));
				}
				parent.AddParameter(queryParameterExpression);
				return queryParameterExpression.ParameterReference;
			}
		}

		// Token: 0x02000A81 RID: 2689
		private abstract class BinaryTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06006218 RID: 25112 RVA: 0x00155052 File Offset: 0x00153252
			protected BinaryTranslator(params ExpressionType[] nodeTypes)
				: base(nodeTypes)
			{
			}

			// Token: 0x06006219 RID: 25113 RVA: 0x0015505B File Offset: 0x0015325B
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				return this.TranslateBinary(parent, parent.TranslateExpression(linq.Left), parent.TranslateExpression(linq.Right), linq);
			}

			// Token: 0x0600621A RID: 25114
			protected abstract DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq);
		}

		// Token: 0x02000A82 RID: 2690
		private sealed class CoalesceTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600621B RID: 25115 RVA: 0x0015507D File Offset: 0x0015327D
			internal CoalesceTranslator()
				: base(new ExpressionType[] { ExpressionType.Coalesce })
			{
			}

			// Token: 0x0600621C RID: 25116 RVA: 0x00155090 File Offset: 0x00153290
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				DbExpression dbExpression = ExpressionConverter.CreateIsNullExpression(left, linq.Left.Type);
				return DbExpressionBuilder.Case(new List<DbExpression>(1) { dbExpression }, new List<DbExpression>(1) { right }, left);
			}
		}

		// Token: 0x02000A83 RID: 2691
		private sealed class AndAlsoTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600621D RID: 25117 RVA: 0x001550D2 File Offset: 0x001532D2
			internal AndAlsoTranslator()
				: base(new ExpressionType[] { ExpressionType.AndAlso })
			{
			}

			// Token: 0x0600621E RID: 25118 RVA: 0x001550E4 File Offset: 0x001532E4
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.And(right);
			}
		}

		// Token: 0x02000A84 RID: 2692
		private sealed class OrElseTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600621F RID: 25119 RVA: 0x001550ED File Offset: 0x001532ED
			internal OrElseTranslator()
				: base(new ExpressionType[] { ExpressionType.OrElse })
			{
			}

			// Token: 0x06006220 RID: 25120 RVA: 0x00155100 File Offset: 0x00153300
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Or(right);
			}
		}

		// Token: 0x02000A85 RID: 2693
		private sealed class LessThanTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006221 RID: 25121 RVA: 0x00155109 File Offset: 0x00153309
			internal LessThanTranslator()
				: base(new ExpressionType[] { ExpressionType.LessThan })
			{
			}

			// Token: 0x06006222 RID: 25122 RVA: 0x0015511C File Offset: 0x0015331C
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.LessThan(right);
			}
		}

		// Token: 0x02000A86 RID: 2694
		private sealed class LessThanOrEqualsTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006223 RID: 25123 RVA: 0x00155125 File Offset: 0x00153325
			internal LessThanOrEqualsTranslator()
				: base(new ExpressionType[] { ExpressionType.LessThanOrEqual })
			{
			}

			// Token: 0x06006224 RID: 25124 RVA: 0x00155138 File Offset: 0x00153338
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.LessThanOrEqual(right);
			}
		}

		// Token: 0x02000A87 RID: 2695
		private sealed class GreaterThanTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006225 RID: 25125 RVA: 0x00155141 File Offset: 0x00153341
			internal GreaterThanTranslator()
				: base(new ExpressionType[] { ExpressionType.GreaterThan })
			{
			}

			// Token: 0x06006226 RID: 25126 RVA: 0x00155154 File Offset: 0x00153354
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.GreaterThan(right);
			}
		}

		// Token: 0x02000A88 RID: 2696
		private sealed class GreaterThanOrEqualsTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006227 RID: 25127 RVA: 0x0015515D File Offset: 0x0015335D
			internal GreaterThanOrEqualsTranslator()
				: base(new ExpressionType[] { ExpressionType.GreaterThanOrEqual })
			{
			}

			// Token: 0x06006228 RID: 25128 RVA: 0x00155170 File Offset: 0x00153370
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.GreaterThanOrEqual(right);
			}
		}

		// Token: 0x02000A89 RID: 2697
		private sealed class EqualsTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06006229 RID: 25129 RVA: 0x00155179 File Offset: 0x00153379
			internal EqualsTranslator()
				: base(new ExpressionType[] { ExpressionType.Equal })
			{
			}

			// Token: 0x0600622A RID: 25130 RVA: 0x0015518C File Offset: 0x0015338C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				Expression left = linq.Left;
				Expression right = linq.Right;
				bool flag = left.IsNullConstant();
				bool flag2 = right.IsNullConstant();
				if (flag && flag2)
				{
					return DbExpressionBuilder.True;
				}
				if (flag)
				{
					return ExpressionConverter.EqualsTranslator.CreateIsNullExpression(parent, right);
				}
				if (flag2)
				{
					return ExpressionConverter.EqualsTranslator.CreateIsNullExpression(parent, left);
				}
				DbExpression dbExpression = parent.TranslateExpression(left);
				DbExpression dbExpression2 = parent.TranslateExpression(right);
				ExpressionConverter.EqualsPattern equalsPattern = ExpressionConverter.EqualsPattern.Store;
				if (parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					equalsPattern = ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable;
				}
				return parent.CreateEqualsExpression(dbExpression, dbExpression2, equalsPattern, left.Type, right.Type);
			}

			// Token: 0x0600622B RID: 25131 RVA: 0x0015521D File Offset: 0x0015341D
			private static DbExpression CreateIsNullExpression(ExpressionConverter parent, Expression input)
			{
				input = input.RemoveConvert();
				return ExpressionConverter.CreateIsNullExpression(parent.TranslateExpression(input), input.Type);
			}
		}

		// Token: 0x02000A8A RID: 2698
		private sealed class NotEqualsTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x0600622C RID: 25132 RVA: 0x00155239 File Offset: 0x00153439
			internal NotEqualsTranslator()
				: base(new ExpressionType[] { ExpressionType.NotEqual })
			{
			}

			// Token: 0x0600622D RID: 25133 RVA: 0x0015524C File Offset: 0x0015344C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				Expression expression = Expression.Not(Expression.Equal(linq.Left, linq.Right));
				return parent.TranslateExpression(expression);
			}
		}

		// Token: 0x02000A8B RID: 2699
		private sealed class IsTranslator : ExpressionConverter.TypedTranslator<TypeBinaryExpression>
		{
			// Token: 0x0600622E RID: 25134 RVA: 0x00155277 File Offset: 0x00153477
			internal IsTranslator()
				: base(new ExpressionType[] { ExpressionType.TypeIs })
			{
			}

			// Token: 0x0600622F RID: 25135 RVA: 0x0015528C File Offset: 0x0015348C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, TypeBinaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Expression);
				TypeUsage isOrAsTargetType = parent.GetIsOrAsTargetType(ExpressionType.TypeIs, linq.TypeOperand, linq.Expression.Type);
				return dbExpression.IsOf(isOrAsTargetType);
			}
		}

		// Token: 0x02000A8C RID: 2700
		private sealed class AddTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006230 RID: 25136 RVA: 0x001552C5 File Offset: 0x001534C5
			internal AddTranslator()
				: base(new ExpressionType[]
				{
					ExpressionType.Add,
					ExpressionType.AddChecked
				})
			{
			}

			// Token: 0x06006231 RID: 25137 RVA: 0x001552D7 File Offset: 0x001534D7
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				if (linq.IsStringAddExpression())
				{
					return ExpressionConverter.StringTranslatorUtil.ConcatArgs(parent, linq);
				}
				return this.TranslateBinary(parent, parent.TranslateExpression(linq.Left), parent.TranslateExpression(linq.Right), linq);
			}

			// Token: 0x06006232 RID: 25138 RVA: 0x00155309 File Offset: 0x00153509
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Plus(right);
			}
		}

		// Token: 0x02000A8D RID: 2701
		private sealed class DivideTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006233 RID: 25139 RVA: 0x00155312 File Offset: 0x00153512
			internal DivideTranslator()
				: base(new ExpressionType[] { ExpressionType.Divide })
			{
			}

			// Token: 0x06006234 RID: 25140 RVA: 0x00155325 File Offset: 0x00153525
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Divide(right);
			}
		}

		// Token: 0x02000A8E RID: 2702
		private sealed class ModuloTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006235 RID: 25141 RVA: 0x0015532E File Offset: 0x0015352E
			internal ModuloTranslator()
				: base(new ExpressionType[] { ExpressionType.Modulo })
			{
			}

			// Token: 0x06006236 RID: 25142 RVA: 0x00155341 File Offset: 0x00153541
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Modulo(right);
			}
		}

		// Token: 0x02000A8F RID: 2703
		private sealed class MultiplyTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006237 RID: 25143 RVA: 0x0015534A File Offset: 0x0015354A
			internal MultiplyTranslator()
				: base(new ExpressionType[]
				{
					ExpressionType.Multiply,
					ExpressionType.MultiplyChecked
				})
			{
			}

			// Token: 0x06006238 RID: 25144 RVA: 0x00155362 File Offset: 0x00153562
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Multiply(right);
			}
		}

		// Token: 0x02000A90 RID: 2704
		private sealed class PowerTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06006239 RID: 25145 RVA: 0x0015536B File Offset: 0x0015356B
			internal PowerTranslator()
				: base(new ExpressionType[] { ExpressionType.Power })
			{
			}

			// Token: 0x0600623A RID: 25146 RVA: 0x0015537E File Offset: 0x0015357E
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Power(right);
			}
		}

		// Token: 0x02000A91 RID: 2705
		private sealed class SubtractTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600623B RID: 25147 RVA: 0x00155387 File Offset: 0x00153587
			internal SubtractTranslator()
				: base(new ExpressionType[]
				{
					ExpressionType.Subtract,
					ExpressionType.SubtractChecked
				})
			{
			}

			// Token: 0x0600623C RID: 25148 RVA: 0x0015539F File Offset: 0x0015359F
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Minus(right);
			}
		}

		// Token: 0x02000A92 RID: 2706
		private sealed class NegateTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x0600623D RID: 25149 RVA: 0x001553A8 File Offset: 0x001535A8
			internal NegateTranslator()
				: base(new ExpressionType[]
				{
					ExpressionType.Negate,
					ExpressionType.NegateChecked
				})
			{
			}

			// Token: 0x0600623E RID: 25150 RVA: 0x001553C0 File Offset: 0x001535C0
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand.UnaryMinus();
			}
		}

		// Token: 0x02000A93 RID: 2707
		private sealed class UnaryPlusTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x0600623F RID: 25151 RVA: 0x001553C8 File Offset: 0x001535C8
			internal UnaryPlusTranslator()
				: base(new ExpressionType[] { ExpressionType.UnaryPlus })
			{
			}

			// Token: 0x06006240 RID: 25152 RVA: 0x001553DB File Offset: 0x001535DB
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand;
			}
		}

		// Token: 0x02000A94 RID: 2708
		private abstract class BitwiseBinaryTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06006241 RID: 25153 RVA: 0x001553DE File Offset: 0x001535DE
			protected BitwiseBinaryTranslator(ExpressionType nodeType, string canonicalFunctionName)
				: base(new ExpressionType[] { nodeType })
			{
				this._canonicalFunctionName = canonicalFunctionName;
			}

			// Token: 0x06006242 RID: 25154 RVA: 0x001553F8 File Offset: 0x001535F8
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Left);
				DbExpression dbExpression2 = parent.TranslateExpression(linq.Right);
				if (TypeSemantics.IsBooleanType(dbExpression.ResultType))
				{
					return this.TranslateIntoLogicExpression(parent, linq, dbExpression, dbExpression2);
				}
				return parent.CreateCanonicalFunction(this._canonicalFunctionName, linq, new DbExpression[] { dbExpression, dbExpression2 });
			}

			// Token: 0x06006243 RID: 25155
			protected abstract DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right);

			// Token: 0x04002B2B RID: 11051
			private readonly string _canonicalFunctionName;
		}

		// Token: 0x02000A95 RID: 2709
		private sealed class AndTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06006244 RID: 25156 RVA: 0x00155452 File Offset: 0x00153652
			internal AndTranslator()
				: base(ExpressionType.And, "BitwiseAnd")
			{
			}

			// Token: 0x06006245 RID: 25157 RVA: 0x00155460 File Offset: 0x00153660
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				return left.And(right);
			}
		}

		// Token: 0x02000A96 RID: 2710
		private sealed class OrTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06006246 RID: 25158 RVA: 0x0015546A File Offset: 0x0015366A
			internal OrTranslator()
				: base(ExpressionType.Or, "BitwiseOr")
			{
			}

			// Token: 0x06006247 RID: 25159 RVA: 0x00155479 File Offset: 0x00153679
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				return left.Or(right);
			}
		}

		// Token: 0x02000A97 RID: 2711
		private sealed class ExclusiveOrTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06006248 RID: 25160 RVA: 0x00155483 File Offset: 0x00153683
			internal ExclusiveOrTranslator()
				: base(ExpressionType.ExclusiveOr, "BitwiseXor")
			{
			}

			// Token: 0x06006249 RID: 25161 RVA: 0x00155494 File Offset: 0x00153694
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				DbExpression dbExpression = left.And(right.Not());
				DbExpression dbExpression2 = left.Not().And(right);
				return dbExpression.Or(dbExpression2);
			}
		}

		// Token: 0x02000A98 RID: 2712
		private sealed class NotTranslator : ExpressionConverter.TypedTranslator<UnaryExpression>
		{
			// Token: 0x0600624A RID: 25162 RVA: 0x001554C2 File Offset: 0x001536C2
			internal NotTranslator()
				: base(new ExpressionType[] { ExpressionType.Not })
			{
			}

			// Token: 0x0600624B RID: 25163 RVA: 0x001554D8 File Offset: 0x001536D8
			protected override DbExpression TypedTranslate(ExpressionConverter parent, UnaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Operand);
				if (TypeSemantics.IsBooleanType(dbExpression.ResultType))
				{
					return dbExpression.Not();
				}
				return parent.CreateCanonicalFunction("BitwiseNot", linq, new DbExpression[] { dbExpression });
			}
		}

		// Token: 0x02000A99 RID: 2713
		private abstract class UnaryTranslator : ExpressionConverter.TypedTranslator<UnaryExpression>
		{
			// Token: 0x0600624C RID: 25164 RVA: 0x0015551C File Offset: 0x0015371C
			protected UnaryTranslator(params ExpressionType[] nodeTypes)
				: base(nodeTypes)
			{
			}

			// Token: 0x0600624D RID: 25165 RVA: 0x00155525 File Offset: 0x00153725
			protected override DbExpression TypedTranslate(ExpressionConverter parent, UnaryExpression linq)
			{
				return this.TranslateUnary(parent, linq, parent.TranslateExpression(linq.Operand));
			}

			// Token: 0x0600624E RID: 25166
			protected abstract DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand);
		}

		// Token: 0x02000A9A RID: 2714
		private sealed class QuoteTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x0600624F RID: 25167 RVA: 0x0015553B File Offset: 0x0015373B
			internal QuoteTranslator()
				: base(new ExpressionType[] { ExpressionType.Quote })
			{
			}

			// Token: 0x06006250 RID: 25168 RVA: 0x0015554E File Offset: 0x0015374E
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand;
			}
		}

		// Token: 0x02000A9B RID: 2715
		private sealed class ConvertTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06006251 RID: 25169 RVA: 0x00155551 File Offset: 0x00153751
			internal ConvertTranslator()
				: base(new ExpressionType[]
				{
					ExpressionType.Convert,
					ExpressionType.ConvertChecked
				})
			{
			}

			// Token: 0x06006252 RID: 25170 RVA: 0x0015556C File Offset: 0x0015376C
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				Type type = unary.Type;
				Type type2 = unary.Operand.Type;
				return parent.CreateCastExpression(operand, type, type2);
			}
		}

		// Token: 0x02000A9C RID: 2716
		private sealed class AsTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06006253 RID: 25171 RVA: 0x00155595 File Offset: 0x00153795
			internal AsTranslator()
				: base(new ExpressionType[] { ExpressionType.TypeAs })
			{
			}

			// Token: 0x06006254 RID: 25172 RVA: 0x001555A8 File Offset: 0x001537A8
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				TypeUsage isOrAsTargetType = parent.GetIsOrAsTargetType(ExpressionType.TypeAs, unary.Type, unary.Operand.Type);
				return operand.TreatAs(isOrAsTargetType);
			}
		}
	}
}
