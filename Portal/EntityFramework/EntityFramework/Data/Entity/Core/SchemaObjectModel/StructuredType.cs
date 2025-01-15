using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200031F RID: 799
	internal abstract class StructuredType : SchemaType
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x0006C90F File Offset: 0x0006AB0F
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x0006C917 File Offset: 0x0006AB17
		public StructuredType BaseType { get; private set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x0006C920 File Offset: 0x0006AB20
		public ISchemaElementLookUpTable<StructuredProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new FilteredSchemaElementLookUpTable<StructuredProperty, SchemaElement>(this.NamedMembers);
				}
				return this._properties;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x0006C941 File Offset: 0x0006AB41
		protected SchemaElementLookUpTable<SchemaElement> NamedMembers
		{
			get
			{
				if (this._namedMembers == null)
				{
					this._namedMembers = new SchemaElementLookUpTable<SchemaElement>();
				}
				return this._namedMembers;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x0006C95C File Offset: 0x0006AB5C
		public virtual bool IsTypeHierarchyRoot
		{
			get
			{
				return this.BaseType == null;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x0006C967 File Offset: 0x0006AB67
		public bool IsAbstract
		{
			get
			{
				return this._isAbstract;
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0006C970 File Offset: 0x0006AB70
		public StructuredProperty FindProperty(string name)
		{
			StructuredProperty structuredProperty = this.Properties.LookUpEquivalentKey(name);
			if (structuredProperty != null)
			{
				return structuredProperty;
			}
			if (this.IsTypeHierarchyRoot)
			{
				return null;
			}
			return this.BaseType.FindProperty(name);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0006C9A8 File Offset: 0x0006ABA8
		public bool IsOfType(StructuredType baseType)
		{
			StructuredType structuredType = this;
			while (structuredType != null && structuredType != baseType)
			{
				structuredType = structuredType.BaseType;
			}
			return structuredType == baseType;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0006C9CC File Offset: 0x0006ABCC
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.TryResolveBaseType();
			foreach (SchemaElement schemaElement in this.NamedMembers)
			{
				schemaElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0006CA24 File Offset: 0x0006AC24
		internal override void Validate()
		{
			base.Validate();
			foreach (SchemaElement schemaElement in this.NamedMembers)
			{
				if (this.BaseType != null)
				{
					string text = null;
					StructuredType structuredType;
					SchemaElement schemaElement2;
					if (StructuredType.HowDefined.AsMember == this.BaseType.DefinesMemberName(schemaElement.Name, out structuredType, out schemaElement2))
					{
						text = Strings.DuplicateMemberName(schemaElement.Name, this.FQName, structuredType.FQName);
					}
					if (text != null)
					{
						schemaElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, text);
					}
				}
				schemaElement.Validate();
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x0006CAC4 File Offset: 0x0006ACC4
		protected StructuredType(Schema parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0006CAD0 File Offset: 0x0006ACD0
		protected void AddMember(SchemaElement newMember)
		{
			if (string.IsNullOrEmpty(newMember.Name))
			{
				return;
			}
			if (base.Schema.DataModel != SchemaDataModelOption.ProviderDataModel && Utils.CompareNames(newMember.Name, this.Name) == 0)
			{
				newMember.AddError(ErrorCode.BadProperty, EdmSchemaErrorSeverity.Error, Strings.InvalidMemberNameMatchesTypeName(newMember.Name, this.FQName));
			}
			this.NamedMembers.Add(newMember, true, new Func<object, string>(Strings.PropertyNameAlreadyDefinedDuplicate));
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0006CB40 File Offset: 0x0006AD40
		private StructuredType.HowDefined DefinesMemberName(string name, out StructuredType definingType, out SchemaElement definingMember)
		{
			if (this.NamedMembers.ContainsKey(name))
			{
				definingType = this;
				definingMember = this.NamedMembers[name];
				return StructuredType.HowDefined.AsMember;
			}
			definingMember = this.NamedMembers.LookUpEquivalentKey(name);
			if (this.IsTypeHierarchyRoot)
			{
				definingType = null;
				definingMember = null;
				return StructuredType.HowDefined.NotDefined;
			}
			return this.BaseType.DefinesMemberName(name, out definingType, out definingMember);
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x0006CB9A File Offset: 0x0006AD9A
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x0006CBA2 File Offset: 0x0006ADA2
		protected string UnresolvedBaseType
		{
			get
			{
				return this._unresolvedBaseType;
			}
			set
			{
				this._unresolvedBaseType = value;
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0006CBAB File Offset: 0x0006ADAB
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Property"))
			{
				this.HandlePropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0006CBD0 File Offset: 0x0006ADD0
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "BaseType"))
			{
				this.HandleBaseTypeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Abstract"))
			{
				this.HandleAbstractAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0006CC0C File Offset: 0x0006AE0C
		private bool TryResolveBaseType()
		{
			if (this._baseTypeResolveResult != null)
			{
				return this._baseTypeResolveResult.Value;
			}
			if (this.BaseType != null)
			{
				this._baseTypeResolveResult = new bool?(true);
				return this._baseTypeResolveResult.Value;
			}
			if (this.UnresolvedBaseType == null)
			{
				this._baseTypeResolveResult = new bool?(true);
				return this._baseTypeResolveResult.Value;
			}
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, this.UnresolvedBaseType, out schemaType))
			{
				this._baseTypeResolveResult = new bool?(false);
				return this._baseTypeResolveResult.Value;
			}
			this.BaseType = schemaType as StructuredType;
			if (this.BaseType == null)
			{
				base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForStructuredType(this.UnresolvedBaseType, this.FQName));
				this._baseTypeResolveResult = new bool?(false);
				return this._baseTypeResolveResult.Value;
			}
			if (this.CheckForInheritanceCycle())
			{
				this.BaseType = null;
				base.AddError(ErrorCode.CycleInTypeHierarchy, EdmSchemaErrorSeverity.Error, Strings.CycleInTypeHierarchy(this.FQName));
				this._baseTypeResolveResult = new bool?(false);
				return this._baseTypeResolveResult.Value;
			}
			this._baseTypeResolveResult = new bool?(true);
			return true;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0006CD30 File Offset: 0x0006AF30
		private void HandleBaseTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this.UnresolvedBaseType = text;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0006CD55 File Offset: 0x0006AF55
		private void HandleAbstractAttribute(XmlReader reader)
		{
			base.HandleBoolAttribute(reader, ref this._isAbstract);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0006CD68 File Offset: 0x0006AF68
		private void HandlePropertyElement(XmlReader reader)
		{
			StructuredProperty structuredProperty = new StructuredProperty(this);
			structuredProperty.Parse(reader);
			this.AddMember(structuredProperty);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0006CD8C File Offset: 0x0006AF8C
		private bool CheckForInheritanceCycle()
		{
			StructuredType structuredType2;
			StructuredType structuredType = (structuredType2 = this.BaseType);
			for (;;)
			{
				structuredType2 = structuredType2.BaseType;
				if (structuredType == structuredType2)
				{
					break;
				}
				if (structuredType == null)
				{
					return false;
				}
				structuredType = structuredType.BaseType;
				if (structuredType2 != null)
				{
					structuredType2 = structuredType2.BaseType;
				}
				if (structuredType2 == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000D57 RID: 3415
		private bool? _baseTypeResolveResult;

		// Token: 0x04000D58 RID: 3416
		private string _unresolvedBaseType;

		// Token: 0x04000D59 RID: 3417
		private bool _isAbstract;

		// Token: 0x04000D5A RID: 3418
		private SchemaElementLookUpTable<SchemaElement> _namedMembers;

		// Token: 0x04000D5B RID: 3419
		private ISchemaElementLookUpTable<StructuredProperty> _properties;

		// Token: 0x020009C0 RID: 2496
		private enum HowDefined
		{
			// Token: 0x0400280C RID: 10252
			NotDefined,
			// Token: 0x0400280D RID: 10253
			AsMember
		}
	}
}
