using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002140 RID: 8512
	[DebuggerDisplay("{InnerText}")]
	internal class TrueFalseValue : OpenXmlSimpleType
	{
		// Token: 0x0600D35D RID: 54109 RVA: 0x0029F3E8 File Offset: 0x0029D5E8
		public TrueFalseValue()
		{
			this.Initialize();
		}

		// Token: 0x0600D35E RID: 54110 RVA: 0x0029F3F6 File Offset: 0x0029D5F6
		public TrueFalseValue(bool value)
		{
			this.Initialize();
			this.Value = value;
		}

		// Token: 0x0600D35F RID: 54111 RVA: 0x0029F40B File Offset: 0x0029D60B
		public TrueFalseValue(TrueFalseValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.Initialize();
			this._impl.InnerText = source.InnerText;
		}

		// Token: 0x0600D360 RID: 54112 RVA: 0x0029F439 File Offset: 0x0029D639
		private void Initialize()
		{
			this._impl = new TrueFalseValueImpl(new Func<string, bool>(this.GetBooleanValue), new Func<bool, string>(this.GetDefaultTextValue));
		}

		// Token: 0x1700330C RID: 13068
		// (get) Token: 0x0600D361 RID: 54113 RVA: 0x0029F45E File Offset: 0x0029D65E
		public override bool HasValue
		{
			get
			{
				return this._impl.HasValue;
			}
		}

		// Token: 0x1700330D RID: 13069
		// (get) Token: 0x0600D362 RID: 54114 RVA: 0x0029F46B File Offset: 0x0029D66B
		// (set) Token: 0x0600D363 RID: 54115 RVA: 0x0029F478 File Offset: 0x0029D678
		public bool Value
		{
			get
			{
				return this._impl.Value;
			}
			set
			{
				this._impl.Value = value;
			}
		}

		// Token: 0x1700330E RID: 13070
		// (get) Token: 0x0600D364 RID: 54116 RVA: 0x0029F486 File Offset: 0x0029D686
		// (set) Token: 0x0600D365 RID: 54117 RVA: 0x0029F493 File Offset: 0x0029D693
		public override string InnerText
		{
			get
			{
				return this._impl.InnerText;
			}
			set
			{
				this._impl.InnerText = value;
			}
		}

		// Token: 0x0600D366 RID: 54118 RVA: 0x0029F4A1 File Offset: 0x0029D6A1
		public static implicit operator bool(TrueFalseValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return TrueFalseValue.ToBoolean(xmlAttribute);
		}

		// Token: 0x0600D367 RID: 54119 RVA: 0x0029F4B7 File Offset: 0x0029D6B7
		public static implicit operator TrueFalseValue(bool value)
		{
			return TrueFalseValue.FromBoolean(value);
		}

		// Token: 0x0600D368 RID: 54120 RVA: 0x0029F4BF File Offset: 0x0029D6BF
		public static TrueFalseValue FromBoolean(bool value)
		{
			return new TrueFalseValue(value);
		}

		// Token: 0x0600D369 RID: 54121 RVA: 0x0029F4C7 File Offset: 0x0029D6C7
		public static bool ToBoolean(TrueFalseValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D36A RID: 54122 RVA: 0x0029F4DD File Offset: 0x0029D6DD
		internal override OpenXmlSimpleType CloneImp()
		{
			return new TrueFalseValue(this);
		}

		// Token: 0x0600D36B RID: 54123 RVA: 0x0029F4E8 File Offset: 0x0029D6E8
		private bool GetBooleanValue(string textValue)
		{
			if (textValue == null)
			{
				return false;
			}
			if ("true".Equals(textValue))
			{
				return true;
			}
			if ("false".Equals(textValue))
			{
				return false;
			}
			if ("t".Equals(textValue))
			{
				return true;
			}
			if ("f".Equals(textValue))
			{
				return false;
			}
			throw new FormatException(ExceptionMessages.TextIsInvalidTrueFalseValue);
		}

		// Token: 0x0600D36C RID: 54124 RVA: 0x0029F540 File Offset: 0x0029D740
		private string GetDefaultTextValue(bool boolValue)
		{
			if (!boolValue)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x04006998 RID: 27032
		private TrueFalseValueImpl _impl;
	}
}
