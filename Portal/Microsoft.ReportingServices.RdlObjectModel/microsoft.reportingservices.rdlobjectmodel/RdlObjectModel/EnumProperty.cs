using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001BC RID: 444
	public class EnumProperty : PropertyDefinition, IPropertyDefinition
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00023B24 File Offset: 0x00021D24
		public object Default
		{
			get
			{
				return this.m_default;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00023B2C File Offset: 0x00021D2C
		public IList<object> ValidValues
		{
			get
			{
				if (this.m_validValues == null)
				{
					object[] array;
					if (this.m_validIntValues != null)
					{
						array = new object[this.m_validValues.Count];
						for (int i = 0; i < this.m_validIntValues.Count; i++)
						{
							array[i] = Enum.ToObject(this.m_type, this.m_validValues[i]);
						}
					}
					else
					{
						Array values = Enum.GetValues(this.m_type);
						array = new object[values.Length];
						values.CopyTo(array, 0);
					}
					this.m_validValues = new ReadOnlyCollection<object>(array);
				}
				return this.m_validValues;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00023BBC File Offset: 0x00021DBC
		object IPropertyDefinition.Minimum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00023BBF File Offset: 0x00021DBF
		object IPropertyDefinition.Maximum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00023BC4 File Offset: 0x00021DC4
		void IPropertyDefinition.Validate(object component, object value)
		{
			if (value is IExpression)
			{
				if (((IExpression)value).IsExpression)
				{
					return;
				}
				value = ((IExpression)value).Value;
			}
			if (value.GetType() == this.m_type)
			{
				this.Validate(component, (int)value);
				return;
			}
			throw new ArgumentException("Invalid type.");
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00023C20 File Offset: 0x00021E20
		public void Validate(object component, int value)
		{
			if (this.m_validIntValues != null && !this.m_validIntValues.Contains(value))
			{
				object obj = Enum.ToObject(this.m_type, value);
				throw new ArgumentConstraintException(component, base.Name, obj, null, SRErrorsWrapper.InvalidParam(base.Name, obj));
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00023C6B File Offset: 0x00021E6B
		public EnumProperty(string name, Type enumType, object defaultValue, IList<int> validValues)
			: base(name)
		{
			this.m_type = enumType;
			this.m_default = defaultValue;
			this.m_validIntValues = validValues;
		}

		// Token: 0x04000541 RID: 1345
		private readonly object m_default;

		// Token: 0x04000542 RID: 1346
		private readonly IList<int> m_validIntValues;

		// Token: 0x04000543 RID: 1347
		private IList<object> m_validValues;

		// Token: 0x04000544 RID: 1348
		private readonly Type m_type;
	}
}
