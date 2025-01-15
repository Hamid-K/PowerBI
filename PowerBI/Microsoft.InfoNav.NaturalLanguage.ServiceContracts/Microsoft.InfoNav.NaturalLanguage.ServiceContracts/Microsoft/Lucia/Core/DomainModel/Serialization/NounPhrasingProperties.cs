using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001BE RID: 446
	public sealed class NounPhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00011DF3 File Offset: 0x0000FFF3
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00011DFB File Offset: 0x0000FFFB
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00011E04 File Offset: 0x00010004
		[JsonProperty(Required = Required.Always)]
		public List<Term> Nouns { get; } = new TermList();

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00011E0C File Offset: 0x0001000C
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x0600095A RID: 2394 RVA: 0x00011E14 File Offset: 0x00010014
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00011E1D File Offset: 0x0001001D
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00011E26 File Offset: 0x00010026
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00011E30 File Offset: 0x00010030
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00011E40 File Offset: 0x00010040
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
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
