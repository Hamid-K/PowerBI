using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015B2 RID: 5554
	internal class OptionItem
	{
		// Token: 0x06008B28 RID: 35624 RVA: 0x001D405E File Offset: 0x001D225E
		public OptionItem(string optionKey, TypeValue typeValue)
			: this(optionKey, typeValue, Value.Null, OptionItemOption.None, null, null)
		{
		}

		// Token: 0x06008B29 RID: 35625 RVA: 0x001D4070 File Offset: 0x001D2270
		public OptionItem(string optionKey, TypeValue typeValue, Value defaultValue, OptionItemOption options = OptionItemOption.None, TryConvertOption tryConvert = null, string helpQualifier = null)
		{
			this.key = optionKey;
			this.typeValue = typeValue;
			this.defaultValue = defaultValue;
			this.options = options;
			this.tryConvert = tryConvert ?? new TryConvertOption(this.DefaultTryConvert);
			this.helpQualifier = helpQualifier;
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x06008B2A RID: 35626 RVA: 0x001D40C0 File Offset: 0x001D22C0
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x06008B2B RID: 35627 RVA: 0x001D40C8 File Offset: 0x001D22C8
		public TypeValue Type
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x06008B2C RID: 35628 RVA: 0x001D40D0 File Offset: 0x001D22D0
		public Value Default
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x06008B2D RID: 35629 RVA: 0x001D40D8 File Offset: 0x001D22D8
		public OptionItemOption Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x06008B2E RID: 35630 RVA: 0x001D40E0 File Offset: 0x001D22E0
		public string HelpQualifier
		{
			get
			{
				return this.helpQualifier;
			}
		}

		// Token: 0x06008B2F RID: 35631 RVA: 0x001D40E8 File Offset: 0x001D22E8
		public bool TryConvert(Value option, out object value)
		{
			return this.tryConvert(option, out value);
		}

		// Token: 0x06008B30 RID: 35632 RVA: 0x001D40F7 File Offset: 0x001D22F7
		public static bool NoConversion(Value option, out object value)
		{
			value = null;
			return true;
		}

		// Token: 0x06008B31 RID: 35633 RVA: 0x001D40FD File Offset: 0x001D22FD
		private bool DefaultTryConvert(Value option, out object value)
		{
			value = null;
			if (!option.Type.IsCompatibleWith(this.typeValue))
			{
				return false;
			}
			if (option.IsNull)
			{
				return true;
			}
			value = ValueMarshaller.MarshalToClr(option, this.typeValue.NonNullable);
			return true;
		}

		// Token: 0x04004C31 RID: 19505
		private readonly string key;

		// Token: 0x04004C32 RID: 19506
		private readonly TypeValue typeValue;

		// Token: 0x04004C33 RID: 19507
		private readonly Value defaultValue;

		// Token: 0x04004C34 RID: 19508
		private readonly OptionItemOption options;

		// Token: 0x04004C35 RID: 19509
		private readonly TryConvertOption tryConvert;

		// Token: 0x04004C36 RID: 19510
		private readonly string helpQualifier;
	}
}
