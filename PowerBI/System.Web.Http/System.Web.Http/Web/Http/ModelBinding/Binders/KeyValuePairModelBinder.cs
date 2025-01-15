using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000074 RID: 116
	public sealed class KeyValuePairModelBinder<TKey, TValue> : IModelBinder
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000088E4 File Offset: 0x00006AE4
		// (set) Token: 0x06000305 RID: 773 RVA: 0x000088EC File Offset: 0x00006AEC
		internal ModelMetadataProvider MetadataProvider { private get; set; }

		// Token: 0x06000306 RID: 774 RVA: 0x000088F8 File Offset: 0x00006AF8
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelMetadataProvider modelMetadataProvider = this.MetadataProvider ?? actionContext.GetMetadataProvider();
			ModelBindingHelper.ValidateBindingContext(bindingContext, typeof(KeyValuePair<TKey, TValue>), true);
			TKey tkey;
			bool flag = actionContext.TryBindStrongModel(bindingContext, "key", modelMetadataProvider, out tkey);
			TValue tvalue;
			bool flag2 = actionContext.TryBindStrongModel(bindingContext, "value", modelMetadataProvider, out tvalue);
			if (flag && flag2)
			{
				bindingContext.Model = new KeyValuePair<TKey, TValue>(tkey, tvalue);
			}
			return flag || flag2;
		}
	}
}
