using System;
using System.Collections;
using System.Xml;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000FC RID: 252
	public interface IMajorObject
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001054 RID: 4180
		Server ParentServer { get; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001055 RID: 4181
		Database ParentDatabase { get; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001056 RID: 4182
		bool IsLoaded { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001057 RID: 4183
		string Path { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001058 RID: 4184
		Type BaseType { get; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001059 RID: 4185
		ObjectReference ObjectReference { get; }

		// Token: 0x0600105A RID: 4186
		void WriteRef(XmlWriter writer);

		// Token: 0x0600105B RID: 4187
		void CreateBody();

		// Token: 0x0600105C RID: 4188
		Hashtable GetDependents(Hashtable dependents);

		// Token: 0x0600105D RID: 4189
		void Refresh(bool full, RefreshType type);

		// Token: 0x0600105E RID: 4190
		bool DependsOn(IMajorObject obj);

		// Token: 0x0600105F RID: 4191
		void Update();

		// Token: 0x06001060 RID: 4192
		void Refresh();
	}
}
