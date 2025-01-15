using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E9 RID: 745
	internal sealed class EntityContainerAssociationSetEnd : EntityContainerRelationshipSetEnd
	{
		// Token: 0x06002389 RID: 9097 RVA: 0x000647A8 File Offset: 0x000629A8
		public EntityContainerAssociationSetEnd(EntityContainerAssociationSet parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x000647B1 File Offset: 0x000629B1
		// (set) Token: 0x0600238B RID: 9099 RVA: 0x000647B9 File Offset: 0x000629B9
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

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000647C2 File Offset: 0x000629C2
		public override string Name
		{
			get
			{
				return this.Role;
			}
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000647CA File Offset: 0x000629CA
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

		// Token: 0x0600238E RID: 9102 RVA: 0x000647EE File Offset: 0x000629EE
		private void HandleRoleAttribute(XmlReader reader)
		{
			this._unresolvedRelationshipEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedRelationshipEndRole);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00064803 File Offset: 0x00062A03
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			IRelationship relationship = base.ParentElement.Relationship;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00064818 File Offset: 0x00062A18
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

		// Token: 0x06002391 RID: 9105 RVA: 0x000648B0 File Offset: 0x00062AB0
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
				base.AddError(ErrorCode.FailedInference, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndFailedNoEntitySetMatch(set.Name, base.ParentElement.Name, base.ParentElement.Relationship.FQName, set.EntityType.FQName, base.ParentElement.ParentElement.FQName));
			}
			else
			{
				base.AddError(ErrorCode.FailedInference, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndAmbiguous(set.Name, base.ParentElement.Name, base.ParentElement.Relationship.FQName, set.EntityType.FQName, base.ParentElement.ParentElement.FQName));
			}
			return null;
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000649E0 File Offset: 0x00062BE0
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new EntityContainerAssociationSetEnd((EntityContainerAssociationSet)parentElement)
			{
				_unresolvedRelationshipEndRole = this._unresolvedRelationshipEndRole,
				EntitySet = base.EntitySet
			};
		}

		// Token: 0x04000C22 RID: 3106
		private string _unresolvedRelationshipEndRole;
	}
}
