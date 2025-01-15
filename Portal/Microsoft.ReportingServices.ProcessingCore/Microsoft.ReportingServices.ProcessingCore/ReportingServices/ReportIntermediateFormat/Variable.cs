using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000527 RID: 1319
	internal sealed class Variable : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004733 RID: 18227 RVA: 0x0012A6C4 File Offset: 0x001288C4
		internal Variable()
		{
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x0012A6D3 File Offset: 0x001288D3
		// (set) Token: 0x06004735 RID: 18229 RVA: 0x0012A6DB File Offset: 0x001288DB
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

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06004736 RID: 18230 RVA: 0x0012A6E4 File Offset: 0x001288E4
		// (set) Token: 0x06004737 RID: 18231 RVA: 0x0012A6EC File Offset: 0x001288EC
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

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x0012A6F5 File Offset: 0x001288F5
		// (set) Token: 0x06004739 RID: 18233 RVA: 0x0012A6FD File Offset: 0x001288FD
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

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x0012A706 File Offset: 0x00128906
		// (set) Token: 0x0600473B RID: 18235 RVA: 0x0012A70E File Offset: 0x0012890E
		internal int SequenceID
		{
			get
			{
				return this.m_sequenceID;
			}
			set
			{
				this.m_sequenceID = value;
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x0012A717 File Offset: 0x00128917
		// (set) Token: 0x0600473D RID: 18237 RVA: 0x0012A71F File Offset: 0x0012891F
		internal bool Writable
		{
			get
			{
				return this.m_writable;
			}
			set
			{
				this.m_writable = value;
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x0012A728 File Offset: 0x00128928
		internal void Initialize(InitializationContext context)
		{
			if (this.m_value != null)
			{
				this.m_value.Initialize(this.GetPropertyName(), context);
			}
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x0012A744 File Offset: 0x00128944
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			Variable variable = (Variable)base.MemberwiseClone();
			variable.SequenceID = context.GenerateVariableSequenceID();
			variable.m_isClone = true;
			if (this.m_name != null)
			{
				variable.m_name = context.CreateUniqueVariableName(this.m_name, this.m_isClone);
			}
			if (this.m_value != null)
			{
				variable.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			return variable;
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x0012A7B4 File Offset: 0x001289B4
		internal string GetPropertyName()
		{
			if (this.m_propertyName == null)
			{
				StringBuilder stringBuilder = new StringBuilder("Variable");
				stringBuilder.Append("(");
				stringBuilder.Append(this.m_name);
				stringBuilder.Append(")");
				this.m_propertyName = stringBuilder.ToString();
			}
			return this.m_propertyName;
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x0012A80C File Offset: 0x00128A0C
		internal VariableImpl GetCachedVariableObj(OnDemandProcessingContext odpContext)
		{
			if (this.m_cachedVariableObj == null)
			{
				VariableImpl variableImpl = odpContext.ReportObjectModel.VariablesImpl[this.m_name] as VariableImpl;
				this.m_cachedVariableObj = variableImpl;
			}
			return this.m_cachedVariableObj;
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x0012A84C File Offset: 0x00128A4C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Variable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.SequenceID, Token.Int32),
				new MemberInfo(MemberName.Writable, Token.Boolean)
			});
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x0012A8C8 File Offset: 0x00128AC8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Variable.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Name:
					writer.Write(this.m_name);
					break;
				case MemberName.Value:
					writer.Write(this.m_value);
					break;
				case MemberName.DataType:
					writer.WriteEnum((int)this.m_constantDataType);
					break;
				default:
					if (memberName != MemberName.SequenceID)
					{
						if (memberName != MemberName.Writable)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_writable);
						}
					}
					else
					{
						writer.Write(this.m_sequenceID);
					}
					break;
				}
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x0012A97C File Offset: 0x00128B7C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Variable.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Name:
					this.m_name = reader.ReadString();
					break;
				case MemberName.Value:
					this.m_value = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.DataType:
					this.m_constantDataType = (DataType)reader.ReadEnum();
					break;
				default:
					if (memberName != MemberName.SequenceID)
					{
						if (memberName != MemberName.Writable)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_writable = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_sequenceID = reader.ReadInt32();
					}
					break;
				}
			}
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x0012AA35 File Offset: 0x00128C35
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x0012AA42 File Offset: 0x00128C42
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Variable;
		}

		// Token: 0x04001FD1 RID: 8145
		private DataType m_constantDataType;

		// Token: 0x04001FD2 RID: 8146
		private string m_name;

		// Token: 0x04001FD3 RID: 8147
		private ExpressionInfo m_value;

		// Token: 0x04001FD4 RID: 8148
		private int m_sequenceID = -1;

		// Token: 0x04001FD5 RID: 8149
		private bool m_writable;

		// Token: 0x04001FD6 RID: 8150
		[NonSerialized]
		private bool m_isClone;

		// Token: 0x04001FD7 RID: 8151
		[NonSerialized]
		private string m_propertyName;

		// Token: 0x04001FD8 RID: 8152
		[NonSerialized]
		private VariableImpl m_cachedVariableObj;

		// Token: 0x04001FD9 RID: 8153
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Variable.GetDeclaration();
	}
}
