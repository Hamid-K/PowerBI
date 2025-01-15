using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E0 RID: 224
	public sealed class StreamDescriptor : Descriptor
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0001FCBF File Offset: 0x0001DEBF
		internal StreamDescriptor(string name, EntityDescriptor entityDescriptor)
			: base(EntityStates.Unchanged)
		{
			this.streamLink = new DataServiceStreamLink(name);
			this.entityDescriptor = entityDescriptor;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001FCDB File Offset: 0x0001DEDB
		internal StreamDescriptor(EntityDescriptor entityDescriptor)
			: base(EntityStates.Unchanged)
		{
			this.streamLink = new DataServiceStreamLink(null);
			this.entityDescriptor = entityDescriptor;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		public DataServiceStreamLink StreamLink
		{
			get
			{
				return this.streamLink;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001FCFF File Offset: 0x0001DEFF
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001FD07 File Offset: 0x0001DF07
		public EntityDescriptor EntityDescriptor
		{
			get
			{
				return this.entityDescriptor;
			}
			set
			{
				this.entityDescriptor = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001FD10 File Offset: 0x0001DF10
		internal string Name
		{
			get
			{
				return this.streamLink.Name;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001FD1D File Offset: 0x0001DF1D
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0001FD2A File Offset: 0x0001DF2A
		internal Uri SelfLink
		{
			get
			{
				return this.streamLink.SelfLink;
			}
			set
			{
				this.streamLink.SelfLink = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001FD38 File Offset: 0x0001DF38
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0001FD45 File Offset: 0x0001DF45
		internal Uri EditLink
		{
			get
			{
				return this.streamLink.EditLink;
			}
			set
			{
				this.streamLink.EditLink = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001FD53 File Offset: 0x0001DF53
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0001FD60 File Offset: 0x0001DF60
		internal string ContentType
		{
			get
			{
				return this.streamLink.ContentType;
			}
			set
			{
				this.streamLink.ContentType = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001FD6E File Offset: 0x0001DF6E
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0001FD7B File Offset: 0x0001DF7B
		internal string ETag
		{
			get
			{
				return this.streamLink.ETag;
			}
			set
			{
				this.streamLink.ETag = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001FD89 File Offset: 0x0001DF89
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0001FD91 File Offset: 0x0001DF91
		internal DataServiceSaveStream SaveStream { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0001FD9A File Offset: 0x0001DF9A
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.NamedStream;
			}
		}

		// Token: 0x170001AB RID: 427
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x0001FD9D File Offset: 0x0001DF9D
		internal StreamDescriptor TransientNamedStreamInfo
		{
			set
			{
				if (this.transientNamedStreamInfo == null)
				{
					this.transientNamedStreamInfo = value;
					return;
				}
				StreamDescriptor.MergeStreamDescriptor(this.transientNamedStreamInfo, value);
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		internal static void MergeStreamDescriptor(StreamDescriptor existingStreamDescriptor, StreamDescriptor newStreamDescriptor)
		{
			if (newStreamDescriptor.SelfLink != null)
			{
				existingStreamDescriptor.SelfLink = newStreamDescriptor.SelfLink;
			}
			if (newStreamDescriptor.EditLink != null)
			{
				existingStreamDescriptor.EditLink = newStreamDescriptor.EditLink;
			}
			if (newStreamDescriptor.ContentType != null)
			{
				existingStreamDescriptor.ContentType = newStreamDescriptor.ContentType;
			}
			if (newStreamDescriptor.ETag != null)
			{
				existingStreamDescriptor.ETag = newStreamDescriptor.ETag;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001FE25 File Offset: 0x0001E025
		internal override void ClearChanges()
		{
			this.transientNamedStreamInfo = null;
			this.CloseSaveStream();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001FE34 File Offset: 0x0001E034
		internal Uri GetLatestEditLink()
		{
			if (this.transientNamedStreamInfo != null && this.transientNamedStreamInfo.EditLink != null)
			{
				return this.transientNamedStreamInfo.EditLink;
			}
			return this.EditLink;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001FE63 File Offset: 0x0001E063
		internal string GetLatestETag()
		{
			if (this.transientNamedStreamInfo != null && this.transientNamedStreamInfo.ETag != null)
			{
				return this.transientNamedStreamInfo.ETag;
			}
			return this.ETag;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001FE8C File Offset: 0x0001E08C
		internal void CloseSaveStream()
		{
			if (this.SaveStream != null)
			{
				DataServiceSaveStream saveStream = this.SaveStream;
				this.SaveStream = null;
				saveStream.Close();
			}
		}

		// Token: 0x0400035E RID: 862
		private DataServiceStreamLink streamLink;

		// Token: 0x0400035F RID: 863
		private EntityDescriptor entityDescriptor;

		// Token: 0x04000360 RID: 864
		private StreamDescriptor transientNamedStreamInfo;
	}
}
