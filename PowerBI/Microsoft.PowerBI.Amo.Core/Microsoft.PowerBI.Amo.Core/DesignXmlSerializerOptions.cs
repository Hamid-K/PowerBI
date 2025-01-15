using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000005 RID: 5
	[Flags]
	internal enum DesignXmlSerializerOptions
	{
		// Token: 0x0400003E RID: 62
		Default = 0,
		// Token: 0x0400003F RID: 63
		IncludeForeignComponent = 1,
		// Token: 0x04000040 RID: 64
		DontForceSiteName = 2,
		// Token: 0x04000041 RID: 65
		DontWriteStartDocument = 4,
		// Token: 0x04000042 RID: 66
		IgnoreDesignTimeProperties = 8,
		// Token: 0x04000043 RID: 67
		IgnoreRuntimeProperties = 16,
		// Token: 0x04000044 RID: 68
		DontWriteSiteName = 32,
		// Token: 0x04000045 RID: 69
		DoNotUseNamespaces = 64,
		// Token: 0x04000046 RID: 70
		BinaryXml = 128
	}
}
