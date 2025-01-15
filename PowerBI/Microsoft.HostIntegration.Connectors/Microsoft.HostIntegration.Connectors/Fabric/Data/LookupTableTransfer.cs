using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C4 RID: 964
	[DataContract(Name = "LookupTableTransfer", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class LookupTableTransfer
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x0006883B File Offset: 0x00066A3B
		internal LookupTableTransfer(List<LookupTableEntry> entries, VersionRanges ranges, GenerationNumber generationNumber)
		{
			this.Entries = entries;
			this.Ranges = ranges;
			this.GenerationNumber = generationNumber;
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00068858 File Offset: 0x00066A58
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x00068860 File Offset: 0x00066A60
		[DataMember]
		internal List<LookupTableEntry> Entries
		{
			get
			{
				return this.m_entries;
			}
			private set
			{
				this.m_entries = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x00068869 File Offset: 0x00066A69
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x00068871 File Offset: 0x00066A71
		[DataMember]
		internal VersionRanges Ranges
		{
			get
			{
				return this.m_ranges;
			}
			private set
			{
				this.m_ranges = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0006887A File Offset: 0x00066A7A
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x00068882 File Offset: 0x00066A82
		[DataMember]
		internal GenerationNumber GenerationNumber
		{
			get
			{
				return this.m_generationNumber;
			}
			private set
			{
				this.m_generationNumber = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x0006888B File Offset: 0x00066A8B
		internal int Count
		{
			get
			{
				return this.m_entries.Count;
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00068898 File Offset: 0x00066A98
		internal Message CreateMessage()
		{
			return Message.CreateMessage(MessageVersion.Default, "http://schemas.microsoft.com/2008/casmsg/LookupTableTransfer", this);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000688AC File Offset: 0x00066AAC
		internal string ToShortString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}) generation: {1}", new object[] { this.m_ranges, this.m_generationNumber });
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000688E4 File Offset: 0x00066AE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.m_entries.Count << 7);
			stringBuilder.Append(this.ToShortString()).AppendLine();
			foreach (LookupTableEntry lookupTableEntry in this.m_entries)
			{
				stringBuilder.Append(lookupTableEntry.ToString()).AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400158E RID: 5518
		private List<LookupTableEntry> m_entries;

		// Token: 0x0400158F RID: 5519
		private VersionRanges m_ranges;

		// Token: 0x04001590 RID: 5520
		private GenerationNumber m_generationNumber;
	}
}
