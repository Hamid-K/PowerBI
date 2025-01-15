using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000062 RID: 98
	public abstract class ModelBinderProvider
	{
		// Token: 0x060002AA RID: 682
		public abstract IModelBinder GetBinder(HttpConfiguration configuration, Type modelType);
	}
}
