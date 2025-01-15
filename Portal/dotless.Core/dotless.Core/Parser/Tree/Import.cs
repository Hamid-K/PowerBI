using System;
using dotless.Core.Exceptions;
using dotless.Core.Importers;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000039 RID: 57
	public class Import : Directive
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A8EF File Offset: 0x00008AEF
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000A8F7 File Offset: 0x00008AF7
		protected Node OriginalPath { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A900 File Offset: 0x00008B00
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000A908 File Offset: 0x00008B08
		public string Path { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A911 File Offset: 0x00008B11
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000A919 File Offset: 0x00008B19
		public Ruleset InnerRoot { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A922 File Offset: 0x00008B22
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000A92A File Offset: 0x00008B2A
		public string InnerContent { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A933 File Offset: 0x00008B33
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000A93B File Offset: 0x00008B3B
		public Node Features { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A944 File Offset: 0x00008B44
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000A94C File Offset: 0x00008B4C
		public ImportOptions ImportOptions { get; set; }

		// Token: 0x06000230 RID: 560 RVA: 0x0000A955 File Offset: 0x00008B55
		public Import(Quoted path, Value features, ImportOptions option)
			: this(path, features, option)
		{
			this.OriginalPath = path;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A967 File Offset: 0x00008B67
		public Import(Url path, Value features, ImportOptions option)
			: this(path, features, option)
		{
			this.OriginalPath = path;
			this.Path = path.GetUnadjustedUrl();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A985 File Offset: 0x00008B85
		private Import(Node originalPath, Node features)
		{
			this.referenceVisitor = new ReferenceVisitor(true);
			base..ctor();
			this.OriginalPath = originalPath;
			this.Features = features;
			this._importAction = new ImportAction?(ImportAction.LeaveImport);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A9B3 File Offset: 0x00008BB3
		private Import(Node path, Value features, ImportOptions option)
		{
			this.referenceVisitor = new ReferenceVisitor(true);
			base..ctor();
			if (path == null)
			{
				throw new ParserException("Imports do not allow expressions");
			}
			this.OriginalPath = path;
			this.Features = features;
			this.ImportOptions = option;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A9EA File Offset: 0x00008BEA
		private ImportAction GetImportAction(IImporter importer)
		{
			if (this._importAction == null)
			{
				this._importAction = new ImportAction?(importer.Import(this));
			}
			return this._importAction.Value;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AA18 File Offset: 0x00008C18
		public override void AppendCSS(Env env, Context context)
		{
			ImportAction importAction = this.GetImportAction(env.Parser.Importer);
			if (importAction == ImportAction.ImportNothing)
			{
				return;
			}
			if (importAction == ImportAction.ImportCss)
			{
				env.Output.Append(this.InnerContent);
				return;
			}
			env.Output.Append("@import ").Append(this.OriginalPath.ToCSS(env));
			if (this.Features)
			{
				env.Output.Append(" ").Append(this.Features);
			}
			env.Output.Append(";");
			if (!env.Compress)
			{
				env.Output.Append("\n");
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public override void Accept(IVisitor visitor)
		{
			this.Features = base.VisitAndReplace<Node>(this.Features, visitor, true);
			if (this._importAction == ImportAction.ImportLess)
			{
				this.InnerRoot = base.VisitAndReplace<Ruleset>(this.InnerRoot, visitor);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AB1C File Offset: 0x00008D1C
		public override Node Evaluate(Env env)
		{
			this.OriginalPath = this.OriginalPath.Evaluate(env);
			Quoted quoted = this.OriginalPath as Quoted;
			if (quoted != null)
			{
				this.Path = quoted.Value;
			}
			ImportAction importAction = this.GetImportAction(env.Parser.Importer);
			if (importAction == ImportAction.ImportNothing)
			{
				return new NodeList().ReducedFrom<NodeList>(new Node[] { this });
			}
			Node node = null;
			if (this.Features)
			{
				node = this.Features.Evaluate(env);
			}
			if (importAction == ImportAction.LeaveImport)
			{
				return new Import(this.OriginalPath, node);
			}
			if (importAction == ImportAction.ImportCss)
			{
				Import import = new Import(this.OriginalPath, null)
				{
					_importAction = new ImportAction?(ImportAction.ImportCss),
					InnerContent = this.InnerContent
				};
				if (node)
				{
					return new Media(node, new NodeList { import });
				}
				return import;
			}
			else
			{
				using (env.Parser.Importer.BeginScope(this))
				{
					if (base.IsReference || this.IsOptionSet(this.ImportOptions, ImportOptions.Reference))
					{
						base.IsReference = true;
						this.Accept(this.referenceVisitor);
					}
					NodeHelper.RecursiveExpandNodes<Import>(env, this.InnerRoot);
				}
				NodeList nodeList = new NodeList(this.InnerRoot.Rules).ReducedFrom<NodeList>(new Node[] { this });
				if (node)
				{
					return new Media(node, nodeList);
				}
				return nodeList;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AC90 File Offset: 0x00008E90
		private bool IsOptionSet(ImportOptions options, ImportOptions test)
		{
			return (options & test) == test;
		}

		// Token: 0x04000075 RID: 117
		private readonly ReferenceVisitor referenceVisitor;

		// Token: 0x0400007C RID: 124
		private ImportAction? _importAction;
	}
}
