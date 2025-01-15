using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200004B RID: 75
	internal sealed class ODataAtomFeedMetadataDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000B22C File Offset: 0x0000942C
		internal ODataAtomFeedMetadataDeserializer(ODataAtomInputContext atomInputContext, bool inSourceElement)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.InSourceElement = inSourceElement;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B264 File Offset: 0x00009464
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000B26C File Offset: 0x0000946C
		private bool InSourceElement { get; set; }

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B278 File Offset: 0x00009478
		internal void ReadAtomElementAsFeedMetadata(AtomFeedMetadata atomFeedMetadata)
		{
			string localName;
			if ((localName = base.XmlReader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60002aa-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(12);
					dictionary.Add("author", 0);
					dictionary.Add("category", 1);
					dictionary.Add("contributor", 2);
					dictionary.Add("generator", 3);
					dictionary.Add("icon", 4);
					dictionary.Add("id", 5);
					dictionary.Add("link", 6);
					dictionary.Add("logo", 7);
					dictionary.Add("rights", 8);
					dictionary.Add("subtitle", 9);
					dictionary.Add("title", 10);
					dictionary.Add("updated", 11);
					<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60002aa-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60002aa-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.ReadAuthorElement(atomFeedMetadata);
						return;
					case 1:
						this.ReadCategoryElement(atomFeedMetadata);
						return;
					case 2:
						this.ReadContributorElement(atomFeedMetadata);
						return;
					case 3:
						this.ReadGeneratorElement(atomFeedMetadata);
						return;
					case 4:
						this.ReadIconElement(atomFeedMetadata);
						return;
					case 5:
						if (this.InSourceElement)
						{
							this.ReadIdElementAsSourceId(atomFeedMetadata);
							return;
						}
						base.XmlReader.Skip();
						return;
					case 6:
						this.ReadLinkElementIntoLinksCollection(atomFeedMetadata);
						return;
					case 7:
						this.ReadLogoElement(atomFeedMetadata);
						return;
					case 8:
						this.ReadRightsElement(atomFeedMetadata);
						return;
					case 9:
						this.ReadSubtitleElement(atomFeedMetadata);
						return;
					case 10:
						this.ReadTitleElement(atomFeedMetadata);
						return;
					case 11:
						this.ReadUpdatedElement(atomFeedMetadata);
						return;
					}
				}
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B410 File Offset: 0x00009610
		internal AtomLinkMetadata ReadAtomLinkElementInFeed(string relation, string hrefStringValue)
		{
			AtomLinkMetadata atomLinkMetadata = new AtomLinkMetadata
			{
				Relation = relation,
				Href = ((hrefStringValue == null) ? null : base.ProcessUriFromPayload(hrefStringValue, base.XmlReader.XmlBaseUri))
			};
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
				{
					if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60002ab-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("type", 0);
						dictionary.Add("hreflang", 1);
						dictionary.Add("title", 2);
						dictionary.Add("length", 3);
						dictionary.Add("rel", 4);
						dictionary.Add("href", 5);
						<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60002ab-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60002ab-1.TryGetValue(localName, ref num))
					{
						switch (num)
						{
						case 0:
							atomLinkMetadata.MediaType = base.XmlReader.Value;
							break;
						case 1:
							atomLinkMetadata.HrefLang = base.XmlReader.Value;
							break;
						case 2:
							atomLinkMetadata.Title = base.XmlReader.Value;
							break;
						case 3:
						{
							string value = base.XmlReader.Value;
							int num2;
							if (!int.TryParse(value, 511, NumberFormatInfo.InvariantInfo, ref num2))
							{
								throw new ODataException(Strings.ODataAtomEntryMetadataDeserializer_InvalidLinkLengthValue(value));
							}
							atomLinkMetadata.Length = new int?(num2);
							break;
						}
						case 4:
							if (atomLinkMetadata.Relation == null)
							{
								atomLinkMetadata.Relation = base.XmlReader.Value;
							}
							break;
						case 5:
							if (atomLinkMetadata.Href == null)
							{
								atomLinkMetadata.Href = base.ProcessUriFromPayload(base.XmlReader.Value, base.XmlReader.XmlBaseUri);
							}
							break;
						}
					}
				}
			}
			base.XmlReader.Skip();
			return atomLinkMetadata;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B5F2 File Offset: 0x000097F2
		private void ReadAuthorElement(AtomFeedMetadata atomFeedMetadata)
		{
			atomFeedMetadata.AddAuthor(base.ReadAtomPersonConstruct());
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B600 File Offset: 0x00009800
		private void ReadCategoryElement(AtomFeedMetadata atomFeedMetadata)
		{
			AtomCategoryMetadata atomCategoryMetadata = new AtomCategoryMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
				{
					if (!(localName == "scheme"))
					{
						if (!(localName == "term"))
						{
							if (localName == "label")
							{
								atomCategoryMetadata.Label = base.XmlReader.Value;
							}
						}
						else
						{
							atomCategoryMetadata.Term = base.XmlReader.Value;
						}
					}
					else
					{
						atomCategoryMetadata.Scheme = base.XmlReader.Value;
					}
				}
			}
			atomFeedMetadata.AddCategory(atomCategoryMetadata);
			base.XmlReader.Skip();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B6BC File Offset: 0x000098BC
		private void ReadContributorElement(AtomFeedMetadata atomFeedMetadata)
		{
			atomFeedMetadata.AddContributor(base.ReadAtomPersonConstruct());
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B6CC File Offset: 0x000098CC
		private void ReadGeneratorElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Generator);
			AtomGeneratorMetadata atomGeneratorMetadata = new AtomGeneratorMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
				{
					if (!(localName == "uri"))
					{
						if (localName == "version")
						{
							atomGeneratorMetadata.Version = base.XmlReader.Value;
						}
					}
					else
					{
						atomGeneratorMetadata.Uri = base.ProcessUriFromPayload(base.XmlReader.Value, base.XmlReader.XmlBaseUri);
					}
				}
			}
			base.XmlReader.MoveToElement();
			if (base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Skip();
			}
			else
			{
				atomGeneratorMetadata.Name = base.XmlReader.ReadElementValue();
			}
			atomFeedMetadata.Generator = atomGeneratorMetadata;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B7AB File Offset: 0x000099AB
		private void ReadIconElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Icon);
			atomFeedMetadata.Icon = this.ReadUriValuedElement();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B7C8 File Offset: 0x000099C8
		private void ReadIdElementAsSourceId(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.SourceId);
			string text = base.XmlReader.ReadElementValue();
			atomFeedMetadata.SourceId = UriUtils.CreateUriAsEntryOrFeedId(text, 1, true);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B7FC File Offset: 0x000099FC
		private void ReadLinkElementIntoLinksCollection(AtomFeedMetadata atomFeedMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = this.ReadAtomLinkElementInFeed(null, null);
			atomFeedMetadata.AddLink(atomLinkMetadata);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B819 File Offset: 0x00009A19
		private void ReadLogoElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Logo);
			atomFeedMetadata.Logo = this.ReadUriValuedElement();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B833 File Offset: 0x00009A33
		private void ReadRightsElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Rights);
			atomFeedMetadata.Rights = base.ReadAtomTextConstruct();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B84D File Offset: 0x00009A4D
		private void ReadSubtitleElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Subtitle);
			atomFeedMetadata.Subtitle = base.ReadAtomTextConstruct();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B867 File Offset: 0x00009A67
		private void ReadTitleElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Title);
			atomFeedMetadata.Title = base.ReadAtomTextConstruct();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B881 File Offset: 0x00009A81
		private void ReadUpdatedElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Updated);
			atomFeedMetadata.Updated = base.ReadAtomDateConstruct();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		private Uri ReadUriValuedElement()
		{
			string text = base.XmlReader.ReadElementValue();
			return base.ProcessUriFromPayload(text, base.XmlReader.XmlBaseUri);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B8CC File Offset: 0x00009ACC
		private void VerifyNotPreviouslyDefined(object metadataValue)
		{
			if (metadataValue != null)
			{
				string text = (this.InSourceElement ? "source" : "feed");
				throw new ODataException(Strings.ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(base.XmlReader.LocalName, text));
			}
		}

		// Token: 0x0400017C RID: 380
		private readonly string EmptyNamespace;
	}
}
