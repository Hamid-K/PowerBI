using System;
using System.Collections;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000025 RID: 37
	internal class CustomAssembliesPermissions
	{
		// Token: 0x1700003D RID: 61
		internal CustomAssemblyPermissions this[string assemblyName]
		{
			get
			{
				return (CustomAssemblyPermissions)this.m_hTable[assemblyName];
			}
			set
			{
				this.m_hTable[assemblyName] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003389 File Offset: 0x00001589
		internal ICollection AssemblyNames
		{
			get
			{
				return this.m_hTable.Keys;
			}
		}

		// Token: 0x040000EC RID: 236
		private Hashtable m_hTable = new Hashtable();
	}
}
