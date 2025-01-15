using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200006B RID: 107
	public interface IVariableService
	{
		// Token: 0x060001AE RID: 430
		bool TryGetValue(string name, out object value);

		// Token: 0x060001AF RID: 431
		void Add(string name, object value);

		// Token: 0x060001B0 RID: 432
		void Remove(string name);
	}
}
