using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ValueProviderAttribute : ModelBinderAttribute
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00005759 File Offset: 0x00003959
		public ValueProviderAttribute(Type valueProviderFactory)
			: this(new Type[] { valueProviderFactory })
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000576B File Offset: 0x0000396B
		public ValueProviderAttribute(params Type[] valueProviderFactories)
		{
			this._valueProviderFactoryTypes = valueProviderFactories;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000577A File Offset: 0x0000397A
		public IEnumerable<Type> ValueProviderFactoryTypes
		{
			get
			{
				return this._valueProviderFactoryTypes;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005782 File Offset: 0x00003982
		public override IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
		{
			return Array.ConvertAll<Type, ValueProviderFactory>(this._valueProviderFactoryTypes, new Converter<Type, ValueProviderFactory>(ValueProviderAttribute.Instantiate));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000579C File Offset: 0x0000399C
		private static ValueProviderFactory Instantiate(Type factoryType)
		{
			if (factoryType == null)
			{
				throw new ArgumentNullException("factoryType");
			}
			if (!typeof(ValueProviderFactory).IsAssignableFrom(factoryType))
			{
				throw Error.InvalidOperation(SRResources.ValueProviderFactory_Cannot_Create, new object[]
				{
					typeof(ValueProviderFactory),
					factoryType
				});
			}
			return (ValueProviderFactory)Activator.CreateInstance(factoryType);
		}

		// Token: 0x04000053 RID: 83
		private readonly Type[] _valueProviderFactoryTypes;
	}
}
