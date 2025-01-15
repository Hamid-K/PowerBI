using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000271 RID: 625
	internal sealed class ODataAtomCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x00049983 File Offset: 0x00047B83
		internal ODataAtomCollectionWriter(ODataAtomOutputContext atomOutputContext, IEdmTypeReference itemTypeReference)
			: base(atomOutputContext, itemTypeReference)
		{
			this.atomOutputContext = atomOutputContext;
			this.atomCollectionSerializer = new ODataAtomCollectionSerializer(atomOutputContext);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000499A0 File Offset: 0x00047BA0
		protected override void VerifyNotDisposed()
		{
			this.atomOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000499AD File Offset: 0x00047BAD
		protected override void FlushSynchronously()
		{
			this.atomOutputContext.Flush();
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000499BA File Offset: 0x00047BBA
		protected override void StartPayload()
		{
			this.atomCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000499C7 File Offset: 0x00047BC7
		protected override void EndPayload()
		{
			this.atomCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000499D4 File Offset: 0x00047BD4
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			string name = collectionStart.Name;
			if (name == null)
			{
				throw new ODataException(Strings.ODataAtomCollectionWriter_CollectionNameMustNotBeNull);
			}
			this.atomOutputContext.XmlWriter.WriteStartElement(name, this.atomCollectionSerializer.MessageWriterSettings.WriterBehavior.ODataNamespace);
			this.atomOutputContext.XmlWriter.WriteAttributeString("xmlns", "http://www.w3.org/2000/xmlns/", this.atomCollectionSerializer.MessageWriterSettings.WriterBehavior.ODataNamespace);
			this.atomCollectionSerializer.WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata | ODataAtomSerializer.DefaultNamespaceFlags.GeoRss | ODataAtomSerializer.DefaultNamespaceFlags.Gml);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00049A58 File Offset: 0x00047C58
		protected override void EndCollection()
		{
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00049A6C File Offset: 0x00047C6C
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			this.atomOutputContext.XmlWriter.WriteStartElement("element", this.atomCollectionSerializer.MessageWriterSettings.WriterBehavior.ODataNamespace);
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.atomOutputContext.MessageWriterSettings.WriterBehavior);
				this.atomOutputContext.XmlWriter.WriteAttributeString("null", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
			}
			else
			{
				ODataComplexValue odataComplexValue = item as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.atomCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, true, null, null, base.DuplicatePropertyNamesChecker, base.CollectionValidator, null, null, null);
					base.DuplicatePropertyNamesChecker.Clear();
				}
				else
				{
					this.atomCollectionSerializer.WritePrimitiveValue(item, base.CollectionValidator, expectedItemType, null);
				}
			}
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x0400074E RID: 1870
		private readonly ODataAtomOutputContext atomOutputContext;

		// Token: 0x0400074F RID: 1871
		private readonly ODataAtomCollectionSerializer atomCollectionSerializer;
	}
}
