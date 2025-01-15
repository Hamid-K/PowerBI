using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002130 RID: 8496
	[DebuggerDisplay("{InnerText}")]
	internal class IntegerValue : OpenXmlSimpleValue<long>
	{
		// Token: 0x0600D2B3 RID: 53939 RVA: 0x0029DDE0 File Offset: 0x0029BFE0
		public IntegerValue()
		{
		}

		// Token: 0x0600D2B4 RID: 53940 RVA: 0x0029DDE8 File Offset: 0x0029BFE8
		public IntegerValue(long value)
			: base(value)
		{
		}

		// Token: 0x0600D2B5 RID: 53941 RVA: 0x0029DDF1 File Offset: 0x0029BFF1
		public IntegerValue(IntegerValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F5 RID: 13045
		// (get) Token: 0x0600D2B6 RID: 53942 RVA: 0x0029DF08 File Offset: 0x0029C108
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

		// Token: 0x0600D2B7 RID: 53943 RVA: 0x0029DE4C File Offset: 0x0029C04C
		internal override void Parse()
		{
			base.InnerValue = new long?(XmlConvert.ToInt64(base.TextValue));
		}

		// Token: 0x0600D2B8 RID: 53944 RVA: 0x0029DF4C File Offset: 0x0029C14C
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

		// Token: 0x0600D2B9 RID: 53945 RVA: 0x0029DFAC File Offset: 0x0029C1AC
		public static implicit operator long(IntegerValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return IntegerValue.ToInt64(xmlAttribute);
		}

		// Token: 0x0600D2BA RID: 53946 RVA: 0x0029DFC2 File Offset: 0x0029C1C2
		public static implicit operator IntegerValue(long value)
		{
			return IntegerValue.FromInt64(value);
		}

		// Token: 0x0600D2BB RID: 53947 RVA: 0x0029DFCA File Offset: 0x0029C1CA
		public static IntegerValue FromInt64(long value)
		{
			return new IntegerValue(value);
		}

		// Token: 0x0600D2BC RID: 53948 RVA: 0x0029DEEA File Offset: 0x0029C0EA
		public static long ToInt64(IntegerValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2BD RID: 53949 RVA: 0x0029DFD2 File Offset: 0x0029C1D2
		internal override OpenXmlSimpleType CloneImp()
		{
			return new IntegerValue(this);
		}
	}
}
