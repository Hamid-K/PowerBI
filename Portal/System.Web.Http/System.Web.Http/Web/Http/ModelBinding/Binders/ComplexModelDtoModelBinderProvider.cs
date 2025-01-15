using System;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200006F RID: 111
	public sealed class ComplexModelDtoModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x000087F4 File Offset: 0x000069F4
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			return ComplexModelDtoModelBinderProvider._underlyingProvider.GetBinder(configuration, modelType);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008802 File Offset: 0x00006A02
		private static SimpleModelBinderProvider GetUnderlyingProvider()
		{
			return new SimpleModelBinderProvider(typeof(ComplexModelDto), new ComplexModelDtoModelBinder())
			{
				SuppressPrefixCheck = true
			};
		}

		// Token: 0x040000A5 RID: 165
		private static readonly SimpleModelBinderProvider _underlyingProvider = ComplexModelDtoModelBinderProvider.GetUnderlyingProvider();
	}
}
