using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.Detection.DataType.DataTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.DataType
{
	// Token: 0x02000AFE RID: 2814
	public static class DataTypeIdentifier
	{
		// Token: 0x06004686 RID: 18054 RVA: 0x000DC7B8 File Offset: 0x000DA9B8
		public static DataType Identify(params string[] values)
		{
			return DataTypeIdentifier.Identify(values, CultureInfo.CurrentCulture);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x000DC7C8 File Offset: 0x000DA9C8
		public static DataType Identify(IEnumerable<string> values, CultureInfo cultureInfo)
		{
			HashSet<DataType> hashSet = values.Select((string value) => DataTypeIdentifier.Identify(value, cultureInfo)).ConvertToHashSet<DataType>();
			if (hashSet.Count == 1)
			{
				return hashSet.Single<DataType>();
			}
			int num = (hashSet.Intersect(DataTypeIdentifier.NumberTypes).Any<DataType>() ? 1 : 0);
			bool flag = hashSet.Intersect(DataTypeIdentifier.NonNumberTypes).Any<DataType>();
			if (num == 0 || flag)
			{
				return StringType.Instance;
			}
			if (hashSet.Contains(DoubleType.Instance))
			{
				return DoubleType.Instance;
			}
			return IntegerType.Instance;
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x000DC850 File Offset: 0x000DAA50
		private static DataType Identify(string value, CultureInfo cultureInfo)
		{
			value = value.Trim();
			bool flag;
			if (bool.TryParse(value.ToLower(cultureInfo), out flag))
			{
				return BoolType.Instance;
			}
			int num;
			if (int.TryParse(value, NumberStyles.Integer, cultureInfo, out num))
			{
				if (num == 0 || num == 1)
				{
					return BitType.Instance;
				}
				return IntegerType.Instance;
			}
			else
			{
				double num2;
				if (double.TryParse(value, NumberStyles.Any, cultureInfo, out num2))
				{
					return DoubleType.Instance;
				}
				DateTime dateTime;
				if (DateTime.TryParse(value, cultureInfo, DateTimeStyles.None, out dateTime))
				{
					return DateTimeType.Instance;
				}
				return StringType.Instance;
			}
		}

		// Token: 0x04002016 RID: 8214
		private static readonly HashSet<DataType> NumberTypes = new HashSet<DataType>
		{
			BitType.Instance,
			IntegerType.Instance,
			DoubleType.Instance
		};

		// Token: 0x04002017 RID: 8215
		private static readonly HashSet<DataType> NonNumberTypes = new HashSet<DataType>
		{
			BoolType.Instance,
			DateTimeType.Instance,
			StringType.Instance
		};
	}
}
