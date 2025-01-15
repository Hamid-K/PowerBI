using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C0 RID: 448
	public sealed class PrepositionPhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00011EE7 File Offset: 0x000100E7
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00011EEF File Offset: 0x000100EF
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00011EF8 File Offset: 0x000100F8
		[JsonProperty(Required = Required.Always)]
		public List<Term> Prepositions { get; } = new TermList();

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00011F00 File Offset: 0x00010100
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00011F08 File Offset: 0x00010108
		[JsonProperty(Required = Required.Always)]
		public RoleReference Object { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00011F11 File Offset: 0x00010111
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x06000971 RID: 2417 RVA: 0x00011F19 File Offset: 0x00010119
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00011F22 File Offset: 0x00010122
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00011F2B File Offset: 0x0001012B
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00011F35 File Offset: 0x00010135
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00011F45 File Offset: 0x00010145
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.Object != null)
			{
				yield return this.Object;
			}
			if (this.PrepositionalPhrases != null)
			{
				foreach (PrepPhrase prepPhrase in this.PrepositionalPhrases)
				{
					if (prepPhrase.Object != null)
					{
						yield return prepPhrase.Object;
					}
				}
				List<PrepPhrase>.Enumerator enumerator = default(List<PrepPhrase>.Enumerator);
			}
			yield break;
			yield break;
		}
	}
}
