using System;
using System.Globalization;
using System.IO;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class NewerPackagePartException : FileFormatException
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00007150 File Offset: 0x00005350
		public NewerPackagePartException(string packagePartName, string packagePartVersion, string supportedVersion)
			: base(NewerPackagePartException.GetMessage(packagePartName, packagePartVersion, supportedVersion))
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007160 File Offset: 0x00005360
		private static string GetMessage(string packagePartName, string packagePartVersion, string supportedVersion)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} is newer than the supported version ({1}) for {2} package part", packagePartVersion, supportedVersion, packagePartName);
		}
	}
}
