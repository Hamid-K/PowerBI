using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002FA RID: 762
	[DebuggerDisplay("Name={Name}, BaseType={BaseType.FQName}, HasKeys={HasKeys}")]
	internal sealed class SchemaEntityType : StructuredType
	{
		// Token: 0x0600243E RID: 9278 RVA: 0x000668E2 File Offset: 0x00064AE2
		public SchemaEntityType(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00066910 File Offset: 0x00064B10
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

		// Token: 0x06002440 RID: 9280 RVA: 0x000669C4 File Offset: 0x00064BC4
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || (SchemaElement.CanHandleAttribute(reader, "OpenType") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel);
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000669EE File Offset: 0x00064BEE
		public EntityKeyElement KeyElement
		{
			get
			{
				return this._keyElement;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x000669F6 File Offset: 0x00064BF6
		public IList<PropertyRefElement> DeclaredKeyProperties
		{
			get
			{
				if (this.KeyElement == null)
				{
					return SchemaEntityType._emptyKeyProperties;
				}
				return this.KeyElement.KeyProperties;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00066A11 File Offset: 0x00064C11
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
				return SchemaEntityType._emptyKeyProperties;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x00066A45 File Offset: 0x00064C45
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

		// Token: 0x06002445 RID: 9285 RVA: 0x00066A66 File Offset: 0x00064C66
		internal override void Validate()
		{
			base.Validate();
			if (this.KeyElement != null)
			{
				this.KeyElement.Validate();
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00066A84 File Offset: 0x00064C84
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
			if (base.CanHandleElement(reader, "ValueAnnotation") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				this.SkipElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeAnnotation") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				this.SkipElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00066B14 File Offset: 0x00064D14
		private void HandleNavigationPropertyElement(XmlReader reader)
		{
			NavigationProperty navigationProperty = new NavigationProperty(this);
			navigationProperty.Parse(reader);
			base.AddMember(navigationProperty);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00066B36 File Offset: 0x00064D36
		private void HandleKeyElement(XmlReader reader)
		{
			this._keyElement = new EntityKeyElement(this);
			this._keyElement.Parse(reader);
		}

		// Token: 0x04000CE7 RID: 3303
		private const char KEY_DELIMITER = ' ';

		// Token: 0x04000CE8 RID: 3304
		private ISchemaElementLookUpTable<NavigationProperty> _navigationProperties;

		// Token: 0x04000CE9 RID: 3305
		private EntityKeyElement _keyElement;

		// Token: 0x04000CEA RID: 3306
		private static readonly List<PropertyRefElement> _emptyKeyProperties = new List<PropertyRefElement>(0);
	}
}
