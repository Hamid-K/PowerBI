using System;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B6 RID: 438
	public sealed class ExampleProperties : ICustomSerializationOptions
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00011A22 File Offset: 0x0000FC22
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00011A2A File Offset: 0x0000FC2A
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TemplateSchema { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00011A33 File Offset: 0x0000FC33
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00011A36 File Offset: 0x0000FC36
		internal bool IsDefault()
		{
			return this.TemplateSchema == null;
		}
	}
}
