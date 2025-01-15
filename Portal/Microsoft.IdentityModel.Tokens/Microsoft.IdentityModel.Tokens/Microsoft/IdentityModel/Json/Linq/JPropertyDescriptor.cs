using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C1 RID: 193
	[NullableContext(1)]
	[Nullable(0)]
	internal class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000A97 RID: 2711 RVA: 0x0002ACB7 File Offset: 0x00028EB7
		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002ACC1 File Offset: 0x00028EC1
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002ACC9 File Offset: 0x00028EC9
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002ACCC File Offset: 0x00028ECC
		[NullableContext(2)]
		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002ACE5 File Offset: 0x00028EE5
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		[NullableContext(2)]
		public override void SetValue(object component, object value)
		{
			JObject jobject = component as JObject;
			if (jobject != null)
			{
				JToken jtoken = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = jtoken;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002AD1D File Offset: 0x00028F1D
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0002AD20 File Offset: 0x00028F20
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002AD2C File Offset: 0x00028F2C
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0002AD2F File Offset: 0x00028F2F
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002AD3B File Offset: 0x00028F3B
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
