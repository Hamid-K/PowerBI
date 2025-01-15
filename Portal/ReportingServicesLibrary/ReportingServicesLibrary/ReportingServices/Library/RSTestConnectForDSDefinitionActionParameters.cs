using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F1 RID: 241
	internal sealed class RSTestConnectForDSDefinitionActionParameters : TestConnectActionParameters
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00026F00 File Offset: 0x00025100
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00026F08 File Offset: 0x00025108
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

		// Token: 0x06000A16 RID: 2582 RVA: 0x00026F11 File Offset: 0x00025111
		internal override void Validate()
		{
			if (this.DataSourceDefinition == null)
			{
				throw new MissingParameterException("DataSourceDefinition");
			}
		}

		// Token: 0x0400047C RID: 1148
		private DataSourceDefinition m_dataSourceDefinition;
	}
}
