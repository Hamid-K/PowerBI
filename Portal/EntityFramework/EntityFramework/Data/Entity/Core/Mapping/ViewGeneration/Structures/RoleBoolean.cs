using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000594 RID: 1428
	internal sealed class RoleBoolean : TrueFalseLiteral
	{
		// Token: 0x060044F5 RID: 17653 RVA: 0x000F3CB4 File Offset: 0x000F1EB4
		internal RoleBoolean(EntitySetBase extent)
		{
			this.m_metadataItem = extent;
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000F3CC3 File Offset: 0x000F1EC3
		internal RoleBoolean(AssociationSetEnd end)
		{
			this.m_metadataItem = end;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x000F3CD2 File Offset: 0x000F1ED2
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return null;
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x000F3CD5 File Offset: 0x000F1ED5
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return null;
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x000F3CD8 File Offset: 0x000F1ED8
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				builder.Append(Strings.ViewGen_AssociationSet_AsUserString(blockAlias, associationSetEnd.Name, associationSetEnd.ParentAssociationSet));
			}
			else
			{
				builder.Append(Strings.ViewGen_EntitySet_AsUserString(blockAlias, this.m_metadataItem.ToString()));
			}
			return builder;
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000F3D28 File Offset: 0x000F1F28
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				builder.Append(Strings.ViewGen_AssociationSet_AsUserString_Negated(blockAlias, associationSetEnd.Name, associationSetEnd.ParentAssociationSet));
			}
			else
			{
				builder.Append(Strings.ViewGen_EntitySet_AsUserString_Negated(blockAlias, this.m_metadataItem.ToString()));
			}
			return builder;
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000F3D78 File Offset: 0x000F1F78
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000F3D80 File Offset: 0x000F1F80
		protected override bool IsEqualTo(BoolLiteral right)
		{
			RoleBoolean roleBoolean = right as RoleBoolean;
			return roleBoolean != null && this.m_metadataItem == roleBoolean.m_metadataItem;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000F3DA7 File Offset: 0x000F1FA7
		public override int GetHashCode()
		{
			return this.m_metadataItem.GetHashCode();
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000F3DB4 File Offset: 0x000F1FB4
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return this;
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000F3DB8 File Offset: 0x000F1FB8
		internal override void ToCompactString(StringBuilder builder)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				string text = "InEnd:";
				AssociationSet parentAssociationSet = associationSetEnd.ParentAssociationSet;
				builder.Append(text + ((parentAssociationSet != null) ? parentAssociationSet.ToString() : null) + "_" + associationSetEnd.Name);
				return;
			}
			string text2 = "InSet:";
			MetadataItem metadataItem = this.m_metadataItem;
			builder.Append(text2 + ((metadataItem != null) ? metadataItem.ToString() : null));
		}

		// Token: 0x040018D0 RID: 6352
		private readonly MetadataItem m_metadataItem;
	}
}
