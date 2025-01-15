using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000176 RID: 374
	public class LengthRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00019309 File Offset: 0x00017509
		public LengthRouteConstraint(int length)
		{
			if (length < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("length", length, 0);
			}
			this.Length = new int?(length);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00019338 File Offset: 0x00017538
		public LengthRouteConstraint(int minLength, int maxLength)
		{
			if (minLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("minLength", minLength, 0);
			}
			if (maxLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxLength", maxLength, 0);
			}
			this.MinLength = new int?(minLength);
			this.MaxLength = new int?(maxLength);
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00019399 File Offset: 0x00017599
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x000193A1 File Offset: 0x000175A1
		public int? Length { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000193AA File Offset: 0x000175AA
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x000193B2 File Offset: 0x000175B2
		public int? MinLength { get; private set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000193BB File Offset: 0x000175BB
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x000193C3 File Offset: 0x000175C3
		public int? MaxLength { get; private set; }

		// Token: 0x060009CD RID: 2509 RVA: 0x000193CC File Offset: 0x000175CC
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			object obj;
			if (!values.TryGetValue(parameterName, out obj) || obj == null)
			{
				return false;
			}
			int length = Convert.ToString(obj, CultureInfo.InvariantCulture).Length;
			if (this.Length != null)
			{
				return length == this.Length.Value;
			}
			return length >= this.MinLength.Value && length <= this.MaxLength.Value;
		}
	}
}
