using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Eventing.EventsKit
{
	// Token: 0x020003AA RID: 938
	public class GenerateEventsKitDllCode
	{
		// Token: 0x06001D07 RID: 7431 RVA: 0x0006E1E8 File Offset: 0x0006C3E8
		public static int GenerateEventsKitCode(string targetAssembly, string[] references, string outputDirectory)
		{
			AppDomain appDomain = AppDomain.CreateDomain("MsBuildTask" + Path.GetFileName(targetAssembly));
			string location = Assembly.GetExecutingAssembly().Location;
			((GenerateEventsKitAppDomain)appDomain.CreateInstanceFrom(location, typeof(GenerateEventsKitAppDomain).FullName).Unwrap()).Execute(targetAssembly, references, outputDirectory, false);
			AppDomain.Unload(appDomain);
			return 0;
		}
	}
}
