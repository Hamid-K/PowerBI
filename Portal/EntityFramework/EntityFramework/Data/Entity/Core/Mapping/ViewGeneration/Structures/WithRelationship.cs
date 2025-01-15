using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005B5 RID: 1461
	internal sealed class WithRelationship : InternalBase
	{
		// Token: 0x060046FA RID: 18170 RVA: 0x000FAC9C File Offset: 0x000F8E9C
		internal WithRelationship(AssociationSet associationSet, AssociationEndMember fromEnd, EntityType fromEndEntityType, AssociationEndMember toEnd, EntityType toEndEntityType, IEnumerable<MemberPath> toEndEntityKeyMemberPaths)
		{
			this.m_associationSet = associationSet;
			this.m_fromEnd = fromEnd;
			this.m_fromEndEntityType = fromEndEntityType;
			this.m_toEnd = toEnd;
			this.m_toEndEntityType = toEndEntityType;
			this.m_toEndEntitySet = MetadataHelper.GetEntitySetAtEnd(associationSet, toEnd);
			this.m_toEndEntityKeyMemberPaths = toEndEntityKeyMemberPaths;
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x000FACEA File Offset: 0x000F8EEA
		internal EntityType FromEndEntityType
		{
			get
			{
				return this.m_fromEndEntityType;
			}
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x000FACF4 File Offset: 0x000F8EF4
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel + 1);
			builder.Append("RELATIONSHIP(");
			List<string> list = new List<string>();
			builder.Append("CREATEREF(");
			CqlWriter.AppendEscapedQualifiedName(builder, this.m_toEndEntitySet.EntityContainer.Name, this.m_toEndEntitySet.Name);
			builder.Append(", ROW(");
			foreach (MemberPath memberPath in this.m_toEndEntityKeyMemberPaths)
			{
				string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, memberPath.CqlFieldAlias);
				list.Add(qualifiedName);
			}
			StringUtil.ToSeparatedString(builder, list, ", ", null);
			builder.Append(')');
			builder.Append(",");
			CqlWriter.AppendEscapedTypeName(builder, this.m_toEndEntityType);
			builder.Append(')');
			builder.Append(',');
			CqlWriter.AppendEscapedTypeName(builder, this.m_associationSet.ElementType);
			builder.Append(',');
			CqlWriter.AppendEscapedName(builder, this.m_fromEnd.Name);
			builder.Append(',');
			CqlWriter.AppendEscapedName(builder, this.m_toEnd.Name);
			builder.Append(')');
			builder.Append(' ');
			return builder;
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x000FAE3C File Offset: 0x000F903C
		internal DbRelatedEntityRef AsCqt(DbExpression row)
		{
			return DbExpressionBuilder.CreateRelatedEntityRef(this.m_fromEnd, this.m_toEnd, this.m_toEndEntitySet.CreateRef(this.m_toEndEntityType, this.m_toEndEntityKeyMemberPaths.Select((MemberPath keyMember) => row.Property(keyMember.CqlFieldAlias))));
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x000FAE8F File Offset: 0x000F908F
		internal override void ToCompactString(StringBuilder builder)
		{
		}

		// Token: 0x0400192D RID: 6445
		private readonly AssociationSet m_associationSet;

		// Token: 0x0400192E RID: 6446
		private readonly RelationshipEndMember m_fromEnd;

		// Token: 0x0400192F RID: 6447
		private readonly EntityType m_fromEndEntityType;

		// Token: 0x04001930 RID: 6448
		private readonly RelationshipEndMember m_toEnd;

		// Token: 0x04001931 RID: 6449
		private readonly EntityType m_toEndEntityType;

		// Token: 0x04001932 RID: 6450
		private readonly EntitySet m_toEndEntitySet;

		// Token: 0x04001933 RID: 6451
		private readonly IEnumerable<MemberPath> m_toEndEntityKeyMemberPaths;
	}
}
