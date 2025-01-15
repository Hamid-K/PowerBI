using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001970 RID: 6512
	public sealed class MashupEngines
	{
		// Token: 0x17002A33 RID: 10803
		// (get) Token: 0x0600A542 RID: 42306 RVA: 0x00223348 File Offset: 0x00221548
		public static IEngine Version1
		{
			get
			{
				if (MashupEngines.version1 == null)
				{
					object obj = MashupEngines.obj;
					lock (obj)
					{
						if (MashupEngines.version1 == null)
						{
							AssemblyName assemblyName = new AssemblyName("Microsoft.MashupEngine");
							assemblyName.SetPublicKeyToken(Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken());
							assemblyName.Version = Assembly.GetExecutingAssembly().GetName().Version;
							MashupEngines.version1 = (IEngine)Activator.CreateInstance(Assembly.Load(assemblyName).GetType("Microsoft.Mashup.Engine1.Engine"));
						}
					}
				}
				return MashupEngines.version1;
			}
		}

		// Token: 0x17002A34 RID: 10804
		// (get) Token: 0x0600A543 RID: 42307 RVA: 0x002233E8 File Offset: 0x002215E8
		public static string Name
		{
			get
			{
				return "Microsoft Mashup Engine";
			}
		}

		// Token: 0x17002A35 RID: 10805
		// (get) Token: 0x0600A544 RID: 42308 RVA: 0x002233EF File Offset: 0x002215EF
		public static string Version1Version
		{
			get
			{
				if (MashupEngines.version1Version == null)
				{
					MashupEngines.version1Version = FileVersionInfo.GetVersionInfo(MashupEngines.Version1.GetType().Assembly.Location).FileVersion;
				}
				return MashupEngines.version1Version;
			}
		}

		// Token: 0x0400560C RID: 22028
		private static object obj = new object();

		// Token: 0x0400560D RID: 22029
		private static IEngine version1;

		// Token: 0x0400560E RID: 22030
		private static string version1Version;
	}
}
