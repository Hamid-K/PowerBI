using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000E4 RID: 228
	[Guid("E2203D7F-FE31-4253-AC05-108033E25E91")]
	internal interface IMajorObject : INamedComponent, IModelComponent, IComponent, IDisposable
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000E54 RID: 3668
		Server ParentServer { get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000E55 RID: 3669
		Database ParentDatabase { get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000E56 RID: 3670
		bool IsLoaded { get; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000E57 RID: 3671
		string Path { get; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000E58 RID: 3672
		Type BaseType { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000E59 RID: 3673
		IObjectReference ObjectReference { get; }

		// Token: 0x06000E5A RID: 3674
		void WriteRef(XmlWriter writer);

		// Token: 0x06000E5B RID: 3675
		void CreateBody();

		// Token: 0x06000E5C RID: 3676
		Hashtable GetDependents(Hashtable dependents);

		// Token: 0x06000E5D RID: 3677
		bool DependsOn(IMajorObject obj);

		// Token: 0x06000E5E RID: 3678
		void Refresh();

		// Token: 0x06000E5F RID: 3679
		void Refresh(bool full, RefreshType type);

		// Token: 0x06000E60 RID: 3680
		void Update();
	}
}
