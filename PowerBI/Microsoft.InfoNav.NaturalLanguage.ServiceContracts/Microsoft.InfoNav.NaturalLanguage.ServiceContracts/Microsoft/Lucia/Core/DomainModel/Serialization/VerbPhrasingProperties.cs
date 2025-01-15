using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C1 RID: 449
	public sealed class VerbPhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00011F73 File Offset: 0x00010173
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00011F7B File Offset: 0x0001017B
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00011F84 File Offset: 0x00010184
		[JsonProperty(Required = Required.Always)]
		public List<Term> Verbs { get; } = new TermList();

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00011F8C File Offset: 0x0001018C
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00011F94 File Offset: 0x00010194
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference IndirectObject { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00011F9D File Offset: 0x0001019D
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00011FA5 File Offset: 0x000101A5
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Object { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00011FAE File Offset: 0x000101AE
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00011FB6 File Offset: 0x000101B6
		[JsonProperty]
		public List<AdverbPhrase> AdverbPhrases { get; } = new List<AdverbPhrase>();

		// Token: 0x06000980 RID: 2432 RVA: 0x00011FBE File Offset: 0x000101BE
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00011FC7 File Offset: 0x000101C7
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00011FD0 File Offset: 0x000101D0
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00011FDA File Offset: 0x000101DA
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00011FEA File Offset: 0x000101EA
		public bool ShouldSerializeAdverbPhrases()
		{
			return this.AdverbPhrases.Count > 0;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00011FFA File Offset: 0x000101FA
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.IndirectObject != null)
			{
				yield return this.IndirectObject;
			}
			if (this.Object != null)
			{
				yield return this.Object;
			}
			foreach (PrepPhrase prepPhrase in this.PrepositionalPhrases)
			{
				if (prepPhrase.Object != null)
				{
					yield return prepPhrase.Object;
				}
			}
			List<PrepPhrase>.Enumerator enumerator = default(List<PrepPhrase>.Enumerator);
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
