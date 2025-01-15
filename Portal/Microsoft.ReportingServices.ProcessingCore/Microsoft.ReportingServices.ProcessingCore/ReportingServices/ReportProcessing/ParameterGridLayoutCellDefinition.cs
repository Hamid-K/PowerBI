using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000623 RID: 1571
	[Serializable]
	public sealed class ParameterGridLayoutCellDefinition : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600565D RID: 22109 RVA: 0x0016BEB0 File Offset: 0x0016A0B0
		public void WriteXml(XmlTextWriter resultXml)
		{
			resultXml.WriteStartElement("CellDefinition");
			resultXml.WriteElementString("ColumnIndex", this.ColumnIndex.ToString(CultureInfo.InvariantCulture));
			resultXml.WriteElementString("RowIndex", this.RowIndex.ToString(CultureInfo.InvariantCulture));
			resultXml.WriteElementString("ParameterName", this.ParameterName);
			resultXml.WriteEndElement();
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x0016BF18 File Offset: 0x0016A118
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterGridLayoutCellDefinition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ParameterCellColumnIndex, Token.Int32, Lifetime.AddedIn(300)),
				new MemberInfo(MemberName.ParameterCellRowIndex, Token.Int32, Lifetime.AddedIn(300)),
				new MemberInfo(MemberName.ParameterName, Token.String, Lifetime.AddedIn(300))
			});
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x0016BF94 File Offset: 0x0016A194
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParameterGridLayoutCellDefinition.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.ParameterCellRowIndex:
					writer.Write(this.RowIndex);
					break;
				case MemberName.ParameterCellColumnIndex:
					writer.Write(this.ColumnIndex);
					break;
				case MemberName.ParameterName:
					writer.Write(this.ParameterName);
					break;
				default:
					Global.Tracer.Assert(false, "Unexpected RIF Member for ParametersGridCellDefinition");
					break;
				}
			}
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x0016C01C File Offset: 0x0016A21C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterGridLayoutCellDefinition.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.ParameterCellRowIndex:
					this.RowIndex = reader.ReadInt32();
					break;
				case MemberName.ParameterCellColumnIndex:
					this.ColumnIndex = reader.ReadInt32();
					break;
				case MemberName.ParameterName:
					this.ParameterName = reader.ReadString();
					break;
				default:
					Global.Tracer.Assert(false, "Unexpected RIF Member for ParametersGridCellDefinition");
					break;
				}
			}
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x0016C0A4 File Offset: 0x0016A2A4
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterGridLayoutCellDefinition;
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x0016C0AB File Offset: 0x0016A2AB
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002DC3 RID: 11715
		public int RowIndex;

		// Token: 0x04002DC4 RID: 11716
		public int ColumnIndex;

		// Token: 0x04002DC5 RID: 11717
		public string ParameterName;

		// Token: 0x04002DC6 RID: 11718
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterGridLayoutCellDefinition.GetDeclaration();
	}
}
