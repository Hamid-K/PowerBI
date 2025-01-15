using System;
using System.Globalization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000008 RID: 8
	internal static class TypeConversionUtil
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020C8 File Offset: 0x000002C8
		internal static int GetLengthForType(ClrTypeCode dataType)
		{
			switch (dataType)
			{
			case ClrTypeCode.Object:
				return 8;
			case ClrTypeCode.Boolean:
			case ClrTypeCode.SByte:
			case ClrTypeCode.Byte:
				return 1;
			case ClrTypeCode.Int16:
			case ClrTypeCode.UInt16:
			case ClrTypeCode.Char:
				return 2;
			case ClrTypeCode.Int32:
			case ClrTypeCode.UInt32:
			case ClrTypeCode.Single:
			case ClrTypeCode.String:
			case ClrTypeCode.ByteArray:
			case ClrTypeCode.CharArray:
				return 4;
			case ClrTypeCode.Int64:
			case ClrTypeCode.UInt64:
			case ClrTypeCode.Double:
			case ClrTypeCode.DateTime:
			case ClrTypeCode.TimeSpan:
				return 8;
			case ClrTypeCode.Decimal:
			case ClrTypeCode.DateTimeOffset:
			case ClrTypeCode.Guid:
				return 16;
			default:
				RuntimeChecks.Fail(string.Format(CultureInfo.InvariantCulture, "Unknown type {0}", dataType));
				return 0;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000215B File Offset: 0x0000035B
		internal static bool IsVariableLengthType(ClrTypeCode dataType)
		{
			return dataType == ClrTypeCode.String || dataType == ClrTypeCode.ByteArray || dataType == ClrTypeCode.CharArray || dataType == ClrTypeCode.Object;
		}
	}
}
