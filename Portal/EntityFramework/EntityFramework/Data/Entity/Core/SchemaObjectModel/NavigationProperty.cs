using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002FD RID: 765
	[DebuggerDisplay("Name={Name}, Relationship={_unresolvedRelationshipName}, FromRole={_unresolvedFromEndRole}, ToRole={_unresolvedToEndRole}")]
	internal sealed class NavigationProperty : Property
	{
		// Token: 0x06002459 RID: 9305 RVA: 0x00066D55 File Offset: 0x00064F55
		public NavigationProperty(SchemaEntityType parent)
			: base(parent)
		{
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00066D5E File Offset: 0x00064F5E
		public new SchemaEntityType ParentElement
		{
			get
			{
				return base.ParentElement as SchemaEntityType;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x00066D6B File Offset: 0x00064F6B
		internal IRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00066D73 File Offset: 0x00064F73
		internal IRelationshipEnd ToEnd
		{
			get
			{
				return this._toEnd;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x00066D7B File Offset: 0x00064F7B
		internal IRelationshipEnd FromEnd
		{
			get
			{
				return this._fromEnd;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00066D83 File Offset: 0x00064F83
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

		// Token: 0x0600245F RID: 9311 RVA: 0x00066DA8 File Offset: 0x00064FA8
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
			return SchemaElement.CanHandleAttribute(reader, "ContainsTarget");
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00066E14 File Offset: 0x00065014
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

		// Token: 0x06002461 RID: 9313 RVA: 0x00066EFC File Offset: 0x000650FC
		internal override void Validate()
		{
			base.Validate();
			if (this._fromEnd.Type != this.ParentElement)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyBadFromRoleType(this.Name, this._fromEnd.Type.FQName, this._fromEnd.Name, this._relationship.FQName, this.ParentElement.FQName));
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00066F67 File Offset: 0x00065167
		private void HandleToRoleAttribute(XmlReader reader)
		{
			this._unresolvedToEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedToEndRole);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00066F7C File Offset: 0x0006517C
		private void HandleFromRoleAttribute(XmlReader reader)
		{
			this._unresolvedFromEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedFromEndRole);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00066F94 File Offset: 0x00065194
		private void HandleAssociationAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this._unresolvedRelationshipName = text;
		}

		// Token: 0x04000CED RID: 3309
		private string _unresolvedFromEndRole;

		// Token: 0x04000CEE RID: 3310
		private string _unresolvedToEndRole;

		// Token: 0x04000CEF RID: 3311
		private string _unresolvedRelationshipName;

		// Token: 0x04000CF0 RID: 3312
		private IRelationshipEnd _fromEnd;

		// Token: 0x04000CF1 RID: 3313
		private IRelationshipEnd _toEnd;

		// Token: 0x04000CF2 RID: 3314
		private IRelationship _relationship;
	}
}
