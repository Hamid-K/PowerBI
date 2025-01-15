using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000229 RID: 553
	internal abstract class TestConnectAction<P> : RSSoapAction<P> where P : TestConnectActionParameters, new()
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0004B091 File Offset: 0x00049291
		public TestConnectAction(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004B09C File Offset: 0x0004929C
		internal override void PerformActionNow()
		{
			this.InitDataSourceInfo();
			base.ActionParameters.ConnectionError = null;
			if (base.ActionParameters.DSInfo.OriginalConnectStringExpressionBased)
			{
				throw new InvalidParameterException("DataSource");
			}
			try
			{
				try
				{
					base.ActionParameters.DSInfo.ThrowIfNotUsable(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
				}
				catch (RSException ex)
				{
					throw new ReportProcessingException(ErrorCode.rsErrorOpeningConnection, ex, new object[] { base.ActionParameters.DSInfo.Name });
				}
				if (base.ActionParameters.DSInfo.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt)
				{
					if (base.ActionParameters.UserName == null || base.ActionParameters.Password == null)
					{
						throw new ReportProcessingException(ErrorCode.rsCredentialsNotSpecified);
					}
					base.ActionParameters.DSInfo.SetUserName(base.ActionParameters.UserName, DataProtection.Instance);
					base.ActionParameters.DSInfo.SetPassword(base.ActionParameters.Password, DataProtection.Instance);
				}
				string text = ((!string.IsNullOrEmpty(base.ActionParameters.DSInfo.DataSourceReference)) ? base.ActionParameters.DSInfo.DataSourceReference : "/");
				CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, new CatalogItemPath(text), "DataSourceInfo");
				using (ServerDataExtensionConnectionWrapper serverDataExtensionConnectionWrapper = base.Service.CreateAndOpenDataExtensionConnectionWrapper(catalogItemContext, base.ActionParameters.DSInfo, null))
				{
					IDbConnection connection = serverDataExtensionConnectionWrapper.Connection;
					if (connection is IDbConnectionTest)
					{
						((IDbConnectionTest)connection).TestConnection();
					}
					base.ActionParameters.ConnectionSuccess = true;
				}
			}
			catch (RSException ex2)
			{
				if (!(ex2 is ReportProcessingException) && !(ex2 is DataExtensionNotFoundException) && !(ex2 is ServerConfigurationErrorException))
				{
					throw;
				}
				Exception ex3;
				if (ex2 is ReportProcessingException && ex2.InnerException != null)
				{
					ex3 = ex2.InnerException;
				}
				else
				{
					ex3 = ex2;
				}
				base.ActionParameters.ConnectionSuccess = false;
				if (Global.EnableTestConnectionDetailedErrors)
				{
					base.ActionParameters.ConnectionError = ex3.Message;
				}
				else
				{
					base.ActionParameters.ConnectionError = ErrorStrings.DataSourceConnectionErrorNotVisible;
				}
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void InitDataSourceInfo()
		{
		}
	}
}
