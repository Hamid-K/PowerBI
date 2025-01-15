using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001892 RID: 6290
	internal class ImportBlockBuilder
	{
		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x0600CDC9 RID: 52681 RVA: 0x002BF3C1 File Offset: 0x002BD5C1
		public IEnumerable<PythonImport> ImportStatements
		{
			get
			{
				return this._imports.Values;
			}
		}

		// Token: 0x0600CDCA RID: 52682 RVA: 0x002BF3CE File Offset: 0x002BD5CE
		public void Add(string module)
		{
			this.Add(module, null, null);
		}

		// Token: 0x0600CDCB RID: 52683 RVA: 0x002BF3D9 File Offset: 0x002BD5D9
		public void Add(string module, IEnumerable<string> objects)
		{
			this.Add(module, objects.ConvertToHashSet<string>(), null);
		}

		// Token: 0x0600CDCC RID: 52684 RVA: 0x002BF3E9 File Offset: 0x002BD5E9
		public void Add(string module, string alias)
		{
			this.Add(module, null, alias);
		}

		// Token: 0x0600CDCD RID: 52685 RVA: 0x002BF3F4 File Offset: 0x002BD5F4
		private void Add(string module, HashSet<string> objects, string alias)
		{
			PythonImport pythonImport;
			if (this._imports.TryGetValue(module, out pythonImport))
			{
				if (objects != null && objects.Any<string>())
				{
					this._imports[module] = pythonImport.WithNewObjects(objects);
					return;
				}
			}
			else
			{
				this._imports.Add(module, new PythonImport(module, objects, alias));
			}
		}

		// Token: 0x04005067 RID: 20583
		private readonly Dictionary<string, PythonImport> _imports = new Dictionary<string, PythonImport>();
	}
}
