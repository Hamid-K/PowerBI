using System;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3.Compiler
{
	// Token: 0x020008F2 RID: 2290
	internal static class LiteralConverter
	{
		// Token: 0x0600414C RID: 16716 RVA: 0x000DAB3C File Offset: 0x000D8D3C
		public static bool AreTrueFalseLiterals(params QueryToken[] expressions)
		{
			for (int i = 0; i < expressions.Length; i++)
			{
				if (expressions[i] != LiteralConverter.LiteralTokenTrue || expressions[i] != LiteralConverter.LiteralTokenFalse)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000DAB6E File Offset: 0x000D8D6E
		public static bool CanConvert(TypeValue type)
		{
			return TypeServices.IsScalar(type);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000DAB78 File Offset: 0x000D8D78
		public static LiteralQueryToken Convert(Value value, TypeValue type)
		{
			if (value.IsLogical && value.AsBoolean)
			{
				return LiteralConverter.LiteralTokenTrue;
			}
			if (value.IsLogical && !value.AsBoolean)
			{
				return LiteralConverter.LiteralTokenFalse;
			}
			if (value.IsNull)
			{
				return LiteralConverter.LiteralTokenNull;
			}
			if (type.NonNullable.Equals(TypeValue.Guid))
			{
				try
				{
					return new LiteralQueryToken(new Guid(value.AsString));
				}
				catch (Exception)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.Guid_FromFunction_NotConvertibleToGuid, value, null);
				}
			}
			return new LiteralQueryToken(ValueMarshaller.MarshalToClr(value, type.NonNullable));
		}

		// Token: 0x04002257 RID: 8791
		public static readonly LiteralQueryToken LiteralTokenEmptyString = new LiteralQueryToken(string.Empty);

		// Token: 0x04002258 RID: 8792
		public static readonly LiteralQueryToken LiteralTokenNull = new LiteralQueryToken(null);

		// Token: 0x04002259 RID: 8793
		public static readonly LiteralQueryToken LiteralTokenTrue = new LiteralQueryToken(true);

		// Token: 0x0400225A RID: 8794
		public static readonly LiteralQueryToken LiteralTokenFalse = new LiteralQueryToken(false);
	}
}
