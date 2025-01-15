using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000645 RID: 1605
	internal class OdbcRelationship
	{
		// Token: 0x06003305 RID: 13061 RVA: 0x000A3860 File Offset: 0x000A1A60
		public OdbcRelationship(Keys sourceKeys, OdbcIdentifier targetTable, Keys targetKeys)
		{
			this.sourceKeys = sourceKeys;
			this.targetTable = targetTable;
			this.targetKeys = targetKeys;
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000A387D File Offset: 0x000A1A7D
		public Keys SourceKeys
		{
			get
			{
				return this.sourceKeys;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000A3885 File Offset: 0x000A1A85
		public OdbcIdentifier TargetTable
		{
			get
			{
				return this.targetTable;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x000A388D File Offset: 0x000A1A8D
		public Keys TargetKeys
		{
			get
			{
				return this.targetKeys;
			}
		}

		// Token: 0x040016B3 RID: 5811
		private readonly Keys sourceKeys;

		// Token: 0x040016B4 RID: 5812
		private readonly OdbcIdentifier targetTable;

		// Token: 0x040016B5 RID: 5813
		private readonly Keys targetKeys;
	}
}
