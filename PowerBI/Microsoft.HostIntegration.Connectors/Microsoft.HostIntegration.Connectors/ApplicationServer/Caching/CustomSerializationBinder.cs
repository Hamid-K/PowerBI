using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200038C RID: 908
	internal sealed class CustomSerializationBinder : SerializationBinder
	{
		// Token: 0x06002000 RID: 8192 RVA: 0x0006103C File Offset: 0x0005F23C
		public override Type BindToType(string assemblyName, string typeName)
		{
			string text = null;
			if (CustomSerializationBinder.CheckValidAssembly(assemblyName, out text))
			{
				return Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}, {1}", new object[] { typeName, text }));
			}
			throw new SerializationException(string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[] { assemblyName, typeName }));
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000610A0 File Offset: 0x0005F2A0
		private static bool CheckValidAssembly(string assemblyName, out string newname)
		{
			if (CustomSerializationBinder._assemblyMap.ContainsKey(assemblyName))
			{
				newname = assemblyName;
				return true;
			}
			if (string.Equals(assemblyName, "0", StringComparison.OrdinalIgnoreCase))
			{
				newname = assemblyName;
				return true;
			}
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (string.Equals(assembly.FullName, assemblyName, StringComparison.OrdinalIgnoreCase))
				{
					lock (CustomSerializationBinder._assemblyMap)
					{
						CustomSerializationBinder._assemblyMap[assemblyName] = true;
						newname = assemblyName;
						return true;
					}
				}
				if (string.Equals(assembly.GetName().Name, assemblyName2.Name, StringComparison.OrdinalIgnoreCase))
				{
					newname = assemblyName2.Name;
					return true;
				}
			}
			newname = null;
			return false;
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x00061174 File Offset: 0x0005F374
		public static CustomSerializationBinder Singleton
		{
			get
			{
				return CustomSerializationBinder._binder;
			}
		}

		// Token: 0x040012E6 RID: 4838
		private const string _defaultAssemblyName = "0";

		// Token: 0x040012E7 RID: 4839
		private static CustomSerializationBinder _binder = new CustomSerializationBinder();

		// Token: 0x040012E8 RID: 4840
		private static Dictionary<string, bool> _assemblyMap = new Dictionary<string, bool>();
	}
}
