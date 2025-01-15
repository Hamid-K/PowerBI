using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200043E RID: 1086
	internal abstract class SapHanaDimensionCollection : IEnumerable<SapHanaDimension>, IEnumerable
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x00069684 File Offset: 0x00067884
		protected SapHanaDimensionCollection(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
		{
			this.dataSource = dataSource;
			this.cube = cube;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0006969A File Offset: 0x0006789A
		public IEnumerator<SapHanaDimension> GetEnumerator()
		{
			this.EnsureDimensions();
			return this.dimensions.Values.GetEnumerator();
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000696B7 File Offset: 0x000678B7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000696BF File Offset: 0x000678BF
		public bool TryGetDimension(string name, out SapHanaDimension dimension)
		{
			this.EnsureDimensions();
			return this.dimensions.TryGetValue(name, out dimension);
		}

		// Token: 0x060024F2 RID: 9458
		protected abstract Dictionary<string, SapHanaDimension> GetDimensions();

		// Token: 0x060024F3 RID: 9459 RVA: 0x000696D4 File Offset: 0x000678D4
		private void EnsureDimensions()
		{
			if (this.dimensions == null)
			{
				this.dimensions = this.GetDimensions();
			}
		}

		// Token: 0x04000EE8 RID: 3816
		protected const string measuresDimensionName = "[Measures]";

		// Token: 0x04000EE9 RID: 3817
		protected readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000EEA RID: 3818
		protected readonly SapHanaCubeBase cube;

		// Token: 0x04000EEB RID: 3819
		private Dictionary<string, SapHanaDimension> dimensions;
	}
}
