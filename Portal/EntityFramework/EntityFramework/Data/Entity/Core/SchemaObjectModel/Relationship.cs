using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000308 RID: 776
	internal sealed class Relationship : SchemaType, IRelationship
	{
		// Token: 0x060024B4 RID: 9396 RVA: 0x0006826C File Offset: 0x0006646C
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

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000682C7 File Offset: 0x000664C7
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

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x000682E2 File Offset: 0x000664E2
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

		// Token: 0x060024B7 RID: 9399 RVA: 0x000682FD File Offset: 0x000664FD
		public bool TryGetEnd(string roleName, out IRelationshipEnd end)
		{
			return this._ends.TryGetEnd(roleName, out end);
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0006830C File Offset: 0x0006650C
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x00068314 File Offset: 0x00066514
		public RelationshipKind RelationshipKind { get; private set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x0006831D File Offset: 0x0006651D
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00068328 File Offset: 0x00066528
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

		// Token: 0x060024BC RID: 9404 RVA: 0x00068414 File Offset: 0x00066614
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

		// Token: 0x060024BD RID: 9405 RVA: 0x000684A4 File Offset: 0x000666A4
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

		// Token: 0x060024BE RID: 9406 RVA: 0x000684E0 File Offset: 0x000666E0
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

		// Token: 0x060024BF RID: 9407 RVA: 0x0006852C File Offset: 0x0006672C
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

		// Token: 0x04000D03 RID: 3331
		private RelationshipEndCollection _ends;

		// Token: 0x04000D04 RID: 3332
		private List<ReferentialConstraint> _constraints;

		// Token: 0x04000D05 RID: 3333
		private bool _isForeignKey;
	}
}
