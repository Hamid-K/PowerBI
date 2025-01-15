using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D6 RID: 470
	public abstract class DefaultLsdlDocumentVisitor : ILsdlDocumentVisitor, IRelationshipVisitor, IPhrasingVisitor
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00012880 File Offset: 0x00010A80
		public virtual void Visit(LsdlDocument lsdlDocument)
		{
			foreach (KeyValuePair<string, Entity> keyValuePair in lsdlDocument.Entities)
			{
				this.Visit(keyValuePair.Value);
			}
			foreach (KeyValuePair<string, Relationship> keyValuePair2 in lsdlDocument.Relationships)
			{
				this.Visit(keyValuePair2.Value);
			}
			foreach (GlobalSubstitution globalSubstitution in lsdlDocument.GlobalSubstitutions)
			{
				this.Visit(globalSubstitution);
			}
			foreach (Example example in lsdlDocument.Examples)
			{
				this.Visit(example);
			}
			foreach (KeyValuePair<string, AgentProperties> keyValuePair3 in lsdlDocument.Agents)
			{
				this.Visit(keyValuePair3.Value);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000129F8 File Offset: 0x00010BF8
		public virtual void Visit(Entity entity)
		{
			foreach (Term term in entity.Terms)
			{
				this.Visit(term);
			}
			if (entity.Instances != null)
			{
				this.Visit(entity.Instances);
			}
			this.Visit(entity.State);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00012A6C File Offset: 0x00010C6C
		public virtual void Visit(Term term)
		{
			this.Visit(term.Properties.State);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00012A80 File Offset: 0x00010C80
		public virtual void Visit(Instances instances)
		{
			if (instances.Synonyms != null)
			{
				this.Visit(instances.Synonyms);
			}
			if (instances.Weights != null)
			{
				this.Visit(instances.Weights);
			}
			this.Visit(instances.Index);
			this.Visit(instances.PluralNormalization);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00012ACD File Offset: 0x00010CCD
		public virtual void Visit(InstanceSynonyms instanceSynonyms)
		{
			this.Visit(instanceSynonyms.SynonymBinding);
			this.Visit(instanceSynonyms.ValueBinding);
			this.Visit(instanceSynonyms.State);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00012AF3 File Offset: 0x00010CF3
		public virtual void Visit(Binding binding)
		{
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00012AF5 File Offset: 0x00010CF5
		public virtual void Visit(InstanceWeights instanceWeights)
		{
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00012AF7 File Offset: 0x00010CF7
		public virtual void Visit(EntityInstanceIndex instanceIndex)
		{
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00012AF9 File Offset: 0x00010CF9
		public virtual void Visit(EntityInstancePluralNormalization instancePluralNormalization)
		{
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00012AFC File Offset: 0x00010CFC
		public virtual void Visit(Relationship relationship)
		{
			foreach (KeyValuePair<string, Role> keyValuePair in relationship.Roles)
			{
				this.Visit(keyValuePair.Value);
			}
			SemanticSlots semanticSlots = relationship.SemanticSlots;
			if (semanticSlots != null)
			{
				if (semanticSlots.Where != null)
				{
					this.Visit(semanticSlots.Where);
				}
				if (semanticSlots.When != null)
				{
					this.Visit(semanticSlots.When);
				}
				if (semanticSlots.Duration != null)
				{
					this.Visit(semanticSlots.Duration);
				}
				if (semanticSlots.Occurrences != null)
				{
					this.Visit(semanticSlots.Occurrences);
				}
			}
			foreach (Condition condition in relationship.Conditions)
			{
				this.Visit(condition);
			}
			foreach (Phrasing phrasing in relationship.Phrasings)
			{
				this.Visit(phrasing);
			}
			this.Visit(relationship.State);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00012C44 File Offset: 0x00010E44
		public virtual void Visit(Role role)
		{
			this.Visit(role.Target);
			if (role.Quantity != null)
			{
				this.Visit(role.Quantity);
			}
			if (role.Amount != null)
			{
				this.Visit(role.Amount);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00012C7A File Offset: 0x00010E7A
		public virtual void Visit(RoleReference roleReference)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00012C7C File Offset: 0x00010E7C
		public virtual void Visit(EntityReference entityReference)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00012C7E File Offset: 0x00010E7E
		public virtual void Visit(Condition condition)
		{
			this.Visit(condition.Target);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00012C8C File Offset: 0x00010E8C
		public virtual void Visit(Phrasing phrasing)
		{
			phrasing.Properties.Accept(this);
			this.Visit(phrasing.State);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00012CA6 File Offset: 0x00010EA6
		public virtual void Visit(AttributePhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.Object != null)
			{
				this.Visit(phrasing.Object);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00012CDC File Offset: 0x00010EDC
		public virtual void Visit(NamePhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.Name != null)
			{
				this.Visit(phrasing.Name);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00012D08 File Offset: 0x00010F08
		public virtual void Visit(AdjectivePhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.Measurement != null)
			{
				this.Visit(phrasing.Measurement);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
			this.VisitAdverbPhrases(phrasing.AdverbPhrases);
			foreach (Term term in phrasing.Adjectives)
			{
				this.Visit(term);
			}
			foreach (Term term2 in phrasing.Antonyms)
			{
				this.Visit(term2);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00012DE0 File Offset: 0x00010FE0
		public virtual void Visit(DynamicAdjectivePhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.Adjective != null)
			{
				this.Visit(phrasing.Adjective);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00012E18 File Offset: 0x00011018
		public virtual void Visit(NounPhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
			foreach (Term term in phrasing.Nouns)
			{
				this.Visit(term);
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00012E8C File Offset: 0x0001108C
		public virtual void Visit(DynamicNounPhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.Noun != null)
			{
				this.Visit(phrasing.Noun);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00012EC4 File Offset: 0x000110C4
		public virtual void Visit(PrepositionPhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.Object != null)
			{
				this.Visit(phrasing.Object);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
			foreach (Term term in phrasing.Prepositions)
			{
				this.Visit(term);
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00012F4C File Offset: 0x0001114C
		public virtual void Visit(VerbPhrasingProperties phrasing)
		{
			if (phrasing.Subject != null)
			{
				this.Visit(phrasing.Subject);
			}
			if (phrasing.IndirectObject != null)
			{
				this.Visit(phrasing.IndirectObject);
			}
			if (phrasing.Object != null)
			{
				this.Visit(phrasing.Object);
			}
			this.VisitPrepPhrases(phrasing.PrepositionalPhrases);
			this.VisitAdverbPhrases(phrasing.AdverbPhrases);
			foreach (Term term in phrasing.Verbs)
			{
				this.Visit(term);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00012FF4 File Offset: 0x000111F4
		public virtual void Visit(GlobalSubstitution globalSubstitution)
		{
			this.Visit(globalSubstitution.Properties.State);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00013007 File Offset: 0x00011207
		public virtual void Visit(Example example)
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00013009 File Offset: 0x00011209
		public virtual void Visit(State state)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001300B File Offset: 0x0001120B
		public virtual void Visit(AgentProperties agentProperties)
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00013010 File Offset: 0x00011210
		public virtual void Visit(PrepPhrase prepPhrase)
		{
			if (prepPhrase.Object != null)
			{
				this.Visit(prepPhrase.Object);
			}
			foreach (Term term in prepPhrase.Prepositions)
			{
				this.Visit(term);
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00013078 File Offset: 0x00011278
		public virtual void Visit(AdverbPhrase adverbPhrase)
		{
			if (adverbPhrase.Measurement != null)
			{
				this.Visit(adverbPhrase.Measurement);
			}
			foreach (Term term in adverbPhrase.Adverbs)
			{
				this.Visit(term);
			}
			foreach (Term term2 in adverbPhrase.Antonyms)
			{
				this.Visit(term2);
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00013124 File Offset: 0x00011324
		private void VisitPrepPhrases(List<PrepPhrase> prepositionalPhrases)
		{
			foreach (PrepPhrase prepPhrase in prepositionalPhrases)
			{
				this.Visit(prepPhrase);
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00013174 File Offset: 0x00011374
		private void VisitAdverbPhrases(List<AdverbPhrase> adverbPhrases)
		{
			foreach (AdverbPhrase adverbPhrase in adverbPhrases)
			{
				this.Visit(adverbPhrase);
			}
		}
	}
}
