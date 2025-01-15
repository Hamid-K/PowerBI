using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000655 RID: 1621
	internal sealed class OdbcScalarExpression : OdbcSqlExpression
	{
		// Token: 0x0600334F RID: 13135 RVA: 0x000A433E File Offset: 0x000A253E
		public OdbcScalarExpression(OdbcDerivedColumnTypeInfo type, SqlExpression expression)
		{
			this.TypeInfo = type;
			this.Expression = expression;
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000A4354 File Offset: 0x000A2554
		// (set) Token: 0x06003351 RID: 13137 RVA: 0x000A435C File Offset: 0x000A255C
		public OdbcDerivedColumnTypeInfo TypeInfo { get; private set; }

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000A4365 File Offset: 0x000A2565
		// (set) Token: 0x06003353 RID: 13139 RVA: 0x000A436D File Offset: 0x000A256D
		public SqlExpression Expression { get; private set; }

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x00002139 File Offset: 0x00000339
		public override OdbcSqlExpressionKind Kind
		{
			get
			{
				return OdbcSqlExpressionKind.Scalar;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000A4376 File Offset: 0x000A2576
		public override TypeValue TypeValue
		{
			get
			{
				return this.TypeInfo.TypeValue;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000033E7 File Offset: 0x000015E7
		public override OdbcConditionExpression AsCondition
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override OdbcScalarExpression AsScalar
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000033E7 File Offset: 0x000015E7
		public override OdbcStatementExpression AsStatement
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
