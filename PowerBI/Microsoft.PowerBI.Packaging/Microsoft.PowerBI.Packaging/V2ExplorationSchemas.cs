using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000032 RID: 50
	public class V2ExplorationSchemas : IV2ExplorationSchemas
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000572C File Offset: 0x0000392C
		public V2ExplorationSchemas(Dictionary<string, Dictionary<Version, string>> resourceDict, Func<string, Version, Version> schemaVersionResolver = null)
		{
			NewtonsoftValidationLicense.RegisterPowerBILicenseForNewtonsoftJsonSchema();
			foreach (KeyValuePair<string, Dictionary<Version, string>> keyValuePair in resourceDict)
			{
				string key = keyValuePair.Key;
				List<V2ExplorationSchemas.JsonAtVersion> list = new List<V2ExplorationSchemas.JsonAtVersion>();
				HashSet<int> hashSet = new HashSet<int>();
				foreach (KeyValuePair<Version, string> keyValuePair2 in keyValuePair.Value)
				{
					Version key2 = keyValuePair2.Key;
					if (hashSet.Contains(key2.Major))
					{
						throw new ArgumentException(string.Format("Multiple schemas with major version {0} provided for {1}", key2.Major, key));
					}
					string value = keyValuePair2.Value;
					string text = string.Format("https://developer.microsoft.com/json-schemas/fabric/item/report/definition/{0}/{1}/schema.json", key, key2);
					this._resolver.Add(new Uri(text), value);
					hashSet.Add(key2.Major);
					list.Add(new V2ExplorationSchemas.JsonAtVersion(key2, value));
				}
				this._resourceDict.Add(key, list);
			}
			this._schemaVersionResolver = schemaVersionResolver;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005894 File Offset: 0x00003A94
		public JSchema GetSchema(string schemaKey, Version schemaVersion)
		{
			V2ExplorationSchemas.<>c__DisplayClass6_0 CS$<>8__locals1 = new V2ExplorationSchemas.<>c__DisplayClass6_0();
			CS$<>8__locals1.schemaVersion = schemaVersion;
			CS$<>8__locals1.<>4__this = this;
			V2ExplorationSchemas.JsonAtVersion jsonAtVersion = this._resourceDict[schemaKey].Find((V2ExplorationSchemas.JsonAtVersion version) => version.Version.Major == CS$<>8__locals1.schemaVersion.Major);
			if (jsonAtVersion == null)
			{
				throw new KeyNotFoundException();
			}
			return this._schemas.GetOrAdd(jsonAtVersion.Json, new Func<string, JSchema>(CS$<>8__locals1.<GetSchema>g__BuildSchema|1));
		}

		// Token: 0x040000AF RID: 175
		private readonly Dictionary<string, List<V2ExplorationSchemas.JsonAtVersion>> _resourceDict = new Dictionary<string, List<V2ExplorationSchemas.JsonAtVersion>>();

		// Token: 0x040000B0 RID: 176
		private readonly JSchemaPreloadedResolver _resolver = new JSchemaPreloadedResolver();

		// Token: 0x040000B1 RID: 177
		private readonly ConcurrentDictionary<string, JSchema> _schemas = new ConcurrentDictionary<string, JSchema>();

		// Token: 0x040000B2 RID: 178
		private Func<string, Version, Version> _schemaVersionResolver;

		// Token: 0x020000C7 RID: 199
		private class JsonAtVersion
		{
			// Token: 0x060004C6 RID: 1222 RVA: 0x0000D9E1 File Offset: 0x0000BBE1
			public JsonAtVersion(Version version, string json)
			{
				this.Version = version;
				this.Json = json;
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000D9F7 File Offset: 0x0000BBF7
			public Version Version { get; }

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000D9FF File Offset: 0x0000BBFF
			public string Json { get; }
		}
	}
}
