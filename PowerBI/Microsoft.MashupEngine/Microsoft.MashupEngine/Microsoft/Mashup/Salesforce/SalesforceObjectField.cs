using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001D2 RID: 466
	internal class SalesforceObjectField
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x000120C0 File Offset: 0x000102C0
		public SalesforceObjectField(BinaryReader reader)
		{
			this.caseSensitive = reader.ReadBoolean();
			this.custom = reader.ReadBoolean();
			this.deprecated = reader.ReadBoolean();
			this.digits = reader.ReadInt32();
			this.filterable = reader.ReadBoolean();
			this.groupable = reader.ReadBoolean();
			this.idLookup = reader.ReadBoolean();
			this.length = reader.ReadInt32();
			this.name = reader.ReadString();
			this.nameField = reader.ReadBoolean();
			this.nullable = reader.ReadBoolean();
			int num = reader.ReadInt32();
			if (num > 0)
			{
				this.referenceTo = new string[num];
				for (int i = 0; i < num; i++)
				{
					this.referenceTo[i] = reader.ReadString();
				}
			}
			this.relationshipName = reader.ReadNullableString();
			this.sortable = reader.ReadBoolean();
			this.columnType = reader.ReadString();
			this.unique = reader.ReadBoolean();
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000121B8 File Offset: 0x000103B8
		public SalesforceObjectField(JsonTokenizer json)
		{
			json.ReadRecordValues(this, SalesforceObjectField.fieldMap);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000121CC File Offset: 0x000103CC
		public bool CaseSensitive
		{
			get
			{
				return this.caseSensitive;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000121D4 File Offset: 0x000103D4
		public bool Custom
		{
			get
			{
				return this.custom;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000121DC File Offset: 0x000103DC
		public bool Deprecated
		{
			get
			{
				return this.deprecated;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000121E4 File Offset: 0x000103E4
		public int Digits
		{
			get
			{
				return this.digits;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000121EC File Offset: 0x000103EC
		public bool Filterable
		{
			get
			{
				return this.filterable;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000121F4 File Offset: 0x000103F4
		public bool Groupable
		{
			get
			{
				return this.groupable;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000121FC File Offset: 0x000103FC
		public bool IdLookup
		{
			get
			{
				return this.idLookup;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00012204 File Offset: 0x00010404
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001220C File Offset: 0x0001040C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00012214 File Offset: 0x00010414
		public bool NameField
		{
			get
			{
				return this.nameField;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001221C File Offset: 0x0001041C
		public bool Nullable
		{
			get
			{
				return this.nullable;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00012224 File Offset: 0x00010424
		public string[] ReferenceTo
		{
			get
			{
				return this.referenceTo;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001222C File Offset: 0x0001042C
		public string RelationshipName
		{
			get
			{
				return this.relationshipName;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00012234 File Offset: 0x00010434
		public bool Sortable
		{
			get
			{
				return this.sortable;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001223C File Offset: 0x0001043C
		public string ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00012244 File Offset: 0x00010444
		public bool Unique
		{
			get
			{
				return this.unique;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001224C File Offset: 0x0001044C
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.caseSensitive);
			writer.Write(this.custom);
			writer.Write(this.deprecated);
			writer.Write(this.digits);
			writer.Write(this.filterable);
			writer.Write(this.groupable);
			writer.Write(this.idLookup);
			writer.Write(this.length);
			writer.Write(this.name);
			writer.Write(this.nameField);
			writer.Write(this.nullable);
			if (this.referenceTo == null || this.referenceTo.Length == 0)
			{
				writer.Write(0);
			}
			else
			{
				writer.Write(this.referenceTo.Length);
				foreach (string text in this.referenceTo)
				{
					writer.Write(text);
				}
			}
			writer.WriteNullable(this.relationshipName);
			writer.Write(this.sortable);
			writer.Write(this.columnType);
			writer.Write(this.unique);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00012358 File Offset: 0x00010558
		private void ReadReferences(JsonTokenizer json)
		{
			this.referenceTo = null;
			Value value = json.ReadValue();
			if (value.IsList && value.AsList.Count > 0)
			{
				ListValue asList = value.AsList;
				this.referenceTo = new string[asList.Count];
				for (int i = 0; i < asList.Count; i++)
				{
					this.referenceTo[i] = asList[i].AsText.String;
				}
			}
		}

		// Token: 0x04000537 RID: 1335
		private static readonly Dictionary<string, Action<JsonTokenizer, SalesforceObjectField>> fieldMap = new Dictionary<string, Action<JsonTokenizer, SalesforceObjectField>>
		{
			{
				"caseSensitive",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.caseSensitive = t.ReadBoolean();
				}
			},
			{
				"custom",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.custom = t.ReadBoolean();
				}
			},
			{
				"deprecatedAndHidden",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.deprecated = t.ReadBoolean();
				}
			},
			{
				"digits",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.digits = t.ReadInt32();
				}
			},
			{
				"filterable",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.filterable = t.ReadBoolean();
				}
			},
			{
				"groupable",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.groupable = t.ReadBoolean();
				}
			},
			{
				"idLookup",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.idLookup = t.ReadBoolean();
				}
			},
			{
				"length",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.length = t.ReadInt32();
				}
			},
			{
				"name",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.name = t.ReadString(false);
				}
			},
			{
				"nameField",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.nameField = t.ReadBoolean();
				}
			},
			{
				"nillable",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.nullable = t.ReadBoolean();
				}
			},
			{
				"referenceTo",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.ReadReferences(t);
				}
			},
			{
				"relationshipName",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.relationshipName = t.ReadString(true);
				}
			},
			{
				"sortable",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.sortable = t.ReadBoolean();
				}
			},
			{
				"type",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.columnType = t.ReadString(false);
				}
			},
			{
				"unique",
				delegate(JsonTokenizer t, SalesforceObjectField f)
				{
					f.unique = t.ReadBoolean();
				}
			}
		};

		// Token: 0x04000538 RID: 1336
		private bool caseSensitive;

		// Token: 0x04000539 RID: 1337
		private bool custom;

		// Token: 0x0400053A RID: 1338
		private bool deprecated;

		// Token: 0x0400053B RID: 1339
		private int digits;

		// Token: 0x0400053C RID: 1340
		private bool filterable;

		// Token: 0x0400053D RID: 1341
		private bool groupable;

		// Token: 0x0400053E RID: 1342
		private bool idLookup;

		// Token: 0x0400053F RID: 1343
		private int length;

		// Token: 0x04000540 RID: 1344
		private string name;

		// Token: 0x04000541 RID: 1345
		private bool nameField;

		// Token: 0x04000542 RID: 1346
		private bool nullable;

		// Token: 0x04000543 RID: 1347
		private string[] referenceTo;

		// Token: 0x04000544 RID: 1348
		private string relationshipName;

		// Token: 0x04000545 RID: 1349
		private bool sortable;

		// Token: 0x04000546 RID: 1350
		private string columnType;

		// Token: 0x04000547 RID: 1351
		private bool unique;
	}
}
