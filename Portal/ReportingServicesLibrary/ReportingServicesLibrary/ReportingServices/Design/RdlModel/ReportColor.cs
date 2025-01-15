using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000416 RID: 1046
	public struct ReportColor : IXmlSerializable, IVoluntarySerializable
	{
		// Token: 0x06002133 RID: 8499 RVA: 0x0008092E File Offset: 0x0007EB2E
		public ReportColor(Color rptColor, ColorPropertyDef definition)
		{
			this.m_colorRgb = rptColor;
			this.m_Expression = null;
			this.m_Definition = definition;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00080945 File Offset: 0x0007EB45
		public ReportColor(ColorPropertyDef definition)
		{
			this = new ReportColor(definition.Default, definition);
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00080954 File Offset: 0x0007EB54
		public bool IsColorRgb
		{
			get
			{
				return !this.m_colorRgb.IsEmpty;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00080964 File Offset: 0x0007EB64
		public bool IsExpression
		{
			get
			{
				return this.m_Expression != null;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x0008096F File Offset: 0x0007EB6F
		// (set) Token: 0x06002138 RID: 8504 RVA: 0x00080977 File Offset: 0x0007EB77
		public Color ColorRgb
		{
			get
			{
				return this.m_colorRgb;
			}
			set
			{
				this.m_colorRgb = value;
				this.m_Expression = null;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00080987 File Offset: 0x0007EB87
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x0008098F File Offset: 0x0007EB8F
		public string ColorExpression
		{
			get
			{
				return this.m_Expression;
			}
			set
			{
				this.m_Expression = value;
				this.m_colorRgb = Color.Empty;
			}
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000809A3 File Offset: 0x0007EBA3
		private void Init(string rdfColorString)
		{
			if (rdfColorString != null && rdfColorString.Length > 0)
			{
				if (rdfColorString[0] == '=')
				{
					this.ColorExpression = rdfColorString;
					return;
				}
				this.ColorRgb = ReportColor.RdfStringToColor(rdfColorString);
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000809D0 File Offset: 0x0007EBD0
		public static Color RdfStringToColor(string rdfColorString)
		{
			if (rdfColorString[0] == '#')
			{
				return ReportColor.RgbStringToColor(rdfColorString);
			}
			Color color = ReportColor.FromName(rdfColorString);
			if (!color.IsKnownColor)
			{
				return Color.Empty;
			}
			return color;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00080A08 File Offset: 0x0007EC08
		private static Color RgbStringToColor(string rptColorString)
		{
			bool flag = true;
			if (rptColorString.Length != 7 || rptColorString[0] != '#')
			{
				flag = false;
			}
			else
			{
				string text = "abcdefABCDEF";
				for (int i = 1; i < 6; i++)
				{
					if (!char.IsDigit(rptColorString[i]) && -1 == text.IndexOf(rptColorString[i]))
					{
						flag = false;
						break;
					}
				}
			}
			Color color;
			if (flag)
			{
				int num = (int)Convert.ToByte(rptColorString.Substring(1, 2), 16);
				byte b = Convert.ToByte(rptColorString.Substring(3, 2), 16);
				byte b2 = Convert.ToByte(rptColorString.Substring(5, 2), 16);
				color = Color.FromArgb(num, (int)b, (int)b2);
			}
			else
			{
				color = Color.Empty;
			}
			return color;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00080AAE File Offset: 0x0007ECAE
		public static string ColorToRdfString(Color c)
		{
			if (c.IsNamedColor)
			{
				return ReportColor.ToName(c);
			}
			return string.Format(CultureInfo.InvariantCulture, "#{0:x6}", c.ToArgb() & 16777215);
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00080AE4 File Offset: 0x0007ECE4
		public bool IsEmpty
		{
			get
			{
				bool flag = false;
				if (this.IsColorRgb)
				{
					if (Color.Empty == this.ColorRgb)
					{
						flag = true;
					}
				}
				else if (this.ColorExpression == null || string.Empty == this.ColorExpression)
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00080B2E File Offset: 0x0007ED2E
		public void Empty()
		{
			this.m_Expression = null;
			this.m_colorRgb = Color.Empty;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00080B42 File Offset: 0x0007ED42
		internal static Color FromName(string name)
		{
			if (name == "LightGrey")
			{
				name = "LightGray";
			}
			return Color.FromName(name);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00080B60 File Offset: 0x0007ED60
		internal static string ToName(Color color)
		{
			string text = color.Name;
			if (text == "LightGray")
			{
				text = "LightGrey";
			}
			return text;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00080B89 File Offset: 0x0007ED89
		public override string ToString()
		{
			if (this.IsColorRgb)
			{
				return ReportColor.ColorToRdfString(this.ColorRgb);
			}
			return this.ColorExpression;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00080BA8 File Offset: 0x0007EDA8
		public override int GetHashCode()
		{
			int num;
			if (this.IsColorRgb)
			{
				num = this.m_colorRgb.GetHashCode();
			}
			else if (this.m_Expression != null)
			{
				num = this.m_Expression.GetHashCode();
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00080BEA File Offset: 0x0007EDEA
		public override bool Equals(object obj)
		{
			return obj != null && obj is ReportColor && this == (ReportColor)obj;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00080C0C File Offset: 0x0007EE0C
		public static bool operator ==(ReportColor left, ReportColor right)
		{
			bool flag = false;
			if (left.IsColorRgb == right.IsColorRgb)
			{
				if (left.IsColorRgb)
				{
					flag = left.ColorRgb == right.ColorRgb;
				}
				else
				{
					flag = left.ColorExpression == right.ColorExpression;
				}
			}
			return flag;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00080C5F File Offset: 0x0007EE5F
		public static bool operator !=(ReportColor left, ReportColor right)
		{
			return !(left == right);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00080C6C File Offset: 0x0007EE6C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text.Trim());
			reader.Skip();
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00080C94 File Offset: 0x0007EE94
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			string text = this.ToString();
			writer.WriteString(text);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00080CB5 File Offset: 0x0007EEB5
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return !this.IsEmpty && (this.m_Definition == null || this.IsExpression || !(this.m_colorRgb == this.m_Definition.Default));
		}

		// Token: 0x04000E9A RID: 3738
		private Color m_colorRgb;

		// Token: 0x04000E9B RID: 3739
		private string m_Expression;

		// Token: 0x04000E9C RID: 3740
		private ColorPropertyDef m_Definition;
	}
}
