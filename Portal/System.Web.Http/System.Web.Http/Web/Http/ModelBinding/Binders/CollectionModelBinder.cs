using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200006C RID: 108
	public class CollectionModelBinder<TElement> : IModelBinder
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00008444 File Offset: 0x00006644
		private static List<TElement> BindComplexCollection(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			string text = ModelBindingHelper.CreatePropertyModelName(bindingContext.ModelName, "index");
			IEnumerable<string> indexNamesFromValueProviderResult = CollectionModelBinderUtil.GetIndexNamesFromValueProviderResult(bindingContext.ValueProvider.GetValue(text));
			return CollectionModelBinder<TElement>.BindComplexCollectionFromIndexes(actionContext, bindingContext, indexNamesFromValueProviderResult);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000847C File Offset: 0x0000667C
		internal static List<TElement> BindComplexCollectionFromIndexes(HttpActionContext actionContext, ModelBindingContext bindingContext, IEnumerable<string> indexNames)
		{
			bool flag;
			if (indexNames != null)
			{
				flag = true;
			}
			else
			{
				flag = false;
				indexNames = CollectionModelBinderUtil.GetZeroBasedIndexes();
			}
			List<TElement> list = new List<TElement>();
			foreach (string text in indexNames)
			{
				string text2 = ModelBindingHelper.CreateIndexModelName(bindingContext.ModelName, text);
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TElement)),
					ModelName = text2
				};
				bool flag2 = false;
				object obj = null;
				if (actionContext.Bind(modelBindingContext))
				{
					flag2 = true;
					obj = modelBindingContext.Model;
					bindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				}
				if (!flag2 && !flag)
				{
					break;
				}
				list.Add(ModelBindingHelper.CastOrDefault<TElement>(obj));
			}
			return list;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008560 File Offset: 0x00006760
		public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return false;
			}
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			List<TElement> list = ((value != null) ? CollectionModelBinder<TElement>.BindSimpleCollection(actionContext, bindingContext, value.RawValue, value.Culture) : CollectionModelBinder<TElement>.BindComplexCollection(actionContext, bindingContext));
			return this.CreateOrReplaceCollection(actionContext, bindingContext, list);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000085C4 File Offset: 0x000067C4
		internal static List<TElement> BindSimpleCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, object rawValue, CultureInfo culture)
		{
			if (rawValue == null)
			{
				return null;
			}
			List<TElement> list = new List<TElement>();
			foreach (object obj in ModelBindingHelper.RawValueToObjectArray(rawValue))
			{
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(null, typeof(TElement)),
					ModelName = bindingContext.ModelName,
					ValueProvider = new CompositeValueProvider
					{
						new ElementalValueProvider(bindingContext.ModelName, obj, culture),
						bindingContext.ValueProvider
					}
				};
				object obj2 = null;
				if (actionContext.Bind(modelBindingContext))
				{
					obj2 = modelBindingContext.Model;
					bindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				}
				list.Add(ModelBindingHelper.CastOrDefault<TElement>(obj2));
			}
			return list;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008690 File Offset: 0x00006890
		protected virtual bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			CollectionModelBinderUtil.CreateOrReplaceCollection<TElement>(bindingContext, newCollection, () => new List<TElement>());
			return true;
		}
	}
}
