using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D8 RID: 472
	internal sealed class TermList : CompactList<Term>
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x000132EB File Offset: 0x000114EB
		internal override IEnumerable<Term> GetItemsToSerialize()
		{
			HashSet<Term> plainTerms = new HashSet<Term>(Term.ValueComparer);
			foreach (Term term in this)
			{
				if (term.Properties.State != State.Deleted && term.Properties.Type == null)
				{
					plainTerms.Add(term);
				}
			}
			foreach (Term term2 in this)
			{
				if (term2.Properties.State == State.Generated || term2.Properties.State == State.Suggested)
				{
					TermPropertiesType? type = term2.Properties.Type;
					TermPropertiesType termPropertiesType = TermPropertiesType.Noun;
					if (((type.GetValueOrDefault() == termPropertiesType) & (type != null)) && term2.Properties.Source.IsDefault() && plainTerms.Contains(term2))
					{
						continue;
					}
				}
				yield return term2;
			}
			List<Term>.Enumerator enumerator2 = default(List<Term>.Enumerator);
			yield break;
			yield break;
		}
	}
}
