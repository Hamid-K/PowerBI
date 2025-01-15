using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000097 RID: 151
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Main resource point for reader functionality")]
	public sealed class ODataMessageReader : IDisposable
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		public ODataMessageReader(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000CFE5 File Offset: 0x0000B1E5
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.container = ODataMessageReader.GetContainer<IODataRequestMessage>(requestMessage);
			this.settings = ODataMessageReaderSettings.CreateReaderSettings(this.container, settings);
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, false);
			this.readingResponse = false;
			this.message = new ODataRequestMessage(requestMessage, false, this.settings.EnableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.payloadUriConverter = requestMessage as IODataPayloadUriConverter;
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.container);
			ODataVersion odataVersion = ODataUtilsInternal.GetODataVersion(this.message, this.settings.MaxProtocolVersion);
			if (odataVersion > this.settings.MaxProtocolVersion)
			{
				throw new ODataException(Strings.ODataUtils_MaxProtocolVersionExceeded(ODataUtils.ODataVersionToString(odataVersion), ODataUtils.ODataVersionToString(this.settings.MaxProtocolVersion)));
			}
			this.model = model ?? ODataMessageReader.GetModel(this.container);
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ClientCustomTypeResolver);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000D108 File Offset: 0x0000B308
		public ODataMessageReader(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000D112 File Offset: 0x0000B312
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000D120 File Offset: 0x0000B320
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.container = ODataMessageReader.GetContainer<IODataResponseMessage>(responseMessage);
			this.settings = ODataMessageReaderSettings.CreateReaderSettings(this.container, settings);
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, true);
			this.readingResponse = true;
			this.message = new ODataResponseMessage(responseMessage, false, this.settings.EnableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.payloadUriConverter = responseMessage as IODataPayloadUriConverter;
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.container);
			ODataVersion odataVersion = ODataUtilsInternal.GetODataVersion(this.message, this.settings.MaxProtocolVersion);
			if (odataVersion > this.settings.MaxProtocolVersion)
			{
				throw new ODataException(Strings.ODataUtils_MaxProtocolVersionExceeded(ODataUtils.ODataVersionToString(odataVersion), ODataUtils.ODataVersionToString(this.settings.MaxProtocolVersion)));
			}
			this.model = model ?? ODataMessageReader.GetModel(this.container);
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ClientCustomTypeResolver);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (this.settings.ShouldIncludeAnnotation == null && !string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000D26A File Offset: 0x0000B46A
		internal ODataMessageReaderSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000D274 File Offset: 0x0000B474
		public IEnumerable<ODataPayloadKindDetectionResult> DetectPayloadKind()
		{
			IEnumerable<ODataPayloadKindDetectionResult> enumerable;
			if (this.TryGetSinglePayloadKindResultFromContentType(out enumerable))
			{
				return enumerable;
			}
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			try
			{
				IEnumerable<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerable2 = from kvp in enumerable
					group kvp by kvp.Format;
				foreach (IGrouping<ODataFormat, ODataPayloadKindDetectionResult> grouping in enumerable2)
				{
					ODataMessageInfo orCreateMessageInfo = this.GetOrCreateMessageInfo(this.message.GetStream(), false);
					IEnumerable<ODataPayloadKind> enumerable3 = grouping.Key.DetectPayloadKind(orCreateMessageInfo, this.settings);
					if (enumerable3 != null)
					{
						using (IEnumerator<ODataPayloadKind> enumerator2 = enumerable3.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ODataPayloadKind kind = enumerator2.Current;
								if (enumerable.Any((ODataPayloadKindDetectionResult pk) => pk.PayloadKind == kind))
								{
									list.Add(new ODataPayloadKindDetectionResult(kind, grouping.Key));
								}
							}
						}
					}
				}
			}
			finally
			{
				this.message.UseBufferingReadStream = new bool?(false);
				this.message.BufferingReadStream.StopBuffering();
			}
			list.Sort(new Comparison<ODataPayloadKindDetectionResult>(this.ComparePayloadKindDetectionResult));
			return list;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Need to a return a task of an enumerable.")]
		public Task<IEnumerable<ODataPayloadKindDetectionResult>> DetectPayloadKindAsync()
		{
			IEnumerable<ODataPayloadKindDetectionResult> enumerable;
			if (this.TryGetSinglePayloadKindResultFromContentType(out enumerable))
			{
				return TaskUtils.GetCompletedTask<IEnumerable<ODataPayloadKindDetectionResult>>(enumerable);
			}
			List<ODataPayloadKindDetectionResult> detectedPayloadKinds = new List<ODataPayloadKindDetectionResult>();
			return Task.Factory.Iterate(this.GetPayloadKindDetectionTasks(enumerable, detectedPayloadKinds)).FollowAlwaysWith(delegate(Task t)
			{
				this.message.UseBufferingReadStream = new bool?(false);
				this.message.BufferingReadStream.StopBuffering();
			}).FollowOnSuccessWith(delegate(Task t)
			{
				detectedPayloadKinds.Sort(new Comparison<ODataPayloadKindDetectionResult>(this.ComparePayloadKindDetectionResult));
				return detectedPayloadKinds;
			});
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000D443 File Offset: 0x0000B643
		public ODataAsynchronousReader CreateODataAsynchronousReader()
		{
			this.VerifyCanCreateODataAsynchronousReader();
			return this.ReadFromInput<ODataAsynchronousReader>((ODataInputContext context) => context.CreateAsynchronousReader(), new ODataPayloadKind[] { ODataPayloadKind.Asynchronous });
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000D47B File Offset: 0x0000B67B
		public Task<ODataAsynchronousReader> CreateODataAsynchronousReaderAsync()
		{
			this.VerifyCanCreateODataAsynchronousReader();
			return this.ReadFromInputAsync<ODataAsynchronousReader>((ODataInputContext context) => context.CreateAsynchronousReaderAsync(), new ODataPayloadKind[] { ODataPayloadKind.Asynchronous });
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000D4B3 File Offset: 0x0000B6B3
		public ODataReader CreateODataResourceSetReader()
		{
			return this.CreateODataResourceSetReader(null, null);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000D4BD File Offset: 0x0000B6BD
		public ODataReader CreateODataResourceSetReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateODataResourceSetReader(null, expectedResourceType);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public ODataReader CreateODataResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateResourceSetReader(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000D534 File Offset: 0x0000B734
		public Task<ODataReader> CreateODataResourceSetReaderAsync()
		{
			return this.CreateODataResourceSetReaderAsync(null, null);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000D53E File Offset: 0x0000B73E
		public Task<ODataReader> CreateODataResourceSetReaderAsync(IEdmStructuredType expectedResourceType)
		{
			return this.CreateODataResourceSetReaderAsync(null, expectedResourceType);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000D548 File Offset: 0x0000B748
		public Task<ODataReader> CreateODataResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInputAsync<ODataReader>((ODataInputContext context) => context.CreateResourceSetReaderAsync(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public ODataReader CreateODataDeltaResourceSetReader()
		{
			return this.CreateODataDeltaResourceSetReader(null, null);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000D5BE File Offset: 0x0000B7BE
		public ODataReader CreateODataDeltaResourceSetReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateODataDeltaResourceSetReader(null, expectedResourceType);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		public ODataReader CreateODataDeltaResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateDeltaResourceSetReader(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000D634 File Offset: 0x0000B834
		public Task<ODataReader> CreateODataDeltaResourceSetReaderAsync()
		{
			return this.CreateODataDeltaResourceSetReaderAsync(null, null);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000D63E File Offset: 0x0000B83E
		public Task<ODataReader> CreateODataDeltaResourceSetReaderAsync(IEdmStructuredType expectedResourceType)
		{
			return this.CreateODataDeltaResourceSetReaderAsync(null, expectedResourceType);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000D648 File Offset: 0x0000B848
		public Task<ODataReader> CreateODataDeltaResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInputAsync<ODataReader>((ODataInputContext context) => context.CreateDeltaResourceSetReaderAsync(entitySet, expectedResourceType), new ODataPayloadKind[] { ODataPayloadKind.Delta });
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		[Obsolete("Use CreateODataDeltaResourceSetReader.", false)]
		public ODataDeltaReader CreateODataDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataDeltaReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataDeltaReader>((ODataInputContext context) => context.CreateDeltaReader(entitySet, expectedBaseEntityType), new ODataPayloadKind[1]);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000D728 File Offset: 0x0000B928
		[Obsolete("Use CreateODataDeltaResourceSetReader.", false)]
		public Task<ODataDeltaReader> CreateODataDeltaReaderAsync(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInputAsync<ODataDeltaReader>((ODataInputContext context) => context.CreateDeltaReaderAsync(entitySet, expectedBaseEntityType), new ODataPayloadKind[1]);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000D794 File Offset: 0x0000B994
		public ODataReader CreateODataResourceReader()
		{
			return this.CreateODataResourceReader(null, null);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000D79E File Offset: 0x0000B99E
		public ODataReader CreateODataResourceReader(IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceReader(null, resourceType);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		public ODataReader CreateODataResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceReader(navigationSource, resourceType);
			resourceType = resourceType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateResourceReader(navigationSource, resourceType), new ODataPayloadKind[] { ODataPayloadKind.Resource });
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000D818 File Offset: 0x0000BA18
		public Task<ODataReader> CreateODataResourceReaderAsync()
		{
			return this.CreateODataResourceReaderAsync(null, null);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000D822 File Offset: 0x0000BA22
		public Task<ODataReader> CreateODataResourceReaderAsync(IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceReaderAsync(null, resourceType);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public Task<ODataReader> CreateODataResourceReaderAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceReader(navigationSource, resourceType);
			resourceType = resourceType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInputAsync<ODataReader>((ODataInputContext context) => context.CreateResourceReaderAsync(navigationSource, resourceType), new ODataPayloadKind[] { ODataPayloadKind.Resource });
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000D89C File Offset: 0x0000BA9C
		public ODataCollectionReader CreateODataCollectionReader()
		{
			return this.CreateODataCollectionReader(null);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public ODataCollectionReader CreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInput<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReader(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000D8EA File Offset: 0x0000BAEA
		public Task<ODataCollectionReader> CreateODataCollectionReaderAsync()
		{
			return this.CreateODataCollectionReaderAsync(null);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		public Task<ODataCollectionReader> CreateODataCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInputAsync<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReaderAsync(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000D936 File Offset: 0x0000BB36
		public ODataBatchReader CreateODataBatchReader()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInput<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReader(), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public Task<ODataBatchReader> CreateODataBatchReaderAsync()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInputAsync<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReaderAsync(), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		public ODataReader CreateODataUriParameterResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceReader(navigationSource, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateUriParameterResourceReader(navigationSource, expectedResourceType), new ODataPayloadKind[] { ODataPayloadKind.Resource });
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public Task<ODataReader> CreateODataUriParameterResourceReaderAsync(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceReader(navigationSource, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInputAsync<ODataReader>((ODataInputContext context) => context.CreateUriParameterResourceReaderAsync(navigationSource, expectedResourceType), new ODataPayloadKind[] { ODataPayloadKind.Resource });
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public ODataReader CreateODataUriParameterResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateUriParameterResourceSetReader(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public Task<ODataReader> CreateODataUriParameterResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInputAsync<ODataReader>((ODataInputContext context) => context.CreateUriParameterResourceSetReaderAsync(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public ODataParameterReader CreateODataParameterReader(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterReader(operation);
			return this.ReadFromInput<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReader(operation), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		public Task<ODataParameterReader> CreateODataParameterReaderAsync(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterReader(operation);
			return this.ReadFromInputAsync<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReaderAsync(operation), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000DBE7 File Offset: 0x0000BDE7
		public ODataServiceDocument ReadServiceDocument()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInput<ODataServiceDocument>((ODataInputContext context) => context.ReadServiceDocument(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000DC1E File Offset: 0x0000BE1E
		public Task<ODataServiceDocument> ReadServiceDocumentAsync()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInputAsync<ODataServiceDocument>((ODataInputContext context) => context.ReadServiceDocumentAsync(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000DC55 File Offset: 0x0000BE55
		public ODataProperty ReadProperty()
		{
			return this.ReadProperty(null);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000DC60 File Offset: 0x0000BE60
		public ODataProperty ReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public ODataProperty ReadProperty(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		public Task<ODataProperty> ReadPropertyAsync()
		{
			return this.ReadPropertyAsync(null);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public Task<ODataProperty> ReadPropertyAsync(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInputAsync<ODataProperty>((ODataInputContext context) => context.ReadPropertyAsync(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInputAsync<ODataProperty>((ODataInputContext context) => context.ReadPropertyAsync(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000DD76 File Offset: 0x0000BF76
		public ODataError ReadError()
		{
			this.VerifyCanReadError();
			return this.ReadFromInput<ODataError>((ODataInputContext context) => context.ReadError(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000DDAE File Offset: 0x0000BFAE
		public Task<ODataError> ReadErrorAsync()
		{
			this.VerifyCanReadError();
			return this.ReadFromInputAsync<ODataError>((ODataInputContext context) => context.ReadErrorAsync(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000DDE6 File Offset: 0x0000BFE6
		public ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			this.VerifyCanReadEntityReferenceLinks();
			return this.ReadFromInput<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinks(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000DE1D File Offset: 0x0000C01D
		public Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync()
		{
			this.VerifyCanReadEntityReferenceLinks();
			return this.ReadFromInputAsync<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinksAsync(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000DE54 File Offset: 0x0000C054
		public ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInput<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLink(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000DE8B File Offset: 0x0000C08B
		public Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync()
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInputAsync<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLinkAsync(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public object ReadValue(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInput<object>((ODataInputContext context) => context.ReadValue(expectedTypeReference.AsPrimitiveOrNull()), array);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000DF00 File Offset: 0x0000C100
		public Task<object> ReadValueAsync(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInputAsync<object>((ODataInputContext context) => context.ReadValueAsync((IEdmPrimitiveTypeReference)expectedTypeReference), array);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000DF3A File Offset: 0x0000C13A
		public IEdmModel ReadMetadataDocument()
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(null), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000DF74 File Offset: 0x0000C174
		public IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(getReferencedModelReaderFunc), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000DFB1 File Offset: 0x0000C1B1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		internal ODataFormat GetFormat()
		{
			if (this.format == null)
			{
				throw new ODataException(Strings.ODataMessageReader_GetFormatCalledBeforeReadingStarted);
			}
			return this.format;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		private static IServiceProvider GetContainer<T>(T message) where T : class
		{
			IContainerProvider containerProvider = message as IContainerProvider;
			if (containerProvider != null)
			{
				return containerProvider.Container;
			}
			return null;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000E000 File Offset: 0x0000C200
		private static IEdmModel GetModel(IServiceProvider container)
		{
			if (container != null)
			{
				return container.GetRequiredService<IEdmModel>();
			}
			return EdmCoreModel.Instance;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000E020 File Offset: 0x0000C220
		private ODataMessageInfo GetOrCreateMessageInfo(Stream messageStream, bool isAsync)
		{
			if (this.messageInfo == null)
			{
				if (this.container == null)
				{
					this.messageInfo = new ODataMessageInfo();
				}
				else
				{
					this.messageInfo = this.container.GetRequiredService<ODataMessageInfo>();
				}
				this.messageInfo.Encoding = this.encoding;
				this.messageInfo.IsResponse = this.readingResponse;
				this.messageInfo.IsAsync = isAsync;
				this.messageInfo.MediaType = this.contentType;
				this.messageInfo.Model = this.model;
				this.messageInfo.PayloadUriConverter = this.payloadUriConverter;
				this.messageInfo.Container = this.container;
				this.messageInfo.MessageStream = messageStream;
				this.messageInfo.PayloadKind = this.readerPayloadKind;
			}
			return this.messageInfo;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		private void ProcessContentType(params ODataPayloadKind[] payloadKinds)
		{
			string contentTypeHeader = this.GetContentTypeHeader(payloadKinds);
			this.format = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, payloadKinds, this.mediaTypeResolver, out this.contentType, out this.encoding, out this.readerPayloadKind);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000E130 File Offset: 0x0000C330
		private string GetContentTypeHeader(params ODataPayloadKind[] payloadKinds)
		{
			string text = this.message.GetHeader("Content-Type");
			text = ((text == null) ? null : text.Trim());
			if (string.IsNullOrEmpty(text))
			{
				if (this.GetContentLengthHeader() != 0)
				{
					throw new ODataContentTypeException(Strings.ODataMessageReader_NoneOrEmptyContentTypeHeader);
				}
				if (payloadKinds.Contains(ODataPayloadKind.Value))
				{
					text = "text/plain";
				}
				else if (payloadKinds.Contains(ODataPayloadKind.BinaryValue))
				{
					text = "application/octet-stream";
				}
				else
				{
					text = "application/json";
				}
			}
			return text;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		private int GetContentLengthHeader()
		{
			int num = 0;
			int.TryParse(this.message.GetHeader("Content-Length"), out num);
			return num;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		private void VerifyCanCreateODataResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedBaseResourceType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (expectedBaseResourceType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedBaseEntityType"), "expectedBaseEntityType");
				}
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000E218 File Offset: 0x0000C418
		private void VerifyCanCreateODataDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_DeltaInRequest);
			}
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (expectedBaseEntityType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedBaseEntityType"), "expectedBaseEntityType");
				}
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000E27C File Offset: 0x0000C47C
		private void VerifyCanCreateODataResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (navigationSource != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("navigationSource"), "navigationSource");
				}
				if (resourceType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("resourceType"), "resourceType");
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		private void VerifyCanCreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedItemTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedItemTypeReference"), "expectedItemTypeReference");
				}
				if (!expectedItemTypeReference.IsODataPrimitiveTypeKind() && expectedItemTypeReference.TypeKind() != EdmTypeKind.Complex && expectedItemTypeReference.TypeKind() != EdmTypeKind.Enum)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedCollectionTypeWrongKind(expectedItemTypeReference.TypeKind().ToString()), "expectedItemTypeReference");
				}
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000E342 File Offset: 0x0000C542
		private void VerifyCanCreateODataAsynchronousReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000E342 File Offset: 0x0000C542
		private void VerifyCanCreateODataBatchReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000E34A File Offset: 0x0000C54A
		private void VerifyCanCreateODataParameterReader(IEdmOperation operation)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ParameterPayloadInResponse);
			}
			if (operation != null && !this.model.IsUserModel())
			{
				throw new ArgumentException(Strings.ODataMessageReader_OperationSpecifiedWithoutMetadata("operation"), "operation");
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000E38A File Offset: 0x0000C58A
		private void VerifyCanReadServiceDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ServiceDocumentInRequest);
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000E3A5 File Offset: 0x0000C5A5
		private void VerifyCanReadMetadataDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_MetadataDocumentInRequest);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		private void VerifyCanReadProperty(IEdmStructuralProperty property)
		{
			if (property == null)
			{
				return;
			}
			this.VerifyCanReadProperty(property.Type);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		private void VerifyCanReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedPropertyTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedPropertyTypeReference"), "expectedPropertyTypeReference");
				}
				IEdmCollectionType edmCollectionType = expectedPropertyTypeReference.Definition as IEdmCollectionType;
				if (edmCollectionType != null && edmCollectionType.ElementType.IsODataEntityTypeKind())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind, "expectedPropertyTypeReference");
				}
				if (expectedPropertyTypeReference.IsODataEntityTypeKind())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeEntityKind, "expectedPropertyTypeReference");
				}
				if (expectedPropertyTypeReference.IsStream())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeStream, "expectedPropertyTypeReference");
				}
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000E468 File Offset: 0x0000C668
		private void VerifyCanReadError()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ErrorPayloadInRequest);
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000E342 File Offset: 0x0000C542
		private void VerifyCanReadEntityReferenceLinks()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000E342 File Offset: 0x0000C542
		private void VerifyCanReadEntityReferenceLink()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000E484 File Offset: 0x0000C684
		private ODataPayloadKind[] VerifyCanReadValue(IEdmTypeReference expectedTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedTypeReference == null)
			{
				return new ODataPayloadKind[]
				{
					ODataPayloadKind.Value,
					ODataPayloadKind.BinaryValue
				};
			}
			if (!expectedTypeReference.IsODataPrimitiveTypeKind() && !expectedTypeReference.IsODataTypeDefinitionTypeKind())
			{
				throw new ArgumentException(Strings.ODataMessageReader_ExpectedValueTypeWrongKind(expectedTypeReference.TypeKind().ToString()), "expectedTypeReference");
			}
			if (expectedTypeReference.IsBinary())
			{
				return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.Value };
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		private void VerifyReaderNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.readMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageReader_ReaderAlreadyUsed);
			}
			if (this.message.BufferingReadStream != null && this.message.BufferingReadStream.IsBuffering)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionRunning);
			}
			this.readMethodCalled = true;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000E553 File Offset: 0x0000C753
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000E570 File Offset: 0x0000C770
		private void Dispose(bool disposing)
		{
			this.isDisposed = true;
			if (disposing)
			{
				try
				{
					if (this.inputContext != null)
					{
						this.inputContext.Dispose();
					}
				}
				finally
				{
					this.inputContext = null;
				}
				if (this.settings.EnableMessageStreamDisposal && this.message.BufferingReadStream != null)
				{
					this.message.BufferingReadStream.Dispose();
				}
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		private T ReadFromInput<T>(Func<ODataInputContext, T> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			this.inputContext = this.format.CreateInputContext(this.GetOrCreateMessageInfo(this.message.GetStream(), false), this.settings);
			return readFunc(this.inputContext);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000E620 File Offset: 0x0000C820
		private bool TryGetSinglePayloadKindResultFromContentType(out IEnumerable<ODataPayloadKindDetectionResult> payloadKindResults)
		{
			if (this.message.UseBufferingReadStream == true)
			{
				throw new ODataException(Strings.ODataMessageReader_DetectPayloadKindMultipleTimes);
			}
			string contentTypeHeader = this.GetContentTypeHeader(new ODataPayloadKind[0]);
			IList<ODataPayloadKindDetectionResult> payloadKindsForContentType = MediaTypeUtils.GetPayloadKindsForContentType(contentTypeHeader, this.mediaTypeResolver, out this.contentType, out this.encoding);
			payloadKindResults = payloadKindsForContentType.Where((ODataPayloadKindDetectionResult r) => ODataUtilsInternal.IsPayloadKindSupported(r.PayloadKind, !this.readingResponse));
			if (payloadKindResults.Count<ODataPayloadKindDetectionResult>() > 1)
			{
				this.message.UseBufferingReadStream = new bool?(true);
				return false;
			}
			return true;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		private int ComparePayloadKindDetectionResult(ODataPayloadKindDetectionResult first, ODataPayloadKindDetectionResult second)
		{
			ODataPayloadKind payloadKind = first.PayloadKind;
			ODataPayloadKind payloadKind2 = second.PayloadKind;
			if (payloadKind == payloadKind2)
			{
				return 0;
			}
			if (first.PayloadKind >= second.PayloadKind)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		private IEnumerable<Task> GetPayloadKindDetectionTasks(IEnumerable<ODataPayloadKindDetectionResult> payloadKindsFromContentType, List<ODataPayloadKindDetectionResult> detectionResults)
		{
			IEnumerable<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerable = from kvp in payloadKindsFromContentType
				group kvp by kvp.Format;
			using (IEnumerator<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGrouping<ODataFormat, ODataPayloadKindDetectionResult> payloadKindGroup = enumerator.Current;
					Task<IEnumerable<ODataPayloadKind>> task = this.message.GetStreamAsync().FollowOnSuccessWithTask((Task<Stream> streamTask) => payloadKindGroup.Key.DetectPayloadKindAsync(this.GetOrCreateMessageInfo(streamTask.Result, true), this.settings));
					yield return task.FollowOnSuccessWith(delegate(Task<IEnumerable<ODataPayloadKind>> t)
					{
						IEnumerable<ODataPayloadKind> result = t.Result;
						if (result != null)
						{
							using (IEnumerator<ODataPayloadKind> enumerator2 = result.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ODataPayloadKind kind = enumerator2.Current;
									if (payloadKindsFromContentType.Any((ODataPayloadKindDetectionResult pk) => pk.PayloadKind == kind))
									{
										detectionResults.Add(new ODataPayloadKindDetectionResult(kind, payloadKindGroup.Key));
									}
								}
							}
						}
					});
				}
			}
			IEnumerator<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000E704 File Offset: 0x0000C904
		private Task<T> ReadFromInputAsync<T>(Func<ODataInputContext, Task<T>> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			return this.message.GetStreamAsync().FollowOnSuccessWithTask((Task<Stream> streamTask) => this.format.CreateInputContextAsync(this.GetOrCreateMessageInfo(streamTask.Result, true), this.settings)).FollowOnSuccessWithTask(delegate(Task<ODataInputContext> createInputContextTask)
			{
				this.inputContext = createInputContextTask.Result;
				return readFunc(this.inputContext);
			});
		}

		// Token: 0x0400024A RID: 586
		private readonly ODataMessage message;

		// Token: 0x0400024B RID: 587
		private readonly bool readingResponse;

		// Token: 0x0400024C RID: 588
		private readonly ODataMessageReaderSettings settings;

		// Token: 0x0400024D RID: 589
		private readonly IEdmModel model;

		// Token: 0x0400024E RID: 590
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x0400024F RID: 591
		private readonly IServiceProvider container;

		// Token: 0x04000250 RID: 592
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000251 RID: 593
		private readonly ODataMediaTypeResolver mediaTypeResolver;

		// Token: 0x04000252 RID: 594
		private bool readMethodCalled;

		// Token: 0x04000253 RID: 595
		private bool isDisposed;

		// Token: 0x04000254 RID: 596
		private ODataInputContext inputContext;

		// Token: 0x04000255 RID: 597
		private ODataPayloadKind readerPayloadKind;

		// Token: 0x04000256 RID: 598
		private ODataFormat format;

		// Token: 0x04000257 RID: 599
		private ODataMediaType contentType;

		// Token: 0x04000258 RID: 600
		private Encoding encoding;

		// Token: 0x04000259 RID: 601
		private ODataMessageInfo messageInfo;
	}
}
