using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000031 RID: 49
	[NLogConfigurationItem]
	public class DatabaseCommandInfo
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0000B4E9 File Offset: 0x000096E9
		public DatabaseCommandInfo()
		{
			this.Parameters = new List<DatabaseParameterInfo>();
			this.CommandType = CommandType.Text;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000B503 File Offset: 0x00009703
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0000B50B File Offset: 0x0000970B
		[RequiredParameter]
		[DefaultValue(CommandType.Text)]
		public CommandType CommandType { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000B514 File Offset: 0x00009714
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0000B51C File Offset: 0x0000971C
		public Layout ConnectionString { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000B525 File Offset: 0x00009725
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000B52D File Offset: 0x0000972D
		[RequiredParameter]
		public Layout Text { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000B536 File Offset: 0x00009736
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0000B53E File Offset: 0x0000973E
		public bool IgnoreFailures { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000B547 File Offset: 0x00009747
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000B54F File Offset: 0x0000974F
		[ArrayParameter(typeof(DatabaseParameterInfo), "parameter")]
		public IList<DatabaseParameterInfo> Parameters { get; private set; }
	}
}
