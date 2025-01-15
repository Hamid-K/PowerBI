using System;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000DF RID: 223
	internal static class Constants
	{
		// Token: 0x020001A4 RID: 420
		internal static class AvailableSerializers
		{
			// Token: 0x040010AE RID: 4270
			internal const string DesignXmlSerializer = "Microsoft.DataWarehouse.Serialization.DesignXmlSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

			// Token: 0x040010AF RID: 4271
			internal const string DesignerComponentSerializer = "Microsoft.DataWarehouse.Serialization.DesignerComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

			// Token: 0x040010B0 RID: 4272
			internal const string OnlineComponentSerializer = "Microsoft.DataWarehouse.Serialization.OnlineComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

			// Token: 0x040010B1 RID: 4273
			internal const string OnlineDatabaseSerializer = "Microsoft.AnalysisServices.Project.ComponentModel.DatabaseOnlineSerializer, Microsoft.AnalysisServices.Project.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
		}

		// Token: 0x020001A5 RID: 421
		internal static class ConfigurationOptionStrings
		{
			// Token: 0x040010B2 RID: 4274
			internal const string DatabaseStorageLocations = "DatabaseStorageLocations";

			// Token: 0x040010B3 RID: 4275
			internal const string ConnectionMappings = "ConnectionMappings";

			// Token: 0x040010B4 RID: 4276
			internal const string ConnectionSecurityMappings = "ConnectionSecurityMappings";

			// Token: 0x040010B5 RID: 4277
			internal const string ConnectionProviderMappings = "ConnectionProviderMappings";

			// Token: 0x040010B6 RID: 4278
			internal const string CubeStorageLocations = "CubeStorageLocations";

			// Token: 0x040010B7 RID: 4279
			internal const string CubeKeyErrorLogFiles = "CubeKeyErrorLogFiles";

			// Token: 0x040010B8 RID: 4280
			internal const string MeasureGroupStorageLocations = "MeasureGroupStorageLocations";

			// Token: 0x040010B9 RID: 4281
			internal const string MeasureGroupKeyErrorLogFiles = "MeasureGroupKeyErrorLogFiles";

			// Token: 0x040010BA RID: 4282
			internal const string PartitionStorageLocations = "PartitionStorageLocations";

			// Token: 0x040010BB RID: 4283
			internal const string PartitionKeyErrorLogFiles = "PartitionKeyErrorLogFiles";

			// Token: 0x040010BC RID: 4284
			internal const string ReportActionServers = "ReportActionServers";

			// Token: 0x040010BD RID: 4285
			internal const string ReportActionPaths = "ReportActionPaths";

			// Token: 0x040010BE RID: 4286
			internal const string DimensionKeyErrorLogFiles = "DimensionKeyErrorLogFiles";

			// Token: 0x040010BF RID: 4287
			internal const string MiningStructureKeyErrorLogFiles = "MiningStructureKeyErrorLogFiles";
		}

		// Token: 0x020001A6 RID: 422
		internal static class DesignerTypes
		{
			// Token: 0x040010C0 RID: 4288
			internal const string MajorObjectDesigner = "Microsoft.AnalysisServices.Design.MajorObjectDesigner, Microsoft.AnalysisServices.Design.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

			// Token: 0x040010C1 RID: 4289
			internal const string NameComponentDesigner = "Microsoft.DataWarehouse.Design.NameComponentDesigner, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

			// Token: 0x040010C2 RID: 4290
			internal const string ModelComponentDesigner = "Microsoft.DataWarehouse.Design.ModelComponentDesigner, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

			// Token: 0x040010C3 RID: 4291
			internal const string EmptyComponentDesigner = "Microsoft.DataWarehouse.Design.EmptyComponentDesigner, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
		}

		// Token: 0x020001A7 RID: 423
		internal static class DatabaseCompatibilityLevel
		{
			// Token: 0x040010C4 RID: 4292
			public const int KJ = 1050;

			// Token: 0x040010C5 RID: 4293
			public const int SQL11 = 1100;

			// Token: 0x040010C6 RID: 4294
			public const int SQL11_SP1 = 1103;

			// Token: 0x040010C7 RID: 4295
			public const int SQL16 = 1200;

			// Token: 0x040010C8 RID: 4296
			public const int SQL17 = 1400;
		}
	}
}
