using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000582 RID: 1410
	internal class ViewKeyConstraint : KeyConstraint<ViewCellRelation, ViewCellSlot>
	{
		// Token: 0x0600442C RID: 17452 RVA: 0x000EFA11 File Offset: 0x000EDC11
		internal ViewKeyConstraint(ViewCellRelation relation, IEnumerable<ViewCellSlot> keySlots)
			: base(relation, keySlots, ProjectedSlot.EqualityComparer)
		{
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600442D RID: 17453 RVA: 0x000EFA20 File Offset: 0x000EDC20
		internal Cell Cell
		{
			get
			{
				return base.CellRelation.Cell;
			}
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000EFA30 File Offset: 0x000EDC30
		internal bool Implies(ViewKeyConstraint second)
		{
			if (base.CellRelation != second.CellRelation)
			{
				return false;
			}
			if (base.KeySlots.IsSubsetOf(second.KeySlots))
			{
				return true;
			}
			Set<ViewCellSlot> set = new Set<ViewCellSlot>(second.KeySlots);
			foreach (ViewCellSlot viewCellSlot in base.KeySlots)
			{
				bool flag = false;
				foreach (ViewCellSlot viewCellSlot2 in set)
				{
					if (ProjectedSlot.EqualityComparer.Equals(viewCellSlot.SSlot, viewCellSlot2.SSlot))
					{
						MemberPath memberPath = viewCellSlot.CSlot.MemberPath;
						MemberPath memberPath2 = viewCellSlot2.CSlot.MemberPath;
						if (MemberPath.EqualityComparer.Equals(memberPath, memberPath2) || memberPath.IsEquivalentViaRefConstraint(memberPath2))
						{
							set.Remove(viewCellSlot2);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x000EFB54 File Offset: 0x000EDD54
		internal static ErrorLog.Record GetErrorRecord(ViewKeyConstraint rightKeyConstraint)
		{
			List<ViewCellSlot> list = new List<ViewCellSlot>(rightKeyConstraint.KeySlots);
			EntitySetBase extent = list[0].SSlot.MemberPath.Extent;
			EntitySetBase extent2 = list[0].CSlot.MemberPath.Extent;
			MemberPath memberPath = new MemberPath(extent);
			MemberPath memberPath2 = new MemberPath(extent2);
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(memberPath, (EntityType)extent.ElementType);
			ExtentKey extentKey;
			if (extent2 is EntitySet)
			{
				extentKey = ExtentKey.GetPrimaryKeyForEntityType(memberPath2, (EntityType)extent2.ElementType);
			}
			else
			{
				extentKey = ExtentKey.GetKeyForRelationType(memberPath2, (AssociationType)extent2.ElementType);
			}
			string text = Strings.ViewGen_KeyConstraint_Violation(extent.Name, ViewCellSlot.SlotsToUserString(rightKeyConstraint.KeySlots, false), primaryKeyForEntityType.ToUserString(), extent2.Name, ViewCellSlot.SlotsToUserString(rightKeyConstraint.KeySlots, true), extentKey.ToUserString());
			string text2 = StringUtil.FormatInvariant("PROBLEM: Not implied {0}", new object[] { rightKeyConstraint });
			return new ErrorLog.Record(ViewGenErrorCode.KeyConstraintViolation, text, rightKeyConstraint.CellRelation.Cell, text2);
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x000EFC54 File Offset: 0x000EDE54
		internal static ErrorLog.Record GetErrorRecord(IEnumerable<ViewKeyConstraint> rightKeyConstraints)
		{
			ViewKeyConstraint viewKeyConstraint = null;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (ViewKeyConstraint viewKeyConstraint2 in rightKeyConstraints)
			{
				string text = ViewCellSlot.SlotsToUserString(viewKeyConstraint2.KeySlots, true);
				if (!flag)
				{
					stringBuilder.Append("; ");
				}
				flag = false;
				stringBuilder.Append(text);
				viewKeyConstraint = viewKeyConstraint2;
			}
			List<ViewCellSlot> list = new List<ViewCellSlot>(viewKeyConstraint.KeySlots);
			EntitySetBase extent = list[0].SSlot.MemberPath.Extent;
			EntitySetBase extent2 = list[0].CSlot.MemberPath.Extent;
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(new MemberPath(extent), (EntityType)extent.ElementType);
			string text2;
			if (extent2 is EntitySet)
			{
				text2 = Strings.ViewGen_KeyConstraint_Update_Violation_EntitySet(stringBuilder.ToString(), extent2.Name, primaryKeyForEntityType.ToUserString(), extent.Name);
			}
			else
			{
				AssociationEndMember endThatShouldBeMappedToKey = Helper.GetEndThatShouldBeMappedToKey(((AssociationSet)extent2).ElementType);
				if (endThatShouldBeMappedToKey != null)
				{
					text2 = Strings.ViewGen_AssociationEndShouldBeMappedToKey(endThatShouldBeMappedToKey.Name, extent.Name);
				}
				else
				{
					text2 = Strings.ViewGen_KeyConstraint_Update_Violation_AssociationSet(extent2.Name, primaryKeyForEntityType.ToUserString(), extent.Name);
				}
			}
			string text3 = StringUtil.FormatInvariant("PROBLEM: Not implied {0}", new object[] { viewKeyConstraint });
			return new ErrorLog.Record(ViewGenErrorCode.KeyConstraintUpdateViolation, text2, viewKeyConstraint.CellRelation.Cell, text3);
		}
	}
}
