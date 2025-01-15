using System;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureTables
{
	// Token: 0x02000EB6 RID: 3766
	internal static class AzureTablesHelper
	{
		// Token: 0x0600641D RID: 25629 RVA: 0x0015680C File Offset: 0x00154A0C
		public static BinaryOperatorKind GetBinaryOperatorKind(BinaryOperator2 operator2)
		{
			BinaryOperatorKind binaryOperatorKind;
			switch (operator2)
			{
			case BinaryOperator2.GreaterThan:
				binaryOperatorKind = BinaryOperatorKind.GreaterThan;
				break;
			case BinaryOperator2.LessThan:
				binaryOperatorKind = BinaryOperatorKind.LessThan;
				break;
			case BinaryOperator2.GreaterThanOrEquals:
				binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
				break;
			case BinaryOperator2.LessThanOrEquals:
				binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
				break;
			case BinaryOperator2.Equals:
				binaryOperatorKind = BinaryOperatorKind.Equal;
				break;
			case BinaryOperator2.NotEquals:
				binaryOperatorKind = BinaryOperatorKind.NotEqual;
				break;
			case BinaryOperator2.And:
				binaryOperatorKind = BinaryOperatorKind.And;
				break;
			case BinaryOperator2.Or:
				binaryOperatorKind = BinaryOperatorKind.Or;
				break;
			default:
				throw new InvalidOperationException();
			}
			return binaryOperatorKind;
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0015686A File Offset: 0x00154A6A
		public static bool IsSupportedOperator(BinaryOperator2 binaryOperator)
		{
			return binaryOperator == BinaryOperator2.Equals || binaryOperator == BinaryOperator2.GreaterThan || binaryOperator == BinaryOperator2.GreaterThanOrEquals || binaryOperator == BinaryOperator2.LessThan || binaryOperator == BinaryOperator2.LessThanOrEquals || binaryOperator == BinaryOperator2.NotEquals || binaryOperator == BinaryOperator2.And || binaryOperator == BinaryOperator2.Or;
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x00156894 File Offset: 0x00154A94
		public static BinaryOperator2 ReverseOperator(BinaryOperator2 @operator)
		{
			switch (@operator)
			{
			case BinaryOperator2.GreaterThan:
				return BinaryOperator2.LessThan;
			case BinaryOperator2.LessThan:
				return BinaryOperator2.GreaterThan;
			case BinaryOperator2.GreaterThanOrEquals:
				return BinaryOperator2.LessThanOrEquals;
			case BinaryOperator2.LessThanOrEquals:
				return BinaryOperator2.GreaterThanOrEquals;
			case BinaryOperator2.Equals:
				return BinaryOperator2.Equals;
			case BinaryOperator2.NotEquals:
				return BinaryOperator2.NotEquals;
			case BinaryOperator2.And:
				return BinaryOperator2.And;
			case BinaryOperator2.Or:
				return BinaryOperator2.Or;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x001568E3 File Offset: 0x00154AE3
		public static RecordValue CreateHeaders()
		{
			return RecordValue.New(AzureTablesHelper.commonTableKeys, new Value[] { AzureTablesHelper.April2015Version });
		}

		// Token: 0x040036A5 RID: 13989
		private static readonly TextValue April2015Version = TextValue.New("2015-04-05");

		// Token: 0x040036A6 RID: 13990
		private static readonly Keys commonTableKeys = Keys.New("x-ms-version");
	}
}
