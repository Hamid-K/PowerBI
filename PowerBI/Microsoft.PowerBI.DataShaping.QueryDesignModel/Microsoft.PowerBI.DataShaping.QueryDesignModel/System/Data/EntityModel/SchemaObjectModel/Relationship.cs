using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200003E RID: 62
	internal sealed class Relationship : SchemaType, IRelationship
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		public Relationship(Schema parent, RelationshipKind kind)
			: base(parent)
		{
			this.RelationshipKind = kind;
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				this._isForeignKey = false;
				base.OtherContent.Add(base.Schema.SchemaSource);
				return;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this._isForeignKey = true;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000E113 File Offset: 0x0000C313
		public IList<IRelationshipEnd> Ends
		{
			get
			{
				if (this._ends == null)
				{
					this._ends = new RelationshipEndCollection();
				}
				return this._ends;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0000E12E File Offset: 0x0000C32E
		public IList<ReferentialConstraint> Constraints
		{
			get
			{
				if (this._constraints == null)
				{
					this._constraints = new List<ReferentialConstraint>();
				}
				return this._constraints;
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000E149 File Offset: 0x0000C349
		public bool TryGetEnd(string roleName, out IRelationshipEnd end)
		{
			return this._ends.TryGetEnd(roleName, out end);
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0000E158 File Offset: 0x0000C358
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0000E160 File Offset: 0x0000C360
		public RelationshipKind RelationshipKind
		{
			get
			{
				return this._relationshipKind;
			}
			private set
			{
				this._relationshipKind = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0000E169 File Offset: 0x0000C369
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000E174 File Offset: 0x0000C374
		internal override void Validate()
		{
			base.Validate();
			bool flag = false;
			foreach (IRelationshipEnd relationshipEnd in this.Ends)
			{
				RelationshipEnd relationshipEnd2 = (RelationshipEnd)relationshipEnd;
				relationshipEnd2.Validate();
				if (this.RelationshipKind == RelationshipKind.Association && relationshipEnd2.Operations.Count > 0)
				{
					if (flag)
					{
						relationshipEnd2.AddError(ErrorCode.InvalidOperation, EdmSchemaErrorSeverity.Error, Strings.InvalidOperationMultipleEndsInAssociation);
					}
					flag = true;
				}
			}
			if (this.Constraints.Count == 0)
			{
				if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
				{
					base.AddError(ErrorCode.MissingConstraintOnRelationshipType, EdmSchemaErrorSeverity.Error, Strings.MissingConstraintOnRelationshipType(this.FQName));
					return;
				}
			}
			else
			{
				foreach (ReferentialConstraint referentialConstraint in this.Constraints)
				{
					referentialConstraint.Validate();
				}
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000E260 File Offset: 0x0000C460
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			foreach (IRelationshipEnd relationshipEnd in this.Ends)
			{
				((RelationshipEnd)relationshipEnd).ResolveTopLevelNames();
			}
			foreach (ReferentialConstraint referentialConstraint in this.Constraints)
			{
				referentialConstraint.ResolveTopLevelNames();
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
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
			if (base.CanHandleElement(reader, "ReferentialConstraint"))
			{
				this.HandleConstraintElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000E32C File Offset: 0x0000C52C
		private void HandleEndElement(XmlReader reader)
		{
			RelationshipEnd relationshipEnd = new RelationshipEnd(this);
			relationshipEnd.Parse(reader);
			if (this.Ends.Count == 2)
			{
				base.AddError(ErrorCode.InvalidAssociation, EdmSchemaErrorSeverity.Error, Strings.TooManyAssociationEnds(this.FQName));
				return;
			}
			this.Ends.Add(relationshipEnd);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000E378 File Offset: 0x0000C578
		private void HandleConstraintElement(XmlReader reader)
		{
			ReferentialConstraint referentialConstraint = new ReferentialConstraint(this);
			referentialConstraint.Parse(reader);
			this.Constraints.Add(referentialConstraint);
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel && base.Schema.SchemaVersion >= 2.0)
			{
				this._isForeignKey = true;
			}
		}

		// Token: 0x0400067D RID: 1661
		private RelationshipKind _relationshipKind;

		// Token: 0x0400067E RID: 1662
		private RelationshipEndCollection _ends;

		// Token: 0x0400067F RID: 1663
		private List<ReferentialConstraint> _constraints;

		// Token: 0x04000680 RID: 1664
		private bool _isForeignKey;
	}
}
