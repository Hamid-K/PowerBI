using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001BF RID: 447
	public sealed class DynamicNounPhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00011E6E File Offset: 0x0001006E
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00011E76 File Offset: 0x00010076
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00011E7F File Offset: 0x0001007F
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00011E87 File Offset: 0x00010087
		[JsonProperty(Required = Required.Always)]
		public RoleReference Noun { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00011E90 File Offset: 0x00010090
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x06000965 RID: 2405 RVA: 0x00011E98 File Offset: 0x00010098
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00011EA1 File Offset: 0x000100A1
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00011EAA File Offset: 0x000100AA
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00011EB4 File Offset: 0x000100B4
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00011EC4 File Offset: 0x000100C4
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.Noun != null)
			{
				yield return this.Noun;
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
