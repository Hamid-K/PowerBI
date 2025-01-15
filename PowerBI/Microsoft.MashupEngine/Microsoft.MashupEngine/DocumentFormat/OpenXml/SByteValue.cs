using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200212C RID: 8492
	[DebuggerDisplay("{InnerText}")]
	[CLSCompliant(false)]
	internal class SByteValue : OpenXmlSimpleValue<sbyte>
	{
		// Token: 0x0600D287 RID: 53895 RVA: 0x0029DA68 File Offset: 0x0029BC68
		public SByteValue()
		{
		}

		// Token: 0x0600D288 RID: 53896 RVA: 0x0029DA70 File Offset: 0x0029BC70
		public SByteValue(sbyte value)
			: base(value)
		{
		}

		// Token: 0x0600D289 RID: 53897 RVA: 0x0029DA79 File Offset: 0x0029BC79
		public SByteValue(SByteValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F1 RID: 13041
		// (get) Token: 0x0600D28A RID: 53898 RVA: 0x0029DA90 File Offset: 0x0029BC90
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

		// Token: 0x0600D28B RID: 53899 RVA: 0x0029DAD4 File Offset: 0x0029BCD4
		internal override void Parse()
		{
			base.InnerValue = new sbyte?(XmlConvert.ToSByte(base.TextValue));
		}

		// Token: 0x0600D28C RID: 53900 RVA: 0x0029DAEC File Offset: 0x0029BCEC
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				sbyte b = XmlConvert.ToSByte(base.TextValue);
				base.InnerValue = new sbyte?(b);
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

		// Token: 0x0600D28D RID: 53901 RVA: 0x0029DB4C File Offset: 0x0029BD4C
		public static implicit operator sbyte(SByteValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return SByteValue.ToSByte(xmlAttribute);
		}

		// Token: 0x0600D28E RID: 53902 RVA: 0x0029DB62 File Offset: 0x0029BD62
		public static implicit operator SByteValue(sbyte value)
		{
			return SByteValue.FromSByte(value);
		}

		// Token: 0x0600D28F RID: 53903 RVA: 0x0029DB6A File Offset: 0x0029BD6A
		public static SByteValue FromSByte(sbyte value)
		{
			return new SByteValue(value);
		}

		// Token: 0x0600D290 RID: 53904 RVA: 0x0029DB72 File Offset: 0x0029BD72
		public static sbyte ToSByte(SByteValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D291 RID: 53905 RVA: 0x0029DB88 File Offset: 0x0029BD88
		internal override OpenXmlSimpleType CloneImp()
		{
			return new SByteValue(this);
		}
	}
}
