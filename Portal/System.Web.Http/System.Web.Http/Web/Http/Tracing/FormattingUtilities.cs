using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.Routing;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000120 RID: 288
	internal static class FormattingUtilities
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x000132F8 File Offset: 0x000114F8
		public static string ActionArgumentsToString(IDictionary<string, object> actionArguments)
		{
			return string.Join(", ", actionArguments.Keys.Select((string k) => k + "=" + FormattingUtilities.ValueToString(actionArguments[k], CultureInfo.CurrentCulture)));
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00013338 File Offset: 0x00011538
		public static string ActionDescriptorToString(HttpActionDescriptor actionDescriptor)
		{
			string text = string.Join(", ", from p in actionDescriptor.GetParameters()
				select p.ParameterType.Name + " " + p.ParameterName);
			return actionDescriptor.ActionName + "(" + text + ")";
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00013390 File Offset: 0x00011590
		public static string ActionInvokeToString(HttpActionContext actionContext)
		{
			return FormattingUtilities.ActionInvokeToString(actionContext.ActionDescriptor.ActionName, actionContext.ActionArguments);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000133A8 File Offset: 0x000115A8
		public static string ActionInvokeToString(string actionName, IDictionary<string, object> arguments)
		{
			return actionName + "(" + FormattingUtilities.ActionArgumentsToString(arguments) + ")";
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000133C0 File Offset: 0x000115C0
		public static string FormattersToString(IEnumerable<MediaTypeFormatter> formatters)
		{
			return string.Join(", ", formatters.Select((MediaTypeFormatter f) => f.GetType().Name));
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000133F4 File Offset: 0x000115F4
		public static string ModelBinderToString(ModelBinderProvider provider)
		{
			CompositeModelBinderProvider compositeModelBinderProvider = provider as CompositeModelBinderProvider;
			if (compositeModelBinderProvider == null)
			{
				return provider.GetType().Name;
			}
			string text = string.Join(", ", compositeModelBinderProvider.Providers.Select(new Func<ModelBinderProvider, string>(FormattingUtilities.ModelBinderToString)));
			return provider.GetType().Name + "(" + text + ")";
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00013454 File Offset: 0x00011654
		public static string ModelStateToString(ModelStateDictionary modelState)
		{
			if (modelState.IsValid)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in modelState.Keys)
			{
				ModelState modelState2 = modelState[text];
				if (modelState2.Errors.Count > 0)
				{
					foreach (ModelError modelError in modelState2.Errors)
					{
						string text2 = Error.Format(SRResources.TraceModelStateErrorMessage, new object[] { text, modelError.ErrorMessage });
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(text2);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00013548 File Offset: 0x00011748
		public static string RouteToString(IHttpRouteData routeData)
		{
			return string.Join(",", routeData.Values.Select((KeyValuePair<string, object> pair) => Error.Format("{0}:{1}", new object[] { pair.Key, pair.Value })));
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00013580 File Offset: 0x00011780
		public static string ValueProviderToString(IValueProvider provider)
		{
			CompositeValueProvider compositeValueProvider = provider as CompositeValueProvider;
			if (compositeValueProvider == null)
			{
				return provider.GetType().Name;
			}
			string text = string.Join(", ", compositeValueProvider.Select(new Func<IValueProvider, string>(FormattingUtilities.ValueProviderToString)));
			return provider.GetType().Name + "(" + text + ")";
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000135DB File Offset: 0x000117DB
		public static string ValueToString(object value, CultureInfo cultureInfo)
		{
			if (value == null)
			{
				return FormattingUtilities.NullMessage;
			}
			return Convert.ToString(value, cultureInfo);
		}

		// Token: 0x04000205 RID: 517
		public static readonly string NullMessage = "null";
	}
}
