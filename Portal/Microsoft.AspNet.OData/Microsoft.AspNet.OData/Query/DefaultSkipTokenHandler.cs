using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000A7 RID: 167
	public class DefaultSkipTokenHandler : SkipTokenHandler
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x000149E0 File Offset: 0x00012BE0
		public override Uri GenerateNextPageLink(Uri baseUri, int pageSize, object instance, ODataSerializerContext context)
		{
			if (context == null)
			{
				return null;
			}
			if (pageSize <= 0)
			{
				return null;
			}
			Func<object, string> func = null;
			IList<OrderByNode> orderByNodes = null;
			ExpandedReferenceSelectItem currentExpandedSelectItem = context.CurrentExpandedSelectItem;
			IEdmModel model = context.Model;
			if (context.QueryContext.DefaultQuerySettings.EnableSkipToken)
			{
				if (currentExpandedSelectItem != null)
				{
					if (TypedDelta.IsDeltaOfT(context.ExpandedResource.GetType()))
					{
						return GetNextPageHelper.GetNextPageLink(baseUri, pageSize, null, null);
					}
					if (currentExpandedSelectItem.OrderByOption != null)
					{
						orderByNodes = OrderByNode.CreateCollection(currentExpandedSelectItem.OrderByOption);
					}
					func = (object obj) => DefaultSkipTokenHandler.GenerateSkipTokenValue(obj, model, orderByNodes);
					return GetNextPageHelper.GetNextPageLink(baseUri, pageSize, instance, func);
				}
				else
				{
					if (context.QueryOptions != null && context.QueryOptions.OrderBy != null)
					{
						orderByNodes = context.QueryOptions.OrderBy.OrderByNodes;
					}
					func = (object obj) => DefaultSkipTokenHandler.GenerateSkipTokenValue(obj, model, orderByNodes);
				}
			}
			return context.InternalRequest.GetNextPageLink(pageSize, instance, func);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00014AD0 File Offset: 0x00012CD0
		private static string GenerateSkipTokenValue(object lastMember, IEdmModel model, IList<OrderByNode> orderByNodes)
		{
			if (lastMember == null)
			{
				return string.Empty;
			}
			IEnumerable<IEdmProperty> propertiesForSkipToken = DefaultSkipTokenHandler.GetPropertiesForSkipToken(lastMember, model, orderByNodes);
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			if (propertiesForSkipToken == null)
			{
				return stringBuilder.ToString();
			}
			int num = 0;
			int num2 = propertiesForSkipToken.Count<IEdmProperty>() - 1;
			IEdmStructuredObject edmStructuredObject = lastMember as IEdmStructuredObject;
			foreach (IEdmProperty edmProperty in propertiesForSkipToken)
			{
				bool flag = num == num2;
				string clrPropertyName = EdmLibHelpers.GetClrPropertyName(edmProperty, model);
				object value;
				if (edmStructuredObject != null)
				{
					edmStructuredObject.TryGetPropertyValue(clrPropertyName, out value);
				}
				else
				{
					value = lastMember.GetType().GetProperty(clrPropertyName).GetValue(lastMember);
				}
				string text;
				if (value == null)
				{
					text = ODataUriUtils.ConvertToUriLiteral(value, ODataVersion.V401);
				}
				else if (edmProperty.Type.IsEnum())
				{
					text = ODataUriUtils.ConvertToUriLiteral(new ODataEnumValue(value.ToString(), value.GetType().FullName), ODataVersion.V401, model);
				}
				else
				{
					text = ODataUriUtils.ConvertToUriLiteral(value, ODataVersion.V401, model);
				}
				stringBuilder.Append(edmProperty.Name).Append(DefaultSkipTokenHandler.propertyDelimiter).Append(text)
					.Append(flag ? string.Empty : ','.ToString());
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00014C1C File Offset: 0x00012E1C
		public override IQueryable<T> ApplyTo<T>(IQueryable<T> query, SkipTokenQueryOption skipTokenQueryOption)
		{
			return this.ApplyTo<T>(query, skipTokenQueryOption);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00014C28 File Offset: 0x00012E28
		public override IQueryable ApplyTo(IQueryable query, SkipTokenQueryOption skipTokenQueryOption)
		{
			if (skipTokenQueryOption == null)
			{
				throw Error.ArgumentNullOrEmpty("skipTokenQueryOption");
			}
			ODataQuerySettings querySettings = skipTokenQueryOption.QuerySettings;
			ODataQueryOptions queryOptions = skipTokenQueryOption.QueryOptions;
			IList<OrderByNode> list = null;
			if (queryOptions != null)
			{
				OrderByQueryOption orderByQueryOption = queryOptions.GenerateStableOrder();
				if (orderByQueryOption != null)
				{
					list = orderByQueryOption.OrderByNodes;
				}
			}
			return DefaultSkipTokenHandler.ApplyToCore(query, querySettings, list, skipTokenQueryOption.Context, skipTokenQueryOption.RawValue);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00014C7C File Offset: 0x00012E7C
		private static IQueryable ApplyToCore(IQueryable query, ODataQuerySettings querySettings, IList<OrderByNode> orderByNodes, ODataQueryContext context, string skipTokenRawValue)
		{
			if (context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			IDictionary<string, OrderByDirection> dictionary;
			if (orderByNodes != null)
			{
				dictionary = orderByNodes.OfType<OrderByPropertyNode>().ToDictionary((OrderByPropertyNode node) => node.Property.Name, (OrderByPropertyNode node) => node.Direction);
			}
			else
			{
				dictionary = new Dictionary<string, OrderByDirection>();
			}
			IDictionary<string, object> dictionary2 = DefaultSkipTokenHandler.PopulatePropertyValuePairs(skipTokenRawValue, context);
			if (dictionary2.Count == 0)
			{
				throw Error.InvalidOperation("Unable to get property values from the skiptoken value.", new object[0]);
			}
			ExpressionBinderBase expressionBinderBase = new FilterBinder(context.RequestContainer);
			bool enableConstantParameterization = querySettings.EnableConstantParameterization;
			ParameterExpression parameterExpression = Expression.Parameter(context.ElementClrType);
			Expression expression = null;
			Expression expression2 = null;
			bool flag = true;
			foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
			{
				string key = keyValuePair.Key;
				MemberExpression memberExpression = Expression.Property(parameterExpression, key);
				object obj = keyValuePair.Value;
				ODataEnumValue odataEnumValue = obj as ODataEnumValue;
				if (odataEnumValue != null)
				{
					obj = odataEnumValue.Value;
				}
				Expression expression3 = (enableConstantParameterization ? LinqParameterContainer.Parameterize(obj.GetType(), obj) : Expression.Constant(obj));
				Expression expression4;
				if (dictionary.ContainsKey(key) && dictionary[key] == OrderByDirection.Descending)
				{
					expression4 = expressionBinderBase.CreateBinaryExpression(BinaryOperatorKind.LessThan, memberExpression, expression3, true);
				}
				else
				{
					expression4 = expressionBinderBase.CreateBinaryExpression(BinaryOperatorKind.GreaterThan, memberExpression, expression3, true);
				}
				if (flag)
				{
					expression2 = expressionBinderBase.CreateBinaryExpression(BinaryOperatorKind.Equal, memberExpression, expression3, true);
					expression = expression4;
					flag = false;
				}
				else
				{
					Expression expression5 = Expression.AndAlso(expression2, expression4);
					expression = Expression.OrElse(expression, expression5);
					expression2 = Expression.AndAlso(expression2, expressionBinderBase.CreateBinaryExpression(BinaryOperatorKind.Equal, memberExpression, expression3, true));
				}
			}
			Expression expression6 = Expression.Lambda(expression, new ParameterExpression[] { parameterExpression });
			return ExpressionHelpers.Where(query, expression6, query.ElementType);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00014E80 File Offset: 0x00013080
		private static IDictionary<string, object> PopulatePropertyValuePairs(string value, ODataQueryContext context)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			IEnumerable<string> enumerable = DefaultSkipTokenHandler.ParseValue(value, ',');
			IEdmStructuredType edmStructuredType = context.ElementType as IEdmStructuredType;
			foreach (string text in enumerable)
			{
				string[] array = text.Split(new char[] { DefaultSkipTokenHandler.propertyDelimiter }, 2);
				if (array.Length <= 1 || string.IsNullOrWhiteSpace(array[0]))
				{
					throw Error.InvalidOperation(SRResources.SkipTokenParseError, new object[0]);
				}
				IEdmTypeReference edmTypeReference = null;
				IEdmProperty edmProperty = edmStructuredType.FindProperty(array[0]);
				if (edmProperty != null)
				{
					edmTypeReference = edmProperty.Type;
				}
				object obj = ODataUriUtils.ConvertFromUriLiteral(array[1], ODataVersion.V401, context.Model, edmTypeReference);
				dictionary.Add(array[0], obj);
			}
			return dictionary;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00014F54 File Offset: 0x00013154
		private static IList<string> ParseValue(string value, char delim)
		{
			IList<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '\'' || value[i] == '"')
				{
					stringBuilder.Append(value[i]);
					char c = value[i];
					i++;
					while (i < value.Length && value[i] != c)
					{
						stringBuilder.Append(value[i++]);
					}
					if (i != value.Length)
					{
						stringBuilder.Append(value[i]);
					}
				}
				else if (value[i] == delim)
				{
					list.Add(stringBuilder.ToString());
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			string text = stringBuilder.ToString();
			if (!string.IsNullOrWhiteSpace(text))
			{
				list.Add(text);
			}
			return list;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001503C File Offset: 0x0001323C
		private static IEnumerable<IEdmProperty> GetPropertiesForSkipToken(object lastMember, IEdmModel model, IList<OrderByNode> orderByNodes)
		{
			IEdmEntityType edmEntityType = DefaultSkipTokenHandler.GetTypeFromObject(lastMember, model) as IEdmEntityType;
			if (edmEntityType == null)
			{
				return null;
			}
			IEnumerable<IEdmProperty> enumerable = edmEntityType.Key();
			if (orderByNodes == null)
			{
				return enumerable;
			}
			if (orderByNodes.OfType<OrderByOpenPropertyNode>().Any<OrderByOpenPropertyNode>())
			{
				return null;
			}
			IList<IEdmProperty> list = (from p in orderByNodes.OfType<OrderByPropertyNode>()
				select p.Property).AsList<IEdmProperty>();
			foreach (IEdmProperty edmProperty in enumerable)
			{
				if (!list.Contains(edmProperty))
				{
					list.Add(edmProperty);
				}
			}
			return list.AsEnumerable<IEdmProperty>();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000150F4 File Offset: 0x000132F4
		private static IEdmType GetTypeFromObject(object value, IEdmModel model)
		{
			SelectExpandWrapper selectExpandWrapper = value as SelectExpandWrapper;
			if (selectExpandWrapper != null)
			{
				return selectExpandWrapper.GetEdmType().Definition;
			}
			Type type = value.GetType();
			return model.GetEdmType(type);
		}

		// Token: 0x0400014E RID: 334
		private const char CommaDelimiter = ',';

		// Token: 0x0400014F RID: 335
		private static char propertyDelimiter = '-';

		// Token: 0x04000150 RID: 336
		internal static DefaultSkipTokenHandler Instance = new DefaultSkipTokenHandler();
	}
}
