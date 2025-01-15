using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200009D RID: 157
	internal abstract class NamingStrategy
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00023A10 File Offset: 0x00021C10
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00023A18 File Offset: 0x00021C18
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00023A21 File Offset: 0x00021C21
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00023A29 File Offset: 0x00021C29
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00023A32 File Offset: 0x00021C32
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00023A3A File Offset: 0x00021C3A
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x06000826 RID: 2086 RVA: 0x00023A43 File Offset: 0x00021C43
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00023A59 File Offset: 0x00021C59
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00023A6C File Offset: 0x00021C6C
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x06000829 RID: 2089
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x0600082A RID: 2090 RVA: 0x00023A80 File Offset: 0x00021C80
		public override int GetHashCode()
		{
			return (((((base.GetType().GetHashCode() * 397) ^ this.ProcessDictionaryKeys.GetHashCode()) * 397) ^ this.ProcessExtensionDataNames.GetHashCode()) * 397) ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00023AD7 File Offset: 0x00021CD7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00023AE8 File Offset: 0x00021CE8
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
