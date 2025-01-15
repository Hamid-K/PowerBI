using System;
using System.Data;

namespace Dapper
{
	// Token: 0x0200000B RID: 11
	internal class FeatureSupport
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000387C File Offset: 0x00001A7C
		public static FeatureSupport Get(IDbConnection connection)
		{
			string name = ((connection != null) ? connection.GetType().Name : null);
			if (string.Equals(name, "npgsqlconnection", StringComparison.OrdinalIgnoreCase))
			{
				return FeatureSupport.Postgres;
			}
			return FeatureSupport.Default;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000038B4 File Offset: 0x00001AB4
		private FeatureSupport(bool arrays)
		{
			this.Arrays = arrays;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000038C3 File Offset: 0x00001AC3
		public bool Arrays { get; }

		// Token: 0x04000029 RID: 41
		private static readonly FeatureSupport Default = new FeatureSupport(false);

		// Token: 0x0400002A RID: 42
		private static readonly FeatureSupport Postgres = new FeatureSupport(true);
	}
}
