using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200001C RID: 28
	public interface IPowerBIPackage : IDisposable
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000098 RID: 152
		// (set) Token: 0x06000099 RID: 153
		IStreamablePowerBIPackagePartContent Connections { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009A RID: 154
		// (set) Token: 0x0600009B RID: 155
		IStreamablePowerBIPackagePartContent DataMashup { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009C RID: 156
		// (set) Token: 0x0600009D RID: 157
		IStreamablePowerBIPackagePartContent UnappliedChanges { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009E RID: 158
		// (set) Token: 0x0600009F RID: 159
		IStreamablePowerBIPackagePartContent DataModel { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A0 RID: 160
		// (set) Token: 0x060000A1 RID: 161
		IStreamablePowerBIPackagePartContent DataModelSchema { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A2 RID: 162
		// (set) Token: 0x060000A3 RID: 163
		IStreamablePowerBIPackagePartContent DiagramViewState { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A4 RID: 164
		// (set) Token: 0x060000A5 RID: 165
		IStreamablePowerBIPackagePartContent DiagramLayout { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166
		// (set) Token: 0x060000A7 RID: 167
		IStreamablePowerBIPackagePartContent ReportDocument { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A8 RID: 168
		// (set) Token: 0x060000A9 RID: 169
		IStreamablePowerBIPackagePartContent LinguisticSchema { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AA RID: 170
		// (set) Token: 0x060000AB RID: 171
		IStreamablePowerBIPackagePartContent ReportMetadata { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AC RID: 172
		// (set) Token: 0x060000AD RID: 173
		IStreamablePowerBIPackagePartContent ReportSettings { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AE RID: 174
		// (set) Token: 0x060000AF RID: 175
		IStreamablePowerBIPackagePartContent Version { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B0 RID: 176
		// (set) Token: 0x060000B1 RID: 177
		IStreamablePowerBIPackagePartContent CustomProperties { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B2 RID: 178
		// (set) Token: 0x060000B3 RID: 179
		IStreamablePowerBIPackagePartContent ReportMobileState { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B4 RID: 180
		// (set) Token: 0x060000B5 RID: 181
		IDictionary<Uri, IStreamablePowerBIPackagePartContent> CustomVisuals { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182
		// (set) Token: 0x060000B7 RID: 183
		IDictionary<Uri, IStreamablePowerBIPackagePartContent> StaticResources { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B8 RID: 184
		// (set) Token: 0x060000B9 RID: 185
		IDictionary<Uri, IStreamablePowerBIPackagePartContent> DaxQueryView { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186
		// (set) Token: 0x060000BB RID: 187
		IDictionary<Uri, IStreamablePowerBIPackagePartContent> Exploration { get; set; }
	}
}
