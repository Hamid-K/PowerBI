using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EC7 RID: 3783
	internal sealed class DeleteBlobActionValue : ContinuationActionValue
	{
		// Token: 0x06006473 RID: 25715 RVA: 0x00158023 File Offset: 0x00156223
		public DeleteBlobActionValue(AdlsBinaryValue target)
		{
			this.target = target;
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x00158032 File Offset: 0x00156232
		public AdlsBinaryValue Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x0015803A File Offset: 0x0015623A
		protected override IEngineHost Host
		{
			get
			{
				return this.target.Host;
			}
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06006476 RID: 25718 RVA: 0x00158047 File Offset: 0x00156247
		protected override string TargetUri
		{
			get
			{
				return this.target.BlobUrl.AsString;
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x00158059 File Offset: 0x00156259
		protected override IResource RequestResource
		{
			get
			{
				return this.target.Resource;
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x00158066 File Offset: 0x00156266
		protected override string Method
		{
			get
			{
				return "DELETE";
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x00158070 File Offset: 0x00156270
		protected override Value Headers
		{
			get
			{
				if (this.headers == null)
				{
					if (this.Target.Version != null)
					{
						this.headers = RecordValue.New(Keys.New("If-Match"), new Value[] { TextValue.New(this.target.Version.GetTrackedUrlETag(this.target.BlobUrl.AsString)) });
					}
					else
					{
						this.headers = RecordValue.Empty;
					}
				}
				return this.headers;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x0600647A RID: 25722 RVA: 0x001580E8 File Offset: 0x001562E8
		protected override bool IsOneLake
		{
			get
			{
				return this.Target.Options.GetBool("IsOneLake", false);
			}
		}

		// Token: 0x040036E8 RID: 14056
		private readonly AdlsBinaryValue target;

		// Token: 0x040036E9 RID: 14057
		private RecordValue headers;
	}
}
