using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003EB RID: 1003
	public static class AssemblyResourceUtils
	{
		// Token: 0x060016D3 RID: 5843 RVA: 0x00045DF4 File Offset: 0x00043FF4
		public static string LoadResourceFromAssembly(Assembly assembly, string resourceName)
		{
			string text;
			using (Stream stream = AssemblyResourceUtils.LoadResourceStreamFromAssembly(assembly, resourceName))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00045E4C File Offset: 0x0004404C
		public static byte[] LoadResourceBytesFromAssembly(Assembly assembly, string resourceName)
		{
			byte[] array;
			using (Stream stream = AssemblyResourceUtils.LoadResourceStreamFromAssembly(assembly, resourceName))
			{
				array = stream.RepeatReadAndAllocate((int)stream.Length);
			}
			return array;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00045E8C File Offset: 0x0004408C
		public static Stream LoadResourceStreamFromAssembly(Assembly assembly, string resourceName)
		{
			Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
			if (manifestResourceStream == null)
			{
				throw new MissingManifestResourceException(FormattableString.Invariant(FormattableStringFactory.Create("Could not load resource \"{0}\"", new object[] { resourceName })));
			}
			return manifestResourceStream;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00045EC4 File Offset: 0x000440C4
		public static bool TryLoadResourceFromAssembly(Assembly assembly, string resourceName, out string resourceContent)
		{
			resourceContent = null;
			bool flag;
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName))
			{
				if (manifestResourceStream == null)
				{
					flag = false;
				}
				else
				{
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						resourceContent = streamReader.ReadToEnd();
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00045F28 File Offset: 0x00044128
		public static T LoadJsonResourceFromAssembly<T>(Assembly assembly, string resourceName)
		{
			return JsonConvertUtils.Deserialize<T>(AssemblyResourceUtils.LoadResourceStreamFromAssembly(assembly, resourceName));
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00045F38 File Offset: 0x00044138
		public static IEnumerable<string> EnumerateResources(Assembly assembly, string prefix = null, string suffix = null)
		{
			return from n in assembly.GetManifestResourceNames()
				where (prefix == null || n.StartsWith(prefix, StringComparison.Ordinal)) && (suffix == null || n.EndsWith(suffix, StringComparison.Ordinal))
				select n;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00045F70 File Offset: 0x00044170
		public static bool ResourceExists(Assembly assembly, string resourceName)
		{
			return assembly.GetManifestResourceInfo(resourceName) != null;
		}
	}
}
