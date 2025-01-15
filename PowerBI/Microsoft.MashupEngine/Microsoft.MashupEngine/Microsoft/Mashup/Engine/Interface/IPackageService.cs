using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000049 RID: 73
	public interface IPackageService
	{
		// Token: 0x06000144 RID: 324
		bool TryImport(string path, IEngineHost host, out IValue value);
	}
}
