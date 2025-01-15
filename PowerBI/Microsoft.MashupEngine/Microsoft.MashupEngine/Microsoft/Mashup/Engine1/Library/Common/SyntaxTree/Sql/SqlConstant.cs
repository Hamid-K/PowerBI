using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001200 RID: 4608
	internal sealed class SqlConstant : ScalarExpression
	{
		// Token: 0x0600797F RID: 31103 RVA: 0x001A3F79 File Offset: 0x001A2179
		public SqlConstant(ConstantType type, string literal)
		{
			this.type = type;
			this.literal = literal;
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x06007980 RID: 31104 RVA: 0x001A3F8F File Offset: 0x001A218F
		public string Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06007981 RID: 31105 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06007982 RID: 31106 RVA: 0x001A3F97 File Offset: 0x001A2197
		public ConstantType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x001A3FA0 File Offset: 0x001A21A0
		public override bool Equals(object obj)
		{
			SqlConstant sqlConstant = obj as SqlConstant;
			return sqlConstant != null && this.Type == sqlConstant.Type && this.Literal == sqlConstant.Literal;
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x001A3FDC File Offset: 0x001A21DC
		public override int GetHashCode()
		{
			if (this.Literal == null)
			{
				return this.Type.GetHashCode();
			}
			return this.Type.GetHashCode() ^ this.Literal.GetHashCode();
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x001A4026 File Offset: 0x001A2226
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteLiteral(this.Type, this.Literal);
		}

		// Token: 0x0400422F RID: 16943
		public static readonly SqlConstant Null = new SqlConstant(ConstantType.Null, null);

		// Token: 0x04004230 RID: 16944
		public static readonly SqlConstant BooleanTrue = new SqlConstant(ConstantType.Boolean, "true");

		// Token: 0x04004231 RID: 16945
		public static readonly SqlConstant BooleanFalse = new SqlConstant(ConstantType.Boolean, "false");

		// Token: 0x04004232 RID: 16946
		public static readonly SqlConstant NumericTrue = new SqlConstant(ConstantType.Integer, "1");

		// Token: 0x04004233 RID: 16947
		public static readonly SqlConstant NumericFalse = new SqlConstant(ConstantType.Integer, "0");

		// Token: 0x04004234 RID: 16948
		public static readonly SqlConstant StringTrue = new SqlConstant(ConstantType.AnsiString, "true");

		// Token: 0x04004235 RID: 16949
		public static readonly SqlConstant StringFalse = new SqlConstant(ConstantType.AnsiString, "false");

		// Token: 0x04004236 RID: 16950
		public static readonly SqlConstant MinusOne = new SqlConstant(ConstantType.Integer, "-1");

		// Token: 0x04004237 RID: 16951
		public static readonly SqlConstant Zero = new SqlConstant(ConstantType.Integer, "0");

		// Token: 0x04004238 RID: 16952
		public static readonly SqlConstant One = new SqlConstant(ConstantType.Integer, "1");

		// Token: 0x04004239 RID: 16953
		public static readonly SqlConstant Two = new SqlConstant(ConstantType.Integer, "2");

		// Token: 0x0400423A RID: 16954
		public static readonly SqlConstant Three = new SqlConstant(ConstantType.Integer, "3");

		// Token: 0x0400423B RID: 16955
		public static readonly SqlConstant Four = new SqlConstant(ConstantType.Integer, "4");

		// Token: 0x0400423C RID: 16956
		public static readonly SqlConstant Five = new SqlConstant(ConstantType.Integer, "5");

		// Token: 0x0400423D RID: 16957
		public static readonly SqlConstant Six = new SqlConstant(ConstantType.Integer, "6");

		// Token: 0x0400423E RID: 16958
		public static readonly SqlConstant Seven = new SqlConstant(ConstantType.Integer, "7");

		// Token: 0x0400423F RID: 16959
		public static readonly SqlConstant Ten = new SqlConstant(ConstantType.Integer, "10");

		// Token: 0x04004240 RID: 16960
		public static readonly SqlConstant Twelve = new SqlConstant(ConstantType.Integer, "12");

		// Token: 0x04004241 RID: 16961
		public static readonly SqlConstant Hundred = new SqlConstant(ConstantType.Integer, "100");

		// Token: 0x04004242 RID: 16962
		public static readonly SqlConstant TenThousand = new SqlConstant(ConstantType.Integer, "10000");

		// Token: 0x04004243 RID: 16963
		public static readonly SqlConstant Million = new SqlConstant(ConstantType.Integer, "1000000");

		// Token: 0x04004244 RID: 16964
		public static readonly SqlConstant EmptyString = new SqlConstant(ConstantType.AnsiString, "");

		// Token: 0x04004245 RID: 16965
		public static readonly SqlConstant SelectAll = new SqlConstant(ConstantType.AnsiString, "*");

		// Token: 0x04004246 RID: 16966
		public static readonly SqlConstant MinutesPerHour = new SqlConstant(ConstantType.Integer, "60");

		// Token: 0x04004247 RID: 16967
		public static readonly SqlConstant SecondsPerMinute = new SqlConstant(ConstantType.Integer, "60");

		// Token: 0x04004248 RID: 16968
		public static readonly SqlConstant SecondsPerHour = new SqlConstant(ConstantType.Integer, "3600");

		// Token: 0x04004249 RID: 16969
		public static readonly SqlConstant SecondsPerDay = new SqlConstant(ConstantType.Integer, "86400");

		// Token: 0x0400424A RID: 16970
		private static readonly IEnumerable<int> UnicodeZsCodePoints = new int[]
		{
			32, 160, 5760, 8192, 8193, 8194, 8195, 8196, 8197, 8198,
			8199, 8200, 8201, 8202, 8239, 8287, 12288
		};

		// Token: 0x0400424B RID: 16971
		private static readonly IEnumerable<int> AdditionalWsCodePoints = new int[] { 9, 10, 11, 12, 13, 133, 8232, 8233 };

		// Token: 0x0400424C RID: 16972
		public static readonly IEnumerable<int> MLanguageWhitespaceCodePoints = SqlConstant.UnicodeZsCodePoints.Concat(SqlConstant.AdditionalWsCodePoints);

		// Token: 0x0400424D RID: 16973
		private readonly ConstantType type;

		// Token: 0x0400424E RID: 16974
		private readonly string literal;
	}
}
