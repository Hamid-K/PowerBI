using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000128 RID: 296
	internal class IndexOptionHelper : OptionsHelper<IndexOptionKind>
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x00090B9C File Offset: 0x0008ED9C
		private IndexOptionHelper()
		{
			base.AddOptionMapping(IndexOptionKind.PadIndex, "PAD_INDEX", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(IndexOptionKind.FillFactor, TSqlTokenType.FillFactor, SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(IndexOptionKind.SortInTempDB, "SORT_IN_TEMPDB", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(IndexOptionKind.IgnoreDupKey, "IGNORE_DUP_KEY", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(IndexOptionKind.StatisticsNoRecompute, "STATISTICS_NORECOMPUTE", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(IndexOptionKind.DropExisting, "DROP_EXISTING", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(IndexOptionKind.Online, "ONLINE", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(IndexOptionKind.AllowPageLocks, "ALLOW_PAGE_LOCKS", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(IndexOptionKind.AllowRowLocks, "ALLOW_ROW_LOCKS", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(IndexOptionKind.MaxDop, "MAXDOP", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(IndexOptionKind.LobCompaction, "LOB_COMPACTION", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(IndexOptionKind.FileStreamOn, "FILESTREAM_ON", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(IndexOptionKind.DataCompression, "DATA_COMPRESSION", SqlVersionFlags.TSql100AndAbove);
		}

		// Token: 0x04001140 RID: 4416
		internal static readonly IndexOptionHelper Instance = new IndexOptionHelper();
	}
}
