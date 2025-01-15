using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000035 RID: 53
	internal class SqlBuilder : ISqlFragment
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000124D4 File Offset: 0x000106D4
		private List<object> sqlFragments
		{
			get
			{
				if (this._sqlFragments == null)
				{
					this._sqlFragments = new List<object>();
				}
				return this._sqlFragments;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000124EF File Offset: 0x000106EF
		public void Append(object s)
		{
			this.sqlFragments.Add(s);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000124FD File Offset: 0x000106FD
		public void AppendLine()
		{
			this.sqlFragments.Add("\r\n");
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001250F File Offset: 0x0001070F
		public virtual bool IsEmpty
		{
			get
			{
				return this._sqlFragments == null || this._sqlFragments.Count == 0;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001252C File Offset: 0x0001072C
		public virtual void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this._sqlFragments != null)
			{
				foreach (object obj in this._sqlFragments)
				{
					string text = obj as string;
					if (text != null)
					{
						writer.Write(text);
					}
					else
					{
						ISqlFragment sqlFragment = obj as ISqlFragment;
						if (sqlFragment == null)
						{
							throw new InvalidOperationException();
						}
						sqlFragment.WriteSql(writer, sqlGenerator);
					}
				}
			}
		}

		// Token: 0x040000E5 RID: 229
		private List<object> _sqlFragments;
	}
}
