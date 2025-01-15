using System;

namespace NLog.Internal
{
	// Token: 0x02000117 RID: 279
	internal class FactoryHelper
	{
		// Token: 0x06000EAA RID: 3754 RVA: 0x0002461B File Offset: 0x0002281B
		private FactoryHelper()
		{
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00024624 File Offset: 0x00022824
		internal static object CreateInstance(Type t)
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(t);
			}
			catch (MissingMethodException ex)
			{
				throw new NLogConfigurationException("Cannot access the constructor of type: " + t.FullName + ". Is the required permission granted?", ex);
			}
			return obj;
		}
	}
}
