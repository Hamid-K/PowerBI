using System;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000107 RID: 263
	internal static class PrimitiveValueUtils
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		internal static string ConvertToPrimitiveLiteral(object value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type type = value.GetType();
			type = Nullable.GetUnderlyingType(type) ?? type;
			if (Type.GetTypeCode(type) == TypeCode.Int32)
			{
				stringBuilder.Append(XmlConvert.ToString((int)value));
				return stringBuilder.ToString();
			}
			return PrimitiveValueEncoding.ToTypeEncodedString(value);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000F414 File Offset: 0x0000D614
		internal static bool TryParseFromPrimitiveLiteral(string content, out object value)
		{
			PrimitiveValue primitiveValue;
			if (PrimitiveValueEncoding.TryParseTypeEncodedString(content, out primitiveValue))
			{
				value = primitiveValue.GetValueAsObject();
				return true;
			}
			int num;
			if (int.TryParse(content, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
			{
				value = num;
				return true;
			}
			value = null;
			return false;
		}
	}
}
