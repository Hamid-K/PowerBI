using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200212E RID: 8494
	[DebuggerDisplay("{InnerText}")]
	internal class Int32Value : OpenXmlSimpleValue<int>
	{
		// Token: 0x0600D29D RID: 53917 RVA: 0x0029DCB8 File Offset: 0x0029BEB8
		public Int32Value()
		{
		}

		// Token: 0x0600D29E RID: 53918 RVA: 0x0029DCC0 File Offset: 0x0029BEC0
		public Int32Value(int value)
			: base(value)
		{
		}

		// Token: 0x0600D29F RID: 53919 RVA: 0x0029DCC9 File Offset: 0x0029BEC9
		public Int32Value(Int32Value source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F3 RID: 13043
		// (get) Token: 0x0600D2A0 RID: 53920 RVA: 0x0029DCE0 File Offset: 0x0029BEE0
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

		// Token: 0x0600D2A1 RID: 53921 RVA: 0x0029DD24 File Offset: 0x0029BF24
		internal override void Parse()
		{
			base.InnerValue = new int?(XmlConvert.ToInt32(base.TextValue));
		}

		// Token: 0x0600D2A2 RID: 53922 RVA: 0x0029DD3C File Offset: 0x0029BF3C
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				int num = XmlConvert.ToInt32(base.TextValue);
				base.InnerValue = new int?(num);
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

		// Token: 0x0600D2A3 RID: 53923 RVA: 0x0029DD9C File Offset: 0x0029BF9C
		public static implicit operator int(Int32Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return Int32Value.ToInt32(xmlAttribute);
		}

		// Token: 0x0600D2A4 RID: 53924 RVA: 0x0029DDB2 File Offset: 0x0029BFB2
		public static implicit operator Int32Value(int value)
		{
			return Int32Value.FromInt32(value);
		}

		// Token: 0x0600D2A5 RID: 53925 RVA: 0x0029DDBA File Offset: 0x0029BFBA
		public static Int32Value FromInt32(int value)
		{
			return new Int32Value(value);
		}

		// Token: 0x0600D2A6 RID: 53926 RVA: 0x0029DDC2 File Offset: 0x0029BFC2
		public static int ToInt32(Int32Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2A7 RID: 53927 RVA: 0x0029DDD8 File Offset: 0x0029BFD8
		internal override OpenXmlSimpleType CloneImp()
		{
			return new Int32Value(this);
		}
	}
}
