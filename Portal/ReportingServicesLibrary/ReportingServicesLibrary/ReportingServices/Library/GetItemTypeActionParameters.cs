using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B7 RID: 183
	internal sealed class GetItemTypeActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x000217D6 File Offset: 0x0001F9D6
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x000217DE File Offset: 0x0001F9DE
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000217E7 File Offset: 0x0001F9E7
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x000217EF File Offset: 0x0001F9EF
		public int ItemType
		{
			get
			{
				return this.m_itemType;
			}
			set
			{
				this.m_itemType = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000217F8 File Offset: 0x0001F9F8
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00021800 File Offset: 0x0001FA00
		public bool AllowEditSessions
		{
			get
			{
				return this.m_allowEditSessions;
			}
			set
			{
				this.m_allowEditSessions = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00021809 File Offset: 0x0001FA09
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00021814 File Offset: 0x0001FA14
		internal override string OutputTrace
		{
			get
			{
				return StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(this.ItemType.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00021839 File Offset: 0x0001FA39
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x0400041B RID: 1051
		private string m_itemPath;

		// Token: 0x0400041C RID: 1052
		private int m_itemType;

		// Token: 0x0400041D RID: 1053
		private bool m_allowEditSessions;
	}
}
