using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D0 RID: 1488
	internal struct RVUnit
	{
		// Token: 0x060053A3 RID: 21411 RVA: 0x00160822 File Offset: 0x0015EA22
		public RVUnit(double value, RVUnitType type)
		{
			this.m_value = value;
			this.m_type = type;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x00160832 File Offset: 0x0015EA32
		public RVUnit(string value)
		{
			this = new RVUnit(value, CultureInfo.CurrentCulture, RVUnit.DefaultType);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00160845 File Offset: 0x0015EA45
		public RVUnit(string value, CultureInfo culture)
		{
			this = new RVUnit(value, culture, RVUnit.DefaultType);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00160854 File Offset: 0x0015EA54
		public RVUnit(string value, CultureInfo culture, RVUnitType defaultType)
		{
			this.m_value = 0.0;
			this.m_type = defaultType;
			if (value != null && value.Length != 0)
			{
				this.Init(value, culture, defaultType);
			}
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00160880 File Offset: 0x0015EA80
		private void Init(string value, CultureInfo culture, RVUnitType defaultType)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string text = value.Trim().ToLower(culture);
			int length = text.Length;
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if ((c < '0' || c > '9') && c != '-' && c != '.' && c != ',')
				{
					break;
				}
				num = i;
			}
			if (num == -1)
			{
				throw new FormatException();
			}
			if (num < length - 1)
			{
				this.m_type = RVUnit.GetTypeFromString(text.Substring(num + 1).Trim());
			}
			else
			{
				if (defaultType == (RVUnitType)0)
				{
					throw new FormatException();
				}
				this.m_type = defaultType;
			}
			string text2 = text.Substring(0, num + 1);
			try
			{
				TypeConverter typeConverter = new SingleConverter();
				this.m_value = (double)((float)typeConverter.ConvertFromString(null, culture, text2));
			}
			catch (Exception ex)
			{
				Exception ex2 = RVUnit.FindStoppingException(ex);
				if (ex2 == ex)
				{
					throw;
				}
				if (ex2 != null)
				{
					throw ex2;
				}
				throw new FormatException();
			}
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x060053A8 RID: 21416 RVA: 0x00160980 File Offset: 0x0015EB80
		public bool IsEmpty
		{
			get
			{
				return this.m_type == (RVUnitType)0;
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x0016098B File Offset: 0x0015EB8B
		public RVUnitType Type
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.m_type;
				}
				return RVUnit.DefaultType;
			}
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x001609A1 File Offset: 0x0015EBA1
		public double Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x001609AC File Offset: 0x0015EBAC
		private static Exception FindStoppingException(Exception e)
		{
			if (e is OutOfMemoryException || e is StackOverflowException || e is ThreadAbortException)
			{
				return e;
			}
			Exception innerException = e.InnerException;
			if (innerException != null)
			{
				return RVUnit.FindStoppingException(innerException);
			}
			return null;
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x001609E8 File Offset: 0x0015EBE8
		private static string GetStringFromType(RVUnitType type)
		{
			switch (type)
			{
			case RVUnitType.Cm:
				return "cm";
			case RVUnitType.Em:
				return "em";
			case RVUnitType.Ex:
				return "ex";
			case RVUnitType.Inch:
				return "in";
			case RVUnitType.Mm:
				return "mm";
			case RVUnitType.Percentage:
				return "%";
			case RVUnitType.Pica:
				return "pc";
			case RVUnitType.Point:
				return "pt";
			}
			return string.Empty;
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x00160A58 File Offset: 0x0015EC58
		public static RVUnitType GetTypeFromString(string value)
		{
			if (value == null || value.Length <= 0)
			{
				return RVUnit.DefaultType;
			}
			if (value.Equals("pt"))
			{
				return RVUnitType.Point;
			}
			if (value.Equals("pc"))
			{
				return RVUnitType.Pica;
			}
			if (value.Equals("in"))
			{
				return RVUnitType.Inch;
			}
			if (value.Equals("mm"))
			{
				return RVUnitType.Mm;
			}
			if (value.Equals("cm"))
			{
				return RVUnitType.Cm;
			}
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00160ACD File Offset: 0x0015ECCD
		internal static RVUnit Parse(string s)
		{
			return new RVUnit(s, null);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00160AD6 File Offset: 0x0015ECD6
		public static RVUnit Parse(string s, CultureInfo culture)
		{
			return new RVUnit(s, culture);
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00160ADF File Offset: 0x0015ECDF
		internal static bool TryParse(string s, out RVUnit rvUnit)
		{
			return RVUnit.TryParse(s, null, out rvUnit);
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00160AEC File Offset: 0x0015ECEC
		public static bool TryParse(string s, CultureInfo culture, out RVUnit rvUnit)
		{
			bool flag;
			try
			{
				rvUnit = new RVUnit(s, culture);
				flag = true;
			}
			catch (Exception ex)
			{
				Exception ex2 = RVUnit.FindStoppingException(ex);
				if (ex2 == ex)
				{
					throw;
				}
				if (ex2 != null)
				{
					throw ex2;
				}
				rvUnit = RVUnit.Empty;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00160B40 File Offset: 0x0015ED40
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00160B4D File Offset: 0x0015ED4D
		public string ToString(CultureInfo culture)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			return this.m_value.ToString("0.#####", culture) + RVUnit.GetStringFromType(this.m_type);
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x00160B80 File Offset: 0x0015ED80
		public double ToMillimeters()
		{
			double num = this.Value;
			switch (this.Type)
			{
			case RVUnitType.Cm:
				num *= 10.0;
				break;
			case RVUnitType.Inch:
				num *= 25.4;
				break;
			case RVUnitType.Pica:
				num *= 4.2333333333333325;
				break;
			case RVUnitType.Point:
				num *= 0.35277777777777775;
				break;
			}
			return num;
		}

		// Token: 0x04002A2E RID: 10798
		public static readonly RVUnit Empty = default(RVUnit);

		// Token: 0x04002A2F RID: 10799
		private double m_value;

		// Token: 0x04002A30 RID: 10800
		private RVUnitType m_type;

		// Token: 0x04002A31 RID: 10801
		private static RVUnitType DefaultType = RVUnitType.Inch;

		// Token: 0x04002A32 RID: 10802
		internal const double Pt1 = 0.35277777777777775;

		// Token: 0x04002A33 RID: 10803
		internal const double Pc1 = 4.2333333333333325;
	}
}
