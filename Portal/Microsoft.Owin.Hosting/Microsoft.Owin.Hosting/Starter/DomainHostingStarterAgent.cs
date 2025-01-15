using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using Microsoft.Owin.Hosting.Engine;
using Microsoft.Owin.Hosting.Services;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000011 RID: 17
	public class DomainHostingStarterAgent : MarshalByRefObject, ISponsor, IDisposable
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003498 File Offset: 0x00001698
		public virtual void ResolveAssembliesFromDirectory(string directory)
		{
			directory = Path.GetFullPath(directory);
			Dictionary<string, Assembly> cache = new Dictionary<string, Assembly>();
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object a, ResolveEventArgs b)
			{
				Assembly assembly;
				if (cache.TryGetValue(b.Name, out assembly))
				{
					return assembly;
				}
				string shortName = new AssemblyName(b.Name).Name;
				string path = Path.Combine(directory, shortName + ".dll");
				if (File.Exists(path))
				{
					assembly = Assembly.LoadFile(path);
				}
				cache[b.Name] = assembly;
				if (assembly != null)
				{
					cache[assembly.FullName] = assembly;
				}
				return assembly;
			};
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000034E4 File Offset: 0x000016E4
		public virtual void Start(StartOptions options)
		{
			StartContext context = new StartContext(options);
			IServiceProvider services = ServicesFactory.Create(context.Options.Settings);
			IHostingEngine engine = services.GetService<IHostingEngine>();
			this._runningApp = engine.Start(context);
			this._lease = (ILease)RemotingServices.GetLifetimeService(this);
			this._lease.Register(this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000353A File Offset: 0x0000173A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003549 File Offset: 0x00001749
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				this._disposed = true;
				this._lease.Unregister(this);
				this._runningApp.Dispose();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003574 File Offset: 0x00001774
		public virtual TimeSpan Renewal(ILease lease)
		{
			if (this._disposed)
			{
				return TimeSpan.Zero;
			}
			return TimeSpan.FromMinutes(5.0);
		}

		// Token: 0x0400002D RID: 45
		private ILease _lease;

		// Token: 0x0400002E RID: 46
		private bool _disposed;

		// Token: 0x0400002F RID: 47
		private IDisposable _runningApp;
	}
}
