using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002096 RID: 8342
	public static class XmlSerializationAssembly
	{
		// Token: 0x0600CC2A RID: 52266 RVA: 0x00289D24 File Offset: 0x00287F24
		public static void EnsureLoadRelative()
		{
			if (!XmlSerializationAssembly.isEventRegistered)
			{
				object obj = XmlSerializationAssembly.objectLock;
				lock (obj)
				{
					if (!XmlSerializationAssembly.isEventRegistered)
					{
						AppDomain.CurrentDomain.AssemblyResolve += XmlSerializationAssembly.OnResolveAssembly;
						XmlSerializationAssembly.isEventRegistered = true;
					}
				}
			}
		}

		// Token: 0x0600CC2B RID: 52267 RVA: 0x00289D90 File Offset: 0x00287F90
		private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
		{
			if (!XmlSerializationAssembly.resolveAttempted && args.Name.StartsWith(XmlSerializationAssembly.serializationAssemblyName, StringComparison.OrdinalIgnoreCase))
			{
				XmlSerializationAssembly.resolveAttempted = true;
				return XmlSerializationAssembly.LoadSerializationAssembly();
			}
			return null;
		}

		// Token: 0x0600CC2C RID: 52268 RVA: 0x00289DBC File Offset: 0x00287FBC
		private static Assembly LoadSerializationAssembly()
		{
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				AssemblyName assemblyName = new AssemblyName(XmlSerializationAssembly.serializationAssemblyName);
				assemblyName.CodeBase = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), XmlSerializationAssembly.serializationAssemblyName + ".dll");
				assemblyName.SetPublicKeyToken(executingAssembly.GetName().GetPublicKeyToken());
				return Assembly.Load(assemblyName);
			}
			catch (SecurityException)
			{
			}
			catch (FileLoadException)
			{
			}
			catch (FileNotFoundException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return null;
		}

		// Token: 0x0400677B RID: 26491
		private static readonly object objectLock = new object();

		// Token: 0x0400677C RID: 26492
		private static readonly string serializationAssemblyName = Assembly.GetExecutingAssembly().GetName().Name + ".XmlSerializers";

		// Token: 0x0400677D RID: 26493
		private static volatile bool isEventRegistered;

		// Token: 0x0400677E RID: 26494
		private static bool resolveAttempted;
	}
}
