using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson
{
	// Token: 0x02001A77 RID: 6775
	public class TableToJsonLoader : SimpleProgramLoader<TableToJsonProgram, Table, JToken>
	{
		// Token: 0x0600DF11 RID: 57105 RVA: 0x002F59D8 File Offset: 0x002F3BD8
		private TableToJsonLoader()
		{
		}

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x0600DF12 RID: 57106 RVA: 0x002F59E0 File Offset: 0x002F3BE0
		public static TableToJsonLoader Instance { get; } = new TableToJsonLoader();

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x0600DF13 RID: 57107 RVA: 0x002DF48F File Offset: 0x002DD68F
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x0600DF14 RID: 57108 RVA: 0x002F59AE File Offset: 0x002F3BAE
		public override TableToJsonProgram Create(ProgramNode program)
		{
			return new TableToJsonProgram(program);
		}
	}
}
