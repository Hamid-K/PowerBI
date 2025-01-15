using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000023 RID: 35
	internal class EntityContainerRelationshipSetEnd : SchemaElement
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0000B2A5 File Offset: 0x000094A5
		public EntityContainerRelationshipSetEnd(EntityContainerRelationshipSet parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000B2AE File Offset: 0x000094AE
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000B2B6 File Offset: 0x000094B6
		public IRelationshipEnd RelationshipEnd
		{
			get
			{
				return this._relationshipEnd;
			}
			internal set
			{
				this._relationshipEnd = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000B2BF File Offset: 0x000094BF
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0000B2C7 File Offset: 0x000094C7
		public EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
			internal set
			{
				this._entitySet = value;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000B2D0 File Offset: 0x000094D0
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000B2F0 File Offset: 0x000094F0
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				this.HandleEntitySetAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000B314 File Offset: 0x00009514
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this._unresolvedEntitySetName = reader.Value;
				return;
			}
			this._unresolvedEntitySetName = base.HandleUndottedNameAttribute(reader, this._unresolvedEntitySetName);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000B344 File Offset: 0x00009544
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._entitySet == null)
			{
				this._entitySet = this.ParentElement.ParentElement.FindEntitySet(this._unresolvedEntitySetName);
				if (this._entitySet == null)
				{
					base.AddError(ErrorCode.InvalidEndEntitySet, EdmSchemaErrorSeverity.Error, Strings.InvalidEntitySetNameReference(this._unresolvedEntitySetName, this.Name));
				}
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000B3A0 File Offset: 0x000095A0
		internal override void Validate()
		{
			base.Validate();
			if (this._relationshipEnd == null || this._entitySet == null)
			{
				return;
			}
			if (!this._relationshipEnd.Type.IsOfType(this._entitySet.EntityType) && !this._entitySet.EntityType.IsOfType(this._relationshipEnd.Type))
			{
				base.AddError(ErrorCode.InvalidEndEntitySet, EdmSchemaErrorSeverity.Error, Strings.InvalidEndEntitySetTypeMismatch(this._relationshipEnd.Name));
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000B417 File Offset: 0x00009617
		internal new EntityContainerRelationshipSet ParentElement
		{
			get
			{
				return (EntityContainerRelationshipSet)base.ParentElement;
			}
		}

		// Token: 0x040005B8 RID: 1464
		private IRelationshipEnd _relationshipEnd;

		// Token: 0x040005B9 RID: 1465
		private string _unresolvedEntitySetName;

		// Token: 0x040005BA RID: 1466
		private EntityContainerEntitySet _entitySet;
	}
}
