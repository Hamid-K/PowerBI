using System;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200108F RID: 4239
	internal sealed class DbQueryPlan : QueryPlanBase
	{
		// Token: 0x06006EF4 RID: 28404 RVA: 0x0017EC4A File Offset: 0x0017CE4A
		public DbQueryPlan(TypeValue type, SqlQueryExpression query, SqlQueryOptions options, SqlSettings sqlSettings)
			: base(type)
		{
			this.Query = query;
			this.Options = options;
			this.SqlSettings = sqlSettings;
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x0017EC6C File Offset: 0x0017CE6C
		public string ExternalQuery
		{
			get
			{
				if (this.externalQuery == null)
				{
					using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
					{
						ScriptWriter scriptWriter = new ScriptWriter(stringWriter, this.SqlSettings);
						this.Query.WriteCreateScript(scriptWriter);
						this.Options.WriteOptions(scriptWriter);
						this.externalQuery = stringWriter.ToString();
					}
				}
				return this.externalQuery;
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x06006EF6 RID: 28406 RVA: 0x0017ECE0 File Offset: 0x0017CEE0
		// (set) Token: 0x06006EF7 RID: 28407 RVA: 0x0017ECE8 File Offset: 0x0017CEE8
		public SqlQueryExpression Query { get; private set; }

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x0017ECF1 File Offset: 0x0017CEF1
		// (set) Token: 0x06006EF9 RID: 28409 RVA: 0x0017ECF9 File Offset: 0x0017CEF9
		public SqlQueryOptions Options { get; private set; }

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x06006EFA RID: 28410 RVA: 0x0017ED02 File Offset: 0x0017CF02
		// (set) Token: 0x06006EFB RID: 28411 RVA: 0x0017ED0A File Offset: 0x0017CF0A
		public SqlSettings SqlSettings { get; private set; }

		// Token: 0x04003D87 RID: 15751
		private string externalQuery;
	}
}
