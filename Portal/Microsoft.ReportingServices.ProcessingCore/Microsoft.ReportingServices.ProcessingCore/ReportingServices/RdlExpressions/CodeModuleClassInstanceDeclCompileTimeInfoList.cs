using System;
using System.Collections;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200055C RID: 1372
	internal sealed class CodeModuleClassInstanceDeclCompileTimeInfoList : Hashtable
	{
		// Token: 0x17001DEF RID: 7663
		internal CodeModuleClassInstanceDeclCompileTimeInfo this[object id]
		{
			get
			{
				CodeModuleClassInstanceDeclCompileTimeInfo codeModuleClassInstanceDeclCompileTimeInfo = (CodeModuleClassInstanceDeclCompileTimeInfo)base[id];
				if (codeModuleClassInstanceDeclCompileTimeInfo == null)
				{
					codeModuleClassInstanceDeclCompileTimeInfo = new CodeModuleClassInstanceDeclCompileTimeInfo();
					base.Add(id, codeModuleClassInstanceDeclCompileTimeInfo);
				}
				return codeModuleClassInstanceDeclCompileTimeInfo;
			}
		}
	}
}
