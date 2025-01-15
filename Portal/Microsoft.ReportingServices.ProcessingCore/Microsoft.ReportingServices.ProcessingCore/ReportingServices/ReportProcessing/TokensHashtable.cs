using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006BA RID: 1722
	[Serializable]
	internal sealed class TokensHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CD9 RID: 23769 RVA: 0x0017A4C3 File Offset: 0x001786C3
		internal TokensHashtable()
		{
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x0017A4CB File Offset: 0x001786CB
		internal TokensHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002096 RID: 8342
		internal object this[int key]
		{
			get
			{
				return this.m_hashtable[key];
			}
			set
			{
				this.m_hashtable[key] = value;
			}
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x0017A4FB File Offset: 0x001786FB
		internal void Add(int tokenID, object tokenValue)
		{
			this.m_hashtable.Add(tokenID, tokenValue);
		}
	}
}
