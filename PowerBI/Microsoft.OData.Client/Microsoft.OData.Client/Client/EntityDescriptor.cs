using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x020000DA RID: 218
	[DebuggerDisplay("State = {state}, Uri = {editLink}, Element = {entity.GetType().ToString()}")]
	public sealed class EntityDescriptor : Descriptor
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x0001D271 File Offset: 0x0001B471
		internal EntityDescriptor(ClientEdmModel model)
			: base(EntityStates.Unchanged)
		{
			this.Model = model;
			this.PropertiesToSerialize = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001D291 File Offset: 0x0001B491
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001D299 File Offset: 0x0001B499
		public Uri Identity
		{
			get
			{
				return this.identity;
			}
			internal set
			{
				this.identity = value;
				this.ParentForInsert = null;
				this.ParentPropertyForInsert = null;
				this.ParentForUpdate = null;
				this.ParentPropertyForUpdate = null;
				this.addToUri = null;
				this.identity = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		public Uri SelfLink
		{
			get
			{
				return this.selfLink;
			}
			internal set
			{
				this.selfLink = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001D2DD File Offset: 0x0001B4DD
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0001D2E5 File Offset: 0x0001B4E5
		public Uri EditLink
		{
			get
			{
				return this.editLink;
			}
			internal set
			{
				this.editLink = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001D2EE File Offset: 0x0001B4EE
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001D305 File Offset: 0x0001B505
		public Uri ReadStreamUri
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.SelfLink;
			}
			internal set
			{
				if (value != null)
				{
					this.CreateDefaultStreamDescriptor().SelfLink = value;
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001D31C File Offset: 0x0001B51C
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001D333 File Offset: 0x0001B533
		public Uri EditStreamUri
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.EditLink;
			}
			internal set
			{
				if (value != null)
				{
					this.CreateDefaultStreamDescriptor().EditLink = value;
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001D34A File Offset: 0x0001B54A
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001D354 File Offset: 0x0001B554
		public object Entity
		{
			get
			{
				return this.entity;
			}
			internal set
			{
				this.entity = value;
				if (value != null)
				{
					IEdmType orCreateEdmType = this.Model.GetOrCreateEdmType(value.GetType());
					ClientTypeAnnotation clientTypeAnnotation = this.Model.GetClientTypeAnnotation(orCreateEdmType);
					this.EdmValue = new ClientEdmStructuredValue(value, this.Model, clientTypeAnnotation);
					if (clientTypeAnnotation.IsMediaLinkEntry)
					{
						this.CreateDefaultStreamDescriptor();
					}
				}
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		public string ETag { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001D3BD File Offset: 0x0001B5BD
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
		public string StreamETag
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.ETag;
			}
			internal set
			{
				this.CreateDefaultStreamDescriptor().ETag = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001D3EA File Offset: 0x0001B5EA
		public EntityDescriptor ParentForInsert { get; internal set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001D3F3 File Offset: 0x0001B5F3
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001D3FB File Offset: 0x0001B5FB
		public string ParentPropertyForInsert { get; internal set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001D404 File Offset: 0x0001B604
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001D40C File Offset: 0x0001B60C
		public EntityDescriptor ParentForUpdate { get; internal set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001D415 File Offset: 0x0001B615
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001D41D File Offset: 0x0001B61D
		public string ParentPropertyForUpdate { get; internal set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001D426 File Offset: 0x0001B626
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001D42E File Offset: 0x0001B62E
		public string ServerTypeName { get; internal set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001D437 File Offset: 0x0001B637
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "LinkInfoCollection is cumbersome and Links isn't expressive enough")]
		public ReadOnlyCollection<LinkInfo> LinkInfos
		{
			get
			{
				if (this.relatedEntityLinks != null)
				{
					return new ReadOnlyCollection<LinkInfo>(this.relatedEntityLinks.Values.ToList<LinkInfo>());
				}
				return new ReadOnlyCollection<LinkInfo>(new List<LinkInfo>(0));
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001D462 File Offset: 0x0001B662
		public ReadOnlyCollection<StreamDescriptor> StreamDescriptors
		{
			get
			{
				if (this.streamDescriptors != null)
				{
					return new ReadOnlyCollection<StreamDescriptor>(this.streamDescriptors.Values.ToList<StreamDescriptor>());
				}
				return new ReadOnlyCollection<StreamDescriptor>(new List<StreamDescriptor>(0));
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001D48D File Offset: 0x0001B68D
		public ReadOnlyCollection<OperationDescriptor> OperationDescriptors
		{
			get
			{
				if (this.operationDescriptors != null)
				{
					return new ReadOnlyCollection<OperationDescriptor>(this.operationDescriptors);
				}
				return new ReadOnlyCollection<OperationDescriptor>(new List<OperationDescriptor>());
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001D4AD File Offset: 0x0001B6AD
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001D4B5 File Offset: 0x0001B6B5
		internal ClientEdmModel Model { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001D4BE File Offset: 0x0001B6BE
		internal object ParentEntity
		{
			get
			{
				if (this.ParentEntityDescriptor == null)
				{
					return null;
				}
				return this.ParentEntityDescriptor.entity;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001D4D5 File Offset: 0x0001B6D5
		internal EntityDescriptor ParentEntityDescriptor
		{
			get
			{
				return this.ParentForInsert ?? this.ParentForUpdate;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001D4E7 File Offset: 0x0001B6E7
		internal string ParentProperty
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ParentPropertyForInsert))
				{
					return this.ParentPropertyForInsert;
				}
				if (string.IsNullOrEmpty(this.ParentPropertyForUpdate))
				{
					return null;
				}
				return this.ParentPropertyForUpdate;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00015066 File Offset: 0x00013266
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.Entity;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001D512 File Offset: 0x0001B712
		internal bool IsDeepInsert
		{
			get
			{
				return this.ParentForInsert != null;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001D51D File Offset: 0x0001B71D
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001D534 File Offset: 0x0001B734
		internal DataServiceSaveStream SaveStream
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.SaveStream;
			}
			set
			{
				this.CreateDefaultStreamDescriptor().SaveStream = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001D542 File Offset: 0x0001B742
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001D559 File Offset: 0x0001B759
		internal EntityStates StreamState
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return EntityStates.Unchanged;
				}
				return this.defaultStreamDescriptor.State;
			}
			set
			{
				this.defaultStreamDescriptor.State = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001D567 File Offset: 0x0001B767
		internal bool IsMediaLinkEntry
		{
			get
			{
				return this.defaultStreamDescriptor != null;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001D572 File Offset: 0x0001B772
		internal override bool IsModified
		{
			get
			{
				return base.IsModified || (this.defaultStreamDescriptor != null && this.defaultStreamDescriptor.SaveStream != null);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001D596 File Offset: 0x0001B796
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
		internal EntityDescriptor TransientEntityDescriptor
		{
			get
			{
				return this.transientEntityDescriptor;
			}
			set
			{
				if (this.transientEntityDescriptor == null)
				{
					this.transientEntityDescriptor = value;
				}
				else
				{
					AtomMaterializerLog.MergeEntityDescriptorInfo(this.transientEntityDescriptor, value, true, MergeOption.OverwriteChanges);
				}
				if (value.streamDescriptors != null && this.streamDescriptors != null)
				{
					foreach (StreamDescriptor streamDescriptor in value.streamDescriptors.Values)
					{
						StreamDescriptor streamDescriptor2;
						if (this.streamDescriptors.TryGetValue(streamDescriptor.Name, out streamDescriptor2))
						{
							streamDescriptor2.TransientNamedStreamInfo = streamDescriptor;
						}
					}
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001D63C File Offset: 0x0001B83C
		internal StreamDescriptor DefaultStreamDescriptor
		{
			get
			{
				return this.defaultStreamDescriptor;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001D644 File Offset: 0x0001B844
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001D64C File Offset: 0x0001B84C
		internal IEdmStructuredValue EdmValue { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001D655 File Offset: 0x0001B855
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001D65D File Offset: 0x0001B85D
		internal string EntitySetName { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001D666 File Offset: 0x0001B866
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001D66E File Offset: 0x0001B86E
		internal HashSet<string> PropertiesToSerialize { get; set; }

		// Token: 0x06000740 RID: 1856 RVA: 0x0001D677 File Offset: 0x0001B877
		internal Uri GetLatestIdentity()
		{
			if (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.Identity != null)
			{
				return this.TransientEntityDescriptor.Identity;
			}
			return this.Identity;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001D6A6 File Offset: 0x0001B8A6
		internal Uri GetLatestEditLink()
		{
			if (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.EditLink != null)
			{
				return this.TransientEntityDescriptor.EditLink;
			}
			return this.EditLink;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		internal Uri GetLatestEditStreamUri()
		{
			if (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.EditStreamUri != null)
			{
				return this.TransientEntityDescriptor.EditStreamUri;
			}
			return this.EditStreamUri;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001D704 File Offset: 0x0001B904
		internal string GetLatestETag()
		{
			if (this.TransientEntityDescriptor != null && !string.IsNullOrEmpty(this.TransientEntityDescriptor.ETag))
			{
				return this.TransientEntityDescriptor.ETag;
			}
			return this.ETag;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001D732 File Offset: 0x0001B932
		internal string GetLatestStreamETag()
		{
			if (this.TransientEntityDescriptor != null && !string.IsNullOrEmpty(this.TransientEntityDescriptor.StreamETag))
			{
				return this.TransientEntityDescriptor.StreamETag;
			}
			return this.StreamETag;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001D760 File Offset: 0x0001B960
		internal string GetLatestServerTypeName()
		{
			if (this.TransientEntityDescriptor != null && !string.IsNullOrEmpty(this.TransientEntityDescriptor.ServerTypeName))
			{
				return this.TransientEntityDescriptor.ServerTypeName;
			}
			return this.ServerTypeName;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001D790 File Offset: 0x0001B990
		internal Uri GetResourceUri(UriResolver baseUriResolver, bool queryLink)
		{
			if (this.ParentEntityDescriptor == null)
			{
				return this.GetLink(queryLink);
			}
			if (this.ParentEntityDescriptor.Identity == null)
			{
				Uri uri = UriUtil.CreateUri("$" + this.ParentEntityDescriptor.ChangeOrder.ToString(CultureInfo.InvariantCulture), UriKind.Relative);
				Uri orCreateAbsoluteUri = baseUriResolver.GetOrCreateAbsoluteUri(uri);
				Uri uri2 = UriUtil.CreateUri(this.ParentProperty, UriKind.Relative);
				return UriUtil.CreateUri(orCreateAbsoluteUri, uri2);
			}
			LinkInfo linkInfo;
			if (this.ParentEntityDescriptor.TryGetLinkInfo(this.ParentProperty, out linkInfo) && linkInfo.NavigationLink != null)
			{
				return linkInfo.NavigationLink;
			}
			return UriUtil.CreateUri(this.ParentEntityDescriptor.GetLink(queryLink), this.GetLink(queryLink));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001D84D File Offset: 0x0001BA4D
		internal bool IsRelatedEntity(LinkDescriptor related)
		{
			return this.entity == related.Source || this.entity == related.Target;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001D86D File Offset: 0x0001BA6D
		internal LinkDescriptor GetRelatedEnd()
		{
			return new LinkDescriptor(this.ParentForInsert.entity, this.ParentPropertyForInsert, this.entity, this.Model);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001D891 File Offset: 0x0001BA91
		internal override void ClearChanges()
		{
			this.transientEntityDescriptor = null;
			this.CloseSaveStream();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		internal void CloseSaveStream()
		{
			if (this.defaultStreamDescriptor != null)
			{
				this.defaultStreamDescriptor.CloseSaveStream();
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
		internal void AddNestedResourceInfo(string propertyName, Uri navigationUri)
		{
			LinkInfo linkInfo = this.GetLinkInfo(propertyName);
			linkInfo.NavigationLink = navigationUri;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		internal void AddAssociationLink(string propertyName, Uri associationUri)
		{
			LinkInfo linkInfo = this.GetLinkInfo(propertyName);
			linkInfo.AssociationLink = associationUri;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
		internal void MergeLinkInfo(LinkInfo linkInfo)
		{
			if (this.relatedEntityLinks == null)
			{
				this.relatedEntityLinks = new Dictionary<string, LinkInfo>(StringComparer.Ordinal);
			}
			LinkInfo linkInfo2 = null;
			if (!this.relatedEntityLinks.TryGetValue(linkInfo.Name, out linkInfo2))
			{
				this.relatedEntityLinks[linkInfo.Name] = linkInfo;
				return;
			}
			if (linkInfo.AssociationLink != null)
			{
				linkInfo2.AssociationLink = linkInfo.AssociationLink;
			}
			if (linkInfo.NavigationLink != null)
			{
				linkInfo2.NavigationLink = linkInfo.NavigationLink;
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001D974 File Offset: 0x0001BB74
		internal Uri GetNestedResourceInfo(UriResolver baseUriResolver, ClientPropertyAnnotation property)
		{
			LinkInfo linkInfo = null;
			Uri uri = null;
			if (this.TryGetLinkInfo(property.PropertyName, out linkInfo))
			{
				uri = linkInfo.NavigationLink;
			}
			if (uri == null)
			{
				Uri uri2 = UriUtil.CreateUri(property.PropertyName, UriKind.Relative);
				uri = UriUtil.CreateUri(this.GetResourceUri(baseUriResolver, true), uri2);
			}
			return uri;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001D9C2 File Offset: 0x0001BBC2
		internal bool TryGetLinkInfo(string propertyName, out LinkInfo linkInfo)
		{
			Util.CheckArgumentNullAndEmpty(propertyName, "propertyName");
			linkInfo = null;
			return (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.TryGetLinkInfo(propertyName, out linkInfo)) || (this.relatedEntityLinks != null && this.relatedEntityLinks.TryGetValue(propertyName, out linkInfo));
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001DA04 File Offset: 0x0001BC04
		internal StreamDescriptor AddStreamInfoIfNotPresent(string name)
		{
			if (this.streamDescriptors == null)
			{
				this.streamDescriptors = new Dictionary<string, StreamDescriptor>(StringComparer.Ordinal);
			}
			StreamDescriptor streamDescriptor;
			if (!this.streamDescriptors.TryGetValue(name, out streamDescriptor))
			{
				streamDescriptor = new StreamDescriptor(name, this);
				this.streamDescriptors.Add(name, streamDescriptor);
			}
			return streamDescriptor;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001DA4F File Offset: 0x0001BC4F
		internal void AddOperationDescriptor(OperationDescriptor operationDescriptor)
		{
			if (this.operationDescriptors == null)
			{
				this.operationDescriptors = new List<OperationDescriptor>();
			}
			this.operationDescriptors.Add(operationDescriptor);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001DA70 File Offset: 0x0001BC70
		internal void ClearOperationDescriptors()
		{
			if (this.operationDescriptors != null)
			{
				this.operationDescriptors.Clear();
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001DA85 File Offset: 0x0001BC85
		internal void AppendOperationalDescriptors(IEnumerable<OperationDescriptor> descriptors)
		{
			if (this.operationDescriptors == null)
			{
				this.operationDescriptors = new List<OperationDescriptor>();
			}
			this.operationDescriptors.AddRange(descriptors);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001DAA6 File Offset: 0x0001BCA6
		internal bool TryGetNamedStreamInfo(string name, out StreamDescriptor namedStreamInfo)
		{
			namedStreamInfo = null;
			return this.streamDescriptors != null && this.streamDescriptors.TryGetValue(name, out namedStreamInfo);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		internal void MergeStreamDescriptor(StreamDescriptor materializedStreamDescriptor)
		{
			if (this.streamDescriptors == null)
			{
				this.streamDescriptors = new Dictionary<string, StreamDescriptor>(StringComparer.Ordinal);
			}
			StreamDescriptor streamDescriptor = null;
			if (!this.streamDescriptors.TryGetValue(materializedStreamDescriptor.Name, out streamDescriptor))
			{
				this.streamDescriptors[materializedStreamDescriptor.Name] = materializedStreamDescriptor;
				materializedStreamDescriptor.EntityDescriptor = this;
				return;
			}
			StreamDescriptor.MergeStreamDescriptor(streamDescriptor, materializedStreamDescriptor);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001DB21 File Offset: 0x0001BD21
		internal void SetParentForInsert(EntityDescriptor parentDescriptor, string propertyForInsert)
		{
			this.ParentForInsert = parentDescriptor;
			this.ParentPropertyForInsert = propertyForInsert;
			this.ParentForUpdate = null;
			this.ParentPropertyForUpdate = null;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001DB3F File Offset: 0x0001BD3F
		internal void SetParentForUpdate(EntityDescriptor parentDescriptor, string propertyForUpdate)
		{
			this.ParentForUpdate = parentDescriptor;
			this.ParentPropertyForUpdate = propertyForUpdate;
			this.ParentForInsert = null;
			this.ParentPropertyForInsert = null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001DB5D File Offset: 0x0001BD5D
		internal void SetEntitySetUriForInsert(Uri entitySetInsertUri)
		{
			this.addToUri = entitySetInsertUri;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001DB68 File Offset: 0x0001BD68
		private LinkInfo GetLinkInfo(string propertyName)
		{
			if (this.relatedEntityLinks == null)
			{
				this.relatedEntityLinks = new Dictionary<string, LinkInfo>(StringComparer.Ordinal);
			}
			LinkInfo linkInfo = null;
			if (!this.relatedEntityLinks.TryGetValue(propertyName, out linkInfo))
			{
				linkInfo = new LinkInfo(propertyName);
				this.relatedEntityLinks[propertyName] = linkInfo;
			}
			return linkInfo;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		private Uri GetLink(bool queryLink)
		{
			if (queryLink && this.SelfLink != null)
			{
				return this.SelfLink;
			}
			Uri latestEditLink;
			if ((latestEditLink = this.GetLatestEditLink()) != null)
			{
				return latestEditLink;
			}
			if (base.State != EntityStates.Added)
			{
				throw new ArgumentNullException(Strings.EntityDescriptor_MissingSelfEditLink(this.identity));
			}
			if (this.addToUri != null)
			{
				return this.addToUri;
			}
			return UriUtil.CreateUri(this.ParentPropertyForInsert, UriKind.Relative);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001DC26 File Offset: 0x0001BE26
		private StreamDescriptor CreateDefaultStreamDescriptor()
		{
			if (this.defaultStreamDescriptor == null)
			{
				this.defaultStreamDescriptor = new StreamDescriptor(this);
			}
			return this.defaultStreamDescriptor;
		}

		// Token: 0x04000333 RID: 819
		private Uri identity;

		// Token: 0x04000334 RID: 820
		private object entity;

		// Token: 0x04000335 RID: 821
		private StreamDescriptor defaultStreamDescriptor;

		// Token: 0x04000336 RID: 822
		private Uri addToUri;

		// Token: 0x04000337 RID: 823
		private Uri selfLink;

		// Token: 0x04000338 RID: 824
		private Uri editLink;

		// Token: 0x04000339 RID: 825
		private Dictionary<string, LinkInfo> relatedEntityLinks;

		// Token: 0x0400033A RID: 826
		private EntityDescriptor transientEntityDescriptor;

		// Token: 0x0400033B RID: 827
		private Dictionary<string, StreamDescriptor> streamDescriptors;

		// Token: 0x0400033C RID: 828
		private List<OperationDescriptor> operationDescriptors;
	}
}
