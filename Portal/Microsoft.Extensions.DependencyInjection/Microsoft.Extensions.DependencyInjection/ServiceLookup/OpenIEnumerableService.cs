using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000018 RID: 24
	internal class OpenIEnumerableService : IGenericService
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000036AB File Offset: 0x000018AB
		public OpenIEnumerableService(ServiceTable table)
		{
			this._table = table;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000034F8 File Offset: 0x000016F8
		public ServiceLifetime Lifetime
		{
			get
			{
				return ServiceLifetime.Transient;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000036BC File Offset: 0x000018BC
		public IService GetService(Type closedServiceType)
		{
			Type type = closedServiceType.GetTypeInfo().GenericTypeArguments[0];
			ServiceEntry serviceEntry;
			if (!this._table.TryGetEntry(type, out serviceEntry))
			{
				return null;
			}
			return new ClosedIEnumerableService(type, serviceEntry);
		}

		// Token: 0x04000029 RID: 41
		private readonly ServiceTable _table;
	}
}
