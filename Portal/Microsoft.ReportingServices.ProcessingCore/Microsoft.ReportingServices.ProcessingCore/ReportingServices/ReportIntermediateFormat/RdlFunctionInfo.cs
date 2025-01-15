using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C6 RID: 1222
	internal sealed class RdlFunctionInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x00109E55 File Offset: 0x00108055
		// (set) Token: 0x06003E19 RID: 15897 RVA: 0x00109E5D File Offset: 0x0010805D
		internal RdlFunctionInfo.RdlFunctionType FunctionType
		{
			get
			{
				return this.m_functionType;
			}
			set
			{
				this.m_functionType = value;
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x00109E66 File Offset: 0x00108066
		// (set) Token: 0x06003E1B RID: 15899 RVA: 0x00109E6E File Offset: 0x0010806E
		internal List<ExpressionInfo> Expressions
		{
			get
			{
				return this.m_simpleExpressions;
			}
			set
			{
				this.m_simpleExpressions = value;
			}
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00109E77 File Offset: 0x00108077
		internal void SetFunctionType(string functionName)
		{
			this.FunctionType = (RdlFunctionInfo.RdlFunctionType)Enum.Parse(typeof(RdlFunctionInfo.RdlFunctionType), functionName, true);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x00109E98 File Offset: 0x00108098
		internal void Initialize(string propertyName, InitializationContext context, bool initializeDataOnError)
		{
			foreach (ExpressionInfo expressionInfo in this.m_simpleExpressions)
			{
				expressionInfo.Initialize(propertyName, context, initializeDataOnError);
			}
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00109EEC File Offset: 0x001080EC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			RdlFunctionInfo rdlFunctionInfo = (RdlFunctionInfo)base.MemberwiseClone();
			rdlFunctionInfo.m_simpleExpressions = new List<ExpressionInfo>(this.m_simpleExpressions.Count);
			foreach (ExpressionInfo expressionInfo in this.m_simpleExpressions)
			{
				rdlFunctionInfo.m_simpleExpressions.Add((ExpressionInfo)expressionInfo.PublishClone(context));
			}
			return rdlFunctionInfo;
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00109F74 File Offset: 0x00108174
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RdlFunctionInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.RdlFunctionType, Token.Enum),
				new MemberInfo(MemberName.Expressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00109FB8 File Offset: 0x001081B8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RdlFunctionInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Expressions)
				{
					if (memberName == MemberName.RdlFunctionType)
					{
						writer.WriteEnum((int)this.m_functionType);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write<ExpressionInfo>(this.m_simpleExpressions);
				}
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x0010A020 File Offset: 0x00108220
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RdlFunctionInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Expressions)
				{
					if (memberName == MemberName.RdlFunctionType)
					{
						this.m_functionType = (RdlFunctionInfo.RdlFunctionType)reader.ReadEnum();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_simpleExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
				}
			}
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x0010A087 File Offset: 0x00108287
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x0010A094 File Offset: 0x00108294
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RdlFunctionInfo;
		}

		// Token: 0x04001CF4 RID: 7412
		private RdlFunctionInfo.RdlFunctionType m_functionType;

		// Token: 0x04001CF5 RID: 7413
		private List<ExpressionInfo> m_simpleExpressions;

		// Token: 0x04001CF6 RID: 7414
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RdlFunctionInfo.GetDeclaration();

		// Token: 0x0200097B RID: 2427
		internal enum RdlFunctionType
		{
			// Token: 0x040040F0 RID: 16624
			MinValue,
			// Token: 0x040040F1 RID: 16625
			MaxValue,
			// Token: 0x040040F2 RID: 16626
			ScopeKeys,
			// Token: 0x040040F3 RID: 16627
			Comparable,
			// Token: 0x040040F4 RID: 16628
			Array
		}
	}
}
