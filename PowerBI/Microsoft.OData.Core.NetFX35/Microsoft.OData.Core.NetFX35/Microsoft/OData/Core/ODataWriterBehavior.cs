using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A9 RID: 425
	internal sealed class ODataWriterBehavior
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x000364E4 File Offset: 0x000346E4
		private ODataWriterBehavior(ODataBehaviorKind formatBehaviorKind, ODataBehaviorKind apiBehaviorKind, bool allowNullValuesForNonNullablePrimitiveTypes, bool allowDuplicatePropertyNames)
		{
			this.formatBehaviorKind = formatBehaviorKind;
			this.apiBehaviorKind = apiBehaviorKind;
			this.allowNullValuesForNonNullablePrimitiveTypes = allowNullValuesForNonNullablePrimitiveTypes;
			this.allowDuplicatePropertyNames = allowDuplicatePropertyNames;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00036509 File Offset: 0x00034709
		internal static ODataWriterBehavior DefaultBehavior
		{
			get
			{
				return ODataWriterBehavior.defaultWriterBehavior;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00036510 File Offset: 0x00034710
		internal bool AllowNullValuesForNonNullablePrimitiveTypes
		{
			get
			{
				return this.allowNullValuesForNonNullablePrimitiveTypes;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00036518 File Offset: 0x00034718
		internal bool AllowDuplicatePropertyNames
		{
			get
			{
				return this.allowDuplicatePropertyNames;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00036520 File Offset: 0x00034720
		internal ODataBehaviorKind FormatBehaviorKind
		{
			get
			{
				return this.formatBehaviorKind;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00036528 File Offset: 0x00034728
		internal ODataBehaviorKind ApiBehaviorKind
		{
			get
			{
				return this.apiBehaviorKind;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00036530 File Offset: 0x00034730
		internal static ODataWriterBehavior CreateWcfDataServicesClientBehavior()
		{
			return new ODataWriterBehavior(ODataBehaviorKind.WcfDataServicesClient, ODataBehaviorKind.WcfDataServicesClient, false, false);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003653B File Offset: 0x0003473B
		internal static ODataWriterBehavior CreateODataServerBehavior()
		{
			return new ODataWriterBehavior(ODataBehaviorKind.ODataServer, ODataBehaviorKind.ODataServer, true, true);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00036546 File Offset: 0x00034746
		internal void UseDefaultFormatBehavior()
		{
			this.formatBehaviorKind = ODataBehaviorKind.Default;
			this.allowNullValuesForNonNullablePrimitiveTypes = false;
			this.allowDuplicatePropertyNames = false;
		}

		// Token: 0x040006F4 RID: 1780
		private static readonly ODataWriterBehavior defaultWriterBehavior = new ODataWriterBehavior(ODataBehaviorKind.Default, ODataBehaviorKind.Default, false, false);

		// Token: 0x040006F5 RID: 1781
		private readonly ODataBehaviorKind apiBehaviorKind;

		// Token: 0x040006F6 RID: 1782
		private bool allowNullValuesForNonNullablePrimitiveTypes;

		// Token: 0x040006F7 RID: 1783
		private bool allowDuplicatePropertyNames;

		// Token: 0x040006F8 RID: 1784
		private ODataBehaviorKind formatBehaviorKind;
	}
}
