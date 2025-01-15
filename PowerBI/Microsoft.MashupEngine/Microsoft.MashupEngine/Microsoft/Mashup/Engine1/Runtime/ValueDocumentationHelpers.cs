using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200134E RID: 4942
	public static class ValueDocumentationHelpers
	{
		// Token: 0x06008207 RID: 33287 RVA: 0x001B9CC0 File Offset: 0x001B7EC0
		public static string GetName(this Value value)
		{
			return value.MetaValue["Documentation.Name"].AsString;
		}
	}
}
