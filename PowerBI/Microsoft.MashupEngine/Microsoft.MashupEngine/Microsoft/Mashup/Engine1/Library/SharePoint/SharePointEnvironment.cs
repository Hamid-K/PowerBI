using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.File;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x020003F3 RID: 1011
	internal sealed class SharePointEnvironment
	{
		// Token: 0x0600229A RID: 8858 RVA: 0x00060629 File Offset: 0x0005E829
		public SharePointEnvironment(IEngineHost host, ResourceCredentialCollection feedCredentials, FileHelper.FolderOptions options)
		{
			this.Host = host;
			this.FeedCredentials = feedCredentials;
			this.Options = options;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00060646 File Offset: 0x0005E846
		private SharePointEnvironment(SharePointEnvironment environment)
		{
			this.Host = environment.Host;
			this.FeedCredentials = environment.FeedCredentials;
			this.Options = environment.Options;
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x00060672 File Offset: 0x0005E872
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x0006067A File Offset: 0x0005E87A
		public ResourceCredentialCollection FeedCredentials { get; private set; }

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00060683 File Offset: 0x0005E883
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x0006068B File Offset: 0x0005E88B
		public string Entity { get; private set; }

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x00060694 File Offset: 0x0005E894
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x0006069C File Offset: 0x0005E89C
		public FileHelper.FolderOptions Options { get; private set; }

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000606A5 File Offset: 0x0005E8A5
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x000606AD File Offset: 0x0005E8AD
		public IEngineHost Host { get; private set; }

		// Token: 0x060022A4 RID: 8868 RVA: 0x000606B6 File Offset: 0x0005E8B6
		public SharePointEnvironment AddOptions(FileHelper.FolderOptions options)
		{
			SharePointEnvironment sharePointEnvironment = new SharePointEnvironment(this);
			sharePointEnvironment.Options |= options;
			return sharePointEnvironment;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000606CC File Offset: 0x0005E8CC
		public SharePointEnvironment SetEntity(string entity)
		{
			return new SharePointEnvironment(this)
			{
				Entity = entity
			};
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000606DB File Offset: 0x0005E8DB
		public SharePointEnvironment SetOptions(FileHelper.FolderOptions options)
		{
			return new SharePointEnvironment(this)
			{
				Options = options
			};
		}
	}
}
