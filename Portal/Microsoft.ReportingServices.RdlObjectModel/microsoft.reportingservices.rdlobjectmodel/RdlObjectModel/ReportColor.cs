using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D3 RID: 467
	[TypeConverter(typeof(ReportColorConverter))]
	public struct ReportColor : IXmlSerializable, IFormattable, IShouldSerialize
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00024A0F File Offset: 0x00022C0F
		public static ReportColor Empty
		{
			get
			{
				return ReportColor.m_empty;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00024A16 File Offset: 0x00022C16
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00024A1E File Offset: 0x00022C1E
		public Color Color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00024A27 File Offset: 0x00022C27
		public ReportColor(Color color)
		{
			this.m_color = color;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00024A30 File Offset: 0x00022C30
		public ReportColor(string value)
		{
			this.m_color = Color.Empty;
			this.Init(value);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00024A44 File Offset: 0x00022C44
		private void Init(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.Color = ReportColor.RdlStringToColor(value);
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00024A5C File Offset: 0x00022C5C
		internal static Color RdlStringToColor(string value)
		{
			if (value[0] == '#')
			{
				return ReportColor.RgbStringToColor(value);
			}
			Color color = ReportColor.FromName(value);
			if (!color.IsKnownColor)
			{
				throw new ArgumentException(SRErrorsWrapper.InvalidColor(value));
			}
			return color;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00024A98 File Offset: 0x00022C98
		private static Color RgbStringToColor(string value)
		{
			byte b = byte.MaxValue;
			if (value == "#00ffffff")
			{
				return Color.Transparent;
			}
			if (value == "#00000000")
			{
				return Color.Empty;
			}
			bool flag = true;
			if ((value.Length != 7 && value.Length != 9) || value[0] != '#')
			{
				flag = false;
			}
			else
			{
				string text = "abcdefABCDEF";
				for (int i = 1; i < value.Length; i++)
				{
					if (!char.IsDigit(value[i]) && -1 == text.IndexOf(value[i]))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				int num = 1;
				if (value.Length == 9)
				{
					b = Convert.ToByte(value.Substring(num, 2), 16);
					num += 2;
				}
				byte b2 = Convert.ToByte(value.Substring(num, 2), 16);
				byte b3 = Convert.ToByte(value.Substring(num + 2, 2), 16);
				byte b4 = Convert.ToByte(value.Substring(num + 4, 2), 16);
				return Color.FromArgb((int)b, (int)b2, (int)b3, (int)b4);
			}
			throw new ArgumentException(SRErrorsWrapper.InvalidColor(value));
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00024BB4 File Offset: 0x00022DB4
		public static string ColorToRdlString(Color c)
		{
			if (c.IsEmpty)
			{
				return "";
			}
			if (c == Color.Transparent)
			{
				return "#00ffffff";
			}
			if (c.IsNamedColor && !c.IsSystemColor)
			{
				return ReportColor.ToName(c);
			}
			if (c.A == 255)
			{
				return StringUtil.FormatInvariant("#{0:x6}", new object[] { c.ToArgb() & 16777215 });
			}
			return StringUtil.FormatInvariant("#{0:x8}", new object[] { c.ToArgb() });
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00024C4F File Offset: 0x00022E4F
		public bool IsEmpty
		{
			get
			{
				return Color.Empty == this.m_color;
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00024C61 File Offset: 0x00022E61
		public static ReportColor Parse(string s, IFormatProvider provider)
		{
			return new ReportColor(s);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00024C69 File Offset: 0x00022E69
		public void SetEmpty()
		{
			this.m_color = Color.Empty;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00024C76 File Offset: 0x00022E76
		internal static Color FromName(string name)
		{
			if (string.Equals(name, "LightGrey", StringComparison.OrdinalIgnoreCase))
			{
				name = "LightGray";
			}
			return Color.FromName(name);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00024C94 File Offset: 0x00022E94
		internal static string ToName(Color color)
		{
			string text = color.Name;
			if (text == "LightGray")
			{
				text = "LightGrey";
			}
			return text;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00024CBD File Offset: 0x00022EBD
		public override string ToString()
		{
			return ReportColor.ColorToRdlString(this.Color);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00024CCA File Offset: 0x00022ECA
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00024CD8 File Offset: 0x00022ED8
		public override int GetHashCode()
		{
			return this.m_color.GetHashCode();
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00024CEB File Offset: 0x00022EEB
		public override bool Equals(object obj)
		{
			return obj != null && obj is ReportColor && this == (ReportColor)obj;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00024D0B File Offset: 0x00022F0B
		public static bool operator ==(ReportColor left, ReportColor right)
		{
			return left.Color == right.Color;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00024D20 File Offset: 0x00022F20
		public static bool operator !=(ReportColor left, ReportColor right)
		{
			return left.Color != right.Color;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00024D35 File Offset: 0x00022F35
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00024D38 File Offset: 0x00022F38
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text.Trim());
			reader.Skip();
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00024D60 File Offset: 0x00022F60
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			string text = this.ToString();
			writer.WriteString(text);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00024D81 File Offset: 0x00022F81
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return !this.IsEmpty;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00024D8C File Offset: 0x00022F8C
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			return SerializationMethod.Auto;
		}

		// Token: 0x04000559 RID: 1369
		private Color m_color;

		// Token: 0x0400055A RID: 1370
		private static readonly ReportColor m_empty;
	}
}
