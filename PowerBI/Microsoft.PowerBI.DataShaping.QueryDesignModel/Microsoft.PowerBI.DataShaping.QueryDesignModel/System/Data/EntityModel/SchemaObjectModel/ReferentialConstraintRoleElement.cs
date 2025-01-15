using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200003D RID: 61
	internal sealed class ReferentialConstraintRoleElement : SchemaElement
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0000DF21 File Offset: 0x0000C121
		public ReferentialConstraintRoleElement(ReferentialConstraint parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0000DF2A File Offset: 0x0000C12A
		public IList<PropertyRefElement> RoleProperties
		{
			get
			{
				if (this._roleProperties == null)
				{
					this._roleProperties = new List<PropertyRefElement>();
				}
				return this._roleProperties;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000DF45 File Offset: 0x0000C145
		public IRelationshipEnd End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000DF4D File Offset: 0x0000C14D
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "PropertyRef"))
			{
				this.HandlePropertyRefElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000DF72 File Offset: 0x0000C172
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000DF8C File Offset: 0x0000C18C
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement(base.ParentElement);
			propertyRefElement.Parse(reader);
			this.RoleProperties.Add(propertyRefElement);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		private void HandleRoleAttribute(XmlReader reader)
		{
			string text;
			Utils.GetString(base.Schema, reader, out text);
			this.Name = text;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		internal override void ResolveTopLevelNames()
		{
			IRelationship relationship = (IRelationship)base.ParentElement.ParentElement;
			if (!relationship.TryGetEnd(this.Name, out this._end))
			{
				base.AddError(ErrorCode.InvalidRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidEndRoleInRelationshipConstraint(this.Name, relationship.Name));
				return;
			}
			SchemaEntityType type = this._end.Type;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000E038 File Offset: 0x0000C238
		internal override void Validate()
		{
			base.Validate();
			foreach (PropertyRefElement propertyRefElement in this._roleProperties)
			{
				if (!propertyRefElement.ResolveNames(this._end.Type))
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidPropertyInRelationshipConstraint(propertyRefElement.Name, this.Name));
				}
			}
		}

		// Token: 0x0400067B RID: 1659
		private List<PropertyRefElement> _roleProperties;

		// Token: 0x0400067C RID: 1660
		private IRelationshipEnd _end;
	}
}
