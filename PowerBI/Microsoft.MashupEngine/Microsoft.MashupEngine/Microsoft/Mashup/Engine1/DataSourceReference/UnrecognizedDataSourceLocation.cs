using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001913 RID: 6419
	internal sealed class UnrecognizedDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A3AD RID: 41901 RVA: 0x0021DC8C File Offset: 0x0021BE8C
		public UnrecognizedDataSourceLocation(string protocol)
		{
			base.Protocol = protocol;
		}

		// Token: 0x0600A3AE RID: 41902 RVA: 0x0021DC9B File Offset: 0x0021BE9B
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.UnrecognizedDataSourceType, null);
		}

		// Token: 0x0600A3AF RID: 41903 RVA: 0x000E6755 File Offset: 0x000E4955
		public override bool TryGetResource(out IResource resource)
		{
			resource = null;
			return false;
		}

		// Token: 0x0600A3B0 RID: 41904 RVA: 0x0021DCA4 File Offset: 0x0021BEA4
		public static DataSourceLocationFactory Factory(string protocol)
		{
			return new UnrecognizedDataSourceLocation.DslFactory(protocol);
		}

		// Token: 0x02001914 RID: 6420
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x0600A3B1 RID: 41905 RVA: 0x0021DCAC File Offset: 0x0021BEAC
			public DslFactory(string protocol)
			{
				this.protocol = protocol;
			}

			// Token: 0x170029E2 RID: 10722
			// (get) Token: 0x0600A3B2 RID: 41906 RVA: 0x0021DCBB File Offset: 0x0021BEBB
			public override string Protocol
			{
				get
				{
					return this.protocol;
				}
			}

			// Token: 0x0600A3B3 RID: 41907 RVA: 0x0021DCC3 File Offset: 0x0021BEC3
			public override IDataSourceLocation New()
			{
				return new UnrecognizedDataSourceLocation(this.protocol);
			}

			// Token: 0x0400550E RID: 21774
			private readonly string protocol;
		}
	}
}
