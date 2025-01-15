using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000030 RID: 48
	[DebuggerDisplay("Name={Name}, BaseType={BaseType.FQName}, HasKeys={HasKeys}")]
	internal sealed class SchemaEntityType : StructuredType
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0000C84E File Offset: 0x0000AA4E
		public SchemaEntityType(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000C87C File Offset: 0x0000AA7C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.BaseType != null)
			{
				if (!(base.BaseType is SchemaEntityType))
				{
					base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForItemType(base.BaseType.FQName, this.FQName));
					return;
				}
				if (this._keyElement != null && base.BaseType != null)
				{
					base.AddError(ErrorCode.InvalidKey, EdmSchemaErrorSeverity.Error, Strings.InvalidKeyKeyDefinedInBaseClass(this.FQName, base.BaseType.FQName));
					return;
				}
			}
			else
			{
				if (this._keyElement == null)
				{
					base.AddError(ErrorCode.KeyMissingOnEntityType, EdmSchemaErrorSeverity.Error, Strings.KeyMissingOnEntityType(this.FQName));
					return;
				}
				if (base.BaseType == null && base.UnresolvedBaseType != null)
				{
					return;
				}
				this._keyElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000C930 File Offset: 0x0000AB30
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader);
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000C93E File Offset: 0x0000AB3E
		public EntityKeyElement KeyElement
		{
			get
			{
				return this._keyElement;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0000C946 File Offset: 0x0000AB46
		public IList<PropertyRefElement> DeclaredKeyProperties
		{
			get
			{
				if (this.KeyElement == null)
				{
					return SchemaEntityType.EmptyKeyProperties;
				}
				return this.KeyElement.KeyProperties;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000C961 File Offset: 0x0000AB61
		public IList<PropertyRefElement> KeyProperties
		{
			get
			{
				if (this.KeyElement != null)
				{
					return this.KeyElement.KeyProperties;
				}
				if (base.BaseType != null)
				{
					return (base.BaseType as SchemaEntityType).KeyProperties;
				}
				return SchemaEntityType.EmptyKeyProperties;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000C995 File Offset: 0x0000AB95
		public ISchemaElementLookUpTable<NavigationProperty> NavigationProperties
		{
			get
			{
				if (this._navigationProperties == null)
				{
					this._navigationProperties = new FilteredSchemaElementLookUpTable<NavigationProperty, SchemaElement>(base.NamedMembers);
				}
				return this._navigationProperties;
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000C9B6 File Offset: 0x0000ABB6
		internal override void Validate()
		{
			base.Validate();
			if (this.KeyElement != null)
			{
				this.KeyElement.Validate();
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000C9D1 File Offset: 0x0000ABD1
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Key"))
			{
				this.HandleKeyElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "NavigationProperty"))
			{
				this.HandleNavigationPropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000CA10 File Offset: 0x0000AC10
		private void HandleNavigationPropertyElement(XmlReader reader)
		{
			NavigationProperty navigationProperty = new NavigationProperty(this);
			navigationProperty.Parse(reader);
			base.AddMember(navigationProperty);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000CA32 File Offset: 0x0000AC32
		private void HandleKeyElement(XmlReader reader)
		{
			this._keyElement = new EntityKeyElement(this);
			this._keyElement.Parse(reader);
		}

		// Token: 0x04000663 RID: 1635
		private ISchemaElementLookUpTable<NavigationProperty> _navigationProperties;

		// Token: 0x04000664 RID: 1636
		private EntityKeyElement _keyElement;

		// Token: 0x04000665 RID: 1637
		private static List<PropertyRefElement> EmptyKeyProperties = new List<PropertyRefElement>(0);
	}
}
