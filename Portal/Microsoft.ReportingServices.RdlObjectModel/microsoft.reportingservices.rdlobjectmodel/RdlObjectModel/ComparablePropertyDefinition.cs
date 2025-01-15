using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B6 RID: 438
	public class ComparablePropertyDefinition<T> : PropertyDefinition<T>, IPropertyDefinition where T : struct, IComparable
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00023874 File Offset: 0x00021A74
		public T? Minimum
		{
			get
			{
				return this.m_minimum;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0002387C File Offset: 0x00021A7C
		public T? Maximum
		{
			get
			{
				return this.m_maximum;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00023884 File Offset: 0x00021A84
		object IPropertyDefinition.Default
		{
			get
			{
				return base.Default;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00023891 File Offset: 0x00021A91
		object IPropertyDefinition.Minimum
		{
			get
			{
				return this.Minimum;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0002389E File Offset: 0x00021A9E
		object IPropertyDefinition.Maximum
		{
			get
			{
				return this.Maximum;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000238AC File Offset: 0x00021AAC
		void IPropertyDefinition.Validate(object component, object value)
		{
			if (value is T)
			{
				this.Validate(component, (T)((object)value));
				return;
			}
			if (value is ReportExpression<T>)
			{
				this.Validate(component, (ReportExpression<T>)value);
				return;
			}
			if (value is string)
			{
				this.Validate(component, (string)value);
				return;
			}
			throw new ArgumentException("Invalid type.");
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00023905 File Offset: 0x00021B05
		public ComparablePropertyDefinition(string name, T? defaultValue)
			: base(name, defaultValue)
		{
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002390F File Offset: 0x00021B0F
		public ComparablePropertyDefinition(string name, T? defaultValue, T? minimum, T? maximum)
			: this(name, defaultValue)
		{
			this.m_minimum = minimum;
			this.m_maximum = maximum;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00023928 File Offset: 0x00021B28
		public void Constrain(ref T value)
		{
			if (this.Minimum != null)
			{
				T t = this.Minimum.Value;
				if (t.CompareTo(value) > 0)
				{
					value = this.Minimum.Value;
					return;
				}
			}
			if (this.Maximum != null)
			{
				T t = this.Maximum.Value;
				if (t.CompareTo(value) < 0)
				{
					value = this.Maximum.Value;
				}
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000239D4 File Offset: 0x00021BD4
		public void Validate(object component, T value)
		{
			if (this.Minimum != null)
			{
				T t = this.Minimum.Value;
				if (t.CompareTo(value) > 0)
				{
					throw new ArgumentTooSmallException(component, base.Name, value, this.Minimum);
				}
			}
			if (this.Maximum != null)
			{
				T t = this.Maximum.Value;
				if (t.CompareTo(value) < 0)
				{
					throw new ArgumentTooLargeException(component, base.Name, value, this.Maximum);
				}
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00023A87 File Offset: 0x00021C87
		public void Validate(object component, ReportExpression<T> value)
		{
			if (value.IsExpression)
			{
				return;
			}
			this.Validate(component, value.Value);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00023AA1 File Offset: 0x00021CA1
		public void Validate(object component, string value)
		{
			this.Validate(component, new ReportExpression<T>(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0400053E RID: 1342
		private readonly T? m_minimum;

		// Token: 0x0400053F RID: 1343
		private readonly T? m_maximum;
	}
}
