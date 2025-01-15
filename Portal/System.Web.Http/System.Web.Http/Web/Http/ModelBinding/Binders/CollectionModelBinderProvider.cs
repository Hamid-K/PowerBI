using System;
using System.Collections.Generic;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200006B RID: 107
	public sealed class CollectionModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000841D File Offset: 0x0000661D
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return CollectionModelBinderUtil.GetGenericBinder(typeof(ICollection<>), typeof(List<>), typeof(CollectionModelBinder<>), modelType);
		}
	}
}
