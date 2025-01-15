using System;
using System.Collections.Generic;
using System.IO.Packaging;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000023 RID: 35
	public sealed class PowerBIPackage : IPowerBIPackage, IDisposable
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public PowerBIPackage(IStreamablePowerBIPackagePartContent connection, IStreamablePowerBIPackagePartContent dataMashup, IStreamablePowerBIPackagePartContent unappliedChanges, IStreamablePowerBIPackagePartContent dataModel, IStreamablePowerBIPackagePartContent dataModelSchema, IStreamablePowerBIPackagePartContent diagramViewState, IStreamablePowerBIPackagePartContent diagramLayout, IStreamablePowerBIPackagePartContent reportDocument, IStreamablePowerBIPackagePartContent reportLinguisticSchema, IStreamablePowerBIPackagePartContent reportMetadata, IStreamablePowerBIPackagePartContent reportSettings, IStreamablePowerBIPackagePartContent version, IStreamablePowerBIPackagePartContent customProperties, IStreamablePowerBIPackagePartContent reportMobileState, IDictionary<Uri, IStreamablePowerBIPackagePartContent> customVisuals, IDictionary<Uri, IStreamablePowerBIPackagePartContent> staticResources, IDictionary<Uri, IStreamablePowerBIPackagePartContent> daxQueryView, IDictionary<Uri, IStreamablePowerBIPackagePartContent> exploration)
		{
			this.Connections = connection;
			this.DataMashup = dataMashup;
			this.UnappliedChanges = unappliedChanges;
			this.DataModel = dataModel;
			this.DataModelSchema = dataModelSchema;
			this.DiagramViewState = diagramViewState;
			this.DiagramLayout = diagramLayout;
			this.ReportDocument = reportDocument;
			this.LinguisticSchema = reportLinguisticSchema;
			this.ReportMetadata = reportMetadata;
			this.ReportSettings = reportSettings;
			this.Version = version;
			this.CustomProperties = customProperties;
			this.CustomVisuals = customVisuals;
			this.ReportMobileState = reportMobileState;
			this.StaticResources = staticResources;
			this.DaxQueryView = daxQueryView;
			this.Exploration = exploration;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C74 File Offset: 0x00001E74
		internal PowerBIPackage(Package sourcePackage, IStreamablePowerBIPackagePartContent connection, IStreamablePowerBIPackagePartContent dataMashup, IStreamablePowerBIPackagePartContent unappliedChanges, IStreamablePowerBIPackagePartContent dataModel, IStreamablePowerBIPackagePartContent dataModelSchema, IStreamablePowerBIPackagePartContent diagramViewState, IStreamablePowerBIPackagePartContent diagramLayout, IStreamablePowerBIPackagePartContent reportDocument, IStreamablePowerBIPackagePartContent reportLinguisticSchema, IStreamablePowerBIPackagePartContent reportMetadata, IStreamablePowerBIPackagePartContent reportSettings, IStreamablePowerBIPackagePartContent version, IStreamablePowerBIPackagePartContent customXml, IStreamablePowerBIPackagePartContent reportMobileState, IDictionary<Uri, IStreamablePowerBIPackagePartContent> customVisuals, IDictionary<Uri, IStreamablePowerBIPackagePartContent> staticResources, IDictionary<Uri, IStreamablePowerBIPackagePartContent> daxQueryView, IDictionary<Uri, IStreamablePowerBIPackagePartContent> exploration)
			: this(connection, dataMashup, unappliedChanges, dataModel, dataModelSchema, diagramViewState, diagramLayout, reportDocument, reportLinguisticSchema, reportMetadata, reportSettings, version, customXml, reportMobileState, customVisuals, staticResources, daxQueryView, exploration)
		{
			this.sourcePackage = sourcePackage;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003CB0 File Offset: 0x00001EB0
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public IStreamablePowerBIPackagePartContent Connections { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003CC1 File Offset: 0x00001EC1
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003CC9 File Offset: 0x00001EC9
		public IStreamablePowerBIPackagePartContent DataMashup { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003CD2 File Offset: 0x00001ED2
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003CDA File Offset: 0x00001EDA
		public IStreamablePowerBIPackagePartContent UnappliedChanges { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003CE3 File Offset: 0x00001EE3
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003CEB File Offset: 0x00001EEB
		public IStreamablePowerBIPackagePartContent DataModel { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003CF4 File Offset: 0x00001EF4
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003CFC File Offset: 0x00001EFC
		public IStreamablePowerBIPackagePartContent DataModelSchema { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003D05 File Offset: 0x00001F05
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003D0D File Offset: 0x00001F0D
		public IStreamablePowerBIPackagePartContent DiagramViewState { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003D16 File Offset: 0x00001F16
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003D1E File Offset: 0x00001F1E
		public IStreamablePowerBIPackagePartContent DiagramLayout { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003D27 File Offset: 0x00001F27
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003D2F File Offset: 0x00001F2F
		public IStreamablePowerBIPackagePartContent ReportDocument { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003D38 File Offset: 0x00001F38
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003D40 File Offset: 0x00001F40
		public IStreamablePowerBIPackagePartContent LinguisticSchema { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003D49 File Offset: 0x00001F49
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003D51 File Offset: 0x00001F51
		public IStreamablePowerBIPackagePartContent ReportMetadata { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003D5A File Offset: 0x00001F5A
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003D62 File Offset: 0x00001F62
		public IStreamablePowerBIPackagePartContent ReportSettings { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003D6B File Offset: 0x00001F6B
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003D73 File Offset: 0x00001F73
		public IStreamablePowerBIPackagePartContent Version { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003D7C File Offset: 0x00001F7C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003D84 File Offset: 0x00001F84
		public IStreamablePowerBIPackagePartContent CustomProperties { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003D8D File Offset: 0x00001F8D
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003D95 File Offset: 0x00001F95
		public IStreamablePowerBIPackagePartContent ReportMobileState { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003D9E File Offset: 0x00001F9E
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003DA6 File Offset: 0x00001FA6
		public IDictionary<Uri, IStreamablePowerBIPackagePartContent> CustomVisuals { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003DAF File Offset: 0x00001FAF
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public IDictionary<Uri, IStreamablePowerBIPackagePartContent> StaticResources { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003DC0 File Offset: 0x00001FC0
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public IDictionary<Uri, IStreamablePowerBIPackagePartContent> DaxQueryView { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003DD1 File Offset: 0x00001FD1
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003DD9 File Offset: 0x00001FD9
		public IDictionary<Uri, IStreamablePowerBIPackagePartContent> Exploration { get; set; }

		// Token: 0x060000EA RID: 234 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public void Dispose()
		{
			if (this.sourcePackage != null)
			{
				this.sourcePackage.Close();
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly Package sourcePackage;
	}
}
