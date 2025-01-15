using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E8 RID: 744
	internal sealed class EntityContainerAssociationSet : EntityContainerRelationshipSet
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x00064530 File Offset: 0x00062730
		public EntityContainerAssociationSet(EntityContainer parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x0006454F File Offset: 0x0006274F
		internal override IEnumerable<EntityContainerRelationshipSetEnd> Ends
		{
			get
			{
				foreach (EntityContainerAssociationSetEnd entityContainerAssociationSetEnd in this._relationshipEnds.Values)
				{
					yield return entityContainerAssociationSetEnd;
				}
				Dictionary<string, EntityContainerAssociationSetEnd>.ValueCollection.Enumerator enumerator = default(Dictionary<string, EntityContainerAssociationSetEnd>.ValueCollection.Enumerator);
				foreach (EntityContainerAssociationSetEnd entityContainerAssociationSetEnd2 in this._rolelessEnds)
				{
					yield return entityContainerAssociationSetEnd2;
				}
				List<EntityContainerAssociationSetEnd>.Enumerator enumerator2 = default(List<EntityContainerAssociationSetEnd>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0006455F File Offset: 0x0006275F
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Association"))
			{
				base.HandleRelationshipTypeNameAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x00064583 File Offset: 0x00062783
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "End"))
			{
				this.HandleEndElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000645A8 File Offset: 0x000627A8
		private void HandleEndElement(XmlReader reader)
		{
			EntityContainerAssociationSetEnd entityContainerAssociationSetEnd = new EntityContainerAssociationSetEnd(this);
			entityContainerAssociationSetEnd.Parse(reader);
			if (entityContainerAssociationSetEnd.Role == null)
			{
				this._rolelessEnds.Add(entityContainerAssociationSetEnd);
				return;
			}
			if (this.HasEnd(entityContainerAssociationSetEnd.Role))
			{
				entityContainerAssociationSetEnd.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.DuplicateEndName(entityContainerAssociationSetEnd.Name));
				return;
			}
			this._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0006460E File Offset: 0x0006280E
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00064618 File Offset: 0x00062818
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (EntityContainerAssociationSetEnd entityContainerAssociationSetEnd in this._rolelessEnds)
			{
				if (entityContainerAssociationSetEnd.Role != null)
				{
					if (this.HasEnd(entityContainerAssociationSetEnd.Role))
					{
						entityContainerAssociationSetEnd.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndGivesAlreadyDefinedEnd(entityContainerAssociationSetEnd.EntitySet.FQName, this.Name));
					}
					else
					{
						this._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
					}
				}
			}
			this._rolelessEnds.Clear();
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000646C0 File Offset: 0x000628C0
		protected override void AddEnd(IRelationshipEnd relationshipEnd, EntityContainerEntitySet entitySet)
		{
			EntityContainerAssociationSetEnd entityContainerAssociationSetEnd = new EntityContainerAssociationSetEnd(this);
			entityContainerAssociationSetEnd.Role = relationshipEnd.Name;
			entityContainerAssociationSetEnd.RelationshipEnd = relationshipEnd;
			entityContainerAssociationSetEnd.EntitySet = entitySet;
			if (entityContainerAssociationSetEnd.EntitySet != null)
			{
				this._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00064708 File Offset: 0x00062908
		protected override bool HasEnd(string role)
		{
			return this._relationshipEnds.ContainsKey(role);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00064718 File Offset: 0x00062918
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			EntityContainerAssociationSet entityContainerAssociationSet = new EntityContainerAssociationSet((EntityContainer)parentElement);
			entityContainerAssociationSet.Name = this.Name;
			entityContainerAssociationSet.Relationship = base.Relationship;
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				EntityContainerAssociationSetEnd entityContainerAssociationSetEnd = (EntityContainerAssociationSetEnd)((EntityContainerAssociationSetEnd)entityContainerRelationshipSetEnd).Clone(entityContainerAssociationSet);
				entityContainerAssociationSet._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
			}
			return entityContainerAssociationSet;
		}

		// Token: 0x04000C20 RID: 3104
		private readonly Dictionary<string, EntityContainerAssociationSetEnd> _relationshipEnds = new Dictionary<string, EntityContainerAssociationSetEnd>();

		// Token: 0x04000C21 RID: 3105
		private readonly List<EntityContainerAssociationSetEnd> _rolelessEnds = new List<EntityContainerAssociationSetEnd>();
	}
}
