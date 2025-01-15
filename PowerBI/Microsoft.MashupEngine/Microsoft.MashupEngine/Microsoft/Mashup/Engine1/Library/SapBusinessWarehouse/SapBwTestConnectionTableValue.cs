using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004EF RID: 1263
	internal sealed class SapBwTestConnectionTableValue : DelegatingTableValue
	{
		// Token: 0x0600293D RID: 10557 RVA: 0x0007B663 File Offset: 0x00079863
		public SapBwTestConnectionTableValue(ISapBwService service, Func<SapBwService> testConnectionAltConstructor, TableValue tableValue)
			: base(tableValue)
		{
			this.service = service;
			this.testConnectionAltConstructor = testConnectionAltConstructor;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0007B67C File Offset: 0x0007987C
		public override void TestConnection()
		{
			if (this.testConnectionAltConstructor != null)
			{
				try
				{
					this.testConnectionAltConstructor().TestConnection();
					return;
				}
				catch (Exception ex)
				{
					if (ex is ResourceAccessAuthorizationException)
					{
						throw;
					}
					ValueException ex2 = ex as ValueException;
					if (ex2 != null)
					{
						Value value;
						if (!ex2.Value.TryGetValue(TextValue.New("DataSource.MissingClientLibrary"), out value))
						{
							throw;
						}
						this.service.TestConnection();
					}
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
			}
			this.service.TestConnection();
		}

		// Token: 0x040011B7 RID: 4535
		private readonly ISapBwService service;

		// Token: 0x040011B8 RID: 4536
		private readonly Func<SapBwService> testConnectionAltConstructor;
	}
}
