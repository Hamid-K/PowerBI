using System;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000123 RID: 291
	internal sealed class EnumBinder
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x00027B70 File Offset: 0x00025D70
		internal static bool TryBindDottedIdentifierAsEnum(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, BindingState state, ODataUriResolver resolver, out QueryNode boundEnum)
		{
			return EnumBinder.TryBindIdentifier(dottedIdentifierToken.Identifier, null, state.Model, resolver, out boundEnum);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00027B87 File Offset: 0x00025D87
		internal static bool TryBindIdentifier(string identifier, IEdmEnumTypeReference typeReference, IEdmModel modelWhenNoTypeReference, out QueryNode boundEnum)
		{
			return EnumBinder.TryBindIdentifier(identifier, typeReference, modelWhenNoTypeReference, null, out boundEnum);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00027B94 File Offset: 0x00025D94
		internal static bool TryBindIdentifier(string identifier, IEdmEnumTypeReference typeReference, IEdmModel modelWhenNoTypeReference, ODataUriResolver resolver, out QueryNode boundEnum)
		{
			boundEnum = null;
			string text = identifier;
			int num = text.IndexOf('\'');
			if (num < 0)
			{
				return false;
			}
			string text2 = text.Substring(0, num);
			if (typeReference != null && !string.Equals(text2, typeReference.FullName(), StringComparison.Ordinal))
			{
				return false;
			}
			IEdmEnumType edmEnumType = ((typeReference != null) ? ((IEdmEnumType)typeReference.Definition) : UriEdmHelpers.FindEnumTypeFromModel(modelWhenNoTypeReference, text2, resolver));
			if (edmEnumType == null)
			{
				return false;
			}
			UriParserHelper.TryRemovePrefix(text2, ref text);
			UriParserHelper.TryRemoveQuotes(ref text);
			string text3 = text;
			ODataEnumValue odataEnumValue;
			if (!EnumBinder.TryParseEnum(edmEnumType, text3, out odataEnumValue))
			{
				return false;
			}
			IEdmEnumTypeReference edmEnumTypeReference = typeReference ?? new EdmEnumTypeReference(edmEnumType, false);
			boundEnum = new ConstantNode(odataEnumValue, identifier, edmEnumTypeReference);
			return true;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00027C30 File Offset: 0x00025E30
		internal static bool TryParseEnum(IEdmEnumType enumType, string value, out ODataEnumValue enumValue)
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
