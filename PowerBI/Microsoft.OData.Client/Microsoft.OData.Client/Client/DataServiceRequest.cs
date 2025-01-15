using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x020000CE RID: 206
	public abstract class DataServiceRequest
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0001C42D File Offset: 0x0001A62D
		internal DataServiceRequest()
		{
			this.PayloadKind = ODataPayloadKind.Unsupported;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006AD RID: 1709
		public abstract Type ElementType { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006AE RID: 1710
		// (set) Token: 0x060006AF RID: 1711
		public abstract Uri RequestUri { get; internal set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006B0 RID: 1712
		internal abstract ProjectionPlan Plan { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001C440 File Offset: 0x0001A640
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001C448 File Offset: 0x0001A648
		internal ODataPayloadKind PayloadKind { get; set; }

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001C451 File Offset: 0x0001A651
		internal static MaterializeAtom Materialize(ResponseInfo responseInfo, QueryComponents queryComponents, ProjectionPlan plan, string contentType, IODataResponseMessage message, ODataPayloadKind expectedPayloadKind)
		{
			if (message.StatusCode == 204 || string.IsNullOrEmpty(contentType))
			{
				return MaterializeAtom.EmptyResults;
			}
			return new MaterializeAtom(responseInfo, queryComponents, plan, message, expectedPayloadKind);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001C47C File Offset: 0x0001A67C
		internal static DataServiceRequest GetInstance(Type elementType, Uri requestUri)
		{
			Type type = typeof(DataServiceRequest<>).MakeGenericType(new Type[] { elementType });
			return (DataServiceRequest)Activator.CreateInstance(type, new object[] { requestUri });
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		internal static IEnumerable<TElement> EndExecute<TElement>(object source, DataServiceContext context, string method, IAsyncResult asyncResult)
		{
			IEnumerable<TElement> enumerable;
			try
			{
				QueryResult queryResult = QueryResult.EndExecuteQuery<TElement>(source, method, asyncResult);
				enumerable = queryResult.ProcessResult<TElement>(queryResult.ServiceRequest.Plan);
			}
			catch (DataServiceQueryException ex)
			{
				Exception ex2 = ex;
				while (ex2.InnerException != null)
				{
					ex2 = ex2.InnerException;
				}
				DataServiceClientException ex3 = ex2 as DataServiceClientException;
				if (!context.IgnoreResourceNotFoundException || ex3 == null || ex3.StatusCode != 404)
				{
					throw;
				}
				enumerable = (IEnumerable<TElement>)new QueryOperationResponse<TElement>(ex.Response.HeaderCollection, ex.Response.Query, MaterializeAtom.EmptyResults)
				{
					StatusCode = 404
				};
			}
			return enumerable;
		}

		// Token: 0x060006B6 RID: 1718
		internal abstract QueryComponents QueryComponents(ClientEdmModel model);

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001C568 File Offset: 0x0001A768
		internal QueryOperationResponse<TElement> Execute<TElement>(DataServiceContext context, QueryComponents queryComponents)
		{
			QueryResult queryResult = null;
			QueryOperationResponse<TElement> queryOperationResponse;
			try
			{
				Uri uri = queryComponents.Uri;
				DataServiceRequest<TElement> dataServiceRequest = new DataServiceRequest<TElement>(uri, queryComponents, this.Plan);
				queryResult = dataServiceRequest.CreateExecuteResult(this, context, null, null, "Execute");
				queryResult.ExecuteQuery();
				queryOperationResponse = queryResult.ProcessResult<TElement>(this.Plan);
			}
			catch (InvalidOperationException ex)
			{
				if (queryResult != null)
				{
					QueryOperationResponse response = queryResult.GetResponse<TElement>(MaterializeAtom.EmptyResults);
					if (response != null)
					{
						if (context.IgnoreResourceNotFoundException)
						{
							DataServiceClientException ex2 = ex as DataServiceClientException;
							if (ex2 != null && ex2.StatusCode == 404)
							{
								return (QueryOperationResponse<TElement>)response;
							}
						}
						response.Error = ex;
						throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, response);
					}
				}
				throw;
			}
			return queryOperationResponse;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001C620 File Offset: 0x0001A820
		internal long GetQuerySetCount(DataServiceContext context)
		{
			Version version = this.QueryComponents(context.Model).Version;
			if (version == null)
			{
				version = Util.ODataVersion4;
			}
			QueryResult queryResult = null;
			QueryComponents queryComponents = this.QueryComponents(context.Model);
			Uri uri = queryComponents.Uri;
			DataServiceRequest<long> dataServiceRequest = new DataServiceRequest<long>(uri, queryComponents, null);
			HeaderCollection headerCollection = new HeaderCollection();
			headerCollection.SetRequestVersion(version, context.MaxProtocolVersionAsVersion);
			context.Format.SetRequestAcceptHeaderForCount(headerCollection);
			string text = "GET";
			ODataRequestMessageWrapper odataRequestMessageWrapper = context.CreateODataRequestMessage(context.CreateRequestArgsAndFireBuildingRequest(text, uri, headerCollection, context.HttpStack, null), null);
			queryResult = new QueryResult(this, "Execute", dataServiceRequest, odataRequestMessageWrapper, new RequestInfo(context), null, null);
			long num2;
			try
			{
				queryResult.ExecuteQuery();
				if (HttpStatusCode.NoContent == queryResult.StatusCode)
				{
					throw new DataServiceQueryException(Strings.DataServiceRequest_FailGetCount, queryResult.Failure);
				}
				StreamReader streamReader = new StreamReader(queryResult.GetResponseStream());
				long num = -1L;
				try
				{
					num = XmlConvert.ToInt64(streamReader.ReadToEnd());
				}
				finally
				{
					streamReader.Close();
				}
				num2 = num;
			}
			catch (InvalidOperationException ex)
			{
				QueryOperationResponse response = queryResult.GetResponse<long>(MaterializeAtom.EmptyResults);
				if (response != null)
				{
					response.Error = ex;
					throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, response);
				}
				throw;
			}
			return num2;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001C76C File Offset: 0x0001A96C
		internal IAsyncResult BeginExecute(object source, DataServiceContext context, AsyncCallback callback, object state, string method)
		{
			QueryResult queryResult = this.CreateExecuteResult(source, context, callback, state, method);
			queryResult.BeginExecuteQuery();
			return queryResult;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001C790 File Offset: 0x0001A990
		private QueryResult CreateExecuteResult(object source, DataServiceContext context, AsyncCallback callback, object state, string method)
		{
			QueryComponents queryComponents = this.QueryComponents(context.Model);
			RequestInfo requestInfo = new RequestInfo(context);
			if (queryComponents.UriOperationParameters != null)
			{
				Serializer serializer = new Serializer(requestInfo);
				this.RequestUri = serializer.WriteUriOperationParametersToUri(this.RequestUri, queryComponents.UriOperationParameters);
			}
			HeaderCollection headerCollection = new HeaderCollection();
			if (string.CompareOrdinal("POST", queryComponents.HttpMethod) == 0)
			{
				if (queryComponents.BodyOperationParameters == null)
				{
					headerCollection.SetHeader("Content-Length", "0");
				}
				else
				{
					context.Format.SetRequestContentTypeForOperationParameters(headerCollection);
				}
			}
			headerCollection.SetRequestVersion(queryComponents.Version, requestInfo.MaxProtocolVersionAsVersion);
			requestInfo.Format.SetRequestAcceptHeaderForQuery(headerCollection, queryComponents);
			ODataRequestMessageWrapper odataRequestMessageWrapper = new RequestInfo(context).WriteHelper.CreateRequestMessage(context.CreateRequestArgsAndFireBuildingRequest(queryComponents.HttpMethod, this.RequestUri, headerCollection, context.HttpStack, null));
			odataRequestMessageWrapper.FireSendingRequest2(null);
			if (queryComponents.BodyOperationParameters != null)
			{
				Serializer serializer2 = new Serializer(requestInfo, context.EntityParameterSendOption);
				serializer2.WriteBodyOperationParameters(queryComponents.BodyOperationParameters, odataRequestMessageWrapper);
				return new QueryResult(source, method, this, odataRequestMessageWrapper, requestInfo, callback, state, odataRequestMessageWrapper.CachedRequestStream);
			}
			return new QueryResult(source, method, this, odataRequestMessageWrapper, requestInfo, callback, state);
		}
	}
}
