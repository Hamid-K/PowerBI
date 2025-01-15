using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000059 RID: 89
	internal static class ModelBindingHelper
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00007604 File Offset: 0x00005804
		internal static TModel CastOrDefault<TModel>(object model)
		{
			if (!(model is TModel))
			{
				return default(TModel);
			}
			return (TModel)((object)model);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007629 File Offset: 0x00005829
		internal static string CreateIndexModelName(string parentName, int index)
		{
			return ModelBindingHelper.CreateIndexModelName(parentName, index.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000763D File Offset: 0x0000583D
		internal static string CreateIndexModelName(string parentName, string index)
		{
			if (parentName.Length != 0)
			{
				return parentName + "[" + index + "]";
			}
			return "[" + index + "]";
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007669 File Offset: 0x00005869
		internal static string CreatePropertyModelName(string prefix, string propertyName)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return propertyName ?? string.Empty;
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				return prefix ?? string.Empty;
			}
			return prefix + "." + propertyName;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000769D File Offset: 0x0000589D
		internal static string ConcatenateKeys(string prefix, string suffix)
		{
			if (string.IsNullOrEmpty(suffix))
			{
				return prefix;
			}
			if (!suffix.StartsWith("[", StringComparison.Ordinal))
			{
				return prefix + "." + suffix;
			}
			return prefix + suffix;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000076CC File Offset: 0x000058CC
		internal static IModelBinder GetPossibleBinderInstance(Type closedModelType, Type openModelType, Type openBinderType)
		{
			Type[] typeArgumentsIfMatch = TypeHelper.GetTypeArgumentsIfMatch(closedModelType, openModelType);
			if (typeArgumentsIfMatch == null)
			{
				return null;
			}
			return (IModelBinder)Activator.CreateInstance(openBinderType.MakeGenericType(typeArgumentsIfMatch));
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000076F8 File Offset: 0x000058F8
		internal static object[] RawValueToObjectArray(object rawValue)
		{
			if (rawValue is string)
			{
				return new object[] { rawValue };
			}
			object[] array = rawValue as object[];
			if (array != null)
			{
				return array;
			}
			IEnumerable enumerable = rawValue as IEnumerable;
			if (enumerable != null)
			{
				return enumerable.Cast<object>().ToArray<object>();
			}
			return new object[] { rawValue };
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007744 File Offset: 0x00005944
		internal static void ReplaceEmptyStringWithNull(ModelMetadata modelMetadata, ref object model)
		{
			if (model is string && modelMetadata.ConvertEmptyStringToNull && string.IsNullOrWhiteSpace(model as string))
			{
				model = null;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00007768 File Offset: 0x00005968
		internal static bool TryGetProviderFromAttribute(Type modelType, ModelBinderAttribute modelBinderAttribute, out ModelBinderProvider provider)
		{
			if (modelBinderAttribute.BinderType == null)
			{
				provider = null;
				return false;
			}
			if (typeof(ModelBinderProvider).IsAssignableFrom(modelBinderAttribute.BinderType))
			{
				provider = (ModelBinderProvider)Activator.CreateInstance(modelBinderAttribute.BinderType);
			}
			else
			{
				if (!typeof(IModelBinder).IsAssignableFrom(modelBinderAttribute.BinderType))
				{
					throw Error.InvalidOperation(SRResources.ModelBinderProviderCollection_InvalidBinderType, new object[]
					{
						modelBinderAttribute.BinderType,
						typeof(ModelBinderProvider),
						typeof(IModelBinder)
					});
				}
				IModelBinder modelBinder = (IModelBinder)Activator.CreateInstance(modelBinderAttribute.BinderType.IsGenericTypeDefinition ? modelBinderAttribute.BinderType.MakeGenericType(modelType.GetGenericArguments()) : modelBinderAttribute.BinderType);
				provider = new SimpleModelBinderProvider(modelType, modelBinder)
				{
					SuppressPrefixCheck = modelBinderAttribute.SuppressPrefixCheck
				};
			}
			return true;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000784C File Offset: 0x00005A4C
		internal static bool TryGetProviderFromAttributes(Type modelType, out ModelBinderProvider provider)
		{
			ModelBinderAttribute modelBinderAttribute = ModelBindingHelper.GetModelBinderAttribute(modelType);
			if (modelBinderAttribute == null)
			{
				provider = null;
				return false;
			}
			return ModelBindingHelper.TryGetProviderFromAttribute(modelType, modelBinderAttribute, out provider);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007870 File Offset: 0x00005A70
		private static ModelBinderAttribute GetModelBinderAttribute(Type modelType)
		{
			ModelBinderAttribute modelBinderAttribute;
			if (!ModelBindingHelper._modelBinderAttributeCache.TryGetValue(modelType, out modelBinderAttribute))
			{
				modelBinderAttribute = TypeDescriptorHelper.Get(modelType).GetAttributes().OfType<ModelBinderAttribute>()
					.FirstOrDefault<ModelBinderAttribute>();
				ModelBindingHelper._modelBinderAttributeCache.TryAdd(modelType, modelBinderAttribute);
			}
			return modelBinderAttribute;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000078B0 File Offset: 0x00005AB0
		internal static void ValidateBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw Error.ArgumentNull("bindingContext");
			}
			if (bindingContext.ModelMetadata == null)
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelMetadataCannotBeNull, new object[0]);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000078E0 File Offset: 0x00005AE0
		internal static void ValidateBindingContext(ModelBindingContext bindingContext, Type requiredType, bool allowNullModel)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			if (bindingContext.ModelType != requiredType)
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelTypeIsWrong, new object[] { bindingContext.ModelType, requiredType });
			}
			if (!allowNullModel && bindingContext.Model == null)
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelCannotBeNull, new object[] { requiredType });
			}
			if (bindingContext.Model != null && !requiredType.IsInstanceOfType(bindingContext.Model))
			{
				throw Error.Argument("bindingContext", SRResources.ModelBinderUtil_ModelInstanceIsWrong, new object[]
				{
					bindingContext.Model.GetType(),
					requiredType
				});
			}
		}

		// Token: 0x04000086 RID: 134
		private static readonly ConcurrentDictionary<Type, ModelBinderAttribute> _modelBinderAttributeCache = new ConcurrentDictionary<Type, ModelBinderAttribute>();
	}
}
