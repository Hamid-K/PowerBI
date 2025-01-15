using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x020003A1 RID: 929
	internal class OpenApiUserSettings
	{
		// Token: 0x06002052 RID: 8274 RVA: 0x0005507B File Offset: 0x0005327B
		private OpenApiUserSettings(RecordValue rawOption)
		{
			this.rawOption = rawOption;
			this.IncludeExtensions = true;
			this.IncludeDeprecated = false;
			this.ManualCredentials = false;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000550A0 File Offset: 0x000532A0
		public static OpenApiUserSettings BuildUserSettings(Value options)
		{
			OpenApiUserSettings openApiUserSettings;
			if (options.IsRecord)
			{
				openApiUserSettings = new OpenApiUserSettings(options.AsRecord);
				RecordValue asRecord = options.AsRecord;
				Options.ValidateOptions(asRecord, OpenApiUserSettings.validOptionKeys, "OpenApi.Document", null);
				Value value;
				if (asRecord.TryGetValue("IncludeExtensions", out value))
				{
					openApiUserSettings.IncludeExtensions = value.AsBoolean;
				}
				Value value2;
				if (asRecord.TryGetValue("SecurityDefinition", out value2))
				{
					openApiUserSettings.SecurityDefinitionName = value2.AsString;
				}
				Value value3;
				if (asRecord.TryGetValue("ManualStatusHandling", out value3))
				{
					openApiUserSettings.ManualStatusHandling = value3.AsList;
				}
				Value value4;
				if (asRecord.TryGetValue("IncludeDeprecated", out value4))
				{
					openApiUserSettings.IncludeDeprecated = value4.AsBoolean;
				}
				Value value5;
				if (asRecord.TryGetValue("IncludeMoreColumns", out value5))
				{
					openApiUserSettings.IncludeMoreColumns = value5.AsBoolean;
				}
				Value value6;
				if (asRecord.TryGetValue("ManualCredentials", out value6))
				{
					openApiUserSettings.ManualCredentials = value6.AsBoolean;
				}
			}
			else
			{
				openApiUserSettings = new OpenApiUserSettings(RecordValue.Empty);
			}
			return openApiUserSettings;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0005518E File Offset: 0x0005338E
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x00055196 File Offset: 0x00053396
		public bool IncludeExtensions { get; private set; }

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0005519F File Offset: 0x0005339F
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x000551A7 File Offset: 0x000533A7
		public string SecurityDefinitionName { get; private set; }

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x000551B0 File Offset: 0x000533B0
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x000551B8 File Offset: 0x000533B8
		public ListValue ManualStatusHandling { get; private set; }

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x000551C1 File Offset: 0x000533C1
		// (set) Token: 0x0600205B RID: 8283 RVA: 0x000551C9 File Offset: 0x000533C9
		public bool IncludeDeprecated { get; private set; }

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x000551D2 File Offset: 0x000533D2
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x000551DA File Offset: 0x000533DA
		public bool IncludeMoreColumns { get; private set; }

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x000551E3 File Offset: 0x000533E3
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x000551EB File Offset: 0x000533EB
		public bool ManualCredentials { get; private set; }

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x000551F4 File Offset: 0x000533F4
		public RecordValue Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = this.getRecordValue("Headers");
				}
				return this.headers;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00055215 File Offset: 0x00053415
		public RecordValue Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = this.getRecordValue("Query");
				}
				return this.query;
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00055238 File Offset: 0x00053438
		private RecordValue getRecordValue(string key)
		{
			Value value;
			if (this.rawOption.TryGetValue(key, out value) && value.IsRecord)
			{
				return value.AsRecord;
			}
			return RecordValue.Empty;
		}

		// Token: 0x04000C56 RID: 3158
		private static readonly HashSet<string> validOptionKeys = new HashSet<string> { "IncludeExtensions", "SecurityDefinition", "ManualStatusHandling", "IncludeDeprecated", "IncludeMoreColumns", "ManualCredentials", "Query", "Headers" };

		// Token: 0x04000C57 RID: 3159
		private readonly RecordValue rawOption;

		// Token: 0x04000C58 RID: 3160
		private RecordValue headers;

		// Token: 0x04000C59 RID: 3161
		private RecordValue query;
	}
}
