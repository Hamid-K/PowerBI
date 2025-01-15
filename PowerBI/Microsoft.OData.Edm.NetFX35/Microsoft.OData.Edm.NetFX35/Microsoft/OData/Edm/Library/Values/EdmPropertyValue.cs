using System;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000205 RID: 517
	public class EdmPropertyValue : IEdmPropertyValue, IEdmDelayedValue
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x000227D3 File Offset: 0x000209D3
		public EdmPropertyValue(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000227EE File Offset: 0x000209EE
		public EdmPropertyValue(string name, IEdmValue value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002281C File Offset: 0x00020A1C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00022824 File Offset: 0x00020A24
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0002282C File Offset: 0x00020A2C
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

		// Token: 0x0400058F RID: 1423
		private readonly string name;

		// Token: 0x04000590 RID: 1424
		private IEdmValue value;
	}
}
