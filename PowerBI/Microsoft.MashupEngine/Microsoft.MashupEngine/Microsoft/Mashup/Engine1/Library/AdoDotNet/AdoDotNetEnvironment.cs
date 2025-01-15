using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F41 RID: 3905
	internal class AdoDotNetEnvironment : GenericDbEnvironment
	{
		// Token: 0x0600673B RID: 26427 RVA: 0x001634CB File Offset: 0x001616CB
		public AdoDotNetEnvironment(IEngineHost host, IResource resource, string providerName, Value connectionProperties, Value options)
			: base(host, AdoDotNetEnvironment.ConnectionString, "ADO.NET", resource, connectionProperties, options)
		{
			this.providerName = providerName;
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x0600673C RID: 26428 RVA: 0x001634EA File Offset: 0x001616EA
		protected override string ProviderName
		{
			get
			{
				return this.providerName;
			}
		}

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x0600673D RID: 26429 RVA: 0x001634F2 File Offset: 0x001616F2
		public override bool SuppressNativeQueryPermissionChallenge
		{
			get
			{
				return base.Host.QueryService<IExtensibilityService>() != null;
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x00163504 File Offset: 0x00161704
		public override DbCommandTypeMap ParameterTypeMap
		{
			get
			{
				object obj;
				if (base.UserOptions.TryGetValue("TypeMap", out obj))
				{
					return (DbCommandTypeMap)obj;
				}
				return base.ParameterTypeMap;
			}
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x00163532 File Offset: 0x00161732
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return AdoDotNetModule.OptionRecord.Concatenate(GenericDbEnvironment.NativeQueryOptionRecord);
			}
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x00163543 File Offset: 0x00161743
		public override bool TryGetProviderMissingException(ArgumentException e, out Exception providerMissingException)
		{
			providerMissingException = DataSourceException.NewMissingClientLibraryError<Message2>(base.Host, DataSourceException.DataSourceMessage(base.DataSourceNameString, e.Message), this.Resource, null, null, e);
			return true;
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x0016356D File Offset: 0x0016176D
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			return base.SelectSchema(new AdoDotNetSchemasTableValue(this), schema);
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x0016357C File Offset: 0x0016177C
		public override IList<RecordKeyDefinition> GetDbExceptionDetails(DbException dbException)
		{
			List<RecordKeyDefinition> details = DbExceptionInfo.GetDetails(dbException);
			((ICollection<RecordKeyDefinition>)details).Add(new RecordKeyDefinition("ExceptionType", TextValue.New(dbException.GetType().FullName), TypeValue.Text));
			return details;
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x001635AC File Offset: 0x001617AC
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			DbProviderFactory dbProviderFactory;
			if (this.TryCreateFactory(out dbProviderFactory))
			{
				return dbProviderFactory;
			}
			return base.CreateDbProviderFactory();
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x001635CC File Offset: 0x001617CC
		private bool TryCreateFactory(out DbProviderFactory factory)
		{
			factory = null;
			if (DbEnvironment.privateProviderManager.Value.TryCreateFactory(base.Host, this.providerName, out factory, true))
			{
				return true;
			}
			IApplicationConfigurationService applicationConfigurationService = base.Host.QueryService<IApplicationConfigurationService>();
			if (applicationConfigurationService == null)
			{
				return false;
			}
			DataRow dataRow = applicationConfigurationService.GetDbProviderFactoryClasses().Rows.Find(this.ProviderName);
			if (dataRow == null)
			{
				return false;
			}
			try
			{
				factory = DbProviderFactories.GetFactory(dataRow);
				return true;
			}
			catch (InvalidOperationException ex)
			{
			}
			catch (ConfigurationException ex)
			{
			}
			Exception ex;
			throw DataSourceException.NewMissingClientLibraryError<Message2>(base.Host, DataSourceException.DataSourceMessage(base.DataSourceNameString, ex.Message), this.Resource, null, null, ex);
		}

		// Token: 0x040038C1 RID: 14529
		public static readonly ConnectionStringHandler ConnectionString = new AdoDotNetConnectionStringHandler();

		// Token: 0x040038C2 RID: 14530
		private readonly string providerName;
	}
}
