using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005BF RID: 1471
	internal sealed class CommonDataComparer : IDataComparer, IEqualityComparer, IEqualityComparer<object>, IComparer, IComparer<object>
	{
		// Token: 0x06005339 RID: 21305 RVA: 0x0015E4DC File Offset: 0x0015C6DC
		internal CommonDataComparer(CompareInfo compareInfo, CompareOptions compareOptions, bool nullsAsBlanks)
		{
			this.m_compareInfo = compareInfo;
			if (this.m_compareInfo == null)
			{
				throw new ArgumentNullException("compareInfo");
			}
			this.m_compareOptions = compareOptions;
			this.m_cultureInfo = new CultureInfo(this.m_compareInfo.Name);
			this.m_nullsAsBlanks = nullsAsBlanks;
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x0015E52D File Offset: 0x0015C72D
		public CompareInfo CompareInfo
		{
			get
			{
				return this.m_compareInfo;
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x0015E535 File Offset: 0x0015C735
		public CompareOptions CompareOptions
		{
			get
			{
				return this.m_compareOptions;
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0015E53D File Offset: 0x0015C73D
		bool IEqualityComparer.Equals(object x, object y)
		{
			return this.InternalCompareTo(x, y, false) == 0;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0015E54B File Offset: 0x0015C74B
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return this.InternalCompareTo(x, y, false) == 0;
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0015E559 File Offset: 0x0015C759
		int IComparer.Compare(object x, object y)
		{
			return this.InternalCompareTo(x, y, false);
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x0015E564 File Offset: 0x0015C764
		int IComparer<object>.Compare(object x, object y)
		{
			return this.InternalCompareTo(x, y, false);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0015E56F File Offset: 0x0015C76F
		int IDataComparer.Compare(object x, object y, bool extendedTypeComparisons)
		{
			return this.InternalCompareTo(x, y, false);
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x0015E57A File Offset: 0x0015C77A
		int IDataComparer.Compare(object x, object y, bool throwExceptionOnComparisonFailure, bool extendedTypeComparisons, out bool validComparisonResult)
		{
			validComparisonResult = true;
			return this.InternalCompareTo(x, y, throwExceptionOnComparisonFailure);
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0015E58C File Offset: 0x0015C78C
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is string)
			{
				string text = (string)obj;
				if ((CompareOptions.IgnoreCase & this.m_compareOptions) != CompareOptions.None)
				{
					text = text.ToUpper(this.m_cultureInfo);
				}
				return text.GetHashCode();
			}
			ICustomComparable customComparable = obj as ICustomComparable;
			if (customComparable != null)
			{
				return customComparable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0015E5E4 File Offset: 0x0015C7E4
		private int InternalCompareTo(object x, object y, bool throwExceptionOnComparisonFailure)
		{
			string text = x as string;
			string text2 = y as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2, this.m_compareOptions);
			}
			DataTypeCode dataTypeCode = ObjectSerializer.GetDataTypeCode(x);
			DataTypeCode dataTypeCode2 = ObjectSerializer.GetDataTypeCode(y);
			if (dataTypeCode == DataTypeCode.Empty && dataTypeCode2 == DataTypeCode.Empty)
			{
				return 0;
			}
			if (dataTypeCode == DataTypeCode.Empty)
			{
				if (this.m_nullsAsBlanks && ComparerUtility.IsNumericLessThanZero(y))
				{
					return 1;
				}
				return -1;
			}
			else if (dataTypeCode2 == DataTypeCode.Empty)
			{
				if (this.m_nullsAsBlanks && ComparerUtility.IsNumericLessThanZero(x))
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (dataTypeCode != dataTypeCode2)
				{
					DataTypeCode commonVariantConversionType = ComparerUtility.GetCommonVariantConversionType(dataTypeCode, dataTypeCode2);
					if (commonVariantConversionType != DataTypeCode.Unknown)
					{
						if (commonVariantConversionType == DataTypeCode.Double)
						{
							double num;
							double num2;
							if (dataTypeCode == DataTypeCode.DateTime)
							{
								num = ((DateTime)x).ToOADate();
								num2 = Convert.ToDouble(y, this.m_cultureInfo);
							}
							else if (dataTypeCode2 == DataTypeCode.DateTime)
							{
								num2 = ((DateTime)y).ToOADate();
								num = Convert.ToDouble(x, this.m_cultureInfo);
							}
							else
							{
								num = Convert.ToDouble(x, this.m_cultureInfo);
								num2 = Convert.ToDouble(y, this.m_cultureInfo);
							}
							int num3 = num.CompareTo(num2);
							if (num3 == 0)
							{
								return CommonDataComparer.CompareNumericDateVariantTypes(dataTypeCode, dataTypeCode2, throwExceptionOnComparisonFailure);
							}
							return num3;
						}
						else if (commonVariantConversionType == DataTypeCode.Decimal)
						{
							decimal num4 = Convert.ToDecimal(x, this.m_cultureInfo);
							decimal num5 = Convert.ToDecimal(y, this.m_cultureInfo);
							int num6 = num4.CompareTo(num5);
							if (num6 == 0)
							{
								return CommonDataComparer.CompareNumericDateVariantTypes(dataTypeCode, dataTypeCode2, throwExceptionOnComparisonFailure);
							}
							return num6;
						}
						else if (commonVariantConversionType == DataTypeCode.Int64)
						{
							long num7 = Convert.ToInt64(x, this.m_cultureInfo);
							long num8 = Convert.ToInt64(y, this.m_cultureInfo);
							int num9 = num7.CompareTo(num8);
							if (num9 == 0)
							{
								return CommonDataComparer.CompareNumericDateVariantTypes(dataTypeCode, dataTypeCode2, throwExceptionOnComparisonFailure);
							}
							return num9;
						}
					}
					else if (ComparerUtility.IsNonNumericVariant(dataTypeCode) || ComparerUtility.IsNonNumericVariant(dataTypeCode2))
					{
						return CommonDataComparer.CompareToNonNumericVariantTypes(dataTypeCode, dataTypeCode2, x, y, throwExceptionOnComparisonFailure);
					}
				}
				ICustomComparable customComparable = x as ICustomComparable;
				ICustomComparable customComparable2 = y as ICustomComparable;
				if (customComparable != null && customComparable2 != null)
				{
					return customComparable.CompareTo(customComparable2, this);
				}
				IComparable comparable = (IComparable)x;
				IComparable comparable2 = (IComparable)y;
				return this.Compare(comparable, comparable2, throwExceptionOnComparisonFailure);
			}
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0015E7EC File Offset: 0x0015C9EC
		private int Compare(IComparable left, IComparable right, bool throwExceptionOnComparisonFailure)
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
				if (throwExceptionOnComparisonFailure)
				{
					throw new CommonDataComparerException(left.GetType().ToString(), right.GetType().ToString());
				}
				num = -1;
			}
			return num;
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0015E840 File Offset: 0x0015CA40
		private static int CompareNumericDateVariantTypes(DataTypeCode x, DataTypeCode y, bool throwExceptionOnComparisonFailure)
		{
			switch (x)
			{
			case DataTypeCode.Int32:
				return -1;
			case DataTypeCode.Int64:
				if (ComparerUtility.IsLessThanInt64(y))
				{
					return 1;
				}
				return -1;
			case DataTypeCode.Double:
				if (ComparerUtility.IsLessThanReal(y))
				{
					return 1;
				}
				return -1;
			case DataTypeCode.Decimal:
				if (ComparerUtility.IsLessThanCurrency(y))
				{
					return 1;
				}
				return -1;
			case DataTypeCode.DateTime:
				return 1;
			}
			if (throwExceptionOnComparisonFailure)
			{
				throw new CommonDataComparerException(x.ToString(), y.ToString());
			}
			return -1;
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0015E8BC File Offset: 0x0015CABC
		private static int CompareToNonNumericVariantTypes(DataTypeCode xDataType, DataTypeCode yDataType, object x, object y, bool throwExceptionOnComparisonFailure)
		{
			if (ComparerUtility.IsNumericDateVariant(xDataType) && ComparerUtility.IsNonNumericVariant(yDataType))
			{
				return -1;
			}
			if (ComparerUtility.IsNonNumericVariant(xDataType) && ComparerUtility.IsNumericDateVariant(yDataType))
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
			if (throwExceptionOnComparisonFailure)
			{
				throw new CommonDataComparerException(x.ToString(), y.ToString());
			}
			return -1;
		}

		// Token: 0x040029ED RID: 10733
		private const bool DefaultThrowExceptionOnComparisonFailure = false;

		// Token: 0x040029EE RID: 10734
		private readonly CompareInfo m_compareInfo;

		// Token: 0x040029EF RID: 10735
		private readonly CompareOptions m_compareOptions;

		// Token: 0x040029F0 RID: 10736
		private readonly CultureInfo m_cultureInfo;

		// Token: 0x040029F1 RID: 10737
		private readonly bool m_nullsAsBlanks;
	}
}
