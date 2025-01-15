using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Mashup.ScriptDom.ScriptGenerator;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000DB RID: 219
	internal abstract class SqlScriptGenerator
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0008ED15 File Offset: 0x0008CF15
		protected SqlScriptGenerator(SqlScriptGeneratorOptions options)
		{
			ScriptGeneratorSupporter.CheckForNullReference(options, "options");
			this._options = options;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0008ED2F File Offset: 0x0008CF2F
		public SqlScriptGeneratorOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0008ED38 File Offset: 0x0008CF38
		public void GenerateScript(TSqlFragment scriptFragment, out string script)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				this.GenerateScript(scriptFragment, stringWriter);
			}
			script = stringBuilder.ToString();
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0008ED84 File Offset: 0x0008CF84
		public void GenerateScript(TSqlFragment scriptFragment, TextWriter writer)
		{
			ScriptGeneratorSupporter.CheckForNullReference(scriptFragment, "scriptFragment");
			ScriptGeneratorSupporter.CheckForNullReference(writer, "writer");
			if (scriptFragment == null)
			{
				throw new ArgumentException(SqlScriptGeneratorResource.ScriptDomTreeTypeNotSupported, "scriptFragment");
			}
			ScriptWriter scriptWriter = this.WriteScript(scriptFragment);
			scriptWriter.WriteTo(writer);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0008EDCC File Offset: 0x0008CFCC
		public IList<TSqlParserToken> GenerateTokens(TSqlFragment scriptFragment)
		{
			ScriptGeneratorSupporter.CheckForNullReference(scriptFragment, "scriptFragment");
			ScriptWriter scriptWriter = this.WriteScript(scriptFragment);
			IList<TSqlParserToken> list = new List<TSqlParserToken>();
			scriptWriter.WriteTo(list);
			return list;
		}

		// Token: 0x0600141C RID: 5148
		internal abstract SqlScriptGeneratorVisitor CreateSqlScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter scriptWriter);

		// Token: 0x0600141D RID: 5149 RVA: 0x0008EDFC File Offset: 0x0008CFFC
		private ScriptWriter WriteScript(TSqlFragment scriptFragment)
		{
			ScriptWriter scriptWriter = new ScriptWriter(this._options);
			SqlScriptGeneratorVisitor sqlScriptGeneratorVisitor = this.CreateSqlScriptGeneratorVisitor(this._options, scriptWriter);
			scriptFragment.Accept(sqlScriptGeneratorVisitor);
			return scriptWriter;
		}

		// Token: 0x04000931 RID: 2353
		private SqlScriptGeneratorOptions _options;
	}
}
