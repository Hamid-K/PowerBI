using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.DataShaping.Common.DaxComparer
{
	// Token: 0x02000027 RID: 39
	internal sealed class DaxDataComparer : IDataComparer, IEqualityComparer, IComparer
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00004E6E File Offset: 0x0000306E
		internal DaxDataComparer(CompareInfo compareInfo, CompareOptions compareOptions, bool nullsAsBlanks)
		{
			this._compareInfo = compareInfo;
			this._compareOptions = compareOptions;
			this._cultureInfo = new CultureInfo(this._compareInfo.Name);
			this._nullsAsBlanks = nullsAsBlanks;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004EA1 File Offset: 0x000030A1
		bool IEqualityComparer.Equals(object x, object y)
		{
			return this.InternalCompareTo(x, y) == 0;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004EAE File Offset: 0x000030AE
		int IComparer.Compare(object x, object y)
		{
			return this.InternalCompareTo(x, y);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004EB8 File Offset: 0x000030B8
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is string)
			{
				string text = (string)obj;
				if ((CompareOptions.IgnoreCase & this._compareOptions) != CompareOptions.None)
				{
					text = text.ToUpper(this._cultureInfo);
				}
				return text.GetHashCode();
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004F00 File Offset: 0x00003100
		private int InternalCompareTo(object x, object y)
		{
			string text = x as string;
			string text2 = y as string;
			if (text != null && text2 != null)
			{
				return this._compareInfo.Compare(text, text2, this._compareOptions);
			}
			DataTypeCode dataTypeCode = DaxComparerUtils.GetDataTypeCode(x);
			DataTypeCode dataTypeCode2 = DaxComparerUtils.GetDataTypeCode(y);
			if (dataTypeCode == DataTypeCode.Empty && dataTypeCode2 == DataTypeCode.Empty)
			{
				return 0;
			}
			if (dataTypeCode == DataTypeCode.Empty)
			{
				if (this._nullsAsBlanks && DaxComparerUtils.IsNumericLessThanZero(y))
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (dataTypeCode2 != DataTypeCode.Empty)
				{
					if (dataTypeCode != dataTypeCode2)
					{
						DataTypeCode commonVariantConversionType = DaxComparerUtils.GetCommonVariantConversionType(dataTypeCode, dataTypeCode2);
						if (commonVariantConversionType != DataTypeCode.Unknown)
						{
							if (commonVariantConversionType == DataTypeCode.Double)
							{
								double num;
								double num2;
								if (dataTypeCode == DataTypeCode.DateTime)
								{
									num = ((DateTime)x).ToOADate();
									num2 = Convert.ToDouble(y, this._cultureInfo);
								}
								else if (dataTypeCode2 == DataTypeCode.DateTime)
								{
									num2 = ((DateTime)y).ToOADate();
									num = Convert.ToDouble(x, this._cultureInfo);
								}
								else
								{
									num = Convert.ToDouble(x, this._cultureInfo);
									num2 = Convert.ToDouble(y, this._cultureInfo);
								}
								int num3 = num.CompareTo(num2);
								if (num3 == 0)
								{
									return DaxDataComparer.CompareNumericDateVariantTypes(dataTypeCode, dataTypeCode2);
								}
								return num3;
							}
							else if (commonVariantConversionType == DataTypeCode.Decimal)
							{
								decimal num4 = Convert.ToDecimal(x, this._cultureInfo);
								decimal num5 = Convert.ToDecimal(y, this._cultureInfo);
								int num6 = num4.CompareTo(num5);
								if (num6 == 0)
								{
									return DaxDataComparer.CompareNumericDateVariantTypes(dataTypeCode, dataTypeCode2);
								}
								return num6;
							}
							else if (commonVariantConversionType == DataTypeCode.Int64)
							{
								long num7 = Convert.ToInt64(x, this._cultureInfo);
								long num8 = Convert.ToInt64(y, this._cultureInfo);
								int num9 = num7.CompareTo(num8);
								if (num9 == 0)
								{
									return DaxDataComparer.CompareNumericDateVariantTypes(dataTypeCode, dataTypeCode2);
								}
								return num9;
							}
						}
						else if (DaxComparerUtils.IsNonNumericVariant(dataTypeCode) || DaxComparerUtils.IsNonNumericVariant(dataTypeCode2))
						{
							return DaxDataComparer.CompareToNonNumericVariantTypes(dataTypeCode, dataTypeCode2, x, y);
						}
					}
					IComparable comparable = (IComparable)x;
					IComparable comparable2 = (IComparable)y;
					return this.Compare(comparable, comparable2);
				}
				if (this._nullsAsBlanks && DaxComparerUtils.IsNumericLessThanZero(x))
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000050E0 File Offset: 0x000032E0
		private int Compare(IComparable left, IComparable right)
		{
			if (left == right)
			{
				return 0;
			}
			int num;
			try
			{
				num = left.CompareTo(right);
			}
			catch (ArgumentException)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005114 File Offset: 0x00003314
		private static int CompareNumericDateVariantTypes(DataTypeCode x, DataTypeCode y)
		{
			switch (x)
			{
			case DataTypeCode.Int32:
				return -1;
			case DataTypeCode.Int64:
				if (DaxComparerUtils.IsLessThanInt64(y))
				{
					return 1;
				}
				return -1;
			case DataTypeCode.Double:
				if (DaxComparerUtils.IsLessThanReal(y))
				{
					return 1;
				}
				return -1;
			case DataTypeCode.Decimal:
				if (DaxComparerUtils.IsLessThanCurrency(y))
				{
					return 1;
				}
				return -1;
			case DataTypeCode.DateTime:
				return 1;
			}
			return -1;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000516C File Offset: 0x0000336C
		private static int CompareToNonNumericVariantTypes(DataTypeCode xDataType, DataTypeCode yDataType, object x, object y)
		{
			if (DaxComparerUtils.IsNumericDateVariant(xDataType) && DaxComparerUtils.IsNonNumericVariant(yDataType))
			{
				return -1;
			}
			if (DaxComparerUtils.IsNonNumericVariant(xDataType) && DaxComparerUtils.IsNumericDateVariant(yDataType))
			{
				return 1;
			}
			if (xDataType == DataTypeCode.String && yDataType == DataTypeCode.Boolean)
			{
				return -1;
			}
			if (xDataType == DataTypeCode.Boolean && yDataType == DataTypeCode.String)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000051A9 File Offset: 0x000033A9
		public CompareInfo CompareInfo
		{
			get
			{
				return this._compareInfo;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000051B1 File Offset: 0x000033B1
		public CompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly CompareInfo _compareInfo;

		// Token: 0x0400007F RID: 127
		private readonly CompareOptions _compareOptions;

		// Token: 0x04000080 RID: 128
		private readonly CultureInfo _cultureInfo;

		// Token: 0x04000081 RID: 129
		private readonly bool _nullsAsBlanks;
	}
}
