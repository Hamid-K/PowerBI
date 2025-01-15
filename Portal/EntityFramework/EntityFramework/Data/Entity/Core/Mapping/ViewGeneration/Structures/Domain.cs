using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A1 RID: 1441
	internal class Domain : InternalBase
	{
		// Token: 0x060045CB RID: 17867 RVA: 0x000F6053 File Offset: 0x000F4253
		internal Domain(Constant value, IEnumerable<Constant> possibleDiscreteValues)
			: this(new Constant[] { value }, possibleDiscreteValues)
		{
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x000F6066 File Offset: 0x000F4266
		internal Domain(IEnumerable<Constant> values, IEnumerable<Constant> possibleDiscreteValues)
		{
			this.m_possibleValues = Domain.DeterminePossibleValues(values, possibleDiscreteValues);
			this.m_domain = Domain.ExpandNegationsInDomain(values, this.m_possibleValues);
			this.AssertInvariant();
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x000F6093 File Offset: 0x000F4293
		internal Domain(Domain domain)
		{
			this.m_domain = new Set<Constant>(domain.m_domain, Constant.EqualityComparer);
			this.m_possibleValues = new Set<Constant>(domain.m_possibleValues, Constant.EqualityComparer);
			this.AssertInvariant();
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x000F60CD File Offset: 0x000F42CD
		internal IEnumerable<Constant> AllPossibleValues
		{
			get
			{
				return this.AllPossibleValuesInternal;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x000F60D8 File Offset: 0x000F42D8
		private Set<Constant> AllPossibleValuesInternal
		{
			get
			{
				NegatedConstant negatedConstant = new NegatedConstant(this.m_possibleValues);
				return this.m_possibleValues.Union(new Constant[] { negatedConstant });
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x000F6106 File Offset: 0x000F4306
		internal int Count
		{
			get
			{
				return this.m_domain.Count;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x000F6113 File Offset: 0x000F4313
		internal IEnumerable<Constant> Values
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x000F611C File Offset: 0x000F431C
		internal static Set<Constant> DeriveDomainFromMemberPath(MemberPath memberPath, EdmItemCollection edmItemCollection, bool leaveDomainUnbounded)
		{
			Set<Constant> set = Domain.DeriveDomainFromType(memberPath.EdmType, edmItemCollection, leaveDomainUnbounded);
			if (memberPath.IsNullable)
			{
				set.Add(Constant.Null);
			}
			return set;
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000F614C File Offset: 0x000F434C
		private static Set<Constant> DeriveDomainFromType(EdmType type, EdmItemCollection edmItemCollection, bool leaveDomainUnbounded)
		{
			Set<Constant> set;
			if (Helper.IsScalarType(type))
			{
				if (MetadataHelper.HasDiscreteDomain(type))
				{
					set = new Set<Constant>(Domain.CreateList(true, false), Constant.EqualityComparer);
				}
				else
				{
					set = new Set<Constant>(Constant.EqualityComparer);
					if (leaveDomainUnbounded)
					{
						set.Add(Constant.NotNull);
					}
				}
			}
			else
			{
				if (Helper.IsRefType(type))
				{
					type = ((RefType)type).ElementType;
				}
				List<Constant> list = new List<Constant>();
				foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf(type, edmItemCollection, false))
				{
					TypeConstant typeConstant = new TypeConstant(edmType);
					list.Add(typeConstant);
				}
				set = new Set<Constant>(list, Constant.EqualityComparer);
			}
			return set;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000F6214 File Offset: 0x000F4414
		internal static bool TryGetDefaultValueForMemberPath(MemberPath memberPath, out Constant defaultConstant)
		{
			object defaultValue = memberPath.DefaultValue;
			defaultConstant = Constant.Null;
			if (defaultValue != null)
			{
				defaultConstant = new ScalarConstant(defaultValue);
				return true;
			}
			return memberPath.IsNullable || memberPath.IsComputed;
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x000F6250 File Offset: 0x000F4450
		internal static Constant GetDefaultValueForMemberPath(MemberPath memberPath, IEnumerable<LeftCellWrapper> wrappersForErrorReporting, ConfigViewGenerator config)
		{
			Constant constant = null;
			if (!Domain.TryGetDefaultValueForMemberPath(memberPath, out constant))
			{
				string text = Strings.ViewGen_No_Default_Value(memberPath.Extent.Name, memberPath.PathToString(new bool?(false)));
				ExceptionHelpers.ThrowMappingException(new ErrorLog.Record(ViewGenErrorCode.NoDefaultValue, text, wrappersForErrorReporting, string.Empty), config);
			}
			return constant;
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x000F62A0 File Offset: 0x000F44A0
		internal int GetHash()
		{
			int num = 0;
			foreach (Constant constant in this.m_domain)
			{
				num ^= Constant.EqualityComparer.GetHashCode(constant);
			}
			return num;
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000F6300 File Offset: 0x000F4500
		internal bool IsEqualTo(Domain second)
		{
			return this.m_domain.SetEquals(second.m_domain);
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x000F6314 File Offset: 0x000F4514
		internal bool ContainsNotNull()
		{
			NegatedConstant negatedConstant = Domain.GetNegatedConstant(this.m_domain);
			return negatedConstant != null && negatedConstant.Contains(Constant.Null);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000F633D File Offset: 0x000F453D
		internal bool Contains(Constant constant)
		{
			return this.m_domain.Contains(constant);
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000F634C File Offset: 0x000F454C
		internal static Set<Constant> ExpandNegationsInDomain(IEnumerable<Constant> domain, IEnumerable<Constant> otherPossibleValues)
		{
			Set<Constant> set = Domain.DeterminePossibleValues(domain, otherPossibleValues);
			Set<Constant> set2 = new Set<Constant>(Constant.EqualityComparer);
			foreach (Constant constant in domain)
			{
				NegatedConstant negatedConstant = constant as NegatedConstant;
				if (negatedConstant != null)
				{
					set2.Add(new NegatedConstant(set));
					Set<Constant> set3 = set.Difference(negatedConstant.Elements);
					set2.AddRange(set3);
				}
				else
				{
					set2.Add(constant);
				}
			}
			return set2;
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000F63DC File Offset: 0x000F45DC
		internal static Set<Constant> ExpandNegationsInDomain(IEnumerable<Constant> domain)
		{
			return Domain.ExpandNegationsInDomain(domain, domain);
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000F63E8 File Offset: 0x000F45E8
		private static Set<Constant> DeterminePossibleValues(IEnumerable<Constant> domain)
		{
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			foreach (Constant constant in domain)
			{
				NegatedConstant negatedConstant = constant as NegatedConstant;
				if (negatedConstant != null)
				{
					using (IEnumerator<Constant> enumerator2 = negatedConstant.Elements.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Constant constant2 = enumerator2.Current;
							set.Add(constant2);
						}
						continue;
					}
				}
				set.Add(constant);
			}
			return set;
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x000F648C File Offset: 0x000F468C
		internal static Dictionary<MemberPath, Set<Constant>> ComputeConstantDomainSetsForSlotsInQueryViews(IEnumerable<Cell> cells, EdmItemCollection edmItemCollection, bool isValidationEnabled)
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			foreach (Cell cell in cells)
			{
				foreach (MemberRestriction memberRestriction in cell.CQuery.GetConjunctsFromWhereClause())
				{
					MemberProjectedSlot restrictedMemberSlot = memberRestriction.RestrictedMemberSlot;
					Set<Constant> set = Domain.DeriveDomainFromMemberPath(restrictedMemberSlot.MemberPath, edmItemCollection, isValidationEnabled);
					set.AddRange(memberRestriction.Domain.Values.Where((Constant c) => !c.Equals(Constant.Null) && !c.Equals(Constant.NotNull)));
					Set<Constant> set2;
					if (!dictionary.TryGetValue(restrictedMemberSlot.MemberPath, out set2))
					{
						dictionary[restrictedMemberSlot.MemberPath] = set;
					}
					else
					{
						set2.AddRange(set);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x000F6598 File Offset: 0x000F4798
		private static bool GetRestrictedOrUnrestrictedDomain(MemberProjectedSlot slot, CellQuery cellQuery, EdmItemCollection edmItemCollection, out Set<Constant> domain)
		{
			return Domain.TryGetDomainRestrictedByWhereClause(Domain.DeriveDomainFromMemberPath(slot.MemberPath, edmItemCollection, true), slot, cellQuery, out domain);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x000F65B0 File Offset: 0x000F47B0
		internal static Dictionary<MemberPath, Set<Constant>> ComputeConstantDomainSetsForSlotsInUpdateViews(IEnumerable<Cell> cells, EdmItemCollection edmItemCollection)
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				CellQuery squery = cell.SQuery;
				foreach (MemberProjectedSlot memberProjectedSlot in from oneOfConst in squery.GetConjunctsFromWhereClause()
					select oneOfConst.RestrictedMemberSlot)
				{
					Set<Constant> set;
					if (!Domain.GetRestrictedOrUnrestrictedDomain(memberProjectedSlot, squery, edmItemCollection, out set))
					{
						int projectedPosition = squery.GetProjectedPosition(memberProjectedSlot);
						if (projectedPosition >= 0 && !Domain.GetRestrictedOrUnrestrictedDomain(cquery.ProjectedSlotAt(projectedPosition) as MemberProjectedSlot, cquery, edmItemCollection, out set))
						{
							continue;
						}
					}
					MemberPath memberPath = memberProjectedSlot.MemberPath;
					Constant constant;
					if (Domain.TryGetDefaultValueForMemberPath(memberPath, out constant))
					{
						set.Add(constant);
					}
					Set<Constant> set2;
					if (!dictionary.TryGetValue(memberPath, out set2))
					{
						dictionary[memberPath] = set;
					}
					else
					{
						set2.AddRange(set);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000F66E4 File Offset: 0x000F48E4
		private static bool TryGetDomainRestrictedByWhereClause(IEnumerable<Constant> domain, MemberProjectedSlot slot, CellQuery cellQuery, out Set<Constant> result)
		{
			IEnumerable<Set<Constant>> enumerable = from restriction in cellQuery.GetConjunctsFromWhereClause()
				where MemberPath.EqualityComparer.Equals(restriction.RestrictedMemberSlot.MemberPath, slot.MemberPath)
				select new Set<Constant>(restriction.Domain.Values, Constant.EqualityComparer);
			if (!enumerable.Any<Set<Constant>>())
			{
				result = new Set<Constant>(domain);
				return false;
			}
			Set<Constant> set = Domain.DeterminePossibleValues(enumerable.SelectMany((Set<Constant> m) => m.Select((Constant c) => c)), domain);
			Domain domain2 = new Domain(domain, set);
			foreach (Set<Constant> set2 in enumerable)
			{
				domain2 = domain2.Intersect(new Domain(set2, set));
			}
			result = new Set<Constant>(domain2.Values, Constant.EqualityComparer);
			return !domain.SequenceEqual(result);
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000F67E8 File Offset: 0x000F49E8
		private Domain Intersect(Domain second)
		{
			Domain domain = new Domain(this);
			domain.m_domain.Intersect(second.m_domain);
			return domain;
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x000F6804 File Offset: 0x000F4A04
		private static NegatedConstant GetNegatedConstant(IEnumerable<Constant> constants)
		{
			NegatedConstant negatedConstant = null;
			foreach (Constant constant in constants)
			{
				NegatedConstant negatedConstant2 = constant as NegatedConstant;
				if (negatedConstant2 != null)
				{
					negatedConstant = negatedConstant2;
				}
			}
			return negatedConstant;
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x000F6854 File Offset: 0x000F4A54
		private static Set<Constant> DeterminePossibleValues(IEnumerable<Constant> domain1, IEnumerable<Constant> domain2)
		{
			return Domain.DeterminePossibleValues(new Set<Constant>(domain1, Constant.EqualityComparer).Union(domain2));
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000F686C File Offset: 0x000F4A6C
		[Conditional("DEBUG")]
		private static void CheckTwoDomainInvariants(Domain domain1, Domain domain2)
		{
			domain1.AssertInvariant();
			domain2.AssertInvariant();
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000F687A File Offset: 0x000F4A7A
		private static IEnumerable<Constant> CreateList(object value1, object value2)
		{
			yield return new ScalarConstant(value1);
			yield return new ScalarConstant(value2);
			yield break;
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000F6891 File Offset: 0x000F4A91
		internal void AssertInvariant()
		{
			Domain.GetNegatedConstant(this.m_domain);
			Domain.GetNegatedConstant(this.m_possibleValues);
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x000F68AC File Offset: 0x000F4AAC
		internal string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (Constant constant in this.m_domain)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(constant.ToUserString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x000F6924 File Offset: 0x000F4B24
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.ToUserString());
		}

		// Token: 0x040018FF RID: 6399
		private readonly Set<Constant> m_domain;

		// Token: 0x04001900 RID: 6400
		private readonly Set<Constant> m_possibleValues;
	}
}
