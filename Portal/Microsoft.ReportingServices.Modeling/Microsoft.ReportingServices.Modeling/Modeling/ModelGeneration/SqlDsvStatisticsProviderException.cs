using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x02000105 RID: 261
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	[Serializable]
	public class SqlDsvStatisticsProviderException : ApplicationException
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x0002C04B File Offset: 0x0002A24B
		internal SqlDsvStatisticsProviderException(string message)
			: base(message)
		{
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002C054 File Offset: 0x0002A254
		internal SqlDsvStatisticsProviderException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002C05E File Offset: 0x0002A25E
		protected SqlDsvStatisticsProviderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
