using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000025 RID: 37
	[ComVisible(true)]
	[Guid("8B919575-4516-457E-8753-6647523CF64F")]
	public struct MEngineSettings
	{
		// Token: 0x040000BB RID: 187
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool AllowFileAccess;

		// Token: 0x040000BC RID: 188
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool AllowWindowsCredentials;

		// Token: 0x040000BD RID: 189
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool EnableHtmlExtension;

		// Token: 0x040000BE RID: 190
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool ContainerInheritsHostJob;

		// Token: 0x040000BF RID: 191
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool UseManagedOracleProvider;

		// Token: 0x040000C0 RID: 192
		[MarshalAs(UnmanagedType.VariantBool)]
		public bool UseMicrosoftDataSqlClient;

		// Token: 0x040000C1 RID: 193
		[MarshalAs(UnmanagedType.I4)]
		public int OAuthContainerPoolSize;
	}
}
