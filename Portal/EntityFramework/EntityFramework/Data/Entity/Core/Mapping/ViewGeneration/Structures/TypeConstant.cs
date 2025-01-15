using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005B2 RID: 1458
	internal sealed class TypeConstant : Constant
	{
		// Token: 0x060046E2 RID: 18146 RVA: 0x000FA65C File Offset: 0x000F885C
		internal TypeConstant(EdmType type)
		{
			this.m_edmType = type;
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x000FA66B File Offset: 0x000F886B
		internal EdmType EdmType
		{
			get
			{
				return this.m_edmType;
			}
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000FA673 File Offset: 0x000F8873
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000FA676 File Offset: 0x000F8876
		internal override bool IsNotNull()
		{
			return false;
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x000FA679 File Offset: 0x000F8879
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000FA67C File Offset: 0x000F887C
		internal override bool HasNotNull()
		{
			return false;
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x000FA680 File Offset: 0x000F8880
		protected override bool IsEqualTo(Constant right)
		{
			TypeConstant typeConstant = right as TypeConstant;
			return typeConstant != null && this.m_edmType == typeConstant.m_edmType;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x000FA6A7 File Offset: 0x000F88A7
		public override int GetHashCode()
		{
			if (this.m_edmType == null)
			{
				return 0;
			}
			return this.m_edmType.GetHashCode();
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x000FA6C0 File Offset: 0x000F88C0
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			this.AsCql(delegate(EntitySet refScopeEntitySet, IList<MemberPath> keyMemberOutputPaths)
			{
				EntityType entityType = (EntityType)((RefType)outputMember.EdmType).ElementType;
				builder.Append("CreateRef(");
				CqlWriter.AppendEscapedQualifiedName(builder, refScopeEntitySet.EntityContainer.Name, refScopeEntitySet.Name);
				builder.Append(", row(");
				for (int i = 0; i < keyMemberOutputPaths.Count; i++)
				{
					if (i > 0)
					{
						builder.Append(", ");
					}
					string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, keyMemberOutputPaths[i].CqlFieldAlias);
					builder.Append(qualifiedName);
				}
				builder.Append("), ");
				CqlWriter.AppendEscapedTypeName(builder, entityType);
				builder.Append(')');
			}, delegate(IList<MemberPath> membersOutputPaths)
			{
				CqlWriter.AppendEscapedTypeName(builder, this.m_edmType);
				builder.Append('(');
				for (int j = 0; j < membersOutputPaths.Count; j++)
				{
					if (j > 0)
					{
						builder.Append(", ");
					}
					string qualifiedName2 = CqlWriter.GetQualifiedName(blockAlias, membersOutputPaths[j].CqlFieldAlias);
					builder.Append(qualifiedName2);
				}
				builder.Append(')');
			}, outputMember);
			return builder;
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x000FA71C File Offset: 0x000F891C
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			DbExpression cqt = null;
			Func<MemberPath, DbPropertyExpression> <>9__2;
			Func<MemberPath, DbPropertyExpression> <>9__3;
			this.AsCql(delegate(EntitySet refScopeEntitySet, IList<MemberPath> keyMemberOutputPaths)
			{
				EntityType entityType = (EntityType)((RefType)outputMember.EdmType).ElementType;
				EntityType entityType2 = entityType;
				Func<MemberPath, DbPropertyExpression> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (MemberPath km) => row.Property(km.CqlFieldAlias));
				}
				cqt = refScopeEntitySet.CreateRef(entityType2, keyMemberOutputPaths.Select(func));
			}, delegate(IList<MemberPath> membersOutputPaths)
			{
				TypeUsage typeUsage = TypeUsage.Create(this.m_edmType);
				Func<MemberPath, DbPropertyExpression> func2;
				if ((func2 = <>9__3) == null)
				{
					func2 = (<>9__3 = (MemberPath m) => row.Property(m.CqlFieldAlias));
				}
				cqt = typeUsage.New(membersOutputPaths.Select(func2));
			}, outputMember);
			return cqt;
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x000FA778 File Offset: 0x000F8978
		private void AsCql(Action<EntitySet, IList<MemberPath>> createRef, Action<IList<MemberPath>> createType, MemberPath outputMember)
		{
			EntitySet scopeOfRelationEnd = outputMember.GetScopeOfRelationEnd();
			if (scopeOfRelationEnd != null)
			{
				List<MemberPath> list = new List<MemberPath>(scopeOfRelationEnd.ElementType.KeyMembers.Select((EdmMember km) => new MemberPath(outputMember, km)));
				createRef(scopeOfRelationEnd, list);
				return;
			}
			List<MemberPath> list2 = new List<MemberPath>();
			foreach (object obj in Helper.GetAllStructuralMembers(this.m_edmType))
			{
				EdmMember edmMember = (EdmMember)obj;
				list2.Add(new MemberPath(outputMember, edmMember));
			}
			createType(list2);
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x000FA840 File Offset: 0x000F8A40
		internal override string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x000FA860 File Offset: 0x000F8A60
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_edmType.Name);
		}

		// Token: 0x04001929 RID: 6441
		private readonly EdmType m_edmType;
	}
}
