using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002131 RID: 8497
	[CLSCompliant(false)]
	[DebuggerDisplay("{InnerText}")]
	internal class UInt16Value : OpenXmlSimpleValue<ushort>
	{
		// Token: 0x0600D2BE RID: 53950 RVA: 0x0029DFDA File Offset: 0x0029C1DA
		public UInt16Value()
		{
		}

		// Token: 0x0600D2BF RID: 53951 RVA: 0x0029DFE2 File Offset: 0x0029C1E2
		public UInt16Value(ushort value)
			: base(value)
		{
		}

		// Token: 0x0600D2C0 RID: 53952 RVA: 0x0029DFEB File Offset: 0x0029C1EB
		public UInt16Value(UInt16Value source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F6 RID: 13046
		// (get) Token: 0x0600D2C1 RID: 53953 RVA: 0x0029E004 File Offset: 0x0029C204
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

		// Token: 0x0600D2C2 RID: 53954 RVA: 0x0029E048 File Offset: 0x0029C248
		internal override void Parse()
		{
			base.InnerValue = new ushort?(XmlConvert.ToUInt16(base.TextValue));
		}

		// Token: 0x0600D2C3 RID: 53955 RVA: 0x0029E060 File Offset: 0x0029C260
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				ushort num = XmlConvert.ToUInt16(base.TextValue);
				base.InnerValue = new ushort?(num);
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

		// Token: 0x0600D2C4 RID: 53956 RVA: 0x0029E0C0 File Offset: 0x0029C2C0
		public static implicit operator ushort(UInt16Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return UInt16Value.ToUInt16(xmlAttribute);
		}

		// Token: 0x0600D2C5 RID: 53957 RVA: 0x0029E0D6 File Offset: 0x0029C2D6
		public static implicit operator UInt16Value(ushort value)
		{
			return UInt16Value.FromUInt16(value);
		}

		// Token: 0x0600D2C6 RID: 53958 RVA: 0x0029E0DE File Offset: 0x0029C2DE
		public static UInt16Value FromUInt16(ushort value)
		{
			return new UInt16Value(value);
		}

		// Token: 0x0600D2C7 RID: 53959 RVA: 0x0029E0E6 File Offset: 0x0029C2E6
		public static ushort ToUInt16(UInt16Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2C8 RID: 53960 RVA: 0x0029E0FC File Offset: 0x0029C2FC
		internal override OpenXmlSimpleType CloneImp()
		{
			return new UInt16Value(this);
		}
	}
}
