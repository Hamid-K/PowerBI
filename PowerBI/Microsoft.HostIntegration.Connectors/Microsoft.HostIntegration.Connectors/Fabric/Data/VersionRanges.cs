using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C8 RID: 968
	[DataContract(Name = "VersionRange", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class VersionRanges
	{
		// Token: 0x06002215 RID: 8725 RVA: 0x00068F6C File Offset: 0x0006716C
		public VersionRanges()
		{
			this.Ranges = new List<VersionRange>();
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00068F7F File Offset: 0x0006717F
		public VersionRanges(long startVersion, long endVersion)
			: this()
		{
			if (startVersion < endVersion)
			{
				this.Ranges.Add(new VersionRange(startVersion, endVersion));
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00068F9D File Offset: 0x0006719D
		public VersionRanges(VersionRanges other)
		{
			this.m_ranges = new List<VersionRange>(other.m_ranges);
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x00068FB6 File Offset: 0x000671B6
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x00068FBE File Offset: 0x000671BE
		[DataMember]
		public List<VersionRange> Ranges
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

		// Token: 0x0600221A RID: 8730 RVA: 0x00068FC7 File Offset: 0x000671C7
		public bool IsEmpty()
		{
			return this.m_ranges.Count == 0;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00068FD8 File Offset: 0x000671D8
		public bool Add(VersionRanges ranges)
		{
			bool flag = false;
			foreach (VersionRange versionRange in ranges.m_ranges)
			{
				if (this.Add(versionRange))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00069034 File Offset: 0x00067234
		public bool Add(VersionRange range)
		{
			long num = range.StartVersion;
			long num2 = range.EndVersion;
			if (num == num2)
			{
				return false;
			}
			int num3 = 0;
			while (num3 < this.m_ranges.Count && this.m_ranges[num3].EndVersion < num)
			{
				num3++;
			}
			int num4 = num3;
			while (num4 < this.m_ranges.Count && this.m_ranges[num4].StartVersion <= num2)
			{
				num4++;
			}
			if (num3 < num4)
			{
				num = Math.Min(num, this.m_ranges[num3].StartVersion);
				num2 = Math.Max(num2, this.m_ranges[num4 - 1].EndVersion);
				if (num4 == num3 + 1 && this.m_ranges[num3].StartVersion == num && this.m_ranges[num3].EndVersion == num2)
				{
					return false;
				}
				range = new VersionRange(num, num2);
			}
			List<VersionRange> list = new List<VersionRange>();
			for (int i = 0; i < num3; i++)
			{
				list.Add(this.m_ranges[i]);
			}
			list.Add(range);
			for (int j = num4; j < this.m_ranges.Count; j++)
			{
				list.Add(this.m_ranges[j]);
			}
			this.m_ranges = list;
			return true;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00069184 File Offset: 0x00067384
		public void Remove(VersionRanges ranges)
		{
			foreach (VersionRange versionRange in ranges.m_ranges)
			{
				this.Remove(versionRange);
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000691D8 File Offset: 0x000673D8
		public void Remove(VersionRange range)
		{
			long startVersion = range.StartVersion;
			long endVersion = range.EndVersion;
			if (startVersion == endVersion)
			{
				return;
			}
			int num = 0;
			while (num < this.m_ranges.Count && this.m_ranges[num].EndVersion <= startVersion)
			{
				num++;
			}
			int num2 = num;
			while (num2 < this.m_ranges.Count && this.m_ranges[num2].StartVersion < endVersion)
			{
				num2++;
			}
			num2--;
			if (num > num2)
			{
				return;
			}
			VersionRanges versionRanges = new VersionRanges();
			if (this.m_ranges[num].StartVersion < startVersion)
			{
				versionRanges.Add(new VersionRange(this.m_ranges[num].StartVersion, startVersion));
			}
			if (this.m_ranges[num2].EndVersion > endVersion)
			{
				versionRanges.Add(new VersionRange(endVersion, this.m_ranges[num2].EndVersion));
			}
			this.m_ranges.RemoveRange(num, num2 - num + 1);
			if (versionRanges.m_ranges.Count > 0)
			{
				this.Add(versionRanges);
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000692EC File Offset: 0x000674EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			foreach (VersionRange versionRange in this.m_ranges)
			{
				stringBuilder.Append(versionRange.ToString()).Append(",");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400159B RID: 5531
		private List<VersionRange> m_ranges;
	}
}
