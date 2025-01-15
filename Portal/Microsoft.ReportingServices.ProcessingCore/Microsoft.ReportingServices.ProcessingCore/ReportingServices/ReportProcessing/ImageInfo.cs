using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000711 RID: 1809
	[Serializable]
	internal sealed class ImageInfo
	{
		// Token: 0x06006507 RID: 25863 RVA: 0x0018EE6A File Offset: 0x0018D06A
		internal ImageInfo()
		{
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0018EE72 File Offset: 0x0018D072
		internal ImageInfo(string streamName, string mimeType)
		{
			this.m_streamName = streamName;
			this.m_mimeType = mimeType;
		}

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x06006509 RID: 25865 RVA: 0x0018EE88 File Offset: 0x0018D088
		// (set) Token: 0x0600650A RID: 25866 RVA: 0x0018EE90 File Offset: 0x0018D090
		internal string StreamName
		{
			get
			{
				return this.m_streamName;
			}
			set
			{
				this.m_streamName = value;
			}
		}

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x0018EE99 File Offset: 0x0018D099
		// (set) Token: 0x0600650C RID: 25868 RVA: 0x0018EEA1 File Offset: 0x0018D0A1
		internal string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x0600650D RID: 25869 RVA: 0x0018EEAA File Offset: 0x0018D0AA
		// (set) Token: 0x0600650E RID: 25870 RVA: 0x0018EEB2 File Offset: 0x0018D0B2
		internal WeakReference ImageDataRef
		{
			get
			{
				return this.m_imageDataRef;
			}
			set
			{
				this.m_imageDataRef = value;
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0018EEBC File Offset: 0x0018D0BC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.StreamName, Token.String),
				new MemberInfo(MemberName.MIMEType, Token.String)
			});
		}

		// Token: 0x04003298 RID: 12952
		private string m_streamName;

		// Token: 0x04003299 RID: 12953
		private string m_mimeType;

		// Token: 0x0400329A RID: 12954
		[NonSerialized]
		private WeakReference m_imageDataRef;
	}
}
