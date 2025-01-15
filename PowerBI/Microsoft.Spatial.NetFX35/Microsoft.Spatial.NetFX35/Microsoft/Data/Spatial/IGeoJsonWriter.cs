using System;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200000D RID: 13
	public interface IGeoJsonWriter
	{
		// Token: 0x06000098 RID: 152
		void StartObjectScope();

		// Token: 0x06000099 RID: 153
		void EndObjectScope();

		// Token: 0x0600009A RID: 154
		void StartArrayScope();

		// Token: 0x0600009B RID: 155
		void EndArrayScope();

		// Token: 0x0600009C RID: 156
		void AddPropertyName(string name);

		// Token: 0x0600009D RID: 157
		void AddValue(double value);

		// Token: 0x0600009E RID: 158
		void AddValue(string value);
	}
}
