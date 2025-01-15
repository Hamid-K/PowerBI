using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008CC RID: 2252
	[PersistedWithinRequestOnly]
	internal sealed class ChildLeafInfo : List<RuntimeGroupLeafObjReference>, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x1700287B RID: 10363
		// (get) Token: 0x06007B55 RID: 31573 RVA: 0x001FB6A3 File Offset: 0x001F98A3
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf<RuntimeGroupLeafObjReference>(this);
			}
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x001FB6AB File Offset: 0x001F98AB
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChildLeafInfo.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.List)
				{
					writer.WriteRIFList<RuntimeGroupLeafObjReference>(this);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x001FB6E9 File Offset: 0x001F98E9
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChildLeafInfo.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.List)
				{
					reader.ReadListOfRIFObjects(this);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x001FB727 File Offset: 0x001F9927
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x001FB729 File Offset: 0x001F9929
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChildLeafInfo;
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x001FB730 File Offset: 0x001F9930
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ChildLeafInfo.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChildLeafInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.List, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference)
				});
			}
			return ChildLeafInfo.m_declaration;
		}

		// Token: 0x04003D6E RID: 15726
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ChildLeafInfo.GetDeclaration();
	}
}
