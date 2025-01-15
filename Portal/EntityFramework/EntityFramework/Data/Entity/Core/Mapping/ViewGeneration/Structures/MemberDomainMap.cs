using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A5 RID: 1445
	internal class MemberDomainMap : InternalBase
	{
		// Token: 0x0600461C RID: 17948 RVA: 0x000F7422 File Offset: 0x000F5622
		private MemberDomainMap(Dictionary<MemberPath, Set<Constant>> domainMap, Dictionary<MemberPath, Set<Constant>> nonConditionDomainMap, EdmItemCollection edmItemCollection)
		{
			this.m_conditionDomainMap = domainMap;
			this.m_nonConditionDomainMap = nonConditionDomainMap;
			this.m_edmItemCollection = edmItemCollection;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000F744C File Offset: 0x000F564C
		internal MemberDomainMap(ViewTarget viewTarget, bool isValidationEnabled, IEnumerable<Cell> extentCells, EdmItemCollection edmItemCollection, ConfigViewGenerator config, Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			this.m_conditionDomainMap = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			this.m_edmItemCollection = edmItemCollection;
			Dictionary<MemberPath, Set<Constant>> dictionary = null;
			if (viewTarget == ViewTarget.UpdateView)
			{
				dictionary = Domain.ComputeConstantDomainSetsForSlotsInUpdateViews(extentCells, this.m_edmItemCollection);
			}
			else
			{
				dictionary = Domain.ComputeConstantDomainSetsForSlotsInQueryViews(extentCells, this.m_edmItemCollection, isValidationEnabled);
			}
			foreach (Cell cell in extentCells)
			{
				foreach (MemberRestriction memberRestriction in cell.GetLeftQuery(viewTarget).GetConjunctsFromWhereClause())
				{
					MemberPath memberPath = memberRestriction.RestrictedMemberSlot.MemberPath;
					Set<Constant> set;
					if (!dictionary.TryGetValue(memberPath, out set))
					{
						set = Domain.DeriveDomainFromMemberPath(memberPath, edmItemCollection, isValidationEnabled);
					}
					if (!set.Contains(Constant.Null))
					{
						if (memberRestriction.Domain.Values.All((Constant conditionConstant) => conditionConstant.Equals(Constant.NotNull)))
						{
							continue;
						}
					}
					if (set.Count <= 0 || (!set.Contains(Constant.Null) && memberRestriction.Domain.Values.Contains(Constant.Null)))
					{
						string text = Strings.ViewGen_InvalidCondition(memberPath.PathToString(new bool?(false)));
						ExceptionHelpers.ThrowMappingException(new ErrorLog.Record(ViewGenErrorCode.InvalidCondition, text, cell, string.Empty), config);
					}
					if (!memberPath.IsAlwaysDefined(inheritanceGraph))
					{
						set.Add(Constant.Undefined);
					}
					this.AddToDomainMap(memberPath, set);
				}
			}
			this.m_nonConditionDomainMap = new Dictionary<MemberPath, Set<Constant>>(MemberPath.EqualityComparer);
			foreach (Cell cell2 in extentCells)
			{
				foreach (MemberProjectedSlot memberProjectedSlot in cell2.GetLeftQuery(viewTarget).GetAllQuerySlots())
				{
					MemberPath memberPath2 = memberProjectedSlot.MemberPath;
					if (!this.m_conditionDomainMap.ContainsKey(memberPath2) && !this.m_nonConditionDomainMap.ContainsKey(memberPath2))
					{
						Set<Constant> set2 = Domain.DeriveDomainFromMemberPath(memberPath2, this.m_edmItemCollection, true);
						if (!memberPath2.IsAlwaysDefined(inheritanceGraph))
						{
							set2.Add(Constant.Undefined);
						}
						set2 = Domain.ExpandNegationsInDomain(set2, set2);
						this.m_nonConditionDomainMap.Add(memberPath2, new MemberDomainMap.CellConstantSetInfo(set2));
					}
				}
			}
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000F7728 File Offset: 0x000F5928
		internal bool IsProjectedConditionMember(MemberPath memberPath)
		{
			return this.m_projectedConditionMembers.Contains(memberPath);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000F7738 File Offset: 0x000F5938
		internal MemberDomainMap GetOpenDomain()
		{
			Dictionary<MemberPath, Set<Constant>> dictionary = this.m_conditionDomainMap.ToDictionary((KeyValuePair<MemberPath, Set<Constant>> p) => p.Key, (KeyValuePair<MemberPath, Set<Constant>> p) => new Set<Constant>(p.Value, Constant.EqualityComparer));
			this.ExpandDomainsIfNeeded(dictionary);
			return new MemberDomainMap(dictionary, this.m_nonConditionDomainMap, this.m_edmItemCollection);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000F77A8 File Offset: 0x000F59A8
		internal MemberDomainMap MakeCopy()
		{
			return new MemberDomainMap(this.m_conditionDomainMap.ToDictionary((KeyValuePair<MemberPath, Set<Constant>> p) => p.Key, (KeyValuePair<MemberPath, Set<Constant>> p) => new Set<Constant>(p.Value, Constant.EqualityComparer)), this.m_nonConditionDomainMap, this.m_edmItemCollection);
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000F780F File Offset: 0x000F5A0F
		internal void ExpandDomainsToIncludeAllPossibleValues()
		{
			this.ExpandDomainsIfNeeded(this.m_conditionDomainMap);
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000F7820 File Offset: 0x000F5A20
		private void ExpandDomainsIfNeeded(Dictionary<MemberPath, Set<Constant>> domainMapForMembers)
		{
			foreach (MemberPath memberPath in domainMapForMembers.Keys)
			{
				Set<Constant> set = domainMapForMembers[memberPath];
				if (memberPath.IsScalarType())
				{
					if (!set.Any((Constant c) => c is NegatedConstant))
					{
						if (MetadataHelper.HasDiscreteDomain(memberPath.EdmType))
						{
							Set<Constant> set2 = Domain.DeriveDomainFromMemberPath(memberPath, this.m_edmItemCollection, true);
							set.Unite(set2);
						}
						else
						{
							NegatedConstant negatedConstant = new NegatedConstant(set);
							set.Add(negatedConstant);
						}
					}
				}
			}
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000F78D8 File Offset: 0x000F5AD8
		internal void ReduceEnumerableDomainToEnumeratedValues(ConfigViewGenerator config)
		{
			MemberDomainMap.ReduceEnumerableDomainToEnumeratedValues(this.m_conditionDomainMap, config, this.m_edmItemCollection);
			MemberDomainMap.ReduceEnumerableDomainToEnumeratedValues(this.m_nonConditionDomainMap, config, this.m_edmItemCollection);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x000F7900 File Offset: 0x000F5B00
		private static void ReduceEnumerableDomainToEnumeratedValues(Dictionary<MemberPath, Set<Constant>> domainMap, ConfigViewGenerator config, EdmItemCollection edmItemCollection)
		{
			foreach (MemberPath memberPath in domainMap.Keys)
			{
				if (MetadataHelper.HasDiscreteDomain(memberPath.EdmType))
				{
					Set<Constant> set = Domain.DeriveDomainFromMemberPath(memberPath, edmItemCollection, true);
					Set<Constant> set2 = domainMap[memberPath].Difference(set);
					set2.Remove(Constant.Undefined);
					if (set2.Count > 0)
					{
						if (config.IsNormalTracing)
						{
							Helpers.FormatTraceLine("Changed domain of {0} from {1} - subtract {2}", new object[]
							{
								memberPath,
								domainMap[memberPath],
								set2
							});
						}
						domainMap[memberPath].Subtract(set2);
					}
				}
			}
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000F79BC File Offset: 0x000F5BBC
		internal static void PropagateUpdateDomainToQueryDomain(IEnumerable<Cell> cells, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap)
		{
			foreach (Cell cell in cells)
			{
				CellQuery cquery = cell.CQuery;
				CellQuery squery = cell.SQuery;
				for (int i = 0; i < cquery.NumProjectedSlots; i++)
				{
					MemberProjectedSlot memberProjectedSlot = cquery.ProjectedSlotAt(i) as MemberProjectedSlot;
					MemberProjectedSlot memberProjectedSlot2 = squery.ProjectedSlotAt(i) as MemberProjectedSlot;
					if (memberProjectedSlot != null && memberProjectedSlot2 != null)
					{
						MemberPath memberPath = memberProjectedSlot.MemberPath;
						MemberPath memberPath2 = memberProjectedSlot2.MemberPath;
						Set<Constant> domainInternal = queryDomainMap.GetDomainInternal(memberPath);
						Set<Constant> domainInternal2 = updateDomainMap.GetDomainInternal(memberPath2);
						domainInternal.Unite(domainInternal2.Where((Constant constant) => !constant.IsNull() && !(constant is NegatedConstant)));
						if (updateDomainMap.IsConditionMember(memberPath2) && !queryDomainMap.IsConditionMember(memberPath))
						{
							queryDomainMap.m_projectedConditionMembers.Add(memberPath);
						}
					}
				}
			}
			MemberDomainMap.ExpandNegationsInDomainMap(queryDomainMap.m_conditionDomainMap);
			MemberDomainMap.ExpandNegationsInDomainMap(queryDomainMap.m_nonConditionDomainMap);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000F7AD0 File Offset: 0x000F5CD0
		private static void ExpandNegationsInDomainMap(Dictionary<MemberPath, Set<Constant>> domainMap)
		{
			foreach (MemberPath memberPath in domainMap.Keys.ToArray<MemberPath>())
			{
				domainMap[memberPath] = Domain.ExpandNegationsInDomain(domainMap[memberPath]);
			}
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x000F7B0E File Offset: 0x000F5D0E
		internal bool IsConditionMember(MemberPath path)
		{
			return this.m_conditionDomainMap.ContainsKey(path);
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x000F7B1C File Offset: 0x000F5D1C
		internal IEnumerable<MemberPath> ConditionMembers(EntitySetBase extent)
		{
			foreach (MemberPath memberPath in this.m_conditionDomainMap.Keys)
			{
				if (memberPath.Extent.Equals(extent))
				{
					yield return memberPath;
				}
			}
			Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator enumerator = default(Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000F7B33 File Offset: 0x000F5D33
		internal IEnumerable<MemberPath> NonConditionMembers(EntitySetBase extent)
		{
			foreach (MemberPath memberPath in this.m_nonConditionDomainMap.Keys)
			{
				if (memberPath.Extent.Equals(extent))
				{
					yield return memberPath;
				}
			}
			Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator enumerator = default(Dictionary<MemberPath, Set<Constant>>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000F7B4A File Offset: 0x000F5D4A
		internal void AddSentinel(MemberPath path)
		{
			this.GetDomainInternal(path).Add(Constant.AllOtherConstants);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000F7B5D File Offset: 0x000F5D5D
		internal void RemoveSentinel(MemberPath path)
		{
			this.GetDomainInternal(path).Remove(Constant.AllOtherConstants);
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000F7B70 File Offset: 0x000F5D70
		internal IEnumerable<Constant> GetDomain(MemberPath path)
		{
			return this.GetDomainInternal(path);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000F7B7C File Offset: 0x000F5D7C
		private Set<Constant> GetDomainInternal(MemberPath path)
		{
			Set<Constant> set;
			if (!this.m_conditionDomainMap.TryGetValue(path, out set))
			{
				set = this.m_nonConditionDomainMap[path];
			}
			return set;
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000F7BA7 File Offset: 0x000F5DA7
		internal void UpdateConditionMemberDomain(MemberPath path, IEnumerable<Constant> domainValues)
		{
			Set<Constant> set = this.m_conditionDomainMap[path];
			set.Clear();
			set.Unite(domainValues);
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000F7BC4 File Offset: 0x000F5DC4
		private void AddToDomainMap(MemberPath member, IEnumerable<Constant> domainValues)
		{
			Set<Constant> set;
			if (!this.m_conditionDomainMap.TryGetValue(member, out set))
			{
				set = new Set<Constant>(Constant.EqualityComparer);
			}
			set.Unite(domainValues);
			this.m_conditionDomainMap[member] = Domain.ExpandNegationsInDomain(set, set);
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000F7C08 File Offset: 0x000F5E08
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (MemberPath memberPath in this.m_conditionDomainMap.Keys)
			{
				builder.Append('(');
				memberPath.ToCompactString(builder);
				IEnumerable<Constant> domain = this.GetDomain(memberPath);
				builder.Append(": ");
				StringUtil.ToCommaSeparatedStringSorted(builder, domain);
				builder.Append(") ");
			}
		}

		// Token: 0x0400190F RID: 6415
		private readonly Dictionary<MemberPath, Set<Constant>> m_conditionDomainMap;

		// Token: 0x04001910 RID: 6416
		private readonly Dictionary<MemberPath, Set<Constant>> m_nonConditionDomainMap;

		// Token: 0x04001911 RID: 6417
		private readonly Set<MemberPath> m_projectedConditionMembers = new Set<MemberPath>();

		// Token: 0x04001912 RID: 6418
		private readonly EdmItemCollection m_edmItemCollection;

		// Token: 0x02000BD4 RID: 3028
		private class CellConstantSetInfo : Set<Constant>
		{
			// Token: 0x06006818 RID: 26648 RVA: 0x00162EF3 File Offset: 0x001610F3
			internal CellConstantSetInfo(Set<Constant> iconstants)
				: base(iconstants)
			{
			}
		}
	}
}
