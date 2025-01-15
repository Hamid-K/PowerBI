using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamingStrategy
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00024050 File Offset: 0x00022250
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00024058 File Offset: 0x00022258
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00024061 File Offset: 0x00022261
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00024069 File Offset: 0x00022269
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00024072 File Offset: 0x00022272
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0002407A File Offset: 0x0002227A
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x0600082F RID: 2095 RVA: 0x00024083 File Offset: 0x00022283
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00024099 File Offset: 0x00022299
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000240AC File Offset: 0x000222AC
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x06000832 RID: 2098
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x06000833 RID: 2099 RVA: 0x000240C0 File Offset: 0x000222C0
		public override int GetHashCode()
		{
			return (((((base.GetType().GetHashCode() * 397) ^ this.ProcessDictionaryKeys.GetHashCode()) * 397) ^ this.ProcessExtensionDataNames.GetHashCode()) * 397) ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00024117 File Offset: 0x00022317
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00024128 File Offset: 0x00022328
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
