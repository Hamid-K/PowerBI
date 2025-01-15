using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005CE RID: 1486
	internal sealed class OdbcColumnNameGenerator
	{
		// Token: 0x06002E73 RID: 11891 RVA: 0x0008D898 File Offset: 0x0008BA98
		public OdbcColumnNameGenerator(SqlSettings sqlSettings, IEnumerable<string> names = null)
		{
			this.sqlSettings = sqlSettings;
			this.names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (names != null)
			{
				this.names.UnionWith(names);
			}
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0008D8C8 File Offset: 0x0008BAC8
		public Alias GetNextName()
		{
			string text;
			Alias alias;
			do
			{
				this.count++;
				text = "C" + this.count.ToString(CultureInfo.InvariantCulture);
			}
			while (!this.TryName(text, out alias));
			return alias;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0008D90A File Offset: 0x0008BB0A
		public bool TryName(string preferred, out Alias alias)
		{
			if (Alias.TryNewAssignedAlias(preferred, this.sqlSettings, out alias) && this.names.Add(preferred))
			{
				return true;
			}
			alias = null;
			return false;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0008D930 File Offset: 0x0008BB30
		public bool TryName(string preferred)
		{
			Alias alias;
			return this.TryName(preferred, out alias);
		}

		// Token: 0x04001474 RID: 5236
		private readonly HashSet<string> names;

		// Token: 0x04001475 RID: 5237
		private readonly SqlSettings sqlSettings;

		// Token: 0x04001476 RID: 5238
		private int count;
	}
}
