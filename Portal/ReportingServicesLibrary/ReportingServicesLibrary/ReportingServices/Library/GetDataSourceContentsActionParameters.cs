using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E5 RID: 229
	internal sealed class GetDataSourceContentsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00026186 File Offset: 0x00024386
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0002618E File Offset: 0x0002438E
		public string DataSourcePath
		{
			get
			{
				return this.m_DataSourcePath;
			}
			set
			{
				this.m_DataSourcePath = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00026197 File Offset: 0x00024397
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0002619F File Offset: 0x0002439F
		public bool InternalUsePermissionForExecution
		{
			get
			{
				return this.m_internalUsePermissionForExecution;
			}
			set
			{
				this.m_internalUsePermissionForExecution = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000261A8 File Offset: 0x000243A8
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x000261B0 File Offset: 0x000243B0
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

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000261B9 File Offset: 0x000243B9
		internal override string InputTrace
		{
			get
			{
				return this.DataSourcePath;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000261C1 File Offset: 0x000243C1
		internal override void Validate()
		{
			if (this.DataSourcePath == null)
			{
				throw new MissingParameterException("DataSource");
			}
		}

		// Token: 0x0400046D RID: 1133
		private string m_DataSourcePath;

		// Token: 0x0400046E RID: 1134
		private bool m_internalUsePermissionForExecution;

		// Token: 0x0400046F RID: 1135
		private DataSourceDefinition m_dataSourceDefinition;
	}
}
