using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200030F RID: 783
	internal sealed class ScalarType : SchemaType
	{
		// Token: 0x06002518 RID: 9496 RVA: 0x0006975A File Offset: 0x0006795A
		internal ScalarType(Schema parentElement, string typeName, PrimitiveType primitiveType)
			: base(parentElement)
		{
			this.Name = typeName;
			this._primitiveType = primitiveType;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00069774 File Offset: 0x00067974
		public bool TryParse(string text, out object value)
		{
			switch (this._primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return ScalarType.TryParseBinary(text, out value);
			case PrimitiveTypeKind.Boolean:
				return ScalarType.TryParseBoolean(text, out value);
			case PrimitiveTypeKind.Byte:
				return ScalarType.TryParseByte(text, out value);
			case PrimitiveTypeKind.DateTime:
				return ScalarType.TryParseDateTime(text, out value);
			case PrimitiveTypeKind.Decimal:
				return ScalarType.TryParseDecimal(text, out value);
			case PrimitiveTypeKind.Double:
				return ScalarType.TryParseDouble(text, out value);
			case PrimitiveTypeKind.Guid:
				return ScalarType.TryParseGuid(text, out value);
			case PrimitiveTypeKind.Single:
				return ScalarType.TryParseSingle(text, out value);
			case PrimitiveTypeKind.SByte:
				return ScalarType.TryParseSByte(text, out value);
			case PrimitiveTypeKind.Int16:
				return ScalarType.TryParseInt16(text, out value);
			case PrimitiveTypeKind.Int32:
				return ScalarType.TryParseInt32(text, out value);
			case PrimitiveTypeKind.Int64:
				return ScalarType.TryParseInt64(text, out value);
			case PrimitiveTypeKind.String:
				return ScalarType.TryParseString(text, out value);
			case PrimitiveTypeKind.Time:
				return ScalarType.TryParseTime(text, out value);
			case PrimitiveTypeKind.DateTimeOffset:
				return ScalarType.TryParseDateTimeOffset(text, out value);
			default:
				throw new NotSupportedException(this._primitiveType.FullName);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00069859 File Offset: 0x00067A59
		public PrimitiveTypeKind TypeKind
		{
			get
			{
				return this._primitiveType.PrimitiveTypeKind;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x00069866 File Offset: 0x00067A66
		public PrimitiveType Type
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00069870 File Offset: 0x00067A70
		private static bool TryParseBoolean(string text, out object value)
		{
			bool flag;
			if (!bool.TryParse(text, out flag))
			{
				value = null;
				return false;
			}
			value = flag;
			return true;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00069898 File Offset: 0x00067A98
		private static bool TryParseByte(string text, out object value)
		{
			byte b;
			if (!byte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out b))
			{
				value = null;
				return false;
			}
			value = b;
			return true;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000698C4 File Offset: 0x00067AC4
		private static bool TryParseSByte(string text, out object value)
		{
			sbyte b;
			if (!sbyte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out b))
			{
				value = null;
				return false;
			}
			value = b;
			return true;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000698F0 File Offset: 0x00067AF0
		private static bool TryParseInt16(string text, out object value)
		{
			short num;
			if (!short.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0006991C File Offset: 0x00067B1C
		private static bool TryParseInt32(string text, out object value)
		{
			int num;
			if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00069948 File Offset: 0x00067B48
		private static bool TryParseInt64(string text, out object value)
		{
			long num;
			if (!long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00069974 File Offset: 0x00067B74
		private static bool TryParseDouble(string text, out object value)
		{
			double num;
			if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000699A4 File Offset: 0x00067BA4
		private static bool TryParseDecimal(string text, out object value)
		{
			decimal num;
			if (!decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000699D4 File Offset: 0x00067BD4
		private static bool TryParseDateTime(string text, out object value)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, "yyyy-MM-dd HH\\:mm\\:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				value = null;
				return false;
			}
			value = dateTime;
			return true;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00069A08 File Offset: 0x00067C08
		private static bool TryParseTime(string text, out object value)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, "HH\\:mm\\:ss.fffffffZ", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				value = null;
				return false;
			}
			value = new TimeSpan(dateTime.Ticks);
			return true;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00069A44 File Offset: 0x00067C44
		private static bool TryParseDateTimeOffset(string text, out object value)
		{
			DateTimeOffset dateTimeOffset;
			if (!DateTimeOffset.TryParse(text, out dateTimeOffset))
			{
				value = null;
				return false;
			}
			value = dateTimeOffset;
			return true;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x00069A69 File Offset: 0x00067C69
		private static bool TryParseGuid(string text, out object value)
		{
			if (!ScalarType._guidValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			value = new Guid(text);
			return true;
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00069A8B File Offset: 0x00067C8B
		private static bool TryParseString(string text, out object value)
		{
			value = text;
			return true;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00069A94 File Offset: 0x00067C94
		private static bool TryParseBinary(string text, out object value)
		{
			if (!ScalarType._binaryValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			string text2 = text.Substring(2);
			value = ScalarType.ConvertToByteArray(text2);
			return true;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00069AC4 File Offset: 0x00067CC4
		internal static byte[] ConvertToByteArray(string text)
		{
			int num = 2;
			int num2 = text.Length / 2;
			if (text.Length % 2 == 1)
			{
				num = 1;
				num2++;
			}
			byte[] array = new byte[num2];
			int i = 0;
			int num3 = 0;
			while (i < text.Length)
			{
				array[num3] = byte.Parse(text.Substring(i, num), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				i += num;
				num = 2;
				num3++;
			}
			return array;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00069B2C File Offset: 0x00067D2C
		private static bool TryParseSingle(string text, out object value)
		{
			float num;
			if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x04000D19 RID: 3353
		internal const string DateTimeFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffZ";

		// Token: 0x04000D1A RID: 3354
		internal const string TimeFormat = "HH\\:mm\\:ss.fffffffZ";

		// Token: 0x04000D1B RID: 3355
		internal const string DateTimeOffsetFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffffffz";

		// Token: 0x04000D1C RID: 3356
		private static readonly Regex _binaryValueValidator = new Regex("^0[xX][0-9a-fA-F]+$", RegexOptions.Compiled);

		// Token: 0x04000D1D RID: 3357
		private static readonly Regex _guidValueValidator = new Regex("[0-9a-fA-F]{8,8}(-[0-9a-fA-F]{4,4}){3,3}-[0-9a-fA-F]{12,12}", RegexOptions.Compiled);

		// Token: 0x04000D1E RID: 3358
		private readonly PrimitiveType _primitiveType;
	}
}
