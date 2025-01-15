using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000F4 RID: 244
	[Parseable("ParseFromXML", ParseHumanReadableString = "ParseFromHumanReadable")]
	public class TreePath : IRenderableLiteral, IEquatable<TreePath>
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00012A29 File Offset: 0x00010C29
		public TreePath(params TreePathStep[] steps)
			: this(steps.AsEnumerable<TreePathStep>())
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00012A37 File Offset: 0x00010C37
		public TreePath(IEnumerable<TreePathStep> steps)
		{
			TreePathStep[] array = ((steps != null) ? steps.ToArray<TreePathStep>() : null);
			if (array == null)
			{
				throw new ArgumentNullException("steps");
			}
			this.Steps = array;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012A60 File Offset: 0x00010C60
		public TreePath(params string[] steps)
			: this(steps)
		{
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00012A69 File Offset: 0x00010C69
		public TreePath(IEnumerable<string> steps)
		{
			Func<string, TreePathStep> func;
			if ((func = TreePath.<>O.<0>__From) == null)
			{
				func = (TreePath.<>O.<0>__From = new Func<string, TreePathStep>(TreePathStep.From));
			}
			this..ctor(steps.Select(func));
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00012A92 File Offset: 0x00010C92
		public TreePathStep[] Steps { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00012A9C File Offset: 0x00010C9C
		public double Score
		{
			get
			{
				if (this._score == null)
				{
					this._score = new double?(this.Steps.Sum((TreePathStep s) => s.Score));
				}
				return this._score.Value;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00012AF6 File Offset: 0x00010CF6
		public bool Equals(TreePath other)
		{
			return other != null && (this == other || this.Steps.SequenceEqual(other.Steps));
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00012B14 File Offset: 0x00010D14
		public string RenderHumanReadable()
		{
			return this.Serialize();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00012B1C File Offset: 0x00010D1C
		public XElement RenderXML()
		{
			return new XElement("TreePath", this.Serialize());
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00012B34 File Offset: 0x00010D34
		public Node Find(Node node)
		{
			Node node2 = node;
			for (int i = 0; i < this.Steps.Length; i++)
			{
				node2 = this.Steps[i].Find(node2);
				if (node2 == null)
				{
					return null;
				}
			}
			return node2;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00012B6B File Offset: 0x00010D6B
		public static TreePath Join(params TreePath[] paths)
		{
			return new TreePath(paths.SelectMany((TreePath p) => p.Steps));
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00012B98 File Offset: 0x00010D98
		public static TreePath ParseFromXML(XElement pathElement)
		{
			TreePath treePath;
			try
			{
				treePath = ((pathElement.Name != "TreePath") ? null : TreePath.ParseFromHumanReadable(pathElement.Value));
			}
			catch
			{
				treePath = null;
			}
			return treePath;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public static TreePath ParseFromHumanReadable(string path)
		{
			if (string.IsNullOrEmpty(path) || path.Length < 2)
			{
				return null;
			}
			if (path.First<char>() != '"' && path[path.Length - 1] != '"')
			{
				return null;
			}
			string text = path.Substring(1, path.Length - 2);
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			string[] array = text.Split(new char[] { '/' });
			List<TreePathStep> list = new List<TreePathStep>(array.Length);
			Func<string, TreePathStep> func;
			if ((func = TreePath.<>O.<0>__From) == null)
			{
				func = (TreePath.<>O.<0>__From = new Func<string, TreePathStep>(TreePathStep.From));
			}
			foreach (TreePathStep treePathStep in array.Select(func))
			{
				if (treePathStep == null)
				{
					return null;
				}
				list.Add(treePathStep);
			}
			return new TreePath(list);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00012CC4 File Offset: 0x00010EC4
		public string Serialize()
		{
			string text = "\"{0}\"";
			object[] array = new object[1];
			array[0] = string.Join("/", this.Steps.Select((TreePathStep s) => s.Serialize()));
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00012B14 File Offset: 0x00010D14
		public override string ToString()
		{
			return this.Serialize();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00012D1D File Offset: 0x00010F1D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((TreePath)obj)));
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00012D4C File Offset: 0x00010F4C
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			this._hashCode = new int?(this.Steps.Aggregate(19, (int current, TreePathStep step) => current * 31 + step.GetHashCode()));
			return this._hashCode.Value;
		}

		// Token: 0x04000259 RID: 601
		private int? _hashCode;

		// Token: 0x0400025A RID: 602
		private double? _score;

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400025C RID: 604
			public static Func<string, TreePathStep> <0>__From;
		}
	}
}
