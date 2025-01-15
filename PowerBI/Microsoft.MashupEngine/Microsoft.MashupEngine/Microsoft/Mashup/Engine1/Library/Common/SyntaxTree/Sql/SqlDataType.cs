using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001204 RID: 4612
	internal sealed class SqlDataType : IScriptable
	{
		// Token: 0x06007993 RID: 31123 RVA: 0x001A4430 File Offset: 0x001A2630
		public SqlDataType(TypeValue typeValue)
		{
			this.typeValue = typeValue;
		}

		// Token: 0x06007994 RID: 31124 RVA: 0x001A443F File Offset: 0x001A263F
		public SqlDataType(TypeValue typeValue, ConstantSqlString typeName)
		{
			this.typeValue = typeValue.NewFacets(typeValue.Facets.AddNative(typeName.String, null, null));
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06007995 RID: 31125 RVA: 0x001A4467 File Offset: 0x001A2667
		public static SqlDataType BigInt
		{
			get
			{
				return SqlDataType.bigIntType;
			}
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x001A446E File Offset: 0x001A266E
		public static SqlDataType Bit
		{
			get
			{
				return SqlDataType.bitType;
			}
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06007997 RID: 31127 RVA: 0x001A4475 File Offset: 0x001A2675
		public static SqlDataType Bytea
		{
			get
			{
				return SqlDataType.byteaType;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06007998 RID: 31128 RVA: 0x001A447C File Offset: 0x001A267C
		public static SqlDataType Character
		{
			get
			{
				return SqlDataType.characterType;
			}
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06007999 RID: 31129 RVA: 0x001A4483 File Offset: 0x001A2683
		public static SqlDataType Date
		{
			get
			{
				return SqlDataType.dateType;
			}
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x0600799A RID: 31130 RVA: 0x001A448A File Offset: 0x001A268A
		public static SqlDataType DateTime
		{
			get
			{
				return SqlDataType.dateTimeType;
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x0600799B RID: 31131 RVA: 0x001A4491 File Offset: 0x001A2691
		public static SqlDataType DateTime2
		{
			get
			{
				return SqlDataType.dateTime2Type;
			}
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x0600799C RID: 31132 RVA: 0x001A4498 File Offset: 0x001A2698
		public static SqlDataType DateTimeOffset
		{
			get
			{
				return SqlDataType.dateTimeOffset;
			}
		}

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x0600799D RID: 31133 RVA: 0x001A449F File Offset: 0x001A269F
		public static SqlDataType Decimal
		{
			get
			{
				return SqlDataType.decimalType;
			}
		}

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x0600799E RID: 31134 RVA: 0x001A44A6 File Offset: 0x001A26A6
		public static SqlDataType Float
		{
			get
			{
				return SqlDataType.floatType;
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x0600799F RID: 31135 RVA: 0x001A44AD File Offset: 0x001A26AD
		public static SqlDataType Int
		{
			get
			{
				return SqlDataType.intType;
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x060079A0 RID: 31136 RVA: 0x001A44B4 File Offset: 0x001A26B4
		public static SqlDataType Money
		{
			get
			{
				return SqlDataType.moneyType;
			}
		}

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x060079A1 RID: 31137 RVA: 0x001A44BB File Offset: 0x001A26BB
		public static SqlDataType NVarChar
		{
			get
			{
				return SqlDataType.nVarCharType;
			}
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x060079A2 RID: 31138 RVA: 0x001A44C2 File Offset: 0x001A26C2
		public static SqlDataType Real
		{
			get
			{
				return SqlDataType.realType;
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x060079A3 RID: 31139 RVA: 0x001A44C9 File Offset: 0x001A26C9
		public static SqlDataType SmallInt
		{
			get
			{
				return SqlDataType.smallIntType;
			}
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x060079A4 RID: 31140 RVA: 0x001A44D0 File Offset: 0x001A26D0
		public static SqlDataType SmallMoney
		{
			get
			{
				return SqlDataType.smallMoneyType;
			}
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x060079A5 RID: 31141 RVA: 0x001A44D7 File Offset: 0x001A26D7
		public static SqlDataType Time
		{
			get
			{
				return SqlDataType.time;
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x060079A6 RID: 31142 RVA: 0x001A44DE File Offset: 0x001A26DE
		public static SqlDataType Timestamp
		{
			get
			{
				return SqlDataType.timestamp;
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x060079A7 RID: 31143 RVA: 0x001A44E5 File Offset: 0x001A26E5
		public static SqlDataType TinyInt
		{
			get
			{
				return SqlDataType.tinyIntType;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x060079A8 RID: 31144 RVA: 0x001A44EC File Offset: 0x001A26EC
		public static SqlDataType VarBinary
		{
			get
			{
				return SqlDataType.varBinaryType;
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x060079A9 RID: 31145 RVA: 0x001A44F3 File Offset: 0x001A26F3
		public static SqlDataType VarChar
		{
			get
			{
				return SqlDataType.varCharType;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x060079AA RID: 31146 RVA: 0x001A44FA File Offset: 0x001A26FA
		public ConstantSqlString Type
		{
			get
			{
				return new ConstantSqlString(this.typeValue.Facets.NativeTypeName);
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x060079AB RID: 31147 RVA: 0x001A4511 File Offset: 0x001A2711
		public TypeValue TypeValue
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x001A451C File Offset: 0x001A271C
		public override bool Equals(object obj)
		{
			SqlDataType sqlDataType = obj as SqlDataType;
			if (sqlDataType == null)
			{
				return false;
			}
			TypeFacets facets = this.typeValue.Facets;
			TypeFacets facets2 = sqlDataType.typeValue.Facets;
			int? num = facets.NumericPrecision;
			int? num2 = facets2.NumericPrecision;
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				num2 = facets.NumericScale;
				num = facets2.NumericScale;
				if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
				{
					long? maxLength = facets.MaxLength;
					long? maxLength2 = facets2.MaxLength;
					if ((maxLength.GetValueOrDefault() == maxLength2.GetValueOrDefault()) & (maxLength != null == (maxLength2 != null)))
					{
						return string.Compare(SqlDataType.RemoveRedundantWhiteSpaces(this.Type.String.Trim()), SqlDataType.RemoveRedundantWhiteSpaces(sqlDataType.Type.String.Trim()), StringComparison.OrdinalIgnoreCase) == 0;
					}
				}
			}
			return false;
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x00002E09 File Offset: 0x00001009
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x001A4624 File Offset: 0x001A2824
		public void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(this.Type);
			string text = this.Type.String.Trim().ToLowerInvariant();
			if (SqlDataType.typesWithPrecisionAndScale.Contains(text))
			{
				this.WriteTypeWithPrecisionAndScale(writer);
				return;
			}
			text = SqlDataType.RemoveRedundantWhiteSpaces(text);
			if (writer.Settings.TypesWithLength.Contains(text))
			{
				ValueKind typeKind = this.typeValue.TypeKind;
				if (typeKind - ValueKind.Time > 2)
				{
					if (typeKind - ValueKind.Text <= 1)
					{
						if (this.typeValue.Facets.MaxLength != null)
						{
							writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
							writer.WriteLong(this.typeValue.Facets.MaxLength.Value);
							writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
							return;
						}
						if (writer.Settings.UseMaxTypes)
						{
							writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
							writer.Write(writer.Settings.MaxTypesLiteral(text));
							writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
							return;
						}
					}
				}
				else if (this.typeValue.Facets.DateTimePrecision != null)
				{
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteInt(this.TypeValue.Facets.DateTimePrecision.Value);
					writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				}
			}
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x001A4780 File Offset: 0x001A2980
		private static string RemoveRedundantWhiteSpaces(string value)
		{
			int length = value.Length;
			char[] array = new char[length];
			int num = 0;
			bool flag = false;
			int i;
			for (i = 0; i < length; i++)
			{
				bool flag2 = flag;
				flag = char.IsWhiteSpace(value, i);
				if (!flag || !flag2)
				{
					array[num++] = value[i];
				}
			}
			if (i != num)
			{
				return new string(array, 0, num);
			}
			return value;
		}

		// Token: 0x060079B0 RID: 31152 RVA: 0x001A47E0 File Offset: 0x001A29E0
		private void WriteTypeWithPrecisionAndScale(ScriptWriter writer)
		{
			int? numericPrecision = this.typeValue.Facets.NumericPrecision;
			if (numericPrecision != null)
			{
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.WriteInt(numericPrecision.Value);
				int? numericScale = this.typeValue.Facets.NumericScale;
				if (numericScale != null)
				{
					writer.Write(SqlLanguageSymbols.CommaSqlString);
					writer.WriteInt(numericScale.Value);
				}
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}
		}

		// Token: 0x04004255 RID: 16981
		private static readonly SqlDataType bigIntType = new SqlDataType(TypeValue.Int64, SqlLanguageStrings.BigIntSqlString);

		// Token: 0x04004256 RID: 16982
		private static readonly SqlDataType bitType = new SqlDataType(TypeValue.Logical, SqlLanguageStrings.BitSqlString);

		// Token: 0x04004257 RID: 16983
		private static readonly SqlDataType byteaType = new SqlDataType(TypeValue.Binary, SqlLanguageStrings.ByteaSqlString);

		// Token: 0x04004258 RID: 16984
		private static readonly SqlDataType characterType = new SqlDataType(TypeValue.Text, SqlLanguageStrings.CharSqlString);

		// Token: 0x04004259 RID: 16985
		private static readonly SqlDataType dateType = new SqlDataType(TypeValue.Date, SqlLanguageStrings.DateSqlString);

		// Token: 0x0400425A RID: 16986
		private static readonly SqlDataType dateTimeType = new SqlDataType(TypeValue.DateTime, SqlLanguageStrings.DateTimeSqlString);

		// Token: 0x0400425B RID: 16987
		private static readonly SqlDataType dateTime2Type = new SqlDataType(TypeValue.DateTime, SqlLanguageStrings.DateTime2SqlString);

		// Token: 0x0400425C RID: 16988
		private static readonly SqlDataType dateTimeOffset = new SqlDataType(TypeValue.DateTimeZone, SqlLanguageStrings.DateTimeOffsetSqlString);

		// Token: 0x0400425D RID: 16989
		private static readonly SqlDataType decimalType = new SqlDataType(TypeValue.Decimal, SqlLanguageStrings.DecimalSqlString);

		// Token: 0x0400425E RID: 16990
		private static readonly SqlDataType floatType = new SqlDataType(TypeValue.Double, SqlLanguageStrings.FloatSqlString);

		// Token: 0x0400425F RID: 16991
		private static readonly SqlDataType intType = new SqlDataType(TypeValue.Int32, SqlLanguageStrings.IntSqlString);

		// Token: 0x04004260 RID: 16992
		private static readonly SqlDataType moneyType = new SqlDataType(TypeValue.Decimal, SqlLanguageStrings.MoneySqlString);

		// Token: 0x04004261 RID: 16993
		private static readonly SqlDataType nVarCharType = new SqlDataType(TypeValue.Text, SqlLanguageStrings.NVarCharSqlString);

		// Token: 0x04004262 RID: 16994
		private static readonly SqlDataType realType = new SqlDataType(TypeValue.Single, SqlLanguageStrings.RealSqlString);

		// Token: 0x04004263 RID: 16995
		private static readonly SqlDataType smallIntType = new SqlDataType(TypeValue.Int16, SqlLanguageStrings.SmallIntSqlString);

		// Token: 0x04004264 RID: 16996
		private static readonly SqlDataType smallMoneyType = new SqlDataType(TypeValue.Decimal, SqlLanguageStrings.SmallMoneySqlString);

		// Token: 0x04004265 RID: 16997
		private static readonly SqlDataType time = new SqlDataType(TypeValue.Time, SqlLanguageStrings.TimeSqlString);

		// Token: 0x04004266 RID: 16998
		private static readonly SqlDataType timestamp = new SqlDataType(TypeValue.DateTime, SqlLanguageStrings.TimestampSqlString);

		// Token: 0x04004267 RID: 16999
		private static readonly SqlDataType tinyIntType = new SqlDataType(TypeValue.Int8, SqlLanguageStrings.TinyIntSqlString);

		// Token: 0x04004268 RID: 17000
		private static readonly SqlDataType varBinaryType = new SqlDataType(TypeValue.Binary, SqlLanguageStrings.VarBinarySqlString);

		// Token: 0x04004269 RID: 17001
		private static readonly SqlDataType varCharType = new SqlDataType(TypeValue.Text, SqlLanguageStrings.VarCharSqlString);

		// Token: 0x0400426A RID: 17002
		private static readonly HashSet<string> typesWithPrecisionAndScale = new HashSet<string>
		{
			SqlLanguageStrings.DecimalSqlString.String,
			SqlLanguageStrings.NumericSqlString.String,
			SqlLanguageStrings.DecSqlString.String
		};

		// Token: 0x0400426B RID: 17003
		private readonly TypeValue typeValue;
	}
}
