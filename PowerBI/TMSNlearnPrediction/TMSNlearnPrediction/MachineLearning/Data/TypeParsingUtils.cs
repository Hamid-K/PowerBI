using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000393 RID: 915
	public static class TypeParsingUtils
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x0007008C File Offset: 0x0006E28C
		public static bool TryParseDataKind(string str, out DataKind dataKind, out KeyRange keyRange)
		{
			Contracts.CheckValue<string>(str, "str");
			keyRange = null;
			dataKind = 0;
			int num = str.IndexOf('[');
			if (num >= 0)
			{
				if (str[str.Length - 1] != ']')
				{
					return false;
				}
				keyRange = KeyRange.Parse(str.Substring(num + 1, str.Length - num - 2));
				if (keyRange == null)
				{
					return false;
				}
				if (num == 0)
				{
					return true;
				}
				str = str.Substring(0, num);
			}
			DataKind dataKind2;
			if (!Enum.TryParse<DataKind>(str, true, out dataKind2))
			{
				return false;
			}
			dataKind = dataKind2;
			return true;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0007010C File Offset: 0x0006E30C
		public static KeyType ConstructKeyType(DataKind? type, KeyRange range)
		{
			Contracts.CheckValue<KeyRange>(range, "range");
			DataKind dataKind = type ?? 6;
			Contracts.CheckUserArg(KeyType.IsValidDataKind(dataKind), "type", "Bad item type for Key");
			Contracts.CheckUserArg(range.min >= 0UL, "min", "min must be non-negative");
			KeyType keyType;
			if (range.max == null)
			{
				keyType = new KeyType(dataKind, range.min, 0, range.contiguous);
			}
			else
			{
				Contracts.CheckUserArg(range.contiguous, "max", "max must be null when contiguous is false");
				ulong valueOrDefault = range.max.GetValueOrDefault();
				Contracts.CheckUserArg(valueOrDefault >= range.min, "max", "max must be >= min");
				Contracts.CheckUserArg(valueOrDefault - range.min < 2147483647UL, "max", "range is too large");
				int num = (int)(valueOrDefault - range.min + 1UL);
				if ((long)num > (long)DataKindExtensions.ToMaxInt(dataKind))
				{
					throw Contracts.ExceptUserArg("max", "range is too large for type {0}", new object[] { dataKind });
				}
				keyType = new KeyType(dataKind, range.min, num, true);
			}
			return keyType;
		}
	}
}
