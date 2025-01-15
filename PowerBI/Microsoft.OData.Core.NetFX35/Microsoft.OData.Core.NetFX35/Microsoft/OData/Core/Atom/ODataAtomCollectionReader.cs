using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000031 RID: 49
	internal sealed class ODataAtomCollectionReader : ODataCollectionReaderCore
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00006020 File Offset: 0x00004220
		internal ODataAtomCollectionReader(ODataAtomInputContext atomInputContext, IEdmTypeReference expectedItemTypeReference)
			: base(atomInputContext, expectedItemTypeReference, null)
		{
			this.atomInputContext = atomInputContext;
			this.atomCollectionDeserializer = new ODataAtomCollectionDeserializer(atomInputContext);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006040 File Offset: 0x00004240
		protected override bool ReadAtStartImplementation()
		{
			this.atomCollectionDeserializer.ReadPayloadStart();
			bool flag;
			ODataCollectionStart odataCollectionStart = this.atomCollectionDeserializer.ReadCollectionStart(out flag);
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart, flag);
			return true;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006070 File Offset: 0x00004270
		protected override bool ReadAtCollectionStartImplementation()
		{
			this.atomCollectionDeserializer.SkipToElementInODataMetadataNamespace();
			if (this.atomCollectionDeserializer.XmlReader.NodeType == 15 || base.IsCollectionElementEmpty)
			{
				this.atomCollectionDeserializer.ReadCollectionEnd();
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.atomCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000060E0 File Offset: 0x000042E0
		protected override bool ReadAtValueImplementation()
		{
			this.atomCollectionDeserializer.SkipToElementInODataMetadataNamespace();
			if (this.atomInputContext.XmlReader.NodeType == 15)
			{
				this.atomCollectionDeserializer.ReadCollectionEnd();
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.atomCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000614E File Offset: 0x0000434E
		protected override bool ReadAtCollectionEndImplementation()
		{
			this.atomCollectionDeserializer.ReadPayloadEnd();
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x0400011F RID: 287
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x04000120 RID: 288
		private readonly ODataAtomCollectionDeserializer atomCollectionDeserializer;
	}
}
