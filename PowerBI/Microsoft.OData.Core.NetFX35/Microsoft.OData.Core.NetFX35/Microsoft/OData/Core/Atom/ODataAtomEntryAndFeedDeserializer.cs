using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000042 RID: 66
	internal sealed class ODataAtomEntryAndFeedDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00007D3C File Offset: 0x00005F3C
		internal ODataAtomEntryAndFeedDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
			this.AtomEntryElementName = nameTable.Add("entry");
			this.AtomCategoryElementName = nameTable.Add("category");
			this.AtomCategoryTermAttributeName = nameTable.Add("term");
			this.AtomCategorySchemeAttributeName = nameTable.Add("scheme");
			this.AtomContentElementName = nameTable.Add("content");
			this.AtomLinkElementName = nameTable.Add("link");
			this.AtomPropertiesElementName = nameTable.Add("properties");
			this.AtomFeedElementName = nameTable.Add("feed");
			this.AtomIdElementName = nameTable.Add("id");
			this.AtomLinkRelationAttributeName = nameTable.Add("rel");
			this.AtomLinkHrefAttributeName = nameTable.Add("href");
			this.MediaLinkEntryContentSourceAttributeName = nameTable.Add("src");
			this.ODataETagAttributeName = nameTable.Add("etag");
			this.ODataCountElementName = nameTable.Add("count");
			this.ODataInlineElementName = nameTable.Add("inline");
			this.ODataActionElementName = nameTable.Add("action");
			this.ODataFunctionElementName = nameTable.Add("function");
			this.ODataOperationMetadataAttribute = nameTable.Add("metadata");
			this.ODataOperationTitleAttribute = nameTable.Add("title");
			this.ODataOperationTargetAttribute = nameTable.Add("target");
			this.atomAnnotationReader = new ODataAtomAnnotationReader(base.AtomInputContext, this);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00007ED4 File Offset: 0x000060D4
		private ODataAtomEntryMetadataDeserializer EntryMetadataDeserializer
		{
			get
			{
				ODataAtomEntryMetadataDeserializer odataAtomEntryMetadataDeserializer;
				if ((odataAtomEntryMetadataDeserializer = this.entryMetadataDeserializer) == null)
				{
					odataAtomEntryMetadataDeserializer = (this.entryMetadataDeserializer = new ODataAtomEntryMetadataDeserializer(base.AtomInputContext));
				}
				return odataAtomEntryMetadataDeserializer;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007F00 File Offset: 0x00006100
		private ODataAtomFeedMetadataDeserializer FeedMetadataDeserializer
		{
			get
			{
				ODataAtomFeedMetadataDeserializer odataAtomFeedMetadataDeserializer;
				if ((odataAtomFeedMetadataDeserializer = this.feedMetadataDeserializer) == null)
				{
					odataAtomFeedMetadataDeserializer = (this.feedMetadataDeserializer = new ODataAtomFeedMetadataDeserializer(base.AtomInputContext, false));
				}
				return odataAtomFeedMetadataDeserializer;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007F2C File Offset: 0x0000612C
		private bool ReadAtomMetadata
		{
			get
			{
				return base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007F40 File Offset: 0x00006140
		internal static void EnsureMediaResource(IODataAtomReaderEntryState entryState)
		{
			entryState.MediaLinkEntry = new bool?(true);
			ODataEntry entry = entryState.Entry;
			if (entry.MediaResource == null)
			{
				entry.MediaResource = new ODataStreamReferenceValue();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007F74 File Offset: 0x00006174
		internal void VerifyEntryStart()
		{
			if (base.XmlReader.NodeType != 1)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ElementExpected(base.XmlReader.NodeType));
			}
			if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !base.XmlReader.LocalNameEquals(this.AtomEntryElementName))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_EntryElementWrongName(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007FF4 File Offset: 0x000061F4
		internal void ReadEntryStart(ODataEntry entry)
		{
			this.VerifyEntryStart();
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && base.XmlReader.LocalNameEquals(this.ODataETagAttributeName))
				{
					entry.ETag = base.XmlReader.Value;
					break;
				}
			}
			base.XmlReader.MoveToElement();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008060 File Offset: 0x00006260
		internal ODataAtomReaderNavigationLinkDescriptor ReadEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = null;
			while (base.XmlReader.NodeType != 15)
			{
				if (base.XmlReader.NodeType != 1)
				{
					base.XmlReader.Skip();
				}
				else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
				{
					odataAtomReaderNavigationLinkDescriptor = this.ReadAtomElementInEntry(entryState);
					if (odataAtomReaderNavigationLinkDescriptor != null)
					{
						entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataAtomReaderNavigationLinkDescriptor.NavigationLink);
						break;
					}
				}
				else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomPropertiesElementName))
					{
						this.ValidateDuplicateElement(entryState.HasProperties && base.AtomInputContext.UseDefaultFormatBehavior);
						ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState);
						base.ReadProperties(entryState.EntityType, entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), entryState.DuplicatePropertyNamesChecker);
						base.XmlReader.Read();
						entryState.HasProperties = true;
					}
					else if (!base.ReadingResponse || !this.TryReadOperation(entryState))
					{
						AtomInstanceAnnotation atomInstanceAnnotation;
						if (this.atomAnnotationReader.TryReadAnnotation(out atomInstanceAnnotation))
						{
							if (!atomInstanceAnnotation.IsTargetingCurrentElement)
							{
								throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget(atomInstanceAnnotation.Target, atomInstanceAnnotation.TermName));
							}
							entryState.Entry.InstanceAnnotations.Add(new ODataInstanceAnnotation(atomInstanceAnnotation.TermName, atomInstanceAnnotation.Value));
						}
						else
						{
							base.XmlReader.Skip();
						}
					}
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			return odataAtomReaderNavigationLinkDescriptor;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000081E2 File Offset: 0x000063E2
		internal void ReadEntryEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000081F0 File Offset: 0x000063F0
		internal void ReadFeedStart()
		{
			if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !base.XmlReader.LocalNameEquals(this.AtomFeedElementName))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_FeedElementWrongName(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008244 File Offset: 0x00006444
		internal bool ReadFeedContent(IODataAtomReaderFeedState feedState, bool isExpandedLinkContent)
		{
			bool flag = false;
			while (base.XmlReader.NodeType != 15)
			{
				if (base.XmlReader.NodeType != 1)
				{
					base.XmlReader.Skip();
				}
				else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
				{
					if (this.ReadAtomElementInFeed(feedState, isExpandedLinkContent))
					{
						flag = true;
						break;
					}
				}
				else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
				{
					AtomInstanceAnnotation atomInstanceAnnotation;
					if (base.ReadingResponse && !isExpandedLinkContent && base.XmlReader.LocalNameEquals(this.ODataCountElementName))
					{
						this.ValidateDuplicateElement(feedState.HasCount);
						long num = (long)AtomValueUtils.ReadPrimitiveValue(base.XmlReader, EdmCoreModel.Instance.GetInt64(true));
						feedState.Feed.Count = new long?(num);
						base.XmlReader.Read();
						feedState.HasCount = true;
					}
					else if (this.atomAnnotationReader.TryReadAnnotation(out atomInstanceAnnotation))
					{
						if (isExpandedLinkContent)
						{
							throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_EncounteredAnnotationInNestedFeed);
						}
						if (!atomInstanceAnnotation.IsTargetingCurrentElement)
						{
							throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget(atomInstanceAnnotation.Target, atomInstanceAnnotation.TermName));
						}
						feedState.Feed.InstanceAnnotations.Add(new ODataInstanceAnnotation(atomInstanceAnnotation.TermName, atomInstanceAnnotation.Value));
					}
					else
					{
						base.XmlReader.Skip();
					}
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			return flag;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000083AE File Offset: 0x000065AE
		internal void ReadFeedEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000083BC File Offset: 0x000065BC
		internal ODataAtomDeserializerExpandedNavigationLinkContent ReadNavigationLinkContentBeforeExpansion()
		{
			if (!this.ReadNavigationLinkContent())
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.None;
			}
			if (base.XmlReader.IsEmptyElement)
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.Empty;
			}
			base.XmlReader.Read();
			return this.ReadInlineElementContent();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000083EC File Offset: 0x000065EC
		internal bool IsReaderOnInlineEndElement()
		{
			return base.XmlReader.LocalNameEquals(this.ODataInlineElementName) && base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && ((base.XmlReader.NodeType == 1 && base.XmlReader.IsEmptyElement) || base.XmlReader.NodeType == 15);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008454 File Offset: 0x00006654
		internal void SkipNavigationLinkContentOnExpansion()
		{
			do
			{
				base.XmlReader.Skip();
			}
			while (base.XmlReader.NodeType != 15 || !base.XmlReader.LocalNameEquals(this.AtomLinkElementName) || !base.XmlReader.NamespaceEquals(this.AtomNamespace));
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000084A4 File Offset: 0x000066A4
		internal void ReadNavigationLinkContentAfterExpansion(bool emptyInline)
		{
			if (!emptyInline)
			{
				ODataAtomDeserializerExpandedNavigationLinkContent odataAtomDeserializerExpandedNavigationLinkContent = this.ReadInlineElementContent();
				if (odataAtomDeserializerExpandedNavigationLinkContent != ODataAtomDeserializerExpandedNavigationLinkContent.Empty)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleExpansionsInInline(odataAtomDeserializerExpandedNavigationLinkContent.ToString()));
				}
			}
			base.XmlReader.Read();
			if (this.ReadNavigationLinkContent())
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleInlineElementsInLink);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000084F4 File Offset: 0x000066F4
		internal void ReadNavigationLinkEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008504 File Offset: 0x00006704
		internal string FindTypeName()
		{
			base.XmlReader.MoveToElement();
			base.XmlReader.StartBuffering();
			try
			{
				if (!base.XmlReader.IsEmptyElement)
				{
					base.XmlReader.Read();
					string text;
					for (;;)
					{
						XmlNodeType nodeType = base.XmlReader.NodeType;
						if (nodeType != 1)
						{
							if (nodeType == 15)
							{
								goto IL_0118;
							}
							base.XmlReader.Skip();
						}
						else
						{
							if (base.XmlReader.NamespaceEquals(this.AtomNamespace) && base.XmlReader.LocalNameEquals(this.AtomCategoryElementName))
							{
								text = null;
								bool flag = false;
								while (base.XmlReader.MoveToNextAttribute())
								{
									bool flag2 = base.XmlReader.NamespaceEquals(this.EmptyNamespace);
									if (flag2)
									{
										if (base.XmlReader.LocalNameEquals(this.AtomCategorySchemeAttributeName))
										{
											if (string.CompareOrdinal(base.XmlReader.Value, "http://docs.oasis-open.org/odata/ns/scheme") == 0)
											{
												flag = true;
											}
										}
										else if (base.XmlReader.LocalNameEquals(this.AtomCategoryTermAttributeName) && text == null)
										{
											text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName(base.XmlReader.Value));
										}
									}
								}
								if (flag)
								{
									break;
								}
							}
							base.XmlReader.Skip();
						}
					}
					return text;
					IL_0118:
					return null;
				}
			}
			finally
			{
				base.XmlReader.StopBuffering();
			}
			return null;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000868C File Offset: 0x0000688C
		private ODataStreamReferenceValue GetNewOrExistingStreamPropertyValue(IODataAtomReaderEntryState entryState, string streamPropertyName)
		{
			ReadOnlyEnumerable<ODataProperty> readOnlyEnumerable = entryState.Entry.Properties.ToReadOnlyEnumerable("Properties");
			ODataProperty odataProperty = Enumerable.FirstOrDefault<ODataProperty>(readOnlyEnumerable, (ODataProperty p) => string.CompareOrdinal(p.Name, streamPropertyName) == 0);
			ODataStreamReferenceValue odataStreamReferenceValue;
			if (odataProperty == null)
			{
				IEdmProperty edmProperty = ReaderValidationUtils.ValidateLinkPropertyDefined(streamPropertyName, entryState.EntityType, base.MessageReaderSettings);
				odataStreamReferenceValue = new ODataStreamReferenceValue();
				odataProperty = new ODataProperty
				{
					Name = streamPropertyName,
					Value = odataStreamReferenceValue
				};
				ReaderValidationUtils.ValidateStreamReferenceProperty(odataProperty, entryState.EntityType, edmProperty, base.MessageReaderSettings);
				entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
				readOnlyEnumerable.AddToSourceList(odataProperty);
			}
			else
			{
				odataStreamReferenceValue = odataProperty.Value as ODataStreamReferenceValue;
				if (odataStreamReferenceValue == null)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyDuplicatePropertyName(streamPropertyName));
				}
			}
			return odataStreamReferenceValue;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000875E File Offset: 0x0000695E
		private void ValidateDuplicateElement(bool duplicateElementFound)
		{
			if (duplicateElementFound)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_DuplicateElements(base.XmlReader.NamespaceURI, base.XmlReader.LocalName));
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008784 File Offset: 0x00006984
		private ODataAtomReaderNavigationLinkDescriptor ReadAtomElementInEntry(IODataAtomReaderEntryState entryState)
		{
			if (base.XmlReader.LocalNameEquals(this.AtomContentElementName))
			{
				this.ReadAtomContentElement(entryState);
			}
			else if (base.XmlReader.LocalNameEquals(this.AtomIdElementName))
			{
				this.ReadAtomIdElementInEntry(entryState);
			}
			else if (base.XmlReader.LocalNameEquals(this.AtomCategoryElementName))
			{
				string attribute = base.XmlReader.GetAttribute(this.AtomCategorySchemeAttributeName, this.EmptyNamespace);
				if (attribute != null && string.CompareOrdinal(attribute, "http://docs.oasis-open.org/odata/ns/scheme") == 0)
				{
					this.ValidateDuplicateElement(entryState.HasTypeNameCategory && base.AtomInputContext.UseDefaultFormatBehavior);
					if (this.ReadAtomMetadata)
					{
						entryState.AtomEntryMetadata.CategoryWithTypeName = this.EntryMetadataDeserializer.ReadAtomCategoryElement();
					}
					else
					{
						base.XmlReader.Skip();
					}
					entryState.HasTypeNameCategory = true;
				}
				else if (this.ReadAtomMetadata)
				{
					this.EntryMetadataDeserializer.ReadAtomCategoryElementInEntryContent(entryState);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			else
			{
				if (base.XmlReader.LocalNameEquals(this.AtomLinkElementName))
				{
					return this.ReadAtomLinkElementInEntry(entryState);
				}
				if (this.ReadAtomMetadata)
				{
					this.EntryMetadataDeserializer.ReadAtomElementInEntryContent(entryState);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			return null;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000088C0 File Offset: 0x00006AC0
		private void ReadAtomContentElement(IODataAtomReaderEntryState entryState)
		{
			this.ValidateDuplicateElement(entryState.HasContent && base.AtomInputContext.UseDefaultFormatBehavior);
			string text;
			string text2;
			this.ReadAtomContentAttributes(out text, out text2);
			if (text2 != null)
			{
				ODataEntry entry = entryState.Entry;
				ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState);
				if (!base.AtomInputContext.UseServerFormatBehavior)
				{
					entry.MediaResource.ReadLink = base.ProcessUriFromPayload(text2, base.XmlReader.XmlBaseUri);
				}
				entry.MediaResource.ContentType = text;
				if (!base.XmlReader.TryReadEmptyElement())
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithSourceLinkIsNotEmpty);
				}
			}
			else
			{
				string text3 = text;
				if (!string.IsNullOrEmpty(text))
				{
					text3 = ODataAtomEntryAndFeedDeserializer.VerifyAtomContentMediaType(text);
				}
				entryState.MediaLinkEntry = new bool?(false);
				base.XmlReader.MoveToElement();
				if (!base.XmlReader.IsEmptyElement && base.XmlReader.NodeType != 15)
				{
					if (string.IsNullOrEmpty(text3))
					{
						base.XmlReader.ReadElementContentValue();
					}
					else
					{
						base.XmlReader.ReadStartElement();
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
							else
							{
								if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
								{
									if (!base.XmlReader.LocalNameEquals(this.AtomPropertiesElementName))
									{
										throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithInvalidNode(base.XmlReader.LocalName));
									}
									this.ValidateDuplicateElement(entryState.HasProperties);
									base.ReadProperties(entryState.EntityType, entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), entryState.DuplicatePropertyNamesChecker);
									entryState.HasProperties = true;
								}
								else
								{
									base.XmlReader.SkipElementContent();
								}
								base.XmlReader.Read();
							}
						}
					}
				}
			}
			base.XmlReader.Read();
			entryState.HasContent = true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008AB0 File Offset: 0x00006CB0
		private void ReadAtomContentAttributes(out string contentType, out string contentSource)
		{
			contentType = null;
			contentSource = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				bool flag = base.XmlReader.NamespaceEquals(this.EmptyNamespace);
				if (flag)
				{
					if (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName))
					{
						if (contentType == null)
						{
							contentType = base.XmlReader.Value;
						}
					}
					else if (base.XmlReader.LocalNameEquals(this.MediaLinkEntryContentSourceAttributeName) && contentSource == null)
					{
						contentSource = base.XmlReader.Value;
					}
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008B34 File Offset: 0x00006D34
		private void ReadAtomIdElementInEntry(IODataAtomReaderEntryState entryState)
		{
			this.ValidateDuplicateElement(entryState.HasId && base.AtomInputContext.UseDefaultFormatBehavior);
			string text = base.XmlReader.ReadElementValue();
			if (text != null && text.Length == 0)
			{
				entryState.Entry.Id = null;
			}
			else if (text != null && ODataAtomEntryAndFeedDeserializer.IsTransientId(text))
			{
				entryState.Entry.IsTransient = true;
			}
			else
			{
				entryState.Entry.Id = UriUtils.CreateUriAsEntryOrFeedId(text, 1, true);
			}
			entryState.HasId = true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008BB8 File Offset: 0x00006DB8
		private ODataAtomReaderNavigationLinkDescriptor ReadAtomLinkElementInEntry(IODataAtomReaderEntryState entryState)
		{
			string text;
			string text2;
			this.ReadAtomLinkRelationAndHRef(out text, out text2);
			if (text != null)
			{
				bool flag = false;
				if (!base.AtomInputContext.UseServerFormatBehavior && this.TryReadAtomStandardRelationLinkInEntry(entryState, text, text2))
				{
					return null;
				}
				string text3 = AtomUtils.UnescapeAtomLinkRelationAttribute(text);
				if (text3 != null)
				{
					if (!base.AtomInputContext.UseServerFormatBehavior)
					{
						string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(text3, "http://www.iana.org/assignments/relation/");
						if (nameFromAtomLinkRelationAttribute != null && this.TryReadAtomStandardRelationLinkInEntry(entryState, nameFromAtomLinkRelationAttribute, text2))
						{
							return null;
						}
					}
					ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = this.TryReadNavigationLinkInEntry(entryState, text3, text2);
					if (odataAtomReaderNavigationLinkDescriptor != null)
					{
						return odataAtomReaderNavigationLinkDescriptor;
					}
					if (this.TryReadStreamPropertyLinkInEntry(entryState, text3, text2, out flag))
					{
						return null;
					}
					if (!flag && this.TryReadAssociationLinkInEntry(entryState, text3, text2))
					{
						return null;
					}
				}
			}
			if (this.ReadAtomMetadata)
			{
				AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(text, text2);
				if (atomLinkMetadata != null)
				{
					entryState.AtomEntryMetadata.AddLink(atomLinkMetadata);
				}
			}
			base.XmlReader.Skip();
			return null;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008C8C File Offset: 0x00006E8C
		private bool TryReadAtomStandardRelationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			if (string.CompareOrdinal(linkRelation, "edit") == 0)
			{
				if (entryState.HasEditLink && !base.AtomInputContext.UseServerApiBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("edit"));
				}
				if (linkHRef != null)
				{
					entryState.Entry.EditLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				if (this.ReadAtomMetadata)
				{
					entryState.AtomEntryMetadata.EditLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
				entryState.HasEditLink = true;
				base.XmlReader.Skip();
				return true;
			}
			else if (string.CompareOrdinal(linkRelation, "self") == 0)
			{
				if (entryState.HasReadLink && !base.AtomInputContext.UseServerApiBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("self"));
				}
				if (linkHRef != null)
				{
					entryState.Entry.ReadLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				if (this.ReadAtomMetadata)
				{
					entryState.AtomEntryMetadata.SelfLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
				entryState.HasReadLink = true;
				base.XmlReader.Skip();
				return true;
			}
			else
			{
				if (string.CompareOrdinal(linkRelation, "edit-media") != 0)
				{
					return false;
				}
				if (entryState.HasEditMediaLink && !base.AtomInputContext.UseServerApiBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("edit-media"));
				}
				ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState);
				ODataEntry entry = entryState.Entry;
				if (linkHRef != null)
				{
					entry.MediaResource.EditLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				string attribute = base.XmlReader.GetAttribute(this.ODataETagAttributeName, base.XmlReader.ODataMetadataNamespace);
				if (attribute != null)
				{
					entry.MediaResource.ETag = attribute;
				}
				if (this.ReadAtomMetadata)
				{
					AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
					entry.MediaResource.SetAnnotation<AtomStreamReferenceMetadata>(new AtomStreamReferenceMetadata
					{
						EditLink = atomLinkMetadata
					});
				}
				entryState.HasEditMediaLink = true;
				base.XmlReader.Skip();
				return true;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008E78 File Offset: 0x00007078
		private ODataAtomReaderNavigationLinkDescriptor TryReadNavigationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://docs.oasis-open.org/odata/ns/related/");
			if (string.IsNullOrEmpty(nameFromAtomLinkRelationAttribute))
			{
				return null;
			}
			IEdmNavigationProperty edmNavigationProperty = ReaderValidationUtils.ValidateNavigationPropertyDefined(nameFromAtomLinkRelationAttribute, entryState.EntityType, base.MessageReaderSettings);
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = nameFromAtomLinkRelationAttribute
			};
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (!string.IsNullOrEmpty(attribute))
			{
				bool flag;
				bool flag2;
				if (!AtomUtils.IsExactNavigationLinkTypeMatch(attribute, out flag, out flag2))
				{
					string text;
					string text2;
					IList<KeyValuePair<string, string>> list = HttpUtils.ReadMimeType(attribute, out text, out text2);
					if (!HttpUtils.CompareMediaTypeNames(text, "application/atom+xml"))
					{
						return null;
					}
					string text3 = null;
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							KeyValuePair<string, string> keyValuePair = list[i];
							if (HttpUtils.CompareMediaTypeParameterNames("type", keyValuePair.Key))
							{
								text3 = keyValuePair.Value;
								break;
							}
						}
					}
					if (text3 != null)
					{
						if (string.Compare(text3, "entry", 5) == 0)
						{
							flag = true;
						}
						else if (string.Compare(text3, "feed", 5) == 0)
						{
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					odataNavigationLink.IsCollection = new bool?(false);
				}
				else if (flag2)
				{
					odataNavigationLink.IsCollection = new bool?(true);
				}
			}
			if (linkHRef != null)
			{
				odataNavigationLink.Url = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
			}
			base.XmlReader.MoveToElement();
			AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
			if (atomLinkMetadata != null)
			{
				odataNavigationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			return new ODataAtomReaderNavigationLinkDescriptor(odataNavigationLink, edmNavigationProperty);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008FF0 File Offset: 0x000071F0
		private bool TryReadStreamPropertyLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef, out bool isStreamPropertyLink)
		{
			string text = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://docs.oasis-open.org/odata/ns/edit-media/");
			if (text != null)
			{
				isStreamPropertyLink = true;
				return this.ReadStreamPropertyLinkInEntry(entryState, text, linkRelation, linkHRef, true);
			}
			text = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://docs.oasis-open.org/odata/ns/mediaresource/");
			if (text != null)
			{
				isStreamPropertyLink = true;
				return this.ReadStreamPropertyLinkInEntry(entryState, text, linkRelation, linkHRef, false);
			}
			isStreamPropertyLink = false;
			return false;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009040 File Offset: 0x00007240
		private bool ReadStreamPropertyLinkInEntry(IODataAtomReaderEntryState entryState, string streamPropertyName, string linkRelation, string linkHRef, bool editLink)
		{
			if (!base.ReadingResponse)
			{
				return false;
			}
			if (streamPropertyName.Length == 0)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithEmptyName);
			}
			ODataStreamReferenceValue newOrExistingStreamPropertyValue = this.GetNewOrExistingStreamPropertyValue(entryState, streamPropertyName);
			AtomStreamReferenceMetadata atomStreamReferenceMetadata = null;
			if (this.ReadAtomMetadata)
			{
				atomStreamReferenceMetadata = newOrExistingStreamPropertyValue.GetAnnotation<AtomStreamReferenceMetadata>();
				if (atomStreamReferenceMetadata == null)
				{
					atomStreamReferenceMetadata = new AtomStreamReferenceMetadata();
					newOrExistingStreamPropertyValue.SetAnnotation<AtomStreamReferenceMetadata>(atomStreamReferenceMetadata);
				}
			}
			Uri uri = ((linkHRef == null) ? null : base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri));
			if (editLink)
			{
				if (newOrExistingStreamPropertyValue.EditLink != null)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleEditLinks(streamPropertyName));
				}
				newOrExistingStreamPropertyValue.EditLink = uri;
				if (this.ReadAtomMetadata)
				{
					atomStreamReferenceMetadata.EditLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
			}
			else
			{
				if (newOrExistingStreamPropertyValue.ReadLink != null)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleReadLinks(streamPropertyName));
				}
				newOrExistingStreamPropertyValue.ReadLink = uri;
				if (this.ReadAtomMetadata)
				{
					atomStreamReferenceMetadata.SelfLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
			}
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (attribute != null && newOrExistingStreamPropertyValue.ContentType != null && !HttpUtils.CompareMediaTypeNames(attribute, newOrExistingStreamPropertyValue.ContentType))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleContentTypes(streamPropertyName));
			}
			newOrExistingStreamPropertyValue.ContentType = attribute;
			if (editLink)
			{
				string attribute2 = base.XmlReader.GetAttribute(this.ODataETagAttributeName, base.XmlReader.ODataMetadataNamespace);
				newOrExistingStreamPropertyValue.ETag = attribute2;
			}
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000091AC File Offset: 0x000073AC
		private bool TryReadAssociationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://docs.oasis-open.org/odata/ns/relatedlinks/");
			if (string.IsNullOrEmpty(nameFromAtomLinkRelationAttribute) || !base.ReadingResponse)
			{
				return false;
			}
			ReaderValidationUtils.ValidateNavigationPropertyDefined(nameFromAtomLinkRelationAttribute, entryState.EntityType, base.MessageReaderSettings);
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (attribute != null && !HttpUtils.CompareMediaTypeNames(attribute, "application/xml"))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink(nameFromAtomLinkRelationAttribute));
			}
			Uri uri = null;
			if (linkHRef != null)
			{
				uri = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
			}
			ReaderUtils.CheckForDuplicateAssociationLinkAndUpdateNavigationLink(entryState.DuplicatePropertyNamesChecker, nameFromAtomLinkRelationAttribute, uri);
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009250 File Offset: 0x00007450
		private bool TryReadOperation(IODataAtomReaderEntryState entryState)
		{
			bool flag = false;
			if (base.XmlReader.LocalNameEquals(this.ODataActionElementName))
			{
				flag = true;
			}
			else if (!base.XmlReader.LocalNameEquals(this.ODataFunctionElementName))
			{
				return false;
			}
			ODataOperation odataOperation;
			if (flag)
			{
				odataOperation = new ODataAction();
				entryState.Entry.AddAction((ODataAction)odataOperation);
			}
			else
			{
				odataOperation = new ODataFunction();
				entryState.Entry.AddFunction((ODataFunction)odataOperation);
			}
			string localName = base.XmlReader.LocalName;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					string value = base.XmlReader.Value;
					if (base.XmlReader.LocalNameEquals(this.ODataOperationMetadataAttribute))
					{
						odataOperation.Metadata = base.ProcessUriFromPayload(value, base.XmlReader.XmlBaseUri, false);
					}
					else if (base.XmlReader.LocalNameEquals(this.ODataOperationTargetAttribute))
					{
						odataOperation.Target = base.ProcessUriFromPayload(value, base.XmlReader.XmlBaseUri);
					}
					else if (base.XmlReader.LocalNameEquals(this.ODataOperationTitleAttribute))
					{
						odataOperation.Title = base.XmlReader.Value;
					}
				}
			}
			if (odataOperation.Metadata == null)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_OperationMissingMetadataAttribute(localName));
			}
			if (odataOperation.Target == null)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_OperationMissingTargetAttribute(localName));
			}
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000093C0 File Offset: 0x000075C0
		private bool ReadAtomElementInFeed(IODataAtomReaderFeedState feedState, bool isExpandedLinkContent)
		{
			if (base.XmlReader.LocalNameEquals(this.AtomEntryElementName))
			{
				return true;
			}
			if (base.XmlReader.LocalNameEquals(this.AtomLinkElementName))
			{
				string text;
				string text2;
				this.ReadAtomLinkRelationAndHRef(out text, out text2);
				if (text != null)
				{
					if (this.ReadAtomStandardRelationLinkInFeed(feedState, text, text2, isExpandedLinkContent))
					{
						return false;
					}
					string text3 = AtomUtils.UnescapeAtomLinkRelationAttribute(text);
					if (text3 != null)
					{
						string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(text, "http://www.iana.org/assignments/relation/");
						if (nameFromAtomLinkRelationAttribute != null && this.ReadAtomStandardRelationLinkInFeed(feedState, nameFromAtomLinkRelationAttribute, text2, isExpandedLinkContent))
						{
							return false;
						}
					}
				}
				if (this.ReadAtomMetadata)
				{
					AtomLinkMetadata atomLinkMetadata = this.FeedMetadataDeserializer.ReadAtomLinkElementInFeed(text, text2);
					feedState.AtomFeedMetadata.AddLink(atomLinkMetadata);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			else if (base.XmlReader.LocalNameEquals(this.AtomIdElementName))
			{
				string text4 = base.XmlReader.ReadElementValue();
				feedState.Feed.Id = UriUtils.CreateUriAsEntryOrFeedId(text4, 1, true);
			}
			else if (this.ReadAtomMetadata)
			{
				this.FeedMetadataDeserializer.ReadAtomElementAsFeedMetadata(feedState.AtomFeedMetadata);
			}
			else
			{
				base.XmlReader.Skip();
			}
			return false;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009508 File Offset: 0x00007708
		private bool ReadAtomStandardRelationLinkInFeed(IODataAtomReaderFeedState feedState, string linkRelation, string linkHRef, bool isExpandedLinkContent)
		{
			if (string.CompareOrdinal(linkRelation, "next") == 0)
			{
				if (!base.ReadingResponse)
				{
					return false;
				}
				if (feedState.HasNextPageLink)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed("next"));
				}
				if (linkHRef != null)
				{
					feedState.Feed.NextPageLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				feedState.HasNextPageLink = true;
				this.ReadLinkMetadataIfRequired(linkRelation, linkHRef, delegate(AtomLinkMetadata linkMetadata)
				{
					feedState.AtomFeedMetadata.NextPageLink = linkMetadata;
				});
				return true;
			}
			else if (string.CompareOrdinal(linkRelation, "self") == 0)
			{
				if (feedState.HasReadLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed("self"));
				}
				this.ReadLinkMetadataIfRequired(linkRelation, linkHRef, delegate(AtomLinkMetadata linkMetadata)
				{
					feedState.AtomFeedMetadata.SelfLink = linkMetadata;
				});
				feedState.HasReadLink = true;
				return true;
			}
			else
			{
				if (string.CompareOrdinal(linkRelation, "http://docs.oasis-open.org/odata/ns/delta") != 0)
				{
					return false;
				}
				if (!base.ReadingResponse)
				{
					return false;
				}
				if (feedState.HasDeltaLink)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed("http://docs.oasis-open.org/odata/ns/delta"));
				}
				if (isExpandedLinkContent)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_EncounteredDeltaLinkInNestedFeed);
				}
				if (linkHRef != null)
				{
					feedState.Feed.DeltaLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				this.ReadLinkMetadataIfRequired(linkRelation, linkHRef, delegate(AtomLinkMetadata linkMetadata)
				{
					feedState.AtomFeedMetadata.AddLink(linkMetadata);
				});
				feedState.HasDeltaLink = true;
				return true;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000969C File Offset: 0x0000789C
		private void ReadLinkMetadataIfRequired(string linkRelation, string linkHRef, Action<AtomLinkMetadata> setFeedLink)
		{
			if (this.ReadAtomMetadata)
			{
				AtomLinkMetadata atomLinkMetadata = this.FeedMetadataDeserializer.ReadAtomLinkElementInFeed(linkRelation, linkHRef);
				setFeedLink.Invoke(atomLinkMetadata);
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000096D4 File Offset: 0x000078D4
		private void ReadAtomLinkRelationAndHRef(out string linkRelation, out string linkHRef)
		{
			linkRelation = null;
			linkHRef = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomLinkRelationAttributeName))
					{
						linkRelation = base.XmlReader.Value;
						if (linkHRef != null)
						{
							break;
						}
					}
					else if (base.XmlReader.LocalNameEquals(this.AtomLinkHrefAttributeName))
					{
						linkHRef = base.XmlReader.Value;
						if (linkRelation != null)
						{
							break;
						}
					}
				}
			}
			base.XmlReader.MoveToElement();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009760 File Offset: 0x00007960
		private bool ReadNavigationLinkContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != 1)
				{
					if (nodeType == 15)
					{
						break;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.LocalNameEquals(this.ODataInlineElementName) && base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
					{
						return true;
					}
					base.XmlReader.Skip();
				}
			}
			return false;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000097CC File Offset: 0x000079CC
		private ODataAtomDeserializerExpandedNavigationLinkContent ReadInlineElementContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != 1)
				{
					if (nodeType == 15)
					{
						break;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
					{
						goto Block_2;
					}
					base.XmlReader.Skip();
				}
			}
			return ODataAtomDeserializerExpandedNavigationLinkContent.Empty;
			Block_2:
			if (base.XmlReader.LocalNameEquals(this.AtomEntryElementName))
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.Entry;
			}
			if (base.XmlReader.LocalNameEquals(this.AtomFeedElementName))
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.Feed;
			}
			throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_UnknownElementInInline(base.XmlReader.LocalName));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009860 File Offset: 0x00007A60
		private static string VerifyAtomContentMediaType(string contentType)
		{
			if (!HttpUtils.CompareMediaTypeNames("application/xml", contentType) && !HttpUtils.CompareMediaTypeNames("application/atom+xml", contentType))
			{
				string text;
				string text2;
				HttpUtils.ReadMimeType(contentType, out text, out text2);
				if (!HttpUtils.CompareMediaTypeNames(text, "application/xml") && !HttpUtils.CompareMediaTypeNames(text, "application/atom+xml"))
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithWrongType(text));
				}
			}
			return contentType;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000098BC File Offset: 0x00007ABC
		private static bool IsTransientId(string id)
		{
			Match match = ODataAtomEntryAndFeedDeserializer.transientIdRegex.Match(id);
			return match.Success;
		}

		// Token: 0x0400014C RID: 332
		private readonly string AtomNamespace;

		// Token: 0x0400014D RID: 333
		private readonly string AtomEntryElementName;

		// Token: 0x0400014E RID: 334
		private readonly string AtomCategoryElementName;

		// Token: 0x0400014F RID: 335
		private readonly string AtomCategoryTermAttributeName;

		// Token: 0x04000150 RID: 336
		private readonly string AtomCategorySchemeAttributeName;

		// Token: 0x04000151 RID: 337
		private readonly string AtomContentElementName;

		// Token: 0x04000152 RID: 338
		private readonly string AtomLinkElementName;

		// Token: 0x04000153 RID: 339
		private readonly string AtomPropertiesElementName;

		// Token: 0x04000154 RID: 340
		private readonly string AtomFeedElementName;

		// Token: 0x04000155 RID: 341
		private readonly string AtomIdElementName;

		// Token: 0x04000156 RID: 342
		private readonly string AtomLinkRelationAttributeName;

		// Token: 0x04000157 RID: 343
		private readonly string AtomLinkHrefAttributeName;

		// Token: 0x04000158 RID: 344
		private readonly string MediaLinkEntryContentSourceAttributeName;

		// Token: 0x04000159 RID: 345
		private readonly string ODataETagAttributeName;

		// Token: 0x0400015A RID: 346
		private readonly string ODataCountElementName;

		// Token: 0x0400015B RID: 347
		private readonly string ODataInlineElementName;

		// Token: 0x0400015C RID: 348
		private readonly string ODataActionElementName;

		// Token: 0x0400015D RID: 349
		private readonly string ODataFunctionElementName;

		// Token: 0x0400015E RID: 350
		private readonly string ODataOperationMetadataAttribute;

		// Token: 0x0400015F RID: 351
		private readonly string ODataOperationTitleAttribute;

		// Token: 0x04000160 RID: 352
		private readonly string ODataOperationTargetAttribute;

		// Token: 0x04000161 RID: 353
		private readonly ODataAtomAnnotationReader atomAnnotationReader;

		// Token: 0x04000162 RID: 354
		private ODataAtomEntryMetadataDeserializer entryMetadataDeserializer;

		// Token: 0x04000163 RID: 355
		private ODataAtomFeedMetadataDeserializer feedMetadataDeserializer;

		// Token: 0x04000164 RID: 356
		private static readonly Regex transientIdRegex = new Regex("^odata:transient:{([\\s\\S]*)}$");
	}
}
