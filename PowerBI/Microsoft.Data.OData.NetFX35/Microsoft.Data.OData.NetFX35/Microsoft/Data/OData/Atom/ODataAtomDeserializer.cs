using System;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000EB RID: 235
	internal abstract class ODataAtomDeserializer : ODataDeserializer
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x000144A4 File Offset: 0x000126A4
		protected ODataAtomDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			this.atomInputContext = atomInputContext;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000144B4 File Offset: 0x000126B4
		internal BufferingXmlReader XmlReader
		{
			get
			{
				return this.atomInputContext.XmlReader;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000144C1 File Offset: 0x000126C1
		protected ODataAtomInputContext AtomInputContext
		{
			get
			{
				return this.atomInputContext;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000144C9 File Offset: 0x000126C9
		internal void ReadPayloadStart()
		{
			this.XmlReader.ReadPayloadStart();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000144D6 File Offset: 0x000126D6
		internal void ReadPayloadEnd()
		{
			this.XmlReader.ReadPayloadEnd();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000144E3 File Offset: 0x000126E3
		internal Uri ProcessUriFromPayload(string uriFromPayload, Uri xmlBaseUri)
		{
			return this.ProcessUriFromPayload(uriFromPayload, xmlBaseUri, true);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000144F0 File Offset: 0x000126F0
		internal Uri ProcessUriFromPayload(string uriFromPayload, Uri xmlBaseUri, bool makeAbsolute)
		{
			Uri uri = xmlBaseUri;
			if (!(uri != null))
			{
				uri = base.MessageReaderSettings.BaseUri;
				uri != null;
			}
			Uri uri2 = new Uri(uriFromPayload, 0);
			Uri uri3 = this.AtomInputContext.ResolveUri(uri, uri2);
			if (uri3 != null)
			{
				return uri3;
			}
			if (!uri2.IsAbsoluteUri && makeAbsolute)
			{
				if (!(uri != null))
				{
					throw new ODataException(Strings.ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(uriFromPayload));
				}
				uri2 = UriUtils.UriToAbsoluteUri(uri, uri2);
			}
			return uri2;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00014568 File Offset: 0x00012768
		[Conditional("DEBUG")]
		internal void AssertXmlCondition(params XmlNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001456A File Offset: 0x0001276A
		[Conditional("DEBUG")]
		internal void AssertXmlCondition(bool allowEmptyElement, params XmlNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x04000269 RID: 617
		private readonly ODataAtomInputContext atomInputContext;
	}
}
