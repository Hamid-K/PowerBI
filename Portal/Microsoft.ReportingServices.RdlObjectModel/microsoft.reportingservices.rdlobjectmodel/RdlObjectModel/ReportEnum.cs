using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D5 RID: 469
	internal struct ReportEnum<T> : IXmlSerializable, IFormattable where T : struct, IConvertible
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00024EB7 File Offset: 0x000230B7
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x00024EBF File Offset: 0x000230BF
		public T Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00024EC8 File Offset: 0x000230C8
		public static IList<string> GetNames()
		{
			return ReportEnum<T>.m_names;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00024ED0 File Offset: 0x000230D0
		static ReportEnum()
		{
			object[] customAttributes = typeof(T).GetCustomAttributes(typeof(EnumNamesAttribute), false);
			if (customAttributes.Length != 0)
			{
				ReportEnum<T>.m_names = ((EnumNamesAttribute)customAttributes[0]).Names;
				return;
			}
			ReportEnum<T>.m_names = Enum.GetNames(typeof(T));
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00024F23 File Offset: 0x00023123
		public ReportEnum(T value)
		{
			this.m_value = value;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00024F2C File Offset: 0x0002312C
		public ReportEnum(string value)
		{
			this.m_value = default(T);
			this.Init(value);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00024F44 File Offset: 0x00023144
		private void Init(string value)
		{
			int num = ReportEnum<T>.m_names.IndexOf(value);
			if (num < 0)
			{
				throw new ArgumentException(SRErrorsWrapper.InvalidValue(value));
			}
			this.m_value = (T)((object)Enum.ToObject(typeof(T), num));
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00024F88 File Offset: 0x00023188
		public static ReportEnum<T> Parse(string value, IFormatProvider provider)
		{
			return new ReportEnum<T>(value);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00024F90 File Offset: 0x00023190
		public override string ToString()
		{
			return ReportEnum<T>.m_names[this.m_value.ToInt32(CultureInfo.InvariantCulture)];
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00024FB1 File Offset: 0x000231B1
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00024FC0 File Offset: 0x000231C0
		public override bool Equals(object value)
		{
			if (value is ReportEnum<T>)
			{
				return this.m_value.Equals(((ReportEnum<T>)value).Value);
			}
			return this.m_value.Equals(value);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002500C File Offset: 0x0002320C
		public static bool operator ==(ReportEnum<T> left, ReportEnum<T> right)
		{
			T value = left.Value;
			return value.Equals(right.Value);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002503C File Offset: 0x0002323C
		public static bool operator !=(ReportEnum<T> left, ReportEnum<T> right)
		{
			T value = left.Value;
			return !value.Equals(right.Value);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002506D File Offset: 0x0002326D
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00025080 File Offset: 0x00023280
		public static implicit operator T(ReportEnum<T> value)
		{
			return value.Value;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00025089 File Offset: 0x00023289
		public static implicit operator ReportEnum<T>(T value)
		{
			return new ReportEnum<T>(value);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00025091 File Offset: 0x00023291
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00025094 File Offset: 0x00023294
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000250AF File Offset: 0x000232AF
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.ToString());
		}

		// Token: 0x0400055B RID: 1371
		private T m_value;

		// Token: 0x0400055C RID: 1372
		private static readonly IList<string> m_names;
	}
}
