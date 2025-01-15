using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E27 RID: 7719
	public static class RemoteServiceFactories
	{
		// Token: 0x17002ECE RID: 11982
		// (get) Token: 0x0600BE29 RID: 48681 RVA: 0x0026777C File Offset: 0x0026597C
		// (set) Token: 0x0600BE2A RID: 48682 RVA: 0x002677BC File Offset: 0x002659BC
		public static IRemoteServiceFactory[] Factories
		{
			get
			{
				object obj = RemoteServiceFactories.syncRoot;
				IRemoteServiceFactory[] array;
				lock (obj)
				{
					array = RemoteServiceFactories.factories;
				}
				return array;
			}
			set
			{
				object obj = RemoteServiceFactories.syncRoot;
				lock (obj)
				{
					RemoteServiceFactories.factories = value;
					RemoteServiceFactories.version = Guid.NewGuid();
				}
			}
		}

		// Token: 0x0600BE2B RID: 48683 RVA: 0x00267808 File Offset: 0x00265A08
		public static IRemoteServiceFactory[] GetFactories(out Guid factoriesVersion)
		{
			object obj = RemoteServiceFactories.syncRoot;
			IRemoteServiceFactory[] array;
			lock (obj)
			{
				factoriesVersion = RemoteServiceFactories.version;
				array = RemoteServiceFactories.factories;
			}
			return array;
		}

		// Token: 0x040060E2 RID: 24802
		private static readonly object syncRoot = new object();

		// Token: 0x040060E3 RID: 24803
		private static IRemoteServiceFactory[] factories;

		// Token: 0x040060E4 RID: 24804
		private static Guid version;
	}
}
