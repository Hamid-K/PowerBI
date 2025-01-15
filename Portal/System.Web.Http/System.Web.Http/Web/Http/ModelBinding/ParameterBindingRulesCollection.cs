using System;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000057 RID: 87
	public class ParameterBindingRulesCollection : Collection<Func<HttpParameterDescriptor, HttpParameterBinding>>
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00007376 File Offset: 0x00005576
		private static Func<HttpParameterDescriptor, HttpParameterBinding> TypeCheck(Type type, Func<HttpParameterDescriptor, HttpParameterBinding> func)
		{
			return delegate(HttpParameterDescriptor param)
			{
				if (!(param.ParameterType == type))
				{
					return null;
				}
				return func(param);
			};
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007396 File Offset: 0x00005596
		public void Add(Type typeMatch, Func<HttpParameterDescriptor, HttpParameterBinding> funcInner)
		{
			base.Add(ParameterBindingRulesCollection.TypeCheck(typeMatch, funcInner));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000073A5 File Offset: 0x000055A5
		public void Insert(int index, Type typeMatch, Func<HttpParameterDescriptor, HttpParameterBinding> funcInner)
		{
			base.Insert(index, ParameterBindingRulesCollection.TypeCheck(typeMatch, funcInner));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000073B8 File Offset: 0x000055B8
		public HttpParameterBinding LookupBinding(HttpParameterDescriptor parameter)
		{
			foreach (Func<HttpParameterDescriptor, HttpParameterBinding> func in this)
			{
				HttpParameterBinding httpParameterBinding = func(parameter);
				if (httpParameterBinding != null)
				{
					return httpParameterBinding;
				}
			}
			return null;
		}
	}
}
