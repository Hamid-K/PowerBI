using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Http.Internal;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000106 RID: 262
	public class ReflectedHttpParameterDescriptor : HttpParameterDescriptor
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x00011251 File Offset: 0x0000F451
		public ReflectedHttpParameterDescriptor(HttpActionDescriptor actionDescriptor, ParameterInfo parameterInfo)
			: base(actionDescriptor)
		{
			if (parameterInfo == null)
			{
				throw Error.ArgumentNull("parameterInfo");
			}
			this.ParameterInfo = parameterInfo;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001126F File Offset: 0x0000F46F
		public ReflectedHttpParameterDescriptor()
		{
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00011278 File Offset: 0x0000F478
		public override object DefaultValue
		{
			get
			{
				object obj;
				if (this.ParameterInfo.TryGetDefaultValue(out obj))
				{
					return obj;
				}
				return base.DefaultValue;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001129C File Offset: 0x0000F49C
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public ParameterInfo ParameterInfo
		{
			get
			{
				return this._parameterInfo;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._parameterInfo = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000112B6 File Offset: 0x0000F4B6
		public override bool IsOptional
		{
			get
			{
				return this.ParameterInfo.IsOptional;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000112C3 File Offset: 0x0000F4C3
		public override string ParameterName
		{
			get
			{
				return this.ParameterInfo.Name;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public override Type ParameterType
		{
			get
			{
				return this.ParameterInfo.ParameterType;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000112DD File Offset: 0x0000F4DD
		public override Collection<TAttribute> GetCustomAttributes<TAttribute>()
		{
			return new Collection<TAttribute>((TAttribute[])this.ParameterInfo.GetCustomAttributes(typeof(TAttribute), false));
		}

		// Token: 0x040001C1 RID: 449
		private ParameterInfo _parameterInfo;
	}
}
