using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface.DataSourceReference
{
	// Token: 0x02000145 RID: 325
	public interface IDataSourceLocation : IEquatable<IDataSourceLocation>, IComparable<IDataSourceLocation>
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600059C RID: 1436
		string Protocol { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600059D RID: 1437
		// (set) Token: 0x0600059E RID: 1438
		IDictionary<string, object> Address { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600059F RID: 1439
		// (set) Token: 0x060005A0 RID: 1440
		string Authentication { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005A1 RID: 1441
		// (set) Token: 0x060005A2 RID: 1442
		string Query { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005A3 RID: 1443
		string ResourceKind { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005A4 RID: 1444
		string FriendlyName { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005A5 RID: 1445
		IEnumerable<string> DisplayAddressFields { get; }

		// Token: 0x060005A6 RID: 1446
		string ToJson();

		// Token: 0x060005A7 RID: 1447
		string ToJson(DataSourceLocationFormat dataSourceLocationFormat);

		// Token: 0x060005A8 RID: 1448
		IFormulaCreationResult CreateFormula(string optionsJson);

		// Token: 0x060005A9 RID: 1449
		IFormulaCreationResult CreateFormula(bool validateAuthentication = false);

		// Token: 0x060005AA RID: 1450
		string GetAddressFieldLabel(string addressFieldName);

		// Token: 0x060005AB RID: 1451
		bool TryResolve(out IDataSourceLocation resolvedLocation);

		// Token: 0x060005AC RID: 1452
		bool TryGetResource(out IResource resource);

		// Token: 0x060005AD RID: 1453
		void Normalize();
	}
}
