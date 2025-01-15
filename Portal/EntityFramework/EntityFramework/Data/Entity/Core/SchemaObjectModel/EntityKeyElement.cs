using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002EE RID: 750
	internal sealed class EntityKeyElement : SchemaElement
	{
		// Token: 0x060023C0 RID: 9152 RVA: 0x000651B8 File Offset: 0x000633B8
		public EntityKeyElement(SchemaEntityType parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x000651C2 File Offset: 0x000633C2
		public IList<PropertyRefElement> KeyProperties
		{
			get
			{
				if (this._keyProperties == null)
				{
					this._keyProperties = new List<PropertyRefElement>();
				}
				return this._keyProperties;
			}
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000651DD File Offset: 0x000633DD
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000651E0 File Offset: 0x000633E0
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

		// Token: 0x060023C4 RID: 9156 RVA: 0x00065208 File Offset: 0x00063408
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement(base.ParentElement);
			propertyRefElement.Parse(reader);
			this.KeyProperties.Add(propertyRefElement);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00065234 File Offset: 0x00063434
		internal override void ResolveTopLevelNames()
		{
			foreach (PropertyRefElement propertyRefElement in this._keyProperties)
			{
				if (!propertyRefElement.ResolveNames((SchemaEntityType)base.ParentElement))
				{
					base.AddError(ErrorCode.InvalidKey, EdmSchemaErrorSeverity.Error, Strings.InvalidKeyNoProperty(base.ParentElement.FQName, propertyRefElement.Name));
				}
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000652B4 File Offset: 0x000634B4
		internal override void Validate()
		{
			Dictionary<string, PropertyRefElement> dictionary = new Dictionary<string, PropertyRefElement>(StringComparer.Ordinal);
			foreach (PropertyRefElement propertyRefElement in this._keyProperties)
			{
				StructuredProperty property = propertyRefElement.Property;
				if (dictionary.ContainsKey(property.Name))
				{
					base.AddError(ErrorCode.DuplicatePropertySpecifiedInEntityKey, EdmSchemaErrorSeverity.Error, Strings.DuplicatePropertyNameSpecifiedInEntityKey(base.ParentElement.FQName, property.Name));
				}
				else
				{
					dictionary.Add(property.Name, propertyRefElement);
					if (property.Nullable)
					{
						base.AddError(ErrorCode.InvalidKey, EdmSchemaErrorSeverity.Error, Strings.InvalidKeyNullablePart(property.Name, base.ParentElement.Name));
					}
					if ((!(property.Type is ScalarType) && !(property.Type is SchemaEnumType)) || property.CollectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.EntityKeyMustBeScalar, EdmSchemaErrorSeverity.Error, Strings.EntityKeyMustBeScalar(property.Name, base.ParentElement.Name));
					}
					else if (!(property.Type is SchemaEnumType))
					{
						PrimitiveType primitiveType = (PrimitiveType)property.TypeUsage.EdmType;
						if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
						{
							if ((primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary && base.Schema.SchemaVersion < 2.0) || Helper.IsSpatialType(primitiveType))
							{
								base.AddError(ErrorCode.EntityKeyTypeCurrentlyNotSupported, EdmSchemaErrorSeverity.Error, Strings.EntityKeyTypeCurrentlyNotSupported(property.Name, base.ParentElement.FQName, primitiveType.PrimitiveTypeKind));
							}
						}
						else if ((primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary && base.Schema.SchemaVersion < 2.0) || Helper.IsSpatialType(primitiveType))
						{
							base.AddError(ErrorCode.EntityKeyTypeCurrentlyNotSupported, EdmSchemaErrorSeverity.Error, Strings.EntityKeyTypeCurrentlyNotSupportedInSSDL(property.Name, base.ParentElement.FQName, property.TypeUsage.EdmType.Name, property.TypeUsage.EdmType.BaseType.FullName, primitiveType.PrimitiveTypeKind));
						}
					}
				}
			}
		}

		// Token: 0x04000C2E RID: 3118
		private List<PropertyRefElement> _keyProperties;
	}
}
