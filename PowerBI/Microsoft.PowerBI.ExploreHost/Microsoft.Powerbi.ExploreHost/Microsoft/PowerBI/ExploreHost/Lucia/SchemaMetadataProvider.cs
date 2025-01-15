using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.DomainModel.Serialization;
using Microsoft.PowerBI.Lucia.Hosting;
using Microsoft.PowerBI.Lucia.Interpret;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000070 RID: 112
	internal sealed class SchemaMetadataProvider : ISchemaMetadataProvider, IDependentSchemasProvider
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000A13C File Offset: 0x0000833C
		public SchemaMetadataProvider(ITracer tracer, ReportMetadata reportMetadata = null, Func<DateTime> getUpdatedTime = null)
		{
			this.m_tracer = tracer;
			Func<DateTime> func = getUpdatedTime;
			if (getUpdatedTime == null && (func = SchemaMetadataProvider.<>c.<>9__5_0) == null)
			{
				func = (SchemaMetadataProvider.<>c.<>9__5_0 = () => DateTime.UtcNow);
			}
			this.m_getLastUpdatedTime = func;
			this.m_reportMetadata = reportMetadata;
			this.m_lastUpdatedTime = DateTime.MinValue;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A194 File Offset: 0x00008394
		public IReadOnlyDictionary<int, DependentSchemaContainer> GetDependentSchemaContainers(DependentSchema[] dependentSchemas, IConceptualSchema baseConceptualSchema, string databaseName)
		{
			Dictionary<int, DependentSchemaContainer> dictionary = new Dictionary<int, DependentSchemaContainer>(1);
			DependentSchema dependentSchema = dependentSchemas[0];
			IConceptualSchema conceptualSchema;
			if (this.m_reportMetadata.Pods != null && DependentConceptualSchemaBuilder.TryCreateConceptualSchema(dependentSchema.Id, this.m_reportMetadata.Pods, baseConceptualSchema, this.m_tracer, ref conceptualSchema))
			{
				dictionary.Add(0, new DependentSchemaContainer(conceptualSchema, (this.m_reportMetadata.LinguisticSchema == null) ? LsdlDocumentFactory.CreateLsdlDocument(null, null, LsdlDynamicImprovement.Default, null, null, null, null, null, null) : this.m_reportMetadata.LinguisticSchema.ToLsdlDocument()));
			}
			return dictionary;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A215 File Offset: 0x00008415
		public void UpdateReportMetadata(ReportMetadata reportMetadata)
		{
			if (!ReportMetadataComparer.Instance.Equals(this.m_reportMetadata, reportMetadata))
			{
				this.m_reportMetadata = reportMetadata;
				this.m_lastUpdatedTime = this.m_getLastUpdatedTime();
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A242 File Offset: 0x00008442
		public List<DependentSchema> GetDependentSchemas()
		{
			if (this.m_reportMetadata == null)
			{
				return null;
			}
			return new List<DependentSchema>
			{
				new DependentSchema
				{
					Id = "DesktopDependentSchema",
					LastUpdatedTime = this.m_lastUpdatedTime
				}
			};
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A275 File Offset: 0x00008475
		internal static ReportMetadata ConvertReportMetadata(ReportMetadata reportMetadata)
		{
			if (reportMetadata == null)
			{
				return null;
			}
			ReportMetadata reportMetadata2 = new ReportMetadata();
			List<Pod> pods = reportMetadata.Pods;
			reportMetadata2.Pods = ((pods != null) ? pods.ToArray() : null);
			reportMetadata2.LinguisticSchema = reportMetadata.LinguisticSchema;
			return reportMetadata2;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000A2A5 File Offset: 0x000084A5
		private static ModelLinguisticSchema CloneLinguisticSchema(ModelLinguisticSchema schema)
		{
			if (schema == null)
			{
				return new ModelLinguisticSchema();
			}
			return ModelLinguisticSchema.FromJsonString(schema.ToJsonString());
		}

		// Token: 0x04000167 RID: 359
		public const string DesktopDependentSchemaId = "DesktopDependentSchema";

		// Token: 0x04000168 RID: 360
		private readonly ITracer m_tracer;

		// Token: 0x04000169 RID: 361
		private readonly Func<DateTime> m_getLastUpdatedTime;

		// Token: 0x0400016A RID: 362
		private ReportMetadata m_reportMetadata;

		// Token: 0x0400016B RID: 363
		private DateTime m_lastUpdatedTime;
	}
}
