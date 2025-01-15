using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols.Configuration
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class InvalidConfigurationException : Exception
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000029E4 File Offset: 0x00000BE4
		public InvalidConfigurationException()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029EC File Offset: 0x00000BEC
		public InvalidConfigurationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029F5 File Offset: 0x00000BF5
		public InvalidConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029FF File Offset: 0x00000BFF
		protected InvalidConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
