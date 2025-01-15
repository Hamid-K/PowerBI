using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000654 RID: 1620
	internal sealed class OdbcConditionExpression : OdbcSqlExpression
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x000A4317 File Offset: 0x000A2517
		public OdbcConditionExpression(Condition condition)
		{
			this.Expression = condition;
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000A4326 File Offset: 0x000A2526
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x000A432E File Offset: 0x000A252E
		public Condition Expression { get; private set; }

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x00002105 File Offset: 0x00000305
		public override OdbcSqlExpressionKind Kind
		{
			get
			{
				return OdbcSqlExpressionKind.Condition;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000A4337 File Offset: 0x000A2537
		public override TypeValue TypeValue
		{
			get
			{
				return TypeValue.Logical;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000033E7 File Offset: 0x000015E7
		public override OdbcScalarExpression AsScalar
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override OdbcConditionExpression AsCondition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000033E7 File Offset: 0x000015E7
		public override OdbcStatementExpression AsStatement
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
