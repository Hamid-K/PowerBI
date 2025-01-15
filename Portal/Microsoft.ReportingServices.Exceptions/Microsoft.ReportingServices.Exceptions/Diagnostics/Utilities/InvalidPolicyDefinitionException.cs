using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	internal sealed class InvalidPolicyDefinitionException : ReportCatalogException
	{
		// Token: 0x06000212 RID: 530 RVA: 0x000049C1 File Offset: 0x00002BC1
		public InvalidPolicyDefinitionException(string principalName)
			: base(ErrorCode.rsInvalidPolicyDefinition, ErrorStringsWrapper.rsInvalidPolicyDefinition(principalName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000049D8 File Offset: 0x00002BD8
		private InvalidPolicyDefinitionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
