using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200012B RID: 299
	internal class DbccOptionsHelper : OptionsHelper<DbccOptionKind>
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x00090CA0 File Offset: 0x0008EEA0
		private DbccOptionsHelper()
		{
			base.AddOptionMapping(DbccOptionKind.AllErrorMessages, "ALL_ERRORMSGS");
			base.AddOptionMapping(DbccOptionKind.CountRows, "COUNT_ROWS");
			base.AddOptionMapping(DbccOptionKind.NoInfoMessages, "NO_INFOMSGS");
			base.AddOptionMapping(DbccOptionKind.TableResults, "TABLERESULTS");
			base.AddOptionMapping(DbccOptionKind.TabLock, "TABLOCK");
			base.AddOptionMapping(DbccOptionKind.StatHeader, "STAT_HEADER");
			base.AddOptionMapping(DbccOptionKind.DensityVector, "DENSITY_VECTOR");
			base.AddOptionMapping(DbccOptionKind.HistogramSteps, "HISTOGRAM_STEPS");
			base.AddOptionMapping(DbccOptionKind.EstimateOnly, "ESTIMATEONLY");
			base.AddOptionMapping(DbccOptionKind.Fast, "FAST");
			base.AddOptionMapping(DbccOptionKind.AllLevels, "ALL_LEVELS");
			base.AddOptionMapping(DbccOptionKind.AllIndexes, "ALL_INDEXES");
			base.AddOptionMapping(DbccOptionKind.PhysicalOnly, "PHYSICAL_ONLY");
			base.AddOptionMapping(DbccOptionKind.AllConstraints, "ALL_CONSTRAINTS");
			base.AddOptionMapping(DbccOptionKind.StatsStream, "STATS_STREAM");
			base.AddOptionMapping(DbccOptionKind.Histogram, "HISTOGRAM");
			base.AddOptionMapping(DbccOptionKind.DataPurity, "DATA_PURITY");
			base.AddOptionMapping(DbccOptionKind.MarkInUseForRemoval, "MARK_IN_USE_FOR_REMOVAL");
			base.AddOptionMapping(DbccOptionKind.ExtendedLogicalChecks, "EXTENDED_LOGICAL_CHECKS", SqlVersionFlags.TSql100AndAbove);
		}

		// Token: 0x04001147 RID: 4423
		internal static readonly DbccOptionsHelper Instance = new DbccOptionsHelper();
	}
}
