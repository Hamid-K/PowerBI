using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019FE RID: 6654
	public abstract class LibraryInstance
	{
		// Token: 0x17002AE5 RID: 10981
		// (get) Token: 0x0600A867 RID: 43111
		public abstract IRecordValue CurrentLibrary { get; }

		// Token: 0x0600A868 RID: 43112 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void Reset()
		{
		}

		// Token: 0x0600A869 RID: 43113
		public abstract void AddModule(string module);

		// Token: 0x0600A86A RID: 43114
		public abstract void RemoveModule(string module);

		// Token: 0x0600A86B RID: 43115
		public abstract bool HasModule(string module);

		// Token: 0x0600A86C RID: 43116
		public abstract HashSet<string> GetModules();

		// Token: 0x0600A86D RID: 43117 RVA: 0x0022C7C8 File Offset: 0x0022A9C8
		protected IRecordValue MakeLibrary(IEnumerable<string> modules)
		{
			IRecordValue library = MashupEngines.Version1.GetLibrary(MinimalEngineHost.Instance, modules);
			for (int i = 0; i < library.Keys.Length; i++)
			{
				bool isNull = library[i].IsNull;
			}
			return library;
		}
	}
}
