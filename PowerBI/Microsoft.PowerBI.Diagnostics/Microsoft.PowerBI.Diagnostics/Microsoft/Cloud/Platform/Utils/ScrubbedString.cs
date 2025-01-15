using System;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200000E RID: 14
	[DataContract]
	[Serializable]
	public class ScrubbedString : IContainsPrivateInformation, IContainsTelemetryMarkup
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000022EB File Offset: 0x000004EB
		public ScrubbedString(string plainString)
		{
			this.m_plainString = plainString;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022FA File Offset: 0x000004FA
		public override string ToString()
		{
			return this.m_plainString;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002302 File Offset: 0x00000502
		public string ToPrivateString()
		{
			return this.m_plainString.MarkAsPrivate();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000230F File Offset: 0x0000050F
		public string ToInternalString()
		{
			return this.m_plainString.MarkAsInternal();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000231C File Offset: 0x0000051C
		public string ToOriginalString()
		{
			return this.m_plainString;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002324 File Offset: 0x00000524
		public string ToCustomerContentString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsCustomerContent();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002336 File Offset: 0x00000536
		public string ToEUIIString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsEUII();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002348 File Offset: 0x00000548
		public string ToEUPIString()
		{
			return string.Empty;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000234F File Offset: 0x0000054F
		public string ToIPString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsIPAddress();
		}

		// Token: 0x0400003A RID: 58
		[DataMember]
		private string m_plainString;
	}
}
