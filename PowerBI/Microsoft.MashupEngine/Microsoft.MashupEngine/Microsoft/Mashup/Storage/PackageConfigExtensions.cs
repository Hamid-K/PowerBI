using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200207C RID: 8316
	public static class PackageConfigExtensions
	{
		// Token: 0x0600CB83 RID: 52099 RVA: 0x002887B0 File Offset: 0x002869B0
		public static PackageConfig GetPackageConfig(this PackagePartStorage packagePartStorage)
		{
			string text;
			byte[] array;
			if (!packagePartStorage.TryGetPartContent(PackagePartType.Config, "Package.xml", out text, out array))
			{
				return new PackageConfig();
			}
			PackageConfig packageConfig;
			if (!Xml<PackageConfig>.TryDeserializeBytes(array, out packageConfig))
			{
				throw new StorageException(Strings.Package_Unrecognized_File_Format);
			}
			return packageConfig;
		}
	}
}
