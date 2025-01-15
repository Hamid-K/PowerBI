using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006BD RID: 1725
	[Serializable]
	internal sealed class ReceiverInformationHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CE8 RID: 23784 RVA: 0x0017A5A2 File Offset: 0x001787A2
		internal ReceiverInformationHashtable()
		{
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x0017A5AA File Offset: 0x001787AA
		internal ReceiverInformationHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002099 RID: 8345
		internal ReceiverInformation this[int key]
		{
			get
			{
				return (ReceiverInformation)this.m_hashtable[key];
			}
			set
			{
				this.m_hashtable[key] = value;
			}
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x0017A5DF File Offset: 0x001787DF
		internal void Add(int key, ReceiverInformation receiver)
		{
			this.m_hashtable.Add(key, receiver);
		}
	}
}
