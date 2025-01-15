using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.DomainModel;
using Microsoft.Lucia.Core.DomainModel.Serialization;
using Microsoft.Lucia.Diagnostics;
using Microsoft.Lucia.Json;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.ReportingServicesHost;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000069 RID: 105
	internal sealed class LinguisticSchemaServices
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000091F8 File Offset: 0x000073F8
		internal LinguisticSchemaServices(Lazy<INaturalLanguageServicesFactory> serviceFactory, Func<string> getConceptualSchemaXml, Func<string> getLinguisticSchemaJson, IBulkMeasureExpressionProvider measureExpressionProvider, LuciaSessionOptions options, IFeatureSwitchProvider featureSwitchProvider)
		{
			this.m_serviceFactory = serviceFactory;
			this.m_getConceptualSchemaXml = getConceptualSchemaXml;
			this.m_getLinguisticSchemaJson = getLinguisticSchemaJson;
			this.m_measureExpressionProvider = measureExpressionProvider;
			this.m_luciaSessionOptions = options;
			this.m_featureSwitchProvider = featureSwitchProvider ?? FeatureSwitchProvider.Empty;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00009236 File Offset: 0x00007436
		internal Task<ValidateLinguisticSchemaResult> ValidateLinguisticSchemaYamlForImportAsync(TextReader reader)
		{
			return Task.Run<ValidateLinguisticSchemaResult>(delegate
			{
				LsdlDocument lsdlDocument;
				IReadOnlyList<DomainModelDiagnosticMessage> readOnlyList;
				if (!LsdlDocument.TryReadYaml(reader, out lsdlDocument, out readOnlyList))
				{
					return ValidateLinguisticSchemaResult.Failure(readOnlyList.ToFormattedString(false, false));
				}
				string text = lsdlDocument.ToJsonString(Formatting.None);
				IManagementService managementService = this.m_serviceFactory.Value.CreateManagementService(this.m_featureSwitchProvider, LinguisticSchemaServicesBuilderOptions.None);
				ValidateLinguisticSchemaResult validateLinguisticSchemaResult;
				using (SelfDeletingDatabaseContext selfDeletingDatabaseContext = this.CreateNewDatabaseContext(text))
				{
					IReadOnlyList<DomainModelDiagnosticMessage> readOnlyList2;
					bool flag = managementService.ValidateDomainModel(selfDeletingDatabaseContext, out readOnlyList2);
					string text2 = readOnlyList2.ToFormattedString(false, false);
					if (flag)
					{
						managementService.TryGetLinguisticSchema(selfDeletingDatabaseContext, out lsdlDocument, null);
						text = lsdlDocument.ToJsonString(Formatting.None);
						validateLinguisticSchemaResult = ValidateLinguisticSchemaResult.Success(text, text2);
					}
					else if (LinguisticSchemaServices.HasSchemaLanguageNotSupported(readOnlyList2))
					{
						validateLinguisticSchemaResult = ValidateLinguisticSchemaResult.Success(text, text2);
					}
					else
					{
						validateLinguisticSchemaResult = ValidateLinguisticSchemaResult.Failure(text2);
					}
				}
				return validateLinguisticSchemaResult;
			});
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000925B File Offset: 0x0000745B
		internal Task<ExportLinguisticSchemaResult> ExportLinguisticSchemaYamlAsync(string defaultSchemaSource = null)
		{
			return Task.Run<ExportLinguisticSchemaResult>(delegate
			{
				IManagementService managementService = this.m_serviceFactory.Value.CreateManagementService(this.m_featureSwitchProvider, LinguisticSchemaServicesBuilderOptions.None);
				Func<string> getLinguisticSchemaJson = this.m_getLinguisticSchemaJson;
				string text = ((getLinguisticSchemaJson != null) ? getLinguisticSchemaJson() : null);
				ExportLinguisticSchemaResult exportLinguisticSchemaResult;
				using (SelfDeletingDatabaseContext selfDeletingDatabaseContext = this.CreateNewDatabaseContext(text))
				{
					IReadOnlyList<DomainModelDiagnosticMessage> readOnlyList;
					bool flag = managementService.ValidateDomainModel(selfDeletingDatabaseContext, out readOnlyList);
					LsdlDocument lsdlDocument = null;
					if (flag)
					{
						if (!managementService.TryGetLinguisticSchema(selfDeletingDatabaseContext, out lsdlDocument, defaultSchemaSource))
						{
							readOnlyList = readOnlyList.Concat(new DomainModelDiagnosticMessage(DiagnosticSeverity.Error, DomainModelDiagnosticCode.InternalError, "Cannot read the linguistic schema.", DomainModelSchemaLocation.Empty)).ToList<DomainModelDiagnosticMessage>();
							flag = false;
						}
					}
					else if (LinguisticSchemaServices.HasSchemaLanguageNotSupported(readOnlyList))
					{
						IReadOnlyList<DomainModelDiagnosticMessage> readOnlyList2;
						flag = LsdlDocument.TryReadJson(JsonReaderFactory.FromString(text), out lsdlDocument, out readOnlyList2);
						readOnlyList = readOnlyList.Concat(readOnlyList2.EmptyIfNull<DomainModelDiagnosticMessage>()).ToList<DomainModelDiagnosticMessage>();
					}
					IConceptualSchema conceptualSchema = null;
					if (flag && !managementService.TryGetConceptualSchema(selfDeletingDatabaseContext, out conceptualSchema))
					{
						readOnlyList = readOnlyList.Concat(new DomainModelDiagnosticMessage(DiagnosticSeverity.Error, DomainModelDiagnosticCode.InternalError, "Cannot read the conceptual schema.", DomainModelSchemaLocation.Empty)).ToList<DomainModelDiagnosticMessage>();
						flag = false;
					}
					if (flag)
					{
						StringWriter stringWriter = new StringWriter();
						Version version;
						lsdlDocument.WriteYaml(stringWriter, LsdlVersion.LatestPublic, out version, new LsdlSerializerSettings
						{
							ConceptualSchema = conceptualSchema
						});
						if (version > LsdlVersion.LatestPublic)
						{
							readOnlyList = readOnlyList.Concat(new DomainModelDiagnosticMessage(DiagnosticSeverity.Warning, DomainModelDiagnosticCode.LinguisticSchemaVersionNotSupported, "The schema contains content requiring a newer LSDL version that is not public.", DomainModelSchemaLocation.Empty)).ToList<DomainModelDiagnosticMessage>();
						}
						exportLinguisticSchemaResult = ExportLinguisticSchemaResult.Success(stringWriter.ToString(), readOnlyList.ToFormattedString(false, false));
					}
					else
					{
						exportLinguisticSchemaResult = ExportLinguisticSchemaResult.Failure(readOnlyList.ToFormattedString(false, false));
					}
				}
				return exportLinguisticSchemaResult;
			});
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009280 File Offset: 0x00007480
		internal string GetCsdl()
		{
			return this.m_getConceptualSchemaXml();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009290 File Offset: 0x00007490
		internal bool TryWriteRawLinguisticSchemaJson(JsonWriter writer, Version requestedVersion)
		{
			Func<string> getLinguisticSchemaJson = this.m_getLinguisticSchemaJson;
			string text = ((getLinguisticSchemaJson != null) ? getLinguisticSchemaJson() : null);
			if (text == null)
			{
				return false;
			}
			Version version;
			LsdlDocument.FromJsonString(text, true).WriteJson(writer, requestedVersion, out version);
			return requestedVersion == version;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000092CC File Offset: 0x000074CC
		private SelfDeletingDatabaseContext CreateNewDatabaseContext(string linguisticSchemaJson)
		{
			return new SelfDeletingDatabaseContext(Guid.NewGuid().ToString(), this.m_getConceptualSchemaXml(), linguisticSchemaJson, this.m_measureExpressionProvider, null, this.m_serviceFactory, this.m_featureSwitchProvider);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009310 File Offset: 0x00007510
		private static bool HasSchemaLanguageNotSupported(IEnumerable<DomainModelDiagnosticMessage> messages)
		{
			if (messages != null)
			{
				return messages.Any((DomainModelDiagnosticMessage m) => m.Code == DomainModelDiagnosticCode.LinguisticSchemaLanguageNotSupported);
			}
			return false;
		}

		// Token: 0x04000144 RID: 324
		private readonly Lazy<INaturalLanguageServicesFactory> m_serviceFactory;

		// Token: 0x04000145 RID: 325
		private readonly Func<string> m_getConceptualSchemaXml;

		// Token: 0x04000146 RID: 326
		private readonly Func<string> m_getLinguisticSchemaJson;

		// Token: 0x04000147 RID: 327
		private readonly IBulkMeasureExpressionProvider m_measureExpressionProvider;

		// Token: 0x04000148 RID: 328
		private readonly LuciaSessionOptions m_luciaSessionOptions;

		// Token: 0x04000149 RID: 329
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;
	}
}
