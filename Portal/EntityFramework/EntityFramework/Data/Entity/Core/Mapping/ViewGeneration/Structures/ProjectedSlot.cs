using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005AD RID: 1453
	internal abstract class ProjectedSlot : InternalBase, IEquatable<ProjectedSlot>
	{
		// Token: 0x060046B6 RID: 18102 RVA: 0x000F9F75 File Offset: 0x000F8175
		protected virtual bool IsEqualTo(ProjectedSlot right)
		{
			return base.Equals(right);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x000F9F7E File Offset: 0x000F817E
		protected virtual int GetHash()
		{
			return base.GetHashCode();
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x000F9F86 File Offset: 0x000F8186
		public bool Equals(ProjectedSlot right)
		{
			return ProjectedSlot.EqualityComparer.Equals(this, right);
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000F9F94 File Offset: 0x000F8194
		public override bool Equals(object obj)
		{
			ProjectedSlot projectedSlot = obj as ProjectedSlot;
			return obj != null && this.Equals(projectedSlot);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000F9FB4 File Offset: 0x000F81B4
		public override int GetHashCode()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000F9FC1 File Offset: 0x000F81C1
		internal virtual ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new QualifiedSlot(block, this);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x000F9FCA File Offset: 0x000F81CA
		internal virtual string GetCqlFieldAlias(MemberPath outputMember)
		{
			return outputMember.CqlFieldAlias;
		}

		// Token: 0x060046BD RID: 18109
		internal abstract StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel);

		// Token: 0x060046BE RID: 18110
		internal abstract DbExpression AsCqt(DbExpression row, MemberPath outputMember);

		// Token: 0x060046BF RID: 18111 RVA: 0x000F9FD4 File Offset: 0x000F81D4
		internal static bool TryMergeRemapSlots(ProjectedSlot[] slots1, ProjectedSlot[] slots2, out ProjectedSlot[] result)
		{
			ProjectedSlot[] array;
			if (!ProjectedSlot.TryMergeSlots(slots1, slots2, out array))
			{
				result = null;
				return false;
			}
			result = array;
			return true;
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x000F9FF8 File Offset: 0x000F81F8
		private static bool TryMergeSlots(ProjectedSlot[] slots1, ProjectedSlot[] slots2, out ProjectedSlot[] slots)
		{
			slots = new ProjectedSlot[slots1.Length];
			for (int i = 0; i < slots.Length; i++)
			{
				ProjectedSlot projectedSlot = slots1[i];
				ProjectedSlot projectedSlot2 = slots2[i];
				if (projectedSlot == null)
				{
					slots[i] = projectedSlot2;
				}
				else if (projectedSlot2 == null)
				{
					slots[i] = projectedSlot;
				}
				else
				{
					MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
					MemberProjectedSlot memberProjectedSlot2 = projectedSlot2 as MemberProjectedSlot;
					if (memberProjectedSlot != null && memberProjectedSlot2 != null && !ProjectedSlot.EqualityComparer.Equals(memberProjectedSlot, memberProjectedSlot2))
					{
						return false;
					}
					ProjectedSlot projectedSlot3 = ((memberProjectedSlot != null) ? projectedSlot : projectedSlot2);
					slots[i] = projectedSlot3;
				}
			}
			return true;
		}

		// Token: 0x04001926 RID: 6438
		internal static readonly IEqualityComparer<ProjectedSlot> EqualityComparer = new ProjectedSlot.Comparer();

		// Token: 0x02000BDF RID: 3039
		private sealed class Comparer : IEqualityComparer<ProjectedSlot>
		{
			// Token: 0x06006853 RID: 26707 RVA: 0x00163A28 File Offset: 0x00161C28
			public bool Equals(ProjectedSlot left, ProjectedSlot right)
			{
				return left == right || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x06006854 RID: 26708 RVA: 0x00163A3F File Offset: 0x00161C3F
			public int GetHashCode(ProjectedSlot key)
			{
				return key.GetHash();
			}
		}
	}
}
