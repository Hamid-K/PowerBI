using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000227 RID: 551
	internal sealed class ODataAtomCollectionReader : ODataCollectionReaderCore
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x0003DF11 File Offset: 0x0003C111
		internal ODataAtomCollectionReader(ODataAtomInputContext atomInputContext, IEdmTypeReference expectedItemTypeReference)
			: base(atomInputContext, expectedItemTypeReference, null)
		{
			this.atomInputContext = atomInputContext;
			this.atomCollectionDeserializer = new ODataAtomCollectionDeserializer(atomInputContext);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003DF30 File Offset: 0x0003C130
		protected override bool ReadAtStartImplementation()
		{
			this.atomCollectionDeserializer.ReadPayloadStart();
			bool flag;
			ODataCollectionStart odataCollectionStart = this.atomCollectionDeserializer.ReadCollectionStart(out flag);
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart, flag);
			return true;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003DF60 File Offset: 0x0003C160
		protected override bool ReadAtCollectionStartImplementation()
		{
			this.atomCollectionDeserializer.SkipToElementInODataNamespace();
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

		// Token: 0x0600106F RID: 4207 RVA: 0x0003DFD0 File Offset: 0x0003C1D0
		protected override bool ReadAtValueImplementation()
		{
			this.atomCollectionDeserializer.SkipToElementInODataNamespace();
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

		// Token: 0x06001070 RID: 4208 RVA: 0x0003E03E File Offset: 0x0003C23E
		protected override bool ReadAtCollectionEndImplementation()
		{
			this.atomCollectionDeserializer.ReadPayloadEnd();
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x0400065B RID: 1627
		private readonly ODataAtomInputContext atomInputContext;

		// Token: 0x0400065C RID: 1628
		private readonly ODataAtomCollectionDeserializer atomCollectionDeserializer;
	}
}
