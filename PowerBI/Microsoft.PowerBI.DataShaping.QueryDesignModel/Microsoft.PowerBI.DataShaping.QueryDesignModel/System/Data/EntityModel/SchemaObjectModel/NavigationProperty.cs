using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000034 RID: 52
	[DebuggerDisplay("Name={Name}, Relationship={_unresolvedRelationshipName}, FromRole={_unresolvedFromEndRole}, ToRole={_unresolvedToEndRole}")]
	internal sealed class NavigationProperty : Property
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0000CD75 File Offset: 0x0000AF75
		public NavigationProperty(SchemaEntityType parent)
			: base(parent)
		{
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000CD7E File Offset: 0x0000AF7E
		public new SchemaEntityType ParentElement
		{
			get
			{
				return base.ParentElement as SchemaEntityType;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000CD8B File Offset: 0x0000AF8B
		internal IRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000CD93 File Offset: 0x0000AF93
		internal IRelationshipEnd ToEnd
		{
			get
			{
				return this._toEnd;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		internal IRelationshipEnd FromEnd
		{
			get
			{
				return this._fromEnd;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		public override SchemaType Type
		{
			get
			{
				if (this._toEnd == null || this._toEnd.Type == null)
				{
					return null;
				}
				return this._toEnd.Type;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Relationship"))
			{
				this.HandleAssociationAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "FromRole"))
			{
				this.HandleFromRoleAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ToRole"))
			{
				this.HandleToRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000CE24 File Offset: 0x0000B024
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, this._unresolvedRelationshipName, out schemaType))
			{
				return;
			}
			this._relationship = schemaType as IRelationship;
			if (this._relationship == null)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyRelationshipNotRelationship(this._unresolvedRelationshipName));
				return;
			}
			bool flag = true;
			if (!this._relationship.TryGetEnd(this._unresolvedFromEndRole, out this._fromEnd))
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyUndefinedRole(this._unresolvedFromEndRole, this._relationship.FQName));
				flag = false;
			}
			if (!this._relationship.TryGetEnd(this._unresolvedToEndRole, out this._toEnd))
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyUndefinedRole(this._unresolvedToEndRole, this._relationship.FQName));
				flag = false;
			}
			if (flag && this._fromEnd == this._toEnd)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyRolesCannotBeTheSame);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000CF0C File Offset: 0x0000B10C
		internal override void Validate()
		{
			base.Validate();
			if (this._fromEnd.Type != this.ParentElement)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyBadFromRoleType(this.Name, this._fromEnd.Type.FQName, this._fromEnd.Name, this._relationship.FQName, this.ParentElement.FQName));
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000CF77 File Offset: 0x0000B177
		private void HandleToRoleAttribute(XmlReader reader)
		{
			this._unresolvedToEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedToEndRole);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000CF8C File Offset: 0x0000B18C
		private void HandleFromRoleAttribute(XmlReader reader)
		{
			this._unresolvedFromEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedFromEndRole);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		private void HandleAssociationAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this._unresolvedRelationshipName = text;
		}

		// Token: 0x04000669 RID: 1641
		private string _unresolvedFromEndRole;

		// Token: 0x0400066A RID: 1642
		private string _unresolvedToEndRole;

		// Token: 0x0400066B RID: 1643
		private string _unresolvedRelationshipName;

		// Token: 0x0400066C RID: 1644
		private IRelationshipEnd _fromEnd;

		// Token: 0x0400066D RID: 1645
		private IRelationshipEnd _toEnd;

		// Token: 0x0400066E RID: 1646
		private IRelationship _relationship;
	}
}
