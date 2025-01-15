using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002132 RID: 8498
	[DebuggerDisplay("{InnerText}")]
	[CLSCompliant(false)]
	internal class UInt32Value : OpenXmlSimpleValue<uint>
	{
		// Token: 0x0600D2C9 RID: 53961 RVA: 0x0029E104 File Offset: 0x0029C304
		public UInt32Value()
		{
		}

		// Token: 0x0600D2CA RID: 53962 RVA: 0x0029E10C File Offset: 0x0029C30C
		public UInt32Value(uint value)
			: base(value)
		{
		}

		// Token: 0x0600D2CB RID: 53963 RVA: 0x0029E115 File Offset: 0x0029C315
		public UInt32Value(UInt32Value source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F7 RID: 13047
		// (get) Token: 0x0600D2CC RID: 53964 RVA: 0x0029E12C File Offset: 0x0029C32C
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

		// Token: 0x0600D2CD RID: 53965 RVA: 0x0029E170 File Offset: 0x0029C370
		internal override void Parse()
		{
			base.InnerValue = new uint?(XmlConvert.ToUInt32(base.TextValue));
		}

		// Token: 0x0600D2CE RID: 53966 RVA: 0x0029E188 File Offset: 0x0029C388
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				uint num = XmlConvert.ToUInt32(base.TextValue);
				base.InnerValue = new uint?(num);
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

		// Token: 0x0600D2CF RID: 53967 RVA: 0x0029E1E8 File Offset: 0x0029C3E8
		public static implicit operator uint(UInt32Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return UInt32Value.ToUInt32(xmlAttribute);
		}

		// Token: 0x0600D2D0 RID: 53968 RVA: 0x0029E1FE File Offset: 0x0029C3FE
		public static implicit operator UInt32Value(uint value)
		{
			return UInt32Value.FromUInt32(value);
		}

		// Token: 0x0600D2D1 RID: 53969 RVA: 0x0029E206 File Offset: 0x0029C406
		public static UInt32Value FromUInt32(uint value)
		{
			return new UInt32Value(value);
		}

		// Token: 0x0600D2D2 RID: 53970 RVA: 0x0029E20E File Offset: 0x0029C40E
		public static uint ToUInt32(UInt32Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2D3 RID: 53971 RVA: 0x0029E224 File Offset: 0x0029C424
		internal override OpenXmlSimpleType CloneImp()
		{
			return new UInt32Value(this);
		}
	}
}
