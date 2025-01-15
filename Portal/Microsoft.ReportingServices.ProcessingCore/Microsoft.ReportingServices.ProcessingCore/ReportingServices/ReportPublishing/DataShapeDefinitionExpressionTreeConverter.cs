using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataShapeDefinition;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000387 RID: 903
	internal sealed class DataShapeDefinitionExpressionTreeConverter
	{
		// Token: 0x060022AC RID: 8876 RVA: 0x00084D4A File Offset: 0x00082F4A
		public DataShapeDefinitionExpressionTreeConverter(ExprHostCompiler exprHostCompiler)
		{
			this.m_exprHostCompiler = exprHostCompiler;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x00084D5C File Offset: 0x00082F5C
		public Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo Convert(ExpressionPart part)
		{
			switch (part.Kind)
			{
			case ExpressionPartKind.FieldReference:
				return this.ConvertFieldReference((FieldReferenceExpressionPart)part);
			case ExpressionPartKind.FirstFieldValue:
				return this.ConvertFirstFieldValue((FirstFieldValueExpressionPart)part);
			case ExpressionPartKind.FunctionCall:
				return this.ConvertFunctionCall((FunctionCallExpressionPart)part);
			case ExpressionPartKind.Literal:
				return this.ConvertLiteral((LiteralExpressionPart)part);
			case ExpressionPartKind.ScopedFieldReference:
				return this.ConvertScopedFieldReference((ScopedFieldReferenceExpressionPart)part);
			case ExpressionPartKind.ServerAggregate:
				return this.ConvertServerAggregate((ServerAggregateExpressionPart)part);
			default:
				Global.Tracer.Assert(false, "Invalid expression kind");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x00084DF4 File Offset: 0x00082FF4
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> Convert(IEnumerable<ExpressionPart> parts)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> list = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo>();
			foreach (ExpressionPart expressionPart in parts)
			{
				list.Add(this.Convert(expressionPart));
			}
			return list;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00084E4C File Offset: 0x0008304C
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ConvertLiteral(LiteralExpressionPart part)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.SetAsLiteral(new LiteralInfo(part.Value));
			return expressionInfo;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x00084E64 File Offset: 0x00083064
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ConvertFieldReference(FieldReferenceExpressionPart part)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.SetAsSimpleFieldReference(part.FieldName);
			return expressionInfo;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00084E78 File Offset: 0x00083078
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ConvertFunctionCall(FunctionCallExpressionPart part)
		{
			RdlFunctionInfo rdlFunctionInfo = new RdlFunctionInfo();
			rdlFunctionInfo.SetFunctionType(part.FunctionName);
			rdlFunctionInfo.Expressions = this.Convert(part.Arguments);
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.SetAsRdlFunction(rdlFunctionInfo);
			return expressionInfo;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00084EB5 File Offset: 0x000830B5
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ConvertFirstFieldValue(FirstFieldValueExpressionPart part)
		{
			return this.m_exprHostCompiler.CreateScopedFirstAggregate(part.FieldName, part.DataSetName);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00084ECE File Offset: 0x000830CE
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ConvertScopedFieldReference(ScopedFieldReferenceExpressionPart part)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.SetAsScopedFieldReference(part.ScopeName, part.FieldName);
			return expressionInfo;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00084EE8 File Offset: 0x000830E8
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ConvertServerAggregate(ServerAggregateExpressionPart part)
		{
			string text = this.CreateServerAggregateID();
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo
			{
				Type = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate,
				StringValue = text
			};
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo2 = this.ConvertFieldReference(part.FieldReference);
			expressionInfo2.OriginalText = "Fields!" + part.FieldReference.FieldName;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo
			{
				Name = text,
				AggregateType = Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate,
				Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[] { expressionInfo2 }
			};
			expressionInfo.HasAnyFieldReferences = true;
			expressionInfo.AddAggregate(dataAggregateInfo);
			return expressionInfo;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00084F73 File Offset: 0x00083173
		private string CreateServerAggregateID()
		{
			string text = "Aggregate" + this.m_aggregateCount.ToString();
			this.m_aggregateCount++;
			return text;
		}

		// Token: 0x0400151B RID: 5403
		private const string AggregateIdPrefix = "Aggregate";

		// Token: 0x0400151C RID: 5404
		private readonly ExprHostCompiler m_exprHostCompiler;

		// Token: 0x0400151D RID: 5405
		private int m_aggregateCount;
	}
}
