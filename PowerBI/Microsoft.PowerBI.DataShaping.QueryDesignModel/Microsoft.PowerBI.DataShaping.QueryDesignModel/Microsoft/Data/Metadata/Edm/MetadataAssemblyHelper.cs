using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.EntityModel.SchemaObjectModel;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000099 RID: 153
	internal static class MetadataAssemblyHelper
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x00019874 File Offset: 0x00017A74
		internal static Assembly SafeLoadReferencedAssembly(AssemblyName assemblyName)
		{
			Assembly assembly = null;
			try
			{
				assembly = Assembly.Load(assemblyName.FullName);
			}
			catch (FileNotFoundException)
			{
			}
			return assembly;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000198A8 File Offset: 0x00017AA8
		private static bool ComputeShouldFilterAssembly(Assembly assembly)
		{
			return MetadataAssemblyHelper.ShouldFilterAssembly(new AssemblyName(assembly.FullName));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000198BA File Offset: 0x00017ABA
		internal static bool ShouldFilterAssembly(Assembly assembly)
		{
			return MetadataAssemblyHelper._filterAssemblyCacheByAssembly.Evaluate(assembly);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000198C7 File Offset: 0x00017AC7
		private static bool ShouldFilterAssembly(AssemblyName assemblyName)
		{
			return MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper.EcmaPublicKeyToken) || MetadataAssemblyHelper.ArePublicKeyTokensEqual(assemblyName.GetPublicKeyToken(), MetadataAssemblyHelper.MsPublicKeyToken);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000198F0 File Offset: 0x00017AF0
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

		// Token: 0x06000ABD RID: 2749 RVA: 0x00019920 File Offset: 0x00017B20
		internal static IEnumerable<Assembly> GetNonSystemReferencedAssemblies(Assembly assembly)
		{
			return new List<Assembly>();
		}

		// Token: 0x04000855 RID: 2133
		internal const string EcmaPublicKey = "b77a5c561934e089";

		// Token: 0x04000856 RID: 2134
		internal const string MicrosoftPublicKey = "b03f5f7f11d50a3a";

		// Token: 0x04000857 RID: 2135
		private static byte[] EcmaPublicKeyToken = ScalarType.ConvertToByteArray("b77a5c561934e089");

		// Token: 0x04000858 RID: 2136
		private static byte[] MsPublicKeyToken = ScalarType.ConvertToByteArray("b03f5f7f11d50a3a");

		// Token: 0x04000859 RID: 2137
		private static Memoizer<Assembly, bool> _filterAssemblyCacheByAssembly = new Memoizer<Assembly, bool>(new Func<Assembly, bool>(MetadataAssemblyHelper.ComputeShouldFilterAssembly), EqualityComparer<Assembly>.Default);
	}
}
