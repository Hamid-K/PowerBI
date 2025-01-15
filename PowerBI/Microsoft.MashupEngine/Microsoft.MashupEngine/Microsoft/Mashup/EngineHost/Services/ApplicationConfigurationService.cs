using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200198B RID: 6539
	internal class ApplicationConfigurationService : IApplicationConfigurationService
	{
		// Token: 0x0600A5E3 RID: 42467 RVA: 0x00224DF8 File Offset: 0x00222FF8
		public DataTable GetDbProviderFactoryClasses()
		{
			DataTable dataTable;
			try
			{
				dataTable = DbProviderFactories.GetFactoryClasses();
			}
			catch (ConfigurationErrorsException)
			{
				dataTable = null;
			}
			return dataTable;
		}
	}
}
