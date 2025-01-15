using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Resolution
{
	// Token: 0x020000F7 RID: 247
	internal sealed class ExtensionEntityMapping
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x00021438 File Offset: 0x0001F638
		internal ExtensionEntityMapping(string originalName, string resolvedName, IReadOnlyList<ExtensionPropertyMapping> measures, IReadOnlyList<ExtensionPropertyMapping> columns)
		{
			this.OriginalName = originalName;
			this.ResolvedName = resolvedName;
			this.Measures = measures;
			this.Columns = columns;
			if (!this.Measures.IsNullOrEmpty<ExtensionPropertyMapping>())
			{
				this.MeasuresByName = new Dictionary<string, ExtensionPropertyMapping>(ConceptualNameComparer.Instance);
				foreach (ExtensionPropertyMapping extensionPropertyMapping in this.Measures)
				{
					this.MeasuresByName.Add(extensionPropertyMapping.OriginalName, extensionPropertyMapping);
				}
			}
			if (!this.Columns.IsNullOrEmpty<ExtensionPropertyMapping>())
			{
				this.ColumnsByName = new Dictionary<string, ExtensionPropertyMapping>(ConceptualNameComparer.Instance);
				foreach (ExtensionPropertyMapping extensionPropertyMapping2 in this.Columns)
				{
					this.ColumnsByName.Add(extensionPropertyMapping2.OriginalName, extensionPropertyMapping2);
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00021534 File Offset: 0x0001F734
		internal string OriginalName { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0002153C File Offset: 0x0001F73C
		internal string ResolvedName { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00021544 File Offset: 0x0001F744
		internal IReadOnlyList<ExtensionPropertyMapping> Measures { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0002154C File Offset: 0x0001F74C
		internal IReadOnlyList<ExtensionPropertyMapping> Columns { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00021554 File Offset: 0x0001F754
		private Dictionary<string, ExtensionPropertyMapping> MeasuresByName { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0002155C File Offset: 0x0001F75C
		private Dictionary<string, ExtensionPropertyMapping> ColumnsByName { get; }

		// Token: 0x0600085A RID: 2138 RVA: 0x00021564 File Offset: 0x0001F764
		internal bool TryGetMeasure(string name, out ExtensionPropertyMapping measure)
		{
			return this.TryGetProperty(name, this.MeasuresByName, out measure);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00021574 File Offset: 0x0001F774
		internal bool TryGetColumn(string name, out ExtensionPropertyMapping column)
		{
			return this.TryGetProperty(name, this.ColumnsByName, out column);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00021584 File Offset: 0x0001F784
		private bool TryGetProperty(string name, Dictionary<string, ExtensionPropertyMapping> propertyByName, out ExtensionPropertyMapping property)
		{
			if (propertyByName != null)
			{
				return propertyByName.TryGetValue(name, out property);
			}
			property = null;
			return false;
		}
	}
}
