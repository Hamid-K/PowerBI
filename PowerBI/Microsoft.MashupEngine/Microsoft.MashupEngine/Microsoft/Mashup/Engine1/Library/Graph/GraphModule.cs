using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Graph
{
	// Token: 0x02000AEB RID: 2795
	internal sealed class GraphModule : Module
	{
		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x001023FB File Offset: 0x001005FB
		public override string Name
		{
			get
			{
				return "Graph";
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x00102402 File Offset: 0x00100602
		public override Keys ExportKeys
		{
			get
			{
				if (GraphModule.exportKeys == null)
				{
					GraphModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Graph.Nodes";
						}
						throw new InvalidOperationException();
					});
				}
				return GraphModule.exportKeys;
			}
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x0010243A File Offset: 0x0010063A
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return GraphModule.Graph.Nodes;
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x040029B3 RID: 10675
		public const string NameKey = "Name";

		// Token: 0x040029B4 RID: 10676
		public const string ValueKey = "Value";

		// Token: 0x040029B5 RID: 10677
		public const string ToKey = "To";

		// Token: 0x040029B6 RID: 10678
		public static readonly Keys NodeKeys = Keys.New("Name", "Value", "To");

		// Token: 0x040029B7 RID: 10679
		private static Keys exportKeys;

		// Token: 0x02000AEC RID: 2796
		private enum Exports
		{
			// Token: 0x040029B9 RID: 10681
			Graph_Nodes,
			// Token: 0x040029BA RID: 10682
			Count
		}

		// Token: 0x02000AED RID: 2797
		private static class Graph
		{
			// Token: 0x040029BB RID: 10683
			public static FunctionValue Nodes = new GraphModule.Graph.NodesFunctionValue();

			// Token: 0x02000AEE RID: 2798
			private class NodesFunctionValue : NativeFunctionValue1<ListValue, RecordValue>
			{
				// Token: 0x06004DC3 RID: 19907 RVA: 0x0010248D File Offset: 0x0010068D
				public NodesFunctionValue()
					: base(TypeValue.List, "graph", TypeValue.Record)
				{
				}

				// Token: 0x06004DC4 RID: 19908 RVA: 0x001024A4 File Offset: 0x001006A4
				public override ListValue TypedInvoke(RecordValue graph)
				{
					return ListValue.New(this.GetNodes(graph));
				}

				// Token: 0x06004DC5 RID: 19909 RVA: 0x001024B2 File Offset: 0x001006B2
				private IEnumerable<IValueReference> GetNodes(RecordValue graph)
				{
					HashSet<string> seen = new HashSet<string>();
					seen.Add(graph["Name"].AsText.String);
					Queue<RecordValue> nodesToVisit = new Queue<RecordValue>();
					nodesToVisit.Enqueue(graph);
					while (nodesToVisit.Count > 0)
					{
						RecordValue recordValue = nodesToVisit.Dequeue();
						foreach (IValueReference valueReference in recordValue["To"].AsList)
						{
							RecordValue asRecord = valueReference.Value.AsRecord;
							string @string = asRecord["Name"].AsText.String;
							if (seen.Add(@string))
							{
								nodesToVisit.Enqueue(asRecord);
							}
						}
						yield return recordValue["Value"];
					}
					yield break;
				}
			}
		}
	}
}
