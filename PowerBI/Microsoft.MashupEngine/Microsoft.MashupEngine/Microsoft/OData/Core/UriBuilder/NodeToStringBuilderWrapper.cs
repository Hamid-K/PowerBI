using System;
using System.Reflection;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriBuilder
{
	// Token: 0x02000174 RID: 372
	internal static class NodeToStringBuilderWrapper
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0000BE20 File Offset: 0x0000A020
		static NodeToStringBuilderWrapper()
		{
			Assembly assembly = typeof(ODataUriBuilder).Assembly;
			if (assembly != null)
			{
				Type type = assembly.GetType("Microsoft.OData.Core.UriBuilder.NodeToStringBuilder");
				if (type != null)
				{
					NodeToStringBuilderWrapper.nodeToStringBuilderInstance = Activator.CreateInstance(type);
					NodeToStringBuilderWrapper.translateNodeMethodInfo = type.GetMethod("TranslateNode", BindingFlags.Instance | BindingFlags.NonPublic);
					NodeToStringBuilderWrapper.translateFilterClauseMethodInfo = type.GetMethod("TranslateFilterClause", BindingFlags.Instance | BindingFlags.NonPublic);
				}
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000BE8C File Offset: 0x0000A08C
		private static bool TryInvokeTranslateMethod(MethodInfo methodInfo, object[] parameters, out string translated)
		{
			if (methodInfo != null && NodeToStringBuilderWrapper.nodeToStringBuilderInstance != null)
			{
				object obj = methodInfo.Invoke(NodeToStringBuilderWrapper.nodeToStringBuilderInstance, parameters);
				translated = ((obj != null) ? ((string)obj) : null);
				return true;
			}
			translated = string.Empty;
			return false;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000BECE File Offset: 0x0000A0CE
		public static bool TryTranslateNode(QueryNode node, out string translated)
		{
			return NodeToStringBuilderWrapper.TryInvokeTranslateMethod(NodeToStringBuilderWrapper.translateNodeMethodInfo, new object[] { node }, out translated);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		public static bool TryTranslateFilterClause(FilterClause filterClause, out string translated)
		{
			return NodeToStringBuilderWrapper.TryInvokeTranslateMethod(NodeToStringBuilderWrapper.translateFilterClauseMethodInfo, new object[] { filterClause }, out translated);
		}

		// Token: 0x04000457 RID: 1111
		private const string NodeToStringBuilderFQN = "Microsoft.OData.Core.UriBuilder.NodeToStringBuilder";

		// Token: 0x04000458 RID: 1112
		private const string TranslateNodeMethod = "TranslateNode";

		// Token: 0x04000459 RID: 1113
		private const string TranslateFilterClauseMethod = "TranslateFilterClause";

		// Token: 0x0400045A RID: 1114
		private static object nodeToStringBuilderInstance;

		// Token: 0x0400045B RID: 1115
		private static MethodInfo translateNodeMethodInfo;

		// Token: 0x0400045C RID: 1116
		private static MethodInfo translateFilterClauseMethodInfo;
	}
}
