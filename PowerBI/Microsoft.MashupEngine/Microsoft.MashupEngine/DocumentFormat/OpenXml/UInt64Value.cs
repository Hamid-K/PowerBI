using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002133 RID: 8499
	[CLSCompliant(false)]
	[DebuggerDisplay("{InnerText}")]
	internal class UInt64Value : OpenXmlSimpleValue<ulong>
	{
		// Token: 0x0600D2D4 RID: 53972 RVA: 0x0029E22C File Offset: 0x0029C42C
		public UInt64Value()
		{
		}

		// Token: 0x0600D2D5 RID: 53973 RVA: 0x0029E234 File Offset: 0x0029C434
		public UInt64Value(ulong value)
			: base(value)
		{
		}

		// Token: 0x0600D2D6 RID: 53974 RVA: 0x0029E23D File Offset: 0x0029C43D
		public UInt64Value(UInt64Value source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F8 RID: 13048
		// (get) Token: 0x0600D2D7 RID: 53975 RVA: 0x0029E254 File Offset: 0x0029C454
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

		// Token: 0x0600D2D8 RID: 53976 RVA: 0x0029E298 File Offset: 0x0029C498
		internal override void Parse()
		{
			base.InnerValue = new ulong?(XmlConvert.ToUInt64(base.TextValue));
		}

		// Token: 0x0600D2D9 RID: 53977 RVA: 0x0029E2B0 File Offset: 0x0029C4B0
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				ulong num = XmlConvert.ToUInt64(base.TextValue);
				base.InnerValue = new ulong?(num);
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

		// Token: 0x0600D2DA RID: 53978 RVA: 0x0029E310 File Offset: 0x0029C510
		public static implicit operator ulong(UInt64Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return UInt64Value.ToUInt64(xmlAttribute);
		}

		// Token: 0x0600D2DB RID: 53979 RVA: 0x0029E326 File Offset: 0x0029C526
		public static implicit operator UInt64Value(ulong value)
		{
			return UInt64Value.FromUInt64(value);
		}

		// Token: 0x0600D2DC RID: 53980 RVA: 0x0029E32E File Offset: 0x0029C52E
		public static UInt64Value FromUInt64(ulong value)
		{
			return new UInt64Value(value);
		}

		// Token: 0x0600D2DD RID: 53981 RVA: 0x0029E336 File Offset: 0x0029C536
		public static ulong ToUInt64(UInt64Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2DE RID: 53982 RVA: 0x0029E34C File Offset: 0x0029C54C
		internal override OpenXmlSimpleType CloneImp()
		{
			return new UInt64Value(this);
		}
	}
}
