using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000167 RID: 359
	public class SingletonActivityType<T> : ActivityType where T : class, new()
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x00020338 File Offset: 0x0001E538
		protected SingletonActivityType(string shortName)
			: base(shortName)
		{
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00020341 File Offset: 0x0001E541
		public static T Instance
		{
			get
			{
				return SingletonActivityType<T>.s_instance;
			}
		}

		// Token: 0x04000386 RID: 902
		private static T s_instance = new T();
	}
}
