using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000086 RID: 134
	public interface ILibraryService
	{
		// Token: 0x060001ED RID: 493
		string[] GetLoadedVersions(string[] moduleNames);

		// Token: 0x060001EE RID: 494
		string GetSource(string moduleName);

		// Token: 0x060001EF RID: 495
		string GetResourceString(string moduleName, string cultureName, string stringName);

		// Token: 0x060001F0 RID: 496
		byte[] GetResourceFile(string moduleName, string filename);

		// Token: 0x060001F1 RID: 497
		ModuleTrustLevel GetTrustLevel(string moduleName);

		// Token: 0x060001F2 RID: 498
		ISerializableValue GetLibraries(string culture);

		// Token: 0x060001F3 RID: 499
		ISerializableValue GetLibraryExports(string culture, string libraryIdentifier);

		// Token: 0x060001F4 RID: 500
		ISerializableValue GetLibraryDataSources(string culture, string libraryIdentifier);
	}
}
