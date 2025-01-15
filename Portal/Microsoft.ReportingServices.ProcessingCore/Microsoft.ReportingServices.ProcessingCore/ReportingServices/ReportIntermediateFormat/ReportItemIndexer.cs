using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004FA RID: 1274
	[Serializable]
	internal struct ReportItemIndexer : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600415B RID: 16731 RVA: 0x00113695 File Offset: 0x00111895
		public object PublishClone(AutomaticSubtotalContext context)
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x001136A8 File Offset: 0x001118A8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemIndexer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.IsComputed, Token.Boolean),
				new MemberInfo(MemberName.Index, Token.Int32)
			});
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x001136F4 File Offset: 0x001118F4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ReportItemIndexer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.IsComputed)
				{
					if (memberName != MemberName.Index)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.Index);
					}
				}
				else
				{
					writer.Write(this.IsComputed);
				}
			}
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x00113760 File Offset: 0x00111960
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ReportItemIndexer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.IsComputed)
				{
					if (memberName != MemberName.Index)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.Index = reader.ReadInt32();
					}
				}
				else
				{
					this.IsComputed = reader.ReadBoolean();
				}
			}
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x001137CC File Offset: 0x001119CC
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x001137D9 File Offset: 0x001119D9
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemIndexer;
		}

		// Token: 0x04001DF1 RID: 7665
		internal bool IsComputed;

		// Token: 0x04001DF2 RID: 7666
		internal int Index;

		// Token: 0x04001DF3 RID: 7667
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportItemIndexer.GetDeclaration();
	}
}
