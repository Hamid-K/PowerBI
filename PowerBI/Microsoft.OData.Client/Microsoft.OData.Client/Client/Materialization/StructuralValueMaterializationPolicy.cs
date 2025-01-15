using System;
using System.Collections.Generic;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000100 RID: 256
	internal abstract class StructuralValueMaterializationPolicy : MaterializationPolicy
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x00028E9B File Offset: 0x0002709B
		protected StructuralValueMaterializationPolicy(IODataMaterializerContext materializerContext, SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter)
		{
			this.MaterializerContext = materializerContext;
			this.lazyPrimitivePropertyConverter = lazyPrimitivePropertyConverter;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00028EB1 File Offset: 0x000270B1
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x00028EB9 File Offset: 0x000270B9
		protected internal CollectionValueMaterializationPolicy CollectionValueMaterializationPolicy
		{
			get
			{
				return this.collectionValueMaterializationPolicy;
			}
			set
			{
				this.collectionValueMaterializationPolicy = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00028EC2 File Offset: 0x000270C2
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00028ECA File Offset: 0x000270CA
		protected internal InstanceAnnotationMaterializationPolicy InstanceAnnotationMaterializationPolicy
		{
			get
			{
				return this.instanceAnnotationMaterializationPolicy;
			}
			set
			{
				this.instanceAnnotationMaterializationPolicy = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00028ED3 File Offset: 0x000270D3
		protected PrimitivePropertyConverter PrimitivePropertyConverter
		{
			get
			{
				return this.lazyPrimitivePropertyConverter.Value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00028EE0 File Offset: 0x000270E0
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00028EE8 File Offset: 0x000270E8
		private protected IODataMaterializerContext MaterializerContext { protected get; private set; }

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00028EF4 File Offset: 0x000270F4
		internal void MaterializePrimitiveDataValue(Type type, ODataProperty property)
		{
			if (!property.HasMaterializedValue())
			{
				object obj = property.Value;
				ODataUntypedValue odataUntypedValue = obj as ODataUntypedValue;
				if (odataUntypedValue != null && this.MaterializerContext.UndeclaredPropertyBehavior == UndeclaredPropertyBehavior.Support)
				{
					obj = CommonUtil.ParseJsonToPrimitiveValue(odataUntypedValue.RawValue);
				}
				object obj2 = this.PrimitivePropertyConverter.ConvertPrimitiveValue(obj, type);
				property.SetMaterializedValue(obj2);
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00028F48 File Offset: 0x00027148
		internal void ApplyDataValues(ClientTypeAnnotation type, IEnumerable<ODataProperty> properties, object instance)
		{
			foreach (ODataProperty odataProperty in properties)
			{
				this.ApplyDataValue(type, odataProperty, instance);
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00028F94 File Offset: 0x00027194
		internal void ApplyDataValue(ClientTypeAnnotation type, ODataProperty property, object instance)
		{
			ClientPropertyAnnotation property2 = type.GetProperty(property.Name, this.MaterializerContext.UndeclaredPropertyBehavior);
			if (property2 == null)
			{
				return;
			}
			Type type2;
			if (property2.IsPrimitiveOrEnumOrComplexCollection)
			{
				if (property.Value == null)
				{
					throw Error.InvalidOperation(Strings.Collection_NullCollectionNotSupported(property.Name));
				}
				if (property.Value is string)
				{
					throw Error.InvalidOperation(Strings.Deserialize_MixedTextWithComment);
				}
				object obj = property2.GetValue(instance);
				if (obj == null)
				{
					obj = this.CollectionValueMaterializationPolicy.CreateCollectionPropertyInstance(property, property2.PropertyType);
					property2.SetValue(instance, obj, property.Name, false);
				}
				else
				{
					property2.ClearBackingICollectionInstance(obj);
				}
				bool isNullable = property2.EdmProperty.Type.AsCollection().ElementType().IsNullable;
				this.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(property, obj, property2.PrimitiveOrComplexCollectionItemType, new Action<object, object>(property2.AddValueToBackingICollectionInstance), isNullable);
			}
			else if ((type2 = Nullable.GetUnderlyingType(property2.NullablePropertyType) ?? property2.NullablePropertyType) != null && type2.IsEnum())
			{
				ODataEnumValue odataEnumValue = property.Value as ODataEnumValue;
				object obj2 = EnumValueMaterializationPolicy.MaterializeODataEnumValue(type2, odataEnumValue);
				property2.SetValue(instance, obj2, property.Name, false);
			}
			else
			{
				this.MaterializePrimitiveDataValue(property2.NullablePropertyType, property);
				property2.SetValue(instance, property.GetMaterializedValue(), property.Name, true);
			}
			if (!this.MaterializerContext.Context.DisableInstanceAnnotationMaterialization)
			{
				this.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(property, type.ElementType, instance);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00029104 File Offset: 0x00027304
		internal void MaterializeDataValues(ClientTypeAnnotation actualType, IEnumerable<ODataProperty> values, UndeclaredPropertyBehavior undeclaredPropertyBehavior)
		{
			foreach (ODataProperty odataProperty in values)
			{
				if (!(odataProperty.Value is ODataStreamReferenceValue))
				{
					string name = odataProperty.Name;
					ClientPropertyAnnotation property = actualType.GetProperty(name, undeclaredPropertyBehavior);
					if (property != null)
					{
						if (ClientTypeUtil.TypeOrElementTypeIsEntity(property.PropertyType))
						{
							throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidEntityType(property.EntityCollectionItemType ?? property.PropertyType));
						}
						if (property.IsKnownType)
						{
							this.MaterializePrimitiveDataValue(property.NullablePropertyType, odataProperty);
						}
					}
				}
			}
		}

		// Token: 0x0400061C RID: 1564
		private CollectionValueMaterializationPolicy collectionValueMaterializationPolicy;

		// Token: 0x0400061D RID: 1565
		private InstanceAnnotationMaterializationPolicy instanceAnnotationMaterializationPolicy;

		// Token: 0x0400061E RID: 1566
		private SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter;
	}
}
