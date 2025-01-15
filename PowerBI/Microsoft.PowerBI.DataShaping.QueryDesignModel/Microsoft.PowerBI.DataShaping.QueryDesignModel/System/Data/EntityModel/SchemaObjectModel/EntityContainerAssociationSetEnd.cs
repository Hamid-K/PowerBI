using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200001F RID: 31
	internal sealed class EntityContainerAssociationSetEnd : EntityContainerRelationshipSetEnd
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0000AA68 File Offset: 0x00008C68
		public EntityContainerAssociationSetEnd(EntityContainerAssociationSet parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000AA71 File Offset: 0x00008C71
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000AA79 File Offset: 0x00008C79
		public string Role
		{
			get
			{
				return this._unresolvedRelationshipEndRole;
			}
			set
			{
				this._unresolvedRelationshipEndRole = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000AA82 File Offset: 0x00008C82
		public override string Name
		{
			get
			{
				return this.Role;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000AA8A File Offset: 0x00008C8A
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000AAAE File Offset: 0x00008CAE
		private void HandleRoleAttribute(XmlReader reader)
		{
			this._unresolvedRelationshipEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedRelationshipEndRole);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			IRelationship relationship = base.ParentElement.Relationship;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			if (this._unresolvedRelationshipEndRole == null && base.EntitySet != null)
			{
				base.RelationshipEnd = this.InferRelationshipEnd(base.EntitySet);
				if (base.RelationshipEnd != null)
				{
					this._unresolvedRelationshipEndRole = base.RelationshipEnd.Name;
					return;
				}
			}
			else if (this._unresolvedRelationshipEndRole != null)
			{
				IRelationship relationship = base.ParentElement.Relationship;
				IRelationshipEnd relationshipEnd;
				if (relationship.TryGetEnd(this._unresolvedRelationshipEndRole, out relationshipEnd))
				{
					base.RelationshipEnd = relationshipEnd;
					return;
				}
				base.AddError(ErrorCode.InvalidContainerTypeForEnd, EdmSchemaErrorSeverity.Error, Strings.InvalidEntityEndName(this.Role, relationship.FQName));
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000AB70 File Offset: 0x00008D70
		private IRelationshipEnd InferRelationshipEnd(EntityContainerEntitySet set)
		{
			if (base.ParentElement.Relationship == null)
			{
				return null;
			}
			List<IRelationshipEnd> list = new List<IRelationshipEnd>();
			foreach (IRelationshipEnd relationshipEnd in base.ParentElement.Relationship.Ends)
			{
				if (set.EntityType.IsOfType(relationshipEnd.Type))
				{
					list.Add(relationshipEnd);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (list.Count == 0)
			{
				base.AddError(ErrorCode.FailedInference, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndFailedNoEntitySetMatch(set.FQName, base.ParentElement.FQName, base.ParentElement.Relationship.FQName, set.EntityType.FQName, base.ParentElement.ParentElement.FQName));
			}
			else
			{
				base.AddError(ErrorCode.FailedInference, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndAmbiguous(set.FQName, base.ParentElement.FQName, base.ParentElement.Relationship.FQName, set.EntityType.FQName, base.ParentElement.ParentElement.FQName));
			}
			return null;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new EntityContainerAssociationSetEnd((EntityContainerAssociationSet)parentElement)
			{
				_unresolvedRelationshipEndRole = this._unresolvedRelationshipEndRole,
				EntitySet = base.EntitySet
			};
		}

		// Token: 0x040005AF RID: 1455
		private string _unresolvedRelationshipEndRole;
	}
}
