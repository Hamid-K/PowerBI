using System;
using System.Collections.Generic;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000073 RID: 115
	public sealed class KeyValuePairModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000302 RID: 770 RVA: 0x000088C8 File Offset: 0x00006AC8
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return ModelBindingHelper.GetPossibleBinderInstance(modelType, typeof(KeyValuePair<, >), typeof(KeyValuePairModelBinder<, >));
		}
	}
}
