using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200004D RID: 77
	[Obsolete("Deprecated!")]
	public sealed class SignatureHashAlgorithm
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00011E3F File Offset: 0x0001003F
		public string Name
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011E4B File Offset: 0x0001004B
		public static SignatureHashAlgorithm CreateSha256()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011E57 File Offset: 0x00010057
		public static SignatureHashAlgorithm CreateSha384()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011E63 File Offset: 0x00010063
		public static SignatureHashAlgorithm CreateSha512()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}
	}
}
