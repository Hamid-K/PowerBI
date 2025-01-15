using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001DB RID: 475
	internal class ReportExpressionUtils
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00025AA4 File Offset: 0x00023CA4
		internal static void GetDependencies(IList<ReportObject> dependencies, ReportObject parent, string Expression)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (dependencies == null)
			{
				throw new ArgumentNullException("dependencies");
			}
			if (string.IsNullOrEmpty(Expression))
			{
				return;
			}
			ReportObject reportObject = null;
			Expression expression = ExpressionFactory.CreateExpression(Expression, true);
			if (expression == null)
			{
				return;
			}
			if (expression.ObjectDependencyList != null && expression.ObjectDependencyList.Count > 0)
			{
				foreach (IInternalExpression internalExpression in expression.ObjectDependencyList)
				{
					if (internalExpression is FunctionField)
					{
						if (((FunctionField)internalExpression).Fld != null)
						{
							reportObject = ((FunctionField)internalExpression).Fld.GetAncestor<DataSet>();
						}
						if (reportObject == null)
						{
							IList<DataSet> dataSets = parent.GetAncestor<Report>().DataSets;
							if (dataSets.Count == 1 && parent.GetAncestor<DataSet>() != dataSets[0])
							{
								reportObject = dataSets[0];
							}
						}
					}
					else if (!(internalExpression is FunctionTextbox))
					{
						if (internalExpression is FunctionReportParameter)
						{
							using (IEnumerator<ReportParameter> enumerator2 = parent.GetAncestor<Report>().ReportParameters.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ReportParameter reportParameter = enumerator2.Current;
									if (string.Equals(reportParameter.Name, ((FunctionReportParameter)internalExpression).Name, StringComparison.Ordinal))
									{
										reportObject = reportParameter;
										break;
									}
								}
								goto IL_01D8;
							}
						}
						if (internalExpression is ConstantString)
						{
							using (IEnumerator<DataSet> enumerator3 = parent.GetAncestor<Report>().DataSets.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									DataSet dataSet = enumerator3.Current;
									if (string.Equals(dataSet.Name, ((ConstantString)internalExpression).ConstantValue, StringComparison.Ordinal))
									{
										reportObject = dataSet;
										break;
									}
								}
								goto IL_01D8;
							}
						}
						if (internalExpression is FunctionMethodOrProperty && ((FunctionMethodOrProperty)internalExpression).MethodName.ToUpperInvariant() == "CODE" && parent.GetAncestor<Report>().Code != null)
						{
							reportObject = new CodeObject(parent.GetAncestor<Report>().Code);
						}
					}
					IL_01D8:
					if (reportObject != null && !dependencies.Contains(reportObject))
					{
						dependencies.Add(reportObject);
					}
				}
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00025D04 File Offset: 0x00023F04
		internal static string UpdateNamedReferences(string Expression, NameChanges RenameList)
		{
			Expression expression = ExpressionFactory.CreateExpression(Expression, true);
			if (expression == null || expression.InternalExpression == null || expression.ObjectDependencyList.Count == 0)
			{
				return Expression;
			}
			if (expression.InternalExpression is ConstantNonExpression)
			{
				return expression.InternalExpression.WriteSource(RenameList);
			}
			return "=" + expression.InternalExpression.WriteSource(RenameList);
		}
	}
}
