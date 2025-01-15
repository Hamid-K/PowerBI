using System;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001AF RID: 431
	public sealed class EntityReference : ICustomSerializationOptions, IScalarForm<string>
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00011963 File Offset: 0x0000FB63
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001196B File Offset: 0x0000FB6B
		[JsonProperty(Required = Required.Always)]
		public string Entity { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00011974 File Offset: 0x0000FB74
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0001197C File Offset: 0x0000FB7C
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Namespace { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00011985 File Offset: 0x0000FB85
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00011988 File Offset: 0x0000FB88
		internal bool TryGetScalarForm(out string value)
		{
			if (this.Namespace == null)
			{
				value = this.Entity;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000119A0 File Offset: 0x0000FBA0
		internal static EntityReference FromScalarForm(string value)
		{
			return new EntityReference
			{
				Entity = value
			};
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000119AE File Offset: 0x0000FBAE
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			return this.TryGetScalarForm(out value);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000119B7 File Offset: 0x0000FBB7
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			this.Entity = value;
		}
	}
}
