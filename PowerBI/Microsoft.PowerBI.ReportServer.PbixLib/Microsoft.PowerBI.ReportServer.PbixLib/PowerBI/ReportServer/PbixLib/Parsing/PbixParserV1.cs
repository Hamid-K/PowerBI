using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.PowerBI.Packaging;
using Microsoft.PowerBI.Packaging.Extensions;
using Microsoft.PowerBI.Packaging.Storage;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x0200000A RID: 10
	public class PbixParserV1 : IPbixParser
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002355 File Offset: 0x00000555
		public static IPbixParser GetInstance(bool areCustomVisualsEnabled)
		{
			return new PbixParserV1(areCustomVisualsEnabled);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000235D File Offset: 0x0000055D
		internal PbixParserV1()
		{
			this._areCustomVisualsEnabled = false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000236C File Offset: 0x0000056C
		internal PbixParserV1(bool areCustomVisualsEnabled)
		{
			this._areCustomVisualsEnabled = areCustomVisualsEnabled;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000237B File Offset: 0x0000057B
		public PbixReportElements ParsePbixFileIntoParts(Stream pbixFileAsStream, string requestId, string sessionId)
		{
			return this.ParsePbixFileIntoParts(pbixFileAsStream, requestId, sessionId, this._areCustomVisualsEnabled);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000238C File Offset: 0x0000058C
		internal PbixReportElements ParsePbixFileIntoParts(Stream pbixFileAsStream, string requestId, string sessionId, bool areCustomVisualsEnabled)
		{
			bool flag;
			byte[] array;
			Version version;
			PbixReportElements pbixReportElements2;
			using (IPowerBIPackage powerBIPackage = PowerBIPackager.Open(pbixFileAsStream, out flag, out array, out version, false))
			{
				Guid.NewGuid().ToString();
				string reportDocumentFromPowerBIPackage = this.GetReportDocumentFromPowerBIPackage(powerBIPackage);
				string reportMobileStateFromPowerBIPackage = this.getReportMobileStateFromPowerBIPackage(powerBIPackage);
				StaticResourceCollection staticResources = this.GetStaticResources(powerBIPackage);
				IDictionary<string, byte[]> customVisuals = this.GetCustomVisuals(powerBIPackage, areCustomVisualsEnabled);
				ConnectionsSettings connectionSettings = this.GetConnectionSettings(powerBIPackage);
				byte[] dataModel = this.GetDataModel(powerBIPackage);
				PbixReportElements pbixReportElements = new PbixReportElements
				{
					ReportDocument = reportDocumentFromPowerBIPackage,
					ReportMobileState = reportMobileStateFromPowerBIPackage,
					StaticResources = staticResources,
					CustomVisuals = customVisuals,
					ConnectionsSettings = connectionSettings,
					DataModel = dataModel,
					ModelVersion = "PowerBI_V1",
					DataSources = new List<PbixDataSource>()
				};
				this.AppendASDataSourceIfPresentInConnectionSettings(pbixReportElements, connectionSettings);
				this.AppendReportMetadataIfPresentInPackage(powerBIPackage, pbixReportElements, version);
				pbixReportElements2 = pbixReportElements;
			}
			return pbixReportElements2;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002478 File Offset: 0x00000678
		private void AppendReportMetadataIfPresentInPackage(IPowerBIPackage powerBIPackage, PbixReportElements pbixReportElements, Version pbixVersion)
		{
			ReportMetadataContainer reportMetadataContainer;
			ReportMetadataUtils.TryDeserializeReportMetadata(powerBIPackage.ReportMetadata, pbixVersion, out reportMetadataContainer);
			if (reportMetadataContainer != null)
			{
				if (!this.IsFromSupportedRelease(reportMetadataContainer.CreatedFromRelease))
				{
					throw new NewerFileVersionException(reportMetadataContainer.CreatedFromRelease);
				}
				pbixReportElements.AuthorVersion = reportMetadataContainer.CreatedFromRelease;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024C0 File Offset: 0x000006C0
		private void AppendASDataSourceIfPresentInConnectionSettings(PbixReportElements pbixReportElements, ConnectionsSettings connectionsSettings)
		{
			if (connectionsSettings == null)
			{
				return;
			}
			string text = this.GenerateConnectionStringFromConnectionSettings(connectionsSettings);
			if (!string.IsNullOrEmpty(text))
			{
				pbixReportElements.DataSources.Add(new PbixDataSource
				{
					ConnectionString = text,
					Kind = SourceKind.AnalysisServices,
					Type = AccessType.Live
				});
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002506 File Offset: 0x00000706
		private byte[] GetDataModel(IPowerBIPackage powerBIPackage)
		{
			return PowerBIPackagingUtils.GetContentAsBytes(powerBIPackage.DataModel, PowerBIPackager.IsDataModelOptional);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002518 File Offset: 0x00000718
		private ConnectionsSettings GetConnectionSettings(IPowerBIPackage powerBIPackage)
		{
			ConnectionsSettings connectionsSettings = null;
			if (powerBIPackage.Connections != null)
			{
				connectionsSettings = ConnectionsSettingsUtils.DeserializeConnectionsSettings(powerBIPackage.Connections);
			}
			return connectionsSettings;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000253C File Offset: 0x0000073C
		private string GenerateConnectionStringFromConnectionSettings(ConnectionsSettings connectionsSettings)
		{
			string text = null;
			if (connectionsSettings != null && connectionsSettings.Connections != null && connectionsSettings.Connections.ContainsKey("EntityDataSource"))
			{
				text = connectionsSettings.Connections["EntityDataSource"].ConnectionString;
			}
			return text;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002580 File Offset: 0x00000780
		private IDictionary<string, byte[]> GetCustomVisuals(IPowerBIPackage powerBIPackage, bool areCustomVisualsEnabled)
		{
			IDictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>();
			if (areCustomVisualsEnabled && powerBIPackage.CustomVisuals != null && powerBIPackage.CustomVisuals.Count != 0)
			{
				IDictionary<Uri, IStreamablePowerBIPackagePartContent> customVisuals = powerBIPackage.CustomVisuals;
				dictionary = this.ReadCustomVisuals(customVisuals);
			}
			return dictionary;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025BC File Offset: 0x000007BC
		private StaticResourceCollection GetStaticResources(IPowerBIPackage powerBIPackage)
		{
			StaticResourceCollection staticResourceCollection = null;
			if (powerBIPackage.StaticResources != null && powerBIPackage.StaticResources.Count != 0)
			{
				IDictionary<Uri, IStreamablePowerBIPackagePartContent> staticResources = powerBIPackage.StaticResources;
				staticResourceCollection = this.DeserializeStaticResources(staticResources);
			}
			return staticResourceCollection;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025F0 File Offset: 0x000007F0
		private string getReportMobileStateFromPowerBIPackage(IPowerBIPackage powerBIPackage)
		{
			if (powerBIPackage.ReportMobileState == null)
			{
				return null;
			}
			return this.DeserializeStreamablePart(powerBIPackage.ReportMobileState, PowerBIPackager.IsReportMobileStateOptional);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002610 File Offset: 0x00000810
		private string GetReportDocumentFromPowerBIPackage(IPowerBIPackage powerBIPackage)
		{
			if (powerBIPackage.ReportDocument == null)
			{
				return null;
			}
			string text = this.DeserializeStreamablePart(powerBIPackage.ReportDocument, PowerBIPackager.IsReportDocumentOptional);
			JObject jobject = JObject.Parse(text);
			if (!this._areCustomVisualsEnabled && jobject["resourcePackages"] != null)
			{
				object[] array = jobject["resourcePackages"].Where((JToken package) => (int)package["resourcePackage"]["type"] != 0).ToArray<JToken>();
				JArray jarray = new JArray(array);
				jobject["resourcePackages"] = jarray;
			}
			int num = 1;
			JToken jtoken = jobject["resourcePackages"];
			if (jtoken != null)
			{
				foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
				{
					JObject jobject2 = (JObject)jtoken2;
					if (jobject2["resourcePackage"]["id"] == null)
					{
						jobject2["resourcePackage"]["id"] = num.ToString();
						num++;
					}
				}
				text = jobject.ToString();
			}
			return text;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002738 File Offset: 0x00000938
		internal IDictionary<string, byte[]> ReadCustomVisuals(IDictionary<Uri, IStreamablePowerBIPackagePartContent> customVisuals)
		{
			return customVisuals.Select((KeyValuePair<Uri, IStreamablePowerBIPackagePartContent> kvp) => new
			{
				Key = this.FormatResourceName(kvp.Key),
				Value = kvp.Value.GetStream().ReadAllBytes()
			}).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000279C File Offset: 0x0000099C
		internal bool IsFromSupportedRelease(string version)
		{
			PbixReleaseVersion pbixReleaseVersion;
			return PbixReleaseVersion.TryParse(version, out pbixReleaseVersion, null) && pbixReleaseVersion.CompareTo(PbixParserV1.MaxKnownRelease) <= 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027C8 File Offset: 0x000009C8
		private string FormatResourceName(Uri key)
		{
			string[] array = key.ToString().Split(new char[] { '/' });
			return array.First<string>() + "." + array.Last<string>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002804 File Offset: 0x00000A04
		private StaticResourceCollection DeserializeStaticResources(IDictionary<Uri, IStreamablePowerBIPackagePartContent> streamablePowerBIPackagePartContents)
		{
			StaticResourceCollection staticResourceCollection = new StaticResourceCollection();
			if (streamablePowerBIPackagePartContents != null)
			{
				foreach (KeyValuePair<Uri, IStreamablePowerBIPackagePartContent> keyValuePair in streamablePowerBIPackagePartContents)
				{
					string originalString = keyValuePair.Key.OriginalString;
					byte[] contentAsBytes = PowerBIPackagingUtils.GetContentAsBytes(keyValuePair.Value, PowerBIPackager.IsStaticResourcesOptional);
					staticResourceCollection.AddResource(originalString, contentAsBytes);
				}
			}
			return staticResourceCollection;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002878 File Offset: 0x00000A78
		private string DeserializeStreamablePart(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, bool isOptional)
		{
			byte[] contentAsBytes = PowerBIPackagingUtils.GetContentAsBytes(streamablePowerBIPackagePartContent, isOptional);
			string reportDocumentContentString = this.GetReportDocumentContentString(contentAsBytes);
			if (string.IsNullOrWhiteSpace(reportDocumentContentString))
			{
				return null;
			}
			return reportDocumentContentString;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028A0 File Offset: 0x00000AA0
		private string GetReportDocumentContentString(byte[] content)
		{
			string text = null;
			if (content != null)
			{
				text = Encoding.Unicode.GetString(content);
			}
			return text;
		}

		// Token: 0x04000063 RID: 99
		public static readonly PbixReleaseVersion MaxKnownRelease = new PbixReleaseVersion(2024, 9, 0);

		// Token: 0x04000064 RID: 100
		internal const string V1ModelVersion = "PowerBI_V1";

		// Token: 0x04000065 RID: 101
		internal const int ResourceTypeCustomVisual = 0;

		// Token: 0x04000066 RID: 102
		private readonly bool _areCustomVisualsEnabled;
	}
}
