using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000665 RID: 1637
	internal sealed class OdbcStatementRegistrar : IOdbcStatementRegistrar
	{
		// Token: 0x060033AB RID: 13227 RVA: 0x000A54E4 File Offset: 0x000A36E4
		public static IOdbcStatementRegistrar New(OdbcOptions options, IEngineHost host)
		{
			if (!options.CancelQueryExplicitly)
			{
				return OdbcStatementRegistrar.DummyRegistrar;
			}
			return new OdbcStatementRegistrar(host);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000A5507 File Offset: 0x000A3707
		private OdbcStatementRegistrar(IEngineHost host)
		{
			this.cancellationService = ((host != null) ? host.QueryService<ICancellationService>() : null);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000A5521 File Offset: 0x000A3721
		public OdbcStatementRegistration Register(OdbcStatementHandle statement)
		{
			return new OdbcStatementRegistration(this.cancellationService, statement);
		}

		// Token: 0x040016FE RID: 5886
		public static readonly IOdbcStatementRegistrar DummyRegistrar = new OdbcStatementRegistrar.DummyOdbcStatementRegistrar();

		// Token: 0x040016FF RID: 5887
		private readonly ICancellationService cancellationService;

		// Token: 0x02000666 RID: 1638
		private sealed class DummyOdbcStatementRegistrar : IOdbcStatementRegistrar
		{
			// Token: 0x060033AF RID: 13231 RVA: 0x000A553B File Offset: 0x000A373B
			public OdbcStatementRegistration Register(OdbcStatementHandle statement)
			{
				return new OdbcStatementRegistration(OdbcStatementRegistrar.DummyOdbcStatementRegistrar.DummyCancellationService.Instance, statement);
			}

			// Token: 0x02000667 RID: 1639
			private sealed class DummyCancellationService : ICancellationService, ITrackingService<ICancellable>
			{
				// Token: 0x060033B1 RID: 13233 RVA: 0x0000336E File Offset: 0x0000156E
				public void Register(ICancellable cancellable)
				{
				}

				// Token: 0x060033B2 RID: 13234 RVA: 0x0000336E File Offset: 0x0000156E
				public void Unregister(ICancellable cancellable)
				{
				}

				// Token: 0x060033B3 RID: 13235 RVA: 0x00002105 File Offset: 0x00000305
				public int CancelAll()
				{
					return 0;
				}

				// Token: 0x04001700 RID: 5888
				public static readonly ICancellationService Instance = new OdbcStatementRegistrar.DummyOdbcStatementRegistrar.DummyCancellationService();
			}
		}
	}
}
