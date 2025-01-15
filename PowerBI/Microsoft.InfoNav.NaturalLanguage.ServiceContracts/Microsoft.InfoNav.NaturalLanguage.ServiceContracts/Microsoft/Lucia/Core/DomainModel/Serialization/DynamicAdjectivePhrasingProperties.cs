using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001BD RID: 445
	public sealed class DynamicAdjectivePhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00011D7A File Offset: 0x0000FF7A
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00011D82 File Offset: 0x0000FF82
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00011D8B File Offset: 0x0000FF8B
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00011D93 File Offset: 0x0000FF93
		[JsonProperty(Required = Required.Always)]
		public RoleReference Adjective { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00011D9C File Offset: 0x0000FF9C
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x06000950 RID: 2384 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00011DAD File Offset: 0x0000FFAD
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00011DB6 File Offset: 0x0000FFB6
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.Adjective != null)
			{
				yield return this.Adjective;
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
