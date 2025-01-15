using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A4F RID: 2639
	public class Package
	{
		// Token: 0x06005274 RID: 21108 RVA: 0x0014EF1E File Offset: 0x0014D11E
		internal Package(Package package)
		{
			this._package = package;
			this.Sync(true);
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0014EF3F File Offset: 0x0014D13F
		public Package()
		{
			this._package = new Package();
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x0014EF5D File Offset: 0x0014D15D
		// (set) Token: 0x06005277 RID: 21111 RVA: 0x0014EF6A File Offset: 0x0014D16A
		public string IsolationLevel
		{
			get
			{
				return this._package.Title;
			}
			set
			{
				this._package.Title = value;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x0014EF78 File Offset: 0x0014D178
		// (set) Token: 0x06005279 RID: 21113 RVA: 0x0014EF85 File Offset: 0x0014D185
		public string Token
		{
			get
			{
				return this._package.ConsistencyToken;
			}
			set
			{
				this._package.ConsistencyToken = value;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x0600527A RID: 21114 RVA: 0x0014EF93 File Offset: 0x0014D193
		// (set) Token: 0x0600527B RID: 21115 RVA: 0x0014EFA0 File Offset: 0x0014D1A0
		public string Version
		{
			get
			{
				return this._package.VersionName;
			}
			set
			{
				this._package.VersionName = value;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x0014EFAE File Offset: 0x0014D1AE
		// (set) Token: 0x0600527D RID: 21117 RVA: 0x0014EFBB File Offset: 0x0014D1BB
		public string Collection
		{
			get
			{
				return this._package.CollectionIdentifier;
			}
			set
			{
				this._package.CollectionIdentifier = value;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x0014EFC9 File Offset: 0x0014D1C9
		// (set) Token: 0x0600527F RID: 21119 RVA: 0x0014EFD6 File Offset: 0x0014D1D6
		public string Id
		{
			get
			{
				return this._package.PackageIdentifier;
			}
			set
			{
				this._package.PackageIdentifier = value;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x0014EFE4 File Offset: 0x0014D1E4
		public List<Section> Sections
		{
			get
			{
				return this._sections;
			}
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x0014EFEC File Offset: 0x0014D1EC
		public void Drop(DrdaConnection connection, IProgress<string> progress)
		{
			this.InternalDropAsync(connection, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x0014F014 File Offset: 0x0014D214
		public async Task DropAsync(DrdaConnection connection, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this.InternalDropAsync(connection, progress, true, cancellationToken);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0014F074 File Offset: 0x0014D274
		internal async Task InternalDropAsync(DrdaConnection connection, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (connection == null)
			{
				throw new ArgumentNullException();
			}
			await connection.DropPackageAsync(this.ToPackage(), progress, isAsync, cancellationToken);
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0014F0DC File Offset: 0x0014D2DC
		public void Bind(DrdaConnection connection, Options options, IProgress<string> progress)
		{
			this.InternalBindAsync(connection, options, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0014F108 File Offset: 0x0014D308
		public async Task BindAsync(DrdaConnection connection, Options options, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this.InternalBindAsync(connection, options, progress, true, cancellationToken);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x0014F170 File Offset: 0x0014D370
		internal async Task InternalBindAsync(DrdaConnection connection, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (connection == null)
			{
				throw new ArgumentNullException();
			}
			await connection.BindPackageAsync(this.ToPackage(), (options == null) ? Package._defaultOption : options.ToOptions(), progress, isAsync, cancellationToken);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0014F1E0 File Offset: 0x0014D3E0
		public void CopyTo(DrdaConnection connection, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress)
		{
			this.InternalCopyToAsync(connection, options, targetRdbName, targetCollectionId, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x0014F210 File Offset: 0x0014D410
		public async Task CopyToAsync(DrdaConnection connection, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this.InternalCopyToAsync(connection, options, targetRdbName, targetCollectionId, progress, true, cancellationToken);
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x0014F288 File Offset: 0x0014D488
		internal async Task InternalCopyToAsync(DrdaConnection connection, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (connection == null)
			{
				throw new ArgumentNullException();
			}
			await connection.CopyPackageAsync(this.ToPackage(), (options == null) ? Package._defaultOption : options.ToOptions(), targetRdbName, targetCollectionId, progress, isAsync, cancellationToken);
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0014F30C File Offset: 0x0014D50C
		public void Rebind(DrdaConnection connection, Options options, IProgress<string> progress)
		{
			this.InternalRebindAsync(connection, options, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0014F338 File Offset: 0x0014D538
		public async Task RebindAsync(DrdaConnection connection, Options options, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this.InternalRebindAsync(connection, options, progress, true, cancellationToken);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0014F3A0 File Offset: 0x0014D5A0
		internal async Task InternalRebindAsync(DrdaConnection connection, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (connection == null)
			{
				throw new ArgumentNullException();
			}
			await connection.RebindPackageAsync(this.ToPackage(), (options == null) ? Package._defaultOption : options.ToOptions(), progress, isAsync, cancellationToken);
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x0014F40F File Offset: 0x0014D60F
		internal Package ToPackage()
		{
			this.Sync(false);
			return this._package;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0014F420 File Offset: 0x0014D620
		private void Sync(bool isRead)
		{
			if (isRead)
			{
				this._sections.Clear();
				using (List<Section>.Enumerator enumerator = this._package.Sections.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Section section = enumerator.Current;
						this._sections.Add(new Section(section));
					}
					return;
				}
			}
			this._package.Sections.Clear();
			foreach (Section section2 in this._sections)
			{
				this._package.Sections.Add(section2.ToSection());
			}
		}

		// Token: 0x040040E5 RID: 16613
		private Package _package;

		// Token: 0x040040E6 RID: 16614
		private List<Section> _sections = new List<Section>();

		// Token: 0x040040E7 RID: 16615
		private static Options _defaultOption = new Options();
	}
}
