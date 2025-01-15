using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003B8 RID: 952
	internal class ClientPartitionTable : PartitionTable
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x00067CCA File Offset: 0x00065ECA
		public ClientPartitionTable()
			: base(new object())
		{
			this.m_lookupVersionRanges = new VersionRanges();
			this.m_generationNumber = GenerationNumber.Zero;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x00067CED File Offset: 0x00065EED
		protected override GenerationNumber GenerationNumber
		{
			get
			{
				return this.m_generationNumber;
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00067CF8 File Offset: 0x00065EF8
		public IEnumerable<LookupTableEntry> UpdateFromTransfer(LookupTableTransfer transfer)
		{
			GenerationNumber generationNumber = transfer.GenerationNumber;
			int num = generationNumber.CompareTo(this.m_generationNumber);
			if (num < 0)
			{
				return null;
			}
			if (num > 0)
			{
				base.Clear();
				this.m_lookupVersionRanges = new VersionRanges();
				this.m_generationNumber = generationNumber;
			}
			if (!this.m_lookupVersionRanges.Add(transfer.Ranges))
			{
				return null;
			}
			foreach (LookupTableEntry lookupTableEntry in transfer.Entries)
			{
				base.UpdateEntry(lookupTableEntry);
			}
			return base.Entries;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00067D9C File Offset: 0x00065F9C
		public LookupTableTransfer GetUpdates(LookupTableRequest request)
		{
			LookupTableTransfer lookupTableTransfer;
			lock (base.LockObject)
			{
				List<LookupTableEntry> updatedEntries = base.GetUpdatedEntries(request);
				request.Ranges.Add(this.m_lookupVersionRanges);
				lookupTableTransfer = new LookupTableTransfer(updatedEntries, request.Ranges, this.m_generationNumber);
			}
			return lookupTableTransfer;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00067E00 File Offset: 0x00066000
		public LookupTableRequest CreateRequest(HashSet<string> interestedApps)
		{
			LookupTableRequest lookupTableRequest;
			lock (base.LockObject)
			{
				lookupTableRequest = new LookupTableRequest(this.m_lookupVersionRanges, interestedApps, this.m_generationNumber);
			}
			return lookupTableRequest;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00067E48 File Offset: 0x00066048
		public string ToShortString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}) generation: {1}", new object[] { this.m_lookupVersionRanges, this.m_generationNumber });
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00067E7E File Offset: 0x0006607E
		public override string ToString()
		{
			return this.ToShortString() + "\n" + base.ToString();
		}

		// Token: 0x0400156E RID: 5486
		private VersionRanges m_lookupVersionRanges;

		// Token: 0x0400156F RID: 5487
		private GenerationNumber m_generationNumber;
	}
}
