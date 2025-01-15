using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000595 RID: 1429
	[DataContract]
	internal sealed class FunctionCallExpressionPart : ExpressionPart
	{
		// Token: 0x060051CC RID: 20940 RVA: 0x0015A12B File Offset: 0x0015832B
		public FunctionCallExpressionPart(string functionName, IEnumerable<ExpressionPart> arguments)
		{
			this.m_functionName = functionName;
			this.m_arguments = arguments.ToReadOnlyCollection<ExpressionPart>();
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x0015A146 File Offset: 0x00158346
		internal override ExpressionPartKind Kind
		{
			get
			{
				return ExpressionPartKind.FunctionCall;
			}
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x060051CE RID: 20942 RVA: 0x0015A149 File Offset: 0x00158349
		public string FunctionName
		{
			get
			{
				return this.m_functionName;
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x0015A151 File Offset: 0x00158351
		public IEnumerable<ExpressionPart> Arguments
		{
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0015A15C File Offset: 0x0015835C
		public override bool Equals(ExpressionPart other)
		{
			FunctionCallExpressionPart functionCallExpressionPart = other as FunctionCallExpressionPart;
			return functionCallExpressionPart != null && this.FunctionName == functionCallExpressionPart.FunctionName && this.Arguments.SequenceEqual(functionCallExpressionPart.Arguments);
		}

		// Token: 0x04002953 RID: 10579
		[DataMember(Name = "FunctionName", Order = 1)]
		private readonly string m_functionName;

		// Token: 0x04002954 RID: 10580
		[DataMember(Name = "Arguments", Order = 2)]
		private readonly IEnumerable<ExpressionPart> m_arguments;
	}
}
