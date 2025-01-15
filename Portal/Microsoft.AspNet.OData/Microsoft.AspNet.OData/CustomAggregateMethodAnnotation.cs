using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000018 RID: 24
	public class CustomAggregateMethodAnnotation
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000414C File Offset: 0x0000234C
		public CustomAggregateMethodAnnotation AddMethod(string methodToken, IDictionary<Type, MethodInfo> methods)
		{
			this._tokenToMethodMap.Add(methodToken, methods);
			return this;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000415C File Offset: 0x0000235C
		public bool GetMethodInfo(string methodToken, Type returnType, out MethodInfo methodInfo)
		{
			methodInfo = null;
			IDictionary<Type, MethodInfo> dictionary;
			return this._tokenToMethodMap.TryGetValue(methodToken, out dictionary) && dictionary.TryGetValue(returnType, out methodInfo);
		}

		// Token: 0x0400001E RID: 30
		private readonly Dictionary<string, IDictionary<Type, MethodInfo>> _tokenToMethodMap = new Dictionary<string, IDictionary<Type, MethodInfo>>();
	}
}
