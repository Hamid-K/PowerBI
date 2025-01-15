using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200212A RID: 8490
	[DebuggerDisplay("{InnerText}")]
	internal class BooleanValue : OpenXmlSimpleValue<bool>
	{
		// Token: 0x0600D271 RID: 53873 RVA: 0x0029D819 File Offset: 0x0029BA19
		public BooleanValue()
		{
		}

		// Token: 0x0600D272 RID: 53874 RVA: 0x0029D821 File Offset: 0x0029BA21
		public BooleanValue(bool value)
			: base(value)
		{
		}

		// Token: 0x0600D273 RID: 53875 RVA: 0x0029D82A File Offset: 0x0029BA2A
		public BooleanValue(BooleanValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032EF RID: 13039
		// (get) Token: 0x0600D274 RID: 53876 RVA: 0x0029D844 File Offset: 0x0029BA44
		public override string InnerText
		{
			get
			{
				if (base.TextValue == null && base.InnerValue != null)
				{
					base.TextValue = (base.InnerValue.Value ? "1" : "0");
				}
				return base.TextValue;
			}
		}

		// Token: 0x0600D275 RID: 53877 RVA: 0x0029D891 File Offset: 0x0029BA91
		internal override void Parse()
		{
			base.InnerValue = new bool?(XmlConvert.ToBoolean(base.TextValue));
		}

		// Token: 0x0600D276 RID: 53878 RVA: 0x0029D8AC File Offset: 0x0029BAAC
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag2;
			try
			{
				bool flag = XmlConvert.ToBoolean(base.TextValue);
				base.InnerValue = new bool?(flag);
				flag2 = true;
			}
			catch (FormatException)
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600D277 RID: 53879 RVA: 0x0029D8FC File Offset: 0x0029BAFC
		public static implicit operator bool(BooleanValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return BooleanValue.ToBoolean(xmlAttribute);
		}

		// Token: 0x0600D278 RID: 53880 RVA: 0x0029D912 File Offset: 0x0029BB12
		public static implicit operator BooleanValue(bool value)
		{
			return BooleanValue.FromBoolean(value);
		}

		// Token: 0x0600D279 RID: 53881 RVA: 0x0029D91A File Offset: 0x0029BB1A
		public static BooleanValue FromBoolean(bool value)
		{
			return new BooleanValue(value);
		}

		// Token: 0x0600D27A RID: 53882 RVA: 0x0029D922 File Offset: 0x0029BB22
		public static bool ToBoolean(BooleanValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D27B RID: 53883 RVA: 0x0029D938 File Offset: 0x0029BB38
		internal override OpenXmlSimpleType CloneImp()
		{
			return new BooleanValue(this);
		}
	}
}
