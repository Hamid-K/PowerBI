using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000239 RID: 569
	internal sealed class ODataJsonLightCollectionReader : ODataCollectionReaderCoreAsync
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x00046949 File Offset: 0x00044B49
		internal ODataJsonLightCollectionReader(ODataJsonLightInputContext jsonLightInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, expectedItemTypeReference, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightCollectionDeserializer = new ODataJsonLightCollectionDeserializer(jsonLightInputContext);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00046968 File Offset: 0x00044B68
		protected override bool ReadAtStartImplementation()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			this.jsonLightCollectionDeserializer.ReadPayloadStart(ODataPayloadKind.Collection, propertyAndAnnotationCollector, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0004699C File Offset: 0x00044B9C
		protected override Task<bool> ReadAtStartImplementationAsync()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			return this.jsonLightCollectionDeserializer.ReadPayloadStartAsync(ODataPayloadKind.Collection, propertyAndAnnotationCollector, base.IsReadingNestedPayload, false).FollowOnSuccessWith((Task t) => this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector));
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000469F1 File Offset: 0x00044BF1
		protected override bool ReadAtCollectionStartImplementation()
		{
			return this.ReadAtCollectionStartImplementationSynchronously();
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000469F9 File Offset: 0x00044BF9
		protected override Task<bool> ReadAtCollectionStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtCollectionStartImplementationSynchronously));
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00046A0C File Offset: 0x00044C0C
		protected override bool ReadAtValueImplementation()
		{
			return this.ReadAtValueImplementationSynchronously();
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00046A14 File Offset: 0x00044C14
		protected override Task<bool> ReadAtValueImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtValueImplementationSynchronously));
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00046A27 File Offset: 0x00044C27
		protected override bool ReadAtCollectionEndImplementation()
		{
			return this.ReadAtCollectionEndImplementationSynchronously();
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00046A2F File Offset: 0x00044C2F
		protected override Task<bool> ReadAtCollectionEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtCollectionEndImplementationSynchronously));
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00046A44 File Offset: 0x00044C44
		private bool ReadAtStartImplementationSynchronously(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			base.ExpectedItemTypeReference = ReaderValidationUtils.ValidateCollectionContextUriAndGetPayloadItemTypeReference(this.jsonLightCollectionDeserializer.ContextUriParseResult, base.ExpectedItemTypeReference);
			IEdmTypeReference edmTypeReference;
			ODataCollectionStart odataCollectionStart = this.jsonLightCollectionDeserializer.ReadCollectionStart(propertyAndAnnotationCollector, base.IsReadingNestedPayload, base.ExpectedItemTypeReference, out edmTypeReference);
			if (edmTypeReference != null)
			{
				base.ExpectedItemTypeReference = edmTypeReference;
			}
			this.jsonLightCollectionDeserializer.JsonReader.ReadStartArray();
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart);
			return true;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00046AAC File Offset: 0x00044CAC
		private bool ReadAtCollectionStartImplementationSynchronously()
		{
			if (this.jsonLightCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.jsonLightCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00046AFC File Offset: 0x00044CFC
		private bool ReadAtValueImplementationSynchronously()
		{
			if (this.jsonLightCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.jsonLightCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00046B53 File Offset: 0x00044D53
		private bool ReadAtCollectionEndImplementationSynchronously()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.jsonLightCollectionDeserializer.ReadCollectionEnd(base.IsReadingNestedPayload);
			this.jsonLightCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x04000B1A RID: 2842
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000B1B RID: 2843
		private readonly ODataJsonLightCollectionDeserializer jsonLightCollectionDeserializer;
	}
}
