using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FDD RID: 4061
	internal class ActiveDirectoryService : IActiveDirectoryService
	{
		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06006A7A RID: 27258 RVA: 0x0016E64D File Offset: 0x0016C84D
		public string ComputerDomainName
		{
			get
			{
				return IPGlobalProperties.GetIPGlobalProperties().DomainName;
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0008A700 File Offset: 0x00088900
		public int PageSize
		{
			get
			{
				return 1024;
			}
		}

		// Token: 0x06006A7C RID: 27260 RVA: 0x0016E65C File Offset: 0x0016C85C
		public IEnumerable<ActiveDirectoryServiceSearchResult> FindAll(DirectoryEntry searchRoot, string filter, SortOption sortOption, RowCount rowCount, SearchScope searchScope, params string[] propertiesToLoad)
		{
			if (!rowCount.IsZero)
			{
				using (DirectorySearcher directorySearcher = this.CreateSearcher(searchRoot, filter, sortOption, rowCount, searchScope, propertiesToLoad))
				{
					try
					{
						return ActiveDirectoryService.GetResults(directorySearcher.FindAll());
					}
					catch (COMException ex)
					{
						throw new ActiveDirectoryServiceException(ex);
					}
				}
			}
			return new ActiveDirectoryServiceSearchResult[0];
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x0016E6C4 File Offset: 0x0016C8C4
		public ActiveDirectoryServiceSearchResult FindOne(DirectoryEntry searchRoot, string filter, SearchScope searchScope, params string[] propertiesToLoad)
		{
			ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult;
			try
			{
				using (DirectorySearcher directorySearcher = this.CreateSearcher(searchRoot, filter, null, new RowCount(1L), searchScope, propertiesToLoad))
				{
					activeDirectoryServiceSearchResult = new DirectoryServicesActiveDirectoryServiceSearchResult(directorySearcher.FindOne());
				}
			}
			catch (COMException ex)
			{
				throw new ActiveDirectoryServiceException(ex);
			}
			return activeDirectoryServiceSearchResult;
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x0016E720 File Offset: 0x0016C920
		public ActiveDirectoryRootServiceEntry GetRootServiceEntry(DirectoryEntry root)
		{
			ActiveDirectoryRootServiceEntry activeDirectoryRootServiceEntry;
			try
			{
				activeDirectoryRootServiceEntry = new ActiveDirectoryRootServiceEntry
				{
					RootDomainNamingContext = root.Properties["RootDomainNamingContext"].Value.ToString(),
					DefaultNamingContext = root.Properties["DefaultNamingContext"].Value.ToString(),
					ConfigurationNamingContext = root.Properties["ConfigurationNamingContext"].Value.ToString(),
					SchemaNamingContext = root.Properties["SchemaNamingContext"].Value.ToString()
				};
			}
			catch (COMException ex)
			{
				throw new ActiveDirectoryServiceException(ex);
			}
			return activeDirectoryRootServiceEntry;
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x0016E7CC File Offset: 0x0016C9CC
		private static IEnumerable<ActiveDirectoryServiceSearchResult> GetResults(SearchResultCollection results)
		{
			IEnumerator enumerator;
			try
			{
				enumerator = results.GetEnumerator();
			}
			catch (COMException ex)
			{
				throw new ActiveDirectoryServiceException(ex);
			}
			for (;;)
			{
				SearchResult searchResult;
				try
				{
					if (!enumerator.MoveNext())
					{
						yield break;
					}
					searchResult = (SearchResult)enumerator.Current;
				}
				catch (DirectoryServicesCOMException ex2)
				{
					if (ex2.ExtendedError == 234)
					{
						continue;
					}
					throw new ActiveDirectoryServiceException(ex2);
				}
				catch (COMException ex3)
				{
					throw new ActiveDirectoryServiceException(ex3);
				}
				yield return new DirectoryServicesActiveDirectoryServiceSearchResult(searchResult);
			}
			yield break;
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x0016E7DC File Offset: 0x0016C9DC
		private DirectorySearcher CreateSearcher(DirectoryEntry searchRoot, string filter, SortOption sortOption, RowCount rowCount, SearchScope searchScope, string[] propertiesToLoad)
		{
			DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot);
			directorySearcher.PageSize = (rowCount.IsInfinite ? this.PageSize : ((int)Math.Min(rowCount.Value, (long)this.PageSize)));
			directorySearcher.Filter = filter;
			directorySearcher.SizeLimit = (rowCount.IsInfinite ? int.MaxValue : ((int)Math.Min(rowCount.Value, 2147483647L)));
			directorySearcher.Asynchronous = true;
			directorySearcher.CacheResults = false;
			directorySearcher.SearchScope = searchScope;
			if (sortOption != null)
			{
				directorySearcher.Sort = sortOption;
			}
			directorySearcher.PropertiesToLoad.Clear();
			foreach (string text in propertiesToLoad)
			{
				directorySearcher.PropertiesToLoad.Add(text);
			}
			return directorySearcher;
		}

		// Token: 0x04003B23 RID: 15139
		private const int ServerTakingLongErrorCode = 234;
	}
}
