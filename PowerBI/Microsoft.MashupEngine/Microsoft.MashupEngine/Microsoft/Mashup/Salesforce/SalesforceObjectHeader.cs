using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001D0 RID: 464
	internal class SalesforceObjectHeader
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public SalesforceObjectHeader(BinaryReader reader)
		{
			this.name = reader.ReadString();
			this.label = reader.ReadString();
			this.keyPrefix = reader.ReadNullableString();
			this.describeUrl = reader.ReadString();
			this.custom = reader.ReadBoolean();
			this.deprecated = reader.ReadBoolean();
			this.queryable = reader.ReadBoolean();
			this.retrievable = reader.ReadBoolean();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00011D13 File Offset: 0x0000FF13
		public SalesforceObjectHeader(JsonTokenizer json)
		{
			json.ReadRecordValues(this, SalesforceObjectHeader.fieldMap);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00011D27 File Offset: 0x0000FF27
		internal SalesforceObjectHeader(SalesforceDataLoader dataLoader, string name)
		{
			this.name = name;
			this.describeUrl = dataLoader.DefaultDescribePath(name);
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00011D43 File Offset: 0x0000FF43
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00011D4B File Offset: 0x0000FF4B
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00011D53 File Offset: 0x0000FF53
		public string KeyPrefix
		{
			get
			{
				return this.keyPrefix;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00011D5B File Offset: 0x0000FF5B
		public string DescribeUrl
		{
			get
			{
				return this.describeUrl;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00011D63 File Offset: 0x0000FF63
		public bool Custom
		{
			get
			{
				return this.custom;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00011D6B File Offset: 0x0000FF6B
		public bool Deprecated
		{
			get
			{
				return this.deprecated;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00011D73 File Offset: 0x0000FF73
		public bool Queryable
		{
			get
			{
				return this.queryable;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00011D7B File Offset: 0x0000FF7B
		public bool Retrievable
		{
			get
			{
				return this.retrievable;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00011D83 File Offset: 0x0000FF83
		public SalesforceObjectDetail Detail
		{
			get
			{
				return this.detail;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00011D8B File Offset: 0x0000FF8B
		public void ReadDescribeUrl(JsonTokenizer json)
		{
			this.describeUrl = json.ReadValue().AsRecord["describe"].AsText.String;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.Name);
			writer.Write(this.Label);
			writer.WriteNullable(this.KeyPrefix);
			writer.Write(this.DescribeUrl);
			writer.Write(this.Custom);
			writer.Write(this.Deprecated);
			writer.Write(this.Queryable);
			writer.Write(this.Retrievable);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00011E24 File Offset: 0x00010024
		public SalesforceObjectDetail LoadDetail(IEngineHost host, SalesforceDataLoader dataLoader)
		{
			if (this.detail == null)
			{
				IPersistentCache metadataCache = host.GetMetadataCache();
				string text = dataLoader.CreateCacheKey(new string[] { "Catalog", this.Name });
				Stream stream;
				if (!metadataCache.TryGetValue(text, out stream))
				{
					using (JsonTokenizer jsonTokenizer = dataLoader.StreamJsonValue(this.DescribeUrl, null))
					{
						this.detail = new SalesforceObjectDetail(jsonTokenizer);
						stream = metadataCache.BeginAdd();
						BinaryWriter binaryWriter = new BinaryWriter(stream);
						this.detail.Serialize(binaryWriter);
						metadataCache.EndAdd(text, stream).Close();
						goto IL_00B7;
					}
				}
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					this.detail = new SalesforceObjectDetail(binaryReader);
				}
			}
			IL_00B7:
			return this.detail;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void LoadFields(JsonTokenizer json)
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void LoadRelationships(JsonTokenizer json)
		{
		}

		// Token: 0x0400052C RID: 1324
		protected static readonly Dictionary<string, Action<JsonTokenizer, SalesforceObjectHeader>> fieldMap = new Dictionary<string, Action<JsonTokenizer, SalesforceObjectHeader>>
		{
			{
				"name",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.name = t.ReadString(false);
				}
			},
			{
				"label",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.label = t.ReadString(false);
				}
			},
			{
				"keyPrefix",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.keyPrefix = t.ReadString(true);
				}
			},
			{
				"urls",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.ReadDescribeUrl(t);
				}
			},
			{
				"custom",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.custom = t.ReadBoolean();
				}
			},
			{
				"deprecatedAndHidden",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.deprecated = t.ReadBoolean();
				}
			},
			{
				"queryable",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.queryable = t.ReadBoolean();
				}
			},
			{
				"retrieveable",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.retrievable = t.ReadBoolean();
				}
			},
			{
				"fields",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.LoadFields(t);
				}
			},
			{
				"childRelationships",
				delegate(JsonTokenizer t, SalesforceObjectHeader h)
				{
					h.LoadRelationships(t);
				}
			}
		};

		// Token: 0x0400052D RID: 1325
		private string name;

		// Token: 0x0400052E RID: 1326
		private string label;

		// Token: 0x0400052F RID: 1327
		private string keyPrefix;

		// Token: 0x04000530 RID: 1328
		private string describeUrl;

		// Token: 0x04000531 RID: 1329
		private bool custom;

		// Token: 0x04000532 RID: 1330
		private bool deprecated;

		// Token: 0x04000533 RID: 1331
		private bool queryable;

		// Token: 0x04000534 RID: 1332
		private bool retrievable;

		// Token: 0x04000535 RID: 1333
		protected SalesforceObjectDetail detail;
	}
}
