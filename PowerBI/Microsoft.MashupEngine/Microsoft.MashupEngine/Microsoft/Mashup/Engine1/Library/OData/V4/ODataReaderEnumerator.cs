using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000887 RID: 2183
	internal sealed class ODataReaderEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06003ECA RID: 16074 RVA: 0x000CD54C File Offset: 0x000CB74C
		public ODataReaderEnumerator(ODataEnvironment environment, TypeValue requestType, Uri requestUri, IEnumerable<IValueReference> initialEntryValues)
		{
			this.environment = environment;
			this.requestType = requestType;
			this.requestUri = requestUri;
			if (initialEntryValues != null)
			{
				this.initialEntriesEnumerator = initialEntryValues.GetEnumerator();
			}
			this.isFeed = true;
			this.notFoundIsNull = false;
			this.completedReading = false;
			this.entitySet = null;
			this.getReader = new Func<GetReaderArgs, Lazy<ODataReaderWrapper>>(this.environment.GetRequestReader);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000CD5B8 File Offset: 0x000CB7B8
		public ODataReaderEnumerator(ODataEnvironment environment, TypeValue requestType, Uri requestUri, Microsoft.OData.Edm.IEdmNavigationSource entitySet, bool isFeed = true, bool notFoundIsNull = false, Lazy<ODataReaderWrapper> reader = null, Func<GetReaderArgs, Lazy<ODataReaderWrapper>> getReader = null)
		{
			this.environment = environment;
			this.requestType = requestType;
			this.requestUri = requestUri;
			this.entitySet = entitySet;
			this.isFeed = isFeed;
			this.notFoundIsNull = notFoundIsNull;
			this.getReader = getReader ?? new Func<GetReaderArgs, Lazy<ODataReaderWrapper>>(this.environment.GetRequestReader);
			this.completedReading = false;
			this.currentReader = ((reader != null) ? reader.Value : null);
			this.InitializeReader();
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000CD638 File Offset: 0x000CB838
		public static Value CreateEntryResultFromResponse(ODataEnvironment environment, HttpResponseData responseData, TypeValue entryType, Uri requestUri)
		{
			Lazy<ODataReaderWrapper> lazy = null;
			if (responseData != null)
			{
				lazy = new Lazy<ODataReaderWrapper>(() => ODataReaderWrapper.CreateFromHttpResponseData(environment.Host, responseData, requestUri, environment.Resource.Kind, false, environment.Settings.EdmModel));
			}
			using (ODataReaderEnumerator odataReaderEnumerator = new ODataReaderEnumerator(environment, entryType, requestUri, null, false, false, lazy, null))
			{
				if (odataReaderEnumerator.MoveNext())
				{
					return odataReaderEnumerator.Current.Value;
				}
			}
			return Value.Null;
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000CD6D0 File Offset: 0x000CB8D0
		public IValueReference Current
		{
			get
			{
				return this.currentEntry;
			}
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000CD6D8 File Offset: 0x000CB8D8
		public void Dispose()
		{
			this.completedReading = true;
			if (this.initialEntriesEnumerator != null)
			{
				this.initialEntriesEnumerator.Dispose();
				this.initialEntriesEnumerator = null;
			}
			if (this.currentReader != null)
			{
				this.currentReader.Dispose();
				this.currentReader = null;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000CD6D0 File Offset: 0x000CB8D0
		object IEnumerator.Current
		{
			get
			{
				return this.currentEntry;
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000091AE File Offset: 0x000073AE
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000CD718 File Offset: 0x000CB918
		public bool MoveNext()
		{
			if (this.completedReading)
			{
				return false;
			}
			if (this.initialEntriesEnumerator != null)
			{
				if (this.initialEntriesEnumerator.MoveNext())
				{
					this.currentEntry = this.initialEntriesEnumerator.Current;
					return true;
				}
				this.initialEntriesEnumerator.Dispose();
				this.initialEntriesEnumerator = null;
				if (!(this.requestUri != null))
				{
					return false;
				}
				this.currentReader = this.getReader(new GetReaderArgs
				{
					IsFeed = this.isFeed,
					Uri = this.requestUri,
					Catch404 = (!this.isFeed && this.notFoundIsNull)
				}).Value;
			}
			this.InitializeReader();
			if (this.TryReturnNullEntity())
			{
				return true;
			}
			if (this.currentReader.State == ODataReaderState.Start && !this.MoveFromStartStateToEntryStartFeedEndOrCompleteState())
			{
				return false;
			}
			while (this.currentReader.State == ODataReaderState.FeedEnd)
			{
				Uri nextPageLink = this.currentReader.Read<ODataFeed>(ODataReaderState.FeedEnd).NextPageLink;
				if (nextPageLink == null)
				{
					this.CompleteReading();
					return false;
				}
				this.currentReader.Dispose();
				this.currentReader = this.getReader(new GetReaderArgs
				{
					IsFeed = this.isFeed,
					Uri = nextPageLink,
					Catch404 = (!this.isFeed && this.notFoundIsNull)
				}).Value;
				if (!this.MoveFromStartStateToEntryStartFeedEndOrCompleteState())
				{
					this.CompleteReading();
					return false;
				}
			}
			if (this.currentReader.State == ODataReaderState.Completed)
			{
				this.CompleteReading();
				return false;
			}
			this.currentReader.VerifyState(ODataReaderState.EntryStart);
			Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> dictionary;
			ODataEntry odataEntry;
			this.ReadEntryHelper(out dictionary, out odataEntry);
			this.currentEntry = this.CreateEntryValue(this.requestType, odataEntry, this.entitySet, dictionary);
			return true;
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000CD8D4 File Offset: 0x000CBAD4
		private bool TryReturnNullEntity()
		{
			HttpStatusCode statusCode = (HttpStatusCode)this.currentReader.StatusCode;
			if (!this.isFeed && ((statusCode == HttpStatusCode.NotFound && this.notFoundIsNull) || statusCode == HttpStatusCode.NoContent))
			{
				this.currentReader.Dispose();
				this.completedReading = true;
				this.currentEntry = Value.Null;
				return true;
			}
			return false;
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000CD92D File Offset: 0x000CBB2D
		private bool MoveFromStartStateToEntryStartFeedEndOrCompleteState()
		{
			this.currentReader.Read(ODataReaderState.Start);
			if (this.currentReader.State == ODataReaderState.Completed)
			{
				this.CompleteReading();
				return false;
			}
			if (this.isFeed)
			{
				this.currentReader.Read(ODataReaderState.FeedStart);
			}
			return true;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000CD967 File Offset: 0x000CBB67
		private void CompleteReading()
		{
			this.currentReader.Read(ODataReaderState.Completed);
			this.currentReader.Dispose();
			this.completedReading = true;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000CD988 File Offset: 0x000CBB88
		private void InitializeReader()
		{
			if (this.currentReader == null)
			{
				this.currentReader = this.getReader(new GetReaderArgs
				{
					IsFeed = this.isFeed,
					Uri = this.requestUri,
					Catch404 = (!this.isFeed && this.notFoundIsNull)
				}).Value;
			}
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000CD9E8 File Offset: 0x000CBBE8
		private void ReadEntryHelper(out Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links, out ODataEntry entry)
		{
			this.currentReader.Read(ODataReaderState.EntryStart);
			links = new Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries>();
			while (this.currentReader.State == ODataReaderState.NavigationLinkStart)
			{
				List<Func<TypeValue, IValueReference>> list;
				Uri uri;
				IODataNavigationLinkWrapper iodataNavigationLinkWrapper = this.ReadLink(out list, out uri);
				links.Add(iodataNavigationLinkWrapper, new NavigationLinkWrapperInlineEntries(list, uri));
			}
			entry = this.currentReader.Read<ODataEntry>(ODataReaderState.EntryEnd);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000CDA40 File Offset: 0x000CBC40
		private IODataNavigationLinkWrapper ReadLink(out List<Func<TypeValue, IValueReference>> inlineEntries, out Uri nextPageUri)
		{
			inlineEntries = null;
			nextPageUri = null;
			this.currentReader.Read(ODataReaderState.NavigationLinkStart);
			ODataReaderState state = this.currentReader.State;
			if (state != ODataReaderState.FeedStart)
			{
				if (state == ODataReaderState.EntryStart)
				{
					inlineEntries = this.ReadEntryAsListFromLink();
				}
			}
			else
			{
				inlineEntries = this.ReadInlineFeedFromLink(out nextPageUri);
			}
			return new ODataNavigationLinkWrapper(this.currentReader.Read<ODataNavigationLink>(ODataReaderState.NavigationLinkEnd));
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000CDA99 File Offset: 0x000CBC99
		private List<Func<TypeValue, IValueReference>> ReadEntryAsListFromLink()
		{
			return new List<Func<TypeValue, IValueReference>>(1) { this.ReadEntryFromLink() };
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000CDAB0 File Offset: 0x000CBCB0
		private List<Func<TypeValue, IValueReference>> ReadInlineFeedFromLink(out Uri nextPageUri)
		{
			List<Func<TypeValue, IValueReference>> list = new List<Func<TypeValue, IValueReference>>();
			this.currentReader.Read(ODataReaderState.FeedStart);
			while (this.currentReader.State == ODataReaderState.EntryStart)
			{
				list.Add(this.ReadEntryFromLink());
			}
			ODataFeed odataFeed = this.currentReader.Read<ODataFeed>(ODataReaderState.FeedEnd);
			nextPageUri = odataFeed.NextPageLink;
			return list;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000CDB04 File Offset: 0x000CBD04
		private Func<TypeValue, IValueReference> ReadEntryFromLink()
		{
			ODataEntry entry;
			Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links;
			this.ReadEntryHelper(out links, out entry);
			if (entry != null)
			{
				return (TypeValue type) => this.CreateEntryValue(type, entry, this.entitySet, links);
			}
			return (TypeValue type) => Value.Null;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000CDB64 File Offset: 0x000CBD64
		private IValueReference CreateEntryLinkValue(NavigationLinkWrapperPropertyLookupValue lookupValue, TypeValue fieldType, int column)
		{
			RecordTypeValue elementTypeValue;
			if (fieldType.TypeKind == ValueKind.Table)
			{
				elementTypeValue = fieldType.AsTableType.ItemType.AsRecordType;
			}
			else
			{
				elementTypeValue = fieldType.AsRecordType;
			}
			bool flag = lookupValue.NavigationLinkWrapper.IsCollection();
			if (lookupValue.InlineEntries == null)
			{
				Uri navigationUrl;
				try
				{
					navigationUrl = lookupValue.NavigationLinkWrapper.Url;
				}
				catch (ODataException ex)
				{
					return new ExceptionValueReference(ODataCommonErrors.CouldNotFindNavigationUrl(lookupValue.NavigationLinkWrapper.Name, ex));
				}
				if (flag)
				{
					return ODataStructuralValueConverter.GetDelayed<ODataException>(this.environment, delegate
					{
						if (this.entitySet != null)
						{
							Microsoft.OData.Edm.IEdmNavigationProperty edmNavigationProperty = this.entitySet.EntityType().FindProperty(lookupValue.NavigationLinkWrapper.Name) as Microsoft.OData.Edm.IEdmNavigationProperty;
							if (edmNavigationProperty != null)
							{
								Microsoft.OData.Edm.IEdmNavigationSource edmNavigationSource = this.entitySet.FindNavigationTarget(edmNavigationProperty);
								if (edmNavigationSource != null)
								{
									UriBuilder uriBuilder = new UriBuilder(navigationUrl);
									string[] array = navigationUrl.AbsolutePath.Split(new char[] { '/' });
									uriBuilder.Path = string.Join('/'.ToString(), array.Take(array.Length - 1).ToArray<string>());
									try
									{
										return new QueryTableValue(new OptimizableQuery(new ODataQuery(this.environment, edmNavigationProperty, edmNavigationSource, elementTypeValue.AsRecordType, uriBuilder.Uri)), TableTypeValue.New(elementTypeValue, fieldType.AsTableType.TableKeys));
									}
									catch (NotSupportedException)
									{
									}
								}
							}
						}
						return new FeedTableValue(this.environment, navigationUrl, elementTypeValue.AsRecordType, fieldType.AsTableType.TableKeys);
					});
				}
				Lazy<ODataReaderWrapper> reader = this.getReader(new GetReaderArgs
				{
					IsFeed = false,
					Uri = navigationUrl,
					Catch404 = this.notFoundIsNull,
					Column = new int?(column)
				});
				Value value = null;
				ValueException exception = null;
				Func<int, Value> func = delegate(int index)
				{
					if (value == null)
					{
						try
						{
							using (ODataReaderEnumerator odataReaderEnumerator = new ODataReaderEnumerator(this.environment, elementTypeValue, lookupValue.NavigationLinkWrapper.Url, this.entitySet, false, this.notFoundIsNull, reader, this.getReader))
							{
								if (odataReaderEnumerator.MoveNext())
								{
									value = odataReaderEnumerator.Current.Value;
								}
								else
								{
									value = Value.Null;
								}
							}
						}
						catch (ODataException ex2)
						{
							throw ODataCommonErrors.ODataFailedToParseODataResult(this.environment.Host, ex2, this.environment.ServiceUri, this.environment.Resource.Kind);
						}
						catch (ValueException ex3)
						{
							exception = ex3;
							value = Value.Null;
						}
					}
					if (exception != null)
					{
						throw exception;
					}
					if (value.IsNull)
					{
						return Value.Null;
					}
					return value[index];
				};
				return RecordValue.New(elementTypeValue.AsRecordType, func);
			}
			if (flag)
			{
				return new FeedTableValue(this.environment, lookupValue.NavigationLinkWrapper.Url, elementTypeValue, lookupValue.InlineEntries.Select((Func<TypeValue, IValueReference> e) => e(elementTypeValue)), lookupValue.NextPage, fieldType.AsTableType.TableKeys);
			}
			IList<IValueReference> list = lookupValue.InlineEntries.Select((Func<TypeValue, IValueReference> e) => e(fieldType)).ToList<IValueReference>();
			if (list.Count == 0)
			{
				return Value.Null;
			}
			return list.Single<IValueReference>();
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x000CDD58 File Offset: 0x000CBF58
		private IValueReference CreateEntryValue(TypeValue type, ODataEntry entry, Microsoft.OData.Edm.IEdmNavigationSource entryEntitySet, Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links)
		{
			RecordValue recordValue = ODataStructuralValueConverter.CreateEntryValue<ODataException>(this.environment, type, entry.Properties.Select((ODataProperty p) => new ODataPropertyWrapper(p)), links, new Func<NavigationLinkWrapperPropertyLookupValue, TypeValue, int, IValueReference>(this.CreateEntryLinkValue), new Func<object, Value>(ODataTypeServices.MarshalFromClr), entry, entryEntitySet as Microsoft.OData.Edm.IEdmEntitySetBase);
			if (entry != null)
			{
				recordValue = BinaryOperator.AddMeta.Invoke(recordValue, RecordValue.New(ODataReaderEnumerator.EntryAnnotations, new Value[]
				{
					ODataReaderEnumerator.GetNewOrNullValue(() => entry.ETag),
					ODataReaderEnumerator.GetNewOrNullValue(() => entry.Id),
					ODataReaderEnumerator.GetNewOrNullValue(() => entry.EditLink),
					ODataReaderEnumerator.GetNewOrNullValue(() => entry.ReadLink),
					ODataReaderEnumerator.GetNewOrNullValue(() => entry.TypeName)
				})).AsRecord;
			}
			return recordValue;
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000CDE68 File Offset: 0x000CC068
		private static Value GetNewOrNullValue(Func<object> getEntryItem)
		{
			Value value;
			try
			{
				object obj = getEntryItem();
				if (obj is Uri)
				{
					Uri uri = (Uri)obj;
					value = ((uri != null) ? TextValue.New(uri.OriginalString) : Value.Null);
				}
				else if (obj is string)
				{
					value = TextValue.NewOrNull((string)obj);
				}
				else
				{
					value = Value.Null;
				}
			}
			catch (ODataException)
			{
				value = Value.Null;
			}
			return value;
		}

		// Token: 0x040020F1 RID: 8433
		private readonly TypeValue requestType;

		// Token: 0x040020F2 RID: 8434
		private readonly ODataEnvironment environment;

		// Token: 0x040020F3 RID: 8435
		private readonly bool isFeed;

		// Token: 0x040020F4 RID: 8436
		private readonly Uri requestUri;

		// Token: 0x040020F5 RID: 8437
		private readonly bool notFoundIsNull;

		// Token: 0x040020F6 RID: 8438
		private readonly Microsoft.OData.Edm.IEdmNavigationSource entitySet;

		// Token: 0x040020F7 RID: 8439
		private Func<GetReaderArgs, Lazy<ODataReaderWrapper>> getReader;

		// Token: 0x040020F8 RID: 8440
		private bool completedReading;

		// Token: 0x040020F9 RID: 8441
		private IEnumerator<IValueReference> initialEntriesEnumerator;

		// Token: 0x040020FA RID: 8442
		private ODataReaderWrapper currentReader;

		// Token: 0x040020FB RID: 8443
		private IValueReference currentEntry;

		// Token: 0x040020FC RID: 8444
		private static Keys EntryAnnotations = Keys.New(new string[] { "@odata.etag", "@odata.id", "@odata.editLink", "@odata.readLink", "@odata.type" });
	}
}
