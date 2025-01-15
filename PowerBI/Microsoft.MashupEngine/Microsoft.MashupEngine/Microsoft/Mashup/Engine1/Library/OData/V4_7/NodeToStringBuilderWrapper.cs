using System;
using System.Reflection;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000769 RID: 1897
	internal static class NodeToStringBuilderWrapper
	{
		// Token: 0x060037E6 RID: 14310 RVA: 0x000B338C File Offset: 0x000B158C
		static NodeToStringBuilderWrapper()
		{
			Assembly assembly = typeof(ODataUri).Assembly;
			if (assembly != null)
			{
				Type type = assembly.GetType("Microsoft.OData.NodeToStringBuilder");
				if (type != null)
				{
					NodeToStringBuilderWrapper.nodeToStringBuilderInstance = Activator.CreateInstance(type);
					NodeToStringBuilderWrapper.translateNodeMethodInfo = type.GetMethod("TranslateNode", BindingFlags.Instance | BindingFlags.NonPublic);
					NodeToStringBuilderWrapper.translateFilterClauseMethodInfo = type.GetMethod("TranslateFilterClause", BindingFlags.Instance | BindingFlags.NonPublic);
				}
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000B33F8 File Offset: 0x000B15F8
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

		// Token: 0x060037E8 RID: 14312 RVA: 0x000B343A File Offset: 0x000B163A
		public static bool TryTranslateNode(QueryNode node, out string translated)
		{
			return NodeToStringBuilderWrapper.TryInvokeTranslateMethod(NodeToStringBuilderWrapper.translateNodeMethodInfo, new object[] { node }, out translated);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000B3451 File Offset: 0x000B1651
		public static bool TryTranslateFilterClause(FilterClause filterClause, out string translated)
		{
			return NodeToStringBuilderWrapper.TryInvokeTranslateMethod(NodeToStringBuilderWrapper.translateFilterClauseMethodInfo, new object[] { filterClause }, out translated);
		}

		// Token: 0x04001CEE RID: 7406
		private const string NodeToStringBuilderFQN = "Microsoft.OData.NodeToStringBuilder";

		// Token: 0x04001CEF RID: 7407
		private const string TranslateNodeMethod = "TranslateNode";

		// Token: 0x04001CF0 RID: 7408
		private const string TranslateFilterClauseMethod = "TranslateFilterClause";

		// Token: 0x04001CF1 RID: 7409
		private static object nodeToStringBuilderInstance;

		// Token: 0x04001CF2 RID: 7410
		private static MethodInfo translateNodeMethodInfo;

		// Token: 0x04001CF3 RID: 7411
		private static MethodInfo translateFilterClauseMethodInfo;
	}
}
