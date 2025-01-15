using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000309 RID: 777
	internal sealed class RelationshipEnd : SchemaElement, IRelationshipEnd
	{
		// Token: 0x060024C0 RID: 9408 RVA: 0x0006857D File Offset: 0x0006677D
		public RelationshipEnd(Relationship relationship)
			: base(relationship, null)
		{
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x00068587 File Offset: 0x00066787
		// (set) Token: 0x060024C2 RID: 9410 RVA: 0x0006858F File Offset: 0x0006678F
		public SchemaEntityType Type { get; private set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x00068598 File Offset: 0x00066798
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x000685A0 File Offset: 0x000667A0
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

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000685A9 File Offset: 0x000667A9
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

		// Token: 0x060024C6 RID: 9414 RVA: 0x000685C4 File Offset: 0x000667C4
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

		// Token: 0x060024C7 RID: 9415 RVA: 0x00068634 File Offset: 0x00066834
		internal override void Validate()
		{
			base.Validate();
			RelationshipMultiplicity? multiplicity = this.Multiplicity;
			RelationshipMultiplicity relationshipMultiplicity = RelationshipMultiplicity.Many;
			if (((multiplicity.GetValueOrDefault() == relationshipMultiplicity) & (multiplicity != null)) && this.Operations.Count != 0)
			{
				base.AddError(ErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, EdmSchemaErrorSeverity.Error, Strings.EndWithManyMultiplicityCannotHaveOperationsSpecified(this.Name, this.ParentElement.FQName));
			}
			if (this.ParentElement.Constraints.Count == 0 && this.Multiplicity == null)
			{
				base.AddError(ErrorCode.EndWithoutMultiplicity, EdmSchemaErrorSeverity.Error, Strings.EndWithoutMultiplicity(this.Name, this.ParentElement.FQName));
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000686D7 File Offset: 0x000668D7
		protected override void HandleAttributesComplete()
		{
			if (this.Name == null && this._unresolvedType != null)
			{
				this.Name = Utils.ExtractTypeName(this._unresolvedType);
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00068700 File Offset: 0x00066900
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

		// Token: 0x060024CA RID: 9418 RVA: 0x00068720 File Offset: 0x00066920
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

		// Token: 0x060024CB RID: 9419 RVA: 0x0006877B File Offset: 0x0006697B
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

		// Token: 0x060024CC RID: 9420 RVA: 0x000687A0 File Offset: 0x000669A0
		private void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000687C8 File Offset: 0x000669C8
		private void HandleMultiplicityAttribute(XmlReader reader)
		{
			RelationshipMultiplicity relationshipMultiplicity;
			if (!RelationshipMultiplicityConverter.TryParseMultiplicity(reader.Value, out relationshipMultiplicity))
			{
				base.AddError(ErrorCode.InvalidMultiplicity, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidRelationshipEndMultiplicity(this.ParentElement.Name, reader.Value));
			}
			this._multiplicity = new RelationshipMultiplicity?(relationshipMultiplicity);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00068810 File Offset: 0x00066A10
		private void HandleOnDeleteElement(XmlReader reader)
		{
			this.HandleOnOperationElement(reader, Operation.Delete);
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0006881C File Offset: 0x00066A1C
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

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0006889C File Offset: 0x00066A9C
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x04000D07 RID: 3335
		private string _unresolvedType;

		// Token: 0x04000D08 RID: 3336
		private RelationshipMultiplicity? _multiplicity;

		// Token: 0x04000D09 RID: 3337
		private List<OnOperation> _operations;
	}
}
