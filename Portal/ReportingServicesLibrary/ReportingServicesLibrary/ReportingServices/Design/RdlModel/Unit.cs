using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000427 RID: 1063
	[Serializable]
	public struct Unit : IXmlSerializable, IVoluntarySerializable
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000813C0 File Offset: 0x0007F5C0
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x000813C7 File Offset: 0x0007F5C7
		public static UnitType DefaultType
		{
			get
			{
				return Unit.m_defaultType;
			}
			set
			{
				Unit.m_defaultType = value;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000813D0 File Offset: 0x0007F5D0
		public static float DotsPerInch
		{
			get
			{
				if (Unit.m_dotsPerInch == 0f)
				{
					using (Bitmap bitmap = new Bitmap(1, 1))
					{
						using (Graphics graphics = Graphics.FromImage(bitmap))
						{
							Unit.m_dotsPerInch = graphics.DpiX;
						}
					}
				}
				return Unit.m_dotsPerInch;
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0008143C File Offset: 0x0007F63C
		public Unit(int pixels)
		{
			double num = Unit.ConvertToUnits((double)pixels, Unit.DefaultType);
			this.m_value = num;
			this.m_type = Unit.DefaultType;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x00081468 File Offset: 0x0007F668
		public Unit(double pixels)
		{
			double num = Unit.ConvertToUnits(pixels, Unit.DefaultType);
			this.m_value = num;
			this.m_type = Unit.DefaultType;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00081493 File Offset: 0x0007F693
		public Unit(double value, UnitType type)
		{
			this.m_value = value;
			this.m_type = type;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000814A3 File Offset: 0x0007F6A3
		public Unit(string value)
		{
			this = new Unit(value, CultureInfo.CurrentCulture, Unit.DefaultType);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000814B6 File Offset: 0x0007F6B6
		public Unit(string value, CultureInfo culture)
		{
			this = new Unit(value, culture, Unit.DefaultType);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000814C5 File Offset: 0x0007F6C5
		public Unit(string value, CultureInfo culture, UnitType defaultType)
		{
			this.m_value = 0.0;
			this.m_type = defaultType;
			if (value != null && value.Length != 0)
			{
				this.Init(value, culture, defaultType);
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000814F4 File Offset: 0x0007F6F4
		private void Init(string value, CultureInfo culture, UnitType defaultType)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string text = value.Trim().ToLowerInvariant();
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
				throw new FormatException(SRErrors.UnitParseNoDigits(value));
			}
			if (num < length - 1)
			{
				this.m_type = Unit.GetTypeFromString(text.Substring(num + 1).Trim());
			}
			else
			{
				if (defaultType == UnitType.Invalid)
				{
					throw new FormatException(SRErrors.UnitParseNoUnit(value));
				}
				this.m_type = defaultType;
			}
			string text2 = text.Substring(0, num + 1);
			try
			{
				TypeConverter typeConverter = new SingleConverter();
				this.m_value = (double)((float)typeConverter.ConvertFromString(null, culture, text2));
			}
			catch
			{
				throw new FormatException(SRErrors.UnitParseNumericPart(value, text2, this.m_type.ToString("G")));
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00081600 File Offset: 0x0007F800
		public bool IsEmpty
		{
			get
			{
				return this.m_type == UnitType.Invalid;
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0008160B File Offset: 0x0007F80B
		public void Empty()
		{
			this.m_type = UnitType.Invalid;
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00081614 File Offset: 0x0007F814
		public UnitType Type
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.m_type;
				}
				return Unit.DefaultType;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0008162A File Offset: 0x0007F82A
		public double Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00081632 File Offset: 0x0007F832
		public override int GetHashCode()
		{
			return (this.m_type.GetHashCode() << 2) ^ this.m_value.GetHashCode();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00081654 File Offset: 0x0007F854
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Unit))
			{
				return false;
			}
			Unit unit = (Unit)obj;
			return unit.m_type == this.m_type && unit.m_value == this.m_value;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00081694 File Offset: 0x0007F894
		public static bool operator ==(Unit left, Unit right)
		{
			return left.m_type == right.m_type && left.m_value == right.m_value;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000816B4 File Offset: 0x0007F8B4
		public static bool operator !=(Unit left, Unit right)
		{
			return left.m_type != right.m_type || left.m_value != right.m_value;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000816D7 File Offset: 0x0007F8D7
		public static bool operator <(Unit left, Unit right)
		{
			return left.FPixels < right.FPixels;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000816E9 File Offset: 0x0007F8E9
		public static bool operator >(Unit left, Unit right)
		{
			return left.FPixels > right.FPixels;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000816FC File Offset: 0x0007F8FC
		public static Unit operator +(Unit unit1, Unit unit2)
		{
			Unit unit3 = unit1;
			unit3.FPixels += unit2.FPixels;
			return unit3;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00081724 File Offset: 0x0007F924
		private static string GetStringFromType(UnitType type)
		{
			switch (type)
			{
			case UnitType.Point:
				return "pt";
			case UnitType.Pica:
				return "pc";
			case UnitType.Inch:
				return "in";
			case UnitType.Mm:
				return "mm";
			case UnitType.Cm:
				return "cm";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00081774 File Offset: 0x0007F974
		public static UnitType GetTypeFromString(string value)
		{
			if (value == null || value.Length <= 0)
			{
				return Unit.DefaultType;
			}
			if (value.Equals("pt"))
			{
				return UnitType.Point;
			}
			if (value.Equals("pc"))
			{
				return UnitType.Pica;
			}
			if (value.Equals("in"))
			{
				return UnitType.Inch;
			}
			if (value.Equals("mm"))
			{
				return UnitType.Mm;
			}
			if (value.Equals("cm"))
			{
				return UnitType.Cm;
			}
			throw new ArgumentException(SRErrors.InvalidUnitType(value));
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000817E9 File Offset: 0x0007F9E9
		internal static Unit Parse(string s)
		{
			return new Unit(s);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000817F1 File Offset: 0x0007F9F1
		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture);
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000817FA File Offset: 0x0007F9FA
		public int Pixels
		{
			get
			{
				return Convert.ToInt32(Unit.ConvertToPixels(this.m_value, this.m_type));
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x00081812 File Offset: 0x0007FA12
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x00081825 File Offset: 0x0007FA25
		public double FPixels
		{
			get
			{
				return Unit.ConvertToPixels(this.m_value, this.m_type);
			}
			set
			{
				if (this.IsEmpty)
				{
					this.m_type = Unit.DefaultType;
				}
				this.m_value = Unit.ConvertToUnits(value, this.m_type);
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0008184C File Offset: 0x0007FA4C
		internal static Unit FromPixels(double pixels, UnitType type)
		{
			return new Unit(Unit.ConvertToUnits(pixels, type), type);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0008185B File Offset: 0x0007FA5B
		public override string ToString()
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			return this.m_value.ToString("0.#####", CultureInfo.InvariantCulture) + Unit.GetStringFromType(this.m_type);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00081890 File Offset: 0x0007FA90
		public Unit ChangeType(UnitType type)
		{
			if (type == this.m_type)
			{
				return this;
			}
			return new Unit(Unit.ConvertToUnits(Unit.ConvertToPixels(this.m_value, this.m_type), type), type);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000818C0 File Offset: 0x0007FAC0
		public static double ConvertToPixels(double value, UnitType type)
		{
			switch (type)
			{
			case UnitType.Point:
				value *= (double)(Unit.DotsPerInch / 72f);
				break;
			case UnitType.Pica:
				value *= (double)(Unit.DotsPerInch / 6f);
				break;
			case UnitType.Inch:
				value *= (double)Unit.DotsPerInch;
				break;
			case UnitType.Mm:
				value *= (double)(Unit.DotsPerInch / 25.4f);
				break;
			case UnitType.Cm:
				value *= (double)(Unit.DotsPerInch / 2.54f);
				break;
			}
			return value;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00081940 File Offset: 0x0007FB40
		public static double ConvertToUnits(double pixels, UnitType type)
		{
			double num = pixels;
			switch (type)
			{
			case UnitType.Point:
				num /= (double)(Unit.DotsPerInch / 72f);
				break;
			case UnitType.Pica:
				num /= (double)(Unit.DotsPerInch / 6f);
				break;
			case UnitType.Inch:
				num /= (double)Unit.DotsPerInch;
				break;
			case UnitType.Mm:
				num /= (double)(Unit.DotsPerInch / 25.4f);
				break;
			case UnitType.Cm:
				num /= (double)(Unit.DotsPerInch / 2.54f);
				break;
			}
			return num;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000819BC File Offset: 0x0007FBBC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text, CultureInfo.InvariantCulture, UnitType.Invalid);
			reader.Skip();
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000819E4 File Offset: 0x0007FBE4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			string text = this.ToString();
			writer.WriteString(text);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00081A05 File Offset: 0x0007FC05
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return !this.IsEmpty;
		}

		// Token: 0x04000ED5 RID: 3797
		public const float CentimetersPerInch = 2.54f;

		// Token: 0x04000ED6 RID: 3798
		public const float MillimetersPerInch = 25.4f;

		// Token: 0x04000ED7 RID: 3799
		public const float PicasPerInch = 6f;

		// Token: 0x04000ED8 RID: 3800
		public const float PointsPerInch = 72f;

		// Token: 0x04000ED9 RID: 3801
		private static float m_dotsPerInch;

		// Token: 0x04000EDA RID: 3802
		private static UnitType m_defaultType = UnitType.Inch;

		// Token: 0x04000EDB RID: 3803
		private UnitType m_type;

		// Token: 0x04000EDC RID: 3804
		private double m_value;
	}
}
