using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200057C RID: 1404
	internal sealed class VBExpressionCodeProvider : VBCodeProvider
	{
		// Token: 0x060050EB RID: 20715 RVA: 0x00153F37 File Offset: 0x00152137
		public override CompilerResults CompileAssemblyFromDom(CompilerParameters options, params CodeCompileUnit[] compilationUnits)
		{
			if (compilationUnits.Length == 1)
			{
				return this.CompileAssemblyFromDomWithRetry(options, compilationUnits[0]);
			}
			return base.CompileAssemblyFromDom(options, compilationUnits);
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x00153F54 File Offset: 0x00152154
		private CompilerResults CompileAssemblyFromDomWithRetry(CompilerParameters options, CodeCompileUnit compilationUnit)
		{
			options.TempFiles.KeepFiles = true;
			CompilerResults compilerResults = base.CompileAssemblyFromDom(options, new CodeCompileUnit[] { compilationUnit });
			TempFileCollection tempFiles = compilerResults.TempFiles;
			try
			{
				if (compilerResults.Errors.HasErrors)
				{
					options.TempFiles = new TempFileCollection();
					compilerResults = this.RetryCompile(options, compilerResults.Errors) ?? compilerResults;
				}
			}
			finally
			{
				foreach (object obj in tempFiles)
				{
					string text = obj as string;
					if (text != null)
					{
						try
						{
							File.Delete(text);
						}
						catch
						{
						}
					}
				}
			}
			return compilerResults;
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00154020 File Offset: 0x00152220
		private CompilerResults RetryCompile(CompilerParameters options, CompilerErrorCollection compilerErrorCollection)
		{
			CompilerResults compilerResults = null;
			string text = null;
			List<int> list = new List<int>();
			foreach (object obj in compilerErrorCollection)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (string.Equals(compilerError.ErrorNumber, "BC30494", StringComparison.OrdinalIgnoreCase))
				{
					list.Add(compilerError.Line);
					if (text == null)
					{
						text = compilerError.FileName;
					}
				}
			}
			if (text != null)
			{
				List<string> list2 = File.ReadAllLines(text).ToList<string>();
				VBExpressionCodeProvider.SplitLongLines(list2, list);
				File.WriteAllLines(text, list2.ToArray());
				options.TempFiles = new TempFileCollection();
				compilerResults = base.CompileAssemblyFromFile(options, new string[] { text });
			}
			return compilerResults;
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x001540EC File Offset: 0x001522EC
		private static void SplitLongLines(List<string> lines, List<int> longLineNumbers)
		{
			if (longLineNumbers.Count > 0)
			{
				longLineNumbers.Sort();
			}
			for (int i = longLineNumbers.Count - 1; i >= 0; i--)
			{
				int num = longLineNumbers[i] - 1;
				string text = lines[num];
				lines.RemoveAt(num);
				while (text.Length > 65535)
				{
					int num2 = VBExpressionCodeProvider.FindSafeSplitPosition(text);
					if (num2 <= 0)
					{
						lines.Insert(num, text);
						return;
					}
					string text2 = text.Substring(0, num2) + " _";
					lines.Insert(num, text2);
					num++;
					text = text.Substring(num2 + 1);
				}
				lines.Insert(num, text);
			}
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0015418C File Offset: 0x0015238C
		private static int FindSafeSplitPosition(string line)
		{
			int num = line.LastIndexOf(' ', 65535 - " _".Length);
			bool flag = true;
			while (flag && num > 80)
			{
				int num2 = line.LastIndexOf('"', num, 80);
				flag = num2 != -1;
				if (flag)
				{
					num = line.LastIndexOf(' ', num2);
				}
			}
			if (flag)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x040028D5 RID: 10453
		private const string LineContinuation = " _";

		// Token: 0x040028D6 RID: 10454
		private const string LineTooLongError = "BC30494";

		// Token: 0x040028D7 RID: 10455
		private const int MaxLineLength = 65535;
	}
}
