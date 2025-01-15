using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000045 RID: 69
	internal sealed class ODataAtomEntryMetadataDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000A27C File Offset: 0x0000847C
		internal ODataAtomEntryMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A2C0 File Offset: 0x000084C0
		private ODataAtomFeedMetadataDeserializer SourceMetadataDeserializer
		{
			get
			{
				ODataAtomFeedMetadataDeserializer odataAtomFeedMetadataDeserializer;
				if ((odataAtomFeedMetadataDeserializer = this.sourceMetadataDeserializer) == null)
				{
					odataAtomFeedMetadataDeserializer = (this.sourceMetadataDeserializer = new ODataAtomFeedMetadataDeserializer(base.AtomInputContext, true));
				}
				return odataAtomFeedMetadataDeserializer;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A2EC File Offset: 0x000084EC
		internal void ReadAtomElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			string localName;
			if (base.ReadAtomMetadata && (localName = base.XmlReader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x600028c-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
					dictionary.Add("author", 0);
					dictionary.Add("contributor", 1);
					dictionary.Add("updated", 2);
					dictionary.Add("published", 3);
					dictionary.Add("rights", 4);
					dictionary.Add("source", 5);
					dictionary.Add("summary", 6);
					dictionary.Add("title", 7);
					<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x600028c-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x600028c-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.ReadAuthorElement(entryState);
						return;
					case 1:
						this.ReadContributorElement(entryState);
						return;
					case 2:
					{
						AtomEntryMetadata atomEntryMetadata = entryState.AtomEntryMetadata;
						if (this.ShouldReadSingletonElement(atomEntryMetadata.Updated != null))
						{
							atomEntryMetadata.Updated = base.ReadAtomDateConstruct();
							return;
						}
						break;
					}
					case 3:
					{
						AtomEntryMetadata atomEntryMetadata2 = entryState.AtomEntryMetadata;
						if (this.ShouldReadSingletonElement(atomEntryMetadata2.Published != null))
						{
							atomEntryMetadata2.Published = base.ReadAtomDateConstruct();
							return;
						}
						break;
					}
					case 4:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Rights != null))
						{
							entryState.AtomEntryMetadata.Rights = base.ReadAtomTextConstruct();
							return;
						}
						break;
					case 5:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Source != null))
						{
							entryState.AtomEntryMetadata.Source = this.ReadAtomSourceInEntryContent();
							return;
						}
						break;
					case 6:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Summary != null))
						{
							entryState.AtomEntryMetadata.Summary = base.ReadAtomTextConstruct();
							return;
						}
						break;
					case 7:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Title != null))
						{
							entryState.AtomEntryMetadata.Title = base.ReadAtomTextConstruct();
							return;
						}
						break;
					}
				}
			}
			base.XmlReader.Skip();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A4F4 File Offset: 0x000086F4
		internal AtomLinkMetadata ReadAtomLinkElementInEntryContent(string relation, string hrefStringValue)
		{
			AtomLinkMetadata atomLinkMetadata = null;
			if (base.ReadAtomMetadata)
			{
				atomLinkMetadata = new AtomLinkMetadata();
				atomLinkMetadata.Relation = relation;
				if (base.ReadAtomMetadata)
				{
					atomLinkMetadata.Href = ((hrefStringValue == null) ? null : base.ProcessUriFromPayload(hrefStringValue, base.XmlReader.XmlBaseUri));
				}
				while (base.XmlReader.MoveToNextAttribute())
				{
					string localName;
					if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
					{
						if (!(localName == "type"))
						{
							if (!(localName == "hreflang"))
							{
								if (!(localName == "title"))
								{
									if (localName == "length")
									{
										if (base.ReadAtomMetadata)
										{
											string value = base.XmlReader.Value;
											int num;
											if (!int.TryParse(value, 511, NumberFormatInfo.InvariantInfo, ref num))
											{
												throw new ODataException(Strings.ODataAtomEntryMetadataDeserializer_InvalidLinkLengthValue(value));
											}
											atomLinkMetadata.Length = new int?(num);
										}
									}
								}
								else
								{
									atomLinkMetadata.Title = base.XmlReader.Value;
								}
							}
							else
							{
								atomLinkMetadata.HrefLang = base.XmlReader.Value;
							}
						}
						else
						{
							atomLinkMetadata.MediaType = base.XmlReader.Value;
						}
					}
				}
			}
			base.XmlReader.MoveToElement();
			return atomLinkMetadata;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A640 File Offset: 0x00008840
		internal void ReadAtomCategoryElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			if (base.ReadAtomMetadata)
			{
				AtomCategoryMetadata atomCategoryMetadata = this.ReadAtomCategoryElement();
				entryState.AtomEntryMetadata.AddCategory(atomCategoryMetadata);
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A674 File Offset: 0x00008874
		internal AtomCategoryMetadata ReadAtomCategoryElement()
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
							atomCategoryMetadata.Term = atomCategoryMetadata.Term ?? base.XmlReader.Value;
						}
					}
					else
					{
						atomCategoryMetadata.Scheme = atomCategoryMetadata.Scheme ?? base.XmlReader.Value;
					}
				}
			}
			base.XmlReader.Skip();
			return atomCategoryMetadata;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A744 File Offset: 0x00008944
		internal AtomFeedMetadata ReadAtomSourceInEntryContent()
		{
			AtomFeedMetadata atomFeedMetadata = AtomMetadataReaderUtils.CreateNewAtomFeedMetadata();
			if (base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				return atomFeedMetadata;
			}
			base.XmlReader.Read();
			while (base.XmlReader.NodeType != 15)
			{
				if (base.XmlReader.NodeType != 1)
				{
					base.XmlReader.Skip();
				}
				else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
				{
					this.SourceMetadataDeserializer.ReadAtomElementAsFeedMetadata(atomFeedMetadata);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			base.XmlReader.Read();
			return atomFeedMetadata;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A7E3 File Offset: 0x000089E3
		private void ReadAuthorElement(IODataAtomReaderEntryState entryState)
		{
			if (this.ShouldReadCollectionElement(Enumerable.Any<AtomPersonMetadata>(entryState.AtomEntryMetadata.Authors)))
			{
				entryState.AtomEntryMetadata.AddAuthor(base.ReadAtomPersonConstruct());
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A81A File Offset: 0x00008A1A
		private void ReadContributorElement(IODataAtomReaderEntryState entryState)
		{
			if (this.ShouldReadCollectionElement(Enumerable.Any<AtomPersonMetadata>(entryState.AtomEntryMetadata.Contributors)))
			{
				entryState.AtomEntryMetadata.AddContributor(base.ReadAtomPersonConstruct());
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A851 File Offset: 0x00008A51
		private bool ShouldReadCollectionElement(bool someAlreadyExist)
		{
			return base.ReadAtomMetadata || !someAlreadyExist;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A861 File Offset: 0x00008A61
		private bool ShouldReadSingletonElement(bool alreadyExists)
		{
			if (!alreadyExists)
			{
				return true;
			}
			if (base.ReadAtomMetadata || base.AtomInputContext.UseDefaultFormatBehavior)
			{
				throw new ODataException(Strings.ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(base.XmlReader.LocalName, "entry"));
			}
			return false;
		}

		// Token: 0x0400016D RID: 365
		private readonly string EmptyNamespace;

		// Token: 0x0400016E RID: 366
		private readonly string AtomNamespace;

		// Token: 0x0400016F RID: 367
		private ODataAtomFeedMetadataDeserializer sourceMetadataDeserializer;
	}
}
