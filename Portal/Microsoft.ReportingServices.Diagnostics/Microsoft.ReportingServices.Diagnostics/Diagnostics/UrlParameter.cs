using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000043 RID: 67
	internal sealed class UrlParameter : ApplicationParameter
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000A595 File Offset: 0x00008795
		public UrlParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, string defaultValue, UriKind uriKind)
			: base(parameterSource, tracer, name, configValue, defaultValue, "")
		{
			this.m_uriKind = uriKind;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A5B4 File Offset: 0x000087B4
		protected override bool Validate(string valueToValidate, out object validatedValue)
		{
			validatedValue = null;
			Uri uri;
			if (!Uri.TryCreate(valueToValidate, this.m_uriKind, out uri))
			{
				return false;
			}
			if (uri.IsAbsoluteUri && uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
			{
				return false;
			}
			validatedValue = uri.ToString();
			return true;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00003213 File Offset: 0x00001413
		public string Value
		{
			get
			{
				return (string)this.BaseValue;
			}
		}

		// Token: 0x04000207 RID: 519
		private UriKind m_uriKind;
	}
}
