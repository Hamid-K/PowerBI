using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x0200013F RID: 319
	public static class CommonUtils
	{
		// Token: 0x0600114A RID: 4426 RVA: 0x00046791 File Offset: 0x00044991
		public static string NormalizeCase(string s)
		{
			return s.ToLowerInvariant();
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00046799 File Offset: 0x00044999
		public static string GenerateEntityKey(PersistableItemTypes type, string entityId)
		{
			return "{0}{1}{2}".FormatWithInvariantCulture(new object[]
			{
				type.ToString(),
				".",
				entityId
			});
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000467C8 File Offset: 0x000449C8
		public static string ParseEntityKey(string key, out PersistableItemTypes type)
		{
			ExtendedDiagnostics.EnsureArgument(key, key.Contains("."), "Invalid key. Key should contain key delimiter {0}.".FormatWithInvariantCulture(new object[] { "." }));
			int num = key.IndexOf(".", StringComparison.Ordinal);
			type = ExtendedEnum.Parse<PersistableItemTypes>(key.Substring(0, num));
			return key.Substring(num + 1, key.Length - (num + 1));
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00046830 File Offset: 0x00044A30
		public static string GetDatabaseFullName(string databaseEntityKey)
		{
			ExtendedDiagnostics.EnsureArgument(databaseEntityKey, databaseEntityKey.StartsWith(PersistableItemTypes.DatabaseEntity.ToString(), StringComparison.Ordinal), "databaseEntityKey should start with DatabaseEntity");
			return databaseEntityKey.Substring(PersistableItemTypes.DatabaseEntity.ToString().Length + ".".Length);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00046884 File Offset: 0x00044A84
		public static Uri GetDatabaseUri(string host, DatabaseMoniker databaseMoniker, string databaseId = null)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(host, "host cannot be emtpy");
			ExtendedDiagnostics.EnsureArgumentNotNull<DatabaseMoniker>(databaseMoniker, "Database Moniker cannot be null");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(databaseMoniker.DatabaseName, "Database Name cannot be empty or null");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(databaseMoniker.VirtualServerName, "Virtual Server Name cannot be empty or null");
			UriBuilder uriBuilder = new UriBuilder
			{
				Scheme = "https",
				Path = "/xmla/"
			};
			string text = (string.IsNullOrEmpty(databaseId) ? "vs={0}&db={1}".FormatWithInvariantCulture(new object[] { databaseMoniker.VirtualServerName, databaseMoniker.DatabaseName }) : "vs={0}&db={1}&dbid={2}".FormatWithInvariantCulture(new object[] { databaseMoniker.VirtualServerName, databaseMoniker.DatabaseName, databaseId }));
			uriBuilder.Host = host;
			uriBuilder.Query = text;
			return uriBuilder.Uri;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0004694A File Offset: 0x00044B4A
		public static IEnumerable<DatabaseType> GetOriginalDatabaseTypes(DatabaseType databaseType)
		{
			if (databaseType == DatabaseType.Ephemeral)
			{
				return Enumerable.Empty<DatabaseType>();
			}
			if (databaseType == DatabaseType.Persisted)
			{
				DatabaseType[] array = new DatabaseType[2];
				array[0] = DatabaseType.Persisted;
				return array;
			}
			return new DatabaseType[] { databaseType };
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00046970 File Offset: 0x00044B70
		public static string BuildThreadPoolCountersString(Dictionary<string, int> threadPoolPCs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in threadPoolPCs.Keys)
			{
				stringBuilder.Append(" {0} : {1}".FormatWithInvariantCulture(new object[]
				{
					text,
					threadPoolPCs[text].ToString(CultureInfo.InvariantCulture)
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000469FC File Offset: 0x00044BFC
		public static bool IsO365Database(DatabaseMoniker moniker)
		{
			return moniker.DatabaseName.StartsWith("o365_");
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00046A0E File Offset: 0x00044C0E
		public static string GetPBIDedicatedEndpoint(string pbiDedicatedUriHost, string capacity)
		{
			return "pbidedicated://{0}/{1}".FormatWithInvariantCulture(new object[] { pbiDedicatedUriHost, capacity });
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00046A28 File Offset: 0x00044C28
		public static string GetDatabaseName(DatabaseMoniker databaseMoniker, ConnectionType connectionType, bool isSample)
		{
			if (isSample)
			{
				return databaseMoniker.DatabaseName;
			}
			if (connectionType == ConnectionType.Tcp)
			{
				return databaseMoniker.EngineFriendlyFullName;
			}
			return databaseMoniker.DatabaseName;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00046A45 File Offset: 0x00044C45
		public static List<DatabaseType> DatabaseTypeSupportedByPools
		{
			get
			{
				return CommonUtils.databaseTypesSupportedByPools;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00046A4C File Offset: 0x00044C4C
		public static DatabaseType ConvertToDatabaseTypeSupportedByPools(DatabaseType databaseType)
		{
			if (databaseType != DatabaseType.Ephemeral)
			{
				return databaseType;
			}
			return DatabaseType.Persisted;
		}

		// Token: 0x040003DE RID: 990
		private static List<DatabaseType> databaseTypesSupportedByPools = new List<DatabaseType>
		{
			DatabaseType.BIPro,
			DatabaseType.Persisted,
			DatabaseType.ScaleOut
		};
	}
}
