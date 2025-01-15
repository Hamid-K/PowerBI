using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001F2 RID: 498
	internal sealed class ODataWriterBehavior
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x00034013 File Offset: 0x00032213
		private ODataWriterBehavior(ODataBehaviorKind formatBehaviorKind, ODataBehaviorKind apiBehaviorKind, bool usesV1Provider, bool allowNullValuesForNonNullablePrimitiveTypes, bool allowDuplicatePropertyNames, string odataNamespace, string typeScheme)
		{
			this.formatBehaviorKind = formatBehaviorKind;
			this.apiBehaviorKind = apiBehaviorKind;
			this.usesV1Provider = usesV1Provider;
			this.allowNullValuesForNonNullablePrimitiveTypes = allowNullValuesForNonNullablePrimitiveTypes;
			this.allowDuplicatePropertyNames = allowDuplicatePropertyNames;
			this.odataNamespace = odataNamespace;
			this.typeScheme = typeScheme;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00034050 File Offset: 0x00032250
		internal static ODataWriterBehavior DefaultBehavior
		{
			get
			{
				return ODataWriterBehavior.defaultWriterBehavior;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00034057 File Offset: 0x00032257
		internal string ODataTypeScheme
		{
			get
			{
				return this.typeScheme;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003405F File Offset: 0x0003225F
		internal string ODataNamespace
		{
			get
			{
				return this.odataNamespace;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00034067 File Offset: 0x00032267
		internal bool UseV1ProviderBehavior
		{
			get
			{
				return this.usesV1Provider;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0003406F File Offset: 0x0003226F
		internal bool AllowNullValuesForNonNullablePrimitiveTypes
		{
			get
			{
				return this.allowNullValuesForNonNullablePrimitiveTypes;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00034077 File Offset: 0x00032277
		internal bool AllowDuplicatePropertyNames
		{
			get
			{
				return this.allowDuplicatePropertyNames;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003407F File Offset: 0x0003227F
		internal ODataBehaviorKind FormatBehaviorKind
		{
			get
			{
				return this.formatBehaviorKind;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00034087 File Offset: 0x00032287
		internal ODataBehaviorKind ApiBehaviorKind
		{
			get
			{
				return this.apiBehaviorKind;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003408F File Offset: 0x0003228F
		internal static ODataWriterBehavior CreateWcfDataServicesClientBehavior(string odataNamespace, string typeScheme)
		{
			return new ODataWriterBehavior(ODataBehaviorKind.WcfDataServicesClient, ODataBehaviorKind.WcfDataServicesClient, false, false, false, odataNamespace, typeScheme);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003409D File Offset: 0x0003229D
		internal static ODataWriterBehavior CreateWcfDataServicesServerBehavior(bool usesV1Provider)
		{
			return new ODataWriterBehavior(ODataBehaviorKind.WcfDataServicesServer, ODataBehaviorKind.WcfDataServicesServer, usesV1Provider, true, true, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000340B3 File Offset: 0x000322B3
		internal void UseDefaultFormatBehavior()
		{
			this.formatBehaviorKind = ODataBehaviorKind.Default;
			this.usesV1Provider = false;
			this.allowNullValuesForNonNullablePrimitiveTypes = false;
			this.allowDuplicatePropertyNames = false;
			this.odataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices";
			this.typeScheme = "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme";
		}

		// Token: 0x0400055D RID: 1373
		private static readonly ODataWriterBehavior defaultWriterBehavior = new ODataWriterBehavior(ODataBehaviorKind.Default, ODataBehaviorKind.Default, false, false, false, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");

		// Token: 0x0400055E RID: 1374
		private readonly ODataBehaviorKind apiBehaviorKind;

		// Token: 0x0400055F RID: 1375
		private bool usesV1Provider;

		// Token: 0x04000560 RID: 1376
		private bool allowNullValuesForNonNullablePrimitiveTypes;

		// Token: 0x04000561 RID: 1377
		private bool allowDuplicatePropertyNames;

		// Token: 0x04000562 RID: 1378
		private string typeScheme;

		// Token: 0x04000563 RID: 1379
		private string odataNamespace;

		// Token: 0x04000564 RID: 1380
		private ODataBehaviorKind formatBehaviorKind;
	}
}
