using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005AF RID: 1455
	internal sealed class ScalarConstant : Constant
	{
		// Token: 0x060046C6 RID: 18118 RVA: 0x000FA0C0 File Offset: 0x000F82C0
		internal ScalarConstant(object value)
		{
			this.m_scalar = value;
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x000FA0CF File Offset: 0x000F82CF
		internal object Value
		{
			get
			{
				return this.m_scalar;
			}
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000FA0D7 File Offset: 0x000F82D7
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000FA0DA File Offset: 0x000F82DA
		internal override bool IsNotNull()
		{
			return false;
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000FA0DD File Offset: 0x000F82DD
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x000FA0E0 File Offset: 0x000F82E0
		internal override bool HasNotNull()
		{
			return false;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x000FA0E4 File Offset: 0x000F82E4
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			EdmType edmType = modelTypeUsage.EdmType;
			if (BuiltInTypeKind.PrimitiveType == edmType.BuiltInTypeKind)
			{
				PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)edmType).PrimitiveTypeKind;
				if (primitiveTypeKind == PrimitiveTypeKind.Boolean)
				{
					bool flag = (bool)this.m_scalar;
					string text = StringUtil.FormatInvariant("{0}", new object[] { flag });
					builder.Append(text);
					return builder;
				}
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					bool flag2;
					if (!TypeHelpers.TryGetIsUnicode(modelTypeUsage, out flag2))
					{
						flag2 = true;
					}
					if (flag2)
					{
						builder.Append('N');
					}
					this.AppendEscapedScalar(builder);
					return builder;
				}
			}
			else if (BuiltInTypeKind.EnumType == edmType.BuiltInTypeKind)
			{
				EnumMember enumMember = (EnumMember)this.m_scalar;
				builder.Append(enumMember.Name);
				return builder;
			}
			builder.Append("CAST(");
			this.AppendEscapedScalar(builder);
			builder.Append(" AS ");
			CqlWriter.AppendEscapedTypeName(builder, edmType);
			builder.Append(')');
			return builder;
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x000FA1D4 File Offset: 0x000F83D4
		private StringBuilder AppendEscapedScalar(StringBuilder builder)
		{
			string text = StringUtil.FormatInvariant("{0}", new object[] { this.m_scalar });
			if (text.Contains("'"))
			{
				text = text.Replace("'", "''");
			}
			StringUtil.FormatStringBuilder(builder, "'{0}'", new object[] { text });
			return builder;
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x000FA230 File Offset: 0x000F8430
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return Helper.GetModelTypeUsage(outputMember.LeafEdmMember).Constant(this.m_scalar);
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000FA248 File Offset: 0x000F8448
		protected override bool IsEqualTo(Constant right)
		{
			ScalarConstant scalarConstant = right as ScalarConstant;
			return scalarConstant != null && ByValueEqualityComparer.Default.Equals(this.m_scalar, scalarConstant.m_scalar);
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x000FA277 File Offset: 0x000F8477
		public override int GetHashCode()
		{
			return this.m_scalar.GetHashCode();
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x000FA284 File Offset: 0x000F8484
		internal override string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x000FA2A4 File Offset: 0x000F84A4
		internal override void ToCompactString(StringBuilder builder)
		{
			EnumMember enumMember = this.m_scalar as EnumMember;
			if (enumMember != null)
			{
				builder.Append(enumMember.Name);
				return;
			}
			builder.Append(StringUtil.FormatInvariant("'{0}'", new object[] { this.m_scalar }));
		}

		// Token: 0x04001928 RID: 6440
		private readonly object m_scalar;
	}
}
