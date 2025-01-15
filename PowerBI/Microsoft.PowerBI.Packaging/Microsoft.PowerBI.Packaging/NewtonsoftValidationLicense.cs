using System;
using Newtonsoft.Json.Schema;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000020 RID: 32
	public static class NewtonsoftValidationLicense
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003B3C File Offset: 0x00001D3C
		public static void RegisterPowerBILicenseForNewtonsoftJsonSchema()
		{
			if (!NewtonsoftValidationLicense._isJschemaRegistered)
			{
				object licenseInitializationLock = NewtonsoftValidationLicense._licenseInitializationLock;
				lock (licenseInitializationLock)
				{
					if (!NewtonsoftValidationLicense._isJschemaRegistered)
					{
						License.RegisterLicense(NewtonsoftValidationLicense._jschemaLicense);
					}
					NewtonsoftValidationLicense._isJschemaRegistered = true;
				}
			}
		}

		// Token: 0x04000045 RID: 69
		private static readonly string _jschemaLicense = "3343-eeHKxW4k6F+ED9xjR9Dlo0vHRUGfcZ70GU50yFfJq2mJfZo6qe4/a2bfJdsbP79lIG4zWOsServFRvlCxcT+rvvhJ6sBTs6T3zlDxksLZLEFYGow5LOgNwxcklc0O5M8xxKZYlvgJeI1icFx/7O31jXvPfhxrE1ymnCzQtB6WYF7IklkIjozMzQzLCJFeHBpcnlEYXRlIjoiMjAxOC0wMi0xMlQwMDowMDowMCIsIlR5cGUiOiJKc29uU2NoZW1hU2l0ZSJ9";

		// Token: 0x04000046 RID: 70
		private static object _licenseInitializationLock = new object();

		// Token: 0x04000047 RID: 71
		private static bool _isJschemaRegistered = false;
	}
}
