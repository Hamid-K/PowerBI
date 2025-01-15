using System;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000007 RID: 7
	internal static class ConceptualTypeConverter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020C8 File Offset: 0x000002C8
		internal static ConceptualPrimitiveType ToConceptualType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					return ConceptualPrimitiveType.Binary;
				}
				if (type == typeof(object))
				{
					return ConceptualPrimitiveType.Variant;
				}
				break;
			case TypeCode.Boolean:
				return ConceptualPrimitiveType.Boolean;
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
				return ConceptualPrimitiveType.Integer;
			case TypeCode.Double:
				return ConceptualPrimitiveType.Double;
			case TypeCode.Decimal:
				return ConceptualPrimitiveType.Decimal;
			case TypeCode.DateTime:
				return ConceptualPrimitiveType.DateTime;
			case TypeCode.String:
				return ConceptualPrimitiveType.Text;
			}
			throw new InvalidOperationException(StringUtil.FormatInvariant("Attempt to convert unsupported type {0}", new object[] { type }));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000217D File Offset: 0x0000037D
		internal static bool IsVariant(Type type)
		{
			return ConceptualTypeConverter.ToConceptualType(type) == ConceptualPrimitiveType.Variant;
		}
	}
}
