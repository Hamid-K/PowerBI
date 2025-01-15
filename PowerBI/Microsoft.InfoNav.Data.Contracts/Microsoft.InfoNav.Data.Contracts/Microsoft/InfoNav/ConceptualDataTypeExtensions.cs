using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002D RID: 45
	internal static class ConceptualDataTypeExtensions
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000288B File Offset: 0x00000A8B
		internal static bool IsNumeric(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.Decimal || type == ConceptualPrimitiveType.Double || type == ConceptualPrimitiveType.Integer;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000289B File Offset: 0x00000A9B
		internal static bool IsDateOrTime(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.Date || type == ConceptualPrimitiveType.DateTime || type == ConceptualPrimitiveType.DateTimeZone || type == ConceptualPrimitiveType.Time;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000028B0 File Offset: 0x00000AB0
		internal static bool IsDateTime(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.DateTime;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000028B6 File Offset: 0x00000AB6
		internal static bool IsText(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.Text;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000028BC File Offset: 0x00000ABC
		internal static bool IsDouble(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.Double;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000028C2 File Offset: 0x00000AC2
		internal static bool IsScalar(this ConceptualPrimitiveType type)
		{
			return type.IsNumeric() || type.IsDateOrTime();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal static bool IsBoolean(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.Boolean;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000028DA File Offset: 0x00000ADA
		internal static bool IsInteger(this ConceptualPrimitiveType type)
		{
			return type == ConceptualPrimitiveType.Integer;
		}
	}
}
