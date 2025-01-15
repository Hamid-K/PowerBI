using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x0200010A RID: 266
	internal abstract class ODataMaterializer : IDisposable
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x0002AD44 File Offset: 0x00028F44
		protected ODataMaterializer(IODataMaterializerContext materializerContext, Type expectedType)
		{
			this.ExpectedType = expectedType;
			this.MaterializerContext = materializerContext;
			this.nextLinkTable = new Dictionary<IEnumerable, DataServiceQueryContinuation>(ReferenceEqualityComparer<IEnumerable>.Instance);
			this.enumValueMaterializationPolicy = new EnumValueMaterializationPolicy(this.MaterializerContext);
			this.lazyPrimitivePropertyConverter = new SimpleLazy<PrimitivePropertyConverter>(() => new PrimitivePropertyConverter());
			this.primitiveValueMaterializationPolicy = new PrimitiveValueMaterializationPolicy(this.MaterializerContext, this.lazyPrimitivePropertyConverter);
			this.collectionValueMaterializationPolicy = new CollectionValueMaterializationPolicy(this.MaterializerContext, this.primitiveValueMaterializationPolicy);
			this.instanceAnnotationMaterializationPolicy = new InstanceAnnotationMaterializationPolicy(this.MaterializerContext);
			this.collectionValueMaterializationPolicy.InstanceAnnotationMaterializationPolicy = this.instanceAnnotationMaterializationPolicy;
			this.instanceAnnotationMaterializationPolicy.CollectionValueMaterializationPolicy = this.collectionValueMaterializationPolicy;
			this.instanceAnnotationMaterializationPolicy.EnumValueMaterializationPolicy = this.enumValueMaterializationPolicy;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B3D RID: 2877
		internal abstract object CurrentValue { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B3E RID: 2878
		internal abstract ODataResourceSet CurrentFeed { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B3F RID: 2879
		internal abstract ODataResource CurrentEntry { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002AE22 File Offset: 0x00029022
		internal Dictionary<IEnumerable, DataServiceQueryContinuation> NextLinkTable
		{
			get
			{
				return this.nextLinkTable;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B41 RID: 2881
		internal abstract bool IsEndOfStream { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00015066 File Offset: 0x00013266
		internal virtual bool IsCountable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002AE2A File Offset: 0x0002902A
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0002AE32 File Offset: 0x00029032
		internal Action<IDictionary<string, object>> SetInstanceAnnotations { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B45 RID: 2885
		internal abstract long CountValue { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B46 RID: 2886
		internal abstract ProjectionPlan MaterializeEntryPlan { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002AE3B File Offset: 0x0002903B
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0002AE43 File Offset: 0x00029043
		protected internal IODataMaterializerContext MaterializerContext { get; private set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B49 RID: 2889
		protected abstract bool IsDisposed { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0002AE4C File Offset: 0x0002904C
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0002AE54 File Offset: 0x00029054
		private protected Type ExpectedType { protected get; private set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002AE5D File Offset: 0x0002905D
		protected CollectionValueMaterializationPolicy CollectionValueMaterializationPolicy
		{
			get
			{
				return this.collectionValueMaterializationPolicy;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002AE65 File Offset: 0x00029065
		protected EnumValueMaterializationPolicy EnumValueMaterializationPolicy
		{
			get
			{
				return this.enumValueMaterializationPolicy;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002AE6D File Offset: 0x0002906D
		protected InstanceAnnotationMaterializationPolicy InstanceAnnotationMaterializationPolicy
		{
			get
			{
				return this.instanceAnnotationMaterializationPolicy;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002AE75 File Offset: 0x00029075
		protected PrimitivePropertyConverter PrimitivePropertyConverter
		{
			get
			{
				return this.lazyPrimitivePropertyConverter.Value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002AE82 File Offset: 0x00029082
		protected PrimitiveValueMaterializationPolicy PrimitiveValueMaterializier
		{
			get
			{
				return this.primitiveValueMaterializationPolicy;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B51 RID: 2897
		protected abstract ODataFormat Format { get; }

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002AE8C File Offset: 0x0002908C
		public static ODataMaterializer CreateMaterializerForMessage(IODataResponseMessage responseMessage, ResponseInfo responseInfo, Type materializerType, QueryComponents queryComponents, ProjectionPlan plan, ODataPayloadKind payloadKind)
		{
			ODataMessageReader odataMessageReader = ODataMaterializer.CreateODataMessageReader(responseMessage, responseInfo, ref payloadKind);
			IEdmType edmType = null;
			ODataMaterializer odataMaterializer2;
			try
			{
				ODataMaterializerContext odataMaterializerContext = new ODataMaterializerContext(responseInfo);
				if (materializerType != typeof(object))
				{
					edmType = responseInfo.TypeResolver.ResolveExpectedTypeForReading(materializerType);
				}
				if (payloadKind == ODataPayloadKind.Property && edmType != null)
				{
					if (edmType.TypeKind.IsStructured())
					{
						payloadKind = ODataPayloadKind.Resource;
					}
					else if (edmType.TypeKind == EdmTypeKind.Collection && (edmType as IEdmCollectionType).ElementType.IsStructured())
					{
						payloadKind = ODataPayloadKind.ResourceSet;
					}
				}
				ODataMaterializer odataMaterializer;
				if (payloadKind != ODataPayloadKind.Resource && payloadKind != ODataPayloadKind.ResourceSet)
				{
					switch (payloadKind)
					{
					case ODataPayloadKind.Property:
					case ODataPayloadKind.IndividualProperty:
						if (edmType != null && (edmType.TypeKind == EdmTypeKind.Entity || edmType.TypeKind == EdmTypeKind.Complex))
						{
							throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidEntityType(materializerType.FullName));
						}
						odataMaterializer = new ODataPropertyMaterializer(odataMessageReader, odataMaterializerContext, materializerType, queryComponents.SingleResult);
						goto IL_01D1;
					case ODataPayloadKind.EntityReferenceLink:
					case ODataPayloadKind.EntityReferenceLinks:
						odataMaterializer = new ODataLinksMaterializer(odataMessageReader, odataMaterializerContext, materializerType, queryComponents.SingleResult);
						goto IL_01D1;
					case ODataPayloadKind.Value:
						odataMaterializer = new ODataValueMaterializer(odataMessageReader, odataMaterializerContext, materializerType, queryComponents.SingleResult);
						goto IL_01D1;
					case ODataPayloadKind.Collection:
						odataMaterializer = new ODataCollectionMaterializer(odataMessageReader, odataMaterializerContext, materializerType, queryComponents.SingleResult);
						goto IL_01D1;
					case ODataPayloadKind.Error:
					{
						ODataError odataError = odataMessageReader.ReadError();
						throw new ODataErrorException(odataError.Message, odataError);
					}
					}
					throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidResponsePayload("http://docs.oasis-open.org/odata/ns/data"));
				}
				if (edmType != null && !edmType.TypeKind.IsStructured())
				{
					throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidNonEntityType(materializerType.FullName));
				}
				ODataReaderWrapper odataReaderWrapper = ODataReaderWrapper.Create(odataMessageReader, payloadKind, edmType, responseInfo.ResponsePipeline);
				EntityTrackingAdapter entityTrackingAdapter = new EntityTrackingAdapter(responseInfo.EntityTracker, responseInfo.MergeOption, responseInfo.Model, responseInfo.Context);
				LoadPropertyResponseInfo loadPropertyResponseInfo = responseInfo as LoadPropertyResponseInfo;
				if (loadPropertyResponseInfo != null)
				{
					odataMaterializer = new ODataLoadNavigationPropertyMaterializer(odataMessageReader, odataReaderWrapper, odataMaterializerContext, entityTrackingAdapter, queryComponents, materializerType, plan, loadPropertyResponseInfo);
				}
				else
				{
					odataMaterializer = new ODataReaderEntityMaterializer(odataMessageReader, odataReaderWrapper, odataMaterializerContext, entityTrackingAdapter, queryComponents, materializerType, plan);
				}
				IL_01D1:
				odataMaterializer2 = odataMaterializer;
			}
			catch (Exception ex)
			{
				if (CommonUtil.IsCatchableExceptionType(ex))
				{
					odataMessageReader.Dispose();
				}
				throw;
			}
			return odataMaterializer2;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public bool Read()
		{
			this.VerifyNotDisposed();
			return this.ReadImplementation();
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002B0AE File Offset: 0x000292AE
		public void Dispose()
		{
			this.OnDispose();
		}

		// Token: 0x06000B55 RID: 2901
		internal abstract void ClearLog();

		// Token: 0x06000B56 RID: 2902
		internal abstract void ApplyLogToContext();

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002B0B8 File Offset: 0x000292B8
		protected static ODataMessageReader CreateODataMessageReader(IODataResponseMessage responseMessage, ResponseInfo responseInfo, ref ODataPayloadKind payloadKind)
		{
			ODataMessageReaderSettings odataMessageReaderSettings = responseInfo.ReadHelper.CreateSettings();
			ODataMessageReader odataMessageReader = responseInfo.ReadHelper.CreateReader(responseMessage, odataMessageReaderSettings);
			if (payloadKind == ODataPayloadKind.Unsupported)
			{
				List<ODataPayloadKindDetectionResult> list = odataMessageReader.DetectPayloadKind().ToList<ODataPayloadKindDetectionResult>();
				if (list.Count == 0)
				{
					throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidResponsePayload("http://docs.oasis-open.org/odata/ns/data"));
				}
				ODataPayloadKindDetectionResult odataPayloadKindDetectionResult = list.FirstOrDefault((ODataPayloadKindDetectionResult k) => k.PayloadKind == ODataPayloadKind.EntityReferenceLink || k.PayloadKind == ODataPayloadKind.EntityReferenceLinks);
				if (odataPayloadKindDetectionResult == null)
				{
					odataPayloadKindDetectionResult = list.First<ODataPayloadKindDetectionResult>();
				}
				if (odataPayloadKindDetectionResult.Format != ODataFormat.Json && odataPayloadKindDetectionResult.Format != ODataFormat.RawValue)
				{
					throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidContentTypeEncountered(responseMessage.GetHeader("Content-Type")));
				}
				payloadKind = odataPayloadKindDetectionResult.PayloadKind;
			}
			return odataMessageReader;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002B178 File Offset: 0x00029378
		protected void VerifyNotDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(typeof(ODataEntityMaterializer).FullName);
			}
		}

		// Token: 0x06000B59 RID: 2905
		protected abstract bool ReadImplementation();

		// Token: 0x06000B5A RID: 2906
		protected abstract void OnDispose();

		// Token: 0x04000631 RID: 1585
		internal static readonly ODataNestedResourceInfo[] EmptyLinks = new ODataNestedResourceInfo[0];

		// Token: 0x04000632 RID: 1586
		protected static readonly ODataProperty[] EmptyProperties = new ODataProperty[0];

		// Token: 0x04000633 RID: 1587
		protected Dictionary<IEnumerable, DataServiceQueryContinuation> nextLinkTable;

		// Token: 0x04000634 RID: 1588
		private readonly CollectionValueMaterializationPolicy collectionValueMaterializationPolicy;

		// Token: 0x04000635 RID: 1589
		private readonly EnumValueMaterializationPolicy enumValueMaterializationPolicy;

		// Token: 0x04000636 RID: 1590
		private readonly InstanceAnnotationMaterializationPolicy instanceAnnotationMaterializationPolicy;

		// Token: 0x04000637 RID: 1591
		private readonly PrimitiveValueMaterializationPolicy primitiveValueMaterializationPolicy;

		// Token: 0x04000638 RID: 1592
		private SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter;
	}
}
