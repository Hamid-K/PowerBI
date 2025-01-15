using System;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F9 RID: 505
	internal abstract class ODataAtomEpmDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x00034845 File Offset: 0x00032A45
		internal ODataAtomEpmDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00034850 File Offset: 0x00032A50
		internal bool TryReadExtensionElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataEntityPropertyMappingCache cachedEpm = entryState.CachedEpm;
			if (cachedEpm == null)
			{
				return false;
			}
			EpmTargetPathSegment nonSyndicationRoot = cachedEpm.EpmTargetTree.NonSyndicationRoot;
			return this.TryReadCustomEpmElement(entryState, nonSyndicationRoot);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000348B8 File Offset: 0x00032AB8
		private bool TryReadCustomEpmElement(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegment)
		{
			string localName = base.XmlReader.LocalName;
			string namespaceUri = base.XmlReader.NamespaceURI;
			EpmTargetPathSegment epmTargetPathSegment2 = Enumerable.FirstOrDefault<EpmTargetPathSegment>(epmTargetPathSegment.SubSegments, (EpmTargetPathSegment segment) => !segment.IsAttribute && string.CompareOrdinal(segment.SegmentName, localName) == 0 && string.CompareOrdinal(segment.SegmentNamespaceUri, namespaceUri) == 0);
			if (epmTargetPathSegment2 == null)
			{
				return false;
			}
			if (epmTargetPathSegment2.HasContent && entryState.EpmCustomReaderValueCache.Contains(epmTargetPathSegment2.EpmInfo))
			{
				return false;
			}
			while (base.XmlReader.MoveToNextAttribute())
			{
				this.ReadCustomEpmAttribute(entryState, epmTargetPathSegment2);
			}
			base.XmlReader.MoveToElement();
			if (epmTargetPathSegment2.HasContent)
			{
				string text = base.ReadElementStringValue();
				entryState.EpmCustomReaderValueCache.Add(epmTargetPathSegment2.EpmInfo, text);
			}
			else
			{
				if (!base.XmlReader.IsEmptyElement)
				{
					base.XmlReader.Read();
					while (base.XmlReader.NodeType != 15)
					{
						XmlNodeType nodeType = base.XmlReader.NodeType;
						if (nodeType != 1)
						{
							if (nodeType != 15)
							{
								base.XmlReader.Skip();
							}
						}
						else if (!this.TryReadCustomEpmElement(entryState, epmTargetPathSegment2))
						{
							base.XmlReader.Skip();
						}
					}
				}
				base.XmlReader.Read();
			}
			return true;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00034A18 File Offset: 0x00032C18
		private void ReadCustomEpmAttribute(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegmentForElement)
		{
			string localName = base.XmlReader.LocalName;
			string namespaceUri = base.XmlReader.NamespaceURI;
			EpmTargetPathSegment epmTargetPathSegment = Enumerable.FirstOrDefault<EpmTargetPathSegment>(epmTargetPathSegmentForElement.SubSegments, (EpmTargetPathSegment segment) => segment.IsAttribute && string.CompareOrdinal(segment.AttributeName, localName) == 0 && string.CompareOrdinal(segment.SegmentNamespaceUri, namespaceUri) == 0);
			if (epmTargetPathSegment != null && !entryState.EpmCustomReaderValueCache.Contains(epmTargetPathSegment.EpmInfo))
			{
				entryState.EpmCustomReaderValueCache.Add(epmTargetPathSegment.EpmInfo, base.XmlReader.Value);
			}
		}
	}
}
