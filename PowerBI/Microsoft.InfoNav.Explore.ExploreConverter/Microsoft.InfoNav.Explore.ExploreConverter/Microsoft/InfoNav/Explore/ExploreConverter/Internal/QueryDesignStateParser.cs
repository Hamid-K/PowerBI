using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000076 RID: 118
	internal static class QueryDesignStateParser
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000BB3A File Offset: 0x00009D3A
		internal static QueryDesignState ParseDesignerState(XElement designerStateNode)
		{
			return new QueryDesignState(QueryDesignStateParser.ParseQueryDefinition(designerStateNode.RequiredElementByLocalName("QueryDefinition")));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000BB54 File Offset: 0x00009D54
		private static QueryDesignGroup ParseGroup(XElement groupNode)
		{
			IRdmQueryExpression rdmQueryExpression = null;
			string text = string.Empty;
			XElement xelement = groupNode.ElementByLocalName("Key");
			if (xelement != null)
			{
				rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(xelement);
			}
			XElement xelement2 = groupNode.ElementByLocalName("Name");
			if (xelement2 != null)
			{
				text = xelement2.Value;
			}
			return new QueryDesignGroup(rdmQueryExpression, text);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000BB9C File Offset: 0x00009D9C
		private static QueryDesignMeasure ParseMeasure(XElement measureNode)
		{
			IRdmQueryExpression rdmQueryExpression = null;
			string text = string.Empty;
			XElement xelement = measureNode.ElementByLocalName("Expression");
			if (xelement != null)
			{
				rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(xelement);
			}
			XElement xelement2 = measureNode.ElementByLocalName("Name");
			if (xelement2 != null)
			{
				text = xelement2.Value;
			}
			return new QueryDesignMeasure(rdmQueryExpression, text);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		private static QueryDesignQueryDefinition ParseQueryDefinition(XElement queryDefinitionNode)
		{
			XElement xelement = queryDefinitionNode.ElementByLocalName("Groups");
			Dictionary<string, QueryDesignGroup> dictionary = new Dictionary<string, QueryDesignGroup>();
			if (xelement != null)
			{
				dictionary = (from @group in xelement.ElementsByLocalName("Group")
					select QueryDesignStateParser.ParseGroup(@group)).ToDictionary((QueryDesignGroup x) => x.Name, (QueryDesignGroup x) => x);
			}
			XElement xelement2 = queryDefinitionNode.ElementByLocalName("Measures");
			Dictionary<string, QueryDesignMeasure> dictionary2 = new Dictionary<string, QueryDesignMeasure>();
			if (xelement2 != null)
			{
				dictionary2 = (from measure in xelement2.ElementsByLocalName("Measure")
					select QueryDesignStateParser.ParseMeasure(measure)).ToDictionary((QueryDesignMeasure x) => x.Name, (QueryDesignMeasure x) => x);
			}
			return new QueryDesignQueryDefinition(dictionary, dictionary2);
		}
	}
}
