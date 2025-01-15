using System;
using System.Data.Services.Common;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000216 RID: 534
	internal sealed class EpmSyndicationReader : EpmReader
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x00039B5C File Offset: 0x00037D5C
		private EpmSyndicationReader(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
			: base(entryState, inputContext)
		{
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00039B68 File Offset: 0x00037D68
		internal static void ReadEntryEpm(IODataAtomReaderEntryState entryState, ODataAtomInputContext inputContext)
		{
			EpmSyndicationReader epmSyndicationReader = new EpmSyndicationReader(entryState, inputContext);
			epmSyndicationReader.ReadEntryEpm();
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00039B84 File Offset: 0x00037D84
		private void ReadEntryEpm()
		{
			AtomEntryMetadata atomEntryMetadata = base.EntryState.AtomEntryMetadata;
			EpmTargetPathSegment syndicationRoot = base.EntryState.CachedEpm.EpmTargetTree.SyndicationRoot;
			if (syndicationRoot.SubSegments.Count == 0)
			{
				return;
			}
			foreach (EpmTargetPathSegment epmTargetPathSegment in syndicationRoot.SubSegments)
			{
				if (epmTargetPathSegment.HasContent)
				{
					this.ReadPropertyValueSegment(epmTargetPathSegment, atomEntryMetadata);
				}
				else
				{
					this.ReadParentSegment(epmTargetPathSegment, atomEntryMetadata);
				}
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00039C1C File Offset: 0x00037E1C
		private void ReadPropertyValueSegment(EpmTargetPathSegment targetSegment, AtomEntryMetadata entryMetadata)
		{
			EntityPropertyMappingInfo epmInfo = targetSegment.EpmInfo;
			switch (epmInfo.Attribute.TargetSyndicationItem)
			{
			case SyndicationItemProperty.Updated:
				if (base.MessageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
				{
					if (entryMetadata.UpdatedString != null)
					{
						base.SetEntryEpmValue(targetSegment.EpmInfo, entryMetadata.UpdatedString);
						return;
					}
				}
				else if (entryMetadata.Updated != null)
				{
					base.SetEntryEpmValue(targetSegment.EpmInfo, XmlConvert.ToString(entryMetadata.Updated.Value));
					return;
				}
				break;
			case SyndicationItemProperty.Published:
				if (base.MessageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
				{
					if (entryMetadata.PublishedString != null)
					{
						base.SetEntryEpmValue(targetSegment.EpmInfo, entryMetadata.PublishedString);
						return;
					}
				}
				else if (entryMetadata.Published != null)
				{
					base.SetEntryEpmValue(targetSegment.EpmInfo, XmlConvert.ToString(entryMetadata.Published.Value));
					return;
				}
				break;
			case SyndicationItemProperty.Rights:
				this.ReadTextConstructEpm(targetSegment, entryMetadata.Rights);
				return;
			case SyndicationItemProperty.Summary:
				this.ReadTextConstructEpm(targetSegment, entryMetadata.Summary);
				return;
			case SyndicationItemProperty.Title:
				this.ReadTextConstructEpm(targetSegment, entryMetadata.Title);
				return;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationReader_ReadEntryEpm_ContentTarget));
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00039D60 File Offset: 0x00037F60
		private void ReadParentSegment(EpmTargetPathSegment targetSegment, AtomEntryMetadata entryMetadata)
		{
			string segmentName;
			if ((segmentName = targetSegment.SegmentName) != null)
			{
				if (!(segmentName == "author"))
				{
					if (!(segmentName == "contributor"))
					{
						goto IL_00B3;
					}
					AtomPersonMetadata atomPersonMetadata = Enumerable.FirstOrDefault<AtomPersonMetadata>(entryMetadata.Contributors);
					if (atomPersonMetadata != null)
					{
						this.ReadPersonEpm(base.EntryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), base.EntryState.EntityType.ToTypeReference(), targetSegment, atomPersonMetadata);
						return;
					}
				}
				else
				{
					AtomPersonMetadata atomPersonMetadata2 = Enumerable.FirstOrDefault<AtomPersonMetadata>(entryMetadata.Authors);
					if (atomPersonMetadata2 != null)
					{
						this.ReadPersonEpm(base.EntryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), base.EntryState.EntityType.ToTypeReference(), targetSegment, atomPersonMetadata2);
						return;
					}
				}
				return;
			}
			IL_00B3:
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationReader_ReadParentSegment_TargetSegmentName));
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00039E34 File Offset: 0x00038034
		private void ReadPersonEpm(ReadOnlyEnumerable<ODataProperty> targetList, IEdmTypeReference targetTypeReference, EpmTargetPathSegment targetSegment, AtomPersonMetadata personMetadata)
		{
			foreach (EpmTargetPathSegment epmTargetPathSegment in targetSegment.SubSegments)
			{
				switch (epmTargetPathSegment.EpmInfo.Attribute.TargetSyndicationItem)
				{
				case SyndicationItemProperty.AuthorEmail:
				case SyndicationItemProperty.ContributorEmail:
				{
					string email = personMetadata.Email;
					if (email != null)
					{
						base.SetEpmValue(targetList, targetTypeReference, epmTargetPathSegment.EpmInfo, email);
					}
					break;
				}
				case SyndicationItemProperty.AuthorName:
				case SyndicationItemProperty.ContributorName:
				{
					string name = personMetadata.Name;
					if (name != null)
					{
						base.SetEpmValue(targetList, targetTypeReference, epmTargetPathSegment.EpmInfo, name);
					}
					break;
				}
				case SyndicationItemProperty.AuthorUri:
				case SyndicationItemProperty.ContributorUri:
				{
					string uriFromEpm = personMetadata.UriFromEpm;
					if (uriFromEpm != null)
					{
						base.SetEpmValue(targetList, targetTypeReference, epmTargetPathSegment.EpmInfo, uriFromEpm);
					}
					break;
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmSyndicationReader_ReadPersonEpm));
				}
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00039F24 File Offset: 0x00038124
		private void ReadTextConstructEpm(EpmTargetPathSegment targetSegment, AtomTextConstruct textConstruct)
		{
			if (textConstruct != null && textConstruct.Text != null)
			{
				base.SetEntryEpmValue(targetSegment.EpmInfo, textConstruct.Text);
			}
		}
	}
}
