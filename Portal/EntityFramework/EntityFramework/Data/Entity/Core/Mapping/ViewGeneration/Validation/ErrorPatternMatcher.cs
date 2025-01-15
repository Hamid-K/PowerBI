using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200057C RID: 1404
	internal class ErrorPatternMatcher
	{
		// Token: 0x060043E6 RID: 17382 RVA: 0x000ED204 File Offset: 0x000EB404
		private ErrorPatternMatcher(ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog)
		{
			this.m_viewgenContext = context;
			this.m_domainMap = domainMap;
			MemberPath.GetKeyMembers(context.Extent, domainMap);
			this.m_errorLog = errorLog;
			this.m_originalErrorCount = this.m_errorLog.Count;
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000ED240 File Offset: 0x000EB440
		public static bool FindMappingErrors(ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog)
		{
			if (context.ViewTarget == ViewTarget.QueryView && !context.Config.IsValidationEnabled)
			{
				return false;
			}
			ErrorPatternMatcher errorPatternMatcher = new ErrorPatternMatcher(context, domainMap, errorLog);
			errorPatternMatcher.MatchMissingMappingErrors();
			errorPatternMatcher.MatchConditionErrors();
			errorPatternMatcher.MatchSplitErrors();
			if (errorPatternMatcher.m_errorLog.Count == errorPatternMatcher.m_originalErrorCount)
			{
				errorPatternMatcher.MatchPartitionErrors();
			}
			if (errorPatternMatcher.m_errorLog.Count > errorPatternMatcher.m_originalErrorCount)
			{
				ExceptionHelpers.ThrowMappingException(errorPatternMatcher.m_errorLog, errorPatternMatcher.m_viewgenContext.Config);
			}
			return false;
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000ED2C4 File Offset: 0x000EB4C4
		private void MatchMissingMappingErrors()
		{
			if (this.m_viewgenContext.ViewTarget == ViewTarget.QueryView)
			{
				Set<EdmType> set = new Set<EdmType>(MetadataHelper.GetTypeAndSubtypesOf(this.m_viewgenContext.Extent.ElementType, this.m_viewgenContext.EdmItemCollection, false));
				foreach (LeftCellWrapper leftCellWrapper in this.m_viewgenContext.AllWrappersForExtent)
				{
					foreach (Cell cell in leftCellWrapper.Cells)
					{
						foreach (MemberRestriction memberRestriction in cell.CQuery.Conditions)
						{
							foreach (Constant constant in memberRestriction.Domain.Values)
							{
								TypeConstant typeConstant = constant as TypeConstant;
								if (typeConstant != null)
								{
									set.Remove(typeConstant.EdmType);
								}
							}
						}
					}
				}
				if (set.Count > 0)
				{
					this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternMissingMappingError, Strings.ViewGen_Missing_Type_Mapping(ErrorPatternMatcher.BuildCommaSeparatedErrorString<EdmType>(set)), this.m_viewgenContext.AllWrappersForExtent, ""));
				}
			}
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x000ED450 File Offset: 0x000EB650
		private static bool HasNotNullCondition(CellQuery cellQuery, MemberPath member)
		{
			foreach (MemberRestriction memberRestriction in cellQuery.GetConjunctsFromWhereClause())
			{
				if (memberRestriction.RestrictedMemberSlot.MemberPath.Equals(member))
				{
					if (memberRestriction.Domain.Values.Contains(Constant.NotNull))
					{
						return true;
					}
					using (IEnumerator<NegatedConstant> enumerator2 = (from cellConstant in memberRestriction.Domain.Values
						select cellConstant as NegatedConstant into negated
						where negated != null
						select negated).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.Elements.Contains(Constant.Null))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x000ED56C File Offset: 0x000EB76C
		private static bool IsMemberPartOfNotNullCondition(IEnumerable<LeftCellWrapper> wrappers, MemberPath leftMember, ViewTarget viewTarget)
		{
			Func<MemberPath, bool> <>9__0;
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				CellQuery leftQuery = leftCellWrapper.OnlyInputCell.GetLeftQuery(viewTarget);
				if (ErrorPatternMatcher.HasNotNullCondition(leftQuery, leftMember))
				{
					return true;
				}
				CellQuery rightQuery = leftCellWrapper.OnlyInputCell.GetRightQuery(viewTarget);
				IEnumerable<MemberPath> projectedMembers = leftQuery.GetProjectedMembers();
				Func<MemberPath, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (MemberPath path) => !path.Equals(leftMember));
				}
				int num = projectedMembers.TakeWhile(func).Count<MemberPath>();
				if (num < leftQuery.GetProjectedMembers().Count<MemberPath>())
				{
					MemberPath memberPath = ((MemberProjectedSlot)rightQuery.ProjectedSlotAt(num)).MemberPath;
					if (ErrorPatternMatcher.HasNotNullCondition(rightQuery, memberPath))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x000ED65C File Offset: 0x000EB85C
		private void MatchConditionErrors()
		{
			List<LeftCellWrapper> allWrappersForExtent = this.m_viewgenContext.AllWrappersForExtent;
			Set<MemberPath> set = new Set<MemberPath>();
			Set<Dictionary<MemberPath, Set<Constant>>> set2 = new Set<Dictionary<MemberPath, Set<Constant>>>(new ConditionComparer());
			Dictionary<Dictionary<MemberPath, Set<Constant>>, LeftCellWrapper> dictionary = new Dictionary<Dictionary<MemberPath, Set<Constant>>, LeftCellWrapper>(new ConditionComparer());
			foreach (LeftCellWrapper leftCellWrapper in allWrappersForExtent)
			{
				Dictionary<MemberPath, Set<Constant>> dictionary2 = new Dictionary<MemberPath, Set<Constant>>();
				foreach (MemberRestriction memberRestriction in leftCellWrapper.OnlyInputCell.GetLeftQuery(this.m_viewgenContext.ViewTarget).GetConjunctsFromWhereClause())
				{
					MemberPath memberPath = memberRestriction.RestrictedMemberSlot.MemberPath;
					if (this.m_domainMap.IsConditionMember(memberPath))
					{
						ScalarRestriction scalarRestriction = memberRestriction as ScalarRestriction;
						if (scalarRestriction != null && !set.Contains(memberPath) && !leftCellWrapper.OnlyInputCell.CQuery.WhereClause.Equals(leftCellWrapper.OnlyInputCell.SQuery.WhereClause) && !ErrorPatternMatcher.IsMemberPartOfNotNullCondition(allWrappersForExtent, memberPath, this.m_viewgenContext.ViewTarget))
						{
							this.CheckThatConditionMemberIsNotMapped(memberPath, allWrappersForExtent, set);
						}
						if (this.m_viewgenContext.ViewTarget == ViewTarget.UpdateView && scalarRestriction != null && memberPath.IsNullable && ErrorPatternMatcher.IsMemberPartOfNotNullCondition(new LeftCellWrapper[] { leftCellWrapper }, memberPath, this.m_viewgenContext.ViewTarget))
						{
							MemberPath rightMemberPath = ErrorPatternMatcher.GetRightMemberPath(memberPath, leftCellWrapper);
							if (rightMemberPath != null && rightMemberPath.IsNullable && !ErrorPatternMatcher.IsMemberPartOfNotNullCondition(new LeftCellWrapper[] { leftCellWrapper }, rightMemberPath, this.m_viewgenContext.ViewTarget))
							{
								this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(memberPath, rightMemberPath), leftCellWrapper.OnlyInputCell, ""));
							}
						}
						foreach (Constant constant in memberRestriction.Domain.Values)
						{
							Set<Constant> set3;
							if (!dictionary2.TryGetValue(memberPath, out set3))
							{
								set3 = new Set<Constant>(Constant.EqualityComparer);
								dictionary2.Add(memberPath, set3);
							}
							set3.Add(constant);
						}
					}
				}
				if (dictionary2.Count > 0)
				{
					if (set2.Contains(dictionary2))
					{
						if (!this.RightSideEqual(dictionary[dictionary2], leftCellWrapper))
						{
							this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_DuplicateConditionValue(ErrorPatternMatcher.BuildCommaSeparatedErrorString<MemberPath>(dictionary2.Keys)), ErrorPatternMatcher.ToIEnum(dictionary[dictionary2].OnlyInputCell, leftCellWrapper.OnlyInputCell), ""));
						}
					}
					else
					{
						set2.Add(dictionary2);
						dictionary.Add(dictionary2, leftCellWrapper);
					}
				}
			}
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000ED964 File Offset: 0x000EBB64
		private static MemberPath GetRightMemberPath(MemberPath conditionMember, LeftCellWrapper leftCellWrapper)
		{
			List<int> projectedPositions = leftCellWrapper.OnlyInputCell.GetRightQuery(ViewTarget.QueryView).GetProjectedPositions(conditionMember);
			if (projectedPositions.Count != 1)
			{
				return null;
			}
			int num = projectedPositions.First<int>();
			return ((MemberProjectedSlot)leftCellWrapper.OnlyInputCell.GetLeftQuery(ViewTarget.QueryView).ProjectedSlotAt(num)).MemberPath;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x000ED9B4 File Offset: 0x000EBBB4
		private void MatchSplitErrors()
		{
			IEnumerable<LeftCellWrapper> enumerable = this.m_viewgenContext.AllWrappersForExtent.Where((LeftCellWrapper r) => !(r.LeftExtent is AssociationSet) && !(r.RightCellQuery.Extent is AssociationSet));
			if (this.m_viewgenContext.ViewTarget == ViewTarget.UpdateView && enumerable.Any<LeftCellWrapper>())
			{
				LeftCellWrapper leftCellWrapper = enumerable.First<LeftCellWrapper>();
				EntitySetBase extent = leftCellWrapper.RightCellQuery.Extent;
				foreach (LeftCellWrapper leftCellWrapper2 in enumerable)
				{
					if (!leftCellWrapper2.RightCellQuery.Extent.EdmEquals(extent) && !this.RightSideEqual(leftCellWrapper2, leftCellWrapper))
					{
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternSplittingError, Strings.Viewgen_ErrorPattern_TableMappedToMultipleES(leftCellWrapper2.LeftExtent.ToString(), leftCellWrapper2.RightCellQuery.Extent.ToString(), extent.ToString()), leftCellWrapper2.Cells.First<Cell>(), ""));
					}
				}
			}
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x000EDAC4 File Offset: 0x000EBCC4
		private void MatchPartitionErrors()
		{
			List<LeftCellWrapper> allWrappersForExtent = this.m_viewgenContext.AllWrappersForExtent;
			int num = 0;
			foreach (LeftCellWrapper leftCellWrapper in allWrappersForExtent)
			{
				foreach (LeftCellWrapper leftCellWrapper2 in allWrappersForExtent.Skip(++num))
				{
					FragmentQuery fragmentQuery = this.CreateRightFragmentQuery(leftCellWrapper);
					FragmentQuery fragmentQuery2 = this.CreateRightFragmentQuery(leftCellWrapper2);
					bool flag = this.CompareS(ErrorPatternMatcher.ComparisonOP.IsDisjointFrom, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
					bool flag2 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsDisjointFrom, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
					bool flag3;
					bool flag4;
					bool flag5;
					if (flag)
					{
						if (flag2)
						{
							continue;
						}
						flag3 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
						flag4 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper2, leftCellWrapper, fragmentQuery2, fragmentQuery);
						flag5 = flag3 && flag4;
						StringBuilder stringBuilder = new StringBuilder();
						if (flag5)
						{
							stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Eq);
						}
						else if (flag3 || flag4)
						{
							if (this.CSideHasDifferentEntitySets(leftCellWrapper, leftCellWrapper2))
							{
								stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Subs_Ref);
							}
							else
							{
								stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Subs);
							}
						}
						else
						{
							stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Unk);
						}
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder.ToString(), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
						if (this.FoundTooManyErrors())
						{
							return;
						}
					}
					else
					{
						flag3 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
						flag4 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper2, leftCellWrapper, fragmentQuery2, fragmentQuery);
					}
					bool flag6 = this.CompareS(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
					bool flag7 = this.CompareS(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper2, leftCellWrapper, fragmentQuery2, fragmentQuery);
					flag5 = flag3 && flag4;
					if (flag6 && flag7)
					{
						if (!flag5)
						{
							StringBuilder stringBuilder2 = new StringBuilder();
							if (flag2)
							{
								stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Disj);
							}
							else if (flag3 || flag4)
							{
								if (this.CSideHasDifferentEntitySets(leftCellWrapper, leftCellWrapper2))
								{
									stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Subs_Ref);
								}
								else
								{
									if (leftCellWrapper.LeftExtent.Equals(leftCellWrapper2.LeftExtent))
									{
										bool flag8;
										List<EdmType> list;
										ErrorPatternMatcher.GetTypesAndConditionForWrapper(leftCellWrapper, out flag8, out list);
										bool flag9;
										List<EdmType> list2;
										ErrorPatternMatcher.GetTypesAndConditionForWrapper(leftCellWrapper2, out flag9, out list2);
										if (!flag8 && !flag9 && (list.Except(list2).Count<EdmType>() != 0 || list2.Except(list).Count<EdmType>() != 0) && (!ErrorPatternMatcher.CheckForStoreConditions(leftCellWrapper) || !ErrorPatternMatcher.CheckForStoreConditions(leftCellWrapper2)))
										{
											IEnumerable<string> enumerable = list.Select((EdmType it) => it.FullName).Union(list2.Select((EdmType it) => it.FullName));
											this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(StringUtil.ToCommaSeparatedString(enumerable), leftCellWrapper.LeftExtent), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
											return;
										}
									}
									stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Subs);
								}
							}
							else if (!this.IsQueryView() && (leftCellWrapper.OnlyInputCell.CQuery.Extent is AssociationSet || leftCellWrapper2.OnlyInputCell.CQuery.Extent is AssociationSet))
							{
								stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Unk_Association);
							}
							else
							{
								stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Unk);
							}
							this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder2.ToString(), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
							if (this.FoundTooManyErrors())
							{
								return;
							}
						}
					}
					else if ((flag6 || flag7) && (!flag6 || !flag3 || flag4) && (!flag7 || !flag4 || flag3))
					{
						StringBuilder stringBuilder3 = new StringBuilder();
						if (flag2)
						{
							stringBuilder3.Append(Strings.Viewgen_ErrorPattern_Partition_Sub_Disj);
						}
						else if (flag5)
						{
							if (this.CSideHasDifferentEntitySets(leftCellWrapper, leftCellWrapper2))
							{
								stringBuilder3.Append(" " + Strings.Viewgen_ErrorPattern_Partition_Sub_Eq_Ref);
							}
							else
							{
								stringBuilder3.Append(Strings.Viewgen_ErrorPattern_Partition_Sub_Eq);
							}
						}
						else
						{
							stringBuilder3.Append(Strings.Viewgen_ErrorPattern_Partition_Sub_Unk);
						}
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder3.ToString(), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
						if (this.FoundTooManyErrors())
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x000EDFB4 File Offset: 0x000EC1B4
		private static void GetTypesAndConditionForWrapper(LeftCellWrapper wrapper, out bool hasCondition, out List<EdmType> edmTypes)
		{
			hasCondition = false;
			edmTypes = new List<EdmType>();
			foreach (Cell cell in wrapper.Cells)
			{
				foreach (MemberRestriction memberRestriction in cell.CQuery.Conditions)
				{
					foreach (Constant constant in memberRestriction.Domain.Values)
					{
						TypeConstant typeConstant = constant as TypeConstant;
						if (typeConstant != null)
						{
							edmTypes.Add(typeConstant.EdmType);
						}
						else
						{
							hasCondition = true;
						}
					}
				}
			}
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x000EE094 File Offset: 0x000EC294
		private static bool CheckForStoreConditions(LeftCellWrapper wrapper)
		{
			return wrapper.Cells.SelectMany((Cell c) => c.SQuery.Conditions).Any<MemberRestriction>();
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x000EE0C8 File Offset: 0x000EC2C8
		private void CheckThatConditionMemberIsNotMapped(MemberPath conditionMember, List<LeftCellWrapper> mappingFragments, Set<MemberPath> mappedConditionMembers)
		{
			foreach (LeftCellWrapper leftCellWrapper in mappingFragments)
			{
				foreach (Cell cell in leftCellWrapper.Cells)
				{
					if (cell.GetLeftQuery(this.m_viewgenContext.ViewTarget).GetProjectedMembers().Contains(conditionMember))
					{
						mappedConditionMembers.Add(conditionMember);
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_ConditionMemberIsMapped(conditionMember.ToString()), cell, ""));
					}
				}
			}
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000EE190 File Offset: 0x000EC390
		private bool FoundTooManyErrors()
		{
			return this.m_errorLog.Count > this.m_originalErrorCount + 5;
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000EE1A8 File Offset: 0x000EC3A8
		private static string BuildCommaSeparatedErrorString<T>(IEnumerable<T> members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			T t = members.First<T>();
			foreach (T t2 in members)
			{
				if (!t2.Equals(t))
				{
					stringBuilder.Append(", ");
				}
				StringBuilder stringBuilder2 = stringBuilder;
				string text = "'";
				T t3 = t2;
				stringBuilder2.Append(text + ((t3 != null) ? t3.ToString() : null) + "'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000EE250 File Offset: 0x000EC450
		private bool CSideHasDifferentEntitySets(LeftCellWrapper a, LeftCellWrapper b)
		{
			if (this.IsQueryView())
			{
				return a.LeftExtent == b.LeftExtent;
			}
			return a.RightCellQuery == b.RightCellQuery;
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000EE277 File Offset: 0x000EC477
		private bool CompareC(ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			return this.Compare(true, op, context, leftWrapper1, leftWrapper2, rightQuery1, rightQuery2);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000EE289 File Offset: 0x000EC489
		private bool CompareS(ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			return this.Compare(false, op, context, leftWrapper1, leftWrapper2, rightQuery1, rightQuery2);
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000EE29C File Offset: 0x000EC49C
		private bool Compare(bool lookingForC, ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			LCWComparer lcwcomparer;
			if ((lookingForC && this.IsQueryView()) || (!lookingForC && !this.IsQueryView()))
			{
				if (op == ErrorPatternMatcher.ComparisonOP.IsContainedIn)
				{
					lcwcomparer = new LCWComparer(context.LeftFragmentQP.IsContainedIn);
				}
				else
				{
					if (op != ErrorPatternMatcher.ComparisonOP.IsDisjointFrom)
					{
						return false;
					}
					lcwcomparer = new LCWComparer(context.LeftFragmentQP.IsDisjointFrom);
				}
				return lcwcomparer(leftWrapper1.FragmentQuery, leftWrapper2.FragmentQuery);
			}
			if (op == ErrorPatternMatcher.ComparisonOP.IsContainedIn)
			{
				lcwcomparer = new LCWComparer(context.RightFragmentQP.IsContainedIn);
			}
			else
			{
				if (op != ErrorPatternMatcher.ComparisonOP.IsDisjointFrom)
				{
					return false;
				}
				lcwcomparer = new LCWComparer(context.RightFragmentQP.IsDisjointFrom);
			}
			return lcwcomparer(rightQuery1, rightQuery2);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000EE340 File Offset: 0x000EC540
		private bool RightSideEqual(LeftCellWrapper wrapper1, LeftCellWrapper wrapper2)
		{
			FragmentQuery fragmentQuery = this.CreateRightFragmentQuery(wrapper1);
			FragmentQuery fragmentQuery2 = this.CreateRightFragmentQuery(wrapper2);
			return this.m_viewgenContext.RightFragmentQP.IsEquivalentTo(fragmentQuery, fragmentQuery2);
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000EE36F File Offset: 0x000EC56F
		private FragmentQuery CreateRightFragmentQuery(LeftCellWrapper wrapper)
		{
			return FragmentQuery.Create(wrapper.OnlyInputCell.CellLabel.ToString(), wrapper.CreateRoleBoolean(), wrapper.OnlyInputCell.GetRightQuery(this.m_viewgenContext.ViewTarget));
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x000EE3A2 File Offset: 0x000EC5A2
		private static IEnumerable<Cell> ToIEnum(Cell one, Cell two)
		{
			return new List<Cell> { one, two };
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x000EE3B7 File Offset: 0x000EC5B7
		private bool IsQueryView()
		{
			return this.m_viewgenContext.ViewTarget == ViewTarget.QueryView;
		}

		// Token: 0x04001882 RID: 6274
		private readonly ViewgenContext m_viewgenContext;

		// Token: 0x04001883 RID: 6275
		private readonly MemberDomainMap m_domainMap;

		// Token: 0x04001884 RID: 6276
		private readonly ErrorLog m_errorLog;

		// Token: 0x04001885 RID: 6277
		private readonly int m_originalErrorCount;

		// Token: 0x04001886 RID: 6278
		private const int NUM_PARTITION_ERR_TO_FIND = 5;

		// Token: 0x02000B86 RID: 2950
		private enum ComparisonOP
		{
			// Token: 0x04002E00 RID: 11776
			IsContainedIn,
			// Token: 0x04002E01 RID: 11777
			IsDisjointFrom
		}
	}
}
