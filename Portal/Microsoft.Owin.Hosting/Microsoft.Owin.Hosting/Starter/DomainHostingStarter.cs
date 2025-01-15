using System;
using System.IO;
using System.Linq;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000010 RID: 16
	public class DomainHostingStarter : IHostingStarter
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003308 File Offset: 0x00001508
		public virtual IDisposable Start(StartOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			string directory;
			if (!options.Settings.TryGetValue("directory", out directory) || string.IsNullOrWhiteSpace(directory))
			{
				directory = Directory.GetCurrentDirectory();
				DirectoryInfo directoryInfo = new DirectoryInfo(directory);
				if ((from subDirInfo in directoryInfo.GetDirectories()
					where subDirInfo.Name.Equals("bin", StringComparison.OrdinalIgnoreCase)
					select subDirInfo).Count<DirectoryInfo>() == 0 && directoryInfo.Name.Equals("bin", StringComparison.OrdinalIgnoreCase))
				{
					directory = directoryInfo.Parent.FullName;
				}
			}
			AppDomainSetup info = new AppDomainSetup
			{
				ApplicationBase = directory,
				PrivateBinPath = "bin",
				PrivateBinPathProbe = "*",
				LoaderOptimization = LoaderOptimization.MultiDomainHost,
				ConfigurationFile = Path.Combine(directory, "web.config")
			};
			AppDomain domain = AppDomain.CreateDomain("OWIN", null, info);
			DomainHostingStarterAgent agent = DomainHostingStarter.CreateAgent(domain);
			agent.ResolveAssembliesFromDirectory(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
			agent.Start(options);
			return agent;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000340C File Offset: 0x0000160C
		private static DomainHostingStarterAgent CreateAgent(AppDomain domain)
		{
			DomainHostingStarterAgent domainHostingStarterAgent;
			try
			{
				domainHostingStarterAgent = (DomainHostingStarterAgent)domain.CreateInstanceAndUnwrap(typeof(DomainHostingStarterAgent).Assembly.FullName, typeof(DomainHostingStarterAgent).FullName);
			}
			catch
			{
				domainHostingStarterAgent = (DomainHostingStarterAgent)domain.CreateInstanceFromAndUnwrap(typeof(DomainHostingStarterAgent).Assembly.Location, typeof(DomainHostingStarterAgent).FullName);
			}
			return domainHostingStarterAgent;
		}
	}
}
