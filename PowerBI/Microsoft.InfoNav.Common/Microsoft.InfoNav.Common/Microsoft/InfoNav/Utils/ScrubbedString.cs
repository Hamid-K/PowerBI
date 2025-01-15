using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x02000035 RID: 53
	[DataContract]
	[Serializable]
	public class ScrubbedString : IContainsPrivateInformation, IContainsTelemetryMarkup
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00007250 File Offset: 0x00005450
		public ScrubbedString(string plainString)
		{
			this.m_plainString = plainString;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000725F File Offset: 0x0000545F
		public override string ToString()
		{
			return this.m_plainString;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007267 File Offset: 0x00005467
		public string ToPrivateString()
		{
			return this.m_plainString.MarkAsPrivate();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007274 File Offset: 0x00005474
		public string ToInternalString()
		{
			return this.m_plainString.MarkAsInternal();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007281 File Offset: 0x00005481
		public string ToOriginalString()
		{
			return this.m_plainString;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007289 File Offset: 0x00005489
		public string ToCustomerContentString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsCustomerContent();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000729B File Offset: 0x0000549B
		public string ToEUIIString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsEUII();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000072AD File Offset: 0x000054AD
		public string ToEUPIString()
		{
			return string.Empty;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000072B4 File Offset: 0x000054B4
		public string ToIPString()
		{
			return this.m_plainString.RemovePrivateAndInternalMarkup().MarkAsIPAddress();
		}

		// Token: 0x04000075 RID: 117
		[DataMember]
		private string m_plainString;
	}
}
