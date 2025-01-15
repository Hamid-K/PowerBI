using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000154 RID: 340
	[DataContract(Name = "ConceptualEntityCapabilities", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal class ClientConceptualEntityCapabilities
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x00012039 File Offset: 0x00010239
		internal ClientConceptualEntityCapabilities()
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00012044 File Offset: 0x00010244
		internal ClientConceptualEntityCapabilities(bool canRefresh, bool canEditSource, bool canRename, bool canDelete, bool canEditStorageMode, bool supportsClustering, bool supportsBinByCount, int? limitOnNumberOfGroups, DataViewCapabilities dataViewCapabilities = null)
		{
			this._canRefresh = ClientConceptualSchemaFactory.ConvertTrueToNull(canRefresh);
			this._canEditSource = ClientConceptualSchemaFactory.ConvertTrueToNull(canEditSource);
			this._canRename = ClientConceptualSchemaFactory.ConvertTrueToNull(canRename);
			this._canDelete = ClientConceptualSchemaFactory.ConvertTrueToNull(canDelete);
			this._canEditStorageMode = ClientConceptualSchemaFactory.ConvertTrueToNull(canEditStorageMode);
			this._supportsClustering = ClientConceptualSchemaFactory.ConvertTrueToNull(supportsClustering);
			this._supportsBinByCount = ClientConceptualSchemaFactory.ConvertTrueToNull(supportsBinByCount);
			this._limitOnNumberOfGroups = limitOnNumberOfGroups;
			this._dataViewCapabilities = dataViewCapabilities;
		}

		// Token: 0x0400043C RID: 1084
		[DataMember(Name = "CanRefresh", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private bool? _canRefresh;

		// Token: 0x0400043D RID: 1085
		[DataMember(Name = "CanEditSource", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private bool? _canEditSource;

		// Token: 0x0400043E RID: 1086
		[DataMember(Name = "CanRename", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private bool? _canRename;

		// Token: 0x0400043F RID: 1087
		[DataMember(Name = "CanDelete", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private bool? _canDelete;

		// Token: 0x04000440 RID: 1088
		[DataMember(Name = "CanEditStorageMode", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private bool? _canEditStorageMode;

		// Token: 0x04000441 RID: 1089
		[DataMember(Name = "SupportsClustering", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private bool? _supportsClustering;

		// Token: 0x04000442 RID: 1090
		[DataMember(Name = "SupportsBinByCount", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private bool? _supportsBinByCount;

		// Token: 0x04000443 RID: 1091
		[DataMember(Name = "LimitOnNumberOfGroups", IsRequired = false, EmitDefaultValue = false, Order = 7)]
		private int? _limitOnNumberOfGroups;

		// Token: 0x04000444 RID: 1092
		[DataMember(Name = "DataView", IsRequired = false, EmitDefaultValue = false, Order = 8)]
		private DataViewCapabilities _dataViewCapabilities;
	}
}
