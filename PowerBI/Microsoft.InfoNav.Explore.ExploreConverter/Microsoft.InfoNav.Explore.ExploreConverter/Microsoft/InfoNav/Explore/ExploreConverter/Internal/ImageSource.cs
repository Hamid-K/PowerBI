using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200006C RID: 108
	internal sealed class ImageSource
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000B993 File Offset: 0x00009B93
		internal ImageSource(ReportImageSource? source, string value, string mimeType)
		{
			this.source = source;
			this.value = value;
			this.mimeType = mimeType;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public Expression ValueAsExpression
		{
			get
			{
				if (this.value == null)
				{
					return null;
				}
				return new Expression(this.value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000B9C7 File Offset: 0x00009BC7
		public string ValueAsString
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B9CF File Offset: 0x00009BCF
		public ReportImageSource? Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B9D7 File Offset: 0x00009BD7
		public string MIMEType
		{
			get
			{
				return this.mimeType;
			}
		}

		// Token: 0x0400017C RID: 380
		private ReportImageSource? source;

		// Token: 0x0400017D RID: 381
		private string value;

		// Token: 0x0400017E RID: 382
		private string mimeType;
	}
}
