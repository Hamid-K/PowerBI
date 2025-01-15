using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011DA RID: 4570
	internal sealed class GroupByClause : IScriptable
	{
		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x06007897 RID: 30871 RVA: 0x001A1754 File Offset: 0x0019F954
		public IList<GroupByItem> Items
		{
			get
			{
				return this.groupByItems;
			}
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x001A175C File Offset: 0x0019F95C
		public void WriteCreateScript(ScriptWriter writer)
		{
			if (this.groupByItems.Count == 0)
			{
				throw new InvalidOperationException();
			}
			writer.WriteSpaceAfter(SqlLanguageStrings.GroupBySqlString);
			writer.Indent();
			bool flag = false;
			foreach (GroupByItem groupByItem in this.groupByItems)
			{
				flag = writer.WriteLineCommaIfNeeded(flag);
				groupByItem.WriteCreateScript(writer);
			}
			writer.Unindent();
		}

		// Token: 0x040041BC RID: 16828
		private readonly List<GroupByItem> groupByItems = new List<GroupByItem>();
	}
}
