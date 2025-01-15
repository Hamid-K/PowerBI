using System;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Microsoft.PowerBI.ReportServer.WebApi.ASConnection
{
	// Token: 0x02000043 RID: 67
	internal sealed class ASConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00007854 File Offset: 0x00005A54
		public ASConnectionStringBuilder(string connectionString)
		{
			if (connectionString == null)
			{
				throw new ArgumentNullException("connectionString");
			}
			base.ConnectionString = connectionString;
			if (!base.ContainsKey("data source") || base["data source"].ToString() == "")
			{
				throw new ArgumentException("{0} is missing or empty", "data source");
			}
			if (!base.ContainsKey("initial catalog") || base["initial catalog"].ToString() == "")
			{
				throw new ArgumentException("{0} is missing or empty", "initial catalog");
			}
			this.isASAzure = Regex.Match(this.DataSource, "^asazure://", RegexOptions.IgnoreCase).Success;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000790C File Offset: 0x00005B0C
		public string CubeName
		{
			get
			{
				if (base.ContainsKey("cube") && base["data source"] != null && base["data source"].ToString().Trim() != string.Empty)
				{
					return (string)base["cube"];
				}
				return null;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007966 File Offset: 0x00005B66
		public string DataSource
		{
			get
			{
				return (string)base["data source"];
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007978 File Offset: 0x00005B78
		public string InitialCatalog
		{
			get
			{
				return (string)base["initial catalog"];
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000798A File Offset: 0x00005B8A
		public string CustomData
		{
			get
			{
				if (base.ContainsKey("CustomData"))
				{
					return (string)base["CustomData"];
				}
				return null;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000079AB File Offset: 0x00005BAB
		public string Roles
		{
			get
			{
				if (base.ContainsKey("roles"))
				{
					return (string)base["roles"];
				}
				return null;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000079CC File Offset: 0x00005BCC
		public bool IsASAzure
		{
			get
			{
				return this.isASAzure;
			}
		}

		// Token: 0x040000C0 RID: 192
		private const string DataSourceKeyName = "data source";

		// Token: 0x040000C1 RID: 193
		private const string InitialCatalogKeyName = "initial catalog";

		// Token: 0x040000C2 RID: 194
		private const string CubeKeyName = "cube";

		// Token: 0x040000C3 RID: 195
		private const string RolesKeyName = "roles";

		// Token: 0x040000C4 RID: 196
		private const string CustomDataKeyName = "CustomData";

		// Token: 0x040000C5 RID: 197
		private readonly bool isASAzure;
	}
}
