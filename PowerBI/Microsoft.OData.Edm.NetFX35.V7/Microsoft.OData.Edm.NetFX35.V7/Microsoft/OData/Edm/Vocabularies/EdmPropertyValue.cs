using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000120 RID: 288
	public class EdmPropertyValue : IEdmPropertyValue, IEdmDelayedValue
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x00013DE9 File Offset: 0x00011FE9
		public EdmPropertyValue(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00013E04 File Offset: 0x00012004
		public EdmPropertyValue(string name, IEdmValue value)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00013E32 File Offset: 0x00012032
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00013E3A File Offset: 0x0001203A
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00013E42 File Offset: 0x00012042
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

		// Token: 0x0400042E RID: 1070
		private readonly string name;

		// Token: 0x0400042F RID: 1071
		private IEdmValue value;
	}
}
