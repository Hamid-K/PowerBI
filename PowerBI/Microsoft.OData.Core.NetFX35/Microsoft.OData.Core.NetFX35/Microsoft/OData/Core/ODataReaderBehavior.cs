using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000199 RID: 409
	internal sealed class ODataReaderBehavior
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x000357C6 File Offset: 0x000339C6
		private ODataReaderBehavior(ODataBehaviorKind formatBehaviorKind, ODataBehaviorKind apiBehaviorKind, bool allowDuplicatePropertyNames, Func<IEdmType, string, IEdmType> typeResolver)
		{
			this.formatBehaviorKind = formatBehaviorKind;
			this.apiBehaviorKind = apiBehaviorKind;
			this.allowDuplicatePropertyNames = allowDuplicatePropertyNames;
			this.typeResolver = typeResolver;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x000357EB File Offset: 0x000339EB
		internal static ODataReaderBehavior DefaultBehavior
		{
			get
			{
				return ODataReaderBehavior.defaultReaderBehavior;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000357F2 File Offset: 0x000339F2
		internal bool AllowDuplicatePropertyNames
		{
			get
			{
				return this.allowDuplicatePropertyNames;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000357FA File Offset: 0x000339FA
		internal Func<IEdmType, string, IEdmType> TypeResolver
		{
			get
			{
				return this.typeResolver;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00035802 File Offset: 0x00033A02
		internal ODataBehaviorKind FormatBehaviorKind
		{
			get
			{
				return this.formatBehaviorKind;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003580A File Offset: 0x00033A0A
		internal ODataBehaviorKind ApiBehaviorKind
		{
			get
			{
				return this.apiBehaviorKind;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00035812 File Offset: 0x00033A12
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0003581A File Offset: 0x00033A1A
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

		// Token: 0x06000F61 RID: 3937 RVA: 0x00035823 File Offset: 0x00033A23
		internal static ODataReaderBehavior CreateWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver)
		{
			return new ODataReaderBehavior(ODataBehaviorKind.WcfDataServicesClient, ODataBehaviorKind.WcfDataServicesClient, true, typeResolver);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003582E File Offset: 0x00033A2E
		internal static ODataReaderBehavior CreateWcfDataServicesServerBehavior()
		{
			return new ODataReaderBehavior(ODataBehaviorKind.ODataServer, ODataBehaviorKind.ODataServer, true, null);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00035839 File Offset: 0x00033A39
		internal void ResetFormatBehavior()
		{
			this.formatBehaviorKind = ODataBehaviorKind.Default;
			this.allowDuplicatePropertyNames = false;
			this.operationsBoundToEntityTypeMustBeContainerQualified = null;
		}

		// Token: 0x040006B4 RID: 1716
		private static readonly ODataReaderBehavior defaultReaderBehavior = new ODataReaderBehavior(ODataBehaviorKind.Default, ODataBehaviorKind.Default, false, null);

		// Token: 0x040006B5 RID: 1717
		private readonly ODataBehaviorKind apiBehaviorKind;

		// Token: 0x040006B6 RID: 1718
		private readonly Func<IEdmType, string, IEdmType> typeResolver;

		// Token: 0x040006B7 RID: 1719
		private bool allowDuplicatePropertyNames;

		// Token: 0x040006B8 RID: 1720
		private ODataBehaviorKind formatBehaviorKind;

		// Token: 0x040006B9 RID: 1721
		private Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified;
	}
}
