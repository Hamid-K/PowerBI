using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008AD RID: 2221
	internal static class ConcurrentRequestor
	{
		// Token: 0x06003F7D RID: 16253 RVA: 0x000D1758 File Offset: 0x000CF958
		public static List<IValueReference> CreatePagedValues(ODataEnvironment environment, TypeValue type, Uri uri, int expectedRequests, bool useConcurrentRequests, out Uri nextPageUri)
		{
			int num = Math.Min(expectedRequests, environment.ConcurrentRequestsLimit);
			if (useConcurrentRequests)
			{
				try
				{
					Uri[] array = ConcurrentRequestor.CreatePageUris(uri, num, environment);
					return ConcurrentRequestor.CreateBatchResult(uri, array, environment, type, useConcurrentRequests, out nextPageUri);
				}
				catch (WebException)
				{
					environment.DisableConcurrentRequests();
				}
				catch (IOException ex)
				{
					throw DataSourceException.NewDataSourceError<Message1>(environment.Host, Strings.IoError(uri), Resource.New(environment.Resource.Kind, uri.AbsoluteUri), null, ex);
				}
			}
			List<IValueReference> list;
			try
			{
				list = ODataMessageReaderValueConverters.CreateSerialValues(environment, type, false, uri, out nextPageUri, null);
			}
			catch (WebException ex2)
			{
				throw ODataCommonErrors.RequestFailed(environment.Host, ex2, uri, environment.HttpResource);
			}
			return list;
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000D1814 File Offset: 0x000CFA14
		private static void GetResponse(object obj)
		{
			((RequestInfo)obj).CreateResult();
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000D1824 File Offset: 0x000CFA24
		private static List<int> ExecuteRequestsInParallel(IThreadPoolService threadPool, RequestInfo[] requests)
		{
			for (int i = 0; i < requests.Length; i++)
			{
				WaitCallback waitCallback = CloneCurrentCultures.CreateWrapper(new WaitCallback(ConcurrentRequestor.GetResponse));
				threadPool.QueueUserWorkItem(waitCallback, requests[i]);
			}
			for (int j = 0; j < requests.Length; j++)
			{
				requests[j].Complete.WaitOne();
			}
			List<int> list = null;
			for (int k = 0; k < requests.Length; k++)
			{
				if (requests[k].Exception != null)
				{
					WebException ex = requests[k].Exception as WebException;
					if ((ex == null || !HttpResponseHandler.IsBadRequestError(ex)) && !(requests[k].Exception is IOException))
					{
						throw requests[k].Exception;
					}
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(k);
				}
			}
			return list;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000D18E4 File Offset: 0x000CFAE4
		private static Uri[] CreatePageUris(Uri uri, int concurrentRequests, ODataEnvironment environment)
		{
			int num;
			int? num2;
			Uri uri2 = ODataUriCommon.RemoveSkipAndTake(uri, out num, out num2);
			int pageSize = environment.PageSize;
			int num3 = pageSize * concurrentRequests;
			int num4 = Math.Min(num2.GetValueOrDefault(num3), num3);
			int num5 = num4 / pageSize;
			int num6 = num4 % pageSize;
			Uri[] array = new Uri[num5 + ((num6 > 0) ? 1 : 0)];
			for (int i = 0; i < num5; i++)
			{
				array[i] = ConcurrentRequestor.CreatePageUri(uri2, num + i * pageSize, new int?(pageSize));
			}
			if (num6 > 0)
			{
				array[num5] = ConcurrentRequestor.CreatePageUri(uri2, num + num5 * pageSize, new int?(num6));
			}
			return array;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000D1978 File Offset: 0x000CFB78
		private static Uri CreatePageUri(Uri baseUri, int skip, int? top)
		{
			if (skip == 0 && top == null)
			{
				return baseUri;
			}
			UriBuilder uriBuilder = new UriBuilder(baseUri);
			string text = UriHelper.NormalizeUriComponent(uriBuilder.Query);
			if (skip != 0)
			{
				text = UriHelper.AddQueryPart(text, "$skip", skip.ToString(CultureInfo.InvariantCulture));
			}
			if (top != null)
			{
				text = UriHelper.AddQueryPart(text, "$top", top.Value.ToString(CultureInfo.InvariantCulture));
			}
			uriBuilder.Query = text;
			return uriBuilder.Uri;
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000D19F8 File Offset: 0x000CFBF8
		private static Uri CreateNextUri(Uri uri, int pageSize, int completedPages)
		{
			int num;
			int? num2;
			Uri uri2 = ODataUriCommon.RemoveSkipAndTake(uri, out num, out num2);
			num += completedPages * pageSize;
			if (num2 != null)
			{
				num2 -= completedPages * pageSize;
			}
			if (num2 != null)
			{
				int? num3 = num2;
				int num4 = 0;
				if (!((num3.GetValueOrDefault() > num4) & (num3 != null)))
				{
					return null;
				}
			}
			return ConcurrentRequestor.CreatePageUri(uri2, num, num2);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000D1A78 File Offset: 0x000CFC78
		private static List<IValueReference> CreateBatchResult(Uri baseUri, Uri[] pageUris, ODataEnvironment environment, TypeValue type, bool useConcurrentRequests, out Uri nextUri)
		{
			RequestInfo[] array = new RequestInfo[pageUris.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new RequestInfo(pageUris[i], environment, type, false);
			}
			int num = array.Length;
			List<int> list = ConcurrentRequestor.ExecuteRequestsInParallel(environment.Host.QueryService<IThreadPoolService>(), array);
			if (list != null)
			{
				int num2 = array.Length - list.Count;
				if (num2 == 0)
				{
					throw array[0].Exception;
				}
				if (environment.ConcurrentRequestsLimit > 1)
				{
					environment.ConcurrentRequestsLimit = Math.Max(num2, 1);
				}
				num = list[0];
			}
			bool flag = false;
			List<IValueReference> list2 = null;
			int num3 = 0;
			while (!flag && num3 < num)
			{
				if (array[num3].Result != null)
				{
					bool flag2 = array[num3].Result.Count == environment.PageSize;
					flag = !flag2 && array[num3].NextPageUri == null;
					if (list2 == null)
					{
						list2 = new List<IValueReference>();
					}
					list2.AddRange(array[num3].Result);
					if (!flag2 && !flag)
					{
						num = num3 + 1;
						environment.PageSize = array[num3].Result.Count;
					}
				}
				else
				{
					flag = true;
				}
				num3++;
			}
			if (!flag)
			{
				if (useConcurrentRequests)
				{
					nextUri = ConcurrentRequestor.CreateNextUri(baseUri, environment.PageSize, num);
				}
				else
				{
					nextUri = array[num - 1].NextPageUri;
				}
			}
			else
			{
				nextUri = null;
			}
			return list2;
		}
	}
}
