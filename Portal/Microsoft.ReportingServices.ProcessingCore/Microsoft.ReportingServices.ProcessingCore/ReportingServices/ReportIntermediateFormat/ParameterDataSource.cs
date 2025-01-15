using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004EF RID: 1263
	[Serializable]
	internal class ParameterDataSource : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IParameterDataSource
	{
		// Token: 0x06004020 RID: 16416 RVA: 0x0010EE4C File Offset: 0x0010D04C
		internal ParameterDataSource()
		{
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x0010EE70 File Offset: 0x0010D070
		internal ParameterDataSource(int dataSourceIndex, int dataSetIndex)
		{
			this.m_dataSourceIndex = dataSourceIndex;
			this.m_dataSetIndex = dataSetIndex;
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0010EEA2 File Offset: 0x0010D0A2
		// (set) Token: 0x06004023 RID: 16419 RVA: 0x0010EEAA File Offset: 0x0010D0AA
		public int DataSourceIndex
		{
			get
			{
				return this.m_dataSourceIndex;
			}
			set
			{
				this.m_dataSourceIndex = value;
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x0010EEB3 File Offset: 0x0010D0B3
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x0010EEBB File Offset: 0x0010D0BB
		public int DataSetIndex
		{
			get
			{
				return this.m_dataSetIndex;
			}
			set
			{
				this.m_dataSetIndex = value;
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x0010EEC4 File Offset: 0x0010D0C4
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x0010EECC File Offset: 0x0010D0CC
		public int ValueFieldIndex
		{
			get
			{
				return this.m_valueFieldIndex;
			}
			set
			{
				this.m_valueFieldIndex = value;
			}
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x0010EED5 File Offset: 0x0010D0D5
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x0010EEDD File Offset: 0x0010D0DD
		public int LabelFieldIndex
		{
			get
			{
				return this.m_labelFieldIndex;
			}
			set
			{
				this.m_labelFieldIndex = value;
			}
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x0010EEE8 File Offset: 0x0010D0E8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDataSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataSourceIndex, Token.Int32),
				new MemberInfo(MemberName.DataSetIndex, Token.Int32),
				new MemberInfo(MemberName.ValueFieldIndex, Token.Int32),
				new MemberInfo(MemberName.LabelFieldIndex, Token.Int32)
			});
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x0010EF5C File Offset: 0x0010D15C
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParameterDataSource.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.DataSourceIndex:
					writer.Write(this.m_dataSourceIndex);
					break;
				case MemberName.DataSetIndex:
					writer.Write(this.m_dataSetIndex);
					break;
				case MemberName.ValueFieldIndex:
					writer.Write(this.m_valueFieldIndex);
					break;
				case MemberName.LabelFieldIndex:
					writer.Write(this.m_labelFieldIndex);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x0010EFF4 File Offset: 0x0010D1F4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterDataSource.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.DataSourceIndex:
					this.m_dataSourceIndex = reader.ReadInt32();
					break;
				case MemberName.DataSetIndex:
					this.m_dataSetIndex = reader.ReadInt32();
					break;
				case MemberName.ValueFieldIndex:
					this.m_valueFieldIndex = reader.ReadInt32();
					break;
				case MemberName.LabelFieldIndex:
					this.m_labelFieldIndex = reader.ReadInt32();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x0010F08A File Offset: 0x0010D28A
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x0010F097 File Offset: 0x0010D297
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDataSource;
		}

		// Token: 0x04001D81 RID: 7553
		private int m_dataSourceIndex = -1;

		// Token: 0x04001D82 RID: 7554
		private int m_dataSetIndex = -1;

		// Token: 0x04001D83 RID: 7555
		private int m_valueFieldIndex = -1;

		// Token: 0x04001D84 RID: 7556
		private int m_labelFieldIndex = -1;

		// Token: 0x04001D85 RID: 7557
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterDataSource.GetDeclaration();
	}
}
