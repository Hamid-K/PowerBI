using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000024 RID: 36
	internal sealed class EntityKeyElement : SchemaElement
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0000B424 File Offset: 0x00009624
		public EntityKeyElement(SchemaEntityType parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000B42D File Offset: 0x0000962D
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

		// Token: 0x0600065D RID: 1629 RVA: 0x0000B448 File Offset: 0x00009648
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000B44B File Offset: 0x0000964B
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

		// Token: 0x0600065F RID: 1631 RVA: 0x0000B470 File Offset: 0x00009670
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement((SchemaEntityType)base.ParentElement);
			propertyRefElement.Parse(reader);
			this.KeyProperties.Add(propertyRefElement);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000B4A4 File Offset: 0x000096A4
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

		// Token: 0x06000661 RID: 1633 RVA: 0x0000B524 File Offset: 0x00009724
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
					if (!(property.Type is ScalarType) || property.CollectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.EntityKeyMustBeScalar, EdmSchemaErrorSeverity.Error, Strings.EntityKeyMustBeScalar(property.Name, base.ParentElement.Name));
					}
					else
					{
						PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)property.TypeUsage.EdmType).PrimitiveTypeKind;
						if (primitiveTypeKind == PrimitiveTypeKind.Binary)
						{
							if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
							{
								if (base.Schema.SchemaVersion < 2.0)
								{
									base.AddError(ErrorCode.BinaryEntityKeyCurrentlyNotSupported, EdmSchemaErrorSeverity.Error, Strings.EntityKeyTypeCurrentlyNotSupported(property.Name, base.ParentElement.FQName, primitiveTypeKind));
								}
							}
							else if (base.Schema.SchemaVersion < 2.0)
							{
								base.AddError(ErrorCode.BinaryEntityKeyCurrentlyNotSupported, EdmSchemaErrorSeverity.Error, Strings.EntityKeyTypeCurrentlyNotSupportedInSSDL(property.Name, base.ParentElement.FQName, property.TypeUsage.EdmType.Name, property.TypeUsage.EdmType.BaseType.FullName, primitiveTypeKind));
							}
						}
					}
				}
			}
		}

		// Token: 0x040005BB RID: 1467
		private List<PropertyRefElement> _keyProperties;
	}
}
