using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FD3 RID: 4051
	internal class ActiveDirectoryModuleCore
	{
		// Token: 0x06006A40 RID: 27200 RVA: 0x0016DB50 File Offset: 0x0016BD50
		public ActiveDirectoryModuleCore(IEngineHost host, ActiveDirectoryServiceAccessor service)
		{
			this.environment = new ActiveDirectoryEnvironment(host, service);
			this.categoriesTableType = NavigationTableServices.AddNavigationTableMetadata(ActiveDirectoryModuleCore.GetTableType(ActiveDirectoryModuleCore.CategoriesTableColumns, NavigationTableServices.ConvertToLink(TypeValue.Table, "Objects", true)), TextValue.New("Category"), TextValue.New("Objects"));
			this.domainsTableType = NavigationTableServices.AddNavigationTableMetadata(ActiveDirectoryModuleCore.GetTableType(ActiveDirectoryModuleCore.DomainsTableColumns, this.categoriesTableType), TextValue.New("Domain"), TextValue.New("Object Categories"));
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x0016DBD8 File Offset: 0x0016BDD8
		public TableValue GetDomainsTableValue()
		{
			return new ActiveDirectoryModuleCore.KeyedTableValue(ActiveDirectoryModuleCore.DomainsTableColumns, this.GetDomainsTableRows(), null, this.domainsTableType, this.environment.Service);
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x0016DBFC File Offset: 0x0016BDFC
		private IEnumerable<IValueReference> GetDomainsTableRows()
		{
			yield return this.GetDomainsTableRow(this.environment.Service.FullyQualifiedDomainName);
			foreach (string text in this.environment.Service.ForestDomains)
			{
				if (!text.Equals(this.environment.Service.FullyQualifiedDomainName, StringComparison.OrdinalIgnoreCase))
				{
					yield return this.GetDomainsTableRow(text);
				}
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0016DC0C File Offset: 0x0016BE0C
		private RecordValue GetDomainsTableRow(string domain)
		{
			TextValue textValue = TextValue.New(domain);
			return RecordValue.New(ActiveDirectoryModuleCore.DomainsTableColumns, new Value[]
			{
				textValue,
				this.GetCategoriesTableValue(domain)
			});
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x0016DC40 File Offset: 0x0016BE40
		private TableValue GetCategoriesTableValue(string domain)
		{
			return new ActiveDirectoryModuleCore.KeyedTableValue(ActiveDirectoryModuleCore.CategoriesTableColumns, this.GetCategoriesTableRows(domain), (string c) => this.GetCategoryTableRow(domain, c), this.categoriesTableType, this.environment.Service);
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x0016DC94 File Offset: 0x0016BE94
		private IEnumerable<IValueReference> GetCategoriesTableRows(string domain)
		{
			this.environment.Service.EnsureAccessToDomain(domain);
			string[] array = new string[] { "user", "group", "computer", "printQueue", "contact", "packageRegistration" };
			HashSet<string> categories = new HashSet<string>(this.environment.TypeCatalog.GetStructuralObjectClasses());
			foreach (string category in array)
			{
				if (categories.Contains(category))
				{
					yield return this.GetCategoryTableRow(domain, category);
					categories.Remove(category);
				}
				category = null;
			}
			string[] array2 = null;
			foreach (string text in categories.OrderBy((string s) => s))
			{
				yield return this.GetCategoryTableRow(domain, text);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x0016DCAB File Offset: 0x0016BEAB
		private IValueReference GetCategoryTableRow(string domain, string categoryName)
		{
			this.environment.Service.EnsureAccessToDomain(domain);
			return RecordValue.New(ActiveDirectoryModuleCore.CategoriesTableColumns, new Value[]
			{
				TextValue.New(categoryName),
				ActiveDirectoryCategoryTableValue.New(this.environment, categoryName, domain)
			});
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0016DCE8 File Offset: 0x0016BEE8
		private static TableTypeValue GetTableType(Keys columns, TypeValue secondColumnType)
		{
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(columns, new Value[]
			{
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					secondColumnType,
					LogicalValue.False
				})
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			});
		}

		// Token: 0x04003AF8 RID: 15096
		private const string DomainKey = "Domain";

		// Token: 0x04003AF9 RID: 15097
		private const string ObjectCategoriesKey = "Object Categories";

		// Token: 0x04003AFA RID: 15098
		private const string CategoryKey = "Category";

		// Token: 0x04003AFB RID: 15099
		private const string ObjectsKey = "Objects";

		// Token: 0x04003AFC RID: 15100
		private static readonly Keys DomainsTableColumns = Keys.New("Domain", "Object Categories");

		// Token: 0x04003AFD RID: 15101
		private static readonly Keys CategoriesTableColumns = Keys.New("Category", "Objects");

		// Token: 0x04003AFE RID: 15102
		private readonly ActiveDirectoryEnvironment environment;

		// Token: 0x04003AFF RID: 15103
		private readonly TableTypeValue domainsTableType;

		// Token: 0x04003B00 RID: 15104
		private readonly TableTypeValue categoriesTableType;

		// Token: 0x02000FD4 RID: 4052
		private sealed class KeyedTableValue : TableValue
		{
			// Token: 0x06006A49 RID: 27209 RVA: 0x0016DD8C File Offset: 0x0016BF8C
			public KeyedTableValue(Keys columns, IEnumerable<IValueReference> fullList, Func<string, IValueReference> keySelection, TableTypeValue type, ActiveDirectoryServiceAccessor service)
			{
				this.type = type;
				this.columns = columns;
				this.fullList = fullList;
				this.keySelection = keySelection;
				this.service = service;
			}

			// Token: 0x17001E80 RID: 7808
			// (get) Token: 0x06006A4A RID: 27210 RVA: 0x0016DDB9 File Offset: 0x0016BFB9
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17001E81 RID: 7809
			// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0016DDC1 File Offset: 0x0016BFC1
			public override Keys Columns
			{
				get
				{
					return this.columns;
				}
			}

			// Token: 0x06006A4C RID: 27212 RVA: 0x0016DDC9 File Offset: 0x0016BFC9
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.fullList.GetEnumerator();
			}

			// Token: 0x06006A4D RID: 27213 RVA: 0x0016DDD8 File Offset: 0x0016BFD8
			public override bool TryGetValue(Value key, out Value value)
			{
				Value value2;
				if (this.keySelection != null && key.IsRecord && key.AsRecord.TryGetValue(this.columns[0], out value2))
				{
					value = this.keySelection(value2.AsText.String).Value;
					return true;
				}
				return base.TryGetValue(key, out value);
			}

			// Token: 0x06006A4E RID: 27214 RVA: 0x0016DE37 File Offset: 0x0016C037
			public override void TestConnection()
			{
				this.service.ForestDomains.Any<string>();
			}

			// Token: 0x04003B01 RID: 15105
			private readonly Func<string, IValueReference> keySelection;

			// Token: 0x04003B02 RID: 15106
			private readonly Keys columns;

			// Token: 0x04003B03 RID: 15107
			private readonly TableTypeValue type;

			// Token: 0x04003B04 RID: 15108
			private readonly IEnumerable<IValueReference> fullList;

			// Token: 0x04003B05 RID: 15109
			private readonly ActiveDirectoryServiceAccessor service;
		}
	}
}
