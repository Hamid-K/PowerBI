using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200019B RID: 411
	public sealed class ConceptualEntityBinding : Binding, IConceptualEntityBinding
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00010C88 File Offset: 0x0000EE88
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00010C90 File Offset: 0x0000EE90
		[JsonProperty(Required = Required.Always)]
		public string ConceptualEntity { get; set; }

		// Token: 0x06000850 RID: 2128 RVA: 0x00010C9C File Offset: 0x0000EE9C
		public override bool Equals(Binding other)
		{
			ConceptualEntityBinding conceptualEntityBinding = other as ConceptualEntityBinding;
			bool? flag = Util.AreEqual<ConceptualEntityBinding>(this, conceptualEntityBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.ConceptualEntity, conceptualEntityBinding.ConceptualEntity);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00010CDF File Offset: 0x0000EEDF
		protected override int GetHashCodeCore()
		{
			return ConceptualNameComparer.Instance.GetHashCode(this.ConceptualEntity);
		}

		// Token: 0x0400071F RID: 1823
		internal const string ConceptualEntityCommonName = "Table";
	}
}
