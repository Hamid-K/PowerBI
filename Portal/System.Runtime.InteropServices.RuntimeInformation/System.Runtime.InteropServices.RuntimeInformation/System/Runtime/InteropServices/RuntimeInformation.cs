using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000005 RID: 5
	public static class RuntimeInformation
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021AA File Offset: 0x000003AA
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return OSPlatform.Windows == osPlatform;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B7 File Offset: 0x000003B7
		public static string OSDescription
		{
			get
			{
				if (RuntimeInformation.s_osDescription == null)
				{
					RuntimeInformation.s_osDescription = Interop.NtDll.RtlGetVersion();
				}
				return RuntimeInformation.s_osDescription;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021D0 File Offset: 0x000003D0
		public static Architecture OSArchitecture
		{
			get
			{
				object obj = RuntimeInformation.s_osLock;
				lock (obj)
				{
					if (RuntimeInformation.s_osArch == null)
					{
						Interop.mincore.SYSTEM_INFO system_INFO;
						Interop.mincore.GetNativeSystemInfo(out system_INFO);
						Interop.mincore.ProcessorArchitecture wProcessorArchitecture = (Interop.mincore.ProcessorArchitecture)system_INFO.wProcessorArchitecture;
						if (wProcessorArchitecture <= Interop.mincore.ProcessorArchitecture.Processor_Architecture_ARM)
						{
							if (wProcessorArchitecture != Interop.mincore.ProcessorArchitecture.Processor_Architecture_INTEL)
							{
								if (wProcessorArchitecture == Interop.mincore.ProcessorArchitecture.Processor_Architecture_ARM)
								{
									RuntimeInformation.s_osArch = new Architecture?(Architecture.Arm);
								}
							}
							else
							{
								RuntimeInformation.s_osArch = new Architecture?(Architecture.X86);
							}
						}
						else if (wProcessorArchitecture != Interop.mincore.ProcessorArchitecture.Processor_Architecture_AMD64)
						{
							if (wProcessorArchitecture == Interop.mincore.ProcessorArchitecture.Processor_Architecture_ARM64)
							{
								RuntimeInformation.s_osArch = new Architecture?(Architecture.Arm64);
							}
						}
						else
						{
							RuntimeInformation.s_osArch = new Architecture?(Architecture.X64);
						}
					}
				}
				return RuntimeInformation.s_osArch.Value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002278 File Offset: 0x00000478
		public static Architecture ProcessArchitecture
		{
			get
			{
				object obj = RuntimeInformation.s_processLock;
				lock (obj)
				{
					if (RuntimeInformation.s_processArch == null)
					{
						Interop.mincore.SYSTEM_INFO system_INFO;
						Interop.mincore.GetSystemInfo(out system_INFO);
						Interop.mincore.ProcessorArchitecture wProcessorArchitecture = (Interop.mincore.ProcessorArchitecture)system_INFO.wProcessorArchitecture;
						if (wProcessorArchitecture <= Interop.mincore.ProcessorArchitecture.Processor_Architecture_ARM)
						{
							if (wProcessorArchitecture != Interop.mincore.ProcessorArchitecture.Processor_Architecture_INTEL)
							{
								if (wProcessorArchitecture == Interop.mincore.ProcessorArchitecture.Processor_Architecture_ARM)
								{
									RuntimeInformation.s_processArch = new Architecture?(Architecture.Arm);
								}
							}
							else
							{
								RuntimeInformation.s_processArch = new Architecture?(Architecture.X86);
							}
						}
						else if (wProcessorArchitecture != Interop.mincore.ProcessorArchitecture.Processor_Architecture_AMD64)
						{
							if (wProcessorArchitecture == Interop.mincore.ProcessorArchitecture.Processor_Architecture_ARM64)
							{
								RuntimeInformation.s_processArch = new Architecture?(Architecture.Arm64);
							}
						}
						else
						{
							RuntimeInformation.s_processArch = new Architecture?(Architecture.X64);
						}
					}
				}
				return RuntimeInformation.s_processArch.Value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002320 File Offset: 0x00000520
		public static string FrameworkDescription
		{
			get
			{
				if (RuntimeInformation.s_frameworkDescription == null)
				{
					AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)typeof(object).GetTypeInfo().Assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute));
					RuntimeInformation.s_frameworkDescription = string.Format("{0} {1}", new object[] { ".NET Framework", assemblyFileVersionAttribute.Version });
				}
				return RuntimeInformation.s_frameworkDescription;
			}
		}

		// Token: 0x04000003 RID: 3
		private static string s_osDescription = null;

		// Token: 0x04000004 RID: 4
		private static object s_osLock = new object();

		// Token: 0x04000005 RID: 5
		private static object s_processLock = new object();

		// Token: 0x04000006 RID: 6
		private static Architecture? s_osArch = null;

		// Token: 0x04000007 RID: 7
		private static Architecture? s_processArch = null;

		// Token: 0x04000008 RID: 8
		private const string FrameworkName = ".NET Framework";

		// Token: 0x04000009 RID: 9
		private static string s_frameworkDescription;
	}
}
