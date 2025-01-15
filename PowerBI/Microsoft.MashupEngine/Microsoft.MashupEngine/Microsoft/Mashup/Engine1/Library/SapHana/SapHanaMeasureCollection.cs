using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200044B RID: 1099
	internal abstract class SapHanaMeasureCollection : IEnumerable<SapHanaMeasure>, IEnumerable
	{
		// Token: 0x06002526 RID: 9510 RVA: 0x0006A435 File Offset: 0x00068635
		protected SapHanaMeasureCollection(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
		{
			this.dataSource = dataSource;
			this.cube = cube;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0006A44B File Offset: 0x0006864B
		public bool TryGetMeasure(string name, out SapHanaMeasure measure)
		{
			this.EnsureMeasures();
			return this.measures.TryGetValue(name, out measure);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0006A460 File Offset: 0x00068660
		public bool TryGetMeasureByColumn(string columnName, out SapHanaMeasure measure)
		{
			this.EnsureMeasures();
			return this.measuresByColumn.TryGetValue(columnName, out measure);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x0006A475 File Offset: 0x00068675
		public IEnumerator<SapHanaMeasure> GetEnumerator()
		{
			this.EnsureMeasures();
			return this.measures.Values.GetEnumerator();
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x0006A492 File Offset: 0x00068692
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600252B RID: 9515
		protected abstract Dictionary<string, SapHanaMeasure> GetMeasures();

		// Token: 0x0600252C RID: 9516
		protected abstract TypeValue GetMeasureTypeValue(OdbcColumnInfo columnInfo, bool isAggregatable, SapHanaAggregationType aggregationType);

		// Token: 0x0600252D RID: 9517 RVA: 0x0006A49C File Offset: 0x0006869C
		protected SapHanaAggregationType GetAggregateFunction(int aggregateFunction)
		{
			if (Enum.IsDefined(typeof(SapHanaAggregationType), (SapHanaAggregationType)aggregateFunction))
			{
				return (SapHanaAggregationType)aggregateFunction;
			}
			return SapHanaAggregationType.Unknown;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x0006A4C8 File Offset: 0x000686C8
		protected TypeValue GetTypeValue(OdbcColumnInfo columnInfo, string dataTypeName, TypeValue baseTypeValue, TypeFacets baseFacets)
		{
			TypeValue typeValue = baseTypeValue.NewFacets(baseFacets.AddNative(dataTypeName, null, null));
			RecordValue asRecord = typeValue.MetaValue.Concatenate(columnInfo.TypeValue.MetaValue).AsRecord;
			return typeValue.NewMeta(asRecord).AsType;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0006A50C File Offset: 0x0006870C
		private void EnsureMeasures()
		{
			if (this.measures == null)
			{
				this.measures = this.GetMeasures();
				this.measuresByColumn = new Dictionary<string, SapHanaMeasure>();
				foreach (SapHanaMeasure sapHanaMeasure in this.measures.Values)
				{
					this.measuresByColumn.Add(sapHanaMeasure.Column.Name, sapHanaMeasure);
				}
			}
		}

		// Token: 0x04000F1C RID: 3868
		protected readonly SapHanaCubeBase cube;

		// Token: 0x04000F1D RID: 3869
		protected readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000F1E RID: 3870
		private Dictionary<string, SapHanaMeasure> measures;

		// Token: 0x04000F1F RID: 3871
		private Dictionary<string, SapHanaMeasure> measuresByColumn;
	}
}
