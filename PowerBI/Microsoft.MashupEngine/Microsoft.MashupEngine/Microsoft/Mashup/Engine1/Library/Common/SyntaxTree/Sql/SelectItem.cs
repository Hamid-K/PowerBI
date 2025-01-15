using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011F9 RID: 4601
	internal sealed class SelectItem : IScriptable
	{
		// Token: 0x06007966 RID: 31078 RVA: 0x001A3CA5 File Offset: 0x001A1EA5
		public SelectItem(SqlExpression expression)
			: this(expression, null)
		{
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x001A3CAF File Offset: 0x001A1EAF
		public SelectItem(SqlExpression expression, Alias alias)
		{
			this.alias = alias;
			this.expression = expression;
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x06007968 RID: 31080 RVA: 0x001A3CC5 File Offset: 0x001A1EC5
		// (set) Token: 0x06007969 RID: 31081 RVA: 0x001A3CCD File Offset: 0x001A1ECD
		public Alias Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x0600796A RID: 31082 RVA: 0x001A3CD6 File Offset: 0x001A1ED6
		public SqlExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x0600796B RID: 31083 RVA: 0x001A3CDE File Offset: 0x001A1EDE
		public Alias Name
		{
			get
			{
				if (this.Alias != null)
				{
					return this.Alias;
				}
				if (this.Expression != null && this.Expression is ColumnReference)
				{
					return ((ColumnReference)this.Expression).Name;
				}
				return null;
			}
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x001A3D16 File Offset: 0x001A1F16
		public SelectItem ShallowCopy()
		{
			return new SelectItem(this.Expression, this.Alias);
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x001A3D2C File Offset: 0x001A1F2C
		public void WriteCreateScript(ScriptWriter writer)
		{
			if (this.Expression == SqlConstant.Null)
			{
				writer.WriteLiteralNullForSelectItem();
			}
			else if (this.Expression == SqlConstant.SelectAll)
			{
				SqlConstant sqlConstant = this.Expression as SqlConstant;
				writer.WriteSpaceAfter(new ConstantSqlString(sqlConstant.Literal));
			}
			else
			{
				writer.WriteSubexpression(SqlQueryExpression.QueryPrecedence, this.Expression);
			}
			if (this.Alias != null)
			{
				writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.AsSqlString);
				writer.WriteIdentifier(this.Alias);
			}
		}

		// Token: 0x04004202 RID: 16898
		private Alias alias;

		// Token: 0x04004203 RID: 16899
		private readonly SqlExpression expression;
	}
}
