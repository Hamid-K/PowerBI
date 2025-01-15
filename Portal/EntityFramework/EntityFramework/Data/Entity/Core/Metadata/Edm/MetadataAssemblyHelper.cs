using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.SchemaObjectModel;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050F RID: 1295
	internal static class MetadataAssemblyHelper
	{
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000D41BC File Offset: 0x000D23BC
		internal static Assembly SafeLoadReferencedAssembly(AssemblyName assemblyName)
		{
			Assembly assembly = null;
			try
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			return assembly;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000D41F8 File Offset: 0x000D23F8
		private static bool ComputeShouldFilterAssembly(Assembly assembly)
		{
			return MetadataAssemblyHelper.ShouldFilterAssembly(new AssemblyName(assembly.FullName));
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x000D420A File Offset: 0x000D240A
		internal static bool ShouldFilterAssembly(Assembly assembly)
		{
			return MetadataAssemblyHelper._filterAssemblyCacheByAssembly.Evaluate(assembly);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x000D4217 File Offset: 0x000D2417
		private static bool ShouldFilterAssembly(AssemblyName assemblyName)
		{
			return MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper._ecmaPublicKeyToken) || MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper._msPublicKeyToken);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000D4240 File Offset: 0x000D2440
		private static bool ArePublicKeyTokensEqual(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000D4270 File Offset: 0x000D2470
		internal static IEnumerable<Assembly> GetNonSystemReferencedAssemblies(Assembly assembly)
		{
			foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
			{
				if (!MetadataAssemblyHelper.ShouldFilterAssembly(assemblyName))
				{
					Assembly assembly2 = MetadataAssemblyHelper.SafeLoadReferencedAssembly(assemblyName);
					if (assembly2 != null)
					{
						yield return assembly2;
					}
				}
			}
			AssemblyName[] array = null;
			yield break;
		}

		// Token: 0x04001642 RID: 5698
		private const string EcmaPublicKey = "b77a5c561934e089";

		// Token: 0x04001643 RID: 5699
		private const string MicrosoftPublicKey = "b03f5f7f11d50a3a";

		// Token: 0x04001644 RID: 5700
		private static readonly byte[] _ecmaPublicKeyToken = ScalarType.ConvertToByteArray("b77a5c561934e089");

		// Token: 0x04001645 RID: 5701
		private static readonly byte[] _msPublicKeyToken = ScalarType.ConvertToByteArray("b03f5f7f11d50a3a");

		// Token: 0x04001646 RID: 5702
		private static readonly Memoizer<Assembly, bool> _filterAssemblyCacheByAssembly = new Memoizer<Assembly, bool>(new Func<Assembly, bool>(MetadataAssemblyHelper.ComputeShouldFilterAssembly), EqualityComparer<Assembly>.Default);
	}
}
