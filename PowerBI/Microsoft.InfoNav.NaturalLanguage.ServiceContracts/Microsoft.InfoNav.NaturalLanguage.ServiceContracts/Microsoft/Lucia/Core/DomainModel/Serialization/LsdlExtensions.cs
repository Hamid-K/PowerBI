using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200018E RID: 398
	public static class LsdlExtensions
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0000FE34 File Offset: 0x0000E034
		public static LsdlDocument UpdateTemplateSchema(this LsdlDocument lsdlDocument, string defaultTemplateSchema = null)
		{
			if (string.IsNullOrEmpty(defaultTemplateSchema))
			{
				return lsdlDocument;
			}
			new LsdlExtensions.TemplateSchemaUpdateVisitor(defaultTemplateSchema).Visit(lsdlDocument);
			return lsdlDocument;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0000FE50 File Offset: 0x0000E050
		public static bool IsHidden(this Entity entity)
		{
			return entity.Visibility.Value == EntityVisibility.Hidden || entity.Visibility.Value == EntityVisibility.Children;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0000FE81 File Offset: 0x0000E081
		public static HashSet<string> GetAllImplicitRoles(this Relationship relationship)
		{
			LsdlExtensions.RoleReferenceCollector roleReferenceCollector = new LsdlExtensions.RoleReferenceCollector();
			roleReferenceCollector.Visit(relationship);
			return roleReferenceCollector.Roles;
		}

		// Token: 0x02000229 RID: 553
		private sealed class RoleReferenceCollector : DefaultLsdlDocumentVisitor
		{
			// Token: 0x17000349 RID: 841
			// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00017A28 File Offset: 0x00015C28
			public HashSet<string> Roles { get; } = new HashSet<string>();

			// Token: 0x06000BD8 RID: 3032 RVA: 0x00017A30 File Offset: 0x00015C30
			public override void Visit(RoleReference roleReference)
			{
				this.Roles.Add(roleReference.Role);
			}
		}

		// Token: 0x0200022A RID: 554
		private sealed class TemplateSchemaUpdateVisitor : DefaultLsdlDocumentVisitor
		{
			// Token: 0x06000BDA RID: 3034 RVA: 0x00017A57 File Offset: 0x00015C57
			internal TemplateSchemaUpdateVisitor(string templateSchema)
			{
				this._templateSchema = templateSchema;
			}

			// Token: 0x06000BDB RID: 3035 RVA: 0x00017A68 File Offset: 0x00015C68
			public override void Visit(Entity entity)
			{
				this._visitingEntity = true;
				base.Visit(entity);
				if (string.IsNullOrEmpty(entity.TemplateSchema))
				{
					entity.TemplateSchema = this._templateSchema;
					entity.State = this.GetState(entity.State);
				}
				this._visitingEntity = false;
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x00017AB8 File Offset: 0x00015CB8
			public override void Visit(GlobalSubstitution globalSubstitution)
			{
				base.Visit(globalSubstitution);
				if (string.IsNullOrEmpty(globalSubstitution.Properties.TemplateSchema))
				{
					globalSubstitution.Properties.TemplateSchema = this._templateSchema;
					globalSubstitution.Properties.State = this.GetState(globalSubstitution.Properties.State);
				}
			}

			// Token: 0x06000BDD RID: 3037 RVA: 0x00017B0B File Offset: 0x00015D0B
			public override void Visit(Example example)
			{
				base.Visit(example);
				if (string.IsNullOrEmpty(example.Properties.TemplateSchema))
				{
					example.Properties.TemplateSchema = this._templateSchema;
				}
			}

			// Token: 0x06000BDE RID: 3038 RVA: 0x00017B37 File Offset: 0x00015D37
			public override void Visit(Relationship relationship)
			{
				base.Visit(relationship);
				if (string.IsNullOrEmpty(relationship.TemplateSchema))
				{
					relationship.TemplateSchema = this._templateSchema;
					relationship.State = this.GetState(relationship.State);
				}
			}

			// Token: 0x06000BDF RID: 3039 RVA: 0x00017B6B File Offset: 0x00015D6B
			public override void Visit(Phrasing phrasing)
			{
				base.Visit(phrasing);
				if (string.IsNullOrEmpty(phrasing.TemplateSchema))
				{
					phrasing.TemplateSchema = this._templateSchema;
					phrasing.State = this.GetState(phrasing.State);
				}
			}

			// Token: 0x06000BE0 RID: 3040 RVA: 0x00017B9F File Offset: 0x00015D9F
			public override void Visit(PrepPhrase prepPhrase)
			{
				this._visitingPrepPhrase = true;
				base.Visit(prepPhrase);
				this._visitingPrepPhrase = false;
			}

			// Token: 0x06000BE1 RID: 3041 RVA: 0x00017BB8 File Offset: 0x00015DB8
			public override void Visit(Term term)
			{
				base.Visit(term);
				if (string.IsNullOrEmpty(term.Properties.TemplateSchema))
				{
					if (!this._visitingPrepPhrase)
					{
						term.Properties.TemplateSchema = this._templateSchema;
					}
					if (this._visitingEntity)
					{
						term.Properties.State = this.GetState(term.Properties.State);
					}
				}
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x00017C1B File Offset: 0x00015E1B
			public override void Visit(InstanceSynonyms instanceSynonyms)
			{
				base.Visit(instanceSynonyms);
				instanceSynonyms.State = this.GetState(instanceSynonyms.State);
			}

			// Token: 0x06000BE3 RID: 3043 RVA: 0x00017C36 File Offset: 0x00015E36
			private State GetState(State state)
			{
				if (state != State.Generated)
				{
					return state;
				}
				if (!string.IsNullOrEmpty(this._templateSchema))
				{
					return State.Authored;
				}
				return State.Generated;
			}

			// Token: 0x0400090A RID: 2314
			private readonly string _templateSchema;

			// Token: 0x0400090B RID: 2315
			private bool _visitingEntity;

			// Token: 0x0400090C RID: 2316
			private bool _visitingPrepPhrase;
		}
	}
}
