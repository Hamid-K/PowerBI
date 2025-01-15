using System;
using System.Configuration;
using System.IO;
using System.Xml;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000548 RID: 1352
	public class DrdaServiceConfigfurationUtilities
	{
		// Token: 0x06002DA5 RID: 11685 RVA: 0x00098AB0 File Offset: 0x00096CB0
		public void WriteConfigurationToXmlFile(ref DrdaServiceConfigurationSectionHandler drdaServiceConfig, string fileName, DrdaServiceGeneratedSchemaType schemaType)
		{
			try
			{
				string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><configSections><section name=\"hostIntegration.drdaAr.staticSql\" type=\"Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As.DrdaServiceConfigurationSectionHandler, Microsoft.HostIntegration.ConfigurationSectionHandlers, Version=10.0.1000.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" /></configSections><hostIntegration.drdaAs.drdaService xmlns=\"http://schemas.microsoft.com/his/DrdaAs/DrdaService/2013\">";
				text += "<services>";
				foreach (object obj in drdaServiceConfig.Services)
				{
					Service service = (Service)obj;
					text = text + "<service name=\"" + service.Name + "\" >";
					text = string.Concat(new string[]
					{
						text,
						"<connectionManager type=\"",
						service.ConnectionManager.Type,
						"\" port=\"",
						service.ConnectionManager.Port.ToString(),
						"\" "
					});
					PropertyInformation propertyInformation = service.ConnectionManager.ElementInformation.Properties["useSsl"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "useSsl=\"" + service.ConnectionManager.UseSsl.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["sslCertificatePath"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "sslCertificatePath=\"" + service.ConnectionManager.SslCertificatePath + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["sslCertificateSerialNumber"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "sslCertificateSerialNumber=\"" + service.ConnectionManager.SslCertificateSerialNumber + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["sslClientCertificateRequired"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "sslClientCertificateRequired=\"" + service.ConnectionManager.SslClientCertificateRequired.ToString() + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["certificateDnsName"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "certificateDnsName=\"" + service.ConnectionManager.CertificateDnsName + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["certificateFriendlyName"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "certificateFriendlyName=\"" + service.ConnectionManager.CertificateFriendlyName + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["partnerServers"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "partnerServers=\"" + service.ConnectionManager.PartnerServers + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["isPrimary"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "isPrimary=\"" + service.ConnectionManager.IsPrimary.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["pingInterval"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
					{
						text = text + "pingInterval=\"" + service.ConnectionManager.PingInterval.ToString() + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["performanceCountersOn"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "performanceCountersOn=\"" + service.ConnectionManager.PerformanceCountersOn.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.ConnectionManager.ElementInformation.Properties["clientIpAddressesAllowed"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "clientIpAddressesAllowed=\"" + service.ConnectionManager.ClientIpAddressesAllowed + "\" ";
					}
					text += "/>";
					text = text + "<securityManager type=\"" + service.SecurityManager.Type + "\" ";
					propertyInformation = service.SecurityManager.ElementInformation.Properties["mappedAuthenticationDomain"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "mappedAuthenticationDomain=\"" + service.SecurityManager.MappedAuthenticationDomain + "\" ";
					}
					propertyInformation = service.SecurityManager.ElementInformation.Properties["securityTokenTimeout"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "securityTokenTimeout=\"" + service.SecurityManager.SecurityTokenTimeoutString + "\" ";
					}
					propertyInformation = service.SecurityManager.ElementInformation.Properties["authenticationLookupTimeout"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "authenticationLookupTimeout=\"" + service.SecurityManager.AuthenticationLookupTimeoutString + "\" ";
					}
					propertyInformation = service.SecurityManager.ElementInformation.Properties["authenticationLookupRetries"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
					{
						text = text + "authenticationLookupRetries=\"" + service.SecurityManager.AuthenticationLookupRetries.ToString() + "\" ";
					}
					text += "/>";
					text = text + "<sqlApplicationManager type=\"" + service.SqlApplicationManager.Type + "\" ";
					propertyInformation = service.SqlApplicationManager.ElementInformation.Properties["rollbackTransactionOnError"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "rollbackTransactionOnError=\"" + service.SqlApplicationManager.RollbackTransactionOnError.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.SqlApplicationManager.ElementInformation.Properties["disableXaTransaction"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "disableXaTransaction=\"" + service.SqlApplicationManager.DisableXaTransaction.ToString().ToLowerInvariant() + "\" ";
					}
					text += "/>";
					text = text + "<resynchronizationManager type=\"" + service.ResynchronizationManager.Type + "\" ";
					propertyInformation = service.ResynchronizationManager.ElementInformation.Properties["transactionExpiryDuration"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "transactionExpiryDuration=\"" + service.ResynchronizationManager.TransactionExpiryDurationString + "\" ";
					}
					propertyInformation = service.ResynchronizationManager.ElementInformation.Properties["resyncRetryDurationInMinutes"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "resyncRetryDurationInMinutes=\"" + service.ResynchronizationManager.ResyncRetryDurationInMinutes.ToString() + "\" ";
					}
					propertyInformation = service.ResynchronizationManager.ElementInformation.Properties["resyncIntervalInMinutes"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "resyncIntervalInMinutes=\"" + service.ResynchronizationManager.ResyncIntervalInMinutes.ToString() + "\" ";
					}
					propertyInformation = service.ResynchronizationManager.ElementInformation.Properties["transactionLogLocation"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "transactionLogLocation=\"" + service.ResynchronizationManager.TransactionLogLocation + "\" ";
					}
					propertyInformation = service.ResynchronizationManager.ElementInformation.Properties["xaIsolationLevel"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "xaIsolationLevel=\"" + service.ResynchronizationManager.XaIsolationLevel + "\" ";
					}
					text += "/>";
					text = string.Concat(new string[]
					{
						text,
						"<database type=\"",
						service.Database.Type,
						"\" connectionString=\"",
						service.Database.ConnectionString,
						"\" "
					});
					propertyInformation = service.Database.ElementInformation.Properties["commandParameterNameCase"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "commandParameterNameCase=\"" + service.Database.CommandParameterNameCase + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["hostInitiatedAffiliateApplication"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "hostInitiatedAffiliateApplication=\"" + service.Database.HostInitiatedAffiliateApplication + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["windowsInitiatedAffiliateApplication"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "windowsInitiatedAffiliateApplication=\"" + service.Database.WindowsInitiatedAffiliateApplication + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["storedProcedureNameSeparator"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "storedProcedureNameSeparator=\"" + service.Database.StoredProcedureNameSeparator + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["createPackageXml"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "createPackageXml=\"" + service.Database.CreatePackageXml.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["createPackageProcedure"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "createPackageProcedure=\"" + service.Database.CreatePackageProcedure.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["createPackageProcedureWithCustomSqlScripts"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "createPackageProcedureWithCustomSqlScripts=\"" + service.Database.CreatePackageProcedureWithCustomSqlScripts.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["packageXmlLocation"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "packageXmlLocation=\"" + service.Database.PackageXmlLocation + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["packageProcedureSchemaList"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "packageProcedureSchemaList=\"" + service.Database.PackageProcedureSchemaList + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["packageXmlFormat"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (PackageXmlFormat)propertyInformation.Value != (PackageXmlFormat)propertyInformation.DefaultValue))
					{
						text = text + "packageXmlFormat=\"" + service.Database.PackageXmlFormat.ToString() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["sqlTransforms"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (SqlTransforms)propertyInformation.Value != (SqlTransforms)propertyInformation.DefaultValue))
					{
						text = text + "sqlTransforms=\"" + service.Database.SqlTransforms.ToString() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["storedProcedureCallTimeout"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
					{
						text = text + "storedProcedureCallTimeout=\"" + service.Database.StoredProcedureCallTimeout.ToString() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["createPackageProcedureWithExtendedProperties"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
					{
						text = text + "createPackageProcedureWithExtendedProperties=\"" + service.Database.CreatePackageProcedureWithExtendedProperties.ToString().ToLowerInvariant() + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["packageProcedureCacheFlush"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "packageProcedureCacheFlush=\"" + service.Database.PackageProcedureCacheFlushString + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["packageProcedureLastInvoke"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "packageProcedureLastInvoke=\"" + service.Database.PackageProcedureLastInvokeString + "\" ";
					}
					propertyInformation = service.Database.ElementInformation.Properties["defaultCollationName"];
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
					{
						text = text + "defaultCollationName=\"" + service.Database.DefaultCollationName + "\" ";
					}
					text += "/>";
					if (service.ApplicationEncodings.Count > 0)
					{
						text += "<applicationEncodings>";
						foreach (object obj2 in service.ApplicationEncodings)
						{
							ApplicationEncoding applicationEncoding = (ApplicationEncoding)obj2;
							text = string.Concat(new string[]
							{
								text,
								"<applicationEncoding scheme=\"",
								applicationEncoding.Scheme.ToString(),
								"\" ccsid=\"",
								((int)applicationEncoding.Ccsid).ToString(),
								"\" database=\"",
								applicationEncoding.Database,
								"\" "
							});
							propertyInformation = applicationEncoding.ElementInformation.Properties["customCcsid"];
							if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
							{
								text = text + "customCcsid=\"" + applicationEncoding.CustomCcsid.ToString() + "\" ";
							}
							text += "/>";
						}
						text += "</applicationEncodings>";
					}
					if (service.CollationNames.Count > 0)
					{
						text += "<collationNames>";
						foreach (object obj3 in service.CollationNames)
						{
							CollationName collationName = (CollationName)obj3;
							text += "<collationName ";
							text = text + "from=\"" + collationName.From + "\" ";
							text = text + "to=\"" + collationName.To + "\" />";
						}
						text += "</collationNames>";
					}
					if (service.ConversionFormats.DateTimeMasks.Count > 0 || service.ConversionFormats.TimeMasks.Count > 0 || service.ConversionFormats.DateMasks.Count > 0)
					{
						text += "<conversionFormats>";
						if (service.ConversionFormats.DateTimeMasks.Count > 0)
						{
							text += "<dateTimeMasks>";
							foreach (object obj4 in service.ConversionFormats.DateTimeMasks)
							{
								DateTimeMask dateTimeMask = (DateTimeMask)obj4;
								text += "<dateTimeMask>";
								if (dateTimeMask.DateAndTimeUsage == DateAndTimeUsage.Db2ToSql)
								{
									text = text + "<db2ToSql sourceFormat=\"" + dateTimeMask.Db2ToSql.SourceFormat.ToString() + "\" />";
								}
								if (dateTimeMask.DateAndTimeUsage == DateAndTimeUsage.SqlToDb2)
								{
									text = text + "<sqlToDb2 targetFormat=\"" + dateTimeMask.SqlToDb2.TargetFormat.ToString() + "\" />";
								}
								text += "</dateTimeMask>";
							}
							text += "</dateTimeMasks>";
						}
						if (service.ConversionFormats.TimeMasks.Count > 0)
						{
							text += "<timeMasks>";
							foreach (object obj5 in service.ConversionFormats.TimeMasks)
							{
								TimeMask timeMask = (TimeMask)obj5;
								text += "<timeMask>";
								if (timeMask.DateAndTimeUsage == DateAndTimeUsage.Db2ToSql)
								{
									text = text + "<db2ToSql sourceFormat=\"" + timeMask.Db2ToSql.SourceFormat.ToString() + "\" />";
								}
								if (timeMask.DateAndTimeUsage == DateAndTimeUsage.SqlToDb2)
								{
									text = text + "<sqlToDb2 targetFormat=\"" + timeMask.SqlToDb2.TargetFormat.ToString() + "\" />";
								}
								text += "</timeMask>";
							}
							text += "</timeMasks>";
						}
						if (service.ConversionFormats.DateMasks.Count > 0)
						{
							text += "<dateMasks>";
							foreach (object obj6 in service.ConversionFormats.DateMasks)
							{
								DateMask dateMask = (DateMask)obj6;
								text += "<dateMask>";
								if (dateMask.DateAndTimeUsage == DateAndTimeUsage.Db2ToSql)
								{
									text = text + "<db2ToSql sourceFormat=\"" + dateMask.Db2ToSql.SourceFormat.ToString() + "\" />";
								}
								if (dateMask.DateAndTimeUsage == DateAndTimeUsage.SqlToDb2)
								{
									text = text + "<sqlToDb2 targetFormat=\"" + dateMask.SqlToDb2.TargetFormat.ToString() + "\" />";
								}
								text += "</dateMask>";
							}
							text += "</dateMasks>";
						}
						text += "</conversionFormats>";
					}
					if (service.DatabaseAliases.Count > 0)
					{
						text += "<databaseAliases>";
						foreach (object obj7 in service.DatabaseAliases)
						{
							DatabaseAlias databaseAlias = (DatabaseAlias)obj7;
							text = string.Concat(new string[] { text, "<databaseAlias sourceLocation=\"", databaseAlias.SourceLocation, "\" sourceCollection=\"", databaseAlias.SourceCollection, "\" targetDatabase=\"", databaseAlias.TargetDatabase, "\" targetSchema=\"", databaseAlias.TargetSchema, "\" />" });
						}
						text += "</databaseAliases>";
					}
					if (service.DrdaServiceTraceListeners.Count > 0)
					{
						text += "<drdaServiceTraceListeners>";
						foreach (object obj8 in service.DrdaServiceTraceListeners)
						{
							DrdaServiceTraceListener drdaServiceTraceListener = (DrdaServiceTraceListener)obj8;
							text = text + "<drdaServiceTraceListener type=\"" + drdaServiceTraceListener.Type + "\" ";
							propertyInformation = drdaServiceTraceListener.ElementInformation.Properties["traceLevel"];
							if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
							{
								text = text + "traceLevel=\"" + drdaServiceTraceListener.TraceLevel.ToString() + "\" ";
							}
							text += "/>";
						}
						text += "</drdaServiceTraceListeners>";
					}
					if (service.PackageBindListeners.Count > 0)
					{
						text += "<packageBindListeners>";
						foreach (object obj9 in service.PackageBindListeners)
						{
							PackageBindListener packageBindListener = (PackageBindListener)obj9;
							text = text + "<packageBindListener type=\"" + packageBindListener.Type + "\" ";
							propertyInformation = packageBindListener.ElementInformation.Properties["errorWhenNoCallback"];
							if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
							{
								text = text + "errorWhenNoCallback=\"" + packageBindListener.ErrorWhenNoCallback.ToString().ToLowerInvariant() + "\" ";
							}
							text += "/>";
						}
						text += "</packageBindListeners>";
					}
					bool flag = false;
					if (schemaType == DrdaServiceGeneratedSchemaType.Minimal)
					{
						foreach (object obj10 in service.ConversionBehavior.ElementInformation.Properties)
						{
							PropertyInformation propertyInformation2 = (PropertyInformation)obj10;
							if ((bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue)
							{
								flag = true;
								break;
							}
						}
					}
					if (schemaType == DrdaServiceGeneratedSchemaType.Full || flag)
					{
						text += "<conversionBehavior ";
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["acceptAllInvalidNumerics"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "acceptAllInvalidNumerics=\"" + service.ConversionBehavior.AcceptAllInvalidNumerics.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["acceptBadCOMP3Sign"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "acceptBadCOMP3Sign=\"" + service.ConversionBehavior.AcceptBadCOMP3Sign.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["acceptNullPacked"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "acceptNullPacked=\"" + service.ConversionBehavior.AcceptNullPacked.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["acceptNullZoned"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "acceptNullZoned=\"" + service.ConversionBehavior.AcceptNullZoned.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["alwaysCheckForNull"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "alwaysCheckForNull=\"" + service.ConversionBehavior.AlwaysCheckForNull.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["stringsAreNullTerminatedAndSpacePadded"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "stringsAreNullTerminatedAndSpacePadded=\"" + service.ConversionBehavior.StringsAreNullTerminatedAndSpacePadded.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["trimTrailingNulls"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "trimTrailingNulls=\"" + service.ConversionBehavior.TrimTrailingNulls.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["convertReceivedStringsAsIs"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "convertReceivedStringsAsIs=\"" + service.ConversionBehavior.ConvertReceivedStringsAsIs.ToString().ToLowerInvariant() + "\" ";
						}
						propertyInformation = service.ConversionBehavior.ElementInformation.Properties["allowNullRedefines"];
						if (schemaType == DrdaServiceGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == DrdaServiceGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
						{
							text = text + "allowNullRedefines=\"" + service.ConversionBehavior.AllowNullRedefines.ToString().ToLowerInvariant() + "\" ";
						}
						text += "/>";
					}
					text += "</service></services></hostIntegration.drdaAs.drdaService></configuration>";
				}
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
				xmlReaderSettings.XmlResolver = null;
				XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.Load(xmlReader);
				xmlDocument.Save(fileName);
				new ValidateConfigurationFile().ValidateConfigFile(fileName, "HostIntegrationDrdaServiceConfiguration.xsd");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
