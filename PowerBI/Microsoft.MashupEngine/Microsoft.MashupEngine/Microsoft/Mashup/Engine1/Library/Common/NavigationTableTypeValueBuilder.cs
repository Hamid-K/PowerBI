using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F1 RID: 4337
	internal struct NavigationTableTypeValueBuilder
	{
		// Token: 0x06007179 RID: 29049 RVA: 0x0018628F File Offset: 0x0018448F
		public NavigationTableTypeValueBuilder(TableTypeValue typeValue, int capacity)
		{
			this.metadata = new RecordBuilder(capacity);
			this.typeValue = typeValue;
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x001862A4 File Offset: 0x001844A4
		public void AddIdColumnName(string columnName = "Id")
		{
			this.metadata.Add("NavigationTable.IdColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x0600717B RID: 29051 RVA: 0x001862C1 File Offset: 0x001844C1
		public void AddNameColumnName(string columnName = "Name")
		{
			this.metadata.Add("NavigationTable.NameColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x001862DE File Offset: 0x001844DE
		public void AddDataColumnName(string columnName = "Data")
		{
			this.metadata.Add("NavigationTable.DataColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x001862FB File Offset: 0x001844FB
		public void AddItemKindColumnName(string columnName = "Kind")
		{
			this.metadata.Add("NavigationTable.ItemKind", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x0600717E RID: 29054 RVA: 0x00186318 File Offset: 0x00184518
		public void AddKindColumnName(string columnName = "Kind")
		{
			this.metadata.Add("NavigationTable.KindColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x00186335 File Offset: 0x00184535
		public void AddHiddenColumnName(string columnName = "Hidden")
		{
			this.metadata.Add("NavigationTable.HiddenColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x00186352 File Offset: 0x00184552
		public void AddDescriptionColumnName(string columnName = "Description")
		{
			this.metadata.Add("NavigationTable.DescriptionColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x06007181 RID: 29057 RVA: 0x0018636F File Offset: 0x0018456F
		public void AddRowConfigurationColumnName(string columnName)
		{
			this.metadata.Add("NavigationTable.RowConfigurationColumn", TextValue.New(columnName), TypeValue.Text);
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x0018638C File Offset: 0x0018458C
		public void AddPreviewIdColumnEnabledDefault(bool value)
		{
			this.metadata.Add("Preview.IdColumnEnabledDefault", LogicalValue.New(value), TypeValue.Logical);
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x001863A9 File Offset: 0x001845A9
		public TableTypeValue ToTypeValue()
		{
			return this.typeValue.NewMeta(this.typeValue.MetaValue.Concatenate(this.metadata.ToRecord()).AsRecord).AsType.AsTableType;
		}

		// Token: 0x04003E90 RID: 16016
		private RecordBuilder metadata;

		// Token: 0x04003E91 RID: 16017
		private TableTypeValue typeValue;
	}
}
