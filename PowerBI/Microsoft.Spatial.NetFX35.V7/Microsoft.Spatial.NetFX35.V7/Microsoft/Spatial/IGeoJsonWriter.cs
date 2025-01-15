using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000008 RID: 8
	public interface IGeoJsonWriter
	{
		// Token: 0x06000065 RID: 101
		void StartObjectScope();

		// Token: 0x06000066 RID: 102
		void EndObjectScope();

		// Token: 0x06000067 RID: 103
		void StartArrayScope();

		// Token: 0x06000068 RID: 104
		void EndArrayScope();

		// Token: 0x06000069 RID: 105
		void AddPropertyName(string name);

		// Token: 0x0600006A RID: 106
		void AddValue(double value);

		// Token: 0x0600006B RID: 107
		void AddValue(string value);
	}
}
