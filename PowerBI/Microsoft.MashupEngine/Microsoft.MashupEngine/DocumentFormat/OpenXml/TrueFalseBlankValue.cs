using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002141 RID: 8513
	[DebuggerDisplay("{InnerText}")]
	internal class TrueFalseBlankValue : OpenXmlSimpleType
	{
		// Token: 0x0600D36D RID: 54125 RVA: 0x0029F550 File Offset: 0x0029D750
		public TrueFalseBlankValue()
		{
			this.Initialize();
		}

		// Token: 0x0600D36E RID: 54126 RVA: 0x0029F55E File Offset: 0x0029D75E
		public TrueFalseBlankValue(bool value)
		{
			this.Initialize();
			this.Value = value;
		}

		// Token: 0x0600D36F RID: 54127 RVA: 0x0029F573 File Offset: 0x0029D773
		public TrueFalseBlankValue(TrueFalseBlankValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.Initialize();
			this._impl.InnerText = source.InnerText;
		}

		// Token: 0x0600D370 RID: 54128 RVA: 0x0029F5A1 File Offset: 0x0029D7A1
		private void Initialize()
		{
			this._impl = new TrueFalseValueImpl(new Func<string, bool>(this.GetBooleanValue), new Func<bool, string>(this.GetDefaultTextValue));
		}

		// Token: 0x1700330F RID: 13071
		// (get) Token: 0x0600D371 RID: 54129 RVA: 0x0029F5C6 File Offset: 0x0029D7C6
		public override bool HasValue
		{
			get
			{
				return this._impl.HasValue;
			}
		}

		// Token: 0x17003310 RID: 13072
		// (get) Token: 0x0600D372 RID: 54130 RVA: 0x0029F5D3 File Offset: 0x0029D7D3
		// (set) Token: 0x0600D373 RID: 54131 RVA: 0x0029F5E0 File Offset: 0x0029D7E0
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

		// Token: 0x17003311 RID: 13073
		// (get) Token: 0x0600D374 RID: 54132 RVA: 0x0029F5EE File Offset: 0x0029D7EE
		// (set) Token: 0x0600D375 RID: 54133 RVA: 0x0029F5FB File Offset: 0x0029D7FB
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

		// Token: 0x0600D376 RID: 54134 RVA: 0x0029F609 File Offset: 0x0029D809
		public static implicit operator bool(TrueFalseBlankValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return TrueFalseBlankValue.ToBoolean(xmlAttribute);
		}

		// Token: 0x0600D377 RID: 54135 RVA: 0x0029F61F File Offset: 0x0029D81F
		public static implicit operator TrueFalseBlankValue(bool value)
		{
			return TrueFalseBlankValue.FromBoolean(value);
		}

		// Token: 0x0600D378 RID: 54136 RVA: 0x0029F627 File Offset: 0x0029D827
		public static TrueFalseBlankValue FromBoolean(bool value)
		{
			return new TrueFalseBlankValue(value);
		}

		// Token: 0x0600D379 RID: 54137 RVA: 0x0029F62F File Offset: 0x0029D82F
		public static bool ToBoolean(TrueFalseBlankValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D37A RID: 54138 RVA: 0x0029F645 File Offset: 0x0029D845
		internal override OpenXmlSimpleType CloneImp()
		{
			return new TrueFalseBlankValue(this);
		}

		// Token: 0x0600D37B RID: 54139 RVA: 0x0029F650 File Offset: 0x0029D850
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
			if (textValue.Length == 0)
			{
				return false;
			}
			throw new FormatException(ExceptionMessages.TextIsInvalidTrueFalseBlankValue);
		}

		// Token: 0x0600D37C RID: 54140 RVA: 0x0029F540 File Offset: 0x0029D740
		private string GetDefaultTextValue(bool boolValue)
		{
			if (!boolValue)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x04006999 RID: 27033
		private TrueFalseValueImpl _impl;
	}
}
