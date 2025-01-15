using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002142 RID: 8514
	[DebuggerDisplay("{InnerText}")]
	public class OnOffValue : OpenXmlSimpleType
	{
		// Token: 0x0600D37D RID: 54141 RVA: 0x0029F6B2 File Offset: 0x0029D8B2
		public OnOffValue()
		{
			this.Initialize();
		}

		// Token: 0x0600D37E RID: 54142 RVA: 0x0029F6C0 File Offset: 0x0029D8C0
		public OnOffValue(bool value)
		{
			this.Initialize();
			this.Value = value;
		}

		// Token: 0x0600D37F RID: 54143 RVA: 0x0029F6D5 File Offset: 0x0029D8D5
		public OnOffValue(OnOffValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.Initialize();
			this._impl.InnerText = source.InnerText;
		}

		// Token: 0x0600D380 RID: 54144 RVA: 0x0029F703 File Offset: 0x0029D903
		private void Initialize()
		{
			this._impl = new TrueFalseValueImpl(new Func<string, bool>(this.GetBooleanValue), new Func<bool, string>(this.GetDefaultTextValue));
		}

		// Token: 0x17003312 RID: 13074
		// (get) Token: 0x0600D381 RID: 54145 RVA: 0x0029F728 File Offset: 0x0029D928
		public override bool HasValue
		{
			get
			{
				return this._impl.HasValue;
			}
		}

		// Token: 0x17003313 RID: 13075
		// (get) Token: 0x0600D382 RID: 54146 RVA: 0x0029F735 File Offset: 0x0029D935
		// (set) Token: 0x0600D383 RID: 54147 RVA: 0x0029F742 File Offset: 0x0029D942
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

		// Token: 0x17003314 RID: 13076
		// (get) Token: 0x0600D384 RID: 54148 RVA: 0x0029F750 File Offset: 0x0029D950
		// (set) Token: 0x0600D385 RID: 54149 RVA: 0x0029F75D File Offset: 0x0029D95D
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

		// Token: 0x0600D386 RID: 54150 RVA: 0x0029F76C File Offset: 0x0029D96C
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
			if ("on".Equals(textValue))
			{
				return true;
			}
			if ("off".Equals(textValue))
			{
				return false;
			}
			if ("1".Equals(textValue))
			{
				return true;
			}
			if ("0".Equals(textValue))
			{
				return false;
			}
			throw new FormatException(ExceptionMessages.TextIsInvalidOnOffValue);
		}

		// Token: 0x0600D387 RID: 54151 RVA: 0x0029F540 File Offset: 0x0029D740
		private string GetDefaultTextValue(bool boolValue)
		{
			if (!boolValue)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600D388 RID: 54152 RVA: 0x0029F7E2 File Offset: 0x0029D9E2
		internal override OpenXmlSimpleType CloneImp()
		{
			return new OnOffValue(this);
		}

		// Token: 0x0600D389 RID: 54153 RVA: 0x0029F7EA File Offset: 0x0029D9EA
		public static implicit operator bool(OnOffValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new ArgumentNullException("xmlAttribute");
			}
			return OnOffValue.ToBoolean(xmlAttribute);
		}

		// Token: 0x0600D38A RID: 54154 RVA: 0x0029F800 File Offset: 0x0029DA00
		public static implicit operator OnOffValue(bool value)
		{
			return OnOffValue.FromBoolean(value);
		}

		// Token: 0x0600D38B RID: 54155 RVA: 0x0029F808 File Offset: 0x0029DA08
		public static OnOffValue FromBoolean(bool value)
		{
			return new OnOffValue(value);
		}

		// Token: 0x0600D38C RID: 54156 RVA: 0x0029F810 File Offset: 0x0029DA10
		public static bool ToBoolean(OnOffValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0400699A RID: 27034
		private TrueFalseValueImpl _impl;
	}
}
