using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000076 RID: 118
	public sealed class MutableObjectModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00008FF8 File Offset: 0x000071F8
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (!MutableObjectModelBinder.CanBindType(modelType))
			{
				return null;
			}
			return new MutableObjectModelBinder();
		}
	}
}
