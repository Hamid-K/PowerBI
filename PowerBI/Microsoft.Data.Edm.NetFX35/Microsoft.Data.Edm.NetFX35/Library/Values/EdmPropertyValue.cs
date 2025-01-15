using System;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001D9 RID: 473
	public class EdmPropertyValue : IEdmPropertyValue, IEdmDelayedValue
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x00020D2B File Offset: 0x0001EF2B
		public EdmPropertyValue(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00020D46 File Offset: 0x0001EF46
		public EdmPropertyValue(string name, IEdmValue value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00020D74 File Offset: 0x0001EF74
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00020D7C File Offset: 0x0001EF7C
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00020D84 File Offset: 0x0001EF84
		public IEdmValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
				if (this.value != null)
				{
					throw new InvalidOperationException(Strings.ValueHasAlreadyBeenSet);
				}
				this.value = value;
			}
		}

		// Token: 0x04000548 RID: 1352
		private readonly string name;

		// Token: 0x04000549 RID: 1353
		private IEdmValue value;
	}
}
