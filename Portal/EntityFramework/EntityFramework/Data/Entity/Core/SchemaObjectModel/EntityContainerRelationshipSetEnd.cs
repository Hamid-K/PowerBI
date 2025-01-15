using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002ED RID: 749
	internal class EntityContainerRelationshipSetEnd : SchemaElement
	{
		// Token: 0x060023B5 RID: 9141 RVA: 0x00065035 File Offset: 0x00063235
		public EntityContainerRelationshipSetEnd(EntityContainerRelationshipSet parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x0006503F File Offset: 0x0006323F
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x00065047 File Offset: 0x00063247
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

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x00065050 File Offset: 0x00063250
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x00065058 File Offset: 0x00063258
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

		// Token: 0x060023BA RID: 9146 RVA: 0x00065061 File Offset: 0x00063261
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

		// Token: 0x060023BB RID: 9147 RVA: 0x00065081 File Offset: 0x00063281
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

		// Token: 0x060023BC RID: 9148 RVA: 0x000650A5 File Offset: 0x000632A5
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this._unresolvedEntitySetName = reader.Value;
				return;
			}
			this._unresolvedEntitySetName = base.HandleUndottedNameAttribute(reader, this._unresolvedEntitySetName);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000650D8 File Offset: 0x000632D8
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

		// Token: 0x060023BE RID: 9150 RVA: 0x00065134 File Offset: 0x00063334
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

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000651AB File Offset: 0x000633AB
		internal new EntityContainerRelationshipSet ParentElement
		{
			get
			{
				return (EntityContainerRelationshipSet)base.ParentElement;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private IRelationshipEnd _relationshipEnd;

		// Token: 0x04000C2C RID: 3116
		private string _unresolvedEntitySetName;

		// Token: 0x04000C2D RID: 3117
		private EntityContainerEntitySet _entitySet;
	}
}
