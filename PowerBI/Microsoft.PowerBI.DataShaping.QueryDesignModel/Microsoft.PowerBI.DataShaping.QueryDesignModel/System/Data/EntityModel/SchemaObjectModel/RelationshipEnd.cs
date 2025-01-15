using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200003F RID: 63
	internal sealed class RelationshipEnd : SchemaElement, IRelationshipEnd
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0000E3C9 File Offset: 0x0000C5C9
		public RelationshipEnd(Relationship relationship)
			: base(relationship)
		{
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0000E3D2 File Offset: 0x0000C5D2
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		public SchemaEntityType Type
		{
			get
			{
				return this._type;
			}
			private set
			{
				this._type = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0000E3E3 File Offset: 0x0000C5E3
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0000E3EB File Offset: 0x0000C5EB
		public RelationshipMultiplicity? Multiplicity
		{
			get
			{
				return this._multiplicity;
			}
			set
			{
				this._multiplicity = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public ICollection<OnOperation> Operations
		{
			get
			{
				if (this._operations == null)
				{
					this._operations = new List<OnOperation>();
				}
				return this._operations;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000E410 File Offset: 0x0000C610
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this.Type == null && this._unresolvedType != null)
			{
				SchemaType schemaType;
				if (!base.Schema.ResolveTypeName(this, this._unresolvedType, out schemaType))
				{
					return;
				}
				this.Type = schemaType as SchemaEntityType;
				if (this.Type == null)
				{
					base.AddError(ErrorCode.InvalidRelationshipEndType, EdmSchemaErrorSeverity.Error, Strings.InvalidRelationshipEndType(this.ParentElement.Name, schemaType.FQName));
				}
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000E480 File Offset: 0x0000C680
		internal override void Validate()
		{
			base.Validate();
			RelationshipMultiplicity? multiplicity = this.Multiplicity;
			RelationshipMultiplicity relationshipMultiplicity = RelationshipMultiplicity.Many;
			if (((multiplicity.GetValueOrDefault() == relationshipMultiplicity) & (multiplicity != null)) && this.Operations.Count != 0)
			{
				base.AddError(ErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, EdmSchemaErrorSeverity.Error, Strings.EndWithManyMultiplicityCannotHaveOperationsSpecified(this.Name, this.ParentElement.FQName));
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000E4DF File Offset: 0x0000C6DF
		protected override void HandleAttributesComplete()
		{
			if (this.Name == null && this._unresolvedType != null)
			{
				this.Name = Utils.ExtractTypeName(base.Schema.DataModel, this._unresolvedType);
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000E513 File Offset: 0x0000C713
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

		// Token: 0x06000759 RID: 1881 RVA: 0x0000E534 File Offset: 0x0000C734
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Multiplicity"))
			{
				this.HandleMultiplicityAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleNameAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000E58F File Offset: 0x0000C78F
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "OnDelete"))
			{
				this.HandleOnDeleteElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		private void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		private void HandleMultiplicityAttribute(XmlReader reader)
		{
			RelationshipMultiplicity relationshipMultiplicity;
			if (!RelationshipEnd.TryParseMultiplicity(reader.Value, out relationshipMultiplicity))
			{
				base.AddError(ErrorCode.InvalidMultiplicity, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidRelationshipEndMultiplicity(this.ParentElement.Name, reader.Value));
			}
			this._multiplicity = new RelationshipMultiplicity?(relationshipMultiplicity);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000E624 File Offset: 0x0000C824
		private void HandleOnDeleteElement(XmlReader reader)
		{
			this.HandleOnOperationElement(reader, Operation.Delete);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000E630 File Offset: 0x0000C830
		private void HandleOnOperationElement(XmlReader reader, Operation operation)
		{
			using (IEnumerator<OnOperation> enumerator = this.Operations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Operation == operation)
					{
						base.AddError(ErrorCode.InvalidOperation, EdmSchemaErrorSeverity.Error, reader, Strings.DuplicationOperation(reader.Name));
					}
				}
			}
			OnOperation onOperation = new OnOperation(this, operation);
			onOperation.Parse(reader);
			this._operations.Add(onOperation);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000E6BD File Offset: 0x0000C8BD
		private static bool TryParseMultiplicity(string value, out RelationshipMultiplicity multiplicity)
		{
			if (value == "0..1")
			{
				multiplicity = RelationshipMultiplicity.ZeroOrOne;
				return true;
			}
			if (value == "1")
			{
				multiplicity = RelationshipMultiplicity.One;
				return true;
			}
			if (!(value == "*"))
			{
				multiplicity = (RelationshipMultiplicity)(-1);
				return false;
			}
			multiplicity = RelationshipMultiplicity.Many;
			return true;
		}

		// Token: 0x04000681 RID: 1665
		private string _unresolvedType;

		// Token: 0x04000682 RID: 1666
		private RelationshipMultiplicity? _multiplicity;

		// Token: 0x04000683 RID: 1667
		private SchemaEntityType _type;

		// Token: 0x04000684 RID: 1668
		private List<OnOperation> _operations;
	}
}
