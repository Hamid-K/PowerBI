using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000072 RID: 114
	internal static class ODataPathSegmentExtensions
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public static string TranslatePathTemplateSegment(this PathTemplateSegment pathTemplatesegment, out string value)
		{
			if (pathTemplatesegment == null)
			{
				throw Error.ArgumentNull("pathTemplatesegment");
			}
			string literalText = pathTemplatesegment.LiteralText;
			if (literalText == null)
			{
				throw new ODataException(Error.Format(SRResources.InvalidAttributeRoutingTemplateSegment, new object[] { string.Empty }));
			}
			if (!literalText.StartsWith("{", StringComparison.Ordinal) || !literalText.EndsWith("}", StringComparison.Ordinal))
			{
				value = string.Empty;
				return string.Empty;
			}
			string[] array = literalText.Substring(1, literalText.Length - 2).Split(new char[] { ':' });
			if (array.Length != 2)
			{
				throw new ODataException(Error.Format(SRResources.InvalidAttributeRoutingTemplateSegment, new object[] { literalText }));
			}
			value = "{" + array[0] + "}";
			return array[1];
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000DE1B File Offset: 0x0000C01B
		public static string ToUriLiteral(this MetadataSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return "$metadata";
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000DE30 File Offset: 0x0000C030
		public static string ToUriLiteral(this ValueSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return "$value";
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000DE45 File Offset: 0x0000C045
		public static string ToUriLiteral(this BatchSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return "$batch";
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000DE5A File Offset: 0x0000C05A
		public static string ToUriLiteral(this CountSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return "$count";
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000DE70 File Offset: 0x0000C070
		public static string ToUriLiteral(this TypeSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			IEdmType edmType = segment.EdmType;
			if (segment.EdmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)segment.EdmType).ElementType.Definition;
			}
			return edmType.FullTypeName();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public static string ToUriLiteral(this SingletonSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.Singleton.Name;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000DED7 File Offset: 0x0000C0D7
		public static string ToUriLiteral(this PropertySegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.Property.Name;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000DEF2 File Offset: 0x0000C0F2
		public static string ToUriLiteral(this PathTemplateSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.LiteralText;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000DF08 File Offset: 0x0000C108
		public static string ToUriLiteral(this DynamicPathSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.Identifier;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000DF1E File Offset: 0x0000C11E
		public static string ToUriLiteral(this NavigationPropertySegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.NavigationProperty.Name;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000DF39 File Offset: 0x0000C139
		public static string ToUriLiteral(this NavigationPropertyLinkSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.NavigationProperty.Name + "/$ref";
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000DF5E File Offset: 0x0000C15E
		public static string ToUriLiteral(this KeySegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return ODataPathSegmentExtensions.ConvertKeysToUriLiteral(segment.Keys, segment.EdmType);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000DF7F File Offset: 0x0000C17F
		public static string ToUriLiteral(this EntitySetSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.EntitySet.Name;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000DF9C File Offset: 0x0000C19C
		public static string ToUriLiteral(this OperationSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			IEdmAction edmAction = segment.Operations.Single<IEdmOperation>() as IEdmAction;
			if (edmAction != null)
			{
				return edmAction.FullName();
			}
			IEnumerable<KeyValuePair<string, string>> enumerable = segment.Parameters.ToDictionary((OperationSegmentParameter parameterValue) => parameterValue.Name, (OperationSegmentParameter parameterValue) => ODataPathSegmentExtensions.TranslateNode(parameterValue.Value));
			IEdmFunction edmFunction = (IEdmFunction)segment.Operations.Single<IEdmOperation>();
			IEnumerable<string> enumerable2 = enumerable.Select((KeyValuePair<string, string> v) => string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[] { v.Key, v.Value }));
			return string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				edmFunction.FullName(),
				string.Join(",", enumerable2)
			});
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E080 File Offset: 0x0000C280
		public static string ToUriLiteral(this OperationImportSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			IEdmActionImport edmActionImport = segment.OperationImports.Single<IEdmOperationImport>() as IEdmActionImport;
			if (edmActionImport != null)
			{
				return edmActionImport.Name;
			}
			IEnumerable<KeyValuePair<string, string>> enumerable = segment.Parameters.ToDictionary((OperationSegmentParameter parameterValue) => parameterValue.Name, (OperationSegmentParameter parameterValue) => ODataPathSegmentExtensions.TranslateNode(parameterValue.Value));
			IEdmFunctionImport edmFunctionImport = (IEdmFunctionImport)segment.OperationImports.Single<IEdmOperationImport>();
			IEnumerable<string> enumerable2 = enumerable.Select((KeyValuePair<string, string> v) => string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[] { v.Key, v.Value }));
			return string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				edmFunctionImport.Name,
				string.Join(",", enumerable2)
			});
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E161 File Offset: 0x0000C361
		public static string ToUriLiteral(this UnresolvedPathSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			return segment.SegmentValue;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E178 File Offset: 0x0000C378
		private static string ConvertKeysToUriLiteral(IEnumerable<KeyValuePair<string, object>> keys, IEdmType edmType)
		{
			IEdmEntityType edmEntityType = edmType as IEdmEntityType;
			if (keys.Count<KeyValuePair<string, object>>() < 2)
			{
				KeyValuePair<string, object> keyValue = keys.First<KeyValuePair<string, object>>();
				if (edmEntityType.Key().Any((IEdmStructuralProperty k) => k.Name == keyValue.Key))
				{
					return string.Join(",", keys.Select((KeyValuePair<string, object> keyValuePair) => ODataPathSegmentExtensions.TranslateKeySegmentValue(keyValuePair.Value)).ToArray<string>());
				}
			}
			return string.Join(",", keys.Select((KeyValuePair<string, object> keyValuePair) => keyValuePair.Key + "=" + ODataPathSegmentExtensions.TranslateKeySegmentValue(keyValuePair.Value)).ToArray<string>());
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E22C File Offset: 0x0000C42C
		private static string TranslateKeySegmentValue(object value)
		{
			if (value == null)
			{
				throw Error.ArgumentNull("value");
			}
			UriTemplateExpression uriTemplateExpression = value as UriTemplateExpression;
			if (uriTemplateExpression != null)
			{
				return uriTemplateExpression.LiteralText;
			}
			ConstantNode constantNode = value as ConstantNode;
			if (constantNode != null)
			{
				ODataEnumValue odataEnumValue = constantNode.Value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					return ODataUriUtils.ConvertToUriLiteral(odataEnumValue, ODataVersion.V4);
				}
			}
			return ODataUriUtils.ConvertToUriLiteral(value, ODataVersion.V4);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E280 File Offset: 0x0000C480
		private static string TranslateNode(object node)
		{
			if (node == null)
			{
				throw Error.ArgumentNull("node");
			}
			ConstantNode constantNode = node as ConstantNode;
			if (constantNode != null)
			{
				UriTemplateExpression uriTemplateExpression = constantNode.Value as UriTemplateExpression;
				if (uriTemplateExpression != null)
				{
					return uriTemplateExpression.LiteralText;
				}
				ODataEnumValue odataEnumValue = constantNode.Value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					return ODataUriUtils.ConvertToUriLiteral(odataEnumValue, ODataVersion.V4);
				}
				return constantNode.LiteralText;
			}
			else
			{
				ConvertNode convertNode = node as ConvertNode;
				if (convertNode != null)
				{
					return ODataPathSegmentExtensions.TranslateNode(convertNode.Source);
				}
				throw Error.NotSupported(SRResources.CannotRecognizeNodeType, new object[]
				{
					typeof(ODataPathSegmentTranslator),
					node.GetType().FullName
				});
			}
		}
	}
}
