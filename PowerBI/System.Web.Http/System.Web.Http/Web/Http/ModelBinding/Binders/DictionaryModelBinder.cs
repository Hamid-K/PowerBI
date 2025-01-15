using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000072 RID: 114
	public class DictionaryModelBinder<TKey, TValue> : CollectionModelBinder<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00008897 File Offset: 0x00006A97
		protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<KeyValuePair<TKey, TValue>> newCollection)
		{
			CollectionModelBinderUtil.CreateOrReplaceDictionary<TKey, TValue>(bindingContext, newCollection, () => new Dictionary<TKey, TValue>());
			return true;
		}
	}
}
