using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001D4 RID: 468
	internal class SalesforceObjectRelationship
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x0001267D File Offset: 0x0001087D
		public SalesforceObjectRelationship(BinaryReader reader)
		{
			this.child = reader.ReadString();
			this.field = reader.ReadString();
			this.relationshipName = reader.ReadNullableString();
			this.deprecated = reader.ReadBoolean();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000126B5 File Offset: 0x000108B5
		public SalesforceObjectRelationship(JsonTokenizer json)
		{
			json.ReadRecordValues(this, SalesforceObjectRelationship.fieldMap);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000126C9 File Offset: 0x000108C9
		public string Child
		{
			get
			{
				return this.child;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000126D1 File Offset: 0x000108D1
		public string Field
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000126D9 File Offset: 0x000108D9
		public string RelationshipName
		{
			get
			{
				return this.relationshipName;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x000126E1 File Offset: 0x000108E1
		public bool Deprecated
		{
			get
			{
				return this.deprecated;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000126E9 File Offset: 0x000108E9
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.child);
			writer.Write(this.field);
			writer.WriteNullable(this.relationshipName);
			writer.Write(this.deprecated);
		}

		// Token: 0x04000549 RID: 1353
		private static readonly Dictionary<string, Action<JsonTokenizer, SalesforceObjectRelationship>> fieldMap = new Dictionary<string, Action<JsonTokenizer, SalesforceObjectRelationship>>
		{
			{
				"childSObject",
				delegate(JsonTokenizer t, SalesforceObjectRelationship f)
				{
					f.child = t.ReadString(false);
				}
			},
			{
				"field",
				delegate(JsonTokenizer t, SalesforceObjectRelationship f)
				{
					f.field = t.ReadString(false);
				}
			},
			{
				"relationshipName",
				delegate(JsonTokenizer t, SalesforceObjectRelationship f)
				{
					f.relationshipName = t.ReadString(true);
				}
			},
			{
				"deprecatedAndHidden",
				delegate(JsonTokenizer t, SalesforceObjectRelationship f)
				{
					f.deprecated = t.ReadBoolean();
				}
			}
		};

		// Token: 0x0400054A RID: 1354
		private string child;

		// Token: 0x0400054B RID: 1355
		private string field;

		// Token: 0x0400054C RID: 1356
		private string relationshipName;

		// Token: 0x0400054D RID: 1357
		private bool deprecated;
	}
}
