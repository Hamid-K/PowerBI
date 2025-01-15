using System;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C6 RID: 454
	public sealed class RoleReference : ICustomSerializationOptions, IScalarForm<string>
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00012452 File Offset: 0x00010652
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0001245A File Offset: 0x0001065A
		[JsonProperty(Required = Required.Always)]
		public string Role { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00012463 File Offset: 0x00010663
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00012466 File Offset: 0x00010666
		internal bool TryGetScalarForm(out string value)
		{
			value = this.Role;
			return true;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00012471 File Offset: 0x00010671
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			return this.TryGetScalarForm(out value);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001247A File Offset: 0x0001067A
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			this.Role = value;
		}
	}
}
