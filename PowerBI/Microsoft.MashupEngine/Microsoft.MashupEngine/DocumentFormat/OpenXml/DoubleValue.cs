using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002136 RID: 8502
	[DebuggerDisplay("{InnerText}")]
	internal class DoubleValue : OpenXmlSimpleValue<double>
	{
		// Token: 0x0600D2F5 RID: 54005 RVA: 0x0029E5A4 File Offset: 0x0029C7A4
		public DoubleValue()
		{
		}

		// Token: 0x0600D2F6 RID: 54006 RVA: 0x0029E5AC File Offset: 0x0029C7AC
		public DoubleValue(double value)
			: base(value)
		{
		}

		// Token: 0x0600D2F7 RID: 54007 RVA: 0x0029E5B5 File Offset: 0x0029C7B5
		public DoubleValue(DoubleValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032FB RID: 13051
		// (get) Token: 0x0600D2F8 RID: 54008 RVA: 0x0029E5CC File Offset: 0x0029C7CC
		public override string InnerText
		{
			get
			{
				if (base.TextValue == null && base.InnerValue != null)
				{
					base.TextValue = XmlConvert.ToString(base.InnerValue.Value);
				}
				return base.TextValue;
			}
		}

		// Token: 0x0600D2F9 RID: 54009 RVA: 0x0029E610 File Offset: 0x0029C810
		internal override void Parse()
		{
			base.InnerValue = new double?(XmlConvert.ToDouble(base.TextValue));
		}

		// Token: 0x0600D2FA RID: 54010 RVA: 0x0029E628 File Offset: 0x0029C828
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				double num = XmlConvert.ToDouble(base.TextValue);
				base.InnerValue = new double?(num);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (OverflowException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600D2FB RID: 54011 RVA: 0x0029E688 File Offset: 0x0029C888
		public static implicit operator double(DoubleValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return DoubleValue.ToDouble(xmlAttribute);
		}

		// Token: 0x0600D2FC RID: 54012 RVA: 0x0029E69E File Offset: 0x0029C89E
		public static implicit operator DoubleValue(double value)
		{
			return DoubleValue.FromDouble(value);
		}

		// Token: 0x0600D2FD RID: 54013 RVA: 0x0029E6A6 File Offset: 0x0029C8A6
		public static DoubleValue FromDouble(double value)
		{
			return new DoubleValue(value);
		}

		// Token: 0x0600D2FE RID: 54014 RVA: 0x0029E6AE File Offset: 0x0029C8AE
		public static double ToDouble(DoubleValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2FF RID: 54015 RVA: 0x0029E6C4 File Offset: 0x0029C8C4
		internal override OpenXmlSimpleType CloneImp()
		{
			return new DoubleValue(this);
		}
	}
}
