using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F0 RID: 496
	[TypeConverter(typeof(ReportSizeConverter))]
	public struct ReportSize : IComparable, IXmlSerializable, IFormattable, IShouldSerialize
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000268D1 File Offset: 0x00024AD1
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x000268D8 File Offset: 0x00024AD8
		public static SizeTypes DefaultType
		{
			get
			{
				return ReportSize.m_defaultType;
			}
			set
			{
				ReportSize.m_defaultType = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x000268E0 File Offset: 0x00024AE0
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x000268E7 File Offset: 0x00024AE7
		public static int SerializedDecimalDigits
		{
			get
			{
				return ReportSize.m_serializedDecimalDigits;
			}
			set
			{
				if (value <= 0 || value > 99)
				{
					throw new ArgumentException("SerializedDecimalDigits");
				}
				ReportSize.m_serializedDecimalDigits = value;
				ReportSize.m_serializationFormat = "{0:0." + new string('#', value) + "}{1}";
			}
		} = 5;

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00026920 File Offset: 0x00024B20
		public static float DotsPerInch
		{
			get
			{
				if (ReportSize.m_dotsPerInch == 0f)
				{
					using (Bitmap bitmap = new Bitmap(1, 1))
					{
						using (Graphics graphics = Graphics.FromImage(bitmap))
						{
							ReportSize.m_dotsPerInch = graphics.DpiX;
						}
					}
				}
				return ReportSize.m_dotsPerInch;
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000269A5 File Offset: 0x00024BA5
		public ReportSize(double value, SizeTypes type)
		{
			this.m_value = value;
			this.m_type = type;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000269B5 File Offset: 0x00024BB5
		public ReportSize(string value)
		{
			this = new ReportSize(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000269C3 File Offset: 0x00024BC3
		public ReportSize(double value)
		{
			this.m_value = value;
			this.m_type = ReportSize.DefaultType;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000269D7 File Offset: 0x00024BD7
		public ReportSize(string value, IFormatProvider provider)
		{
			this = new ReportSize(value, provider, ReportSize.DefaultType);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000269E6 File Offset: 0x00024BE6
		public ReportSize(string value, IFormatProvider provider, SizeTypes defaultType)
		{
			this.m_value = 0.0;
			this.m_type = SizeTypes.Invalid;
			if (!string.IsNullOrEmpty(value))
			{
				this.Init(value, provider, defaultType);
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00026A10 File Offset: 0x00024C10
		private void Init(string value, IFormatProvider provider, SizeTypes defaultType)
		{
			if (provider == null)
			{
				provider = CultureInfo.CurrentCulture;
			}
			string text = value.Trim();
			int length = text.Length;
			NumberFormatInfo numberFormatInfo = provider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
			if (numberFormatInfo == null)
			{
				numberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;
			}
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (!char.IsDigit(c) && c != numberFormatInfo.NegativeSign[0] && c != numberFormatInfo.NumberDecimalSeparator[0] && c != numberFormatInfo.NumberGroupSeparator[0])
				{
					break;
				}
				num = i;
			}
			if (num == -1)
			{
				throw new FormatException(SRErrorsWrapper.UnitParseNoDigits(value));
			}
			if (num < length - 1)
			{
				try
				{
					this.m_type = ReportSize.GetTypeFromString(text.Substring(num + 1).Trim().ToLowerInvariant());
					goto IL_00ED;
				}
				catch (ArgumentException ex)
				{
					throw new FormatException(ex.Message);
				}
			}
			if (defaultType == SizeTypes.Invalid)
			{
				throw new FormatException(SRErrorsWrapper.UnitParseNoUnit(value));
			}
			this.m_type = defaultType;
			IL_00ED:
			string text2 = text.Substring(0, num + 1);
			try
			{
				this.m_value = double.Parse(text2, provider);
			}
			catch
			{
				throw new FormatException(SRErrorsWrapper.UnitParseNumericPart(value, text2, this.m_type.ToString("G")));
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00026B68 File Offset: 0x00024D68
		public static ReportSize Parse(string s, IFormatProvider provider)
		{
			return new ReportSize(s, provider);
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00026B71 File Offset: 0x00024D71
		public static ReportSize Empty
		{
			get
			{
				return ReportSize.m_empty;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00026B78 File Offset: 0x00024D78
		public SizeTypes Type
		{
			get
			{
				if (this.m_type == SizeTypes.Invalid)
				{
					return ReportSize.DefaultType;
				}
				return this.m_type;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00026B8E File Offset: 0x00024D8E
		public double Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00026B96 File Offset: 0x00024D96
		public double SerializedValue
		{
			get
			{
				return Math.Round(this.m_value, ReportSize.m_serializedDecimalDigits);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00026BA8 File Offset: 0x00024DA8
		public bool IsEmpty
		{
			get
			{
				return this.m_type == SizeTypes.Invalid;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00026BB3 File Offset: 0x00024DB3
		public override int GetHashCode()
		{
			return (this.m_type.GetHashCode() << 2) ^ this.m_value.GetHashCode();
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00026BD4 File Offset: 0x00024DD4
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is ReportSize))
			{
				return false;
			}
			ReportSize reportSize = (ReportSize)obj;
			return reportSize.Value == this.Value && reportSize.m_type == this.m_type;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00026C15 File Offset: 0x00024E15
		public static bool operator ==(ReportSize left, ReportSize right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00026C2A File Offset: 0x00024E2A
		public static bool operator !=(ReportSize left, ReportSize right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00026C42 File Offset: 0x00024E42
		public static bool operator <(ReportSize left, ReportSize right)
		{
			return left.ToMillimeters() < right.ToMillimeters();
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00026C54 File Offset: 0x00024E54
		public static bool operator >(ReportSize left, ReportSize right)
		{
			return left.ToMillimeters() > right.ToMillimeters();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00026C66 File Offset: 0x00024E66
		public static ReportSize operator +(ReportSize size1, ReportSize size2)
		{
			if (size1.IsEmpty)
			{
				size1 = new ReportSize(0.0);
			}
			size1.SetPixels(size1.ToPixels() + size2.ToPixels());
			return size1;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00026C98 File Offset: 0x00024E98
		public static ReportSize operator -(ReportSize size1, ReportSize size2)
		{
			if (size1.IsEmpty)
			{
				size1 = new ReportSize(0.0);
			}
			size1.SetPixels(size1.ToPixels() - size2.ToPixels());
			return size1;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00026CCC File Offset: 0x00024ECC
		private static string GetStringFromType(SizeTypes type)
		{
			switch (type)
			{
			case SizeTypes.Inch:
				return "in";
			case SizeTypes.Cm:
				return "cm";
			case SizeTypes.Mm:
				return "mm";
			case SizeTypes.Point:
				return "pt";
			case SizeTypes.Pica:
				return "pc";
			default:
				return string.Empty;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00026D1C File Offset: 0x00024F1C
		internal static SizeTypes GetTypeFromString(string value)
		{
			if (value == null || value.Length <= 0)
			{
				return ReportSize.DefaultType;
			}
			if (value.Equals("pt"))
			{
				return SizeTypes.Point;
			}
			if (value.Equals("pc"))
			{
				return SizeTypes.Pica;
			}
			if (value.Equals("in"))
			{
				return SizeTypes.Inch;
			}
			if (value.Equals("mm"))
			{
				return SizeTypes.Mm;
			}
			if (value.Equals("cm"))
			{
				return SizeTypes.Cm;
			}
			throw new ArgumentException(SRErrorsWrapper.InvalidUnitType(value));
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00026D91 File Offset: 0x00024F91
		public int ToIntPixels()
		{
			return Convert.ToInt32(this.ConvertToPixels(this.m_value, this.m_type));
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00026DAA File Offset: 0x00024FAA
		public double ToPixels()
		{
			return this.ConvertToPixels(this.m_value, this.m_type);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00026DBE File Offset: 0x00024FBE
		public void SetPixels(double pixels)
		{
			this.m_value = ReportSize.ConvertToUnits(pixels, this.m_type);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00026DD2 File Offset: 0x00024FD2
		public static ReportSize FromPixels(double pixels, SizeTypes type)
		{
			return new ReportSize(ReportSize.ConvertToUnits(pixels, type), type);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00026DE1 File Offset: 0x00024FE1
		public double ToMillimeters()
		{
			return this.ConvertToMillimeters(this.m_value, this.m_type);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00026DF5 File Offset: 0x00024FF5
		public double ToCentimeters()
		{
			return 0.1 * this.ConvertToMillimeters(this.m_value, this.m_type);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00026E13 File Offset: 0x00025013
		public double ToInches()
		{
			return this.ConvertToMillimeters(this.m_value, this.m_type) / 25.4;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00026E31 File Offset: 0x00025031
		public double ToPoints()
		{
			if (this.m_type == SizeTypes.Point)
			{
				return this.m_value;
			}
			return this.ToInches() * 72.0;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00026E53 File Offset: 0x00025053
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00026E61 File Offset: 0x00025061
		public string ToString(string format, IFormatProvider provider)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			return string.Format(provider, ReportSize.m_serializationFormat, this.SerializedValue, ReportSize.GetStringFromType(this.m_type));
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00026E92 File Offset: 0x00025092
		internal ReportSize ChangeType(SizeTypes type)
		{
			if (type == this.m_type)
			{
				return this;
			}
			return new ReportSize(ReportSize.ConvertToUnits(this.ConvertToPixels(this.m_value, this.m_type), type), type);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00026EC4 File Offset: 0x000250C4
		internal double ConvertToPixels(double value, SizeTypes type)
		{
			switch (type)
			{
			case SizeTypes.Inch:
				value *= (double)ReportSize.DotsPerInch;
				break;
			case SizeTypes.Cm:
				value *= (double)ReportSize.DotsPerInch / 2.54;
				break;
			case SizeTypes.Mm:
				value *= (double)ReportSize.DotsPerInch / 25.4;
				break;
			case SizeTypes.Point:
				value *= (double)ReportSize.DotsPerInch / 72.0;
				break;
			case SizeTypes.Pica:
				value *= (double)ReportSize.DotsPerInch / 6.0;
				break;
			}
			return value;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00026F54 File Offset: 0x00025154
		internal double ConvertToMillimeters(double value, SizeTypes type)
		{
			switch (type)
			{
			case SizeTypes.Inch:
				value *= 25.4;
				break;
			case SizeTypes.Cm:
				value *= 10.0;
				break;
			case SizeTypes.Point:
				value *= 0.35277777777777775;
				break;
			case SizeTypes.Pica:
				value *= 4.233333333333333;
				break;
			}
			return value;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00026FBC File Offset: 0x000251BC
		internal static double ConvertToUnits(double pixels, SizeTypes type)
		{
			double num = pixels;
			switch (type)
			{
			case SizeTypes.Inch:
				num /= (double)ReportSize.DotsPerInch;
				break;
			case SizeTypes.Cm:
				num /= (double)ReportSize.DotsPerInch / 2.54;
				break;
			case SizeTypes.Mm:
				num /= (double)ReportSize.DotsPerInch / 25.4;
				break;
			case SizeTypes.Point:
				num /= (double)ReportSize.DotsPerInch / 72.0;
				break;
			case SizeTypes.Pica:
				num /= (double)ReportSize.DotsPerInch / 6.0;
				break;
			}
			return num;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00027048 File Offset: 0x00025248
		int IComparable.CompareTo(object value)
		{
			if (!(value is ReportSize))
			{
				throw new ArgumentException("value is not a RdlSize");
			}
			double num = this.ToMillimeters();
			double num2 = ((ReportSize)value).ToMillimeters();
			if (num >= num2)
			{
				return (num > num2) ? 1 : 0;
			}
			return -1;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00027088 File Offset: 0x00025288
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002708C File Offset: 0x0002528C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text, CultureInfo.InvariantCulture, SizeTypes.Invalid);
			reader.Skip();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000270B4 File Offset: 0x000252B4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			string text = this.ToString(null, CultureInfo.InvariantCulture);
			writer.WriteString(text);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000270D5 File Offset: 0x000252D5
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return !this.IsEmpty;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000270E0 File Offset: 0x000252E0
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			return SerializationMethod.Auto;
		}

		// Token: 0x04000576 RID: 1398
		internal const double CentimetersPerInch = 2.54;

		// Token: 0x04000577 RID: 1399
		internal const double MillimetersPerInch = 25.4;

		// Token: 0x04000578 RID: 1400
		internal const double PicasPerInch = 6.0;

		// Token: 0x04000579 RID: 1401
		internal const double PointsPerInch = 72.0;

		// Token: 0x0400057A RID: 1402
		internal const int DefaultDecimalDigits = 5;

		// Token: 0x0400057B RID: 1403
		private static float m_dotsPerInch;

		// Token: 0x0400057C RID: 1404
		private static readonly ReportSize m_empty = default(ReportSize);

		// Token: 0x0400057D RID: 1405
		private static string m_serializationFormat;

		// Token: 0x0400057E RID: 1406
		private static int m_serializedDecimalDigits;

		// Token: 0x0400057F RID: 1407
		private static SizeTypes m_defaultType = SizeTypes.Inch;

		// Token: 0x04000580 RID: 1408
		private SizeTypes m_type;

		// Token: 0x04000581 RID: 1409
		private double m_value;
	}
}
