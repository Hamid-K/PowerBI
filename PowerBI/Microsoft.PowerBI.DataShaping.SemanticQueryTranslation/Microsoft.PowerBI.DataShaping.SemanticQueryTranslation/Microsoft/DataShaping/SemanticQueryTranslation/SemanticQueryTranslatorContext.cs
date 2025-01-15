using System;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000015 RID: 21
	internal sealed class SemanticQueryTranslatorContext
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000047C0 File Offset: 0x000029C0
		internal SemanticQueryTranslatorContext(ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, IFeatureSwitchProvider featureSwitchProvider, IDumper dumper, IConceptualSchema schema, EntityDataModel model, string dataSourceName)
			: this(telemetryService, tracer, featureSwitchProvider, dumper, schema, model, dataSourceName, null, 0, null)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000047EC File Offset: 0x000029EC
		internal SemanticQueryTranslatorContext(ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, IFeatureSwitchProvider featureSwitchProvider, IDumper dumper, IConceptualSchema schema, EntityDataModel model, string dataSourceName, IDaxDataTransformMetadataFactory transformMetadataFactory, int queryId = 0, CancellationToken? cancellationToken = null)
		{
			this.TelemetryService = telemetryService;
			this.Tracer = tracer;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.Dumper = dumper;
			this.Schema = schema;
			this.Model = model;
			this.DataSourceName = dataSourceName;
			this.TransformMetadataFactory = transformMetadataFactory;
			this.ErrorContext = new SemanticQueryTranslationErrorContext(this.Tracer);
			this.QueryId = queryId;
			this.CancellationToken = cancellationToken ?? CancellationToken.None;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004875 File Offset: 0x00002A75
		internal ITelemetryService TelemetryService { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000487D File Offset: 0x00002A7D
		internal Microsoft.DataShaping.ServiceContracts.ITracer Tracer { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004885 File Offset: 0x00002A85
		internal IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000488D File Offset: 0x00002A8D
		internal IDumper Dumper { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004895 File Offset: 0x00002A95
		internal EntityDataModel Model { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000489D File Offset: 0x00002A9D
		internal IConceptualSchema Schema { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000048A5 File Offset: 0x00002AA5
		internal SemanticQueryTranslationErrorContext ErrorContext { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000048AD File Offset: 0x00002AAD
		internal string DataSourceName { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000048B5 File Offset: 0x00002AB5
		internal IDaxDataTransformMetadataFactory TransformMetadataFactory { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000048BD File Offset: 0x00002ABD
		internal int QueryId { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000048C5 File Offset: 0x00002AC5
		internal CancellationToken CancellationToken { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000048CD File Offset: 0x00002ACD
		internal bool UseConceptualSchema
		{
			get
			{
				return this.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			}
		}
	}
}
