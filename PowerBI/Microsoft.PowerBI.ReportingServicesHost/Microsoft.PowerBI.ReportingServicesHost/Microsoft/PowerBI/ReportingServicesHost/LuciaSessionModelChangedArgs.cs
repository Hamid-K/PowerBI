using System;
using System.Collections.Immutable;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200004A RID: 74
	public sealed class LuciaSessionModelChangedArgs
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public LuciaSessionModelChangedArgs(DateTime lastModifiedTime, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate = null, string filePath = null)
		{
			this.LastModifiedTime = lastModifiedTime;
			this.SchemaItemsToInvalidate = schemaItemsToInvalidate;
			this.FilePath = filePath;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004E11 File Offset: 0x00003011
		public DateTime LastModifiedTime { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004E19 File Offset: 0x00003019
		public ImmutableHashSet<SchemaItem> SchemaItemsToInvalidate { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004E21 File Offset: 0x00003021
		public string FilePath { get; }
	}
}
