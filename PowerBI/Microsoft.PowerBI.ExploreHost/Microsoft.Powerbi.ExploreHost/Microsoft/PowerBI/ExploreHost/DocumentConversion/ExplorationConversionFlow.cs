using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Explore.ExploreConverter.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExplorationContracts;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.DocumentConversion
{
	// Token: 0x0200008F RID: 143
	internal sealed class ExplorationConversionFlow : ExploreClientHandlerBaseFlow
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0000BBA9 File Offset: 0x00009DA9
		internal ExplorationConversionFlow(Stream stream, string databaseID, ExploreClientHandlerContext context, Dictionary<string, string> workSheetNames = null, Dictionary<string, bool> workSheetNameToIsEmbeddedDataSourceMapping = null, bool convertFromRdlxStream = false)
			: base(context, databaseID)
		{
			this.m_stream = stream;
			this.m_workSheetNames = workSheetNames;
			this.m_workSheetNameToIsEmbeddedDataSourceMapping = workSheetNameToIsEmbeddedDataSourceMapping;
			this.m_convertFromRdlxStream = convertFromRdlxStream;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000BBD2 File Offset: 0x00009DD2
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000BBDA File Offset: 0x00009DDA
		public string SerializedExploration { get; private set; }

		// Token: 0x060003B5 RID: 949 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		protected override void InternalRun()
		{
			string text = "2.0";
			IConceptualSchema conceptualSchema = ExploreHostUtils.GetConceptualSchema(this.Context.PowerViewHandler, base.DatabaseID, text, null);
			Exploration exploration;
			try
			{
				if (this.m_convertFromRdlxStream)
				{
					exploration = ExploreConverter.ConvertFromRdlxStream(this.m_stream, new ExploreConverterContext(conceptualSchema));
				}
				else
				{
					exploration = ExploreConverter.Convert(this.m_stream, new ExploreConverterContext(conceptualSchema));
				}
			}
			catch (Exception ex)
			{
				if (ex is JsonException || ex is ArgumentException)
				{
					TelemetryService.Instance.Log(new PBIWinConvertToExplorationBadDocumentError(ex.StackTrace, ex.Message));
					throw new PowerBIExploreException("ExplorationConversionFailed", "Generation of Exploration contract from document stream and conceptual schema failed.", ex, ErrorSource.PowerBI, ServiceErrorStatusCode.GeneralError);
				}
				throw;
			}
			foreach (ExplorationCompatibilityInfo explorationCompatibilityInfo in exploration.CompatibilityInfo)
			{
				TelemetryService.Instance.Log(new PBIWinConvertToExplorationUnsupportedVisual(this.GetTelemetryMessageForCompatibilityInfo(explorationCompatibilityInfo), "00000000-0000-0000-0000-000000000000", false));
			}
			if (this.m_workSheetNames != null && this.m_workSheetNames.IsNotEmpty<KeyValuePair<string, string>>())
			{
				int count = exploration.Sections.Count;
				for (int i = 0; i < count; i++)
				{
					string name = exploration.Sections[i].Name;
					string text2;
					if (this.m_workSheetNames.TryGetValue(name, out text2))
					{
						exploration.Sections[i].DisplayName = text2;
					}
				}
				this.DeleteSectionsWithASOnPremConnection(exploration);
			}
			this.SerializedExploration = JsonConvert.SerializeObject(exploration);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000BD78 File Offset: 0x00009F78
		private string GetTelemetryMessageForCompatibilityInfo(ExplorationCompatibilityInfo info)
		{
			return info.Category.ToString() + ", " + info.Details;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BDAC File Offset: 0x00009FAC
		private void DeleteSectionsWithASOnPremConnection(Exploration exploration)
		{
			if (this.m_workSheetNameToIsEmbeddedDataSourceMapping != null)
			{
				List<int> list = new List<int>();
				for (int i = 0; i < exploration.Sections.Count; i++)
				{
					bool flag = true;
					string displayName = exploration.Sections[i].DisplayName;
					if (displayName != null && this.m_workSheetNameToIsEmbeddedDataSourceMapping.TryGetValue(displayName, out flag) && !flag)
					{
						list.Add(i);
					}
				}
				for (int j = list.Count - 1; j >= 0; j--)
				{
					exploration.Sections.RemoveAt(list[j]);
				}
			}
		}

		// Token: 0x040001B8 RID: 440
		private readonly Stream m_stream;

		// Token: 0x040001B9 RID: 441
		private readonly Dictionary<string, string> m_workSheetNames;

		// Token: 0x040001BA RID: 442
		private readonly Dictionary<string, bool> m_workSheetNameToIsEmbeddedDataSourceMapping;

		// Token: 0x040001BB RID: 443
		private readonly bool m_convertFromRdlxStream;
	}
}
