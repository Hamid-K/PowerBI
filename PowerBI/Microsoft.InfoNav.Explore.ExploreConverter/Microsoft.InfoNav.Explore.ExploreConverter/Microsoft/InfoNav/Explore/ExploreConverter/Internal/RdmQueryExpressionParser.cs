using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200007C RID: 124
	internal static class RdmQueryExpressionParser
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000BF2C File Offset: 0x0000A12C
		public static IRdmQueryExpression ParseQueryExpression(XElement expressionNode)
		{
			if (expressionNode == null)
			{
				return null;
			}
			string value = expressionNode.RequiredAttributeByLocalName("type").Value;
			if ("QueryFieldExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryFieldExpression(expressionNode);
			}
			if ("QueryCalculateExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryCalculateExpression(expressionNode);
			}
			if ("QueryFunctionExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryFunctionExpression(expressionNode);
			}
			if ("QueryScanExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryScanExpression(expressionNode);
			}
			if ("QdmEntityPlaceholderExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQdmEntityPlaceholderExpression(expressionNode);
			}
			if ("QueryVariableReferenceExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryVariableReferenceExpression(expressionNode);
			}
			if ("QueryProjectExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryProjectExpression(expressionNode);
			}
			if ("QueryLiteralExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryLiteralExpression(expressionNode);
			}
			if ("QueryMeasureExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryMeasureExpression(expressionNode);
			}
			if ("QueryNullExpression" == value)
			{
				return new RdmQueryNullExpression();
			}
			string prefixOfNamespace = expressionNode.GetPrefixOfNamespace("http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportstate");
			if (!string.IsNullOrEmpty(prefixOfNamespace) && prefixOfNamespace + ":QueryHierarchyLevelExpression" == value)
			{
				return RdmQueryExpressionParser.ParseQueryHierarchyLevelExpression(expressionNode);
			}
			throw new ArgumentException("Expect the QueryExpression to be parseable");
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C054 File Offset: 0x0000A254
		private static RdmQueryFieldExpression ParseQueryFieldExpression(XElement expressionNode)
		{
			string value = expressionNode.RequiredElementByLocalName("Field").Value;
			IRdmQueryExpression rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(expressionNode.ElementByLocalName("Instance"));
			return new RdmQueryFieldExpression(value, rdmQueryExpression);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C088 File Offset: 0x0000A288
		private static QdmEntityPlaceholderExpression ParseQdmEntityPlaceholderExpression(XElement expressionNode)
		{
			XElement xelement = expressionNode.ElementByLocalName("Target");
			if (xelement != null)
			{
				xelement = expressionNode.RequiredElementByLocalName("Target");
			}
			return new QdmEntityPlaceholderExpression(xelement.Value);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000C0BB File Offset: 0x0000A2BB
		private static RdmQueryScanExpression ParseQueryScanExpression(XElement expressionNode)
		{
			return new RdmQueryScanExpression(expressionNode.RequiredElementByLocalName("Target").Value);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
		private static RdmQueryVariableReferenceExpression ParseQueryVariableReferenceExpression(XElement expressionNode)
		{
			return new RdmQueryVariableReferenceExpression(expressionNode.RequiredElementByLocalName("VariableName").Value);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C0E9 File Offset: 0x0000A2E9
		private static RdmQueryCalculateExpression ParseQueryCalculateExpression(XElement expressionNode)
		{
			return new RdmQueryCalculateExpression(RdmQueryExpressionParser.ParseQueryExpression(expressionNode.ElementByLocalName("Argument")));
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C100 File Offset: 0x0000A300
		private static RdmQueryFunctionExpression ParseQueryFunctionExpression(XElement expressionNode)
		{
			string value = expressionNode.RequiredElementByLocalName("FunctionName").Value;
			IEnumerable<IRdmQueryExpression> enumerable = new List<IRdmQueryExpression>();
			enumerable = from expr in expressionNode.ElementByLocalName("Arguments").ElementsByLocalName("QueryExpression")
				select RdmQueryExpressionParser.ParseQueryExpression(expr);
			return new RdmQueryFunctionExpression(value, enumerable.ToList<IRdmQueryExpression>());
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C168 File Offset: 0x0000A368
		private static RdmQueryProjectExpression ParseQueryProjectExpression(XElement expressionNode)
		{
			XElement xelement = expressionNode.RequiredElementByLocalName("Input");
			IRdmQueryExpression rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(xelement.ElementByLocalName("Expression"));
			string value = xelement.RequiredElementByLocalName("VariableName").Value;
			IRdmQueryExpression rdmQueryExpression2 = RdmQueryExpressionParser.ParseQueryExpression(expressionNode.ElementByLocalName("Projection"));
			return new RdmQueryProjectExpression(rdmQueryExpression, value, rdmQueryExpression2);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		private static RdmQueryLiteralExpression ParseQueryLiteralExpression(XElement expressionNode)
		{
			XElement xelement = expressionNode.ElementByLocalName("Value");
			if (xelement != null)
			{
				string value = xelement.RequiredAttributeByLocalName("type").Value;
				int num = value.IndexOf(':');
				Contract.Check(num != -1, "Invalid Literal");
				string text = value.Substring(0, num);
				Contract.Check(xelement.GetNamespaceOfPrefix(text) == "http://www.w3.org/2001/XMLSchema", "Invalid Namespace for type");
				string text2 = value.Substring(num + 1);
				string value2 = xelement.Value;
				PrimitiveValue primitiveValue;
				if (text2 == "string")
				{
					primitiveValue = new TextPrimitiveValue("'" + value2 + "'");
				}
				else if (text2 == "long")
				{
					primitiveValue = new TextPrimitiveValue(value2 + "L");
				}
				else if (text2 == "double")
				{
					primitiveValue = new TextPrimitiveValue(value2 + "D");
				}
				else if (text2 == "decimal")
				{
					primitiveValue = new TextPrimitiveValue(value2 + "M");
				}
				else if (text2 == "boolean")
				{
					primitiveValue = (bool.Parse(value2) ? BooleanPrimitiveValue.True : BooleanPrimitiveValue.False);
				}
				else
				{
					if (!(text2 == "dateTime"))
					{
						throw new InvalidOperationException("Unsupported type " + value);
					}
					primitiveValue = new DateTimePrimitiveValue(DateTime.Parse(value2, CultureInfo.InvariantCulture));
				}
				return new RdmQueryLiteralExpression(primitiveValue);
			}
			return null;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C338 File Offset: 0x0000A538
		private static RdmQueryMeasureExpression ParseQueryMeasureExpression(XElement expressionNode)
		{
			string value = expressionNode.RequiredElementByLocalName("Measure").Value;
			string value2 = expressionNode.RequiredElementByLocalName("Target").Value;
			return new RdmQueryMeasureExpression(value, value2);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C36C File Offset: 0x0000A56C
		private static RdmQueryHierarchyLevelExpression ParseQueryHierarchyLevelExpression(XElement expressionNode)
		{
			string value = expressionNode.RequiredElementByLocalName("Hierarchy").Value;
			string value2 = expressionNode.RequiredElementByLocalName("HierarchyLevel").Value;
			IRdmQueryExpression rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(expressionNode.ElementByLocalName("Instance"));
			return new RdmQueryHierarchyLevelExpression(value, value2, rdmQueryExpression);
		}
	}
}
