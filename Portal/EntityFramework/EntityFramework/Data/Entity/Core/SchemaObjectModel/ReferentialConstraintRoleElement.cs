using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000307 RID: 775
	internal sealed class ReferentialConstraintRoleElement : SchemaElement
	{
		// Token: 0x060024AB RID: 9387 RVA: 0x000680D5 File Offset: 0x000662D5
		public ReferentialConstraintRoleElement(ReferentialConstraint parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000680DF File Offset: 0x000662DF
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

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x000680FA File Offset: 0x000662FA
		public IRelationshipEnd End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x00068102 File Offset: 0x00066302
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

		// Token: 0x060024AF RID: 9391 RVA: 0x00068127 File Offset: 0x00066327
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00068140 File Offset: 0x00066340
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement(base.ParentElement);
			propertyRefElement.Parse(reader);
			this.RoleProperties.Add(propertyRefElement);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0006816C File Offset: 0x0006636C
		private void HandleRoleAttribute(XmlReader reader)
		{
			string text;
			Utils.GetString(base.Schema, reader, out text);
			this.Name = text;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00068190 File Offset: 0x00066390
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

		// Token: 0x060024B3 RID: 9395 RVA: 0x000681EC File Offset: 0x000663EC
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

		// Token: 0x04000D01 RID: 3329
		private List<PropertyRefElement> _roleProperties;

		// Token: 0x04000D02 RID: 3330
		private IRelationshipEnd _end;
	}
}
