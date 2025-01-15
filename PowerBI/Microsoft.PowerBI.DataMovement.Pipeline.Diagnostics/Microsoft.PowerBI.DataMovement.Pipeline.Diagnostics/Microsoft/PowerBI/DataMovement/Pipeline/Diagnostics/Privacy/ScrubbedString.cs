using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000D4 RID: 212
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	[Serializable]
	public class ScrubbedString : IContainsPrivateInformation
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x000457FB File Offset: 0x000439FB
		public ScrubbedString(string plainString)
		{
			this.m_plainString = plainString;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004580A File Offset: 0x00043A0A
		public override string ToString()
		{
			return this.m_plainString;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00045812 File Offset: 0x00043A12
		public string ToPrivateString()
		{
			return this.m_plainString.MarkAsCustomerContent();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004581F File Offset: 0x00043A1F
		public string ToOriginalString()
		{
			return this.m_plainString;
		}

		// Token: 0x0400034C RID: 844
		[DataMember]
		private string m_plainString;
	}
}
