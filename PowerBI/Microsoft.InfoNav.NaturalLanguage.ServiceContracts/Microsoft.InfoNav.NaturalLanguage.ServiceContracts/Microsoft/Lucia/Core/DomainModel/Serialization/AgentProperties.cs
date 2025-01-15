using System;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000198 RID: 408
	public sealed class AgentProperties : ICustomSerializationOptions
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00010C28 File Offset: 0x0000EE28
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x00010C30 File Offset: 0x0000EE30
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public DateTime LastModified
		{
			get
			{
				return this._lastModified;
			}
			set
			{
				LsdlAsserts.DateTimeShouldBeUtc(value);
				this._lastModified = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00010C40 File Offset: 0x0000EE40
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x0400071E RID: 1822
		private DateTime _lastModified;
	}
}
