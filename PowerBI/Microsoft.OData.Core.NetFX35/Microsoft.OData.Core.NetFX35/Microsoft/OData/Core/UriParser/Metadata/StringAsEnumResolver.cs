using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Metadata
{
	// Token: 0x020001EB RID: 491
	public sealed class StringAsEnumResolver : ODataUriResolver
	{
		// Token: 0x060011E2 RID: 4578 RVA: 0x00040AC0 File Offset: 0x0003ECC0
		public override void PromoteBinaryOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode leftNode, ref SingleValueNode rightNode, out IEdmTypeReference typeReference)
		{
			typeReference = null;
			if (leftNode.TypeReference != null && rightNode.TypeReference != null)
			{
				if (leftNode.TypeReference.IsEnum() && rightNode.TypeReference.IsString() && rightNode is ConstantNode)
				{
					string text = ((ConstantNode)rightNode).Value as string;
					IEdmTypeReference typeReference2 = leftNode.TypeReference;
					ODataEnumValue odataEnumValue;
					if (StringAsEnumResolver.TryParseEnum(typeReference2.Definition as IEdmEnumType, text, out odataEnumValue))
					{
						rightNode = new ConstantNode(odataEnumValue, text, typeReference2);
						return;
					}
				}
				else if (rightNode.TypeReference.IsEnum() && leftNode.TypeReference.IsString() && leftNode is ConstantNode)
				{
					string text2 = ((ConstantNode)leftNode).Value as string;
					IEdmTypeReference typeReference3 = rightNode.TypeReference;
					ODataEnumValue odataEnumValue2;
					if (StringAsEnumResolver.TryParseEnum(typeReference3.Definition as IEdmEnumType, text2, out odataEnumValue2))
					{
						leftNode = new ConstantNode(odataEnumValue2, text2, typeReference3);
						return;
					}
				}
			}
			base.PromoteBinaryOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode, out typeReference);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Parameter type is needed here to check whether can be convert from source")]
		public override IDictionary<IEdmOperationParameter, SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, SingleValueNode> input)
		{
			Dictionary<IEdmOperationParameter, SingleValueNode> dictionary = new Dictionary<IEdmOperationParameter, SingleValueNode>(EqualityComparer<IEdmOperationParameter>.Default);
			foreach (KeyValuePair<string, SingleValueNode> keyValuePair in input)
			{
				IEdmOperationParameter edmOperationParameter;
				if (this.EnableCaseInsensitive)
				{
					edmOperationParameter = ODataUriResolver.ResolveOpearationParameterNameCaseInsensitive(operation, keyValuePair.Key);
				}
				else
				{
					edmOperationParameter = operation.FindParameter(keyValuePair.Key);
				}
				if (edmOperationParameter == null)
				{
					throw new ODataException(Strings.ODataParameterWriterCore_ParameterNameNotFoundInOperation(keyValuePair.Key, operation.Name));
				}
				SingleValueNode singleValueNode = keyValuePair.Value;
				if (edmOperationParameter.Type.IsEnum() && singleValueNode is ConstantNode && singleValueNode.TypeReference != null && singleValueNode.TypeReference.IsString())
				{
					string text = ((ConstantNode)keyValuePair.Value).Value as string;
					IEdmTypeReference type = edmOperationParameter.Type;
					ODataEnumValue odataEnumValue;
					if (StringAsEnumResolver.TryParseEnum(type.Definition as IEdmEnumType, text, out odataEnumValue))
					{
						singleValueNode = new ConstantNode(odataEnumValue, text, type);
					}
				}
				dictionary.Add(edmOperationParameter, singleValueNode);
			}
			return dictionary;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00040D24 File Offset: 0x0003EF24
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IList<string> positionalValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			return base.ResolveKeys(type, positionalValues, delegate(IEdmTypeReference typeRef, string valueText)
			{
				if (typeRef.IsEnum() && valueText.StartsWith("'", 4) && valueText.EndsWith("'", 4))
				{
					valueText = typeRef.FullName() + valueText;
				}
				return convertFunc.Invoke(typeRef, valueText);
			});
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00040DA8 File Offset: 0x0003EFA8
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			return base.ResolveKeys(type, namedValues, delegate(IEdmTypeReference typeRef, string valueText)
			{
				if (typeRef.IsEnum() && valueText.StartsWith("'", 4) && valueText.EndsWith("'", 4))
				{
					valueText = typeRef.FullName() + valueText;
				}
				return convertFunc.Invoke(typeRef, valueText);
			});
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00040DD8 File Offset: 0x0003EFD8
		private static bool TryParseEnum(IEdmEnumType enumType, string value, out ODataEnumValue enumValue)
		{
			long num;
			bool flag = enumType.TryParseEnum(value, true, out num);
			enumValue = null;
			if (flag)
			{
				enumValue = new ODataEnumValue(num.ToString(CultureInfo.InvariantCulture), enumType.FullTypeName());
			}
			return flag;
		}
	}
}
