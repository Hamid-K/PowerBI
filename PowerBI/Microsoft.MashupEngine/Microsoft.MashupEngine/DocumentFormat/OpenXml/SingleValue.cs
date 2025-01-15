using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002135 RID: 8501
	[DebuggerDisplay("{InnerText}")]
	internal class SingleValue : OpenXmlSimpleValue<float>
	{
		// Token: 0x0600D2EA RID: 53994 RVA: 0x0029E47C File Offset: 0x0029C67C
		public SingleValue()
		{
		}

		// Token: 0x0600D2EB RID: 53995 RVA: 0x0029E484 File Offset: 0x0029C684
		public SingleValue(float value)
			: base(value)
		{
		}

		// Token: 0x0600D2EC RID: 53996 RVA: 0x0029E48D File Offset: 0x0029C68D
		public SingleValue(SingleValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032FA RID: 13050
		// (get) Token: 0x0600D2ED RID: 53997 RVA: 0x0029E4A4 File Offset: 0x0029C6A4
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

		// Token: 0x0600D2EE RID: 53998 RVA: 0x0029E4E8 File Offset: 0x0029C6E8
		internal override void Parse()
		{
			float num = XmlConvert.ToSingle(base.TextValue);
			base.InnerValue = new float?(num);
		}

		// Token: 0x0600D2EF RID: 53999 RVA: 0x0029E510 File Offset: 0x0029C710
		internal override bool TryParse()
		{
			base.InnerValue = null;
			bool flag;
			try
			{
				this.Parse();
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

		// Token: 0x0600D2F0 RID: 54000 RVA: 0x0029E560 File Offset: 0x0029C760
		public static implicit operator float(SingleValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return SingleValue.ToSingle(xmlAttribute);
		}

		// Token: 0x0600D2F1 RID: 54001 RVA: 0x0029E576 File Offset: 0x0029C776
		public static implicit operator SingleValue(float value)
		{
			return SingleValue.FromSingle(value);
		}

		// Token: 0x0600D2F2 RID: 54002 RVA: 0x0029E57E File Offset: 0x0029C77E
		public static SingleValue FromSingle(float value)
		{
			return new SingleValue(value);
		}

		// Token: 0x0600D2F3 RID: 54003 RVA: 0x0029E586 File Offset: 0x0029C786
		public static float ToSingle(SingleValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D2F4 RID: 54004 RVA: 0x0029E59C File Offset: 0x0029C79C
		internal override OpenXmlSimpleType CloneImp()
		{
			return new SingleValue(this);
		}
	}
}
