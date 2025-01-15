using System;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000516 RID: 1302
	internal class ObjectItemNoOpAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x06004014 RID: 16404 RVA: 0x000D5D94 File Offset: 0x000D3F94
		internal ObjectItemNoOpAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData)
			: base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000D5DA3 File Offset: 0x000D3FA3
		internal override void Load()
		{
			if (!base.SessionData.KnownAssemblies.Contains(base.SourceAssembly, base.SessionData.ObjectItemAssemblyLoaderFactory, base.SessionData.EdmItemCollection))
			{
				this.AddToKnownAssemblies();
			}
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x000D5DD9 File Offset: 0x000D3FD9
		protected override void AddToAssembliesLoaded()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x000D5DE0 File Offset: 0x000D3FE0
		protected override void LoadTypesFromAssembly()
		{
			throw new NotImplementedException();
		}
	}
}
