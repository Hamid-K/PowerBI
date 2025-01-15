using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000CC RID: 204
	public class ModularizationFrameworkContext : UtilsContext
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00014B3E File Offset: 0x00012D3E
		public new static ModularizationFrameworkContext Current
		{
			get
			{
				return ModularizationFrameworkContext.sm_instance;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00014B45 File Offset: 0x00012D45
		protected ModularizationFrameworkContext()
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00014B4D File Offset: 0x00012D4D
		public bool IsSelfTest
		{
			get
			{
				return base.GetContextMember<bool>(4);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00014B56 File Offset: 0x00012D56
		public IDisposable PushApplicationRoot(ApplicationRoot ar)
		{
			return base.PushContextMember<ApplicationRoot>(3, ar);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00014B60 File Offset: 0x00012D60
		public IDisposable PushTestContext(bool value)
		{
			return base.PushContextMember<bool>(4, value);
		}

		// Token: 0x04000201 RID: 513
		private static ModularizationFrameworkContext sm_instance = new ModularizationFrameworkContext();
	}
}
