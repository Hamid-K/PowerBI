using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200003E RID: 62
	public sealed class ConnectionProperties
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000066E9 File Offset: 0x000048E9
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000066F1 File Offset: 0x000048F1
		public string Name { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000066FA File Offset: 0x000048FA
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006702 File Offset: 0x00004902
		public string ConnectionString { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000670B File Offset: 0x0000490B
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006713 File Offset: 0x00004913
		public bool IsMultiDimensional { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000671C File Offset: 0x0000491C
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00006724 File Offset: 0x00004924
		public string ConnectionType { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000672D File Offset: 0x0000492D
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00006735 File Offset: 0x00004935
		public long? PbiServiceModelId { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000673E File Offset: 0x0000493E
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006746 File Offset: 0x00004946
		public string PbiModelVirtualServerName { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000674F File Offset: 0x0000494F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00006757 File Offset: 0x00004957
		public string PbiModelDatabaseName { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006760 File Offset: 0x00004960
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006768 File Offset: 0x00004968
		public string PbiServiceGroupId { get; set; }

		// Token: 0x060001BD RID: 445 RVA: 0x00006771 File Offset: 0x00004971
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConnectionProperties);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006780 File Offset: 0x00004980
		public override int GetHashCode()
		{
			long? num;
			return ConnectionProperties.stringComparer.GetHashCode(this.Name ?? string.Empty) ^ ConnectionProperties.stringComparer.GetHashCode(this.ConnectionString ?? string.Empty) ^ this.IsMultiDimensional.GetHashCode() ^ ConnectionProperties.stringComparer.GetHashCode(this.ConnectionType ?? string.Empty) ^ ((this.PbiServiceModelId != null) ? num.GetValueOrDefault().GetHashCode() : 0) ^ ConnectionProperties.stringComparer.GetHashCode(this.PbiModelVirtualServerName ?? string.Empty) ^ ConnectionProperties.stringComparer.GetHashCode(this.PbiModelDatabaseName ?? string.Empty) ^ ConnectionProperties.stringComparer.GetHashCode(this.PbiServiceGroupId ?? string.Empty);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000685C File Offset: 0x00004A5C
		public bool Equals(ConnectionProperties obj)
		{
			if (obj != null && ConnectionProperties.stringComparer.Equals(this.Name, obj.Name) && ConnectionProperties.stringComparer.Equals(this.ConnectionString, obj.ConnectionString) && this.IsMultiDimensional == obj.IsMultiDimensional && ConnectionProperties.stringComparer.Equals(this.ConnectionType, obj.ConnectionType))
			{
				long? pbiServiceModelId = this.PbiServiceModelId;
				long? pbiServiceModelId2 = obj.PbiServiceModelId;
				if (((pbiServiceModelId.GetValueOrDefault() == pbiServiceModelId2.GetValueOrDefault()) & (pbiServiceModelId != null == (pbiServiceModelId2 != null))) && ConnectionProperties.stringComparer.Equals(this.PbiModelVirtualServerName, obj.PbiModelVirtualServerName) && ConnectionProperties.stringComparer.Equals(this.PbiModelDatabaseName, obj.PbiModelDatabaseName))
				{
					return ConnectionProperties.stringComparer.Equals(this.PbiServiceGroupId, obj.PbiServiceGroupId);
				}
			}
			return false;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006948 File Offset: 0x00004B48
		public string ToTelemetryString()
		{
			return JsonConvert.SerializeObject(new ConnectionProperties
			{
				Name = this.Name,
				ConnectionString = null,
				IsMultiDimensional = this.IsMultiDimensional,
				ConnectionType = this.ConnectionType,
				PbiServiceModelId = this.PbiServiceModelId,
				PbiModelVirtualServerName = this.PbiModelVirtualServerName,
				PbiModelDatabaseName = this.PbiModelDatabaseName,
				PbiServiceGroupId = this.PbiServiceGroupId
			});
		}

		// Token: 0x040000F7 RID: 247
		public const string PowerViewDefaultDataSource = "EntityDataSource";

		// Token: 0x040000F8 RID: 248
		private static readonly StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
	}
}
