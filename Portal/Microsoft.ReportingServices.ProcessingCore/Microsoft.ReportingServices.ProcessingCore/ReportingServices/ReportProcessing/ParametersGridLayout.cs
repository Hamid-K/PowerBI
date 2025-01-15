using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000621 RID: 1569
	[Serializable]
	public sealed class ParametersGridLayout : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06005653 RID: 22099 RVA: 0x0016BCDC File Offset: 0x00169EDC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParametersLayout, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ParametersLayoutNumberOfColumns, Token.Int32, Lifetime.AddedIn(300)),
				new MemberInfo(MemberName.ParametersLayoutNumberOfRows, Token.Int32, Lifetime.AddedIn(300)),
				new MemberInfo(MemberName.ParametersLayoutCellDefinitions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterGridLayoutCellDefinition, Lifetime.AddedIn(300))
			});
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x0016BD5C File Offset: 0x00169F5C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParametersGridLayout.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.ParametersLayoutCellDefinitions:
					writer.Write(this.CellDefinitions);
					break;
				case MemberName.ParametersLayoutNumberOfColumns:
					writer.Write(this.NumberOfColumns);
					break;
				case MemberName.ParametersLayoutNumberOfRows:
					writer.Write(this.NumberOfRows);
					break;
				default:
					Global.Tracer.Assert(false, "Unexpected RIF Member for ParametersGridLayout");
					break;
				}
			}
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x0016BDE4 File Offset: 0x00169FE4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParametersGridLayout.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.ParametersLayoutCellDefinitions:
					this.CellDefinitions = reader.ReadListOfRIFObjects<ParametersGridCellDefinitionList>();
					break;
				case MemberName.ParametersLayoutNumberOfColumns:
					this.NumberOfColumns = reader.ReadInt32();
					break;
				case MemberName.ParametersLayoutNumberOfRows:
					this.NumberOfRows = reader.ReadInt32();
					break;
				default:
					Global.Tracer.Assert(false, "Unexpected RIF Member for ParametersGridLayout");
					break;
				}
			}
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x0016BE6C File Offset: 0x0016A06C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x0016BE73 File Offset: 0x0016A073
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParametersLayout;
		}

		// Token: 0x04002DBF RID: 11711
		public int NumberOfColumns;

		// Token: 0x04002DC0 RID: 11712
		public int NumberOfRows;

		// Token: 0x04002DC1 RID: 11713
		public ParametersGridCellDefinitionList CellDefinitions;

		// Token: 0x04002DC2 RID: 11714
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParametersGridLayout.GetDeclaration();
	}
}
