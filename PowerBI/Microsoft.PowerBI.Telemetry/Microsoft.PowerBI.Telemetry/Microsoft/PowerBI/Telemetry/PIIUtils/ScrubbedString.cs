using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x0200003A RID: 58
	[DataContract]
	[Serializable]
	public class ScrubbedString : IContainsPrivateInformation, IContainsTelemetryMarkup
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00004A03 File Offset: 0x00002C03
		public ScrubbedString(string plainString)
		{
			this.m_plainString = plainString;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004A12 File Offset: 0x00002C12
		public override string ToString()
		{
			return this.m_plainString;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004A1A File Offset: 0x00002C1A
		public string ToPrivateString()
		{
			return this.m_plainString.MarkAsPrivate();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004A27 File Offset: 0x00002C27
		public string ToInternalString()
		{
			return this.m_plainString.MarkAsInternal();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004A34 File Offset: 0x00002C34
		public string ToOriginalString()
		{
			return this.m_plainString;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004A3C File Offset: 0x00002C3C
		public string ToCustomerContentString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsCustomerContent();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004A4E File Offset: 0x00002C4E
		public string ToEUIIString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsEUII();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004A60 File Offset: 0x00002C60
		public string ToEUPIString()
		{
			return string.Empty;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004A67 File Offset: 0x00002C67
		public string ToIPString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsIPAddress();
		}

		// Token: 0x040000D8 RID: 216
		[DataMember]
		private string m_plainString;
	}
}
