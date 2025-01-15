using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021C RID: 540
	internal sealed class ODataAtomEntryAndFeedDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0003A384 File Offset: 0x00038584
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003A51C File Offset: 0x0003871C
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0003A548 File Offset: 0x00038748
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003A574 File Offset: 0x00038774
		private bool ReadAtomMetadata
		{
			get
			{
				return base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003A588 File Offset: 0x00038788
		internal static void EnsureMediaResource(IODataAtomReaderEntryState entryState, bool validateMLEPresence)
		{
			if (validateMLEPresence)
			{
				entryState.MediaLinkEntry = new bool?(true);
			}
			ODataEntry entry = entryState.Entry;
			if (entry.MediaResource == null)
			{
				entry.MediaResource = new ODataStreamReferenceValue();
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003A5C0 File Offset: 0x000387C0
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

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003A640 File Offset: 0x00038840
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

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003A6AC File Offset: 0x000388AC
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
						ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState, true);
						base.ReadProperties(entryState.EntityType, entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), entryState.DuplicatePropertyNamesChecker, entryState.CachedEpm != null);
						base.XmlReader.Read();
						entryState.HasProperties = true;
					}
					else if (base.MessageReaderSettings.MaxProtocolVersion < ODataVersion.V3 || !base.ReadingResponse || !this.TryReadOperation(entryState))
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
						else if (!this.EntryMetadataDeserializer.TryReadExtensionElementInEntryContent(entryState))
						{
							base.XmlReader.Skip();
						}
					}
				}
				else if (!this.EntryMetadataDeserializer.TryReadExtensionElementInEntryContent(entryState))
				{
					base.XmlReader.Skip();
				}
			}
			return odataAtomReaderNavigationLinkDescriptor;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003A86B File Offset: 0x00038A6B
		internal void ReadEntryEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003A87C File Offset: 0x00038A7C
		internal void ReadFeedStart()
		{
			if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !base.XmlReader.LocalNameEquals(this.AtomFeedElementName))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_FeedElementWrongName(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003A8D0 File Offset: 0x00038AD0
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
					if (base.ReadingResponse && base.Version >= ODataVersion.V2 && !isExpandedLinkContent && base.XmlReader.LocalNameEquals(this.ODataCountElementName))
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

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003AA43 File Offset: 0x00038C43
		internal void ReadFeedEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003AA51 File Offset: 0x00038C51
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

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003AA80 File Offset: 0x00038C80
		internal bool IsReaderOnInlineEndElement()
		{
			return base.XmlReader.LocalNameEquals(this.ODataInlineElementName) && base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && ((base.XmlReader.NodeType == 1 && base.XmlReader.IsEmptyElement) || base.XmlReader.NodeType == 15);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003AAE8 File Offset: 0x00038CE8
		internal void SkipNavigationLinkContentOnExpansion()
		{
			do
			{
				base.XmlReader.Skip();
			}
			while (base.XmlReader.NodeType != 15 || !base.XmlReader.LocalNameEquals(this.AtomLinkElementName) || !base.XmlReader.NamespaceEquals(this.AtomNamespace));
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003AB38 File Offset: 0x00038D38
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

		// Token: 0x06000FED RID: 4077 RVA: 0x0003AB88 File Offset: 0x00038D88
		internal void ReadNavigationLinkEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003AB98 File Offset: 0x00038D98
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
								goto IL_013D;
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
									if (flag2 || (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.AtomNamespace)))
									{
										if (base.XmlReader.LocalNameEquals(this.AtomCategorySchemeAttributeName))
										{
											if (string.CompareOrdinal(base.XmlReader.Value, base.MessageReaderSettings.ReaderBehavior.ODataTypeScheme) == 0)
											{
												flag = true;
											}
										}
										else if (base.XmlReader.LocalNameEquals(this.AtomCategoryTermAttributeName) && (text == null || !flag2))
										{
											text = base.XmlReader.Value;
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
					IL_013D:
					return null;
				}
			}
			finally
			{
				base.XmlReader.StopBuffering();
			}
			return null;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003AD44 File Offset: 0x00038F44
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

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003AE16 File Offset: 0x00039016
		private void ValidateDuplicateElement(bool duplicateElementFound)
		{
			if (duplicateElementFound)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_DuplicateElements(base.XmlReader.NamespaceURI, base.XmlReader.LocalName));
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003AE3C File Offset: 0x0003903C
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
				if (attribute != null && string.CompareOrdinal(attribute, base.MessageReaderSettings.ReaderBehavior.ODataTypeScheme) == 0)
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
				else if (entryState.CachedEpm != null || this.ReadAtomMetadata)
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
				if (entryState.CachedEpm != null || this.ReadAtomMetadata)
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

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003AF90 File Offset: 0x00039190
		private void ReadAtomContentElement(IODataAtomReaderEntryState entryState)
		{
			this.ValidateDuplicateElement(entryState.HasContent && base.AtomInputContext.UseDefaultFormatBehavior);
			if (base.AtomInputContext.UseClientFormatBehavior)
			{
				entryState.HasProperties = false;
			}
			string text;
			string text2;
			this.ReadAtomContentAttributes(out text, out text2);
			if (text2 != null)
			{
				ODataEntry entry = entryState.Entry;
				ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState, true);
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
				bool flag = string.IsNullOrEmpty(text);
				if (flag && base.AtomInputContext.UseClientFormatBehavior)
				{
					base.XmlReader.SkipElementContent();
				}
				string text3 = text;
				if (!flag)
				{
					text3 = this.VerifyAtomContentMediaType(text);
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
									this.ValidateDuplicateElement(entryState.HasProperties && base.AtomInputContext.UseDefaultFormatBehavior);
									if (base.UseClientFormatBehavior && entryState.HasProperties)
									{
										base.XmlReader.SkipElementContent();
									}
									else
									{
										base.ReadProperties(entryState.EntityType, entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), entryState.DuplicatePropertyNamesChecker, entryState.CachedEpm != null);
									}
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

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003B1F0 File Offset: 0x000393F0
		private void ReadAtomContentAttributes(out string contentType, out string contentSource)
		{
			contentType = null;
			contentSource = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				bool flag = base.XmlReader.NamespaceEquals(this.EmptyNamespace);
				if (flag || (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.AtomNamespace)))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName))
					{
						if (!flag || contentType == null)
						{
							contentType = base.XmlReader.Value;
						}
					}
					else if (base.XmlReader.LocalNameEquals(this.MediaLinkEntryContentSourceAttributeName) && (!flag || contentSource == null))
					{
						contentSource = base.XmlReader.Value;
					}
				}
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003B298 File Offset: 0x00039498
		private void ReadAtomIdElementInEntry(IODataAtomReaderEntryState entryState)
		{
			this.ValidateDuplicateElement(entryState.HasId && base.AtomInputContext.UseDefaultFormatBehavior);
			string text = base.XmlReader.ReadElementValue();
			if (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasId)
			{
				entryState.Entry.Id = ((text != null && text.Length == 0) ? null : text);
			}
			entryState.HasId = true;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003B304 File Offset: 0x00039504
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
			if (entryState.CachedEpm != null || this.ReadAtomMetadata)
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

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003B3E0 File Offset: 0x000395E0
		private bool TryReadAtomStandardRelationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			if (string.CompareOrdinal(linkRelation, "edit") == 0)
			{
				if (entryState.HasEditLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("edit"));
				}
				if (linkHRef != null && (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasEditLink))
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
				if (entryState.HasReadLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("self"));
				}
				if (linkHRef != null && (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasReadLink))
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
				if (entryState.HasEditMediaLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("edit-media"));
				}
				if (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasEditMediaLink)
				{
					ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState, !base.UseClientFormatBehavior);
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
				}
				entryState.HasEditMediaLink = true;
				base.XmlReader.Skip();
				return true;
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003B61C File Offset: 0x0003981C
		private ODataAtomReaderNavigationLinkDescriptor TryReadNavigationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/related/");
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
					if (!base.UseClientFormatBehavior)
					{
						odataNavigationLink.IsCollection = new bool?(false);
					}
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

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003B79C File Offset: 0x0003999C
		private bool TryReadStreamPropertyLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef, out bool isStreamPropertyLink)
		{
			string text = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/edit-media/");
			if (text != null)
			{
				isStreamPropertyLink = true;
				return this.ReadStreamPropertyLinkInEntry(entryState, text, linkRelation, linkHRef, true);
			}
			text = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/mediaresource/");
			if (text != null)
			{
				isStreamPropertyLink = true;
				return this.ReadStreamPropertyLinkInEntry(entryState, text, linkRelation, linkHRef, false);
			}
			isStreamPropertyLink = false;
			return false;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003B7EC File Offset: 0x000399EC
		private bool ReadStreamPropertyLinkInEntry(IODataAtomReaderEntryState entryState, string streamPropertyName, string linkRelation, string linkHRef, bool editLink)
		{
			if (!base.ReadingResponse || base.Version < ODataVersion.V3)
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

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003B960 File Offset: 0x00039B60
		private bool TryReadAssociationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/relatedlinks/");
			if (string.IsNullOrEmpty(nameFromAtomLinkRelationAttribute) || !base.ReadingResponse || base.MessageReaderSettings.MaxProtocolVersion < ODataVersion.V3)
			{
				return false;
			}
			ReaderValidationUtils.ValidateNavigationPropertyDefined(nameFromAtomLinkRelationAttribute, entryState.EntityType, base.MessageReaderSettings);
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (attribute != null && !HttpUtils.CompareMediaTypeNames(attribute, "application/xml"))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink(nameFromAtomLinkRelationAttribute));
			}
			ODataAssociationLink odataAssociationLink = new ODataAssociationLink
			{
				Name = nameFromAtomLinkRelationAttribute
			};
			if (linkHRef != null)
			{
				odataAssociationLink.Url = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
			}
			ReaderUtils.CheckForDuplicateAssociationLinkAndUpdateNavigationLink(entryState.DuplicatePropertyNamesChecker, odataAssociationLink);
			entryState.Entry.AddAssociationLink(odataAssociationLink);
			AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
			if (atomLinkMetadata != null)
			{
				odataAssociationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003BA4C File Offset: 0x00039C4C
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

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003BBBC File Offset: 0x00039DBC
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
				feedState.Feed.Id = text4;
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

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003BD00 File Offset: 0x00039F00
		private bool ReadAtomStandardRelationLinkInFeed(IODataAtomReaderFeedState feedState, string linkRelation, string linkHRef, bool isExpandedLinkContent)
		{
			if (string.CompareOrdinal(linkRelation, "next") == 0)
			{
				if (!base.ReadingResponse || base.Version < ODataVersion.V2)
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
				if (!base.ReadingResponse || base.Version < ODataVersion.V3)
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

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003BEA4 File Offset: 0x0003A0A4
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

		// Token: 0x06000FFF RID: 4095 RVA: 0x0003BEDC File Offset: 0x0003A0DC
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

		// Token: 0x06001000 RID: 4096 RVA: 0x0003BF68 File Offset: 0x0003A168
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

		// Token: 0x06001001 RID: 4097 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
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

		// Token: 0x06001002 RID: 4098 RVA: 0x0003C068 File Offset: 0x0003A268
		private string VerifyAtomContentMediaType(string contentType)
		{
			if (!HttpUtils.CompareMediaTypeNames("application/xml", contentType) && !HttpUtils.CompareMediaTypeNames("application/atom+xml", contentType))
			{
				string text;
				string text2;
				HttpUtils.ReadMimeType(contentType, out text, out text2);
				if (!HttpUtils.CompareMediaTypeNames(text, "application/xml") && !HttpUtils.CompareMediaTypeNames(text, "application/atom+xml"))
				{
					if (!base.AtomInputContext.UseClientFormatBehavior)
					{
						throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithWrongType(text));
					}
					base.XmlReader.SkipElementContent();
				}
			}
			return contentType;
		}

		// Token: 0x0400060D RID: 1549
		private readonly string AtomNamespace;

		// Token: 0x0400060E RID: 1550
		private readonly string AtomEntryElementName;

		// Token: 0x0400060F RID: 1551
		private readonly string AtomCategoryElementName;

		// Token: 0x04000610 RID: 1552
		private readonly string AtomCategoryTermAttributeName;

		// Token: 0x04000611 RID: 1553
		private readonly string AtomCategorySchemeAttributeName;

		// Token: 0x04000612 RID: 1554
		private readonly string AtomContentElementName;

		// Token: 0x04000613 RID: 1555
		private readonly string AtomLinkElementName;

		// Token: 0x04000614 RID: 1556
		private readonly string AtomPropertiesElementName;

		// Token: 0x04000615 RID: 1557
		private readonly string AtomFeedElementName;

		// Token: 0x04000616 RID: 1558
		private readonly string AtomIdElementName;

		// Token: 0x04000617 RID: 1559
		private readonly string AtomLinkRelationAttributeName;

		// Token: 0x04000618 RID: 1560
		private readonly string AtomLinkHrefAttributeName;

		// Token: 0x04000619 RID: 1561
		private readonly string MediaLinkEntryContentSourceAttributeName;

		// Token: 0x0400061A RID: 1562
		private readonly string ODataETagAttributeName;

		// Token: 0x0400061B RID: 1563
		private readonly string ODataCountElementName;

		// Token: 0x0400061C RID: 1564
		private readonly string ODataInlineElementName;

		// Token: 0x0400061D RID: 1565
		private readonly string ODataActionElementName;

		// Token: 0x0400061E RID: 1566
		private readonly string ODataFunctionElementName;

		// Token: 0x0400061F RID: 1567
		private readonly string ODataOperationMetadataAttribute;

		// Token: 0x04000620 RID: 1568
		private readonly string ODataOperationTitleAttribute;

		// Token: 0x04000621 RID: 1569
		private readonly string ODataOperationTargetAttribute;

		// Token: 0x04000622 RID: 1570
		private readonly ODataAtomAnnotationReader atomAnnotationReader;

		// Token: 0x04000623 RID: 1571
		private ODataAtomEntryMetadataDeserializer entryMetadataDeserializer;

		// Token: 0x04000624 RID: 1572
		private ODataAtomFeedMetadataDeserializer feedMetadataDeserializer;
	}
}
