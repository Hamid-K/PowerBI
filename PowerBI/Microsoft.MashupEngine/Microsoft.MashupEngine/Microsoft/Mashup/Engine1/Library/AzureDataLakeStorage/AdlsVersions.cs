using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EE0 RID: 3808
	internal sealed class AdlsVersions : IEnumerable<AdlsVersions.Version>, IEnumerable
	{
		// Token: 0x06006521 RID: 25889 RVA: 0x0015B34F File Offset: 0x0015954F
		private AdlsVersions()
		{
			this.versions = new Dictionary<string, AdlsVersions.Version>();
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x0015B362 File Offset: 0x00159562
		public IEnumerator<AdlsVersions.Version> GetEnumerator()
		{
			return this.versions.Values.GetEnumerator();
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x0015B379 File Offset: 0x00159579
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x0015B381 File Offset: 0x00159581
		public bool TryGetVersion(string identity, out AdlsVersions.Version version)
		{
			return this.versions.TryGetValue(identity, out version);
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x0015B390 File Offset: 0x00159590
		public AdlsVersions.Version CreateVersion(string identity)
		{
			AdlsVersions.Version version = new AdlsVersions.Version(this, identity);
			this.versions.Add(identity, version);
			return version;
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x0015B3B3 File Offset: 0x001595B3
		private void RemoveVersion(AdlsVersions.Version version)
		{
			this.versions.Remove(version.Identity);
		}

		// Token: 0x0400375D RID: 14173
		public static readonly AdlsVersions Instance = new AdlsVersions();

		// Token: 0x0400375E RID: 14174
		private readonly Dictionary<string, AdlsVersions.Version> versions;

		// Token: 0x02000EE1 RID: 3809
		public sealed class Version : IDisposable
		{
			// Token: 0x06006528 RID: 25896 RVA: 0x0015B3D3 File Offset: 0x001595D3
			public Version(AdlsVersions versions, string identity)
			{
				this.versions = versions;
				this.identity = identity;
				this.trackedUrls = new Dictionary<string, string>();
				this.state = AdlsVersions.Version.State.Initial;
			}

			// Token: 0x17001D71 RID: 7537
			// (get) Token: 0x06006529 RID: 25897 RVA: 0x0015B3FB File Offset: 0x001595FB
			public string Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x17001D72 RID: 7538
			// (get) Token: 0x0600652A RID: 25898 RVA: 0x0015B403 File Offset: 0x00159603
			public IEnumerable<string> TrackedUrls
			{
				get
				{
					return this.trackedUrls.Keys;
				}
			}

			// Token: 0x0600652B RID: 25899 RVA: 0x0015B410 File Offset: 0x00159610
			public void TrackUrl(string url, string etag)
			{
				this.trackedUrls.Add(url, etag);
			}

			// Token: 0x0600652C RID: 25900 RVA: 0x0015B41F File Offset: 0x0015961F
			public string GetTrackedUrlETag(string url)
			{
				return this.trackedUrls[url];
			}

			// Token: 0x0600652D RID: 25901 RVA: 0x0015B430 File Offset: 0x00159630
			public IDictionary<string, BinaryValue> GetBlobReplacements()
			{
				Dictionary<string, BinaryValue> dictionary = new Dictionary<string, BinaryValue>();
				switch (this.state)
				{
				case AdlsVersions.Version.State.TargetDeleted:
				{
					DeleteBlobActionValue deleteBlobActionValue = (DeleteBlobActionValue)this.effectiveAction;
					dictionary.Add(deleteBlobActionValue.Target.BlobUrl.AsString, null);
					break;
				}
				case AdlsVersions.Version.State.TargetCreated:
				{
					CreateBlobActionValue createBlobActionValue = (CreateBlobActionValue)this.effectiveAction;
					dictionary.Add(createBlobActionValue.Target.BlobUrl.AsString, createBlobActionValue.Target);
					break;
				}
				case AdlsVersions.Version.State.TargetReplaced:
				{
					ReplaceBlobActionValue replaceBlobActionValue = (ReplaceBlobActionValue)this.effectiveAction;
					dictionary.Add(replaceBlobActionValue.Target.BlobUrl.AsString, replaceBlobActionValue.Source);
					break;
				}
				case AdlsVersions.Version.State.SourceMoved:
				{
					MoveBlobActionValue moveBlobActionValue = (MoveBlobActionValue)this.effectiveAction;
					dictionary.Add(moveBlobActionValue.Target.BlobUrl.AsString, moveBlobActionValue.Source);
					dictionary.Add(moveBlobActionValue.Source.BlobUrl.AsString, null);
					break;
				}
				}
				return dictionary;
			}

			// Token: 0x0600652E RID: 25902 RVA: 0x0015B529 File Offset: 0x00159729
			public ActionValue CreateVersionedAction(ActionValue action)
			{
				return ActionValue.New(delegate
				{
					this.AddAction(action);
					return Value.Null;
				});
			}

			// Token: 0x0600652F RID: 25903 RVA: 0x0015B550 File Offset: 0x00159750
			private void AddAction(ActionValue action)
			{
				DeleteBlobActionValue deleteBlobActionValue = action as DeleteBlobActionValue;
				if (deleteBlobActionValue != null && this.state == AdlsVersions.Version.State.Initial)
				{
					this.effectiveAction = deleteBlobActionValue;
					this.state = AdlsVersions.Version.State.TargetDeleted;
					return;
				}
				CreateBlobActionValue createBlobActionValue = action as CreateBlobActionValue;
				if (createBlobActionValue != null && (this.state == AdlsVersions.Version.State.Initial || (this.state == AdlsVersions.Version.State.TargetDeleted && ((DeleteBlobActionValue)this.effectiveAction).Target.BlobUrl.Equals(createBlobActionValue.Target.BlobUrl))))
				{
					this.effectiveAction = createBlobActionValue;
					this.state = AdlsVersions.Version.State.TargetCreated;
					return;
				}
				ReplaceBlobActionValue replaceBlobActionValue = action as ReplaceBlobActionValue;
				if (replaceBlobActionValue != null && (this.state == AdlsVersions.Version.State.Initial || (this.state == AdlsVersions.Version.State.TargetDeleted && ((DeleteBlobActionValue)this.effectiveAction).Target.BlobUrl.Equals(replaceBlobActionValue.Target.BlobUrl)) || (this.state == AdlsVersions.Version.State.TargetCreated && ((CreateBlobActionValue)this.effectiveAction).Target.BlobUrl.Equals(replaceBlobActionValue.Target.BlobUrl))))
				{
					this.effectiveAction = replaceBlobActionValue;
					this.state = AdlsVersions.Version.State.TargetReplaced;
					return;
				}
				if (deleteBlobActionValue != null && this.state == AdlsVersions.Version.State.TargetReplaced && ((ReplaceBlobActionValue)this.effectiveAction).Source.BlobUrl.Equals(deleteBlobActionValue.Target.BlobUrl))
				{
					ReplaceBlobActionValue replaceBlobActionValue2 = (ReplaceBlobActionValue)this.effectiveAction;
					this.effectiveAction = new MoveBlobActionValue(replaceBlobActionValue2.Target, replaceBlobActionValue2.Source);
					this.state = AdlsVersions.Version.State.SourceMoved;
					return;
				}
				throw ValueException.NewExpressionError<Message0>(Strings.Value_UpdateNotSupported, null, null);
			}

			// Token: 0x06006530 RID: 25904 RVA: 0x0015B6BC File Offset: 0x001598BC
			public void Commit()
			{
				if (this.state == AdlsVersions.Version.State.SourceMoved)
				{
					MoveBlobActionValue moveBlobActionValue = (MoveBlobActionValue)this.effectiveAction;
					moveBlobActionValue.ClearCache(moveBlobActionValue.Target.Host).Execute();
					this.versions.RemoveVersion(this);
					this.state = AdlsVersions.Version.State.Committed;
					return;
				}
				throw ValueException.NewExpressionError<Message0>(Strings.Value_UpdateNotSupported, null, null);
			}

			// Token: 0x06006531 RID: 25905 RVA: 0x0015B715 File Offset: 0x00159915
			public void Dispose()
			{
				this.versions.RemoveVersion(this);
			}

			// Token: 0x0400375F RID: 14175
			private readonly AdlsVersions versions;

			// Token: 0x04003760 RID: 14176
			private readonly string identity;

			// Token: 0x04003761 RID: 14177
			private readonly Dictionary<string, string> trackedUrls;

			// Token: 0x04003762 RID: 14178
			private AdlsVersions.Version.State state;

			// Token: 0x04003763 RID: 14179
			private ActionValue effectiveAction;

			// Token: 0x02000EE2 RID: 3810
			private enum State
			{
				// Token: 0x04003765 RID: 14181
				Initial,
				// Token: 0x04003766 RID: 14182
				TargetDeleted,
				// Token: 0x04003767 RID: 14183
				TargetCreated,
				// Token: 0x04003768 RID: 14184
				TargetReplaced,
				// Token: 0x04003769 RID: 14185
				SourceMoved,
				// Token: 0x0400376A RID: 14186
				Committed
			}
		}
	}
}
