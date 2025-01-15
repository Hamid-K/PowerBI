using System;
using System.Collections.Generic;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000071 RID: 113
	public sealed class DictionaryModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00008871 File Offset: 0x00006A71
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return CollectionModelBinderUtil.GetGenericBinder(typeof(IDictionary<, >), typeof(Dictionary<, >), typeof(DictionaryModelBinder<, >), modelType);
		}
	}
}
