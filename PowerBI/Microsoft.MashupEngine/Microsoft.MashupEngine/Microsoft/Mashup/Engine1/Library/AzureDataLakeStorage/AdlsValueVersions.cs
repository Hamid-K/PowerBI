using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EDD RID: 3805
	internal sealed class AdlsValueVersions : ValueVersions
	{
		// Token: 0x0600650E RID: 25870 RVA: 0x0015B06F File Offset: 0x0015926F
		public AdlsValueVersions(AdlsVersions versions, IEngineHost host, IResource resource, IList<string> blobUriFilter, Action<AdlsVersions.Version> versionCtor, Func<AdlsVersions.Version, Value> valueCtor)
		{
			this.versions = versions;
			this.host = host;
			this.resource = resource;
			this.blobUriFilter = blobUriFilter;
			this.versionCtor = versionCtor;
			this.valueCtor = valueCtor;
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x0600650F RID: 25871 RVA: 0x0015B0A4 File Offset: 0x001592A4
		protected override IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x0015B0AC File Offset: 0x001592AC
		protected override void VerifyActionPermitted()
		{
			this.host.VerifyActionPermitted(this.resource);
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x0015B0C0 File Offset: 0x001592C0
		protected override bool TryCreateVersion(string identity)
		{
			AdlsVersions.Version version;
			if (!this.versions.TryGetVersion(identity, out version))
			{
				version = this.versions.CreateVersion(identity);
				this.host.QueryService<ILifetimeService>().Register(version);
			}
			this.versionCtor(version);
			return true;
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x0015B108 File Offset: 0x00159308
		protected override IEnumerable<ValueVersions.ValueVersion> GetVersions()
		{
			yield return new AdlsValueVersions.AdlsValueVersion(this, null);
			foreach (AdlsVersions.Version version in this.versions.Where((AdlsVersions.Version v) => !this.blobUriFilter.Except(v.TrackedUrls).Any<string>()))
			{
				yield return new AdlsValueVersions.AdlsValueVersion(this, version);
			}
			IEnumerator<AdlsVersions.Version> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04003750 RID: 14160
		private readonly AdlsVersions versions;

		// Token: 0x04003751 RID: 14161
		private readonly IEngineHost host;

		// Token: 0x04003752 RID: 14162
		private readonly IResource resource;

		// Token: 0x04003753 RID: 14163
		private readonly IList<string> blobUriFilter;

		// Token: 0x04003754 RID: 14164
		private readonly Action<AdlsVersions.Version> versionCtor;

		// Token: 0x04003755 RID: 14165
		private readonly Func<AdlsVersions.Version, Value> valueCtor;

		// Token: 0x02000EDE RID: 3806
		private class AdlsValueVersion : ValueVersions.ValueVersion
		{
			// Token: 0x06006514 RID: 25876 RVA: 0x0015B133 File Offset: 0x00159333
			public AdlsValueVersion(AdlsValueVersions valueVersions, AdlsVersions.Version version)
			{
				this.valueVersions = valueVersions;
				this.version = version;
			}

			// Token: 0x17001D6E RID: 7534
			// (get) Token: 0x06006515 RID: 25877 RVA: 0x0015B149 File Offset: 0x00159349
			public override string Identity
			{
				get
				{
					AdlsVersions.Version version = this.version;
					if (version == null)
					{
						return null;
					}
					return version.Identity;
				}
			}

			// Token: 0x06006516 RID: 25878 RVA: 0x0015B15C File Offset: 0x0015935C
			public override bool TryCreateValue(out IValueReference value)
			{
				value = this.valueVersions.valueCtor(this.version);
				return true;
			}

			// Token: 0x06006517 RID: 25879 RVA: 0x0015B177 File Offset: 0x00159377
			public override bool TryCommit()
			{
				this.version.Commit();
				this.valueVersions.host.QueryService<ILifetimeService>().Unregister(this.version);
				return true;
			}

			// Token: 0x04003756 RID: 14166
			private readonly AdlsValueVersions valueVersions;

			// Token: 0x04003757 RID: 14167
			private readonly AdlsVersions.Version version;
		}
	}
}
