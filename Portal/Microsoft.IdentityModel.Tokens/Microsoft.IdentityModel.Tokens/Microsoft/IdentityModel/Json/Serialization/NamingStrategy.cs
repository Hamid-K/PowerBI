using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200009E RID: 158
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class NamingStrategy
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00024074 File Offset: 0x00022274
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0002407C File Offset: 0x0002227C
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00024085 File Offset: 0x00022285
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0002408D File Offset: 0x0002228D
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00024096 File Offset: 0x00022296
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0002409E File Offset: 0x0002229E
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x06000830 RID: 2096 RVA: 0x000240A7 File Offset: 0x000222A7
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000240BD File Offset: 0x000222BD
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000240D0 File Offset: 0x000222D0
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x06000833 RID: 2099
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x06000834 RID: 2100 RVA: 0x000240E4 File Offset: 0x000222E4
		public override int GetHashCode()
		{
			return (((((base.GetType().GetHashCode() * 397) ^ this.ProcessDictionaryKeys.GetHashCode()) * 397) ^ this.ProcessExtensionDataNames.GetHashCode()) * 397) ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002413B File Offset: 0x0002233B
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002414C File Offset: 0x0002234C
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
