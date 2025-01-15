using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000067 RID: 103
	public sealed class CompositeModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00008193 File Offset: 0x00006393
		public CompositeModelBinderProvider()
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000819B File Offset: 0x0000639B
		public CompositeModelBinderProvider(IEnumerable<ModelBinderProvider> providers)
		{
			if (providers == null)
			{
				throw Error.ArgumentNull("providers");
			}
			this._providers = providers.ToArray<ModelBinderProvider>();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000081BD File Offset: 0x000063BD
		public IEnumerable<ModelBinderProvider> Providers
		{
			get
			{
				return this._providers;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000081C8 File Offset: 0x000063C8
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			IEnumerable<ModelBinderProvider> providers = this._providers;
			return new CompositeModelBinder(from provider in providers ?? configuration.Services.GetModelBinderProviders()
				let binder = provider.GetBinder(configuration, modelType)
				where binder != null
				select binder);
		}

		// Token: 0x040000A0 RID: 160
		private ModelBinderProvider[] _providers;
	}
}
