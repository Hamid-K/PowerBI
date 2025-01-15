using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000508 RID: 1288
	internal class ExpressionInfoTypeValuePair : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060043D2 RID: 17362 RVA: 0x0011C72C File Offset: 0x0011A92C
		internal ExpressionInfoTypeValuePair(DataType constantType, bool hadExplicitDataType, ExpressionInfo value)
		{
			this.m_constantDataType = constantType;
			this.m_hadExplicitDataType = hadExplicitDataType;
			this.m_value = value;
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x0011C749 File Offset: 0x0011A949
		internal ExpressionInfoTypeValuePair()
		{
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x0011C751 File Offset: 0x0011A951
		internal DataType DataType
		{
			get
			{
				return this.m_constantDataType;
			}
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x0011C759 File Offset: 0x0011A959
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x0011C761 File Offset: 0x0011A961
		internal bool HadExplicitDataType
		{
			get
			{
				return this.m_hadExplicitDataType;
			}
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x0011C76C File Offset: 0x0011A96C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfoTypeValuePair, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x0011C7AC File Offset: 0x0011A9AC
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ExpressionInfoTypeValuePair.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName == MemberName.DataType)
					{
						writer.WriteEnum((int)this.m_constantDataType);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_value);
				}
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0011C810 File Offset: 0x0011AA10
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ExpressionInfoTypeValuePair.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName == MemberName.DataType)
					{
						this.m_constantDataType = (DataType)reader.ReadEnum();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_value = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x0011C879 File Offset: 0x0011AA79
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x0011C886 File Offset: 0x0011AA86
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfoTypeValuePair;
		}

		// Token: 0x04001EDB RID: 7899
		private DataType m_constantDataType;

		// Token: 0x04001EDC RID: 7900
		private ExpressionInfo m_value;

		// Token: 0x04001EDD RID: 7901
		[NonSerialized]
		private bool m_hadExplicitDataType;

		// Token: 0x04001EDE RID: 7902
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ExpressionInfoTypeValuePair.GetDeclaration();
	}
}
