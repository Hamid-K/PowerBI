using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200212B RID: 8491
	[DebuggerDisplay("{InnerText}")]
	internal class ByteValue : OpenXmlSimpleValue<byte>
	{
		// Token: 0x0600D27C RID: 53884 RVA: 0x0029D940 File Offset: 0x0029BB40
		public ByteValue()
		{
		}

		// Token: 0x0600D27D RID: 53885 RVA: 0x0029D948 File Offset: 0x0029BB48
		public ByteValue(byte value)
			: base(value)
		{
		}

		// Token: 0x0600D27E RID: 53886 RVA: 0x0029D951 File Offset: 0x0029BB51
		public ByteValue(ByteValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F0 RID: 13040
		// (get) Token: 0x0600D27F RID: 53887 RVA: 0x0029D968 File Offset: 0x0029BB68
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

		// Token: 0x0600D280 RID: 53888 RVA: 0x0029D9AC File Offset: 0x0029BBAC
		internal override void Parse()
		{
			base.InnerValue = new byte?(XmlConvert.ToByte(base.TextValue));
		}

		// Token: 0x0600D281 RID: 53889 RVA: 0x0029D9C4 File Offset: 0x0029BBC4
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				byte b = XmlConvert.ToByte(base.TextValue);
				base.InnerValue = new byte?(b);
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

		// Token: 0x0600D282 RID: 53890 RVA: 0x0029DA24 File Offset: 0x0029BC24
		public static implicit operator byte(ByteValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return ByteValue.ToByte(xmlAttribute);
		}

		// Token: 0x0600D283 RID: 53891 RVA: 0x0029DA3A File Offset: 0x0029BC3A
		public static implicit operator ByteValue(byte value)
		{
			return ByteValue.FromByte(value);
		}

		// Token: 0x0600D284 RID: 53892 RVA: 0x0029DA42 File Offset: 0x0029BC42
		public static ByteValue FromByte(byte value)
		{
			return new ByteValue(value);
		}

		// Token: 0x0600D285 RID: 53893 RVA: 0x0029DA4A File Offset: 0x0029BC4A
		public static byte ToByte(ByteValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D286 RID: 53894 RVA: 0x0029DA60 File Offset: 0x0029BC60
		internal override OpenXmlSimpleType CloneImp()
		{
			return new ByteValue(this);
		}
	}
}
