using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x020000A0 RID: 160
	internal static class RoutingConventionHelpers
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00012328 File Offset: 0x00010528
		public static string SelectAction(this IEdmOperation operation, IWebApiActionMap actionMap, bool isCollection)
		{
			if (operation == null)
			{
				return null;
			}
			IEdmOperationParameter edmOperationParameter = operation.Parameters.FirstOrDefault<IEdmOperationParameter>();
			if (!operation.IsBound || edmOperationParameter == null)
			{
				return null;
			}
			IEdmEntityType edmEntityType = null;
			if (!isCollection)
			{
				edmEntityType = edmOperationParameter.Type.Definition as IEdmEntityType;
			}
			else
			{
				IEdmCollectionType edmCollectionType = edmOperationParameter.Type.Definition as IEdmCollectionType;
				if (edmCollectionType != null)
				{
					edmEntityType = edmCollectionType.ElementType.Definition as IEdmEntityType;
				}
			}
			if (edmEntityType == null)
			{
				return null;
			}
			string text = (isCollection ? (operation.Name + "OnCollectionOf" + edmEntityType.Name) : (operation.Name + "On" + edmEntityType.Name));
			return actionMap.FindMatchingAction(new string[] { text, operation.Name });
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000123E8 File Offset: 0x000105E8
		public static bool TryMatch(IDictionary<string, string> templateParameters, IDictionary<string, object> parameters, IDictionary<string, object> matches)
		{
			if (templateParameters.Count != parameters.Count)
			{
				return false;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<string, string> keyValuePair in templateParameters)
			{
				string key = keyValuePair.Key;
				object obj;
				if (!parameters.TryGetValue(key, out obj))
				{
					return false;
				}
				string value = keyValuePair.Value;
				dictionary.Add(value, obj);
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in dictionary)
			{
				matches[keyValuePair2.Key] = keyValuePair2.Value;
			}
			return true;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000124BC File Offset: 0x000106BC
		public static bool TryMatch(this KeySegment keySegment, IDictionary<string, string> mapping, IDictionary<string, object> values)
		{
			if (keySegment.Keys.Count<KeyValuePair<string, object>>() != mapping.Count)
			{
				return false;
			}
			IEdmEntityType edmEntityType = keySegment.EdmType as IEdmEntityType;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			using (IEnumerator<KeyValuePair<string, object>> enumerator = keySegment.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, object> key = enumerator.Current;
					string text;
					if (!mapping.TryGetValue(key.Key, out text))
					{
						return false;
					}
					IEdmTypeReference edmTypeReference;
					if (edmEntityType != null)
					{
						IEdmStructuralProperty edmStructuralProperty = edmEntityType.Key().FirstOrDefault((IEdmStructuralProperty k) => k.Name == key.Key);
						if (edmStructuralProperty == null)
						{
							edmStructuralProperty = edmEntityType.Properties().OfType<IEdmStructuralProperty>().FirstOrDefault((IEdmStructuralProperty p) => p.Name == key.Key);
						}
						edmTypeReference = edmStructuralProperty.Type;
					}
					else
					{
						edmTypeReference = EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(key.Value.GetType());
					}
					RoutingConventionHelpers.AddKeyValues(text, key.Value, edmTypeReference, dictionary, dictionary);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				values[keyValuePair.Key] = keyValuePair.Value;
			}
			return true;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012620 File Offset: 0x00010820
		public static void AddKeyValueToRouteData(this IWebApiControllerContext controllerContext, KeySegment segment, string keyName = "key")
		{
			IDictionary<string, object> routingConventionsStore = controllerContext.Request.Context.RoutingConventionsStore;
			IEdmEntityType edmEntityType = segment.EdmType as IEdmEntityType;
			int num = segment.Keys.Count<KeyValuePair<string, object>>();
			using (IEnumerator<KeyValuePair<string, object>> enumerator = segment.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, object> keyValuePair = enumerator.Current;
					bool flag = false;
					IEdmStructuralProperty edmStructuralProperty = edmEntityType.Key().FirstOrDefault((IEdmStructuralProperty k) => k.Name == keyValuePair.Key);
					if (edmStructuralProperty == null)
					{
						edmStructuralProperty = edmEntityType.Properties().OfType<IEdmStructuralProperty>().FirstOrDefault((IEdmStructuralProperty p) => p.Name == keyValuePair.Key);
						flag = true;
					}
					if (flag || num > 1)
					{
						RoutingConventionHelpers.AddKeyValues(keyName + keyValuePair.Key, keyValuePair.Value, edmStructuralProperty.Type, controllerContext.RouteData, routingConventionsStore);
					}
					else
					{
						RoutingConventionHelpers.AddKeyValues(keyName, keyValuePair.Value, edmStructuralProperty.Type, controllerContext.RouteData, routingConventionsStore);
						if (num == 1)
						{
							RoutingConventionHelpers.AddKeyValues(keyName + keyValuePair.Key, keyValuePair.Value, edmStructuralProperty.Type, controllerContext.RouteData, routingConventionsStore);
						}
					}
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012778 File Offset: 0x00010978
		private static void AddKeyValues(string name, object value, IEdmTypeReference edmTypeReference, IDictionary<string, object> routeValues, IDictionary<string, object> odataValues)
		{
			object obj = null;
			object obj2 = null;
			ConstantNode constantNode = value as ConstantNode;
			if (constantNode != null)
			{
				ODataEnumValue odataEnumValue = constantNode.Value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					obj2 = new ODataParameterValue(odataEnumValue, edmTypeReference);
					obj = odataEnumValue.Value;
				}
			}
			else
			{
				obj2 = new ODataParameterValue(value, edmTypeReference);
				obj = value;
			}
			routeValues[name] = obj;
			string text = "DF908045-6922-46A0-82F2-2F6E7F43D1B1_" + name;
			odataValues[text] = obj2;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000127E0 File Offset: 0x000109E0
		public static void AddFunctionParameterToRouteData(this IWebApiControllerContext controllerContext, OperationSegment functionSegment)
		{
			IDictionary<string, object> routingConventionsStore = controllerContext.Request.Context.RoutingConventionsStore;
			IEdmFunction edmFunction = functionSegment.Operations.First<IEdmOperation>() as IEdmFunction;
			if (edmFunction == null)
			{
				return;
			}
			foreach (OperationSegmentParameter operationSegmentParameter in functionSegment.Parameters)
			{
				string name = operationSegmentParameter.Name;
				object parameterValue = functionSegment.GetParameterValue(name);
				RoutingConventionHelpers.AddFunctionParameters(edmFunction, name, parameterValue, controllerContext.RouteData, routingConventionsStore, null);
			}
			ODataOptionalParameter odataOptionalParameter = new ODataOptionalParameter();
			using (IEnumerator<IEdmOptionalParameter> enumerator2 = edmFunction.Parameters.OfType<IEdmOptionalParameter>().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					IEdmOptionalParameter optionalParameter = enumerator2.Current;
					if (!functionSegment.Parameters.Any((OperationSegmentParameter c) => c.Name == optionalParameter.Name))
					{
						odataOptionalParameter.Add(optionalParameter);
					}
				}
			}
			if (odataOptionalParameter.OptionalParameters.Any<IEdmOptionalParameter>())
			{
				controllerContext.RouteData.Add(ODataRouteConstants.OptionalParameters, odataOptionalParameter);
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00012908 File Offset: 0x00010B08
		public static void AddFunctionParameters(IEdmFunction function, string paramName, object paramValue, IDictionary<string, object> routeData, IDictionary<string, object> values, IDictionary<string, string> paramMapping)
		{
			IEdmOperationParameter edmOperationParameter = function.FindParameter(paramName);
			ODataParameterValue odataParameterValue = new ODataParameterValue(paramValue, edmOperationParameter.Type);
			string text = paramName;
			if (paramMapping != null)
			{
				text = paramMapping[paramName];
			}
			string text2 = "DF908045-6922-46A0-82F2-2F6E7F43D1B1_" + text;
			values[text2] = odataParameterValue;
			if (!routeData.ContainsKey(text))
			{
				routeData.Add(text, paramValue);
			}
			if (paramValue is ODataNullValue)
			{
				routeData[text] = null;
			}
			ODataEnumValue odataEnumValue = paramValue as ODataEnumValue;
			if (odataEnumValue != null)
			{
				routeData[text] = odataEnumValue.Value;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001298C File Offset: 0x00010B8C
		public static IDictionary<string, string> BuildParameterMappings(IEnumerable<OperationSegmentParameter> parameters, string segment)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (OperationSegmentParameter operationSegmentParameter in parameters)
			{
				string name = operationSegmentParameter.Name;
				string text = null;
				ConstantNode constantNode = operationSegmentParameter.Value as ConstantNode;
				if (constantNode != null)
				{
					UriTemplateExpression uriTemplateExpression = constantNode.Value as UriTemplateExpression;
					if (uriTemplateExpression != null)
					{
						text = uriTemplateExpression.LiteralText.Trim();
					}
				}
				else
				{
					text = operationSegmentParameter.Value as string;
				}
				if (text == null || !RoutingConventionHelpers.IsRouteParameter(text))
				{
					throw new ODataException(Error.Format(SRResources.ParameterAliasMustBeInCurlyBraces, new object[] { operationSegmentParameter.Value, segment }));
				}
				text = text.Substring(1, text.Length - 2);
				if (string.IsNullOrEmpty(text))
				{
					throw new ODataException(Error.Format(SRResources.EmptyParameterAlias, new object[] { operationSegmentParameter.Value, segment }));
				}
				dictionary[name] = text;
			}
			return dictionary;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00012A9C File Offset: 0x00010C9C
		public static bool IsRouteParameter(string parameterName)
		{
			return parameterName.StartsWith("{", StringComparison.Ordinal) && parameterName.EndsWith("}", StringComparison.Ordinal);
		}
	}
}
