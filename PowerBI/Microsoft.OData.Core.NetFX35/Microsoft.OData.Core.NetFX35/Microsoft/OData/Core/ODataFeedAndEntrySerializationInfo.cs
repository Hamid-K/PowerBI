using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000174 RID: 372
	public sealed class ODataFeedAndEntrySerializationInfo
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00031676 File Offset: 0x0002F876
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x0003167E File Offset: 0x0002F87E
		public string NavigationSourceName
		{
			get
			{
				return this.navigationSourceName;
			}
			set
			{
				this.navigationSourceName = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00031687 File Offset: 0x0002F887
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0003168F File Offset: 0x0002F88F
		public string NavigationSourceEntityTypeName
		{
			get
			{
				return this.navigationSourceEntityTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "NavigationSourceEntityTypeName");
				this.navigationSourceEntityTypeName = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000316A3 File Offset: 0x0002F8A3
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000316AB File Offset: 0x0002F8AB
		public EdmNavigationSourceKind NavigationSourceKind { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000316B4 File Offset: 0x0002F8B4
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x000316BC File Offset: 0x0002F8BC
		public bool IsFromCollection { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000316C5 File Offset: 0x0002F8C5
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x000316D7 File Offset: 0x0002F8D7
		public string ExpectedTypeName
		{
			get
			{
				return this.expectedTypeName ?? this.NavigationSourceEntityTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "ExpectedTypeName");
				this.expectedTypeName = value;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000316EB File Offset: 0x0002F8EB
		internal static ODataFeedAndEntrySerializationInfo Validate(ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				if (serializationInfo.NavigationSourceKind != EdmNavigationSourceKind.UnknownEntitySet)
				{
					ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceName, "serializationInfo.NavigationSourceName");
				}
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceEntityTypeName, "serializationInfo.NavigationSourceEntityTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040005F1 RID: 1521
		private string navigationSourceName;

		// Token: 0x040005F2 RID: 1522
		private string navigationSourceEntityTypeName;

		// Token: 0x040005F3 RID: 1523
		private string expectedTypeName;
	}
}
