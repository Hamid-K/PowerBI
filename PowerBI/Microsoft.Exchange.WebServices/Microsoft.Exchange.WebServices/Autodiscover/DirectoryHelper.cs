using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000012 RID: 18
	internal class DirectoryHelper
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004E24 File Offset: 0x00003E24
		public List<string> GetAutodiscoverScpUrlsForDomain(string domainName)
		{
			int num = 10;
			List<string> list;
			try
			{
				list = this.GetScpUrlList(domainName, null, ref num);
			}
			catch (InvalidOperationException ex)
			{
				this.TraceMessage(string.Format("LDAP call failed, exception: {0}", ex.ToString()));
				list = new List<string>();
			}
			catch (NotSupportedException ex2)
			{
				this.TraceMessage(string.Format("LDAP call failed, exception: {0}", ex2.ToString()));
				list = new List<string>();
			}
			catch (COMException ex3)
			{
				this.TraceMessage(string.Format("LDAP call failed, exception: {0}", ex3.ToString()));
				list = new List<string>();
			}
			return list;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004ECC File Offset: 0x00003ECC
		private List<string> GetScpUrlList(string domainName, string ldapPath, ref int maxHops)
		{
			if (maxHops <= 0)
			{
				throw new ServiceLocalException(Strings.MaxScpHopsExceeded);
			}
			maxHops--;
			this.TraceMessage(string.Format("Starting SCP lookup for domainName='{0}', root path='{1}'", domainName, ldapPath));
			string text = null;
			string text2 = null;
			string text3 = null;
			List<string> list = new List<string>();
			string text4 = ((ldapPath == null) ? "LDAP://RootDSE" : (ldapPath + "/RootDSE"));
			using (DirectoryEntry directoryEntry = new DirectoryEntry(text4))
			{
				text3 = directoryEntry.Properties["configurationNamingContext"].Value as string;
			}
			SearchResultCollection searchResultCollection = null;
			try
			{
				using (DirectoryEntry directoryEntry2 = new DirectoryEntry("LDAP://" + text3))
				{
					using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry2))
					{
						directorySearcher.Filter = "(&(objectClass=serviceConnectionPoint)(|(keywords=67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68)(keywords=77378F46-2C66-4aa9-A6A6-3E7A48B19596)))";
						directorySearcher.PropertiesToLoad.Add("keywords");
						directorySearcher.PropertiesToLoad.Add("serviceBindingInformation");
						this.TraceMessage(string.Format("Searching for SCP entries in {0}", directoryEntry2.Path));
						searchResultCollection = directorySearcher.FindAll();
					}
				}
				string text5 = "Domain=" + domainName;
				this.TraceMessage(string.Format("Scanning for SCP pointers {0}", text5));
				foreach (object obj in searchResultCollection)
				{
					SearchResult searchResult = (SearchResult)obj;
					ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["keywords"];
					if (resultPropertyValueCollection.CaseInsensitiveContains("67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68"))
					{
						string text6 = searchResult.Properties["serviceBindingInformation"][0] as string;
						if (resultPropertyValueCollection.CaseInsensitiveContains(text5))
						{
							this.TraceMessage(string.Format("SCP pointer for '{0}' is found in '{1}', restarting seach in '{2}'", text5, searchResult.Path, text6));
							return this.GetScpUrlList(domainName, text6, ref maxHops);
						}
						if (resultPropertyValueCollection.Count == 1 && string.IsNullOrEmpty(text2))
						{
							text2 = text6;
							this.TraceMessage(string.Format("Fallback SCP pointer='{0}' for '{1}' is found in '{2}' and saved.", text2, text5, searchResult.Path));
						}
					}
				}
				this.TraceMessage(string.Format("No SCP pointers found for '{0}' in configPath='{1}'", text5, text3));
				string siteName = this.GetSiteName();
				if (!string.IsNullOrEmpty(siteName))
				{
					string text7 = "Site=";
					string text8 = text7 + siteName;
					List<string> list2 = new List<string>();
					this.TraceMessage(string.Format("Scanning for SCP urls for the current computer {0}", text8));
					foreach (object obj2 in searchResultCollection)
					{
						SearchResult searchResult2 = (SearchResult)obj2;
						ResultPropertyValueCollection resultPropertyValueCollection2 = searchResult2.Properties["keywords"];
						if (resultPropertyValueCollection2.CaseInsensitiveContains("77378F46-2C66-4aa9-A6A6-3E7A48B19596") && searchResult2.Properties["serviceBindingInformation"].Count > 0)
						{
							text = searchResult2.Properties["serviceBindingInformation"][0] as string;
							if (resultPropertyValueCollection2.CaseInsensitiveContains(text8))
							{
								if (!list.CaseInsensitiveContains(text))
								{
									this.TraceMessage(string.Format("Adding (prio 1) '{0}' for the '{1}' from '{2}' to the top of the list (exact match)", text, text8, searchResult2.Path));
									list.Add(text);
								}
							}
							else
							{
								bool flag = false;
								foreach (object obj3 in resultPropertyValueCollection2)
								{
									string text9 = (string)obj3;
									flag |= text9.StartsWith(text7, 5);
								}
								if (!list2.CaseInsensitiveContains(text))
								{
									if (!flag)
									{
										this.TraceMessage(string.Format("Adding (prio 2) '{0}' from '{1}' to the middle of the list (wildcard)", text, searchResult2.Path));
										list2.Insert(0, text);
									}
									else
									{
										this.TraceMessage(string.Format("Adding (prio 3) '{0}' from '{1}' to the end of the list (site mismatch)", text, searchResult2.Path));
										list2.Add(text);
									}
								}
							}
						}
					}
					if (list2.Count > 0)
					{
						foreach (string text10 in list2)
						{
							if (!list.CaseInsensitiveContains(text10))
							{
								list.Add(text10);
							}
						}
					}
				}
			}
			finally
			{
				if (searchResultCollection != null)
				{
					searchResultCollection.Dispose();
				}
			}
			if (list.Count == 0 && !string.IsNullOrEmpty(text2))
			{
				this.TraceMessage(string.Format("Restarting search for domain '{0}' in SCP fallback pointer '{1}'", domainName, text2));
				return this.GetScpUrlList(domainName, text2, ref maxHops);
			}
			return list;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005408 File Offset: 0x00004408
		private string GetSiteName()
		{
			string text;
			try
			{
				using (ActiveDirectorySite computerSite = ActiveDirectorySite.GetComputerSite())
				{
					text = computerSite.Name;
				}
			}
			catch (ActiveDirectoryObjectNotFoundException)
			{
				text = null;
			}
			catch (ActiveDirectoryOperationException)
			{
				text = null;
			}
			catch (ActiveDirectoryServerDownException)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005474 File Offset: 0x00004474
		private void TraceMessage(string message)
		{
			this.Service.TraceMessage(TraceFlags.AutodiscoverConfiguration, message);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005485 File Offset: 0x00004485
		public DirectoryHelper(ExchangeServiceBase service)
		{
			this.service = service;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005494 File Offset: 0x00004494
		internal ExchangeServiceBase Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x04000050 RID: 80
		private const int AutodiscoverMaxScpHops = 10;

		// Token: 0x04000051 RID: 81
		private const string ScpUrlGuidString = "77378F46-2C66-4aa9-A6A6-3E7A48B19596";

		// Token: 0x04000052 RID: 82
		private const string ScpPtrGuidString = "67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68";

		// Token: 0x04000053 RID: 83
		private const string ScpFilterString = "(&(objectClass=serviceConnectionPoint)(|(keywords=67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68)(keywords=77378F46-2C66-4aa9-A6A6-3E7A48B19596)))";

		// Token: 0x04000054 RID: 84
		private ExchangeServiceBase service;
	}
}
