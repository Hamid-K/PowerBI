using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001BC RID: 444
	public sealed class AdjectivePhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00011C98 File Offset: 0x0000FE98
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00011CA9 File Offset: 0x0000FEA9
		[JsonProperty]
		public List<Term> Adjectives { get; } = new TermList();

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00011CB1 File Offset: 0x0000FEB1
		[JsonProperty]
		public List<Term> Antonyms { get; } = new TermList();

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00011CB9 File Offset: 0x0000FEB9
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00011CC1 File Offset: 0x0000FEC1
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Measurement { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00011CCA File Offset: 0x0000FECA
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00011CD2 File Offset: 0x0000FED2
		[JsonProperty]
		public List<AdverbPhrase> AdverbPhrases { get; } = new List<AdverbPhrase>();

		// Token: 0x06000942 RID: 2370 RVA: 0x00011CDA File Offset: 0x0000FEDA
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00011CE3 File Offset: 0x0000FEE3
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00011CEC File Offset: 0x0000FEEC
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00011CF6 File Offset: 0x0000FEF6
		public bool ShouldSerializeAdjectives()
		{
			return this.Adjectives.Count > 0;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00011D06 File Offset: 0x0000FF06
		public bool ShouldSerializeAntonyms()
		{
			return this.Antonyms.Count > 0;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00011D16 File Offset: 0x0000FF16
		public bool ShouldSerializeAdverbPhrases()
		{
			return this.AdverbPhrases.Count > 0;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00011D26 File Offset: 0x0000FF26
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00011D36 File Offset: 0x0000FF36
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.Measurement != null)
			{
				yield return this.Measurement;
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
			foreach (AdverbPhrase adverbPhrase in this.AdverbPhrases)
			{
				if (adverbPhrase.Measurement != null)
				{
					yield return adverbPhrase.Measurement;
				}
			}
			List<AdverbPhrase>.Enumerator enumerator2 = default(List<AdverbPhrase>.Enumerator);
			yield break;
			yield break;
		}
	}
}
