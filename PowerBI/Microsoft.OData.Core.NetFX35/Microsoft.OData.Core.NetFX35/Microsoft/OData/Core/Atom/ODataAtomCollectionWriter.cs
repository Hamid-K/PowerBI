using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200003C RID: 60
	internal sealed class ODataAtomCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x0600022D RID: 557 RVA: 0x000075F2 File Offset: 0x000057F2
		internal ODataAtomCollectionWriter(ODataAtomOutputContext atomOutputContext, IEdmTypeReference itemTypeReference)
			: base(atomOutputContext, itemTypeReference)
		{
			this.atomOutputContext = atomOutputContext;
			this.atomCollectionSerializer = new ODataAtomCollectionSerializer(atomOutputContext);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000760F File Offset: 0x0000580F
		protected override void VerifyNotDisposed()
		{
			this.atomOutputContext.VerifyNotDisposed();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000761C File Offset: 0x0000581C
		protected override void FlushSynchronously()
		{
			this.atomOutputContext.Flush();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007629 File Offset: 0x00005829
		protected override void StartPayload()
		{
			this.atomCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007636 File Offset: 0x00005836
		protected override void EndPayload()
		{
			this.atomCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007643 File Offset: 0x00005843
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.atomCollectionSerializer.WriteCollectionStart();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007650 File Offset: 0x00005850
		protected override void EndCollection()
		{
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007664 File Offset: 0x00005864
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			this.atomOutputContext.XmlWriter.WriteStartElement("element", "http://docs.oasis-open.org/odata/ns/metadata");
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.atomOutputContext.MessageWriterSettings.WriterBehavior);
				this.atomOutputContext.XmlWriter.WriteAttributeString("null", "http://docs.oasis-open.org/odata/ns/metadata", "true");
			}
			else
			{
				ODataComplexValue odataComplexValue = item as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.atomCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, true, null, null, base.DuplicatePropertyNamesChecker, base.CollectionValidator, null);
					base.DuplicatePropertyNamesChecker.Clear();
				}
				else
				{
					this.atomCollectionSerializer.WritePrimitiveValue(item, base.CollectionValidator, expectedItemType, null);
				}
			}
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x0400013B RID: 315
		private readonly ODataAtomOutputContext atomOutputContext;

		// Token: 0x0400013C RID: 316
		private readonly ODataAtomCollectionSerializer atomCollectionSerializer;
	}
}
