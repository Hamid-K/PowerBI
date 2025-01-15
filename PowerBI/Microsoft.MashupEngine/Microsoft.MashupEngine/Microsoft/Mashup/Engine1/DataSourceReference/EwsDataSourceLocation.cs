using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018DB RID: 6363
	internal sealed class EwsDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A240 RID: 41536 RVA: 0x0021A4D3 File Offset: 0x002186D3
		public EwsDataSourceLocation()
		{
			base.Protocol = "ews";
		}

		// Token: 0x17002981 RID: 10625
		// (get) Token: 0x0600A241 RID: 41537 RVA: 0x00118D3D File Offset: 0x00116F3D
		public override string ResourceKind
		{
			get
			{
				return "Exchange";
			}
		}

		// Token: 0x17002982 RID: 10626
		// (get) Token: 0x0600A242 RID: 41538 RVA: 0x00118D3D File Offset: 0x00116F3D
		public override string FriendlyName
		{
			get
			{
				return "Exchange";
			}
		}

		// Token: 0x0600A243 RID: 41539 RVA: 0x0021A4E8 File Offset: 0x002186E8
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			try
			{
				OptionRecordDefinition.Empty.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string stringOrNull = base.Address.GetStringOrNull("emailAddress");
			if (stringOrNull != null)
			{
				return DataSourceLocation.FormatInvocation("Exchange.Contents", 1, new object[] { stringOrNull });
			}
			return DataSourceLocation.FormatInvocation("Exchange.Contents", 0, Array.Empty<object>());
		}

		// Token: 0x0600A244 RID: 41540 RVA: 0x0021A560 File Offset: 0x00218760
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("emailAddress");
			if (stringOrNull == null)
			{
				return base.TryGetSingletonResource(out resource);
			}
			resource = Resource.New(this.ResourceKind, stringOrNull);
			return true;
		}

		// Token: 0x040054BA RID: 21690
		public static readonly DataSourceLocationFactory Factory = new EwsDataSourceLocation.DslFactory();

		// Token: 0x040054BB RID: 21691
		private const string Exchange_Contents_Function = "Exchange.Contents";

		// Token: 0x020018DC RID: 6364
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002983 RID: 10627
			// (get) Token: 0x0600A246 RID: 41542 RVA: 0x0021A5A4 File Offset: 0x002187A4
			public override string Protocol
			{
				get
				{
					return "ews";
				}
			}

			// Token: 0x0600A247 RID: 41543 RVA: 0x0021A5AB File Offset: 0x002187AB
			public override IDataSourceLocation New()
			{
				return new EwsDataSourceLocation();
			}

			// Token: 0x0600A248 RID: 41544 RVA: 0x0021A5B2 File Offset: 0x002187B2
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				location = new EwsDataSourceLocation
				{
					Address = new Dictionary<string, object> { 
					{
						"emailAddress",
						(resourcePath == "Exchange") ? null : resourcePath
					} }
				};
				return true;
			}
		}
	}
}
