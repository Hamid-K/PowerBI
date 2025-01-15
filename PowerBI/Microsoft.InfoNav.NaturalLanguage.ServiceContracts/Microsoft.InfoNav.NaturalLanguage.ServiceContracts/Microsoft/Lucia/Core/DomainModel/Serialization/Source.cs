using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001CC RID: 460
	public struct Source : IScalarForm<string>
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x000125AD File Offset: 0x000107AD
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x000125B5 File Offset: 0x000107B5
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public SourceType Type { readonly get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x000125BE File Offset: 0x000107BE
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x000125C6 File Offset: 0x000107C6
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Agent { readonly get; set; }

		// Token: 0x060009DF RID: 2527 RVA: 0x000125D0 File Offset: 0x000107D0
		public void SetFromScalarForm(string value)
		{
			SourceType sourceType;
			if (Enum.TryParse<SourceType>(value, out sourceType))
			{
				this.Type = sourceType;
				return;
			}
			this.Type = SourceType.Default;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000125F8 File Offset: 0x000107F8
		public bool TryGetScalarForm(out string value)
		{
			if (this.Agent == null)
			{
				value = this.Type.ToString();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00012629 File Offset: 0x00010829
		internal bool IsDefault()
		{
			return this.Agent == null && this.Type == SourceType.Default;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001263E File Offset: 0x0001083E
		public bool ShouldSerialize()
		{
			return !this.IsDefault();
		}
	}
}
