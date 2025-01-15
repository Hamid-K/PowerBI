using System;
using System.Collections.Generic;
using dotless.Core.Importers;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200004B RID: 75
	public class Url : Node
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000E078 File Offset: 0x0000C278
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000E080 File Offset: 0x0000C280
		public Node Value { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000E089 File Offset: 0x0000C289
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000E091 File Offset: 0x0000C291
		public List<string> ImportPaths { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000E09A File Offset: 0x0000C29A
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000E0A2 File Offset: 0x0000C2A2
		public IImporter Importer { get; set; }

		// Token: 0x0600030B RID: 779 RVA: 0x0000E0AB File Offset: 0x0000C2AB
		public Url(Node value, IImporter importer)
		{
			this.Importer = importer;
			this.ImportPaths = importer.GetCurrentPathsClone();
			this.Value = value;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E0CD File Offset: 0x0000C2CD
		public Url(Node value)
		{
			this.Value = value;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		public string GetUnadjustedUrl()
		{
			TextNode textNode = this.Value as TextNode;
			if (textNode != null)
			{
				return textNode.Value;
			}
			return null;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E100 File Offset: 0x0000C300
		private Node AdjustUrlPath(Node value)
		{
			TextNode textNode = value as TextNode;
			if (textNode != null)
			{
				return this.AdjustUrlPath(textNode);
			}
			return value;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E120 File Offset: 0x0000C320
		private TextNode AdjustUrlPath(TextNode textValue)
		{
			if (this.Importer != null)
			{
				textValue.Value = this.Importer.AlterUrl(textValue.Value, this.ImportPaths);
			}
			return textValue;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E148 File Offset: 0x0000C348
		public override Node Evaluate(Env env)
		{
			return new Url(this.AdjustUrlPath(this.Value.Evaluate(env)), this.Importer);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E167 File Offset: 0x0000C367
		protected override Node CloneCore()
		{
			return new Url(this.Value.Clone(), this.Importer);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E17F File Offset: 0x0000C37F
		public override void AppendCSS(Env env)
		{
			env.Output.Append("url(").Append(this.Value).Append(")");
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E1A7 File Offset: 0x0000C3A7
		public override void Accept(IVisitor visitor)
		{
			this.Value = base.VisitAndReplace<Node>(this.Value, visitor);
		}
	}
}
