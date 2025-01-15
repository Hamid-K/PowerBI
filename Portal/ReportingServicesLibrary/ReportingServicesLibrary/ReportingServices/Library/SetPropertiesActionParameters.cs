using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B5 RID: 181
	internal sealed class SetPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0002154C File Offset: 0x0001F74C
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00021554 File Offset: 0x0001F754
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

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0002155D File Offset: 0x0001F75D
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00021565 File Offset: 0x0001F765
		public Property[] Properties
		{
			get
			{
				return this.m_properties;
			}
			set
			{
				this.m_properties = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0002156E File Offset: 0x0001F76E
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00021576 File Offset: 0x0001F776
		public DateTime ModificationDate
		{
			get
			{
				return this.m_modificationDate;
			}
			internal set
			{
				this.m_modificationDate = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0002157F File Offset: 0x0001F77F
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00021587 File Offset: 0x0001F787
		public bool IgnoreSecCheck
		{
			get
			{
				return this.m_ignoreSecCheck;
			}
			set
			{
				this.m_ignoreSecCheck = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00021590 File Offset: 0x0001F790
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00021598 File Offset: 0x0001F798
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
			if (this.Properties == null)
			{
				throw new MissingParameterException("Properties");
			}
		}

		// Token: 0x04000417 RID: 1047
		private string m_itemPath;

		// Token: 0x04000418 RID: 1048
		private Property[] m_properties;

		// Token: 0x04000419 RID: 1049
		private DateTime m_modificationDate;

		// Token: 0x0400041A RID: 1050
		private bool m_ignoreSecCheck;
	}
}
