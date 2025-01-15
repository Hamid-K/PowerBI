using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011DC RID: 4572
	internal sealed class InArrayExpression : SqlExpression
	{
		// Token: 0x0600789E RID: 30878 RVA: 0x001A181B File Offset: 0x0019FA1B
		public InArrayExpression()
		{
			this.elements = new List<SqlExpression>();
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x001A182E File Offset: 0x0019FA2E
		public InArrayExpression(IEnumerable<SqlExpression> expressions)
		{
			this.elements = new List<SqlExpression>(expressions);
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x060078A0 RID: 30880 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x001A1842 File Offset: 0x0019FA42
		public void Add(SqlExpression expression)
		{
			this.elements.Add(expression);
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x001A1850 File Offset: 0x0019FA50
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			this.elements[0].WriteCreateScript(writer);
			for (int i = 1; i < this.elements.Count; i++)
			{
				writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
				this.elements[i].WriteCreateScript(writer);
			}
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}

		// Token: 0x040041BE RID: 16830
		private readonly IList<SqlExpression> elements;
	}
}
