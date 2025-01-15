using System;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000197 RID: 407
	public struct EnumProperty<TEnum> : IScalarForm<string>, ICustomSerializationOptions where TEnum : struct, Enum
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00010B5B File Offset: 0x0000ED5B
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00010B63 File Offset: 0x0000ED63
		[JsonProperty(Required = Required.Always)]
		public TEnum Value { readonly get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00010B6C File Offset: 0x0000ED6C
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00010B74 File Offset: 0x0000ED74
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public PropertyState State { readonly get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00010B7D File Offset: 0x0000ED7D
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00010B80 File Offset: 0x0000ED80
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			if (this.State == PropertyState.Default)
			{
				TEnum value2 = this.Value;
				value = value2.ToString();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			TEnum tenum;
			if (Enum.TryParse<TEnum>(value, out tenum))
			{
				this.Value = tenum;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		public bool ShouldSerialize()
		{
			if (this.State == PropertyState.Default)
			{
				TEnum value = this.Value;
				return !value.Equals(EnumProperty<TEnum>._defaultEnumObject);
			}
			return true;
		}

		// Token: 0x0400071B RID: 1819
		private static readonly object _defaultEnumObject = default(TEnum);
	}
}
