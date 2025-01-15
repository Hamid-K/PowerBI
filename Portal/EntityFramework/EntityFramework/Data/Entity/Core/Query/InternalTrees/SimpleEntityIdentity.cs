using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E6 RID: 998
	internal class SimpleEntityIdentity : EntityIdentity
	{
		// Token: 0x06002F07 RID: 12039 RVA: 0x00095401 File Offset: 0x00093601
		internal SimpleEntityIdentity(EntitySet entitySet, SimpleColumnMap[] keyColumns)
			: base(keyColumns)
		{
			this.m_entitySet = entitySet;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x00095411 File Offset: 0x00093611
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0009541C File Offset: 0x0009361C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[(ES={0}) (Keys={", new object[] { this.EntitySet.Name });
			foreach (SimpleColumnMap simpleColumnMap in base.Keys)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[] { text, simpleColumnMap });
				text = ",";
			}
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "})]", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x04000FD8 RID: 4056
		private readonly EntitySet m_entitySet;
	}
}
