using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200001E RID: 30
	internal sealed class EntityContainerAssociationSet : EntityContainerRelationshipSet
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public EntityContainerAssociationSet(EntityContainer parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000A80F File Offset: 0x00008A0F
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

		// Token: 0x0600061E RID: 1566 RVA: 0x0000A81F File Offset: 0x00008A1F
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

		// Token: 0x0600061F RID: 1567 RVA: 0x0000A843 File Offset: 0x00008A43
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

		// Token: 0x06000620 RID: 1568 RVA: 0x0000A868 File Offset: 0x00008A68
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

		// Token: 0x06000621 RID: 1569 RVA: 0x0000A8CE File Offset: 0x00008ACE
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000A8D8 File Offset: 0x00008AD8
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

		// Token: 0x06000623 RID: 1571 RVA: 0x0000A980 File Offset: 0x00008B80
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

		// Token: 0x06000624 RID: 1572 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		protected override bool HasEnd(string role)
		{
			return this._relationshipEnds.ContainsKey(role);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000A9D8 File Offset: 0x00008BD8
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

		// Token: 0x040005AD RID: 1453
		private Dictionary<string, EntityContainerAssociationSetEnd> _relationshipEnds = new Dictionary<string, EntityContainerAssociationSetEnd>();

		// Token: 0x040005AE RID: 1454
		private List<EntityContainerAssociationSetEnd> _rolelessEnds = new List<EntityContainerAssociationSetEnd>();
	}
}
