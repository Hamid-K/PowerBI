using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000317 RID: 791
	public class PythonSnippet
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x000332D8 File Offset: 0x000314D8
		public PythonSnippet(PythonImports imports, string body, string returnVariable, string expectPrependReturnVariable = null)
		{
			if (imports == null)
			{
				throw new ArgumentNullException("imports");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			this.Imports = imports;
			this.Body = body;
			this.ReturnVariable = returnVariable;
			this.ExpectPrependReturnVariable = expectPrependReturnVariable;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00033324 File Offset: 0x00031524
		public PythonImports Imports { get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0003332C File Offset: 0x0003152C
		public string Body { get; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00033334 File Offset: 0x00031534
		public string ReturnVariable { get; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0003333C File Offset: 0x0003153C
		public string ExpectPrependReturnVariable { get; }

		// Token: 0x0600117D RID: 4477 RVA: 0x00033344 File Offset: 0x00031544
		public PythonSnippet Merge(PythonSnippet other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.ExpectPrependReturnVariable != this.ReturnVariable)
			{
				throw new Exception("Snippets to merge are incomaptible");
			}
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendIndented(this.Body);
			if (!string.IsNullOrWhiteSpace(this.Body) && !string.IsNullOrWhiteSpace(other.Body))
			{
				codeBuilder.AppendLine();
			}
			codeBuilder.AppendIndented(other.Body);
			return new PythonSnippet(this.Imports.Merge(other.Imports), codeBuilder.GetCode(), other.ReturnVariable, null);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000333E0 File Offset: 0x000315E0
		public string GetCode(bool separateFromImports = true, uint spacesAfterImports = 2U, uint spacesBeforeReturn = 1U)
		{
			CodeBuilder code = this.Imports.GetCode(separateFromImports, spacesAfterImports);
			code.AppendIndented(this.Body);
			if (!string.IsNullOrWhiteSpace(this.ReturnVariable))
			{
				while (spacesBeforeReturn-- > 0U)
				{
					code.AppendLine();
				}
				code.AppendLine(this.ReturnVariable);
			}
			return code.GetCode();
		}
	}
}
