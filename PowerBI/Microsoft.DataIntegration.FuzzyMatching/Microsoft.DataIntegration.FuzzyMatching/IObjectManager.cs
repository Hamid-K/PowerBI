using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D4 RID: 212
	public interface IObjectManager
	{
		// Token: 0x0600086A RID: 2154
		int CreateReference(object obj);

		// Token: 0x0600086B RID: 2155
		int CreateReference(string objectName, object obj);

		// Token: 0x0600086C RID: 2156
		int CreateReference(string objectName, object obj, int timeout);

		// Token: 0x0600086D RID: 2157
		TemporalHandle GetObjectHandle(int handle);

		// Token: 0x0600086E RID: 2158
		int GetHandle(string objectName);

		// Token: 0x0600086F RID: 2159
		int GetHandle(string objectName, int timeout);

		// Token: 0x06000870 RID: 2160
		object GetObject(int objectHandle);

		// Token: 0x06000871 RID: 2161
		object GetObject(string objectName);

		// Token: 0x06000872 RID: 2162
		object GetObject(string objectName, out int objectHandle);

		// Token: 0x06000873 RID: 2163
		string GetObjectName(int objectHandle);

		// Token: 0x06000874 RID: 2164
		void Release(int objectHandle);

		// Token: 0x06000875 RID: 2165
		void Collect();
	}
}
