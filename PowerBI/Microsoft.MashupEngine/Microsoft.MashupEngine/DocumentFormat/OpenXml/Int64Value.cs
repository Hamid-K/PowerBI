using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200212F RID: 8495
	[DebuggerDisplay("{InnerText}")]
	internal class Int64Value : OpenXmlSimpleValue<long>
	{
		// Token: 0x0600D2A8 RID: 53928 RVA: 0x0029DDE0 File Offset: 0x0029BFE0
		public Int64Value()
		{
		}

		// Token: 0x0600D2A9 RID: 53929 RVA: 0x0029DDE8 File Offset: 0x0029BFE8
		public Int64Value(long value)
			: base(value)
		{
		}

		// Token: 0x0600D2AA RID: 53930 RVA: 0x0029DDF1 File Offset: 0x0029BFF1
		public Int64Value(Int64Value source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F4 RID: 13044
		// (get) Token: 0x0600D2AB RID: 53931 RVA: 0x0029DE08 File Offset: 0x0029C008
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

		// Token: 0x0600D2AC RID: 53932 RVA: 0x0029DE4C File Offset: 0x0029C04C
		internal override void Parse()
		{
			base.InnerValue = new long?(XmlConvert.ToInt64(base.TextValue));
		}

		// Token: 0x0600D2AD RID: 53933 RVA: 0x0029DE64 File Offset: 0x0029C064
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				long num = XmlConvert.ToInt64(base.TextValue);
				base.InnerValue = new long?(num);
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

		// Token: 0x0600D2AE RID: 53934 RVA: 0x0029DEC4 File Offset: 0x0029C0C4
		public static implicit operator long(Int64Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return Int64Value.ToInt64(xmlAttribute);
		}

		// Token: 0x0600D2AF RID: 53935 RVA: 0x0029DEDA File Offset: 0x0029C0DA
		public static implicit operator Int64Value(long value)
		{
			return Int64Value.FromInt64(value);
		}

		// Token: 0x0600D2B0 RID: 53936 RVA: 0x0029DEE2 File Offset: 0x0029C0E2
		public static Int64Value FromInt64(long value)
		{
			return new Int64Value(value);
		}

		// Token: 0x0600D2B1 RID: 53937 RVA: 0x0029DEEA File Offset: 0x0029C0EA
		public static long ToInt64(Int64Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2B2 RID: 53938 RVA: 0x0029DF00 File Offset: 0x0029C100
		internal override OpenXmlSimpleType CloneImp()
		{
			return new Int64Value(this);
		}
	}
}
