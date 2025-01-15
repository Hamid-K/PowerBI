using System;
using System.ComponentModel;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000051 RID: 81
	public sealed class ReportSettings : IBinarySerializable
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00007898 File Offset: 0x00005A98
		public ReportSettings()
		{
			this.ShowHiddenFields = false;
			this.IsRelationshipAutodetectionEnabled = true;
			this.IsParallelQueryLoadingEnabled = true;
			this.IsAutoRecoveryEnabledForThisFile = true;
			this.IsQnaEnabledForThisFile = true;
			this.UserConsentsToCompositeModels = false;
			this.UserConsentsToQnaForLiveConnect = false;
			this.IsQnaEnabledForLiveConnect = false;
			this.UserConsentsToDynamicQueryParameter = false;
			this.QnaLsdlSharingPermission = 0;
			this.ShouldNotifyUserOfNameConflictResolution = true;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000078F8 File Offset: 0x00005AF8
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00007900 File Offset: 0x00005B00
		public bool ShowHiddenFields { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007909 File Offset: 0x00005B09
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00007911 File Offset: 0x00005B11
		[DefaultValue(true)]
		public bool IsRelationshipAutodetectionEnabled { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000791A File Offset: 0x00005B1A
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00007922 File Offset: 0x00005B22
		[DefaultValue(true)]
		public bool IsParallelQueryLoadingEnabled { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000792B File Offset: 0x00005B2B
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00007933 File Offset: 0x00005B33
		[DefaultValue(true)]
		public bool IsAutoRecoveryEnabledForThisFile { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000793C File Offset: 0x00005B3C
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00007944 File Offset: 0x00005B44
		[DefaultValue(true)]
		public bool IsQnaEnabledForThisFile { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000794D File Offset: 0x00005B4D
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00007955 File Offset: 0x00005B55
		public bool IsQnaEnabledForLiveConnect { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000795E File Offset: 0x00005B5E
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00007966 File Offset: 0x00005B66
		public bool UserConsentsToCompositeModels { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000796F File Offset: 0x00005B6F
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00007977 File Offset: 0x00005B77
		public bool UserConsentsToDynamicQueryParameter { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007980 File Offset: 0x00005B80
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00007988 File Offset: 0x00005B88
		public bool UserConsentsToQnaForLiveConnect { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00007991 File Offset: 0x00005B91
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00007999 File Offset: 0x00005B99
		[DefaultValue(0)]
		public int QnaLsdlSharingPermission { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000079A2 File Offset: 0x00005BA2
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000079AA File Offset: 0x00005BAA
		[DefaultValue(true)]
		public bool ShouldNotifyUserOfNameConflictResolution { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000079B3 File Offset: 0x00005BB3
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000079BB File Offset: 0x00005BBB
		public string RemoteModelingObjectId { get; set; }

		// Token: 0x0600027B RID: 635 RVA: 0x000079C4 File Offset: 0x00005BC4
		public void Deserialize(BinarySerializationReader reader)
		{
			if (reader.ReadInt() == 0)
			{
				this.ShowHiddenFields = reader.ReadBool();
				this.IsRelationshipAutodetectionEnabled = reader.ReadBool();
			}
			if (!reader.EndOfStream)
			{
				int num = reader.ReadInt();
				if (num >= 1)
				{
					this.IsParallelQueryLoadingEnabled = reader.ReadBool();
				}
				if (num >= 2)
				{
					this.IsAutoRecoveryEnabledForThisFile = reader.ReadBool();
				}
				if (num >= 3)
				{
					this.IsQnaEnabledForThisFile = reader.ReadBool();
				}
				if (num >= 4)
				{
					this.UserConsentsToCompositeModels = reader.ReadBool();
				}
				if (num >= 5)
				{
					this.UserConsentsToQnaForLiveConnect = reader.ReadBool();
				}
			}
		}
	}
}
