using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class DbFunctionAttribute : Attribute
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000A5E2 File Offset: 0x000087E2
		public DbFunctionAttribute(string namespaceName, string functionName)
		{
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotEmpty(functionName, "functionName");
			this._namespaceName = namespaceName;
			this._functionName = functionName;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A610 File Offset: 0x00008810
		public string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A618 File Offset: 0x00008818
		public string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x040000B6 RID: 182
		private readonly string _namespaceName;

		// Token: 0x040000B7 RID: 183
		private readonly string _functionName;
	}
}
