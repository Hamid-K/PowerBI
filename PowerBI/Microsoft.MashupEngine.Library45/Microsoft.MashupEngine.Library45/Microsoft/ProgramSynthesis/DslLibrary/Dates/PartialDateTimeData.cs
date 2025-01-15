using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000873 RID: 2163
	public struct PartialDateTimeData : IEquatable<PartialDateTimeData>
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x00089D37 File Offset: 0x00087F37
		public PartialDateTimeData(DateTimePartSet setParts, int[] values)
		{
			this.SetParts = setParts;
			this.Values = values;
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x00089D47 File Offset: 0x00087F47
		public static PartialDateTimeData Empty
		{
			get
			{
				return new PartialDateTimeData(DateTimePartSet.Empty, new int[DateTimePartUtil.PartKindCount]);
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x00089D5D File Offset: 0x00087F5D
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x00089D65 File Offset: 0x00087F65
		public DateTimePartSet SetParts { readonly get; private set; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x00089D6E File Offset: 0x00087F6E
		public readonly int[] Values { get; }

		// Token: 0x06002F26 RID: 12070 RVA: 0x00089D78 File Offset: 0x00087F78
		public PartialDateTimeData? CombineWith(PartialDateTimeData other)
		{
			int partKindCount = DateTimePartUtil.PartKindCount;
			DateTimePartSet dateTimePartSet = this.SetParts.Intersect(other.SetParts);
			for (int i = 0; i < partKindCount; i++)
			{
				if (dateTimePartSet.ContainsBit(i) && this.Values[i] != other.Values[i])
				{
					return null;
				}
			}
			DateTimePartSet dateTimePartSet2 = this.SetParts.Union(other.SetParts);
			int[] array = new int[partKindCount];
			DateTimePartSet dateTimePartSet3 = dateTimePartSet.Union(this.SetParts);
			for (int j = 0; j < partKindCount; j++)
			{
				array[j] = (dateTimePartSet3.ContainsBit(j) ? this.Values[j] : other.Values[j]);
			}
			return new PartialDateTimeData?(new PartialDateTimeData(dateTimePartSet2, array));
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x00089E4C File Offset: 0x0008804C
		public PartialDateTimeData? With(DateTimePart part, int newValue)
		{
			int? num = this.Get(part);
			if (num == null)
			{
				DateTimePartSet dateTimePartSet = this.SetParts.Set(part);
				int partKindCount = DateTimePartUtil.PartKindCount;
				int[] array = new int[partKindCount];
				Array.Copy(this.Values, array, partKindCount);
				array[(int)part] = newValue;
				return new PartialDateTimeData?(new PartialDateTimeData(dateTimePartSet, array));
			}
			int? num2 = num;
			if (!((num2.GetValueOrDefault() == newValue) & (num2 != null)))
			{
				return null;
			}
			return new PartialDateTimeData?(this);
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x00089ED8 File Offset: 0x000880D8
		public PartialDateTimeData Without(DateTimePart part)
		{
			DateTimePartSet dateTimePartSet = this.SetParts.Clear(part);
			int partKindCount = DateTimePartUtil.PartKindCount;
			int[] array = new int[partKindCount];
			Array.Copy(this.Values, array, partKindCount);
			array[(int)part] = 0;
			return new PartialDateTimeData(dateTimePartSet, array);
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x00089F1C File Offset: 0x0008811C
		public PartialDateTimeData Without(DateTimePartSet parts)
		{
			int partKindCount = DateTimePartUtil.PartKindCount;
			int[] array = new int[partKindCount];
			Array.Copy(this.Values, array, partKindCount);
			using (IEnumerator<DateTimePart> enumerator = parts.AsEnumerable().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = (int)enumerator.Current;
					array[num] = 0;
				}
			}
			return new PartialDateTimeData(this.SetParts.SetDifference(parts), array);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x00089F98 File Offset: 0x00088198
		public bool TryAdd(DateTimePart part, int newValue)
		{
			int? num = this.Get(part);
			if (num != null)
			{
				int? num2 = num;
				return (num2.GetValueOrDefault() == newValue) & (num2 != null);
			}
			this.Values[(int)part] = newValue;
			this.SetParts = this.SetParts.Set(part);
			return true;
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x00089FF0 File Offset: 0x000881F0
		public bool Contains(DateTimePart part)
		{
			return this.SetParts.Contains(part);
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x0008A00C File Offset: 0x0008820C
		public int? Get(DateTimePart part)
		{
			if (part >= (DateTimePart)DateTimePartUtil.PartKindCount)
			{
				throw new NotImplementedException("Unknown DateTimePart: " + part.ToString());
			}
			if (!this.Contains(part))
			{
				return null;
			}
			return new int?(this.Values[(int)part]);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x0008A05E File Offset: 0x0008825E
		public bool Equals(PartialDateTimeData other)
		{
			return this.SetParts == other.SetParts && this.Values.SequenceEqual(other.Values);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x0008A088 File Offset: 0x00088288
		public override bool Equals(object obj)
		{
			return obj != null && obj is PartialDateTimeData && this.Equals((PartialDateTimeData)obj);
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x0008A0A8 File Offset: 0x000882A8
		public override int GetHashCode()
		{
			int num = this.SetParts.GetHashCode();
			foreach (int num2 in this.Values)
			{
				num = (num * 10734547) ^ num2;
			}
			return num;
		}
	}
}
