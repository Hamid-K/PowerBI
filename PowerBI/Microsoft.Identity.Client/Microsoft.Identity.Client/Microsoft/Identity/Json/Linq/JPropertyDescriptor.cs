using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000C0 RID: 192
	internal class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0002A5C7 File Offset: 0x000287C7
		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002A5D1 File Offset: 0x000287D1
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002A5D9 File Offset: 0x000287D9
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002A5DC File Offset: 0x000287DC
		[return: Nullable(2)]
		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002A5F5 File Offset: 0x000287F5
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002A5F8 File Offset: 0x000287F8
		public override void SetValue(object component, object value)
		{
			JObject jobject = component as JObject;
			if (jobject != null)
			{
				JToken jtoken = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = jtoken;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002A62D File Offset: 0x0002882D
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002A630 File Offset: 0x00028830
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002A63C File Offset: 0x0002883C
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0002A63F File Offset: 0x0002883F
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002A64B File Offset: 0x0002884B
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
