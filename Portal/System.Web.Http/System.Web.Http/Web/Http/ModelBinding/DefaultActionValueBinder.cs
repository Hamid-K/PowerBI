using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000058 RID: 88
	public class DefaultActionValueBinder : IActionValueBinder
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00007414 File Offset: 0x00005614
		public virtual HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			HttpParameterBinding[] array = Array.ConvertAll<HttpParameterDescriptor, HttpParameterBinding>(actionDescriptor.GetParameters().ToArray<HttpParameterDescriptor>(), new Converter<HttpParameterDescriptor, HttpParameterBinding>(this.GetParameterBinding));
			HttpActionBinding httpActionBinding = new HttpActionBinding(actionDescriptor, array);
			DefaultActionValueBinder.EnsureOneBodyParameter(httpActionBinding);
			return httpActionBinding;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000745C File Offset: 0x0000565C
		private static void EnsureOneBodyParameter(HttpActionBinding actionBinding)
		{
			IList<HttpParameterDescriptor> parameters = actionBinding.ActionDescriptor.GetParameters();
			int num = -1;
			for (int i = 0; i < actionBinding.ParameterBindings.Length; i++)
			{
				if (actionBinding.ParameterBindings[i].WillReadBody)
				{
					if (num >= 0)
					{
						string parameterName = parameters[num].ParameterName;
						string parameterName2 = parameters[i].ParameterName;
						string text = Error.Format(SRResources.ParameterBindingCantHaveMultipleBodyParameters, new object[] { parameterName, parameterName2 });
						actionBinding.ParameterBindings[i] = new ErrorParameterBinding(parameters[i], text);
						actionBinding.ParameterBindings[num] = new ErrorParameterBinding(parameters[num], text);
					}
					else
					{
						num = i;
					}
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007508 File Offset: 0x00005708
		protected virtual HttpParameterBinding GetParameterBinding(HttpParameterDescriptor parameter)
		{
			ParameterBindingAttribute parameterBindingAttribute = parameter.ParameterBinderAttribute;
			if (parameterBindingAttribute != null)
			{
				return parameterBindingAttribute.GetBinding(parameter);
			}
			ParameterBindingRulesCollection parameterBindingRules = parameter.Configuration.ParameterBindingRules;
			if (parameterBindingRules != null)
			{
				HttpParameterBinding httpParameterBinding = parameterBindingRules.LookupBinding(parameter);
				if (httpParameterBinding != null)
				{
					return httpParameterBinding;
				}
			}
			if (TypeHelper.CanConvertFromString(parameter.ParameterType))
			{
				return parameter.BindWithAttribute(new FromUriAttribute());
			}
			parameterBindingAttribute = new FromBodyAttribute();
			return parameterBindingAttribute.GetBinding(parameter);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000756C File Offset: 0x0000576C
		internal static ParameterBindingRulesCollection GetDefaultParameterBinders()
		{
			ParameterBindingRulesCollection parameterBindingRulesCollection = new ParameterBindingRulesCollection();
			parameterBindingRulesCollection.Add(typeof(CancellationToken), (HttpParameterDescriptor parameter) => new CancellationTokenParameterBinding(parameter));
			parameterBindingRulesCollection.Add(typeof(HttpRequestMessage), (HttpParameterDescriptor parameter) => new HttpRequestParameterBinding(parameter));
			parameterBindingRulesCollection.Add(delegate(HttpParameterDescriptor parameter)
			{
				if (!typeof(HttpContent).IsAssignableFrom(parameter.ParameterType))
				{
					return null;
				}
				return parameter.BindAsError(Error.Format(SRResources.ParameterBindingIllegalType, new object[]
				{
					parameter.ParameterType.Name,
					parameter.ParameterName
				}));
			});
			return parameterBindingRulesCollection;
		}
	}
}
