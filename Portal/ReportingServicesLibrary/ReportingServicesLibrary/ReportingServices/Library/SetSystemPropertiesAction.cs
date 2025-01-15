using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C6 RID: 198
	internal sealed class SetSystemPropertiesAction : RSSoapAction<SetSystemPropertiesActionParameters>
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x000221F6 File Offset: 0x000203F6
		public SetSystemPropertiesAction(RSService service)
			: base("SetSystemPropertiesAction", service)
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00022204 File Offset: 0x00020404
		protected override void FinalizeAction()
		{
			CachedSystemProperties.InvalidateCache();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002220C File Offset: 0x0002040C
		internal override void PerformActionNow()
		{
			ExternalItemPath externalItemPath = new ExternalItemPath(base.Service.PropertyProvider.GetSystemUrl());
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.UpdateSystemProperties, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			SystemProperties systemProperties = new SystemProperties(base.ActionParameters.SystemProperties);
			systemProperties.EnsurePropertiesWritable();
			SystemProperties allConfigurationInfo = base.Service.Storage.GetAllConfigurationInfo();
			for (int i = 0; i < systemProperties.Count; i++)
			{
				string name = systemProperties.GetName(i);
				string text = allConfigurationInfo[name];
				string value = systemProperties.GetValue(i);
				if (text != value)
				{
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
					if (num <= 2707123363U)
					{
						if (num != 944640186U)
						{
							if (num != 1878817881U)
							{
								if (num == 2707123363U)
								{
									if (name == "OAuthClientSecret")
									{
										systemProperties[name] = CatalogEncryption.Instance.EncryptToString(value);
									}
								}
							}
							else if (name == "MaxFileSizeMb")
							{
								this.OnSetMaxFileSizeMb(value);
							}
						}
						else if (name == "MyReportsRole")
						{
							this.OnSetMyReportRole(text, value);
						}
					}
					else if (num <= 3214216043U)
					{
						if (num != 3046869803U)
						{
							if (num == 3214216043U)
							{
								if (name == "EnableMyReports")
								{
									this.OnSetEnableMyReports(text, value);
								}
							}
						}
						else if (name == "SystemSnapshotLimit")
						{
							this.OnSetSystemSnapshotLimit(text, value);
						}
					}
					else if (num != 3352570797U)
					{
						if (num == 3477586280U)
						{
							if (name == "EnableClientPrinting")
							{
								this.OnSetEnableClientPrinting(value);
							}
						}
					}
					else if (name == "SiteName")
					{
						this.OnSetSiteName(value);
					}
				}
			}
			base.Service.Storage.SetConfigurationInfo(systemProperties);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00022424 File Offset: 0x00020624
		private void OnSetSystemSnapshotLimit(string oldSnapshotLimit, string newSnapshotLimit)
		{
			int num = -1;
			if (oldSnapshotLimit != null)
			{
				num = int.Parse(oldSnapshotLimit, CultureInfo.InvariantCulture);
			}
			int num2;
			try
			{
				num2 = int.Parse(newSnapshotLimit, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new ElementTypeMismatchException("SystemSnapshotLimit");
			}
			if (num2 <= 0 && num2 != -1)
			{
				throw new InvalidElementException("SystemSnapshotLimit");
			}
			if ((num2 < num && num2 >= 0 && num >= 0) || (num == -1 && num2 >= 0) || (num == -2 && num2 >= 0))
			{
				if (num2 == 0)
				{
					base.Service.Storage.DeleteHistoriesWithNoPolicy();
					return;
				}
				base.Service.Storage.CleanAllHistories(num2);
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000224C4 File Offset: 0x000206C4
		private void OnSetMyReportRole(string oldValue, string newValue)
		{
			if (oldValue != newValue && !base.Service.SecMgr.RoleExists(newValue))
			{
				throw new RoleNotFoundException(newValue);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000224EC File Offset: 0x000206EC
		private void OnSetEnableMyReports(string oldValue, string newValue)
		{
			bool flag = oldValue != null && bool.Parse(oldValue);
			bool flag2;
			try
			{
				flag2 = bool.Parse(newValue);
			}
			catch (FormatException)
			{
				throw new ElementTypeMismatchException("EnableMyReports");
			}
			if (flag == flag2)
			{
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "EnableMyReports value unchanged ({0})", new object[] { flag });
				}
				return;
			}
			if (flag2)
			{
				if (RSTrace.CatalogTrace.TraceInfo)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Info, "OnSetEnableMyReports: Enabling My Reports");
				}
				if (!base.Service.SecMgr.RoleExists(base.Service.MyReportsRole))
				{
					throw new RoleNotFoundException(base.Service.MyReportsRole);
				}
				if (base.Service.Storage.ObjectExists(new ExternalItemPath(Global.VirtualMyReportsPath)))
				{
					throw new ItemAlreadyExistsException(Global.VirtualMyReportsPath);
				}
				ExternalItemPath empty = ExternalItemPath.Empty;
				ItemType itemType;
				Guid guid;
				byte[] array;
				base.Service.Storage.ObjectExists(empty, out itemType, out guid, out array);
				Guid empty2 = Guid.Empty;
				ItemType itemType2;
				if (!base.Service.Storage.ObjectExists(new ExternalItemPath(Global.AllUsersFolderPath), out itemType2))
				{
					DateTime now = DateTime.Now;
					if (base.Service.Storage.CreateObject(Guid.NewGuid(), Global.AllUsersFolderName, new CatalogItemPath(Global.AllUsersFolderPath), empty, guid, ItemType.Folder, null, Guid.Empty, Guid.Empty, null, null, null, Global.GetSystemSid(base.Service.UserContext.AuthenticationType), Global.GetSystemUserName(base.Service.UserContext.AuthenticationType), now, now, null) == Guid.Empty)
					{
						throw new InternalCatalogException("Couldn't create Users Folders but it doesn't exist.");
					}
					string text = "<Policies></Policies>";
					base.Service.SecMgr.SetCatalogItemPolicies(new ExternalItemPath(Global.AllUsersFolderPath), ItemType.Folder, text);
					return;
				}
				else
				{
					if (RSTrace.CatalogTrace.TraceWarning)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "OnSetEnableMyReports: AllUsersFolder already exists on enabling of My Reports");
					}
					if (itemType2 != ItemType.Folder)
					{
						throw new WrongItemTypeException(Global.AllUsersFolderPath);
					}
				}
			}
			else if (RSTrace.CatalogTrace.TraceInfo)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "OnSetEnableMyReports: Disabling My Reports");
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002270C File Offset: 0x0002090C
		private void OnSetEnableClientPrinting(string newValue)
		{
			try
			{
				bool.Parse(newValue);
			}
			catch (FormatException)
			{
				throw new ElementTypeMismatchException("EnableClientPrinting");
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00022740 File Offset: 0x00020940
		private void OnSetSiteName(string newValue)
		{
			if (string.IsNullOrEmpty(newValue.Trim()))
			{
				throw new InvalidElementException("SiteName");
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002275A File Offset: 0x0002095A
		private void OnSetMaxFileSizeMb(string newValue)
		{
			if (string.IsNullOrEmpty(newValue.Trim()))
			{
				throw new InvalidElementException("MaxFileSizeMb");
			}
		}
	}
}
