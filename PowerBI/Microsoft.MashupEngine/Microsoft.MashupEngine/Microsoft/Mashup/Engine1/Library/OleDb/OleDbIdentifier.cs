using System;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x02000588 RID: 1416
	internal sealed class OleDbIdentifier : DbIdentifier
	{
		// Token: 0x06002CEA RID: 11498 RVA: 0x00088CB4 File Offset: 0x00086EB4
		public OleDbIdentifier(string catalog, string schema, string name)
			: base(catalog, schema, name)
		{
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00088CBF File Offset: 0x00086EBF
		public SchemaItem SchemaItem
		{
			get
			{
				return new SchemaItem(base.Schema, base.Name, null);
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x00088CD4 File Offset: 0x00086ED4
		public string ToQualifiedName(OleDbDataSourceInfo info)
		{
			if (this.qualifiedName == null)
			{
				TableReference tableReference = new TableReference(this.NewAlias(base.Schema), this.NewAlias(base.Name), this.NewAlias(base.Catalog));
				this.qualifiedName = tableReference.ToScript(OleDbIdentifier.GetIdentifierSettings(info));
			}
			return this.qualifiedName;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x00088D2B File Offset: 0x00086F2B
		private Alias NewAlias(string name)
		{
			if (name == null)
			{
				return null;
			}
			return Alias.NewNativeAlias(name);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x00088D38 File Offset: 0x00086F38
		private static SqlSettings GetIdentifierSettings(OleDbDataSourceInfo info)
		{
			Func<string, string> func = (string s) => s;
			if (!info.IsAce && info.QuotePrefix != null && info.QuoteSuffix != null)
			{
				func = (string s) => info.QuotePrefix + s + info.QuoteSuffix;
			}
			SqlSettings sqlSettings = new SqlSettings();
			sqlSettings.QuoteIdentifier = func;
			int? catalogLocation = info.CatalogLocation;
			int num = 2;
			sqlSettings.CatalogLocation = (((catalogLocation.GetValueOrDefault() == num) & (catalogLocation != null)) ? CatalogNameLocation.End : CatalogNameLocation.Start);
			sqlSettings.CatalogSeparator = new ConstantSqlString(info.CatalogSeperator);
			sqlSettings.SchemaSeparator = new ConstantSqlString(info.SchemaSeperator);
			return sqlSettings;
		}

		// Token: 0x0400138F RID: 5007
		private string qualifiedName;
	}
}
