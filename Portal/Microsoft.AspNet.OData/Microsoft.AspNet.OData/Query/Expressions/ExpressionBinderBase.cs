using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000EC RID: 236
	public abstract class ExpressionBinderBase
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001D146 File Offset: 0x0001B346
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001D14E File Offset: 0x0001B34E
		internal IEdmModel Model { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001D157 File Offset: 0x0001B357
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0001D15F File Offset: 0x0001B35F
		internal ODataQuerySettings QuerySettings { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001D168 File Offset: 0x0001B368
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001D170 File Offset: 0x0001B370
		internal IWebApiAssembliesResolver InternalAssembliesResolver { get; set; }

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001D17C File Offset: 0x0001B37C
		protected ExpressionBinderBase(IServiceProvider requestContainer)
		{
			this.QuerySettings = ServiceProviderServiceExtensions.GetRequiredService<ODataQuerySettings>(requestContainer);
			this.Model = ServiceProviderServiceExtensions.GetRequiredService<IEdmModel>(requestContainer);
			IWebApiAssembliesResolver service = ServiceProviderServiceExtensions.GetService<IWebApiAssembliesResolver>(requestContainer);
			this.InternalAssembliesResolver = ((service != null) ? service : WebApiAssembliesResolver.Default);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001D1BF File Offset: 0x0001B3BF
		internal ExpressionBinderBase(IEdmModel model, IWebApiAssembliesResolver assembliesResolver, ODataQuerySettings querySettings)
			: this(model, querySettings)
		{
			this.InternalAssembliesResolver = assembliesResolver;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		internal ExpressionBinderBase(IEdmModel model, ODataQuerySettings querySettings)
		{
			this.QuerySettings = querySettings;
			this.Model = model;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		internal Expression CreateBinaryExpression(BinaryOperatorKind binaryOperator, Expression left, Expression right, bool liftToNull)
		{
			Type type = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
			Type type2 = Nullable.GetUnderlyingType(right.Type) ?? right.Type;
			if ((TypeHelper.IsEnum(type) || TypeHelper.IsEnum(type2)) && binaryOperator != BinaryOperatorKind.Has)
			{
				Type type3 = (TypeHelper.IsEnum(type) ? type : type2);
				Type underlyingType = Enum.GetUnderlyingType(type3);
				left = ExpressionBinderBase.ConvertToEnumUnderlyingType(left, type3, underlyingType);
				right = ExpressionBinderBase.ConvertToEnumUnderlyingType(right, type3, underlyingType);
			}
			if (type == typeof(DateTime) && type2 == typeof(DateTimeOffset))
			{
				right = ExpressionBinderBase.DateTimeOffsetToDateTime(right);
			}
			else if (type2 == typeof(DateTime) && type == typeof(DateTimeOffset))
			{
				left = ExpressionBinderBase.DateTimeOffsetToDateTime(left);
			}
			if ((ExpressionBinderBase.IsDateOrOffset(type) && ExpressionBinderBase.IsDate(type2)) || (ExpressionBinderBase.IsDate(type) && ExpressionBinderBase.IsDateOrOffset(type2)))
			{
				left = this.CreateDateBinaryExpression(left);
				right = this.CreateDateBinaryExpression(right);
			}
			if ((ExpressionBinderBase.IsDateOrOffset(type) && ExpressionBinderBase.IsTimeOfDay(type2)) || (ExpressionBinderBase.IsTimeOfDay(type) && ExpressionBinderBase.IsDateOrOffset(type2)) || (ExpressionBinderBase.IsTimeSpan(type) && ExpressionBinderBase.IsTimeOfDay(type2)) || (ExpressionBinderBase.IsTimeOfDay(type) && ExpressionBinderBase.IsTimeSpan(type2)))
			{
				left = this.CreateTimeBinaryExpression(left);
				right = this.CreateTimeBinaryExpression(right);
			}
			if (left.Type != right.Type)
			{
				left = ExpressionBinderBase.ToNullable(left);
				right = ExpressionBinderBase.ToNullable(right);
			}
			if (left.Type == typeof(Guid) || right.Type == typeof(Guid))
			{
				left = ExpressionBinderBase.ConvertNull(left, typeof(Guid));
				right = ExpressionBinderBase.ConvertNull(right, typeof(Guid));
				if (binaryOperator - BinaryOperatorKind.GreaterThan <= 3)
				{
					left = Expression.Call(ExpressionBinderBase.GuidCompareMethodInfo, left, right);
					right = ExpressionBinderBase.ZeroConstant;
				}
			}
			if (left.Type == typeof(string) || right.Type == typeof(string))
			{
				left = ExpressionBinderBase.ConvertNull(left, typeof(string));
				right = ExpressionBinderBase.ConvertNull(right, typeof(string));
				if (binaryOperator - BinaryOperatorKind.GreaterThan <= 3)
				{
					left = Expression.Call(ExpressionBinderBase.StringCompareMethodInfo, left, right, ExpressionBinderBase.OrdinalStringComparisonConstant);
					right = ExpressionBinderBase.ZeroConstant;
				}
			}
			ExpressionType expressionType;
			if (ExpressionBinderBase.BinaryOperatorMapping.TryGetValue(binaryOperator, out expressionType))
			{
				if (!(left.Type == typeof(byte[])) && !(right.Type == typeof(byte[])))
				{
					return Expression.MakeBinary(expressionType, left, right, liftToNull, null);
				}
				left = ExpressionBinderBase.ConvertNull(left, typeof(byte[]));
				right = ExpressionBinderBase.ConvertNull(right, typeof(byte[]));
				if (expressionType == ExpressionType.Equal)
				{
					return Expression.MakeBinary(expressionType, left, right, liftToNull, Linq2ObjectsComparisonMethods.AreByteArraysEqualMethodInfo);
				}
				if (expressionType != ExpressionType.NotEqual)
				{
					IEdmPrimitiveType edmPrimitiveTypeOrNull = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(typeof(byte[]));
					throw new ODataException(Error.Format(SRResources.BinaryOperatorNotSupported, new object[]
					{
						edmPrimitiveTypeOrNull.FullName(),
						edmPrimitiveTypeOrNull.FullName(),
						binaryOperator
					}));
				}
				return Expression.MakeBinary(expressionType, left, right, liftToNull, Linq2ObjectsComparisonMethods.AreByteArraysNotEqualMethodInfo);
			}
			else
			{
				if (TypeHelper.IsEnum(left.Type) && TypeHelper.IsEnum(right.Type) && binaryOperator == BinaryOperatorKind.Has)
				{
					UnaryExpression unaryExpression = Expression.Convert(right, typeof(Enum));
					return this.BindHas(left, unaryExpression);
				}
				throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
				{
					binaryOperator,
					typeof(FilterBinder).Name
				});
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001D58C File Offset: 0x0001B78C
		internal Expression CreateConvertExpression(ConvertNode convertNode, Expression source)
		{
			Type clrType = EdmLibHelpers.GetClrType(convertNode.TypeReference, this.Model, this.InternalAssembliesResolver);
			if (clrType == typeof(bool?) && source.Type == typeof(bool))
			{
				return source;
			}
			if (clrType == typeof(Date?) && (source.Type == typeof(DateTimeOffset?) || source.Type == typeof(DateTime?)))
			{
				return source;
			}
			if ((clrType == typeof(TimeOfDay?) && source.Type == typeof(TimeOfDay)) || (clrType == typeof(Date?) && source.Type == typeof(Date)))
			{
				return source;
			}
			if (clrType == typeof(TimeOfDay?) && (source.Type == typeof(DateTimeOffset?) || source.Type == typeof(DateTime?) || source.Type == typeof(TimeSpan?)))
			{
				return source;
			}
			if (ExpressionBinderBase.IsDateAndTimeRelated(clrType) && ExpressionBinderBase.IsDateAndTimeRelated(source.Type))
			{
				return source;
			}
			if (source == ExpressionBinderBase.NullConstant)
			{
				return source;
			}
			if (TypeHelper.IsEnum(source.Type))
			{
				return source;
			}
			if (this.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(source.Type) && !ExpressionBinderBase.IsNullable(clrType))
			{
				return Expression.Condition(ExpressionBinderBase.CheckForNull(source), Expression.Constant(null, ExpressionBinderBase.ToNullable(clrType)), Expression.Convert(ExpressionBinderBase.ExtractValueFromNullableExpression(source), ExpressionBinderBase.ToNullable(clrType)));
			}
			return Expression.Convert(source, clrType);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001D750 File Offset: 0x0001B950
		internal Expression ConvertNonStandardPrimitives(Expression source)
		{
			bool flag;
			Type type = EdmLibHelpers.IsNonstandardEdmPrimitive(source.Type, out flag);
			if (!flag)
			{
				return source;
			}
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(source.Type);
			Expression expression = null;
			if (TypeHelper.IsEnum(underlyingTypeOrSelf))
			{
				expression = source;
			}
			else
			{
				TypeCode typeCode = Type.GetTypeCode(underlyingTypeOrSelf);
				if (typeCode <= TypeCode.Char)
				{
					if (typeCode != TypeCode.Object)
					{
						if (typeCode == TypeCode.Char)
						{
							expression = Expression.Call(ExpressionBinderBase.ExtractValueFromNullableExpression(source), "ToString", null, null);
						}
					}
					else if (underlyingTypeOrSelf == typeof(char[]))
					{
						expression = Expression.New(typeof(string).GetConstructor(new Type[] { typeof(char[]) }), new Expression[] { source });
					}
					else if (underlyingTypeOrSelf == typeof(XElement))
					{
						expression = Expression.Call(source, "ToString", null, null);
					}
					else if (underlyingTypeOrSelf == typeof(Binary))
					{
						expression = Expression.Call(source, "ToArray", null, null);
					}
				}
				else
				{
					switch (typeCode)
					{
					case TypeCode.UInt16:
					case TypeCode.UInt32:
					case TypeCode.UInt64:
						expression = Expression.Convert(ExpressionBinderBase.ExtractValueFromNullableExpression(source), type);
						break;
					case TypeCode.Int32:
					case TypeCode.Int64:
						break;
					default:
						if (typeCode == TypeCode.DateTime)
						{
							expression = source;
						}
						break;
					}
				}
			}
			if (this.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(source.Type))
			{
				return Expression.Condition(ExpressionBinderBase.CheckForNull(source), Expression.Constant(null, ExpressionBinderBase.ToNullable(expression.Type)), ExpressionBinderBase.ToNullable(expression));
			}
			return expression;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001D8CC File Offset: 0x0001BACC
		internal Expression MakePropertyAccess(PropertyInfo propertyInfo, Expression argument)
		{
			Expression expression = argument;
			if (this.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				expression = this.RemoveInnerNullPropagation(argument);
			}
			expression = ExpressionBinderBase.ExtractValueFromNullableExpression(expression);
			return Expression.Property(expression, propertyInfo);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001D900 File Offset: 0x0001BB00
		internal Expression MakeFunctionCall(MemberInfo member, params Expression[] arguments)
		{
			IEnumerable<Expression> enumerable = arguments;
			if (this.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				enumerable = arguments.Select((Expression a) => this.RemoveInnerNullPropagation(a));
			}
			enumerable = ExpressionBinderBase.ExtractValueFromNullableArguments(enumerable);
			Expression expression;
			if (member.MemberType == MemberTypes.Method)
			{
				MethodInfo methodInfo = member as MethodInfo;
				if (methodInfo.IsStatic)
				{
					expression = Expression.Call(null, methodInfo, enumerable);
				}
				else
				{
					expression = Expression.Call(enumerable.First<Expression>(), methodInfo, enumerable.Skip(1));
				}
			}
			else
			{
				expression = Expression.Property(enumerable.First<Expression>(), member as PropertyInfo);
			}
			return this.CreateFunctionCallWithNullPropagation(expression, arguments);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001D98C File Offset: 0x0001BB8C
		internal Expression CreateFunctionCallWithNullPropagation(Expression functionCall, Expression[] arguments)
		{
			if (this.QuerySettings.HandleNullPropagation != HandleNullPropagationOption.True)
			{
				return functionCall;
			}
			Expression expression = ExpressionBinderBase.CheckIfArgumentsAreNull(arguments);
			if (expression == ExpressionBinderBase.FalseConstant)
			{
				return functionCall;
			}
			return Expression.Condition(expression, Expression.Constant(null, ExpressionBinderBase.ToNullable(functionCall.Type)), ExpressionBinderBase.ToNullable(functionCall));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		internal Expression RemoveInnerNullPropagation(Expression expression)
		{
			if (this.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && expression.NodeType == ExpressionType.Conditional)
			{
				ConditionalExpression conditionalExpression = (ConditionalExpression)expression;
				if (conditionalExpression.Test.NodeType != ExpressionType.OrElse)
				{
					expression = conditionalExpression.IfFalse;
					if (expression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = expression as UnaryExpression;
						if (Nullable.GetUnderlyingType(unaryExpression.Type) == unaryExpression.Operand.Type)
						{
							expression = unaryExpression.Operand;
						}
					}
				}
			}
			return expression;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001DA54 File Offset: 0x0001BC54
		internal string GetFullPropertyPath(SingleValueNode node)
		{
			string text = null;
			SingleValueNode singleValueNode = null;
			QueryNodeKind kind = node.Kind;
			if (kind != QueryNodeKind.SingleValuePropertyAccess)
			{
				if (kind != QueryNodeKind.SingleNavigationNode)
				{
					if (kind == QueryNodeKind.SingleComplexNode)
					{
						SingleComplexNode singleComplexNode = (SingleComplexNode)node;
						text = singleComplexNode.Property.Name;
						singleValueNode = singleComplexNode.Source;
					}
				}
				else
				{
					SingleNavigationNode singleNavigationNode = (SingleNavigationNode)node;
					text = singleNavigationNode.NavigationProperty.Name;
					singleValueNode = singleNavigationNode.Source;
				}
			}
			else
			{
				SingleValuePropertyAccessNode singleValuePropertyAccessNode = (SingleValuePropertyAccessNode)node;
				text = singleValuePropertyAccessNode.Property.Name;
				singleValueNode = singleValuePropertyAccessNode.Source;
			}
			if (singleValueNode != null)
			{
				string fullPropertyPath = this.GetFullPropertyPath(singleValueNode);
				if (fullPropertyPath != null)
				{
					text = fullPropertyPath + "\\" + text;
				}
			}
			return text;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		protected PropertyInfo GetDynamicPropertyContainer(SingleValueOpenPropertyAccessNode openNode)
		{
			IEdmTypeReference typeReference = openNode.Source.TypeReference;
			IEdmStructuredType edmStructuredType;
			if (typeReference.IsEntity())
			{
				edmStructuredType = typeReference.AsEntity().EntityDefinition();
			}
			else
			{
				if (!typeReference.IsComplex())
				{
					throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
					{
						openNode.Kind,
						typeof(FilterBinder).Name
					});
				}
				edmStructuredType = typeReference.AsComplex().ComplexDefinition();
			}
			return EdmLibHelpers.GetDynamicPropertyDictionary(edmStructuredType, this.Model);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001DB68 File Offset: 0x0001BD68
		private static Expression CheckIfArgumentsAreNull(Expression[] arguments)
		{
			if (arguments.Any((Expression arg) => arg == ExpressionBinderBase.NullConstant))
			{
				return ExpressionBinderBase.TrueConstant;
			}
			arguments = (from arg in arguments
				select ExpressionBinderBase.CheckForNull(arg) into arg
				where arg != null
				select arg).ToArray<Expression>();
			if (arguments.Any<Expression>())
			{
				return arguments.Aggregate((Expression left, Expression right) => Expression.OrElse(left, right));
			}
			return ExpressionBinderBase.FalseConstant;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001DC25 File Offset: 0x0001BE25
		internal static Expression CheckForNull(Expression expression)
		{
			if (ExpressionBinderBase.IsNullable(expression.Type) && expression.NodeType != ExpressionType.Constant)
			{
				return Expression.Equal(expression, Expression.Constant(null));
			}
			return null;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001DC4C File Offset: 0x0001BE4C
		private static IEnumerable<Expression> ExtractValueFromNullableArguments(IEnumerable<Expression> arguments)
		{
			return arguments.Select((Expression arg) => ExpressionBinderBase.ExtractValueFromNullableExpression(arg));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001DC73 File Offset: 0x0001BE73
		internal static Expression ExtractValueFromNullableExpression(Expression source)
		{
			if (!(Nullable.GetUnderlyingType(source.Type) != null))
			{
				return source;
			}
			return Expression.Property(source, "Value");
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001DC98 File Offset: 0x0001BE98
		internal Expression BindHas(Expression left, Expression flag)
		{
			Expression[] array = new Expression[] { left, flag };
			return this.MakeFunctionCall(ClrCanonicalFunctions.HasFlag, array);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		protected void EnsureFlattenedPropertyContainer(ParameterExpression source)
		{
			if (this.BaseQuery != null)
			{
				this.FlattenedPropertyContainer = this.FlattenedPropertyContainer ?? this.GetFlattenedProperties(source);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		internal IDictionary<string, Expression> GetFlattenedProperties(ParameterExpression source)
		{
			if (this.BaseQuery == null)
			{
				return null;
			}
			if (!typeof(GroupByWrapper).IsAssignableFrom(this.BaseQuery.ElementType))
			{
				return null;
			}
			MethodCallExpression methodCallExpression = this.BaseQuery.Expression as MethodCallExpression;
			if (methodCallExpression == null)
			{
				return null;
			}
			while (methodCallExpression.Method.Name == "Where")
			{
				methodCallExpression = methodCallExpression.Arguments.FirstOrDefault<Expression>() as MethodCallExpression;
			}
			if (methodCallExpression == null)
			{
				return null;
			}
			Dictionary<string, Expression> dictionary = new Dictionary<string, Expression>();
			ExpressionBinderBase.CollectAssigments(dictionary, Expression.Property(source, "GroupByContainer"), ExpressionBinderBase.ExtractContainerExpression(methodCallExpression.Arguments.FirstOrDefault<Expression>() as MethodCallExpression, "GroupByContainer"), null);
			ExpressionBinderBase.CollectAssigments(dictionary, Expression.Property(source, "Container"), ExpressionBinderBase.ExtractContainerExpression(methodCallExpression, "Container"), null);
			return dictionary;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		private static MemberInitExpression ExtractContainerExpression(MethodCallExpression expression, string containerName)
		{
			MemberInitExpression memberInitExpression = ((expression.Arguments[1] as UnaryExpression).Operand as LambdaExpression).Body as MemberInitExpression;
			if (memberInitExpression != null)
			{
				MemberAssignment memberAssignment = memberInitExpression.Bindings.FirstOrDefault((MemberBinding m) => m.Member.Name == containerName) as MemberAssignment;
				if (memberAssignment != null)
				{
					return memberAssignment.Expression as MemberInitExpression;
				}
			}
			return null;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001DE1C File Offset: 0x0001C01C
		private static void CollectAssigments(IDictionary<string, Expression> flattenPropertyContainer, Expression source, MemberInitExpression expression, string prefix = null)
		{
			if (expression == null)
			{
				return;
			}
			string text = null;
			Type type = null;
			MemberInitExpression memberInitExpression = null;
			Expression expression2 = null;
			foreach (MemberAssignment memberAssignment in expression.Bindings.OfType<MemberAssignment>())
			{
				MemberInitExpression memberInitExpression2 = memberAssignment.Expression as MemberInitExpression;
				if (memberInitExpression2 != null && memberAssignment.Member.Name == "Next")
				{
					memberInitExpression = memberInitExpression2;
				}
				else if (memberAssignment.Member.Name == "Name")
				{
					text = (memberAssignment.Expression as ConstantExpression).Value as string;
				}
				else if (memberAssignment.Member.Name == "Value" || memberAssignment.Member.Name == "NestedValue")
				{
					type = memberAssignment.Expression.Type;
					if (type == typeof(object) && memberAssignment.Expression.NodeType == ExpressionType.Convert)
					{
						type = ((UnaryExpression)memberAssignment.Expression).Operand.Type;
					}
					if (typeof(GroupByWrapper).IsAssignableFrom(type))
					{
						expression2 = memberAssignment.Expression;
					}
				}
			}
			if (prefix != null)
			{
				text = prefix + "\\" + text;
			}
			if (typeof(GroupByWrapper).IsAssignableFrom(type))
			{
				flattenPropertyContainer.Add(text, Expression.Property(source, "NestedValue"));
			}
			else
			{
				flattenPropertyContainer.Add(text, Expression.Convert(Expression.Property(source, "Value"), type));
			}
			if (memberInitExpression != null)
			{
				ExpressionBinderBase.CollectAssigments(flattenPropertyContainer, Expression.Property(source, "Next"), memberInitExpression, prefix);
			}
			if (expression2 != null)
			{
				MemberInitExpression memberInitExpression3 = ((expression2 as MemberInitExpression).Bindings.First<MemberBinding>() as MemberAssignment).Expression as MemberInitExpression;
				MemberExpression memberExpression = Expression.Property(Expression.Property(source, "NestedValue"), "GroupByContainer");
				ExpressionBinderBase.CollectAssigments(flattenPropertyContainer, memberExpression, memberInitExpression3, text);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001E02C File Offset: 0x0001C22C
		protected Expression GetFlattenedPropertyExpression(string propertyPath)
		{
			if (this.FlattenedPropertyContainer == null)
			{
				return null;
			}
			Expression expression;
			if (this.FlattenedPropertyContainer.TryGetValue(propertyPath, out expression))
			{
				return expression;
			}
			throw new ODataException(Error.Format(SRResources.PropertyOrPathWasRemovedFromContext, new object[] { propertyPath }));
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001E070 File Offset: 0x0001C270
		private Expression GetProperty(Expression source, string propertyName)
		{
			if (ExpressionBinderBase.IsDateOrOffset(source.Type))
			{
				if (ExpressionBinderBase.IsDateTime(source.Type))
				{
					return this.MakePropertyAccess(ClrCanonicalFunctions.DateTimeProperties[propertyName], source);
				}
				return this.MakePropertyAccess(ClrCanonicalFunctions.DateTimeOffsetProperties[propertyName], source);
			}
			else
			{
				if (ExpressionBinderBase.IsDate(source.Type))
				{
					return this.MakePropertyAccess(ClrCanonicalFunctions.DateProperties[propertyName], source);
				}
				if (ExpressionBinderBase.IsTimeOfDay(source.Type))
				{
					return this.MakePropertyAccess(ClrCanonicalFunctions.TimeOfDayProperties[propertyName], source);
				}
				if (ExpressionBinderBase.IsTimeSpan(source.Type))
				{
					return this.MakePropertyAccess(ClrCanonicalFunctions.TimeSpanProperties[propertyName], source);
				}
				return source;
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001E120 File Offset: 0x0001C320
		private Expression CreateDateBinaryExpression(Expression source)
		{
			source = ExpressionBinderBase.ConvertToDateTimeRelatedConstExpression(source);
			Expression property = this.GetProperty(source, "year");
			Expression property2 = this.GetProperty(source, "month");
			Expression property3 = this.GetProperty(source, "day");
			Expression expression = Expression.Add(Expression.Add(Expression.Multiply(property, Expression.Constant(10000)), Expression.Multiply(property2, Expression.Constant(100))), property3);
			return this.CreateFunctionCallWithNullPropagation(expression, new Expression[] { source });
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		private Expression CreateTimeBinaryExpression(Expression source)
		{
			source = ExpressionBinderBase.ConvertToDateTimeRelatedConstExpression(source);
			Expression property = this.GetProperty(source, "hour");
			Expression property2 = this.GetProperty(source, "minute");
			Expression property3 = this.GetProperty(source, "second");
			Expression property4 = this.GetProperty(source, "millisecond");
			Expression expression = Expression.Multiply(Expression.Convert(property, typeof(long)), Expression.Constant(36000000000L));
			Expression expression2 = Expression.Multiply(Expression.Convert(property2, typeof(long)), Expression.Constant(600000000L));
			Expression expression3 = Expression.Multiply(Expression.Convert(property3, typeof(long)), Expression.Constant(10000000L));
			Expression expression4 = Expression.Add(expression, Expression.Add(expression2, Expression.Add(expression3, Expression.Convert(property4, typeof(long)))));
			return this.CreateFunctionCallWithNullPropagation(expression4, new Expression[] { source });
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001E294 File Offset: 0x0001C494
		private static Expression ConvertToDateTimeRelatedConstExpression(Expression source)
		{
			object obj = ExpressionBinderBase.ExtractParameterizedConstant(source);
			if (obj != null && TypeHelper.IsNullable(source.Type))
			{
				DateTimeOffset? dateTimeOffset = obj as DateTimeOffset?;
				if (dateTimeOffset != null)
				{
					return Expression.Constant(dateTimeOffset.Value, typeof(DateTimeOffset));
				}
				DateTime? dateTime = obj as DateTime?;
				if (dateTime != null)
				{
					return Expression.Constant(dateTime.Value, typeof(DateTime));
				}
				Date? date = obj as Date?;
				if (date != null)
				{
					return Expression.Constant(date.Value, typeof(Date));
				}
				TimeOfDay? timeOfDay = obj as TimeOfDay?;
				if (timeOfDay != null)
				{
					return Expression.Constant(timeOfDay.Value, typeof(TimeOfDay));
				}
			}
			return source;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001E384 File Offset: 0x0001C584
		internal static Expression ConvertToEnumUnderlyingType(Expression expression, Type enumType, Type enumUnderlyingType)
		{
			object obj = ExpressionBinderBase.ExtractParameterizedConstant(expression);
			if (obj != null)
			{
				string text = obj as string;
				if (text != null)
				{
					return Expression.Constant(Convert.ChangeType(Enum.Parse(enumType, text), enumUnderlyingType, CultureInfo.InvariantCulture));
				}
				return Expression.Constant(Convert.ChangeType(obj, enumUnderlyingType, CultureInfo.InvariantCulture));
			}
			else
			{
				if (expression.Type == enumType)
				{
					return Expression.Convert(expression, enumUnderlyingType);
				}
				if (Nullable.GetUnderlyingType(expression.Type) == enumType)
				{
					return Expression.Convert(expression, typeof(Nullable<>).MakeGenericType(new Type[] { enumUnderlyingType }));
				}
				if (expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value == null)
				{
					return expression;
				}
				throw Error.NotSupported(SRResources.ConvertToEnumFailed, new object[] { enumType, expression.Type });
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001E450 File Offset: 0x0001C650
		internal static object ExtractParameterizedConstant(Expression expression)
		{
			if (expression.NodeType == ExpressionType.MemberAccess)
			{
				MemberExpression memberExpression = expression as MemberExpression;
				PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
				if (propertyInfo != null && propertyInfo.GetMethod.IsStatic)
				{
					return propertyInfo.GetValue(new object());
				}
				if (memberExpression.Expression.NodeType == ExpressionType.Constant)
				{
					return ((memberExpression.Expression as ConstantExpression).Value as LinqParameterContainer).Property;
				}
			}
			return null;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		internal static Expression DateTimeOffsetToDateTime(Expression expression)
		{
			UnaryExpression unaryExpression = expression as UnaryExpression;
			if (unaryExpression != null && Nullable.GetUnderlyingType(unaryExpression.Type) == unaryExpression.Operand.Type)
			{
				expression = unaryExpression.Operand;
			}
			DateTimeOffset? dateTimeOffset = ExpressionBinderBase.ExtractParameterizedConstant(expression) as DateTimeOffset?;
			if (dateTimeOffset != null)
			{
				expression = Expression.Constant(EdmPrimitiveHelpers.ConvertPrimitiveValue(dateTimeOffset.Value, typeof(DateTime)));
			}
			return expression;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001E541 File Offset: 0x0001C741
		internal static bool IsNullable(Type t)
		{
			return !TypeHelper.IsValueType(t) || (t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001E56D File Offset: 0x0001C76D
		internal static Type ToNullable(Type t)
		{
			if (ExpressionBinderBase.IsNullable(t))
			{
				return t;
			}
			return typeof(Nullable<>).MakeGenericType(new Type[] { t });
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001E592 File Offset: 0x0001C792
		internal static Expression ToNullable(Expression expression)
		{
			if (!ExpressionBinderBase.IsNullable(expression.Type))
			{
				return Expression.Convert(expression, ExpressionBinderBase.ToNullable(expression.Type));
			}
			return expression;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
		internal static bool IsIQueryable(Type type)
		{
			return typeof(IQueryable).IsAssignableFrom(type);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001E5C6 File Offset: 0x0001C7C6
		internal static bool IsDoubleOrDecimal(Type type)
		{
			return ExpressionBinderBase.IsType<double>(type) || ExpressionBinderBase.IsType<decimal>(type);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
		internal static bool IsDateAndTimeRelated(Type type)
		{
			return ExpressionBinderBase.IsType<Date>(type) || ExpressionBinderBase.IsType<DateTime>(type) || ExpressionBinderBase.IsType<DateTimeOffset>(type) || ExpressionBinderBase.IsType<TimeOfDay>(type) || ExpressionBinderBase.IsType<TimeSpan>(type);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001E602 File Offset: 0x0001C802
		internal static bool IsDateRelated(Type type)
		{
			return ExpressionBinderBase.IsType<Date>(type) || ExpressionBinderBase.IsType<DateTime>(type) || ExpressionBinderBase.IsType<DateTimeOffset>(type);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001E61C File Offset: 0x0001C81C
		internal static bool IsTimeRelated(Type type)
		{
			return ExpressionBinderBase.IsType<TimeOfDay>(type) || ExpressionBinderBase.IsType<DateTime>(type) || ExpressionBinderBase.IsType<DateTimeOffset>(type) || ExpressionBinderBase.IsType<TimeSpan>(type);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001E63E File Offset: 0x0001C83E
		internal static bool IsDateOrOffset(Type type)
		{
			return ExpressionBinderBase.IsType<DateTime>(type) || ExpressionBinderBase.IsType<DateTimeOffset>(type);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001E650 File Offset: 0x0001C850
		internal static bool IsDateTime(Type type)
		{
			return ExpressionBinderBase.IsType<DateTime>(type);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001E658 File Offset: 0x0001C858
		internal static bool IsTimeSpan(Type type)
		{
			return ExpressionBinderBase.IsType<TimeSpan>(type);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001E660 File Offset: 0x0001C860
		internal static bool IsTimeOfDay(Type type)
		{
			return ExpressionBinderBase.IsType<TimeOfDay>(type);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001E668 File Offset: 0x0001C868
		internal static bool IsDate(Type type)
		{
			return ExpressionBinderBase.IsType<Date>(type);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001E670 File Offset: 0x0001C870
		internal static bool IsInteger(Type type)
		{
			return ExpressionBinderBase.IsType<short>(type) || ExpressionBinderBase.IsType<int>(type) || ExpressionBinderBase.IsType<long>(type);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001E68A File Offset: 0x0001C88A
		internal static bool IsType<T>(Type type) where T : struct
		{
			return type == typeof(T) || type == typeof(T?);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		internal static Expression ConvertNull(Expression expression, Type type)
		{
			ConstantExpression constantExpression = expression as ConstantExpression;
			if (constantExpression != null && constantExpression.Value == null)
			{
				return Expression.Constant(null, type);
			}
			return expression;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		public static int GuidCompare(Guid firstValue, Guid secondValue)
		{
			return firstValue.CompareTo(secondValue);
		}

		// Token: 0x0400024F RID: 591
		internal static readonly MethodInfo StringCompareMethodInfo = typeof(string).GetMethod("Compare", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(StringComparison)
		});

		// Token: 0x04000250 RID: 592
		internal static readonly MethodInfo GuidCompareMethodInfo = typeof(ExpressionBinderBase).GetMethod("GuidCompare", new Type[]
		{
			typeof(Guid),
			typeof(Guid)
		});

		// Token: 0x04000251 RID: 593
		internal static readonly string DictionaryStringObjectIndexerName = typeof(Dictionary<string, object>).GetDefaultMembers()[0].Name;

		// Token: 0x04000252 RID: 594
		internal static readonly Expression NullConstant = Expression.Constant(null);

		// Token: 0x04000253 RID: 595
		internal static readonly Expression FalseConstant = Expression.Constant(false);

		// Token: 0x04000254 RID: 596
		internal static readonly Expression TrueConstant = Expression.Constant(true);

		// Token: 0x04000255 RID: 597
		internal static readonly Expression ZeroConstant = Expression.Constant(0);

		// Token: 0x04000256 RID: 598
		internal static readonly Expression OrdinalStringComparisonConstant = Expression.Constant(StringComparison.Ordinal);

		// Token: 0x04000257 RID: 599
		internal static readonly MethodInfo EnumTryParseMethod = typeof(Enum).GetMethods().Single((MethodInfo m) => m.Name == "TryParse" && m.GetParameters().Length == 2);

		// Token: 0x04000258 RID: 600
		internal static readonly Dictionary<BinaryOperatorKind, ExpressionType> BinaryOperatorMapping = new Dictionary<BinaryOperatorKind, ExpressionType>
		{
			{
				BinaryOperatorKind.Add,
				ExpressionType.Add
			},
			{
				BinaryOperatorKind.And,
				ExpressionType.AndAlso
			},
			{
				BinaryOperatorKind.Divide,
				ExpressionType.Divide
			},
			{
				BinaryOperatorKind.Equal,
				ExpressionType.Equal
			},
			{
				BinaryOperatorKind.GreaterThan,
				ExpressionType.GreaterThan
			},
			{
				BinaryOperatorKind.GreaterThanOrEqual,
				ExpressionType.GreaterThanOrEqual
			},
			{
				BinaryOperatorKind.LessThan,
				ExpressionType.LessThan
			},
			{
				BinaryOperatorKind.LessThanOrEqual,
				ExpressionType.LessThanOrEqual
			},
			{
				BinaryOperatorKind.Modulo,
				ExpressionType.Modulo
			},
			{
				BinaryOperatorKind.Multiply,
				ExpressionType.Multiply
			},
			{
				BinaryOperatorKind.NotEqual,
				ExpressionType.NotEqual
			},
			{
				BinaryOperatorKind.Or,
				ExpressionType.OrElse
			},
			{
				BinaryOperatorKind.Subtract,
				ExpressionType.Subtract
			}
		};

		// Token: 0x0400025C RID: 604
		internal IQueryable BaseQuery;

		// Token: 0x0400025D RID: 605
		internal IDictionary<string, Expression> FlattenedPropertyContainer;
	}
}
