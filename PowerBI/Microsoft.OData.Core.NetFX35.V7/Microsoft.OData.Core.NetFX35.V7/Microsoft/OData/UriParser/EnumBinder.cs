using System;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E5 RID: 229
	internal sealed class EnumBinder
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		internal static bool TryBindDottedIdentifierAsEnum(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, BindingState state, out QueryNode boundEnum)
		{
			return EnumBinder.TryBindIdentifier(dottedIdentifierToken.Identifier, null, state.Model, out boundEnum);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0001C3F0 File Offset: 0x0001A5F0
		internal static bool TryBindIdentifier(string identifier, IEdmEnumTypeReference typeReference, IEdmModel modelWhenNoTypeReference, out QueryNode boundEnum)
		{
			boundEnum = null;
			string text = identifier;
			int num = text.IndexOf('\'');
			if (num < 0)
			{
				return false;
			}
			string text2 = text.Substring(0, num);
			if (typeReference != null && !string.Equals(text2, typeReference.FullName()))
			{
				return false;
			}
			IEdmEnumType edmEnumType = ((typeReference != null) ? ((IEdmEnumType)typeReference.Definition) : UriEdmHelpers.FindEnumTypeFromModel(modelWhenNoTypeReference, text2));
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

		// Token: 0x06000B76 RID: 2934 RVA: 0x0001C488 File Offset: 0x0001A688
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
