using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200007B RID: 123
	public sealed class TypeMatchModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00009344 File Offset: 0x00007544
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return TypeMatchModelBinderProvider._binder;
		}

		// Token: 0x040000AD RID: 173
		private static readonly TypeMatchModelBinder _binder = new TypeMatchModelBinder();
	}
}
