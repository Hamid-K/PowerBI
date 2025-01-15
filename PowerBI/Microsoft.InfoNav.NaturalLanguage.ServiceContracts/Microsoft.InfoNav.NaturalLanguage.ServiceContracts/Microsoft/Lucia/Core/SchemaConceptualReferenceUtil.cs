using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.DomainModel;
using Microsoft.Lucia.Core.DomainModel.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000073 RID: 115
	public static class SchemaConceptualReferenceUtil
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00004C5E File Offset: 0x00002E5E
		public static bool Remove(LsdlDocument schema, string conceptualEntity)
		{
			return SchemaConceptualReferenceUtil.RemoveConceptualReferenceVisitor.Remove(schema, conceptualEntity, null);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00004C68 File Offset: 0x00002E68
		public static bool Remove(LsdlDocument schema, string conceptualEntity, string conceptualProperty)
		{
			return SchemaConceptualReferenceUtil.RemoveConceptualReferenceVisitor.Remove(schema, conceptualEntity, conceptualProperty);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004C72 File Offset: 0x00002E72
		public static bool Rename(LsdlDocument schema, string oldConceptualEntity, string newConceptualEntity)
		{
			return SchemaConceptualReferenceUtil.RenameConceptualReferenceVisitor.Rename(schema, oldConceptualEntity, null, newConceptualEntity, null);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004C7E File Offset: 0x00002E7E
		public static bool Rename(LsdlDocument schema, string conceptualEntity, string oldConceptualProperty, string newConceptualProperty)
		{
			return SchemaConceptualReferenceUtil.RenameConceptualReferenceVisitor.Rename(schema, conceptualEntity, oldConceptualProperty, conceptualEntity, newConceptualProperty);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004C8A File Offset: 0x00002E8A
		public static bool Move(LsdlDocument schema, string oldConceptualEntity, string oldConceptualProperty, string newConceptualEntity, string newConceptualProperty)
		{
			return SchemaConceptualReferenceUtil.RenameConceptualReferenceVisitor.Rename(schema, oldConceptualEntity, oldConceptualProperty, newConceptualEntity, newConceptualProperty);
		}

		// Token: 0x020001FF RID: 511
		private sealed class RemoveConceptualReferenceVisitor : LsdlDocumentUpdateVisitor
		{
			// Token: 0x06000B08 RID: 2824 RVA: 0x00014814 File Offset: 0x00012A14
			private RemoveConceptualReferenceVisitor(string conceptualEntity, string conceptualProperty)
			{
				this._conceptualEntity = conceptualEntity;
				this._conceptualProperty = conceptualProperty;
			}

			// Token: 0x06000B09 RID: 2825 RVA: 0x0001482A File Offset: 0x00012A2A
			internal static bool Remove(LsdlDocument schema, string conceptualEntity, string conceptualProperty)
			{
				SchemaConceptualReferenceUtil.RemoveConceptualReferenceVisitor removeConceptualReferenceVisitor = new SchemaConceptualReferenceUtil.RemoveConceptualReferenceVisitor(conceptualEntity, conceptualProperty);
				removeConceptualReferenceVisitor.Visit(schema);
				return removeConceptualReferenceVisitor._found;
			}

			// Token: 0x06000B0A RID: 2826 RVA: 0x00014840 File Offset: 0x00012A40
			protected override bool Visit(KeyValuePair<string, Entity> entity)
			{
				EntityDefinition definition = entity.Value.Definition;
				if (!this.Match((definition != null) ? definition.Binding : null))
				{
					Instances instances = entity.Value.Instances;
					Binding binding;
					if (instances == null)
					{
						binding = null;
					}
					else
					{
						InstanceSynonyms synonyms = instances.Synonyms;
						binding = ((synonyms != null) ? synonyms.ValueBinding : null);
					}
					if (!this.Match(binding))
					{
						Instances instances2 = entity.Value.Instances;
						Binding binding2;
						if (instances2 == null)
						{
							binding2 = null;
						}
						else
						{
							InstanceSynonyms synonyms2 = instances2.Synonyms;
							binding2 = ((synonyms2 != null) ? synonyms2.SynonymBinding : null);
						}
						if (!this.Match(binding2))
						{
							Instances instances3 = entity.Value.Instances;
							Binding binding3;
							if (instances3 == null)
							{
								binding3 = null;
							}
							else
							{
								InstanceWeights weights = instances3.Weights;
								binding3 = ((weights != null) ? weights.Binding : null);
							}
							if (!this.Match(binding3))
							{
								return base.Visit(entity);
							}
						}
					}
				}
				this.MarkFound();
				return false;
			}

			// Token: 0x06000B0B RID: 2827 RVA: 0x00014903 File Offset: 0x00012B03
			protected override bool Visit(KeyValuePair<string, Relationship> relationship)
			{
				if (this.Match(relationship.Value.Binding))
				{
					this.MarkFound();
					return false;
				}
				return base.Visit(relationship);
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x00014928 File Offset: 0x00012B28
			private void MarkFound()
			{
				this._found = true;
			}

			// Token: 0x06000B0D RID: 2829 RVA: 0x00014934 File Offset: 0x00012B34
			private bool Match(Binding binding)
			{
				if (binding == null)
				{
					return false;
				}
				if (this._conceptualProperty == null)
				{
					IConceptualEntityBinding conceptualEntityBinding = binding as IConceptualEntityBinding;
					return conceptualEntityBinding != null && ConceptualNameComparer.Instance.Equals(this._conceptualEntity, conceptualEntityBinding.ConceptualEntity);
				}
				ConceptualPropertyBinding conceptualPropertyBinding = binding as ConceptualPropertyBinding;
				return conceptualPropertyBinding != null && ConceptualNameComparer.Instance.Equals(this._conceptualEntity, conceptualPropertyBinding.ConceptualEntity) && ConceptualNameComparer.Instance.Equals(this._conceptualProperty, conceptualPropertyBinding.ConceptualProperty);
			}

			// Token: 0x04000824 RID: 2084
			private readonly string _conceptualEntity;

			// Token: 0x04000825 RID: 2085
			private readonly string _conceptualProperty;

			// Token: 0x04000826 RID: 2086
			private bool _found;
		}

		// Token: 0x02000200 RID: 512
		private sealed class RenameConceptualReferenceVisitor : DefaultLsdlDocumentVisitor
		{
			// Token: 0x06000B0E RID: 2830 RVA: 0x000149AB File Offset: 0x00012BAB
			private RenameConceptualReferenceVisitor(string oldConceptualEntity, string oldConceptualProperty, string newConceptualEntity, string newConceptualProperty)
			{
				this._oldConceptualEntity = oldConceptualEntity;
				this._oldConceptualProperty = oldConceptualProperty;
				this._newConceptualEntity = newConceptualEntity;
				this._newConceptualProperty = newConceptualProperty;
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x000149D0 File Offset: 0x00012BD0
			internal static bool Rename(LsdlDocument document, string oldConceptualEntity, string oldConceptualProperty, string newConceptualEntity, string newConceptualProperty)
			{
				if (ConceptualNameComparer.Instance.Equals(newConceptualEntity, oldConceptualEntity) && ConceptualNameComparer.Instance.Equals(newConceptualProperty, oldConceptualProperty))
				{
					return false;
				}
				SchemaConceptualReferenceUtil.RenameConceptualReferenceVisitor renameConceptualReferenceVisitor = new SchemaConceptualReferenceUtil.RenameConceptualReferenceVisitor(oldConceptualEntity, oldConceptualProperty, newConceptualEntity, newConceptualProperty);
				renameConceptualReferenceVisitor.Visit(document);
				return renameConceptualReferenceVisitor._found;
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x00014A08 File Offset: 0x00012C08
			public override void Visit(Entity entity)
			{
				EntityDefinition definition = entity.Definition;
				this.UpdateBinding((definition != null) ? definition.Binding : null);
				Instances instances = entity.Instances;
				if (instances != null)
				{
					InstanceSynonyms synonyms = instances.Synonyms;
					if (synonyms != null)
					{
						this.UpdateBinding(synonyms.ValueBinding);
						this.UpdateBinding(synonyms.SynonymBinding);
					}
					InstanceWeights weights = instances.Weights;
					this.UpdateBinding((weights != null) ? weights.Binding : null);
				}
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x00014A71 File Offset: 0x00012C71
			public override void Visit(Relationship relationship)
			{
				this.UpdateBinding(relationship.Binding);
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x00014A80 File Offset: 0x00012C80
			private void UpdateBinding(Binding binding)
			{
				IConceptualEntityBinding conceptualEntityBinding = binding as IConceptualEntityBinding;
				if (conceptualEntityBinding == null || !ConceptualNameComparer.Instance.Equals(conceptualEntityBinding.ConceptualEntity, this._oldConceptualEntity))
				{
					return;
				}
				ConceptualEntityBinding conceptualEntityBinding2 = binding as ConceptualEntityBinding;
				if (conceptualEntityBinding2 == null)
				{
					ConceptualPropertyBinding conceptualPropertyBinding = binding as ConceptualPropertyBinding;
					if (conceptualPropertyBinding == null)
					{
						HierarchyBinding hierarchyBinding = binding as HierarchyBinding;
						if (hierarchyBinding == null)
						{
							HierarchyLevelBinding hierarchyLevelBinding = binding as HierarchyLevelBinding;
							if (hierarchyLevelBinding == null)
							{
								return;
							}
							if (this._oldConceptualProperty == null)
							{
								hierarchyLevelBinding.ConceptualEntity = this._newConceptualEntity;
								this._found = true;
							}
						}
						else if (this._oldConceptualProperty == null)
						{
							hierarchyBinding.ConceptualEntity = this._newConceptualEntity;
							this._found = true;
							return;
						}
					}
					else
					{
						if (this._oldConceptualProperty == null)
						{
							conceptualPropertyBinding.ConceptualEntity = this._newConceptualEntity;
							this._found = true;
							return;
						}
						if (ConceptualNameComparer.Instance.Equals(conceptualPropertyBinding.ConceptualProperty, this._oldConceptualProperty))
						{
							conceptualPropertyBinding.ConceptualEntity = this._newConceptualEntity;
							conceptualPropertyBinding.ConceptualProperty = this._newConceptualProperty;
							this._found = true;
							return;
						}
					}
				}
				else if (this._oldConceptualProperty == null)
				{
					conceptualEntityBinding2.ConceptualEntity = this._newConceptualEntity;
					this._found = true;
					return;
				}
			}

			// Token: 0x04000827 RID: 2087
			private readonly string _oldConceptualEntity;

			// Token: 0x04000828 RID: 2088
			private readonly string _newConceptualEntity;

			// Token: 0x04000829 RID: 2089
			private readonly string _oldConceptualProperty;

			// Token: 0x0400082A RID: 2090
			private readonly string _newConceptualProperty;

			// Token: 0x0400082B RID: 2091
			private bool _found;
		}
	}
}
