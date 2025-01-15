using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000F7 RID: 247
	internal class InstanceAnnotationMaterializationPolicy
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x000271EC File Offset: 0x000253EC
		internal InstanceAnnotationMaterializationPolicy(IODataMaterializerContext materializerContext)
		{
			this.MaterializerContext = materializerContext;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x000271FB File Offset: 0x000253FB
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00027203 File Offset: 0x00025403
		internal CollectionValueMaterializationPolicy CollectionValueMaterializationPolicy
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002720C File Offset: 0x0002540C
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00027214 File Offset: 0x00025414
		internal EnumValueMaterializationPolicy EnumValueMaterializationPolicy
		{
			get
			{
				return this.enumValueMaterializationPolicy;
			}
			set
			{
				this.enumValueMaterializationPolicy = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002721D File Offset: 0x0002541D
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00027225 File Offset: 0x00025425
		internal IODataMaterializerContext MaterializerContext { get; private set; }

		// Token: 0x06000A7E RID: 2686 RVA: 0x00027230 File Offset: 0x00025430
		internal void SetInstanceAnnotations(ODataResource entry, object entity)
		{
			if (entry != null)
			{
				IDictionary<string, object> dictionary = this.ConvertToClrInstanceAnnotations(entry.InstanceAnnotations);
				this.SetInstanceAnnotations(entity, dictionary);
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00027258 File Offset: 0x00025458
		internal void SetInstanceAnnotations(ODataProperty property, object instance)
		{
			if (property != null)
			{
				IDictionary<string, object> clrInstanceAnnotationsFromODataProperty = this.GetClrInstanceAnnotationsFromODataProperty(property);
				this.SetInstanceAnnotations(instance, clrInstanceAnnotationsFromODataProperty);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00027278 File Offset: 0x00025478
		internal void SetInstanceAnnotations(ODataProperty property, Type type, object declaringInstance)
		{
			if (property != null)
			{
				IDictionary<string, object> clrInstanceAnnotationsFromODataProperty = this.GetClrInstanceAnnotationsFromODataProperty(property);
				this.SetInstanceAnnotations(property.Name, clrInstanceAnnotationsFromODataProperty, type, declaringInstance);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000272A0 File Offset: 0x000254A0
		internal void SetInstanceAnnotations(string navigationPropertyName, ODataResource navigationProperty, Type type, object declaringInstance)
		{
			if (navigationProperty != null)
			{
				IDictionary<string, object> dictionary = this.ConvertToClrInstanceAnnotations(navigationProperty.InstanceAnnotations);
				this.SetInstanceAnnotations(navigationPropertyName, dictionary, type, declaringInstance);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000272C8 File Offset: 0x000254C8
		internal IDictionary<string, object> ConvertToClrInstanceAnnotations(ICollection<ODataInstanceAnnotation> instanceAnnotations)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			if (instanceAnnotations != null)
			{
				foreach (ODataInstanceAnnotation odataInstanceAnnotation in instanceAnnotations)
				{
					object obj;
					if (this.TryConvertToClrInstanceAnnotation(odataInstanceAnnotation, out obj))
					{
						dictionary.Add(odataInstanceAnnotation.Name, obj);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00027330 File Offset: 0x00025530
		private void SetInstanceAnnotations(object instance, IDictionary<string, object> instanceAnnotations)
		{
			if (instance != null)
			{
				this.MaterializerContext.Context.InstanceAnnotations.Remove(instance);
				if (instanceAnnotations != null && instanceAnnotations.Count > 0)
				{
					this.MaterializerContext.Context.InstanceAnnotations.Add(instance, instanceAnnotations);
				}
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00027370 File Offset: 0x00025570
		private void SetInstanceAnnotations(string propertyName, IDictionary<string, object> instanceAnnotations, Type type, object declaringInstance)
		{
			if (declaringInstance != null)
			{
				UndeclaredPropertyBehavior undeclaredPropertyBehavior = this.MaterializerContext.Context.UndeclaredPropertyBehavior;
				ClientEdmModel model = this.MaterializerContext.Model;
				ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(type));
				ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(propertyName, undeclaredPropertyBehavior);
				Tuple<object, MemberInfo> tuple = new Tuple<object, MemberInfo>(declaringInstance, property.PropertyInfo);
				this.SetInstanceAnnotations(tuple, instanceAnnotations);
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000273D0 File Offset: 0x000255D0
		private IDictionary<string, object> GetClrInstanceAnnotationsFromODataProperty(ODataProperty property)
		{
			IDictionary<string, object> dictionary = null;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			}
			if (property.InstanceAnnotations != null)
			{
				foreach (ODataInstanceAnnotation odataInstanceAnnotation in property.InstanceAnnotations)
				{
					object obj;
					if (this.TryConvertToClrInstanceAnnotation(odataInstanceAnnotation, out obj))
					{
						dictionary.Add(odataInstanceAnnotation.Name, obj);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00027448 File Offset: 0x00025648
		private bool TryConvertToClrInstanceAnnotation(ODataInstanceAnnotation instanceAnnotation, out object clrInstanceAnnotation)
		{
			clrInstanceAnnotation = null;
			ODataPrimitiveValue odataPrimitiveValue = instanceAnnotation.Value as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				clrInstanceAnnotation = odataPrimitiveValue.Value;
				return true;
			}
			ODataEnumValue odataEnumValue = instanceAnnotation.Value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				Type type = this.MaterializerContext.Context.ResolveTypeFromName(odataEnumValue.TypeName);
				if (type != null)
				{
					clrInstanceAnnotation = EnumValueMaterializationPolicy.MaterializeODataEnumValue(type, odataEnumValue);
					return true;
				}
				return false;
			}
			else
			{
				ODataCollectionValue odataCollectionValue = instanceAnnotation.Value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					IEdmModel edmModel = this.MaterializerContext.Context.Format.LoadServiceModel();
					IEdmTerm edmTerm = edmModel.FindTerm(instanceAnnotation.Name);
					if (edmTerm != null && edmTerm.Type != null && edmTerm.Type.Definition != null)
					{
						IEdmCollectionType edmCollectionType = edmTerm.Type.Definition as IEdmCollectionType;
						if (edmCollectionType != null)
						{
							IEdmTypeReference elementType = edmCollectionType.ElementType;
							PrimitiveType primitiveType;
							Type type2;
							if (PrimitiveType.TryGetPrimitiveType(elementType.FullName(), out primitiveType))
							{
								type2 = primitiveType.ClrType;
							}
							else
							{
								type2 = this.MaterializerContext.Context.ResolveTypeFromName(elementType.FullName());
							}
							if (type2 != null)
							{
								Type type3 = typeof(ICollection<>).MakeGenericType(new Type[] { type2 });
								ClientTypeAnnotation clientTypeAnnotation = this.MaterializerContext.ResolveTypeForMaterialization(type3, odataCollectionValue.TypeName);
								bool isNullable = edmCollectionType.ElementType.IsNullable;
								object obj = this.CollectionValueMaterializationPolicy.CreateCollectionInstance(clientTypeAnnotation.EdmTypeReference as IEdmCollectionTypeReference, clientTypeAnnotation.ElementType);
								this.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(odataCollectionValue.Items, odataCollectionValue.TypeName, obj, type2, ClientTypeUtil.GetAddToCollectionDelegate(type3), isNullable);
								clrInstanceAnnotation = obj;
								return true;
							}
						}
					}
					return false;
				}
				ODataNullValue odataNullValue = instanceAnnotation.Value as ODataNullValue;
				if (odataNullValue != null)
				{
					clrInstanceAnnotation = null;
					return true;
				}
				return false;
			}
		}

		// Token: 0x04000606 RID: 1542
		private CollectionValueMaterializationPolicy collectionValueMaterializationPolicy;

		// Token: 0x04000607 RID: 1543
		private EnumValueMaterializationPolicy enumValueMaterializationPolicy;
	}
}
