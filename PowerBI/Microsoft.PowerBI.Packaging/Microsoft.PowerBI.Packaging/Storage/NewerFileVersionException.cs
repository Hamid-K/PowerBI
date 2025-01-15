using System;
using System.Globalization;
using System.IO;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class NewerFileVersionException : FileFormatException
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00007130 File Offset: 0x00005330
		public NewerFileVersionException(string version)
			: base(NewerFileVersionException.GetMessage(version))
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000713E File Offset: 0x0000533E
		private static string GetMessage(string version)
		{
			return string.Format(CultureInfo.InvariantCulture, "'{0}' is newer than the product version.", version);
		}
	}
}
