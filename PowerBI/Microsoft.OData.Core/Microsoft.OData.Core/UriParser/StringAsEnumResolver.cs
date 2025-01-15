using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000146 RID: 326
	public sealed class StringAsEnumResolver : ODataUriResolver
	{
		// Token: 0x060010CD RID: 4301 RVA: 0x0002F07C File Offset: 0x0002D27C
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

		// Token: 0x060010CE RID: 4302 RVA: 0x0002F174 File Offset: 0x0002D374
		public override IDictionary<IEdmOperationParameter, SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, SingleValueNode> input)
		{
			Dictionary<IEdmOperationParameter, SingleValueNode> dictionary = new Dictionary<IEdmOperationParameter, SingleValueNode>(EqualityComparer<IEdmOperationParameter>.Default);
			foreach (KeyValuePair<string, SingleValueNode> keyValuePair in input)
			{
				IEdmOperationParameter edmOperationParameter;
				if (this.EnableCaseInsensitive)
				{
					edmOperationParameter = ODataUriResolver.ResolveOperationParameterNameCaseInsensitive(operation, keyValuePair.Key);
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

		// Token: 0x060010CF RID: 4303 RVA: 0x0002F290 File Offset: 0x0002D490
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IList<string> positionalValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			return base.ResolveKeys(type, positionalValues, delegate(IEdmTypeReference typeRef, string valueText)
			{
				if (typeRef.IsEnum() && valueText.StartsWith("'", StringComparison.Ordinal) && valueText.EndsWith("'", StringComparison.Ordinal))
				{
					valueText = typeRef.FullName() + valueText;
				}
				return convertFunc(typeRef, valueText);
			});
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
		public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
		{
			return base.ResolveKeys(type, namedValues, delegate(IEdmTypeReference typeRef, string valueText)
			{
				if (typeRef.IsEnum() && valueText.StartsWith("'", StringComparison.Ordinal) && valueText.EndsWith("'", StringComparison.Ordinal))
				{
					valueText = typeRef.FullName() + valueText;
				}
				return convertFunc(typeRef, valueText);
			});
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
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
