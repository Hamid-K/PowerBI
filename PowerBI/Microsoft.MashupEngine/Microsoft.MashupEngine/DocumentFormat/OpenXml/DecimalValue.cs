using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002134 RID: 8500
	[DebuggerDisplay("{InnerText}")]
	internal class DecimalValue : OpenXmlSimpleValue<decimal>
	{
		// Token: 0x0600D2DF RID: 53983 RVA: 0x0029E354 File Offset: 0x0029C554
		public DecimalValue()
		{
		}

		// Token: 0x0600D2E0 RID: 53984 RVA: 0x0029E35C File Offset: 0x0029C55C
		public DecimalValue(decimal value)
			: base(value)
		{
		}

		// Token: 0x0600D2E1 RID: 53985 RVA: 0x0029E365 File Offset: 0x0029C565
		public DecimalValue(DecimalValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F9 RID: 13049
		// (get) Token: 0x0600D2E2 RID: 53986 RVA: 0x0029E37C File Offset: 0x0029C57C
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

		// Token: 0x0600D2E3 RID: 53987 RVA: 0x0029E3C0 File Offset: 0x0029C5C0
		internal override void Parse()
		{
			base.InnerValue = new decimal?(XmlConvert.ToDecimal(base.TextValue));
		}

		// Token: 0x0600D2E4 RID: 53988 RVA: 0x0029E3D8 File Offset: 0x0029C5D8
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				decimal num = XmlConvert.ToDecimal(base.TextValue);
				base.InnerValue = new decimal?(num);
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

		// Token: 0x0600D2E5 RID: 53989 RVA: 0x0029E438 File Offset: 0x0029C638
		public static implicit operator decimal(DecimalValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return DecimalValue.ToDecimal(xmlAttribute);
		}

		// Token: 0x0600D2E6 RID: 53990 RVA: 0x0029E44E File Offset: 0x0029C64E
		public static implicit operator DecimalValue(decimal value)
		{
			return DecimalValue.FromDecimal(value);
		}

		// Token: 0x0600D2E7 RID: 53991 RVA: 0x0029E456 File Offset: 0x0029C656
		public static DecimalValue FromDecimal(decimal value)
		{
			return new DecimalValue(value);
		}

		// Token: 0x0600D2E8 RID: 53992 RVA: 0x0029E45E File Offset: 0x0029C65E
		public static decimal ToDecimal(DecimalValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2E9 RID: 53993 RVA: 0x0029E474 File Offset: 0x0029C674
		internal override OpenXmlSimpleType CloneImp()
		{
			return new DecimalValue(this);
		}
	}
}
