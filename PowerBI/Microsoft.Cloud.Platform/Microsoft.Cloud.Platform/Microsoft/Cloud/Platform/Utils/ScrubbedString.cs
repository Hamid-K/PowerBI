using System;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B5 RID: 437
	[DataContract]
	[Serializable]
	public class ScrubbedString : IContainsPrivateInformation
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x00027597 File Offset: 0x00025797
		public ScrubbedString(string plainString)
		{
			this.m_plainString = plainString;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000275A6 File Offset: 0x000257A6
		public override string ToString()
		{
			return this.m_plainString;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000275AE File Offset: 0x000257AE
		public string ToPrivateString()
		{
			return this.m_plainString.MarkAsPrivate();
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000275BB File Offset: 0x000257BB
		public string ToInternalString()
		{
			return this.m_plainString.MarkAsInternal();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000275A6 File Offset: 0x000257A6
		public string ToOriginalString()
		{
			return this.m_plainString;
		}

		// Token: 0x0400046D RID: 1133
		[DataMember]
		private string m_plainString;
	}
}
