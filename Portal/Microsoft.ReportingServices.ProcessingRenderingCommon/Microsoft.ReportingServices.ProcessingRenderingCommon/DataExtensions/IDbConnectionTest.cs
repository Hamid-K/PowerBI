using System;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x02000018 RID: 24
	internal interface IDbConnectionTest : IDbConnection, IDisposable, IExtension
	{
		// Token: 0x060000C9 RID: 201
		void TestConnection();
	}
}
