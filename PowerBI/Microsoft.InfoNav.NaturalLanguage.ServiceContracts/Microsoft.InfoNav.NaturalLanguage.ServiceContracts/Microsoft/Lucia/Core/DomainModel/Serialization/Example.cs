using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B5 RID: 437
	public sealed class Example : SingletonMapping<ExampleProperties>, IScalarForm<string>
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public Example()
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000119D0 File Offset: 0x0000FBD0
		public Example(string value)
			: base(value)
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000119D9 File Offset: 0x0000FBD9
		public Example(string value, ExampleProperties properties)
			: base(value, properties)
		{
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000119E3 File Offset: 0x0000FBE3
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x000119EB File Offset: 0x0000FBEB
		public string Value
		{
			get
			{
				return base.UnderlyingKey;
			}
			set
			{
				base.UnderlyingKey = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000119F4 File Offset: 0x0000FBF4
		public ExampleProperties Properties
		{
			get
			{
				return base.UnderlyingValue;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000119FC File Offset: 0x0000FBFC
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			if (this.Properties.IsDefault())
			{
				value = this.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00011A19 File Offset: 0x0000FC19
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			this.Value = value;
		}
	}
}
