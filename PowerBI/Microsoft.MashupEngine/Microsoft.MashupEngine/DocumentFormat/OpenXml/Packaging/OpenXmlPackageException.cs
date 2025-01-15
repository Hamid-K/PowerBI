using System;
using System.Runtime.Serialization;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200211D RID: 8477
	[Serializable]
	internal sealed class OpenXmlPackageException : Exception
	{
		// Token: 0x0600D1CC RID: 53708 RVA: 0x00005F33 File Offset: 0x00004133
		public OpenXmlPackageException()
		{
		}

		// Token: 0x0600D1CD RID: 53709 RVA: 0x00002FDF File Offset: 0x000011DF
		public OpenXmlPackageException(string message)
			: base(message)
		{
		}

		// Token: 0x0600D1CE RID: 53710 RVA: 0x00005F45 File Offset: 0x00004145
		private OpenXmlPackageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600D1CF RID: 53711 RVA: 0x00005F3B File Offset: 0x0000413B
		public OpenXmlPackageException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
