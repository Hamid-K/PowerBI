using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021D RID: 541
	internal sealed class ODataAtomEntryMetadataDeserializer : ODataAtomEpmDeserializer
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		internal ODataAtomEntryMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003C120 File Offset: 0x0003A320
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

		// Token: 0x06001005 RID: 4101 RVA: 0x0003C14C File Offset: 0x0003A34C
		internal void ReadAtomElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataEntityPropertyMappingCache cachedEpm = entryState.CachedEpm;
			EpmTargetPathSegment epmTargetPathSegment = null;
			if (cachedEpm != null)
			{
				epmTargetPathSegment = cachedEpm.EpmTargetTree.SyndicationRoot;
			}
			EpmTargetPathSegment epmTargetPathSegment2;
			string localName;
			if (base.ShouldReadElement(epmTargetPathSegment, base.XmlReader.LocalName, out epmTargetPathSegment2) && (localName = base.XmlReader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000f8f-1 == null)
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
					<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000f8f-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6000f8f-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.ReadAuthorElement(entryState, epmTargetPathSegment2);
						return;
					case 1:
						this.ReadContributorElement(entryState, epmTargetPathSegment2);
						return;
					case 2:
					{
						AtomEntryMetadata atomEntryMetadata = entryState.AtomEntryMetadata;
						if (base.UseClientFormatBehavior)
						{
							if (this.ShouldReadSingletonElement(atomEntryMetadata.UpdatedString != null))
							{
								atomEntryMetadata.UpdatedString = base.ReadAtomDateConstructAsString();
								return;
							}
						}
						else if (this.ShouldReadSingletonElement(atomEntryMetadata.Updated != null))
						{
							atomEntryMetadata.Updated = base.ReadAtomDateConstruct();
							return;
						}
						break;
					}
					case 3:
					{
						AtomEntryMetadata atomEntryMetadata2 = entryState.AtomEntryMetadata;
						if (base.UseClientFormatBehavior)
						{
							if (this.ShouldReadSingletonElement(atomEntryMetadata2.PublishedString != null))
							{
								atomEntryMetadata2.PublishedString = base.ReadAtomDateConstructAsString();
								return;
							}
						}
						else if (this.ShouldReadSingletonElement(atomEntryMetadata2.Published != null))
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

		// Token: 0x06001006 RID: 4102 RVA: 0x0003C3DC File Offset: 0x0003A5DC
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
												throw new ODataException(Strings.EpmSyndicationWriter_InvalidLinkLengthValue(value));
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

		// Token: 0x06001007 RID: 4103 RVA: 0x0003C53C File Offset: 0x0003A73C
		internal void ReadAtomCategoryElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataEntityPropertyMappingCache cachedEpm = entryState.CachedEpm;
			EpmTargetPathSegment epmTargetPathSegment = null;
			if (cachedEpm != null)
			{
				epmTargetPathSegment = cachedEpm.EpmTargetTree.SyndicationRoot;
			}
			bool flag;
			if (epmTargetPathSegment != null)
			{
				flag = Enumerable.Any<EpmTargetPathSegment>(epmTargetPathSegment.SubSegments, (EpmTargetPathSegment segment) => string.CompareOrdinal(segment.SegmentName, "category") == 0);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (base.ReadAtomMetadata || flag2)
			{
				AtomCategoryMetadata atomCategoryMetadata = this.ReadAtomCategoryElement();
				entryState.AtomEntryMetadata.AddCategory(atomCategoryMetadata);
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003C5BC File Offset: 0x0003A7BC
		internal AtomCategoryMetadata ReadAtomCategoryElement()
		{
			AtomCategoryMetadata atomCategoryMetadata = new AtomCategoryMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName2;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					string localName;
					if ((localName = base.XmlReader.LocalName) != null)
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
				else if (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.AtomNamespace) && (localName2 = base.XmlReader.LocalName) != null)
				{
					if (!(localName2 == "scheme"))
					{
						if (localName2 == "term")
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
			base.XmlReader.Skip();
			return atomCategoryMetadata;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003C700 File Offset: 0x0003A900
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

		// Token: 0x0600100A RID: 4106 RVA: 0x0003C79F File Offset: 0x0003A99F
		private void ReadAuthorElement(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegment)
		{
			if (this.ShouldReadCollectionElement(Enumerable.Any<AtomPersonMetadata>(entryState.AtomEntryMetadata.Authors)))
			{
				entryState.AtomEntryMetadata.AddAuthor(base.ReadAtomPersonConstruct(epmTargetPathSegment));
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003C7D7 File Offset: 0x0003A9D7
		private void ReadContributorElement(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegment)
		{
			if (this.ShouldReadCollectionElement(Enumerable.Any<AtomPersonMetadata>(entryState.AtomEntryMetadata.Contributors)))
			{
				entryState.AtomEntryMetadata.AddContributor(base.ReadAtomPersonConstruct(epmTargetPathSegment));
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003C80F File Offset: 0x0003AA0F
		private bool ShouldReadCollectionElement(bool someAlreadyExist)
		{
			return base.ReadAtomMetadata || !someAlreadyExist;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003C81F File Offset: 0x0003AA1F
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

		// Token: 0x04000625 RID: 1573
		private readonly string EmptyNamespace;

		// Token: 0x04000626 RID: 1574
		private readonly string AtomNamespace;

		// Token: 0x04000627 RID: 1575
		private ODataAtomFeedMetadataDeserializer sourceMetadataDeserializer;
	}
}
