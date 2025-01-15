using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000417 RID: 1047
	public struct StyleUnit : IXmlSerializable, IVoluntarySerializable
	{
		// Token: 0x0600214C RID: 8524 RVA: 0x00080CEC File Offset: 0x0007EEEC
		public StyleUnit(UnitPropertyDef definition)
		{
			this.m_Definition = definition;
			this.m_Expression = null;
			this.m_baseUnit = definition.Default;
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x00080D08 File Offset: 0x0007EF08
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x00080D10 File Offset: 0x0007EF10
		public Unit BaseUnit
		{
			get
			{
				return this.m_baseUnit;
			}
			set
			{
				this.m_baseUnit = value;
				this.m_Expression = null;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00080D20 File Offset: 0x0007EF20
		// (set) Token: 0x06002150 RID: 8528 RVA: 0x00080D28 File Offset: 0x0007EF28
		public string Expression
		{
			get
			{
				return this.m_Expression;
			}
			set
			{
				this.m_Expression = value;
				this.m_baseUnit.Empty();
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00080D3C File Offset: 0x0007EF3C
		public bool IsUnit
		{
			get
			{
				return !this.m_baseUnit.IsEmpty;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x00080D4C File Offset: 0x0007EF4C
		public bool IsExpression
		{
			get
			{
				return this.m_Expression != null;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00080D57 File Offset: 0x0007EF57
		public bool IsEmpty
		{
			get
			{
				return this.m_Expression == null && this.m_baseUnit.IsEmpty;
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00080D6E File Offset: 0x0007EF6E
		public void Empty()
		{
			this.m_Expression = null;
			this.m_baseUnit.Empty();
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00080D84 File Offset: 0x0007EF84
		internal void Init(string initString, CultureInfo culture, UnitType defaultType)
		{
			string text = initString.Trim();
			if (text.Length != 0)
			{
				if (text[0] == '=')
				{
					this.Expression = text;
					return;
				}
				this.BaseUnit = new Unit(initString, culture, UnitType.Point);
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00080DC4 File Offset: 0x0007EFC4
		public override int GetHashCode()
		{
			if (this.IsUnit)
			{
				return this.BaseUnit.GetHashCode();
			}
			if (this.Expression != null)
			{
				return this.Expression.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00080E03 File Offset: 0x0007F003
		public override bool Equals(object obj)
		{
			return obj != null && obj is StyleUnit && this == (StyleUnit)obj;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00080E23 File Offset: 0x0007F023
		public static bool operator ==(StyleUnit left, StyleUnit right)
		{
			return left.m_Expression == right.m_Expression && left.m_baseUnit == right.m_baseUnit;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00080E4B File Offset: 0x0007F04B
		public static bool operator !=(StyleUnit left, StyleUnit right)
		{
			return !(left == right);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00080E57 File Offset: 0x0007F057
		public override string ToString()
		{
			if (!this.IsUnit)
			{
				return this.m_Expression;
			}
			return this.m_baseUnit.ToString();
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00080E7C File Offset: 0x0007F07C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text, CultureInfo.InvariantCulture, UnitType.Invalid);
			reader.Skip();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00080EA4 File Offset: 0x0007F0A4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			string text = this.ToString();
			writer.WriteString(text);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00080EC5 File Offset: 0x0007F0C5
		public bool ShouldBeSerialized()
		{
			return !this.IsEmpty && (this.m_Definition == null || this.IsExpression || !(this.m_baseUnit == this.m_Definition.Default));
		}

		// Token: 0x04000E9D RID: 3741
		private Unit m_baseUnit;

		// Token: 0x04000E9E RID: 3742
		private string m_Expression;

		// Token: 0x04000E9F RID: 3743
		private UnitPropertyDef m_Definition;
	}
}
