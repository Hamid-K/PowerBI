using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D50 RID: 3408
	internal class MemberSet : Set
	{
		// Token: 0x06005BF9 RID: 23545 RVA: 0x0013FCE3 File Offset: 0x0013DEE3
		public MemberSet(ICubeLevel level, Value member)
		{
			this.level = level;
			this.member = member;
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x000024ED File Offset: 0x000006ED
		public override SetKind Kind
		{
			get
			{
				return SetKind.Member;
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x0013F9F7 File Offset: 0x0013DBF7
		public override double Cardinality
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x0013FCF9 File Offset: 0x0013DEF9
		public override Dimensionality Dimensionality
		{
			get
			{
				if (this.dimensionality == null)
				{
					this.dimensionality = new Dimensionality(new CubeLevelRange[]
					{
						new CubeLevelRange(this.level, this.level)
					});
				}
				return this.dimensionality;
			}
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x00002105 File Offset: 0x00000305
		public override bool HasMeasureFilter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06005BFE RID: 23550 RVA: 0x0013FD2E File Offset: 0x0013DF2E
		public ICubeLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x0013FD36 File Offset: 0x0013DF36
		public Value Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x0013FD3E File Offset: 0x0013DF3E
		public override IEnumerable<Set> GetSubsets()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return this;
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this;
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set Unordered()
		{
			return this;
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x0013FD4E File Offset: 0x0013DF4E
		public override Set NewScope(string scope)
		{
			return new MemberSet(this.level.NewScope(scope), this.member);
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x0013FD67 File Offset: 0x0013DF67
		public bool Equals(MemberSet other)
		{
			return other != null && this.level.Equals(other.level) && this.member.Equals(other.member);
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x0013FD92 File Offset: 0x0013DF92
		public override bool Equals(object other)
		{
			return this.Equals(other as MemberSet);
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x0013FDA0 File Offset: 0x0013DFA0
		public override int GetHashCode()
		{
			return this.level.GetHashCode() + 37 * this.member.GetHashCode();
		}

		// Token: 0x04003312 RID: 13074
		private readonly ICubeLevel level;

		// Token: 0x04003313 RID: 13075
		private readonly Value member;

		// Token: 0x04003314 RID: 13076
		private Dimensionality dimensionality;
	}
}
