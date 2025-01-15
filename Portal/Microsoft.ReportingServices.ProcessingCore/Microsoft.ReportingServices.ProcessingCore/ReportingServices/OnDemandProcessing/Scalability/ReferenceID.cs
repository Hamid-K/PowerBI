using System;
using System.Globalization;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000850 RID: 2128
	public struct ReferenceID
	{
		// Token: 0x060076B6 RID: 30390 RVA: 0x001EB983 File Offset: 0x001E9B83
		internal ReferenceID(long value)
		{
			this.m_value = value;
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x001EB98C File Offset: 0x001E9B8C
		internal ReferenceID(bool hasMultiPart, bool isTemporary, int partitionId)
		{
			this.m_value = 0L;
			this.HasMultiPart = hasMultiPart;
			this.IsTemporary = isTemporary;
			this.PartitionID = partitionId;
		}

		// Token: 0x170027BE RID: 10174
		// (get) Token: 0x060076B8 RID: 30392 RVA: 0x001EB9AB File Offset: 0x001E9BAB
		// (set) Token: 0x060076B9 RID: 30393 RVA: 0x001EB9B3 File Offset: 0x001E9BB3
		internal long Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170027BF RID: 10175
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x001EB9BC File Offset: 0x001E9BBC
		// (set) Token: 0x060076BB RID: 30395 RVA: 0x001EB9C8 File Offset: 0x001E9BC8
		internal bool HasMultiPart
		{
			get
			{
				return this.m_value < 0L;
			}
			set
			{
				ulong num = (ulong)this.m_value;
				if (value)
				{
					num |= 9223372036854775808UL;
				}
				else
				{
					num &= 9223372036854775807UL;
				}
				this.m_value = (long)num;
			}
		}

		// Token: 0x170027C0 RID: 10176
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x001EBA00 File Offset: 0x001E9C00
		// (set) Token: 0x060076BD RID: 30397 RVA: 0x001EBA16 File Offset: 0x001E9C16
		internal bool IsTemporary
		{
			get
			{
				return (this.m_value & 4611686018427387904L) != 0L;
			}
			set
			{
				if (value)
				{
					this.m_value |= 4611686018427387904L;
					return;
				}
				this.m_value &= -4611686018427387905L;
			}
		}

		// Token: 0x170027C1 RID: 10177
		// (get) Token: 0x060076BE RID: 30398 RVA: 0x001EBA48 File Offset: 0x001E9C48
		// (set) Token: 0x060076BF RID: 30399 RVA: 0x001EBA54 File Offset: 0x001E9C54
		internal int PartitionID
		{
			get
			{
				return (int)(this.m_value & (long)((ulong)(-1)));
			}
			set
			{
				long num = (long)value;
				num &= (long)((ulong)(-1));
				this.m_value &= -4294967296L;
				this.m_value |= num;
			}
		}

		// Token: 0x060076C0 RID: 30400 RVA: 0x001EBA8D File Offset: 0x001E9C8D
		public static bool operator ==(ReferenceID id1, ReferenceID id2)
		{
			return id1.m_value == id2.m_value;
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x001EBA9D File Offset: 0x001E9C9D
		public static bool operator !=(ReferenceID id1, ReferenceID id2)
		{
			return id1.m_value != id2.m_value;
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x001EBAB0 File Offset: 0x001E9CB0
		public static bool operator <(ReferenceID id1, ReferenceID id2)
		{
			return id1.m_value < id2.m_value;
		}

		// Token: 0x060076C3 RID: 30403 RVA: 0x001EBAC0 File Offset: 0x001E9CC0
		public static bool operator >(ReferenceID id1, ReferenceID id2)
		{
			return id1.m_value > id2.m_value;
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x001EBAD0 File Offset: 0x001E9CD0
		public static bool operator <=(ReferenceID id1, ReferenceID id2)
		{
			return id1.m_value <= id2.m_value;
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x001EBAE3 File Offset: 0x001E9CE3
		public static bool operator >=(ReferenceID id1, ReferenceID id2)
		{
			return id1.m_value >= id2.m_value;
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x001EBAF8 File Offset: 0x001E9CF8
		public override bool Equals(object obj)
		{
			return this.m_value == ((ReferenceID)obj).Value;
		}

		// Token: 0x060076C7 RID: 30407 RVA: 0x001EBB1B File Offset: 0x001E9D1B
		public override int GetHashCode()
		{
			return (int)this.m_value;
		}

		// Token: 0x060076C8 RID: 30408 RVA: 0x001EBB24 File Offset: 0x001E9D24
		public override string ToString()
		{
			return this.m_value.ToString("x", CultureInfo.InvariantCulture);
		}

		// Token: 0x04003C13 RID: 15379
		private long m_value;

		// Token: 0x04003C14 RID: 15380
		private const long IsTemporaryMask = 4611686018427387904L;

		// Token: 0x04003C15 RID: 15381
		private const ulong HasMultiPartMask = 9223372036854775808UL;

		// Token: 0x04003C16 RID: 15382
		private const long PartitionIDMask = 4294967295L;

		// Token: 0x04003C17 RID: 15383
		public const long MinimumValidTempID = -9223372036854775808L;

		// Token: 0x04003C18 RID: 15384
		public const long MaximumValidOffset = 72057594037927935L;

		// Token: 0x04003C19 RID: 15385
		public const int SizeInBytes = 16;
	}
}
