using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	internal sealed class SystemResourcePackageWrongTypeException : SystemResourcePackageException
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0001089C File Offset: 0x0000EA9C
		public SystemResourcePackageWrongTypeException(string typeName)
			: base(ErrorCode.rsSystemResourcePackageWrongType, string.Format(ErrorStringsWrapper.rsSystemResourcePackageWrongType(typeName), typeName), null, typeName, Array.Empty<object>())
		{
		}
	}
}
