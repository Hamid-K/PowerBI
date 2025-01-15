using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000138 RID: 312
	public abstract class ParameterConfiguration
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		protected ParameterConfiguration(string name, IEdmTypeConfiguration parameterType)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (parameterType == null)
			{
				throw Error.ArgumentNull("parameterType");
			}
			this.Name = name;
			this.TypeConfiguration = parameterType;
			Type type;
			this.Nullable = (TypeHelper.IsCollection(parameterType.ClrType, out type) ? EdmLibHelpers.IsNullable(type) : EdmLibHelpers.IsNullable(parameterType.ClrType));
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002C049 File Offset: 0x0002A249
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x0002C051 File Offset: 0x0002A251
		public string Name { get; protected set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002C05A File Offset: 0x0002A25A
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0002C062 File Offset: 0x0002A262
		public IEdmTypeConfiguration TypeConfiguration { get; protected set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002C06B File Offset: 0x0002A26B
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0002C073 File Offset: 0x0002A273
		public bool Nullable { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002C07C File Offset: 0x0002A27C
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0002C084 File Offset: 0x0002A284
		public bool IsOptional { get; protected set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002C08D File Offset: 0x0002A28D
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0002C095 File Offset: 0x0002A295
		public string DefaultValue { get; protected set; }

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002C09E File Offset: 0x0002A29E
		public ParameterConfiguration Optional()
		{
			this.IsOptional = true;
			return this;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		public ParameterConfiguration Required()
		{
			this.IsOptional = false;
			return this;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002C0B2 File Offset: 0x0002A2B2
		public ParameterConfiguration HasDefaultValue(string defaultValue)
		{
			this.IsOptional = true;
			this.DefaultValue = defaultValue;
			return this;
		}
	}
}
