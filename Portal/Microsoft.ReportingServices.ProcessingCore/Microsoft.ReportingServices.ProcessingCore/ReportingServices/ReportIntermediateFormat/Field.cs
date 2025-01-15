using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	internal sealed class Field : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStaticReferenceable
	{
		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x001070B9 File Offset: 0x001052B9
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x001070C1 File Offset: 0x001052C1
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x001070CA File Offset: 0x001052CA
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x001070D2 File Offset: 0x001052D2
		internal string DataField
		{
			get
			{
				return this.m_dataField;
			}
			set
			{
				this.m_dataField = value;
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x001070DB File Offset: 0x001052DB
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x001070E3 File Offset: 0x001052E3
		internal int AggregateIndicatorFieldIndex
		{
			get
			{
				return this.m_aggregateIndicatorFieldIndex;
			}
			set
			{
				this.m_aggregateIndicatorFieldIndex = value;
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x001070EC File Offset: 0x001052EC
		internal bool HasAggregateIndicatorField
		{
			get
			{
				return this.m_aggregateIndicatorFieldIndex != -1;
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x001070FA File Offset: 0x001052FA
		// (set) Token: 0x06003D54 RID: 15700 RVA: 0x00107102 File Offset: 0x00105302
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x0010710B File Offset: 0x0010530B
		internal bool IsCalculatedField
		{
			get
			{
				return this.m_dataField == null;
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x00107116 File Offset: 0x00105316
		// (set) Token: 0x06003D57 RID: 15703 RVA: 0x0010711E File Offset: 0x0010531E
		internal DataType DataType
		{
			get
			{
				return this.m_constantDataType;
			}
			set
			{
				this.m_constantDataType = value;
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x00107127 File Offset: 0x00105327
		// (set) Token: 0x06003D59 RID: 15705 RVA: 0x0010712F File Offset: 0x0010532F
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x00107138 File Offset: 0x00105338
		internal CalcFieldExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00107140 File Offset: 0x00105340
		internal void Initialize(InitializationContext context)
		{
			if (this.Value != null)
			{
				context.ExprHostBuilder.CalcFieldStart(this.m_name);
				this.m_value.Initialize("Field", context);
				context.ExprHostBuilder.GenericValue(this.m_value);
				this.m_exprHostID = context.ExprHostBuilder.CalcFieldEnd();
			}
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0010719C File Offset: 0x0010539C
		internal void SetExprHost(DataSetExprHost dataSetExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(dataSetExprHost != null && reportObjectModel != null, "(dataSetExprHost != null && reportObjectModel != null)");
				this.m_exprHost = dataSetExprHost.FieldHostsRemotable[this.ExprHostID];
				this.EnsureExprHostReportObjectModelBinding(reportObjectModel);
			}
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x001071E9 File Offset: 0x001053E9
		internal void EnsureExprHostReportObjectModelBinding(ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHost != null && this.m_lastReportOm != reportObjectModel)
			{
				this.m_lastReportOm = reportObjectModel;
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x00107210 File Offset: 0x00105410
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Field, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.DataField, Token.String),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.AggregateIndicatorFieldIndex, Token.Int32)
			});
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x001072A0 File Offset: 0x001054A0
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Field.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataField)
				{
					switch (memberName)
					{
					case MemberName.Name:
						writer.Write(this.m_name);
						continue;
					case MemberName.Value:
						writer.Write(this.m_value);
						continue;
					case MemberName.DataType:
						writer.WriteEnum((int)this.m_constantDataType);
						continue;
					default:
						if (memberName == MemberName.DataField)
						{
							writer.Write(this.m_dataField);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.AggregateIndicatorFieldIndex)
					{
						writer.Write(this.m_aggregateIndicatorFieldIndex);
						continue;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x0010737C File Offset: 0x0010557C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Field.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataField)
				{
					switch (memberName)
					{
					case MemberName.Name:
						this.m_name = reader.ReadString();
						continue;
					case MemberName.Value:
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DataType:
						this.m_constantDataType = (DataType)reader.ReadEnum();
						continue;
					default:
						if (memberName == MemberName.DataField)
						{
							this.m_dataField = reader.ReadString();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.AggregateIndicatorFieldIndex)
					{
						this.m_aggregateIndicatorFieldIndex = reader.ReadInt32();
						continue;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x0010745B File Offset: 0x0010565B
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, string.Empty);
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x0010746D File Offset: 0x0010566D
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Field;
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x00107474 File Offset: 0x00105674
		public int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0010747C File Offset: 0x0010567C
		public void SetID(int id)
		{
			this.m_id = id;
		}

		// Token: 0x04001CA7 RID: 7335
		private string m_name;

		// Token: 0x04001CA8 RID: 7336
		private string m_dataField;

		// Token: 0x04001CA9 RID: 7337
		private ExpressionInfo m_value;

		// Token: 0x04001CAA RID: 7338
		private int m_exprHostID = -1;

		// Token: 0x04001CAB RID: 7339
		private DataType m_constantDataType = DataType.String;

		// Token: 0x04001CAC RID: 7340
		private int m_aggregateIndicatorFieldIndex = -1;

		// Token: 0x04001CAD RID: 7341
		[NonSerialized]
		private const int AggregateIndicatorFieldNotSpecified = -1;

		// Token: 0x04001CAE RID: 7342
		[NonSerialized]
		private CalcFieldExprHost m_exprHost;

		// Token: 0x04001CAF RID: 7343
		[NonSerialized]
		private ObjectModelImpl m_lastReportOm;

		// Token: 0x04001CB0 RID: 7344
		[NonSerialized]
		private int m_id = int.MinValue;

		// Token: 0x04001CB1 RID: 7345
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Field.GetDeclaration();
	}
}
