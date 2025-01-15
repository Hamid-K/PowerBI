using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002137 RID: 8503
	[DebuggerDisplay("{InnerText}")]
	internal class DateTimeValue : OpenXmlSimpleValue<DateTime>
	{
		// Token: 0x0600D300 RID: 54016 RVA: 0x0029E6CC File Offset: 0x0029C8CC
		public DateTimeValue()
		{
		}

		// Token: 0x0600D301 RID: 54017 RVA: 0x0029E6D4 File Offset: 0x0029C8D4
		public DateTimeValue(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600D302 RID: 54018 RVA: 0x0029E6DD File Offset: 0x0029C8DD
		public DateTimeValue(DateTimeValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032FC RID: 13052
		// (get) Token: 0x0600D303 RID: 54019 RVA: 0x0029E6F4 File Offset: 0x0029C8F4
		public override string InnerText
		{
			get
			{
				if (base.TextValue == null && base.InnerValue != null)
				{
					base.TextValue = XmlConvert.ToString(base.InnerValue.Value, XmlDateTimeSerializationMode.RoundtripKind);
				}
				return base.TextValue;
			}
		}

		// Token: 0x0600D304 RID: 54020 RVA: 0x0029E739 File Offset: 0x0029C939
		internal override void Parse()
		{
			base.InnerValue = new DateTime?(XmlConvert.ToDateTime(base.TextValue, XmlDateTimeSerializationMode.Utc));
		}

		// Token: 0x0600D305 RID: 54021 RVA: 0x0029E754 File Offset: 0x0029C954
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				DateTime dateTime = XmlConvert.ToDateTime(base.TextValue, XmlDateTimeSerializationMode.Utc);
				base.InnerValue = new DateTime?(dateTime);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600D306 RID: 54022 RVA: 0x0029E7A4 File Offset: 0x0029C9A4
		public static implicit operator DateTime(DateTimeValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return DateTimeValue.ToDateTime(xmlAttribute);
		}

		// Token: 0x0600D307 RID: 54023 RVA: 0x0029E7BA File Offset: 0x0029C9BA
		public static implicit operator DateTimeValue(DateTime value)
		{
			return DateTimeValue.FromDateTime(value);
		}

		// Token: 0x0600D308 RID: 54024 RVA: 0x0029E7C2 File Offset: 0x0029C9C2
		public static DateTimeValue FromDateTime(DateTime value)
		{
			return new DateTimeValue(value);
		}

		// Token: 0x0600D309 RID: 54025 RVA: 0x0029E7CA File Offset: 0x0029C9CA
		public static DateTime ToDateTime(DateTimeValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D30A RID: 54026 RVA: 0x0029E7E0 File Offset: 0x0029C9E0
		internal override OpenXmlSimpleType CloneImp()
		{
			return new DateTimeValue(this);
		}
	}
}
