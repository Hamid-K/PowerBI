using System;
using System.Collections.Generic;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x0200005A RID: 90
	// (Invoke) Token: 0x060001E0 RID: 480
	internal delegate IntPtr TargetCreator(object target, List<KeyValuePair<string, object>> targetParameters, IntPtr sess);
}
