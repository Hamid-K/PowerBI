using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x0200008D RID: 141
	internal class ModelValidatorCache : IModelValidatorCache
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00009FEB File Offset: 0x000081EB
		public ModelValidatorCache(Lazy<IEnumerable<ModelValidatorProvider>> validatorProviders)
		{
			this._validatorProviders = validatorProviders;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A008 File Offset: 0x00008208
		public ModelValidator[] GetValidators(ModelMetadata metadata)
		{
			ModelValidator[] array;
			if (!this._validatorCache.TryGetValue(metadata.CacheKey, out array))
			{
				array = metadata.GetValidators(this._validatorProviders.Value).ToArray<ModelValidator>();
				this._validatorCache.TryAdd(metadata.CacheKey, array);
			}
			return array;
		}

		// Token: 0x040000C7 RID: 199
		private ConcurrentDictionary<EfficientTypePropertyKey<Type, string>, ModelValidator[]> _validatorCache = new ConcurrentDictionary<EfficientTypePropertyKey<Type, string>, ModelValidator[]>();

		// Token: 0x040000C8 RID: 200
		private Lazy<IEnumerable<ModelValidatorProvider>> _validatorProviders;
	}
}
