using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001E2 RID: 482
	internal sealed class ODataAtomFeedMetadataDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x000324B0 File Offset: 0x000306B0
		internal ODataAtomFeedMetadataDeserializer(ODataAtomInputContext atomInputContext, bool inSourceElement)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.InSourceElement = inSourceElement;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000324E8 File Offset: 0x000306E8
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x000324F0 File Offset: 0x000306F0
		private bool InSourceElement { get; set; }

		// Token: 0x06000E17 RID: 3607 RVA: 0x000324FC File Offset: 0x000306FC
		internal void ReadAtomElementAsFeedMetadata(AtomFeedMetadata atomFeedMetadata)
		{
			string localName;
			if ((localName = base.XmlReader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000da8-1 == null)
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
					<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000da8-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000da8-1.TryGetValue(localName, ref num))
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

		// Token: 0x06000E18 RID: 3608 RVA: 0x00032694 File Offset: 0x00030894
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
					if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000da9-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("type", 0);
						dictionary.Add("hreflang", 1);
						dictionary.Add("title", 2);
						dictionary.Add("length", 3);
						dictionary.Add("rel", 4);
						dictionary.Add("href", 5);
						<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000da9-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000da9-1.TryGetValue(localName, ref num))
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
								throw new ODataException(Strings.EpmSyndicationWriter_InvalidLinkLengthValue(value));
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

		// Token: 0x06000E19 RID: 3609 RVA: 0x00032876 File Offset: 0x00030A76
		private void ReadAuthorElement(AtomFeedMetadata atomFeedMetadata)
		{
			atomFeedMetadata.AddAuthor(base.ReadAtomPersonConstruct(null));
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00032888 File Offset: 0x00030A88
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

		// Token: 0x06000E1B RID: 3611 RVA: 0x00032944 File Offset: 0x00030B44
		private void ReadContributorElement(AtomFeedMetadata atomFeedMetadata)
		{
			atomFeedMetadata.AddContributor(base.ReadAtomPersonConstruct(null));
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00032954 File Offset: 0x00030B54
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

		// Token: 0x06000E1D RID: 3613 RVA: 0x00032A33 File Offset: 0x00030C33
		private void ReadIconElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Icon);
			atomFeedMetadata.Icon = this.ReadUriValuedElement();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00032A4D File Offset: 0x00030C4D
		private void ReadIdElementAsSourceId(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.SourceId);
			atomFeedMetadata.SourceId = base.XmlReader.ReadElementValue();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00032A6C File Offset: 0x00030C6C
		private void ReadLinkElementIntoLinksCollection(AtomFeedMetadata atomFeedMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = this.ReadAtomLinkElementInFeed(null, null);
			atomFeedMetadata.AddLink(atomLinkMetadata);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00032A89 File Offset: 0x00030C89
		private void ReadLogoElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Logo);
			atomFeedMetadata.Logo = this.ReadUriValuedElement();
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00032AA3 File Offset: 0x00030CA3
		private void ReadRightsElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Rights);
			atomFeedMetadata.Rights = base.ReadAtomTextConstruct();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00032ABD File Offset: 0x00030CBD
		private void ReadSubtitleElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Subtitle);
			atomFeedMetadata.Subtitle = base.ReadAtomTextConstruct();
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00032AD7 File Offset: 0x00030CD7
		private void ReadTitleElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Title);
			atomFeedMetadata.Title = base.ReadAtomTextConstruct();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00032AF1 File Offset: 0x00030CF1
		private void ReadUpdatedElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Updated);
			atomFeedMetadata.Updated = base.ReadAtomDateConstruct();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00032B10 File Offset: 0x00030D10
		private Uri ReadUriValuedElement()
		{
			string text = base.XmlReader.ReadElementValue();
			return base.ProcessUriFromPayload(text, base.XmlReader.XmlBaseUri);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00032B3C File Offset: 0x00030D3C
		private void VerifyNotPreviouslyDefined(object metadataValue)
		{
			if (metadataValue != null)
			{
				string text = (this.InSourceElement ? "source" : "feed");
				throw new ODataException(Strings.ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(base.XmlReader.LocalName, text));
			}
		}

		// Token: 0x04000522 RID: 1314
		private readonly string EmptyNamespace;
	}
}
