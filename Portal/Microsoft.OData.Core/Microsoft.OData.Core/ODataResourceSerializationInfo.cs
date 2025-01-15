using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000086 RID: 134
	public sealed class ODataResourceSerializationInfo
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000C0E2 File Offset: 0x0000A2E2
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000C0EB File Offset: 0x0000A2EB
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000C107 File Offset: 0x0000A307
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000C10F File Offset: 0x0000A30F
		public EdmNavigationSourceKind NavigationSourceKind { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000C118 File Offset: 0x0000A318
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000C120 File Offset: 0x0000A320
		public bool IsFromCollection { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000C129 File Offset: 0x0000A329
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000C13B File Offset: 0x0000A33B
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

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000C14F File Offset: 0x0000A34F
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

		// Token: 0x04000212 RID: 530
		private string navigationSourceName;

		// Token: 0x04000213 RID: 531
		private string navigationSourceEntityTypeName;

		// Token: 0x04000214 RID: 532
		private string expectedTypeName;
	}
}
