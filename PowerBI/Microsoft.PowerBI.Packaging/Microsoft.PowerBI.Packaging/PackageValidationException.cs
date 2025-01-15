using System;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class PackageValidationException : Exception
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public PackageValidationException(Exception innerException)
			: base("Validation failed. See innerException for details: " + innerException.Message, innerException)
		{
		}
	}
}
