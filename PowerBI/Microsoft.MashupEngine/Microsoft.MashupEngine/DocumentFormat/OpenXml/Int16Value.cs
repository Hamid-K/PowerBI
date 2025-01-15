using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200212D RID: 8493
	[DebuggerDisplay("{InnerText}")]
	internal class Int16Value : OpenXmlSimpleValue<short>
	{
		// Token: 0x0600D292 RID: 53906 RVA: 0x0029DB90 File Offset: 0x0029BD90
		public Int16Value()
		{
		}

		// Token: 0x0600D293 RID: 53907 RVA: 0x0029DB98 File Offset: 0x0029BD98
		public Int16Value(short value)
			: base(value)
		{
		}

		// Token: 0x0600D294 RID: 53908 RVA: 0x0029DBA1 File Offset: 0x0029BDA1
		public Int16Value(Int16Value source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032F2 RID: 13042
		// (get) Token: 0x0600D295 RID: 53909 RVA: 0x0029DBB8 File Offset: 0x0029BDB8
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

		// Token: 0x0600D296 RID: 53910 RVA: 0x0029DBFC File Offset: 0x0029BDFC
		internal override void Parse()
		{
			base.InnerValue = new short?(XmlConvert.ToInt16(base.TextValue));
		}

		// Token: 0x0600D297 RID: 53911 RVA: 0x0029DC14 File Offset: 0x0029BE14
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				short num = XmlConvert.ToInt16(base.TextValue);
				base.InnerValue = new short?(num);
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

		// Token: 0x0600D298 RID: 53912 RVA: 0x0029DC74 File Offset: 0x0029BE74
		public static implicit operator short(Int16Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return Int16Value.ToInt16(xmlAttribute);
		}

		// Token: 0x0600D299 RID: 53913 RVA: 0x0029DC8A File Offset: 0x0029BE8A
		public static implicit operator Int16Value(short value)
		{
			return Int16Value.FromInt16(value);
		}

		// Token: 0x0600D29A RID: 53914 RVA: 0x0029DC92 File Offset: 0x0029BE92
		public static Int16Value FromInt16(short value)
		{
			return new Int16Value(value);
		}

		// Token: 0x0600D29B RID: 53915 RVA: 0x0029DC9A File Offset: 0x0029BE9A
		public static short ToInt16(Int16Value xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D29C RID: 53916 RVA: 0x0029DCB0 File Offset: 0x0029BEB0
		internal override OpenXmlSimpleType CloneImp()
		{
			return new Int16Value(this);
		}
	}
}
