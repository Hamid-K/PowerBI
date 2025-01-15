using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000015 RID: 21
	public class HostingStarterFactory : IHostingStarterFactory
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003640 File Offset: 0x00001840
		public HostingStarterFactory(IHostingStarterActivator hostingStarterActivator)
		{
			this._hostingStarterActivator = hostingStarterActivator;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003650 File Offset: 0x00001850
		public virtual IHostingStarter Create(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return this._hostingStarterActivator.Activate(typeof(DirectHostingStarter));
			}
			if (name == "Domain")
			{
				return this._hostingStarterActivator.Activate(typeof(DomainHostingStarter));
			}
			Type hostingStarterType = (from attribute in HostingStarterFactory.LoadProvider(new string[] { name }).GetCustomAttributes(typeof(HostingStarterAttribute), false).OfType<HostingStarterAttribute>()
				select attribute.HostingStarterType).SingleOrDefault<Type>();
			return this._hostingStarterActivator.Activate(hostingStarterType);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000036F8 File Offset: 0x000018F8
		private static Assembly LoadProvider(params string[] names)
		{
			List<Exception> innerExceptions = new List<Exception>();
			foreach (string name in names)
			{
				try
				{
					return Assembly.Load(name);
				}
				catch (FileNotFoundException ex)
				{
					innerExceptions.Add(ex);
				}
				catch (FileLoadException ex2)
				{
					innerExceptions.Add(ex2);
				}
				catch (BadImageFormatException ex3)
				{
					innerExceptions.Add(ex3);
				}
			}
			throw new AggregateException(innerExceptions);
		}

		// Token: 0x04000033 RID: 51
		private readonly IHostingStarterActivator _hostingStarterActivator;
	}
}
