using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C1 RID: 961
	[DataContract(Name = "LookupTableRequest", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class LookupTableRequest
	{
		// Token: 0x060021CC RID: 8652 RVA: 0x00068466 File Offset: 0x00066666
		internal LookupTableRequest(VersionRanges ranges, HashSet<string> interestedApps, GenerationNumber generationNumber)
		{
			this.Ranges = ranges;
			this.InterestedApps = interestedApps;
			this.GenerationNumber = generationNumber;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x00068483 File Offset: 0x00066683
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x0006848B File Offset: 0x0006668B
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

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00068494 File Offset: 0x00066694
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x0006849C File Offset: 0x0006669C
		[DataMember]
		internal HashSet<string> InterestedApps
		{
			get
			{
				return this.m_interestedApps;
			}
			private set
			{
				this.m_interestedApps = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000684A5 File Offset: 0x000666A5
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x000684AD File Offset: 0x000666AD
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

		// Token: 0x060021D3 RID: 8659 RVA: 0x000684B8 File Offset: 0x000666B8
		public override string ToString()
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[] { this.m_ranges, this.m_generationNumber });
			if (this.m_interestedApps != null)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				foreach (string text2 in this.m_interestedApps)
				{
					stringBuilder.Append(" ").Append(text2);
				}
				text += stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x04001581 RID: 5505
		private VersionRanges m_ranges;

		// Token: 0x04001582 RID: 5506
		private HashSet<string> m_interestedApps;

		// Token: 0x04001583 RID: 5507
		private GenerationNumber m_generationNumber;
	}
}
