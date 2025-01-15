using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Parsers.UriParsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C7 RID: 455
	internal sealed class EnumBinder
	{
		// Token: 0x060010F9 RID: 4345 RVA: 0x0003B386 File Offset: 0x00039586
		internal EnumBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003B395 File Offset: 0x00039595
		internal static bool TryBindDottedIdentifierAsEnum(DottedIdentifierToken dottedIdentifierToken, SingleValueNode parent, BindingState state, out QueryNode boundEnum)
		{
			return EnumBinder.TryBindIdentifier(dottedIdentifierToken.Identifier, null, state.Model, out boundEnum);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003B3AC File Offset: 0x000395AC
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri Parser does not need to go through the ODL behavior knob.")]
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

		// Token: 0x060010FC RID: 4348 RVA: 0x0003B444 File Offset: 0x00039644
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

		// Token: 0x0400077F RID: 1919
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
