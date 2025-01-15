using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000308 RID: 776
	public class DocumentMapNode
	{
		// Token: 0x06001B14 RID: 6932 RVA: 0x000025F4 File Offset: 0x000007F4
		public DocumentMapNode()
		{
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0006DE9B File Offset: 0x0006C09B
		internal static DocumentMapNode CollectionToSoapStruct(DocumentMapNodeInfo docMap)
		{
			if (docMap == null)
			{
				return null;
			}
			return new DocumentMapNode(docMap);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0006DEA8 File Offset: 0x0006C0A8
		private DocumentMapNode(DocumentMapNodeInfo docMap)
		{
			this.Label = docMap.Label;
			this.UniqueName = docMap.Id;
			if (docMap.Children != null)
			{
				ArrayList arrayList = new ArrayList();
				DocumentMapNodeInfo[] children = docMap.Children;
				for (int i = 0; i < children.Length; i++)
				{
					DocumentMapNode documentMapNode = new DocumentMapNode(children[i]);
					arrayList.Add(documentMapNode);
				}
				this.Children = (DocumentMapNode[])arrayList.ToArray(typeof(DocumentMapNode));
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0006DF24 File Offset: 0x0006C124
		public static DocumentMapNode CollectionToSoapStruct(IDocumentMap docMap)
		{
			if (docMap == null)
			{
				return null;
			}
			Stack<DocumentMapNode.NodeStackEntry> stack = new Stack<DocumentMapNode.NodeStackEntry>();
			List<DocumentMapNode> list = new List<DocumentMapNode>();
			foreach (OnDemandDocumentMapNode onDemandDocumentMapNode in Enumerable<OnDemandDocumentMapNode>.Wrap(docMap))
			{
				DocumentMapNode.NodeStackEntry nodeStackEntry;
				nodeStackEntry.Node = DocumentMapNode.FromOnDemandNode(onDemandDocumentMapNode);
				nodeStackEntry.Level = onDemandDocumentMapNode.Level;
				while (stack.Count > 0 && onDemandDocumentMapNode.Level < stack.Peek().Level)
				{
					DocumentMapNode.CollapseTopLevel(stack, list);
				}
				stack.Push(nodeStackEntry);
			}
			RSTrace.CatalogTrace.Assert(stack.Count > 0, "nodeStack.Count > 0");
			while (stack.Count > 1)
			{
				DocumentMapNode.CollapseTopLevel(stack, list);
			}
			RSTrace.CatalogTrace.Assert(stack.Count == 1, "nodeStack.Count == 1");
			return stack.Pop().Node;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0006E018 File Offset: 0x0006C218
		private static void CollapseTopLevel(Stack<DocumentMapNode.NodeStackEntry> nodeStack, List<DocumentMapNode> workspace)
		{
			if (nodeStack == null || nodeStack.Count <= 1)
			{
				return;
			}
			int level = nodeStack.Peek().Level;
			workspace.Clear();
			while (nodeStack.Peek().Level == level)
			{
				workspace.Add(nodeStack.Pop().Node);
			}
			RSTrace.CatalogTrace.Assert(nodeStack.Count > 0, "nodeStack.Count > 0");
			RSTrace.CatalogTrace.Assert(workspace.Count > 0, "workspace.Count > 0");
			DocumentMapNode node = nodeStack.Peek().Node;
			node.Children = new DocumentMapNode[workspace.Count];
			for (int i = workspace.Count - 1; i >= 0; i--)
			{
				node.Children[workspace.Count - i - 1] = workspace[i];
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0006E0DD File Offset: 0x0006C2DD
		private static DocumentMapNode FromOnDemandNode(OnDemandDocumentMapNode node)
		{
			return new DocumentMapNode
			{
				UniqueName = node.Id,
				Label = node.Label
			};
		}

		// Token: 0x04000A6D RID: 2669
		public string Label;

		// Token: 0x04000A6E RID: 2670
		public string UniqueName;

		// Token: 0x04000A6F RID: 2671
		public DocumentMapNode[] Children;

		// Token: 0x020004F1 RID: 1265
		private struct NodeStackEntry
		{
			// Token: 0x040011A4 RID: 4516
			public DocumentMapNode Node;

			// Token: 0x040011A5 RID: 4517
			public int Level;
		}
	}
}
