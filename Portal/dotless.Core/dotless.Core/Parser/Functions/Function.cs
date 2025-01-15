using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Loggers;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000079 RID: 121
	public abstract class Function
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00015E7C File Offset: 0x0001407C
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00015E84 File Offset: 0x00014084
		public string Name { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00015E8D File Offset: 0x0001408D
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00015E95 File Offset: 0x00014095
		protected List<Node> Arguments { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00015E9E File Offset: 0x0001409E
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00015EA6 File Offset: 0x000140A6
		public ILogger Logger { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00015EAF File Offset: 0x000140AF
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00015EB7 File Offset: 0x000140B7
		public NodeLocation Location { get; set; }

		// Token: 0x0600048A RID: 1162 RVA: 0x00015EC0 File Offset: 0x000140C0
		public Node Call(Env env, IEnumerable<Node> arguments)
		{
			this.Arguments = arguments.ToList<Node>();
			Node node = this.Evaluate(env);
			node.Location = this.Location;
			return node;
		}

		// Token: 0x0600048B RID: 1163
		protected abstract Node Evaluate(Env env);

		// Token: 0x0600048C RID: 1164 RVA: 0x00015EE1 File Offset: 0x000140E1
		public override string ToString()
		{
			return string.Format("function '{0}'", this.Name.ToLowerInvariant());
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00015EF8 File Offset: 0x000140F8
		protected void WarnNotSupportedByLessJS(string functionPattern)
		{
			this.WarnNotSupportedByLessJS(functionPattern, null, null);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00015F03 File Offset: 0x00014103
		protected void WarnNotSupportedByLessJS(string functionPattern, string replacementPattern)
		{
			this.WarnNotSupportedByLessJS(functionPattern, replacementPattern, null);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015F10 File Offset: 0x00014110
		protected void WarnNotSupportedByLessJS(string functionPattern, string replacementPattern, string extraInfo)
		{
			if (string.IsNullOrEmpty(replacementPattern))
			{
				this.Logger.Info("{0} is not supported by less.js, so this will work but not compile with other less implementations.{1}", new object[] { functionPattern, extraInfo });
				return;
			}
			this.Logger.Warn("{0} is not supported by less.js, so this will work but not compile with other less implementations. You may want to consider using {1} which does the same thing and is supported.{2}", new object[] { functionPattern, replacementPattern, extraInfo });
		}
	}
}
