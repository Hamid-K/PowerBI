using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000049 RID: 73
	public abstract class AssociatedMetadataProvider<TModelMetadata> : ModelMetadataProvider where TModelMetadata : ModelMetadata
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x000062CD File Offset: 0x000044CD
		public sealed override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			if (containerType == null)
			{
				throw Error.ArgumentNull("containerType");
			}
			return this.GetMetadataForPropertiesImpl(container, containerType);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000062EB File Offset: 0x000044EB
		private IEnumerable<ModelMetadata> GetMetadataForPropertiesImpl(object container, Type containerType)
		{
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInformation = this.GetTypeInformation(containerType);
			foreach (KeyValuePair<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> keyValuePair in typeInformation.Properties)
			{
				AssociatedMetadataProvider<TModelMetadata>.PropertyInformation value = keyValuePair.Value;
				Func<object> func = null;
				if (container != null)
				{
					Func<object, object> propertyGetter = value.ValueAccessor;
					func = () => propertyGetter(container);
				}
				yield return this.CreateMetadataFromPrototype(value.Prototype, func);
			}
			Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation>.Enumerator enumerator = default(Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000630C File Offset: 0x0000450C
		public sealed override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			if (containerType == null)
			{
				throw Error.ArgumentNull("containerType");
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				throw Error.ArgumentNullOrEmpty("propertyName");
			}
			AssociatedMetadataProvider<TModelMetadata>.PropertyInformation propertyInformation;
			if (!this.GetTypeInformation(containerType).Properties.TryGetValue(propertyName, out propertyInformation))
			{
				throw Error.Argument("propertyName", SRResources.Common_PropertyNotFound, new object[] { containerType, propertyName });
			}
			return this.CreateMetadataFromPrototype(propertyInformation.Prototype, modelAccessor);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006388 File Offset: 0x00004588
		public sealed override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			TModelMetadata prototype = this.GetTypeInformation(modelType).Prototype;
			return this.CreateMetadataFromPrototype(prototype, modelAccessor);
		}

		// Token: 0x060001FA RID: 506
		protected abstract TModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName);

		// Token: 0x060001FB RID: 507
		protected abstract TModelMetadata CreateMetadataFromPrototype(TModelMetadata prototype, Func<object> modelAccessor);

		// Token: 0x060001FC RID: 508 RVA: 0x000063C4 File Offset: 0x000045C4
		private AssociatedMetadataProvider<TModelMetadata>.TypeInformation GetTypeInformation(Type type)
		{
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInformation;
			if (!this._typeInfoCache.TryGetValue(type, out typeInformation))
			{
				typeInformation = this.CreateTypeInformation(type);
				this._typeInfoCache.TryAdd(type, typeInformation);
			}
			return typeInformation;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000063F8 File Offset: 0x000045F8
		private AssociatedMetadataProvider<TModelMetadata>.TypeInformation CreateTypeInformation(Type type)
		{
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInformation = new AssociatedMetadataProvider<TModelMetadata>.TypeInformation();
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptorHelper.Get(type);
			typeInformation.TypeDescriptor = customTypeDescriptor;
			typeInformation.Prototype = this.CreateMetadataPrototype(AssociatedMetadataProvider<TModelMetadata>.AsAttributes(customTypeDescriptor.GetAttributes()), null, type, null);
			Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> dictionary = new Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation>();
			foreach (object obj in customTypeDescriptor.GetProperties())
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!dictionary.ContainsKey(propertyDescriptor.Name))
				{
					dictionary.Add(propertyDescriptor.Name, this.CreatePropertyInformation(type, propertyDescriptor));
				}
			}
			typeInformation.Properties = dictionary;
			return typeInformation;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000064B0 File Offset: 0x000046B0
		private AssociatedMetadataProvider<TModelMetadata>.PropertyInformation CreatePropertyInformation(Type containerType, PropertyDescriptor property)
		{
			return new AssociatedMetadataProvider<TModelMetadata>.PropertyInformation
			{
				ValueAccessor = AssociatedMetadataProvider<TModelMetadata>.CreatePropertyValueAccessor(property),
				Prototype = this.CreateMetadataPrototype(AssociatedMetadataProvider<TModelMetadata>.AsAttributes(property.Attributes), containerType, property.PropertyType, property.Name)
			};
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000064E7 File Offset: 0x000046E7
		private static IEnumerable<Attribute> AsAttributes(IEnumerable attributes)
		{
			foreach (object obj in attributes)
			{
				yield return obj as Attribute;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000064F8 File Offset: 0x000046F8
		private static Func<object, object> CreatePropertyValueAccessor(PropertyDescriptor property)
		{
			Type componentType = property.ComponentType;
			if (componentType.IsVisible)
			{
				string name = property.Name;
				PropertyInfo property2 = componentType.GetProperty(name, property.PropertyType);
				if (property2 != null && property2.CanRead)
				{
					MethodInfo getMethod = property2.GetGetMethod();
					if (getMethod != null)
					{
						return AssociatedMetadataProvider<TModelMetadata>.CreateDynamicValueAccessor(getMethod, componentType, name);
					}
				}
			}
			return (object container) => property.GetValue(container);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006580 File Offset: 0x00004780
		private static Func<object, object> CreateDynamicValueAccessor(MethodInfo getMethodInfo, Type declaringType, string propertyName)
		{
			Type returnType = getMethodInfo.ReturnType;
			DynamicMethod dynamicMethod = new DynamicMethod("Get" + propertyName + "From" + declaringType.Name, typeof(object), new Type[] { typeof(object) });
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			if (declaringType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox, declaringType);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Castclass, declaringType);
			}
			if (declaringType.IsValueType || !getMethodInfo.IsVirtual || getMethodInfo.IsFinal)
			{
				ilgenerator.Emit(OpCodes.Call, getMethodInfo);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Callvirt, getMethodInfo);
			}
			if (returnType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Box, returnType);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (Func<object, object>)dynamicMethod.CreateDelegate(typeof(Func<object, object>));
		}

		// Token: 0x0400006F RID: 111
		private ConcurrentDictionary<Type, AssociatedMetadataProvider<TModelMetadata>.TypeInformation> _typeInfoCache = new ConcurrentDictionary<Type, AssociatedMetadataProvider<TModelMetadata>.TypeInformation>();

		// Token: 0x0200019F RID: 415
		private class TypeInformation
		{
			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001AFEB File Offset: 0x000191EB
			// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0001AFF3 File Offset: 0x000191F3
			public ICustomTypeDescriptor TypeDescriptor { get; set; }

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001AFFC File Offset: 0x000191FC
			// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0001B004 File Offset: 0x00019204
			public TModelMetadata Prototype { get; set; }

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0001B00D File Offset: 0x0001920D
			// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0001B015 File Offset: 0x00019215
			public Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> Properties { get; set; }
		}

		// Token: 0x020001A0 RID: 416
		private class PropertyInformation
		{
			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001B01E File Offset: 0x0001921E
			// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0001B026 File Offset: 0x00019226
			public Func<object, object> ValueAccessor { get; set; }

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001B02F File Offset: 0x0001922F
			// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0001B037 File Offset: 0x00019237
			public TModelMetadata Prototype { get; set; }
		}
	}
}
