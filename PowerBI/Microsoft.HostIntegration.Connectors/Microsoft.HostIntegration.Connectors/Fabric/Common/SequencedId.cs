using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003EE RID: 1006
	internal class SequencedId : UniqueId
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x0006C6C3 File Offset: 0x0006A8C3
		internal SequencedId(string id, long first64Bits, long second64Bits, int offset)
			: base(id)
		{
			this.m_stringID = id;
			this.m_first64Bits = first64Bits;
			this.m_second64Bits = second64Bits;
			this.m_offset = offset;
			ReleaseAssert.IsTrue(!base.IsGuid);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0006C6F7 File Offset: 0x0006A8F7
		internal SequencedId(SequencedId copyFrom, int offset)
			: this(SequencedId.CopyWithOffset(copyFrom, offset), copyFrom.m_first64Bits, copyFrom.m_second64Bits, offset)
		{
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0006C714 File Offset: 0x0006A914
		private static string CopyWithOffset(SequencedId copyFrom, int offset)
		{
			string stringID = copyFrom.m_stringID;
			ReleaseAssert.IsTrue(stringID.Trim().Length == stringID.Length);
			ReleaseAssert.IsTrue(stringID.Length > 49 && stringID[45] == ";id="[0] && stringID[46] == ";id="[1] && stringID[47] == ";id="[2] && stringID[48] == ";id="[3]);
			return stringID.Substring(0, 49) + offset.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x0006C7C0 File Offset: 0x0006A9C0
		public long First64Bits
		{
			get
			{
				return this.m_first64Bits;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x0006C7C8 File Offset: 0x0006A9C8
		public long Second64Bits
		{
			get
			{
				return this.m_second64Bits;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x0006C7D0 File Offset: 0x0006A9D0
		public int SequenceOffset
		{
			get
			{
				return this.m_offset;
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0006C7D8 File Offset: 0x0006A9D8
		internal bool IsPartOfSequence(long first64Bits, long second64Bits)
		{
			return this.m_first64Bits == first64Bits && this.m_second64Bits == second64Bits;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0006C7EE File Offset: 0x0006A9EE
		private static bool EliminateNullEqualityChecks(object o1, object o2, out bool result)
		{
			if (object.ReferenceEquals(o1, null) && object.ReferenceEquals(o2, null))
			{
				result = true;
				return true;
			}
			result = false;
			return object.ReferenceEquals(o1, null) || object.ReferenceEquals(o2, null);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0006C81F File Offset: 0x0006AA1F
		private static bool EqualityCompare(SequencedId id1, UniqueId id2)
		{
			return id1.m_stringID == id2.ToString();
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0006C832 File Offset: 0x0006AA32
		private static bool EqualityCompare(SequencedId id1, SequencedId id2)
		{
			return id1.m_first64Bits == id2.m_first64Bits && id1.m_second64Bits == id2.m_second64Bits && id1.m_offset == id2.m_offset;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0006C860 File Offset: 0x0006AA60
		public static bool operator ==(SequencedId id1, UniqueId id2)
		{
			bool flag;
			if (SequencedId.EliminateNullEqualityChecks(id1, id2, out flag))
			{
				return flag;
			}
			return SequencedId.EqualityCompare(id1, id2);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0006C881 File Offset: 0x0006AA81
		public static bool operator !=(SequencedId id1, UniqueId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0006C88D File Offset: 0x0006AA8D
		public static bool operator ==(UniqueId id1, SequencedId id2)
		{
			return id2 == id1;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0006C896 File Offset: 0x0006AA96
		public static bool operator !=(UniqueId id1, SequencedId id2)
		{
			return !(id2 == id1);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0006C8A4 File Offset: 0x0006AAA4
		public static bool operator ==(SequencedId id1, SequencedId id2)
		{
			bool flag;
			if (SequencedId.EliminateNullEqualityChecks(id1, id2, out flag))
			{
				return flag;
			}
			return SequencedId.EqualityCompare(id1, id2);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0006C8C5 File Offset: 0x0006AAC5
		public static bool operator !=(SequencedId id1, SequencedId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0006C8D4 File Offset: 0x0006AAD4
		public override bool Equals(object obj)
		{
			SequencedId sequencedId = obj as SequencedId;
			if (sequencedId != null)
			{
				return SequencedId.EqualityCompare(this, sequencedId);
			}
			UniqueId uniqueId = obj as UniqueId;
			return uniqueId != null && SequencedId.EqualityCompare(this, uniqueId);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0006C912 File Offset: 0x0006AB12
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0006C91A File Offset: 0x0006AB1A
		public override string ToString()
		{
			return this.m_stringID;
		}

		// Token: 0x04001607 RID: 5639
		private string m_stringID;

		// Token: 0x04001608 RID: 5640
		private long m_first64Bits;

		// Token: 0x04001609 RID: 5641
		private long m_second64Bits;

		// Token: 0x0400160A RID: 5642
		private int m_offset;
	}
}
