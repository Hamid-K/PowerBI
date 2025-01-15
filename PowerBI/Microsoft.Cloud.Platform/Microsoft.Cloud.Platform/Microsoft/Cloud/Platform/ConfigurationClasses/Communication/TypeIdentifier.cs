using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000455 RID: 1109
	[Serializable]
	public sealed class TypeIdentifier : ConfigurationClass
	{
		// Token: 0x0600229B RID: 8859 RVA: 0x0000458E File Offset: 0x0000278E
		public TypeIdentifier()
		{
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007E3AC File Offset: 0x0007C5AC
		public TypeIdentifier(Type type)
		{
			this.TypeName = type.FullName;
			this.AssemblyName = type.Assembly.FullName;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0007E3D1 File Offset: 0x0007C5D1
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x0007E3D9 File Offset: 0x0007C5D9
		[ConfigurationProperty]
		public string TypeName { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0007E3E2 File Offset: 0x0007C5E2
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x0007E3EA File Offset: 0x0007C5EA
		[ConfigurationProperty]
		public string AssemblyName { get; set; }
	}
}
