using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EC8 RID: 3784
	internal sealed class DeleteDirectoryActionValue : ContinuationActionValue
	{
		// Token: 0x0600647B RID: 25723 RVA: 0x00158100 File Offset: 0x00156300
		public DeleteDirectoryActionValue(AdlsTableValue target)
		{
			this.target = target;
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x0015810F File Offset: 0x0015630F
		public AdlsTableValue Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x0600647D RID: 25725 RVA: 0x00158117 File Offset: 0x00156317
		protected override IEngineHost Host
		{
			get
			{
				return this.target.Host;
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x0600647E RID: 25726 RVA: 0x00158124 File Offset: 0x00156324
		protected override string TargetUri
		{
			get
			{
				return this.target.FolderUrl.AsString;
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x0600647F RID: 25727 RVA: 0x00158136 File Offset: 0x00156336
		protected override IResource RequestResource
		{
			get
			{
				return Resource.New(this.target.ResourceKind, this.TargetUri);
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x0015814E File Offset: 0x0015634E
		protected override bool IsOneLake
		{
			get
			{
				return this.Target.Options.GetBool("IsOneLake", false);
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x00158066 File Offset: 0x00156266
		protected override string Method
		{
			get
			{
				return "DELETE";
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x00158166 File Offset: 0x00156366
		protected override RecordValue BaseQuery
		{
			get
			{
				return RecordValue.New(Keys.New("recursive"), new Value[] { TextValue.New("true") });
			}
		}

		// Token: 0x040036EA RID: 14058
		private readonly AdlsTableValue target;
	}
}
