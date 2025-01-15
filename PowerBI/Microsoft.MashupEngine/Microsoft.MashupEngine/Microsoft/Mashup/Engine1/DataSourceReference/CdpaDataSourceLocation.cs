using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018D3 RID: 6355
	internal sealed class CdpaDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A20B RID: 41483 RVA: 0x0021A07A File Offset: 0x0021827A
		public CdpaDataSourceLocation()
		{
			base.Protocol = "cdpa";
		}

		// Token: 0x1700296E RID: 10606
		// (get) Token: 0x0600A20C RID: 41484 RVA: 0x0021A08D File Offset: 0x0021828D
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return CdpaDataSourceLocation.displayAddressFields;
			}
		}

		// Token: 0x1700296F RID: 10607
		// (get) Token: 0x0600A20D RID: 41485 RVA: 0x0021A094 File Offset: 0x00218294
		public override string FriendlyName
		{
			get
			{
				IResource resource;
				if (this.TryGetResource(out resource))
				{
					return resource.Path;
				}
				return base.FriendlyName;
			}
		}

		// Token: 0x17002970 RID: 10608
		// (get) Token: 0x0600A20E RID: 41486 RVA: 0x0014A82D File Offset: 0x00148A2D
		public override string ResourceKind
		{
			get
			{
				return "CDPA";
			}
		}

		// Token: 0x17002971 RID: 10609
		// (get) Token: 0x0600A20F RID: 41487 RVA: 0x0021A0B8 File Offset: 0x002182B8
		// (set) Token: 0x0600A210 RID: 41488 RVA: 0x0021A0CA File Offset: 0x002182CA
		public string TenantUri
		{
			get
			{
				return base.Address.GetStringOrNull("url");
			}
			set
			{
				base.Address["url"] = value;
			}
		}

		// Token: 0x0600A211 RID: 41489 RVA: 0x0021A0DD File Offset: 0x002182DD
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			if (this.TenantUri != null)
			{
				return new FormulaCreationResult(ExpressionBuilder.Instance.Invoke("Cdpa.Database", 1, new object[] { TextValue.New(this.TenantUri) }));
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
		}

		// Token: 0x0600A212 RID: 41490 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x040054B0 RID: 21680
		public static readonly DataSourceLocationFactory Factory = new CdpaDataSourceLocation.DslFactory();

		// Token: 0x040054B1 RID: 21681
		private static readonly string[] displayAddressFields = new string[] { "url" };

		// Token: 0x020018D4 RID: 6356
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002972 RID: 10610
			// (get) Token: 0x0600A214 RID: 41492 RVA: 0x0021A140 File Offset: 0x00218340
			public override string Protocol
			{
				get
				{
					return "cdpa";
				}
			}

			// Token: 0x0600A215 RID: 41493 RVA: 0x0021A147 File Offset: 0x00218347
			public override IDataSourceLocation New()
			{
				return new CdpaDataSourceLocation();
			}

			// Token: 0x0600A216 RID: 41494 RVA: 0x0021A14E File Offset: 0x0021834E
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("cdpa", resourcePath, out location);
			}
		}
	}
}
