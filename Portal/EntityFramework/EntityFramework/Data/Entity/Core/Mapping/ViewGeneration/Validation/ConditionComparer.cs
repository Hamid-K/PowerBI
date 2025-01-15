using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000579 RID: 1401
	internal class ConditionComparer : IEqualityComparer<Dictionary<MemberPath, Set<Constant>>>
	{
		// Token: 0x060043DD RID: 17373 RVA: 0x000ED0F4 File Offset: 0x000EB2F4
		public bool Equals(Dictionary<MemberPath, Set<Constant>> one, Dictionary<MemberPath, Set<Constant>> two)
		{
			Set<MemberPath> set = new Set<MemberPath>(one.Keys, MemberPath.EqualityComparer);
			Set<MemberPath> set2 = new Set<MemberPath>(two.Keys, MemberPath.EqualityComparer);
			if (!set.SetEquals(set2))
			{
				return false;
			}
			foreach (MemberPath memberPath in set)
			{
				Set<Constant> set3 = one[memberPath];
				Set<Constant> set4 = two[memberPath];
				if (!set3.SetEquals(set4))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000ED18C File Offset: 0x000EB38C
		public int GetHashCode(Dictionary<MemberPath, Set<Constant>> obj)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath memberPath in obj.Keys)
			{
				stringBuilder.Append(memberPath);
			}
			return stringBuilder.ToString().GetHashCode();
		}
	}
}
