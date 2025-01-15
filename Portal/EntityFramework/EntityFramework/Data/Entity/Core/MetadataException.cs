using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	public sealed class MetadataException : EntityException
	{
		// Token: 0x0600232D RID: 9005 RVA: 0x0006362E File Offset: 0x0006182E
		public MetadataException()
			: base(Strings.Metadata_General_Error)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00063646 File Offset: 0x00061846
		public MetadataException(string message)
			: base(message)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x0006365A File Offset: 0x0006185A
		public MetadataException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0006366F File Offset: 0x0006186F
		private MetadataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000C0C RID: 3084
		private const int HResultMetadata = -2146232007;
	}
}
