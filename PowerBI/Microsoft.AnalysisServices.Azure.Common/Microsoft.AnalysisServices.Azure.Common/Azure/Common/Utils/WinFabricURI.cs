using System;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000149 RID: 329
	public sealed class WinFabricURI
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x0004705D File Offset: 0x0004525D
		public WinFabricURI()
		{
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0004707C File Offset: 0x0004527C
		public WinFabricURI(Uri serviceUri)
		{
			ExtendedDiagnostics.EnsureNotNull<Uri>(serviceUri, "Service Uri cannot be null");
			ExtendedDiagnostics.EnsureOperation(WinFabricURI.IsValidServiceURI(serviceUri), "Invalid Service Uri:'{0}'".FormatWithInvariantCulture(new object[] { serviceUri }));
			string text = serviceUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
			int num = 0;
			while (num < serviceUri.Segments.Count<string>() && num < 4)
			{
				text += serviceUri.Segments[num];
				num++;
			}
			this.ServiceName = text;
			if (this.IsValidDBRelatedServiceURI(serviceUri))
			{
				this.ServiceID = serviceUri.Segments[4];
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00047120 File Offset: 0x00045320
		public WinFabricURI(string serviceName, string serviceID)
		{
			this.ServiceName = serviceName;
			this.ServiceID = serviceID;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004714C File Offset: 0x0004534C
		public Uri ServiceURI
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.ServiceName))
				{
					return null;
				}
				return new Uri("{0}{2}{1}".FormatWithInvariantCulture(new object[] { this.ServiceName, this.ServiceID, '/' }).TrimEnd(new char[] { '/' }));
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x000471A9 File Offset: 0x000453A9
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x000471B1 File Offset: 0x000453B1
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
			set
			{
				this.serviceName = WinFabricURI.NormalizePartName(value, false);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x000471C0 File Offset: 0x000453C0
		public string ServiceType
		{
			get
			{
				string text = this.serviceName.Trim(WinFabricURI.CHARS_TO_STRIP);
				return text.Substring(text.LastIndexOf('/')).Trim(WinFabricURI.CHARS_TO_STRIP);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x000471E9 File Offset: 0x000453E9
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x000471F1 File Offset: 0x000453F1
		public string ServiceID
		{
			get
			{
				return this.serviceId;
			}
			private set
			{
				this.serviceId = WinFabricURI.NormalizePartName(value, false);
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00047200 File Offset: 0x00045400
		private static bool IsValidServiceURI(Uri serviceUri)
		{
			return !(serviceUri == null) && serviceUri.Segments.Count<string>() >= 4;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00047220 File Offset: 0x00045420
		private bool IsValidDBRelatedServiceURI(Uri serviceUri)
		{
			return !(serviceUri == null) && serviceUri.Segments.Count<string>() == 5 && (this.ServiceType.Equals(25.ToString(), StringComparison.OrdinalIgnoreCase) || this.ServiceType.Equals(26.ToString(), StringComparison.OrdinalIgnoreCase) || this.ServiceType.Equals(28.ToString(), StringComparison.OrdinalIgnoreCase) || this.ServiceType.Equals(27.ToString(), StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000472C0 File Offset: 0x000454C0
		private static string NormalizePartName(string partName, bool maintainCase = false)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(partName, "Part name cannot be null or empty");
			ExtendedDiagnostics.EnsureOperation(partName.IndexOfAny(WinFabricURI.INVALID_PART_CHARS) == -1, "Part name contains invalid characters");
			if (!maintainCase)
			{
				return CommonUtils.NormalizeCase(partName).Trim(WinFabricURI.CHARS_TO_STRIP);
			}
			return partName.Trim(WinFabricURI.CHARS_TO_STRIP);
		}

		// Token: 0x040003FB RID: 1019
		private const int MIN_SERVICE_URI_SEGMENT_COUNT = 4;

		// Token: 0x040003FC RID: 1020
		private const int DB_RELATED_SERVICE_URI_SEGMENT_COUNT = 5;

		// Token: 0x040003FD RID: 1021
		private const int SERVICE_ID_SEGMENT_INDEX = 4;

		// Token: 0x040003FE RID: 1022
		private const char SERVICE_URI_DELIMITER = '/';

		// Token: 0x040003FF RID: 1023
		private static readonly char[] CHARS_TO_STRIP = new char[] { '/' };

		// Token: 0x04000400 RID: 1024
		private static readonly char[] INVALID_PART_CHARS = new char[] { ' ' };

		// Token: 0x04000401 RID: 1025
		private string serviceName = string.Empty;

		// Token: 0x04000402 RID: 1026
		private string serviceId = string.Empty;
	}
}
