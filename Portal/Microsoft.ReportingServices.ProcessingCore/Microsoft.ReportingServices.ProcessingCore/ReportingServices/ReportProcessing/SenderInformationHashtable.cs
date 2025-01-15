using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006BC RID: 1724
	[Serializable]
	internal sealed class SenderInformationHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CE3 RID: 23779 RVA: 0x0017A551 File Offset: 0x00178751
		internal SenderInformationHashtable()
		{
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x0017A559 File Offset: 0x00178759
		internal SenderInformationHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002098 RID: 8344
		internal SenderInformation this[int key]
		{
			get
			{
				return (SenderInformation)this.m_hashtable[key];
			}
			set
			{
				this.m_hashtable[key] = value;
			}
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x0017A58E File Offset: 0x0017878E
		internal void Add(int key, SenderInformation sender)
		{
			this.m_hashtable.Add(key, sender);
		}
	}
}
