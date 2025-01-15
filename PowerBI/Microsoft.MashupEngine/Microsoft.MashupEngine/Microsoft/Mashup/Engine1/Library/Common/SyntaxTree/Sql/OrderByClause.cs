using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E4 RID: 4580
	internal sealed class OrderByClause : IScriptable
	{
		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x060078CE RID: 30926 RVA: 0x001A227D File Offset: 0x001A047D
		public IList<OrderByItem> OrderByItems
		{
			get
			{
				return this.orderByItems;
			}
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x001A2288 File Offset: 0x001A0488
		public void WriteCreateScript(ScriptWriter writer)
		{
			if (this.orderByItems.Count == 0)
			{
				throw new InvalidOperationException();
			}
			writer.WriteSpaceAfter(SqlLanguageStrings.OrderBySqlString);
			writer.Indent();
			bool flag = false;
			foreach (OrderByItem orderByItem in this.orderByItems)
			{
				flag = writer.WriteLineCommaIfNeeded(flag);
				orderByItem.WriteCreateScript(writer);
			}
			writer.Unindent();
		}

		// Token: 0x040041CE RID: 16846
		private readonly List<OrderByItem> orderByItems = new List<OrderByItem>();
	}
}
