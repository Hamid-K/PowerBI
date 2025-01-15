using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000062 RID: 98
	public sealed class ODataResourceSerializationInfo
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000A17A File Offset: 0x0000837A
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000A182 File Offset: 0x00008382
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000A18B File Offset: 0x0000838B
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000A193 File Offset: 0x00008393
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000A1A7 File Offset: 0x000083A7
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000A1AF File Offset: 0x000083AF
		public EdmNavigationSourceKind NavigationSourceKind { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000A1B8 File Offset: 0x000083B8
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public bool IsFromCollection { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A1C9 File Offset: 0x000083C9
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000A1DB File Offset: 0x000083DB
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

		// Token: 0x06000325 RID: 805 RVA: 0x0000A1EF File Offset: 0x000083EF
		internal static ODataResourceSerializationInfo Validate(ODataResourceSerializationInfo serializationInfo)
		{
			if (serializationInfo != null && serializationInfo.NavigationSourceKind != EdmNavigationSourceKind.None)
			{
				if (serializationInfo.NavigationSourceKind != EdmNavigationSourceKind.UnknownEntitySet)
				{
					ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceName, "serializationInfo.NavigationSourceName");
				}
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceEntityTypeName, "serializationInfo.NavigationSourceEntityTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040001B3 RID: 435
		private string navigationSourceName;

		// Token: 0x040001B4 RID: 436
		private string navigationSourceEntityTypeName;

		// Token: 0x040001B5 RID: 437
		private string expectedTypeName;
	}
}
