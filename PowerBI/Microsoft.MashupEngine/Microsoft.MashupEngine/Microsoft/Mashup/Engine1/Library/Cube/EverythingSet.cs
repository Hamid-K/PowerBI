using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D4C RID: 3404
	internal class EverythingSet : Set
	{
		// Token: 0x06005BC6 RID: 23494 RVA: 0x0013F9EF File Offset: 0x0013DBEF
		protected EverythingSet()
		{
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06005BC7 RID: 23495 RVA: 0x0000240C File Offset: 0x0000060C
		public override SetKind Kind
		{
			get
			{
				return SetKind.Everything;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x0013F9F7 File Offset: 0x0013DBF7
		public override double Cardinality
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06005BC9 RID: 23497 RVA: 0x000E9ECC File Offset: 0x000E80CC
		public override Dimensionality Dimensionality
		{
			get
			{
				return Dimensionality.Empty;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06005BCA RID: 23498 RVA: 0x00002105 File Offset: 0x00000305
		public override bool HasMeasureFilter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0013FA02 File Offset: 0x0013DC02
		public override IEnumerable<Set> GetSubsets()
		{
			yield break;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public override Set IntersectAsLeft(Set other)
		{
			return other;
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public override Set IntersectAsRight(Set other)
		{
			return other;
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public override Set CrossJoinAsLeft(Set other)
		{
			return other;
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public override Set CrossJoinAsRight(Set other)
		{
			return other;
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return this;
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set Unordered()
		{
			return this;
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this;
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set NewScope(string scope)
		{
			return this;
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x0003391C File Offset: 0x00031B1C
		public bool Equals(EverythingSet other)
		{
			return other != null;
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0013FA0B File Offset: 0x0013DC0B
		public override bool Equals(object other)
		{
			return this.Equals(other as EverythingSet);
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x00002139 File Offset: 0x00000339
		public override int GetHashCode()
		{
			return 1;
		}

		// Token: 0x04003309 RID: 13065
		public static readonly Set Instance = new EverythingSet();
	}
}
