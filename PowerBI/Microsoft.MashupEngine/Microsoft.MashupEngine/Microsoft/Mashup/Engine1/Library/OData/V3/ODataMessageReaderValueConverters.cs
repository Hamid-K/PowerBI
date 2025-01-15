using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Spatial;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008C3 RID: 2243
	internal class ODataMessageReaderValueConverters
	{
		// Token: 0x0600402A RID: 16426 RVA: 0x000D6166 File Offset: 0x000D4366
		private ODataMessageReaderValueConverters(ODataEnvironment environment, TypeValue type)
		{
			this.environment = environment;
			this.requestType = type;
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000D6188 File Offset: 0x000D4388
		public static List<IValueReference> CreateCollectionResult(TypeValue itemType, ODataCollectionReader reader, ODataEnvironment environment = null)
		{
			List<IValueReference> list = new List<IValueReference>();
			while (reader.Read())
			{
				if (reader.State == ODataCollectionReaderState.Value)
				{
					list.Add(ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(ODataWrapperHelper.WrapValueIfNecessary(reader.Item), itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment));
				}
			}
			return list;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000D61D4 File Offset: 0x000D43D4
		public static ValueException CreateErrorResult(IEngineHost engineHost, ODataMessageReader reader, Uri uri, string resourceKind)
		{
			ODataError odataError = reader.ReadError();
			return DataSourceException.NewDataSourceError<Message2>(engineHost, DataSourceException.DataSourceMessage("OData", odataError.Message), Resource.New(resourceKind, uri.AbsoluteUri), null, null);
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000D620C File Offset: 0x000D440C
		public static Value CreateFeedEntryResult(ODataReader reader, Uri uri, ODataEnvironment environment, TypeValue type, bool expectSingleton)
		{
			RequestInfo requestInfo = new RequestInfo(uri, environment, type, expectSingleton);
			List<IValueReference> list = new ODataMessageReaderValueConverters(environment, type).CreateFeedEntryResult(new ODataMessageReaderValueConverters.ODataReaderWithRequest(requestInfo, reader));
			if (!expectSingleton)
			{
				return ODataValue.Create(environment, type, requestInfo.NextPageUri, list, true);
			}
			if (list.Count != 0)
			{
				return list.Single<IValueReference>().Value;
			}
			return Value.Null;
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000D6265 File Offset: 0x000D4465
		public static List<IValueReference> CreateFeedEntryResult(RequestInfo request, ODataReader reader)
		{
			return new ODataMessageReaderValueConverters(request.Environment, request.Type).CreateFeedEntryResult(new ODataMessageReaderValueConverters.ODataReaderWithRequest(request, reader));
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x000D6284 File Offset: 0x000D4484
		public static Value CreatePropertyResult(TypeValue itemType, ODataMessageReader reader, ODataEnvironment environment = null)
		{
			return ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(new ODataPropertyWrapper(reader.ReadProperty()), itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment);
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x000D62A4 File Offset: 0x000D44A4
		public static Value CreateRawResult(TypeValue itemType, ODataMessageReader reader, ODataEnvironment environment = null)
		{
			return ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(reader.ReadValue(ODataTypeServices.GetEdmPrimitiveTypeReference(itemType, false)), itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment);
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000D62C8 File Offset: 0x000D44C8
		public static List<IValueReference> RequestResult(ODataEnvironment environment, TypeValue type, bool isSingleton, Uri uri, out Uri nextPageUri, Func<RequestInfo, Uri> GetNextPageUri = null)
		{
			RequestInfo requestInfo = new RequestInfo(uri, environment, type, isSingleton);
			requestInfo.CreateResult();
			if (GetNextPageUri == null)
			{
				nextPageUri = requestInfo.NextPageUri;
			}
			else
			{
				nextPageUri = GetNextPageUri(requestInfo);
			}
			if (requestInfo.Exception == null)
			{
				environment.PageSize = ((requestInfo.Result != null) ? requestInfo.Result.Count : 0);
				return requestInfo.Result;
			}
			WebException ex = requestInfo.Exception as WebException;
			if (ex != null && ex.Status == WebExceptionStatus.ProtocolError)
			{
				throw new FoldingFailureException(ODataCommonErrors.RequestFailed(environment.Host, ex, uri, environment.HttpResource));
			}
			if (requestInfo.Exception is ResourceAccessAuthorizationException || requestInfo.Exception is ResourceAccessForbiddenException)
			{
				throw new FoldingFailureException(requestInfo.Exception);
			}
			IOException ex2 = requestInfo.Exception as IOException;
			if (ex2 != null)
			{
				throw DataSourceException.NewDataSourceError<Message1>(environment.Host, Strings.IoError(uri), Resource.New(environment.Resource.Kind, uri.AbsoluteUri), null, ex2);
			}
			if (requestInfo.Exception is RuntimeException || requestInfo.Exception is FormatException || requestInfo.Exception is WebException || !SafeExceptions.IsSafeException(requestInfo.Exception))
			{
				throw requestInfo.Exception;
			}
			throw new Exception(requestInfo.Exception.Message, requestInfo.Exception);
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000D6410 File Offset: 0x000D4610
		public static List<IValueReference> CreateSerialValues(ODataEnvironment environment, TypeValue type, bool isSingleton, Uri uri, out Uri nextPageUri, Func<RequestInfo, Uri> GetNextPageUri = null)
		{
			if (!environment.Settings.IsSharePoint || uri.AbsoluteUri.Length <= environment.UserSettings.MaxUriLength)
			{
				return ODataMessageReaderValueConverters.RequestResult(environment, type, isSingleton, uri, out nextPageUri, GetNextPageUri);
			}
			QueryDescriptorQueryToken queryDescriptorQueryToken;
			if (ODataUri.TryParseODataUri(environment.Host, environment.Resource, uri, environment.ServiceUri, out queryDescriptorQueryToken) && queryDescriptorQueryToken.Select != null)
			{
				QueryDescriptorQueryToken queryDescriptorQueryToken2 = new QueryDescriptorQueryToken(queryDescriptorQueryToken.Path, queryDescriptorQueryToken.Filter, queryDescriptorQueryToken.OrderByTokens, null, queryDescriptorQueryToken.Expand, queryDescriptorQueryToken.Skip, queryDescriptorQueryToken.Top, queryDescriptorQueryToken.InlineCount, queryDescriptorQueryToken.Format, queryDescriptorQueryToken.QueryOptions);
				Uri uri2 = MashupODataUriBuilder.CreateUri(environment.ServiceUri, queryDescriptorQueryToken2);
				return ODataMessageReaderValueConverters.RequestResult(environment, type, isSingleton, uri2, out nextPageUri, GetNextPageUri);
			}
			throw new FoldingFailureException(ValueException.NewDataSourceError("UrlLengthExceeded", TextValue.New(uri.AbsoluteUri), null));
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x000D64F0 File Offset: 0x000D46F0
		private List<IValueReference> CreateFeedEntryResult(ODataMessageReaderValueConverters.ODataReaderWithRequest reader)
		{
			List<IValueReference> list = null;
			reader.Read(ODataReaderState.Start);
			ODataReaderState state = reader.State;
			if (state != ODataReaderState.FeedStart)
			{
				if (state == ODataReaderState.EntryStart)
				{
					list = this.ReadEntryAsList(reader);
				}
			}
			else
			{
				Uri uri;
				list = this.ReadFeed(reader, out uri);
				reader.Request.NextPageUri = uri;
			}
			if (reader.State == ODataReaderState.Exception)
			{
				ODataErrorException errorEntry = reader.ErrorEntry;
				ValueException ex = DataSourceException.NewDataSourceError(this.environment.Host, errorEntry.Message, this.environment.Resource, new RecordKeyDefinition[]
				{
					new RecordKeyDefinition(this.ErrorProperty, TextValue.New(errorEntry.Error.Message), TypeValue.Text)
				}, errorEntry);
				list.Add(new ExceptionValueReference(ex));
			}
			else
			{
				reader.Read(ODataReaderState.Completed);
			}
			return list;
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x000D65B0 File Offset: 0x000D47B0
		private Value CreateLinkValue(TypeValue type, Uri startPageUri, bool unwrapFoldingExceptions)
		{
			if (type.TypeKind == ValueKind.Table)
			{
				if (type.AsTableType.ItemType.Fields.IsEmpty)
				{
					try
					{
						return new ODataValue.ODataListValue(this.environment, ListTypeValue.Record, startPageUri, unwrapFoldingExceptions, null, true).ToTableValue();
					}
					catch (FoldingFailureException ex)
					{
						throw ex.InnerException;
					}
				}
				return new CacheODataTableValue(this.environment, type.AsTableType, startPageUri, unwrapFoldingExceptions);
			}
			return new ODataValue.ODataListValue(this.environment, type, startPageUri, unwrapFoldingExceptions, null, false);
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000D6638 File Offset: 0x000D4838
		private IValueReference CreateEntryLinkValue(NavigationLinkWrapperPropertyLookupValue lookupValue, TypeValue fieldType, int column)
		{
			Func<Func<TypeValue, IValueReference>, IValueReference> <>9__1;
			return ODataStructuralValueConverter.GetDelayed<ODataException>(this.environment, delegate
			{
				bool flag = lookupValue.NavigationLinkWrapper.IsCollection();
				Value value;
				if (lookupValue.InlineEntries != null)
				{
					ODataEnvironment odataEnvironment = this.environment;
					TypeValue fieldType2 = fieldType;
					Uri nextPage = lookupValue.NextPage;
					IEnumerable<Func<TypeValue, IValueReference>> inlineEntries = lookupValue.InlineEntries;
					Func<Func<TypeValue, IValueReference>, IValueReference> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (Func<TypeValue, IValueReference> e) => e(fieldType));
					}
					value = ODataValue.Create(odataEnvironment, fieldType2, nextPage, new List<IValueReference>(inlineEntries.Select(func)), true);
				}
				else
				{
					value = this.CreateLinkValue(fieldType, lookupValue.NavigationLinkWrapper.Url, true);
				}
				if (flag)
				{
					return value;
				}
				return Library.List.SingleOrDefault.Invoke(value);
			});
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x000D6677 File Offset: 0x000D4877
		private IValueReference CreateEntryValue(TypeValue type, IEnumerable<IODataPropertyWrapper> properties, Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links)
		{
			return ODataStructuralValueConverter.CreateEntryValue<ODataException>(this.environment, type, properties, links, new Func<NavigationLinkWrapperPropertyLookupValue, TypeValue, int, IValueReference>(this.CreateEntryLinkValue), new Func<object, Value>(ODataTypeServices.MarshalFromClr), null, null);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000D66A4 File Offset: 0x000D48A4
		private IValueReference ReadEntryInternal(ODataMessageReaderValueConverters.ODataReaderWithRequest reader)
		{
			Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> dictionary;
			ODataEntry odataEntry;
			this.ReadEntryHelper(reader, out dictionary, out odataEntry);
			return this.CreateEntryValue(this.requestType, odataEntry.Properties.Select((ODataProperty p) => new ODataPropertyWrapper(p)), dictionary);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000D66F4 File Offset: 0x000D48F4
		private Func<TypeValue, IValueReference> ReadEntryFromLink(ODataMessageReaderValueConverters.ODataReaderWithRequest reader)
		{
			ODataEntry entry;
			Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links;
			this.ReadEntryHelper(reader, out links, out entry);
			if (entry != null)
			{
				return (TypeValue type) => this.CreateEntryValue(type, entry.Properties.Select((ODataProperty p) => new ODataPropertyWrapper(p)), links);
			}
			return (TypeValue type) => Value.Null;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000D6758 File Offset: 0x000D4958
		private void ReadEntryHelper(ODataMessageReaderValueConverters.ODataReaderWithRequest reader, out Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links, out ODataEntry entry)
		{
			reader.Read(ODataReaderState.EntryStart);
			links = new Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries>();
			while (reader.State == ODataReaderState.NavigationLinkStart)
			{
				List<Func<TypeValue, IValueReference>> list;
				Uri uri;
				IODataNavigationLinkWrapper iodataNavigationLinkWrapper = this.ReadLink(reader, out list, out uri);
				links.Add(iodataNavigationLinkWrapper, new NavigationLinkWrapperInlineEntries(list, uri));
			}
			entry = reader.Read<ODataEntry>(ODataReaderState.EntryEnd);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000D67A2 File Offset: 0x000D49A2
		private List<IValueReference> ReadEntryAsList(ODataMessageReaderValueConverters.ODataReaderWithRequest reader)
		{
			return new List<IValueReference>(1) { this.ReadEntryInternal(reader) };
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000D67B7 File Offset: 0x000D49B7
		private List<Func<TypeValue, IValueReference>> ReadEntryAsListFromLink(ODataMessageReaderValueConverters.ODataReaderWithRequest reader)
		{
			return new List<Func<TypeValue, IValueReference>>(1) { this.ReadEntryFromLink(reader) };
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000D67CC File Offset: 0x000D49CC
		private List<IValueReference> ReadFeed(ODataMessageReaderValueConverters.ODataReaderWithRequest reader, out Uri nextPageUri)
		{
			return ODataMessageReaderValueConverters.ReadFeedHelper<IValueReference>(reader, new Func<ODataMessageReaderValueConverters.ODataReaderWithRequest, IValueReference>(this.ReadEntryInternal), out nextPageUri);
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x000D67E1 File Offset: 0x000D49E1
		private List<Func<TypeValue, IValueReference>> ReadFeedFromLink(ODataMessageReaderValueConverters.ODataReaderWithRequest reader, out Uri nextPageUri)
		{
			return ODataMessageReaderValueConverters.ReadFeedHelper<Func<TypeValue, IValueReference>>(reader, new Func<ODataMessageReaderValueConverters.ODataReaderWithRequest, Func<TypeValue, IValueReference>>(this.ReadEntryFromLink), out nextPageUri);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000D67F8 File Offset: 0x000D49F8
		private static List<T> ReadFeedHelper<T>(ODataMessageReaderValueConverters.ODataReaderWithRequest reader, Func<ODataMessageReaderValueConverters.ODataReaderWithRequest, T> readEntry, out Uri nextPageUri)
		{
			List<T> list = new List<T>();
			reader.Read(ODataReaderState.FeedStart);
			while (reader.State == ODataReaderState.EntryStart)
			{
				list.Add(readEntry(reader));
			}
			nextPageUri = null;
			if (reader.State != ODataReaderState.Exception)
			{
				ODataFeed odataFeed = reader.Read<ODataFeed>(ODataReaderState.FeedEnd);
				nextPageUri = odataFeed.NextPageLink;
			}
			return list;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000D6848 File Offset: 0x000D4A48
		private IODataNavigationLinkWrapper ReadLink(ODataMessageReaderValueConverters.ODataReaderWithRequest reader, out List<Func<TypeValue, IValueReference>> inlineEntries, out Uri nextPageUri)
		{
			inlineEntries = null;
			nextPageUri = null;
			reader.Read(ODataReaderState.NavigationLinkStart);
			ODataReaderState state = reader.State;
			if (state != ODataReaderState.FeedStart)
			{
				if (state == ODataReaderState.EntryStart)
				{
					inlineEntries = this.ReadEntryAsListFromLink(reader);
				}
			}
			else
			{
				inlineEntries = this.ReadFeedFromLink(reader, out nextPageUri);
			}
			return new ODataNavigationLinkWrapper(reader.Read<ODataNavigationLink>(ODataReaderState.NavigationLinkEnd));
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000D6894 File Offset: 0x000D4A94
		public static List<IValueReference> CreateODataBatch(IEngineHost host, string resourceKind, ODataSettings settings, ODataMessageReader odataMessageReader, IODataResponseMessage response, Uri responseUri, Func<ODataMessageReader, ODataPayloadKind, List<IValueReference>> getEntryOrFeed, out ODataPayloadKind payloadKind, ODataEnvironment environment = null)
		{
			ODataBatchReader odataBatchReader = odataMessageReader.CreateODataBatchReader();
			odataBatchReader.Read();
			if (odataBatchReader.State != ODataBatchReaderState.Operation)
			{
				throw ODataCommonErrors.UnsupportedBatchReaderState(odataBatchReader.State.ToString(), responseUri);
			}
			List<IValueReference> list;
			using (ODataMessageReader odataMessageReader2 = new ODataMessageReader(odataBatchReader.CreateOperationResponseMessage(), ODataResponse.DefaultReaderSettings, settings.EdmModel))
			{
				ODataPayloadKindDetectionResult odataPayloadKindDetectionResult = ODataResponse.DetermineODataPayloadKind(settings, odataMessageReader2, null);
				payloadKind = odataPayloadKindDetectionResult.PayloadKind;
				switch (odataPayloadKindDetectionResult.PayloadKind)
				{
				case ODataPayloadKind.Feed:
				case ODataPayloadKind.Entry:
					list = getEntryOrFeed(odataMessageReader2, odataPayloadKindDetectionResult.PayloadKind);
					goto IL_014D;
				case ODataPayloadKind.Property:
					list = new List<IValueReference> { ODataMessageReaderValueConverters.CreatePropertyResult(TypeValue.Any, odataMessageReader2, environment) };
					goto IL_014D;
				case ODataPayloadKind.Value:
					list = new List<IValueReference> { ODataMessageReaderValueConverters.CreateRawResult(TypeValue.Any, odataMessageReader2, environment) };
					goto IL_014D;
				case ODataPayloadKind.BinaryValue:
					list = new List<IValueReference> { ODataMessageReaderValueConverters.CreateRawResult(TypeValue.Binary, odataMessageReader2, environment) };
					goto IL_014D;
				case ODataPayloadKind.Collection:
					list = ODataMessageReaderValueConverters.CreateCollectionResult(TypeValue.Any, odataMessageReader2.CreateODataCollectionReader(), environment);
					goto IL_014D;
				case ODataPayloadKind.Error:
					throw ODataMessageReaderValueConverters.CreateErrorResult(host, odataMessageReader2, responseUri, resourceKind);
				}
				throw ODataCommonErrors.UnsupportedBatchInnerPayloadKind(odataPayloadKindDetectionResult.PayloadKind.ToString(), responseUri);
			}
			IL_014D:
			odataBatchReader.Read();
			if (odataBatchReader.State != ODataBatchReaderState.Completed)
			{
				throw ODataCommonErrors.UnsupportedBatchResponseWithMoreThanOneInnerPayload(responseUri);
			}
			return list;
		}

		// Token: 0x040021BB RID: 8635
		private readonly string ErrorProperty = "Error";

		// Token: 0x040021BC RID: 8636
		private readonly ODataEnvironment environment;

		// Token: 0x040021BD RID: 8637
		private readonly TypeValue requestType;

		// Token: 0x020008C4 RID: 2244
		private sealed class ODataReaderWithRequest : ODataReader
		{
			// Token: 0x06004041 RID: 16449 RVA: 0x000D6A18 File Offset: 0x000D4C18
			public ODataReaderWithRequest(RequestInfo request, ODataReader reader)
			{
				this.request = request;
				this.reader = reader;
			}

			// Token: 0x170014B4 RID: 5300
			// (get) Token: 0x06004042 RID: 16450 RVA: 0x000D6A2E File Offset: 0x000D4C2E
			public override ODataItem Item
			{
				get
				{
					return this.reader.Item;
				}
			}

			// Token: 0x170014B5 RID: 5301
			// (get) Token: 0x06004043 RID: 16451 RVA: 0x000D6A3B File Offset: 0x000D4C3B
			public override ODataReaderState State
			{
				get
				{
					return this.reader.State;
				}
			}

			// Token: 0x170014B6 RID: 5302
			// (get) Token: 0x06004044 RID: 16452 RVA: 0x000D6A48 File Offset: 0x000D4C48
			public RequestInfo Request
			{
				get
				{
					return this.request;
				}
			}

			// Token: 0x170014B7 RID: 5303
			// (get) Token: 0x06004045 RID: 16453 RVA: 0x000D6A50 File Offset: 0x000D4C50
			// (set) Token: 0x06004046 RID: 16454 RVA: 0x000D6A58 File Offset: 0x000D4C58
			public ODataErrorException ErrorEntry { get; set; }

			// Token: 0x06004047 RID: 16455 RVA: 0x000D6A64 File Offset: 0x000D4C64
			public override bool Read()
			{
				if (this.State == ODataReaderState.Completed)
				{
					return false;
				}
				bool flag;
				try
				{
					flag = this.reader.Read();
				}
				catch (ArgumentException ex)
				{
					throw DataSourceException.NewDataSourceError(this.request.Environment.Host, ex.Message, Resource.New(this.request.Environment.Resource.Kind, this.request.Uri.AbsoluteUri), null, ex);
				}
				catch (ParseErrorException ex2)
				{
					throw DataSourceException.NewDataSourceError(this.request.Environment.Host, ex2.Message, Resource.New(this.request.Environment.Resource.Kind, this.request.Uri.AbsoluteUri), null, ex2);
				}
				catch (IOException ex3)
				{
					throw DataSourceException.NewDataSourceError(this.request.Environment.Host, ex3.Message, Resource.New(this.request.Environment.Resource.Kind, this.request.Uri.AbsoluteUri), null, ex3);
				}
				catch (ODataErrorException ex4)
				{
					this.ErrorEntry = ex4;
					flag = false;
				}
				return flag;
			}

			// Token: 0x06004048 RID: 16456 RVA: 0x000D6BB0 File Offset: 0x000D4DB0
			public void Read(ODataReaderState expected)
			{
				if (this.State != expected)
				{
					throw ODataCommonErrors.InvalidReaderState(this.State.ToString(), this.request.Uri);
				}
				this.Read();
			}

			// Token: 0x06004049 RID: 16457 RVA: 0x000D6BF4 File Offset: 0x000D4DF4
			public T Read<T>(ODataReaderState expected) where T : ODataItem
			{
				if (this.State != expected)
				{
					throw ODataCommonErrors.InvalidReaderState(this.State.ToString(), this.request.Uri);
				}
				T t = (T)((object)this.Item);
				this.Read();
				return t;
			}

			// Token: 0x040021BE RID: 8638
			private readonly RequestInfo request;

			// Token: 0x040021BF RID: 8639
			private readonly ODataReader reader;
		}
	}
}
