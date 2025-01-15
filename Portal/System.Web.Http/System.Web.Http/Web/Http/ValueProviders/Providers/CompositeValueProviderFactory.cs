using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000040 RID: 64
	public class CompositeValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public CompositeValueProviderFactory(IEnumerable<ValueProviderFactory> factories)
		{
			this._factories = factories.ToArray<ValueProviderFactory>();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005BDC File Offset: 0x00003DDC
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			return CompositeValueProviderFactory.GetValueProvider(actionContext, this._factories);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005BEC File Offset: 0x00003DEC
		internal static IValueProvider GetValueProvider(HttpActionContext actionContext, ValueProviderFactory[] factories)
		{
			if (factories.Length == 1)
			{
				IValueProvider valueProvider = factories[0].GetValueProvider(actionContext);
				if (valueProvider != null)
				{
					return valueProvider;
				}
			}
			List<IValueProvider> list = new List<IValueProvider>();
			for (int i = 0; i < factories.Length; i++)
			{
				IValueProvider valueProvider2 = factories[i].GetValueProvider(actionContext);
				if (valueProvider2 != null)
				{
					list.Add(valueProvider2);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new CompositeValueProvider(list);
		}

		// Token: 0x04000058 RID: 88
		private ValueProviderFactory[] _factories;
	}
}
