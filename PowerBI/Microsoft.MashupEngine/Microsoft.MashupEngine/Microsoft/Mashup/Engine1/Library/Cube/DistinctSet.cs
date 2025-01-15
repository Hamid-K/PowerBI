using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D53 RID: 3411
	internal class DistinctSet : Set
	{
		// Token: 0x06005C20 RID: 23584 RVA: 0x00140040 File Offset: 0x0013E240
		public DistinctSet(Set set)
		{
			this.set = set;
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06005C21 RID: 23585 RVA: 0x000023C4 File Offset: 0x000005C4
		public override SetKind Kind
		{
			get
			{
				return SetKind.Distinct;
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06005C22 RID: 23586 RVA: 0x0014004F File Offset: 0x0013E24F
		public override double Cardinality
		{
			get
			{
				return this.set.Cardinality;
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06005C23 RID: 23587 RVA: 0x0014005C File Offset: 0x0013E25C
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.set.Dimensionality;
			}
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06005C24 RID: 23588 RVA: 0x00140069 File Offset: 0x0013E269
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter;
			}
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x00140076 File Offset: 0x0013E276
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x0014007E File Offset: 0x0013E27E
		public override IEnumerable<Set> GetSubsets()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this;
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x0014008E File Offset: 0x0013E28E
		public override Set Unordered()
		{
			return new DistinctSet(this.set.Unordered());
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x001400A0 File Offset: 0x0013E2A0
		public override Set NewScope(string scope)
		{
			return new DistinctSet(this.set.NewScope(scope));
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x001400B3 File Offset: 0x0013E2B3
		public bool Equals(DistinctSet other)
		{
			return other != null && this.set.Equals(other.set);
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x001400CB File Offset: 0x0013E2CB
		public override bool Equals(object other)
		{
			return this.Equals(other as DistinctSet);
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x001400D9 File Offset: 0x0013E2D9
		public override int GetHashCode()
		{
			return this.set.GetHashCode() * 9131;
		}

		// Token: 0x0400331B RID: 13083
		protected readonly Set set;
	}
}
