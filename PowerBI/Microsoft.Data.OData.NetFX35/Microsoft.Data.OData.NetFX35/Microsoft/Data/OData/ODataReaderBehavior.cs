using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001F1 RID: 497
	internal sealed class ODataReaderBehavior
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x00033F13 File Offset: 0x00032113
		private ODataReaderBehavior(ODataBehaviorKind formatBehaviorKind, ODataBehaviorKind apiBehaviorKind, bool allowDuplicatePropertyNames, bool usesV1Provider, Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme)
		{
			this.formatBehaviorKind = formatBehaviorKind;
			this.apiBehaviorKind = apiBehaviorKind;
			this.allowDuplicatePropertyNames = allowDuplicatePropertyNames;
			this.usesV1Provider = usesV1Provider;
			this.typeResolver = typeResolver;
			this.odataNamespace = odataNamespace;
			this.typeScheme = typeScheme;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00033F50 File Offset: 0x00032150
		internal static ODataReaderBehavior DefaultBehavior
		{
			get
			{
				return ODataReaderBehavior.defaultReaderBehavior;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00033F57 File Offset: 0x00032157
		internal string ODataTypeScheme
		{
			get
			{
				return this.typeScheme;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00033F5F File Offset: 0x0003215F
		internal string ODataNamespace
		{
			get
			{
				return this.odataNamespace;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00033F67 File Offset: 0x00032167
		internal bool AllowDuplicatePropertyNames
		{
			get
			{
				return this.allowDuplicatePropertyNames;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00033F6F File Offset: 0x0003216F
		internal bool UseV1ProviderBehavior
		{
			get
			{
				return this.usesV1Provider;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00033F77 File Offset: 0x00032177
		internal Func<IEdmType, string, IEdmType> TypeResolver
		{
			get
			{
				return this.typeResolver;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00033F7F File Offset: 0x0003217F
		internal ODataBehaviorKind FormatBehaviorKind
		{
			get
			{
				return this.formatBehaviorKind;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00033F87 File Offset: 0x00032187
		internal ODataBehaviorKind ApiBehaviorKind
		{
			get
			{
				return this.apiBehaviorKind;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00033F8F File Offset: 0x0003218F
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00033F97 File Offset: 0x00032197
		internal Func<IEdmEntityType, bool> OperationsBoundToEntityTypeMustBeContainerQualified
		{
			get
			{
				return this.operationsBoundToEntityTypeMustBeContainerQualified;
			}
			set
			{
				this.operationsBoundToEntityTypeMustBeContainerQualified = value;
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00033FA0 File Offset: 0x000321A0
		internal static ODataReaderBehavior CreateWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme)
		{
			return new ODataReaderBehavior(ODataBehaviorKind.WcfDataServicesClient, ODataBehaviorKind.WcfDataServicesClient, true, false, typeResolver, odataNamespace, typeScheme);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00033FAE File Offset: 0x000321AE
		internal static ODataReaderBehavior CreateWcfDataServicesServerBehavior(bool usesV1Provider)
		{
			return new ODataReaderBehavior(ODataBehaviorKind.WcfDataServicesServer, ODataBehaviorKind.WcfDataServicesServer, true, usesV1Provider, null, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00033FC4 File Offset: 0x000321C4
		internal void ResetFormatBehavior()
		{
			this.formatBehaviorKind = ODataBehaviorKind.Default;
			this.allowDuplicatePropertyNames = false;
			this.usesV1Provider = false;
			this.odataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices";
			this.typeScheme = "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme";
			this.operationsBoundToEntityTypeMustBeContainerQualified = null;
		}

		// Token: 0x04000554 RID: 1364
		private static readonly ODataReaderBehavior defaultReaderBehavior = new ODataReaderBehavior(ODataBehaviorKind.Default, ODataBehaviorKind.Default, false, false, null, "http://schemas.microsoft.com/ado/2007/08/dataservices", "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");

		// Token: 0x04000555 RID: 1365
		private readonly ODataBehaviorKind apiBehaviorKind;

		// Token: 0x04000556 RID: 1366
		private readonly Func<IEdmType, string, IEdmType> typeResolver;

		// Token: 0x04000557 RID: 1367
		private bool allowDuplicatePropertyNames;

		// Token: 0x04000558 RID: 1368
		private bool usesV1Provider;

		// Token: 0x04000559 RID: 1369
		private string typeScheme;

		// Token: 0x0400055A RID: 1370
		private string odataNamespace;

		// Token: 0x0400055B RID: 1371
		private ODataBehaviorKind formatBehaviorKind;

		// Token: 0x0400055C RID: 1372
		private Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified;
	}
}
