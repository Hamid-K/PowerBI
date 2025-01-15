using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200059F RID: 1439
	internal abstract class Constant : InternalBase
	{
		// Token: 0x060045B6 RID: 17846
		internal abstract bool IsNull();

		// Token: 0x060045B7 RID: 17847
		internal abstract bool IsNotNull();

		// Token: 0x060045B8 RID: 17848
		internal abstract bool IsUndefined();

		// Token: 0x060045B9 RID: 17849
		internal abstract bool HasNotNull();

		// Token: 0x060045BA RID: 17850
		internal abstract StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias);

		// Token: 0x060045BB RID: 17851
		internal abstract DbExpression AsCqt(DbExpression row, MemberPath outputMember);

		// Token: 0x060045BC RID: 17852 RVA: 0x000F5EE4 File Offset: 0x000F40E4
		public override bool Equals(object obj)
		{
			Constant constant = obj as Constant;
			return constant != null && this.IsEqualTo(constant);
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x000F5F04 File Offset: 0x000F4104
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060045BE RID: 17854
		protected abstract bool IsEqualTo(Constant right);

		// Token: 0x060045BF RID: 17855
		internal abstract string ToUserString();

		// Token: 0x060045C0 RID: 17856 RVA: 0x000F5F0C File Offset: 0x000F410C
		internal static void ConstantsToUserString(StringBuilder builder, Set<Constant> constants)
		{
			bool flag = true;
			foreach (Constant constant in constants)
			{
				if (!flag)
				{
					builder.Append(Strings.ViewGen_CommaBlank);
				}
				flag = false;
				string text = constant.ToUserString();
				builder.Append(text);
			}
		}

		// Token: 0x040018F9 RID: 6393
		internal static readonly IEqualityComparer<Constant> EqualityComparer = new Constant.CellConstantComparer();

		// Token: 0x040018FA RID: 6394
		internal static readonly Constant Null = Constant.NullConstant.Instance;

		// Token: 0x040018FB RID: 6395
		internal static readonly Constant NotNull = new NegatedConstant(new Constant[] { Constant.NullConstant.Instance });

		// Token: 0x040018FC RID: 6396
		internal static readonly Constant Undefined = Constant.UndefinedConstant.Instance;

		// Token: 0x040018FD RID: 6397
		internal static readonly Constant AllOtherConstants = Constant.AllOtherConstantsConstant.Instance;

		// Token: 0x02000BC3 RID: 3011
		private class CellConstantComparer : IEqualityComparer<Constant>
		{
			// Token: 0x060067B2 RID: 26546 RVA: 0x00162443 File Offset: 0x00160643
			public bool Equals(Constant left, Constant right)
			{
				return left == right || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x060067B3 RID: 26547 RVA: 0x0016245A File Offset: 0x0016065A
			public int GetHashCode(Constant key)
			{
				return key.GetHashCode();
			}
		}

		// Token: 0x02000BC4 RID: 3012
		private sealed class NullConstant : Constant
		{
			// Token: 0x060067B5 RID: 26549 RVA: 0x0016246A File Offset: 0x0016066A
			private NullConstant()
			{
			}

			// Token: 0x060067B6 RID: 26550 RVA: 0x00162472 File Offset: 0x00160672
			internal override bool IsNull()
			{
				return true;
			}

			// Token: 0x060067B7 RID: 26551 RVA: 0x00162475 File Offset: 0x00160675
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x060067B8 RID: 26552 RVA: 0x00162478 File Offset: 0x00160678
			internal override bool IsUndefined()
			{
				return false;
			}

			// Token: 0x060067B9 RID: 26553 RVA: 0x0016247B File Offset: 0x0016067B
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x060067BA RID: 26554 RVA: 0x00162480 File Offset: 0x00160680
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				EdmType edmType = Helper.GetModelTypeUsage(outputMember.LeafEdmMember).EdmType;
				builder.Append("CAST(NULL AS ");
				CqlWriter.AppendEscapedTypeName(builder, edmType);
				builder.Append(')');
				return builder;
			}

			// Token: 0x060067BB RID: 26555 RVA: 0x001624BB File Offset: 0x001606BB
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				return TypeUsage.Create(Helper.GetModelTypeUsage(outputMember.LeafEdmMember).EdmType).Null();
			}

			// Token: 0x060067BC RID: 26556 RVA: 0x001624D7 File Offset: 0x001606D7
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x060067BD RID: 26557 RVA: 0x001624DA File Offset: 0x001606DA
			protected override bool IsEqualTo(Constant right)
			{
				return this == right;
			}

			// Token: 0x060067BE RID: 26558 RVA: 0x001624E0 File Offset: 0x001606E0
			internal override string ToUserString()
			{
				return Strings.ViewGen_Null;
			}

			// Token: 0x060067BF RID: 26559 RVA: 0x001624E7 File Offset: 0x001606E7
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("NULL");
			}

			// Token: 0x04002EA4 RID: 11940
			internal static readonly Constant Instance = new Constant.NullConstant();
		}

		// Token: 0x02000BC5 RID: 3013
		private sealed class UndefinedConstant : Constant
		{
			// Token: 0x060067C1 RID: 26561 RVA: 0x00162501 File Offset: 0x00160701
			private UndefinedConstant()
			{
			}

			// Token: 0x060067C2 RID: 26562 RVA: 0x00162509 File Offset: 0x00160709
			internal override bool IsNull()
			{
				return false;
			}

			// Token: 0x060067C3 RID: 26563 RVA: 0x0016250C File Offset: 0x0016070C
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x060067C4 RID: 26564 RVA: 0x0016250F File Offset: 0x0016070F
			internal override bool IsUndefined()
			{
				return true;
			}

			// Token: 0x060067C5 RID: 26565 RVA: 0x00162512 File Offset: 0x00160712
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x060067C6 RID: 26566 RVA: 0x00162515 File Offset: 0x00160715
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060067C7 RID: 26567 RVA: 0x0016251C File Offset: 0x0016071C
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060067C8 RID: 26568 RVA: 0x00162523 File Offset: 0x00160723
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x060067C9 RID: 26569 RVA: 0x00162526 File Offset: 0x00160726
			protected override bool IsEqualTo(Constant right)
			{
				return this == right;
			}

			// Token: 0x060067CA RID: 26570 RVA: 0x0016252C File Offset: 0x0016072C
			internal override string ToUserString()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060067CB RID: 26571 RVA: 0x00162533 File Offset: 0x00160733
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("?");
			}

			// Token: 0x04002EA5 RID: 11941
			internal static readonly Constant Instance = new Constant.UndefinedConstant();
		}

		// Token: 0x02000BC6 RID: 3014
		private sealed class AllOtherConstantsConstant : Constant
		{
			// Token: 0x060067CD RID: 26573 RVA: 0x0016254D File Offset: 0x0016074D
			private AllOtherConstantsConstant()
			{
			}

			// Token: 0x060067CE RID: 26574 RVA: 0x00162555 File Offset: 0x00160755
			internal override bool IsNull()
			{
				return false;
			}

			// Token: 0x060067CF RID: 26575 RVA: 0x00162558 File Offset: 0x00160758
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x060067D0 RID: 26576 RVA: 0x0016255B File Offset: 0x0016075B
			internal override bool IsUndefined()
			{
				return false;
			}

			// Token: 0x060067D1 RID: 26577 RVA: 0x0016255E File Offset: 0x0016075E
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x060067D2 RID: 26578 RVA: 0x00162561 File Offset: 0x00160761
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060067D3 RID: 26579 RVA: 0x00162568 File Offset: 0x00160768
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060067D4 RID: 26580 RVA: 0x0016256F File Offset: 0x0016076F
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x060067D5 RID: 26581 RVA: 0x00162572 File Offset: 0x00160772
			protected override bool IsEqualTo(Constant right)
			{
				return this == right;
			}

			// Token: 0x060067D6 RID: 26582 RVA: 0x00162578 File Offset: 0x00160778
			internal override string ToUserString()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060067D7 RID: 26583 RVA: 0x0016257F File Offset: 0x0016077F
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("AllOtherConstants");
			}

			// Token: 0x04002EA6 RID: 11942
			internal static readonly Constant Instance = new Constant.AllOtherConstantsConstant();
		}
	}
}
