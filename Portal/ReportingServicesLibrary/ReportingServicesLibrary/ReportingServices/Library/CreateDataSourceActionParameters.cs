using System;
using System.Globalization;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E3 RID: 227
	internal sealed class CreateDataSourceActionParameters : CreateItemActionParameters
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00025F4A File Offset: 0x0002414A
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00025F52 File Offset: 0x00024152
		public DataSourceDefinition DataSourceDefinition
		{
			get
			{
				return this.m_dataSourceDefinition;
			}
			set
			{
				this.m_dataSourceDefinition = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0000FB45 File Offset: 0x0000DD45
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, base.Overwrite);
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00025F5C File Offset: 0x0002415C
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("DataSource");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.DataSourceDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
			if (HttpUtility.UrlDecode(base.ItemName).Length != base.ItemName.Length)
			{
				throw new InvalidItemNameException("Name contains URL-encoded characters", CatalogItemNameUtility.MaxItemNameLength);
			}
		}

		// Token: 0x0400046C RID: 1132
		private DataSourceDefinition m_dataSourceDefinition;
	}
}
