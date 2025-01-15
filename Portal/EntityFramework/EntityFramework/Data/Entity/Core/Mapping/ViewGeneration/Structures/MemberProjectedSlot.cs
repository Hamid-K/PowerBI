using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A8 RID: 1448
	internal sealed class MemberProjectedSlot : ProjectedSlot
	{
		// Token: 0x0600465C RID: 18012 RVA: 0x000F88CD File Offset: 0x000F6ACD
		internal MemberProjectedSlot(MemberPath node)
		{
			this.m_memberPath = node;
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x000F88DC File Offset: 0x000F6ADC
		internal MemberPath MemberPath
		{
			get
			{
				return this.m_memberPath;
			}
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000F88E4 File Offset: 0x000F6AE4
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			TypeUsage typeUsage;
			if (this.NeedToCastCqlValue(outputMember, out typeUsage))
			{
				builder.Append("CAST(");
				this.m_memberPath.AsEsql(builder, blockAlias);
				builder.Append(" AS ");
				CqlWriter.AppendEscapedTypeName(builder, typeUsage.EdmType);
				builder.Append(')');
			}
			else
			{
				this.m_memberPath.AsEsql(builder, blockAlias);
			}
			return builder;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000F8948 File Offset: 0x000F6B48
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			DbExpression dbExpression = this.m_memberPath.AsCqt(row);
			TypeUsage typeUsage;
			if (this.NeedToCastCqlValue(outputMember, out typeUsage))
			{
				dbExpression = dbExpression.CastTo(typeUsage);
			}
			return dbExpression;
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000F8976 File Offset: 0x000F6B76
		private bool NeedToCastCqlValue(MemberPath outputMember, out TypeUsage outputMemberTypeUsage)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(this.m_memberPath.LeafEdmMember);
			outputMemberTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			return !modelTypeUsage.EdmType.Equals(outputMemberTypeUsage.EdmType);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000F89A9 File Offset: 0x000F6BA9
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_memberPath.ToCompactString(builder);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000F89B7 File Offset: 0x000F6BB7
		internal string ToUserString()
		{
			return this.m_memberPath.PathToString(new bool?(false));
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x000F89CC File Offset: 0x000F6BCC
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			MemberProjectedSlot memberProjectedSlot = right as MemberProjectedSlot;
			return memberProjectedSlot != null && MemberPath.EqualityComparer.Equals(this.m_memberPath, memberProjectedSlot.m_memberPath);
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x000F89FB File Offset: 0x000F6BFB
		protected override int GetHash()
		{
			return MemberPath.EqualityComparer.GetHashCode(this.m_memberPath);
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000F8A10 File Offset: 0x000F6C10
		internal MemberProjectedSlot RemapSlot(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberPath memberPath = null;
			if (remap.TryGetValue(this.MemberPath, out memberPath))
			{
				return new MemberProjectedSlot(memberPath);
			}
			return new MemberProjectedSlot(this.MemberPath);
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x000F8A44 File Offset: 0x000F6C44
		internal static List<MemberProjectedSlot> GetKeySlots(IEnumerable<MemberProjectedSlot> slots, MemberPath prefix)
		{
			EntitySet entitySet = prefix.EntitySet;
			List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(prefix, entitySet.ElementType);
			return MemberProjectedSlot.GetSlots(slots, keysForEntityType[0].KeyFields);
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x000F8A78 File Offset: 0x000F6C78
		internal static List<MemberProjectedSlot> GetSlots(IEnumerable<MemberProjectedSlot> slots, IEnumerable<MemberPath> members)
		{
			List<MemberProjectedSlot> list = new List<MemberProjectedSlot>();
			foreach (MemberPath memberPath in members)
			{
				MemberProjectedSlot slotForMember = MemberProjectedSlot.GetSlotForMember(Helpers.AsSuperTypeList<MemberProjectedSlot, ProjectedSlot>(slots), memberPath);
				if (slotForMember == null)
				{
					return null;
				}
				list.Add(slotForMember);
			}
			return list;
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x000F8AE0 File Offset: 0x000F6CE0
		internal static MemberProjectedSlot GetSlotForMember(IEnumerable<ProjectedSlot> slots, MemberPath member)
		{
			foreach (ProjectedSlot projectedSlot in slots)
			{
				MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)projectedSlot;
				if (MemberPath.EqualityComparer.Equals(memberProjectedSlot.MemberPath, member))
				{
					return memberProjectedSlot;
				}
			}
			return null;
		}

		// Token: 0x0400191A RID: 6426
		private readonly MemberPath m_memberPath;
	}
}
