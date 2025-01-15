using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0002AD8F File Offset: 0x00028F8F
		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002AD99 File Offset: 0x00028F99
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002ADA1 File Offset: 0x00028FA1
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002ADA4 File Offset: 0x00028FA4
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

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002ADBD File Offset: 0x00028FBD
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002ADC0 File Offset: 0x00028FC0
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

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002ADF5 File Offset: 0x00028FF5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002ADF8 File Offset: 0x00028FF8
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002AE04 File Offset: 0x00029004
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002AE07 File Offset: 0x00029007
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0002AE13 File Offset: 0x00029013
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
