using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.SqlServer.ReportingServices2010;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000060 RID: 96
	internal static class DataSourceExtensions
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00012E28 File Offset: 0x00011028
		internal static Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition ToDataSourceDefinition(this global::Model.DataSource dataSource)
		{
			if (dataSource == null)
			{
				return null;
			}
			Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition dataSourceDefinition = new Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition
			{
				Enabled = dataSource.IsEnabled,
				EnabledSpecified = true,
				ConnectString = dataSource.ConnectionString,
				UseOriginalConnectString = !dataSource.IsConnectionStringOverridden,
				OriginalConnectStringExpressionBased = dataSource.IsOriginalConnectionStringExpressionBased,
				Extension = dataSource.DataSourceType
			};
			dataSourceDefinition.CredentialRetrieval = (Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum)dataSource.CredentialRetrieval;
			switch (dataSourceDefinition.CredentialRetrieval)
			{
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Prompt:
				if (dataSource.CredentialsByUser != null)
				{
					dataSourceDefinition.Prompt = dataSource.CredentialsByUser.DisplayText;
					dataSourceDefinition.WindowsCredentials = dataSource.CredentialsByUser.UseAsWindowsCredentials;
				}
				break;
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Store:
				if (dataSource.CredentialsInServer != null)
				{
					dataSourceDefinition.UserName = dataSource.CredentialsInServer.UserName;
					dataSourceDefinition.Password = dataSource.CredentialsInServer.Password;
					dataSourceDefinition.WindowsCredentials = dataSource.CredentialsInServer.UseAsWindowsCredentials;
					dataSourceDefinition.ImpersonateUser = dataSource.CredentialsInServer.ImpersonateAuthenticatedUser;
					dataSourceDefinition.ImpersonateUserSpecified = dataSource.CredentialsInServer.ImpersonateAuthenticatedUser;
				}
				break;
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Integrated:
				dataSourceDefinition.WindowsCredentials = true;
				break;
			}
			return dataSourceDefinition;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00012F48 File Offset: 0x00011148
		private static Guid GenerateGuidBasedOnKeyString(string keyString)
		{
			Guid guid = default(Guid);
			using (HMAC hmac = HMAC.Create())
			{
				byte[] array = hmac.ComputeHash(Encoding.UTF8.GetBytes(keyString));
				if (hmac.HashSize >= 128)
				{
					int num = (int)array[0];
					num <<= 1;
					num += (int)array[1];
					num <<= 1;
					num += (int)array[2];
					num <<= 1;
					num += (int)array[3];
					short num2 = (short)array[4];
					num2 = (short)(num2 << 1);
					num2 += (short)array[5];
					short num3 = (short)array[6];
					num3 = (short)(num3 << 1);
					num3 += (short)array[7];
					guid = new Guid(num, num2, num3, array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15]);
				}
			}
			return guid;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00013018 File Offset: 0x00011218
		internal static global::Model.DataSource ToDataSource(this Microsoft.ReportingServices.Library.Soap2005.DataSource soapDs)
		{
			if (soapDs == null || soapDs.Item == null)
			{
				return null;
			}
			if (soapDs.Item is Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition)
			{
				global::Model.DataSource dataSource = ((Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition)soapDs.Item).ToDataSource();
				dataSource.Name = soapDs.Name;
				dataSource.Id = DataSourceExtensions.GenerateGuidBasedOnKeyString(soapDs.Name);
				return dataSource;
			}
			if (soapDs.Item is Microsoft.ReportingServices.Library.Soap2005.DataSourceReference)
			{
				Microsoft.ReportingServices.Library.Soap2005.DataSourceReference dataSourceReference = (Microsoft.ReportingServices.Library.Soap2005.DataSourceReference)soapDs.Item;
				return new global::Model.DataSource
				{
					Name = soapDs.Name,
					Path = dataSourceReference.Reference,
					IsReference = true,
					Id = DataSourceExtensions.GenerateGuidBasedOnKeyString(soapDs.Name)
				};
			}
			if (soapDs.Item is Microsoft.ReportingServices.Library.Soap2005.InvalidDataSourceReference)
			{
				return new global::Model.DataSource
				{
					Name = soapDs.Name,
					IsReference = true
				};
			}
			throw new InvalidOperationException(SR.Error_InvalidDataSourceType);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000130EC File Offset: 0x000112EC
		internal static global::Model.DataSource ToDataSource(this Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition dsd)
		{
			if (dsd == null)
			{
				return null;
			}
			global::Model.DataSource dataSource = new global::Model.DataSource();
			dataSource.IsEnabled = dsd.Enabled;
			dataSource.DataSourceType = dsd.Extension;
			dataSource.ConnectionString = dsd.ConnectString;
			dataSource.IsOriginalConnectionStringExpressionBased = dsd.OriginalConnectStringExpressionBased;
			dataSource.IsConnectionStringOverridden = !dsd.UseOriginalConnectString;
			dataSource.CredentialRetrieval = (CredentialRetrievalType)dsd.CredentialRetrieval;
			Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum credentialRetrieval = dsd.CredentialRetrieval;
			if (credentialRetrieval != Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Prompt)
			{
				if (credentialRetrieval == Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Store)
				{
					dataSource.CredentialsInServer = new CredentialsStoredInServer
					{
						UserName = dsd.UserName,
						Password = dsd.Password,
						UseAsWindowsCredentials = dsd.WindowsCredentials,
						ImpersonateAuthenticatedUser = dsd.ImpersonateUser
					};
				}
			}
			else
			{
				dataSource.CredentialsByUser = new CredentialsSuppliedByUser
				{
					DisplayText = dsd.Prompt,
					UseAsWindowsCredentials = dsd.WindowsCredentials
				};
			}
			return dataSource;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000131C0 File Offset: 0x000113C0
		internal static global::Model.DataSource ToDataSource(this Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition dsd)
		{
			if (dsd == null)
			{
				return null;
			}
			global::Model.DataSource dataSource = new global::Model.DataSource();
			dataSource.IsEnabled = dsd.Enabled;
			dataSource.DataSourceType = dsd.Extension;
			dataSource.ConnectionString = dsd.ConnectString;
			dataSource.IsOriginalConnectionStringExpressionBased = dsd.OriginalConnectStringExpressionBased;
			dataSource.IsConnectionStringOverridden = !dsd.UseOriginalConnectString;
			dataSource.CredentialRetrieval = (CredentialRetrievalType)dsd.CredentialRetrieval;
			Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum credentialRetrieval = dsd.CredentialRetrieval;
			if (credentialRetrieval != Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Prompt)
			{
				if (credentialRetrieval == Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Store)
				{
					dataSource.CredentialsInServer = new CredentialsStoredInServer
					{
						UserName = dsd.UserName,
						Password = dsd.Password,
						UseAsWindowsCredentials = dsd.WindowsCredentials,
						ImpersonateAuthenticatedUser = dsd.ImpersonateUser
					};
				}
			}
			else
			{
				dataSource.CredentialsByUser = new CredentialsSuppliedByUser
				{
					DisplayText = dsd.Prompt,
					UseAsWindowsCredentials = dsd.WindowsCredentials
				};
			}
			return dataSource;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00013294 File Offset: 0x00011494
		internal static global::Model.DataSource ToDataSource(this Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition dsd, DataSourceRepository dsr)
		{
			if (dsd == null || dsr == null)
			{
				return null;
			}
			dsr.IsEnabled = dsd.Enabled;
			dsr.DataSourceType = dsd.Extension;
			dsr.ConnectionString = dsd.ConnectString;
			dsr.IsOriginalConnectionStringExpressionBased = dsd.OriginalConnectStringExpressionBased;
			dsr.IsConnectionStringOverridden = !dsd.UseOriginalConnectString;
			dsr.CredentialRetrieval = (CredentialRetrievalType)dsd.CredentialRetrieval;
			Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum credentialRetrieval = dsd.CredentialRetrieval;
			if (credentialRetrieval != Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Prompt)
			{
				if (credentialRetrieval == Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Store)
				{
					dsr.CredentialsInServer = new CredentialsStoredInServer
					{
						UserName = dsd.UserName,
						Password = dsd.Password,
						UseAsWindowsCredentials = dsd.WindowsCredentials,
						ImpersonateAuthenticatedUser = dsd.ImpersonateUser
					};
				}
			}
			else
			{
				dsr.CredentialsByUser = new CredentialsSuppliedByUser
				{
					DisplayText = dsd.Prompt,
					UseAsWindowsCredentials = dsd.WindowsCredentials
				};
			}
			return dsr;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00013368 File Offset: 0x00011568
		internal static global::Model.DataSource ToDataSource(this Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition dsd, DataSourceRepository dsr)
		{
			dsr.IsEnabled = dsd.Enabled;
			dsr.DataSourceType = dsd.Extension;
			dsr.ConnectionString = dsd.ConnectString;
			dsr.CredentialRetrieval = (CredentialRetrievalType)dsd.CredentialRetrieval;
			Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum credentialRetrieval = dsd.CredentialRetrieval;
			if (credentialRetrieval != Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Prompt)
			{
				if (credentialRetrieval == Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Store)
				{
					dsr.CredentialsInServer = new CredentialsStoredInServer
					{
						UserName = dsd.UserName,
						Password = dsd.Password,
						UseAsWindowsCredentials = dsd.WindowsCredentials,
						ImpersonateAuthenticatedUser = dsd.ImpersonateUser
					};
				}
			}
			else
			{
				dsr.CredentialsByUser = new CredentialsSuppliedByUser
				{
					DisplayText = dsd.Prompt,
					UseAsWindowsCredentials = dsd.WindowsCredentials
				};
			}
			return dsr;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00013418 File Offset: 0x00011618
		internal static global::Model.DataSource ToDataSource(this Microsoft.SqlServer.ReportingServices2010.DataSource dataSource)
		{
			if (dataSource == null || dataSource.Item == null)
			{
				return null;
			}
			if (dataSource.Item is Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition)
			{
				global::Model.DataSource dataSource2 = ((Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition)dataSource.Item).ToDataSource();
				dataSource2.Name = dataSource.Name;
				return dataSource2;
			}
			if (dataSource.Item is Microsoft.SqlServer.ReportingServices2010.DataSourceReference)
			{
				Microsoft.SqlServer.ReportingServices2010.DataSourceReference dataSourceReference = (Microsoft.SqlServer.ReportingServices2010.DataSourceReference)dataSource.Item;
				return new global::Model.DataSource
				{
					Name = dataSource.Name,
					Path = dataSourceReference.Reference,
					IsReference = true
				};
			}
			if (dataSource.Item is Microsoft.SqlServer.ReportingServices2010.InvalidDataSourceReference)
			{
				return new global::Model.DataSource
				{
					Name = dataSource.Name,
					IsReference = true
				};
			}
			throw new InvalidOperationException(SR.Error_InvalidDataSourceType);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000134CC File Offset: 0x000116CC
		internal static Microsoft.ReportingServices.Library.Soap2005.DataSource ToSoapDataSource(this global::Model.DataSource ds)
		{
			if (ds == null)
			{
				return null;
			}
			if (!ds.IsReference)
			{
				return new Microsoft.ReportingServices.Library.Soap2005.DataSource
				{
					Name = ds.Name,
					Item = ds.ToDataSourceDefinition()
				};
			}
			if (ds.Path != null)
			{
				return new Microsoft.ReportingServices.Library.Soap2005.DataSource
				{
					Name = ds.Name,
					Item = new Microsoft.ReportingServices.Library.Soap2005.DataSourceReference
					{
						Reference = ds.Path
					}
				};
			}
			return new Microsoft.ReportingServices.Library.Soap2005.DataSource
			{
				Name = ds.Name,
				Item = new Microsoft.ReportingServices.Library.Soap2005.InvalidDataSourceReference()
			};
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013554 File Offset: 0x00011754
		internal static global::Model.DataSource ToDataSource(this Microsoft.SqlServer.ReportingServices2010.DataSourceDefinitionOrReference dataSourceDefinitionOrReference)
		{
			if (dataSourceDefinitionOrReference == null)
			{
				throw new ArgumentNullException("dataSourceDefinitionOrReference");
			}
			global::Model.DataSource dataSource = new global::Model.DataSource();
			if (dataSourceDefinitionOrReference is Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition)
			{
				dataSource = ((Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition)dataSourceDefinitionOrReference).ToDataSource();
			}
			if (dataSourceDefinitionOrReference is Microsoft.SqlServer.ReportingServices2010.DataSourceReference)
			{
				Microsoft.SqlServer.ReportingServices2010.DataSourceReference dataSourceReference = (Microsoft.SqlServer.ReportingServices2010.DataSourceReference)dataSourceDefinitionOrReference;
				return new global::Model.DataSource
				{
					Name = dataSourceReference.Reference,
					Path = dataSourceReference.Reference,
					IsReference = true
				};
			}
			return dataSource;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000135C0 File Offset: 0x000117C0
		internal static Microsoft.SqlServer.ReportingServices2010.DataSource ToProxy2010DataSource(this global::Model.DataSource ds)
		{
			if (ds == null)
			{
				return null;
			}
			if (!ds.IsReference)
			{
				return new Microsoft.SqlServer.ReportingServices2010.DataSource
				{
					Name = ds.Name,
					Item = ds.ToDataSourceDefinition2010()
				};
			}
			if (ds.Path != null)
			{
				return new Microsoft.SqlServer.ReportingServices2010.DataSource
				{
					Name = ds.Name,
					Item = new Microsoft.SqlServer.ReportingServices2010.DataSourceReference
					{
						Reference = ds.Path
					}
				};
			}
			return new Microsoft.SqlServer.ReportingServices2010.DataSource
			{
				Name = ds.Name,
				Item = new Microsoft.SqlServer.ReportingServices2010.InvalidDataSourceReference()
			};
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00013648 File Offset: 0x00011848
		internal static Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition ToDataSourceDefinition2010(this global::Model.DataSource dataSource)
		{
			Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition dataSourceDefinition = dataSource.ToDataSourceDefinition();
			return new Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition
			{
				Extension = dataSourceDefinition.Extension,
				ConnectString = dataSourceDefinition.ConnectString,
				UseOriginalConnectString = dataSourceDefinition.UseOriginalConnectString,
				OriginalConnectStringExpressionBased = dataSourceDefinition.OriginalConnectStringExpressionBased,
				CredentialRetrieval = dataSourceDefinition.CredentialRetrieval.ConvertTo2010(),
				WindowsCredentials = dataSourceDefinition.WindowsCredentials,
				ImpersonateUser = dataSourceDefinition.ImpersonateUser,
				ImpersonateUserSpecified = dataSourceDefinition.ImpersonateUserSpecified,
				Prompt = dataSourceDefinition.Prompt,
				UserName = dataSourceDefinition.UserName,
				Password = dataSourceDefinition.Password,
				Enabled = dataSourceDefinition.Enabled,
				EnabledSpecified = true
			};
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000136FD File Offset: 0x000118FD
		internal static Microsoft.SqlServer.ReportingServices2010.DataSourceDefinitionOrReference ToDataSourceDefinitionOrReference(this global::Model.DataSource dataSource)
		{
			dataSource.ToDataSourceDefinition();
			if (dataSource.IsReference)
			{
				return new Microsoft.SqlServer.ReportingServices2010.DataSourceReference
				{
					Reference = dataSource.Name
				};
			}
			return dataSource.ToDataSourceDefinition2010();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00013726 File Offset: 0x00011926
		private static Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum ConvertTo2010(this Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum credentialsRetrival)
		{
			switch (credentialsRetrival)
			{
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Prompt:
				return Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Prompt;
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Store:
				return Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Store;
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.Integrated:
				return Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.Integrated;
			case Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum.None:
				return Microsoft.SqlServer.ReportingServices2010.CredentialRetrievalEnum.None;
			default:
				throw new InvalidCastException("Mismatch between 2010 and 2005 CredentialRetrivalEnums");
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00013754 File Offset: 0x00011954
		internal static global::Model.DataSource ToDataSource(this DataSourceEntity datasourceEntity)
		{
			global::Model.DataSource dataSource = new global::Model.DataSource
			{
				Name = datasourceEntity.Name,
				DataSourceType = datasourceEntity.Extension,
				Type = CatalogItemType.DataSource,
				Hidden = false,
				Size = 0L
			};
			dataSource.ConnectionString = DataProtection.Instance.UnprotectDataToString(datasourceEntity.ConnectionString, "ConnectionString");
			dataSource.IsEnabled = datasourceEntity.IsEnabled;
			dataSource.IsConnectionStringOverridden = dataSource.ConnectionString != null;
			string text = datasourceEntity.CredentialRetrieval.ToString();
			Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum credentialRetrievalEnum = (Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum)Enum.Parse(typeof(Microsoft.ReportingServices.Library.Soap.CredentialRetrievalEnum), text);
			dataSource.CredentialRetrieval = (CredentialRetrievalType)credentialRetrievalEnum;
			CredentialRetrievalType credentialRetrieval = dataSource.CredentialRetrieval;
			if (credentialRetrieval != CredentialRetrievalType.prompt)
			{
				if (credentialRetrieval == CredentialRetrievalType.store)
				{
					dataSource.CredentialsInServer = new CredentialsStoredInServer
					{
						UserName = DataProtection.Instance.UnprotectDataToString(datasourceEntity.UserName, "UserName"),
						Password = ((datasourceEntity.Password == null) ? null : Convert.ToBase64String(datasourceEntity.Password)),
						UseAsWindowsCredentials = datasourceEntity.UseAsWindowsCredentials,
						ImpersonateAuthenticatedUser = datasourceEntity.ImpersonateAuthenticatedUser
					};
				}
			}
			else
			{
				dataSource.CredentialsByUser = new CredentialsSuppliedByUser
				{
					DisplayText = datasourceEntity.Prompt,
					UseAsWindowsCredentials = datasourceEntity.UseAsWindowsCredentials
				};
			}
			dataSource.Id = DataSourceExtensions.GenerateGuidBasedOnKeyString(datasourceEntity.Name);
			return dataSource;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000138A4 File Offset: 0x00011AA4
		internal static void LoadFromContent(this global::Model.DataSource dataSource)
		{
			if (dataSource.Content != null && dataSource.Content.Length != 0)
			{
				XmlDocument xmlDocument = new XmlDocument();
				string text = Encoding.UTF8.GetString(dataSource.Content);
				text = text.Substring(text.IndexOf("<"));
				XmlUtil.SafeOpenXmlDocumentString(xmlDocument, text);
				string text2 = JsonConvert.SerializeXmlNode(xmlDocument.DocumentElement);
				object obj = JsonConvert.DeserializeObject(text2);
				if (DataSourceExtensions.<>o__15.<>p__7 == null)
				{
					DataSourceExtensions.<>o__15.<>p__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Func<CallSite, object, bool> target = DataSourceExtensions.<>o__15.<>p__7.Target;
				CallSite <>p__ = DataSourceExtensions.<>o__15.<>p__7;
				if (DataSourceExtensions.<>o__15.<>p__1 == null)
				{
					DataSourceExtensions.<>o__15.<>p__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Func<CallSite, object, object, object> target2 = DataSourceExtensions.<>o__15.<>p__1.Target;
				CallSite <>p__2 = DataSourceExtensions.<>o__15.<>p__1;
				if (DataSourceExtensions.<>o__15.<>p__0 == null)
				{
					DataSourceExtensions.<>o__15.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "RptDataSource", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				object obj2 = target2(<>p__2, DataSourceExtensions.<>o__15.<>p__0.Target(DataSourceExtensions.<>o__15.<>p__0, obj), null);
				if (DataSourceExtensions.<>o__15.<>p__6 == null)
				{
					DataSourceExtensions.<>o__15.<>p__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				object obj4;
				if (!DataSourceExtensions.<>o__15.<>p__6.Target(DataSourceExtensions.<>o__15.<>p__6, obj2))
				{
					if (DataSourceExtensions.<>o__15.<>p__5 == null)
					{
						DataSourceExtensions.<>o__15.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object, object> target3 = DataSourceExtensions.<>o__15.<>p__5.Target;
					CallSite <>p__3 = DataSourceExtensions.<>o__15.<>p__5;
					object obj3 = obj2;
					if (DataSourceExtensions.<>o__15.<>p__4 == null)
					{
						DataSourceExtensions.<>o__15.<>p__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target4 = DataSourceExtensions.<>o__15.<>p__4.Target;
					CallSite <>p__4 = DataSourceExtensions.<>o__15.<>p__4;
					if (DataSourceExtensions.<>o__15.<>p__3 == null)
					{
						DataSourceExtensions.<>o__15.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ConnectionProperties", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, object> target5 = DataSourceExtensions.<>o__15.<>p__3.Target;
					CallSite <>p__5 = DataSourceExtensions.<>o__15.<>p__3;
					if (DataSourceExtensions.<>o__15.<>p__2 == null)
					{
						DataSourceExtensions.<>o__15.<>p__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "RptDataSource", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					obj4 = target3(<>p__3, obj3, target4(<>p__4, target5(<>p__5, DataSourceExtensions.<>o__15.<>p__2.Target(DataSourceExtensions.<>o__15.<>p__2, obj)), null));
				}
				else
				{
					obj4 = obj2;
				}
				if (target(<>p__, obj4))
				{
					if (DataSourceExtensions.<>o__15.<>p__9 == null)
					{
						DataSourceExtensions.<>o__15.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ConnectionProperties", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, object> target6 = DataSourceExtensions.<>o__15.<>p__9.Target;
					CallSite <>p__6 = DataSourceExtensions.<>o__15.<>p__9;
					if (DataSourceExtensions.<>o__15.<>p__8 == null)
					{
						DataSourceExtensions.<>o__15.<>p__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "RptDataSource", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj5 = target6(<>p__6, DataSourceExtensions.<>o__15.<>p__8.Target(DataSourceExtensions.<>o__15.<>p__8, obj));
					if (DataSourceExtensions.<>o__15.<>p__15 == null)
					{
						DataSourceExtensions.<>o__15.<>p__15 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(DataSourceExtensions)));
					}
					Func<CallSite, object, string> target7 = DataSourceExtensions.<>o__15.<>p__15.Target;
					CallSite <>p__7 = DataSourceExtensions.<>o__15.<>p__15;
					if (DataSourceExtensions.<>o__15.<>p__12 == null)
					{
						DataSourceExtensions.<>o__15.<>p__12 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target8 = DataSourceExtensions.<>o__15.<>p__12.Target;
					CallSite <>p__8 = DataSourceExtensions.<>o__15.<>p__12;
					if (DataSourceExtensions.<>o__15.<>p__11 == null)
					{
						DataSourceExtensions.<>o__15.<>p__11 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target9 = DataSourceExtensions.<>o__15.<>p__11.Target;
					CallSite <>p__9 = DataSourceExtensions.<>o__15.<>p__11;
					if (DataSourceExtensions.<>o__15.<>p__10 == null)
					{
						DataSourceExtensions.<>o__15.<>p__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ConnectString", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj6;
					if (!target8(<>p__8, target9(<>p__9, DataSourceExtensions.<>o__15.<>p__10.Target(DataSourceExtensions.<>o__15.<>p__10, obj5), null)))
					{
						obj6 = null;
					}
					else
					{
						if (DataSourceExtensions.<>o__15.<>p__14 == null)
						{
							DataSourceExtensions.<>o__15.<>p__14 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, object> target10 = DataSourceExtensions.<>o__15.<>p__14.Target;
						CallSite <>p__10 = DataSourceExtensions.<>o__15.<>p__14;
						if (DataSourceExtensions.<>o__15.<>p__13 == null)
						{
							DataSourceExtensions.<>o__15.<>p__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ConnectString", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						obj6 = target10(<>p__10, DataSourceExtensions.<>o__15.<>p__13.Target(DataSourceExtensions.<>o__15.<>p__13, obj5));
					}
					dataSource.ConnectionString = target7(<>p__7, obj6);
					if (DataSourceExtensions.<>o__15.<>p__21 == null)
					{
						DataSourceExtensions.<>o__15.<>p__21 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(DataSourceExtensions)));
					}
					Func<CallSite, object, string> target11 = DataSourceExtensions.<>o__15.<>p__21.Target;
					CallSite <>p__11 = DataSourceExtensions.<>o__15.<>p__21;
					if (DataSourceExtensions.<>o__15.<>p__18 == null)
					{
						DataSourceExtensions.<>o__15.<>p__18 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target12 = DataSourceExtensions.<>o__15.<>p__18.Target;
					CallSite <>p__12 = DataSourceExtensions.<>o__15.<>p__18;
					if (DataSourceExtensions.<>o__15.<>p__17 == null)
					{
						DataSourceExtensions.<>o__15.<>p__17 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target13 = DataSourceExtensions.<>o__15.<>p__17.Target;
					CallSite <>p__13 = DataSourceExtensions.<>o__15.<>p__17;
					if (DataSourceExtensions.<>o__15.<>p__16 == null)
					{
						DataSourceExtensions.<>o__15.<>p__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Extension", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj7;
					if (!target12(<>p__12, target13(<>p__13, DataSourceExtensions.<>o__15.<>p__16.Target(DataSourceExtensions.<>o__15.<>p__16, obj5), null)))
					{
						obj7 = null;
					}
					else
					{
						if (DataSourceExtensions.<>o__15.<>p__20 == null)
						{
							DataSourceExtensions.<>o__15.<>p__20 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, object> target14 = DataSourceExtensions.<>o__15.<>p__20.Target;
						CallSite <>p__14 = DataSourceExtensions.<>o__15.<>p__20;
						if (DataSourceExtensions.<>o__15.<>p__19 == null)
						{
							DataSourceExtensions.<>o__15.<>p__19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Extension", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						obj7 = target14(<>p__14, DataSourceExtensions.<>o__15.<>p__19.Target(DataSourceExtensions.<>o__15.<>p__19, obj5));
					}
					dataSource.DataSourceType = target11(<>p__11, obj7);
					if (DataSourceExtensions.<>o__15.<>p__27 == null)
					{
						DataSourceExtensions.<>o__15.<>p__27 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(DataSourceExtensions)));
					}
					Func<CallSite, object, bool> target15 = DataSourceExtensions.<>o__15.<>p__27.Target;
					CallSite <>p__15 = DataSourceExtensions.<>o__15.<>p__27;
					if (DataSourceExtensions.<>o__15.<>p__24 == null)
					{
						DataSourceExtensions.<>o__15.<>p__24 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target16 = DataSourceExtensions.<>o__15.<>p__24.Target;
					CallSite <>p__16 = DataSourceExtensions.<>o__15.<>p__24;
					if (DataSourceExtensions.<>o__15.<>p__23 == null)
					{
						DataSourceExtensions.<>o__15.<>p__23 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target17 = DataSourceExtensions.<>o__15.<>p__23.Target;
					CallSite <>p__17 = DataSourceExtensions.<>o__15.<>p__23;
					if (DataSourceExtensions.<>o__15.<>p__22 == null)
					{
						DataSourceExtensions.<>o__15.<>p__22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Enabled", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj8;
					if (!target16(<>p__16, target17(<>p__17, DataSourceExtensions.<>o__15.<>p__22.Target(DataSourceExtensions.<>o__15.<>p__22, obj5), null)))
					{
						obj8 = true;
					}
					else
					{
						if (DataSourceExtensions.<>o__15.<>p__26 == null)
						{
							DataSourceExtensions.<>o__15.<>p__26 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, object> target18 = DataSourceExtensions.<>o__15.<>p__26.Target;
						CallSite <>p__18 = DataSourceExtensions.<>o__15.<>p__26;
						if (DataSourceExtensions.<>o__15.<>p__25 == null)
						{
							DataSourceExtensions.<>o__15.<>p__25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Enabled", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						obj8 = target18(<>p__18, DataSourceExtensions.<>o__15.<>p__25.Target(DataSourceExtensions.<>o__15.<>p__25, obj5));
					}
					dataSource.IsEnabled = target15(<>p__15, obj8);
					dataSource.IsConnectionStringOverridden = true;
					dataSource.CredentialRetrieval = CredentialRetrievalType.none;
					if (DataSourceExtensions.<>o__15.<>p__30 == null)
					{
						DataSourceExtensions.<>o__15.<>p__30 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target19 = DataSourceExtensions.<>o__15.<>p__30.Target;
					CallSite <>p__19 = DataSourceExtensions.<>o__15.<>p__30;
					if (DataSourceExtensions.<>o__15.<>p__29 == null)
					{
						DataSourceExtensions.<>o__15.<>p__29 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target20 = DataSourceExtensions.<>o__15.<>p__29.Target;
					CallSite <>p__20 = DataSourceExtensions.<>o__15.<>p__29;
					if (DataSourceExtensions.<>o__15.<>p__28 == null)
					{
						DataSourceExtensions.<>o__15.<>p__28 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Prompt", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					if (target19(<>p__19, target20(<>p__20, DataSourceExtensions.<>o__15.<>p__28.Target(DataSourceExtensions.<>o__15.<>p__28, obj5), null)))
					{
						dataSource.CredentialRetrieval = CredentialRetrievalType.prompt;
						CredentialsSuppliedByUser credentialsSuppliedByUser = new CredentialsSuppliedByUser();
						CredentialsSuppliedByUser credentialsSuppliedByUser2 = credentialsSuppliedByUser;
						if (DataSourceExtensions.<>o__15.<>p__36 == null)
						{
							DataSourceExtensions.<>o__15.<>p__36 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(DataSourceExtensions)));
						}
						Func<CallSite, object, string> target21 = DataSourceExtensions.<>o__15.<>p__36.Target;
						CallSite <>p__21 = DataSourceExtensions.<>o__15.<>p__36;
						if (DataSourceExtensions.<>o__15.<>p__33 == null)
						{
							DataSourceExtensions.<>o__15.<>p__33 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, bool> target22 = DataSourceExtensions.<>o__15.<>p__33.Target;
						CallSite <>p__22 = DataSourceExtensions.<>o__15.<>p__33;
						if (DataSourceExtensions.<>o__15.<>p__32 == null)
						{
							DataSourceExtensions.<>o__15.<>p__32 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						Func<CallSite, object, object, object> target23 = DataSourceExtensions.<>o__15.<>p__32.Target;
						CallSite <>p__23 = DataSourceExtensions.<>o__15.<>p__32;
						if (DataSourceExtensions.<>o__15.<>p__31 == null)
						{
							DataSourceExtensions.<>o__15.<>p__31 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Prompt", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						object obj9;
						if (!target22(<>p__22, target23(<>p__23, DataSourceExtensions.<>o__15.<>p__31.Target(DataSourceExtensions.<>o__15.<>p__31, obj5), null)))
						{
							obj9 = string.Empty;
						}
						else
						{
							if (DataSourceExtensions.<>o__15.<>p__35 == null)
							{
								DataSourceExtensions.<>o__15.<>p__35 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target24 = DataSourceExtensions.<>o__15.<>p__35.Target;
							CallSite <>p__24 = DataSourceExtensions.<>o__15.<>p__35;
							if (DataSourceExtensions.<>o__15.<>p__34 == null)
							{
								DataSourceExtensions.<>o__15.<>p__34 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Prompt", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							obj9 = target24(<>p__24, DataSourceExtensions.<>o__15.<>p__34.Target(DataSourceExtensions.<>o__15.<>p__34, obj5));
						}
						credentialsSuppliedByUser2.DisplayText = target21(<>p__21, obj9);
						CredentialsSuppliedByUser credentialsSuppliedByUser3 = credentialsSuppliedByUser;
						if (DataSourceExtensions.<>o__15.<>p__42 == null)
						{
							DataSourceExtensions.<>o__15.<>p__42 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(DataSourceExtensions)));
						}
						Func<CallSite, object, bool> target25 = DataSourceExtensions.<>o__15.<>p__42.Target;
						CallSite <>p__25 = DataSourceExtensions.<>o__15.<>p__42;
						if (DataSourceExtensions.<>o__15.<>p__39 == null)
						{
							DataSourceExtensions.<>o__15.<>p__39 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, bool> target26 = DataSourceExtensions.<>o__15.<>p__39.Target;
						CallSite <>p__26 = DataSourceExtensions.<>o__15.<>p__39;
						if (DataSourceExtensions.<>o__15.<>p__38 == null)
						{
							DataSourceExtensions.<>o__15.<>p__38 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						Func<CallSite, object, object, object> target27 = DataSourceExtensions.<>o__15.<>p__38.Target;
						CallSite <>p__27 = DataSourceExtensions.<>o__15.<>p__38;
						if (DataSourceExtensions.<>o__15.<>p__37 == null)
						{
							DataSourceExtensions.<>o__15.<>p__37 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowsCredentials", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						object obj10;
						if (!target26(<>p__26, target27(<>p__27, DataSourceExtensions.<>o__15.<>p__37.Target(DataSourceExtensions.<>o__15.<>p__37, obj5), null)))
						{
							obj10 = false;
						}
						else
						{
							if (DataSourceExtensions.<>o__15.<>p__41 == null)
							{
								DataSourceExtensions.<>o__15.<>p__41 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target28 = DataSourceExtensions.<>o__15.<>p__41.Target;
							CallSite <>p__28 = DataSourceExtensions.<>o__15.<>p__41;
							if (DataSourceExtensions.<>o__15.<>p__40 == null)
							{
								DataSourceExtensions.<>o__15.<>p__40 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowsCredentials", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							obj10 = target28(<>p__28, DataSourceExtensions.<>o__15.<>p__40.Target(DataSourceExtensions.<>o__15.<>p__40, obj5));
						}
						credentialsSuppliedByUser3.UseAsWindowsCredentials = target25(<>p__25, obj10);
						dataSource.CredentialsByUser = credentialsSuppliedByUser;
					}
					if (DataSourceExtensions.<>o__15.<>p__45 == null)
					{
						DataSourceExtensions.<>o__15.<>p__45 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target29 = DataSourceExtensions.<>o__15.<>p__45.Target;
					CallSite <>p__29 = DataSourceExtensions.<>o__15.<>p__45;
					if (DataSourceExtensions.<>o__15.<>p__44 == null)
					{
						DataSourceExtensions.<>o__15.<>p__44 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target30 = DataSourceExtensions.<>o__15.<>p__44.Target;
					CallSite <>p__30 = DataSourceExtensions.<>o__15.<>p__44;
					if (DataSourceExtensions.<>o__15.<>p__43 == null)
					{
						DataSourceExtensions.<>o__15.<>p__43 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IntegratedSecurity", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					if (target29(<>p__29, target30(<>p__30, DataSourceExtensions.<>o__15.<>p__43.Target(DataSourceExtensions.<>o__15.<>p__43, obj5), null)))
					{
						dataSource.CredentialRetrieval = CredentialRetrievalType.integrated;
						return;
					}
				}
				else
				{
					if (DataSourceExtensions.<>o__15.<>p__48 == null)
					{
						DataSourceExtensions.<>o__15.<>p__48 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					Func<CallSite, object, bool> target31 = DataSourceExtensions.<>o__15.<>p__48.Target;
					CallSite <>p__31 = DataSourceExtensions.<>o__15.<>p__48;
					if (DataSourceExtensions.<>o__15.<>p__47 == null)
					{
						DataSourceExtensions.<>o__15.<>p__47 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DataSourceExtensions), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target32 = DataSourceExtensions.<>o__15.<>p__47.Target;
					CallSite <>p__32 = DataSourceExtensions.<>o__15.<>p__47;
					if (DataSourceExtensions.<>o__15.<>p__46 == null)
					{
						DataSourceExtensions.<>o__15.<>p__46 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "DataSourceDefinition", typeof(DataSourceExtensions), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					if (!target31(<>p__31, target32(<>p__32, DataSourceExtensions.<>o__15.<>p__46.Target(DataSourceExtensions.<>o__15.<>p__46, obj), null)))
					{
						throw new InvalidXmlException();
					}
					Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition dataSourceDefinition = JsonConvert.DeserializeObject<DataSourceExtensions.DataSourceDefinitionWrapper>(text2).DataSourceDefinition;
					dataSource.ConnectionString = dataSourceDefinition.ConnectString;
					dataSource.DataSourceType = dataSourceDefinition.Extension;
					dataSource.IsEnabled = dataSourceDefinition.Enabled;
					dataSource.CredentialRetrieval = (CredentialRetrievalType)dataSourceDefinition.CredentialRetrieval;
					dataSource.IsConnectionStringOverridden = true;
					CredentialRetrievalType credentialRetrieval = dataSource.CredentialRetrieval;
					if (credentialRetrieval == CredentialRetrievalType.prompt)
					{
						dataSource.CredentialsByUser = new CredentialsSuppliedByUser
						{
							DisplayText = dataSourceDefinition.Prompt,
							UseAsWindowsCredentials = dataSourceDefinition.WindowsCredentials
						};
						return;
					}
					if (credentialRetrieval == CredentialRetrievalType.store)
					{
						dataSource.CredentialsInServer = new CredentialsStoredInServer
						{
							UserName = dataSourceDefinition.UserName,
							Password = dataSourceDefinition.Password,
							UseAsWindowsCredentials = dataSourceDefinition.WindowsCredentials,
							ImpersonateAuthenticatedUser = dataSourceDefinition.ImpersonateUser
						};
						return;
					}
				}
			}
		}

		// Token: 0x0200018F RID: 399
		private class DataSourceDefinitionWrapper
		{
			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000927 RID: 2343 RVA: 0x00020AE4 File Offset: 0x0001ECE4
			// (set) Token: 0x06000928 RID: 2344 RVA: 0x00020AEC File Offset: 0x0001ECEC
			public Microsoft.ReportingServices.Library.Soap2005.DataSourceDefinition DataSourceDefinition { get; set; }
		}
	}
}
