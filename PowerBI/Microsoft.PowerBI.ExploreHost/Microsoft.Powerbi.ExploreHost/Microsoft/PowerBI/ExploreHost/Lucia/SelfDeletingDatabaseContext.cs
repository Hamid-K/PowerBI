using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.DomainModel;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000063 RID: 99
	internal sealed class SelfDeletingDatabaseContext : IDatabaseContextWithAnnotations, IDatabaseContextWithLsdlContentType, IDatabaseContext, IDisposable
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00008DA4 File Offset: 0x00006FA4
		internal SelfDeletingDatabaseContext(string datebaseName, string conceptualSchemaXml, string linguisticSchemaJson, IBulkMeasureExpressionProvider measureExpressionProvider, StatisticsAnnotationProviderCreator statisticsAnnotationProviderCreator, Lazy<INaturalLanguageServicesFactory> serviceFactory, IFeatureSwitchProvider featureSwitchProvider)
		{
			this.m_databaseName = datebaseName;
			this.m_conceptualSchemaXml = conceptualSchemaXml;
			this.m_linguisticSchemaJson = linguisticSchemaJson;
			this.m_measureExpressionProvider = measureExpressionProvider;
			this.m_statisticsProviderCreator = statisticsAnnotationProviderCreator;
			this.m_serviceFactory = serviceFactory;
			this.m_featureSwitchProvider = featureSwitchProvider ?? FeatureSwitchProvider.Empty;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008DF5 File Offset: 0x00006FF5
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008DFD File Offset: 0x00006FFD
		public DateTime GetLastUpdateTime()
		{
			return DateTime.MaxValue;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008E04 File Offset: 0x00007004
		public IEnumerable<XmlReader> GetConceptualSchemaReaders()
		{
			if (this.m_conceptualSchemaXml == null)
			{
				return null;
			}
			return XmlReader.Create(new StringReader(this.m_conceptualSchemaXml), XmlUtil.CreateSafeXmlReaderSettings()).ArrayWrap<XmlReader>();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008E2A File Offset: 0x0000702A
		public bool TryGetLinguisticSchemaReader(LanguageIdentifier language, out string contentType, out TextReader reader)
		{
			if (this.m_linguisticSchemaJson != null)
			{
				contentType = "Json";
				reader = new StringReader(this.m_linguisticSchemaJson);
				return true;
			}
			contentType = null;
			reader = null;
			return false;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008E51 File Offset: 0x00007051
		public bool TryGetLinguisticSchemaReaders(out ReadOnlyCollection<XmlReader> readers)
		{
			readers = null;
			return false;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008E58 File Offset: 0x00007058
		public void AnnotateConceptualSchema(IConceptualSchema schema, CancellationToken cancellationToken)
		{
			if (this.m_measureExpressionProvider != null)
			{
				MeasureMetadataAnnotationProvider result = MeasureMetadataDiscoverer.DiscoverAsync(this.m_measureExpressionProvider, schema, cancellationToken, ExploreTracer.Instance).Result;
				schema.RegisterAnnotationProvider<IMeasureLogicalIdentityAnnotation, IConceptualMeasure>(result);
			}
			IAnnotationProvider<IUniqueKeyAnnotation, IConceptualEntity> annotationProvider = UniqueKeyAnnotationProvider.Create(schema);
			schema.RegisterAnnotationProvider<IUniqueKeyAnnotation, IConceptualEntity>(annotationProvider);
			if (this.m_statisticsProviderCreator != null)
			{
				IStatisticsAnnotationProvider statisticsAnnotationProvider = this.m_statisticsProviderCreator(schema, cancellationToken);
				schema.RegisterAnnotationProvider<IColumnStatisticsAnnotation, IConceptualColumn>(statisticsAnnotationProvider);
				schema.RegisterAnnotationProvider<EntityRowCountAnnotation, IConceptualEntity>(statisticsAnnotationProvider);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008EC2 File Offset: 0x000070C2
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008ECC File Offset: 0x000070CC
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!this.m_serviceFactory.IsValueCreated)
				{
					return;
				}
				this.m_serviceFactory.Value.CreateManagementService(null, LinguisticSchemaServicesBuilderOptions.None).NotifyDatabaseChanges(new DatabaseNotification[]
				{
					new DatabaseNotification
					{
						ChangeType = ChangeType.Schema,
						DatabaseName = this.DatabaseName
					}
				});
			}
		}

		// Token: 0x04000131 RID: 305
		private readonly string m_databaseName;

		// Token: 0x04000132 RID: 306
		private readonly string m_conceptualSchemaXml;

		// Token: 0x04000133 RID: 307
		private readonly string m_linguisticSchemaJson;

		// Token: 0x04000134 RID: 308
		private readonly IBulkMeasureExpressionProvider m_measureExpressionProvider;

		// Token: 0x04000135 RID: 309
		private readonly StatisticsAnnotationProviderCreator m_statisticsProviderCreator;

		// Token: 0x04000136 RID: 310
		private readonly Lazy<INaturalLanguageServicesFactory> m_serviceFactory;

		// Token: 0x04000137 RID: 311
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;
	}
}
