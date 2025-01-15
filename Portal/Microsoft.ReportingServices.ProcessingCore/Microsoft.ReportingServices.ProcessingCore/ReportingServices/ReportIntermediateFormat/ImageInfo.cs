using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004CD RID: 1229
	[Serializable]
	internal sealed class ImageInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003E6A RID: 15978 RVA: 0x0010AD6C File Offset: 0x00108F6C
		internal ImageInfo()
		{
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x0010AD74 File Offset: 0x00108F74
		internal ImageInfo(string streamName, string mimeType)
		{
			this.m_streamName = streamName;
			this.m_mimeType = mimeType;
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x06003E6C RID: 15980 RVA: 0x0010AD8A File Offset: 0x00108F8A
		// (set) Token: 0x06003E6D RID: 15981 RVA: 0x0010AD92 File Offset: 0x00108F92
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

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x0010AD9B File Offset: 0x00108F9B
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x0010ADA3 File Offset: 0x00108FA3
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

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x0010ADAC File Offset: 0x00108FAC
		// (set) Token: 0x06003E71 RID: 15985 RVA: 0x0010ADB4 File Offset: 0x00108FB4
		internal bool ErrorOccurred
		{
			get
			{
				return this.m_errorOccurred;
			}
			set
			{
				this.m_errorOccurred = value;
			}
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0010ADBD File Offset: 0x00108FBD
		internal byte[] GetCachedImageData()
		{
			if (this.m_imageDataRef != null && this.m_imageDataRef.IsAlive)
			{
				return (byte[])this.m_imageDataRef.Target;
			}
			return null;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0010ADE6 File Offset: 0x00108FE6
		internal void SetCachedImageData(byte[] imageData)
		{
			if (this.m_imageDataRef == null)
			{
				this.m_imageDataRef = new WeakReference(imageData);
				return;
			}
			this.m_imageDataRef.Target = imageData;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x0010AE0C File Offset: 0x0010900C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StreamName, Token.String),
				new MemberInfo(MemberName.MIMEType, Token.String),
				new MemberInfo(MemberName.ErrorOccurred, Token.Boolean)
			});
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0010AE6C File Offset: 0x0010906C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ImageInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.MIMEType)
				{
					if (memberName != MemberName.StreamName)
					{
						if (memberName != MemberName.ErrorOccurred)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_errorOccurred);
						}
					}
					else
					{
						writer.Write(this.m_streamName);
					}
				}
				else
				{
					writer.Write(this.m_mimeType);
				}
			}
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0010AEF0 File Offset: 0x001090F0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ImageInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.MIMEType)
				{
					if (memberName != MemberName.StreamName)
					{
						if (memberName != MemberName.ErrorOccurred)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_errorOccurred = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_streamName = reader.ReadString();
					}
				}
				else
				{
					this.m_mimeType = reader.ReadString();
				}
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x0010AF73 File Offset: 0x00109173
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0010AF80 File Offset: 0x00109180
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInfo;
		}

		// Token: 0x04001D0E RID: 7438
		private string m_streamName;

		// Token: 0x04001D0F RID: 7439
		private string m_mimeType;

		// Token: 0x04001D10 RID: 7440
		private bool m_errorOccurred;

		// Token: 0x04001D11 RID: 7441
		[NonSerialized]
		private WeakReference m_imageDataRef;

		// Token: 0x04001D12 RID: 7442
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ImageInfo.GetDeclaration();
	}
}
