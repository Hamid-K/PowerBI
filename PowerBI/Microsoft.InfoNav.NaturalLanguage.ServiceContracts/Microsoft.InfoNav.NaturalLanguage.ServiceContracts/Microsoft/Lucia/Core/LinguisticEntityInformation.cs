using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000AF RID: 175
	[ImmutableObject(true)]
	public sealed class LinguisticEntityInformation
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00006C9C File Offset: 0x00004E9C
		internal LinguisticEntityInformation(string primaryTerm)
		{
			this.PrimaryTerm = primaryTerm;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00006CAB File Offset: 0x00004EAB
		public string PrimaryTerm { get; }
	}
}
