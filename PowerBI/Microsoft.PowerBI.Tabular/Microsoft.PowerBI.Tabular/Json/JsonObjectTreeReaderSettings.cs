using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B4 RID: 436
	internal sealed class JsonObjectTreeReaderSettings
	{
		// Token: 0x06001AAC RID: 6828 RVA: 0x000B18C5 File Offset: 0x000AFAC5
		internal JsonObjectTreeReaderSettings(JsonObjectTreeReaderSettings.ReadObjectMethod readMethod, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			this.readMethod = readMethod;
			this.mode = mode;
			this.dbCompatibilityLevel = dbCompatibilityLevel;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x000B18E2 File Offset: 0x000AFAE2
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x000B18EA File Offset: 0x000AFAEA
		public Predicate<ObjectType> ReadObjectFilter { get; set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x000B18F3 File Offset: 0x000AFAF3
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x000B18FB File Offset: 0x000AFAFB
		public Predicate<ObjectType> ReadCollectionFilter { get; set; }

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000B1904 File Offset: 0x000AFB04
		internal bool ShouldReadObject(ObjectType type)
		{
			return ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(type, this.mode, this.dbCompatibilityLevel) && (this.ReadObjectFilter == null || this.ReadObjectFilter(type));
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000B1932 File Offset: 0x000AFB32
		internal bool ShouldReadCollection(ObjectType type)
		{
			return ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(type, this.mode, this.dbCompatibilityLevel) && (this.ReadCollectionFilter == null || this.ReadCollectionFilter(type));
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000B1960 File Offset: 0x000AFB60
		internal void ReadObject(JObject jObj, ObjectType type, ObjectPath currentPath)
		{
			this.readMethod(jObj, type, this.mode, this.dbCompatibilityLevel, currentPath);
		}

		// Token: 0x04000523 RID: 1315
		private readonly JsonObjectTreeReaderSettings.ReadObjectMethod readMethod;

		// Token: 0x04000524 RID: 1316
		private readonly CompatibilityMode mode;

		// Token: 0x04000525 RID: 1317
		private readonly int dbCompatibilityLevel;

		// Token: 0x020003FA RID: 1018
		// (Invoke) Token: 0x06002753 RID: 10067
		public delegate void ReadObjectMethod(JObject jObj, ObjectType type, CompatibilityMode mode, int dbCompatibilityLevel, ObjectPath currentPath);
	}
}
