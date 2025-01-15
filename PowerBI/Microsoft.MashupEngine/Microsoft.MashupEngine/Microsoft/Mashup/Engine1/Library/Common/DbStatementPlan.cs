using System;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001096 RID: 4246
	internal sealed class DbStatementPlan
	{
		// Token: 0x06006F12 RID: 28434 RVA: 0x0017F344 File Offset: 0x0017D544
		public DbStatementPlan(SqlStatement statement, SqlQueryOptions options, SqlSettings sqlSettings, bool countOnly)
		{
			this.Statement = statement;
			this.Options = options;
			this.SqlSettings = sqlSettings;
			this.CountOnly = countOnly;
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x06006F13 RID: 28435 RVA: 0x0017F36C File Offset: 0x0017D56C
		public string ExternalStatement
		{
			get
			{
				if (this.externalStatement == null)
				{
					using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
					{
						ScriptWriter scriptWriter = new ScriptWriter(stringWriter, this.SqlSettings);
						this.Statement.WriteCreateScript(scriptWriter);
						this.Options.WriteOptions(scriptWriter);
						this.externalStatement = stringWriter.ToString();
					}
				}
				return this.externalStatement;
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x06006F14 RID: 28436 RVA: 0x0017F3E0 File Offset: 0x0017D5E0
		// (set) Token: 0x06006F15 RID: 28437 RVA: 0x0017F3E8 File Offset: 0x0017D5E8
		public SqlStatement Statement { get; private set; }

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x06006F16 RID: 28438 RVA: 0x0017F3F1 File Offset: 0x0017D5F1
		// (set) Token: 0x06006F17 RID: 28439 RVA: 0x0017F3F9 File Offset: 0x0017D5F9
		public SqlQueryOptions Options { get; private set; }

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x06006F18 RID: 28440 RVA: 0x0017F402 File Offset: 0x0017D602
		// (set) Token: 0x06006F19 RID: 28441 RVA: 0x0017F40A File Offset: 0x0017D60A
		public SqlSettings SqlSettings { get; private set; }

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x06006F1A RID: 28442 RVA: 0x0017F413 File Offset: 0x0017D613
		// (set) Token: 0x06006F1B RID: 28443 RVA: 0x0017F41B File Offset: 0x0017D61B
		public bool CountOnly { get; private set; }

		// Token: 0x04003D9D RID: 15773
		private string externalStatement;
	}
}
