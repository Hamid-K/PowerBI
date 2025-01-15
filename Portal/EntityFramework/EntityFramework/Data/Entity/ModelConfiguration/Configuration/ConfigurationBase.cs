using System;
using System.ComponentModel;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C0 RID: 448
	internal abstract class ConfigurationBase
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x00040A03 File Offset: 0x0003EC03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00040A0B File Offset: 0x0003EC0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00040A14 File Offset: 0x0003EC14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00040A1C File Offset: 0x0003EC1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
