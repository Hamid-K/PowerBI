using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004EE RID: 1262
	[Serializable]
	internal struct RenderingPagesRanges : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x0010ECC3 File Offset: 0x0010CEC3
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x0010ECCB File Offset: 0x0010CECB
		internal int StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x0010ECD4 File Offset: 0x0010CED4
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x0010ECDC File Offset: 0x0010CEDC
		internal int StartRow
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x0010ECE5 File Offset: 0x0010CEE5
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x0010ECED File Offset: 0x0010CEED
		internal int EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x0010ECF6 File Offset: 0x0010CEF6
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x0010ECFE File Offset: 0x0010CEFE
		internal int NumberOfDetails
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x0010ED08 File Offset: 0x0010CF08
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RenderingPagesRanges, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StartPage, Token.Int32),
				new MemberInfo(MemberName.EndPage, Token.Int32)
			});
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x0010ED54 File Offset: 0x0010CF54
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RenderingPagesRanges.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StartPage)
				{
					if (memberName != MemberName.EndPage)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_endPage);
					}
				}
				else
				{
					writer.Write(this.m_startPage);
				}
			}
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x0010EDC0 File Offset: 0x0010CFC0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RenderingPagesRanges.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StartPage)
				{
					if (memberName != MemberName.EndPage)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_endPage = reader.ReadInt32();
					}
				}
				else
				{
					this.m_startPage = reader.ReadInt32();
				}
			}
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x0010EE2C File Offset: 0x0010D02C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x0010EE39 File Offset: 0x0010D039
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RenderingPagesRanges;
		}

		// Token: 0x04001D7E RID: 7550
		private int m_startPage;

		// Token: 0x04001D7F RID: 7551
		private int m_endPage;

		// Token: 0x04001D80 RID: 7552
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RenderingPagesRanges.GetDeclaration();
	}
}
