using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012D RID: 301
	public class EdmPropertyValue : IEdmPropertyValue, IEdmDelayedValue
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x000122C9 File Offset: 0x000104C9
		public EdmPropertyValue(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000122E4 File Offset: 0x000104E4
		public EdmPropertyValue(string name, IEdmValue value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00012312 File Offset: 0x00010512
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001231A File Offset: 0x0001051A
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00012322 File Offset: 0x00010522
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

		// Token: 0x04000333 RID: 819
		private readonly string name;

		// Token: 0x04000334 RID: 820
		private IEdmValue value;
	}
}
