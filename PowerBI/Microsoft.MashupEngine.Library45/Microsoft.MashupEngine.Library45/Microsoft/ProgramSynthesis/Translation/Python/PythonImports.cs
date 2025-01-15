using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000310 RID: 784
	public class PythonImports
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x000321A4 File Offset: 0x000303A4
		public PythonImports()
		{
			this._imports = new HashSet<string>();
			this._fromImports = MultiValueDictionary<string, string>.Create<HashSet<string>>();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000321C2 File Offset: 0x000303C2
		public PythonImports(IEnumerable<string> imports, IEnumerable<KeyValuePair<string, IReadOnlyCollection<string>>> fromImports)
		{
			this._imports = imports.ConvertToHashSet<string>();
			this._fromImports = MultiValueDictionary<string, string>.Create<HashSet<string>>(fromImports);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000321E2 File Offset: 0x000303E2
		public void AddImport(string name)
		{
			this._imports.Add(name);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000321F1 File Offset: 0x000303F1
		public void AddImports(IEnumerable<string> names)
		{
			this._imports.AddRange(names);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000321FF File Offset: 0x000303FF
		public void AddImports(params string[] names)
		{
			this.AddImports(names);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00032208 File Offset: 0x00030408
		public void Clear()
		{
			this._imports.Clear();
			this._fromImports.Clear();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00032220 File Offset: 0x00030420
		public void AddFromImport(string fromName, string name)
		{
			this._fromImports.Add(fromName, name);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003222F File Offset: 0x0003042F
		public void AddFromImports(string fromName, params string[] names)
		{
			this.AddFromImports(fromName, names);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003223C File Offset: 0x0003043C
		public void AddFromImports(string fromName, IEnumerable<string> names)
		{
			foreach (string text in names)
			{
				this.AddFromImport(fromName, text);
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00032288 File Offset: 0x00030488
		public CodeBuilder GetCode(bool separateFromImports = false, uint ensureTrailingLines = 0U)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			bool flag = false;
			int num = 0;
			foreach (string text in this._imports.OrderBy((string x) => x))
			{
				codeBuilder.AppendLine("import " + text);
				flag = true;
			}
			if (separateFromImports && flag)
			{
				codeBuilder.AppendLine();
				num++;
			}
			foreach (KeyValuePair<string, IReadOnlyCollection<string>> keyValuePair in this._fromImports.OrderBy((KeyValuePair<string, IReadOnlyCollection<string>> kv) => kv.Key))
			{
				string text2;
				IReadOnlyCollection<string> readOnlyCollection;
				keyValuePair.Deconstruct(out text2, out readOnlyCollection);
				string text3 = text2;
				IReadOnlyCollection<string> readOnlyCollection2 = readOnlyCollection;
				codeBuilder.AppendLine("from " + text3 + " import " + string.Join(", ", readOnlyCollection2.OrderBy((string name) => name)));
				flag = true;
				num = 0;
			}
			if (flag)
			{
				while ((long)num < (long)((ulong)ensureTrailingLines))
				{
					codeBuilder.AppendLine();
					num++;
				}
			}
			return codeBuilder;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000323F0 File Offset: 0x000305F0
		public PythonImports Merge(PythonImports other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return new PythonImports(this._imports.Union(other._imports), this._fromImports.Union(other._fromImports));
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00032428 File Offset: 0x00030628
		public void MergeWith(PythonImports other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.AddImports(other._imports);
			foreach (KeyValuePair<string, IReadOnlyCollection<string>> keyValuePair in other._fromImports)
			{
				this.AddFromImports(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0400084D RID: 2125
		private readonly MultiValueDictionary<string, string> _fromImports;

		// Token: 0x0400084E RID: 2126
		private readonly HashSet<string> _imports;
	}
}
