using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001220 RID: 4640
	internal sealed class TableReference : IScriptable
	{
		// Token: 0x06007AA2 RID: 31394 RVA: 0x001A766C File Offset: 0x001A586C
		public TableReference(Alias schema, Alias name)
			: this(schema, name, null)
		{
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x001A7677 File Offset: 0x001A5877
		public TableReference(Alias schema, Alias name, Alias catalog)
		{
			this.Schema = schema;
			this.Name = name;
			this.Catalog = catalog;
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06007AA4 RID: 31396 RVA: 0x001A7694 File Offset: 0x001A5894
		// (set) Token: 0x06007AA5 RID: 31397 RVA: 0x001A769C File Offset: 0x001A589C
		public Alias Name { get; private set; }

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06007AA6 RID: 31398 RVA: 0x001A76A5 File Offset: 0x001A58A5
		// (set) Token: 0x06007AA7 RID: 31399 RVA: 0x001A76AD File Offset: 0x001A58AD
		public Alias Schema { get; private set; }

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06007AA8 RID: 31400 RVA: 0x001A76B6 File Offset: 0x001A58B6
		// (set) Token: 0x06007AA9 RID: 31401 RVA: 0x001A76BE File Offset: 0x001A58BE
		public Alias Catalog { get; private set; }

		// Token: 0x06007AAA RID: 31402 RVA: 0x001A76C8 File Offset: 0x001A58C8
		public void WriteCreateScript(ScriptWriter writer)
		{
			if (this.Catalog != null && !this.Catalog.IsEmpty && writer.Settings.CatalogLocation == CatalogNameLocation.Start)
			{
				writer.WriteIdentifier(this.Catalog);
				writer.Write(writer.Settings.CatalogSeparator);
			}
			if (this.Schema != null)
			{
				writer.WriteIdentifier(this.Schema);
				writer.Write(writer.Settings.SchemaSeparator);
			}
			writer.WriteIdentifier(this.Name);
			if (this.Catalog != null && !this.Catalog.IsEmpty && writer.Settings.CatalogLocation == CatalogNameLocation.End)
			{
				writer.Write(writer.Settings.CatalogSeparator);
				writer.WriteIdentifier(this.Catalog);
			}
		}
	}
}
