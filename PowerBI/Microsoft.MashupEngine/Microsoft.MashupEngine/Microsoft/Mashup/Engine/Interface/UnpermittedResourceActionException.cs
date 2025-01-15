using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class UnpermittedResourceActionException : ResourceSecurityException
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00005CDF File Offset: 0x00003EDF
		public UnpermittedResourceActionException(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00005D7A File Offset: 0x00003F7A
		public UnpermittedResourceActionException(string dataSourceLocationOrigin, IResource origin, string dataSourceLocation, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(dataSourceLocationOrigin, origin, dataSourceLocation, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00005F19 File Offset: 0x00004119
		public UnpermittedResourceActionException(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected UnpermittedResourceActionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400029D RID: 669
		public static string ReasonString = "ActionNotAllowed";
	}
}
