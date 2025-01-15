using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A1 RID: 161
	public sealed class ExpressionCopyManager
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0001A340 File Offset: 0x00018540
		internal ExpressionCopyManager(SemanticQuery targetQuery)
		{
			if (targetQuery == null)
			{
				throw new InternalModelingException("targetQuery is null");
			}
			this.m_targetQuery = targetQuery;
			foreach (Expression expression in this.m_targetQuery.GetAllTopLevelExpressions())
			{
				this.m_targetCalcAttrNameGen.AddExistingName(expression.Name);
			}
			foreach (Parameter parameter in this.m_targetQuery.Parameters)
			{
				this.m_targetParamNameGen.AddExistingName(parameter.Name);
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001A434 File Offset: 0x00018634
		public Expression GetCalculatedAttributeInTarget(Expression sourceCalcAttr)
		{
			Expression expression;
			if (!this.m_sourceCalcAttrToTarget.TryGetValue(sourceCalcAttr, out expression))
			{
				this.m_sourceCalcAttrToTarget.Add(sourceCalcAttr, null);
				expression = sourceCalcAttr.Clone(this);
				expression.Name = this.m_targetCalcAttrNameGen.CreateName(sourceCalcAttr.Name);
				this.m_targetQuery.CalculatedAttributes.Add(expression);
				this.m_sourceCalcAttrToTarget[sourceCalcAttr] = expression;
				return expression;
			}
			if (expression == null)
			{
				throw new ValidationException(ModelingErrorCode.CyclicExpression, sourceCalcAttr, SRErrors.CyclicExpression_ExpressionObject(SRObjectDescriptor.FromScope(sourceCalcAttr)));
			}
			return expression;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001A4B8 File Offset: 0x000186B8
		public Parameter GetParameterInTarget(Parameter sourceParam)
		{
			Parameter parameter;
			if (!this.m_sourceParamToTarget.TryGetValue(sourceParam, out parameter))
			{
				this.m_sourceParamToTarget.Add(sourceParam, null);
				parameter = sourceParam.Clone(this);
				parameter.Name = this.m_targetParamNameGen.CreateName(sourceParam.Name);
				this.m_targetQuery.Parameters.Add(parameter);
				this.m_sourceParamToTarget[sourceParam] = parameter;
				return parameter;
			}
			if (parameter == null)
			{
				throw new ValidationException(ModelingErrorCode.CyclicExpression, sourceParam, SRErrors.CyclicExpression(SRObjectDescriptor.FromScope(sourceParam)));
			}
			return parameter;
		}

		// Token: 0x040003B0 RID: 944
		private readonly SemanticQuery m_targetQuery;

		// Token: 0x040003B1 RID: 945
		private readonly NameGenerator m_targetCalcAttrNameGen = new NameGenerator();

		// Token: 0x040003B2 RID: 946
		private readonly Dictionary<Expression, Expression> m_sourceCalcAttrToTarget = new Dictionary<Expression, Expression>();

		// Token: 0x040003B3 RID: 947
		private readonly NameGenerator m_targetParamNameGen = new NameGenerator();

		// Token: 0x040003B4 RID: 948
		private readonly Dictionary<Parameter, Parameter> m_sourceParamToTarget = new Dictionary<Parameter, Parameter>();
	}
}
