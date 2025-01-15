using System;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000054 RID: 84
	internal abstract class StructuredType : SchemaType
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00011BF5 File Offset: 0x0000FDF5
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x00011BFD File Offset: 0x0000FDFD
		public StructuredType BaseType
		{
			get
			{
				return this._baseType;
			}
			private set
			{
				this._baseType = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00011C06 File Offset: 0x0000FE06
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

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00011C27 File Offset: 0x0000FE27
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00011C42 File Offset: 0x0000FE42
		public virtual bool IsTypeHierarchyRoot
		{
			get
			{
				return this.BaseType == null;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00011C4D File Offset: 0x0000FE4D
		public bool IsAbstract
		{
			get
			{
				return this._isAbstract;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00011C58 File Offset: 0x0000FE58
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

		// Token: 0x06000871 RID: 2161 RVA: 0x00011C90 File Offset: 0x0000FE90
		public bool IsOfType(StructuredType baseType)
		{
			StructuredType structuredType = this;
			while (structuredType != null && structuredType != baseType)
			{
				structuredType = structuredType.BaseType;
			}
			return structuredType == baseType;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.TryResolveBaseType();
			foreach (SchemaElement schemaElement in this.NamedMembers)
			{
				schemaElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00011D0C File Offset: 0x0000FF0C
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

		// Token: 0x06000874 RID: 2164 RVA: 0x00011DAC File Offset: 0x0000FFAC
		protected StructuredType(Schema parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00011DB8 File Offset: 0x0000FFB8
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
			SchemaElementLookUpTable<SchemaElement> namedMembers = this.NamedMembers;
			bool flag = true;
			Func<object, string> func;
			if ((func = StructuredType.<>O.<0>__PropertyNameAlreadyDefinedDuplicate) == null)
			{
				func = (StructuredType.<>O.<0>__PropertyNameAlreadyDefinedDuplicate = new Func<object, string>(Strings.PropertyNameAlreadyDefinedDuplicate));
			}
			namedMembers.Add(newMember, flag, func);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00011E38 File Offset: 0x00010038
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

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00011E92 File Offset: 0x00010092
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00011E9A File Offset: 0x0001009A
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

		// Token: 0x06000879 RID: 2169 RVA: 0x00011EA3 File Offset: 0x000100A3
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

		// Token: 0x0600087A RID: 2170 RVA: 0x00011EC8 File Offset: 0x000100C8
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

		// Token: 0x0600087B RID: 2171 RVA: 0x00011F04 File Offset: 0x00010104
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

		// Token: 0x0600087C RID: 2172 RVA: 0x00012028 File Offset: 0x00010228
		private void HandleBaseTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this.UnresolvedBaseType = text;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001204D File Offset: 0x0001024D
		private void HandleAbstractAttribute(XmlReader reader)
		{
			base.HandleBoolAttribute(reader, ref this._isAbstract);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00012060 File Offset: 0x00010260
		private void HandlePropertyElement(XmlReader reader)
		{
			StructuredProperty structuredProperty = new StructuredProperty(this);
			structuredProperty.Parse(reader);
			this.AddMember(structuredProperty);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00012084 File Offset: 0x00010284
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

		// Token: 0x040006CA RID: 1738
		private bool? _baseTypeResolveResult;

		// Token: 0x040006CB RID: 1739
		private string _unresolvedBaseType;

		// Token: 0x040006CC RID: 1740
		private StructuredType _baseType;

		// Token: 0x040006CD RID: 1741
		private bool _isAbstract;

		// Token: 0x040006CE RID: 1742
		private SchemaElementLookUpTable<SchemaElement> _namedMembers;

		// Token: 0x040006CF RID: 1743
		private ISchemaElementLookUpTable<StructuredProperty> _properties;

		// Token: 0x020002A7 RID: 679
		private enum HowDefined
		{
			// Token: 0x04000F81 RID: 3969
			NotDefined,
			// Token: 0x04000F82 RID: 3970
			AsMember
		}

		// Token: 0x020002A8 RID: 680
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F83 RID: 3971
			public static Func<object, string> <0>__PropertyNameAlreadyDefinedDuplicate;
		}
	}
}
