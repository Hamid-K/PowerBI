using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003DB RID: 987
	[TypeConverter(typeof(Expression.ExpressionConverter))]
	[Serializable]
	public sealed class Expression : IXmlSerializable
	{
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x0007E51B File Offset: 0x0007C71B
		public bool IsEmpty
		{
			get
			{
				return this.String == null || this.String.Length == 0;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0007E535 File Offset: 0x0007C735
		public bool IsExpression
		{
			get
			{
				return Expression.IsExpressionString(this.String);
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0007E542 File Offset: 0x0007C742
		public static bool IsExpressionString(string value)
		{
			return !string.IsNullOrEmpty(value) && value[0] == '=';
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0007E559 File Offset: 0x0007C759
		public Expression()
			: this(string.Empty)
		{
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0007E566 File Offset: 0x0007C766
		public Expression(string expression)
		{
			this.String = expression;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0007E575 File Offset: 0x0007C775
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0007E57D File Offset: 0x0007C77D
		public override bool Equals(object obj)
		{
			return obj is Expression && this.String == ((Expression)obj).String;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0007E59F File Offset: 0x0007C79F
		public override int GetHashCode()
		{
			if (this.String != null)
			{
				return this.String.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0007E5B6 File Offset: 0x0007C7B6
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.String = reader.ReadString();
			reader.Skip();
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0007E5CA File Offset: 0x0007C7CA
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.String);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0007E5D8 File Offset: 0x0007C7D8
		internal static string FieldValueExpression(Field field)
		{
			return "Fields!" + field.Name + ".Value";
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0007E5EF File Offset: 0x0007C7EF
		internal static string FieldValueExpressionString(Field field)
		{
			return "=Fields!" + field.Name + ".Value";
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0007E606 File Offset: 0x0007C806
		internal static string ExpressionString(string value)
		{
			return "=" + value;
		}

		// Token: 0x04000DB9 RID: 3513
		public string String;

		// Token: 0x02000518 RID: 1304
		internal sealed class ExpressionConverter : TypeConverter
		{
			// Token: 0x06002516 RID: 9494 RVA: 0x0007FD42 File Offset: 0x0007DF42
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06002517 RID: 9495 RVA: 0x00087884 File Offset: 0x00085A84
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06002518 RID: 9496 RVA: 0x000878A2 File Offset: 0x00085AA2
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new Expression(((string)value).Trim());
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x06002519 RID: 9497 RVA: 0x000878C6 File Offset: 0x00085AC6
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string) && value is Expression)
				{
					return ((Expression)value).String;
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
