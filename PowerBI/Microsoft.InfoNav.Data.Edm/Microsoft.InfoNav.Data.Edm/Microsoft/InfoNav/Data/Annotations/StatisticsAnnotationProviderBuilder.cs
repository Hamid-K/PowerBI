using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations.Statistics;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Annotations
{
	// Token: 0x0200002E RID: 46
	public sealed class StatisticsAnnotationProviderBuilder
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000090CB File Offset: 0x000072CB
		public StatisticsAnnotationProviderBuilder()
		{
			this._columnStatistics = new Dictionary<EdmPropertyRef, ConceptualColumnStatistics>();
			this._entityStatistics = new Dictionary<string, int>(ConceptualNameComparer.Instance);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000090EE File Offset: 0x000072EE
		public void Register(string entityName, string columnName, ConceptualColumnStatistics statistics)
		{
			this._columnStatistics.Add(new EdmPropertyRef(entityName, columnName), statistics);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009103 File Offset: 0x00007303
		public void Register(string entityName, int rowCount)
		{
			this._entityStatistics.Add(entityName, rowCount);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009114 File Offset: 0x00007314
		public void BuildAndRegisterAnnotationProviders(IConceptualSchema schema)
		{
			StatisticsAnnotationProviderBuilder.AnnotationProvider annotationProvider = new StatisticsAnnotationProviderBuilder.AnnotationProvider(this._columnStatistics, this._entityStatistics);
			schema.RegisterAnnotationProvider<ColumnStatisticsAnnotation, IConceptualColumn>(annotationProvider);
			schema.RegisterAnnotationProvider<EntityRowCountAnnotation, IConceptualEntity>(annotationProvider);
		}

		// Token: 0x04000197 RID: 407
		private readonly Dictionary<EdmPropertyRef, ConceptualColumnStatistics> _columnStatistics;

		// Token: 0x04000198 RID: 408
		private readonly Dictionary<string, int> _entityStatistics;

		// Token: 0x0200004F RID: 79
		private sealed class AnnotationProvider : IAnnotationProvider<ColumnStatisticsAnnotation, IConceptualColumn>, IAnnotationProvider<EntityRowCountAnnotation, IConceptualEntity>
		{
			// Token: 0x06000231 RID: 561 RVA: 0x00009A30 File Offset: 0x00007C30
			internal AnnotationProvider(IReadOnlyDictionary<EdmPropertyRef, ConceptualColumnStatistics> columnStatistics, IReadOnlyDictionary<string, int> entityStatistics)
			{
				this._columnStatistics = columnStatistics;
				this._entityStatistics = entityStatistics;
			}

			// Token: 0x06000232 RID: 562 RVA: 0x00009A48 File Offset: 0x00007C48
			public bool TryGetAnnotation(IConceptualColumn target, out ColumnStatisticsAnnotation columnStatisticsAnnotation)
			{
				ConceptualColumnStatistics conceptualColumnStatistics;
				if (this._columnStatistics.TryGetValue(new EdmPropertyRef(target.Entity.Name, target.Name), out conceptualColumnStatistics))
				{
					columnStatisticsAnnotation = new ColumnStatisticsAnnotation(conceptualColumnStatistics);
					return true;
				}
				columnStatisticsAnnotation = default(ColumnStatisticsAnnotation);
				return false;
			}

			// Token: 0x06000233 RID: 563 RVA: 0x00009A90 File Offset: 0x00007C90
			public bool TryGetAnnotation(IConceptualEntity target, out EntityRowCountAnnotation rowCountAnnotation)
			{
				int num;
				if (!this._entityStatistics.TryGetValue(target.Name, out num))
				{
					rowCountAnnotation = default(EntityRowCountAnnotation);
					return false;
				}
				rowCountAnnotation = new EntityRowCountAnnotation(num);
				return true;
			}

			// Token: 0x040001EE RID: 494
			private readonly IReadOnlyDictionary<EdmPropertyRef, ConceptualColumnStatistics> _columnStatistics;

			// Token: 0x040001EF RID: 495
			private readonly IReadOnlyDictionary<string, int> _entityStatistics;
		}
	}
}
