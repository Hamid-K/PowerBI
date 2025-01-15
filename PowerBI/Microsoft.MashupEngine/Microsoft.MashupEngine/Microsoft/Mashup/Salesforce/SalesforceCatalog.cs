using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001CD RID: 461
	internal class SalesforceCatalog
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00011928 File Offset: 0x0000FB28
		private SalesforceCatalog(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			this.objects = new SalesforceObjectHeader[num];
			this.uniquePrefixes = new Dictionary<string, SalesforceObjectHeader>(num);
			for (int i = 0; i < num; i++)
			{
				this.objects[i] = new SalesforceObjectHeader(reader);
				this.AddPrefixItem(this.objects[i]);
			}
			Array.Sort<SalesforceObjectHeader>(this.objects, (SalesforceObjectHeader a, SalesforceObjectHeader b) => string.Compare(a.Label, b.Label, StringComparison.CurrentCulture));
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000119AC File Offset: 0x0000FBAC
		private SalesforceCatalog(JsonTokenizer json)
		{
			List<SalesforceObjectHeader> list = new List<SalesforceObjectHeader>();
			json.ReadRecordValues(list, SalesforceCatalog.fieldMap);
			this.objects = list.Where((SalesforceObjectHeader h) => !h.Deprecated).ToArray<SalesforceObjectHeader>();
			Array.Sort<SalesforceObjectHeader>(this.objects, (SalesforceObjectHeader a, SalesforceObjectHeader b) => string.Compare(a.Label, b.Label, StringComparison.CurrentCulture));
			this.uniquePrefixes = new Dictionary<string, SalesforceObjectHeader>(this.objects.Length);
			foreach (SalesforceObjectHeader salesforceObjectHeader in this.objects)
			{
				this.AddPrefixItem(salesforceObjectHeader);
			}
		}

		// Token: 0x1700028D RID: 653
		public SalesforceObjectHeader this[string name]
		{
			get
			{
				return this.objects.FirstOrDefault((SalesforceObjectHeader obj) => obj.Name == name);
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00011A94 File Offset: 0x0000FC94
		public static SalesforceCatalog LoadCatalog(IEngineHost host, SalesforceDataLoader dataLoader)
		{
			IPersistentCache metadataCache = host.GetMetadataCache();
			string text = dataLoader.CreateCacheKey(new string[] { "Catalog" });
			Stream stream;
			SalesforceCatalog salesforceCatalog;
			if (!metadataCache.TryGetValue(text, out stream))
			{
				using (JsonTokenizer jsonTokenizer = dataLoader.StreamJsonValue(dataLoader.ObjectListPath, null))
				{
					salesforceCatalog = new SalesforceCatalog(jsonTokenizer);
					stream = metadataCache.BeginAdd();
					BinaryWriter binaryWriter = new BinaryWriter(stream);
					salesforceCatalog.Serialize(binaryWriter);
					metadataCache.EndAdd(text, stream).Close();
					return salesforceCatalog;
				}
			}
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				salesforceCatalog = new SalesforceCatalog(binaryReader);
			}
			return salesforceCatalog;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00011B58 File Offset: 0x0000FD58
		public static SalesforceObjectDetail LoadReportsMetadata(IEngineHost host, SalesforceDataLoader dataLoader)
		{
			return new SalesforceObjectHeader(dataLoader, "Report").LoadDetail(host, dataLoader);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00011B6C File Offset: 0x0000FD6C
		private void Serialize(BinaryWriter writer)
		{
			writer.Write(this.objects.Length);
			SalesforceObjectHeader[] array = this.objects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Serialize(writer);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00011BA5 File Offset: 0x0000FDA5
		public IEnumerable<SalesforceObjectHeader> GetEnumerator()
		{
			return this.objects;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		private void AddPrefixItem(SalesforceObjectHeader item)
		{
			if (string.IsNullOrEmpty(item.KeyPrefix))
			{
				return;
			}
			SalesforceObjectHeader salesforceObjectHeader;
			if (!this.uniquePrefixes.TryGetValue(item.KeyPrefix, out salesforceObjectHeader))
			{
				this.uniquePrefixes.Add(item.KeyPrefix, item);
				return;
			}
			if (salesforceObjectHeader != null)
			{
				this.uniquePrefixes[item.KeyPrefix] = null;
			}
		}

		// Token: 0x04000523 RID: 1315
		private static readonly Dictionary<string, Action<JsonTokenizer, List<SalesforceObjectHeader>>> fieldMap = new Dictionary<string, Action<JsonTokenizer, List<SalesforceObjectHeader>>> { 
		{
			"sobjects",
			delegate(JsonTokenizer t, List<SalesforceObjectHeader> l)
			{
				t.ReadListValues(l, (JsonTokenizer t2) => new SalesforceObjectHeader(t2));
			}
		} };

		// Token: 0x04000524 RID: 1316
		private readonly SalesforceObjectHeader[] objects;

		// Token: 0x04000525 RID: 1317
		private readonly Dictionary<string, SalesforceObjectHeader> uniquePrefixes;
	}
}
