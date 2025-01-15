using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B6 RID: 182
	public sealed class TranslatedQuerySchemaBuilder
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0000B907 File Offset: 0x00009B07
		public TranslatedQuerySchemaBuilder()
		{
			this._schema = new TranslatedQuerySchema();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000B91C File Offset: 0x00009B1C
		public TranslatedSelectBuilder<TranslatedQuerySchemaBuilder> WithSelect(string name, string columnName)
		{
			if (this._schema.Selects == null)
			{
				this._schema.Selects = new List<TranslatedSelect>();
			}
			TranslatedSelect translatedSelect = new TranslatedSelect();
			this._schema.Selects.Add(translatedSelect);
			TranslatedSelectBuilder<TranslatedQuerySchemaBuilder> translatedSelectBuilder = new TranslatedSelectBuilder<TranslatedQuerySchemaBuilder>(translatedSelect, this);
			translatedSelectBuilder.WithName(name);
			translatedSelectBuilder.WithColumnName(columnName);
			return translatedSelectBuilder;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000B974 File Offset: 0x00009B74
		public TranslatedGroupsBuilder<TranslatedQuerySchemaBuilder> WithGroups()
		{
			if (this._schema.Groups == null)
			{
				this._schema.Groups = new TranslatedGroups();
			}
			return new TranslatedGroupsBuilder<TranslatedQuerySchemaBuilder>(this._schema.Groups, this);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public TranslatedQuerySchema Build()
		{
			return this._schema;
		}

		// Token: 0x04000210 RID: 528
		private readonly TranslatedQuerySchema _schema;
	}
}
