using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000008 RID: 8
	public interface IGeoJsonWriter
	{
		// Token: 0x06000077 RID: 119
		void StartObjectScope();

		// Token: 0x06000078 RID: 120
		void EndObjectScope();

		// Token: 0x06000079 RID: 121
		void StartArrayScope();

		// Token: 0x0600007A RID: 122
		void EndArrayScope();

		// Token: 0x0600007B RID: 123
		void AddPropertyName(string name);

		// Token: 0x0600007C RID: 124
		void AddValue(double value);

		// Token: 0x0600007D RID: 125
		void AddValue(string value);
	}
}
