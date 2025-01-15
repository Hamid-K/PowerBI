using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000171 RID: 369
	internal static class Anchor
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00021380 File Offset: 0x0001F580
		static Anchor()
		{
			CurrentProcess.Initialize();
			Anchor.sm_tweaks = new Tweaks();
			Tracing.Initialize();
			if (Library.Mode == CloudPlatformExecutionMode.HostEnvironment)
			{
				ExtendedEnvironment.Initialize();
				ExtendedDiagnostics.Initialize();
			}
			ExceptionUtility.Initialize();
			Anchor.s_periodicGarbageCollector = new PeriodicGarbageCollector();
			Anchor.s_periodicGarbageCollector.Start();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000213D0 File Offset: 0x0001F5D0
		private static void PreventCloudPlatformFromMultipleLoad()
		{
			Anchor.s_anchorAssembly = typeof(Anchor).Assembly;
			Anchor.s_anchorAssemblyName = Anchor.s_anchorAssembly.GetName().Name;
			Anchor.s_anchorAssemblyLocation = Anchor.s_anchorAssembly.Location;
			AppDomain.CurrentDomain.AssemblyLoad += delegate(object sender, AssemblyLoadEventArgs args)
			{
				Assembly loadedAssembly = args.LoadedAssembly;
				string location = loadedAssembly.Location;
				if (loadedAssembly.GetName().Name.Equals(Anchor.s_anchorAssemblyName, StringComparison.Ordinal) && !location.Equals(Anchor.s_anchorAssemblyLocation, StringComparison.OrdinalIgnoreCase))
				{
					ExtendedEnvironment.FailSlow(sender, new CloudPlatformAssemblyCannotBeLoadedTwiceException(Anchor.s_anchorAssemblyLocation, location));
				}
			};
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002143D File Offset: 0x0001F63D
		public static void Initialize()
		{
			if (Library.Mode == CloudPlatformExecutionMode.HostEnvironment)
			{
				Anchor.NativeMethods.SetErrorMode(1);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002144E File Offset: 0x0001F64E
		public static Tweaks Tweaks
		{
			get
			{
				return Anchor.sm_tweaks;
			}
		}

		// Token: 0x040003AA RID: 938
		private static Assembly s_anchorAssembly;

		// Token: 0x040003AB RID: 939
		private static string s_anchorAssemblyName;

		// Token: 0x040003AC RID: 940
		private static string s_anchorAssemblyLocation;

		// Token: 0x040003AD RID: 941
		private static Tweaks sm_tweaks;

		// Token: 0x040003AE RID: 942
		private static PeriodicGarbageCollector s_periodicGarbageCollector;

		// Token: 0x0200063C RID: 1596
		private static class NativeMethods
		{
			// Token: 0x06002CDE RID: 11486
			[DllImport("kernel32.dll")]
			internal static extern int SetErrorMode(int newMode);
		}
	}
}
