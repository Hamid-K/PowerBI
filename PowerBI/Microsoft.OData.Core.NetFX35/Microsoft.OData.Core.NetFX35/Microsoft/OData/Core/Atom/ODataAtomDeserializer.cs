using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200002B RID: 43
	internal abstract class ODataAtomDeserializer : ODataDeserializer
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00005246 File Offset: 0x00003446
		protected ODataAtomDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			this.atomInputContext = atomInputContext;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005256 File Offset: 0x00003456
		internal BufferingXmlReader XmlReader
		{
			get
			{
				return this.atomInputContext.XmlReader;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005263 File Offset: 0x00003463
		protected ODataAtomInputContext AtomInputContext
		{
			get
			{
				return this.atomInputContext;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000526B File Offset: 0x0000346B
		internal void ReadPayloadStart()
		{
			this.XmlReader.ReadPayloadStart();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005278 File Offset: 0x00003478
		internal void ReadPayloadEnd()
		{
			this.XmlReader.ReadPayloadEnd();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005285 File Offset: 0x00003485
		internal Uri ProcessUriFromPayload(string uriFromPayload, Uri xmlBaseUri)
		{
			return this.ProcessUriFromPayload(uriFromPayload, xmlBaseUri, true);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005290 File Offset: 0x00003490
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

		// Token: 0x06000191 RID: 401 RVA: 0x00005308 File Offset: 0x00003508
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs access to this in Debug only.")]
		[Conditional("DEBUG")]
		internal void AssertXmlCondition(params XmlNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000530A File Offset: 0x0000350A
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs access to this in Debug only.")]
		internal void AssertXmlCondition(bool allowEmptyElement, params XmlNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x0400010F RID: 271
		private readonly ODataAtomInputContext atomInputContext;
	}
}
