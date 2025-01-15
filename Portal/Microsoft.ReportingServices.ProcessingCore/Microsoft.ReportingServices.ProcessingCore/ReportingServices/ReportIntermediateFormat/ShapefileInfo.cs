using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200042F RID: 1071
	[Serializable]
	internal sealed class ShapefileInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F9A RID: 12186 RVA: 0x000D75E4 File Offset: 0x000D57E4
		internal ShapefileInfo()
		{
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000D75EC File Offset: 0x000D57EC
		internal ShapefileInfo(string streamName)
		{
			this.m_streamName = streamName;
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000D75FB File Offset: 0x000D57FB
		// (set) Token: 0x06002F9D RID: 12189 RVA: 0x000D7603 File Offset: 0x000D5803
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

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000D760C File Offset: 0x000D580C
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x000D7614 File Offset: 0x000D5814
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

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000D7620 File Offset: 0x000D5820
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ShapefileInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StreamName, Token.String),
				new MemberInfo(MemberName.ErrorOccurred, Token.Boolean)
			});
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000D766C File Offset: 0x000D586C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ShapefileInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
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
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000D76D8 File Offset: 0x000D58D8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ShapefileInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
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
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000D7744 File Offset: 0x000D5944
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000D7751 File Offset: 0x000D5951
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ShapefileInfo;
		}

		// Token: 0x040018C2 RID: 6338
		private string m_streamName;

		// Token: 0x040018C3 RID: 6339
		private bool m_errorOccurred;

		// Token: 0x040018C4 RID: 6340
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ShapefileInfo.GetDeclaration();
	}
}
