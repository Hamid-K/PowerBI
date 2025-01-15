using System;
using System.Collections.Generic;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x0200010F RID: 271
	internal class MaterializerEntry
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x0002B647 File Offset: 0x00029847
		private MaterializerEntry()
		{
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002B65C File Offset: 0x0002985C
		private MaterializerEntry(ODataResource entry, ODataFormat format, bool isTracking, ClientEdmModel model)
		{
			this.entry = entry;
			this.Format = format;
			this.entityDescriptor = new EntityDescriptor(model);
			this.isTracking = isTracking;
			string text = this.Entry.TypeName;
			if (entry.TypeAnnotation != null && (entry.TypeAnnotation.TypeName != null || this.Format != ODataFormat.Json))
			{
				text = entry.TypeAnnotation.TypeName;
			}
			this.entityDescriptor.ServerTypeName = text;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002B6E2 File Offset: 0x000298E2
		private MaterializerEntry(EntityDescriptor entityDescriptor, ODataFormat format, bool isTracking)
		{
			this.entityDescriptor = entityDescriptor;
			this.Format = format;
			this.isTracking = isTracking;
			this.SetFlagValue(MaterializerEntry.EntryFlags.ShouldUpdateFromPayload | MaterializerEntry.EntryFlags.EntityHasBeenResolved | MaterializerEntry.EntryFlags.ForLoadProperty, true);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002B713 File Offset: 0x00029913
		public ODataResource Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0002B71B File Offset: 0x0002991B
		public bool IsTracking
		{
			get
			{
				return this.isTracking;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002B723 File Offset: 0x00029923
		public Uri Id
		{
			get
			{
				return this.entry.Id;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0002B730 File Offset: 0x00029930
		public IEnumerable<ODataProperty> Properties
		{
			get
			{
				if (this.entry == null)
				{
					return null;
				}
				return this.entry.Properties;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002B747 File Offset: 0x00029947
		public EntityDescriptor EntityDescriptor
		{
			get
			{
				return this.entityDescriptor;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002B74F File Offset: 0x0002994F
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0002B766 File Offset: 0x00029966
		public object ResolvedObject
		{
			get
			{
				if (this.entityDescriptor == null)
				{
					return null;
				}
				return this.entityDescriptor.Entity;
			}
			set
			{
				this.entityDescriptor.Entity = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002B774 File Offset: 0x00029974
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0002B77C File Offset: 0x0002997C
		public ClientTypeAnnotation ActualType { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002B785 File Offset: 0x00029985
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0002B78E File Offset: 0x0002998E
		public bool ShouldUpdateFromPayload
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.ShouldUpdateFromPayload);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.ShouldUpdateFromPayload, value);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002B798 File Offset: 0x00029998
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0002B7A1 File Offset: 0x000299A1
		public bool EntityHasBeenResolved
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.EntityHasBeenResolved);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.EntityHasBeenResolved, value);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002B7AB File Offset: 0x000299AB
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x0002B7B4 File Offset: 0x000299B4
		public bool CreatedByMaterializer
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.CreatedByMaterializer);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.CreatedByMaterializer, value);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002B7BE File Offset: 0x000299BE
		public bool ForLoadProperty
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.ForLoadProperty);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002B7C8 File Offset: 0x000299C8
		public ICollection<ODataNestedResourceInfo> NestedResourceInfos
		{
			get
			{
				return this.navigationLinks;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0002B7D0 File Offset: 0x000299D0
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0002B7D8 File Offset: 0x000299D8
		internal ODataFormat Format { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002B7E1 File Offset: 0x000299E1
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0002B7EA File Offset: 0x000299EA
		private bool EntityDescriptorUpdated
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.EntityDescriptorUpdated);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.EntityDescriptorUpdated, value);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002B7F4 File Offset: 0x000299F4
		public static MaterializerEntry CreateEmpty()
		{
			return new MaterializerEntry();
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002B7FC File Offset: 0x000299FC
		public static MaterializerEntry CreateEntry(ODataResource entry, ODataFormat format, bool isTracking, ClientEdmModel model)
		{
			MaterializerEntry materializerEntry = new MaterializerEntry(entry, format, isTracking, model);
			entry.SetAnnotation(materializerEntry);
			return materializerEntry;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002B81B File Offset: 0x00029A1B
		public static MaterializerEntry CreateEntryForLoadProperty(EntityDescriptor descriptor, ODataFormat format, bool isTracking)
		{
			return new MaterializerEntry(descriptor, format, isTracking);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002B825 File Offset: 0x00029A25
		public static MaterializerEntry GetEntry(ODataResource entry)
		{
			return entry.GetAnnotation<MaterializerEntry>();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002B830 File Offset: 0x00029A30
		public void AddNestedResourceInfo(ODataNestedResourceInfo link)
		{
			if (this.IsTracking)
			{
				this.EntityDescriptor.AddNestedResourceInfo(link.Name, link.Url);
				Uri associationLinkUrl = link.AssociationLinkUrl;
				if (associationLinkUrl != null)
				{
					this.EntityDescriptor.AddAssociationLink(link.Name, associationLinkUrl);
				}
			}
			if (this.navigationLinks == ODataMaterializer.EmptyLinks)
			{
				this.navigationLinks = new List<ODataNestedResourceInfo>();
			}
			this.navigationLinks.Add(link);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002B8A4 File Offset: 0x00029AA4
		public void UpdateEntityDescriptor()
		{
			if (!this.EntityDescriptorUpdated)
			{
				foreach (ODataProperty odataProperty in this.Properties)
				{
					ODataStreamReferenceValue odataStreamReferenceValue = odataProperty.Value as ODataStreamReferenceValue;
					if (odataStreamReferenceValue != null)
					{
						StreamDescriptor streamDescriptor = this.EntityDescriptor.AddStreamInfoIfNotPresent(odataProperty.Name);
						if (odataStreamReferenceValue.ReadLink != null)
						{
							streamDescriptor.SelfLink = odataStreamReferenceValue.ReadLink;
						}
						if (odataStreamReferenceValue.EditLink != null)
						{
							streamDescriptor.EditLink = odataStreamReferenceValue.EditLink;
						}
						streamDescriptor.ETag = odataStreamReferenceValue.ETag;
						streamDescriptor.ContentType = odataStreamReferenceValue.ContentType;
					}
				}
				if (this.IsTracking)
				{
					this.Id == null;
					this.EntityDescriptor.Identity = this.entry.Id;
					this.EntityDescriptor.EditLink = this.entry.EditLink;
					this.EntityDescriptor.SelfLink = this.entry.ReadLink;
					this.EntityDescriptor.ETag = this.entry.ETag;
					if (this.entry.MediaResource != null)
					{
						if (this.entry.MediaResource.ReadLink != null)
						{
							this.EntityDescriptor.ReadStreamUri = this.entry.MediaResource.ReadLink;
						}
						if (this.entry.MediaResource.EditLink != null)
						{
							this.EntityDescriptor.EditStreamUri = this.entry.MediaResource.EditLink;
						}
						if (this.entry.MediaResource.ETag != null)
						{
							this.EntityDescriptor.StreamETag = this.entry.MediaResource.ETag;
						}
					}
					if (this.entry.Functions != null)
					{
						foreach (ODataFunction odataFunction in this.entry.Functions)
						{
							this.EntityDescriptor.AddOperationDescriptor(new FunctionDescriptor
							{
								Title = odataFunction.Title,
								Metadata = odataFunction.Metadata,
								Target = odataFunction.Target
							});
						}
					}
					if (this.entry.Actions != null)
					{
						foreach (ODataAction odataAction in this.entry.Actions)
						{
							this.EntityDescriptor.AddOperationDescriptor(new ActionDescriptor
							{
								Title = odataAction.Title,
								Metadata = odataAction.Metadata,
								Target = odataAction.Target
							});
						}
					}
				}
				this.EntityDescriptorUpdated = true;
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002BB84 File Offset: 0x00029D84
		private bool GetFlagValue(MaterializerEntry.EntryFlags mask)
		{
			return (this.flags & mask) > (MaterializerEntry.EntryFlags)0;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002BB91 File Offset: 0x00029D91
		private void SetFlagValue(MaterializerEntry.EntryFlags mask, bool value)
		{
			if (value)
			{
				this.flags |= mask;
				return;
			}
			this.flags &= ~mask;
		}

		// Token: 0x04000643 RID: 1603
		private readonly ODataResource entry;

		// Token: 0x04000644 RID: 1604
		private readonly EntityDescriptor entityDescriptor;

		// Token: 0x04000645 RID: 1605
		private readonly bool isTracking;

		// Token: 0x04000646 RID: 1606
		private MaterializerEntry.EntryFlags flags;

		// Token: 0x04000647 RID: 1607
		private ICollection<ODataNestedResourceInfo> navigationLinks = ODataMaterializer.EmptyLinks;

		// Token: 0x020001DE RID: 478
		[Flags]
		private enum EntryFlags
		{
			// Token: 0x0400083F RID: 2111
			ShouldUpdateFromPayload = 1,
			// Token: 0x04000840 RID: 2112
			CreatedByMaterializer = 2,
			// Token: 0x04000841 RID: 2113
			EntityHasBeenResolved = 4,
			// Token: 0x04000842 RID: 2114
			EntityDescriptorUpdated = 8,
			// Token: 0x04000843 RID: 2115
			ForLoadProperty = 16
		}
	}
}
