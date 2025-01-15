using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000049 RID: 73
	public interface ILuciaSession : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000195 RID: 405
		// (remove) Token: 0x06000196 RID: 406
		event EventHandler DomainModelUpdated;

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000197 RID: 407
		bool IsActive { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000198 RID: 408
		bool IsDomainModelReady { get; }

		// Token: 0x06000199 RID: 409
		void Activate();

		// Token: 0x0600019A RID: 410
		void Deactivate();

		// Token: 0x0600019B RID: 411
		Task<string> InterpretAsync(string interpretRequest);

		// Token: 0x0600019C RID: 412
		void NotifyModelChanging();

		// Token: 0x0600019D RID: 413
		void NotifyModelChanged(LuciaSessionModelChangedArgs args);

		// Token: 0x0600019E RID: 414
		void NotifyFileSaved(LuciaSessionFileSavedArgs args);

		// Token: 0x0600019F RID: 415
		Task<ValidateLinguisticSchemaResult> ValidateLinguisticSchemaYamlForImportAsync(TextReader reader);

		// Token: 0x060001A0 RID: 416
		Task<ExportLinguisticSchemaResult> ExportLinguisticSchemaYamlAsync(string defaultSchemaSource = null);

		// Token: 0x060001A1 RID: 417
		Task<bool> TryWriteLinguisticSchemaJsonAsync(JsonWriter writer, string requestedVersion);

		// Token: 0x060001A2 RID: 418
		void OverrideLinguisticSchemaJson(string schemaJson);

		// Token: 0x060001A3 RID: 419
		void ResetLinguisticSchemaJson();

		// Token: 0x060001A4 RID: 420
		Task StoreUtteranceFeedAsync(string datasetId, Stream stream, CancellationToken cancellationToken);

		// Token: 0x060001A5 RID: 421
		Task<string> GetUtteranceHistoryAsync(string datasetId, CancellationToken cancellationToken);

		// Token: 0x060001A6 RID: 422
		Task WriteDataIndexAsync(Stream stream, CancellationToken cancellationToken);

		// Token: 0x060001A7 RID: 423
		Task WriteSchemaAnnotationAsync(Stream stream, CancellationToken cancellation);
	}
}
