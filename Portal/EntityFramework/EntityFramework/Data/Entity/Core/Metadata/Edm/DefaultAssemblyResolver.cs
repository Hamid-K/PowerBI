using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200049F RID: 1183
	internal class DefaultAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x06003A1C RID: 14876 RVA: 0x000C025E File Offset: 0x000BE45E
		internal override bool TryResolveAssemblyReference(AssemblyName referenceName, out Assembly assembly)
		{
			assembly = this.ResolveAssembly(referenceName);
			return assembly != null;
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000C0271 File Offset: 0x000BE471
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			return DefaultAssemblyResolver.GetAllDiscoverableAssemblies();
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000C0278 File Offset: 0x000BE478
		internal virtual Assembly ResolveAssembly(AssemblyName referenceName)
		{
			Assembly assembly = null;
			foreach (Assembly assembly2 in DefaultAssemblyResolver.GetAlreadyLoadedNonSystemAssemblies())
			{
				if (AssemblyName.ReferenceMatchesDefinition(referenceName, new AssemblyName(assembly2.FullName)))
				{
					return assembly2;
				}
			}
			if (assembly == null)
			{
				assembly = MetadataAssemblyHelper.SafeLoadReferencedAssembly(referenceName);
				if (assembly != null)
				{
					return assembly;
				}
			}
			DefaultAssemblyResolver.TryFindWildcardAssemblyMatch(referenceName, out assembly);
			return assembly;
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000C0300 File Offset: 0x000BE500
		private static bool TryFindWildcardAssemblyMatch(AssemblyName referenceName, out Assembly assembly)
		{
			foreach (Assembly assembly2 in DefaultAssemblyResolver.GetAllDiscoverableAssemblies())
			{
				if (AssemblyName.ReferenceMatchesDefinition(referenceName, new AssemblyName(assembly2.FullName)))
				{
					assembly = assembly2;
					return true;
				}
			}
			assembly = null;
			return false;
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000C0368 File Offset: 0x000BE568
		private static IEnumerable<Assembly> GetAlreadyLoadedNonSystemAssemblies()
		{
			return from a in AppDomain.CurrentDomain.GetAssemblies()
				where a != null && !MetadataAssemblyHelper.ShouldFilterAssembly(a)
				select a;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000C0398 File Offset: 0x000BE598
		private static IEnumerable<Assembly> GetAllDiscoverableAssemblies()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			HashSet<Assembly> hashSet = new HashSet<Assembly>(DefaultAssemblyResolver.AssemblyComparer.Instance);
			foreach (Assembly assembly in DefaultAssemblyResolver.GetAlreadyLoadedNonSystemAssemblies())
			{
				hashSet.Add(assembly);
			}
			AspProxy aspProxy = new AspProxy();
			if (aspProxy.IsAspNetEnvironment())
			{
				if (aspProxy.HasBuildManagerType())
				{
					IEnumerable<Assembly> buildManagerReferencedAssemblies = aspProxy.GetBuildManagerReferencedAssemblies();
					if (buildManagerReferencedAssemblies != null)
					{
						foreach (Assembly assembly2 in buildManagerReferencedAssemblies)
						{
							if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly2))
							{
								hashSet.Add(assembly2);
							}
						}
					}
				}
				return hashSet.Where((Assembly a) => a != null);
			}
			if (entryAssembly == null)
			{
				return hashSet;
			}
			hashSet.Add(entryAssembly);
			foreach (Assembly assembly3 in MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(entryAssembly))
			{
				hashSet.Add(assembly3);
			}
			return hashSet;
		}

		// Token: 0x02000AC7 RID: 2759
		internal sealed class AssemblyComparer : IEqualityComparer<Assembly>
		{
			// Token: 0x060062FF RID: 25343 RVA: 0x001576E3 File Offset: 0x001558E3
			private AssemblyComparer()
			{
			}

			// Token: 0x170010CB RID: 4299
			// (get) Token: 0x06006300 RID: 25344 RVA: 0x001576EB File Offset: 0x001558EB
			public static DefaultAssemblyResolver.AssemblyComparer Instance
			{
				get
				{
					return DefaultAssemblyResolver.AssemblyComparer._instance;
				}
			}

			// Token: 0x06006301 RID: 25345 RVA: 0x001576F4 File Offset: 0x001558F4
			public bool Equals(Assembly x, Assembly y)
			{
				AssemblyName assemblyName = new AssemblyName(x.FullName);
				AssemblyName assemblyName2 = new AssemblyName(y.FullName);
				return x == y || (AssemblyName.ReferenceMatchesDefinition(assemblyName, assemblyName2) && AssemblyName.ReferenceMatchesDefinition(assemblyName2, assemblyName));
			}

			// Token: 0x06006302 RID: 25346 RVA: 0x00157731 File Offset: 0x00155931
			public int GetHashCode(Assembly assembly)
			{
				return assembly.FullName.GetHashCode();
			}

			// Token: 0x04002B9E RID: 11166
			private static readonly DefaultAssemblyResolver.AssemblyComparer _instance = new DefaultAssemblyResolver.AssemblyComparer();
		}
	}
}
