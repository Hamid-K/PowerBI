using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Globalization;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000266 RID: 614
	internal sealed class EpmSyndicationWriter : EpmWriter
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x00047DF3 File Offset: 0x00045FF3
		private EpmSyndicationWriter(EpmTargetTree epmTargetTree, ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.epmTargetTree = epmTargetTree;
			this.entryMetadata = new AtomEntryMetadata();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00047E10 File Offset: 0x00046010
		internal static AtomEntryMetadata WriteEntryEpm(EpmTargetTree epmTargetTree, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference type, ODataAtomOutputContext atomOutputContext)
		{
			EpmSyndicationWriter epmSyndicationWriter = new EpmSyndicationWriter(epmTargetTree, atomOutputContext);
			return epmSyndicationWriter.WriteEntryEpm(epmValueCache, type);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00047E30 File Offset: 0x00046030
		private static AtomTextConstruct CreateAtomTextConstruct(string textValue, SyndicationTextContentKind contentKind)
		{
			AtomTextConstructKind atomTextConstructKind;
			switch (contentKind)
			{
			case SyndicationTextContentKind.Plaintext:
				atomTextConstructKind = AtomTextConstructKind.Text;
				break;
			case SyndicationTextContentKind.Html:
				atomTextConstructKind = AtomTextConstructKind.Html;
				break;
			case SyndicationTextContentKind.Xhtml:
				atomTextConstructKind = AtomTextConstructKind.Xhtml;
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationWriter_CreateAtomTextConstruct));
			}
			return new AtomTextConstruct
			{
				Kind = atomTextConstructKind,
				Text = textValue
			};
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00047E88 File Offset: 0x00046088
		private static DateTimeOffset CreateDateTimeValue(object propertyValue, SyndicationItemProperty targetProperty, ODataWriterBehavior writerBehavior)
		{
			if (propertyValue == null)
			{
				return DateTimeOffset.Now;
			}
			if (propertyValue is DateTimeOffset)
			{
				return (DateTimeOffset)propertyValue;
			}
			if (propertyValue is DateTime)
			{
				return new DateTimeOffset((DateTime)propertyValue);
			}
			string text = propertyValue as string;
			if (text == null)
			{
				DateTimeOffset dateTimeOffset;
				try
				{
					dateTimeOffset = new DateTimeOffset(Convert.ToDateTime(propertyValue, CultureInfo.InvariantCulture));
				}
				catch (Exception ex)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw new ODataException(Strings.EpmSyndicationWriter_DateTimePropertyCanNotBeConverted(targetProperty.ToString()));
				}
				return dateTimeOffset;
			}
			DateTimeOffset dateTimeOffset2;
			if (DateTimeOffset.TryParse(text, ref dateTimeOffset2))
			{
				return dateTimeOffset2;
			}
			DateTime dateTime;
			if (!DateTime.TryParse(text, ref dateTime))
			{
				throw new ODataException(Strings.EpmSyndicationWriter_DateTimePropertyCanNotBeConverted(targetProperty.ToString()));
			}
			return new DateTimeOffset(dateTime);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00047F48 File Offset: 0x00046148
		private static string CreateDateTimeStringValue(object propertyValue, ODataWriterBehavior writerBehavior)
		{
			if (propertyValue == null)
			{
				propertyValue = DateTimeOffset.Now;
			}
			if (propertyValue is DateTime)
			{
				propertyValue = new DateTimeOffset((DateTime)propertyValue);
			}
			if (propertyValue is DateTimeOffset)
			{
				return ODataAtomConvert.ToAtomString((DateTimeOffset)propertyValue);
			}
			return EpmWriterUtils.GetPropertyValueAsText(propertyValue);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00047F98 File Offset: 0x00046198
		private AtomEntryMetadata WriteEntryEpm(EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType)
		{
			EpmTargetPathSegment syndicationRoot = this.epmTargetTree.SyndicationRoot;
			if (syndicationRoot.SubSegments.Count == 0)
			{
				return null;
			}
			foreach (EpmTargetPathSegment epmTargetPathSegment in syndicationRoot.SubSegments)
			{
				if (epmTargetPathSegment.HasContent)
				{
					EntityPropertyMappingInfo epmInfo = epmTargetPathSegment.EpmInfo;
					object obj = base.ReadEntryPropertyValue(epmInfo, epmValueCache, entityType);
					string propertyValueAsText = EpmWriterUtils.GetPropertyValueAsText(obj);
					switch (epmInfo.Attribute.TargetSyndicationItem)
					{
					case SyndicationItemProperty.Updated:
						if (base.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
						{
							this.entryMetadata.UpdatedString = EpmSyndicationWriter.CreateDateTimeStringValue(obj, base.WriterBehavior);
						}
						else
						{
							this.entryMetadata.Updated = new DateTimeOffset?(EpmSyndicationWriter.CreateDateTimeValue(obj, SyndicationItemProperty.Updated, base.WriterBehavior));
						}
						break;
					case SyndicationItemProperty.Published:
						if (base.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
						{
							this.entryMetadata.PublishedString = EpmSyndicationWriter.CreateDateTimeStringValue(obj, base.WriterBehavior);
						}
						else
						{
							this.entryMetadata.Published = new DateTimeOffset?(EpmSyndicationWriter.CreateDateTimeValue(obj, SyndicationItemProperty.Published, base.WriterBehavior));
						}
						break;
					case SyndicationItemProperty.Rights:
						this.entryMetadata.Rights = EpmSyndicationWriter.CreateAtomTextConstruct(propertyValueAsText, epmInfo.Attribute.TargetTextContentKind);
						break;
					case SyndicationItemProperty.Summary:
						this.entryMetadata.Summary = EpmSyndicationWriter.CreateAtomTextConstruct(propertyValueAsText, epmInfo.Attribute.TargetTextContentKind);
						break;
					case SyndicationItemProperty.Title:
						this.entryMetadata.Title = EpmSyndicationWriter.CreateAtomTextConstruct(propertyValueAsText, epmInfo.Attribute.TargetTextContentKind);
						break;
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationWriter_WriteEntryEpm_ContentTarget));
					}
				}
				else
				{
					this.WriteParentSegment(epmTargetPathSegment, epmValueCache, entityType);
				}
			}
			return this.entryMetadata;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00048178 File Offset: 0x00046378
		private void WriteParentSegment(EpmTargetPathSegment targetSegment, object epmValueCache, IEdmTypeReference typeReference)
		{
			if (targetSegment.SegmentName == "author")
			{
				AtomPersonMetadata atomPersonMetadata = this.WritePersonEpm(targetSegment, epmValueCache, typeReference);
				if (atomPersonMetadata != null)
				{
					List<AtomPersonMetadata> list = (List<AtomPersonMetadata>)this.entryMetadata.Authors;
					if (list == null)
					{
						list = new List<AtomPersonMetadata>();
						this.entryMetadata.Authors = list;
					}
					list.Add(atomPersonMetadata);
					return;
				}
			}
			else
			{
				if (!(targetSegment.SegmentName == "contributor"))
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationWriter_WriteParentSegment_TargetSegmentName));
				}
				AtomPersonMetadata atomPersonMetadata2 = this.WritePersonEpm(targetSegment, epmValueCache, typeReference);
				if (atomPersonMetadata2 != null)
				{
					List<AtomPersonMetadata> list2 = (List<AtomPersonMetadata>)this.entryMetadata.Contributors;
					if (list2 == null)
					{
						list2 = new List<AtomPersonMetadata>();
						this.entryMetadata.Contributors = list2;
					}
					list2.Add(atomPersonMetadata2);
					return;
				}
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00048234 File Offset: 0x00046434
		private AtomPersonMetadata WritePersonEpm(EpmTargetPathSegment targetSegment, object epmValueCache, IEdmTypeReference typeReference)
		{
			AtomPersonMetadata atomPersonMetadata = null;
			foreach (EpmTargetPathSegment epmTargetPathSegment in targetSegment.SubSegments)
			{
				string propertyValueAsText = this.GetPropertyValueAsText(epmTargetPathSegment, epmValueCache, typeReference);
				if (propertyValueAsText != null)
				{
					switch (epmTargetPathSegment.EpmInfo.Attribute.TargetSyndicationItem)
					{
					case SyndicationItemProperty.AuthorEmail:
					case SyndicationItemProperty.ContributorEmail:
						if (propertyValueAsText != null && propertyValueAsText.Length > 0)
						{
							if (atomPersonMetadata == null)
							{
								atomPersonMetadata = new AtomPersonMetadata();
							}
							atomPersonMetadata.Email = propertyValueAsText;
						}
						break;
					case SyndicationItemProperty.AuthorName:
					case SyndicationItemProperty.ContributorName:
						if (propertyValueAsText != null)
						{
							if (atomPersonMetadata == null)
							{
								atomPersonMetadata = new AtomPersonMetadata();
							}
							atomPersonMetadata.Name = propertyValueAsText;
						}
						break;
					case SyndicationItemProperty.AuthorUri:
					case SyndicationItemProperty.ContributorUri:
						if (propertyValueAsText != null && propertyValueAsText.Length > 0)
						{
							if (atomPersonMetadata == null)
							{
								atomPersonMetadata = new AtomPersonMetadata();
							}
							atomPersonMetadata.UriFromEpm = propertyValueAsText;
						}
						break;
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationWriter_WritePersonEpm));
					}
				}
			}
			return atomPersonMetadata;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00048334 File Offset: 0x00046534
		private string GetPropertyValueAsText(EpmTargetPathSegment targetSegment, object epmValueCache, IEdmTypeReference typeReference)
		{
			EntryPropertiesValueCache entryPropertiesValueCache = epmValueCache as EntryPropertiesValueCache;
			object obj;
			if (entryPropertiesValueCache != null)
			{
				obj = base.ReadEntryPropertyValue(targetSegment.EpmInfo, entryPropertiesValueCache, typeReference.AsEntity());
			}
			else
			{
				obj = epmValueCache;
				ValidationUtils.ValidateIsExpectedPrimitiveType(obj, typeReference);
			}
			return EpmWriterUtils.GetPropertyValueAsText(obj);
		}

		// Token: 0x04000726 RID: 1830
		private readonly EpmTargetTree epmTargetTree;

		// Token: 0x04000727 RID: 1831
		private readonly AtomEntryMetadata entryMetadata;
	}
}
