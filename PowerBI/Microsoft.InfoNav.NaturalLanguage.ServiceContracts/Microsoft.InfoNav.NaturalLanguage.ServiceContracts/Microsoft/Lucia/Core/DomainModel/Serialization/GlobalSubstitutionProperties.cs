using System;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A4 RID: 420
	public sealed class GlobalSubstitutionProperties : IScalarForm<string>, ICustomSerializationOptions, IStateItem
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000116C2 File Offset: 0x0000F8C2
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x000116CA File Offset: 0x0000F8CA
		[JsonProperty(Required = Required.Always)]
		public string Substitute { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000116D3 File Offset: 0x0000F8D3
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x000116DB File Offset: 0x0000F8DB
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public State State { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000116E4 File Offset: 0x0000F8E4
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x000116EC File Offset: 0x0000F8EC
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TemplateSchema { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000116F5 File Offset: 0x0000F8F5
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000116F8 File Offset: 0x0000F8F8
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			if (this.TemplateSchema == null && this.State == State.Authored)
			{
				value = this.Substitute;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00011718 File Offset: 0x0000F918
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			this.Substitute = value;
		}
	}
}
