using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace System.Web.Http
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class FromUriAttribute : ModelBinderAttribute
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00004E78 File Offset: 0x00003078
		public override IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			foreach (ValueProviderFactory valueProviderFactory in base.GetValueProviderFactories(configuration))
			{
				if (valueProviderFactory is IUriValueProviderFactory)
				{
					yield return valueProviderFactory;
				}
			}
			IEnumerator<ValueProviderFactory> enumerator = null;
			yield break;
			yield break;
		}
	}
}
